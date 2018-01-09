using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CarTracker.Web.Controllers.Api
{
    [Authorize]
    [Route("api/user/token")]
    public class UserAuthenticationTokenApiController : BaseApiController
    {

        private readonly IUserAuthenticationTokenService _tokenService;

        public UserAuthenticationTokenApiController(IUserAuthenticationTokenService tokenService)
        {
            this._tokenService = tokenService;
        }

        [HttpGet]
        [Route("active")]
        public IActionResult GetActiveForUser(int skip = DefaultSkip, int take = DefaultTake,
            SortParam sort = null)
        {
            var userId = 1;
            return Ok(_tokenService.GetActiveForUser(userId, skip, take, sort));
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateToken([FromBody]string deviceUuid)
        {
            var userId = 1;
            return Ok(_tokenService.CreateToken(userId, deviceUuid));
        }

    }
}
