using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Web.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers.View
{
    public class CarController : BaseViewController
    {
        public CarController(ApplicationConfiguration applicationConfiguration) : base(applicationConfiguration)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}