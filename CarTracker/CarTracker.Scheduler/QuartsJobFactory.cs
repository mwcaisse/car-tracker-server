using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using Quartz.Spi;

namespace CarTracker.Scheduler
{
    public class QuartzJobFactory : IJobFactory
    {
        private readonly IServiceProvider _container;

        public QuartzJobFactory(IServiceProvider container)
        {
            this._container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _container.GetService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            // if the job implements IDisposable, dispose of it when job is over
            (job as IDisposable)?.Dispose();
        }
    }
}
