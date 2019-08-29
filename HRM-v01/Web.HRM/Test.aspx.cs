using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Object.Security;
using Web.Core.Service.Catalog;
using Web.Core.Service.Security;

namespace Web.HRM
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // init time
            var time = DateTime.Now.AddMinutes(1);

            // init to time
            var toTime = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0);

            // get ready scheduler
            var scheduler = SchedulerServices.GetById(27);
                //SchedulerServices.GetAll(null, null, null, SchedulerStatus.Ready, true, SchedulerScope.Internal, null, toTime, null, 1).FirstOrDefault();
            if (scheduler != null)
            {
                // run scheduler
                RunScheduler(scheduler);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduler"></param>
        private void RunScheduler(Scheduler scheduler)
        {
            try
            {
                // next run time
                DateTime? nextRunTime = null;
                if (scheduler.NextRunTime == null) return;
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
                        return;
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
                if (jobType == null) return;
                // init job
                var job = Activator.CreateInstance(jobType);
                // int job property
                ((BaseTask)job).Id = scheduler.Id;
                ((BaseTask)job).Name = scheduler.Name;
                ((BaseTask)job).Arguments = scheduler.Arguments;
                // execute job
                ((BaseTask)job).Excute(scheduler.Arguments);
            }
            catch (ThreadAbortException)
            {
                // do nothing
            }
            catch (Exception ex)
            {
                // log error
                SystemLogController.Create(new SystemLogModel("system", "RunScheduler", SystemAction.TrackError,
                    SystemLogType.ScheduleError, ex.Message, ex.ToString(), true));
            }
            finally
            {
                // update scheduler status when complete
                SchedulerServices.Update(scheduler.Id, scheduler.RepeatType == SchedulerRepeatType.OneTime
                    ? SchedulerStatus.Completed
                    : SchedulerStatus.Ready);
            }
        }
    }
}