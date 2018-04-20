using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities.Places;

namespace CarTracker.Common.ViewModels
{
    public class TripSummaryViewModel
    {
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int NumberOfTrips { get; set; }

        public double MilesDriven { get; set; }

        public IEnumerable<Place> PlacesVisited { get; set; }

        public IEnumerable<Place> NewPlacesVisited { get; set; }
    }
}
