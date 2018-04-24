using System;
using CarTracker.Common.Mappers;
using CarTracker.Common.Mappers.Auth;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels.Auth;
using CarTracker.Web.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserApiController : BaseApiController
    {

        private readonly IUserService _userService;
        private readonly UserAuthenticationManager _authenticationManager;
        private readonly ITripService _tripService;

        public UserApiController(IUserService userService,
            UserAuthenticationManager authenticationManager,
            ITripService tripService)
        {
            this._userService = userService;
            this._authenticationManager = authenticationManager;
            this._tripService = tripService;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]UserRegistrationViewModel registration)
        {
            var user = _userService.RegisterUser(registration);
            return Ok(null != user);
        }

        [HttpGet]
        [Route("available")]
        public IActionResult IsUsernameAvailable(string username)
        {
            return Ok(_userService.IsUsernameAvailable(username));
        }

        [HttpGet]
        [Authorize]
        [Route("me")]
        public IActionResult GetCurrentUser()
        {
            return Ok(_userService.Get(GetCurrentUsername()).ToViewModel());
        }

        [HttpGet]
        [Authorize]
        [Route("trip-summary/monthly")]
        public IActionResult GetTripSummary()
        {
            var startDate = DateTime.Now - TimeSpan.FromDays(180);
            var endDate = DateTime.Now;
            return Ok(_tripService.GetTripSummary(startDate, endDate).ToViewModel());
        }

        [HttpPost]
        [Route("login/password")]
        public IActionResult LoginPassword([FromBody] AuthenticationUserViewModel user)
        {
            var sessionToken = _authenticationManager.LoginPasswordForSessionToken(user.Username, user.Password);
            return Ok(HandleLoginResponse(sessionToken));
        }

        [HttpPost]
        [Route("login/token")]
        public IActionResult LoginToken([FromBody] AuthenticationTokenViewModel token)
        {
            var sessionToken = _authenticationManager.LoginTokenForSessionToken(token.Username, token.DeviceUuid,
                token.AuthenticationToken);
            return Ok(HandleLoginResponse(sessionToken));
        }

        protected bool HandleLoginResponse(ISessionToken sessionToken)
        {
            if (null != sessionToken)
            {
                Response.Headers.Add(TokenAuthenticationOptions.SessionTokenHeader, sessionToken.Id);
                return true;
            }
            return false;
        }

    }
}