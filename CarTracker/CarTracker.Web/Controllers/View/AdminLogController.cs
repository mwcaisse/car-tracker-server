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
    [Route("admin/log")]
    public class AdminLogController : BaseViewController
    {

        
        public AdminLogController(ApplicationConfiguration applicationConfiguration,
            BuildInformation buildInformation) : base(applicationConfiguration, buildInformation)
        {
            
        }

        [Route("request")]
        public IActionResult RequestLogs()
        {
            return View("~/Views/Admin/Logs/RequestLogs.cshtml");
        }

        [Route("request/{guid}")]
        public IActionResult RequestLogDetails(string guid)
        {
            ViewBag.RequestGuid = guid;
            return View("~/Views/Admin/Logs/RequestLogDetails.cshtml");
        }

    }
}
