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

        public CarSupportedCommands CreateOrUpdate(int carId, CarSupportedCommands toUpdate)
        {
            throw new NotImplementedException();
        }

        public CarSupportedCommands CreateOrUpdate(string vin, CarSupportedCommands toUpdate)
        {
            throw new NotImplementedException();
        }

        protected CarSupportedCommands CreateOrUpdate(Car car, CarSupportedCommands toUpdate)
        {
            if (null == car)
            {
                throw new EntityValidationException("Car not found!");
            }

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
