using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for AnnualLeaveConfigureModel
    /// </summary>
    public class AnnualLeaveConfigureModel : BaseModel<hr_AnnualLeaveConfigure>
    {
        // private property
        private readonly hr_AnnualLeaveConfigure _annualLeave;
        private readonly hr_Record _record;

        public AnnualLeaveConfigureModel()
        {
            _annualLeave = new hr_AnnualLeaveConfigure();
            _record = new hr_Record();
            //init
            Init(_annualLeave);
        }

        public AnnualLeaveConfigureModel(hr_AnnualLeaveConfigure annualLeave)
        {
            _annualLeave = annualLeave ?? new hr_AnnualLeaveConfigure();
            //set props
            Init(_annualLeave);

            //get data relation
            _record = hr_RecordServices.GetById(_annualLeave.RecordId);
            _record = _record ?? new hr_Record();
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
        /// Cho phép sử dụng phép năm cũ
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

        /// <summary>
        /// 
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EditedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EditedDate { get; set; }

        #region Custom Properties

        /// <summary>
        /// Họ và tên NV
        /// </summary>
        public string FullName => _record.FullName;

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode => _record.EmployeeCode;
        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string DepartmentName => cat_DepartmentServices.GetFieldValueById(_record.DepartmentId);


        #endregion
    }
}
