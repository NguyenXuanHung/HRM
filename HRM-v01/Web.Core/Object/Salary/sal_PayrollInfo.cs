using System;

namespace Web.Core.Object.Salary
{
    /// <summary>
    /// Payroll info
    /// </summary>
    public class sal_PayrollInfo : BaseEntity
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
    }
}
