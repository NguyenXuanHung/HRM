using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;

namespace Web.Core.Framework
{
    public class MainScheduler
    {
        private const string CronExpression = @"0 0/1 * ? * *";

        private IScheduler _sched;

        public async void Run()
        {
            // check allow scheduler
            if ((bool)AppSettingHelper.GetAppSetting(typeof(bool), "AllowScheduler", true, false))
            {
                // construct a scheduler factory
                var props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                // init schedule
                var schedFact = new StdSchedulerFactory(props);
                // get a scheduler
                _sched = await schedFact.GetScheduler();
                // job scheduler
                // init job
                var jobScheduler = JobBuilder.Create<JobScheduler>()
                    .WithIdentity("jobScheduler", "scheduler").Build();

                var triggerScheduler = TriggerBuilder.Create()
                    .WithIdentity("triggerScheduler", "scheduler")
                    .WithCronSchedule((string)AppSettingHelper.GetAppSetting(typeof(string), "CronExpression",
                        CronExpression, false))
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(60).RepeatForever().WithMisfireHandlingInstructionFireNow())
                    .ForJob(jobScheduler)
                    .Build();

                // add to schedule
                await _sched.ScheduleJob(jobScheduler, triggerScheduler);

                // start
                await _sched.Start();
            }
        }

        public async void Pause()
        {
            await _sched.PauseAll();
        }

        public async void Stop()
        {
            await _sched.Shutdown();
        }
    }
}
