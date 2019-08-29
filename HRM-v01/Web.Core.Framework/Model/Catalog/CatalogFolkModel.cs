using System;
using Web.Core.Object.Catalog;

namespace Web.Core.Framework
{
    public class CatalogFolkModel : BaseModel<cat_Folk>
    {
        public CatalogFolkModel()
        {
            // init entity
            var catalog = new cat_Folk();

            // set field
            Init(catalog);
        }

        public CatalogFolkModel(cat_Folk catalog)
        {
            // init entity
            catalog = catalog ?? new cat_Folk();

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
        /// Minority
        /// </summary>
        public bool IsMinority { get; set; }

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

        /// <summary>
        /// Status name
        /// </summary>
        public string StatusName => Status.Description();
    }
}