using CarTracker.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Services
{
    public interface ICarSupportedCommandsService
    {

        /// <summary>
        /// Fethes the Supported Commands with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CarSupportedCommands Get(long id);

        /// <summary>
        /// Fetches the Supported Commands for the car with the given id
        /// </summary>
        /// <param name="carId">The id of the car</param>
        /// <returns></returns>
        CarSupportedCommands GetForCar(long carId);

        /// <summary>
        /// Fetches the Supported Commands for the car with the given VIN
        /// </summary>
        /// <param name="vin">The VIN of the car</param>
        /// <returns></returns>
        CarSupportedCommands GetForCar(string vin);

        /// <summary>
        /// Creates or Updates the supported commands for the car with the given id
        /// 
        /// If Supported Commands for the car already exist, they will be updated, otherwise they will be
        ///     created
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="toUpdate"></param>
        /// <returns></returns>
        CarSupportedCommands CreateOrUpdate(int carId, CarSupportedCommands toUpdate);

        /// <summary>
        /// Creates or Updates the supported commands for the car with the given VIN
        /// 
        /// If Supported Commands for the car already exist, they will be updated, otherwise they will be
        ///     created
        /// </summary>
        /// <param name="vin"></param>
        /// <param name="toUpdate"></param>
        /// <returns></returns>
        CarSupportedCommands CreateOrUpdate(string vin, CarSupportedCommands toUpdate);
    }
}
