using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Common.Mappers;
using CarTracker.Common.Services;
using CarTracker.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.API.Controllers
{

    [Route("api/car/")]
    public class CarController : Controller
    {

        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            this._carService = carService;
        }
         
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            var car = _carService.Get(id);
            if (null == car)
            {
                return NotFound();
            }
            return Ok(car.ToViewModel());
        }

        [HttpGet]
        [Route("vin/{vin}")]
        public IActionResult GetByVin(string vin)
        {
            var car = _carService.Get(vin);
            if (null == car)
            {
                return NotFound();
            }
            return Ok(car.ToViewModel());
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            var cars = _carService.GetAll();
            return Ok(cars.ToViewModel());
        }

        [HttpGet]
        [Route("registered/{vin}")]
        public IActionResult IsRegistered(string vin)
        {
            var exists = null != _carService.Get(vin);
            return Ok(exists);
        }
    }
}
