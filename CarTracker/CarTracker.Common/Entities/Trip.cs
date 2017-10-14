using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Entities
{
    public class Trip
    {

        public long TripId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public long CarId { get; set; }
        public double AverageSpeed { get; set; }
        public double MaximumSpeed { get; set; }
        public double AverageEngineRpm { get; set; }
        public double MaxEngineRpm { get; set; }
        public double DistanceTraveled { get; set; }
        public long IdleTime { get; set; }
        public long? StartPlaceId { get; set; }
        public long? DestinationPlaceId { get; set; }
        public string Status { get; set; }

        public virtual Car Car { get; set; }
        public virtual Place StartPlace { get; set; }
        public virtual Place DestinationPlace { get; set; }

        public virtual ICollection<Reading> Readings { get; set; }
    }
}
