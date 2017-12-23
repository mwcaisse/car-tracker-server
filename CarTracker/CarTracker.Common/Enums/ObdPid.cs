using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Enums
{
    public enum ObdPid
    {
        //PIDS 01 to 20
        Pid01,
        Pid02,
        Pid03,
        Pid04,
        Pid05,
        Pid06,
        Pid07,
        Pid08,
        Pid09,
        Pid0A,
        Pid0B,
        Pid0C,
        Pid0D,
        Pid0E,
        Pid0F,
        Pid10,
        Pid11,
        Pid12,
        Pid13,
        Pid14,
        Pid15,
        Pid16,
        Pid17,
        Pid18,
        Pid19,
        Pid1A,
        Pid1B,
        Pid1C,
        Pid1D,
        Pid1E,
        Pid1F,

        // PIDS 21 to 40
        Pid21,
        Pid22,
        Pid23,
        Pid24,
        Pid25,
        Pid26,
        Pid27,
        Pid28,
        Pid29,
        Pid2A,
        Pid2B,
        Pid2C,
        Pid2D,
        Pid2E,
        Pid2F,
        Pid30,
        Pid31,
        Pid32,
        Pid33,
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
                // PIDS 01 to 20
                {ObdPid.Pid01, new Tuple<ObdCommand, uint>(ObdCommand.MonitorStatus, (uint) 1 << 31)},
                {ObdPid.Pid02, new Tuple<ObdCommand, uint>(ObdCommand.FreezeDtc, (uint) 1 << 30)},
                {ObdPid.Pid03, new Tuple<ObdCommand, uint>(ObdCommand.FuelSystemStatus, (uint) 1 << 29)},
                {ObdPid.Pid04, new Tuple<ObdCommand, uint>(ObdCommand.CalculatedEngineLoad, (uint) 1 << 28)},
                {ObdPid.Pid05, new Tuple<ObdCommand, uint>(ObdCommand.EngineCoolantTemperature, (uint) 1 << 27)},
                {ObdPid.Pid06, new Tuple<ObdCommand, uint>(ObdCommand.ShortTermFuelTrimBank1, (uint) 1 << 26)},
                {ObdPid.Pid07, new Tuple<ObdCommand, uint>(ObdCommand.LongTermFuelTrimBank1, (uint) 1 << 25)},
                {ObdPid.Pid08, new Tuple<ObdCommand, uint>(ObdCommand.ShortTermFuelTrimBank2, (uint) 1 << 24)},
                {ObdPid.Pid09, new Tuple<ObdCommand, uint>(ObdCommand.LongTermFuelTrimBank2, (uint) 1 << 23)},
                {ObdPid.Pid0A, new Tuple<ObdCommand, uint>(ObdCommand.FuelPressure, (uint) 1 << 22)},
                {ObdPid.Pid0B, new Tuple<ObdCommand, uint>(ObdCommand.IntakeManifoldAbsolutePressure, (uint) 1 << 21)},
                {ObdPid.Pid0C, new Tuple<ObdCommand, uint>(ObdCommand.EngineRpm, (uint) 1 << 20)},
                {ObdPid.Pid0D, new Tuple<ObdCommand, uint>(ObdCommand.VehicleSpeed, (uint) 1 << 19)},
                {ObdPid.Pid0E, new Tuple<ObdCommand, uint>(ObdCommand.TimingAdvance, (uint) 1 << 18)},
                {ObdPid.Pid0F, new Tuple<ObdCommand, uint>(ObdCommand.IntakeAirTemperature, (uint) 1 << 17)},
                {ObdPid.Pid10, new Tuple<ObdCommand, uint>(ObdCommand.MassAirFlow, (uint) 1 << 16)},
                {ObdPid.Pid11, new Tuple<ObdCommand, uint>(ObdCommand.ThrottlePosition, (uint) 1 << 15)},
                {ObdPid.Pid12, new Tuple<ObdCommand, uint>(ObdCommand.CommandedSecondaryAirStatus, (uint) 1 << 14)},
                {ObdPid.Pid13, new Tuple<ObdCommand, uint>(ObdCommand.OxygenSensorsPresentBanks2, (uint) 1 << 13)},
                {ObdPid.Pid14, new Tuple<ObdCommand, uint>(ObdCommand.OxygenSensor1, (uint) 1 << 12)},
                {ObdPid.Pid15, new Tuple<ObdCommand, uint>(ObdCommand.OxygenSensor2, (uint) 1 << 11)},
                {ObdPid.Pid16, new Tuple<ObdCommand, uint>(ObdCommand.OxygenSensor3, (uint) 1 << 10)},
                {ObdPid.Pid17, new Tuple<ObdCommand, uint>(ObdCommand.OxygenSensor4, (uint) 1 << 9)},
                {ObdPid.Pid18, new Tuple<ObdCommand, uint>(ObdCommand.OxygenSensor5, (uint) 1 << 8)},
                {ObdPid.Pid19, new Tuple<ObdCommand, uint>(ObdCommand.OxygenSensor6, (uint) 1 << 7)},
                {ObdPid.Pid1A, new Tuple<ObdCommand, uint>(ObdCommand.OxygenSensor7, (uint) 1 << 6)},
                {ObdPid.Pid1B, new Tuple<ObdCommand, uint>(ObdCommand.OxygenSensor8, (uint) 1 << 5)},
                {ObdPid.Pid1C, new Tuple<ObdCommand, uint>(ObdCommand.ObdStandards, (uint) 1 << 4)},
                {ObdPid.Pid1D, new Tuple<ObdCommand, uint>(ObdCommand.OxygenSensorsPresentBanks4, (uint) 1 << 3)},
                {ObdPid.Pid1E, new Tuple<ObdCommand, uint>(ObdCommand.AuxiliaryInputStatus, (uint) 1 << 2)},
                {ObdPid.Pid1F, new Tuple<ObdCommand, uint>(ObdCommand.RunTimeSinceEngineStart, (uint) 1 << 1)},

                //PIDS 21 to 40
                {ObdPid.Pid21, new Tuple<ObdCommand, uint>(ObdCommand.DistanceTraveledWithMilOn, (uint) 1 << 31)},
                {ObdPid.Pid22, new Tuple<ObdCommand, uint>(ObdCommand.FuelRailPressure, (uint) 1 << 30)},
                {ObdPid.Pid23, new Tuple<ObdCommand, uint>(ObdCommand.FuelRailGaugePressure, (uint) 1 << 29)},
                //{ObdPid.Pid24, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 28)},
                //{ObdPid.Pid25, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 27)},
                //{ObdPid.Pid26, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 26)},
                //{ObdPid.Pid27, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 25)},
                //{ObdPid.Pid28, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 24)},
                //{ObdPid.Pid29, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 23)},
                //{ObdPid.Pid2A, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 22)},
                //{ObdPid.Pid2B, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 21)},
                {ObdPid.Pid2C, new Tuple<ObdCommand, uint>(ObdCommand.CommandedEgr, (uint) 1 << 20)},
                {ObdPid.Pid2D, new Tuple<ObdCommand, uint>(ObdCommand.EgrError, (uint) 1 << 19)},
                {ObdPid.Pid2E, new Tuple<ObdCommand, uint>(ObdCommand.CommandedEvaporatePurge, (uint) 1 << 18)},
                {ObdPid.Pid2F, new Tuple<ObdCommand, uint>(ObdCommand.FuelTankLevelInput, (uint) 1 << 17)},
                {ObdPid.Pid30, new Tuple<ObdCommand, uint>(ObdCommand.WarmUpsSinceCodesCleared, (uint) 1 << 16)},
                {ObdPid.Pid31, new Tuple<ObdCommand, uint>(ObdCommand.DistanceTraveledSinceCodesCleared, (uint) 15 << 1)},
                {ObdPid.Pid32, new Tuple<ObdCommand, uint>(ObdCommand.EvapSystemVaporPressure, (uint) 1 << 14)},
                {ObdPid.Pid33, new Tuple<ObdCommand, uint>(ObdCommand.AbsoluteBarometricPressure, (uint) 1 << 13)},
                //{ObdPid.Pid34, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 12)},
                //{ObdPid.Pid35, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 11)},
                //{ObdPid.Pid36, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 10)},
                //{ObdPid.Pid37, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 9)},
                //{ObdPid.Pid38, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 8)},
                //{ObdPid.Pid39, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 7)},
                //{ObdPid.Pid3A, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 6)},
                //{ObdPid.Pid3B, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 5)},
                {ObdPid.Pid3C, new Tuple<ObdCommand, uint>(ObdCommand.RunTimeSinceEngineStart, (uint) 1 << 4)},
                {ObdPid.Pid3D, new Tuple<ObdCommand, uint>(ObdCommand.RunTimeSinceEngineStart, (uint) 1 << 3)},
                {ObdPid.Pid3E, new Tuple<ObdCommand, uint>(ObdCommand.RunTimeSinceEngineStart, (uint) 1 << 2)},
                {ObdPid.Pid3F, new Tuple<ObdCommand, uint>(ObdCommand.RunTimeSinceEngineStart, (uint) 1 << 1)},


                //PIDS 41 to 60
                {ObdPid.Pid41, new Tuple<ObdCommand, uint>(ObdCommand.MonitorStatusThisDriveCycle, (uint) 1 << 31)},
                {ObdPid.Pid42, new Tuple<ObdCommand, uint>(ObdCommand.ControlModuleVoltage, (uint) 1 << 30)},
                {ObdPid.Pid43, new Tuple<ObdCommand, uint>(ObdCommand.AbsoluteLoadValue, (uint) 1 << 29)},
                {ObdPid.Pid44, new Tuple<ObdCommand, uint>(ObdCommand.FuelAirCommandedEquivalenceRation, (uint) 1 << 28)},
                {ObdPid.Pid45, new Tuple<ObdCommand, uint>(ObdCommand.RelativeThrottlePosition, (uint) 1 << 27)},
                {ObdPid.Pid46, new Tuple<ObdCommand, uint>(ObdCommand.AmbientAirTemperature, (uint) 1 << 26)},
                {ObdPid.Pid47, new Tuple<ObdCommand, uint>(ObdCommand.AbsoluteThrottlePositionB, (uint) 1 << 25)},
                {ObdPid.Pid48, new Tuple<ObdCommand, uint>(ObdCommand.AbsoluteThrottlePositionC, (uint) 1 << 24)},
                {ObdPid.Pid49, new Tuple<ObdCommand, uint>(ObdCommand.AbsoluteThrottlePositionD, (uint) 1 << 23)},
                {ObdPid.Pid4A, new Tuple<ObdCommand, uint>(ObdCommand.AbsoluteThrottlePositionE, (uint) 1 << 22)},
                {ObdPid.Pid4B, new Tuple<ObdCommand, uint>(ObdCommand.AbsoluteThrottlePositionF, (uint) 1 << 21)},
                {ObdPid.Pid4C, new Tuple<ObdCommand, uint>(ObdCommand.CommandedThrottleActuator, (uint) 1 << 20)},
                {ObdPid.Pid4D, new Tuple<ObdCommand, uint>(ObdCommand.TimeRunWithMilOn, (uint) 1 << 19)},
                {ObdPid.Pid4E, new Tuple<ObdCommand, uint>(ObdCommand.TimeSinceTroubleCodesCleared, (uint) 1 << 18)},
                {ObdPid.Pid4F, new Tuple<ObdCommand, uint>(ObdCommand.MaxFuelAirEquivalenceRation, (uint) 1 << 17)},
                {ObdPid.Pid50, new Tuple<ObdCommand, uint>(ObdCommand.MaxMaf, (uint) 1 << 16)},
                {ObdPid.Pid51, new Tuple<ObdCommand, uint>(ObdCommand.FuelType, (uint) 1 << 15)},
                {ObdPid.Pid52, new Tuple<ObdCommand, uint>(ObdCommand.EthanolFuelPercent, (uint) 1 << 14)},
                {ObdPid.Pid53, new Tuple<ObdCommand, uint>(ObdCommand.AbsoluteEvapSystemVaporPressure, (uint) 1 << 13)},
                //{ObdPid.Pid54, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 12)},
                //{ObdPid.Pid55, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 11)},
                //{ObdPid.Pid56, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 10)},
                //{ObdPid.Pid57, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 9)},
                //{ObdPid.Pid58, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 8)},
                {ObdPid.Pid59, new Tuple<ObdCommand, uint>(ObdCommand.FuelRailAbsolutePressure, (uint) 1 << 7)},
                {ObdPid.Pid5A, new Tuple<ObdCommand, uint>(ObdCommand.RelativeAcceleratorPedalPosition, (uint) 1 << 6)},
                {ObdPid.Pid5B, new Tuple<ObdCommand, uint>(ObdCommand.HybridBatteryPackRemainingLife, (uint) 1 << 5)},
                {ObdPid.Pid5C, new Tuple<ObdCommand, uint>(ObdCommand.EngineOilTemperature, (uint) 1 << 4)},
                {ObdPid.Pid5D, new Tuple<ObdCommand, uint>(ObdCommand.FuelInjectionTiming, (uint) 1 << 3)},
                {ObdPid.Pid5E, new Tuple<ObdCommand, uint>(ObdCommand.EngineFuelRate, (uint) 1 << 2)},
                {ObdPid.Pid5F, new Tuple<ObdCommand, uint>(ObdCommand.EmissionsRequirementsStandards, (uint) 1 << 1)},

                // PIDS 61 to 80
                {ObdPid.Pid61, new Tuple<ObdCommand, uint>(ObdCommand.DriversDemandEngineTorquePercent, (uint) 1 << 31)},
                {ObdPid.Pid62, new Tuple<ObdCommand, uint>(ObdCommand.ActualEngineTorquePercent, (uint) 1 << 30)},
                {ObdPid.Pid63, new Tuple<ObdCommand, uint>(ObdCommand.EngineReferenceTorque, (uint) 1 << 29)},
                {ObdPid.Pid64, new Tuple<ObdCommand, uint>(ObdCommand.EngineTorquePercent, (uint) 1 << 28)},
                //{ObdPid.Pid65, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 27)},
                //{ObdPid.Pid66, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 26)},
                //{ObdPid.Pid67, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 25)},
                //{ObdPid.Pid68, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 24)},
                //{ObdPid.Pid69, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 23)},
                //{ObdPid.Pid6A, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 22)},
                {ObdPid.Pid6B, new Tuple<ObdCommand, uint>(ObdCommand.ExhauseGasRecirculationTemperature, (uint) 1 << 21)},
                //{ObdPid.Pid6C, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 20)},
                //{ObdPid.Pid6D, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 19)},
                //{ObdPid.Pid6E, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 18)},
                {ObdPid.Pid6F, new Tuple<ObdCommand, uint>(ObdCommand.TurbochargerCompressorInletPressure, (uint) 1 << 17)},
                {ObdPid.Pid70, new Tuple<ObdCommand, uint>(ObdCommand.BoostPressureControl, (uint) 1 << 16)},
                //{ObdPid.Pid71, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 15)},
                {ObdPid.Pid72, new Tuple<ObdCommand, uint>(ObdCommand.WastegateControl, (uint) 1 << 14)},
                {ObdPid.Pid73, new Tuple<ObdCommand, uint>(ObdCommand.ExhaustPressure, (uint) 1 << 13)},
                {ObdPid.Pid74, new Tuple<ObdCommand, uint>(ObdCommand.TurbochargerRpm, (uint) 1 << 12)},
                {ObdPid.Pid75, new Tuple<ObdCommand, uint>(ObdCommand.TurbochargerTemperature1, (uint) 1 << 11)},
                {ObdPid.Pid76, new Tuple<ObdCommand, uint>(ObdCommand.TurbochargerTemperature2, (uint) 1 << 10)},
                {ObdPid.Pid77, new Tuple<ObdCommand, uint>(ObdCommand.ChargeAirCoolerTemperature, (uint) 1 << 9)},
                {ObdPid.Pid78, new Tuple<ObdCommand, uint>(ObdCommand.ExhaustGasTemperatureBank1, (uint) 1 << 8)},
                {ObdPid.Pid79, new Tuple<ObdCommand, uint>(ObdCommand.ExhaustGasTemperatureBank2, (uint) 1 << 7)},
                //{ObdPid.Pid7A, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 6)},
                //{ObdPid.Pid7B, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 5)},
                //{ObdPid.Pid7C, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 4)},
                //{ObdPid.Pid7D, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 3)},
                //{ObdPid.Pid7E, new Tuple<ObdCommand, uint>(ObdCommand.UnMapped, (uint) 1 << 2)},
                {ObdPid.Pid7F, new Tuple<ObdCommand, uint>(ObdCommand.EngineRunTime, (uint) 1 << 1)},
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
            return string.Format("PID {0}", pid.ToString().Replace("Pid", ""));
        }
        
    }
}
