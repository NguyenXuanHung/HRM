using System;
using Web.Core.Object.Catalog;

namespace Web.Core.Framework
{
    public class CatalogGroupQuantumModel : BaseModel<cat_GroupQuantum>
    {
        public CatalogGroupQuantumModel()
        {
            // init entity
            var entity = new cat_GroupQuantum();

            // set field
            Init(entity);
        }

        public CatalogGroupQuantumModel(cat_GroupQuantum entity)
        {
            // init entity
            entity = entity ?? new cat_GroupQuantum();

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
        /// Số bậc tối đa
        /// </summary>
        public int GradeMax { get; set; }

        /// <summary>
        /// Số tháng nâng lương
        /// </summary>
        public int MonthStep { get; set; }

        /// <summary>
        /// % vượt khung
        /// </summary>
        public decimal PercentageOverGrade { get; set; }

        /// <summary>
        /// % vượt khung tăng
        /// </summary>
        public decimal PercentageOverGradeStep { get; set; }

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
        /// Tên nhóm
        /// </summary>
        public string GroupName => string.IsNullOrEmpty(Group) ? string.Empty : ((CatalogGroupGroupQuantum) Enum.Parse(typeof(CatalogGroupGroupQuantum), Group)).Description();

        /// <summary>
        /// Tên trạng thái
        /// </summary>
        public string StatusName => Status.Description();

        #endregion
    }
}