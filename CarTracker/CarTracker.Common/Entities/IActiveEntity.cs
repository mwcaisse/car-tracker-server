using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Entities
{
    public interface IActiveEntity
    {
        bool Active { get; set; }
    }
}
