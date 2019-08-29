using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Ext.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using Web.Core.Framework.Controller;
using Web.Core.Framework.Interface;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Summary description for report payroll
    /// </summary>
    public class rpHRM_Payroll_A2_Potrait : XtraReport, IBaseReport
    {
        #region Layout

        // margin band
        private TopMarginBand _topMargin;
        private BottomMarginBand _bottomMargin;

        // header band
        private ReportHeaderBand _header;

        // header items
        private XRLabel _lblParentDepartment;
        private XRLabel _lblDepartment;
        private XRLabel _lblReportTitle;
        private XRLabel _lblDuration;

        // page Header band
        private PageHeaderBand _pageHeader;

        // group Header band
        private GroupHeaderBand _groupHeader1;
        private GroupHeaderBand _groupHeader2;
        private GroupHeaderBand _groupHeader3;

        // detail band
        private DetailBand _detail;

        // group footer band
        private GroupFooterBand _groupFooter;

        // footer band
        private ReportFooterBand _footer;

        // footer items
        private XRTable _footerTable;
        private XRTableRow _footerRow;
        private XRTableCell _footerCellCreatedBy;
        private XRLabel _lblCreatedByTitle;
        private XRLabel _lblCreatedByNote;
        private XRLabel _lblCreatedByName;
        private XRTableCell _footerCellReviewedBy;
        private XRLabel _lblReviewedByTitle;
        private XRLabel _lblReviewedByNote;
        private XRLabel _lblReviewedByName;
        private XRTableCell _footerCellSignedBy;
        private XRLabel _lblReportDate;
        private XRLabel _lblSignedByTitle;
        private XRLabel _lblSignedByNote;
        private XRLabel _lblSignedByName;

        /// <summary>
        /// Init component
        /// </summary>
        private void InitializeComponent()
        {
            this._topMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this._bottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this._header = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this._lblParentDepartment = new DevExpress.XtraReports.UI.XRLabel();
            this._lblDepartment = new DevExpress.XtraReports.UI.XRLabel();
            this._lblReportTitle = new DevExpress.XtraReports.UI.XRLabel();
            this._lblDuration = new DevExpress.XtraReports.UI.XRLabel();
            this._footer = new DevExpress.XtraReports.UI.ReportFooterBand();
            this._footerTable = new DevExpress.XtraReports.UI.XRTable();
            this._footerRow = new DevExpress.XtraReports.UI.XRTableRow();
            this._footerCellCreatedBy = new DevExpress.XtraReports.UI.XRTableCell();
            this._lblCreatedByName = new DevExpress.XtraReports.UI.XRLabel();
            this._lblCreatedByNote = new DevExpress.XtraReports.UI.XRLabel();
            this._lblCreatedByTitle = new DevExpress.XtraReports.UI.XRLabel();
            this._footerCellReviewedBy = new DevExpress.XtraReports.UI.XRTableCell();
            this._lblReviewedByName = new DevExpress.XtraReports.UI.XRLabel();
            this._lblReviewedByNote = new DevExpress.XtraReports.UI.XRLabel();
            this._lblReviewedByTitle = new DevExpress.XtraReports.UI.XRLabel();
            this._footerCellSignedBy = new DevExpress.XtraReports.UI.XRTableCell();
            this._lblSignedByName = new DevExpress.XtraReports.UI.XRLabel();
            this._lblSignedByNote = new DevExpress.XtraReports.UI.XRLabel();
            this._lblSignedByTitle = new DevExpress.XtraReports.UI.XRLabel();
            this._lblReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this._pageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this._groupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this._detail = new DevExpress.XtraReports.UI.DetailBand();
            this._groupFooter = new DevExpress.XtraReports.UI.GroupFooterBand();
            this._groupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this._groupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this._footerTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // _topMargin
            // 
            this._topMargin.HeightF = 10F;
            this._topMargin.Name = "_topMargin";
            // 
            // _bottomMargin
            // 
            this._bottomMargin.HeightF = 10F;
            this._bottomMargin.Name = "_bottomMargin";
            // 
            // _header
            // 
            this._header.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this._lblParentDepartment,
            this._lblDepartment,
            this._lblReportTitle,
            this._lblDuration});
            this._header.HeightF = 110F;
            this._header.Name = "_header";
            // 
            // _lblParentDepartment
            // 
            this._lblParentDepartment.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this._lblParentDepartment.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this._lblParentDepartment.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this._lblParentDepartment.Name = "_lblParentDepartment";
            this._lblParentDepartment.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this._lblParentDepartment.SizeF = new System.Drawing.SizeF(300F, 25F);
            this._lblParentDepartment.StylePriority.UseBorderColor = false;
            this._lblParentDepartment.StylePriority.UseBorders = false;
            this._lblParentDepartment.StylePriority.UseFont = false;
            this._lblParentDepartment.StylePriority.UseTextAlignment = false;
            this._lblParentDepartment.Text = "TÊN ĐƠN VỊ QUẢN LÝ";
            this._lblParentDepartment.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // _lblDepartment
            // 
            this._lblDepartment.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this._lblDepartment.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this._lblDepartment.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this._lblDepartment.Name = "_lblDepartment";
            this._lblDepartment.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this._lblDepartment.SizeF = new System.Drawing.SizeF(300F, 25F);
            this._lblDepartment.StylePriority.UseBorderColor = false;
            this._lblDepartment.StylePriority.UseBorders = false;
            this._lblDepartment.StylePriority.UseFont = false;
            this._lblDepartment.StylePriority.UseTextAlignment = false;
            this._lblDepartment.Text = "TÊN ĐƠN VỊ";
            this._lblDepartment.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // _lblReportTitle
            // 
            this._lblReportTitle.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this._lblReportTitle.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this._lblReportTitle.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
            this._lblReportTitle.Name = "_lblReportTitle";
            this._lblReportTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this._lblReportTitle.SizeF = new System.Drawing.SizeF(1620F, 25F);
            this._lblReportTitle.StylePriority.UseBorderColor = false;
            this._lblReportTitle.StylePriority.UseBorders = false;
            this._lblReportTitle.StylePriority.UseFont = false;
            this._lblReportTitle.StylePriority.UseTextAlignment = false;
            this._lblReportTitle.Text = "TIÊU ĐỀ BÁO CÁO";
            this._lblReportTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // _lblDuration
            // 
            this._lblDuration.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this._lblDuration.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this._lblDuration.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            this._lblDuration.Name = "_lblDuration";
            this._lblDuration.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this._lblDuration.SizeF = new System.Drawing.SizeF(1620F, 25F);
            this._lblDuration.StylePriority.UseBorderColor = false;
            this._lblDuration.StylePriority.UseBorders = false;
            this._lblDuration.StylePriority.UseFont = false;
            this._lblDuration.StylePriority.UseTextAlignment = false;
            this._lblDuration.Text = "Từ ngày {0} đến ngày {1}";
            this._lblDuration.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // _footer
            // 
            this._footer.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this._footerTable,
            this._lblReportDate});
            this._footer.HeightF = 185F;
            this._footer.Name = "_footer";
            // 
            // _footerTable
            // 
            this._footerTable.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this._footerTable.LocationFloat = new DevExpress.Utils.PointFloat(0F, 52.08333F);
            this._footerTable.Name = "_footerTable";
            this._footerTable.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this._footerRow});
            this._footerTable.SizeF = new System.Drawing.SizeF(1620F, 132.9167F);
            this._footerTable.StylePriority.UseBorders = false;
            // 
            // _footerRow
            // 
            this._footerRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this._footerCellCreatedBy,
            this._footerCellReviewedBy,
            this._footerCellSignedBy});
            this._footerRow.Name = "_footerRow";
            this._footerRow.Weight = 0.16788321167883211D;
            // 
            // _footerCellCreatedBy
            // 
            this._footerCellCreatedBy.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this._footerCellCreatedBy.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this._lblCreatedByName,
            this._lblCreatedByNote,
            this._lblCreatedByTitle});
            this._footerCellCreatedBy.Name = "_footerCellCreatedBy";
            this._footerCellCreatedBy.StylePriority.UseBorders = false;
            this._footerCellCreatedBy.StylePriority.UseTextAlignment = false;
            this._footerCellCreatedBy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this._footerCellCreatedBy.Weight = 0.24838323205770627D;
            // 
            // _lblCreatedByName
            // 
            this._lblCreatedByName.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this._lblCreatedByName.LocationFloat = new DevExpress.Utils.PointFloat(40F, 95.00002F);
            this._lblCreatedByName.Name = "_lblCreatedByName";
            this._lblCreatedByName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this._lblCreatedByName.SizeF = new System.Drawing.SizeF(490F, 25F);
            this._lblCreatedByName.StylePriority.UseFont = false;
            this._lblCreatedByName.StylePriority.UseTextAlignment = false;
            this._lblCreatedByName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // _lblCreatedByNote
            // 
            this._lblCreatedByNote.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Italic);
            this._lblCreatedByNote.LocationFloat = new DevExpress.Utils.PointFloat(40F, 25F);
            this._lblCreatedByNote.Name = "_lblCreatedByNote";
            this._lblCreatedByNote.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this._lblCreatedByNote.SizeF = new System.Drawing.SizeF(490F, 20F);
            this._lblCreatedByNote.StylePriority.UseFont = false;
            this._lblCreatedByNote.StylePriority.UseTextAlignment = false;
            this._lblCreatedByNote.Text = "(Ký, ghi rõ họ và tên)";
            this._lblCreatedByNote.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // _lblCreatedByTitle
            // 
            this._lblCreatedByTitle.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this._lblCreatedByTitle.LocationFloat = new DevExpress.Utils.PointFloat(40F, 0F);
            this._lblCreatedByTitle.Name = "_lblCreatedByTitle";
            this._lblCreatedByTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this._lblCreatedByTitle.SizeF = new System.Drawing.SizeF(490F, 25F);
            this._lblCreatedByTitle.StylePriority.UseFont = false;
            this._lblCreatedByTitle.StylePriority.UseTextAlignment = false;
            this._lblCreatedByTitle.Text = "NGƯỜI LẬP";
            this._lblCreatedByTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // _footerCellReviewedBy
            // 
            this._footerCellReviewedBy.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this._footerCellReviewedBy.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this._lblReviewedByName,
            this._lblReviewedByNote,
            this._lblReviewedByTitle});
            this._footerCellReviewedBy.Name = "_footerCellReviewedBy";
            this._footerCellReviewedBy.StylePriority.UseBorders = false;
            this._footerCellReviewedBy.StylePriority.UseTextAlignment = false;
            this._footerCellReviewedBy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this._footerCellReviewedBy.Weight = 0.24838324599745343D;
            // 
            // _lblReviewedByName
            // 
            this._lblReviewedByName.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this._lblReviewedByName.LocationFloat = new DevExpress.Utils.PointFloat(10F, 95.00002F);
            this._lblReviewedByName.Name = "_lblReviewedByName";
            this._lblReviewedByName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this._lblReviewedByName.SizeF = new System.Drawing.SizeF(520.0002F, 25F);
            this._lblReviewedByName.StylePriority.UseFont = false;
            this._lblReviewedByName.StylePriority.UseTextAlignment = false;
            this._lblReviewedByName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // _lblReviewedByNote
            // 
            this._lblReviewedByNote.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Italic);
            this._lblReviewedByNote.LocationFloat = new DevExpress.Utils.PointFloat(9.999939F, 25F);
            this._lblReviewedByNote.Name = "_lblReviewedByNote";
            this._lblReviewedByNote.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this._lblReviewedByNote.SizeF = new System.Drawing.SizeF(520.0001F, 20F);
            this._lblReviewedByNote.StylePriority.UseFont = false;
            this._lblReviewedByNote.StylePriority.UseTextAlignment = false;
            this._lblReviewedByNote.Text = "(Chức vụ, ký, ghi rõ họ và tên)";
            this._lblReviewedByNote.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // _lblReviewedByTitle
            // 
            this._lblReviewedByTitle.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this._lblReviewedByTitle.LocationFloat = new DevExpress.Utils.PointFloat(9.999939F, 0F);
            this._lblReviewedByTitle.Name = "_lblReviewedByTitle";
            this._lblReviewedByTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this._lblReviewedByTitle.SizeF = new System.Drawing.SizeF(520.0001F, 25F);
            this._lblReviewedByTitle.StylePriority.UseFont = false;
            this._lblReviewedByTitle.StylePriority.UseTextAlignment = false;
            this._lblReviewedByTitle.Text = "NGƯỜI DUYỆT";
            this._lblReviewedByTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // _footerCellSignedBy
            // 
            this._footerCellSignedBy.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this._footerCellSignedBy.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this._lblSignedByName,
            this._lblSignedByNote,
            this._lblSignedByTitle});
            this._footerCellSignedBy.Name = "_footerCellSignedBy";
            this._footerCellSignedBy.StylePriority.UseBorders = false;
            this._footerCellSignedBy.StylePriority.UseTextAlignment = false;
            this._footerCellSignedBy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this._footerCellSignedBy.Weight = 0.24838324101416937D;
            // 
            // _lblSignedByName
            // 
            this._lblSignedByName.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this._lblSignedByName.LocationFloat = new DevExpress.Utils.PointFloat(9.999878F, 95.00002F);
            this._lblSignedByName.Name = "_lblSignedByName";
            this._lblSignedByName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this._lblSignedByName.SizeF = new System.Drawing.SizeF(520.0002F, 25F);
            this._lblSignedByName.StylePriority.UseFont = false;
            this._lblSignedByName.StylePriority.UseTextAlignment = false;
            this._lblSignedByName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // _lblSignedByNote
            // 
            this._lblSignedByNote.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Italic);
            this._lblSignedByNote.LocationFloat = new DevExpress.Utils.PointFloat(9.999878F, 25F);
            this._lblSignedByNote.Name = "_lblSignedByNote";
            this._lblSignedByNote.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this._lblSignedByNote.SizeF = new System.Drawing.SizeF(520.0002F, 20F);
            this._lblSignedByNote.StylePriority.UseFont = false;
            this._lblSignedByNote.StylePriority.UseTextAlignment = false;
            this._lblSignedByNote.Text = "(Chức vụ, ký, ghi rõ họ và tên)";
            this._lblSignedByNote.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // _lblSignedByTitle
            // 
            this._lblSignedByTitle.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this._lblSignedByTitle.LocationFloat = new DevExpress.Utils.PointFloat(10F, 0F);
            this._lblSignedByTitle.Name = "_lblSignedByTitle";
            this._lblSignedByTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this._lblSignedByTitle.SizeF = new System.Drawing.SizeF(520.0001F, 25F);
            this._lblSignedByTitle.StylePriority.UseFont = false;
            this._lblSignedByTitle.StylePriority.UseTextAlignment = false;
            this._lblSignedByTitle.Text = "THỦ TRƯỞNG CƠ QUAN, ĐƠN VỊ";
            this._lblSignedByTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // _lblReportDate
            // 
            this._lblReportDate.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Italic);
            this._lblReportDate.LocationFloat = new DevExpress.Utils.PointFloat(1090F, 32.08332F);
            this._lblReportDate.Name = "_lblReportDate";
            this._lblReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this._lblReportDate.SizeF = new System.Drawing.SizeF(520.0001F, 20F);
            this._lblReportDate.StylePriority.UseFont = false;
            this._lblReportDate.StylePriority.UseTextAlignment = false;
            this._lblReportDate.Text = "Ngày {0} tháng {1} năm {2}";
            this._lblReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // _pageHeader
            // 
            this._pageHeader.HeightF = 0F;
            this._pageHeader.Name = "_pageHeader";
            // 
            // _groupHeader3
            // 
            this._groupHeader3.HeightF = 0F;
            this._groupHeader3.Name = "_groupHeader3";
            // 
            // _detail
            // 
            this._detail.Expanded = false;
            this._detail.HeightF = 0F;
            this._detail.Name = "_detail";
            // 
            // _groupFooter
            // 
            this._groupFooter.Expanded = false;
            this._groupFooter.HeightF = 0F;
            this._groupFooter.Name = "_groupFooter";
            // 
            // _groupHeader2
            // 
            this._groupHeader2.Expanded = false;
            this._groupHeader2.HeightF = 0F;
            this._groupHeader2.Level = 1;
            this._groupHeader2.Name = "_groupHeader2";
            // 
            // _groupHeader1
            // 
            this._groupHeader1.Expanded = false;
            this._groupHeader1.HeightF = 0F;
            this._groupHeader1.Level = 2;
            this._groupHeader1.Name = "_groupHeader1";
            // 
            // rpHRM_Payroll_A2_Potrait
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this._topMargin,
            this._header,
            this._pageHeader,
            this._groupHeader3,
            this._detail,
            this._footer,
            this._bottomMargin,
            this._groupFooter,
            this._groupHeader2,
            this._groupHeader1});
            this.BorderColor = System.Drawing.Color.DarkGray;
            this.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.Margins = new System.Drawing.Printing.Margins(12, 12, 10, 10);
            this.PageHeight = 2339;
            this.PageWidth = 1654;
            this.PaperKind = System.Drawing.Printing.PaperKind.A2;
            this.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this._footerTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        #region Initial

        /// <summary>
        /// Report
        /// </summary>
        private readonly ReportDynamicModel _reportDynamic;

        /// <summary>
        /// Report config
        /// </summary>
        private readonly List<ReportColumnModel> _config;

        /// <summary>
        /// Payroll ID
        /// </summary>
        private readonly int _payrollId;

        /// <summary>
        /// Filter data
        /// </summary>
        private Filter _filter;

        /// <summary>
        /// Init filter
        /// </summary>
        private void InitFilter()
        {
            // init filter
            _filter = new Filter
            {
                Items = new List<FilterItem>()
            };
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public rpHRM_Payroll_A2_Potrait(int reportId, int payrollId)
        {
            // init component
            InitializeComponent();

            // init Filter
            InitFilter();

            // init payroll id
            _payrollId = payrollId;

            // init report
            _reportDynamic = ReportDynamicController.GetById(reportId);

            // init report configuration
            _config = ReportColumnController.GetAll(null, reportId, null, null, null, ReportColumnStatus.Active, false, null, null);
        }

        #endregion

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
        /// Render data
        /// </summary>
        public void BindData()
        {
            try
            {
                // bind report props
                BindReport();

                // bind data source
                BindDataSource();

                // generate Header
                BindPageHeader();

                // bind Detail
                BindDetail();

                // bind group Header
                BindGroupHeader();

                // bind group Footer
                BindGroupFooter();

                // bind footer
                BindFooter();
            }
            catch(Exception ex)
            {
                Dialog.ShowError(ex);
            }

        }

        #region Private Methods

        /// <summary>
        /// Init count number
        /// </summary>
        private int _index = 1;

        /// <summary>
        /// Init group count number
        /// </summary>
        private int _gCount;

        /// <summary>
        /// Init footer count number
        /// </summary>
        private int _fCount;

        /// <summary>
        /// Format detail before print
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="isGroupHeader"></param>
        /// <param name="isDetailIndex"></param>
        /// <param name="dataType"></param>
        /// <param name="format"></param>
        private void Cell_BeforePrint(object sender, PrintEventArgs e, bool isGroupHeader, bool isDetailIndex, ReportColumnDataType? dataType, string format)
        {
            if (isGroupHeader)
            {
                _index = 1;
            }
            else if (isDetailIndex)
            {
                ((XRTableCell)sender).Text = _index.ToString();
                _index++;
            }
            else
            {
                if(dataType != null && dataType.Value == ReportColumnDataType.Number && !string.IsNullOrEmpty(format))
                {
                    var culture = CultureInfo.CreateSpecificCulture("en-US");
                    ((XRTableCell)sender).Text = decimal.TryParse(((XRTableCell)sender).Text, NumberStyles.AllowDecimalPoint, culture,out var value) ? value.ToString(format) : @"0";
                }
            }
        }

        /// <summary>
        /// Reset custom count in summary for each group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupFooter_SummaryReset(object sender, EventArgs e)
        {
            _gCount = 0;
        }

        /// <summary>
        /// Count by condition in each row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        private void GroupFooter_SummaryRowChanged(object sender, EventArgs e, string fieldName, string value)
        {
            var currentValue = GetCurrentColumnValue(fieldName);
            if(currentValue != null && string.Compare(currentValue.ToString(), value, StringComparison.OrdinalIgnoreCase) == 0)
                _gCount++;
        }

        /// <summary>
        /// Set result for each group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupFooter_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = _gCount;
            e.Handled = true;
        }

        /// <summary>
        /// Reset custom count in footer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Footer_SummaryReset(object sender, EventArgs e)
        {
            _fCount = 0;
        }

        /// <summary>
        /// Count by condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        private void Footer_SummaryRowChanged(object sender, EventArgs e, string fieldName, string value)
        {
            var currentValue = GetCurrentColumnValue(fieldName);
            if(currentValue != null && string.Compare(currentValue.ToString(), value, StringComparison.OrdinalIgnoreCase) == 0)
                _fCount++;
        }

        /// <summary>
        /// Set result for footer summary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Footer_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = _fCount;
            e.Handled = true;
        }

        /// <summary>
        /// Init report props
        /// </summary>
        private void InitReport()
        {
            // report title
            if(!string.IsNullOrEmpty(_filter.ReportTitle))
                _reportDynamic.Title = _filter.ReportTitle;

            // report date
            _reportDynamic.ReportDate = _filter.ReportDate;

            // start date
            if(_filter.StartDate != null)
                _reportDynamic.StartDate = _filter.StartDate.Value;

            // end date
            if(_filter.EndDate != null)
                _reportDynamic.EndDate = _filter.EndDate.Value;

            // duration
            if(!string.IsNullOrEmpty(_reportDynamic.Duration))
                _reportDynamic.Duration =
                    _reportDynamic.Duration.FormatWith(_reportDynamic.StartDate.ToString("dd/MM/yyyy"),
                        _reportDynamic.EndDate.ToString("dd/MM/yyyy"));

            // created by title
            if(!string.IsNullOrEmpty(_filter.CreatedByTitle))
                _reportDynamic.CreatedByTitle = _filter.CreatedByTitle;

            // created by note
            if(!string.IsNullOrEmpty(_filter.CreatedByNote))
                _reportDynamic.CreatedByNote = _filter.CreatedByNote;

            // created by name
            if(!string.IsNullOrEmpty(_filter.CreatedByName))
                _reportDynamic.CreatedByName = _filter.CreatedByName;

            // reviewed by title
            if(!string.IsNullOrEmpty(_filter.ReviewedByTitle))
                _reportDynamic.ReviewedByTitle = _filter.ReviewedByTitle;

            // reviewed by note
            if(!string.IsNullOrEmpty(_filter.ReviewedByNote))
                _reportDynamic.ReviewedByNote = _filter.ReviewedByNote;

            // reviewed by name
            if(!string.IsNullOrEmpty(_filter.ReviewedByName))
                _reportDynamic.ReviewedByName = _filter.ReviewedByName;

            // signed by title
            if(!string.IsNullOrEmpty(_filter.SignedByTitle))
                _reportDynamic.SignedByTitle = _filter.SignedByTitle;

            // signed by note
            if(!string.IsNullOrEmpty(_filter.SignedByNote))
                _reportDynamic.SignedByNote = _filter.SignedByNote;

            // signed by name
            if(!string.IsNullOrEmpty(_filter.SignedByName))
                _reportDynamic.SignedByName = _filter.SignedByName;
        }

        /// <summary>
        /// Bind report properties
        /// </summary>
        private void BindReport()
        {
            // init report props
            InitReport();

            // show/hide report footers
            _footerCellCreatedBy.Visible = !string.IsNullOrEmpty(_reportDynamic.CreatedByName);
            _footerCellReviewedBy.Visible = !string.IsNullOrEmpty(_reportDynamic.ReviewedByName);
            _footerCellSignedBy.Visible = !string.IsNullOrEmpty(_reportDynamic.SignedByName);
            _lblReportDate.Visible = _footerCellCreatedBy.Visible || _footerCellReviewedBy.Visible ||
                                     _footerCellSignedBy.Visible;
            // bind report props
            _lblParentDepartment.Text = _reportDynamic.ParentDepartment;
            _lblDepartment.Text = _reportDynamic.Department;
            _lblReportTitle.Text = _reportDynamic.Title;
            _lblDuration.Text = _reportDynamic.Duration;
            _lblReportDate.Text = _lblReportDate.Text.FormatWith(_reportDynamic.ReportDate.Day,
                _reportDynamic.ReportDate.Month, _reportDynamic.ReportDate.Year);
            _lblCreatedByTitle.Text = _reportDynamic.CreatedByTitle;
            _lblCreatedByNote.Text = _reportDynamic.CreatedByNote;
            _lblCreatedByName.Text = _reportDynamic.CreatedByName;
            _lblReviewedByTitle.Text = _reportDynamic.ReviewedByTitle;
            _lblReviewedByNote.Text = _reportDynamic.ReviewedByNote;
            _lblReviewedByName.Text = _reportDynamic.ReviewedByName;
            _lblSignedByTitle.Text = _reportDynamic.SignedByTitle;
            _lblSignedByNote.Text = _reportDynamic.SignedByNote;
            _lblSignedByName.Text = _reportDynamic.SignedByName;
        }

        /// <summary>
        /// Bind data source
        /// </summary>
        private void BindDataSource()
        {
            // get data salary
            var payroll = PayrollController.GetById(_payrollId);

            // check result
            if(payroll != null)
            {
                //// get salary board config
                //var payrollConfig = SalaryBoardConfigController.GetAll(payroll.ConfigId, null, null, null, null, null, null);

                //// check result
                //if(payrollConfig != null)
                //{
                //    // init array of data column
                //    var columns = new List<string>();

                //    // add fixed column
                //    columns.AddRange(new[] { "FullName", "EmployeeCode", "PositionName" });

                //    // add dynamic column
                //    columns.AddRange(payrollConfig.OrderBy(s => s.ColumnCode).Select(s => s.ColumnCode));

                //    // init data source
                //    var data = new DataTable();

                //    // add columns into data source
                //    foreach(var col in columns)
                //    {
                //        data.Columns.Add(col);
                //    }

                //    // parse salary data to json
                //    var jsonData = JSON.Deserialize<List<List<PayrollItemModel>>>(payroll.Data);

                //    jsonData.RemoveAt(0);

                //    // insert data into data source
                //    foreach(var dataRow in jsonData)
                //    {
                //        // init row
                //        var row = new object[columns.Count];
                //        // loop by column index
                //        for(var j = 0; j < columns.Count; j++)
                //        {
                //            // find cell
                //            var cell = dataRow.Find(r => r.Prop == columns[j]);

                //            // set value for data cell
                //            if(cell != null && int.TryParse(cell.Value, out var intValue))
                //                row[j] = intValue;
                //            else if(cell != null && float.TryParse(cell.Value.Replace(".", ","), out var floatValue))
                //                row[j] = floatValue;
                //            else
                //                row[j] = cell != null ? cell.Value : string.Empty;
                //        }
                //        // add row into data source
                //        data.Rows.Add(row);
                //    }

                //    // init data source
                //    DataSource = data;
                //}

                DataSource = PayrollController.GetPayrollDetail(null, _payrollId, null, null);
            }
        }

        /// <summary>
        /// Bind page header
        /// </summary>
        private void BindPageHeader()
        {
            // get config header
            var config = _config.Where(c => c.Type == ReportColumnType.Header).OrderBy(c => c.Order).ToList();

            // check config count
            if(config.Count > 0)
            {
                // init header table 
                var headerTable = ReportHelper.CreateTable("headerTable", _reportDynamic.Width, 0);

                // insert into page header
                _pageHeader.Controls.Add(headerTable);

                // init header row
                var headerRow = ReportHelper.CreateTableRow();

                // insert into header table
                headerTable.Rows.Add(headerRow);

                // get root config
                var rootConfig = config.Where(c => c.ParentId == 0).OrderBy(c => c.Order).ToList();

                // init array of header cells
                var headerCells = new XRTableCell[rootConfig.Count + 1];

                // init header cell index
                var headerCellIndex = ReportHelper.CreateTableCell(null, "STT", ReportHelper.DefaultFontSize, FontStyle.Bold,
                    BorderSide.Top | BorderSide.Right | BorderSide.Bottom | BorderSide.Left,
                    ReportTextAlign.MiddleCenter, null, null, ReportHelper.DefaultCellIndexWidth, 0);

                // set cell index as the first cell in each row
                headerCells[0] = headerCellIndex;

                // other cells
                for(var i = 0; i < rootConfig.Count; i++)
                {
                    // set item value
                    var configItem = rootConfig[i];

                    // is normal column
                    if(!configItem.IsGroup)
                    {
                        // init header cell
                        var headerCell = ReportHelper.CreateTableCell("xrCell{0}".FormatWith(configItem.Id), configItem.Name, configItem.FontSize,
                            FontStyle.Bold, BorderSide.Top | BorderSide.Right | BorderSide.Bottom, configItem.TextAlign, null, null, configItem.Width, 0);

                        // insert into array
                        headerCells[i + 1] = headerCell;
                    }
                    // is group column
                    else
                    {
                        // init cell
                        var headerCell = ReportHelper.CreateTableCell("xrCell{0}".FormatWith(configItem.Id), null, 0, FontStyle.Bold,
                            BorderSide.None, configItem.TextAlign, null, null, configItem.Width, 0);

                        // init label group level 1
                        var labelGroup = ReportHelper.CreateLabel("xrLabel{0}".FormatWith(configItem.Id), configItem.Name, configItem.FontSize, FontStyle.Bold,
                            BorderSide.Top | BorderSide.Right | BorderSide.Bottom, configItem.TextAlign, 0, 0, configItem.Width, configItem.Height);

                        // insert into cell
                        headerCell.Controls.Add(labelGroup);

                        // init current label pos for group item level 1
                        var lblPosX = 0;
                        var lblPosY = configItem.Height;

                        var listGroupItems = config.Where(c => c.ParentId == configItem.Id).OrderBy(c => c.Order);

                        foreach (var groupItemL1 in listGroupItems)
                        {
                            // normal label
                            if(!groupItemL1.IsGroup)
                            {
                                // init label
                                var labelItem = ReportHelper.CreateLabel("xrLabel{0}".FormatWith(groupItemL1.Id), groupItemL1.Name, groupItemL1.FontSize,
                                    FontStyle.Bold, BorderSide.Right | BorderSide.Bottom, groupItemL1.TextAlign, lblPosX, lblPosY, groupItemL1.Width,
                                    groupItemL1.Height > 0 ? groupItemL1.Height : ReportHelper.DefaultCellHeight);

                                // insert into cell
                                headerCell.Controls.Add(labelItem);

                                // increase position X
                                lblPosX += groupItemL1.Width;
                            }
                            // group level 2
                            else
                            {
                                // init label group level 2
                                var labelGroup2 = ReportHelper.CreateLabel("xrLabel{0}".FormatWith(groupItemL1.Id), groupItemL1.Name, groupItemL1.FontSize,
                                    FontStyle.Bold, BorderSide.Right | BorderSide.Bottom, groupItemL1.TextAlign, lblPosX, lblPosY, groupItemL1.Width,
                                    groupItemL1.Height > 0 ? groupItemL1.Height : ReportHelper.DefaultCellHeight);

                                // insert into cell
                                headerCell.Controls.Add(labelGroup2);

                                // init label pos y for group item level 2
                                var lblPosY2 = configItem.Height + groupItemL1.Height;

                                foreach(var groupItemL2 in config.Where(c => c.ParentId == groupItemL1.Id).OrderBy(c => c.Order))
                                {
                                    // init label
                                    var labelItem = ReportHelper.CreateLabel("xrLabel{0}".FormatWith(groupItemL2.Id), groupItemL2.Name, groupItemL2.FontSize,
                                        FontStyle.Bold, BorderSide.Right | BorderSide.Bottom, groupItemL2.TextAlign, lblPosX, lblPosY2, groupItemL2.Width,
                                        groupItemL2.Height > 0 ? groupItemL2.Height : ReportHelper.DefaultCellHeight);

                                    // insert into cell
                                    headerCell.Controls.Add(labelItem);

                                    // increase position X
                                    lblPosX += groupItemL2.Width;
                                }
                            }
                        }

                        // insert into array
                        headerCells[i + 1] = headerCell;
                    }
                }

                // insert Header cell into table
                headerRow.Cells.AddRange(headerCells);
            }
        }

        /// <summary>
        /// Bind group header
        /// </summary>
        private void BindGroupHeader()
        {
            // init array of report group header
            var reportGroupHeaders = new[] { _reportDynamic.GroupHeader1, _reportDynamic.GroupHeader2, _reportDynamic.GroupHeader3 };

            // init array of group header band
            var groupHeaders = new[] { _groupHeader1, _groupHeader2, _groupHeader3 };

            // loop init group header
            for(var i = 0; i < reportGroupHeaders.Length; i++)
            {
                if(reportGroupHeaders[i] != ReportGroupHeader.NoGroup)
                {
                    // init & insert group Header table into group Header
                    var groupHeaderTable = ReportHelper.CreateTable("groupHeaderTable{0}".FormatWith(i + 1), _reportDynamic.Width, ReportHelper.DefaultCellHeight);

                    // insert table into group header
                    groupHeaders[i].Controls.Add(groupHeaderTable);

                    // init & insert group Header table row into group Header table
                    var groupHeaderRow = ReportHelper.CreateTableRow();

                    // insert row into group header table
                    groupHeaderTable.Rows.Add(groupHeaderRow);

                    // init group header cell 
                    var groupHeaderCell = ReportHelper.CreateTableCell(null, null, ReportHelper.DefaultFontSize,
                        FontStyle.Bold, BorderSide.Right | BorderSide.Bottom | BorderSide.Left,
                        ReportTextAlign.MiddleLeft, null, null, 0, 0);

                    // declare event before print, set start is one in each group
                    groupHeaderCell.BeforePrint += (sender, e) => Cell_BeforePrint(groupHeaderCell, e, true, false, null, null);

                    // insert cell into row
                    groupHeaderRow.Cells.Add(groupHeaderCell);

                    // bind group data
                    switch(reportGroupHeaders[i])
                    {
                        // group by department
                        case ReportGroupHeader.Department:
                            groupHeaders[i].GroupFields.AddRange(new[] { new GroupField("DepartmentId", XRColumnSortOrder.Ascending) });
                            groupHeaderCell.DataBindings.Add("Text", DataSource, "DepartmentName");
                            break;
                        // group by position
                        case ReportGroupHeader.Position:
                            groupHeaders[i].GroupFields.AddRange(new[] { new GroupField("PositionId", XRColumnSortOrder.Ascending) });
                            groupHeaderCell.DataBindings.Add("Text", DataSource, "PositionName");
                            break;
                        default:
                            return;
                    }
                }
            }
        }

        /// <summary>
        /// Bind group footer
        /// </summary>
        private void BindGroupFooter()
        {
            // get config group footer
            var config = _config.Where(c => c.Type == ReportColumnType.FooterGroup).OrderBy(c => c.Order).ToList();

            // check config count
            if(config.Count > 0)
            {
                // init group footer table 
                var groupFooterTable = ReportHelper.CreateTable("groupFooterTable", _reportDynamic.Width, ReportHelper.DefaultCellHeight);

                // into group footer band
                _groupFooter.Controls.Add(groupFooterTable);

                // init group footer row 
                var groupFooterRow = ReportHelper.CreateTableRow();

                // insert into group footer table
                groupFooterTable.Rows.Add(groupFooterRow);

                // init array of footer cells
                var groupFooterCells = new XRTableCell[config.Count];

                // create cells
                for(var i = 0; i < config.Count; i++)
                {
                    // init group footer cell
                    var groupFooterCell = ReportHelper.CreateTableCell("groupFooterCell{0}".FormatWith(config[i].Id),
                        config[i].Name, config[i].FontSize, FontStyle.Bold,
                        i == 0
                            ? BorderSide.Right | BorderSide.Bottom | BorderSide.Left
                            : BorderSide.Right | BorderSide.Bottom,
                        config[i].TextAlign, null, null, config[i].Width, 0);
                    
                    // insert into array
                    groupFooterCells[i] = groupFooterCell;
                }

                // bind data
                for(var j = 0; j < config.Count; j++)
                {
                    if(!string.IsNullOrEmpty(config[j].FieldName))
                    {
                        // bind data
                        groupFooterCells[j].DataBindings.Add("Text", DataSource, config[j].FieldName);

                        // summary running
                        switch(config[j].SummaryRunning)
                        {
                            case ReportSummaryRunning.Group:
                                groupFooterCells[j].Summary.Running = SummaryRunning.Group;
                                break;
                            case ReportSummaryRunning.Page:
                                groupFooterCells[j].Summary.Running = SummaryRunning.Page;
                                break;
                            case ReportSummaryRunning.Report:
                                groupFooterCells[j].Summary.Running = SummaryRunning.Report;
                                break;
                            case ReportSummaryRunning.None:
                                groupFooterCells[j].Summary.Running = SummaryRunning.None;
                                break;
                        }

                        // summary function
                        switch(config[j].SummaryFunction)
                        {
                            case ReportSummaryFunction.Sum:
                                groupFooterCells[j].Summary.Func = SummaryFunc.Sum;
                                groupFooterCells[j].Summary.FormatString = config[j].Format;
                                break;
                            case ReportSummaryFunction.Count:
                                groupFooterCells[j].Summary.Func = SummaryFunc.Count;
                                groupFooterCells[j].Summary.FormatString = config[j].Format;
                                break;
                            case ReportSummaryFunction.CustomCount:
                                var fieldName = config[j].FieldName;
                                var summaryValue = config[j].SummaryValue;
                                groupFooterCells[j].Summary.Func = SummaryFunc.Custom;
                                groupFooterCells[j].Summary.FormatString = config[j].Format;
                                groupFooterCells[j].SummaryReset += GroupFooter_SummaryReset;
                                groupFooterCells[j].SummaryRowChanged += (sender, e) => GroupFooter_SummaryRowChanged(sender, e, fieldName, summaryValue);
                                groupFooterCells[j].SummaryGetResult += GroupFooter_SummaryGetResult;
                                break;
                            case ReportSummaryFunction.None:
                                break;
                        }
                    }
                    else
                    {
                        groupFooterCells[j].Text = config[j].Name;
                    }
                }

                // insert cells into row
                groupFooterRow.Cells.AddRange(groupFooterCells);
            }
        }

        /// <summary>
        /// Bind report footer
        /// </summary>
        private void BindFooter()
        {
            // get config footer
            var config = _config.Where(c => c.ParentId == 0 && c.Type == ReportColumnType.Footer).OrderBy(c => c.Order).ToList();

            // check config count
            if(config.Count > 0)
            {
                // init summary table
                var summaryTable = ReportHelper.CreateTable("summaryTable", _reportDynamic.Width, ReportHelper.DefaultCellHeight);

                // insert summary table into footer
                _footer.Controls.Add(summaryTable);

                // init summary table row
                var summaryRow = ReportHelper.CreateTableRow();

                // insert row into footer table
                summaryTable.Rows.Add(summaryRow);

                // init array of footer cells
                var summaryCells = new XRTableCell[config.Count];

                // create cells
                for(var i = 0; i < config.Count; i++)
                {
                    // init border
                    var border = i == 0 ? BorderSide.Right | BorderSide.Bottom | BorderSide.Left : BorderSide.Right | BorderSide.Bottom;

                    // init & insert group Header cell into group Header row
                    var summaryCell = ReportHelper.CreateTableCell("xrSummaryCell{0}".FormatWith(config[i].Id), null,
                        config[i].FontSize, FontStyle.Bold, border,
                        config[i].TextAlign, null, null, config[i].Width, 0);

                    // insert into array
                    summaryCells[i] = summaryCell;
                }

                // insert cells into row
                summaryRow.Cells.AddRange(summaryCells);

                // bind field into grid
                for(var j = 0; j < config.Count; j++)
                {
                    if(!string.IsNullOrEmpty(config[j].FieldName))
                    {
                        // bind data int cell
                        summaryCells[j].DataBindings.Add("Text", DataSource, config[j].FieldName);

                        // set summary report
                        summaryCells[j].Summary.Running = SummaryRunning.Report;

                        // check summary function
                        switch(config[j].SummaryFunction)
                        {
                            // sum
                            case ReportSummaryFunction.Sum:
                                summaryCells[j].Summary.Func = SummaryFunc.Sum;
                                summaryCells[j].Summary.FormatString = config[j].Format;
                                break;
                            // count
                            case ReportSummaryFunction.Count:
                                summaryCells[j].Summary.Func = SummaryFunc.Count;
                                summaryCells[j].Summary.FormatString = config[j].Format;
                                break;
                            // count by condition (ex: Data = 'X')
                            case ReportSummaryFunction.CustomCount:
                                var fieldName = config[j].FieldName;
                                var summaryValue = config[j].SummaryValue;
                                summaryCells[j].Summary.Func = SummaryFunc.Custom;
                                summaryCells[j].Summary.FormatString = config[j].Format;
                                summaryCells[j].SummaryReset += Footer_SummaryReset;
                                summaryCells[j].SummaryRowChanged += (sender, e) => Footer_SummaryRowChanged(sender, e, fieldName, summaryValue);
                                summaryCells[j].SummaryGetResult += Footer_SummaryGetResult;
                                break;
                        }
                    }
                    else
                    {
                        // no binding field, set static text
                        summaryCells[j].Text = config[j].Name;
                    }
                }
            }
        }

        /// <summary>
        /// Bind detail
        /// </summary>
        private void BindDetail()
        {
            // get config header
            var config = _config.Where(c => c.ParentId == 0 && c.Type == ReportColumnType.Detail).OrderBy(c => c.Order).ToList();

            // check config count
            if(config.Count > 0)
            {
                // init detail table 
                var detailTable = ReportHelper.CreateTable("detailTable", _reportDynamic.Width, 0);

                // insert into detail band
                _detail.Controls.Add(detailTable);

                // init detail row
                var detailRow = ReportHelper.CreateTableRow();

                // insert row into detail table
                detailTable.Rows.Add(detailRow);

                // init array of Detail cells
                var detailCells = new XRTableCell[config.Count + 1];

                // init detail cell index
                var detailCellIndex = ReportHelper.CreateTableCell(null, null, ReportHelper.DefaultFontSize, FontStyle.Regular,
                    BorderSide.Right | BorderSide.Bottom | BorderSide.Left,
                    ReportTextAlign.MiddleCenter, null, null, ReportHelper.DefaultCellIndexWidth, 0);

                // declare event before print
                detailCellIndex.BeforePrint += (sender, e) => Cell_BeforePrint(detailCellIndex, e, false, true, null, null);

                // insert into array of detail cells
                detailCells[0] = detailCellIndex;

                // other cells
                for(var i = 0; i < config.Count; i++)
                {
                    // init Detail cell
                    var detailCell = ReportHelper.CreateTableCell("xrDetailCell{0}".FormatWith(config[i].Id), null,
                        config[i].FontSize, FontStyle.Regular, BorderSide.Right | BorderSide.Bottom,
                        config[i].TextAlign, null, null, config[i].Width, 0);

                    // get data type & format
                    var dataType = config[i].DataType;
                    var format = config[i].Format;

                    // declare event before print
                    detailCell.BeforePrint += (sender, e) => Cell_BeforePrint(detailCell, e, false, false, dataType, format);

                    // insert into array
                    detailCells[i + 1] = detailCell;
                }

                // insert detail cell into table
                detailRow.Cells.AddRange(detailCells);

                // bind field into grid
                for(var j = 1; j < detailCells.Length; j++)
                {
                    var cellConfig = config.Find(c => "xrDetailCell{0}".FormatWith(c.Id) == detailCells[j].Name);
                    detailCells[j].DataBindings.Add("Text", DataSource, cellConfig.FieldName);
                }
            }
        }

        #endregion
    }


}