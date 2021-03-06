﻿using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Enums;

namespace CarTracker.Common.ViewModels
{
    public class TripViewModel
    {
        public long Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Name { get; set; }

        public long CarId { get; set; }

        public double? AverageSpeed { get; set; }

        public double? MaximumSpeed { get; set; }

        public double? AverageEngineRpm { get; set; }

        public double? MaxEngineRpm { get; set; }

        public double? DistanceTraveled { get; set; }

        public long? IdleTime { get; set; }

        public long? StartPlaceId { get; set; }

        public long? DestinationPlaceId { get; set; }

        public TripStatus Status { get; set; }

        public PlaceViewModel StartingPlace { get; set; }
        
        public PlaceViewModel DestinationPlace { get; set; }
    }
}
