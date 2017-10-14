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

        public TripService(CarTrackerDbContext db)
        {
            this._db = db;
        }

        public Trip Get(long id)
        {
            return _db.Trips.FirstOrDefault(x => x.TripId == id);
        }

        public PagedViewModel<Trip> GetForCar(long carId, int skip, int take, SortParam sort)
        {
            var query = _db.Trips.Where(x => x.CarId == carId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(sort?.ColumnName))
            {
                if (sort.Ascending)
                {
                    query = query.OrderBy(sort.ColumnName);
                }
                else
                {
                    query = query.OrderByDescending(sort.ColumnName);
                }
            }
            else
            {
                query = query.OrderByDescending(x => x.StartDate);
            }

            int count = query.Count();

            if (take <= 0 || take > 100)
            {
                throw new EntityValidationException("Invalid page size. Take must be between 1 and 100.");
            }
            if (skip < 0)
            {
                throw new EntityValidationException("Invalid skip. Skip must be >= 0.");
            }

            return new PagedViewModel<Trip>()
            {
                Data = query.Skip(skip).Take(take),
                Count = count,
                Skip = skip,
                Take = take
            };
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

        public Trip ProcessTrip(long id)
        {
            throw new NotImplementedException();
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
