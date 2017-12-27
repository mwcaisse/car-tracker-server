using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Models.PlaceRequester;

namespace CarTracker.Common.Services
{
    public interface IPlaceService
    {

        /// <summary>
        /// Fetches the place with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Place Get(long id);

        /// <summary>
        /// Fetches the place with the given google id
        /// 
        /// //TODO: Make this more generic. ForeignId?
        /// </summary>
        /// <param name="googleId"></param>
        /// <returns></returns>
        Place GetByGoogleId(string googleId);

        /// <summary>
        /// Fetches the place corresponding to the given PlaceModel. If one does not exists
        ///     creates it.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Place CreateOrGetPlace(IPlaceModel model);

    }
}
