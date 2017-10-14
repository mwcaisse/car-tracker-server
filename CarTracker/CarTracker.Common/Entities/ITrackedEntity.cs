using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Entities
{
    public interface ITrackedEntity
    {
        
        DateTime CreateDate { get; set; }
        DateTime ModifiedDate { get; set; }

    }
}
