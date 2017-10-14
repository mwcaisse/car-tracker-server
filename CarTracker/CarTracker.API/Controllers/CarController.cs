using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Common.Mappers;
using CarTracker.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.API.Controllers
{

    [Route("api/car/")]
    public class CarController : Controller
    {

        private readonly CarTrackerDbContext _dbContext;

        public CarController(CarTrackerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
         
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            var car = _dbContext.Cars.FirstOrDefault(c => c.CarId == id);
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
            var car = _dbContext.Cars.FirstOrDefault(c => c.Vin == vin);
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
            var cars = _dbContext.Cars.ToList();
            return Ok(cars.ToViewModel());
        }

        [HttpGet]
        [Route("registered/{vin}")]
        public IActionResult IsRegistered(string vin)
        {
            var exists = _dbContext.Cars.Any(c => c.Vin == vin);
            return Ok(exists);
        }
    }
}
