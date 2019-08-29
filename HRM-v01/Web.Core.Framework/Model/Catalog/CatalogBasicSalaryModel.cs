using System;
using Web.Core.Object.Catalog;
using Web.Core.Object.Salary;

namespace Web.Core.Framework
{
    /// <summary>
    /// Lương cơ bản
    /// </summary>
    public class CatalogBasicSalaryModel : BaseModel<cat_BasicSalary>
    {
        /// <summary>
        /// Ctors default
        /// </summary>
        public CatalogBasicSalaryModel()
        {
            // init entity
            var entity = new cat_BasicSalary();

            // set field
            Init(entity);
        }

        /// <summary>
        /// Ctors with initial value
        /// </summary>
        /// <param name="entity"></param>
        public CatalogBasicSalaryModel(cat_BasicSalary entity)
        {
            // init entity
            entity = entity ?? new cat_BasicSalary();

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
        /// Mức lương cơ bản
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Ngày áp dụng
        /// </summary>
        public DateTime AppliedDate { get; set; }

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
        /// Created by, default system user
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Edited by, default system user
        /// </summary>
        public string EditedBy { get; set; }

        /// <summary>
        /// Edited date
        /// </summary>
        public DateTime EditedDate { get; set; }

        #region Custom properties

        /// <summary>
        /// Status name
        /// </summary>
        public string StatusName => Status.Description();

        #endregion
    }
}