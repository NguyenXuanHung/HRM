using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DevExpress.XtraReports.UI;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Common;
using Web.Core.Framework.Controller;
using Web.Core.Framework.Interface;
using Web.Core.Framework.Report;

namespace Web.HRM.Modules.Report
{
    public partial class DynamicReportView : BasePage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                // init data
                hdfReportId.Text = Request.QueryString["reportId"];

                // load department
                LoadDepartment();

                // init selected departments
                if (string.IsNullOrEmpty(hdfSelectedDepartmentIds.Text))
                {
                    hdfSelectedDepartmentIds.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                }

                // init selected departments name
                if (string.IsNullOrEmpty(hdfSelectedDepartmentNames.Text))
                {
                    hdfSelectedDepartmentNames.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Name));
                }

                // check report id
                if (!string.IsNullOrEmpty(hdfReportId.Text) && int.TryParse(hdfReportId.Text, out var reportId) && reportId > 0)
                {
                    // create report
                    reportViewer.Report = CreateReport(reportId);

                    // init store filter name
                    if (!string.IsNullOrEmpty(hdfFilterItems.Text))
                    {
                        var filterCollection = JSON.Deserialize<List<FilterItem>>(hdfFilterItems.Text);
                        storeFilterName.DataSource = filterCollection;
                        storeFilterName.DataBind();
                    }

                    // init store filter selected
                    if (!string.IsNullOrEmpty(hdfConditionSelected.Text))
                    {
                        var filterSelected = JSON.Deserialize<List<SelectedFilterCondition>>(hdfFilterItems.Text);
                        storeFilterSelected.DataSource = filterSelected;
                        storeFilterSelected.DataBind();
                    }
                }
            }
        }

        /// <summary>
        /// Refresh value for selecting filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeFilterValue_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfFilterItems.Text))
                {
                    var filterCollection = JSON.Deserialize<List<FilterItem>>(hdfFilterItems.Text);
                    var findFilter = filterCollection.Find(f => f.Name == hdfConditionSelecting.Text);
                    if (findFilter != null)
                    {
                        storeFilterValue.DataSource = findFilter.Items;
                        storeFilterValue.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex);
            }

        }

        #region Private Methods

        /// <summary>
        /// set visible filter
        /// </summary>
        private void SetVisibleFilter()
        {
            var paramFilter = Request.QueryString["filter"];
            var arrFilter = string.IsNullOrEmpty(paramFilter) ? new string[] { }
                : paramFilter.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            // cbo select time sheet
            if (arrFilter.Contains("TimeSheet"))
                cboTimeSheetReport.Visible = true;

            // cbo select payroll
            if (arrFilter.Contains("Payroll"))
                cboPayroll.Visible = true;

            // start date field
            if (arrFilter.Contains("FromDate"))
                dfStartDate.Visible = true;

            // end date field
            if (arrFilter.Contains("ToDate"))
                dfEndDate.Visible = true;

            // report date field
            if (arrFilter.Contains("ReportDate"))
                dfReportDate.Visible = true;

            if (arrFilter.Contains("Department"))
                txtSelectedDepartments.Visible = true;
        }

        /// <summary>
        /// Load department tree
        /// </summary>
        private void LoadDepartment()
        {
            // init root, add into tree
            var nodeRoot = new TreeNode();
            pnlTreeDepartments.Root.Clear();
            pnlTreeDepartments.Root.Add(nodeRoot);
            // init node all department, add into tree
            var nodeAllDepartment = new TreeNode
            {
                Text = @"Danh mục đơn vị",
                NodeID = "0",
                Icon = Icon.Group,
                Expanded = true
            };
            nodeRoot.Nodes.Add(nodeAllDepartment);
            // add root department into tree
            var lstRootDepartments = CurrentUser.Departments.Where(d => d.ParentId == 0).OrderBy(d => d.Order);
            foreach (var d in lstRootDepartments)
            {
                var node = new TreeNode
                {
                    Text = d.Name,
                    NodeID = d.Id.ToString(),
                    Icon = Icon.BulletBlue,
                    Expanded = true,
                    Checked = ThreeStateBool.False
                };
                nodeAllDepartment.Nodes.Add(node);
                PopulateDepartment(ref node);
            }
        }

        /// <summary>
        /// Populate tree node
        /// </summary>
        /// <param name="currentNode"></param>
        private void PopulateDepartment(ref TreeNode currentNode)
        {
            var parentId = Convert.ToInt32(currentNode.NodeID);
            var childDepartments = CurrentUser.Departments.Where(d => d.ParentId == Convert.ToInt32(parentId)).OrderBy(d => d.Order);
            foreach (var d in childDepartments)
            {
                var node = new TreeNode
                {
                    Text = d.Name,
                    NodeID = d.Id.ToString(),
                    Icon = Icon.BulletBlue,
                    Expanded = true,
                    Checked = ThreeStateBool.False
                };
                currentNode.Nodes.Add(node);
                PopulateDepartment(ref node);
            }
        }

        /// <summary>
        /// Init report from type
        /// </summary>
        /// <returns></returns>
        private XtraReport InitReport(int reportId)
        {
            // get report from database
            var report = ReportDynamicController.GetById(reportId);

            // check result
            if (report != null)
            {
                // init XtraReport
                XtraReport xReport;

                switch (report.Template)
                {
                    case ReportTemplate.EnterprisePayroll:
                        // return payroll
                        xReport = InitPayrollReport(report);
                        break;
                    default:
                        // invalid report template
                        return null;
                }

                // set window filter props
                txtReportTitle.Text = report.Title;
                txtSelectedDepartments.Text = hdfSelectedDepartmentNames.Text;
                dfReportDate.SelectedDate = report.ReportDate;
                dfStartDate.SelectedDate = report.StartDate;
                dfEndDate.SelectedDate = report.EndDate;
                if (string.IsNullOrEmpty(txtCreatedByTitle.Text))
                    txtCreatedByTitle.Text = report.CreatedByTitle;
                if (string.IsNullOrEmpty(txtCreatedByNote.Text))
                    txtCreatedByNote.Text = report.CreatedByNote;
                if (string.IsNullOrEmpty(txtCreatedByName.Text))
                    txtCreatedByName.Text = report.CreatedByName;
                if (string.IsNullOrEmpty(txtReviewedByTitle.Text))
                    txtReviewedByTitle.Text = report.ReviewedByTitle;
                if (string.IsNullOrEmpty(txtReviewedByNote.Text))
                    txtReviewedByNote.Text = report.ReviewedByNote;
                if (string.IsNullOrEmpty(txtReviewedByName.Text))
                    txtReviewedByName.Text = report.ReviewedByName;
                if (string.IsNullOrEmpty(txtSignedByTitle.Text))
                    txtSignedByTitle.Text = report.SignedByTitle;
                if (string.IsNullOrEmpty(txtSignedByNote.Text))
                    txtSignedByNote.Text = report.SignedByNote;
                if (string.IsNullOrEmpty(txtSignedByName.Text))
                    txtSignedByName.Text = report.SignedByName;

                // return
                return xReport;
            }

            // invalid report id
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private XtraReport InitPayrollReport(ReportDynamicModel report)
        {
            // check payroll config id
            if (!int.TryParse(report.Argument, out var payrollConfigId) || payrollConfigId <= 0)
            {
                // invalid argument
                return null;
            }

            // set current payroll config id
            hdfPayrollConfigId.Text = payrollConfigId.ToString();

            // check payroll id
            if (!int.TryParse(hdfPayrollId.Text, out var payrollId) || payrollId <= 0)
            {
                // get last payroll
                var payrolls = PayrollController.GetAll(null, payrollConfigId, hdfSelectedDepartmentIds.Text, null, null, null, false, null, 1);

                // check result
                if (payrolls.Count > 0)
                {
                    // set current payroll id
                    hdfPayrollId.Text = payrolls.First().Id.ToString();

                    // payroll selected
                    cboPayroll.Text = payrolls.First().Title;
                }
                else
                {
                    // no payroll found
                    return null;
                }
            }
            else
            {
                // get payroll by id
                var payroll = PayrollController.GetById(payrollId);

                if (payroll != null)
                {
                    // get month and year
                    var payrollMonth = payroll.Month;
                    var payrollYear = payroll.Year;

                    // set payroll start date
                    report.StartDate = new DateTime(payrollYear, payrollMonth, 1);

                    // set payroll end date
                    report.EndDate = new DateTime(payrollYear, payrollMonth, DateTime.DaysInMonth(payrollYear, payrollMonth));

                    // payroll selected
                    cboPayroll.Text = payroll.Title;
                }
                else
                {
                    // no payroll found
                    return null;
                }
            }

            // init payroll report
            XtraReport rpHRM_Payroll = new rpHRM_Payroll_A4_Landscape(report.Id, payrollId);

            // check paper kind and orientation
            switch (report.PaperKind)
            {
                case ReportPaperKind.A4:
                    switch (report.Orientation)
                    {
                        case ReportOrientation.Landscape:
                            rpHRM_Payroll = new rpHRM_Payroll_A4_Landscape(report.Id, payrollId);
                            break;
                        case ReportOrientation.Portrait:
                            rpHRM_Payroll = new rpHRM_Payroll_A4_Potrait(report.Id, payrollId);
                            break;
                    }
                    break;
                case ReportPaperKind.A3:
                    switch (report.Orientation)
                    {
                        case ReportOrientation.Landscape:
                            rpHRM_Payroll = new rpHRM_Payroll_A3_Landscape(report.Id, payrollId);
                            break;
                        case ReportOrientation.Portrait:
                            rpHRM_Payroll = new rpHRM_Payroll_A3_Potrait(report.Id, payrollId);
                            break;
                    }
                    break;
                case ReportPaperKind.A2:
                    switch (report.Orientation)
                    {
                        case ReportOrientation.Landscape:
                            rpHRM_Payroll = new rpHRM_Payroll_A2_Landscape(report.Id, payrollId);
                            break;
                        case ReportOrientation.Portrait:
                            rpHRM_Payroll = new rpHRM_Payroll_A2_Landscape(report.Id, payrollId);
                            break;
                    }
                    break;
            }

            // init extra report
            return rpHRM_Payroll;
        }

        /// <summary>
        /// Create report
        /// </summary>
        /// <returns></returns>
        private XtraReport CreateReport(int reportId)
        {
            // init report
            var report = InitReport(reportId);

            // check initialed report
            if (report == null) return null;

            //set visible filter
            SetVisibleFilter();

            // get report filter
            var filter = ((IBaseReport)report).GetFilter();

            // set current report filter to client
            hdfFilterItems.Text = JSON.Serialize(filter.Items);

            // get selected filter
            if (!string.IsNullOrEmpty(hdfConditionSelected.Text))
            {
                var lstAllSelectedCondition = JSON.Deserialize<List<SelectedFilterCondition>>(hdfConditionSelected.Text);
                foreach (var item in filter.Items)
                {
                    var lstSelectedConditionForItem = lstAllSelectedCondition.Where(o => o.FilterItemName == item.Name).ToList();
                    if (lstSelectedConditionForItem.Count > 0)
                    {
                        foreach (var selectedCondition in lstSelectedConditionForItem)
                        {
                            item.SelectedItems.Add(new Core.Framework.Report.FilterCondition(selectedCondition.ConditionName, selectedCondition.ConditionClause));
                        }
                    }
                }
            }

            // init department
            var departments = string.IsNullOrEmpty(hdfSelectedDepartmentIds.Text)
                ? string.Join(",", CurrentUser.Departments.Select(d => d.Id))
                : hdfSelectedDepartmentIds.Text;

            // init report date
            var reportDate = dfReportDate.SelectedDate != DateTime.MinValue
                ? dfReportDate.SelectedDate
                : DateTime.Now;

            // init start date
            DateTime? startDate = null;
            var qStartDate = Request.QueryString["startDate"];
            if (!string.IsNullOrEmpty(qStartDate) && DateTime.TryParseExact(qStartDate, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var parseStartDate))
                startDate = parseStartDate;
            if (dfStartDate.SelectedDate != DateTime.MinValue)
                startDate = dfStartDate.SelectedDate;

            // init end date
            DateTime? endDate = null;
            var qEndDate = Request.QueryString["endDate"];
            if (!string.IsNullOrEmpty(qEndDate) && DateTime.TryParseExact(qEndDate, "yyyy-MM-dd",
                   CultureInfo.InvariantCulture, DateTimeStyles.None, out var parseEndDate))
                endDate = parseEndDate;
            if (dfEndDate.SelectedDate != DateTime.MinValue)
                endDate = dfEndDate.SelectedDate;

            // set filter props
            filter.Departments = departments;
            filter.ReportDate = reportDate;
            filter.StartDate = startDate;
            filter.EndDate = endDate;
            filter.ReportTitle = txtReportTitle.Text;
            filter.CreatedByTitle = txtCreatedByTitle.Text;
            filter.CreatedByName = txtCreatedByName.Text;
            filter.CreatedByNote = txtCreatedByNote.Text;
            filter.ReviewedByTitle = txtReviewedByTitle.Text;
            filter.ReviewedByNote = txtReviewedByNote.Text;
            filter.ReviewedByName = txtReviewedByName.Text;
            filter.SignedByTitle = txtSignedByTitle.Text;
            filter.SignedByNote = txtSignedByNote.Text;
            filter.SignedByName = txtSignedByName.Text;
            filter.TimeSheetReportId = !string.IsNullOrEmpty(hdfTimeSheetReport.Text)
                ? Convert.ToInt32(hdfTimeSheetReport.Text) : 0;
            filter.RecordIds = hdfSelectedEmployees.Text;

            // update filter report
            ((IBaseReport)report).SetFilter(filter);

            // bind data
            ((IBaseReport)report).BindData();

            // return report
            return report;
        }
    }

    #endregion
}