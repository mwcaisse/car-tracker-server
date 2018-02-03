using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities.Logging;
using CarTracker.Common.ViewModels;
using CarTracker.Common.ViewModels.Logging;

namespace CarTracker.Common.Mappers.Logging
{
    public static class ServerLogMapper
    {

        public static ServerLogViewModel ToViewModel(this ServerLog log)
        {
            var vm = new ServerLogViewModel()
            {
                Type = log.Type,
                CreateDate = log.CreateDate,
                Message = log.Message,
                RequestUuid = log.RequestUuid,
                ServerLogId = log.ServerLogId,
                ExceptionMessage = log.ExceptionMessage,
                StackTrace = log.StackTrace
            };
            return vm;
        }

        public static IEnumerable<ServerLogViewModel> ToViewModel(this IEnumerable<ServerLog> logs)
        {
            return logs.Select(l => l.ToViewModel());
        }

        public static PagedViewModel<ServerLogViewModel> ToViewModel(this PagedViewModel<ServerLog> pagedLogs)
        {
            return new PagedViewModel<ServerLogViewModel>(pagedLogs)
            {
                Data = pagedLogs.Data.ToViewModel()
            };
        }

    }
}
