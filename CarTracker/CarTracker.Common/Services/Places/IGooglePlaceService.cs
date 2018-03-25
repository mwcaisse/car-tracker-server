using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Entities.Places;
using CarTracker.Common.Models.PlaceRequester;

namespace CarTracker.Common.Services.Places
{
    public interface IGooglePlaceService
    {

        /// <summary>
        /// Fetches the place with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GooglePlace Get(long id);

        /// <summary>
        /// Fetches the place with the given google id 

        /// </summary>
        /// <param name="googleId"></param>
        /// <returns></returns>
        GooglePlace GetByGoogleId(string googleId);

        /// <summary>
        /// Fetches the place corresponding to the given PlaceModel. If one does not exists
        ///     creates it.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        GooglePlace CreateOrGetPlace(IPlaceModel model);

    }
}
