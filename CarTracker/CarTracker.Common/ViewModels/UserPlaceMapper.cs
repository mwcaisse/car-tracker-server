using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;

namespace CarTracker.Common.ViewModels
{
    public static class UserPlaceMapper
    {

        public static UserPlaceViewModel ToViewModel(this UserPlace userPlace)
        {
            var vm = new UserPlaceViewModel()
            {
                UserPlaceId = userPlace.UserPlaceId,
                Name = userPlace.Name,
                Latitude = userPlace.Latitude,
                Longitude = userPlace.Longitude
            };
            return vm;
        }

        public static IEnumerable<UserPlaceViewModel> ToViewModel(this IEnumerable<UserPlace> userPlaces)
        {
            return userPlaces.Select(up => up.ToViewModel());
        }

        public static PagedViewModel<UserPlaceViewModel> ToViewModel(this PagedViewModel<UserPlace> pagedPlaces)
        {
            return new PagedViewModel<UserPlaceViewModel>()
            {
                Data = pagedPlaces.Data.ToViewModel(),
                Skip = pagedPlaces.Skip,
                Take = pagedPlaces.Take,
                Total = pagedPlaces.Total
            };
        }

    }
}
