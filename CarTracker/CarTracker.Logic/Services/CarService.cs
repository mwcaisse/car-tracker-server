using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Exceptions;
using CarTracker.Common.Services;
using CarTracker.Common.ViewModels;
using CarTracker.Data;
using CarTracker.Data.Extensions;

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

        public PagedViewModel<Car> GetAllPaged(int skip = 0, int take = 10, 
            SortParam sortParam = null)
        {
            var query = _db.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(sortParam?.ColumnName))
            {
                if (sortParam.Ascending)
                {
                    query = query.OrderBy(sortParam.ColumnName);
                }
                else
                {
                    query = query.OrderByDescending(sortParam.ColumnName);
                }
            }
            else
            {
                query = query.OrderBy(x => x.Vin);
            }

            int count = query.Count();

            if (take <= 0 || take > 100)
            {
                throw new EntityValidationException("Invalid page size. Take must be between 1 and 100.");
            }
            if (skip < 0)
            {
                throw new EntityValidationException("Invalid skip. Skip must be >= 0.");
            }

            return new PagedViewModel<Car>()
            {
                Data = query.Skip(skip).Take(take),
                Count = count,
                Skip = skip,
                Take = take
            };
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
