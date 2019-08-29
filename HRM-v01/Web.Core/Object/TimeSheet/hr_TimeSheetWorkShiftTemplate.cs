using System;

namespace Web.Core.Object.TimeSheet
{
    /// <summary>
    /// Bảng template
    /// </summary>
    public class hr_TimeSheetWorkShiftTemplate : BaseEntity
    {
        public hr_TimeSheetWorkShiftTemplate()
        {
            HasMonday = false;
            HasTuesday = false;
            HasWednesday = false;
            HasThursday = false;
            HasFriday = false;
            HasSaturday = false;
            HasSunday = false;
            HasLimitTime = false;
            HasInOutTime = false;
            MinOverTime = 0;
            HasOverTime = false;
            HasHoliday = false;
            TypeEnd = 0;
            TypeStartIn = 0;
            TypeEndIn = 0;
            TypeStartOut = 0;
            TypeEndOut = 0;
            IsDeleted = false;
            CreatedBy = "admin";
            CreatedDate = DateTime.Now;
            EditedBy = "admin";
            EditedDate = DateTime.Now;
            Type = WorkShiftType.Normal;
        }

        /// <summary>
        /// Tên template
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// id nhóm ký hiệu
        /// </summary>
        public int GroupSymbolId { get; set; }

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
        /// Co tinh le tet
        /// </summary>
        public bool HasHoliday { get; set; }

        /// <summary>
        /// Có xóa không
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Giới hạn thời gian làm việc tối đa
        /// </summary>
        public bool HasLimitTime { get; set; }
        
        /// <summary>
        /// has monday
        /// </summary>
        public bool HasMonday { get; set; }
        
        /// <summary>
        /// has tuesday
        /// </summary>
        public bool HasTuesday { get; set; }
        
        /// <summary>
        /// has Wednesday
        /// </summary>
        public bool HasWednesday { get; set; }
        
        /// <summary>
        /// has thursday
        /// </summary>
        public bool HasThursday { get; set; } 
        
        /// <summary>
        /// has friday
        /// </summary>
        public bool HasFriday { get; set; }
        
        /// <summary>
        /// has tuesday
        /// </summary>
        public bool HasSaturday { get; set; }
        
        /// <summary>
        /// has tuesday
        /// </summary>
        public bool HasSunday { get; set; }

        /// <summary>
        /// type normal/break
        /// </summary>
        public WorkShiftType Type { get; set; }

        /// <summary>
        /// cbo same day or other day
        /// </summary>
        public int TypeEnd { get; set; } 
        
        /// <summary>
        /// cbo same day or other day
        /// </summary>
        public int TypeStartIn { get; set; }

        /// <summary>
        /// cbo same day or other day
        /// </summary>
        public int TypeEndIn { get; set; } 
        
        /// <summary>
        /// cbo same day or other day
        /// </summary>
        public int TypeStartOut { get; set; }
        
        /// <summary>
        /// cbo same day or other day
        /// </summary>
        public int TypeEndOut { get; set; }

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
