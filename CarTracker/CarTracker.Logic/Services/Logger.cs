using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities.Logging;
using CarTracker.Common.Enums;
using CarTracker.Common.Models;
using CarTracker.Common.Services;
using CarTracker.Data;
using Newtonsoft.Json;

namespace CarTracker.Logic.Services
{
    public class Logger : IRequestLogger, IServerLogger
    {

        private readonly CarTrackerDbContextFactory _dbFactory;
        private readonly IRequestInformation _requestInformation;
        private readonly Guid _eventGuid;

        public Logger(CarTrackerDbContextFactory dbFactory, IRequestInformation requestInformation)
        {
            _dbFactory = dbFactory;
            _requestInformation = requestInformation;
            _eventGuid = Guid.NewGuid();
        }

        public void LogRequest(string clientAddress, string requestUrl, string requestMethod, 
            IEnumerable requestHeaders, string requestBody, string responseStatus, 
            IEnumerable responseHeaders, string responseBody)
        {
            using (var db = _dbFactory.CreateContext())
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
                    RequestUuid = _eventGuid,
                    UserId = _requestInformation.IsAuthenticated ? _requestInformation.UserId : (long?) null
            };
                db.RequestLogs.Add(requestLog);
                db.SaveChanges();
            }
            
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
            using (var db = _dbFactory.CreateContext())
            {
                var serverLog = new ServerLog()
                {
                    RequestUuid = _eventGuid,
                    Message = message,
                    ExceptionMessage = e?.Message,
                    Type = type,
                    StackTrace = e?.StackTrace
                };

                db.ServerLogs.Add(serverLog);
                db.SaveChanges();
            }
           
        }
    }
}
