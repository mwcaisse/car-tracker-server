using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Entities.Places;
using CarTracker.Common.Enums;
using CarTracker.Common.Exceptions;
using CarTracker.Common.Models;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels;
using CarTracker.Data;
using CarTracker.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CarTracker.Logic.Services
{
    public class TripService : ITripService
    {

        private readonly CarTrackerDbContext _db;

        public TripService(CarTrackerDbContext db)
        {
            this._db = db;
        }

        public Trip Get(long id)
        {
            return _db.Trips.Build()
                            .FirstOrDefault(x => x.TripId == id);
        }

        public PagedViewModel<Trip> GetForCar(long carId, int skip, int take, SortParam sort)
        {
            if (string.IsNullOrWhiteSpace(sort?.ColumnName))
            {
                sort = new SortParam()
                {
                    ColumnName = "StartDate",
                    Ascending = false
                };
            }

            return _db.Trips.Build().Where(t => t.CarId == carId).PageAndSort(skip, take, sort);
        }

        protected void ValidateTrip(TripViewModel trip)
        {
            if (trip.StartDate > DateTime.Now)
            {
                throw new EntityValidationException("Start Date cannot be in the future.");
            }
            if (trip.EndDate.HasValue && trip.EndDate < trip.StartDate)
            {
               throw new EntityValidationException("End date cannot be before start date."); 
            }
            if (trip.EndDate.HasValue && trip.EndDate > DateTime.Now)
            {
                throw new EntityValidationException("End Date cannot be in the future.");
            }
            var car = _db.Cars.FirstOrDefault(x => x.CarId == trip.CarId);
            if (null == car)
            {
                throw new EntityValidationException("Trip must be associated with a car.");
            }

        }

        public Trip Create(long carId, TripViewModel toCreate)
        {
            toCreate.CarId = carId;
            ValidateTrip(toCreate);

            var trip = new Trip()
            {
                StartDate = toCreate.StartDate,
                EndDate = toCreate.EndDate,
                CarId = carId,
                Name = toCreate.Name,
                Status = toCreate.Status
            };

            _db.Trips.Add(trip);
            _db.SaveChanges();

            return trip;
        }

        public Trip Update(TripViewModel toUpdate)
        {
            ValidateTrip(toUpdate);

            var trip = GetTripOrException(toUpdate.Id);

            trip.StartDate = toUpdate.StartDate;
            trip.EndDate = toUpdate.EndDate;
            trip.CarId = toUpdate.CarId;
            trip.Name = toUpdate.Name;
            trip.Status = toUpdate.Status;

            _db.SaveChanges();

            return trip;
        }

        protected void ValidatePlace(long placeId)
        {
            var place = _db.Places.FirstOrDefault(p => p.PlaceId == placeId);
            if (null == place)
            {
                throw new EntityValidationException("The specified place does not exist.");
            }
        }

        protected Trip GetTripOrException(long id, IQueryable<Trip> tripQuery = null)
        {
            if (null == tripQuery)
            {
                tripQuery = _db.Trips;
            }
            var trip = tripQuery.FirstOrDefault(x => x.TripId == id);

            if (null == trip)
            {
                throw new EntityValidationException("Trip does not exist.");
            }

            return trip;
        }

        public bool SetStartingPlace(long id, long placeId, bool userSelected = true)
        {
            ValidatePlace(placeId);
            var trip = GetTripOrException(id, _db.Trips.Include(t => t.Readings));

            trip.StartPlaceId = placeId;
            _db.SaveChanges();

            RecordPlaceVisit(trip, placeId, TripPossiblePlaceType.Start, userSelected);

            return true;
        }

        public bool SetDestinationPlace(long id, long placeId, bool userSelected = true)
        {
            ValidatePlace(placeId);
            var trip = GetTripOrException(id, _db.Trips.Include(t => t.Readings));

            trip.DestinationPlaceId = placeId;
            _db.SaveChanges();

            RecordPlaceVisit(trip, placeId, TripPossiblePlaceType.Destination, userSelected);

            return true;
        }

        protected void RecordPlaceVisit(Trip trip, long placeId, TripPossiblePlaceType type, bool userSelected = true)
        {

            Reading reading;
            if (type == TripPossiblePlaceType.Start)
            {
                reading = GetFirstReading(trip);
            }
            else
            {
                reading = GetLastReading(trip);
            }
            if (null == reading)
            {
                return; // No reading found
            }

            var placeVisit = new PlaceVisit()
            {
                Latitude = reading.Latitude,
                Longitude = reading.Longitude,
                PlaceType = type,
                VisitDate = type == TripPossiblePlaceType.Destination ? trip.EndDate : trip.StartDate,
                UserSelected = userSelected,
                PlaceId = placeId,
                OwnerId = trip.Car.OwnerId
            };

            _db.PlaceVisits.Add(placeVisit);
            _db.SaveChanges();
        }

        protected Reading GetFirstReading(Trip trip)
        {
            return trip.Readings.FirstOrDefault(r => r.Longitude != 0 && r.Latitude != 0);
        }

        protected Reading GetLastReading(Trip trip)
        {
            return trip.Readings.LastOrDefault(r => r.Longitude != 0 && r.Latitude != 0);
        }
    }
}
