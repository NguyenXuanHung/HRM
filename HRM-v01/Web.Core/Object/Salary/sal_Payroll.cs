using System;

namespace Web.Core.Object.Salary
{
    /// <summary>
    /// Payroll
    /// </summary>
    public class sal_Payroll : BaseEntity
    {
        public sal_Payroll()
        {
            ConfigId = 0;
            Code = "";
            Title = "";
            Description = "";
            Data = "";
            Month = DateTime.Now.Month;
            Year = DateTime.Now.Year;
            Status = PayrollStatus.Active;
            IsDeleted = false;
        }

        /// <summary>
        /// Payroll config id
        /// </summary>
        public int ConfigId { get; set; }
        
        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Department ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// JSON data
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Month
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Payroll status
        /// </summary>
        public PayrollStatus Status { get; set; }

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
