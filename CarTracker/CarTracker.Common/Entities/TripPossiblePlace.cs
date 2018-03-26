using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Enums;
using CarTracker.Common.Entities.Places;

namespace CarTracker.Common.Entities
{
    public class TripPossiblePlace : ITrackedEntity, IActiveEntity
    {

        public long TripPossiblePlaceId { get; set; }

        public long TripId { get; set; }

        public long PlaceId { get; set; }

        public decimal Distance { get; set; }

        public TripPossiblePlaceType PlaceType { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Trip Trip { get; set; }
        public virtual Place Place { get; set; }

        public bool Active { get; set; }

    }
}
