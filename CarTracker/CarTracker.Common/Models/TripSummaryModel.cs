using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Entities.Places;

namespace CarTracker.Common.Models
{
    public class TripSummaryModel
    {

        public DateTime? PeriodStart { get; set; }

        public DateTime? PeriodEnd { get; set; }

        public decimal MilesDriven{ get; set; }

        public IEnumerable<Trip> Trips { get; set; }

        public IEnumerable<Place> PlacesVisited { get; set; }

        public IEnumerable<Place> NewPlacesVisited { get; set; }
  
    }
}
