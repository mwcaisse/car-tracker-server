using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CarTracker.Common.Entities;

namespace CarTracker.Common.Services
{
    public interface ITripProcessor
    {

        void ProcessUnprocessedTrips();

        Trip ProcessTrip(Trip trip);

    }
}
