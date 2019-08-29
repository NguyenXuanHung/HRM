using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Web.Core.Object.Report;

namespace CCVC.Web
{

    /// <summary>
    /// Summary description for rp_SalaryByMonth
    /// </summary>
    public class rp_SalaryByMonth : XtraReport
    {
        private DetailBand Detail;
        private TopMarginBand TopMargin;
        private BottomMarginBand BottomMargin;
        private ReportHeaderBand ReportHeader;
        private PageHeaderBand PageHeader;
        private FormattingRule formattingRule1;
        private ReportFooterBand ReportFooter;
        private XRLabel lblThuTruong;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRLabel lblReportDate;
        private XRTable tblPageHeader;
        private XRTableRow xrTableRow11;
        private XRTableCell xrTableCell241;
        private XRTableCell xrTableCell10;
        private XRTableCell xrTableCell242;
        private XRTableCell xrTableCell16;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTextTongSo;
        private XRTable tblDetail;
        private XRTableRow xrDetailRow1;
        private XRTableCell xrTableCellTotalWages;
        private XRTableCell xrTableCellTotalIncome;
        private XRTableCell xrTableCellInsurrance;
        private XRTableCell xrTableCellPersonalIncomeTax;
        private XRTableCell xrTableCellOtherIncome;
        private XRTableCell xrTableCellTheWorkCost;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrTableCell12;
        private XRTableRow xrTableRow3;
        private XRTableCell xrTableCell13;
        private XRTableCell xrTableCell14;
        private XRTableCell xrTableCellTotalWagesMonth;
        private XRTable tblReportHeader;
        private XRTableRow xrTableRow19;
        private XRTableCell xrCellTenBieuMau;
        private XRTableRow xrTableRow4;
        private XRTableCell xrTableCell11;
        private XRLabel xrLabel2;
        private XRLabel xrLabel1;
        private XRTable xrTable5;
        private XRTableRow xrTableRow7;
        private XRTableCell xrTableCell18;
        private XRTableCell xrTableCell19;
        private XRTable xrTable4;
        private XRTableRow xrTableRow6;
        private XRTableCell xrTableCell15;
        private XRTableCell xrTableCellTotal;
        private XRTable xrTable3;
        private XRTableRow xrTableRow5;
        private XRTableCell xrTableCell8;
        private XRTableCell xrTableCellText;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
        private IContainer components = null;

        public rp_SalaryByMonth()
        {
            InitializeComponent();
        }
        int _stt = 1;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCellTotalWages.Text = _stt.ToString();
            _stt++;
        }
        public void BindData(ReportFilter filter)
        {
            try
            {

            }
            catch
            {


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
            string resourceFileName = "rp_SalaryByMonth.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tblReportHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCellTenBieuMau = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.tblPageHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell241 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell242 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellText = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTextTongSo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalWagesMonth = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblThuTruong = new DevExpress.XtraReports.UI.XRLabel();
            this.tblDetail = new DevExpress.XtraReports.UI.XRTable();
            this.xrDetailRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellTotalWages = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalIncome = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellInsurrance = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellPersonalIncomeTax = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOtherIncome = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTheWorkCost = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblReportHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StylePriority.UseBorders = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
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
            this.xrTable2,
            this.tblReportHeader});
            this.ReportHeader.HeightF = 86F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 42.00001F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2,
            this.xrTableRow3});
            this.xrTable2.SizeF = new System.Drawing.SizeF(761.3389F, 34F);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell12});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 0.79999990231146945D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.Text = "BẢNG TỔNG HỢP  LƯƠNG THÁNG 9 / 2017";
            this.xrTableCell12.Weight = 3.9966622210905545D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell14});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 0.33333324536656561D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrTableCell13.StylePriority.UsePadding = false;
            this.xrTableCell13.Weight = 1.5748560433256003D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.Weight = 2.4218061963804294D;
            // 
            // tblReportHeader
            // 
            this.tblReportHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tblReportHeader.Name = "tblReportHeader";
            this.tblReportHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow19,
            this.xrTableRow4});
            this.tblReportHeader.SizeF = new System.Drawing.SizeF(761.3389F, 34F);
            // 
            // xrTableRow19
            // 
            this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCellTenBieuMau});
            this.xrTableRow19.Name = "xrTableRow19";
            this.xrTableRow19.Weight = 0.41805549924192159D;
            // 
            // xrCellTenBieuMau
            // 
            this.xrCellTenBieuMau.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrCellTenBieuMau.Name = "xrCellTenBieuMau";
            this.xrCellTenBieuMau.StylePriority.UseFont = false;
            this.xrCellTenBieuMau.StylePriority.UseTextAlignment = false;
            this.xrCellTenBieuMau.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCellTenBieuMau.Weight = 3.9966622210905545D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 0.71527764843611341D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell11.Weight = 3.99666223970603D;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblPageHeader});
            this.PageHeader.HeightF = 155.0595F;
            this.PageHeader.Name = "PageHeader";
            // 
            // tblPageHeader
            // 
            this.tblPageHeader.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tblPageHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 55.05952F);
            this.tblPageHeader.Name = "tblPageHeader";
            this.tblPageHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
            this.tblPageHeader.SizeF = new System.Drawing.SizeF(803.0001F, 100F);
            this.tblPageHeader.StylePriority.UseBorders = false;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell241,
            this.xrTableCell10,
            this.xrTableCell242,
            this.xrTableCell16,
            this.xrTableCell1,
            this.xrTableCell2});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 9.2D;
            // 
            // xrTableCell241
            // 
            this.xrTableCell241.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell241.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell241.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell241.Name = "xrTableCell241";
            this.xrTableCell241.StylePriority.UseBorderColor = false;
            this.xrTableCell241.StylePriority.UseBorders = false;
            this.xrTableCell241.StylePriority.UseFont = false;
            this.xrTableCell241.Text = "Tổng tiền lương";
            this.xrTableCell241.Weight = 0.47482605866316707D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell10.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseBorderColor = false;
            this.xrTableCell10.StylePriority.UseBorders = false;
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.Text = "Tổng thu nhập";
            this.xrTableCell10.Weight = 0.4635389334915101D;
            // 
            // xrTableCell242
            // 
            this.xrTableCell242.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell242.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell242.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell242.Name = "xrTableCell242";
            this.xrTableCell242.StylePriority.UseBorderColor = false;
            this.xrTableCell242.StylePriority.UseBorders = false;
            this.xrTableCell242.StylePriority.UseFont = false;
            this.xrTableCell242.Text = "BHXH,BHYT,BHTN";
            this.xrTableCell242.Weight = 0.67302614653114712D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell16.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell16.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseBorderColor = false;
            this.xrTableCell16.StylePriority.UseBorders = false;
            this.xrTableCell16.StylePriority.UseFont = false;
            this.xrTableCell16.Text = "Thuế thu nhập cá nhân";
            this.xrTableCell16.Weight = 0.72445911061355661D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorderColor = false;
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "Thu khác";
            this.xrTableCell1.Weight = 0.54480146016993436D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorderColor = false;
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Text = "Công tác phí";
            this.xrTableCell2.Weight = 0.54114028991361063D;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5,
            this.xrTable4,
            this.xrTable3,
            this.xrLabel2,
            this.xrLabel1,
            this.lblReportDate,
            this.xrTable1,
            this.lblThuTruong,
            this.tblDetail});
            this.ReportFooter.HeightF = 252.1667F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrTable5
            // 
            this.xrTable5.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 93.15472F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.xrTable5.SizeF = new System.Drawing.SizeF(378.149F, 25F);
            this.xrTable5.StylePriority.UseBorders = false;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell18,
            this.xrTableCell19});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 1D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseBorders = false;
            this.xrTableCell18.StylePriority.UseTextAlignment = false;
            this.xrTableCell18.Text = "Trong đó";
            this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell18.Weight = 0.54932342646152388D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.StylePriority.UseBorders = false;
            this.xrTableCell19.StylePriority.UseTextAlignment = false;
            this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell19.Weight = 1.3148848042021257D;
            // 
            // xrTable4
            // 
            this.xrTable4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 118.1547F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrTable4.SizeF = new System.Drawing.SizeF(378.149F, 25F);
            this.xrTable4.StylePriority.UseBorders = false;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell15,
            this.xrTableCellTotal});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseBorders = false;
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.Text = "TỔNG CỘNG";
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell15.Weight = 0.54932342646152388D;
            // 
            // xrTableCellTotal
            // 
            this.xrTableCellTotal.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCellTotal.Name = "xrTableCellTotal";
            this.xrTableCellTotal.StylePriority.UseBorders = false;
            this.xrTableCellTotal.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotal.Weight = 1.3148848042021257D;
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 68.15474F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrTable3.SizeF = new System.Drawing.SizeF(378.149F, 25F);
            this.xrTable3.StylePriority.UseBorders = false;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCellText});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "Bằng chữ";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell8.Weight = 0.54932342646152388D;
            // 
            // xrTableCellText
            // 
            this.xrTableCellText.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCellText.Name = "xrTableCellText";
            this.xrTableCellText.StylePriority.UseBorders = false;
            this.xrTableCellText.StylePriority.UseTextAlignment = false;
            this.xrTableCellText.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellText.Weight = 1.3148848042021257D;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(83.53977F, 194.7619F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(121.7877F, 25.00001F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "TỔNG GIÁM ĐỐC";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(322.6191F, 195.8333F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(121.7877F, 25.00001F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "PHÒNG KẾ TOÁN";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblReportDate
            // 
            this.lblReportDate.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Italic);
            this.lblReportDate.LocationFloat = new DevExpress.Utils.PointFloat(599.8099F, 165.625F);
            this.lblReportDate.Name = "lblReportDate";
            this.lblReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblReportDate.SizeF = new System.Drawing.SizeF(175.3329F, 15F);
            this.lblReportDate.StylePriority.UseFont = false;
            this.lblReportDate.StylePriority.UseTextAlignment = false;
            this.lblReportDate.Text = "{0}, ngày {1} tháng {2} năm {3}";
            this.lblReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 43.15476F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(378.149F, 25F);
            this.xrTable1.StylePriority.UseBorders = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTextTongSo,
            this.xrTableCellTotalWagesMonth});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTextTongSo
            // 
            this.xrTextTongSo.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTextTongSo.Name = "xrTextTongSo";
            this.xrTextTongSo.StylePriority.UseBorders = false;
            this.xrTextTongSo.StylePriority.UseTextAlignment = false;
            this.xrTextTongSo.Text = "TỔNG TIỀN LƯƠNG THÁNG 9/2017";
            this.xrTextTongSo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTextTongSo.Weight = 1.0855889282205971D;
            // 
            // xrTableCellTotalWagesMonth
            // 
            this.xrTableCellTotalWagesMonth.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCellTotalWagesMonth.Name = "xrTableCellTotalWagesMonth";
            this.xrTableCellTotalWagesMonth.StylePriority.UseBorders = false;
            this.xrTableCellTotalWagesMonth.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalWagesMonth.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalWagesMonth.Weight = 0.77861930244305255D;
            // 
            // lblThuTruong
            // 
            this.lblThuTruong.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.lblThuTruong.LocationFloat = new DevExpress.Utils.PointFloat(624.6703F, 194.7619F);
            this.lblThuTruong.Name = "lblThuTruong";
            this.lblThuTruong.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblThuTruong.SizeF = new System.Drawing.SizeF(121.7877F, 25.00001F);
            this.lblThuTruong.StylePriority.UseFont = false;
            this.lblThuTruong.StylePriority.UseTextAlignment = false;
            this.lblThuTruong.Text = "CHỦ TÀI KHOẢN";
            this.lblThuTruong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // tblDetail
            // 
            this.tblDetail.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tblDetail.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tblDetail.Name = "tblDetail";
            this.tblDetail.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrDetailRow1});
            this.tblDetail.SizeF = new System.Drawing.SizeF(803F, 25F);
            this.tblDetail.StylePriority.UseBorders = false;
            // 
            // xrDetailRow1
            // 
            this.xrDetailRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellTotalWages,
            this.xrTableCellTotalIncome,
            this.xrTableCellInsurrance,
            this.xrTableCellPersonalIncomeTax,
            this.xrTableCellOtherIncome,
            this.xrTableCellTheWorkCost});
            this.xrDetailRow1.Name = "xrDetailRow1";
            this.xrDetailRow1.Weight = 1D;
            // 
            // xrTableCellTotalWages
            // 
            this.xrTableCellTotalWages.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalWages.Name = "xrTableCellTotalWages";
            this.xrTableCellTotalWages.StylePriority.UseBorders = false;
            this.xrTableCellTotalWages.StylePriority.UseFont = false;
            this.xrTableCellTotalWages.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalWages.Text = " ";
            this.xrTableCellTotalWages.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalWages.Weight = 0.39584745643453212D;
            // 
            // xrTableCellTotalIncome
            // 
            this.xrTableCellTotalIncome.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalIncome.Name = "xrTableCellTotalIncome";
            this.xrTableCellTotalIncome.StylePriority.UseBorders = false;
            this.xrTableCellTotalIncome.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalIncome.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalIncome.Weight = 0.38643787207413272D;
            // 
            // xrTableCellInsurrance
            // 
            this.xrTableCellInsurrance.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellInsurrance.Name = "xrTableCellInsurrance";
            this.xrTableCellInsurrance.StylePriority.UseBorders = false;
            this.xrTableCellInsurrance.StylePriority.UseTextAlignment = false;
            this.xrTableCellInsurrance.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellInsurrance.Weight = 0.56108069673743777D;
            // 
            // xrTableCellPersonalIncomeTax
            // 
            this.xrTableCellPersonalIncomeTax.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellPersonalIncomeTax.Name = "xrTableCellPersonalIncomeTax";
            this.xrTableCellPersonalIncomeTax.StylePriority.UseBorders = false;
            this.xrTableCellPersonalIncomeTax.StylePriority.UseTextAlignment = false;
            this.xrTableCellPersonalIncomeTax.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellPersonalIncomeTax.Weight = 0.60395891750561881D;
            // 
            // xrTableCellOtherIncome
            // 
            this.xrTableCellOtherIncome.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOtherIncome.Name = "xrTableCellOtherIncome";
            this.xrTableCellOtherIncome.StylePriority.UseBorders = false;
            this.xrTableCellOtherIncome.StylePriority.UseTextAlignment = false;
            this.xrTableCellOtherIncome.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOtherIncome.Weight = 0.45418327938471686D;
            // 
            // xrTableCellTheWorkCost
            // 
            this.xrTableCellTheWorkCost.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTheWorkCost.Name = "xrTableCellTheWorkCost";
            this.xrTableCellTheWorkCost.StylePriority.UseBorders = false;
            this.xrTableCellTheWorkCost.StylePriority.UseTextAlignment = false;
            this.xrTableCellTheWorkCost.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTheWorkCost.Weight = 0.45113177779529157D;
            // 
            // rp_SalaryByMonth
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter});
            this.BorderColor = System.Drawing.Color.DarkGray;
            this.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Margins = new System.Drawing.Printing.Margins(12, 12, 15, 0);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblReportHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}