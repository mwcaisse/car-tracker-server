﻿using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Enums;

namespace CarTracker.Common.ViewModels.Logging
{
    public class ServerLogViewModel
    {

        public long ServerLogId { get; set; }
        public Guid RequestUuid { get; set; }
        public LogType Type { get; set; }
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
