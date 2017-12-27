﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.GooglePlaceRequester.Models
{
    public class PlaceSearchModel
    {

        public PlaceGeometry Geometry { get; set; }
        public string Icon { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string PlaceId { get; set; }
        public string Scope { get; set; }
        public string Reference { get; set; }
        public IEnumerable<string> Types { get; set; }
        public string Vicinity { get; set; }

        public PlaceSearchModel()
        {
            Types = new List<string>();
        }

    }
}
