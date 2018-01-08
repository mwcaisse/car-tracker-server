using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace CarTracker.Web.Auth
{
    public class TokenAuthenticationOptions : AuthenticationSchemeOptions
    {

        public const string AuthenticationScheme = "AUTHENTICATION_SESSION_SCHEME";

        public string TokenHeader { get; set; }

    }
}
