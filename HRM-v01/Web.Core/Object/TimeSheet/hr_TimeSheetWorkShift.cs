using System;

namespace Web.Core.Object.TimeSheet
{
    /// <summary>
    /// Bảng phân ca
    /// </summary>
    public class hr_TimeSheetWorkShift : BaseEntity
    {
        public hr_TimeSheetWorkShift()
        {
            HasLimitTime = false;
            HasInOutTime = false;
            MinOverTime = 0;
            HasOverTime = false;
            HasHoliday = false;
            IsDeleted = false;
            CreatedBy = "admin";
            CreatedDate = DateTime.Now;
            EditedBy = "admin";
            EditedDate = DateTime.Now;
            Type = WorkShiftType.Normal;
        }
        /// <summary>
        /// Tên ca
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// id nhóm bảng phân ca
        /// </summary>
        public int GroupWorkShiftId { get; set; }

        /// <summary>
        /// id ký hiệu chấm công
        /// </summary>
        public int SymbolId { get; set; }        

        /// <summary>
        /// Ngày bắt đầu làm việc
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Ngày kết thúc làm việc
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Thời gian bắt đầu vào hiểu ca
        /// </summary>
        public DateTime StartInTime { get; set; }
        
        /// <summary>
        /// Thời gian kết thúc vào hiểu ca
        /// </summary>
        public DateTime EndInTime { get; set; }

        /// <summary>
        /// Thời gian bắt đầu ra hiểu ca
        /// </summary>
        public DateTime StartOutTime { get; set; }
        
        /// <summary>
        /// Thời gian kết thúc ra hiểu ca
        /// </summary>
        public DateTime EndOutTime { get; set; }

        /// <summary>
        /// Thời gian bắt đầu nghỉ ca
        /// </summary>
        public DateTime? StartFreeTime { get; set; }

        /// <summary>
        /// Thời gian kết thúc nghỉ ca
        /// </summary>
        public DateTime? EndFreeTime { get; set; }

        /// <summary>
        /// Công quy đổi
        /// </summary>
        public double WorkConvert { get; set; }

        /// <summary>
        /// Thời gian quy đổi
        /// </summary>
        public double TimeConvert { get; set; }

        /// <summary>
        /// Có thêm giờ không
        /// </summary>
        public bool HasOverTime { get; set; }

        /// <summary>
        /// Số phút tối thiểu thêm giờ
        /// </summary>
        public int MinOverTime { get; set; }

        /// <summary>
        /// Có tinh gio vao ra khong
        /// </summary>
        public bool HasInOutTime { get; set; }

        /// <summary>
        /// Có xóa không
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Giới hạn thời gian làm việc tối đa
        /// </summary>
        public bool HasLimitTime { get; set; }
        
        /// <summary>
        /// Bỏ ngày lễ tết
        /// </summary>
        public bool HasHoliday { get; set; }

        /// <summary>
        /// type normal/break
        /// </summary>
        public WorkShiftType Type { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày thay đổi
        /// </summary>
        public DateTime EditedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string EditedBy { get; set; }
    }
}
