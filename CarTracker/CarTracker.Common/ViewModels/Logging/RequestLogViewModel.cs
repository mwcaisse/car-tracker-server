using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Enums;
using CarTracker.Common.ViewModels.Auth;

namespace CarTracker.Common.ViewModels.Logging
{
    public class RequestLogViewModel
    {
        public long RequestLogId { get; set; }
        public Guid RequestUuid { get; set; }
        public LogType Type { get; set; }
        public string ClientAddress { get; set; }
        public string RequestUrl { get; set; }
        public string RequestMethod { get; set; }
        public string RequestHeaders { get; set; }
        public string RequestBody { get; set; }
        public string ResponseStatus { get; set; }
        public string ResponseHeaders { get; set; }
        public string ResponseBody { get; set; }
        public DateTime CreateDate { get; set; }
        public long? UserId { get; set; }

        public UserViewModel User { get; set; }
    }
}
