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
    public class CarMaintenanceService : ICarMaintenanceService
    {

        private readonly CarTrackerDbContext _db;

        public CarMaintenanceService(CarTrackerDbContext db)
        {
            this._db = db;
        }

        public IEnumerable<CarMaintenance> GetForCar(long carId)
        {
            return _db.CarMaintenances.Active().Where(cm => cm.CarId == carId);
        }

        public PagedViewModel<CarMaintenance> GetForCarPaged(long carId, int skip = 0, int take = 10, SortParam sort = null)
        {
            return _db.CarMaintenances.Active().Where(cm => cm.CarId == carId).PageAndSort(skip, take, sort);
        }

        public CarMaintenance Get(long id)
        {
            return _db.CarMaintenances.Active().FirstOrDefault(cm => cm.CarMaintenanceId == id);
        }

        public CarMaintenance Create(CarMaintenanceViewModel toCreate)
        {
            Validate(toCreate);

            // If the mileage is less than 1, pull the mileage from the car
            if (toCreate.Mileage < 1)
            {
                toCreate.Mileage = _db.Cars.First(c => c.CarId == toCreate.CarId).Mileage ?? 0;
            }

            var maint = new CarMaintenance()
            {
                CarId = toCreate.CarId,
                Date = toCreate.Date,
                Mileage = toCreate.Mileage,
                Type = toCreate.Type,
                Notes = toCreate.Notes,
                Active = true
            };

            _db.CarMaintenances.Add(maint);
            _db.SaveChanges();

            return maint;
        }

        public CarMaintenance Update(CarMaintenanceViewModel toUpdate)
        {
            var maint = Get(toUpdate.CarMaintenanceId);
            if (null == maint)
            {
                throw new EntityValidationException("No Car Maintenance with the given id exists.");
            }

            Validate(toUpdate);

            maint.CarId = toUpdate.CarId;
            maint.Date = toUpdate.Date;
            maint.Mileage = toUpdate.Mileage;
            maint.Notes = toUpdate.Notes;
            maint.Type = toUpdate.Type;

            _db.SaveChanges();

            return maint;
        }

        public bool Delete(long id)
        {
            var maint = Get(id);

            if (null != maint)
            {
                maint.Active = false;
                _db.SaveChanges();
                return true;
            }

            return false;
        }

        private void Validate(CarMaintenanceViewModel vm)
        {
            if (!_db.Cars.Any(c => c.CarId == vm.CarId))
            {
                throw new EntityValidationException("No car with the given id exists.");
            }

            if (vm.Mileage < 0)
            {
                throw new EntityValidationException("Mileage cannot be less than 0.");
            }
        }
    }
}
