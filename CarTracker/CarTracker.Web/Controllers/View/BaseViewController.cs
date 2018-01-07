using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Web.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarTracker.Web.Controllers.View
{
    public class BaseViewController : Controller
    {

        private readonly ApplicationConfiguration _applicationConfiguration;

        public BaseViewController(ApplicationConfiguration applicationConfiguration)
        {
            this._applicationConfiguration = applicationConfiguration;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.GoogleMapsApiKey = _applicationConfiguration.GoogleMapsAPiKey;
            ViewBag.IsAuthenticated = IsAuthenticated();
        }

        protected bool IsAuthenticated()
        {
            return null != User && User.Identity.IsAuthenticated;
        }
    }
}
