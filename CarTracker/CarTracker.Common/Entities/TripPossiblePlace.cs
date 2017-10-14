using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Entities
{
    public class TripPossiblePlace
    {

        public long TripPossiblePlaceId { get; set; }

        public long TripId { get; set; }

        public long PlaceId { get; set; }

        public double Distance { get; set; }

        public string PlaceType { get; set; }

        public virtual Trip Trip { get; set; }
        public virtual Place Place { get; set; }

    }
}
