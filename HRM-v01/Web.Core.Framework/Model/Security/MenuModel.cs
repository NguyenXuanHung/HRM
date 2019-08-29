using System;
using Web.Core.Object.Security;
using Web.Core.Service.Security;

namespace Web.Core.Framework
{
    public class MenuModel : BaseModel<Menu>
    {
        public MenuModel()
        {
            // init entity
            var menu = new Menu();

            // set entity props
            Init(menu);

            // set custom model props
            DisplayName = menu.MenuName;
        }

        public MenuModel(Menu menu)
        {
            // init entity
            menu = menu ?? new Menu();

            // set entity props
            Init(menu);

            // set custom model props
            DisplayName = menu.MenuName;
        }

        /// <summary>
        /// Menu name
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// Display name
        /// </summary>
        public string DisplayName { get; set; }

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
        /// Parent menu name
        /// </summary>
        public string ParentName
        {
            get
            {
                var parentMenu = MenuServices.GetById(ParentId);
                return parentMenu != null ? parentMenu.MenuName : string.Empty;
            }

        }

        /// <summary>
        /// Order
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Menu group: Dashboard | MenuLeft | MenuTop
        /// </summary>
        public MenuGroup Group { get; set; }

        /// <summary>
        /// Menu group name
        /// </summary>
        public string GroupName => Group.Description();

        /// <summary>
        /// Show menu in panel or not
        /// </summary>
        public bool IsPanel { get; set; }

        /// <summary>
        /// Menu status
        /// </summary>
        public MenuStatus Status { get; set; }

        /// <summary>
        /// Menu status name
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
