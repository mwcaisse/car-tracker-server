using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.ViewModels.Auth
{
    public class UserRegistrationKeyUseViewModel
    {
        public long Id { get; set; }
        public long UserRegistrationKeyId { get; set; }
        public DateTime CreateDate { get; set; }
        public UserViewModel User { get; set; }
    }
}
