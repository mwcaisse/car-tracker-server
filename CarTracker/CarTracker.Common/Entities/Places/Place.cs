using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Entities.Places
{
    public class Place : ITrackedEntity
    {
        public long PlaceId { get; set; }

        public string Name { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<GooglePlace> GooglePlaces { get; set; }
        public ICollection<UserPlace> UserPlaces { get; set; }

        public ICollection<Trip> TripStarts { get; set; }
        public ICollection<Trip> TripDestinations { get; set; }

        public ICollection<TripPossiblePlace> TripPossiblePlaces { get; set; }

    }
}
