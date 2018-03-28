using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities.Auth;

namespace CarTracker.Common.Entities
{
    public class Car : ITrackedEntity
    {

        public long CarId { get; set; }

        public string Vin { get; set; }
        
        public string Name { get; set; }

        public long? OwnerId { get; set; }

        public double? Mileage { get; set; }

        public DateTime? MileageLastUserSet { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual User Owner { get; set; }

        public virtual CarSupportedCommands CarSupportedCommands { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }

        public virtual ICollection<CarMaintenance> CarMaintenances { get; set; }

    }
}
