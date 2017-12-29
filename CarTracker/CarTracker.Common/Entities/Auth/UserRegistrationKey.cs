using System;
using System.Collections.Generic;

namespace CarTracker.Common.Entities.Auth
{
    public class UserRegistrationKey : ITrackedEntity
    {
        public long UserRegistrationKeyId { get; set; }
        public string Key { get; set; }
        public int UsesRemaining { get; set; }
        public bool Active { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<UserRegistrationKeyUse> UserRegistrationKeyUses { get; set; }

    }
}
