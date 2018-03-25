using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Entities.Places
{
    public class GooglePlace : ITrackedEntity, IActiveEntity
    {
        public long GooglePlaceId { get; set; }

        public string GoogleId { get; set; }

        public long PlaceId { get; set; }
        public Place Place { get; set; }

        public bool Active { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        
    }
}
