using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace CarTracker.Web.Auth
{
    public class TokenAuthenticationOptions : AuthenticationSchemeOptions
    {

        public const string AuthenticationScheme = "AUTHENTICATION_SESSION_SCHEME";

        public const string SessionTokenHeader = "CT_SESSION";

        /// <summary>
        /// Determines if this request should be processed with Token Authentication
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool IsRequestCanidate(HttpContext context)
        {
            return context.Request.Headers.ContainsKey(TokenAuthenticationOptions.SessionTokenHeader);
        }

    }
}
