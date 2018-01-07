﻿using System;
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
            UserAuthenticationManager authenticationManager) : 
            base(applicationConfiguration)
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
            return Login(SOURCE_ERROR);
        }

        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("logout")]
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", new RouteValueDictionary(
                new {action = "Login", source = SOURCE_LOGOUT }));
        }
    }
}
