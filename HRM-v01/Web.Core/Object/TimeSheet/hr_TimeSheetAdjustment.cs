using System;

namespace Web.Core.Object.TimeSheet
{
    /// <summary>
    /// Phiếu hiệu chỉnh
    /// </summary>
    public class hr_TimeSheetAdjustment : BaseEntity
    {
        public hr_TimeSheetAdjustment()
        {
            CreatedBy = "system";
            CreatedDate = DateTime.Now;
            EditedBy = "system";
            EditedDate = DateTime.Now;
        }

        /// <summary>
        /// id ho so
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// id bang cham cong
        /// </summary>
        public int TimeSheetReportId { get; set; }

        /// <summary>
        ///  id event
        /// </summary>
        public int TimeSheetEventId { get; set; }

        /// <summary>
        /// Mã chấm công
        /// </summary>
        public string TimeSheetCode { get; set; }

        /// <summary>
        /// Công quy đổi
        /// </summary>
        public double WorkConvert { get; set; }

        /// <summary>
        /// Thời gian quy đổi
        /// </summary>
        public double TimeConvert { get; set; }

        /// <summary>
        /// Lý do hiệu chỉnh
        /// </summary>
        public  string Reason { get; set; }

        /// <summary>
        /// Khóa
        /// </summary>
        public bool IsLock { get; set; }

        /// <summary>
        /// Loại hiệu chỉnh
        /// </summary>
        public TimeSheetAdjustmentType Type { get; set; }

        /// <summary>
        /// Chi tiết
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// Ngày hiệu chỉnh
        /// </summary>
        public DateTime? AdjustDate { get; set; }

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
