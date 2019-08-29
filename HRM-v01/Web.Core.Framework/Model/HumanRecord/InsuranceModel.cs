using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public class InsuranceModel : BaseModel<hr_Insurance>
    {
        private readonly hr_Insurance _insurance;
        private readonly hr_Record _record;

        public InsuranceModel()
        {
            _insurance = new hr_Insurance();
            _record = new hr_Record();
            Init(_insurance);
        }

        public InsuranceModel(hr_Insurance insurance)
        {
            _insurance = insurance ?? new hr_Insurance();
            _record = hr_RecordServices.GetById(_insurance.RecordId) ?? new hr_Record();
            Init(_insurance);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// id chuc vu
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Hệ số lương
        /// </summary>
        public decimal SalaryFactor { get; set; }

        /// <summary>
        /// Phụ cấp
        /// </summary>
        public decimal Allowance { get; set; }

        /// <summary>
        /// Mức lương
        /// </summary>        
        public decimal SalaryLevel { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Tỷ lệ
        /// </summary>
        public string Rate { get; set; }

        /// <summary>
        /// Từ ngày
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Đến ngày
        /// </summary>
        public DateTime? ToDate { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime EditedDate { get; set; }
        public string EditedBy { get; set; }

        #region Custom Properties

        /// <summary>
        /// Chức vụ
        /// </summary>
        public string PositionName => cat_PositionServices.GetFieldValueById(_insurance.PositionId);

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string DepartmentName => cat_DepartmentServices.GetFieldValueById(_insurance.DepartmentId);

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode => _record.EmployeeCode;

        /// <summary>
        /// Tên đầy đủ
        /// </summary>
        public string FullName => _record.FullName;

        #endregion

    }
}
