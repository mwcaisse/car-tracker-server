using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Web.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers.View
{
    [Authorize]
    [Route("admin")]
    public class AdminController : BaseViewController
    {
        public AdminController(ApplicationConfiguration applicationConfiguration) 
            : base(applicationConfiguration)
        {
        }
    
        [Route("registration-keys")]
        public IActionResult RegistrationKeys()
        {
            return View();
        }
    }
}