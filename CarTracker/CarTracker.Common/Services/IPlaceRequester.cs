using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Models.PlaceRequester;

namespace CarTracker.Common.Services
{
    public interface IPlaceRequester
    {
        IEnumerable<IPlaceModel> GetPlacesNearby(double latitude, double longitude, int range);
    }
}
