using System;
using System.ComponentModel;

namespace Web.Core.Object.Security
{
    public class Scheduler : BaseEntity
    {
        // Name
        public string Name { get; set; }
        // Description
        public string Description { get; set; }
        // SchedulerTypeId
        public int SchedulerTypeId { get; set; }
        // Arguments
        public string Arguments { get; set; }
        // Repeated Type
        public SchedulerRepeatType RepeatType { get; set; }
        // Scope
        public SchedulerScope Scope { get; set; }
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
    }

    [Flags]
    public enum SchedulerRepeatType
    {
        [Description("Một lần")]
        OneTime = 1,
        [Description("Thời gian")]
        IntervalTime = 2,
        [Description("Hàng giờ")]
        Hourly = 3,
        [Description("Hàng ngày")]
        Daily = 4,
        [Description("Hàng tuần")]
        Weekly = 5,
        [Description("Hàng tháng")]
        Monthly = 6,
    }

    [Flags]
    public enum SchedulerStatus
    {
        [Description("Sẵn sàng")]
        Ready = 1,
        [Description("Đang chạy")]
        Running = 2,
        [Description("Đã hoàn thành")]
        Completed = 3,
        [Description("Đã xóa")]
        Deleted = 4
    }

    [Flags]
    public enum SchedulerScope
    {
        [Description("Nội bộ")]
        Internal = 1,
        [Description("Chạy từ bên ngoài")]
        External = 2
    }
}
