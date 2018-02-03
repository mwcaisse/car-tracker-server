using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities.Logging;
using CarTracker.Common.Mappers.Auth;
using CarTracker.Common.ViewModels;
using CarTracker.Common.ViewModels.Logging;

namespace CarTracker.Common.Mappers.Logging
{
    public static class RequestLogMapper
    {

        public static RequestLogViewModel ToViewModel(this RequestLog log)
        {
            var vm = new RequestLogViewModel()
            {
                RequestLogId = log.RequestLogId,
                RequestUuid = log.RequestUuid,
                Type = log.Type,
                ClientAddress = log.ClientAddress,
                RequestUrl = log.RequestUrl,
                RequestMethod = log.RequestMethod,
                RequestHeaders = log.RequestHeaders,
                RequestBody = log.RequestBody,
                ResponseHeaders = log.ResponseHeaders,
                ResponseBody = log.ResponseBody,
                User = log.User.ToViewModel(),
                UserId = log.UserId,
                CreateDate = log.CreateDate,
                ResponseStatus = log.ResponseStatus
            };

            return vm;
        }

        public static IEnumerable<RequestLogViewModel> ToViewModel(this IEnumerable<RequestLog> logs)
        {
            return logs.Select(l => l.ToViewModel());
        }

        public static PagedViewModel<RequestLogViewModel> ToViewModel(this PagedViewModel<RequestLog> pagedLogs)
        {
            return new PagedViewModel<RequestLogViewModel>(pagedLogs)
            {
                Data = pagedLogs.Data.ToViewModel()
            };
        }
    }
}
