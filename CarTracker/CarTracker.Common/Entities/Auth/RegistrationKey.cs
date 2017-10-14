using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Entities
{
    public class RegistrationKey
    {
        public long RegistrationKeyId { get; set; }
        public string Key { get; set; }
        public int UsesRemaining { get; set; }
        public bool Active { get; set; }

        public ICollection<RegistrationKeyUse> RegistrationKeyUses { get; set; }

    }
}
