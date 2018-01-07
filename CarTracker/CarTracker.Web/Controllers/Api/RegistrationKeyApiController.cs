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
    [Route("api/registration-key")]
    public class RegistrationKeyApiController : BaseApiController
    {

        private readonly IRegistrationKeyService _registrationKeyService;

        public RegistrationKeyApiController(IRegistrationKeyService registrationKeyService)
        {
            this._registrationKeyService = registrationKeyService;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            var key = _registrationKeyService.Get(id);
            if (null != key)
            {
                return Ok(key.ToViewModel());
            }
            return NotFound($"No registration key with id: {id} found.");
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll(int skip = DefaultSkip, int take = DefaultTake,
            SortParam sort = null)
        {
            return Ok(_registrationKeyService.GetAll(skip, take, sort).ToViewModel());
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create(UserRegistrationKeyViewModel toCreate)
        {
            return Ok(_registrationKeyService.Create(toCreate).ToViewModel());
        }

        [HttpPut]
        [Route("")]
        public IActionResult Update(UserRegistrationKeyViewModel toUpdate)
        {
            return Ok(_registrationKeyService.Update(toUpdate).ToViewModel());
        }

    }
}
