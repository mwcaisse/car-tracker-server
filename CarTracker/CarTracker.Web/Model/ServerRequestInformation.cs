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

        private bool? _isAuthenticated;

        public bool IsAuthenticated
        {
            get
            {
                if (!_isAuthenticated.HasValue)
                {
                    _isAuthenticated = HttpContext?.User?.Identity?.IsAuthenticated == true;
                }
                return _isAuthenticated.Value;
            }
        }

        private long? _userId;

        public long UserId
        {
            get
            {
                if (!_userId.HasValue)
                {
                    if (IsAuthenticated)
                    {
                        var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.Sid);
                        _userId = Convert.ToInt64(userIdClaim.Value);
                        if(_userId < 1)
                        {
                            _userId = -1;
                        }
                    }
                    else
                    {
                        _userId = -1;
                    }
                }
                return _userId.Value;
            }
        }

        private string _username;

        public string Username
        {
            get
            {
                if (null == _username)
                {
                    if (IsAuthenticated)
                    {
                        var usernameClaim = HttpContext.User.FindFirst(ClaimTypes.Name);
                        _username = usernameClaim?.Value ?? "";
                    }
                    else
                    {
                        _username = "";
                    }
                    
                }
                return _username;
            }
        }

        private string _clientAddress;

        public string ClientAddress
        {
            get
            {
                if (null == _clientAddress)
                {
                    _clientAddress = HttpContext?.Connection.RemoteIpAddress.ToString();
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
