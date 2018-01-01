using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels;
using CarTracker.Data;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;

namespace CarTracker.Logic.Services
{
    public class ReadingService : IReadingService
    {

        private readonly CarTrackerDbContext _db;

        public ReadingService(CarTrackerDbContext db)
        {
            this._db = db;
        }

        public Reading Get(long id)
        {
            return _db.Readings.FirstOrDefault(r => r.ReadingId == id);
        }

        public IEnumerable<Reading> GetForTrip(long tripId)
        {
            return _db.Readings.Where(r => r.TripId == tripId).OrderBy(r => r.ReadDate);
        }

        public Reading Create(ReadingViewModel toCreate, bool save = true)
        {
            var reading = new Reading()
            {
                TripId = toCreate.TripId,
                AirIntakeTemperature = toCreate.AirIntakeTemperature,
                AmbientAirTemperature = toCreate.AmbientAirTemperature,
                EngineCoolantTemperature = toCreate.EngineCoolantTemperature,
                EngineRpm = toCreate.EngineRpm,
                FuelLevel = toCreate.FuelLevel,
                FuelType = toCreate.FuelType,
                Latitude = toCreate.Latitude,
                Longitude = toCreate.Longitude,
                MassAirFlow = toCreate.MassAirFlow,
                OilTemperature = toCreate.OilTemperature,
                ReadDate = toCreate.ReadDate,
                Speed = toCreate.Speed,
                ThrottlePosition = toCreate.ThrottlePosition
            };

            _db.Readings.Add(reading);
            if (save)
            {
                _db.SaveChanges();
            }
            return reading;
        }

        public IEnumerable<BulkUploadResultViewModel> BulkUpload(long tripId, 
            IEnumerable<BulkUploadViewModel<ReadingViewModel>> readings)
        {
            var results = new List<BulkUploadResultViewModel>();
            foreach (var reading in readings)
            {
                var result = new BulkUploadResultViewModel()
                {
                    Uuid = reading.Uuid
                };
                try
                {
                    var res = Create(reading.Data);
                    result.Id = res.ReadingId;
                    result.Successful = true;
                }
                catch (Exception e)
                {
                    result.Successful = false;
                    result.ErrorMessage = e.Message;
                }
                results.Add(result);
            }

            return results;
        }
    }
}
