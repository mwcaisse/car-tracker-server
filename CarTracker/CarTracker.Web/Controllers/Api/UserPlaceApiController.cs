using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Common.Services;
using CarTracker.Common.Services.Places;
using CarTracker.Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers.Api
{
    [Authorize]
    [Route("api/place/user")]
    public class UserPlaceApiController : BaseApiController
    {
        private readonly IUserPlaceService _userPlaceService;

        public UserPlaceApiController(IUserPlaceService userPlaceService)
        {
            this._userPlaceService = userPlaceService;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            var userPlace = _userPlaceService.Get(id);
            if (null == userPlace)
            {
                return NotFound();
            }
            return Ok(userPlace.ToViewModel());
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetMine(int skip = DefaultSkip, int take = DefaultTake, SortParam sort = null)
        {
            return Ok(_userPlaceService.GetMine(skip, take, sort).ToViewModel());
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] UserPlaceViewModel userPlace)
        {
            return Ok(_userPlaceService.Create(userPlace).ToViewModel());
        }

        [HttpPut]
        [Route("")]
        public IActionResult Update([FromBody] UserPlaceViewModel userPlace)
        {
            return Ok(_userPlaceService.Update(userPlace).ToViewModel());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(long id)
        {
            _userPlaceService.Delete(id);
            return Ok();
        }
    }
}
