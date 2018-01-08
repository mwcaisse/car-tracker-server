using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace CarTracker.Web.Auth
{
    public static class TokenAuthenticationExtensions
    {

        public static AuthenticationBuilder AddTokenAuthentication(this AuthenticationBuilder builder,
            Action<TokenAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<TokenAuthenticationOptions, TokenAuthenticationHandler>(
                TokenAuthenticationOptions.AuthenticationScheme, configureOptions);
        }
    }
}
