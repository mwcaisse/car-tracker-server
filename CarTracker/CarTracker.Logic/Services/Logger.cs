﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities.Logging;
using CarTracker.Common.Enums;
using CarTracker.Common.Services;
using CarTracker.Data;
using Newtonsoft.Json;

namespace CarTracker.Logic.Services
{
    public class Logger : IRequestLogger, IServerLogger
    {

        private readonly CarTrackerDbContext _db;
        private readonly Guid _eventGuid;

        public Logger(CarTrackerDbContext db)
        {
            _db = db;
            _eventGuid = Guid.NewGuid();
        }

        public void LogRequest(string clientAddress, string requestUrl, string requestMethod, 
            IEnumerable requestHeaders, string requestBody, string responseStatus, 
            IEnumerable responseHeaders, string responseBody)
        {
            var requestLog = new RequestLog()
            {
                ClientAddress = clientAddress,
                RequestUrl = requestUrl,
                RequestMethod = requestMethod,
                RequestHeaders = JsonConvert.SerializeObject(requestHeaders),
                RequestBody = requestBody,
                ResponseStatus = responseStatus,
                ResponseHeaders = JsonConvert.SerializeObject(responseHeaders),
                ResponseBody = responseBody,
                Type = LogType.Debug,
                RequestUuid = _eventGuid
            };
            _db.RequestLogs.Add(requestLog);
            _db.SaveChanges();
        }

        public void Debug(string message)
        {
            CreateServerLog(LogType.Debug, message);
        }

        public void Info(string message)
        {
            CreateServerLog(LogType.Info, message);
        }

        public void Warn(string message)
        {
            CreateServerLog(LogType.Warn, message);
        }

        public void Warn(string message, Exception e)
        {
            CreateServerLog(LogType.Warn, message, e);
        }

        public void Warn(Exception e)
        {
            CreateServerLog(LogType.Warn, null, e);
        }

        public void Error(string message)
        {
            CreateServerLog(LogType.Error, message);
        }

        public void Error(string message, Exception e)
        {
            CreateServerLog(LogType.Error, message, e);
        }

        public void Error(Exception e)
        {
            CreateServerLog(LogType.Error, null, e);
        }

        protected void CreateServerLog(LogType type, string message, Exception e = null)
        {
            var serverLog = new ServerLog()
            {
                RequestUuid = _eventGuid,
                Message = message,
                ExceptionMessage = e?.Message,
                Type = type,
                StackTrace = e?.StackTrace
            };

            _db.ServerLogs.Add(serverLog);
            _db.SaveChanges();
        }
    }
}
