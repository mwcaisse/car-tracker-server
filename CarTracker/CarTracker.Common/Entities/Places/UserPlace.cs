using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities.Auth;

namespace CarTracker.Common.Entities.Places
{
    public class UserPlace : ITrackedEntity, IOwnedEntity, IActiveEntity
    {

        public long UserPlaceId { get; set; }

        public long PlaceId { get; set; }
        public Place Place { get; set; }

        public long OwnerId { get; set; }
        public User Owner { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public bool Active { get; set; }
    }
}
