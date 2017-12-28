using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Common.Enums;
using CarTracker.Common.Mappers;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers.Api
{
    [Route("api")]
    public class TripPossiblePlaceApiController : BaseApiController
    {

        private readonly ITripPossiblePlaceService _tripPossiblePlaceService;

        public TripPossiblePlaceApiController(ITripPossiblePlaceService tripPossiblePlaceService)
        {
            this._tripPossiblePlaceService = tripPossiblePlaceService;
        }

        [HttpGet]
        [Route("trip/{tripId}/possible-places/{type}")]
        [Route("car/{carId}/trip/{tripId}/possible-places/{type}")]
        public IActionResult GetPossiblePlacesForTrip(long tripId, string type, 
            int skip, int take, SortParam sort = null)
        {
            var typeEnum = TripPossiblePlaceTypeExtentions.FromString(type);
            return Ok(_tripPossiblePlaceService.GetForTripOfTypePaged(tripId, typeEnum, skip, take, sort)
                .ToViewModel());
        }

    }
}
