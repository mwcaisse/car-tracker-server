using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities.Logging;
using CarTracker.Common.ViewModels;

namespace CarTracker.Common.Services.Logging
{
    public interface IServerLogService
    {

        /// <summary>
        /// Fetches the Server Log with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ServerLog Get(long id);

        /// <summary>
        /// Fetches all Server Logs with paging
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        PagedViewModel<ServerLog> GetAll(int skip, int take, SortParam sort);

        /// <summary>
        /// Fetches the Server Logs for the given event, with paging
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        PagedViewModel<ServerLog> GetForEvent(string eventId, int skip, int take, SortParam sort);

    }
}
