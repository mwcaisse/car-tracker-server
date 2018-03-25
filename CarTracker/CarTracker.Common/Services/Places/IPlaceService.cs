using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities.Places;

namespace CarTracker.Common.Services.Places
{
    public interface IPlaceService
    {
        IEnumerable<Place> GetPlacesNearby(double latitude, double longitude, int range, long userId);
    }
}
