using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarTracker.Web.Auth;
using CarTracker.Web.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;

namespace CarTracker.Web.Controllers.View
{
    [Route("")]
    public class UserController : BaseViewController
    {

        private static readonly string SOURCE_LOGOUT = "logout";
        private static readonly string SOURCE_ERROR = "error";

        private readonly UserAuthenticationManager _authenticationManager;

        public UserController(ApplicationConfiguration applicationConfiguration,
            BuildInformation buildInformation,
            UserAuthenticationManager authenticationManager) : 
            base(applicationConfiguration, buildInformation)
        {
            this._authenticationManager = authenticationManager;
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string source = "")
        {
            ViewBag.IsLogout = ContainsUrlParameter("logout") || 
                string.Equals(SOURCE_LOGOUT, source, StringComparison.CurrentCultureIgnoreCase);

            ViewBag.IsError = ContainsUrlParameter("error") ||
                string.Equals(SOURCE_ERROR, source, StringComparison.CurrentCultureIgnoreCase);

            ViewBag.IsRegistered = ContainsUrlParameter("registered");

            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return Login(SOURCE_ERROR);
            }
            var principal = _authenticationManager.LoginPasswordForPrincipal(username, password);
            if (null != principal)
            {
                await HttpContext.SignInAsync(principal);

                //Redirect the user to the home page after login
                return Redirect(ApplicationConfiguration.PrefixUrl("/"));
            }

            //Login failed, redirect them to login page
            return Login(SOURCE_ERROR);
        }

        [Route("register")]
        public IActionResult Register()
        {
            return VueView("views/Register", "Register");
        }

        [Route("logout")]
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect(ApplicationConfiguration.PrefixUrl("/Login?source=" + SOURCE_LOGOUT));
        }

        [Route("user/tokens")]
        public IActionResult Tokens()
        {
            return VueView("views/User/Tokens", "Authentication Tokens");
        }
    }
}
