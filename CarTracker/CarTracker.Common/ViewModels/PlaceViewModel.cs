using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.ViewModels
{
    public class PlaceViewModel
    {

        public long Id { get; set; }

        public string Name { get; set; }

        public string GooglePlaceId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
