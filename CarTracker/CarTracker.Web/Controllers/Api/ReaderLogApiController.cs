using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Common.Mappers;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers.Api
{
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
        public IActionResult GetAll(int skip, int take, SortParam sort = null)
        {
            return Ok(_readerLogService.GetAllPaged(skip, take, sort).ToViewModel());
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create(ReaderLogViewModel readerLog)
        {
            return Ok(_readerLogService.Create(readerLog));
        }

        [HttpPut]
        [Route("")]
        public IActionResult Update(ReaderLogViewModel readerLog)
        {
            return Ok(_readerLogService.Update(readerLog));
        }

        [HttpPost]
        [Route("bulk")]
        public IActionResult BulkUpload(
            IEnumerable<BulkUploadViewModel<ReaderLogViewModel>> readerLogs)
        {
            return Ok(_readerLogService.BulkUpload(readerLogs));            
        }
    }
}
