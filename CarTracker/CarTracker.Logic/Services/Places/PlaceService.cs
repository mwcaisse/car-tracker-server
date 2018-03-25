using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities.Places;
using CarTracker.Common.Services;
using CarTracker.Common.Services.Places;
using CarTracker.Data;
using CarTracker.Data.Extensions;
using CarTracker.Logic.Util;

namespace CarTracker.Logic.Services.Places
{
    public class PlaceService : IPlaceService
    {
        private readonly CarTrackerDbContext _db;
        private readonly IPlaceRequester _placeRequester;
        private readonly IGooglePlaceService _googlePlaceService;


        public PlaceService(CarTrackerDbContext db, IPlaceRequester placeRequester,
            IGooglePlaceService googlePlaceService)
        {
            this._db = db;
            this._placeRequester = placeRequester;
            this._googlePlaceService = googlePlaceService;
        }

        public IEnumerable<Place> GetPlacesNearby(double latitude, double longitude, int range, long userId)
        {
            var places = new List<Place>();
            places.AddRange(GetGooglePlaces(latitude, longitude, range));
            places.AddRange(GetUserPlaces(latitude, longitude, range, userId));
            return places;
        }

        protected IEnumerable<Place> GetGooglePlaces(double latitude, double longitude, int range)
        {
            return
                _placeRequester.GetPlacesNearby(latitude, longitude, range)
                    .Select(gp => _googlePlaceService.CreateOrGetPlace(gp).Place);
        }

        protected IEnumerable<Place> GetUserPlaces(double latitude, double longitude, int range, long userId)
        {
            var boundingBox = GeographyUtils.CalculateBoxAroundPoint(latitude, longitude, range);
            return _db.UserPlaces.Build().Active().Where(p =>
                    p.OwnerId == userId &&
                    p.Place.Latitude >= boundingBox.MinPoint.Latitude &&
                    p.Place.Latitude <= boundingBox.MaxPoint.Latitude &&
                    p.Place.Longitude >= boundingBox.MinPoint.Longitude &&
                    p.Place.Longitude <= boundingBox.MaxPoint.Longitude)
                .Select(up => up.Place);
        }
    }
}
