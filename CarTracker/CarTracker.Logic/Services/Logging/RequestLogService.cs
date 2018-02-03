﻿using System;
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

        public PagedViewModel<RequestLog> GetAll(int skip, int take, SortParam sort)
        {
            return _db.RequestLogs.Build().PageAndSort(skip, take, sort);
        }
    }
}
