using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Object.Security;
using Web.Core.Service.Security;

namespace Web.HRM.Modules.System
{
    public partial class ModulesSystemRoleDefault : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                LoadMenu();
            }
        }

        #region Event Methods

        /// <summary>
        /// Init window add role for specify role
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitEditingRole(object sender, DirectEventArgs e)
        {
            try
            {
                var role = RoleServices.GetById(Convert.ToInt32(hdfRecordId.Text));
                txtRoleName.Text = role.RoleName;
                txtDescription.Text = role.Description;
                txtRoleCommand.Text = @"Update";
                // wdAddRole.Title = @"Sửa đổi thông tin quyền";
                wdAddRole.Show();
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        /// <summary>
        /// Insert or Update role
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdateRole(object sender, DirectEventArgs e)
        {
            try
            {
                if (e.ExtraParams["Command"] == "Insert")
                {
                    // create new role
                    var role = new Web.Core.Object.Security.Role
                    {
                        RoleName = txtRoleName.Text.Trim(),
                        Description = txtDescription.Text.Trim(),
                        IsDeleted = false,
                        Order = 1,
                        CreatedBy = CurrentUser.User.Id.ToString(),
                        CreatedDate = DateTime.Now,
                        EditedBy = CurrentUser.User.Id.ToString(),
                        EditedDate = DateTime.Now
                    };
                    // create role
                    RoleServices.Create(role);
                }

                if (e.ExtraParams["Command"] == "Update")
                {
                    var role = RoleServices.GetById(Convert.ToInt32(hdfRecordId.Text));
                    if (role != null)
                    {
                        // set new value
                        role.RoleName = txtRoleName.Text.Trim();
                        role.Description = txtDescription.Text.Trim();
                        role.EditedBy = CurrentUser.User.Id.ToString();
                        role.EditedDate = DateTime.Now;
                        // update role
                        RoleServices.Update(role);
                    }
                }

                if (e.ExtraParams["Close"] == "True")
                {
                    // hide window
                    wdAddRole.Hide();
                }
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra khi cập nhật", ex.Message).Show();
            }
        }

        /// <summary>
        /// Duplicate role
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DuplicateRole(object sender, DirectEventArgs e)
        {
            try
            {
                // create new role
                var role = new Web.Core.Object.Security.Role
                {
                    RoleName = "Copy of " + txtRoleName.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    IsDeleted = false,
                    Order = 1,
                    CreatedBy = CurrentUser.User.Id.ToString(),
                    CreatedDate = DateTime.Now,
                    EditedBy = CurrentUser.User.Id.ToString(),
                    EditedDate = DateTime.Now
                };
                // create role
                RoleServices.Create(role);
                // regis srcipt reload data
                RM.RegisterClientScriptBlock("ds", "#{roleStore}.reload();");
                // hide window
                wdAddRole.Hide();
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        #endregion

        #region DirectMethod

        /// <summary>
        /// Load menu for role
        /// </summary>
        [DirectMethod]
        public void LoadMenusForRole(int roleId)
        {
            var menusForRole = MenuRoleServices.GetAll(roleId, null, null, null);
            hdfMenusForRole.Text = new JavaScriptSerializer().Serialize(menusForRole);
            RM.RegisterClientScriptBlock("LoadMenusForRole", "loadMenusForRole();");
        }

        [DirectMethod]
        public void SaveMenusForRole(int roleId)
        {
            // get new list of menus for role
            var menusForRole = new JavaScriptSerializer().Deserialize<List<MenuRole>>(hdfMenusForRole.Text);
            // delete old menus for role
            MenuRoleServices.Delete(roleId, null);
            // insert new menus for role
            foreach (var menuRole in menusForRole)
            {
                MenuRoleServices.Create(menuRole);
            }
        }

        /// <summary>
        /// Delete role
        /// </summary>
        /// <param name="id"></param>
        [DirectMethod]
        public void DeleteRole(int id)
        {
            try
            {
                // delete role
                RoleServices.DeletePermanent(id);
                // reset hidden field
                hdfRecordId.Text = "";
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        /// <summary>
        /// Close window add/edit role
        /// </summary>
        [DirectMethod]
        public void CloseAddRoleWindow()
        {
            wdAddRole.Title = @"Thêm quyền";
            wdAddRole.Icon = Icon.KeyAdd;
            RM.RegisterClientScriptBlock("ds", "#{roleStore}.reload();");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Load root menu
        /// </summary>
        private void LoadMenu()
        {
            var rootMenus = MenuServices.GetAll(0, false, null, null, null, null);
            foreach (var menu in rootMenus)
            {
                ltrMenuTree.Text +=
                    "<li id='{0}'><div class='menuItemName'>{1}</div><div class='menuItemPermission'>| {2}</div>{3}</li>"
                        .FormatWith(
                            menu.Id, menu.MenuName, LoadMenuPermission(menu), LoadChildMenu(menu.Id));

            }
        }

        /// <summary>
        /// Load child menu
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private string LoadChildMenu(int parentId)
        {
            var childMenus = MenuServices.GetAll(parentId, false, null, null, null, null);

            if (childMenus.Count == 0)
            {
                return string.Empty;
            }

            var strChildMenu = string.Empty;
            foreach (var menu in childMenus)
            {
                strChildMenu +=
                    "<li id='{0}'><div class='menuItemName'>{1}</div><div class='menuItemPermission'>| {2}</div>{3}</li>"
                        .FormatWith(menu.Id, menu.MenuName, LoadMenuPermission(menu), LoadChildMenu(menu.Id));
            }

            return "<ul>{0}</ul>".FormatWith(strChildMenu);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menu"></param>
        private string LoadMenuPermission(Web.Core.Object.Security.Menu menu)
        {
            var strMenuPermission =
                "<input type='checkbox' id='read_{0}' class='permissionCheckbox' /> Xem | ".FormatWith(menu.Id);
            strMenuPermission +=
                "<input type='checkbox' id='edit_{0}' class='permissionCheckbox' /> Sửa | ".FormatWith(menu.Id);
            strMenuPermission +=
                "<input type='checkbox' id='del_{0}' class='permissionCheckbox' /> Xóa | ".FormatWith(menu.Id);
            strMenuPermission +=
                "<input type='checkbox' id='full_{0}' class='permissionCheckbox' /> Toàn quyền ".FormatWith(menu.Id);

            return strMenuPermission;
        }

        #endregion
    }
}