using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Models.PlaceRequester;

namespace CarTracker.GooglePlaceRequester.Models
{
    public class Location :ILocation
    {
        public double Lat { get; set; }
        public double Lng { get; set; }


        public double Latitude => Lat;
        public double Longitude => Lng;
    }
}
