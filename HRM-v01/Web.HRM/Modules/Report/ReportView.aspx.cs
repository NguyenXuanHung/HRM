using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DevExpress.XtraReports.UI;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Common;
using Web.Core.Framework.Interface;
using Web.Core.Framework.Report;
using Web.Core.Framework.Report.HJM.Shared.Business;
using FilterCondition = Web.Core.Framework.Report.FilterCondition;
using TreeNode = Ext.Net.TreeNode;

namespace Web.HRM.Modules.Report
{
    public partial class ReportView : BasePage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                // load department
                LoadDepartment();

                // create report
                var report = CreateReport();

                // check create result
                if (report != null)
                {
                    // create report success
                    reportViewer.Report = report;

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
            ucChooseEmployee.AfterClickAcceptButton += new EventHandler(ucChooseEmployee_AfterClickAcceptButton);
        }

        /// <summary>
        /// Refresh value for selecting filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeFilterValue_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
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

        #region Private Methods

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="currNode"></param>
        private void PopulateDepartment(ref TreeNode currNode)
        {
            var parentId = Convert.ToInt32(currNode.NodeID);
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
                currNode.Nodes.Add(node);
                PopulateDepartment(ref node);
            }
        }

        /// <summary>
        /// Init report from type
        /// </summary>
        /// <returns></returns>
        private XtraReport InitReport()
        {
            var typeReport = Request.QueryString["type"];
            if (!string.IsNullOrEmpty(typeReport) && typeReport == ReportKindType.HJM.ToString())
            {
                if (Enum.TryParse(Request.QueryString["rp"], true, out ReportTypeHJM rpType))
                {
                    // init report
                    XtraReport report;
                    switch (rpType)
                    {
                        #region Regulation
                        case ReportTypeHJM.QuantityDistrictCivilServants:
                            report = new rpHJM_QuantityDistrictCivilServants();
                            break;
                        case ReportTypeHJM.QuantityCommuneCivilServants:
                            report = new rpHJM_QuantityCommuneCivilServants();
                            break;
                        case ReportTypeHJM.SalaryDistrictCivilServants:
                            report = new rpHJM_QuantityCommuneCivilServants();
                            break;
                        case ReportTypeHJM.SalaryCommuneCivilServants:
                            report = new rpHJM_SalaryCommuneCivilServants();
                            break;
                        case ReportTypeHJM.QuantityOfEmployee:
                            report = new rpHJM_QuantityOfEmployee();
                            break;
                        case ReportTypeHJM.QuantityFemaleEmployee:
                            report = new rpHJM_QuantityFemaleEmployee();
                            break;
                        case ReportTypeHJM.QuantityEthnicMinority:
                            report = new rpHJM_QuantityEthnicMinority();
                            break;
                        case ReportTypeHJM.QuantityStaff:
                            report = new rpHJM_QuantityStaff();
                            break;
                        case ReportTypeHJM.QuantityDistrictCivilServantsDetail:
                            report = new rpHJM_QuantityDistrictCivilServantsDetail();
                            break;
                        case ReportTypeHJM.ListEmployeeByPosition:
                            report = new rpHJM_ListEmployeeByPosition();
                            break;
                        #endregion

                        #region Business
                        case ReportTypeHJM.EmployeeList:
                            report = new rpHJM_EmployeeList();
                            break;
                        case ReportTypeHJM.EmployeeMoveTo:
                            report = new rpHJM_EmployeeMoveTo();
                            break;
                        case ReportTypeHJM.EmployeeMoveFrom:
                            report = new rpHJM_EmployeeMoveFrom();
                            break;
                        case ReportTypeHJM.EmployeeTurnoverTo:
                            report = new rpHJM_EmployeeTurnoverTo();
                            break;
                        case ReportTypeHJM.EmployeeTurnoverFrom:
                            report = new rpHJM_EmployeeTurnoverFrom();
                            break;
                        case ReportTypeHJM.EmployeeSecondmentFrom:
                            report = new rpHJM_EmployeeSecondmentFrom();
                            break;
                        case ReportTypeHJM.EmployeeSecondmentTo:
                            report = new rpHJM_EmployeeSecondmentTo();
                            break;
                        case ReportTypeHJM.EmployeePlurality:
                            report = new rpHJM_EmployeePlurality();
                            break;
                        case ReportTypeHJM.EmployeeDismissment:
                            report = new rpHJM_EmployeeDismissment();
                            break;
                        case ReportTypeHJM.EmployeeTranfer:
                            report = new rpHJM_EmployeeTranfer();
                            break;
                        case ReportTypeHJM.EmployeeSeniority:
                            report = new rpHJM_EmployeeSeniority();
                            break;
                        case ReportTypeHJM.EmployeeCompensation:
                            report = new rpHJM_EmployeeCompensation();
                            break;
                        case ReportTypeHJM.EmployeeRetirement:
                            report = new rpHJM_EmployeeRetirement();
                            break;
                        case ReportTypeHJM.PartyMember:
                            report = new rpHJM_PartyMember();
                            break;
                        case ReportTypeHJM.ContractsOfEmployee:
                            report = new rpHJM_ContractsOfEmployee();
                            break;

                        case ReportTypeHJM.EmployeeExpired:
                            report = new rpHJM_EmployeeExpired();
                            break;
                        case ReportTypeHJM.EmployeeAssigned:
                            report = new rpHJM_EmployeeAssigned();
                            break;
                        case ReportTypeHJM.BornInMonth:
                            report = new rpHJM_BornInMonth();
                            break;
                        case ReportTypeHJM.UnionMember:
                            report = new rpHJM_UnionMember();
                            break;
                        case ReportTypeHJM.MilitaryList:
                            report = new rpHJM_MilitaryList();
                            break;
                        case ReportTypeHJM.EmployeeByDepartment:
                            report = new rpHJM_EmployeeByDepartment();
                            break;
                        case ReportTypeHJM.EmployeeFostering:
                            report = new rpHJM_EmployeeFostering();
                            break;

                        #endregion

                        #region Salary
                        case ReportTypeHJM.SalaryIncreaseProcess:
                            report = new rpHJM_SalaryIncreaseProcess();
                            break;
                        case ReportTypeHJM.SalaryIncrease:
                            report = new rpHJM_SalaryIncrease();
                            break;
                        case ReportTypeHJM.SalaryOutOfFrame:
                            report = new rpHJM_SalaryOutOfFrame();
                            break;

                        #endregion

                        #region Training - Onsite
                        case ReportTypeHJM.EmployeeTraining:
                            report = new rpHJM_EmployeeTraining();
                            break;
                        case ReportTypeHJM.EmployeeOnsite:
                            report = new rpHJM_EmployeeOnsite();
                            break;

                        #endregion

                        #region Rewared - Disciplined
                        case ReportTypeHJM.EmployeeReceivedAward:
                            report = new rpHJM_EmployeeReceivedAward();
                            break;
                        case ReportTypeHJM.EmployeeRewarded:
                            report = new rpHJM_EmployeeRewarded();
                            break;
                        case ReportTypeHJM.EmployeeDisciplined:
                            report = new rpHJM_EmployeeDisciplined();
                            break;


                        #endregion

                        default:
                        // invalid report type
                        return null;
                    }

                    // return
                    return report;
                }
            }
            else
            {
                if (Enum.TryParse(Request.QueryString["rp"], true, out ReportTypeHRM rpType))
                {
                    // init report
                    XtraReport report;
                    switch (rpType)
                    {

                        // Regulation
                        case ReportTypeHRM.LabourList:
                            report = new rpHRM_LabourList();
                            break;
                        case ReportTypeHRM.LabourIncrease:
                            report = new rpHRM_LabourIncrease();
                            break;
                        case ReportTypeHRM.LabourDecrease:
                            report = new rpHRM_LabourDecrease();
                            break;
                        case ReportTypeHRM.EmployeeList:
                            report = new rpHRM_EmployeeList();
                            break;
                        case ReportTypeHRM.EmployeeIncrease:
                            report = new rpHRM_EmployeeIncrease();
                            break;
                        case ReportTypeHRM.EmployeeDecrease:
                            report = new rpHRM_EmployeeDecrease();
                            break;
                        case ReportTypeHRM.EmployeeTransferred:
                            report = new rpHRM_EmployeeTransferred();
                            break;
                        case ReportTypeHRM.EmployeeSent:
                            report = new rpHRM_EmployeeSent();
                            break;
                        case ReportTypeHRM.EmployeeAssigned:
                            report = new rpHRM_EmployeeAssigned();
                            break;
                        case ReportTypeHRM.EmployeeSeniority:
                            report = new rpHRM_EmployeeSeniority();
                            break;
                        case ReportTypeHRM.EmployeeRetired:
                            report = new rpHRM_EmployeeRetired();
                            break;
                        // Business
                        case ReportTypeHRM.PartyMember:
                            report = new rpHRM_PartyMember();
                            break;
                        case ReportTypeHRM.UnionMember:
                            report = new rpHRM_UnionMember();
                            break;
                        case ReportTypeHRM.MilitaryList:
                            report = new rpHRM_MilitaryList();
                            break;
                        case ReportTypeHRM.ContractsOfEmployee:
                            report = new rpHRM_ContractsOfEmployee();
                            break;
                        case ReportTypeHRM.EmployeeExpired:
                            report = new rpHRM_EmployeeExpired();
                            break;
                        case ReportTypeHRM.EmployeeTrainee:
                            report = new rpHRM_EmployeeTrainee();
                            break;
                        case ReportTypeHRM.InsuranceList:
                            report = new rpHRM_InsuranceList();
                            break;
                        case ReportTypeHRM.InsurancIncrease:
                            report = new rpHRM_InsuranceIncrease();
                            break;
                        case ReportTypeHRM.InsuranceDecrease:
                            report = new rpHRM_DecreaseInsurance();
                            break;
                        case ReportTypeHRM.OccupationStatistics:
                            report = new rpHRM_OccupationStatistics();
                            break;
                        case ReportTypeHRM.BornInMonth:
                            report = new rpHRM_BornInMonth();
                            break;
                        case ReportTypeHRM.ChildernDayGift:
                            report = new rpHRM_ChildernDayGift();
                            break;
                        case ReportTypeHRM.EmployeeFemale:
                            report = new rpHRM_EmployeeFemale();
                            break;
                        // Salary
                        case ReportTypeHRM.EmployeeSalary:
                            report = new rpHRM_EmployeeSalary();
                            break;
                        case ReportTypeHRM.SalaryOutOfFrame:
                            report = new rpHRM_SalaryOutOfFrame();
                            break;
                        case ReportTypeHRM.SalaryIncreaseProcess:
                            report = new rpHRM_SalaryIncreaseProcess();
                            break;
                        case ReportTypeHRM.SalaryIncrease:
                            report = new rpHRM_SalaryIncrease();
                            break;
                        case ReportTypeHRM.EmployeeBank:
                            report = new rpHRM_EmployeeBank();
                            break;
                        case ReportTypeHRM.EmployeeTaxCode:
                            report = new rpHRM_EmployeeTaxCode();
                            break;
                        case ReportTypeHRM.DependentPerson:
                            report = new rpHRM_DependentPerson();
                            break;
                        // Training - On-site
                        case ReportTypeHRM.EmployeeTraining:
                            report = new rpHRM_EmployeeTraining();
                            break;
                        case ReportTypeHRM.EmployeeOnsite:
                            report = new rpHRM_EmployeeOnsite();
                            break;
                        // Rewarded - Disciplined
                        case ReportTypeHRM.EmployeeReceivedAward:
                            report = new rpHRM_EmployeeReceivedAward();
                            break;
                        case ReportTypeHRM.EmployeeRewarded:
                            report = new rpHRM_EmployeeRewarded();
                            break;
                        case ReportTypeHRM.EmployeeDisciplined:
                            report = new rpHRM_EmployeeDisciplined();
                            break;
                        // TimeKeeping
                        case ReportTypeHRM.TotalTimeKeeping:
                            report = new rpHRM_TimeKeepingOffice();
                            break;
                        case ReportTypeHRM.TimeKeepingDetail:
                            report = new rpHRM_TimeKeepingOfficeDetail();
                            break;
                        case ReportTypeHRM.TimeKeepingOverTimeDetail:
                            report = new rpHRM_OvertimeDetail();
                            break;
                        default:
                            // invalid report type
                            return null;
                    }
                    // return
                    return report;
                }

            }
            // invalid report type
            return null;
        }

        /// <summary>
        /// Create report
        /// </summary>
        /// <returns></returns>
        private XtraReport CreateReport()
        {
            // init report
            var report = InitReport();

            // check report inited
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
                            item.SelectedItems.Add(new FilterCondition(selectedCondition.ConditionName, selectedCondition.ConditionClause));
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
            filter.ReviewedByTitle = txtReviewedByTitle.Text;
            filter.ReviewedByName = txtReviewedByName.Text;
            filter.SignedByTitle = txtSignedByTitle.Text;
            filter.SignedByName = txtSignedByName.Text;
            filter.TimeSheetReportId = !string.IsNullOrEmpty(hdfTimeSheetReport.Text)
                ? Convert.ToInt32(hdfTimeSheetReport.Text)
                : 0;
            filter.RecordIds = hdfSelectedEmployees.Text;
            // update filter report
            ((IBaseReport)report).SetFilter(filter);

            // bind data
            ((IBaseReport)report).BindData();

            // return report
            return report;
        }

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
                cbxTimeSheetReport.Visible = true;

            // start date field
            if (arrFilter.Contains("FromDate"))
                dfStartDate.Visible = true;

            // end date field
            if (arrFilter.Contains("ToDate"))
                dfEndDate.Visible = true;

            // report date field
            if (arrFilter.Contains("ReportDate"))
                dfReportDate.Visible = true;

            // department field
            if (arrFilter.Contains("Department"))
                txtSelectedDepartments.Visible = true;

            if (arrFilter.Contains("Employee"))
                txtSelectedEmployee.Visible = true;
        }

        private void ucChooseEmployee_AfterClickAcceptButton(object sender, EventArgs e)
        {
            var fullNames = string.Empty;
            var recordIds = string.Empty;
            foreach (var item in ucChooseEmployee.SelectedRow)
            {
                // get employee information
                var hs = RecordController.GetById(Convert.ToInt32(item.RecordID));
                fullNames += "," + hs.FullName;
                recordIds += "," + hs.Id;
            }

            txtSelectedEmployee.Text = fullNames.TrimStart(',').TrimEnd(',');
            hdfSelectedEmployees.Text = recordIds.TrimStart(',').TrimEnd(',');
        }
    }

    #endregion
}