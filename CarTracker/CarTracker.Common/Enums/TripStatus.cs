using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Enums
{
    public enum TripStatus
    {

        New = 1,
        Started = 2,
        Finished = 3,
        Processed = 4,
        Failed = 5
    }

    public static class TripStatusExtensions
    {

        public static string ToString(this TripStatus tripStatus)
        {
            var str = "";

            switch (tripStatus)
            {
                case TripStatus.New:
                    str = "New";
                    break;
                case TripStatus.Started:
                    str = "Started";
                    break;
                case TripStatus.Finished:
                    str = "Finished";
                    break;
                case TripStatus.Processed:
                    str = "Processed";
                    break;
                case TripStatus.Failed:
                    str = "Failed";
                    break;
            }

            return str;
        }
    }
}
