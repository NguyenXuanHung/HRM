using System;
using Web.Core.Object.Catalog;

namespace Web.Core.Framework
{
    public class CatalogContractTypeModel : BaseModel<cat_ContractType>
    {
        public CatalogContractTypeModel()
        {
            // init entity
            var entity = new cat_ContractType();

            // set field
            Init(entity);
        }

        public CatalogContractTypeModel(cat_ContractType entity)
        {
            // init entity
            entity = entity ?? new cat_ContractType();

            // set field
            Init(entity);
        }

        /// <summary>
        /// Category code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Thời hạn hợp đồng
        /// </summary>
        public int ContractMonth { get; set; }

        /// <summary>
        /// Hệ số hợp đồng
        /// </summary>
        public double ContractFactor { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Category description
        /// </summary>
        public string Description { get; set; }

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

        #region Custom props

        /// <summary>
        /// Group name
        /// </summary>   
        public string GroupName => !string.IsNullOrEmpty(Group) 
            ? ((CatalogGroupContractType)Enum.Parse(typeof(CatalogGroupContractType), Group)).Description() 
            : string.Empty;

        /// <summary>
        /// Status name
        /// </summary>
        public string StatusName => Status.Description();

        #endregion
    }
}