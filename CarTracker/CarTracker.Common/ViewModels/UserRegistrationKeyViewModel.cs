using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.ViewModels
{
    public class UserRegistrationKeyViewModel
    {

        public long UserRegistrationKeyId { get; set; }
        public string Key { get; set; }
        public int UsesRemaining { get; set; }
        public bool Active { get; set; }

    }
}
