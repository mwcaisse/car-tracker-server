using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarTracker.Web.Controllers
{

    [Route("api/car/")]
    public class CarSupportedCommandsController : Controller
    {

        [HttpGet("{id:int}/supported-commands")]
        public IActionResult GetSupportedCommandsByCarId(long carId)
        {
            return Ok();
        }

        [HttpGet("{vin:string}/supported-commands")]
        public IActionResult GetSupportedCommandsByVin(string vin)
        {
            return Ok();
        }

        [HttpPost("{id:int}/supported-commands")]
        public IActionResult CreateOrUpdateByCarId(long carId)
        {
            return Ok();
        }

        [HttpPost("{vin:string}/supported-commands")]
        public IActionResult CreateOrUpdateByVin(string vin)
        {
            return Ok();
        }

    }
}
