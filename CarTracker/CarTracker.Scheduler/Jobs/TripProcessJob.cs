using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarTracker.Common.Services;
using Quartz;

namespace CarTracker.Scheduler.Jobs
{
    public class TripProcessJob : IJob
    {

        private readonly ITripProcessor _tripProcessor;

        public TripProcessJob(ITripProcessor tripProcessor)
        {
            _tripProcessor = tripProcessor;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _tripProcessor.ProcessUnprocessedTrips();
            return Task.CompletedTask;
        }
    }
}
