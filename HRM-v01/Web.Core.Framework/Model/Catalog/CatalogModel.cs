using System;
using Web.Core.Object.Catalog;

namespace Web.Core.Framework
{
    public class CatalogModel : BaseModel<cat_Base>
    {
        private readonly string _objName;

        public CatalogModel(string objName)
        {
            // init entity
            var catalog = new cat_Base();

            // init object name
            _objName = objName;

            // set field
            Init(catalog);
        }

        public CatalogModel(string objName, cat_Base catalog)
        {
            // init entity
            catalog = catalog ?? new cat_Base();

            // init object name
            _objName = objName;

            // set field
            Init(catalog);
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
        public string GroupName => EnumHelper.GetCatalogGroupName(_objName, Group);

        /// <summary>
        /// Status name
        /// </summary>
        public string StatusName => Status.Description();

        #endregion
    }
}