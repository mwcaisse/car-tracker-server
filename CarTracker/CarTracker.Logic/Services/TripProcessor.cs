using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Enums;
using CarTracker.Common.Exceptions;
using CarTracker.Common.Services;
using CarTracker.Data;

namespace CarTracker.Logic.Services
{
    public class TripProcessor : ITripProcessor
    {

        private readonly CarTrackerDbContext _db;

        private readonly IPlaceRequester _placeRequester;
        private readonly IPlaceService _placeService;
        private readonly ITripPossiblePlaceService _tripPossiblePlaceService;

        public TripProcessor(CarTrackerDbContext db,
            IPlaceRequester placeRequester, IPlaceService placeService, 
            ITripPossiblePlaceService tripPossiblePlaceService)
        {
            this._db = db;
            this._placeRequester = placeRequester;
            this._placeService = placeService;
            this._tripPossiblePlaceService = tripPossiblePlaceService;
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

        public Trip ProcessTrip(Trip trip)
        {
            if (trip.Status == TripStatus.Processed)
            {
                throw new EntityValidationException("Trip has already been processed.");
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
                trip.Status = TripStatus.Processed;

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
            }

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
            if (ToRadians(current.Latitude) == 0 || ToRadians(prev.Latitude) == 0)
            {
                return 0; // if either of the latitudes are 0 then return 0. 0 is an incorrect reading
            }

            return CalculateDistanceBetweenLocations(
                new LocationModel()
                {
                    Latitude = prev.Latitude,
                    Longitude = prev.Longitude
                },
                new LocationModel()
                {
                    Latitude  = current.Latitude,
                    Longitude = current.Longitude
                });
        }

        // TODO: Move the Latitude/Longitude calculates to their own utility class
        private const double EarthRadidus = 6371.0;

        protected double CalculateDistanceBetweenLocations(LocationModel start, LocationModel end)
        {

            double startLat = ToRadians(start.Latitude);
            double endLat = ToRadians(end.Latitude);
            double deltaLat = ToRadians(end.Latitude - start.Latitude);
            double deltaLong = ToRadians(end.Longitude - start.Longitude);

            double a = Math.Pow(Math.Sin(deltaLat / 2.0), 2.0) +
                       Math.Pow(Math.Sin(deltaLong / 2.0), 2.0) *
                       Math.Cos(startLat) * Math.Cos(endLat);

            double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));
            double distance = EarthRadidus * c;

            return Math.Abs(distance);
        }

        protected double ToRadians(decimal degrees)
        {
            return (Math.PI / 180.0) * Convert.ToDouble(degrees);
        }

        protected decimal ToDegrees(double radians)
        {
            return Convert.ToDecimal(180.0 * radians / Math.PI);
        }

        protected class LocationModel
        {
            public decimal Latitude { get; set; }
            public decimal Longitude { get; set; }
        }

        
        protected class BoundingBox
        {
            public LocationModel MinPoint { get; set; }
            public LocationModel MaxPoint { get; set; }
        }

        /// <summary>
        ///  Calculate a bounding box around a point
        /// 
        ///     Adopted from https://stackoverflow.com/questions/238260/how-to-calculate-the-bounding-box-for-a-given-lat-lng-location
        /// </summary>
        /// <param name="point">Location</param>
        /// <param name="halfSide">Half of the side of the resulting square in meters</param>
        /// <returns></returns>
        protected BoundingBox CalculateBoxAroundPoint(LocationModel point, double halfSide)
        {
            var lat = ToRadians(point.Latitude);
            var lon = ToRadians(point.Longitude);

            // Radius of the Earth at given latitude
            var radius = WGS84EarthRadius(lat);
            // Radius of the parallel at given latitude
            var pradius = radius * Math.Cos(lat);

            var latMin = lat - halfSide / radius;
            var latMax = lat + halfSide / radius;
            var lonMin = lon - halfSide / pradius;
            var lonMax = lon + halfSide / pradius;

            return new BoundingBox()
            {
                MinPoint = new LocationModel()
                {
                    Latitude = ToDegrees(latMin),
                    Longitude = ToDegrees(lonMin)
                },
                MaxPoint = new LocationModel()
                {
                    Latitude = ToDegrees(latMax),
                    Longitude = ToDegrees(lonMax)
                }
            };
        }

        // Semi-axes of WGS-84 geoidal reference
        private const double WGS84_a = 6378137.0; // Major semiaxis [m]
        private const double WGS84_b = 6356752.3; // Minor semiaxis [m]

        protected static double WGS84EarthRadius(double lat)
        {
            // http://en.wikipedia.org/wiki/Earth_radius
            var an = WGS84_a * WGS84_a * Math.Cos(lat);
            var bn = WGS84_b * WGS84_b * Math.Sin(lat);
            var ad = WGS84_a * Math.Cos(lat);
            var bd = WGS84_b * Math.Sin(lat);
            return Math.Sqrt((an * an + bn * bn) / (ad * ad + bd * bd));
        }

        protected void AddPossiblePlacesToTrip(Trip trip, IEnumerable<Reading> readings)
        {
            var nonZeroReadings = readings.Where(r => r.Latitude != 0 && r.Longitude != 0).ToList();
            if (!nonZeroReadings.Any())
            {
                return;
            }

            AddPossiblePlaceToTrip(trip, nonZeroReadings.First(), TripPossiblePlaceType.Start);
            AddPossiblePlaceToTrip(trip, nonZeroReadings.Last(), TripPossiblePlaceType.Destination);
        }

        protected void AddPossiblePlaceToTrip(Trip trip, Reading reading, TripPossiblePlaceType type)
        {
            var possiblePlaces = _placeRequester.GetPlacesNearby(Convert.ToDouble(reading.Latitude),
                Convert.ToDouble(reading.Longitude), 150);
            foreach (var placeModel in possiblePlaces)
            {
                var place = _placeService.CreateOrGetPlace(placeModel);

                var possiblePlace = new TripPossiblePlace()
                {
                    Trip = trip,
                    TripId = trip.TripId,
                    Place = place,
                    PlaceType = type,
                    Distance = Convert.ToDecimal(CalculateDistanceBetweenLocations(
                        new LocationModel()
                        {
                            Latitude = reading.Latitude,
                            Longitude = reading.Longitude
                        },
                        new LocationModel()
                        {
                            Latitude = place.Latitude,
                            Longitude = place.Longitude
                        }))
                };

                _tripPossiblePlaceService.Create(possiblePlace);
            }
        }
        
    }
}
