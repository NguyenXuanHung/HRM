using System;
using Web.Core.Object.Catalog;
using Web.Core.Object.HumanRecord;
using Web.Core.Object.Salary;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for SalaryDecisionModel
    /// </summary>
    public class SalaryDecisionModel : BaseModel<sal_SalaryDecision>
    {
        private readonly hr_Record _record;
        private readonly cat_ContractType _contractType;
        private readonly cat_GroupQuantum _groupQuantum;
        private readonly cat_Quantum _quantum;
        private readonly cat_BasicSalary _basicSalary;
        private readonly cat_Position _position;

        public SalaryDecisionModel()
        {
            // init default entity
            var entity = new sal_SalaryDecision();
            _record = new hr_Record();
            _contractType = new cat_ContractType();
            _groupQuantum = new cat_GroupQuantum();
            _quantum = new cat_Quantum();
            _basicSalary = new cat_BasicSalary();
            _position = new cat_Position();

            // set default model props
            Init(entity);
        }

        public SalaryDecisionModel(sal_SalaryDecision entity)
        {
            // init salary decision
            entity = entity ?? new sal_SalaryDecision();

            // init record
            _record = hr_RecordServices.GetById(entity.RecordId) ?? new hr_Record();

            // init contract type
            var contract = hr_ContractServices.GetById(entity.ContractId) ?? new hr_Contract();
            _contractType = cat_ContractTypeServices.GetById(contract.ContractTypeId) ?? new cat_ContractType();

            // init group quantum
            _groupQuantum = cat_GroupQuantumServices.GetById(entity.GroupQuantumId) ?? new cat_GroupQuantum();

            // init quantum
            _quantum = cat_QuantumServices.GetById(entity.QuantumId) ?? new cat_Quantum();

            // basic salary
            _basicSalary = cat_BasicSalaryServices.GetCurrent() ?? new cat_BasicSalary();

            //init position
            _position = cat_PositionServices.GetById(entity.SignerPositionId) ?? new cat_Position();
            
            // set model props
            Init(entity);

            // set custom props
            DecisionVnDate = DecisionDate.ToVnDate();
            EffectiveVnDate = EffectiveDate.ToVnDate();
        }

        /// <summary>
        /// Mã hồ sơ
        /// </summary>        
        public int RecordId { get; set; }

        /// <summary>
        /// Mã hợp đồng
        /// </summary>
        public int ContractId { get; set; }

        /// <summary>
        /// Tên quyết định
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Số quyết định
        /// </summary>
        public string DecisionNumber { get; set; }

        /// <summary>
        /// Ngày quyết định
        /// </summary>
        public DateTime DecisionDate { get; set; }

        /// <summary>
        /// Ngày hiệu lực
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// Tên người ký
        /// </summary>
        public string SignerName { get; set; }
        
        /// <summary>
        /// id chức vụ người ký
        /// </summary>
        public int SignerPositionId { get; set; }

        /// <summary>
        /// Tệp đính kèm
        /// </summary>
        public string AttachFileName { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Mã ngạch
        /// </summary>
        public int GroupQuantumId { get; set; }

        /// <summary>
        /// Mã ngạch
        /// </summary>
        public int QuantumId { get; set; }

        /// <summary>
        /// Bậc
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// Hệ số lương
        /// </summary>
        public decimal Factor { get; set; }

        /// <summary>
        /// Lương bảo hiểm
        /// </summary>
        public decimal InsuranceSalary { get; set; }

        /// <summary>
        /// Lương hợp đồng
        /// </summary>
        public decimal ContractSalary { get; set; }

        /// <summary>
        /// Lương tổng
        /// </summary>
        public decimal GrossSalary { get; set; }

        /// <summary>
        /// Lương thực lĩnh
        /// </summary>
        public decimal NetSalary { get; set; }

        /// <summary>
        /// Phần trăm hưởng lương
        /// </summary>
        public decimal PercentageSalary { get; set; }

        /// <summary>
        /// Phụ cấp lãnh đạo
        /// </summary>
        public decimal PercentageLeader { get; set; }

        /// <summary>
        /// Phần trăm vượt khung
        /// </summary>
        public decimal PercentageOverGrade { get; set; }

        /// <summary>
        /// Ngày nâng lương tiếp theo
        /// </summary>
        public DateTime? NextRaiseDate { get; set; }

        /// <summary>
        /// Loại nâng lương:vượt khung, thường xuyên, trước thời hạn
        /// </summary>
        public SalaryDecisionType Type { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public SalaryDecisionStatus Status { get; set; }

        /// <summary>
        /// Trạng thái xóa
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        public string EditedBy { get; set; }

        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime EditedDate { get; set; }

        #region Custom Props

        public string EmployeeCode => _record.EmployeeCode;

        public string EmployeeName => _record.FullName;

        public string EmployeeSex => _record.Sex ? "Nam" : "Nữ";

        public string EmployeeBirthDate => _record.BirthDate != null ? _record.BirthDate.Value.ToVnDate() : "";

        public string DecisionVnDate { get; set; }

        public string EffectiveVnDate { get; set; }

        public string ContractTypeName => _contractType.Name;

        public string GroupQuantumName => _groupQuantum.Name;

        public string QuantumName => _quantum.Name;

        public string QuantumCode => _quantum.Code;

        public string TypeName => Type.Description();

        public string StatusName => Status.Description();

        public string DepartmentName => cat_DepartmentServices.GetFieldValueById(_record.DepartmentId);

        public string PositionName => cat_PositionServices.GetFieldValueById(_record.PositionId);

        public string JobTitleName => cat_JobTitleServices.GetFieldValueById(_record.JobTitleId);
        
        public decimal BasicSalary => _basicSalary.Value;

        public decimal SalaryWithoutAllowance => _basicSalary.Value * Factor * PercentageSalary / 100;

        public decimal Salary => _basicSalary.Value * Factor * (1 + (PercentageLeader / 100) + (PercentageOverGrade / 100)) * PercentageSalary / 100;

        public string SignerPosition => _position.Name;

        #endregion
    }
}

