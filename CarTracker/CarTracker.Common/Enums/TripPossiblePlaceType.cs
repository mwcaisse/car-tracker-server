using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Enums
{
    public enum TripPossiblePlaceType
    {
        Start = 1,
        Destination = 2
    }

    public static class TripPossiblePlaceTypeExtentions
    {
        public static string ToString(this TripPossiblePlaceType type)
        {
            var str = "";

            switch (type)
            {
                case TripPossiblePlaceType.Start:
                    str = "Start";
                    break;
                case TripPossiblePlaceType.Destination:
                    str = "Destination";
                    break;
            }

            return str;
        }

        public static string ToDatabaseValue(this TripPossiblePlaceType type)
        {
            return type.ToString().ToUpper();
        }
    }
}
