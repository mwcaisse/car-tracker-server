using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Enums;

namespace CarTracker.Common.Entities.Logging
{
    public class ServerLog : ITrackedEntity
    {
        public long ServerLogId { get; set; }
        public Guid RequestUuid { get; set; }
        public LogType Type { get; set; }
        public string Message { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
