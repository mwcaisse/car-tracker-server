using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Common.Entities;
using CarTracker.Common.Mappers;
using CarTracker.Common.Services;
using Microsoft.AspNetCore.Authorization;

namespace CarTracker.Web.Controllers.Api
{

    [Route("api/car/")]
    [Authorize]
    public class CarSupportedCommandsApiController : BaseApiController
    {

        private readonly ICarSupportedCommandsService _carSupportedCommandsService;

        public CarSupportedCommandsApiController(ICarSupportedCommandsService carSupportedCommandsService)
        {
            _carSupportedCommandsService = carSupportedCommandsService;
        }

        [HttpGet("{carId:int}/supported-commands")]
        public IActionResult GetSupportedCommandsByCarId(long carId)
        {
            var supportedCommands = _carSupportedCommandsService.GetForCar(carId);
            if (null == supportedCommands)
            {
                return NotFound("No supported commands found for car with id: " + carId);
            }
            return Ok(supportedCommands.ToViewModel());
        }

        [HttpGet("{vin}/supported-commands")]
        public IActionResult GetSupportedCommandsByVin(string vin)
        {
            var supportedCommands = _carSupportedCommandsService.GetForCar(vin);
            if (null == supportedCommands)
            {
                return NotFound("No supported commands found for car with VIN: " + vin);
            }
            return Ok(supportedCommands.ToViewModel());
        }

        [HttpPost("{carId:int}/supported-commands")]
        public IActionResult CreateOrUpdateByCarId(long carId, CarSupportedCommands model)
        {
            return Ok(_carSupportedCommandsService.CreateOrUpdate(carId, model).ToViewModel());
        }

        [HttpPost("{vin}/supported-commands")]
        public IActionResult CreateOrUpdateByVin(string vin, CarSupportedCommands model)
        {
            return Ok(_carSupportedCommandsService.CreateOrUpdate(vin, model).ToViewModel());
        }

    }
}
