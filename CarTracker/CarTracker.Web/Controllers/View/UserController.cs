using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarTracker.Web.Auth;
using CarTracker.Web.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;

namespace CarTracker.Web.Controllers.View
{
    [Route("")]
    public class UserController : BaseViewController
    {

        private readonly UserAuthenticationManager _authenticationManager;

        public UserController(ApplicationConfiguration applicationConfiguration,
            UserAuthenticationManager authenticationManager) : 
            base(applicationConfiguration)
        {
            this._authenticationManager = authenticationManager;
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (_authenticationManager.LoginPassword(username, password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };
                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                //Redirect the user to the home page after login
                return Redirect("/");
            }

            //Login failed, redirect them to login page
            //TODO: Add error hanlding/message showing here
            return View();
        }
    }
}
