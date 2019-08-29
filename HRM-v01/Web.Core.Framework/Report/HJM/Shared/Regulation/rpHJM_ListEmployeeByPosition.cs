using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraReports.UI;
using Web.Core;
using Web.Core.Service.Catalog;
using Web.Core.Framework;
using Web.Core.Framework.Interface;
using Web.Core.Framework.Report;
using Web.Core.Framework.Report.Base;
using Web.Core.Framework.SQLAdapter;

namespace Web.Core.Framework.Report
{

    /// <summary>
    /// Summary description for rp_ListEmployeeByPosition
    /// </summary>
    public class rpHJM_ListEmployeeByPosition : XtraReport, IBaseReport
    {
        private DetailBand Detail;
        private TopMarginBand TopMargin;
        private BottomMarginBand BottomMargin;
        private ReportHeaderBand ReportHeader;
        private FormattingRule formattingRule1;
        private ReportFooterBand ReportFooter;
        private XRTable tblReportHeader;
        private XRTableRow xrTableRow4;
        private XRTableCell xrTableCell601;
        private XRTableCell xrTableCell602;
        private XRTableRow xrTableRow19;
        private XRTableCell xrCellTenBieuMau;
        private XRTable tblDetail;
        private XRTableRow xrDetailRow1;
        private XRTableCell xrTableCellOrder;
        private XRTable xrTable4;
        private XRTableRow xrTableRow2;
        private XRTableCell lblReportTitle;
        private XRTableRow xrTableRow3;
        private XRTableCell xrTableCell26;
        private XRTableCell xrTableCell27;
        private XRLabel lblLuongHienHuong;
        private XRTableCell xrTableCell9;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCell8;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell21;
        private XRTableCell xrTableCell10;
        private XRTableCell xrTableCell16;
        private XRTableCell xrTableCell15;
        private XRTableCell xrTableCell14;
        private XRTableCell xrTableCell242;
        private XRTableCell xrTableCell241;
        private XRTableRow xrTableRow11;
        private XRTable tblPageHeader;
        private XRLabel xrLabel2;
        private XRTableCell xrTableCell18;
        private XRTableCell xrTableCell13;
        private XRTableCell xrTableCell5;
        private XRTableCell xrTableCell4;
        private XRTableRow xrTableRow5;
        private XRTable xrTable3;
        private XRLabel xrLabel3;
        private XRLabel xrLabel4;
        private XRLabel xrLabel5;
        private XRLabel xrLabel6;
        private XRLabel xrLabel7;
        private XRLabel xrLabel8;
        private XRLabel xrLabel9;
        private XRLabel xrLabel10;
        private PageHeaderBand PageHeader;
        private XRTableCell xrTableCellFullName;
        private XRTableCell xrTableCellSex;
        private XRTableCell xrTableCellNation;
        private XRTableCell xrTableCellBirthday;
        private XRTableCell xrTableCellNativeLand;
        private XRTableCell xrTableCellPlaceOfResidence;
        private XRTableCell xrTableCellDateAdmittedToTheParty;
        private XRTableCell xrTableCellDateIntoTheIndustry;
        private XRTableCell xrTableCellDateAppointed;
        private XRTableCell xrTableCellPosition;
        private XRTableCell xrTableCellCode;
        private XRTableCell xrTableCellDegree;
        private XRTableCell xrTableCellSpecialized;
        private XRTableCell xrTableCellPlaceOfTraining;
        private XRTableCell xrTableCellFormOfTraining;
        private XRTableCell xrTableCellRating;
        private XRTableCell xrTableCellCurrentDegree;
        private XRTableCell xrTableCellCurrentSpecialized;
        private XRTableCell xrTableCellCurrentPlaceOfTraining;
        private XRTableCell xrTableCellCurrentFormOfTraining;
        private XRTableCell xrTableCellCurrentRating;
        private XRTableCell xrTableCellPoliticalQualifications;
        private XRTableCell xrTableCellManagerLevel;
        private XRTableCell xrTableCellEducationManagement;
        private XRTableCell xrTableCellInformatics;
        private XRTableCell xrTableCellForeignLanguage;
        private XRTableCell xrTableCell7;
        private XRTableCell xrTableCell19;
        private XRTableCell xrTableCell20;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell3;
        private XRTableCell xrTableCell6;
        private XRTableCell xrTableCell11;
        private XRTableCell xrTableCell12;
        private XRTableCell xrTableCell17;
        private XRTableCell xrTableCellEthnicLanguage;
        private XRTableCell xrTableCellNote;
        private XRTableCell xrTableCellWorkUnit;
        private GroupHeaderBand GroupHeader1;
        private XRTable xrTable2;
        private XRTableRow xrTableRow6;
        private XRTableCell xrTableCell22;
        private XRTableCell xrTableCellGroupHead;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
        private IContainer components = null;
        #region Init

        private const string _reportTitle = "BÁO CÁO DANH SÁCH CÁN BỘ QUẢN LÝ GIÁO VIÊN, VÀ NHÂN VIÊN TRONG CÁC ĐƠN VỊ TRƯỜNG HỌC TÍNH ĐẾN THỜI ĐIỂM";

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
                ReportTitle = _reportTitle,
                Items = new List<FilterItem>()
            };
            // filter department            
            _filter.Items.Add(FilterGenerate.DepartmentFilter());

            // filter folk (dân tộc)            
            _filter.Items.Add(FilterGenerate.FolkFilter());

            // filter job title            
            _filter.Items.Add(FilterGenerate.JobTitleFilter());

            // filter position            
            _filter.Items.Add(FilterGenerate.PositionFilter());
        
            // filter contact type            
            _filter.Items.Add(FilterGenerate.ContractTypeFilter(true, GenerateFilterType.Hc));

            // filter politic level            
            _filter.Items.Add(FilterGenerate.PoliticLevelFilter());

            //filter it level
            _filter.Items.Add(FilterGenerate.ItLevelFilter());

            //filter it level
            _filter.Items.Add(FilterGenerate.LanguageLevelFilter());
            //
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public rpHJM_ListEmployeeByPosition()
        {
            // init compoent
            InitializeComponent();

            // init Filter
            InitFilter();
        }

        #endregion       
        int _stt = 1;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCellOrder.Text = _stt.ToString();
            _stt++;
        }
        public void BindData()
        {
            try
            {               
                // get organization
                var organization = cat_DepartmentServices.GetByDepartments(_filter.Departments);
                if(organization != null)
                {
                    // select form db               

                    // from date
                    var fromDate = _filter.StartDate != null ? _filter.StartDate.Value.ToString("yyyy-MM-dd 00:00:00") : string.Empty;
                    // to date
                    var toDate = _filter.EndDate != null ? _filter.EndDate.Value.ToString("yyyy-MM-dd 23:59:59") : string.Empty;
                    // select form db 
                    var table = SQLHelper.ExecuteTable(SQLReportRegulationAdapter.GetStore_ListEmployeeByPosition(string.Join(",", _filter.Departments.Split(
                        new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(d => "'{0}'".FormatWith(d))), fromDate, toDate, _filter.Condition));
                    DataSource = table;
                    //binding data
                    xrTableCellFullName.DataBindings.Add("Text", DataSource, "FullName");
                    xrTableCellSex.DataBindings.Add("Text", DataSource, "Sex");
                    xrTableCellBirthday.DataBindings.Add("Text", DataSource, "Birthday", "{0:dd/MM/yyyy}");
                    xrTableCellPlaceOfResidence.DataBindings.Add("Text", DataSource, "Address");
                    xrTableCellPosition.DataBindings.Add("Text", DataSource, "Position");
                    xrTableCellCode.DataBindings.Add("Text", DataSource, "CodeQuantum");
                    xrTableCellNation.DataBindings.Add("Text", DataSource, "Nation");
                    xrTableCellPoliticalQualifications.DataBindings.Add("Text", DataSource, "Political");
                    xrTableCellInformatics.DataBindings.Add("Text", DataSource, "It");
                    xrTableCellForeignLanguage.DataBindings.Add("Text", DataSource, "Language");
                    xrTableCellDateAdmittedToTheParty.DataBindings.Add("Text", DataSource, "CPVJoinedDate", "{0:dd/MM/yyyy}");
                    xrTableCellDateIntoTheIndustry.DataBindings.Add("Text", DataSource, "RecruimentDate", "{0:dd/MM/yyyy}");
                    xrTableCellDateAppointed.DataBindings.Add("Text", DataSource, "FunctionaryDate", "{0:dd/MM/yyyy}");
                    xrTableCellWorkUnit.DataBindings.Add("Text", DataSource, "WorkUnit");
                    xrTableCellNativeLand.DataBindings.Add("Text", DataSource, "Hometown");
                    xrTableCellManagerLevel.DataBindings.Add("Text", DataSource, "ManagerLevel");
                    xrTableCellDegree.DataBindings.Add("Text", DataSource, "OldDegree");
                    xrTableCellCurrentDegree.DataBindings.Add("Text", DataSource, "CurrentDegree");
                    xrTableCellSpecialized.DataBindings.Add("Text", DataSource, "IndustryName");
                    xrTableCellPlaceOfTraining.DataBindings.Add("Text", DataSource, "UniversityName");
                    xrTableCellFormOfTraining.DataBindings.Add("Text", DataSource, "TrainingSystemName");
                    xrTableCellCurrentSpecialized.DataBindings.Add("Text", DataSource, "CurrentSpecialized");
                    xrTableCellCurrentPlaceOfTraining.DataBindings.Add("Text", DataSource, "CurrentPlaceOfTraining");
                    this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
                        new DevExpress.XtraReports.UI.GroupField("Code", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
                    xrTableCellGroupHead.DataBindings.Add("Text", DataSource, "Position");
                }
                // other items
                // label org name
                //if(!string.IsNullOrEmpty(_filter.OrganizationName)) lblOrgName.Text = _filter.OrganizationName;
                // label report title
                if(!string.IsNullOrEmpty(_filter.ReportTitle)) lblReportTitle.Text = _filter.ReportTitle;
                // lablel duration
                //if(_filter.StartDate != null && _filter.EndDate != null)
                //{
                //    lblDuration.Text = lblDuration.Text.FormatWith(_filter.StartDate.Value.ToString("dd/MM/yyyy"),
                //        _filter.EndDate.Value.ToString("dd/MM/yyyy"));
                //}
                //else
                //{
                //    lblDuration.Text = string.Empty;
                //}
                // label report date
                //lblReportDate.Text = lblReportDate.Text.FormatWith(_filter.ReportDate.Day, _filter.ReportDate.Month, _filter.ReportDate.Year);
                // created by
                //if(!string.IsNullOrEmpty(_filter.CreatedByTitle)) lblCreatedByTitle.Text = _filter.CreatedByTitle;
                //if(!string.IsNullOrEmpty(_filter.CreatedByName)) lblCreatedByName.Text = _filter.CreatedByName;
                // reviewed by
                //if(!string.IsNullOrEmpty(_filter.ReviewedByTitle)) lblReviewedByTitle.Text = _filter.ReviewedByTitle;
                //if(!string.IsNullOrEmpty(_filter.ReviewedByName)) lblReviewedByName.Text = _filter.ReviewedByName;
                // signed by
                //if(!string.IsNullOrEmpty(_filter.SignedByTitle)) lblSignedByTitle.Text = _filter.SignedByTitle;
                //if(!string.IsNullOrEmpty(_filter.SignedByName)) lblSignedByName.Text = _filter.SignedByName;
            }
            catch(Exception ex)
            {
                Dialog.ShowError(ex);
            }
        }
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
            this.tblDetail = new DevExpress.XtraReports.UI.XRTable();
            this.xrDetailRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellOrder = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellFullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellSex = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNation = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellBirthday = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNativeLand = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellPlaceOfResidence = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDateAdmittedToTheParty = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDateIntoTheIndustry = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDateAppointed = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellPosition = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDegree = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellSpecialized = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellPlaceOfTraining = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellFormOfTraining = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellRating = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCurrentDegree = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCurrentSpecialized = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCurrentPlaceOfTraining = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCurrentFormOfTraining = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCurrentRating = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellPoliticalQualifications = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellManagerLevel = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellEducationManagement = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellInformatics = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellForeignLanguage = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellEthnicLanguage = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNote = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellWorkUnit = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblReportTitle = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tblReportHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCellTenBieuMau = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell601 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell602 = new DevExpress.XtraReports.UI.XRTableCell();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblLuongHienHuong = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell242 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell241 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tblPageHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellGroupHead = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblReportHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblDetail});
            this.Detail.HeightF = 50F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // tblDetail
            // 
            this.tblDetail.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.tblDetail.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tblDetail.Name = "tblDetail";
            this.tblDetail.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrDetailRow1});
            this.tblDetail.SizeF = new System.Drawing.SizeF(1140F, 50F);
            this.tblDetail.StylePriority.UseBorders = false;
            // 
            // xrDetailRow1
            // 
            this.xrDetailRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellOrder,
            this.xrTableCellFullName,
            this.xrTableCellSex,
            this.xrTableCellNation,
            this.xrTableCellBirthday,
            this.xrTableCellNativeLand,
            this.xrTableCellPlaceOfResidence,
            this.xrTableCellDateAdmittedToTheParty,
            this.xrTableCellDateIntoTheIndustry,
            this.xrTableCellDateAppointed,
            this.xrTableCellPosition,
            this.xrTableCellCode,
            this.xrTableCellDegree,
            this.xrTableCellSpecialized,
            this.xrTableCellPlaceOfTraining,
            this.xrTableCellFormOfTraining,
            this.xrTableCellRating,
            this.xrTableCellCurrentDegree,
            this.xrTableCellCurrentSpecialized,
            this.xrTableCellCurrentPlaceOfTraining,
            this.xrTableCellCurrentFormOfTraining,
            this.xrTableCellCurrentRating,
            this.xrTableCellPoliticalQualifications,
            this.xrTableCellManagerLevel,
            this.xrTableCellEducationManagement,
            this.xrTableCellInformatics,
            this.xrTableCellForeignLanguage,
            this.xrTableCellEthnicLanguage,
            this.xrTableCellNote,
            this.xrTableCellWorkUnit});
            this.xrDetailRow1.Name = "xrDetailRow1";
            this.xrDetailRow1.Weight = 1D;
            // 
            // xrTableCellOrder
            // 
            this.xrTableCellOrder.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOrder.Name = "xrTableCellOrder";
            this.xrTableCellOrder.StylePriority.UseBorders = false;
            this.xrTableCellOrder.StylePriority.UseFont = false;
            this.xrTableCellOrder.StylePriority.UseTextAlignment = false;
            this.xrTableCellOrder.Text = " ";
            this.xrTableCellOrder.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOrder.Weight = 0.12386820436519157D;
            this.xrTableCellOrder.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrTableCellFullName
            // 
            this.xrTableCellFullName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellFullName.Name = "xrTableCellFullName";
            this.xrTableCellFullName.StylePriority.UseBorders = false;
            this.xrTableCellFullName.StylePriority.UseTextAlignment = false;
            this.xrTableCellFullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellFullName.Weight = 0.24773643546027541D;
            // 
            // xrTableCellSex
            // 
            this.xrTableCellSex.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSex.Name = "xrTableCellSex";
            this.xrTableCellSex.StylePriority.UseBorders = false;
            this.xrTableCellSex.StylePriority.UseTextAlignment = false;
            this.xrTableCellSex.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSex.Weight = 0.12386820550256353D;
            // 
            // xrTableCellNation
            // 
            this.xrTableCellNation.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNation.Name = "xrTableCellNation";
            this.xrTableCellNation.StylePriority.UseBorders = false;
            this.xrTableCellNation.StylePriority.UseTextAlignment = false;
            this.xrTableCellNation.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNation.Weight = 0.18580231098108288D;
            // 
            // xrTableCellBirthday
            // 
            this.xrTableCellBirthday.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellBirthday.Name = "xrTableCellBirthday";
            this.xrTableCellBirthday.StylePriority.UseBorders = false;
            this.xrTableCellBirthday.StylePriority.UseTextAlignment = false;
            this.xrTableCellBirthday.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellBirthday.Weight = 0.30967051850382354D;
            // 
            // xrTableCellNativeLand
            // 
            this.xrTableCellNativeLand.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNativeLand.Name = "xrTableCellNativeLand";
            this.xrTableCellNativeLand.StylePriority.UseBorders = false;
            this.xrTableCellNativeLand.StylePriority.UseTextAlignment = false;
            this.xrTableCellNativeLand.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellNativeLand.Weight = 0.30967051850382349D;
            // 
            // xrTableCellPlaceOfResidence
            // 
            this.xrTableCellPlaceOfResidence.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellPlaceOfResidence.Name = "xrTableCellPlaceOfResidence";
            this.xrTableCellPlaceOfResidence.StylePriority.UseBorders = false;
            this.xrTableCellPlaceOfResidence.StylePriority.UseTextAlignment = false;
            this.xrTableCellPlaceOfResidence.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellPlaceOfResidence.Weight = 0.30967051668867651D;
            // 
            // xrTableCellDateAdmittedToTheParty
            // 
            this.xrTableCellDateAdmittedToTheParty.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDateAdmittedToTheParty.Name = "xrTableCellDateAdmittedToTheParty";
            this.xrTableCellDateAdmittedToTheParty.StylePriority.UseBorders = false;
            this.xrTableCellDateAdmittedToTheParty.StylePriority.UseTextAlignment = false;
            this.xrTableCellDateAdmittedToTheParty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDateAdmittedToTheParty.Weight = 0.309670520756078D;
            // 
            // xrTableCellDateIntoTheIndustry
            // 
            this.xrTableCellDateIntoTheIndustry.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDateIntoTheIndustry.Name = "xrTableCellDateIntoTheIndustry";
            this.xrTableCellDateIntoTheIndustry.StylePriority.UseBorders = false;
            this.xrTableCellDateIntoTheIndustry.StylePriority.UseTextAlignment = false;
            this.xrTableCellDateIntoTheIndustry.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDateIntoTheIndustry.Weight = 0.37160462285815843D;
            // 
            // xrTableCellDateAppointed
            // 
            this.xrTableCellDateAppointed.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDateAppointed.Name = "xrTableCellDateAppointed";
            this.xrTableCellDateAppointed.StylePriority.UseBorders = false;
            this.xrTableCellDateAppointed.StylePriority.UseTextAlignment = false;
            this.xrTableCellDateAppointed.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDateAppointed.Weight = 0.30967051787977012D;
            // 
            // xrTableCellPosition
            // 
            this.xrTableCellPosition.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellPosition.Name = "xrTableCellPosition";
            this.xrTableCellPosition.StylePriority.UseBorders = false;
            this.xrTableCellPosition.StylePriority.UseTextAlignment = false;
            this.xrTableCellPosition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellPosition.Weight = 0.30967052020521768D;
            // 
            // xrTableCellCode
            // 
            this.xrTableCellCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCode.Name = "xrTableCellCode";
            this.xrTableCellCode.StylePriority.UseBorders = false;
            this.xrTableCellCode.StylePriority.UseTextAlignment = false;
            this.xrTableCellCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellCode.Weight = 0.24773642653969144D;
            // 
            // xrTableCellDegree
            // 
            this.xrTableCellDegree.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDegree.Name = "xrTableCellDegree";
            this.xrTableCellDegree.StylePriority.UseBorders = false;
            this.xrTableCellDegree.StylePriority.UseTextAlignment = false;
            this.xrTableCellDegree.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellDegree.Weight = 0.21676937380043174D;
            // 
            // xrTableCellSpecialized
            // 
            this.xrTableCellSpecialized.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSpecialized.Name = "xrTableCellSpecialized";
            this.xrTableCellSpecialized.StylePriority.UseBorders = false;
            this.xrTableCellSpecialized.StylePriority.UseTextAlignment = false;
            this.xrTableCellSpecialized.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellSpecialized.Weight = 0.2167693619874386D;
            // 
            // xrTableCellPlaceOfTraining
            // 
            this.xrTableCellPlaceOfTraining.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellPlaceOfTraining.Name = "xrTableCellPlaceOfTraining";
            this.xrTableCellPlaceOfTraining.StylePriority.UseBorders = false;
            this.xrTableCellPlaceOfTraining.StylePriority.UseTextAlignment = false;
            this.xrTableCellPlaceOfTraining.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellPlaceOfTraining.Weight = 0.21676936198743857D;
            // 
            // xrTableCellFormOfTraining
            // 
            this.xrTableCellFormOfTraining.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellFormOfTraining.Name = "xrTableCellFormOfTraining";
            this.xrTableCellFormOfTraining.StylePriority.UseBorders = false;
            this.xrTableCellFormOfTraining.StylePriority.UseTextAlignment = false;
            this.xrTableCellFormOfTraining.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellFormOfTraining.Weight = 0.21676936198743857D;
            // 
            // xrTableCellRating
            // 
            this.xrTableCellRating.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellRating.Name = "xrTableCellRating";
            this.xrTableCellRating.StylePriority.UseBorders = false;
            this.xrTableCellRating.StylePriority.UseTextAlignment = false;
            this.xrTableCellRating.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellRating.Weight = 0.20091422247958254D;
            // 
            // xrTableCellCurrentDegree
            // 
            this.xrTableCellCurrentDegree.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCurrentDegree.Name = "xrTableCellCurrentDegree";
            this.xrTableCellCurrentDegree.StylePriority.UseBorders = false;
            this.xrTableCellCurrentDegree.StylePriority.UseTextAlignment = false;
            this.xrTableCellCurrentDegree.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellCurrentDegree.Weight = 0.2167693619874386D;
            // 
            // xrTableCellCurrentSpecialized
            // 
            this.xrTableCellCurrentSpecialized.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCurrentSpecialized.Name = "xrTableCellCurrentSpecialized";
            this.xrTableCellCurrentSpecialized.StylePriority.UseBorders = false;
            this.xrTableCellCurrentSpecialized.StylePriority.UseTextAlignment = false;
            this.xrTableCellCurrentSpecialized.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellCurrentSpecialized.Weight = 0.2167693619874386D;
            // 
            // xrTableCellCurrentPlaceOfTraining
            // 
            this.xrTableCellCurrentPlaceOfTraining.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCurrentPlaceOfTraining.Name = "xrTableCellCurrentPlaceOfTraining";
            this.xrTableCellCurrentPlaceOfTraining.StylePriority.UseBorders = false;
            this.xrTableCellCurrentPlaceOfTraining.StylePriority.UseTextAlignment = false;
            this.xrTableCellCurrentPlaceOfTraining.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellCurrentPlaceOfTraining.Weight = 0.21676936117074133D;
            // 
            // xrTableCellCurrentFormOfTraining
            // 
            this.xrTableCellCurrentFormOfTraining.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCurrentFormOfTraining.Name = "xrTableCellCurrentFormOfTraining";
            this.xrTableCellCurrentFormOfTraining.StylePriority.UseBorders = false;
            this.xrTableCellCurrentFormOfTraining.StylePriority.UseTextAlignment = false;
            this.xrTableCellCurrentFormOfTraining.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellCurrentFormOfTraining.Weight = 0.21676936117074136D;
            // 
            // xrTableCellCurrentRating
            // 
            this.xrTableCellCurrentRating.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCurrentRating.Name = "xrTableCellCurrentRating";
            this.xrTableCellCurrentRating.StylePriority.UseBorders = false;
            this.xrTableCellCurrentRating.StylePriority.UseTextAlignment = false;
            this.xrTableCellCurrentRating.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellCurrentRating.Weight = 0.21676936117074147D;
            // 
            // xrTableCellPoliticalQualifications
            // 
            this.xrTableCellPoliticalQualifications.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellPoliticalQualifications.Name = "xrTableCellPoliticalQualifications";
            this.xrTableCellPoliticalQualifications.StylePriority.UseBorders = false;
            this.xrTableCellPoliticalQualifications.StylePriority.UseTextAlignment = false;
            this.xrTableCellPoliticalQualifications.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellPoliticalQualifications.Weight = 0.21676936020685511D;
            // 
            // xrTableCellManagerLevel
            // 
            this.xrTableCellManagerLevel.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellManagerLevel.Name = "xrTableCellManagerLevel";
            this.xrTableCellManagerLevel.StylePriority.UseBorders = false;
            this.xrTableCellManagerLevel.StylePriority.UseTextAlignment = false;
            this.xrTableCellManagerLevel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellManagerLevel.Weight = 0.21676936036465377D;
            // 
            // xrTableCellEducationManagement
            // 
            this.xrTableCellEducationManagement.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellEducationManagement.Name = "xrTableCellEducationManagement";
            this.xrTableCellEducationManagement.StylePriority.UseBorders = false;
            this.xrTableCellEducationManagement.StylePriority.UseTextAlignment = false;
            this.xrTableCellEducationManagement.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellEducationManagement.Weight = 0.21676936036465366D;
            // 
            // xrTableCellInformatics
            // 
            this.xrTableCellInformatics.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellInformatics.Name = "xrTableCellInformatics";
            this.xrTableCellInformatics.StylePriority.UseBorders = false;
            this.xrTableCellInformatics.StylePriority.UseTextAlignment = false;
            this.xrTableCellInformatics.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellInformatics.Weight = 0.23262461798223649D;
            // 
            // xrTableCellForeignLanguage
            // 
            this.xrTableCellForeignLanguage.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellForeignLanguage.Name = "xrTableCellForeignLanguage";
            this.xrTableCellForeignLanguage.StylePriority.UseBorders = false;
            this.xrTableCellForeignLanguage.StylePriority.UseTextAlignment = false;
            this.xrTableCellForeignLanguage.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellForeignLanguage.Weight = 0.21676936034444905D;
            // 
            // xrTableCellEthnicLanguage
            // 
            this.xrTableCellEthnicLanguage.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellEthnicLanguage.Name = "xrTableCellEthnicLanguage";
            this.xrTableCellEthnicLanguage.StylePriority.UseBorders = false;
            this.xrTableCellEthnicLanguage.StylePriority.UseTextAlignment = false;
            this.xrTableCellEthnicLanguage.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellEthnicLanguage.Weight = 0.21676936034444905D;
            // 
            // xrTableCellNote
            // 
            this.xrTableCellNote.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNote.Name = "xrTableCellNote";
            this.xrTableCellNote.StylePriority.UseBorders = false;
            this.xrTableCellNote.StylePriority.UseTextAlignment = false;
            this.xrTableCellNote.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNote.Weight = 0.21676936034444905D;
            // 
            // xrTableCellWorkUnit
            // 
            this.xrTableCellWorkUnit.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellWorkUnit.Name = "xrTableCellWorkUnit";
            this.xrTableCellWorkUnit.StylePriority.UseBorders = false;
            this.xrTableCellWorkUnit.StylePriority.UseTextAlignment = false;
            this.xrTableCellWorkUnit.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellWorkUnit.Weight = 0.21676936034444905D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 15F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4,
            this.tblReportHeader});
            this.ReportHeader.HeightF = 86F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrTable4
            // 
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 42F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2,
            this.xrTableRow3});
            this.xrTable4.SizeF = new System.Drawing.SizeF(1140F, 34F);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblReportTitle});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 0.79999990231146945D;
            // 
            // lblReportTitle
            // 
            this.lblReportTitle.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblReportTitle.Name = "lblReportTitle";
            this.lblReportTitle.StylePriority.UseFont = false;
            this.lblReportTitle.Text = "DANH SÁCH CBQL, GIÁO VIÊN VÀ NHÂN VIÊN TRONG CÁC ĐƠN VỊ TRƯỜNG HỌC TÍNH ĐẾN THỜI " +
    "ĐIỂM {0}";
            this.lblReportTitle.Weight = 4.867675964494425D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell26,
            this.xrTableCell27});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 0.33333324536656561D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrTableCell26.StylePriority.UsePadding = false;
            this.xrTableCell26.Weight = 1.5748560433256003D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.Weight = 3.2928199438412697D;
            // 
            // tblReportHeader
            // 
            this.tblReportHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tblReportHeader.Name = "tblReportHeader";
            this.tblReportHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow19,
            this.xrTableRow4});
            this.tblReportHeader.SizeF = new System.Drawing.SizeF(1140F, 34F);
            // 
            // xrTableRow19
            // 
            this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCellTenBieuMau});
            this.xrTableRow19.Name = "xrTableRow19";
            this.xrTableRow19.Weight = 0.79999990231146945D;
            // 
            // xrCellTenBieuMau
            // 
            this.xrCellTenBieuMau.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrCellTenBieuMau.Name = "xrCellTenBieuMau";
            this.xrCellTenBieuMau.StylePriority.UseFont = false;
            this.xrCellTenBieuMau.StylePriority.UseTextAlignment = false;
            this.xrCellTenBieuMau.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCellTenBieuMau.Weight = 4.867675964494425D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell601,
            this.xrTableCell602});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 0.33333324536656561D;
            // 
            // xrTableCell601
            // 
            this.xrTableCell601.Name = "xrTableCell601";
            this.xrTableCell601.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrTableCell601.StylePriority.UsePadding = false;
            this.xrTableCell601.Weight = 1.5748560433256003D;
            // 
            // xrTableCell602
            // 
            this.xrTableCell602.Name = "xrTableCell602";
            this.xrTableCell602.Weight = 3.2928199438412697D;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // ReportFooter
            // 
            this.ReportFooter.HeightF = 252.1667F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // lblLuongHienHuong
            // 
            this.lblLuongHienHuong.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lblLuongHienHuong.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2.649095E-05F);
            this.lblLuongHienHuong.Name = "lblLuongHienHuong";
            this.lblLuongHienHuong.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblLuongHienHuong.SizeF = new System.Drawing.SizeF(172.4395F, 25F);
            this.lblLuongHienHuong.StylePriority.UseBorders = false;
            this.lblLuongHienHuong.Text = "Trình độ đào tạo ban đầu";
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell9.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1,
            this.lblLuongHienHuong});
            this.xrTableCell9.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorderColor = false;
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.Text = "xrTableCell9";
            this.xrTableCell9.Weight = 0.73638024803568347D;
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25.00002F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(172.4394F, 75F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell6,
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell17});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.Multiline = true;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.Text = "Trình độ \r\nchuyên môn";
            this.xrTableCell3.Weight = 0.35041500346154242D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell6.Multiline = true;
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseBorders = false;
            this.xrTableCell6.Text = "Chuyên\r\nngành đào tạo";
            this.xrTableCell6.Weight = 0.35041500968240163D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBorders = false;
            this.xrTableCell11.Text = "Nơi đào tạo";
            this.xrTableCell11.Weight = 0.35041500968240147D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseBorders = false;
            this.xrTableCell12.Text = "Hình thức đào tạo";
            this.xrTableCell12.Weight = 0.35041500038106366D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.StylePriority.UseBorders = false;
            this.xrTableCell17.Text = "Xếp hạng bằng";
            this.xrTableCell17.Weight = 0.32477856200780242D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorderColor = false;
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Text = "Mã hạng chức danh nghề nghiệp (hoặc mã ngạch)";
            this.xrTableCell2.Weight = 0.17081485539880514D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell8.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell8.Multiline = true;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseBorderColor = false;
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.Text = "Chức\r\nvụ/ Chức danh";
            this.xrTableCell8.Weight = 0.21351857060367852D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorderColor = false;
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "Ngày bổ nhiệm vào ngạch viên chức";
            this.xrTableCell1.Weight = 0.21351858161174198D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell21.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell21.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell21.Multiline = true;
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseBorderColor = false;
            this.xrTableCell21.StylePriority.UseBorders = false;
            this.xrTableCell21.StylePriority.UseFont = false;
            this.xrTableCell21.Text = "Ngày \r\nvào ngành, hợp đồng, tuyển dụng";
            this.xrTableCell21.Weight = 0.25622231684826918D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell10.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseBorderColor = false;
            this.xrTableCell10.StylePriority.UseBorders = false;
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.Text = "Ngày, tháng, năm kết nạp đảng";
            this.xrTableCell10.Weight = 0.21351855090108354D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell16.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell16.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseBorderColor = false;
            this.xrTableCell16.StylePriority.UseBorders = false;
            this.xrTableCell16.StylePriority.UseFont = false;
            this.xrTableCell16.Text = "Ngày, tháng, năm sinh";
            this.xrTableCell16.Weight = 0.21351857506683697D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell15.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseBorderColor = false;
            this.xrTableCell15.StylePriority.UseBorders = false;
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.Text = "Dân tộc";
            this.xrTableCell15.Weight = 0.12811115324104644D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell14.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseBorderColor = false;
            this.xrTableCell14.StylePriority.UseBorders = false;
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.Text = "Nữ";
            this.xrTableCell14.Weight = 0.085407422048479953D;
            // 
            // xrTableCell242
            // 
            this.xrTableCell242.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell242.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell242.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell242.Name = "xrTableCell242";
            this.xrTableCell242.StylePriority.UseBorderColor = false;
            this.xrTableCell242.StylePriority.UseBorders = false;
            this.xrTableCell242.StylePriority.UseFont = false;
            this.xrTableCell242.Text = "Họ và tên";
            this.xrTableCell242.Weight = 0.17081486252858286D;
            // 
            // xrTableCell241
            // 
            this.xrTableCell241.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell241.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell241.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell241.Name = "xrTableCell241";
            this.xrTableCell241.StylePriority.UseBorderColor = false;
            this.xrTableCell241.StylePriority.UseBorders = false;
            this.xrTableCell241.StylePriority.UseFont = false;
            this.xrTableCell241.Text = "Stt";
            this.xrTableCell241.Weight = 0.085407436108846449D;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell241,
            this.xrTableCell242,
            this.xrTableCell14,
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell7,
            this.xrTableCell19,
            this.xrTableCell10,
            this.xrTableCell21,
            this.xrTableCell1,
            this.xrTableCell8,
            this.xrTableCell2,
            this.xrTableCell9});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 9.2D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell7.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBorderColor = false;
            this.xrTableCell7.StylePriority.UseBorders = false;
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.Text = "Quê quán";
            this.xrTableCell7.Weight = 0.21351855877666276D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell19.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell19.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.StylePriority.UseBorderColor = false;
            this.xrTableCell19.StylePriority.UseBorders = false;
            this.xrTableCell19.StylePriority.UseFont = false;
            this.xrTableCell19.Text = "Nơi thường trú hiện nay";
            this.xrTableCell19.Weight = 0.21351855877666276D;
            // 
            // tblPageHeader
            // 
            this.tblPageHeader.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tblPageHeader.LocationFloat = new DevExpress.Utils.PointFloat(6.103516E-05F, 0F);
            this.tblPageHeader.Name = "tblPageHeader";
            this.tblPageHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
            this.tblPageHeader.SizeF = new System.Drawing.SizeF(682.4394F, 100F);
            this.tblPageHeader.StylePriority.UseBorders = false;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(682.4396F, 0F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(174.9999F, 25F);
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.Text = "Trình độ hiện nay";
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseBorders = false;
            this.xrTableCell18.Text = "Xếp hạng bằng";
            this.xrTableCell18.Weight = 0.35041500298642614D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseBorders = false;
            this.xrTableCell13.Text = "Hình thức đào tạo";
            this.xrTableCell13.Weight = 0.35041500038106366D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell5.Multiline = true;
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.Text = "Chuyên\r\n ngành đào tạo";
            this.xrTableCell5.Weight = 0.35041500968240163D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell4.Multiline = true;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.Text = "Trình độ\r\nchuyên môn";
            this.xrTableCell4.Weight = 0.35041500346154242D;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell20,
            this.xrTableCell13,
            this.xrTableCell18});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseBorders = false;
            this.xrTableCell20.Text = "Nơi đào tạo";
            this.xrTableCell20.Weight = 0.35041500968240147D;
            // 
            // xrTable3
            // 
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(682.4395F, 25.00002F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrTable3.SizeF = new System.Drawing.SizeF(175F, 75F);
            // 
            // xrLabel3
            // 
            this.xrLabel3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(857.4395F, 0F);
            this.xrLabel3.Multiline = true;
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(35F, 99.99998F);
            this.xrLabel3.StylePriority.UseBorders = false;
            this.xrLabel3.StylePriority.UsePadding = false;
            this.xrLabel3.Text = "Trình độ LL\r\nchính trị";
            // 
            // xrLabel4
            // 
            this.xrLabel4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(892.4395F, 0F);
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(35F, 99.99998F);
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UsePadding = false;
            this.xrLabel4.Text = "Trình độ QLNN";
            // 
            // xrLabel5
            // 
            this.xrLabel5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(927.4395F, 0F);
            this.xrLabel5.Multiline = true;
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(35F, 99.99998F);
            this.xrLabel5.StylePriority.UseBorders = false;
            this.xrLabel5.StylePriority.UsePadding = false;
            this.xrLabel5.Text = "Trình độ QLGD";
            // 
            // xrLabel6
            // 
            this.xrLabel6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(962.4395F, 0F);
            this.xrLabel6.Multiline = true;
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(37.56055F, 99.99998F);
            this.xrLabel6.StylePriority.UseBorders = false;
            this.xrLabel6.StylePriority.UsePadding = false;
            this.xrLabel6.Text = "Tin\r\nhọc";
            // 
            // xrLabel7
            // 
            this.xrLabel7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(1000F, 0F);
            this.xrLabel7.Multiline = true;
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel7.ProcessDuplicatesMode = DevExpress.XtraReports.UI.ProcessDuplicatesMode.Merge;
            this.xrLabel7.SizeF = new System.Drawing.SizeF(35F, 99.99998F);
            this.xrLabel7.StylePriority.UseBorders = false;
            this.xrLabel7.StylePriority.UsePadding = false;
            this.xrLabel7.Text = "N. ngữ";
            // 
            // xrLabel8
            // 
            this.xrLabel8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(1035F, 0F);
            this.xrLabel8.Multiline = true;
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(35F, 99.99998F);
            this.xrLabel8.StylePriority.UseBorders = false;
            this.xrLabel8.StylePriority.UsePadding = false;
            this.xrLabel8.Text = "Biết thông thạo 01 thứ tiếng dân tộc trở lên";
            // 
            // xrLabel9
            // 
            this.xrLabel9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(1070F, 0F);
            this.xrLabel9.Multiline = true;
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(35F, 99.99998F);
            this.xrLabel9.StylePriority.UseBorders = false;
            this.xrLabel9.StylePriority.UsePadding = false;
            this.xrLabel9.Text = "Ghi\r\nchú";
            // 
            // xrLabel10
            // 
            this.xrLabel10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(1105F, 2.445319E-05F);
            this.xrLabel10.Multiline = true;
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(35F, 99.99998F);
            this.xrLabel10.StylePriority.UseBorders = false;
            this.xrLabel10.StylePriority.UsePadding = false;
            this.xrLabel10.Text = "Đơn vị công tác";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel10,
            this.xrLabel9,
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.xrTable3,
            this.xrLabel2,
            this.tblPageHeader});
            this.PageHeader.HeightF = 100F;
            this.PageHeader.Name = "PageHeader";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.GroupHeader1.HeightF = 29.04167F;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1140F, 28.95831F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UsePadding = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell22,
            this.xrTableCellGroupHead});
            this.xrTableRow6.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
            this.xrTableRow6.StylePriority.UseFont = false;
            this.xrTableRow6.StylePriority.UsePadding = false;
            this.xrTableRow6.StylePriority.UseTextAlignment = false;
            this.xrTableRow6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell22.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseBorders = false;
            this.xrTableCell22.StylePriority.UseFont = false;
            this.xrTableCell22.StylePriority.UseTextAlignment = false;
            this.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell22.Weight = 0.20210515934183412D;
            // 
            // xrTableCellGroupHead
            // 
            this.xrTableCellGroupHead.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellGroupHead.Name = "xrTableCellGroupHead";
            this.xrTableCellGroupHead.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellGroupHead.StylePriority.UseFont = false;
            this.xrTableCellGroupHead.StylePriority.UsePadding = false;
            this.xrTableCellGroupHead.StylePriority.UseTextAlignment = false;
            this.xrTableCellGroupHead.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellGroupHead.Weight = 11.317889084252602D;
            // 
            // rpHJM_ListEmployeeByPosition
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter,
            this.GroupHeader1});
            this.BorderColor = System.Drawing.Color.DarkGray;
            this.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(12, 11, 15, 0);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblReportHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}
