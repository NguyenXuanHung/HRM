using System;

namespace Web.Core.Object.TimeSheet
{
    /// <summary>
    /// Bảng mã chấm công
    /// </summary>
    public class hr_TimeSheetCode : BaseEntity
    {
        /// <summary>
        /// Mã chấm công
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// Id hồ sơ
        /// </summary>
        public int RecordId { get; set; }
        
        /// <summary>
        /// id các máy chấm công
        /// </summary>
        public int MachineId { get; set; }
        /// <summary>
        /// Thời gian bắt đầu
        /// </summary>
        public DateTime StartTime { get; set; }
        
        /// <summary>
        /// Thời gian kết thúc
        /// </summary>
        public DateTime? EndTime { get; set; }
        
        /// <summary>
        /// Ngày làm việc
        /// </summary>
        public DateTime? WorkingDay { get; set; }
        
        /// <summary>
        /// Trạng thái hoạt động mã chấm công
        /// </summary>
        public bool IsActive { get; set; }


        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
