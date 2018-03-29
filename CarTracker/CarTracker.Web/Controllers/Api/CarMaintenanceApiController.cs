using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Common.Mappers;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers.Api
{


    [Authorize]
    [Route("/api/car/{carId}/maintenance/")]
    public class CarMaintenanceApiController : BaseApiController
    {

        private readonly ICarMaintenanceService _carMaintenanceService;

        public CarMaintenanceApiController(ICarMaintenanceService carMaintenanceService)
        {
            this._carMaintenanceService = carMaintenanceService;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            var maint = _carMaintenanceService.Get(id);
            if (null == maint)
            {
                return NotFound();
            }
            return Ok(maint.ToViewModel());
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetForCarPaged(long carId, int skip = DefaultSkip, int take = DefaultTake,
            SortParam sort = null)
        {
            return Ok(_carMaintenanceService.GetForCarPaged(carId, skip, take, sort).ToViewModel());
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] CarMaintenanceViewModel vm)
        {
            return Ok(_carMaintenanceService.Create(vm).ToViewModel());
        }

        [HttpPut]
        [Route("")]
        public IActionResult Update([FromBody] CarMaintenanceViewModel vm)
        {
            return Ok(_carMaintenanceService.Update(vm).ToViewModel());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(long id)
        {
            return Ok(_carMaintenanceService.Delete(id));
        }

    }

 
}
