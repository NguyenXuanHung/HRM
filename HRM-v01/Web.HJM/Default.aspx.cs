using System;
using System.Collections.Generic;
using System.Linq;
using Ext.Net;
using SoftCore;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Object.Security;
using Web.Core.Service.Security;


public partial class Default : BasePage
{
    protected const string ApplicationName = @"PHẦN MỀM QUẢN LÝ HỒ SƠ CÁN BỘ, CÔNG CHỨC, VIÊN CHỨC";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!ExtNet.IsAjaxRequest)
        {
            // check is super user
            if (CurrentUser.User.IsSuperUser)
            {
                // show system menu
                mnuSystem.Visible = true;
            }
            // init menu for user
            btnUser.Text = !string.IsNullOrEmpty(CurrentUser.User.DisplayName)
                ? CurrentUser.User.DisplayName
                : CurrentUser.User.UserName;
            // load menu top
            LoadMenuTop();
            // load menu left
            LoadMenuLeft();
        }
    }

    #region Event Methods

    /// <summary>
    /// Update password
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdatePassword_Click(object sender, DirectEventArgs e)
    {
        var message = string.Empty;
        // update password
        if (UserServices.ChangePassword(CurrentUser.User.UserName, txtOldPassword.Text, txtNewPassword.Text, ref message) != null)
        {
            // update success
            Dialog.ShowNotification("Cập nhật mật khẩu thành công.");
        }
        else
        {
            // show error
            Dialog.ShowError(message);
        }
        wdChangePassword.Hide();
    }

    /// <summary>
    /// Logout
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogout_Click(object sender, DirectEventArgs e)
    {
        //var accessDiary = new Web.Core.Object.Security.AccessHistory
        //{
        //    Function = "HRM",
        //    Description = "Đăng xuất",
        //    IsError = false,
        //    UserName = CurrentUser.User.UserName,
        //    Time = DateTime.Now,
        //    BusinessCode = "Users",
        //    ComputerName = Util.GetInstance().GetComputerName(Request.UserHostAddress),
        //    ComputerIP = Request.UserHostAddress,
        //    Referent = ""
        //};
        //AccessHistoryServices.Create(accessDiary);

        // logout
        Session.Abandon();
        // redirect to login page
        Response.Redirect(Resource.Get("LoginUrl"), true);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// 
    /// </summary>
    private void LoadMenuTop()
    {
        // init user id
        int? userId = null;
        // check is super user
        if (!CurrentUser.User.IsSuperUser)
            // not admin, load menu by user
            userId = CurrentUser.User.Id;
        // get all menu for user
        var lstMenu = MenuController.GetAll(null, null, null, MenuGroup.MenuTop, MenuStatus.Active, userId, false, null, null);
        var root = lstMenu.FirstOrDefault(m => m.ParentId == 0);
        if (root != null)
        {
            // visible menu top
            mnuSystem.Visible = true;
            // load child menu
            var lstChildMenu = lstMenu.Where(m => m.ParentId == root.Id).OrderBy(m => m.Order).ToList();
            foreach (var menu in lstChildMenu)
            {
                var extMenuItem = new MenuItem
                {
                    ID = "mnu{0}".FormatWith(menu.TabName),
                    IconCls = menu.Icon,
                    Text = menu.MenuName
                };
                extMenuItem.Listeners.Click.Handler = menu.LinkUrl.StartsWith("javascript:")
                                                            ? menu.LinkUrl.Replace("javascript:", "")
                                                            : "addTab(pnlCenter, 'tab_mnu_{0}', '{1}', '{2}');".FormatWith(
                                                                    menu.TabName, menu.LinkUrl, menu.MenuName);
                mnuSystemItems.Items.Add(extMenuItem);
            }

        }
    }

    /// <summary>
    /// Load menu left
    /// </summary>
    private void LoadMenuLeft()
    {
        // init user id
        int? userId = null;
        // check is super user
        if (!CurrentUser.User.IsSuperUser)
            // not admin, load menu by user
            userId = CurrentUser.User.Id;
        // get all menu for user
        var lstAllMenu = MenuController.GetAll(null, null, null, MenuGroup.MenuLeft, MenuStatus.Active, userId, false, null, null);
        var lstRootMenu = lstAllMenu.Where(m => m.ParentId == 0).OrderBy(m => m.Order);
        foreach (var menu in lstRootMenu)
        {
            var pnlTree = new TreePanel { ID = "mnu{0}".FormatWith(menu.Id), Title = menu.MenuName, IconCls = menu.Icon, RootVisible = false, AutoScroll = true };
            pnlWest.Items.Add(pnlTree);
            var root = new TreeNode { Text = "root" };
            pnlTree.Root.Add(root);
            LoadSubMenu(lstAllMenu, root, menu);
        }
        var btnLogo = new ImageButton
        {
            ImageUrl = "~/Resource/images/logo.png",
            OverImageUrl = "~/Resource/images/logoHover.png"
        };
        tbMenuTop.Items.Insert(0, btnLogo);
        var lblApplication = new Label
        {
            Html = "<b style='color:#15428B !important;font-family:verdana !important;'>{0}</b>".FormatWith(ApplicationName)
        };
        tbMenuTop.Items.Insert(1, lblApplication);
        tbMenuTop.Items.Insert(1, new ToolbarSpacer()
        {
            ID = "tbspacer",
            Width = 5
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lstAllMenu"></param>
    /// <param name="currentNode"></param>
    /// <param name="parentMenu"></param>
    private void LoadSubMenu(List<MenuModel> lstAllMenu, TreeNode currentNode, MenuModel parentMenu)
    {
        var lstSubMenu = lstAllMenu.Where(m => m.ParentId == parentMenu.Id && m.IsDeleted == false).OrderBy(m => m.Order);
        foreach (var menu in lstSubMenu)
        {
            var node = new TreeNode() { Text = menu.MenuName, IconCls = menu.Icon };
            if (!string.IsNullOrEmpty(menu.LinkUrl))
            {
                if (menu.LinkUrl.Contains("?") == false)
                    menu.LinkUrl += "?mId=" + menu.Id;
                else
                    menu.LinkUrl += "&mId=" + menu.Id;
                node.Listeners.Click.Handler = "addTab(#{pnlCenter},'dm_file" + menu.Id + "','" + menu.LinkUrl + "', '" + menu.TabName + "')";
            }
            currentNode.Nodes.Add(node);
            LoadSubMenu(lstAllMenu, node, menu);
        }
    }

    #endregion
}