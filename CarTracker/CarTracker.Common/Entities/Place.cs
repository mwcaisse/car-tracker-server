using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Entities
{
    public class Place
    {

        public long PlaceId { get; set; }

        public string Name { get; set; }

        public string GooglePlaceId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public bool Active { get; set; }
    }
}
