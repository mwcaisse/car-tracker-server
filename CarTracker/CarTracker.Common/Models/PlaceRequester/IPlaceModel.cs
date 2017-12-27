using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CarTracker.Common.Models.PlaceRequester
{
    public interface IPlaceModel
    {

        string Name { get; }
        string Id { get; }
        ILocation Location { get; }
    }
}
