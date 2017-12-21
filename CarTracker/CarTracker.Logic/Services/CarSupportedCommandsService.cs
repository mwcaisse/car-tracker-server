using CarTracker.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarTracker.Common.Entities;
using CarTracker.Data;
using CarTracker.Common.Exceptions;

namespace CarTracker.Logic.Services
{
    public class CarSupportedCommandsService : ICarSupportedCommandsService
    { 
        private readonly CarTrackerDbContext _db;

        public CarSupportedCommandsService(CarTrackerDbContext db)
        {
            this._db = db;
        }

        public CarSupportedCommands Get(long id)
        {
            return _db.CarSupportedCommands.FirstOrDefault(c => c.CarSupportedCommandsId == id);
        }

        public CarSupportedCommands GetForCar(long carId)
        {
            return _db.CarSupportedCommands.FirstOrDefault(c => c.CarId == carId);
        }

        public CarSupportedCommands GetForCar(string vin)
        {
            return _db.CarSupportedCommands.FirstOrDefault(c => c.Car.Vin == vin);
        }

        public CarSupportedCommands CreateOrUpdate(long carId, CarSupportedCommands toUpdate)
        {
            var car = _db.Cars.FirstOrDefault(c => c.CarId == carId);
            if (null == car)
            {
                throw new EntityValidationException(string.Format("No car with the ID: {0} exists.", carId));
            }
            return CreateOrUpdate(car, toUpdate);
        }

        public CarSupportedCommands CreateOrUpdate(string vin, CarSupportedCommands toUpdate)
        {
            var car = _db.Cars.FirstOrDefault(c => c.Vin == vin);
            if (null == car)
            {
                throw new EntityValidationException(string.Format("No car with the VIN: {0} exists.", vin));
            }
            return CreateOrUpdate(car, toUpdate);
        }

        protected CarSupportedCommands CreateOrUpdate(Car car, CarSupportedCommands toUpdate)
        {
            var supportedCommands = _db.CarSupportedCommands.FirstOrDefault(s => s.CarId == car.CarId);

            if (null == supportedCommands)
            {
                supportedCommands = new CarSupportedCommands()
                {
                    Car = car
                };
            }

            supportedCommands.Pids0120Bitmask = toUpdate.Pids0120Bitmask;
            supportedCommands.Pids2140Bitmask = toUpdate.Pids2140Bitmask;
            supportedCommands.Pids4160Bitmask = toUpdate.Pids4160Bitmask;
            supportedCommands.Pids6180Bitmask = toUpdate.Pids6180Bitmask;
            supportedCommands.Pids81A0Bitmask = toUpdate.Pids81A0Bitmask;

            _db.SaveChanges();

            return toUpdate;
        }

        protected Car GetCar(long id)
        {
            return _db.Cars.FirstOrDefault(c => c.CarId == id);
        }

        protected Car GetCar(string vin)
        {
            return _db.Cars.FirstOrDefault(c => c.Vin == vin);
        }
    }
}
