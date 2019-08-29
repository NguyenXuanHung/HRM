using System;
using System.Collections.Generic;
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
            if(!X.IsAjaxRequest)
            {
                // load department
                LoadDepartment();

                //init
                hdfTypeTimeSheet.Text = Constant.TimesheetTypeTimeSheet;
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));

                // create report
                var report = CreateReport();

                // check create result
                if(report != null)
                {
                    // create report success
                    reportViewer.Report = report;

                    // init store filter name
                    if(!string.IsNullOrEmpty(hdfFilterItems.Text))
                    {
                        var filterCollection = JSON.Deserialize<List<FilterItem>>(hdfFilterItems.Text);
                        storeFilterName.DataSource = filterCollection;
                        storeFilterName.DataBind();
                    }

                    // init store filter selected
                    if(!string.IsNullOrEmpty(hdfConditionSelected.Text))
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
            if(!string.IsNullOrEmpty(hdfFilterItems.Text))
            {
                var filterCollection = JSON.Deserialize<List<FilterItem>>(hdfFilterItems.Text);
                var findFilter = filterCollection.Find(f => f.Name == hdfConditionSelecting.Text);
                if(findFilter != null)
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
            foreach(var d in lstRootDepartments)
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
            foreach(var d in childDepartments)
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
            if(Enum.TryParse(Request.QueryString["rp"], true, out ReportTypeHJM rpType))
            {
                // init report
                XtraReport report;
                switch(rpType)
                {
                    //Regulation                    
                    case ReportTypeHJM.QuantityDistrictCivilServants:
                        report = new rpHJM_QuantityDistrictCivilServants();
                        break;

                    case ReportTypeHJM.QuantityCommuneCivilServants:
                        report = new rpHJM_QuantityCommuneCivilServants();
                        break;

                    case ReportTypeHJM.SalaryDistrictCivilServants:
                        report = new rpHJM_SalaryDistrictCivilServants();
                        break;

                    case ReportTypeHJM.SalaryCommuneCivilServants:
                        report = new rpHJM_SalaryCommuneCivilServants();
                        break;

                    case ReportTypeHJM.QuantityDistrictCivilServantsDetail:
                        report = new rpHJM_QuantityDistrictCivilServantsDetail();
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

                    case ReportTypeHJM.ListEmployeeByPosition:
                        report = new rpHJM_ListEmployeeByPosition();
                        break;

                    //Business
                    case ReportTypeHJM.EmployeeList:
                        report = new rpHJM_EmployeeList();
                        break;
                    case ReportTypeHJM.PartyMember:
                        report = new rpHJM_PartyMember();
                        break;   

                    case ReportTypeHJM.ContractsOfEmployee:
                        report = new rpHJM_ContractsOfEmployee();
                        break;

                    case ReportTypeHJM.EmployeeExpried:
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

                    case ReportTypeHJM.EmployeeSecondmentTo:
                        report = new rpHJM_EmployeeSecondmentTo();
                        break;

                    case ReportTypeHJM.EmployeeSecondmentFrom:
                        report = new rpHJM_EmployeeSecondmentFrom();
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

                    case ReportTypeHJM.EmployeeFostering:
                        report = new rpHJM_EmployeeFostering();
                        break;

                    //Salary                 
                    case ReportTypeHJM.SalaryIncrease:
                        report = new rpHJM_SalaryIncrease();
                        break;

                    case ReportTypeHJM.SalaryIncreaseProcess:
                        report = new rpHJM_SalaryIncreaseProcess();
                        break;
                    case ReportTypeHJM.SalaryOutOfFrame:
                        report = new rpHJM_SalaryOutOfFrame();
                        break;
                    //Training - Onsite
                    case ReportTypeHJM.EmployeeTraining:
                        report = new rpHJM_EmployeeTraining();
                        break;

                    case ReportTypeHJM.EmployeeOnsite:
                        report = new rpHJM_EmployeeOnsite();
                        break;
                    //Rewared - Disciplined
                    case ReportTypeHJM.EmployeeReceivedAward:
                        report = new rpHJM_EmployeeReceivedAward();
                        break;

                    case ReportTypeHJM.EmployeeRewarded:
                        report = new rpHJM_EmployeeRewarded();
                        break;

                    case ReportTypeHJM.EmployeeDisciplined:
                        report = new rpHJM_EmployeeDisciplined();
                        break;
                    //Curriculum
                    case ReportTypeHJM.CurriculumVitae:
                        report = new rp_PrintCurriculumVitae();
                        break;

                    default:
                        // invalid report type
                        return null;
                }
                // return
                return report;
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
            if(report == null) return null;

            //set visible filter
            SetVisibleFilter();

            // get report filter
            var filter = ((IBaseReport)report).GetFilter();
            // set current report filter to client
            hdfFilterItems.Text = JSON.Serialize(filter.Items);
            // get selected filter
            if(!string.IsNullOrEmpty(hdfConditionSelected.Text))
            {
                var lstAllSelectedCondition = JSON.Deserialize<List<SelectedFilterCondition>>(hdfConditionSelected.Text);
                foreach(var item in filter.Items)
                {
                    var lstSelectedConditionForItem = lstAllSelectedCondition.Where(o => o.FilterItemName == item.Name).ToList();
                    if(lstSelectedConditionForItem.Count > 0)
                    {
                        foreach(var selectedCondition in lstSelectedConditionForItem)
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
            if(dfStartDate.SelectedDate != DateTime.MinValue)
                startDate = dfStartDate.SelectedDate;
            // init end date
            DateTime? endDate = null;
            if(dfEndDate.SelectedDate != DateTime.MinValue)
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
            filter.RecordId = !string.IsNullOrEmpty(hdfEmployeeSelectedId.Text)
                ? Convert.ToInt32(hdfEmployeeSelectedId.Text)
                : 0;
            filter.OrganizationName = CurrentUser.RootDepartment.Name;
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
            // cbo select timesheet
            if (arrFilter.Contains("TimeSheet"))
            {
                cbxTimeSheetReport.Visible = true;
            }

            //cbo select employee
            if (arrFilter.Contains("ChooseEmployee"))
            {
                cbxSelectedEmployee.Visible = true;
            }
        }
    }

    #endregion
}