using System;
using System.Collections.Generic;
using System.Linq;
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

        private static readonly Dictionary<TripStatus, string> StatusNames = new Dictionary<TripStatus, string>()
        {
            {TripStatus.New, "New" },
            {TripStatus.Started, "Started" },
            {TripStatus.Finished, "Finished" },
            {TripStatus.Processed, "Processed" },
            {TripStatus.Failed, "Failed" }
        };

        private static readonly Dictionary<string, TripStatus> NamesToStatus = StatusNames.ToDictionary(
            x => x.Value.ToUpper(), x => x.Key);

        public static string ToString(this TripStatus tripStatus)
        {
            return StatusNames[tripStatus];
        }

        public static TripStatus FromString(string status)
        {
            status = status.ToUpper();
            if (NamesToStatus.ContainsKey(status))
            {
                return NamesToStatus[status];
            }
            throw new ArgumentException($"{status} is not a valid value for Trip Status");
        }
    }
}
