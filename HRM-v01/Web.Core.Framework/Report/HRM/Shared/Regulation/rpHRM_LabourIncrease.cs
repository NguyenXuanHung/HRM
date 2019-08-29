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
    /// BC- khai trình tăng lao động
    /// Summary description for rpHRM_LabourIncrease
    /// </summary>
    public class rpHRM_LabourIncrease : XtraReport, IBaseReport
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
        private XRTableCell xrTableCell7;
        private GroupHeaderBand GroupHeader1;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrCellIndex;
        private XRTableCell xrCellFemale;
        private XRTableCell xrCellUniversity;
        private XRTableCell xrCellColleage;
        private XRTableCell xrCellIntermediate;
        private XRTableCell xrCellPrimary;
        private XRTableCell xrCellTraining;
        private XRLabel lblCreatedByName;
        private XRLabel lblSignedByName;
        private XRLabel lblSignedByTitle;
        private XRLabel lblCreatedByTitle;
        private XRTableCell xrTableCell9;
        private XRTableCell xrCellNotTraining;
        private XRTable xrTable3;
        private XRTableRow xrTableRow3;
        private XRTableCell xrt_GroupDepartment;
        private XRTableCell xrTableCell8;
        private XRLabel xrLabel4;
        private XRLabel xrLabel3;
        private XRLabel xrLabel1;
        private XRLabel xrLabel2;
        private XRLabel lblDuration;
        private XRTableCell xrCellUnlimitedContract;
        private XRTableCell xrCellLimitedContract;
        private XRTableCell xrCellSeasonalContract;
        private XRTableCell xrCellNote;
        private XRLabel xrLabel10;
        private XRLabel xrLabel9;
        private XRLabel xrLabel8;
        private XRLabel xrLabel14;
        private XRLabel xrLabel13;
        private XRLabel xrLabel12;
        private XRLabel xrLabel11;
        private XRTable xrTable4;
        private XRTableRow xrTableRow4;
        private XRTableCell xrTableCell3;
        private XRTableCell xrCellTotalFemale;
        private XRTableCell xrCellTotalUniversity;
        private XRTableCell xrCellTotalColleage;
        private XRTableCell xrCellTotalIntermediate;
        private XRTableCell xrCellTotalPrimary;
        private XRTableCell xrCellTotalTraining;
        private XRTableCell xrCellTotalNotTraining;
        private XRTableCell xrCellTotalUnlimitedContract;
        private XRTableCell xrCellTotalLimitedContract;
        private XRTableCell xrCellTotalSeasonalContract;
        private XRTableCell xrTableCell17;

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

            // filter group education            
            _filter.Items.Add(FilterGenerate.GroupEducationFilter());

            // filter group contract type
            _filter.Items.Add(FilterGenerate.GroupContractTypeFilter());
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public rpHRM_LabourIncrease()
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
        /// 
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
                var table = SQLHelper.ExecuteTable(SQLReportRegulationAdapter.GetStore_LabourIncrease(string.Join(",", _filter.Departments.Split(
                        new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(d => "'{0}'".FormatWith(d))), fromDate, toDate, _filter.Condition));
                DataSource = table;
                // bind data
                xrCellFemale.DataBindings.Add("Text", DataSource, "Female");
                xrCellIntermediate.DataBindings.Add("Text", DataSource, "Intermediate");
                xrCellColleage.DataBindings.Add("Text", DataSource, "College");
                xrCellUniversity.DataBindings.Add("Text", DataSource, "University");
                xrCellPrimary.DataBindings.Add("Text", DataSource, "PrimaryEducation");
                xrCellNotTraining.DataBindings.Add("Text", DataSource, "UnTraining");
                xrCellTraining.DataBindings.Add("Text", DataSource, "Vocational");
                xrCellUnlimitedContract.DataBindings.Add("Text", DataSource, "IndefinitContract");
                xrCellLimitedContract.DataBindings.Add("Text", DataSource, "TermContract");
                xrCellSeasonalContract.DataBindings.Add("Text", DataSource, "SeasonContract");
                xrCellNote.DataBindings.Add("Text", DataSource, "");
                // bind group data
                GroupHeader1.GroupFields.AddRange(new[]
                {
                    new GroupField("DepartmentId", XRColumnSortOrder.Ascending)
                });
                xrt_GroupDepartment.DataBindings.Add("Text", DataSource, "DepartmentName");
                // bind footer
                xrCellTotalFemale.DataBindings.Add("Text", DataSource, "xFemale");
                xrCellTotalUniversity.DataBindings.Add("Text", DataSource, "xUniversity");
                xrCellTotalColleage.DataBindings.Add("Text", DataSource, "xColleage");
                xrCellTotalIntermediate.DataBindings.Add("Text", DataSource, "xIntermediate");
                xrCellTotalPrimary.DataBindings.Add("Text", DataSource, "xPrimaryEducation");
                xrCellTotalNotTraining.DataBindings.Add("Text", DataSource, "xUnTraining");
                xrCellTotalTraining.DataBindings.Add("Text", DataSource, "xVocational");
                xrCellTotalUnlimitedContract.DataBindings.Add("Text", DataSource, "xIndefinitContract");
                xrCellTotalLimitedContract.DataBindings.Add("Text", DataSource, "xTermContract");
                xrCellTotalSeasonalContract.DataBindings.Add("Text", DataSource, "xSeasonContract");
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
            catch (Exception ex)
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
            if (disposing && (components != null))
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
            this.xrCellFemale = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellUniversity = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellColleage = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellIntermediate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellPrimary = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTraining = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellNotTraining = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellUnlimitedContract = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellLimitedContract = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellSeasonalContract = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellNote = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
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
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.lblDuration = new DevExpress.XtraReports.UI.XRLabel();
            this.lblReportTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTotalFemale = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTotalUniversity = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTotalColleage = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTotalIntermediate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTotalPrimary = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTotalTraining = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTotalNotTraining = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTotalUnlimitedContract = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTotalLimitedContract = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTotalSeasonalContract = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblCreatedByName = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSignedByName = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSignedByTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCreatedByTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrt_GroupDepartment = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblReportDate = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
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
            this.xrCellFemale,
            this.xrCellUniversity,
            this.xrCellColleage,
            this.xrCellIntermediate,
            this.xrCellPrimary,
            this.xrCellTraining,
            this.xrCellNotTraining,
            this.xrCellUnlimitedContract,
            this.xrCellLimitedContract,
            this.xrCellSeasonalContract,
            this.xrCellNote});
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
            this.xrCellIndex.Weight = 0.40579355153463026D;
            this.xrCellIndex.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrCellFemale
            // 
            this.xrCellFemale.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellFemale.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellFemale.Name = "xrCellFemale";
            this.xrCellFemale.StylePriority.UseBorders = false;
            this.xrCellFemale.StylePriority.UseFont = false;
            this.xrCellFemale.StylePriority.UseTextAlignment = false;
            this.xrCellFemale.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellFemale.Weight = 0.42907723141814835D;
            // 
            // xrCellUniversity
            // 
            this.xrCellUniversity.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellUniversity.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellUniversity.Name = "xrCellUniversity";
            this.xrCellUniversity.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellUniversity.StylePriority.UseBorders = false;
            this.xrCellUniversity.StylePriority.UseFont = false;
            this.xrCellUniversity.StylePriority.UsePadding = false;
            this.xrCellUniversity.StylePriority.UseTextAlignment = false;
            this.xrCellUniversity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellUniversity.Weight = 0.37493769119272158D;
            // 
            // xrCellColleage
            // 
            this.xrCellColleage.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellColleage.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellColleage.Name = "xrCellColleage";
            this.xrCellColleage.StylePriority.UseBorders = false;
            this.xrCellColleage.StylePriority.UseFont = false;
            this.xrCellColleage.StylePriority.UseTextAlignment = false;
            this.xrCellColleage.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellColleage.Weight = 0.50087683381843362D;
            // 
            // xrCellIntermediate
            // 
            this.xrCellIntermediate.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellIntermediate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellIntermediate.Name = "xrCellIntermediate";
            this.xrCellIntermediate.StylePriority.UseBorders = false;
            this.xrCellIntermediate.StylePriority.UseFont = false;
            this.xrCellIntermediate.StylePriority.UseTextAlignment = false;
            this.xrCellIntermediate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellIntermediate.Weight = 0.43667250078448144D;
            // 
            // xrCellPrimary
            // 
            this.xrCellPrimary.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellPrimary.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellPrimary.Name = "xrCellPrimary";
            this.xrCellPrimary.StylePriority.UseBorders = false;
            this.xrCellPrimary.StylePriority.UseFont = false;
            this.xrCellPrimary.StylePriority.UseTextAlignment = false;
            this.xrCellPrimary.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellPrimary.Weight = 0.4133835336605915D;
            // 
            // xrCellTraining
            // 
            this.xrCellTraining.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTraining.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellTraining.Name = "xrCellTraining";
            this.xrCellTraining.StylePriority.UseBorders = false;
            this.xrCellTraining.StylePriority.UseFont = false;
            this.xrCellTraining.StylePriority.UseTextAlignment = false;
            this.xrCellTraining.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTraining.Weight = 0.49605962866528169D;
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
            this.xrCellNotTraining.Weight = 0.52337452719485578D;
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
            this.xrCellUnlimitedContract.Weight = 0.6833037896678319D;
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
            this.xrCellLimitedContract.Weight = 0.53984322888360126D;
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
            this.xrCellSeasonalContract.Weight = 0.885690354192257D;
            // 
            // xrCellNote
            // 
            this.xrCellNote.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellNote.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellNote.Name = "xrCellNote";
            this.xrCellNote.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellNote.StylePriority.UseBorders = false;
            this.xrCellNote.StylePriority.UseFont = false;
            this.xrCellNote.StylePriority.UsePadding = false;
            this.xrCellNote.StylePriority.UseTextAlignment = false;
            this.xrCellNote.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCellNote.Weight = 0.71644894923737212D;
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
            this.PageHeader.HeightF = 75F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1146F, 75F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell8,
            this.xrTableCell7,
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
            this.xrTableCell1.Weight = 0.38752809461702176D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "TRONG ĐÓ LĐ NỮ";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 0.40976389694076154D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
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
            this.xrTableCell8.Weight = 2.6217345057713737D;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel10.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(397.5251F, 25F);
            this.xrLabel10.Multiline = true;
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(93.63684F, 50F);
            this.xrLabel10.StylePriority.UseBorders = false;
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "Chưa qua đào tạo";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel9.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(308.7751F, 25.00006F);
            this.xrLabel9.Multiline = true;
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(88.75F, 50F);
            this.xrLabel9.StylePriority.UseBorders = false;
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "Dạy nghề thường xuyên";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel8.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(234.8168F, 25.00006F);
            this.xrLabel8.Multiline = true;
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(73.95831F, 50F);
            this.xrLabel8.StylePriority.UseBorders = false;
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "Sơ cấp nghề";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(156.6918F, 25.00006F);
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(78.125F, 50F);
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "Trung cấp/ TC nghề";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(67.08339F, 25.00006F);
            this.xrLabel3.Multiline = true;
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(89.60843F, 50F);
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
            this.xrLabel1.SizeF = new System.Drawing.SizeF(491.162F, 25F);
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "TRÌNH ĐỘ CHUYÊN MÔN KỸ THUẬT";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(1.525879E-05F, 25.00006F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(67.08F, 50F);
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Đại học trở lên";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel14,
            this.xrLabel13,
            this.xrLabel12,
            this.xrLabel11});
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBorders = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell7.Weight = 2.0139149382228454D;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel14.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(218.8331F, 25.00006F);
            this.xrLabel14.Multiline = true;
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(157.4168F, 50F);
            this.xrLabel14.StylePriority.UseBorders = false;
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.Text = "Theo mùa vụ hoặc theo công việc nhất định dưới 12 tháng";
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel13.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(122.2498F, 25.00006F);
            this.xrLabel13.Multiline = true;
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(96.58325F, 50F);
            this.xrLabel13.StylePriority.UseBorders = false;
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "Xác định thời hạn";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel12.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25.00006F);
            this.xrLabel12.Multiline = true;
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(122.2498F, 50F);
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
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 6.357829E-05F);
            this.xrLabel11.Multiline = true;
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(377.2916F, 25F);
            this.xrLabel11.StylePriority.UseBorders = false;
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "LOẠI HĐLĐ";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "GHI CHÚ";
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
            this.lblDuration.Text = "Từ ngày: {0} đến ngày: {1}";
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
            this.lblReportTitle.Text = "KHAI TRÌNH TĂNG LAO ĐỘNG";
            this.lblReportTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblReportDate,
            this.xrTable4,
            this.lblCreatedByName,
            this.lblSignedByName,
            this.lblSignedByTitle,
            this.lblCreatedByTitle});
            this.ReportFooter.HeightF = 226F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrTable4
            // 
            this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrTable4.SizeF = new System.Drawing.SizeF(1146F, 25F);
            this.xrTable4.StylePriority.UseBorders = false;
            this.xrTable4.StylePriority.UseTextAlignment = false;
            this.xrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrCellTotalFemale,
            this.xrCellTotalUniversity,
            this.xrCellTotalColleage,
            this.xrCellTotalIntermediate,
            this.xrCellTotalPrimary,
            this.xrCellTotalTraining,
            this.xrCellTotalNotTraining,
            this.xrCellTotalUnlimitedContract,
            this.xrCellTotalLimitedContract,
            this.xrCellTotalSeasonalContract,
            this.xrTableCell17});
            this.xrTableRow4.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
            this.xrTableRow4.StylePriority.UseFont = false;
            this.xrTableRow4.StylePriority.UsePadding = false;
            this.xrTableRow4.StylePriority.UseTextAlignment = false;
            this.xrTableRow4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "TỔNG";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell3.Weight = 0.40579355153463026D;
            // 
            // xrCellTotalFemale
            // 
            this.xrCellTotalFemale.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalFemale.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalFemale.Name = "xrCellTotalFemale";
            this.xrCellTotalFemale.StylePriority.UseBorders = false;
            this.xrCellTotalFemale.StylePriority.UseFont = false;
            this.xrCellTotalFemale.StylePriority.UseTextAlignment = false;
            this.xrCellTotalFemale.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTotalFemale.Weight = 0.42907723141814835D;
            // 
            // xrCellTotalUniversity
            // 
            this.xrCellTotalUniversity.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalUniversity.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalUniversity.Name = "xrCellTotalUniversity";
            this.xrCellTotalUniversity.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellTotalUniversity.StylePriority.UseBorders = false;
            this.xrCellTotalUniversity.StylePriority.UseFont = false;
            this.xrCellTotalUniversity.StylePriority.UsePadding = false;
            this.xrCellTotalUniversity.StylePriority.UseTextAlignment = false;
            this.xrCellTotalUniversity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTotalUniversity.Weight = 0.37493769119272158D;
            // 
            // xrCellTotalColleage
            // 
            this.xrCellTotalColleage.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalColleage.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalColleage.Name = "xrCellTotalColleage";
            this.xrCellTotalColleage.StylePriority.UseBorders = false;
            this.xrCellTotalColleage.StylePriority.UseFont = false;
            this.xrCellTotalColleage.StylePriority.UseTextAlignment = false;
            this.xrCellTotalColleage.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTotalColleage.Weight = 0.50087683381843362D;
            // 
            // xrCellTotalIntermediate
            // 
            this.xrCellTotalIntermediate.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalIntermediate.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalIntermediate.Name = "xrCellTotalIntermediate";
            this.xrCellTotalIntermediate.StylePriority.UseBorders = false;
            this.xrCellTotalIntermediate.StylePriority.UseFont = false;
            this.xrCellTotalIntermediate.StylePriority.UseTextAlignment = false;
            this.xrCellTotalIntermediate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTotalIntermediate.Weight = 0.43667250078448144D;
            // 
            // xrCellTotalPrimary
            // 
            this.xrCellTotalPrimary.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalPrimary.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalPrimary.Name = "xrCellTotalPrimary";
            this.xrCellTotalPrimary.StylePriority.UseBorders = false;
            this.xrCellTotalPrimary.StylePriority.UseFont = false;
            this.xrCellTotalPrimary.StylePriority.UseTextAlignment = false;
            this.xrCellTotalPrimary.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTotalPrimary.Weight = 0.4133835336605915D;
            // 
            // xrCellTotalTraining
            // 
            this.xrCellTotalTraining.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalTraining.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalTraining.Name = "xrCellTotalTraining";
            this.xrCellTotalTraining.StylePriority.UseBorders = false;
            this.xrCellTotalTraining.StylePriority.UseFont = false;
            this.xrCellTotalTraining.StylePriority.UseTextAlignment = false;
            this.xrCellTotalTraining.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTotalTraining.Weight = 0.49605962866528169D;
            // 
            // xrCellTotalNotTraining
            // 
            this.xrCellTotalNotTraining.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalNotTraining.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalNotTraining.Name = "xrCellTotalNotTraining";
            this.xrCellTotalNotTraining.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellTotalNotTraining.StylePriority.UseBorders = false;
            this.xrCellTotalNotTraining.StylePriority.UseFont = false;
            this.xrCellTotalNotTraining.StylePriority.UsePadding = false;
            this.xrCellTotalNotTraining.StylePriority.UseTextAlignment = false;
            this.xrCellTotalNotTraining.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTotalNotTraining.Weight = 0.52337452719485578D;
            // 
            // xrCellTotalUnlimitedContract
            // 
            this.xrCellTotalUnlimitedContract.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalUnlimitedContract.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalUnlimitedContract.Name = "xrCellTotalUnlimitedContract";
            this.xrCellTotalUnlimitedContract.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellTotalUnlimitedContract.StylePriority.UseBorders = false;
            this.xrCellTotalUnlimitedContract.StylePriority.UseFont = false;
            this.xrCellTotalUnlimitedContract.StylePriority.UsePadding = false;
            this.xrCellTotalUnlimitedContract.StylePriority.UseTextAlignment = false;
            this.xrCellTotalUnlimitedContract.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTotalUnlimitedContract.Weight = 0.6833037896678319D;
            // 
            // xrCellTotalLimitedContract
            // 
            this.xrCellTotalLimitedContract.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalLimitedContract.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalLimitedContract.Name = "xrCellTotalLimitedContract";
            this.xrCellTotalLimitedContract.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellTotalLimitedContract.StylePriority.UseBorders = false;
            this.xrCellTotalLimitedContract.StylePriority.UseFont = false;
            this.xrCellTotalLimitedContract.StylePriority.UsePadding = false;
            this.xrCellTotalLimitedContract.StylePriority.UseTextAlignment = false;
            this.xrCellTotalLimitedContract.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTotalLimitedContract.Weight = 0.53984322888360126D;
            // 
            // xrCellTotalSeasonalContract
            // 
            this.xrCellTotalSeasonalContract.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalSeasonalContract.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalSeasonalContract.Name = "xrCellTotalSeasonalContract";
            this.xrCellTotalSeasonalContract.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellTotalSeasonalContract.StylePriority.UseBorders = false;
            this.xrCellTotalSeasonalContract.StylePriority.UseFont = false;
            this.xrCellTotalSeasonalContract.StylePriority.UsePadding = false;
            this.xrCellTotalSeasonalContract.StylePriority.UseTextAlignment = false;
            this.xrCellTotalSeasonalContract.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTotalSeasonalContract.Weight = 0.885690354192257D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell17.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell17.StylePriority.UseBorders = false;
            this.xrTableCell17.StylePriority.UseFont = false;
            this.xrTableCell17.StylePriority.UsePadding = false;
            this.xrTableCell17.StylePriority.UseTextAlignment = false;
            this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell17.Weight = 0.71644894923737212D;
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
            this.lblSignedByTitle.LocationFloat = new DevExpress.Utils.PointFloat(576.0578F, 86.54169F);
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
            this.lblCreatedByTitle.LocationFloat = new DevExpress.Utils.PointFloat(0F, 86.54169F);
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
            // lblReportDate
            // 
            this.lblReportDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.lblReportDate.LocationFloat = new DevExpress.Utils.PointFloat(576.058F, 63.54167F);
            this.lblReportDate.Name = "lblReportDate";
            this.lblReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblReportDate.SizeF = new System.Drawing.SizeF(569.9419F, 23F);
            this.lblReportDate.StylePriority.UseFont = false;
            this.lblReportDate.StylePriority.UseTextAlignment = false;
            this.lblReportDate.Text = "Ngày {0} tháng {1} năm {2}";
            this.lblReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // rpHRM_LabourIncrease
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
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}