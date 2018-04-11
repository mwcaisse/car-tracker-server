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

        public bool IsAuthenticated { get; }

        public long? UserId { get; }

        public string Username { get; }

        public string ClientAddress { get; }

        public ServerRequestInformation(IHttpContextAccessor httpContextAccessor)
        {
            if (null != httpContextAccessor.HttpContext)
            {
                IsAuthenticated = httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
                ClientAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                Username = null;
                UserId = null;

                if (IsAuthenticated)
                {
                    var usernameClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name);
                    Username = usernameClaim?.Value;

                    var userIdClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid);
                    UserId = Convert.ToInt64(userIdClaim.Value);
                }
            }
        }
    }
}
