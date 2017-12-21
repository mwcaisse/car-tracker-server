using CarTracker.Common.Entities;
using CarTracker.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Enums;

namespace CarTracker.Common.Mappers
{
    public static class CarSupportedCommandsMapper
    {
        public static IEnumerable<CarSupportedCommandViewModel> ToViewModel(this CarSupportedCommands supportedCommands)
        {
            var vm = new List<CarSupportedCommandViewModel>();

            vm.AddRange(GetSupportedCommands(Pids0120, Convert.ToUInt32(supportedCommands.Pids0120Bitmask)));
            vm.AddRange(GetSupportedCommands(Pids2140, Convert.ToUInt32(supportedCommands.Pids2140Bitmask)));
            vm.AddRange(GetSupportedCommands(Pids4160, Convert.ToUInt32(supportedCommands.Pids4160Bitmask)));
            vm.AddRange(GetSupportedCommands(Pids6180, Convert.ToUInt32(supportedCommands.Pids6180Bitmask)));
            vm.AddRange(GetSupportedCommands(Pids81A0, Convert.ToUInt32(supportedCommands.Pids81A0Bitmask)));

            return vm;
        }

        private static IEnumerable<ObdPid> Pids0120 = new List<ObdPid>()
        {
            
        };

        private static IEnumerable<ObdPid> Pids2140 = new List<ObdPid>()
        {

        };

        private static IEnumerable<ObdPid> Pids4160 = new List<ObdPid>()
        {

        };

        private static IEnumerable<ObdPid> Pids6180 = new List<ObdPid>()
        {

        };

        private static IEnumerable<ObdPid> Pids81A0 = new List<ObdPid>()
        {

        };

        private static IEnumerable<CarSupportedCommandViewModel> GetSupportedCommands(IEnumerable<ObdPid> pids, uint bitmask)
        {

            return pids.Select(p => new CarSupportedCommandViewModel()
            {
                Name = p.GetCommand().GetName(), 
                Pid = p.GetName(), 
                Supportd = (bitmask & p.GetBitmask()) > 0
            });
        }
    }
}
