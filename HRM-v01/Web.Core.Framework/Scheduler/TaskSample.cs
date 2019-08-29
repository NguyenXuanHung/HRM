using System;
using Web.Core.Object.Security;
using Web.Core.Service.Security;

namespace Web.Core.Framework
{
    public class TaskSample : BaseTask
    {
        private SchedulerHistory _schedulerHistory;

        public override void Excute(string args)
        {
            // create history
            _schedulerHistory = SchedulerHistoryServices.Create(Id, Name.ToUpper(), false, string.Empty, string.Empty, DateTime.Now, DateTime.MaxValue);
            // start process
            if (_schedulerHistory == null) return;
            // log start
            SchedulerLogServices.Create(_schedulerHistory.Id, "START");
            try
            {
                // progress
                DoTaskSample("TaskSample InProgress");
            }
            catch (Exception ex)
            {
                // log exception
                SchedulerLogServices.Create(_schedulerHistory.Id, "EXCEPTION: {0}".FormatWith(ex.ToString()));
            }
            finally
            {
                // log end
                SchedulerLogServices.Create(_schedulerHistory.Id, "END");
            }
        }

        private void DoTaskSample(string args)
        {
            for (var i = 0; i < 10; i++)
            {
                SchedulerLogServices.Create(_schedulerHistory.Id, "{0} {1}: {2}".FormatWith(args, i, DateTime.Now));
                System.Threading.Thread.Sleep(1000 * 5);
            }
        }
    }
}
