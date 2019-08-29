using System;

namespace Web.Core.Object.HumanRecord
{
    /// <summary>
    /// Bảng lương
    /// </summary>
    public class hr_SalaryBoardInfo : BaseEntity
    {
        /// <summary>
        /// Id danh sách bảng lương
        /// </summary>
        public int SalaryBoardId { get; set; }
        /// <summary>
        /// Id hồ sơ
        /// </summary>
        public int RecordId { get; set; }
       
        /// <summary>
        /// Lương cơ bản
        /// </summary>
        public double SalaryBasic { get; set; }
        /// <summary>
        /// Hệ số lương
        /// </summary>
        public double SalaryFactor { get; set; }

        /// <summary>
        /// Lương Net
        /// </summary>
        public double? SalaryNet { get; set; }
        /// <summary>
        /// Lương gross
        /// </summary>
        public double? SalaryGross { get; set; }
        /// <summary>
        /// Lương theo hợp đồng
        /// </summary>
        public double? SalaryContract { get; set; }
        /// <summary>
        /// Lương theo bảo hiểm
        /// </summary>
        public double? SalaryInsurance { get; set; }

        /// <summary>
        /// Công chuẩn 
        /// </summary>
        public double WorkStandardDay { get; set; }
        /// <summary>
        /// Công thực tế
        /// </summary>
        public double WorkActualDay { get; set; }
        /// <summary>
        /// Công thường
        /// </summary>
        public double WorkNormalDay { get; set; } 
        
        /// <summary>
        /// Công nghỉ phép T
        /// </summary>
        public double? WorkPaidLeave { get; set; }  
        /// <summary>
        /// Công đầy đủ X
        /// </summary>
        public double? WorkFullDay { get; set; } 
        
        /// <summary>
        /// Thêm giờ
        /// </summary>
        public double? OverTime { get; set; }
        /// <summary>
        /// Tăng ca đêm
        /// </summary>
        public double? OverTimeNight { get; set; }
        /// <summary>
        /// Tăng ca ngày
        /// </summary>
        public double? OverTimeDay { get; set; }
        /// <summary>
        /// Tăng ca ngày lễ
        /// </summary>
        public double? OverTimeHoliday { get; set; } 
        /// <summary>
        /// Tăng ca cuối tuần
        /// </summary>
        public double? OverTimeWeekend { get; set; }

        /// <summary>
        /// Tong so cong không phép
        /// </summary>
        public double? WorkUnLeave { get; set; }

        /// <summary>
        /// Tong so cong cong tac
        /// </summary>
        public double? WorkGoWork { get; set; }
        
        /// <summary>
        /// Tong so cong cong tac
        /// </summary>
        public double? WorkLate { get; set; }

        /// <summary>
        /// Tong so cong nghi le
        /// </summary>
        public double? WorkHoliday { get; set; } 
        
        /// <summary>
        /// Tong so cong nghỉ phép không lương
        /// </summary>
        public double? WorkUnpaidLeave { get; set; }

        // các trường mặc định
        public DateTime? CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
