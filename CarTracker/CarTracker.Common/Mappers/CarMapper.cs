using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.ViewModels;

namespace CarTracker.Common.Mappers
{
    public static class CarMapper
    {

        public static CarViewModel ToViewModel(this Car car)
        {
            var vm = new CarViewModel()
            {
                Id = car.CarId,
                Vin = car.Vin,
                Name = car.Name,
                OwnerId = car.OwnerId,
                Mileage = car.Mileage,
                MileageLastUserSet = car.MileageLastUserSet
            };

            return vm;
        }

        public static IEnumerable<CarViewModel> ToViewModel(this IEnumerable<Car> cars)
        {
            return cars.Select(c => c.ToViewModel());
        }

    }
}
