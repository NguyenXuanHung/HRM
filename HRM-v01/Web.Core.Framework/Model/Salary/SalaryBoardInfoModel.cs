using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for SalaryBoardInfoModel
    /// </summary>
    public class SalaryBoardInfoModel : BaseModel<hr_SalaryBoardInfo>
    {
        private readonly hr_SalaryBoardInfo _salaryBoardInfo;
        private readonly hr_Record _record;

        public SalaryBoardInfoModel()
        {
            _salaryBoardInfo = new hr_SalaryBoardInfo();
            _record = new hr_Record();

            //set props
            Init(_salaryBoardInfo);
        }

        public SalaryBoardInfoModel(hr_SalaryBoardInfo salaryBoardInfo)
        {
            _salaryBoardInfo = salaryBoardInfo ?? new hr_SalaryBoardInfo();
            _record = hr_RecordServices.GetById(_salaryBoardInfo.RecordId) ?? new hr_Record();

            //set props
            Init(_salaryBoardInfo);
        }

        /// <summary>
        /// Id danh sách bảng lương
        /// </summary>
        public int SalaryBoardId { get; set; }

        /// <summary>
        /// Id hồ sơ
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string FullName => _record.FullName;

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode => _record.EmployeeCode;

        /// <summary>
        /// Tên chức vụ
        /// </summary>
        public string PositionName => cat_PositionServices.GetFieldValueById(_record.PositionId);

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName => cat_DepartmentServices.GetFieldValueById(_record.DepartmentId);

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
        /// Công nghỉ phép
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
