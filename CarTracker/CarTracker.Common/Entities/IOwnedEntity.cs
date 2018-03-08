using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CarTracker.Common.Entities.Auth;

namespace CarTracker.Common.Entities
{
    public interface IOwnedEntity
    {
        long OwnerId { get; set; }
        User Owner { get; set; }
    }
}
