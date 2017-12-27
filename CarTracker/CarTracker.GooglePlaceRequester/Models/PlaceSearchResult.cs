using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.GooglePlaceRequester.Models
{
    public class PlaceSearchResult
    {

        public IEnumerable<string> HtmlAttributes { get; set; }
        public IEnumerable<PlaceSearchModel> Results { get; set; }
        public String Status { get; set; }

        public PlaceSearchResult()
        {
            HtmlAttributes = new List<string>();
            Results = new List<PlaceSearchModel>();
        }
    }
}
