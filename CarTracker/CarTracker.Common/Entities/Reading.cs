using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Entities
{
    public class Reading : ITrackedEntity
    {
        public long ReadingId { get; set; }

        public DateTime ReadDate { get; set; }

        public long TripId { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? AirIntakeTemperature { get; set; }
        public double? AmbientAirTemperature { get; set; }
        public double? EngineCoolantTemperature { get; set; }
        public double? OilTemperature { get; set; }
        public double? EngineRpm { get; set; }
        public double? Speed { get; set; }
        public double? MassAirFlow { get; set; }
        public double? ThrottlePosition { get; set; }
        public string FuelType { get; set; }
        public double? FuelLevel { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Trip Trip { get; set; }

       
    }
}
