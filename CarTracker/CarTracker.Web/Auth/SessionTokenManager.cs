using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace CarTracker.Web.Auth
{
    public class SessionTokenManager
    {

        private readonly Dictionary<string, ISessionToken> _tokens;

        public SessionTokenManager()
        {
            _tokens = new Dictionary<string, ISessionToken>();
        }

        public ISessionToken Get(string id)
        {
            if (_tokens.ContainsKey(id))
            {
                var token = _tokens[id];
                if (!IsTokenValid(token))
                {
                    token = null;
                }
                return token;
            }
            return null;
        }

        private bool IsTokenValid(ISessionToken token)
        {
            if (null != token && token.Expired)
            {
                _tokens.Remove(token.Id);
                return false;
            }
            return null != token;
        }

        public ISessionToken GetTokenForUser(string username)
        {
            var userTokens = _tokens.Where(kv => kv.Value.Username == username && !kv.Value.Expired)
                .ToList();

            if (userTokens.Any())
            {
                return userTokens.First().Value;
            }
            return null;
        }

        public ISessionToken CreateToken(string username, AuthenticationTicket ticket)
        {
            var token = new SessionToken(CreateTokenId(), username, ticket, CreateExpirationDate());
            _tokens.Add(token.Id, token);
            return token;
        }

        private string CreateTokenId()
        {
            return Guid.NewGuid().ToString();
        }

        private DateTime CreateExpirationDate()
        {
            return DateTime.Now.AddHours(1);
        }
    }
}
