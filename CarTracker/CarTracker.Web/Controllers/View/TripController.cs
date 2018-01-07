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
    public class TripController : BaseViewController
    {
        public TripController(ApplicationConfiguration applicationConfiguration) : base(applicationConfiguration)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}