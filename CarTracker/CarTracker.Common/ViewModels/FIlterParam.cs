using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Enums;

namespace CarTracker.Common.ViewModels
{
    public class FilterParam
    {

        public string ColumnName { get; set; }

        public FilterOperation Operation { get; set; }

        public string Value { get; set; }

    }


}
