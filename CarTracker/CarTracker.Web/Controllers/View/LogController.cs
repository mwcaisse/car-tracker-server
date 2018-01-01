using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Web.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers.View
{
    public class LogController : BaseViewController
    {
        public LogController(ApplicationConfiguration applicationConfiguration) : base(applicationConfiguration)
        {
        }

        public IActionResult Reader()
        {
            return View();
        }
    }
}