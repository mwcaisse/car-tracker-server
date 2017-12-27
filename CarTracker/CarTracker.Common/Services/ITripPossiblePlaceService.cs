using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Enums;
using CarTracker.Common.ViewModels;

namespace CarTracker.Common.Services
{
    public interface ITripPossiblePlaceService
    {
        /// <summary>
        /// Fetches all possible places for the given trip of the given type (start/dest)
        /// </summary>
        /// <param name="tripId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IEnumerable<TripPossiblePlace> GetForTripOfType(long tripId, TripPossiblePlaceType type);

        /// <summary>
        /// Fetches all possible places for the given trip of the given type (start/dest) with
        ///     paging
        /// </summary>
        /// <param name="tripId"></param>
        /// <param name="type"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="sortParam"></param>
        /// <returns></returns>
        PagedViewModel<TripPossiblePlace> GetForTripOfTypePaged(long tripId, TripPossiblePlaceType type,
            int skip = 0, int take = 10, SortParam sortParam = null);

        /// <summary>
        /// Fetches the possible place for a trip by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TripPossiblePlace Get(long id);
    }
}
