using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Object.Security;
using Web.Core.Service.Security;
using Menu = Web.Core.Object.Security.Menu;

namespace Web.HRM.Modules.Setting
{
    public partial class RoleManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!ExtNet.IsAjaxRequest)
            {
                LoadMenu();
            }
        }

        #region Event Methods

        /// <summary>
        /// Init setting window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindow(object sender, DirectEventArgs e)
        {
            try
            {
                // init id
                var param = e.ExtraParams["Id"];
                // parse id
                if(int.TryParse(param, out var id))
                {
                    // init window props
                    if(id > 0)
                    {
                        // edit
                        wdSetting.Title = @"Sửa";
                        wdSetting.Icon = Icon.Pencil;
                    }
                    else
                    {
                        // insert
                        wdSetting.Title = @"Thêm mới";
                        wdSetting.Icon = Icon.Add;
                    }
                    // init id
                    hdfRoleId.Text = id.ToString();
                    // init object
                    var entity = new Role
                    {
                        RoleName = "",
                        Description = "",
                        Order = 0,
                        IsDeleted = false,
                        CreatedBy = CurrentUser.User.UserName,
                        CreatedDate = DateTime.Now,
                        EditedBy = CurrentUser.User.UserName,
                        EditedDate = DateTime.Now
                    };
                    // check id
                    if(id > 0)
                    {
                        var result = RoleServices.GetById(id);
                        if(result != null)
                        {
                            entity = result;
                            entity.EditedBy = CurrentUser.User.UserName;
                            entity.EditedDate = DateTime.Now;
                        }

                    }
                    // set role props
                    // name
                    txtName.Text = entity.RoleName;
                    // description
                    txtDescription.Text = entity.Description;
                    // show window
                    wdSetting.Show();
                }
            }
            catch (Exception ex)
            {
                Dialog.Alert("Exception", ex.Message);
            }
            
        }

        /// <summary>
        /// Insert or Update Role
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                // init entity
                var entity = new Role();
                // check id
                if(!string.IsNullOrEmpty(hdfRoleId.Text) && Convert.ToInt32(hdfRoleId.Text) > 0)
                {
                    var result = RoleServices.GetById(Convert.ToInt32(hdfRoleId.Text)); ;
                    if(result != null)
                        entity = result;
                }
                // set new props for entity
                entity.RoleName = txtName.Text;
                entity.Description = txtDescription.Text;
                // check entity id
                if(entity.Id > 0)
                {
                    // update
                    RoleServices.Update(entity);
                }
                else
                {
                    // insert
                    RoleServices.Create(entity);
                }
                // hide window
                wdSetting.Hide();
                // reload data
                gpRole.Reload();
            }
            catch(Exception ex)
            {
                Dialog.Alert("Exception", ex.Message);
            }
        }

        /// <summary>
        /// Delete catalog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete(object sender, DirectEventArgs e)
        {
            // init id
            var param = e.ExtraParams["Id"];
            // parse id
            if(!int.TryParse(param, out var id) || id <= 0)
            {
                // parse error, show error
                Dialog.ShowError("Có lỗi xảy ra trong quá trình xử lý");
                return;
            }
            // delete
            RoleServices.DeleteTemporary(id);
            // reload data
            gpRole.Reload();
        }

        #endregion

        #region DirectMethod

        /// <summary>
        /// Load menu for role
        /// </summary>
        [DirectMethod]
        public void LoadMenuPermissionForRole()
        {
            if(!string.IsNullOrEmpty(hdfRoleId.Text) && int.TryParse(hdfRoleId.Text, out var roleId) && roleId > 0)
            {
                var lstMenuRole = MenuRoleServices.GetAll(roleId, null, null, null);
                hdfMenuRole.Text = new JavaScriptSerializer().Serialize(lstMenuRole);
                RM.RegisterClientScriptBlock("LoadMenuPermission", "loadMenuPermission();");
            }
            else
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình tải dữ liệu");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void SaveMenuPermissionForRole()
        {
            // get role id
            if(!string.IsNullOrEmpty(hdfRoleId.Text) && int.TryParse(hdfRoleId.Text, out var roleId) && roleId > 0)
            {
                // get new list of menus for role
                var lstMenuRole = new JavaScriptSerializer().Deserialize<List<MenuRole>>(hdfMenuRole.Text);
                // delete old menus for role
                MenuRoleServices.Delete(roleId, null);
                // insert new menus for role
                foreach(var menuRole in lstMenuRole)
                {
                    MenuRoleServices.Create(menuRole);
                }
                Dialog.ShowNotification("Lưu dữ liệu thành công");
            }
            else
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xử lý");
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Load root menu
        /// </summary>
        private void LoadMenu()
        {
            const int level = 0;

            var lstAllMenu = MenuController.GetAll(null, null, null, null, MenuStatus.Active, null, false, null, null);

            var rootMenus = lstAllMenu.Where(m => m.ParentId == 0 && m.Group == MenuGroup.MenuLeft).OrderBy(m => m.Order).ToList();
            rootMenus.AddRange(lstAllMenu.Where(m => m.ParentId == 0 && m.Group == MenuGroup.MenuTop).OrderBy(m => m.Order).ToList());
            
            foreach(var menu in rootMenus)
            {
                ltrMenuTree.Text +=
                    "<div class='menuItem' id='{0}' style='clear: both;'><div class='menuItemName'>{1}</div><div class='menuItemPermission'>{2}</div>{3}</div>"
                        .FormatWith(menu.Id, menu.MenuName, PopulateMenuPermission(menu), PopulateSubMenu(lstAllMenu, level + 1, menu.Id));

            }
        }

        /// <summary>
        /// Populate sub menu
        /// </summary>
        /// <param name="lstAllmenu"></param>
        /// <param name="level"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private string PopulateSubMenu(List<MenuModel> lstAllmenu, int level, int parentId)
        {
            var childMenus = lstAllmenu.Where(m => m.ParentId == parentId).OrderBy(m => m.Order).ToList();

            if(childMenus.Count == 0)
            {
                return string.Empty;
            }

            var strChildMenu = string.Empty;
            foreach(var menu in childMenus)
            {
                var beforeSpace = "";
                for(var i = 0; i < level; i++)
                {
                    beforeSpace += @"----";
                }
                strChildMenu +=
                    "<div class='menuItem' id='{0}' style='clear: both;'><div class='menuItemName'>{1}{2}</div><div class='menuItemPermission'>{3}</div>{4}</div>"
                        .FormatWith(menu.Id, beforeSpace, menu.MenuName, PopulateMenuPermission(menu), PopulateSubMenu(lstAllmenu, level + 1, menu.Id));
            }

            return strChildMenu;
        }

        /// <summary>
        /// Populate menu permission for role
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        private string PopulateMenuPermission(MenuModel menu)
        {
            var strMenuPermission =
                "<input type='checkbox' id='read_{0}' class='permissionCheckbox' onClick='handlerPermissionChanged(this)' disabled='true' /> Xem | ".FormatWith(menu.Id);
            strMenuPermission +=
                "<input type='checkbox' id='edit_{0}' class='permissionCheckbox' onClick='handlerPermissionChanged(this)' disabled='true' /> Sửa | ".FormatWith(menu.Id);
            strMenuPermission +=
                "<input type='checkbox' id='del_{0}' class='permissionCheckbox' onClick='handlerPermissionChanged(this)' disabled='true' /> Xóa | ".FormatWith(menu.Id);
            strMenuPermission +=
                "<input type='checkbox' id='full_{0}' class='permissionCheckbox' onClick='handlerPermissionChanged(this)' disabled='true' /> Toàn quyền ".FormatWith(menu.Id);
            return strMenuPermission;
        }

        #endregion
    }
}