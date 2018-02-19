using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Common.Mappers;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels;
using CarTracker.Web.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers.Api
{
    [Authorize]
    [Route("api/log/reader")]
    public class ReaderLogApiController : BaseApiController
    {

        private readonly IReaderLogService _readerLogService;

        public ReaderLogApiController(IReaderLogService readerLogService)
        {
            this._readerLogService = readerLogService;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            var readerLog = _readerLogService.Get(id);
            if (null == readerLog)
            {
                return NotFound();
            }
            return Ok(readerLog.ToViewModel());
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll(int skip = DefaultSkip, int take = DefaultTake, SortParam sort = null,
            Dictionary<string, string> filters = null)
        {
            filters = filters.CleanFilterParameters();
            return Ok(_readerLogService.GetAllPaged(skip, take, sort, filters).ToViewModel());
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] ReaderLogViewModel readerLog)
        {
            return Ok(_readerLogService.Create(readerLog));
        }

        [HttpPut]
        [Route("")]
        public IActionResult Update([FromBody] ReaderLogViewModel readerLog)
        {
            return Ok(_readerLogService.Update(readerLog));
        }

        [HttpPost]
        [Route("bulk")]
        public IActionResult BulkUpload([FromBody]
            IEnumerable<BulkUploadViewModel<ReaderLogViewModel>> readerLogs)
        {
            return Ok(_readerLogService.BulkUpload(readerLogs));            
        }
    }
}
