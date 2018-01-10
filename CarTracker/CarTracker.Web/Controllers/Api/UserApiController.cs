using CarTracker.Common.Mappers.Auth;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels.Auth;
using CarTracker.Web.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserApiController : BaseApiController
    {

        private readonly IUserService _userService;
        private readonly UserAuthenticationManager _authenticationManager;

        public UserApiController(IUserService userService,
            UserAuthenticationManager authenticationManager)
        {
            this._userService = userService;
            this._authenticationManager = authenticationManager;
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
        [Route("me")]
        public IActionResult GetCurrentUser()
        {
            return Ok(_userService.Get(GetCurrentUsername()).ToViewModel());
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