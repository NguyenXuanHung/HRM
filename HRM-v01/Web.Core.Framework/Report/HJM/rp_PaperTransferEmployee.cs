using DevExpress.XtraReports.UI;
using Web.Core.Framework.Adapter;
using Web.Core.Object.Report;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Summary description for rp_PaperTransferEmployee
    /// </summary>
    public class rp_PaperTransferEmployee : XtraReport
    {
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private ReportHeaderBand ReportHeader;
        private XRLabel xrLabel1;
        private XRLabel xrLabel2;
        private XRLabel lblFullName;
        private XRLabel xrLabel4;
        private XRLabel lblEmployeeCode;
        private XRLabel xrLabel7;
        private XRLabel lblReason;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell3;
        private XRTableRow xrTableRow2;
        private XRTableCell xrTableCellOldPosition;
        private XRTableCell xrTableCellNewPosition;
        private XRTableRow xrTableRow3;
        private XRTableCell xrTableCellOldSalary;
        private XRTableCell xrTableCellNewSalary;
        private XRTableRow xrTableRow4;
        private XRTableCell xrTableCellOldEffectiveDate;
        private XRTableCell xrTableCellNewEffectiveDate;
        private XRTableCell xrTableCell9;
        private XRTableCell xrTableCell10;
        private XRTableCell xrTableCell11;
        private XRTableCell xrTableCell12;
        private XRTableCell xrTableCell13;
        private XRTableCell xrTableCell14;
        private ReportFooterBand ReportFooter;
        private XRLabel xrLabel12;
        private XRLabel xrLabel13;
        private XRLabel xrLabel14;
        private XRLabel lblOldDate;
        private XRLabel lblNewDate;
        private XRLabel xrLabel19;
        private XRLabel xrLabel20;
        private XRLabel lblOldDateFooter;
        private XRLabel lblNewDateFooter;
        private XRLabel xrLabel23;
        private XRLabel xrLabel24;
        private XRLabel xrl_TenCongTy;
        private XRPictureBox xrLogo;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public rp_PaperTransferEmployee()
        {
            InitializeComponent();
        }

        public void BindData(ReportFilter filter)
        {
            const string type = "ThuyenChuyenDieuChuyen";
            var recordId = filter.RecordId;
            var fromDate = filter.StartDate != null ? filter.StartDate.Value.ToString("yyyy-dd-MM") : string.Empty;
            var endDate = filter.EndDate != null ? filter.EndDate.Value.ToString("yyyy-dd-MM") : string.Empty;

            var toDate = filter.ReportedDate;
            lblOldDate.Text = string.Format(lblOldDate.Text, toDate.Day, toDate.Month, toDate.Year);
            lblNewDate.Text = string.Format(lblOldDate.Text, toDate.Day, toDate.Month, toDate.Year);
            lblOldDateFooter.Text = string.Format(lblOldDate.Text, toDate.Day, toDate.Month, toDate.Year);
            lblNewDateFooter.Text = string.Format(lblOldDate.Text, toDate.Day, toDate.Month, toDate.Year);

            // get organization
            var people = hr_RecordServices.GetById(recordId);
            if (people == null) return;

            lblFullName.Text = people.FullName;
            lblEmployeeCode.Text = people.EmployeeCode;

            var table = SQLHelper.ExecuteTable(
                SQLManagementAdapter.GetStore_PaperRequestTransfer(recordId, type, fromDate, endDate));
            DataSource = table;

            xrTableCellOldPosition.DataBindings.Add("Text", DataSource, "OldPosition");
            xrTableCellOldSalary.DataBindings.Add("Text", DataSource, "OldSalary");
            xrTableCellOldEffectiveDate.DataBindings.Add("Text", DataSource, "OldEffectiveDate", "{0:dd/MM/yyyy}");
            xrTableCellNewPosition.DataBindings.Add("Text", DataSource, "NewPosition");
            xrTableCellNewSalary.DataBindings.Add("Text", DataSource, "NewSalary");
            xrTableCellNewEffectiveDate.DataBindings.Add("Text", DataSource, "NewEffectiveDate", "{0:dd/MM/yyyy}");

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
            string resourceFileName = "rp_PaperTransferEmployee.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOldPosition = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNewPosition = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOldSalary = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNewSalary = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOldEffectiveDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNewEffectiveDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblReason = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblEmployeeCode = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblFullName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrl_TenCongTy = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblNewDateFooter = new DevExpress.XtraReports.UI.XRLabel();
            this.lblOldDateFooter = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblNewDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblOldDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize) (this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrTable1,
                this.lblReason,
                this.xrLabel7,
                this.lblEmployeeCode,
                this.xrLabel4,
                this.lblFullName,
                this.xrLabel2
            });
            this.Detail.HeightF = 485F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders =
                ((DevExpress.XtraPrinting.BorderSide) ((((DevExpress.XtraPrinting.BorderSide.Left |
                                                          DevExpress.XtraPrinting.BorderSide.Top)
                                                         | DevExpress.XtraPrinting.BorderSide.Right)
                                                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 102.0001F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[]
            {
                this.xrTableRow1,
                this.xrTableRow2,
                this.xrTableRow3,
                this.xrTableRow4
            });
            this.xrTable1.SizeF = new System.Drawing.SizeF(709F, 373F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[]
            {
                this.xrTableCell1,
                this.xrTableCell3
            });
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "Bộ phận chuyển đi";
            this.xrTableCell1.Weight = 1.5641750018250287D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "Bộ phận chuyển đến";
            this.xrTableCell3.Weight = 1.4358249981749713D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[]
            {
                this.xrTableCell9,
                this.xrTableCellOldPosition,
                this.xrTableCell10,
                this.xrTableCellNewPosition
            });
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 2.7020419059622527D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrTableCell9.Multiline = true;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "\r\nVị trí";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell9.Weight = 0.35895631410843565D;
            // 
            // xrTableCellOldPosition
            // 
            this.xrTableCellOldPosition.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrTableCellOldPosition.Name = "xrTableCellOldPosition";
            this.xrTableCellOldPosition.StylePriority.UseFont = false;
            this.xrTableCellOldPosition.Weight = 1.2052186877165931D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrTableCell10.Multiline = true;
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.Text = "\r\nVị trí";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell10.Weight = 0.36971073661771919D;
            // 
            // xrTableCellNewPosition
            // 
            this.xrTableCellNewPosition.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrTableCellNewPosition.Name = "xrTableCellNewPosition";
            this.xrTableCellNewPosition.StylePriority.UseFont = false;
            this.xrTableCellNewPosition.Weight = 1.0661142615572523D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[]
            {
                this.xrTableCell11,
                this.xrTableCellOldSalary,
                this.xrTableCell13,
                this.xrTableCellNewSalary
            });
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 2.2571480759244751D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrTableCell11.Multiline = true;
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.Text = "Mức \r\nlương đang\r\n nhận";
            this.xrTableCell11.Weight = 0.35895628182608924D;
            // 
            // xrTableCellOldSalary
            // 
            this.xrTableCellOldSalary.Font =
                new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrTableCellOldSalary.Name = "xrTableCellOldSalary";
            this.xrTableCellOldSalary.StylePriority.UseFont = false;
            this.xrTableCellOldSalary.Weight = 1.2052187199989393D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrTableCell13.Multiline = true;
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.Text = "Mức \r\nlương đang\r\n nhận";
            this.xrTableCell13.Weight = 0.36971059134716044D;
            // 
            // xrTableCellNewSalary
            // 
            this.xrTableCellNewSalary.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrTableCellNewSalary.Name = "xrTableCellNewSalary";
            this.xrTableCellNewSalary.StylePriority.UseFont = false;
            this.xrTableCellNewSalary.Weight = 1.0661144068278108D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[]
            {
                this.xrTableCell12,
                this.xrTableCellOldEffectiveDate,
                this.xrTableCell14,
                this.xrTableCellNewEffectiveDate
            });
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1.3485774997825155D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.Text = "Ngày áp dụng";
            this.xrTableCell12.Weight = 0.35895629796726247D;
            // 
            // xrTableCellOldEffectiveDate
            // 
            this.xrTableCellOldEffectiveDate.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrTableCellOldEffectiveDate.Name = "xrTableCellOldEffectiveDate";
            this.xrTableCellOldEffectiveDate.StylePriority.UseFont = false;
            this.xrTableCellOldEffectiveDate.Weight = 1.2052187038577662D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.Text = "Ngày áp dụng";
            this.xrTableCell14.Weight = 0.36971084960593154D;
            // 
            // xrTableCellNewEffectiveDate
            // 
            this.xrTableCellNewEffectiveDate.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrTableCellNewEffectiveDate.Name = "xrTableCellNewEffectiveDate";
            this.xrTableCellNewEffectiveDate.StylePriority.UseFont = false;
            this.xrTableCellNewEffectiveDate.Weight = 1.0661141485690397D;
            // 
            // lblReason
            // 
            this.lblReason.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblReason.LocationFloat = new DevExpress.Utils.PointFloat(1.589457E-05F, 56.00004F);
            this.lblReason.Name = "lblReason";
            this.lblReason.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblReason.SizeF = new System.Drawing.SizeF(665.3749F, 46.00005F);
            this.lblReason.StylePriority.UseFont = false;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 32.99999F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(151.9583F, 23F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.Text = "Lý do thuyên chuyển:";
            // 
            // lblEmployeeCode
            // 
            this.lblEmployeeCode.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblEmployeeCode.LocationFloat = new DevExpress.Utils.PointFloat(471.75F, 10.00001F);
            this.lblEmployeeCode.Name = "lblEmployeeCode";
            this.lblEmployeeCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblEmployeeCode.SizeF = new System.Drawing.SizeF(193.625F, 23F);
            this.lblEmployeeCode.StylePriority.UseFont = false;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(392.5833F, 10.00001F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(79.16666F, 23F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.Text = "Mã số thẻ:";
            // 
            // lblFullName
            // 
            this.lblFullName.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblFullName.LocationFloat = new DevExpress.Utils.PointFloat(112.5F, 10.00001F);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblFullName.SizeF = new System.Drawing.SizeF(280.0833F, 23F);
            this.lblFullName.StylePriority.UseFont = false;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.00001F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(112.5F, 23F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "Tên nhân viên:";
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 51F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 50F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrl_TenCongTy,
                this.xrLogo,
                this.xrLabel1
            });
            this.ReportHeader.HeightF = 200F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrl_TenCongTy
            // 
            this.xrl_TenCongTy.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_TenCongTy.LocationFloat = new DevExpress.Utils.PointFloat(0F, 112.5F);
            this.xrl_TenCongTy.Name = "xrl_TenCongTy";
            this.xrl_TenCongTy.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_TenCongTy.SizeF = new System.Drawing.SizeF(709F, 23F);
            this.xrl_TenCongTy.StylePriority.UseFont = false;
            this.xrl_TenCongTy.StylePriority.UseTextAlignment = false;
            this.xrl_TenCongTy.Text = "CÔNG TY TNHH THƯƠNG MẠI VÀ XÂY DỰNG TRUNG CHÍNH";
            this.xrl_TenCongTy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLogo
            // 
            this.xrLogo.LocationFloat = new DevExpress.Utils.PointFloat(51.95831F, 0F);
            this.xrLogo.Name = "xrLogo";
            this.xrLogo.Padding = new DevExpress.XtraPrinting.PaddingInfo(1, 1, 1, 1, 100F);
            this.xrLogo.SizeF = new System.Drawing.SizeF(110F, 110F);
            this.xrLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrLogo.StylePriority.UsePadding = false;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(1.589457E-05F, 148.9583F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(709F, 28.20833F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "GIẤY ĐỀ NGHỊ THUYÊN CHUYỂN NHÂN SỰ";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrLabel24,
                this.xrLabel23,
                this.lblNewDateFooter,
                this.lblOldDateFooter,
                this.xrLabel20,
                this.xrLabel19,
                this.lblNewDate,
                this.lblOldDate,
                this.xrLabel14,
                this.xrLabel13,
                this.xrLabel12
            });
            this.ReportFooter.HeightF = 377F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrLabel24
            // 
            this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(423.25F, 306.25F);
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel24.SizeF = new System.Drawing.SizeF(242.1249F, 23F);
            this.xrLabel24.StylePriority.UseTextAlignment = false;
            this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel23
            // 
            this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(51.95831F, 306.25F);
            this.xrLabel23.Name = "xrLabel23";
            this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel23.SizeF = new System.Drawing.SizeF(244.7917F, 23F);
            this.xrLabel23.StylePriority.UseTextAlignment = false;
            this.xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblNewDateFooter
            // 
            this.lblNewDateFooter.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblNewDateFooter.LocationFloat = new DevExpress.Utils.PointFloat(423.2499F, 344.7917F);
            this.lblNewDateFooter.Name = "lblNewDateFooter";
            this.lblNewDateFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblNewDateFooter.SizeF = new System.Drawing.SizeF(247.9167F, 23F);
            this.lblNewDateFooter.StylePriority.UseFont = false;
            this.lblNewDateFooter.StylePriority.UseTextAlignment = false;
            this.lblNewDateFooter.Text = "Ngày {0} tháng {1} năm {2}";
            this.lblNewDateFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblOldDateFooter
            // 
            this.lblOldDateFooter.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblOldDateFooter.LocationFloat = new DevExpress.Utils.PointFloat(51.95831F, 344.7917F);
            this.lblOldDateFooter.Name = "lblOldDateFooter";
            this.lblOldDateFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblOldDateFooter.SizeF = new System.Drawing.SizeF(244.7917F, 23F);
            this.lblOldDateFooter.StylePriority.UseFont = false;
            this.lblOldDateFooter.StylePriority.UseTextAlignment = false;
            this.lblOldDateFooter.Text = "Ngày {0} tháng {1} năm {2}";
            this.lblOldDateFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel20
            // 
            this.xrLabel20.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(423.25F, 231.25F);
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(242.1249F, 23F);
            this.xrLabel20.StylePriority.UseFont = false;
            this.xrLabel20.StylePriority.UseTextAlignment = false;
            this.xrLabel20.Text = "Tổng giám đốc";
            this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel19
            // 
            this.xrLabel19.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(51.95831F, 231.25F);
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(242.125F, 23F);
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.StylePriority.UseTextAlignment = false;
            this.xrLabel19.Text = "Phòng HCNS";
            this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblNewDate
            // 
            this.lblNewDate.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblNewDate.LocationFloat = new DevExpress.Utils.PointFloat(423.25F, 197.9167F);
            this.lblNewDate.Name = "lblNewDate";
            this.lblNewDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblNewDate.SizeF = new System.Drawing.SizeF(242.1249F, 23F);
            this.lblNewDate.StylePriority.UseFont = false;
            this.lblNewDate.StylePriority.UseTextAlignment = false;
            this.lblNewDate.Text = "Ngày {0} tháng {1} năm {2}";
            this.lblNewDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblOldDate
            // 
            this.lblOldDate.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblOldDate.LocationFloat = new DevExpress.Utils.PointFloat(51.95831F, 197.9167F);
            this.lblOldDate.Name = "lblOldDate";
            this.lblOldDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblOldDate.SizeF = new System.Drawing.SizeF(242.125F, 23F);
            this.lblOldDate.StylePriority.UseFont = false;
            this.lblOldDate.StylePriority.UseTextAlignment = false;
            this.lblOldDate.Text = "Ngày {0} tháng {1} năm {2}";
            this.lblOldDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(423.25F, 64.58334F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(242.125F, 23F);
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.Text = "Bộ phận chuyển đến";
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(51.95831F, 64.58334F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(242.125F, 23F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "Bộ phận chuyển đi";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(151.9583F, 20.83333F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(362.5F, 23F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "Xác nhận của bộ trưởng bộ phận";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // rp_PaperTransferEmployee
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[]
            {
                this.Detail,
                this.TopMargin,
                this.BottomMargin,
                this.ReportHeader,
                this.ReportFooter
            });
            this.Margins = new System.Drawing.Printing.Margins(59, 59, 51, 50);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize) (this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this)).EndInit();

        }

        #endregion
    }
}
