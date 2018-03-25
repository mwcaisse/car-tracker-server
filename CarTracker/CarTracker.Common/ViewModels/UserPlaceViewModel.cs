using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.ViewModels
{
    public class UserPlaceViewModel
    {
        public long UserPlaceId { get; set; }

        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

    }
}
