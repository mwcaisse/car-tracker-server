using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Enums;
using CarTracker.Common.Exceptions;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels;
using CarTracker.Data;
using CarTracker.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CarTracker.Logic.Services
{
    public class TripPossiblePlaceService : ITripPossiblePlaceService
    {

        private readonly CarTrackerDbContext _db;

        public TripPossiblePlaceService(CarTrackerDbContext db)
        {
            this._db = db;
        }

        public IEnumerable<TripPossiblePlace> GetForTripOfType(long tripId, TripPossiblePlaceType type)
        {
            return _db.TripPossiblePlaces.Active().Build().Where(x => x.TripId == tripId && x.PlaceType == type);
        }

        public PagedViewModel<TripPossiblePlace> GetForTripOfTypePaged(long tripId, TripPossiblePlaceType type, int skip = 0, int take = 10,
            SortParam sortParam = null)
        {
            if (string.IsNullOrWhiteSpace(sortParam?.ColumnName))
            {
                sortParam = new SortParam()
                {
                    ColumnName = "Distance",
                    Ascending = true
                };
            }
            return
                _db.TripPossiblePlaces
                    .Active()
                    .Build()
                    .Where(x => x.TripId == tripId && x.PlaceType ==  type)
                    .PageAndSort(skip, take, sortParam);
        }

        public TripPossiblePlace Get(long id)
        {
            return _db.TripPossiblePlaces.Active().Build().FirstOrDefault(x => x.TripPossiblePlaceId == id);
        }

        public TripPossiblePlace Create(TripPossiblePlace toCreate)
        {
           _db.TripPossiblePlaces.Add(toCreate);
            _db.SaveChanges();

            return toCreate;
        }
    }
}
