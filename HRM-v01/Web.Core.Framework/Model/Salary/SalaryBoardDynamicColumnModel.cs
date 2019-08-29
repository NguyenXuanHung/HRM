using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework.Model
{
    public class SalaryBoardDynamicColumnModel : BaseModel<hr_SalaryBoardDynamicColumn>
    {
        private readonly hr_SalaryBoardDynamicColumn _salaryBoardDynamicColumn;
        private readonly hr_Record _record;

        public SalaryBoardDynamicColumnModel()
        {
            _salaryBoardDynamicColumn = new hr_SalaryBoardDynamicColumn();
            _record = new hr_Record();

            Init(_salaryBoardDynamicColumn);
        }

        public SalaryBoardDynamicColumnModel(hr_SalaryBoardDynamicColumn salaryBoardDynamicColumn)
        {
            _salaryBoardDynamicColumn = salaryBoardDynamicColumn ?? new hr_SalaryBoardDynamicColumn();
            _record = hr_RecordServices.GetById(_salaryBoardDynamicColumn.RecordId) ?? new hr_Record();

            FullName = _record.FullName;
            EmployeeCode = _record.EmployeeCode;
            DepartmentName = cat_DepartmentServices.GetFieldValueById(_record.DepartmentId);

            Init(salaryBoardDynamicColumn);
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

        #region Custom properties

        /// <summary>
        /// Họ tên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string DepartmentName { get; set; }
        #endregion
    }
}
