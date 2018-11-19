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
        public TripController(ApplicationConfiguration applicationConfiguration,
            BuildInformation buildInformation)
            : base(applicationConfiguration, buildInformation)
        {
        }

        public IActionResult Index()
        {
            return VueView("views/Trip", "Trip");
        }
    }
}