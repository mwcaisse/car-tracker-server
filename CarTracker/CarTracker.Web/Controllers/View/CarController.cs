﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers.View
{
    public class CarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}