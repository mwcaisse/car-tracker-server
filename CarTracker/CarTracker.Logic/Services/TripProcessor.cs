using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Entities.Places;
using CarTracker.Common.Enums;
using CarTracker.Common.Exceptions;
using CarTracker.Common.Models;
using CarTracker.Common.Services;
using CarTracker.Common.Services.Places;
using CarTracker.Data;
using CarTracker.Data.Extensions;
using CarTracker.Logic.Util;
using Microsoft.EntityFrameworkCore.Design;

namespace CarTracker.Logic.Services
{
    public class TripProcessor : ITripProcessor
    {

        private readonly CarTrackerDbContext _db;
        private readonly IPlaceService _placeService;
        private readonly ITripService _tripService;
        private readonly ITripPossiblePlaceService _tripPossiblePlaceService;
        private readonly IServerLogger _logger;

        public TripProcessor(CarTrackerDbContext db, IPlaceService placeService,
            ITripService tripService, ITripPossiblePlaceService tripPossiblePlaceService, 
            IServerLogger logger)
        {
            this._db = db;
            this._placeService = placeService;
            this._tripService = tripService;
            this._tripPossiblePlaceService = tripPossiblePlaceService;
            this._logger = logger;
        }

        public void ProcessUnprocessedTrips()
        {
            var cutoffDate = DateTime.Now.AddMinutes(-30);

            var trips = _db.Trips.Where(t => t.Status != TripStatus.Processed)
                .Where(t => t.Status == TripStatus.Finished ||
                            t.Readings.OrderByDescending(r => r.ReadDate).FirstOrDefault().ReadDate < cutoffDate);             

            foreach (var trip in trips)
            {
                ProcessTrip(trip);
            }
        }

        public Trip ProcessTrip(long id)
        {
            var trip = _db.Trips.FirstOrDefault(t => t.TripId == id);
            if (null == trip)
            {
                throw new EntityValidationException("No Trip with the given Id exists!");
            }
            return ProcessTrip(trip);
        }

        protected Trip ProcessTrip(Trip trip)
        {
            _logger.Debug($"Started processing trip {trip.TripId}");

            if (trip.Status == TripStatus.Processed)
            {
                // Trip has already been processed. Clean up anything that was done previously
                RemovePossiblePlacesFromTrip(trip);
            }

            var readings = _db.Readings.Where(x => x.TripId == trip.TripId)
                .OrderBy(x => x.ReadDate)                       
                .ToList();

            if (readings.Any())
            {
                NormalizeStartEndLocations(readings);

                double maxSpeed = 0;
                double totalSpeed = 0;
                double totalDistance = 0;
                double maxEngineRpm = 0;
                double totalEngineRpm = 0;

                TimeSpan idleTime = new TimeSpan(); //ticks

                Reading previousReading = null;

                for (int i = 0; i < readings.Count; i++)
                {
                    var reading = readings[i];
                    if (reading.Speed.HasValue && reading.Speed.Value > maxSpeed)
                    {
                        maxSpeed = reading.Speed.Value;
                    }

                    if (reading.EngineRpm.HasValue && reading.EngineRpm.Value > maxEngineRpm)
                    {
                        maxEngineRpm = reading.EngineRpm.Value;
                    }

                    totalSpeed += reading.Speed ?? 0;
                    totalEngineRpm += reading.EngineRpm ?? 0;

                    if (null != previousReading)
                    {
                        totalDistance += CalculateDistanceBetweenReadings(previousReading, reading);

                        if ((previousReading.Speed ?? 0) == 0 && (reading.Speed ?? 0) == 0)
                        {
                            idleTime = idleTime.Add(reading.ReadDate - previousReading.ReadDate);
                        }
                    }

                    previousReading = reading;
                }

                trip.AverageSpeed = totalSpeed / (readings.Count * 1.0);
                trip.AverageEngineRpm = totalEngineRpm / (readings.Count * 1.0);

                trip.MaxEngineRpm = maxEngineRpm;
                trip.MaximumSpeed = maxSpeed;
                trip.DistanceTraveled = totalDistance;
                trip.IdleTime = Convert.ToInt64(idleTime.TotalMilliseconds);

                //if the end date of the trip is null, set its end date to the read date of the
                //  last reading
                if (!trip.EndDate.HasValue)
                {
                    trip.EndDate = readings.Last().ReadDate;
                }

                var car = _db.Cars.FirstOrDefault(c => c.CarId == trip.CarId);
                if (null != car &&
                    (!car.MileageLastUserSet.HasValue || car.MileageLastUserSet < trip.EndDate))
                {
                    car.Mileage += trip.DistanceTraveled;
                }

                _db.SaveChanges();

                AddPossiblePlacesToTrip(trip, readings);
                AddGuessedPlacesToTrip(trip);
            }

            trip.Status = TripStatus.Processed;
            _db.SaveChanges();

            _logger.Debug($"Finished processing trip {trip.TripId}");

            return trip;
        }

        protected void NormalizeStartEndLocations(IList<Reading> readings)
        {
            var firstReading = readings.First();
            Reading firstDifferentReading = null;

            for (int i = 1; i < readings.Count(); i++)
            {
                var reading = readings[i];
                if (reading.Latitude != firstReading.Latitude && reading.Longitude != firstReading.Longitude)
                {
                    firstDifferentReading = reading;
                    break;
                }
            }

            if (null !=firstDifferentReading && CalculateDistanceBetweenReadings(firstReading, firstDifferentReading) > 0.5)
            {
                firstReading.Longitude = firstDifferentReading.Longitude;
                firstReading.Latitude = firstDifferentReading.Latitude;
            }
        }

        protected double CalculateDistanceBetweenReadings(Reading prev, Reading current)
        {
            if (GeographyUtils.ToRadians(current.Latitude) == 0 || 
                GeographyUtils.ToRadians(prev.Latitude) == 0)
            {
                return 0; // if either of the latitudes are 0 then return 0. 0 is an incorrect reading
            }

            return GeographyUtils.CalculateDistanceBetweenLocations(prev.Latitude, prev.Longitude,
                current.Latitude, current.Longitude);
        }

        protected void AddPossiblePlacesToTrip(Trip trip, IEnumerable<Reading> readings)
        {
            _logger.Debug("Adding possible places to trip: " + trip.TripId);
            var nonZeroReadings = readings.Where(r => r.Latitude != 0 && r.Longitude != 0).ToList();
            if (!nonZeroReadings.Any())
            {
                _logger.Debug($"Trip {trip.TripId} has no non zero readings. Not adding any possible places.");
                return;
            }

            AddPossiblePlaceToTrip(trip, nonZeroReadings.First(), TripPossiblePlaceType.Start);
            AddPossiblePlaceToTrip(trip, nonZeroReadings.Last(), TripPossiblePlaceType.Destination);
        }

        protected void AddPossiblePlaceToTrip(Trip trip, Reading reading, TripPossiblePlaceType type)
        {
            var ownerId = trip.Car.OwnerId;
            var possiblePlaces = _placeService.GetPlacesNearby(reading.Latitude,
                reading.Longitude, 150, ownerId);
            foreach (var place in possiblePlaces)
            {
                var possiblePlace = new TripPossiblePlace()
                {
                    Trip = trip,
                    TripId = trip.TripId,
                    Place = place,
                    PlaceType = type,
                    Distance = Convert.ToDecimal(GeographyUtils.CalculateDistanceBetweenLocations(
                        reading.Latitude,reading.Longitude,place.Latitude,place.Longitude)),
                    Active = true
                };

                _tripPossiblePlaceService.Create(possiblePlace);
            }

            _logger.Debug($"Added possible {type.ToString()} to trip {trip.TripId}");
        }

        protected void RemovePossiblePlacesFromTrip(Trip trip)
        {
            var possiblePlaces = _db.TripPossiblePlaces.Active().Where(p => p.TripId == trip.TripId);
            foreach (var possiblePlace in possiblePlaces)
            {
                possiblePlace.Active = false; 
            }

            _logger.Debug("Removing possible places from trip: " + trip.TripId);

            _db.SaveChanges();
        }

        protected void AddGuessedPlacesToTrip(Trip trip)
        {
            var startPlace = GuessPlaceForTrip(trip, TripPossiblePlaceType.Start);
            var endPlace = GuessPlaceForTrip(trip, TripPossiblePlaceType.Destination);

            if (null != startPlace)
            {
                _tripService.SetStartingPlace(trip.TripId, startPlace.PlaceId, false);
            }
            if (null != endPlace)
            {
                _tripService.SetDestinationPlace(trip.TripId, endPlace.PlaceId, false);
            }
        }

        protected Place GuessPlaceForTrip(Trip trip, TripPossiblePlaceType type)
        {
            var possiblePlaces = _db.TripPossiblePlaces.Active()
                .Where(x => x.PlaceType == type && x.TripId == trip.TripId);
            var ownerId = trip.Car.OwnerId;

            var guessedPlaceId = _db.PlaceVisits.Where(x => x.OwnerId == ownerId && x.PlaceType == type && x.UserSelected)
                .Join(possiblePlaces,
                    pv => pv.PlaceId,
                    pp => pp.PlaceId,
                    (pv, pp) => pv)
                .GroupBy(pv => pv.PlaceId, (placeId, visits) => new
                {
                    PlaceId = placeId,
                    Count = visits.Count()
                })
                .OrderBy(x => x.Count).FirstOrDefault();

            if (null != guessedPlaceId)
            {
                return _db.Places.First(x => x.PlaceId == guessedPlaceId.PlaceId);
            }
            return null;
        }
        
    }
}
