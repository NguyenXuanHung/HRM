using System;
using Web.Core.Object.Catalog;
using Web.Core.Object.Salary;
using Web.Core.Service.Catalog;
using Web.Core.Service.Salary;

namespace Web.Core.Framework
{
    public class SalaryAllowanceModel : BaseModel<sal_SalaryAllowance>
    {
        private readonly sal_SalaryDecision _salaryDecision;
        private readonly cat_BasicSalary _basicSalary;
        private readonly cat_Allowance _allowance;

        public SalaryAllowanceModel()
        {
            // init entity
            _salaryDecision = new sal_SalaryDecision();
            _basicSalary = new cat_BasicSalary();
            _allowance = new cat_Allowance();

            // set default props for model
            Init(new sal_SalaryAllowance());
        }

        public SalaryAllowanceModel(sal_SalaryAllowance entity)
        {
            // init entity
            entity = entity ?? new sal_SalaryAllowance();
            _salaryDecision = sal_SalaryDecisionServices.GetById(entity.SalaryDecisionId) ?? new sal_SalaryDecision();
            _basicSalary = cat_BasicSalaryServices.GetCurrent() ?? new cat_BasicSalary();
            _allowance = cat_AllowanceServices.GetByCode(entity.AllowanceCode) ?? new cat_Allowance();

            // set custom props
            AllowanceName = _allowance.Name;

            // set model props
            Init(entity);
        }

        /// <summary>
        /// Mã quyết định lương
        /// </summary>
        public int SalaryDecisionId { get; set; }

        /// <summary>
        /// Mã phụ cấp
        /// </summary>
        public string AllowanceCode { get; set; }

        /// <summary>
        /// Hệ số lương (trường hợp FixedFactor hoặc FormulaFactor)
        /// </summary>
        public decimal Factor { get; set; }

        /// <summary> 
        /// Phần trăm lương (trường hợp FixedPercent hoặc FormulaPercent)
        /// </summary>
        public decimal Percent { get; set; }

        /// <summary>
        /// Giá trị phụ cấp (trường hợp loại FixedValue)
        /// </summary>
        public decimal Value { get; set; }
        
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        public string EditedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime EditedDate { get; set; }

        #region Custom props

        public string AllowanceName { get; set; } 

        public int SalaryGrade => _salaryDecision.Grade;

        public decimal SalaryFactor => _salaryDecision.Factor;

        public decimal BasicSalary => _basicSalary.Value;

        public decimal InsuranceSalary => _salaryDecision.InsuranceSalary;

        public decimal ContractSalary => _salaryDecision.ContractSalary;

        public decimal GrossSalary => _salaryDecision.GrossSalary;

        public decimal NetSalary => _salaryDecision.NetSalary;

        public decimal PercentageSalary => _salaryDecision.PercentageSalary;

        public decimal PercentageOverGrade => _salaryDecision.PercentageOverGrade;

        #endregion
    }
}
