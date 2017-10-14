using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Entities
{
    public class Place : ITrackedEntity
    {

        public long PlaceId { get; set; }

        public string Name { get; set; }

        public string GooglePlaceId { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public bool Active { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }


        public ICollection<Trip> TripStarts { get; set; }
        public ICollection<Trip> TripDestinations { get; set; }

        public ICollection<TripPossiblePlace> TripPossiblePlaces { get; set; }

    }
}
