using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers.Api
{
    public class BaseApiController : Controller
    {

        protected const int DefaultSkip = 0;
        protected const int DefaultTake = 25;

    }
}
