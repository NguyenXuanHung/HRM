using System;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Modules.Setting
{
    public partial class MenuManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                if (string.IsNullOrEmpty(hdfFilterGroup.Text))
                    hdfFilterGroup.Text = ((int) MenuGroup.MenuLeft).ToString();

                // set combo filter group
                cboFilterGroup.Text = ((MenuGroup) Convert.ToInt32(hdfFilterGroup.Text)).Description();
            }
        }

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
                    hdfId.Text = id.ToString();

                    // init object
                    var model = new MenuModel(null);

                    // check id
                    if(id > 0)
                    {
                        var result = MenuController.GetById(id);
                        if(result != null)
                            model = result;
                    }

                    // set props
                    txtMenuName.Text = model.MenuName;
                    txtTabName.Text = model.TabName;
                    txtIcon.Text = model.Icon;
                    txtLinkUrl.Text = model.LinkUrl;
                    hdfParentId.Text = model.ParentId.ToString();
                    cboParent.Text = model.ParentName;
                    hdfGroup.Text = hdfFilterGroup.Text;
                    cboGroup.Text = cboFilterGroup.Text;
                    txtOrder.Text = model.Order.ToString();
                    hdfStatus.Text = ((int) model.Status).ToString();
                    cboStatus.Text = model.StatusName;

                    // reset link url
                    cboReport.Text = "";
                    cboFilter.Text = "";

                    // show window
                    wdSetting.Show();
                }
            }
            catch(Exception ex)
            {
                // show dilog
                Dialog.ShowError(ex);

                // log exception
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục quản trị - InitWindow", ex));
            }
        }

        /// <summary>
        /// Insert or Update Catalog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                // init entity
                var model = new MenuModel()
                {
                    EditedBy = CurrentUser.User.UserName
                };

                // check id
                if(!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var result = MenuController.GetById(Convert.ToInt32(hdfId.Text)); ;
                    if(result != null)
                        model = result;
                }

                // set new props for entity
                model.MenuName = txtMenuName.Text;
                model.TabName = txtTabName.Text;
                model.Icon = txtIcon.Text;
                model.LinkUrl = txtLinkUrl.Text;
                model.ParentId = string.IsNullOrEmpty(hdfParentId.Text) ? 0 : Convert.ToInt32(hdfParentId.Text);
                model.Order = string.IsNullOrEmpty(txtOrder.Text) ? 0 : Convert.ToInt32(txtOrder.Text);
                model.Group = (MenuGroup) Convert.ToInt32(hdfGroup.Text);
                model.Status = (MenuStatus) Convert.ToInt32(hdfStatus.Text);
                
                // check entity id
                if(model.Id > 0)
                {
                    // update
                    MenuController.Update(model);

                    // log action
                    SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục quản trị", SystemAction.Edit, 
                        "Sửa menu {0}".FormatWith(model.MenuName)));
                }
                else
                {
                    // set created user
                    model.CreatedBy = CurrentUser.User.UserName;

                    // insert
                    MenuController.Create(model);

                    // log action
                    SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục quản trị", SystemAction.Create, 
                        "Tạo menu {0}".FormatWith(model.MenuName)));
                }

                // hide window
                wdSetting.Hide();
                
                // reload data
                gpMenu.Reload();
            }
            catch(Exception ex)
            {
                // show dilog
                Dialog.ShowError(ex);

                // log exception
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục quản trị - InsertOrUpdate", ex));
            }
        }

        /// <summary>
        /// Delete catalog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete(object sender, DirectEventArgs e)
        {
            try
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
                MenuController.Delete(id);

                // log action
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục quản trị", SystemAction.Delete,
                    "Xóa menu {0}".FormatWith(id)));

                // reload data
                gpMenu.Reload();
            }
            catch(Exception ex)
            {
                // show dialog
                Dialog.ShowError(ex);

                // log exception
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục quản trị - Delete", ex));
            }
        }

        #region Store

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeMenuGroup_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            // store report template
            storeMenuGroup.DataSource = typeof(MenuGroup).GetIntAndDescription();
            storeMenuGroup.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeMenuStatus_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeMenuStatus.DataSource = typeof(MenuStatus).GetIntAndDescription();
            storeMenuStatus.DataBind();
        }

        #endregion
    }
}