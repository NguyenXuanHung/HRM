using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Framework.Adapter;
using Web.Core.Framework.Interface;

namespace Web.Core.Framework.Report.HJM.Shared.Business
{
    public partial class rpHJM_EmployeeFostering : XtraReport, IBaseReport
    {
        private string CONST_BUSINESS_TYPE = "PlanJobTitle";
       
        #region Init

        private const string _reportTitle = "DANH SÁCH ĐỐI TƯỢNG BỒI DƯỠNG CÁN BỘ NGUỒN TỈNH ỦY QUẢN LÝ";

        /// <summary>
        /// Filter data
        /// </summary>
        private Filter _filter;

        /// <summary>
        /// Get Filter, implement IBaseReport interface
        /// </summary>
        /// <returns></returns>
        public Filter GetFilter()
        {
            return _filter;
        }

        /// <summary>
        /// Set Filter, implement IBaseReport interface
        /// </summary>
        /// <param name="filter"></param>
        public void SetFilter(Filter filter)
        {
            _filter = filter;
        }

        private void InitFilter()
        {
            // init filter
            _filter = new Filter
            {
                ReportTitle = _reportTitle,
                Items = new List<FilterItem>()
            };
        }

        public rpHJM_EmployeeFostering()
        {
            //container.Add(this);

            // init compoent
            InitializeComponent();

            // init Filter
            InitFilter();
        }

        #endregion

        int _index = 1;
        int _index2 = 1;
        int _index3 = 1;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCellIndex.Text = _index.ToString();
            _index++;
        }
        private void Group_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _index = 1;
            xrTableCellIndex.Text = _index.ToString();
            xrTableCellGroupIndex.Text = _index2.ToString();
            _index2++;
        }
        private void Department_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _index = 1;
            _index2 = 1;
            xrTableCellIndex.Text = _index.ToString();
            xrTableCellGroupIndex.Text = _index2.ToString();
            xrTableCellDepartmentIndex.Text = _index3.ToString();
            _index3++;
        }
        public void BindData()
        {
            // from date
            var fromDate = _filter.StartDate != null ? _filter.StartDate.Value.ToString("yyyy-MM-dd 00:00:00") : string.Empty;
            // to date
            var toDate = _filter.EndDate != null ? _filter.EndDate.Value.ToString("yyyy-MM-dd 23:59:59") : string.Empty;
            // select form db 
            var table = SQLHelper.ExecuteTable(SQLReportAdapter.GetStore_EmployeeFostering(string.Join(",", _filter.Departments.Split(
                new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(d => "'{0}'".FormatWith(d))), fromDate, toDate, CONST_BUSINESS_TYPE, _filter.Condition));

            DataSource = table;

            GroupHeader1.GroupFields.AddRange(new[] { new GroupField("DepartmentId", XRColumnSortOrder.Ascending) });
            xrDepartmentGroupHeader.DataBindings.Add("Text", DataSource, "DepartmentName");
            GroupHeader2.GroupFields.AddRange(new[] { new GroupField("JobTitleId", XRColumnSortOrder.Ascending) });
            xrPositionGroupHeader.DataBindings.Add("Text", DataSource, "JobTitleName");
            xrTableCellFullname.DataBindings.Add("Text", DataSource, "FullName");
            xrTableCellBirthday.DataBindings.Add("Text", DataSource, "BirthDate", "{0:dd/MM/yyyy}");
            xrTableCellSex.DataBindings.Add("Text", DataSource, "SexName");
            xrTableCellCPVMember.DataBindings.Add("Text", DataSource, "CPVPositionName");
            xrTableCellFolk.DataBindings.Add("Text", DataSource, "FolkName");
            xrTableCellEducation.DataBindings.Add("Text", DataSource, "EducationName");
            xrTableCellPoliticLevel.DataBindings.Add("Text", DataSource, "PoliticLevelName");
            xrTableCellLanguageLevel.DataBindings.Add("Text", DataSource, "LanguageLevelName");
            xrTableCellITLevel.DataBindings.Add("Text", DataSource, "ITLevelName");
            xrTableCellManagementLevel.DataBindings.Add("Text", DataSource, "ManagementLevelName");
            xrTableCellPlanJobTitle.DataBindings.Add("Text", DataSource, "PlanJobTitleName");
            xrTableCellPlanPhase.DataBindings.Add("Text", DataSource, "PlanPhaseName");
            xrTableCellTrainningSystem.DataBindings.Add("Text", DataSource, "TrainingSystemName");
            xrTableCellPositionDepartment.DataBindings.Add("Text", DataSource, "PositionDepartmentName");
            // label org name
            if (!string.IsNullOrEmpty(_filter.OrganizationName)) lblOrgName.Text = _filter.OrganizationName;
            // label report title
            if (!string.IsNullOrEmpty(_filter.ReportTitle)) lblReportTitle.Text = _filter.ReportTitle;
            // lablel duration
            if (_filter.StartDate != null && _filter.EndDate != null)
            {
                lblDuration.Text = lblDuration.Text.FormatWith(_filter.StartDate.Value.ToString("dd/MM/yyyy"),
                    _filter.EndDate.Value.ToString("dd/MM/yyyy"));
            }
            else
            {
                lblDuration.Text = string.Empty;
            }
            // label report date
            lblReportDate.Text = lblReportDate.Text.FormatWith(_filter.ReportDate.Day, _filter.ReportDate.Month, _filter.ReportDate.Year);
            // created by
            if (!string.IsNullOrEmpty(_filter.CreatedByTitle)) lblCreatedByTitle.Text = _filter.CreatedByTitle;
            if (!string.IsNullOrEmpty(_filter.CreatedByName)) lblCreatedByName.Text = _filter.CreatedByName;
            // reviewed by
            if (!string.IsNullOrEmpty(_filter.ReviewedByTitle)) lblReviewedByTitle.Text = _filter.ReviewedByTitle;
            if (!string.IsNullOrEmpty(_filter.ReviewedByName)) lblReviewedByName.Text = _filter.ReviewedByName;
            // signed by
            if (!string.IsNullOrEmpty(_filter.SignedByTitle)) lblSignedByTitle.Text = _filter.SignedByTitle;
            if (!string.IsNullOrEmpty(_filter.SignedByName)) lblSignedByName.Text = _filter.SignedByName;
        }
    }
}
