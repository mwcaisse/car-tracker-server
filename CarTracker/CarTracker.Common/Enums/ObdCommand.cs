using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Enums
{
    public enum ObdCommand
    {
        MonitorStatus = 1,
        FreezeDtc = 2,
        FuelSystemStatus = 3,
        CalculatedEngineLoad = 4
    }

    public static class ObdCommandExtensions
    {
        public static string ToString(this ObdCommand command)
        {
            switch (command)
            {
                case ObdCommand.MonitorStatus:
                    return "Monitor status since DTCs cleared";
                default:
                    return "";
            }
        }
    }
}
