using CarTracker.Common.Entities;
using CarTracker.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using CarTracker.Common.Enums;

namespace CarTracker.Common.Mappers
{
    public static class CarSupportedCommandsMapper
    {
        public static IEnumerable<CarSupportedCommandViewModel> ToViewModel(this CarSupportedCommands supportedCommands)
        {
            var vm = new List<CarSupportedCommandViewModel>();
            if (null == supportedCommands)
            {
                return vm;
            }

            vm.AddRange(GetSupportedCommands(Pids0120, supportedCommands.Pids0120Bitmask ?? 0));
            vm.AddRange(GetSupportedCommands(Pids2140, supportedCommands.Pids2140Bitmask ?? 0));
            vm.AddRange(GetSupportedCommands(Pids4160, supportedCommands.Pids4160Bitmask ?? 0));
            vm.AddRange(GetSupportedCommands(Pids6180, supportedCommands.Pids6180Bitmask ?? 0));
            vm.AddRange(GetSupportedCommands(Pids81A0, supportedCommands.Pids81A0Bitmask ?? 0));

            return vm;
        }

        private static IEnumerable<ObdPid> Pids0120 = new List<ObdPid>()
        {
            ObdPid.Pid01,
            ObdPid.Pid02,
            ObdPid.Pid03,
            ObdPid.Pid04,
            ObdPid.Pid05,
            ObdPid.Pid06,
            ObdPid.Pid07,
            ObdPid.Pid08,
            ObdPid.Pid09,
            ObdPid.Pid0A,
            ObdPid.Pid0B,
            ObdPid.Pid0C,
            ObdPid.Pid0D,
            ObdPid.Pid0E,
            ObdPid.Pid0F,
            ObdPid.Pid10,
            ObdPid.Pid11,
            ObdPid.Pid12,
            ObdPid.Pid13,
            ObdPid.Pid14,
            ObdPid.Pid15,
            ObdPid.Pid16,
            ObdPid.Pid17,
            ObdPid.Pid18,
            ObdPid.Pid19,
            ObdPid.Pid1A,
            ObdPid.Pid1B,
            ObdPid.Pid1C,
            ObdPid.Pid1D,
            ObdPid.Pid1E,
            ObdPid.Pid1F,
        };

        private static IEnumerable<ObdPid> Pids2140 = new List<ObdPid>()
        {
            ObdPid.Pid21,
            ObdPid.Pid22,
            ObdPid.Pid23,
            //ObdPid.Pid24,
            //ObdPid.Pid25,
            //ObdPid.Pid26,
            //ObdPid.Pid27,
            //ObdPid.Pid28,
            //ObdPid.Pid29,
            //ObdPid.Pid2A,
            //ObdPid.Pid2B,
            ObdPid.Pid2C,
            ObdPid.Pid2D,
            ObdPid.Pid2E,
            ObdPid.Pid2F,
            ObdPid.Pid30,
            ObdPid.Pid31,
            ObdPid.Pid32,
            ObdPid.Pid33,
            //ObdPid.Pid34,
            //ObdPid.Pid35,
            //ObdPid.Pid36,
            //ObdPid.Pid37,
            //ObdPid.Pid38,
            //ObdPid.Pid39,
            //ObdPid.Pid3A,
            //ObdPid.Pid3B,
            ObdPid.Pid3C,
            ObdPid.Pid3D,
            ObdPid.Pid3E,
            ObdPid.Pid3F,
        };

        private static IEnumerable<ObdPid> Pids4160 = new List<ObdPid>()
        {
            ObdPid.Pid41,
            ObdPid.Pid42,
            ObdPid.Pid43,
            ObdPid.Pid44,
            ObdPid.Pid45,
            ObdPid.Pid46,
            ObdPid.Pid47,
            ObdPid.Pid48,
            ObdPid.Pid49,
            ObdPid.Pid4A,
            ObdPid.Pid4B,
            ObdPid.Pid4C,
            ObdPid.Pid4D,
            ObdPid.Pid4E,
            ObdPid.Pid4F,
            ObdPid.Pid50,
            ObdPid.Pid51,
            ObdPid.Pid52,
            ObdPid.Pid53,
            //ObdPid.Pid54,
            //ObdPid.Pid55,
            //ObdPid.Pid56,
            //ObdPid.Pid57,
            //ObdPid.Pid58,
            ObdPid.Pid59,
            ObdPid.Pid5A,
            ObdPid.Pid5B,
            ObdPid.Pid5C,
            ObdPid.Pid5D,
            ObdPid.Pid5E,
            ObdPid.Pid5F,
        };

        private static IEnumerable<ObdPid> Pids6180 = new List<ObdPid>()
        {
            ObdPid.Pid61,
            ObdPid.Pid62,
            ObdPid.Pid63,
            ObdPid.Pid64,
            //ObdPid.Pid65,
            //ObdPid.Pid66,
            //ObdPid.Pid67,
            //ObdPid.Pid68,
            //ObdPid.Pid69,
            //ObdPid.Pid6A,
            ObdPid.Pid6B,
            //ObdPid.Pid6C,
            //ObdPid.Pid6D,
            //ObdPid.Pid6E,
            ObdPid.Pid6F,
            ObdPid.Pid70,
            //ObdPid.Pid71,
            ObdPid.Pid72,
            ObdPid.Pid73,
            ObdPid.Pid74,
            ObdPid.Pid75,
            ObdPid.Pid76,
            ObdPid.Pid77,
            ObdPid.Pid78,
            ObdPid.Pid79,
            //ObdPid.Pid7A,
            //ObdPid.Pid7B,
            //ObdPid.Pid7C,
            //ObdPid.Pid7D,
            //ObdPid.Pid7E,
            ObdPid.Pid7F,
        };

        private static IEnumerable<ObdPid> Pids81A0 = new List<ObdPid>()
        {
            // No 81 to A0 PIDS yet
        };

        private static IEnumerable<CarSupportedCommandViewModel> GetSupportedCommands(IEnumerable<ObdPid> pids, int bitmask)
        {

            return pids.Select(p => new CarSupportedCommandViewModel()
            {
                Name = p.GetCommand().GetName(), 
                Pid = p.GetName(), 
                Supported = (bitmask & p.GetBitmask()) != 0
            });
        }
    }
}
