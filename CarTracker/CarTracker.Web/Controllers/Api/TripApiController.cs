﻿using System;
using CarTracker.Common.Mappers;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers.Api
{
    [Authorize]
    [Route("api")]
    public class TripApiController : BaseApiController
    {

        private readonly ITripService _tripService;
        private readonly ITripProcessor _tripProcessor;

        public TripApiController(ITripService tripService, ITripProcessor tripProcessor)
        {
            this._tripService = tripService;
            this._tripProcessor = tripProcessor;
        }

        [HttpGet]
        [Route("trip/{id}")]
        [Route("car/{carId}/trip/{id}")]
        public IActionResult Get(long id)
        {
            var trip = _tripService.Get(id);
            if (null == trip)
            {
                return NotFound();
            }
            return Ok(trip.ToViewModel());
        }

        [HttpGet]
        [Route("car/{carId}/trip/")]
        public IActionResult GetTripsForCar(long carId, int skip = DefaultSkip, 
            int take = DefaultTake, SortParam sort = null)
        {
            return Ok(_tripService.GetForCar(carId, skip, take, sort).ToViewModel());
        }

        [HttpGet]
        [Route("car/{carId}/trip/history/")]
        public IActionResult GetTripHistoryForCar(long carId, long startDate, long endDate)
        {
            return Ok(_tripService.GetTripHistory(carId, 
                DateTimeOffset.FromUnixTimeMilliseconds(startDate).DateTime.ToLocalTime(), 
                DateTimeOffset.FromUnixTimeMilliseconds(endDate).DateTime.ToLocalTime()));
        }

        [HttpPost]
        [Route("car/{carId}/trip/")]
        public IActionResult Create(long carId, [FromBody]TripViewModel trip)
        {
            return Ok(_tripService.Create(carId, trip).ToViewModel());
        }

        [HttpPut]
        [Route("car/{carId}/trip/")]
        [Route("trip/")]
        public IActionResult Update([FromBody] TripViewModel trip)
        {
            return Ok(_tripService.Update(trip).ToViewModel());
        }

        [HttpPost]
        [Route("trip/process/unprocessed")]
        public IActionResult ProcessUnprocessedTrips()
        {
            _tripProcessor.ProcessUnprocessedTrips();
            return Ok(true);
        }

        [HttpPost]
        [Route("car/{carId}/trip/{id}/process")]
        [Route("trip/{id}/process")]
        public IActionResult ProcessTrip(long id)
        {
            return Ok(_tripProcessor.ProcessTrip(id).ToViewModel());
        }

        [HttpPost]
        [Route("car/{carId}/trip/{id}/starting-place")]
        [Route("trip/{id}/starting-place")]
        public IActionResult SetStartingPlace(long id, long placeId)
        {
            return Ok(_tripService.SetStartingPlace(id, placeId));
        }

        [HttpPost]
        [Route("car/{carId}/trip/{id}/destination-place")]
        [Route("trip/{id}/destination-place")]
        public IActionResult SetDestinationPlace(long id, long placeId)
        {
            return Ok(_tripService.SetDestinationPlace(id, placeId));
        }

    }
}
