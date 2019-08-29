using System;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Modules.Catalog
{
    public partial class CatalogAllowance : BasePage
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
                    var model = new CatalogAllowanceModel();

                    // check id
                    if(id > 0)
                    {
                        var result = CatalogAllowanceController.GetById(id);
                        if(result != null)
                            model = result;
                    }

                    // set catalog props
                    txtName.Text = model.Name;
                    txtDescription.Text = model.Description;
                    txtValue.Text = model.Value.ToString("0.##");
                    txtFormula.Text = model.Formula;
                    hdfValueType.Text = ((int) model.ValueType).ToString();
                    cboValueType.Text = model.ValueTypeName;
                    hdfType.Text = model.Type;
                    cboType.Text = model.TypeName;
                    hdfGroup.Text = model.Group;
                    cboGroup.Text = model.GroupName;
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
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục phụ cấp - InitWindow", ex));
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
                var model = new CatalogAllowanceModel
                {
                    EditedBy = CurrentUser.User.UserName
                };

                // check id
                if(!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var result = CatalogAllowanceController.GetById(Convert.ToInt32(hdfId.Text)); ;
                    if(result != null)
                        model = result;
                }

                // validate form
                if (string.IsNullOrEmpty(hdfValueType.Text) || string.IsNullOrEmpty(hdfValueType.Text))
                {
                    Dialog.ShowError("Bạn chưa nhập đủ dữ liệu");
                    return;
                }

                // set new props for model
                model.Code = hdfType.Text;
                model.Name = txtName.Text;
                model.Description = txtDescription.Text;
                model.Value = !string.IsNullOrEmpty(txtValue.Text) ? Convert.ToDecimal(txtValue.Text) : 0;
                model.Formula = txtFormula.Text;
                model.ValueType = (AllowanceValueType) Convert.ToInt32(hdfValueType.Text);
                model.Type = hdfType.Text;
                model.Group = hdfGroup.Text;
                model.Order = !string.IsNullOrEmpty(txtOrder.Text) ? Convert.ToInt32(txtOrder.Text) : 0;
                model.Status = !string.IsNullOrEmpty(hdfStatus.Text) ? (CatalogStatus)Convert.ToInt32(hdfStatus.Text) : CatalogStatus.Active;

                // check model id
                if(model.Id > 0)
                {
                    // update
                    var result = CatalogAllowanceController.Update(model);

                    if (result != null)
                        // log action
                        SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục phụ cấp", SystemAction.Edit,
                            "Sửa danh mục {0}".FormatWith(model.Name)));
                    else
                        // show error
                        Dialog.ShowError("Có lỗi xảy ra trong quá trình xử lý");
                }
                else
                {
                    // set created user
                    model.CreatedBy = CurrentUser.User.UserName;

                    // insert
                    var result = CatalogAllowanceController.Create(model);

                    if(result != null)
                        // log action
                        SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục phụ cấp", SystemAction.Create,
                        "Tạo danh mục {0}".FormatWith(model.Name)));
                    else
                        // show error
                        Dialog.ShowError("Có lỗi xảy ra trong quá trình xử lý");
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
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục phụ cấp - InserOrUpdate", ex));
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
                CatalogGroupQuantumController.Delete(id);

                // reload data
                gpCatalog.Reload();
            }
            catch(Exception ex)
            {
                // show dilog
                Dialog.ShowError(ex);

                // log exception
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục phụ cấp - Delete", ex));
            }
        }

        /// <summary>
        /// Value type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeValueType_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeValueType.DataSource = typeof(AllowanceValueType).GetIntAndDescription();
            storeValueType.DataBind();
        }

        /// <summary>
        /// Type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeType_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeType.DataSource = typeof(AllowanceType).GetValuesAndDescription();
            storeType.DataBind();
        }

        /// <summary>
        /// Group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeGroup_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeGroup.DataSource = typeof(CatalogGroupAllowance).GetValuesAndDescription();
            storeGroup.DataBind();
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