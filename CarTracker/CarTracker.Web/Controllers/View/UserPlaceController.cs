using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Web.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers.View
{
    [Route("user/places")]
    public class UserPlaceController : BaseViewController
    {
        public UserPlaceController(ApplicationConfiguration applicationConfiguration, 
            BuildInformation buildInformation) : base(applicationConfiguration, buildInformation)
        {
        }

        public IActionResult Index()
        {
            return VueView("views/User/Places", "User Places");
        }
        
    }
}
