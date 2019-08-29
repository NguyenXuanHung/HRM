using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Object.Catalog;

namespace Web.Core.Framework
{
    public class CatalogAllowanceModel : BaseModel<cat_Allowance>
    {
        public CatalogAllowanceModel()
        {
            // init entity
            var entity = new cat_Allowance();

            // set field
            Init(entity);
        }

        public CatalogAllowanceModel(cat_Allowance entity)
        {
            // init entity
            entity = entity ?? new cat_Allowance();

            // set field
            Init(entity);
        }

        /// <summary>
        /// Category code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Category description
        /// </summary>
        public string Description { get; set; }

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

        /// <summary>
        /// Category group
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Priority
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Category status
        /// </summary>
        public CatalogStatus Status { get; set; }

        /// <summary>
        /// True if object was deleted
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

        #region Custom Props

        /// <summary>
        /// Value type name
        /// </summary>
        public string ValueTypeName => ValueType.Description();

        /// <summary>
        /// Type name
        /// </summary>
        public string TypeName => string.IsNullOrEmpty(Type)
            ? string.Empty
            : ((AllowanceType)Enum.Parse(typeof(AllowanceType), Type)).Description();

        /// <summary>
        /// Group name
        /// </summary>
        public string GroupName => string.IsNullOrEmpty(Group)
            ? string.Empty
            : ((CatalogGroupAllowance) Enum.Parse(typeof(CatalogGroupAllowance), Group)).Description();

        /// <summary>
        /// Status name
        /// </summary>
        public string StatusName => Status.Description();

        #endregion
    }
}
