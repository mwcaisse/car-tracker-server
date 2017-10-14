using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.ViewModels
{
    public class PagedViewModel<T>
    {

        public IEnumerable<T> Data { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }

        public int Count { get; set; }

    }
}
