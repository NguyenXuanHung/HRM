using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Object.Salary;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    public class PayrollInfoModel : BaseModel<sal_PayrollInfo>
    {
        private readonly sal_PayrollInfo _payroll;
        private readonly hr_Record _record;
        public PayrollInfoModel()
        {
            // init payroll value
            _payroll = new sal_PayrollInfo();
            _record = new hr_Record();

            // set model props
            Init(_payroll);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="payroll"></param>
        public PayrollInfoModel(sal_PayrollInfo payroll)
        {
            // init payroll value
            _payroll = payroll ?? new sal_PayrollInfo();

            // set model props
            Init(_payroll);
            _record = hr_RecordServices.GetById(_payroll.RecordId);
            _record = _record ?? new hr_Record();
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
        /// Lương bảo hiểm DN đóng
        /// </summary>
        public decimal EnterpriseSocial { get; set; }

        /// <summary>
        /// Lương bảo hiểm người lao động đóng
        /// </summary>
        public decimal LaborerSocial { get; set; }

        /// <summary>
        /// Tổng thu nhập
        /// </summary>
        public decimal TotalIncome { get; set; }

        /// <summary>
        /// Thuế thu nhập cá nhân
        /// </summary>
        public decimal IndividualTax { get; set; }

        /// <summary>
        /// Thực lĩnh chuyển khoản
        /// </summary>
        public decimal ActualSalary { get; set; }

        /// <summary>
        /// Month
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Deleted status
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// System, created user
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// System, created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// System, edited user
        /// </summary>
        public string EditedBy { get; set; }

        /// <summary>
        /// System, edited date
        /// </summary>
        public DateTime EditedDate { get; set; }

        #region custom props

        /// <summary>
        /// EmployeeCode
        /// </summary>
        public string EmployeeCode => _record.EmployeeCode;

        /// <summary>
        /// FullName
        /// </summary>
        public string FullName => _record.FullName;
        /// <summary>
        /// DepartmentName
        /// </summary>
        public string DepartmentName => cat_DepartmentServices.GetFieldValueById(_record.DepartmentId);

        #endregion
    }
}
