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
    public class ServerLogService : IServerLogService
    {

        private readonly CarTrackerDbContext _db;

        public ServerLogService(CarTrackerDbContext db)
        {
            _db = db;
        }

        public ServerLog Get(long id)
        {
            return _db.ServerLogs.FirstOrDefault(l => l.ServerLogId == id);
        }

        public PagedViewModel<ServerLog> GetAll(int skip, int take, SortParam sort)
        {
            return _db.ServerLogs.PageAndSort(skip, take, sort);
        }

        public PagedViewModel<ServerLog> GetForEvent(string eventId, int skip, int take, SortParam sort)
        {
            var guid = new Guid(eventId);
            return _db.ServerLogs.Where(l => l.RequestUuid == guid).PageAndSort(skip, take, sort);
        }
    }
}
