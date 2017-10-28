using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.ViewModels
{
    public class BulkUploadResultViewModel
    {

        public string Uuid { get; set; }

        public long Id { get; set; }

        public bool Successful { get; set; }

        public string ErrorMessage { get; set; }

    }
}
