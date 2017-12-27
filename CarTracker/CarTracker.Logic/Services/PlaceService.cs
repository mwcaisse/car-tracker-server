using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Models.PlaceRequester;
using CarTracker.Common.Services;
using CarTracker.Data;

namespace CarTracker.Logic.Services
{
    public class PlaceService : IPlaceService
    {

        private readonly CarTrackerDbContext _db;

        public PlaceService(CarTrackerDbContext db)
        {
            this._db = db;
        }

        public Place Get(long id)
        {
            return _db.Places.FirstOrDefault(p => p.PlaceId == id);
        }

        public Place GetByGoogleId(string googleId)
        {

            return _db.Places.FirstOrDefault(p => p.GooglePlaceId == googleId);
        }

        public Place CreateOrGetPlace(IPlaceModel model)
        {
            var place = GetByGoogleId(model.Id);
            if (null == place)
            {
                place = new Place()
                {
                    Latitude = Convert.ToDecimal(model.Location?.Latitude),
                    Longitude = Convert.ToDecimal(model.Location?.Longitude),
                    GooglePlaceId = model.Id,
                    Name = model.Name                    
                };

                _db.Places.Add(place);
                _db.SaveChanges();
            }
            return place;
        }
    }
}
