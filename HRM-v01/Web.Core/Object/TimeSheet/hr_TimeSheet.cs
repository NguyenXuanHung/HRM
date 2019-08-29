using System;

namespace Web.Core.Object.TimeSheet
{
    /// <summary>
    /// Tổng hợp công
    /// </summary>
    public class hr_TimeSheet : BaseEntity
    {
        public hr_TimeSheet()
        {
            CreatedBy = "system";
            CreatedDate = DateTime.Now;
            EditedBy = "system";
            EditedDate = DateTime.Now;
        }

        /// <summary>
        /// id hồ sơ
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// Ngày bắt đầu làm việc
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Ngày kết thúc làm việc
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Thoi gian log
        /// </summary>
        public string TimeLogs { get; set; }

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedDate { get; set; }
    }
}
