using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Enums;

namespace CarTracker.Common.ViewModels
{

    public class CarMaintenanceViewModel
    {

        public long CarMaintenanceId { get; set; }

        public long CarId { get; set; }

        public CarMaintenanceType Type { get; set; }

        public string Notes { get; set; }

        public DateTime Date { get; set; }

        public double Mileage { get; set; }
    }
}
