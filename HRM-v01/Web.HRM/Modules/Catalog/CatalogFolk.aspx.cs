using Ext.Net;
using System;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Modules.Catalog
{
    public partial class CatalogFolk : BasePage
    {
        /// <summary>
        /// Page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!ExtNet.IsAjaxRequest)
            {
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
                    var model = new CatalogFolkModel();
                    // check id
                    if(id > 0)
                    {
                        var result = CatalogFolkController.GetById(id);
                        if(result != null)
                            model = result;
                    }
                    // set catalog props
                    // name
                    txtName.Text = model.Name;
                    // description
                    txtDescription.Text = model.Description;
                    // minority
                    chkIsMinority.Checked = model.IsMinority;
                    // order
                    txtOrder.Text = model.Order.ToString();
                    // status
                    hdfStatus.Text = ((int)model.Status).ToString();
                    cboStatus.Text = model.Status.Description();
                    // show window
                    wdSetting.Show();
                }
            }
            catch(Exception exception)
            {
                Dialog.ShowError(exception);
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
                var model = new CatalogFolkModel
                {
                    EditedBy = CurrentUser.User.UserName
                };
                // check id
                if(!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var result = CatalogFolkController.GetById(Convert.ToInt32(hdfId.Text)); ;
                    if(result != null)
                        model = result;
                }
                // set new props for model
                model.Name = txtName.Text;
                model.Description = txtDescription.Text;
                model.IsMinority = chkIsMinority.Checked;
                model.Order = !string.IsNullOrEmpty(txtOrder.Text) ? Convert.ToInt32(txtOrder.Text) : 0;
                model.Status = !string.IsNullOrEmpty(hdfStatus.Text) ? (CatalogStatus)Convert.ToInt32(hdfStatus.Text) : CatalogStatus.Active;

                // check model id
                if(model.Id > 0)
                {
                    // update
                    CatalogFolkController.Update(model);
                }
                else
                {
                    // set created user
                    model.CreatedBy = CurrentUser.User.UserName;
                    // insert
                    CatalogFolkController.Create(model);
                }
                // hide window
                wdSetting.Hide();
                // reload data
                gpCatalog.Reload();
            }
            catch(Exception exception)
            {
                Dialog.ShowError(exception);
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
                CatalogFolkController.Delete(id);
                // reload data
                gpCatalog.Reload();
            }
            catch(Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }        
        protected void storeStatus_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeStatus.DataSource = typeof(CatalogStatus).GetIntAndDescription();
            storeStatus.DataBind();
        }
    }
}
