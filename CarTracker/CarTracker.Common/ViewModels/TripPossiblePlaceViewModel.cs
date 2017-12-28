using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Enums;

namespace CarTracker.Common.ViewModels
{
    public class TripPossiblePlaceViewModel
    {
        public long TripId { get; set; }

        public long PlaceId { get; set; }

        public TripPossiblePlaceType Type { get; set; }

        public decimal Distance { get; set; }

        public PlaceViewModel Place { get; set; }
    }
}
