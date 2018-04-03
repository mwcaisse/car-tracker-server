using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities.Auth;
using CarTracker.Common.Enums;

namespace CarTracker.Common.Entities.Places
{
    public class PlaceVisit : ITrackedEntity, IOwnedEntity
    {
        public long PlaceVisitId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public TripPossiblePlaceType PlaceType { get; set; }

        public DateTime? VisitDate { get; set; }

        public bool UserSelected { get; set; }

        public long PlaceId { get; set; }

        public long OwnerId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Place Place { get; set; }

        public User Owner { get; set; }
       
    }
}
