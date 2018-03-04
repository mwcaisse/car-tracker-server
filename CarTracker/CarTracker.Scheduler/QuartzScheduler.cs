using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarTracker.Common.Services;
using CarTracker.Scheduler.Jobs;
using Quartz;
using Quartz.Impl;

namespace CarTracker.Scheduler
{
    public class QuartzScheduler : IJobScheduler
    {

        private IScheduler _scheduler;

        private readonly IServiceProvider _container;

        public QuartzScheduler(IServiceProvider container)
        {
            this._container = container;
        }

        public void Start()
        {
            if (null != _scheduler)
            {
                throw new InvalidOperationException("Scheduler is already started.");
            }

            var schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler().Result;
            _scheduler.JobFactory = new QuartzJobFactory(_container);
            _scheduler.Start().Wait();

            RegisterJobs();
        }

        protected void RegisterJobs()
        {
            var processTripsJob = JobBuilder.Create<TripProcessJob>()
                .WithIdentity("process_trips")
                .Build();
            var processTripsTrigger = TriggerBuilder.Create()
                .WithIdentity("process_trips_trigger")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(1))
                .Build();

            _scheduler.ScheduleJob(processTripsJob, processTripsTrigger).Wait();
        }

        public void Stop()
        {
            if (null == _scheduler)
            {
                return;
            }
            if (!_scheduler.Shutdown(waitForJobsToComplete: true).Wait(TimeSpan.FromSeconds(30)))
            {
                // didn't shutdown after 30 seconds. force the shutdown
                _scheduler.Shutdown(waitForJobsToComplete: false);
            }
            _scheduler = null;
        }

        public void RunJobNow(Type jobType)
        {
            var job = JobBuilder.Create(jobType).Build();
            var trigger = TriggerBuilder.Create()
                .StartNow()
                .Build();
            _scheduler.ScheduleJob(job, trigger);
        }

    }
}
