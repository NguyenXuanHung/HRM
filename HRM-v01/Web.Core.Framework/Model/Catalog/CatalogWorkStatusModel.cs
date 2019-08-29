using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Object.Catalog;

namespace Web.Core.Framework
{
    public class CatalogWorkStatusModel : BaseModel<cat_WorkStatus>
    {
        private readonly cat_WorkStatus _catWorkStatus;

        public CatalogWorkStatusModel()
        {
            // init entity
            _catWorkStatus = new cat_WorkStatus();

            // set props
            Init(_catWorkStatus);
        }

        public CatalogWorkStatusModel(cat_WorkStatus catWorkStatus)
        {
            // init entity
            _catWorkStatus = catWorkStatus ?? new cat_WorkStatus();

            // set props
            Init(_catWorkStatus);
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
        /// Category order
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
    }
}
