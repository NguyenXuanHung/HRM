using System;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Modules.Catalog
{
    public partial class CatalogGroupQuantum : BasePage
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
                    var model = new CatalogGroupQuantumModel();

                    // check id
                    if(id > 0)
                    {
                        var result = CatalogGroupQuantumController.GetById(id);
                        if(result != null)
                            model = result;
                    }

                    // set catalog props
                    txtName.Text = model.Name;
                    txtDescription.Text = model.Description;
                    txtGradeMax.Text = model.GradeMax.ToString();
                    txtMonthStep.Text = model.MonthStep.ToString();
                    txtPercentageOverGrade.Text = model.PercentageOverGrade.ToString("0.00");
                    txtPercentageOverGradeStep.Text = model.PercentageOverGradeStep.ToString("0.00");
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
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục nhóm ngạch - InitWindow", ex));
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
                var model = new CatalogGroupQuantumModel
                {
                    EditedBy = CurrentUser.User.UserName
                };

                // check id
                if(!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var result = CatalogGroupQuantumController.GetById(Convert.ToInt32(hdfId.Text)); ;
                    if(result != null)
                        model = result;
                }

                // set new props for model
                model.Name = txtName.Text;
                model.Description = txtDescription.Text;
                model.GradeMax = !string.IsNullOrEmpty(txtGradeMax.Text) && Convert.ToInt32(txtGradeMax.Text) > 0 ? Convert.ToInt32(txtGradeMax.Text) : 36;
                model.MonthStep = !string.IsNullOrEmpty(txtMonthStep.Text) && Convert.ToInt32(txtMonthStep.Text) > 0 ? Convert.ToInt32(txtMonthStep.Text) : 0;
                model.PercentageOverGrade = !string.IsNullOrEmpty(txtPercentageOverGrade.Text) && Convert.ToDecimal(txtPercentageOverGrade.Text) > 0 ? Convert.ToDecimal(txtPercentageOverGrade.Text) : 5;
                model.PercentageOverGradeStep = !string.IsNullOrEmpty(txtPercentageOverGradeStep.Text) && Convert.ToDecimal(txtPercentageOverGradeStep.Text) > 0 ? Convert.ToDecimal(txtPercentageOverGradeStep.Text) : 1;
                model.Group = hdfGroup.Text;
                model.Order = !string.IsNullOrEmpty(txtOrder.Text) ? Convert.ToInt32(txtOrder.Text) : 0;
                model.Status = !string.IsNullOrEmpty(hdfStatus.Text) ? (CatalogStatus)Convert.ToInt32(hdfStatus.Text) : CatalogStatus.Active;

                // check model id
                if(model.Id > 0)
                {
                    // update
                    CatalogGroupQuantumController.Update(model);

                    // log action
                    SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục nhóm ngạch", SystemAction.Edit,
                        "Sửa danh mục {0}".FormatWith(model.Name)));
                }
                else
                {
                    // set created user
                    model.CreatedBy = CurrentUser.User.UserName;

                    // insert
                    CatalogGroupQuantumController.Create(model);

                    // log action
                    SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục nhóm ngạch", SystemAction.Create,
                        "Tạo danh mục {0}".FormatWith(model.Name)));
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
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục nhóm ngạch - InserOrUpdate", ex));
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
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục nhóm ngạch - Delete", ex));
            }
        }

        /// <summary>
        /// Group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeGroup_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeGroup.DataSource = typeof(CatalogGroupGroupQuantum).GetIntAndDescription();
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