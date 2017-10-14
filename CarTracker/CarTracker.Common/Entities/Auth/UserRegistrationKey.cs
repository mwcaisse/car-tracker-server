using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Entities
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
