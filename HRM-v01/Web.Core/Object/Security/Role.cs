using System;

namespace Web.Core.Object.Security
{
    public class Role : BaseEntity
    {
        public Role()
        {
            RoleName = "";
            Description = "";
            Order = 0;
            IsDeleted = false;
            CreatedBy = @"admin";
            CreatedDate = DateTime.Now;
            EditedBy = @"admin";
            EditedDate = DateTime.Now;
        }

        /// <summary>
        /// Role name
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Priority
        /// </summary>
        public int Order { get; set; }
        
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
