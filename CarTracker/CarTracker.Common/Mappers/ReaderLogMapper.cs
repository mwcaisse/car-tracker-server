using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.ViewModels;

namespace CarTracker.Common.Mappers
{
    public static class ReaderLogMapper
    {

        public static ReaderLogViewModel ToViewModel(this ReaderLog log)
        {
            var vm = new ReaderLogViewModel()
            {
                Id = log.ReaderLogId,
                Type = log.Type,
                Message = log.Message,
                Date = log.Date
            };

            return vm;
        }

        public static IEnumerable<ReaderLogViewModel> ToViewModel(this IEnumerable<ReaderLog> logs)
        {
            return logs.Select(l => l.ToViewModel());
        }
    }
}
