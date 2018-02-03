using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.ViewModels
{

    public class GeneralPagedViewModel
    {

        public GeneralPagedViewModel()
        {

        }

        public GeneralPagedViewModel(GeneralPagedViewModel other)
        {
            Skip = other.Skip;
            Take = other.Take;
            Total = other.Total;
        }

        public int Skip { get; set; }

        public int Take { get; set; }

        public int Total { get; set; }
    }

    public class PagedViewModel<T> : GeneralPagedViewModel
    {

        public PagedViewModel()
        {
            
        }

        public PagedViewModel(GeneralPagedViewModel other) : base(other)
        {

        }

        public IEnumerable<T> Data { get; set; }

    }
}
