using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels;
using CarTracker.Web.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(UserRegistrationViewModel registration)
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

    }
}