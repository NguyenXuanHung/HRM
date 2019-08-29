using System;
using Web.Core.Object.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for SalaryBoardConfigModel
    /// </summary>
    public class SalaryBoardConfigModel : BaseModel<hr_SalaryBoardConfig>
    {
        public SalaryBoardConfigModel()
        {
            var salaryBoardConfig = new hr_SalaryBoardConfig();

            Init(salaryBoardConfig);
        }

        public SalaryBoardConfigModel(hr_SalaryBoardConfig boardConfig)
        {
            boardConfig = boardConfig ?? new hr_SalaryBoardConfig();

            Init(boardConfig);
        }
        
        /// <summary>
        /// Id cấu hình bảng lương
        /// </summary>
        public int ConfigId { get; set; }

        /// <summary>
        /// Mã cột
        /// </summary>
        public string ColumnCode { get; set; }

        /// <summary>
        /// Thứ tự sắp xếp
        /// </summary>
        public int Order { get; set; }

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
        /// Có hiển thị trên bảng lương không
        /// </summary>
        public bool IsInUsed { get; set; }

        /// <summary>
        /// Chỉ đọc
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Cho phép xóa không
        /// </summary>
        public bool IsDisable { get; set; }

        /// <summary>
        /// Mô tả công thức
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Loại dữ liệu
        /// </summary>
        public SalaryConfigDataType DataType { get; set; }

        /// <summary>
        /// Data type name
        /// </summary>
        public string DataTypeName => DataType.Description();

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
