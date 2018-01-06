using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarTracker.Common.Enums
{
    public enum LogType
    {

        Debug = 1,
        Info = 2,
        Warn = 3,
        Error = 4
    }

    public static class LogTypeExtensions
    {

        private static readonly Dictionary<LogType, string> LogTypeNames = new Dictionary<LogType, string>()
        {
            {LogType.Debug, "Debug" },
            {LogType.Info, "Info" },
            {LogType.Warn, "Warn" },
            {LogType.Error, "Error" }
        };

        private static readonly Dictionary<string, LogType> NamesToLogType = LogTypeNames.ToDictionary(
            x => x.Value.ToUpper(), x => x.Key);

        public static string ToString(this LogType logType)
        {
            return LogTypeNames[logType];
        }

        public static LogType FromString(string logType)
        {
            logType = logType.ToUpper();
            if (NamesToLogType.ContainsKey(logType))
            {
                return NamesToLogType[logType];
            }
            throw new ArgumentException($"{logType} is not a valid value for Log Type");
        }
    }
}
