using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using Web.Core.Object.Security;
using Web.Core.Service.Catalog;
using Web.Core.Service.Security;

namespace Web.Core.Framework
{
    public class JobScheduler : IJob
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                // init time
                var time = DateTime.Now.AddMinutes(1);

                // init to time
                var toTime = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0);

                // get ready scheduler
                var scheduler = SchedulerServices.GetAll(null, null, null, SchedulerStatus.Ready, true, SchedulerScope.Internal, null, toTime, null, 1).FirstOrDefault();
                if (scheduler != null)
                {
                    // run scheduler
                    await RunScheduler(scheduler);
                }
            }
            catch (Exception ex)
            {
                // create log
                var systemLog = new SystemLogModel
                {
                    Username = "system",
                    Thread = "JobScheduler",
                    Action = SystemAction.TrackError,
                    ShortDescription = ex.Message,
                    LongDescription = ex.ToString(),
                    IsException = true,
                    CreatedDate = DateTime.Now
                };

                // log exception
                SystemLogController.Create(systemLog);
            }
        }

        #region Excute Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduler"></param>
        private Task RunScheduler(Scheduler scheduler)
        {
            try
            {
                // next run time
                DateTime? nextRunTime = null;
                if (scheduler.NextRunTime == null) return Task.FromCanceled(new CancellationToken(true));
                // switch repeat type
                switch (scheduler.RepeatType)
                {
                    case SchedulerRepeatType.OneTime:
                        break;
                    case SchedulerRepeatType.Monthly:
                        nextRunTime = scheduler.NextRunTime.Value.AddMonths(1);
                        break;
                    case SchedulerRepeatType.Weekly:
                        nextRunTime = scheduler.NextRunTime.Value.AddDays(7);
                        break;
                    case SchedulerRepeatType.Daily:
                        nextRunTime = scheduler.NextRunTime.Value.AddDays(1);
                        break;
                    case SchedulerRepeatType.Hourly:
                        nextRunTime = scheduler.NextRunTime.Value.AddHours(1);
                        break;
                    case SchedulerRepeatType.IntervalTime:
                        if (scheduler.IntervalTime > 0)
                            nextRunTime = scheduler.NextRunTime.Value.AddMinutes(scheduler.IntervalTime);
                        break;
                    default:
                        return Task.FromCanceled(new CancellationToken(true));
                }

                // expired time
                DateTime? expiredTime = null;
                if (scheduler.ExpiredAfter > 0 && scheduler.RepeatType != SchedulerRepeatType.OneTime)
                    expiredTime = scheduler.NextRunTime.Value.AddMinutes(scheduler.ExpiredAfter);
                // update current scheduler status to running
                SchedulerServices.Update(scheduler.Id, SchedulerStatus.Running, DateTime.Now, nextRunTime, expiredTime);
                // init scheduler type
                var schedulerType = cat_BaseServices.GetById("cat_SchedulerType", scheduler.SchedulerTypeId);
                // init job type
                var jobType = Type.GetType("Web.Core.Framework.{0},Web.Core.Framework".FormatWith(schedulerType.Name));
                if (jobType == null) return Task.FromCanceled(new CancellationToken(true));
                // init job
                var job = Activator.CreateInstance(jobType);
                // int job property
                ((BaseTask) job).Id = scheduler.Id;
                ((BaseTask) job).Name = scheduler.Name;
                ((BaseTask) job).Arguments = scheduler.Arguments;
                // execute job
                ((BaseTask) job).Excute(scheduler.Arguments);

                // update scheduler status when complete
                SchedulerServices.Update(scheduler.Id, scheduler.RepeatType == SchedulerRepeatType.OneTime
                    ? SchedulerStatus.Completed
                    : SchedulerStatus.Ready);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // log error
                SystemLogController.Create(new SystemLogModel("system", "RunScheduler", SystemAction.TrackError,
                    SystemLogType.ScheduleError, ex.Message, ex.ToString(), true));
                return Task.FromException(ex);
            }
        }

        #endregion
    }
}
