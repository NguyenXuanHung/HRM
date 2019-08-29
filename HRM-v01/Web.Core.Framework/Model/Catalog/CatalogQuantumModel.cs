using System;
using Web.Core.Object.Catalog;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework
{
    public class CatalogQuantumModel : BaseModel<cat_Quantum>
    {
        public CatalogQuantumModel()
        {
            // init entity
            var entity = new cat_Quantum();

            // set field
            Init(entity);
        }

        public CatalogQuantumModel(cat_Quantum entity)
        {
            // init entity
            entity = entity ?? new cat_Quantum();

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
        public string GroupQuantumName => cat_GroupQuantumServices.GetFieldValueById(GroupQuantumId);

        /// <summary>
        /// Tên trạng thái
        /// </summary>
        public string StatusName => Status.Description();

        #endregion
    }
}