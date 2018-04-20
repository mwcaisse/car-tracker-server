using System;
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
    [Route("api/log/")]
    public class ServerLogApiController : BaseApiController
    {

        private readonly IServerLogService _serverLogService;

        public ServerLogApiController(IServerLogService serverLogService)
        {
            _serverLogService = serverLogService;
        }

        [HttpGet]
        [Route("server/{id}")]
        public IActionResult Get(long id)
        {
            return Ok(_serverLogService.Get(id).ToViewModel());
        }

        [HttpGet]
        [Route("server/")]
        public IActionResult GetAll(int skip = DefaultSkip,
            int take = DefaultTake, SortParam sort = null, Dictionary<string, string> filters = null)
        {
            return Ok(_serverLogService.GetAll(skip, take, sort, 
                filters.ConvertToFilterParams()).ToViewModel());
        }

        [HttpGet]
        [Route("event/{id}/server")]
        public IActionResult GetForEvent(string id, int skip = DefaultSkip, 
            int take = DefaultTake, SortParam sort = null, Dictionary<string, string> filters = null)
        {
            return Ok(_serverLogService.GetForEvent(id, skip, take, sort, 
                filters.ConvertToFilterParams()).ToViewModel());
        }
 
    }
}