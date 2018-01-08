using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarTracker.Web.Auth
{
    public class TokenAuthenticationHandler : AuthenticationHandler<TokenAuthenticationOptions>
    {

       private readonly SessionTokenManager _tokenManager;

        private readonly TokenAuthenticationOptions _options;


        public TokenAuthenticationHandler(IOptionsMonitor<TokenAuthenticationOptions> options, 
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, 
            SessionTokenManager tokenManager) 
            : base(options, logger, encoder, clock)
        {
            _options = options.CurrentValue;
            _tokenManager = tokenManager;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Request.Headers.ContainsKey(TokenAuthenticationOptions.SessionTokenHeader))
            {
                var sessionTokenKey = Request.Headers[TokenAuthenticationOptions.SessionTokenHeader];
                var token = _tokenManager.Get(sessionTokenKey);
                if (null != token)
                {
                    return Task.FromResult(AuthenticateResult.Success(token.Ticket));
                }

                return Task.FromResult(AuthenticateResult.Fail("Invalid Session Token"));
            }

            return Task.FromResult(AuthenticateResult.NoResult());
        }
    }
   
}
