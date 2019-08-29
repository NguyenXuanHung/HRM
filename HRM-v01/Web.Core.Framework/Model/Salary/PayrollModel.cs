using System;
using Web.Core.Object.Salary;
using Web.Core.Service.Catalog;
using Web.Core.Service.Salary;

namespace Web.Core.Framework
{
    public class PayrollModel : BaseModel<sal_Payroll>
    {
        private readonly sal_Payroll _payroll;
        public PayrollModel()
        {
            // init payroll value
            _payroll = new sal_Payroll();

            // set model props
            Init(_payroll);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="payroll"></param>
        public PayrollModel(sal_Payroll payroll)
        {
            // init payroll value
            _payroll = payroll ?? new sal_Payroll();

            // set model props
            Init(_payroll);
        }

        /// <summary>
        /// Payroll config id
        /// </summary>
        public int ConfigId { get; set; }

        /// <summary>
        /// Payroll config name
        /// </summary>
        public string ConfigName
        {
            get
            {
                // get payroll config
                var payrollConfig = sal_PayrollConfigServices.GetById(ConfigId);

                // return
                return payrollConfig != null ? payrollConfig.Name : string.Empty;
            }
        }
        
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
        /// Department name
        /// </summary>
        public string DepartmentName
        {
            get
            {
                // get department
                var department = cat_DepartmentServices.GetById(DepartmentId);
                // return
                return department != null ? department.Name : string.Empty;
            }
        }

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
        /// Payroll status name
        /// </summary>
        public string StatusName => Status.Description();

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
