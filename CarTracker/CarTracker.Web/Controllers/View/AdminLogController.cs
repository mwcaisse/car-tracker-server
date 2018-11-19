using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Web.Configuration;
using CarTracker.Web.Util;
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
            return VueView("views/Admin/Log/RequestLogs", "Request Logs");
        }

        [Route("request/{guid}")]
        public IActionResult RequestLogDetails(string guid)
        {
            ViewBag.RequestGuid = guid;
            return VueView("views/Admin/Log/RequestLogDetail", "Request Log Detail",
                new VueViewProperty() { Name = "requestGuid", Value = guid});
        }

        [Route("server")]
        public IActionResult ServerLogs()
        {
            return VueView("views/Admin/Log/ServerLogs", "Server Logs");
        }

    }
}
