using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Common.Mappers;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels;
using CarTracker.Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers.Api
{
    [Authorize]
    [Route("api")]
    public class ReadingApiController : BaseApiController
    {

        private readonly IReadingService _readingService;

        public ReadingApiController(IReadingService readingService)
        {
            this._readingService = readingService;
        }

        [HttpGet]
        [Route("reading/{id}")]
        public IActionResult Get(long id)
        {
            return Ok(_readingService.Get(id).ToViewModel());
        }

        [HttpGet]
        [Route("car/{carId}/trip/{tripId}/reading/")]
        [Route("trip/{tripId}/reading/")]
        public IActionResult GetForTrip(long tripId)
        {
            return Ok(_readingService.GetForTrip(tripId).ToViewModel());
        }

        [HttpPost]
        [Route("car/{carId}/trip/{tripId}/reading/bulk")]
        [Route("trip/{tripId}/reading/bulk")]
        public IActionResult BulkUpload(long tripId,
            IEnumerable<BulkUploadViewModel<ReadingViewModel>> readings)
        {
            return Ok(_readingService.BulkUpload(tripId, readings));
        }

    }
}
