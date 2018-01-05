using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Enums;
using CarTracker.Common.ViewModels;

namespace CarTracker.Common.Mappers
{
    public static class TripPossiblePlaceMapper
    {

        public static TripPossiblePlaceViewModel ToViewModel(this TripPossiblePlace possiblePlace)
        {
            var vm = new TripPossiblePlaceViewModel()
            {
                TripId = possiblePlace.TripId,
                PlaceId = possiblePlace.PlaceId,
                PlaceType = possiblePlace.PlaceType,
                Distance = possiblePlace.Distance,
                Place = possiblePlace.Place?.ToViewModel()
            };

            return vm;
        }

        public static IEnumerable<TripPossiblePlaceViewModel> ToViewModel(
            this IEnumerable<TripPossiblePlace> possiblePlaces)
        {
            return possiblePlaces.Select(p => p.ToViewModel());
        }

        public static PagedViewModel<TripPossiblePlaceViewModel> ToViewModel(
            this PagedViewModel<TripPossiblePlace> pagedPlaces)
        {
            return new PagedViewModel<TripPossiblePlaceViewModel>()
            {
                Data = pagedPlaces.Data.ToViewModel(),
                Total = pagedPlaces.Total,
                Take = pagedPlaces.Take,
                Skip = pagedPlaces.Skip
            };
        }
    }
}
