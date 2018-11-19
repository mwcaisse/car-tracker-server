using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Web.Configuration;
using CarTracker.Web.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarTracker.Web.Controllers.View
{
    public class BaseViewController : Controller
    {

        protected readonly ApplicationConfiguration ApplicationConfiguration;
        protected readonly BuildInformation BuildInformation;

        public BaseViewController(ApplicationConfiguration applicationConfiguration,
            BuildInformation buildInformation)
        {
            this.ApplicationConfiguration = applicationConfiguration;
            this.BuildInformation = buildInformation;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.GoogleMapsApiKey = ApplicationConfiguration.GoogleMapsAPiKey;
            ViewBag.IsAuthenticated = IsAuthenticated();
            ViewBag.RootPathPrefix = ApplicationConfiguration.RootPathPrefix;

            ViewBag.BuildInformation = BuildInformation;
        }

        protected IActionResult VueView(string viewName, string title = "", params VueViewProperty[] properties)
        {
            ViewBag.ViewName = viewName;
            ViewBag.ViewTitle = title;
            ViewBag.Properties = properties;
            return View("VueView");
        }

        protected bool IsAuthenticated()
        {
            return null != User && User.Identity.IsAuthenticated;
        }

        protected bool ContainsUrlParameter(string parameterName)
        {
            return Request.Query.ContainsKey(parameterName);
        }
    }
}
