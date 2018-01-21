using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Enums;

namespace CarTracker.Common.Entities.Logging
{
    public class RequestLog : ITrackedEntity
    {

        public long RequestLogId { get; set; }
        public Guid RequestUuid { get; set; }
        public LogType Type { get; set; }
        public string ClientAddress { get; set; }
        public string RequestUrl { get; set; }
        public string RequestMethod { get; set; }
        public string RequestBody { get; set; }
        public string ResponseStatus { get; set; }
        public string ResponseBody { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
