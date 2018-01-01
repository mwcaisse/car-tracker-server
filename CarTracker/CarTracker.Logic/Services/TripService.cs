using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Exceptions;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels;
using CarTracker.Data;
using CarTracker.Data.Extensions;

namespace CarTracker.Logic.Services
{
    public class TripService : ITripService
    {

        private readonly CarTrackerDbContext _db;
        private readonly ITripProcessor _tripProcessor;

        public TripService(CarTrackerDbContext db, ITripProcessor tripProcessor)
        {
            this._db = db;
            this._tripProcessor = tripProcessor;
        }

        public Trip Get(long id)
        {
            return _db.Trips.FirstOrDefault(x => x.TripId == id);
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

            return _db.Trips.Where(t => t.CarId == carId).PageAndSort(skip, take, sort);
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

            return trip;
        }

        public void ProcessUnprocessedTrips()
        {
            _tripProcessor.ProcessUnprocessedTrips();
        }

        public Trip ProcessTrip(long id)
        {
            var trip = Get(id);
            if (null == trip)
            {
                throw new EntityValidationException("The specified trip does not exist.");
            }
            return _tripProcessor.ProcessTrip(trip);
        }

        protected void ValidatePlace(long placeId)
        {
            var place = _db.Places.FirstOrDefault(p => p.PlaceId == placeId);
            if (null == place)
            {
                throw new EntityValidationException("The specified place does not exist.");
            }
        }

        protected Trip GetTripOrException(long id)
        {
            var trip = _db.Trips.FirstOrDefault(x => x.TripId == id);

            if (null == trip)
            {
                throw new EntityValidationException("Trip does not exist.");
            }

            return trip;
        }

        public bool SetStartingPlace(long id, long placeId)
        {
            ValidatePlace(placeId);
            var trip = GetTripOrException(id);

            trip.StartPlaceId = placeId;
            _db.SaveChanges();

            return true;
        }

        public bool SetDestinationPlace(long id, long placeId)
        {
            ValidatePlace(placeId);
            var trip = GetTripOrException(id);

            trip.DestinationPlaceId = placeId;
            _db.SaveChanges();

            return true;
        }
    }
}
