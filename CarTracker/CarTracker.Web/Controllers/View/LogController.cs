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
    public class LogController : BaseViewController
    {
        public LogController(ApplicationConfiguration applicationConfiguration,
            BuildInformation buildInformation)
            : base(applicationConfiguration, buildInformation)
        {
        }

        public IActionResult Reader()
        {
            return View();
        }
    }
}