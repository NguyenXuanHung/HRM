using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using DevExpress.XtraReports.UI;
using Web.Core.Framework.Interface;
using Web.Core.Framework.Report.Base;
using Web.Core.Framework.SQLAdapter;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Khai trình lao động
    /// Summary description for rpHRM_LabourList
    /// </summary>
    public class rpHRM_LabourList : XtraReport, IBaseReport
    {
        private DetailBand Detail;
        private TopMarginBand TopMargin;
        private BottomMarginBand BottomMargin;
        private PageHeaderBand PageHeader;
        private ReportHeaderBand ReportHeader;
        private ReportFooterBand ReportFooter;
        private XRLabel lblReportTitle;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCell4;
        private XRTableCell xrTableCell3;
        private XRTableCell xrTableCell5;
        private XRTableCell xrTableCell7;
        private GroupHeaderBand GroupHeader1;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrCellIndex;
        private XRTableCell xrCellEmployeeCode;
        private XRTableCell xrCellFullName;
        private XRTableCell xrCellBirthYear;
        private XRTableCell xrMale;
        private XRTableCell xrFemale;
        private XRTableCell xrCellUniversity;
        private XRTableCell xrCellPosition;
        private XRTableCell xrCellOther;
        private XRLabel lblCreatedByName;
        private XRLabel lblSignedByName;
        private XRLabel lblSignedByTitle;
        private XRLabel lblCreatedByTitle;
        private XRTableCell xrTableCell9;
        private XRTableCell xrCellColleage;
        private XRTable xrTable3;
        private XRTableRow xrTableRow3;
        private XRTableCell xrt_GroupDepartment;
        private XRTableCell xrTableCell8;
        private XRLabel xrLabel4;
        private XRLabel xrLabel3;
        private XRLabel xrLabel1;
        private XRLabel xrLabel2;
        private XRLabel lblDuration;
        private XRTableCell xrCellIntermadiate;
        private XRTableCell xrCellPrimary;
        private XRTableCell xrCellTraining;
        private XRTableCell xrCellNotTraining;
        private XRTableCell xrCellUnlimitedContract;
        private XRTableCell xrCellLimitedContract;
        private XRTableCell xrCellSeasonalContract;
        private XRTableCell xrCellStartDate;
        private XRLabel xrLabel7;
        private XRLabel xrLabel6;
        private XRLabel xrLabel5;
        private XRLabel xrLabel10;
        private XRLabel xrLabel9;
        private XRLabel xrLabel8;
        private XRLabel xrLabel14;
        private XRLabel xrLabel13;
        private XRLabel xrLabel12;
        private XRLabel xrLabel11;
        private XRTableCell xrTableCell10;
        private XRTableCell xrTableCell6;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private XRLabel lblReportDate;

        #region Init

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

        /// <summary>
        /// 
        /// </summary>
        private void InitFilter()
        {
            // init filter
            _filter = new Filter
            {
                Items = new List<FilterItem>()
            };

            // filter sex            
            _filter.Items.Add(FilterGenerate.SexFilter());

            // filter birth year            
            _filter.Items.Add(FilterGenerate.BirthYearFilter());
                                                            
            // filter group contract type
            _filter.Items.Add(FilterGenerate.GroupContractTypeFilter());
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public rpHRM_LabourList()
        {
            // init compoent
            InitializeComponent();

            // init Filter
            InitFilter();
        }

        #endregion

        #region DataBind

        /// <summary>
        /// Init count number
        /// </summary>
        private int _stt = 1;

        /// <summary>
        /// Set count number into row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Detail_BeforePrint(object sender, PrintEventArgs e)
        {
            xrCellIndex.Text = _stt.ToString();
            _stt++;
        }

        /// <summary>
        /// Reset count number from 1 in each group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Group_BeforePrint(object sender, PrintEventArgs e)
        {
            _stt = 1;
            xrCellIndex.Text = _stt.ToString();

        }

        /// <summary>
        /// DataBind
        /// </summary>
        public void BindData()
        {
            try
            {
                // from date
                var fromDate = _filter.StartDate != null ? _filter.StartDate.Value.ToString("yyyy-MM-dd 00:00:00") : string.Empty;
                // to date
                var toDate = _filter.EndDate != null ? _filter.EndDate.Value.ToString("yyyy-MM-dd 23:59:59") : string.Empty;
                // select form db 
                var table = SQLHelper.ExecuteTable(SQLReportRegulationAdapter.GetStore_LaborList(string.Join(",", _filter.Departments.Split(
                        new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(d => "'{0}'".FormatWith(d))), fromDate, toDate, _filter.Condition));
                // bind to data source
                DataSource = table;
                //binding data
                xrCellEmployeeCode.DataBindings.Add("Text", DataSource, "EmployeeCode");
                xrCellFullName.DataBindings.Add("Text", DataSource, "FullName");
                xrCellBirthYear.DataBindings.Add("Text", DataSource, "Year");
                xrCellPosition.DataBindings.Add("Text", DataSource, "PositionName");
                xrCellColleage.DataBindings.Add("Text", DataSource, "College");
                xrCellUniversity.DataBindings.Add("Text", DataSource, "University");
                xrCellPrimary.DataBindings.Add("Text", DataSource, "PrimaryEducation");
                xrCellNotTraining.DataBindings.Add("Text", DataSource, "UnTraining");
                xrMale.DataBindings.Add("Text", DataSource, "SexMale");
                xrFemale.DataBindings.Add("Text", DataSource, "SexFemale");
                xrCellIntermadiate.DataBindings.Add("Text", DataSource, "Intermadiate");
                xrCellTraining.DataBindings.Add("Text", DataSource, "Vocational");
                xrCellUnlimitedContract.DataBindings.Add("Text", DataSource, "IndefinitContract");
                xrCellLimitedContract.DataBindings.Add("Text", DataSource, "TermContract");
                xrCellSeasonalContract.DataBindings.Add("Text", DataSource, "SeasonContract");
                xrCellOther.DataBindings.Add("Text", DataSource, "");
                xrCellStartDate.DataBindings.Add("Text", DataSource, "ParticipationDate", "{0:dd/MM/yyyy}");
                // bind group data
                GroupHeader1.GroupFields.AddRange(new[] {
                new GroupField("DepartmentId", XRColumnSortOrder.Ascending)});
                xrt_GroupDepartment.DataBindings.Add("Text", DataSource, "DepartmentName");
                // other items
                // label report title
                if(!string.IsNullOrEmpty(_filter.ReportTitle)) lblReportTitle.Text = _filter.ReportTitle;
                // lablel duration
                if(_filter.StartDate != null && _filter.EndDate != null)
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
                if(!string.IsNullOrEmpty(_filter.CreatedByTitle)) lblCreatedByTitle.Text = _filter.CreatedByTitle;
                if(!string.IsNullOrEmpty(_filter.CreatedByName)) lblCreatedByName.Text = _filter.CreatedByName;
                // reviewed by
                //if(!string.IsNullOrEmpty(_filter.ReviewedByTitle)) lblReviewedByTitle.Text = _filter.ReviewedByTitle;
                //if(!string.IsNullOrEmpty(_filter.ReviewedByName)) lblReviewedByName.Text = _filter.ReviewedByName;
                // signed by
                if(!string.IsNullOrEmpty(_filter.SignedByTitle)) lblSignedByTitle.Text = _filter.SignedByTitle;
                if(!string.IsNullOrEmpty(_filter.SignedByName)) lblSignedByName.Text = _filter.SignedByName;
            }
            catch(Exception ex)
            {
                Dialog.ShowError(ex);
            }
        }

        #endregion
        
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCellIndex = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellEmployeeCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellFullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellBirthYear = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrMale = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrFemale = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellUniversity = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellColleage = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellIntermadiate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellPrimary = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTraining = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellNotTraining = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellUnlimitedContract = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellLimitedContract = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellSeasonalContract = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellPosition = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellStartDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellOther = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.lblDuration = new DevExpress.XtraReports.UI.XRLabel();
            this.lblReportTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCreatedByName = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSignedByName = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSignedByTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCreatedByTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrt_GroupDepartment = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1146F, 25F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCellIndex,
            this.xrCellEmployeeCode,
            this.xrCellFullName,
            this.xrCellBirthYear,
            this.xrMale,
            this.xrFemale,
            this.xrCellUniversity,
            this.xrCellColleage,
            this.xrCellIntermadiate,
            this.xrCellPrimary,
            this.xrCellTraining,
            this.xrCellNotTraining,
            this.xrCellUnlimitedContract,
            this.xrCellLimitedContract,
            this.xrCellSeasonalContract,
            this.xrCellPosition,
            this.xrCellStartDate,
            this.xrCellOther});
            this.xrTableRow2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
            this.xrTableRow2.StylePriority.UseFont = false;
            this.xrTableRow2.StylePriority.UsePadding = false;
            this.xrTableRow2.StylePriority.UseTextAlignment = false;
            this.xrTableRow2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow2.Weight = 1D;
            // 
            // xrCellIndex
            // 
            this.xrCellIndex.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellIndex.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellIndex.Name = "xrCellIndex";
            this.xrCellIndex.StylePriority.UseBorders = false;
            this.xrCellIndex.StylePriority.UseFont = false;
            this.xrCellIndex.StylePriority.UseTextAlignment = false;
            this.xrCellIndex.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellIndex.Weight = 0.32031397287749697D;
            this.xrCellIndex.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrCellEmployeeCode
            // 
            this.xrCellEmployeeCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellEmployeeCode.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellEmployeeCode.Name = "xrCellEmployeeCode";
            this.xrCellEmployeeCode.StylePriority.UseBorders = false;
            this.xrCellEmployeeCode.StylePriority.UseFont = false;
            this.xrCellEmployeeCode.StylePriority.UseTextAlignment = false;
            this.xrCellEmployeeCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellEmployeeCode.Weight = 0.62706377633727772D;
            // 
            // xrCellFullName
            // 
            this.xrCellFullName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellFullName.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellFullName.Name = "xrCellFullName";
            this.xrCellFullName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellFullName.StylePriority.UseBorders = false;
            this.xrCellFullName.StylePriority.UseFont = false;
            this.xrCellFullName.StylePriority.UsePadding = false;
            this.xrCellFullName.StylePriority.UseTextAlignment = false;
            this.xrCellFullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCellFullName.Weight = 1.583022018070964D;
            // 
            // xrCellBirthYear
            // 
            this.xrCellBirthYear.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellBirthYear.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellBirthYear.Name = "xrCellBirthYear";
            this.xrCellBirthYear.StylePriority.UseBorders = false;
            this.xrCellBirthYear.StylePriority.UseFont = false;
            this.xrCellBirthYear.StylePriority.UseTextAlignment = false;
            this.xrCellBirthYear.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellBirthYear.Weight = 0.49749256306779355D;
            // 
            // xrMale
            // 
            this.xrMale.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrMale.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrMale.Name = "xrMale";
            this.xrMale.StylePriority.UseBorders = false;
            this.xrMale.StylePriority.UseFont = false;
            this.xrMale.StylePriority.UseTextAlignment = false;
            this.xrMale.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrMale.Weight = 0.34187975153941608D;
            // 
            // xrFemale
            // 
            this.xrFemale.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrFemale.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrFemale.Name = "xrFemale";
            this.xrFemale.StylePriority.UseBorders = false;
            this.xrFemale.StylePriority.UseFont = false;
            this.xrFemale.StylePriority.UseTextAlignment = false;
            this.xrFemale.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrFemale.Weight = 0.35078074424458361D;
            // 
            // xrCellUniversity
            // 
            this.xrCellUniversity.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellUniversity.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellUniversity.Name = "xrCellUniversity";
            this.xrCellUniversity.StylePriority.UseBorders = false;
            this.xrCellUniversity.StylePriority.UseFont = false;
            this.xrCellUniversity.StylePriority.UseTextAlignment = false;
            this.xrCellUniversity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellUniversity.Weight = 0.377490711767448D;
            // 
            // xrCellColleage
            // 
            this.xrCellColleage.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellColleage.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellColleage.Name = "xrCellColleage";
            this.xrCellColleage.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellColleage.StylePriority.UseBorders = false;
            this.xrCellColleage.StylePriority.UseFont = false;
            this.xrCellColleage.StylePriority.UsePadding = false;
            this.xrCellColleage.StylePriority.UseTextAlignment = false;
            this.xrCellColleage.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellColleage.Weight = 0.32763348094819778D;
            // 
            // xrCellIntermadiate
            // 
            this.xrCellIntermadiate.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellIntermadiate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellIntermadiate.Name = "xrCellIntermadiate";
            this.xrCellIntermadiate.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellIntermadiate.StylePriority.UseBorders = false;
            this.xrCellIntermadiate.StylePriority.UseFont = false;
            this.xrCellIntermadiate.StylePriority.UsePadding = false;
            this.xrCellIntermadiate.StylePriority.UseTextAlignment = false;
            this.xrCellIntermadiate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellIntermadiate.Weight = 0.42211906891882256D;
            // 
            // xrCellPrimary
            // 
            this.xrCellPrimary.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellPrimary.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellPrimary.Name = "xrCellPrimary";
            this.xrCellPrimary.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellPrimary.StylePriority.UseBorders = false;
            this.xrCellPrimary.StylePriority.UseFont = false;
            this.xrCellPrimary.StylePriority.UsePadding = false;
            this.xrCellPrimary.StylePriority.UseTextAlignment = false;
            this.xrCellPrimary.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellPrimary.Weight = 0.359685061736782D;
            // 
            // xrCellTraining
            // 
            this.xrCellTraining.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTraining.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellTraining.Name = "xrCellTraining";
            this.xrCellTraining.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellTraining.StylePriority.UseBorders = false;
            this.xrCellTraining.StylePriority.UseFont = false;
            this.xrCellTraining.StylePriority.UsePadding = false;
            this.xrCellTraining.StylePriority.UseTextAlignment = false;
            this.xrCellTraining.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTraining.Weight = 0.44693514192392636D;
            // 
            // xrCellNotTraining
            // 
            this.xrCellNotTraining.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellNotTraining.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellNotTraining.Name = "xrCellNotTraining";
            this.xrCellNotTraining.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellNotTraining.StylePriority.UseBorders = false;
            this.xrCellNotTraining.StylePriority.UseFont = false;
            this.xrCellNotTraining.StylePriority.UsePadding = false;
            this.xrCellNotTraining.StylePriority.UseTextAlignment = false;
            this.xrCellNotTraining.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellNotTraining.Weight = 0.36075291052958747D;
            // 
            // xrCellUnlimitedContract
            // 
            this.xrCellUnlimitedContract.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellUnlimitedContract.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellUnlimitedContract.Name = "xrCellUnlimitedContract";
            this.xrCellUnlimitedContract.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellUnlimitedContract.StylePriority.UseBorders = false;
            this.xrCellUnlimitedContract.StylePriority.UseFont = false;
            this.xrCellUnlimitedContract.StylePriority.UsePadding = false;
            this.xrCellUnlimitedContract.StylePriority.UseTextAlignment = false;
            this.xrCellUnlimitedContract.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellUnlimitedContract.Weight = 0.56303122447891052D;
            // 
            // xrCellLimitedContract
            // 
            this.xrCellLimitedContract.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellLimitedContract.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellLimitedContract.Name = "xrCellLimitedContract";
            this.xrCellLimitedContract.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellLimitedContract.StylePriority.UseBorders = false;
            this.xrCellLimitedContract.StylePriority.UseFont = false;
            this.xrCellLimitedContract.StylePriority.UsePadding = false;
            this.xrCellLimitedContract.StylePriority.UseTextAlignment = false;
            this.xrCellLimitedContract.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellLimitedContract.Weight = 0.28846001675539718D;
            // 
            // xrCellSeasonalContract
            // 
            this.xrCellSeasonalContract.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellSeasonalContract.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellSeasonalContract.Name = "xrCellSeasonalContract";
            this.xrCellSeasonalContract.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellSeasonalContract.StylePriority.UseBorders = false;
            this.xrCellSeasonalContract.StylePriority.UseFont = false;
            this.xrCellSeasonalContract.StylePriority.UsePadding = false;
            this.xrCellSeasonalContract.StylePriority.UseTextAlignment = false;
            this.xrCellSeasonalContract.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellSeasonalContract.Weight = 0.82015325082941537D;
            // 
            // xrCellPosition
            // 
            this.xrCellPosition.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellPosition.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellPosition.Name = "xrCellPosition";
            this.xrCellPosition.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellPosition.StylePriority.UseBorders = false;
            this.xrCellPosition.StylePriority.UseFont = false;
            this.xrCellPosition.StylePriority.UsePadding = false;
            this.xrCellPosition.StylePriority.UseTextAlignment = false;
            this.xrCellPosition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCellPosition.Weight = 0.736931991466324D;
            // 
            // xrCellStartDate
            // 
            this.xrCellStartDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellStartDate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellStartDate.Name = "xrCellStartDate";
            this.xrCellStartDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellStartDate.StylePriority.UseBorders = false;
            this.xrCellStartDate.StylePriority.UseFont = false;
            this.xrCellStartDate.StylePriority.UsePadding = false;
            this.xrCellStartDate.StylePriority.UseTextAlignment = false;
            this.xrCellStartDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellStartDate.Weight = 0.75227116610566336D;
            // 
            // xrCellOther
            // 
            this.xrCellOther.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellOther.Name = "xrCellOther";
            this.xrCellOther.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellOther.StylePriority.UseFont = false;
            this.xrCellOther.StylePriority.UsePadding = false;
            this.xrCellOther.StylePriority.UseTextAlignment = false;
            this.xrCellOther.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCellOther.Weight = 0.61880309997917626D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 46F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 61F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.PageHeader.HeightF = 101.0417F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1146F, 101.0417F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell4,
            this.xrTableCell3,
            this.xrTableCell5,
            this.xrTableCell8,
            this.xrTableCell7,
            this.xrTableCell10,
            this.xrTableCell6,
            this.xrTableCell9});
            this.xrTableRow1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
            this.xrTableRow1.StylePriority.UseFont = false;
            this.xrTableRow1.StylePriority.UsePadding = false;
            this.xrTableRow1.StylePriority.UseTextAlignment = false;
            this.xrTableRow1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "STT";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell1.Weight = 0.35416664177527146D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "Mã NV";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 0.69333567965068643D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell4.Multiline = true;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "HỌ VÀ TÊN\r\n";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell4.Weight = 1.7503254170336537D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "NĂM SINH";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell3.Weight = 0.55007058398465392D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTableCell5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel7,
            this.xrLabel6,
            this.xrLabel5});
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell5.Weight = 0.76586510288129483D;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel7.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(40.00009F, 25F);
            this.xrLabel7.Multiline = true;
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(41.04166F, 76.04166F);
            this.xrLabel7.StylePriority.UseBorders = false;
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "Nữ";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel6.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(3.051758E-05F, 25F);
            this.xrLabel6.Multiline = true;
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(40.00003F, 76.04166F);
            this.xrLabel6.StylePriority.UseBorders = false;
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "Nam";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel5.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(3.178914E-05F, 0F);
            this.xrLabel5.Multiline = true;
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(80F, 25F);
            this.xrLabel5.StylePriority.UseBorders = false;
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "GIỚI TÍNH";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTableCell8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel10,
            this.xrLabel9,
            this.xrLabel8,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel1,
            this.xrLabel2});
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell8.Weight = 2.5371257849880404D;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel10.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(226.2632F, 25F);
            this.xrLabel10.Multiline = true;
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(42.20831F, 76.04166F);
            this.xrLabel10.StylePriority.UseBorders = false;
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "Chưa qua đào tạo";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel9.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(173.9716F, 25F);
            this.xrLabel9.Multiline = true;
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(52.29169F, 76.04166F);
            this.xrLabel9.StylePriority.UseBorders = false;
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "Dạy nghề thường xuyên";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel8.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(131.8882F, 25F);
            this.xrLabel8.Multiline = true;
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(42.08331F, 76.04166F);
            this.xrLabel8.StylePriority.UseBorders = false;
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "Sơ cấp nghề";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(82.50003F, 25F);
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(49.38821F, 76.04166F);
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "Trung cấp/ TC nghề";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(44.16663F, 25F);
            this.xrLabel3.Multiline = true;
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(38.33344F, 76.04166F);
            this.xrLabel3.StylePriority.UseBorders = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Cao đẳng/CĐ nghề";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(268.4716F, 25F);
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "TRÌNH ĐỘ CHUYÊN MÔN KỸ THUẬT";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(44.16666F, 76.04166F);
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Đại học trở lên";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTableCell7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel14,
            this.xrLabel13,
            this.xrLabel12,
            this.xrLabel11});
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBorders = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell7.Weight = 1.8483133291380041D;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel14.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(99.62494F, 25F);
            this.xrLabel14.Multiline = true;
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(95.95844F, 76.04166F);
            this.xrLabel14.StylePriority.UseBorders = false;
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.Text = "Theo mùa vụ hoặc theo công việc nhất định dưới 12 tháng";
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel13.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(65.87499F, 25F);
            this.xrLabel13.Multiline = true;
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(33.74994F, 76.04166F);
            this.xrLabel13.StylePriority.UseBorders = false;
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "Xác định thời hạn";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel12.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrLabel12.Multiline = true;
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(65.875F, 76.04166F);
            this.xrLabel12.StylePriority.UseBorders = false;
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "Không xác định thời hạn";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel11.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel11.Multiline = true;
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(195.5834F, 25F);
            this.xrLabel11.StylePriority.UseBorders = false;
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "LOẠI HĐLĐ";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseBorders = false;
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.Text = "VỊ TRÍ VIỆC LÀM";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell10.Weight = 0.814815344267114D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseBorders = false;
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "THỜI ĐIỂM BẮT ĐẦU LÀM VIỆC";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell6.Weight = 0.83177612402820045D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "ĐỐI TƯỢNG KHÁC";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell9.Weight = 0.68420005747572754D;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblDuration,
            this.lblReportTitle});
            this.ReportHeader.HeightF = 95.08333F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // lblDuration
            // 
            this.lblDuration.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.lblDuration.LocationFloat = new DevExpress.Utils.PointFloat(0F, 51.12502F);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDuration.SizeF = new System.Drawing.SizeF(1146F, 23F);
            this.lblDuration.StylePriority.UseFont = false;
            this.lblDuration.StylePriority.UseTextAlignment = false;
            this.lblDuration.Text = "(Từ ngày {0} đến ngày {1})";
            this.lblDuration.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblReportTitle
            // 
            this.lblReportTitle.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblReportTitle.LocationFloat = new DevExpress.Utils.PointFloat(0F, 28.12503F);
            this.lblReportTitle.Name = "lblReportTitle";
            this.lblReportTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblReportTitle.SizeF = new System.Drawing.SizeF(1146F, 23F);
            this.lblReportTitle.StylePriority.UseFont = false;
            this.lblReportTitle.StylePriority.UseTextAlignment = false;
            this.lblReportTitle.Text = "KHAI TRÌNH LAO ĐỘNG";
            this.lblReportTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblReportDate,
            this.lblCreatedByName,
            this.lblSignedByName,
            this.lblSignedByTitle,
            this.lblCreatedByTitle});
            this.ReportFooter.HeightF = 226F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // lblReportDate
            // 
            this.lblReportDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.lblReportDate.LocationFloat = new DevExpress.Utils.PointFloat(576.0578F, 40.54165F);
            this.lblReportDate.Name = "lblReportDate";
            this.lblReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblReportDate.SizeF = new System.Drawing.SizeF(569.9422F, 23F);
            this.lblReportDate.StylePriority.UseFont = false;
            this.lblReportDate.StylePriority.UseTextAlignment = false;
            this.lblReportDate.Text = "Ngày {0} tháng {1} năm {2}";
            this.lblReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblCreatedByName
            // 
            this.lblCreatedByName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblCreatedByName.LocationFloat = new DevExpress.Utils.PointFloat(0F, 176.0417F);
            this.lblCreatedByName.Name = "lblCreatedByName";
            this.lblCreatedByName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCreatedByName.SizeF = new System.Drawing.SizeF(576.0578F, 23F);
            this.lblCreatedByName.StylePriority.UseFont = false;
            this.lblCreatedByName.StylePriority.UseTextAlignment = false;
            this.lblCreatedByName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblSignedByName
            // 
            this.lblSignedByName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblSignedByName.LocationFloat = new DevExpress.Utils.PointFloat(576.0578F, 176.0417F);
            this.lblSignedByName.Name = "lblSignedByName";
            this.lblSignedByName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblSignedByName.SizeF = new System.Drawing.SizeF(569.9421F, 23F);
            this.lblSignedByName.StylePriority.UseFont = false;
            this.lblSignedByName.StylePriority.UseTextAlignment = false;
            this.lblSignedByName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblSignedByTitle
            // 
            this.lblSignedByTitle.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.lblSignedByTitle.LocationFloat = new DevExpress.Utils.PointFloat(576.0577F, 63.54167F);
            this.lblSignedByTitle.Multiline = true;
            this.lblSignedByTitle.Name = "lblSignedByTitle";
            this.lblSignedByTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblSignedByTitle.SizeF = new System.Drawing.SizeF(569.9422F, 23F);
            this.lblSignedByTitle.StylePriority.UseFont = false;
            this.lblSignedByTitle.StylePriority.UseTextAlignment = false;
            this.lblSignedByTitle.Text = "TỔNG GIÁM ĐỐC";
            this.lblSignedByTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // lblCreatedByTitle
            // 
            this.lblCreatedByTitle.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.lblCreatedByTitle.LocationFloat = new DevExpress.Utils.PointFloat(0F, 63.54167F);
            this.lblCreatedByTitle.Name = "lblCreatedByTitle";
            this.lblCreatedByTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCreatedByTitle.SizeF = new System.Drawing.SizeF(576.0578F, 23F);
            this.lblCreatedByTitle.StylePriority.UseFont = false;
            this.lblCreatedByTitle.StylePriority.UseTextAlignment = false;
            this.lblCreatedByTitle.Text = "PHÒNG NHÂN SỰ";
            this.lblCreatedByTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.GroupHeader1.HeightF = 25F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable3
            // 
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1146F, 25F);
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrt_GroupDepartment});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrt_GroupDepartment
            // 
            this.xrt_GroupDepartment.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_GroupDepartment.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrt_GroupDepartment.Name = "xrt_GroupDepartment";
            this.xrt_GroupDepartment.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 3, 3, 3, 100F);
            this.xrt_GroupDepartment.StylePriority.UseBorders = false;
            this.xrt_GroupDepartment.StylePriority.UseFont = false;
            this.xrt_GroupDepartment.StylePriority.UsePadding = false;
            this.xrt_GroupDepartment.StylePriority.UseTextAlignment = false;
            this.xrt_GroupDepartment.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_GroupDepartment.Weight = 2D;
            this.xrt_GroupDepartment.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Group_BeforePrint);
            // 
            // rpHRM_LabourList
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.ReportHeader,
            this.ReportFooter,
            this.GroupHeader1});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(11, 12, 46, 61);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}

