using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Entities
{
    public class RegistrationKeyUse
    {
        public long RegistrationKeyUseId { get; set; }

        public long RegistrationKeyId { get; set; }
        public long UserId { get; set;}

        public virtual RegistrationKey RegistrationKey { get; set; }
        public virtual User User { get; set; }
    }
}
