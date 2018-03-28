using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Enums;

namespace CarTracker.Common.Entities
{
    public class CarMaintenance : ITrackedEntity
    {
        public long CarMaintenanceId { get; set; }

        public long CarId { get; set; }

        public CarMaintenanceType Type { get; set; }
        
        public string Notes { get; set; }

        public DateTime Date { get; set; }

        public double Mileage { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Car Car { get; set; }
    }
}
