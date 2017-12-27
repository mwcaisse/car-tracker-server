using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Models.PlaceRequester
{
    public interface ILocation
    {
        double Latitude { get; }
        double Longitude { get; }
    }
}
