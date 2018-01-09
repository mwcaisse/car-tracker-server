using System;
using System.Collections.Generic;

namespace CarTracker.Common.Entities.Auth
{

    
    public class User : ITrackedEntity, IActiveEntity
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public bool Locked { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? PasswordExpirationDate { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<UserAuthenticationToken> UserAuthenticationTokens { get; set; }
        public virtual ICollection<UserRegistrationKeyUse> UserRegistrationKeyUses { get; set; }
        
    }
}
