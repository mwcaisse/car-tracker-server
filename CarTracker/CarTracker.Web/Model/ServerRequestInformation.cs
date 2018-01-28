using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarTracker.Common.Models;
using Microsoft.AspNetCore.Http;

namespace CarTracker.Web.Model
{
    public class ServerRequestInformation : IRequestInformation
    {

        private long? _userId;

        public bool IsAuthenticated => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public long? UserId
        {
            get
            {
                if (!_userId.HasValue)
                {
                    if (IsAuthenticated)
                    {
                        var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid);
                        _userId = Convert.ToInt64(userIdClaim.Value);
                    }
                    else
                    {
                        _userId = -1;
                    }
                }
                if (_userId.Value < 0)
                {
                    return null;
                }
                return _userId.Value;
            }
        }

        private string _username;

        public string Username
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_username) && IsAuthenticated)
                {
                    var usernameClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name);
                    _username = usernameClaim?.Value;
                }  
                return _username;
            }
        }

        private string _clientAddress;

        public string ClientAddress
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_clientAddress))
                {
                    _clientAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                }
                return _clientAddress;
            }
        }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServerRequestInformation(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }
    }
}
