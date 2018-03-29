using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.ViewModels;

namespace CarTracker.Common.Services
{
    public interface ICarMaintenanceService
    {

        IEnumerable<CarMaintenance> GetForCar(long carId);

        PagedViewModel<CarMaintenance> GetForCarPaged(long carId, int skip = 0, int take = 10, SortParam sort = null);

        CarMaintenance Get(long id);

        CarMaintenance Create(CarMaintenanceViewModel toCreate);

        CarMaintenance Update(CarMaintenanceViewModel toUpdate);

        bool Delete(long id);
        
    }
}
