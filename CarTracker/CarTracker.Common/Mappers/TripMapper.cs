using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.ViewModels;

namespace CarTracker.Common.Mappers
{
    public static class TripMapper
    {

        public static TripViewModel ToViewModel(this Trip trip)
        {
            var vm = new TripViewModel()
            {
                Id = trip.TripId,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Name = trip.Name,
                CarId = trip.CarId,
                AverageSpeed = trip.AverageSpeed,
                AverageEngineRpm = trip.AverageEngineRpm,
                MaximumSpeed = trip.MaximumSpeed,
                MaxEngineRpm = trip.MaxEngineRpm,
                DistanceTraveled = trip.DistanceTraveled,
                IdleTime = trip.IdleTime,
                StartPlaceId = trip.StartPlaceId,
                DestinationPlaceId = trip.DestinationPlaceId,
                Status = trip.Status
            };

            return vm;
        }

        public static IEnumerable<TripViewModel> ToViewModel(this IEnumerable<Trip> trips)
        {
            return trips.Select(t => t.ToViewModel());
        }

        public static PagedViewModel<TripViewModel> ToViewModel(this PagedViewModel<Trip> pagedTrips)
        {
            return new PagedViewModel<TripViewModel>()
            {
                Data = pagedTrips.Data.ToViewModel(),
                Count = pagedTrips.Count,
                Take = pagedTrips.Take,
                Skip = pagedTrips.Skip
            };
        }

    }
}
