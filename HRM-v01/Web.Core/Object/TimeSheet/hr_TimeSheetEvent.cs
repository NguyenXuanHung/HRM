using System;

namespace Web.Core.Object.TimeSheet
{
    /// <summary>
    /// Bảng chấm công event
    /// </summary>
    public class hr_TimeSheetEvent : BaseEntity
    {
        public hr_TimeSheetEvent()
        {
            Status = EventStatus.Active;
            IsDeleted = false;
            CreatedBy = "system";
            CreatedDate = DateTime.Now;
            EditedBy = "system";
            EditedDate = DateTime.Now;
            Type = TimeSheetAdjustmentType.Default;
        }

        /// <summary>
        /// Id hồ sơ
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// id ca chi tiết
        /// </summary>
        public int WorkShiftId { get; set; }

        /// <summary>
        /// id nhóm ký hiệu
        /// </summary>
        public int GroupSymbolId { get; set; }

        /// <summary>
        /// id ký hiệu
        /// </summary>
        public int SymbolId { get; set; }

        /// <summary>
        /// Công quy đổi
        /// </summary>
        public double WorkConvert { get; set; }

        /// <summary>
        /// Thời gian quy đổi
        /// </summary>
        public double TimeConvert { get; set; }

        /// <summary>
        /// Loại hiệu chỉnh
        /// </summary>
        public TimeSheetAdjustmentType Type { get; set; }
        
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
      
        /// <summary>
        /// True if object was deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Trạng thái event (active, delete, pending,... )
        /// </summary>
        public EventStatus Status { get; set; }

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedDate { get; set; }
    }
}
