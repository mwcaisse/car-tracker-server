using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.ViewModels
{
    public class TripHistoryViewModel
    {

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public long CarId { get; set; }

        public IEnumerable<TripViewModel> Trips { get; set; }

        public IEnumerable<ReadingViewModel> Readings { get; set; }

        public IEnumerable<PlaceViewModel> Destinations { get; set; }

    }
}
