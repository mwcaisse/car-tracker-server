using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Enums;
using CarTracker.Common.Exceptions;
using CarTracker.Common.Services;
using CarTracker.Data;

namespace CarTracker.Logic.Services
{
    public class TripProcessor : ITripProcessor
    {

        private readonly CarTrackerDbContext _db;

        public TripProcessor(CarTrackerDbContext db)
        {
            this._db = db;
        }

        public void ProcessUnprocessedTrips()
        {
            var cutoffDate = DateTime.Now.AddMinutes(-30);

            var trips = _db.Trips.Where(t => t.Status != TripStatus.Processed.ToString().ToUpper())
                .Where(t => t.Status == TripStatus.Finished.ToString().ToUpper() ||
                            t.Readings.Max(r => r.ReadDate) < cutoffDate);

            foreach (var trip in trips)
            {
                ProcessTrip(trip);
            }
        }

        public Trip ProcessTrip(Trip trip)
        {
            if (string.Equals(trip.Status, TripStatus.Processed.ToString()
                , StringComparison.OrdinalIgnoreCase))
            {
                throw new EntityValidationException("Trip has already been processed.");
            }

            var readings = _db.Readings.Where(x => x.TripId == trip.TripId)
                .OrderBy(x => x.ReadDate)                       
                .ToList();

            if (readings.Any())
            {
                double maxSpeed = 0;
                double totalSpeed = 0;
                double totalDistance = 0;
                double maxEngineRpm = 0;
                double totalEngineRpm = 0;

                TimeSpan idleTime = new TimeSpan(); //ticks

                Reading previousReading = null;

                for (int i = 0; i < readings.Count; i++)
                {
                    var reading = readings[i];
                    if (reading.Speed.HasValue && reading.Speed.Value > maxSpeed)
                    {
                        maxSpeed = reading.Speed.Value;
                    }

                    if (reading.EngineRpm.HasValue && reading.EngineRpm.Value > maxEngineRpm)
                    {
                        maxEngineRpm = reading.EngineRpm.Value;
                    }

                    totalSpeed += reading.Speed ?? 0;
                    totalEngineRpm += reading.EngineRpm ?? 0;

                    if (null != previousReading)
                    {
                        totalDistance += CalculateDistanceBetweenReadings(previousReading, reading);

                        if ((previousReading.Speed ?? 0) == 0 && (reading.Speed ?? 0) == 0)
                        {
                            idleTime = idleTime.Add(reading.ReadDate - previousReading.ReadDate);
                        }
                    }

                    previousReading = reading;
                }

                trip.AverageSpeed = totalSpeed / (readings.Count * 1.0);
                trip.AverageEngineRpm = totalEngineRpm / (readings.Count * 1.0);

                trip.MaxEngineRpm = maxEngineRpm;
                trip.MaximumSpeed = maxSpeed;
                trip.DistanceTraveled = totalDistance;
                trip.IdleTime = Convert.ToInt64(idleTime.TotalMilliseconds);
                trip.Status = TripStatus.Processed.ToString().ToUpper();

                //if the end date of the trip is null, set its end date to the read date of the
                //  last reading
                if (!trip.EndDate.HasValue)
                {
                    trip.EndDate = readings.Last().ReadDate;
                }

                var car = _db.Cars.FirstOrDefault(c => c.CarId == trip.CarId);
                if (null != car &&
                    (!car.MileageLastUserSet.HasValue || car.MileageLastUserSet < trip.EndDate))
                {
                    car.Mileage += trip.DistanceTraveled;
                }

                _db.SaveChanges();
            }

            return trip;
        }

        protected double CalculateDistanceBetweenReadings(Reading prev, Reading current)
        {
            if (ToRadians(current.Latitude) == 0 || ToRadians(prev.Latitude) == 0)
            {
                return 0; // if either of the latitudes are 0 then return 0. 0 is an incorrect reading
            }

            return CalculateDistanceBetweenLocations(
                new LocationModel()
                {
                    Latitude = prev.Latitude,
                    Longitude = prev.Longitude
                },
                new LocationModel()
                {
                    Latitude  = current.Latitude,
                    Longitude = current.Longitude
                });
        }

        private const double EarthRadidus = 6371.0;

        protected double CalculateDistanceBetweenLocations(LocationModel start, LocationModel end)
        {

            double startLat = ToRadians(start.Latitude);
            double endLat = ToRadians(end.Latitude);
            double deltaLat = ToRadians(end.Latitude - start.Latitude);
            double deltaLong = ToRadians(end.Longitude - start.Longitude);

            double a = Math.Pow(Math.Sin(deltaLat / 2.0), 2.0) +
                       Math.Pow(Math.Sin(deltaLong / 2.0), 2.0) *
                       Math.Cos(startLat) * Math.Cos(endLat);

            double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));
            double distance = EarthRadidus * c;

            return Math.Abs(distance);
        }

        protected double ToRadians(decimal degrees)
        {
            return (Math.PI / 180.0) * Convert.ToDouble(degrees);
        }

        protected class LocationModel
        {
            public decimal Latitude { get; set; }
            public decimal Longitude { get; set; }
        }

        
    }
}
