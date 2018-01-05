using System;
using System.Collections.Generic;
using System.Linq;
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

        private static readonly Dictionary<TripPossiblePlaceType, string> PlaceTypeNames = new Dictionary
            <TripPossiblePlaceType, string>()
            {
                {TripPossiblePlaceType.Start, "Start"},
                {TripPossiblePlaceType.Destination, "Destination"}
            };

        private static readonly Dictionary<string, TripPossiblePlaceType> NamesToPlaceType = 
            PlaceTypeNames.ToDictionary(x => x.Value.ToUpper(), x => x.Key);

        public static string ToString(this TripPossiblePlaceType type)
        {
            return PlaceTypeNames[type];
        }

        public static TripPossiblePlaceType FromString(string type)
        {
            type = type.ToUpper();
            if (NamesToPlaceType.ContainsKey(type))
            {
                return NamesToPlaceType[type];
            }

            throw new ArgumentException("Given type is not a valid value for Trip Possible Place type.");
        }
    }
}
