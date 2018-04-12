using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Exceptions;
using CarTracker.Common.Models;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels;
using CarTracker.Data;
using CarTracker.Data.Extensions;

namespace CarTracker.Logic.Services
{
    public class CarService : ICarService
    {

        private readonly CarTrackerDbContext _db;
        private readonly IRequestInformation _requestInformation;

        public CarService(CarTrackerDbContext db, IRequestInformation requestInformation)
        {
            this._db = db;
            _requestInformation = requestInformation;
        }

        /// <summary>
        /// Fetches all cars
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Car> GetAll()
        {
            return _db.Cars.ToList();
        }

        public PagedViewModel<Car> GetAllPaged(int skip = 0, int take = 10, 
            SortParam sortParam = null)
        {
            if (string.IsNullOrWhiteSpace(sortParam?.ColumnName))
            {
                sortParam = new SortParam()
                {
                    ColumnName = "Vin",
                    Ascending = true
                };
            }
            return _db.Cars.PageAndSort(skip, take, sortParam);
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
            if (!_requestInformation.UserId.HasValue)
            {
                throw new EntityValidationException("User is required to create a car!");
            }

            if (string.IsNullOrWhiteSpace(toCreate.Vin))
            {
                throw new EntityValidationException("VIN cannot be blank!");
            }

            if (null != Get(toCreate.Vin))
            {
                throw new EntityValidationException($"Car with VIN {toCreate.Vin} already exists!");
            }
            
            var car = new Car()
            {
                Vin = toCreate.Vin,
                Name = toCreate.Name,
                Mileage = toCreate.Mileage,
                MileageLastUserSet = DateTime.Now,
                OwnerId = _requestInformation.UserId.Value
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
                throw new EntityValidationException("Can't update car! Does not exist!");
            }

            if (!string.Equals(toUpdate.Vin, car.Vin))
            {
                throw new EntityValidationException("Cannot update a car's vin after creation");
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
