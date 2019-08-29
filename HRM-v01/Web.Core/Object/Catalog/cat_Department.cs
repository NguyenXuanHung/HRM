using System.ComponentModel;

namespace Web.Core.Object.Catalog
{
    /// <inheritdoc />
    /// <summary>
    /// Danh mục đơn vị, phòng, ban
    /// </summary>
    public class cat_Department : cat_Base
    {
        /// <summary>
        /// Tên viết tắt
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Điện thoại
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// Số fax
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string TaxCode { get; set; }

        /// <summary>
        /// Số tài khoản
        /// </summary>
        public string BankAccount { get; set; }

        /// <summary>
        /// Ngân hàng
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Người đại diện pháp luật (giám đốc, tổng giám đốc, chủ tịch HDQT, ...)
        /// </summary>
        public string Director { get; set; }

        /// <summary>
        /// Kế toán trưởng
        /// </summary>
        public string ChiefAccountant { get; set; }

        /// <summary>
        /// Mã đơn vị cấp trên
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Loại đơn vị (Công ty - Tổ chức | Ban | Phòng)
        /// </summary>
        public DepartmentType Type { get; set; }

        /// <summary>
        /// Là đơn vị quản lý (cấp cao nhất)
        /// </summary>
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Bị khóa (dùng cho các đơn vị ảo được tạo ra để nhóm các đơn vị cấp dưới, không phải đơn vị thật)
        /// </summary>
        public bool IsLocked { get; set; }
    }

    public enum DepartmentType
    {
        [Description("Tổ chức")]
        Organization = 1,
        [Description("Ban")]
        Board = 2,
        [Description("Phòng")]
        Department = 3
    }
}
