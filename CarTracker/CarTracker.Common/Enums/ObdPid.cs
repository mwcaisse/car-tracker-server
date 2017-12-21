﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Enums
{
    public enum ObdPid
    {
        //PIDS 01 to 20
        Pid01 = 1,
        Pid02 = 2,
        Pid03 = 3,
        Pid04 = 4,
        Pid05 = 5,
        Pid06 = 6,
        Pid07 = 7,
        Pid08 = 8,
        Pid09 = 9,
        Pid0A = 10,
        Pid0B = 11,
        Pid0C = 12,
        Pid0D = 13,
        Pid0E = 14,
        Pid0F = 15,
        Pid10 = 16,
        Pid11 = 17,
        Pid12 = 18,
        Pid13 = 19,
        Pid14 = 20,
        Pid15 = 21,
        Pid16 = 22,
        Pid17 = 23,
        Pid18 = 24,
        Pid19 = 25,
        Pid1A = 26,
        Pid1B = 27,
        Pid1C = 28,
        Pid1D = 29,
        Pid1E = 30,
        Pid1F = 31,

        // PIDS 21 to 40
        Pid21 = 32,
        Pid22 = 33,
        Pid23 = 34,
        Pid24 = 35,
        Pid25 = 36,
        Pid26 = 37,
        Pid27 = 38,
        Pid28 = 39,
        Pid29 = 40,
        Pid2A = 41,
        Pid2B = 42,
        Pid2C = 43,
        Pid2D = 44,
        Pid2E = 45,
        Pid2F = 46,
        Pid30 = 47,
        Pid31 = 48,
        Pid32 = 49,
        Pid33 = 50,
        Pid34,
        Pid35,
        Pid36,
        Pid37,
        Pid38,
        Pid39,
        Pid3A,
        Pid3B,
        Pid3C,
        Pid3D,
        Pid3E,
        Pid3F,
        
        //PIDS 41 to 60
        Pid41,
        Pid42,
        Pid43,
        Pid44,
        Pid45,
        Pid46,
        Pid47,
        Pid48,
        Pid49,
        Pid4A,
        Pid4B,
        Pid4C,
        Pid4D,
        Pid4E,
        Pid4F,
        Pid50,
        Pid51,
        Pid52,
        Pid53,
        Pid54,
        Pid55,
        Pid56,
        Pid57,
        Pid58,
        Pid59,
        Pid5A,
        Pid5B,
        Pid5C,
        Pid5D,
        Pid5E,
        Pid5F,

        // PIDS 61 to 80
        Pid61,
        Pid62,
        Pid63,
        Pid64,
        Pid65,
        Pid66,
        Pid67,
        Pid68,
        Pid69,
        Pid6A,
        Pid6B,
        Pid6C,
        Pid6D,
        Pid6E,
        Pid6F,
        Pid70,
        Pid71,
        Pid72,
        Pid73,
        Pid74,
        Pid75,
        Pid76,
        Pid77,
        Pid78,
        Pid79,
        Pid7A,
        Pid7B,
        Pid7C,
        Pid7D,
        Pid7E,
        Pid7F

    }

    public static class ObdPidExtensions
    {

        private static readonly Dictionary<ObdPid, Tuple<ObdCommand, uint>> PidMapping =
            new Dictionary<ObdPid, Tuple<ObdCommand, uint>>()
            {
                {ObdPid.Pid01, new Tuple<ObdCommand, uint>(ObdCommand.MonitorStatus, (uint) 1 << 31)},
                {ObdPid.Pid02, new Tuple<ObdCommand, uint>(ObdCommand.FreezeDtc, (uint) 1 << 30)}
            };

        public static uint GetBitmask(this ObdPid pid)
        {
            var map = PidMapping[pid];
            if (null != map)
            {
                return map.Item2;
            }
            return 0;
        }

        public static ObdCommand GetCommand(this ObdPid pid)
        {
            var map = PidMapping[pid];
            if (null != map)
            {
                return map.Item1;
            }
            return 0;
        }

        public static string GetName(this ObdPid pid)
        {
            return string.Empty;
        }
        
    }
}
