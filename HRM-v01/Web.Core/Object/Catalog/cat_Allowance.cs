using System;

namespace Web.Core.Object.Catalog
{
    /// <inheritdoc />
    /// <summary>
    /// Danh mục phụ cấp
    /// </summary>
    public class cat_Allowance : cat_Base
    {
        public cat_Allowance()
        {
            Code = AllowanceType.PhuCapChucVu.ToString();
            Name = AllowanceType.PhuCapChucVu.Description();
            Description = "";
            Value = 0;
            Formula = "";
            ValueType = AllowanceValueType.FixValue;
            Type = AllowanceType.PhuCapChucVu.ToString();
            Taxable = false;
            Group = CatalogGroupAllowance.PhuCapTinhTheoHeSo.ToString();
            Order = 0;
            Status = CatalogStatus.Active;
            IsDeleted = false;
            CreatedBy = "admin";
            CreatedDate = DateTime.Now;
            EditedBy = "admin";
            EditedDate = DateTime.Now;
        }

        /// <summary>
        /// Giá trị
        /// </summary>
        public decimal Value { get; set; }
        
        /// <summary>
        /// Công thức
        /// </summary>
        public string Formula { get; set; }

        /// <summary>
        /// Kiểu giá trị
        /// </summary>
        public AllowanceValueType ValueType { get; set; }

        /// <summary>
        /// Kiểu phụ cấp
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Có tính thuế thu nhập cá nhân
        /// </summary>
        public bool Taxable { get; set; }
    }
}
