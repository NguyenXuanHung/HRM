using System;
using Web.Core.Object.Security;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework
{
    public class SchedulerModel : BaseModel<Scheduler>
    {
        private readonly Scheduler _scheduler;

        public SchedulerModel(Scheduler scheduler)
        {
            // init object
            _scheduler = scheduler ?? new Scheduler();

            // set field
            Init(_scheduler);
        }

        // Name
        public string Name { get; set; }

        // Description
        public string Description { get; set; }

        // SchedulerTypeId
        public int SchedulerTypeId { get; set; }

        // SchedulerTypeName
        public string SchedulerTypeName
        {
            get
            {
                var schedulerType = cat_BaseServices.GetById("cat_SchedulerType", _scheduler.SchedulerTypeId);
                return schedulerType != null ? schedulerType.Name : string.Empty;
            }
        }

        // Arguments
        public string Arguments { get; set; }

        // RepeatType
        public SchedulerRepeatType RepeatType { get; set; }

        // Name of RepeatType
        public string RepeatTypeName => _scheduler.RepeatType.Description();

        // Scope
        public SchedulerScope Scope { get; set; }

        // Name of Scope
        public string ScopeName => _scheduler.Scope.Description();

        // Interval time
        public int IntervalTime { get; set; }

        // Expired after
        public int ExpiredAfter { get; set; }

        // Expired Runtime
        public DateTime? ExpiredTime { get; set; }

        // Last Runtime
        public DateTime? LastRunTime { get; set; }

        // Next Runtime
        public DateTime? NextRunTime { get; set; }

        // Enabled
        public bool Enabled { get; set; }

        // Status
        public SchedulerStatus Status { get; set; }

        // Name of Status
        public string StatusName => _scheduler.Status.Description();
    }
}
