using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.Object.HumanRecord
{
    public class hr_SalaryBoardDynamicColumn : BaseEntity
    {
        public hr_SalaryBoardDynamicColumn()
        {
            IsInUsed = true;
            CreatedDate = DateTime.Now;
        }

        /// <summary>
        /// Id nhân viên
        /// </summary>
        public int RecordId { get; set; }
        
        /// <summary>
        /// Id bảng lương
        /// </summary>
        public int SalaryBoardId { get; set; }

        /// <summary>
        /// Tên cột
        /// </summary>
        public string ColumnCode { get; set; }

        /// <summary>
        /// Tên cột
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// Tên cột trong excel
        /// </summary>
        public string ColumnExcel { get; set; }

        /// <summary>
        /// Có đang được sử dụng?
        /// </summary>
        public bool IsInUsed { get; set; }

        /// <summary>
        /// Giá trị ô
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
