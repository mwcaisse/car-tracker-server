using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Common.Mappers;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels;
using CarTracker.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers.Api
{

    [Route("api/car/")]
    public class CarApiController : BaseApiController
    {

        private readonly ICarService _carService;

        public CarApiController(ICarService carService)
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
        public IActionResult GetAllPaged(int skip = 0, int take = 25, SortParam sort = null)
        {
            return Ok(_carService.GetAllPaged(skip, take, sort).ToViewModel());
        }

        [HttpGet]
        [Route("registered/{vin}")]
        public IActionResult IsRegistered(string vin)
        {
            var exists = null != _carService.Get(vin);
            return Ok(exists);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create(CarViewModel car)
        {
            return Ok(_carService.Create(car).ToViewModel());
        }

        [HttpPut]
        [Route("")]
        public IActionResult Update(CarViewModel car)
        {
            return Ok(_carService.Update(car).ToViewModel());
        }
    }
}
