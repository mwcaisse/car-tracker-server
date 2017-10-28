using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.ViewModels
{
    public class BulkUploadViewModel<T> where T: class
    {
        public string Uuid { get; set; }

        public T Data { get; set; }
    }
}
