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
    [Route("api/log/")]
    public class ServerLogApiController : BaseApiController
    {

        [HttpGet]
        [Route("server/{id}")]
        public IActionResult Get(long id)
        {
            return Ok();
        }

        [HttpGet]
        [Route("server/")]
        public IActionResult GetAll(int skip = DefaultSkip,
            int take = DefaultTake, SortParam sort = null)
        {
            return Ok();
        }

        [HttpGet]
        [Route("event/{id}/server")]
        public IActionResult GetForEvent(string id, int skip = DefaultSkip, 
            int take = DefaultTake, SortParam sort = null)
        {
            return Ok();
        }
 
    }
}