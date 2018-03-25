using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Entities.Places;
using CarTracker.Common.ViewModels;

namespace CarTracker.Common.Mappers
{
    public static class PlaceMapper
    {

        public static PlaceViewModel ToViewModel(this Place place)
        {
            if (null == place)
            {
                return null;
            }
            var vm = new PlaceViewModel()
            {
                Id = place.PlaceId,
                Name = place.Name,
                Latitude = place.Latitude,
                Longitude = place.Longitude
            };

            return vm;
        }

        public static IEnumerable<PlaceViewModel> ToViewModel(this IEnumerable<Place> places)
        {
            return places.Select(p => p.ToViewModel());
        }
    }
}
