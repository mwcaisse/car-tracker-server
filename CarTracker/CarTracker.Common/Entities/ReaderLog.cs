using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Entities
{
    public class ReaderLog : ITrackedEntity
    {

        public long ReaderLogId { get; set; }

        public string Type { get; set; }

        public string Message { get; set; }
        
        public DateTime Date { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
