using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarTracker.Common.ViewModels
{
    public class TripSummaryViewModel
    {
        public DateTime? PeriodStart { get; set; }

        public DateTime? PeriodEnd { get; set; }

        public double MilesDriven
        {
            get { return Trips.Select(t => t.DistanceTraveled).Sum() ?? 0; }
        }

        public long NumberOfTrips => Trips.Count();

        public IEnumerable<PlaceViewModel> PlacedVisited { get; set; }

        public IEnumerable<PlaceViewModel> NewPlacesVisited { get; set; }

        public IEnumerable<TripViewModel> Trips { get; set; }



    }
}
