using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Object.Catalog;
using Web.Core.Service.Catalog;

namespace Web.HRM.Modules.Catalog
{
    public partial class CatalogGroupQuantumGrade : BasePage
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
                    var model = new CatalogGroupQuantumGradeModel();

                    // check id
                    if(id > 0)
                    {
                        var result = CatalogGroupQuantumGradeController.GetById(id);
                        if(result != null)
                            model = result;
                    }

                    // set catalog props
                    hdfGroupQuantumId.Text = model.GroupQuantumId.ToString();
                    cboGroupQuantum.Text = model.GroupQuantumName;
                    txtMonthStep.Text = model.MonthStep.ToString();
                    txtGrade.Text = model.Grade.ToString();
                    txtFactor.Text = model.Factor.ToString("0.00");
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
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục mức lương nhóm ngạch - InitWindow", ex));
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
                var model = new CatalogGroupQuantumGradeModel
                {
                    EditedBy = CurrentUser.User.UserName
                };

                // check id
                if(!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var result = CatalogGroupQuantumGradeController.GetById(Convert.ToInt32(hdfId.Text)); ;
                    if(result != null)
                        model = result;
                }

                // get group quantum
                var groupQuantum = CatalogGroupQuantumController.GetById(Convert.ToInt32(hdfGroupQuantumId.Text));

                // check group quantum
                if (groupQuantum == null)
                {
                    // show alert
                    Dialog.ShowError("Chưa chọn nhóm ngạch");
                    return;
                }

                // set new props for model
                model.GroupQuantumId = groupQuantum.Id;
                model.MonthStep = !string.IsNullOrEmpty(txtMonthStep.Text) && Convert.ToInt32(txtMonthStep.Text) > 0 ? Convert.ToInt32(txtMonthStep.Text) : groupQuantum.MonthStep;
                model.Grade = !string.IsNullOrEmpty(txtGrade.Text) && Convert.ToInt32(txtGrade.Text) > 0 ? Convert.ToInt32(txtGrade.Text) : groupQuantum.GradeMax;
                model.Factor = !string.IsNullOrEmpty(txtFactor.Text) ? Convert.ToDecimal(txtFactor.Text) : 0;
                model.Status = !string.IsNullOrEmpty(hdfStatus.Text) ? (CatalogStatus)Convert.ToInt32(hdfStatus.Text) : CatalogStatus.Active;

                // check max grade
                if (model.Grade > groupQuantum.GradeMax)
                {
                    // show alert
                    Dialog.ShowError("Vượt quá bậc tối đa của nhóm ngạch");
                    return;
                }

                // check model id
                if(model.Id > 0)
                {
                    // update
                    CatalogGroupQuantumGradeController.Update(model);

                    // log action
                    SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục mức lương nhóm ngạch", SystemAction.Edit,
                        "Sửa danh mục {0}".FormatWith(model.Name)));
                }
                else
                {
                    // set created user
                    model.CreatedBy = CurrentUser.User.UserName;

                    // insert
                    CatalogGroupQuantumGradeController.Create(model);

                    // log action
                    SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục mức lương nhóm ngạch", SystemAction.Create,
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
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục mức lương nhóm ngạch - InserOrUpdate", ex));
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
                CatalogGroupQuantumGradeController.Delete(id);

                // reload data
                gpCatalog.Reload();
            }
            catch(Exception ex)
            {
                // show dilog
                Dialog.ShowError(ex);

                // log exception
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Danh mục mức lương nhóm ngạch - Delete", ex));
            }
        }

        /// <summary>
        /// Group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeGroupQuantum_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeGroupQuantum.DataSource = CatalogGroupQuantumController.GetAll(null, null, CatalogStatus.Active, false, null, null);
            storeGroupQuantum.DataBind();
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