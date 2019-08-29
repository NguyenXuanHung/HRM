using System;

namespace Web.Core.Object.TimeSheet
{
    /// <summary>
    /// lịch sử phép thường niên hàng năm
    /// </summary>
    public class hr_AnnualLeaveHistory : BaseEntity
    {
        public hr_AnnualLeaveHistory()
        {
            IsDeleted = false;
            CreatedBy = "admin";
            CreatedDate = DateTime.Now;
            EditedBy = "admin";
            EditedDate = DateTime.Now;
        }

        /// <summary>
        /// Id hồ sơ
        /// </summary>
        public int RecordId { get; set; }
        /// <summary>
        /// Id event
        /// </summary>
        public int TimeSheetEventId { get; set; }

        /// <summary>
        /// Ngày sử dụng
        /// </summary>
        public DateTime UsedLeaveDate { get; set; }

        /// <summary>
        /// Số ngày phép đã sử dụng
        /// </summary>
        public double UsedLeaveDay { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted { get; set; }

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
