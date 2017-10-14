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

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
