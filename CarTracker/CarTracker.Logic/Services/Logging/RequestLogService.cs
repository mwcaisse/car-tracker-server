using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities.Logging;
using CarTracker.Common.Services.Logging;
using CarTracker.Common.ViewModels;
using CarTracker.Data;
using CarTracker.Data.Extensions;

namespace CarTracker.Logic.Services.Logging
{
    public class RequestLogService : IRequestLogService
    {

        private readonly CarTrackerDbContext _db;

        public RequestLogService(CarTrackerDbContext db)
        {
            _db = db;
        }

        public RequestLog Get(long id)
        {
            return _db.RequestLogs.Build().FirstOrDefault(l => l.RequestLogId == id);
        }

        public RequestLog GetByEventId(string eventId)
        {
            var guid = new Guid(eventId);
            return _db.RequestLogs.Build().FirstOrDefault(l => l.RequestUuid == guid);
        }

        public PagedViewModel<RequestLog> GetAll(int skip, int take, SortParam sort,
            IEnumerable<FilterParam> filters)
        {
            return _db.RequestLogs.Build().Filter(filters).PageAndSort(skip, take, sort);
        }

        public IEnumerable<string> GetMethodFilters()
        {
            return _db.RequestLogs.Build().Select(l => l.RequestMethod).Distinct();
        }

        public IEnumerable<string> GetStatusFilters()
        {
            return _db.RequestLogs.Build().Select(l => l.ResponseStatus).Distinct();
        }
    }
}
