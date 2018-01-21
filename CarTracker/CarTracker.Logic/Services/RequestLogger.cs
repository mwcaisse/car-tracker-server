using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities.Logging;
using CarTracker.Common.Enums;
using CarTracker.Common.Services;
using CarTracker.Data;

namespace CarTracker.Logic.Services
{
    public class RequestLogger : IRequestLogger
    {

        private readonly CarTrackerDbContext _db;
        private readonly Guid _eventGuid;

        public RequestLogger(CarTrackerDbContext db)
        {
            _db = db;
            _eventGuid = Guid.NewGuid();
        }

        public void LogRequest(string clientAddress, string requestUrl, string requestMethod, 
            string requestBody, string responseStatus, string responseBody)
        {
            var requestLog = new RequestLog()
            {
                ClientAddress = clientAddress,
                RequestUrl = requestUrl,
                RequestMethod = requestMethod,
                RequestBody = requestBody,
                ResponseStatus = responseStatus,
                ResponseBody = responseBody,
                Type = LogType.Debug,
                RequestUuid = _eventGuid
            };
            _db.RequestLogs.Add(requestLog);
            _db.SaveChanges();
        }
    }
}
