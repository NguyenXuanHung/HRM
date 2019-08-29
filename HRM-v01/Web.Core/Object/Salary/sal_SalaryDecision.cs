using System;

namespace Web.Core.Object.Salary
{
    /// <summary>
    /// Quyết định lương
    /// </summary>
    public class sal_SalaryDecision : BaseEntity
    {
        public sal_SalaryDecision()
        {
            ContractId = 0;
            SignerName = string.Empty;
            SignerPositionId = 0;
            AttachFileName = string.Empty;
            Note = string.Empty;
            InsuranceSalary = 0;
            ContractSalary = 0;
            GrossSalary = 0;
            NetSalary = 0;
            PercentageSalary = 0;
            PercentageLeader = 0;
            PercentageOverGrade = 0;
            Type = SalaryDecisionType.Regular;
            Status = SalaryDecisionStatus.Pending;
            CreatedBy = "admin";
            CreatedDate = DateTime.Now;
            EditedBy = "admin";
            EditedDate = DateTime.Now;;
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
    }
}
