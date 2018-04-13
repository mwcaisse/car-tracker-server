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

        private readonly IHttpContextAccessor _httpContextAccessor;

        private HttpContext HttpContext => _httpContextAccessor.HttpContext;

        private bool? _isAuthenticated = null;

        public bool IsAuthenticated
        {
            get
            {
                if (!_isAuthenticated.HasValue)
                {
                    _isAuthenticated = HttpContext.User.Identity.IsAuthenticated;
                }
                return _isAuthenticated.Value;
            }
        }

        private long? _userId = null;

        public long UserId
        {
            get
            {
                if (!_userId.HasValue)
                {
                    var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.Sid);
                    _userId = Convert.ToInt64(userIdClaim.Value);
                    if (_userId < 1)
                    {
                        _userId = -1;
                    }
                }
                return _userId.Value;
            }
        }

        private string _username = null;

        public string Username
        {
            get
            {
                if (null == _username)
                {
                    var usernameClaim = HttpContext.User.FindFirst(ClaimTypes.Name);
                    _username = usernameClaim?.Value ?? "";
                }
                return _username;
            }
        }

        private string _clientAddress = null;

        public string ClientAddress
        {
            get
            {
                if (null == _clientAddress)
                {
                    _clientAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                }
                return _clientAddress;
            }
        }

        public ServerRequestInformation(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }
    }
}
