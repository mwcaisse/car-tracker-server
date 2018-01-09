using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers.Api
{
    public class BaseApiController : Controller
    {

        protected const int DefaultSkip = 0;
        protected const int DefaultTake = 25;

        /// <summary>
        /// Returns the username of the currently logged in user
        /// </summary>
        /// <returns></returns>
        public string GetCurrentUsername()
        {
            var usernameClaim = HttpContext.User.FindFirst(ClaimTypes.Name);
            return usernameClaim?.Value;
        }

        /// <summary>
        /// Returns the Id of the currently logged in user
        /// </summary>
        /// <returns></returns>
        public long GetCurrentUserId()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.Sid);
            if (null != userIdClaim && !string.IsNullOrWhiteSpace(userIdClaim.Value))
            {
                return Convert.ToInt64(userIdClaim.Value);
            }
            return 0;
        }

    }
}
