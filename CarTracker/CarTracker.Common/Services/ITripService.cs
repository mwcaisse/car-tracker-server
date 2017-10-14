using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.ViewModels;

namespace CarTracker.Common.Services
{
    public interface ITripService
    {
        /// <summary>
        /// Fetches the trip with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Trip Get(long id);

        /// <summary>
        /// Fetches all of the trips for the given car, and applies the specified paging
        ///     + sorting
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        PagedViewModel<Trip> GetForCar(long carId, int skip, int take, SortParam sort);

        /// <summary>
        /// Creates the given trip for the given car
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="toCreate"></param>
        /// <returns></returns>
        Trip Create(long carId, TripViewModel toCreate);

        /// <summary>
        /// Updates the given trip
        /// </summary>
        /// <param name="toUpdate"></param>
        /// <returns></returns>
        Trip Update(TripViewModel toUpdate);

        /// <summary>
        /// Processes the specified trip
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Trip ProcessTrip(long id);

        /// <summary>
        /// Sets the starting place of the trip
        /// </summary>
        /// <param name="id"></param>
        /// <param name="placeId"></param>
        /// <returns></returns>
        bool SetStartingPlace(long id, long placeId);

        /// <summary>
        /// Sets the destination place of the trip
        /// </summary>
        /// <param name="id"></param>
        /// <param name="placeId"></param>
        /// <returns></returns>
        bool SetDestinationPlace(long id, long placeId);
    }
}
