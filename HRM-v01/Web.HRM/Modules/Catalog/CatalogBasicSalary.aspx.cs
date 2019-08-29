using Ext.Net;
using System;
using System.Globalization;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Modules.Catalog
{
    public partial class CatalogBasicSalary : BasePage
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
                // init data
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
                    var model = new CatalogBasicSalaryModel();

                    // check id
                    if(id > 0)
                    {
                        var result = CatalogBasicSalaryController.GetById(id);
                        if(result != null)
                            model = result;
                    }

                    // set catalog props
                    txtName.Text = model.Name;
                    txtDescription.Text = model.Description;
                    txtValue.Text = ((decimal)model.Value).ToString(CultureInfo.InvariantCulture);
                    dfAppliedDate.SetValue(model.AppliedDate);
                    txtOrder.Text = model.Order.ToString();
                    hdfStatus.Text = ((int)model.Status).ToString();
                    cboStatus.Text = model.Status.Description();

                    // show window
                    wdSetting.Show();
                }
            }
            catch(Exception ex)
            {
                // show dilog
                Dialog.ShowError(ex);

                // log exception
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục lương cơ bản - InitWindow", ex));
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
                var model = new CatalogBasicSalaryModel
                {
                    EditedBy = CurrentUser.User.UserName
                };

                // check id
                if(!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var result = CatalogBasicSalaryController.GetById(Convert.ToInt32(hdfId.Text)); ;
                    if(result != null)
                        model = result;
                }

                // set new props for model
                model.Name = txtName.Text;
                model.Description = txtDescription.Text;
                model.Order = !string.IsNullOrEmpty(txtOrder.Text) ? Convert.ToInt32(txtOrder.Text) : 0;
                model.Status = !string.IsNullOrEmpty(hdfStatus.Text) ? (CatalogStatus)Convert.ToInt32(hdfStatus.Text) : CatalogStatus.Active;
                model.Value = Convert.ToDecimal(txtValue.Text);
                if(dfAppliedDate.SelectedDate != DateTime.MinValue)
                {
                    model.AppliedDate = dfAppliedDate.SelectedDate;
                }

                // check model id
                if(model.Id > 0)
                {
                    // update
                    CatalogBasicSalaryController.Update(model);

                    // log action
                    SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Lương cơ bản", SystemAction.Edit,
                        "Sửa lương cơ bản {0}".FormatWith(model.Name)));
                }
                else
                {
                    // set created user
                    model.CreatedBy = CurrentUser.User.UserName;

                    // insert
                    CatalogBasicSalaryController.Create(model);

                    // log action
                    SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Lương cơ bản", SystemAction.Create,
                        "Thêm lương cơ bản {0}".FormatWith(model.Name)));
                }
                // hide window
                wdSetting.Hide();
                // reload data
                gpCatalog.Reload();
            }
            catch(Exception ex)
            {
                // show dilog
                Dialog.ShowError(ex);

                // log exception
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục lương cơ bản - InsertOrUpdate", ex));
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
                CatalogBasicSalaryController.Delete(id);
                // reload data
                gpCatalog.Reload();
            }
            catch(Exception ex)
            {
                // show dilog
                Dialog.ShowError(ex);

                // log exception
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục lương cơ bản - Delete", ex));
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