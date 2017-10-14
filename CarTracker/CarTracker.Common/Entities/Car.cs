using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Entities
{
    public class Car
    {

        public long CarId { get; set; }

        public string Vin { get; set; }
        
        public string Name { get; set; }

        public long OwnerId { get; set; }

        public double Mileage { get; set; }

        public DateTime MileageLastUserSet { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual User Owner { get; set; }

    }
}
