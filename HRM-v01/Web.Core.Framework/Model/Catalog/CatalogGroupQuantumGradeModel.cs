using System;
using Web.Core.Object.Catalog;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework
{
    public class CatalogGroupQuantumGradeModel : BaseModel<cat_GroupQuantumGrade>
    {
        private readonly cat_BasicSalary _basicSalary;
        private readonly cat_GroupQuantum _groupQuantum;

        public CatalogGroupQuantumGradeModel()
        {
            // init entity
            var entity = new cat_GroupQuantumGrade();
            _basicSalary = new cat_BasicSalary();
            _groupQuantum = new cat_GroupQuantum();

            // set field
            Init(entity);
        }

        public CatalogGroupQuantumGradeModel(cat_GroupQuantumGrade entity)
        {
            // init entity
            entity = entity ?? new cat_GroupQuantumGrade();

            // basic salary
            _basicSalary = cat_BasicSalaryServices.GetCurrent() ?? new cat_BasicSalary();

            // group quantum
            _groupQuantum = cat_GroupQuantumServices.GetById(entity.GroupQuantumId) ?? new cat_GroupQuantum();

            // set field
            Init(entity);
        }

        /// <summary>
        /// Mã
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Mã nhóm ngạch
        /// </summary>
        public int GroupQuantumId { get; set; }

        /// <summary>
        /// Số tháng nâng lương
        /// </summary>
        public int MonthStep { get; set; }

        /// <summary>
        /// Bậc
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// Hệ số
        /// </summary>
        public decimal Factor { get; set; }

        /// <summary>
        /// Nhóm
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Thứ tự
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public CatalogStatus Status { get; set; }

        /// <summary>
        /// Trạng thái xóa
        /// </summary>
        public bool IsDeleted { get; set; }

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

        #region Custom Props

        /// <summary>
        /// Tên nhóm ngạch
        /// </summary>
        public string GroupQuantumName => _groupQuantum.Name;

        /// <summary>
        /// Số tháng lên lương
        /// </summary>
        public int GroupQuantumMonthStep => _groupQuantum.MonthStep;

        /// <summary>
        /// Số bậc tối đa
        /// </summary>
        public int GroupQuantumGradeMax => _groupQuantum.GradeMax;

        /// <summary>
        /// Lương
        /// </summary>
        public decimal Salary => Factor * _basicSalary.Value;
        
        /// <summary>
        /// Tên trạng thái
        /// </summary>
        public string StatusName => Status.Description();

        #endregion
    }
}