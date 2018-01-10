using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.ViewModels.Auth
{
    public class AuthenticationTokenViewModel
    {
        public string Username { get; set; }
        public string AuthenticationToken { get; set; }
        public string DeviceUuid { get; set; }
    }
}
