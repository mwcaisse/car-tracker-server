using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.API.Controllers
{

    [Route("api/car")]
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
            return Ok(car);
        }
    }
}
