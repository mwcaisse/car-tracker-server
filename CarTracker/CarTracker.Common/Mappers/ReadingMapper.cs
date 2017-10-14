using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.ViewModels;

namespace CarTracker.Common.Mappers
{
    public static class ReadingMapper
    {

        public static ReadingViewModel ToViewModel(this Reading reading)
        {
            var vm = new ReadingViewModel()
            {
                Id = reading.ReadingId,
                ReadDate = reading.ReadDate,
                TripId = reading.TripId,
                Latitude = reading.Latitude,
                Longitude = reading.Longitude,
                AirIntakeTemperature = reading.AirIntakeTemperature,
                AmbientAirTemperature = reading.AmbientAirTemperature,
                EngineCoolantTemperature = reading.EngineCoolantTemperature,
                OilTemperature = reading.OilTemperature,
                EngineRpm = reading.EngineRpm,
                Speed = reading.Speed,
                MassAirFlow = reading.MassAirFlow,
                ThrottlePosition = reading.ThrottlePosition,
                FuelType = reading.FuelType,
                FuelLevel = reading.FuelLevel
            };

            return vm;
        }

        public static IEnumerable<ReadingViewModel> ToViewModel(this IEnumerable<Reading> readings)
        {
            return readings.Select(r => r.ToViewModel());
        }

    }
}
