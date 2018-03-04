using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Services
{
    public interface IJobScheduler
    {

        void Start();

        void Stop();

        void RunJobNow(Type jobType);

    }
}
