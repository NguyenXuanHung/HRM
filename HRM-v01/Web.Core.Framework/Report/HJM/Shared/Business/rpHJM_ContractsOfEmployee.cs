using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Linq;
using DevExpress.XtraReports.UI;
using Web.Core.Framework.Adapter;
using Web.Core.Framework.Interface;
using Web.Core.Framework.Report.Base;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Bao cao danh sach hop dong lao dong cua can bo
    /// Summary description for rpHJM_ContractsOfEmployee
    /// </summary>
    public class rpHJM_ContractsOfEmployee : XtraReport, IBaseReport
    {
        private DetailBand Detail;
        private TopMarginBand TopMargin;
        private BottomMarginBand BottomMargin;
        private ReportHeaderBand ReportHeader;
        private PageHeaderBand PageHeader;
        private FormattingRule formattingRule1;
        private ReportFooterBand ReportFooter;
        private XRLabel lblKyHoTen;
        private XRLabel lblCreatedByTitle;
        private XRLabel lblSignedByTitle;
        private XRTable tblDetail;
        private XRTableRow xrDetailRow1;
        private XRTableCell xrt_FullName;
        private XRTableCell xrt_CurDepartment;
        private XRTableCell xrTableCellDonViNoiDen;
        private XRTableCell xrt_ContractNumber;
        private XRLabel lblReportDate;
        private XRLabel lblKyDongDau;
        private XRTable tblPageHeader;
        private XRTableRow xrTableRow11;
        private XRTableCell xrTableCell241;
        private XRTableCell xrTableCell242;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell8;
        private XRTableCell xrt_STT;
        private XRTable xrTable4;
        private XRTableRow xrTableRow2;
        private XRTableCell xrTableCell10;
        private XRTableCell xrTableCell14;
        private XRTableRow xrTableRow3;
        private XRTableCell lblOrgName;
        private XRTableCell xrTableCell17;
        private XRTableRow xrTableRow4;
        private XRTableCell lblReportTitle;
        private XRTableRow xrTableRow7;
        private XRTableCell lblDuration;
        private XRTableRow xrTableRow8;
        private XRTableCell xrTableCell21;
        private XRLabel xrLabel2;
        private XRLabel lblReviewedByTitle;
        private XRTableCell xrt_EmployeeCode;
        private XRTableCell xrTableCell11;
        private XRTableCell xrt_CurPosition;
        private XRTableCell xrTableCell12;
        private XRTableCell xrt_ContractDate;
        private XRTableCell xrt_ContractType;
        private XRTableCell xrt_ContractTime;
        private XRTableCell xrt_PersonPosition;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCell23;
        private XRTableCell xrTableCell22;
        private XRTableCell xrTableCell4;
        private XRTableCell xrTableCell3;
        private XRTableCell xrt_SignPerson;
        private XRTableCell xrt_ContractTimeRemain;
        private XRTableCell xrTableCell5;
        private GroupHeaderBand GroupHeader1;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell6;
        private XRTableCell xrTableCellGroupHead;
        private XRLabel lblSignedByName;
        private XRLabel lblReviewedByName;
        private XRLabel lblCreatedByName;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
        private IContainer components = null;
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

            // filter salary contact type            
            _filter.Items.Add(FilterGenerate.ContractTypeFilter(true, GenerateFilterType.Hc));

            // filter contact status            
            _filter.Items.Add(FilterGenerate.ContractStatusFilter(true, GenerateFilterType.Hc));

            //filter position
            _filter.Items.Add(FilterGenerate.PositionFilter());

            //filter department
            _filter.Items.Add(FilterGenerate.DepartmentFilter());
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public rpHJM_ContractsOfEmployee()
        {
            // init compoent
            InitializeComponent();

            // init Filter
            InitFilter();
        }

        #endregion
        int _stt;
        private void Detail_BeforePrint(object sender, PrintEventArgs e)
        {
            _stt++;
            xrt_STT.Text = _stt.ToString();

        }
        private void Group_BeforePrint(object sender, PrintEventArgs e)
        {
            _stt = 0;
            xrt_STT.Text = _stt.ToString();
        }

        public void BindData()
        {
            try
            {                
                // from date
                var fromDate = _filter.StartDate != null ? _filter.StartDate.Value.ToString("yyyy-MM-dd 00:00:00") : string.Empty;
                // to date
                var toDate = _filter.EndDate != null ? _filter.EndDate.Value.ToString("yyyy-MM-dd 23:59:59") : string.Empty;
                // select form db 
                var table = SQLHelper.ExecuteTable(SQLReportAdapter.GetStore_ContractOfEmployee(string.Join(",", _filter.Departments.Split(
                    new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(d => "'{0}'".FormatWith(d))), fromDate, toDate, _filter.Condition));
                DataSource = table;

                //binding data detail
                xrt_EmployeeCode.DataBindings.Add("Text", DataSource, "EmployeeCode");
                xrt_FullName.DataBindings.Add("Text", DataSource, "FullName");
                xrt_CurDepartment.DataBindings.Add("Text", DataSource, "CurrentDepartment");
                xrt_CurPosition.DataBindings.Add("Text", DataSource, "CurrentPosition");
                xrt_ContractNumber.DataBindings.Add("Text", DataSource, "ContractNumber");
                xrt_ContractDate.DataBindings.Add("Text", DataSource, "ContractDate", "{0:dd/MM/yyyy}");
                xrt_ContractType.DataBindings.Add("Text", DataSource, "ContractTypeName");
                xrt_ContractTime.DataBindings.Add("Text", DataSource, "ContractTime");
                xrt_SignPerson.DataBindings.Add("Text", DataSource, "Signer");
                xrt_PersonPosition.DataBindings.Add("Text", DataSource, "PositionSigner");
                xrt_ContractTimeRemain.DataBindings.Add("Text", DataSource, "ContractTimeRemain");
                GroupHeader1.GroupFields.AddRange(new[] {  new GroupField("DepartmentId", XRColumnSortOrder.Ascending)});
                xrTableCellGroupHead.DataBindings.Add("Text", DataSource, "DepartmentName");
                // other items
                // label org name
                if(!string.IsNullOrEmpty(_filter.OrganizationName)) lblOrgName.Text = _filter.OrganizationName;
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
                if(!string.IsNullOrEmpty(_filter.ReviewedByTitle)) lblReviewedByTitle.Text = _filter.ReviewedByTitle;
                if(!string.IsNullOrEmpty(_filter.ReviewedByName)) lblReviewedByName.Text = _filter.ReviewedByName;
                // signed by
                if(!string.IsNullOrEmpty(_filter.SignedByTitle)) lblSignedByTitle.Text = _filter.SignedByTitle;
                if(!string.IsNullOrEmpty(_filter.SignedByName)) lblSignedByName.Text = _filter.SignedByName;
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
            this.tblDetail = new DevExpress.XtraReports.UI.XRTable();
            this.xrDetailRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrt_STT = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_EmployeeCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_FullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_CurDepartment = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_CurPosition = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_ContractNumber = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_ContractDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_ContractType = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_ContractTime = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_SignPerson = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_PersonPosition = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_ContractTimeRemain = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblOrgName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblReportTitle = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblDuration = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.tblPageHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell241 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell242 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblSignedByName = new DevExpress.XtraReports.UI.XRLabel();
            this.lblReviewedByName = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCreatedByName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblReviewedByTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.lblReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblKyDongDau = new DevExpress.XtraReports.UI.XRLabel();
            this.lblKyHoTen = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCreatedByTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSignedByTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellGroupHead = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblDetail});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // tblDetail
            // 
            this.tblDetail.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tblDetail.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tblDetail.Name = "tblDetail";
            this.tblDetail.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrDetailRow1});
            this.tblDetail.SizeF = new System.Drawing.SizeF(1140F, 25F);
            this.tblDetail.StylePriority.UseBorders = false;
            // 
            // xrDetailRow1
            // 
            this.xrDetailRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrt_STT,
            this.xrt_EmployeeCode,
            this.xrt_FullName,
            this.xrt_CurDepartment,
            this.xrt_CurPosition,
            this.xrt_ContractNumber,
            this.xrt_ContractDate,
            this.xrt_ContractType,
            this.xrt_ContractTime,
            this.xrt_SignPerson,
            this.xrt_PersonPosition,
            this.xrt_ContractTimeRemain});
            this.xrDetailRow1.Name = "xrDetailRow1";
            this.xrDetailRow1.Weight = 1D;
            // 
            // xrt_STT
            // 
            this.xrt_STT.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_STT.Name = "xrt_STT";
            this.xrt_STT.StylePriority.UseBorders = false;
            this.xrt_STT.StylePriority.UseFont = false;
            this.xrt_STT.StylePriority.UseTextAlignment = false;
            this.xrt_STT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_STT.Weight = 0.04556674448925447D;
            this.xrt_STT.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrt_EmployeeCode
            // 
            this.xrt_EmployeeCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_EmployeeCode.Name = "xrt_EmployeeCode";
            this.xrt_EmployeeCode.StylePriority.UseBorders = false;
            this.xrt_EmployeeCode.StylePriority.UseTextAlignment = false;
            this.xrt_EmployeeCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_EmployeeCode.Weight = 0.12916286338652194D;
            // 
            // xrt_FullName
            // 
            this.xrt_FullName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_FullName.Name = "xrt_FullName";
            this.xrt_FullName.StylePriority.UseBorders = false;
            this.xrt_FullName.StylePriority.UseTextAlignment = false;
            this.xrt_FullName.Text = " ";
            this.xrt_FullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_FullName.Weight = 0.27447108443656304D;
            // 
            // xrt_CurDepartment
            // 
            this.xrt_CurDepartment.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_CurDepartment.Name = "xrt_CurDepartment";
            this.xrt_CurDepartment.StylePriority.UseBorders = false;
            this.xrt_CurDepartment.StylePriority.UseTextAlignment = false;
            this.xrt_CurDepartment.Text = " ";
            this.xrt_CurDepartment.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_CurDepartment.Weight = 0.24389342795476549D;
            // 
            // xrt_CurPosition
            // 
            this.xrt_CurPosition.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_CurPosition.Name = "xrt_CurPosition";
            this.xrt_CurPosition.StylePriority.UseBorders = false;
            this.xrt_CurPosition.StylePriority.UseTextAlignment = false;
            this.xrt_CurPosition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_CurPosition.Weight = 0.19871211244366224D;
            // 
            // xrt_ContractNumber
            // 
            this.xrt_ContractNumber.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_ContractNumber.Name = "xrt_ContractNumber";
            this.xrt_ContractNumber.StylePriority.UseBorders = false;
            this.xrt_ContractNumber.StylePriority.UseTextAlignment = false;
            this.xrt_ContractNumber.Text = " ";
            this.xrt_ContractNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_ContractNumber.Weight = 0.12530853133657921D;
            // 
            // xrt_ContractDate
            // 
            this.xrt_ContractDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_ContractDate.Name = "xrt_ContractDate";
            this.xrt_ContractDate.StylePriority.UseBorders = false;
            this.xrt_ContractDate.StylePriority.UseTextAlignment = false;
            this.xrt_ContractDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_ContractDate.Weight = 0.12042637760201318D;
            // 
            // xrt_ContractType
            // 
            this.xrt_ContractType.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_ContractType.Name = "xrt_ContractType";
            this.xrt_ContractType.StylePriority.UseBorders = false;
            this.xrt_ContractType.StylePriority.UseTextAlignment = false;
            this.xrt_ContractType.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_ContractType.Weight = 0.13181807425674136D;
            // 
            // xrt_ContractTime
            // 
            this.xrt_ContractTime.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_ContractTime.Name = "xrt_ContractTime";
            this.xrt_ContractTime.StylePriority.UseBorders = false;
            this.xrt_ContractTime.StylePriority.UseTextAlignment = false;
            this.xrt_ContractTime.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_ContractTime.Weight = 0.16924779163020889D;
            // 
            // xrt_SignPerson
            // 
            this.xrt_SignPerson.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_SignPerson.Name = "xrt_SignPerson";
            this.xrt_SignPerson.StylePriority.UseBorders = false;
            this.xrt_SignPerson.StylePriority.UseTextAlignment = false;
            this.xrt_SignPerson.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_SignPerson.Weight = 0.12916286514016193D;
            // 
            // xrt_PersonPosition
            // 
            this.xrt_PersonPosition.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_PersonPosition.Name = "xrt_PersonPosition";
            this.xrt_PersonPosition.StylePriority.UseBorders = false;
            this.xrt_PersonPosition.StylePriority.UseTextAlignment = false;
            this.xrt_PersonPosition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_PersonPosition.Weight = 0.12916285839886013D;
            // 
            // xrt_ContractTimeRemain
            // 
            this.xrt_ContractTimeRemain.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_ContractTimeRemain.Name = "xrt_ContractTimeRemain";
            this.xrt_ContractTimeRemain.StylePriority.UseBorders = false;
            this.xrt_ContractTimeRemain.StylePriority.UseTextAlignment = false;
            this.xrt_ContractTimeRemain.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_ContractTimeRemain.Weight = 0.14363807861822703D;
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
            this.xrTable4});
            this.ReportHeader.HeightF = 106F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrTable4
            // 
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow4,
            this.xrTableRow7,
            this.xrTableRow8});
            this.xrTable4.SizeF = new System.Drawing.SizeF(1140F, 106F);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10,
            this.xrTableCell14});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 0.96D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.Weight = 0.537853321276213D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.Weight = 2.4621466787237871D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblOrgName,
            this.xrTableCell17});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 0.96D;
            // 
            // lblOrgName
            // 
            this.lblOrgName.Name = "lblOrgName";
            this.lblOrgName.Text = "TÊN CƠ QUAN ĐƠN VỊ";
            this.lblOrgName.Weight = 0.53785336143092111D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.Weight = 2.4621466385690791D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblReportTitle});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 0.96000000000000008D;
            // 
            // lblReportTitle
            // 
            this.lblReportTitle.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblReportTitle.Name = "lblReportTitle";
            this.lblReportTitle.StylePriority.UseFont = false;
            this.lblReportTitle.Text = "BÁO CÁO DANH SÁCH HỢP ĐỒNG LAO ĐỘNG CỦA CÁN BỘ";
            this.lblReportTitle.Weight = 3D;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblDuration});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 0.96000000000000008D;
            // 
            // lblDuration
            // 
            this.lblDuration.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.StylePriority.UseFont = false;
            this.lblDuration.Text = "Từ ngày: {0} đến ngày: {1}";
            this.lblDuration.Weight = 3D;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell21});
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 0.4D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.Weight = 3D;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblPageHeader});
            this.PageHeader.HeightF = 40.41314F;
            this.PageHeader.Name = "PageHeader";
            // 
            // tblPageHeader
            // 
            this.tblPageHeader.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tblPageHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tblPageHeader.Name = "tblPageHeader";
            this.tblPageHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
            this.tblPageHeader.SizeF = new System.Drawing.SizeF(1140F, 40F);
            this.tblPageHeader.StylePriority.UseBorders = false;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell241,
            this.xrTableCell11,
            this.xrTableCell242,
            this.xrTableCell1,
            this.xrTableCell12,
            this.xrTableCell8,
            this.xrTableCell2,
            this.xrTableCell23,
            this.xrTableCell22,
            this.xrTableCell4,
            this.xrTableCell3,
            this.xrTableCell5});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 9.2D;
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
            this.xrTableCell241.Weight = 0.0653820143067185D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell11.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBorderColor = false;
            this.xrTableCell11.StylePriority.UseBorders = false;
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.Text = "Số hiệu CBCC";
            this.xrTableCell11.Weight = 0.18533097356537384D;
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
            this.xrTableCell242.Weight = 0.39382832165025866D;
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
            this.xrTableCell1.Text = "Đơn vị đang công tác";
            this.xrTableCell1.Weight = 0.34995352997394924D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell12.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseBorderColor = false;
            this.xrTableCell12.StylePriority.UseBorders = false;
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.Text = "Chức vụ hiện tại";
            this.xrTableCell12.Weight = 0.28512458805698387D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell8.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseBorderColor = false;
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.Text = "Số hợp đồng";
            this.xrTableCell8.Weight = 0.17980053548040426D;
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
            this.xrTableCell2.Text = "Ngày ký";
            this.xrTableCell2.Weight = 0.17279531317848856D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell23.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell23.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StylePriority.UseBorderColor = false;
            this.xrTableCell23.StylePriority.UseBorders = false;
            this.xrTableCell23.StylePriority.UseFont = false;
            this.xrTableCell23.Text = "Loại hợp đồng";
            this.xrTableCell23.Weight = 0.1891408184562875D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell22.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell22.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseBorderColor = false;
            this.xrTableCell22.StylePriority.UseBorders = false;
            this.xrTableCell22.StylePriority.UseFont = false;
            this.xrTableCell22.Text = "Thời hạn hợp đồng";
            this.xrTableCell22.Weight = 0.242847478015033D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell4.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseBorderColor = false;
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.Text = "Người ký";
            this.xrTableCell4.Weight = 0.1853309697598825D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorderColor = false;
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.Text = "Chức vụ người ký";
            this.xrTableCell3.Weight = 0.18533097104652793D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell5.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBorderColor = false;
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.Text = "Thời hạn còn lại";
            this.xrTableCell5.Weight = 0.206100821984888D;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblSignedByName,
            this.lblReviewedByName,
            this.lblCreatedByName,
            this.xrLabel2,
            this.lblReviewedByTitle,
            this.lblReportDate,
            this.lblKyDongDau,
            this.lblKyHoTen,
            this.lblCreatedByTitle,
            this.lblSignedByTitle});
            this.ReportFooter.HeightF = 252.1667F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // lblSignedByName
            // 
            this.lblSignedByName.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.lblSignedByName.LocationFloat = new DevExpress.Utils.PointFloat(891.0344F, 107.5284F);
            this.lblSignedByName.Name = "lblSignedByName";
            this.lblSignedByName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblSignedByName.SizeF = new System.Drawing.SizeF(180F, 25F);
            this.lblSignedByName.StylePriority.UseFont = false;
            this.lblSignedByName.StylePriority.UseTextAlignment = false;
            this.lblSignedByName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblReviewedByName
            // 
            this.lblReviewedByName.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.lblReviewedByName.LocationFloat = new DevExpress.Utils.PointFloat(423.6537F, 107.5284F);
            this.lblReviewedByName.Name = "lblReviewedByName";
            this.lblReviewedByName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblReviewedByName.SizeF = new System.Drawing.SizeF(180F, 25F);
            this.lblReviewedByName.StylePriority.UseFont = false;
            this.lblReviewedByName.StylePriority.UseTextAlignment = false;
            this.lblReviewedByName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblCreatedByName
            // 
            this.lblCreatedByName.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.lblCreatedByName.LocationFloat = new DevExpress.Utils.PointFloat(24.38431F, 107.5284F);
            this.lblCreatedByName.Name = "lblCreatedByName";
            this.lblCreatedByName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCreatedByName.SizeF = new System.Drawing.SizeF(180F, 25F);
            this.lblCreatedByName.StylePriority.UseFont = false;
            this.lblCreatedByName.StylePriority.UseTextAlignment = false;
            this.lblCreatedByName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Italic);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(423.6537F, 64.82005F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(180F, 10F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "(Chức vụ, ký, ghi rõ họ và tên)";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblReviewedByTitle
            // 
            this.lblReviewedByTitle.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.lblReviewedByTitle.LocationFloat = new DevExpress.Utils.PointFloat(423.6537F, 39.82006F);
            this.lblReviewedByTitle.Name = "lblReviewedByTitle";
            this.lblReviewedByTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblReviewedByTitle.SizeF = new System.Drawing.SizeF(180F, 25F);
            this.lblReviewedByTitle.StylePriority.UseFont = false;
            this.lblReviewedByTitle.StylePriority.UseTextAlignment = false;
            this.lblReviewedByTitle.Text = "NGƯỜI DUYỆT";
            this.lblReviewedByTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblReportDate
            // 
            this.lblReportDate.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Italic);
            this.lblReportDate.LocationFloat = new DevExpress.Utils.PointFloat(821.3764F, 24.82006F);
            this.lblReportDate.Name = "lblReportDate";
            this.lblReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblReportDate.SizeF = new System.Drawing.SizeF(300F, 15F);
            this.lblReportDate.StylePriority.UseFont = false;
            this.lblReportDate.StylePriority.UseTextAlignment = false;
            this.lblReportDate.Text = "Ngày {0} tháng {1} năm {2}";
            this.lblReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblKyDongDau
            // 
            this.lblKyDongDau.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Italic);
            this.lblKyDongDau.LocationFloat = new DevExpress.Utils.PointFloat(821.3764F, 64.82005F);
            this.lblKyDongDau.Name = "lblKyDongDau";
            this.lblKyDongDau.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblKyDongDau.SizeF = new System.Drawing.SizeF(300F, 10F);
            this.lblKyDongDau.StylePriority.UseFont = false;
            this.lblKyDongDau.StylePriority.UseTextAlignment = false;
            this.lblKyDongDau.Text = "(Chức vụ, ký, ghi rõ họ và tên)";
            this.lblKyDongDau.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblKyHoTen
            // 
            this.lblKyHoTen.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Italic);
            this.lblKyHoTen.LocationFloat = new DevExpress.Utils.PointFloat(24.38424F, 64.82005F);
            this.lblKyHoTen.Name = "lblKyHoTen";
            this.lblKyHoTen.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblKyHoTen.SizeF = new System.Drawing.SizeF(180F, 10F);
            this.lblKyHoTen.StylePriority.UseFont = false;
            this.lblKyHoTen.StylePriority.UseTextAlignment = false;
            this.lblKyHoTen.Text = "(Ký, ghi rõ họ và tên)";
            this.lblKyHoTen.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblCreatedByTitle
            // 
            this.lblCreatedByTitle.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.lblCreatedByTitle.LocationFloat = new DevExpress.Utils.PointFloat(24.38424F, 39.82006F);
            this.lblCreatedByTitle.Name = "lblCreatedByTitle";
            this.lblCreatedByTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCreatedByTitle.SizeF = new System.Drawing.SizeF(180F, 25F);
            this.lblCreatedByTitle.StylePriority.UseFont = false;
            this.lblCreatedByTitle.StylePriority.UseTextAlignment = false;
            this.lblCreatedByTitle.Text = "NGƯỜI LẬP";
            this.lblCreatedByTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblSignedByTitle
            // 
            this.lblSignedByTitle.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.lblSignedByTitle.LocationFloat = new DevExpress.Utils.PointFloat(821.3763F, 39.82006F);
            this.lblSignedByTitle.Name = "lblSignedByTitle";
            this.lblSignedByTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblSignedByTitle.SizeF = new System.Drawing.SizeF(300F, 25F);
            this.lblSignedByTitle.StylePriority.UseFont = false;
            this.lblSignedByTitle.StylePriority.UseTextAlignment = false;
            this.lblSignedByTitle.Text = "THỦ TRƯỞNG CƠ QUAN, ĐƠN VỊ";
            this.lblSignedByTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.GroupHeader1.HeightF = 25F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1140F, 25F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell6,
            this.xrTableCellGroupHead});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseBorders = false;
            this.xrTableCell6.Weight = 0.049122796309621741D;
            // 
            // xrTableCellGroupHead
            // 
            this.xrTableCellGroupHead.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellGroupHead.Name = "xrTableCellGroupHead";
            this.xrTableCellGroupHead.StylePriority.UseBorders = false;
            this.xrTableCellGroupHead.StylePriority.UseTextAlignment = false;
            this.xrTableCellGroupHead.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellGroupHead.Weight = 1.9508772036903783D;
            this.xrTableCellGroupHead.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Group_BeforePrint);
            // 
            // rpHJM_ContractsOfEmployee
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
            this.Margins = new System.Drawing.Printing.Margins(12, 12, 15, 0);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}