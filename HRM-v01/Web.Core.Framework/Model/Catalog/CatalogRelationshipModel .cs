using System;
using Web.Core.Object.Catalog;

namespace Web.Core.Framework
{
    public class CatalogRelationshipModel: BaseModel<cat_Relationship>
    {
        private readonly cat_Relationship _catRelationship;
        
        public CatalogRelationshipModel()
        {
           // init entity
           _catRelationship = new cat_Relationship();

            // set props
            Init(_catRelationship);
        }

        public CatalogRelationshipModel(cat_Relationship _catRelationship)
        {
            _catRelationship = _catRelationship ?? new cat_Relationship();
            Init(_catRelationship);
        }

        /// <summary>
        /// name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasHusband { get; set; }

        /// <summary>
        /// order
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// group
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Report status
        /// </summary>
        public CatalogStatus Status { get; set; }

        /// <summary>
        /// relationship  status name
        /// </summary>
        public string StatusName => Status.Description();

        /// <summary>
        /// Deleted status
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// System, created user
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// System, created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// System, edited user
        /// </summary>
        public string EditedBy { get; set; }

        /// <summary>
        /// System, edited date
        /// </summary>
        public DateTime EditedDate { get; set; }
    }
}