using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.ViewModels;

namespace CarTracker.Common.Mappers
{
    public static class CarMaintenanceMapper
    {

        public static CarMaintenanceViewModel ToViewModel(this CarMaintenance carMaint)
        {
            var vm = new CarMaintenanceViewModel()
            {
                Type = carMaint.Type,
                Date = carMaint.Date,
                CarId = carMaint.CarId,
                Mileage = carMaint.Mileage,
                CarMaintenanceId = carMaint.CarMaintenanceId,
                Notes = carMaint.Notes
            };
            return vm;
        }

        public static IEnumerable<CarMaintenanceViewModel> ToViewModel(this IEnumerable<CarMaintenance> carMaints)
        {
            return carMaints.Select(cm => cm.ToViewModel());
        }

        public static PagedViewModel<CarMaintenanceViewModel> ToViewModel(this PagedViewModel<CarMaintenance> paged)
        {
            return new PagedViewModel<CarMaintenanceViewModel>()
            {
                Data = paged.Data.ToViewModel(),
                Total = paged.Total,
                Take = paged.Take,
                Skip = paged.Skip
            };
        }

    }
}
