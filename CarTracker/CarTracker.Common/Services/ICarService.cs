using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.ViewModels;

namespace CarTracker.Common.Services
{
    public interface ICarService
    {

        /// <summary>
        /// Fetches all cars
        /// </summary>
        /// <returns></returns>
        IEnumerable<Car> GetAll();

        /// <summary>
        /// Fetches the car with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Car Get(long id);

        /// <summary>
        /// Fetches the can with the given vin
        /// </summary>
        /// <param name="vin"></param>
        /// <returns></returns>
        Car Get(string vin);

        /// <summary>
        /// Creates the given car from the view model
        /// </summary>
        /// <param name="toCreate"></param>
        /// <returns></returns>
        Car Create(CarViewModel toCreate);

        /// <summary>
        /// Updates the given car from the view model
        /// </summary>
        /// <param name="toUpdate"></param>
        /// <returns></returns>
        Car Update(CarViewModel toUpdate);

    }
}
