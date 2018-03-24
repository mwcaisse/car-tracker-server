using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities.Auth;

namespace CarTracker.Common.Entities
{
    public class UserPlace : ITrackedEntity, IOwnedEntity, IActiveEntity
    {

        public long UserPlaceId { get; set; }
        public string Name { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public long OwnerId { get; set; }
        public User Owner { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public bool Active { get; set; }
    }
}
