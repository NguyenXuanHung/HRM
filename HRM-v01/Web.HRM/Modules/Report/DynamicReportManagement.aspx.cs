using System;
using System.Collections.Generic;
using System.Linq;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Controller;

namespace Web.HRM.Modules.Report
{
    public partial class DynamicReportManagement : BasePage
    {
        /// <summary>
        /// Page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // custom page permission
            btnConfig.Visible = CurrentPermission.CanWrite;
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
                if (int.TryParse(param, out var id))
                {
                    // init window props
                    if (id > 0)
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
                    var model = new ReportDynamicModel(null);

                    // check id
                    if (id > 0)
                    {
                        var result = ReportDynamicController.GetById(id);
                        if (result != null)
                            model = result;
                    }

                    // set props
                    txtName.Text = model.Name;
                    txtDescription.Text = model.Description;
                    hdfReportTemplate.Text = ((int)model.Template).ToString();
                    cboReportTemplate.Text = model.TemplateName;
                    hdfReportPaperKind.Text = ((int) model.PaperKind).ToString();
                    cboReportPaperKind.Text = model.PaperKindName;
                    hdfReportOrientation.Text = ((int) model.Orientation).ToString();
                    cboReportOrientation.Text = model.OrientationName;
                    hdfGroupHeader1.Text = ((int)model.GroupHeader1).ToString();
                    cboGroupHeader1.Text = model.GroupHeaderName1;
                    hdfGroupHeader2.Text = ((int)model.GroupHeader2).ToString();
                    cboGroupHeader2.Text = model.GroupHeaderName2;
                    hdfGroupHeader3.Text = ((int)model.GroupHeader3).ToString();
                    cboGroupHeader3.Text = model.GroupHeaderName3;
                    txtParentDepartment.Text = model.ParentDepartment;
                    txtDepartment.Text = model.Department;
                    txtTitle.Text = model.Title;
                    txtDuration.Text = model.Duration;
                    txtCreatedByTitle.Text = model.CreatedByTitle;
                    txtCreatedByNote.Text = model.CreatedByNote;
                    txtCreatedByName.Text = model.CreatedByName;
                    txtReviewedByTitle.Text = model.ReviewedByTitle;
                    txtReviewedByNote.Text = model.ReviewedByNote;
                    txtReviewedByName.Text = model.ReviewedByName;
                    txtSignedByTitle.Text = model.SignedByTitle;
                    txtSignedByNote.Text = model.SignedByNote;
                    txtSignedByName.Text = model.SignedByName;
                    hdfStatus.Text = ((int)model.Status).ToString();
                    cboStatus.Text = model.StatusName;

                    // show window
                    wdSetting.Show();
                }
            }
            catch (Exception exception)
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
                // init entity
                var model = new ReportDynamicModel(null);

                // check id
                if (!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var result = ReportDynamicController.GetById(Convert.ToInt32(hdfId.Text)); ;
                    if (result != null)
                        model = result;
                }

                // set new props for entity
                model.Name = txtName.Text;
                model.Description = txtDescription.Text;
                model.Template = (ReportTemplate)Convert.ToInt32(hdfReportTemplate.Text);
                model.PaperKind = (ReportPaperKind)Convert.ToInt32(hdfReportPaperKind.Text);
                model.Orientation = (ReportOrientation)Convert.ToInt32(hdfReportOrientation.Text);
                model.GroupHeader1 = (ReportGroupHeader)Convert.ToInt32(hdfGroupHeader1.Text);
                model.GroupHeader2 = (ReportGroupHeader)Convert.ToInt32(hdfGroupHeader2.Text);
                model.GroupHeader3 = (ReportGroupHeader)Convert.ToInt32(hdfGroupHeader3.Text);
                model.ParentDepartment = txtParentDepartment.Text;
                model.Department = txtDepartment.Text;
                model.Title = txtTitle.Text;
                model.Duration = txtDuration.Text;
                model.CreatedByTitle = txtCreatedByTitle.Text;
                model.CreatedByNote = txtCreatedByNote.Text;
                model.CreatedByName = txtCreatedByName.Text;
                model.ReviewedByTitle = txtReviewedByTitle.Text;
                model.ReviewedByNote = txtReviewedByNote.Text;
                model.ReviewedByName = txtReviewedByName.Text;
                model.SignedByTitle = txtSignedByTitle.Text;
                model.SignedByNote = txtSignedByNote.Text;
                model.SignedByName = txtSignedByName.Text;
                model.Status = (ReportStatus)Convert.ToInt32(hdfStatus.Text);

                // parse report source to json
                if (!string.IsNullOrEmpty(hdfReportSource.Text))
                {
                    var payrollConfigId = Convert.ToInt32(hdfReportSource.Text);
                    switch (model.Template)
                    {
                        case ReportTemplate.EnterprisePayroll:
                            var payrollConfigs = SalaryBoardConfigController.GetAll(payrollConfigId, true, null, null, null, "[ColumnCode]", null);
                            var lstItemModel = new List<ListItemModel>();
                            lstItemModel.Add(new ListItemModel("FullName", "FullName"));
                            lstItemModel.Add(new ListItemModel("EmployeeCode", "EmployeeCode"));
                            lstItemModel.Add(new ListItemModel("PositionName", "PositionName"));
                            lstItemModel.AddRange(payrollConfigs.Select(c => new ListItemModel(c.ColumnCode, c.ColumnCode)));
                            model.ReportSource = JSON.Serialize(lstItemModel);
                            model.Argument = payrollConfigId.ToString();
                            break;
                    }
                }

                // check entity id
                if (model.Id > 0)
                {
                    // update
                    ReportDynamicController.Update(model);
                }
                else
                {
                    // insert
                    ReportDynamicController.Create(model);
                }

                // hide window
                wdSetting.Hide();

                // reload data
                gpReport.Reload();
            }
            catch (Exception exception)
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
                if (!int.TryParse(param, out var id) || id <= 0)
                {
                    // parse error, show error
                    Dialog.ShowError("Có lỗi xảy ra trong quá trình xử lý");
                    return;
                }

                // delete
                ReportDynamicController.Delete(id);

                // reload data
                gpReport.Reload();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// Delete catalog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RedirectConfig(object sender, DirectEventArgs e)
        {
            try
            {
                // init id
                var param = e.ExtraParams["Id"];

                // parse id
                if (!int.TryParse(param, out var id) || id <= 0)
                {
                    // parse error, show error
                    Dialog.ShowError("Có lỗi xảy ra trong quá trình xử lý");
                    return;
                }

                // redirect
                Response.Redirect("~/Modules/Report/ReportColumnManagement.aspx?reportId={0}".FormatWith(id), true);
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DuplicateData(object sender, DirectEventArgs e)
        {
            try
            {
                // init model
                var reportDynamic = new ReportDynamicModel(null);
                var reportColumns = new List<ReportColumnModel>();

                // check id
                if (!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var resultReportDynamic = ReportDynamicController.GetById(Convert.ToInt32(hdfId.Text));
                    if (resultReportDynamic != null)
                    {
                        reportDynamic = resultReportDynamic;
                        var resultReportColumn = ReportColumnController.GetAll(null, resultReportDynamic.Id, null, null, null, null, false, null, null);
                        if (resultReportColumn != null)
                            reportColumns = resultReportColumn;
                    }
                }

                // create duplicated report dynamic
                reportDynamic.Name += " - Copy";
                reportDynamic.CreatedDate = DateTime.Now;
                reportDynamic.CreatedBy = CurrentUser.User.UserName;
                reportDynamic.EditedDate = DateTime.Now;
                reportDynamic.EditedBy = CurrentUser.User.UserName;
                var newReportDynamic = ReportDynamicController.Create(reportDynamic);

                // create list id
                var ids = new Dictionary<int, int>();
                // create list old parent id
                var parentIds = new List<int>();
                // create list new report column
                var newReportColumns = new List<ReportColumnModel>();

                // create duplicated report columns
                foreach (var reportColumn in reportColumns)
                {
                    var oldParentId = reportColumn.ParentId;
                    // 
                    reportColumn.ReportId = newReportDynamic.Id;
                    reportColumn.CreatedDate = DateTime.Now;
                    reportColumn.CreatedBy = CurrentUser.User.UserName;
                    reportColumn.EditedDate = DateTime.Now;
                    reportColumn.EditedBy = CurrentUser.User.UserName;
                    reportColumn.ParentId = 0;
                    // create new column
                    var newReportColumn = ReportColumnController.Create(reportColumn);
                    // add new column to list
                    newReportColumns.Add(newReportColumn);
                    // add pair of old and new id to list
                    var oldId = reportColumn.Id;
                    var newId = newReportColumn.Id;
                    ids.Add(oldId, newId);
                    // add item to list parent id
                    parentIds.Add(oldParentId);
                }

                // update parentId
                for (var i = 0; i < newReportColumns.Count; i++)
                {
                    // set parent id
                    foreach (var id in ids)
                        if (parentIds[i] == id.Key)
                            newReportColumns[i].ParentId = id.Value;
                    // update column
                    ReportColumnController.Update(newReportColumns[i]);
                }

                gpReport.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex);
            }
        }

        #region Store

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeReportTemplate_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            // store report template
            storeReportTemplate.DataSource = typeof(ReportTemplate).GetIntAndDescription();
            storeReportTemplate.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeReportSource_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfReportTemplate.Text))
            {
                // init data source
                var reportSource = new List<ListItemModel>();

                // init report type
                var reportTemplate = Convert.ToInt32(hdfReportTemplate.Text);

                switch (reportTemplate)
                {
                    case (int)ReportTemplate.EnterprisePayroll:
                        reportSource = CatalogController.GetAll("sal_PayrollConfig", null, null, null, false, null, null).OrderBy(c => c.Name).Select(c => new ListItemModel(c.Id.ToString(), c.Name)).ToList();
                        break;
                }

                // store report template
                storeReportSource.DataSource = reportSource;
                storeReportSource.DataBind();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeGroupHeader_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeGroupHeader.DataSource = typeof(ReportGroupHeader).GetIntAndDescription();
            storeGroupHeader.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeReportStatus_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeReportStatus.DataSource = typeof(ReportStatus).GetIntAndDescription();
            storeReportStatus.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeReportPaperKind_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeReportPaperKind.DataSource = typeof(ReportPaperKind).GetIntAndDescription();
            storeReportPaperKind.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeReportOrientation_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeReportOrientation.DataSource = typeof(ReportOrientation).GetIntAndDescription();
            storeReportOrientation.DataBind();
        }

        #endregion

    }
}