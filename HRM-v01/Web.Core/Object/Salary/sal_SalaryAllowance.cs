using System;

namespace Web.Core.Object.Salary
{
    /// <summary>
    /// Phụ cấp cho quyết định lương
    /// </summary>
    public class sal_SalaryAllowance : BaseEntity
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        public sal_SalaryAllowance()
        {
            SalaryDecisionId = 0;
            AllowanceCode = "";
            Factor = 0;
            Percent = 0;
            Value = 0;
            CreatedBy = "admin";
            CreatedDate = DateTime.Now;
            EditedBy = "admin";
            EditedDate = DateTime.Now;
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
    }
}
