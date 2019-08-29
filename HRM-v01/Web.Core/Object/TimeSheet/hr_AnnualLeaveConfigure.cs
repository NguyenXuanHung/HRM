using System;

namespace Web.Core.Object.TimeSheet
{
    /// <summary>
    /// Phép thường niên hàng năm
    /// </summary>
    public class hr_AnnualLeaveConfigure : BaseEntity
    {
        public hr_AnnualLeaveConfigure()
        {
            IsDeleted = false;
            CreatedBy = "admin";
            CreatedDate = DateTime.Now;
            EditedBy = "admin";
            EditedDate = DateTime.Now;
        }

        /// <summary>
        /// Ngày phép mặc định
        /// </summary>
        public double AnnualLeaveDay { get; set; }
        /// <summary>
        /// Cho phép sử dụng phép năm đầu tiên
        /// </summary>
        public bool AllowUseFirstYear { get; set; }
        /// <summary>
        /// Cho phép sử dụng phép năm trước
        /// </summary>
        public bool AllowUsePreviousYear { get; set; }
        /// <summary>
        /// Thời hạn sử dụng phép
        /// </summary>
        public DateTime? ExpiredDate { get; set; }
        /// <summary>
        /// Số ngày phép cộng thêm
        /// </summary>
        public double DayAddedStep { get; set; }
        /// <summary>
        /// Sau bao nhiêu năm cộng thêm ngày phép
        /// </summary>
        public int YearStep { get; set; }
        /// <summary>
        /// Số ngày phép tối đa trên tháng
        /// </summary>
        public double MaximumPerMonth { get; set; }

        /// <summary>
        /// Id hồ sơ
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// Số ngày phép đã sử dụng
        /// </summary>
        public double UsedLeaveDay { get; set; }
        
        /// <summary>
        /// Số ngày phép còn lại
        /// </summary>
        public double RemainLeaveDay { get; set; } 
        
        /// <summary>
        /// Năm hiện tại
        /// </summary>
        public int Year { get; set; }

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
