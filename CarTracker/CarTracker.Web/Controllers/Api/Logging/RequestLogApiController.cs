using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Common.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers.Api.Logging
{
    [Produces("application/json")]
    [Route("api/log")]
    public class RequestLogApiController : BaseApiController
    {

        [HttpGet]
        [Route("request/{id}")]
        public IActionResult Get(long id)
        {
            return Ok();
        }

        [HttpGet]
        [Route("request/")]
        public IActionResult GetAll(int skip = DefaultSkip, int take = DefaultTake, 
            SortParam sort = null)
        {
            return Ok();
        }

        [HttpGet]
        [Route("event/{id}/request")]
        public IActionResult GetByEventId(string id)
        {
            return Ok();
        }
    }
}