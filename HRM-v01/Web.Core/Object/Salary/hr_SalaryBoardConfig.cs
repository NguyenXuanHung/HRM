using System;

namespace Web.Core.Object.HumanRecord
{
    /// <summary>
    /// Cấu hình bảng lương
    /// </summary>
    public class hr_SalaryBoardConfig : BaseEntity
    {
        /// <summary>
        /// Id cấu hình bảng lương
        /// </summary>
        public int ConfigId { get; set; }

        /// <summary>
        /// Mã cột
        /// </summary>
        public string ColumnCode { get; set; }
        /// <summary>
        /// Tên cột
        /// </summary>
        public string Display { get; set; } 
        /// <summary>
        /// Công thức
        /// </summary>
        public string Formula { get; set; }
        
        /// <summary>
        /// Cột tương ứng Excel
        /// </summary>
        public string ColumnExcel { get; set; }

        /// <summary>
        /// Thứ tự sắp xếp
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Có hiển thị trên bảng lương không
        /// </summary>
        public bool IsInUsed { get; set; }
        /// <summary>
        /// Chỉ đọc
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Loại dữ liệu
        /// </summary>
        public SalaryConfigDataType DataType { get; set; }

        /// <summary>
        /// Mô tả công thức
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Cho phép xóa không
        /// </summary>
        public bool IsDisable { get; set; }

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
