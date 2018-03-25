using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Entities.Places;
using CarTracker.Common.Models.PlaceRequester;
using CarTracker.Common.Services.Places;
using CarTracker.Data;
using CarTracker.Data.Extensions;

namespace CarTracker.Logic.Services.Places
{
    public class GooglePlaceService : IGooglePlaceService
    {

        private readonly CarTrackerDbContext _db;

        public GooglePlaceService(CarTrackerDbContext db)
        {
            this._db = db;
        }

        public GooglePlace Get(long id)
        {
            return _db.GooglePlaces.Build().FirstOrDefault(p => p.GooglePlaceId == id);
        }

        public GooglePlace GetByGoogleId(string googleId)
        {

            return _db.GooglePlaces.Build().FirstOrDefault(p => p.GoogleId == googleId);
        }

        public GooglePlace CreateOrGetPlace(IPlaceModel model)
        {
            var place = GetByGoogleId(model.Id);
            if (null == place)
            {
                place = new GooglePlace()
                {
                    GoogleId = model.Id,
                    Place = new Place()
                    {
                        Latitude = Convert.ToDecimal(model.Location?.Latitude),
                        Longitude = Convert.ToDecimal(model.Location?.Longitude),
                        Name = model.Name
                    }
                };

                _db.GooglePlaces.Add(place);
                _db.SaveChanges();
            }
            return place;
        }
    }
}
