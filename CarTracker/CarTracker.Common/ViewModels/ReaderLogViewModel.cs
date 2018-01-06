using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Enums;

namespace CarTracker.Common.ViewModels
{
    public class ReaderLogViewModel
    {

        public long Id { get; set; }

        public LogType Type { get; set; }
        
        public string Message { get; set; }

        public DateTime Date { get; set; }

    }
}
