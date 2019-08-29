using System;

namespace Web.Core.Object.TimeSheet
{
    /// <inheritdoc />
    /// <summary>
    /// Danh mục nhóm ký hiệu chấm công
    /// </summary>
    public class hr_TimeSheetGroupSymbol : BaseEntity
    {
        public hr_TimeSheetGroupSymbol()
        {
            Code = "";
            Name = "";
            Description = "";
            Group = "";
            Order = 0;
            Status = TimeSheetStatus.Active;
            IsDeleted = false;
            CreatedBy = "system";
            CreatedDate = DateTime.Now;
            EditedBy = "system";
            EditedDate = DateTime.Now;
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
        public TimeSheetStatus Status { get; set; }

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
