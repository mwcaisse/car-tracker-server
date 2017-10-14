using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.ViewModels
{
    public class CarViewModel
    {

        public long Id { get; set; }

        public string Vin { get; set; }

        public string Name { get; set; }

        public long? OwnerId { get; set; }

        public double? Mileage { get; set; }

        public DateTime? MileageLastUserSet { get; set; }

    }
}
