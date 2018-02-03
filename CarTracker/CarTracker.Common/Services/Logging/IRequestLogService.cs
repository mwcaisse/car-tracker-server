using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities.Logging;
using CarTracker.Common.ViewModels;

namespace CarTracker.Common.Services.Logging
{
    public interface IRequestLogService
    {

        /// <summary>
        /// Fetches the Request Log with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RequestLog Get(long id);

        /// <summary>
        /// Fetches the Request Log with the given Event Id
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        RequestLog GetByEventId(string eventId);

        /// <summary>
        /// Fetches all Request Logs with paging
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        PagedViewModel<RequestLog> GetAll(int skip, int take, SortParam sort);
        
    }
}
