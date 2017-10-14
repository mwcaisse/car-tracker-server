using System;
using System.Collections.Generic;
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
        public static string ToString(this LogType logType)
        {
            var str = "";

            switch (logType)
            {
                case LogType.Debug:
                    str = "Debug";
                    break;
                case LogType.Info:
                    str = "Info";
                    break;
                case LogType.Warn:
                    str = "Warn";
                    break;
                case LogType.Error:
                    str = "Error";
                    break;
            }

            return str;
        }
    }
}
