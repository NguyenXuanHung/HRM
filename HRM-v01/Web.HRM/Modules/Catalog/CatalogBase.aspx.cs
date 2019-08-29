using Ext.Net;
using System;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Modules.Catalog
{
    public partial class CatalogBase : BasePage
    {
        /// <summary>
        /// Page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                // init data
                hdfObjName.Text = Request.QueryString["objName"];
                hdfCatalogGroup.Text = !string.IsNullOrEmpty(Request.QueryString["catGroup"]) 
                    ? Request.QueryString["catGroup"].ToLower() : "";
                hdfOrder.Text = Request.QueryString["order"];
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
                    var model = new CatalogModel(hdfObjName.Text);

                    // check id
                    if(id > 0)
                    {
                        var result = CatalogController.GetById(hdfObjName.Text, id);
                        if(result != null)
                            model = result;
                    }

                    // set catalog props
                    txtName.Text = model.Name;
                    txtDescription.Text = model.Description;
                    txtOrder.Text = model.Order.ToString();
                    hdfGroup.Text = model.Group;
                    cboGroup.Text = model.GroupName;
                    hdfStatus.Text = ((int)model.Status).ToString();
                    cboStatus.Text = model.Status.Description();

                    // show window
                    wdSetting.Show();
                }
            }
            catch (Exception ex)
            {
                // show dilog
                Dialog.ShowError(ex);

                // log exception
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục {0} - InitWindow".FormatWith(hdfObjName.Text), ex));
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
                // init model
                var model = new CatalogModel(hdfObjName.Text)
                {
                    EditedBy = CurrentUser.User.UserName
                };

                // check id
                if(!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var result = CatalogController.GetById(hdfObjName.Text, Convert.ToInt32(hdfId.Text)); 
                    if(result != null)
                        model = result;
                }

                // set new props for model
                model.Name = txtName.Text;
                model.Description = txtDescription.Text;
                model.Order = !string.IsNullOrEmpty(txtOrder.Text) ? Convert.ToInt32(txtOrder.Text) : 0;
                model.Group = hdfGroup.Text;
                if (Enum.TryParse(hdfStatus.Text, false, out CatalogStatus status))
                    model.Status = status;

                // if cat group is record status
                if (hdfCatalogGroup.Text == nameof(RecordStatus).ToLower())
                    if (Enum.TryParse(hdfGroup.Text, false, out RecordStatus group))
                        model.Group = ((int) group).ToString();

                // create result model
                CatalogModel resultModel;

                // check model id
                if (model.Id > 0)
                {
                    // update
                    resultModel = CatalogController.Update(hdfObjName.Text, model);

                    // log action
                    SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục {0}".FormatWith(hdfObjName.Text), SystemAction.Edit,
                        "Sửa danh mục {0}".FormatWith(model.Name)));
                }
                else
                {                   
                    // set created user
                    model.CreatedBy = CurrentUser.User.UserName;

                    // insert
                    resultModel = CatalogController.Create(hdfObjName.Text, model);

                    // log action
                    SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục {0}".FormatWith(hdfObjName.Text), SystemAction.Create,
                        "Tạo danh mục {0}".FormatWith(model.Name)));
                }

                // show notification
                if (resultModel != null)
                {
                    Dialog.ShowNotification("Lưu thành công");

                    // hide window
                    wdSetting.Hide();

                    // reload data
                    gpCatalog.Reload();
                }
                else
                    Dialog.ShowNotification("Lưu không thành công, Tên đã tồn tại");

            }
            catch (Exception ex)
            {
                // show dilog
                Dialog.ShowError(ex);

                // log exception
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục {0} - InsertOrUpdate".FormatWith(hdfObjName.Text), ex));
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
                CatalogController.Delete(hdfObjName.Text, id);

                // log action
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục {0}".FormatWith(hdfObjName.Text), SystemAction.Delete,
                    "Xóa danh mục {0}".FormatWith(id)));

                // reload data
                gpCatalog.Reload();
            }
            catch (Exception ex)
            {
                // show dilog
                Dialog.ShowError(ex);

                // log exception
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục {0} - Delete".FormatWith(hdfObjName.Text), ex));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeGroup_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if(!string.IsNullOrEmpty(hdfCatalogGroup.Text))
            {
                storeGroup.DataSource = EnumHelper.GetCatalogGroupItems(hdfCatalogGroup.Text);
                storeGroup.DataBind();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeStatus_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeStatus.DataSource = typeof(CatalogStatus).GetIntAndDescription();
            storeStatus.DataBind();
        }
    }
}