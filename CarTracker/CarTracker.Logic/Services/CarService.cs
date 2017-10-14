using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels;
using CarTracker.Data;

namespace CarTracker.Logic.Services
{
    public class CarService : ICarService
    {

        private readonly CarTrackerDbContext _db;

        public CarService(CarTrackerDbContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Fetches all cars
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Car> GetAll()
        {
            return _db.Cars.ToList();
        }

        /// <summary>
        /// Fetches the car with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Car Get(long id)
        {
            return _db.Cars.FirstOrDefault(c => c.CarId == id);
        }

        /// <summary>
        /// Fetches the can with the given vin
        /// </summary>
        /// <param name="vin"></param>
        /// <returns></returns>
        public Car Get(string vin)
        {
            return _db.Cars.FirstOrDefault(c => c.Vin == vin);
        }

        public Car Create(CarViewModel toCreate)
        {
            var car = new Car()
            {
                Vin = toCreate.Vin,
                Name = toCreate.Name,
                Mileage = toCreate.Mileage,
                MileageLastUserSet = DateTime.Now
            };

            _db.Cars.Add(car);
            _db.SaveChanges();

            return car;
        }

        public Car Update(CarViewModel toUpdate)
        {
            var car = Get(toUpdate.Id);

            if (null == car)
            {
                throw new Exception("Can't update car! Does not exist!");
            }

            car.Name = toUpdate.Name;

            if (car.Mileage != toUpdate.Mileage)
            {
                car.Mileage = toUpdate.Mileage;
                car.MileageLastUserSet = DateTime.Now;
            }

            return car;
        }
    }
}
