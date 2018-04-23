using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.ViewModels;

namespace CarTracker.Common.Mappers
{
    public static class TripSummaryMapper
    {

        public static TripSummaryViewModel ToViewModel(this TripSummaryModel model)
        {
            var vm = new TripSummaryViewModel()
            {
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                NumberOfTrips = model.NumberOfTrips,
                MilesDriven = model.MilesDriven,
                PlacesVisited = model.PlacesVisited.ToViewModel(),
                NewPlacesVisited = model.PlacesVisited.ToViewModel()
            };
            return vm;
        }

    }
}
