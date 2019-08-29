using System;

namespace Web.Core.Object.Security
{
    public class Menu : BaseEntity
    {
        public Menu()
        {
            MenuName = "";
            TabName = "";
            LinkUrl = "";
            Icon = "";
            ParentId = 0;
            Order = 0;
            IsPanel = false;
            Group = MenuGroup.MenuLeft;
            Status = MenuStatus.Active;
            IsDeleted = false;
            CreatedBy = "system";
            CreatedDate = DateTime.Now;
            EditedBy = "system";
            EditedDate = DateTime.Now;
        }

        /// <summary>
        /// Menu name
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// Tab name
        /// </summary>
        public string TabName { get; set; }

        /// <summary>
        /// Link url
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Parent menu
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Order
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Show menu in panel or not
        /// </summary>
        public bool IsPanel { get; set; }

        /// <summary>
        /// Menu group: Dashboard | MenuLeft | MenuTop
        /// </summary>
        public MenuGroup Group { get; set; }

        /// <summary>
        /// Menu status
        /// </summary>
        public MenuStatus Status { get; set; }

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
