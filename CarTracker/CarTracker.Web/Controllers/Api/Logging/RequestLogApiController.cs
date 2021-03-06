﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Common.Mappers.Logging;
using CarTracker.Common.Services.Logging;
using CarTracker.Common.ViewModels;
using CarTracker.Web.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers.Api.Logging
{
    [Produces("application/json")]
    [Route("api/log")]
    public class RequestLogApiController : BaseApiController
    {

        private readonly IRequestLogService _requestLogService;

        public RequestLogApiController(IRequestLogService requestLogService)
        {
            _requestLogService = requestLogService;
        }

        [HttpGet]
        [Route("request/{id}")]
        public IActionResult Get(long id)
        {
            return Ok(_requestLogService.Get(id).ToViewModel());
        }

        [HttpGet]
        [Route("request/")]
        public IActionResult GetAll(int skip = DefaultSkip, int take = DefaultTake, 
            SortParam sort = null, Dictionary<string, string> filters = null)
        {
            return Ok(_requestLogService.GetAll(skip, take, sort, filters.ConvertToFilterParams()).ToViewModel());
        }

        [HttpGet]
        [Route("event/{id}/request")]
        public IActionResult GetByEventId(string id)
        {
            return Ok(_requestLogService.GetByEventId(id).ToViewModel());
        }

        [HttpGet]
        [Route("request/filters/method")]
        public IActionResult GetMethodFilters()
        {
            return Ok(_requestLogService.GetMethodFilters());
        }

        [HttpGet]
        [Route("request/filters/status")]
        public IActionResult GetStatusFilters()
        {
            return Ok(_requestLogService.GetStatusFilters());
        }
    }
}