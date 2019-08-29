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
    /// Summary description for rpHRM_LabourDecrease
    /// </summary>
    public class rpHRM_LabourDecrease : XtraReport, IBaseReport
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
        private XRTableCell xrCellImmediate;
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
        private XRTableCell xrCellDeterminationContract;
        private XRTableCell xrCellFire;
        private XRTableCell xrCellExpiredContract;
        private XRTableCell xrCellOtherReason;
        private XRLabel xrLabel10;
        private XRLabel xrLabel9;
        private XRLabel xrLabel8;
        private XRLabel xrLabel14;
        private XRLabel xrLabel13;
        private XRLabel xrLabel12;
        private XRLabel xrLabel11;
        private XRLabel xrLabel17;
        private XRLabel xrLabel16;
        private XRLabel xrLabel15;
        private XRLabel xrLabel7;
        private XRLabel xrLabel6;
        private XRLabel xrLabel5;
        private XRTableCell xrUnlimitedContract;
        private XRTableCell xrLimitedContract;
        private XRTableCell xrCellReasonalContract;
        private XRTableCell xrCellRetirement;
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
        private XRTableCell xrCellTotalRetirement;
        private XRTableCell xrCellTotalDeterminationContract;
        private XRTableCell xrCellTotalFire;
        private XRTableCell xrCellTotalExpiredContract;
        private XRTableCell xrCellTotalOtherReason;

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
            
            // filter group education            
            _filter.Items.Add(FilterGenerate.GroupEducationFilter());
            
            // filter group contract type
            _filter.Items.Add(FilterGenerate.GroupContractTypeFilter());

            // filter department
            _filter.Items.Add(FilterGenerate.GroupContractTypeFilter());
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public rpHRM_LabourDecrease()
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
                var table = SQLHelper.ExecuteTable(SQLReportRegulationAdapter.GetStore_LabourDecrease(string.Join(",", _filter.Departments.Split(
                    new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(d => "'{0}'".FormatWith(d))), fromDate, toDate, _filter.Condition));
                DataSource = table;
                // bind data
                xrCellFemale.DataBindings.Add("Text", DataSource, "Female");
                xrCellImmediate.DataBindings.Add("Text", DataSource, "Intermediate");
                xrCellColleage.DataBindings.Add("Text", DataSource, "College");
                xrCellUniversity.DataBindings.Add("Text", DataSource, "University");
                xrCellPrimary.DataBindings.Add("Text", DataSource, "PrimaryEducation");
                xrCellNotTraining.DataBindings.Add("Text", DataSource, "UnTraining");
                xrCellTraining.DataBindings.Add("Text", DataSource, "Vocational");
                xrUnlimitedContract.DataBindings.Add("Text", DataSource, "IndefinitContract");
                xrLimitedContract.DataBindings.Add("Text", DataSource, "TermContract");
                xrCellReasonalContract.DataBindings.Add("Text", DataSource, "SeasonContract");
                xrCellRetirement.DataBindings.Add("Text", DataSource, "ReasonRetirement");
                xrCellDeterminationContract.DataBindings.Add("Text", DataSource, "ReasonTerminate");
                xrCellExpiredContract.DataBindings.Add("Text", DataSource, "ReasonExpiredContract");
                xrCellOtherReason.DataBindings.Add("Text", DataSource, "ReasonOther");
                xrCellFire.DataBindings.Add("Text", DataSource, "ReasonFired");
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
                xrCellTotalRetirement.DataBindings.Add("Text", DataSource, "xReasonRetirement");
                xrCellTotalDeterminationContract.DataBindings.Add("Text", DataSource, "xReasonTerminate");
                xrCellTotalFire.DataBindings.Add("Text", DataSource, "xReasonFired");
                xrCellTotalExpiredContract.DataBindings.Add("Text", DataSource, "xReasonExpiredContract");
                xrCellTotalOtherReason.DataBindings.Add("Text", DataSource, "xReasonOther");
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
            this.xrCellImmediate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellPrimary = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTraining = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellNotTraining = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrUnlimitedContract = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLimitedContract = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellReasonalContract = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellRetirement = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellDeterminationContract = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellFire = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellExpiredContract = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellOtherReason = new DevExpress.XtraReports.UI.XRTableCell();
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
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
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
            this.xrCellTotalRetirement = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTotalDeterminationContract = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTotalFire = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTotalExpiredContract = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTotalOtherReason = new DevExpress.XtraReports.UI.XRTableCell();
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
            this.xrCellImmediate,
            this.xrCellPrimary,
            this.xrCellTraining,
            this.xrCellNotTraining,
            this.xrUnlimitedContract,
            this.xrLimitedContract,
            this.xrCellReasonalContract,
            this.xrCellRetirement,
            this.xrCellDeterminationContract,
            this.xrCellFire,
            this.xrCellExpiredContract,
            this.xrCellOtherReason});
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
            this.xrCellIndex.Weight = 0.41161583754931513D;
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
            this.xrCellFemale.Weight = 0.33592052725178922D;
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
            this.xrCellUniversity.Weight = 0.27013620177799025D;
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
            this.xrCellColleage.Weight = 0.37276794879834568D;
            // 
            // xrCellImmediate
            // 
            this.xrCellImmediate.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellImmediate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellImmediate.Name = "xrCellImmediate";
            this.xrCellImmediate.StylePriority.UseBorders = false;
            this.xrCellImmediate.StylePriority.UseFont = false;
            this.xrCellImmediate.StylePriority.UseTextAlignment = false;
            this.xrCellImmediate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellImmediate.Weight = 0.32024494674980064D;
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
            this.xrCellPrimary.Weight = 0.27364798700734694D;
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
            this.xrCellTraining.Weight = 0.39708097963464184D;
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
            this.xrCellNotTraining.Weight = 0.34287218955728882D;
            // 
            // xrUnlimitedContract
            // 
            this.xrUnlimitedContract.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrUnlimitedContract.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrUnlimitedContract.Name = "xrUnlimitedContract";
            this.xrUnlimitedContract.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrUnlimitedContract.StylePriority.UseBorders = false;
            this.xrUnlimitedContract.StylePriority.UseFont = false;
            this.xrUnlimitedContract.StylePriority.UsePadding = false;
            this.xrUnlimitedContract.StylePriority.UseTextAlignment = false;
            this.xrUnlimitedContract.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrUnlimitedContract.Weight = 0.49553614245020849D;
            // 
            // xrLimitedContract
            // 
            this.xrLimitedContract.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLimitedContract.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrLimitedContract.Name = "xrLimitedContract";
            this.xrLimitedContract.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLimitedContract.StylePriority.UseBorders = false;
            this.xrLimitedContract.StylePriority.UseFont = false;
            this.xrLimitedContract.StylePriority.UsePadding = false;
            this.xrLimitedContract.StylePriority.UseTextAlignment = false;
            this.xrLimitedContract.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLimitedContract.Weight = 0.53984224453587526D;
            // 
            // xrCellReasonalContract
            // 
            this.xrCellReasonalContract.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellReasonalContract.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellReasonalContract.Name = "xrCellReasonalContract";
            this.xrCellReasonalContract.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellReasonalContract.StylePriority.UseBorders = false;
            this.xrCellReasonalContract.StylePriority.UseFont = false;
            this.xrCellReasonalContract.StylePriority.UsePadding = false;
            this.xrCellReasonalContract.StylePriority.UseTextAlignment = false;
            this.xrCellReasonalContract.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellReasonalContract.Weight = 0.881334764838234D;
            // 
            // xrCellRetirement
            // 
            this.xrCellRetirement.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellRetirement.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellRetirement.Name = "xrCellRetirement";
            this.xrCellRetirement.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellRetirement.StylePriority.UseBorders = false;
            this.xrCellRetirement.StylePriority.UseFont = false;
            this.xrCellRetirement.StylePriority.UsePadding = false;
            this.xrCellRetirement.StylePriority.UseTextAlignment = false;
            this.xrCellRetirement.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellRetirement.Weight = 0.24290593222942836D;
            // 
            // xrCellDeterminationContract
            // 
            this.xrCellDeterminationContract.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellDeterminationContract.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellDeterminationContract.Name = "xrCellDeterminationContract";
            this.xrCellDeterminationContract.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellDeterminationContract.StylePriority.UseBorders = false;
            this.xrCellDeterminationContract.StylePriority.UseFont = false;
            this.xrCellDeterminationContract.StylePriority.UsePadding = false;
            this.xrCellDeterminationContract.StylePriority.UseTextAlignment = false;
            this.xrCellDeterminationContract.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellDeterminationContract.Weight = 0.5657689950447411D;
            // 
            // xrCellFire
            // 
            this.xrCellFire.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellFire.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellFire.Name = "xrCellFire";
            this.xrCellFire.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellFire.StylePriority.UseBorders = false;
            this.xrCellFire.StylePriority.UseFont = false;
            this.xrCellFire.StylePriority.UsePadding = false;
            this.xrCellFire.StylePriority.UseTextAlignment = false;
            this.xrCellFire.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellFire.Weight = 0.31859629975167941D;
            // 
            // xrCellExpiredContract
            // 
            this.xrCellExpiredContract.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellExpiredContract.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellExpiredContract.Name = "xrCellExpiredContract";
            this.xrCellExpiredContract.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellExpiredContract.StylePriority.UseBorders = false;
            this.xrCellExpiredContract.StylePriority.UseFont = false;
            this.xrCellExpiredContract.StylePriority.UsePadding = false;
            this.xrCellExpiredContract.StylePriority.UseTextAlignment = false;
            this.xrCellExpiredContract.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellExpiredContract.Weight = 0.37099699673562908D;
            // 
            // xrCellOtherReason
            // 
            this.xrCellOtherReason.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellOtherReason.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellOtherReason.Name = "xrCellOtherReason";
            this.xrCellOtherReason.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellOtherReason.StylePriority.UseBorders = false;
            this.xrCellOtherReason.StylePriority.UseFont = false;
            this.xrCellOtherReason.StylePriority.UsePadding = false;
            this.xrCellOtherReason.StylePriority.UseTextAlignment = false;
            this.xrCellOtherReason.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellOtherReason.Weight = 0.26619434544528153D;
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
            this.xrTableCell1.Weight = 0.39308831000260225D;
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
            this.xrTableCell2.Weight = 0.32080020642547413D;
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
            this.xrTableCell8.Weight = 1.8877734096071188D;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel10.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(292.3168F, 25.00006F);
            this.xrLabel10.Multiline = true;
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(61.34518F, 50F);
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
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(221.2752F, 25.00006F);
            this.xrLabel9.Multiline = true;
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(71.04166F, 50F);
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
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(172.3168F, 25.00006F);
            this.xrLabel8.Multiline = true;
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(48.95831F, 50F);
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
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(115.0219F, 25.00006F);
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(57.29497F, 50F);
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
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(48.33004F, 25F);
            this.xrLabel3.Multiline = true;
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(66.69179F, 50F);
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
            this.xrLabel1.SizeF = new System.Drawing.SizeF(353.662F, 25F);
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
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(1.525879E-05F, 25.00006F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(48.33F, 50F);
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
            this.xrTableCell7.Weight = 1.8304381337549895D;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel14.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(185.2394F, 25.00006F);
            this.xrLabel14.Multiline = true;
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(157.6793F, 50F);
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
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(88.65611F, 25.00006F);
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
            this.xrLabel12.SizeF = new System.Drawing.SizeF(88.65622F, 50F);
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
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0.00201416F, 6.357829E-05F);
            this.xrLabel11.Multiline = true;
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(342.9167F, 25F);
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
            this.xrTableCell9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel17,
            this.xrLabel16,
            this.xrLabel15,
            this.xrLabel7,
            this.xrLabel6,
            this.xrLabel5});
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell9.Weight = 1.685041433237545D;
            // 
            // xrLabel17
            // 
            this.xrLabel17.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrLabel17.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(268.0548F, 25F);
            this.xrLabel17.Multiline = true;
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(47.62488F, 50F);
            this.xrLabel17.StylePriority.UseBorders = false;
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.StylePriority.UseTextAlignment = false;
            this.xrLabel17.Text = "Lý do khác";
            this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel16
            // 
            this.xrLabel16.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel16.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(201.6799F, 25.00006F);
            this.xrLabel16.Multiline = true;
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(66.37488F, 50F);
            this.xrLabel16.StylePriority.UseBorders = false;
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            this.xrLabel16.Text = "Thỏa thuận chấm dứt";
            this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel15.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(144.6799F, 25.00006F);
            this.xrLabel15.Multiline = true;
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(57F, 50F);
            this.xrLabel15.StylePriority.UseBorders = false;
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "Kỷ luật sa thải";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel7.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(43.45837F, 25.00006F);
            this.xrLabel7.Multiline = true;
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(101.2216F, 50F);
            this.xrLabel7.StylePriority.UseBorders = false;
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "Đơn phương chấm dứt hđlđ/hđlv";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel6.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(6.103516E-05F, 25.00006F);
            this.xrLabel6.Multiline = true;
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(43.45825F, 50F);
            this.xrLabel6.StylePriority.UseBorders = false;
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "Nghỉ hưu";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrLabel5.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel5.Multiline = true;
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(315.6796F, 25F);
            this.xrLabel5.StylePriority.UseBorders = false;
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "LÝ DO GIẢM";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            this.lblReportTitle.Text = "KHAI TRÌNH GIẢM LAO ĐỘNG";
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
            this.xrCellTotalRetirement,
            this.xrCellTotalDeterminationContract,
            this.xrCellTotalFire,
            this.xrCellTotalExpiredContract,
            this.xrCellTotalOtherReason});
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
            this.xrTableCell3.Weight = 0.41161583754931513D;
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
            this.xrCellTotalFemale.Weight = 0.33592052725178922D;
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
            this.xrCellTotalUniversity.Weight = 0.27013620177799025D;
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
            this.xrCellTotalColleage.Weight = 0.37276794879834568D;
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
            this.xrCellTotalIntermediate.Weight = 0.32024494674980064D;
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
            this.xrCellTotalPrimary.Weight = 0.27364798700734694D;
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
            this.xrCellTotalTraining.Weight = 0.39708097963464184D;
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
            this.xrCellTotalNotTraining.Weight = 0.34287218955728882D;
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
            this.xrCellTotalUnlimitedContract.Weight = 0.49553614245020849D;
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
            this.xrCellTotalLimitedContract.Weight = 0.53984224453587526D;
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
            this.xrCellTotalSeasonalContract.Weight = 0.881334764838234D;
            // 
            // xrCellTotalRetirement
            // 
            this.xrCellTotalRetirement.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalRetirement.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalRetirement.Name = "xrCellTotalRetirement";
            this.xrCellTotalRetirement.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellTotalRetirement.StylePriority.UseBorders = false;
            this.xrCellTotalRetirement.StylePriority.UseFont = false;
            this.xrCellTotalRetirement.StylePriority.UsePadding = false;
            this.xrCellTotalRetirement.StylePriority.UseTextAlignment = false;
            this.xrCellTotalRetirement.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTotalRetirement.Weight = 0.24290593222942836D;
            // 
            // xrCellTotalDeterminationContract
            // 
            this.xrCellTotalDeterminationContract.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalDeterminationContract.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalDeterminationContract.Name = "xrCellTotalDeterminationContract";
            this.xrCellTotalDeterminationContract.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellTotalDeterminationContract.StylePriority.UseBorders = false;
            this.xrCellTotalDeterminationContract.StylePriority.UseFont = false;
            this.xrCellTotalDeterminationContract.StylePriority.UsePadding = false;
            this.xrCellTotalDeterminationContract.StylePriority.UseTextAlignment = false;
            this.xrCellTotalDeterminationContract.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTotalDeterminationContract.Weight = 0.5657689950447411D;
            // 
            // xrCellTotalFire
            // 
            this.xrCellTotalFire.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalFire.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalFire.Name = "xrCellTotalFire";
            this.xrCellTotalFire.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellTotalFire.StylePriority.UseBorders = false;
            this.xrCellTotalFire.StylePriority.UseFont = false;
            this.xrCellTotalFire.StylePriority.UsePadding = false;
            this.xrCellTotalFire.StylePriority.UseTextAlignment = false;
            this.xrCellTotalFire.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTotalFire.Weight = 0.31859629975167941D;
            // 
            // xrCellTotalExpiredContract
            // 
            this.xrCellTotalExpiredContract.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalExpiredContract.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalExpiredContract.Name = "xrCellTotalExpiredContract";
            this.xrCellTotalExpiredContract.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellTotalExpiredContract.StylePriority.UseBorders = false;
            this.xrCellTotalExpiredContract.StylePriority.UseFont = false;
            this.xrCellTotalExpiredContract.StylePriority.UsePadding = false;
            this.xrCellTotalExpiredContract.StylePriority.UseTextAlignment = false;
            this.xrCellTotalExpiredContract.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTotalExpiredContract.Weight = 0.37099699673562908D;
            // 
            // xrCellTotalOtherReason
            // 
            this.xrCellTotalOtherReason.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalOtherReason.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalOtherReason.Name = "xrCellTotalOtherReason";
            this.xrCellTotalOtherReason.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellTotalOtherReason.StylePriority.UseBorders = false;
            this.xrCellTotalOtherReason.StylePriority.UseFont = false;
            this.xrCellTotalOtherReason.StylePriority.UsePadding = false;
            this.xrCellTotalOtherReason.StylePriority.UseTextAlignment = false;
            this.xrCellTotalOtherReason.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTotalOtherReason.Weight = 0.26619434544528153D;
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
            this.lblSignedByTitle.LocationFloat = new DevExpress.Utils.PointFloat(576.0577F, 86.54169F);
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
            this.lblReportDate.LocationFloat = new DevExpress.Utils.PointFloat(576.0578F, 63.54167F);
            this.lblReportDate.Name = "lblReportDate";
            this.lblReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblReportDate.SizeF = new System.Drawing.SizeF(569.9421F, 23F);
            this.lblReportDate.StylePriority.UseFont = false;
            this.lblReportDate.StylePriority.UseTextAlignment = false;
            this.lblReportDate.Text = "Ngày {0} tháng {1} năm {2}";
            this.lblReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // rpHRM_LabourDecrease
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