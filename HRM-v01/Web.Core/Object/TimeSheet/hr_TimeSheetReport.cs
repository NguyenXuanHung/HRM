using System;

namespace Web.Core.Object.TimeSheet
{
    public class hr_TimeSheetReport : BaseEntity
    {
        public hr_TimeSheetReport()
        {
            IsDeleted = false;
            Status = TimeSheetStatus.Active;
            CreatedDate = DateTime.Now;
            CreatedBy = "system";
            EditedDate = DateTime.Now;
            EditedBy = "system";
        }

        /// <summary>
        /// Report name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Trạng thái lock/unlock
        /// </summary>
        public TimeSheetStatus Status { get; set; }

        /// <summary>
        /// Ngày bắt đầu
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Deleted status
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// System, created user
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// System, created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// System, edited user
        /// </summary>
        public string EditedBy { get; set; }

        /// <summary>
        /// System, edited date
        /// </summary>
        public DateTime EditedDate { get; set; }
    }
}
