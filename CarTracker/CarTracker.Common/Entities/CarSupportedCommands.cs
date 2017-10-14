using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Entities
{
    public class CarSupportedCommands : ITrackedEntity
    {

        public long CarSupportedCommandsId { get; set; }

        public long CarId { get; set; }

        public int Pids0120Bitmask { get; set; }

        public int Pids2140Bitmask { get; set; }

        public int Pids4160Bitmask { get; set; }

        public int Pids6180Bitmask { get; set; }

        public int Pids81A0Bitmask { get; set; }

        public virtual Car Car { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
