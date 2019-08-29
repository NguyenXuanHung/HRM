using System;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Web.Core;
using Web.Core.Service.Catalog;
using Web.Core.Object.Report;
using Web.Core.Framework;
using Web.Core.Framework.Adapter;

namespace Web.Core.Framework.Report
{

    /// <summary>
    /// Summary description for rp_SalaryCommuneCivilServants
    /// </summary>
    public class rp_SalaryCommuneCivilServants : XtraReport
{
    private DetailBand Detail;
    private TopMarginBand TopMargin;
    private BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private PageHeaderBand PageHeader;
    private FormattingRule formattingRule1;
    private ReportFooterBand ReportFooter;
    private XRLabel lblKyHoTen;
    private XRLabel lblLapBang;
    private XRLabel lblThuTruong;
    private XRTable tblReportHeader;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell401;
    private XRTableCell xrTableCell402;
    private XRTableCell xrTableCell403;
    private XRTableRow xrTableRow3;
    private XRTableCell xrCellLastReceivedDate;
    private XRTableCell xrCellToDate;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell601;
    private XRTableCell xrTableCell602;
    private XRLabel lblNgaySinh;
    private XRTableCell xrTableCell603;
    private XRLabel lblPhuCap;
    private XRTable xrTable12;
    private XRTableRow xrTableRow17;
    private XRTableCell xrTableCell42;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell44;
    private XRTableCell xrTableCell46;
    private XRTableRow xrTableRow19;
    private XRTableCell xrCellTenBieuMau;
    private XRTableCell xrCellReportName;
    private XRTableCell xrCellOrganization;
    private XRTable tblDetail;
    private XRTableRow xrDetailRow1;
    private XRTableCell xrTableCellHoVaTen;
    private XRTableCell xrTableCellNam;
    private XRTableCell xrTableCellNu;
    private XRTableCell xrTableCellChucVu;
    private XRTableCell xrTableCellCoQuan;
    private XRTableCell xrTableCellTongPhuCap;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTongNam;
    private XRTableCell xrTongNu;
    private XRTableCell xrTongCVu;
    private XRTableCell xrTongCoQuan;
    private XRTableCell xrTongThoiGian;
    private XRTableCell xrTongHeSoLuong;
    private XRTableCell xrTongBacLuong;
    private XRTableCell xrTongChucVu;
    private XRTableCell xrTongTrachNhiem;
    private XRTableCell xrTongKhuVuc;
    private XRTableCell xrTongPhuCapVuotKhung;
    private XRTableCell xrTongPhuCap;
    private XRTableCell xrTongGhiChu;
    private XRLabel lblReportDate;
    private XRLabel lblKyDongDau;
    private XRTableCell xrTableCell503;
    private XRTable tblPageHeader;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell241;
    private XRTableCell xrTableCell242;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell9;
    private XRLabel lblLuongHienHuong;
    private XRTable xrTable2;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell13;
    private XRTable xrTable3;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCellSoThuTu;
    private XRTableCell xrTableCellThoiGian;
    private XRTableCell xrTableCellHeSoLuong;
    private XRTableCell xrTableCellBacLuong;
    private XRTableCell xrTableCellChucVuXa;
    private XRTableCell xrTableCellTrachNhiem;
    private XRTableCell xrTableCellKhuVuc;
    private XRTableCell xrTableCellPhuCapVuotKhung;
    private XRTableCell xrTableCellGhiChu;
    private XRTableCell xrTextTongSo;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
        private IContainer components = null;

    public rp_SalaryCommuneCivilServants()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
    }
    int _stt = 1;
    private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrTableCellSoThuTu.Text = _stt.ToString();
        _stt++;
    }
    public void BindData(ReportFilter filter)
    {
        try
        {
            // get organization
            var organization = cat_DepartmentServices.GetByDepartments(filter.SessionDepartment);
            if (organization == null) return;
            // set report name
            xrCellReportName.Text = string.Format(xrCellReportName.Text, filter.Year);
            // set report organization
            xrCellOrganization.Text = string.Format(xrCellOrganization.Text, organization.Name);
            // init to date is the last day of the month
            var toDate = new DateTime(filter.Year, filter.StartMonth, DateTime.DaysInMonth(filter.Year, filter.StartMonth));
            // set received date, assume that expried after one month
            xrCellLastReceivedDate.Text = string.Format(xrCellLastReceivedDate.Text, toDate.Day, toDate.Month);
            // set report to date
            xrCellToDate.Text = string.Format(xrCellToDate.Text, toDate.Day, toDate.Month, toDate.Year);
            // set end report info
            var location = new ReportController().GetCityName(filter.SessionDepartment);
            lblReportDate.Text = string.Format(lblReportDate.Text, location, DateTime.Now.Day,
                DateTime.Now.Month, DateTime.Now.Year);

            // select form db               
            var arrOrgCode = string.IsNullOrEmpty(filter.SelectedDepartment)
                ? new string[] { }
                : filter.SelectedDepartment.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var table = SQLHelper.ExecuteTable(
                SQLReportAdapter.GetStore_DanhSachTienLuongCNCCCapXa(filter.WhereClause), new[] {"@MaDonVi"},
                new object[] {arrOrgCode[arrOrgCode.Length - 1]});


            DataSource = table;
            // bind report detail
            xrTableCellHoVaTen.DataBindings.Add("Text", DataSource, "HoTen");
            xrTableCellNam.DataBindings.Add("Text", DataSource, "Nam");
            xrTableCellNu.DataBindings.Add("Text", DataSource, "Nu");
            xrTableCellChucVu.DataBindings.Add("Text", DataSource, "ChucVu");
            xrTableCellCoQuan.DataBindings.Add("Text", DataSource, "CoQuan");
            xrTableCellThoiGian.DataBindings.Add("Text", DataSource, "ThoiGianGiuChucVu", "{0:n0}");
            xrTableCellHeSoLuong.DataBindings.Add("Text", DataSource, "HeSoLuong");
            xrTableCellBacLuong.DataBindings.Add("Text", DataSource, "BacLuong");
            xrTableCellChucVuXa.DataBindings.Add("Text", DataSource, "PhuCapChucVu");
            xrTableCellTrachNhiem.DataBindings.Add("Text", DataSource, "PhuCapTrachNhiem");
            xrTableCellKhuVuc.DataBindings.Add("Text", DataSource, "PhuCapKhuVuc");
            xrTableCellPhuCapVuotKhung.DataBindings.Add("Text", DataSource, "PhuCapVuotKhung");
            xrTableCellTongPhuCap.DataBindings.Add("Text", DataSource, "TongPhuCapPhanTram", "{0:n0}");
            xrTableCellGhiChu.DataBindings.Add("Text", DataSource, "GhiChu", "{0:n0}");
            // bind report footer
            xrTongHeSoLuong.DataBindings.Add("Text", DataSource, "xHeSoLuong");
            xrTongChucVu.DataBindings.Add("Text", DataSource, "xPhuCapChucVu");
            xrTongTrachNhiem.DataBindings.Add("Text", DataSource, "xPhuCapTrachNhiem");
            xrTongKhuVuc.DataBindings.Add("Text", DataSource, "xPhuCapKhuVuc");
            xrTongPhuCapVuotKhung.DataBindings.Add("Text", DataSource, "xPhuCapVuotKhung");
            xrTongPhuCap.DataBindings.Add("Text", DataSource, "xTongPhuCapPhanTram", "{0:n0}");
            xrTongGhiChu.DataBindings.Add("Text", DataSource, "", "{0:n0}");
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
            string resourceFileName = "rp_SalaryCommuneCivilServants.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.tblDetail = new DevExpress.XtraReports.UI.XRTable();
            this.xrDetailRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellSoThuTu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHoVaTen = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNam = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChucVu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCoQuan = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellThoiGian = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeSoLuong = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellBacLuong = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChucVuXa = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTrachNhiem = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellKhuVuc = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellPhuCapVuotKhung = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTongPhuCap = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellGhiChu = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.tblReportHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCellTenBieuMau = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellReportName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellOrganization = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell401 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell402 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell403 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCellLastReceivedDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellToDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell503 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell601 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell602 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell603 = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.tblPageHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell241 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell242 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblNgaySinh = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblLuongHienHuong = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblPhuCap = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable12 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblKyDongDau = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTextTongSo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongNam = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongNu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongCVu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongCoQuan = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongThoiGian = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongHeSoLuong = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongBacLuong = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongChucVu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongTrachNhiem = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongKhuVuc = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongPhuCapVuotKhung = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongPhuCap = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongGhiChu = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblKyHoTen = new DevExpress.XtraReports.UI.XRLabel();
            this.lblLapBang = new DevExpress.XtraReports.UI.XRLabel();
            this.lblThuTruong = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblReportHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).BeginInit();
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
            this.tblDetail.SizeF = new System.Drawing.SizeF(1139.96F, 25F);
            this.tblDetail.StylePriority.UseBorders = false;
            // 
            // xrDetailRow1
            // 
            this.xrDetailRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellSoThuTu,
            this.xrTableCellHoVaTen,
            this.xrTableCellNam,
            this.xrTableCellNu,
            this.xrTableCellChucVu,
            this.xrTableCellCoQuan,
            this.xrTableCellThoiGian,
            this.xrTableCellHeSoLuong,
            this.xrTableCellBacLuong,
            this.xrTableCellChucVuXa,
            this.xrTableCellTrachNhiem,
            this.xrTableCellKhuVuc,
            this.xrTableCellPhuCapVuotKhung,
            this.xrTableCellTongPhuCap,
            this.xrTableCellGhiChu});
            this.xrDetailRow1.Name = "xrDetailRow1";
            this.xrDetailRow1.Weight = 1D;
            // 
            // xrTableCellSoThuTu
            // 
            this.xrTableCellSoThuTu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSoThuTu.Name = "xrTableCellSoThuTu";
            this.xrTableCellSoThuTu.StylePriority.UseBorders = false;
            this.xrTableCellSoThuTu.StylePriority.UseFont = false;
            this.xrTableCellSoThuTu.StylePriority.UseTextAlignment = false;
            this.xrTableCellSoThuTu.Text = " ";
            this.xrTableCellSoThuTu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSoThuTu.Weight = 0.0817070126658404D;
            this.xrTableCellSoThuTu.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrTableCellHoVaTen
            // 
            this.xrTableCellHoVaTen.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHoVaTen.Name = "xrTableCellHoVaTen";
            this.xrTableCellHoVaTen.StylePriority.UseBorders = false;
            this.xrTableCellHoVaTen.StylePriority.UseTextAlignment = false;
            this.xrTableCellHoVaTen.Text = " ";
            this.xrTableCellHoVaTen.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellHoVaTen.Weight = 0.56825452613142358D;
            // 
            // xrTableCellNam
            // 
            this.xrTableCellNam.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNam.Name = "xrTableCellNam";
            this.xrTableCellNam.StylePriority.UseBorders = false;
            this.xrTableCellNam.StylePriority.UseTextAlignment = false;
            this.xrTableCellNam.Text = " ";
            this.xrTableCellNam.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNam.Weight = 0.21314872430354961D;
            // 
            // xrTableCellNu
            // 
            this.xrTableCellNu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNu.Name = "xrTableCellNu";
            this.xrTableCellNu.StylePriority.UseBorders = false;
            this.xrTableCellNu.StylePriority.UseTextAlignment = false;
            this.xrTableCellNu.Text = " ";
            this.xrTableCellNu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNu.Weight = 0.21314872430354961D;
            // 
            // xrTableCellChucVu
            // 
            this.xrTableCellChucVu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellChucVu.Name = "xrTableCellChucVu";
            this.xrTableCellChucVu.StylePriority.UseBorders = false;
            this.xrTableCellChucVu.StylePriority.UseTextAlignment = false;
            this.xrTableCellChucVu.Text = " ";
            this.xrTableCellChucVu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellChucVu.Weight = 0.29485575942897624D;
            // 
            // xrTableCellCoQuan
            // 
            this.xrTableCellCoQuan.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCoQuan.Name = "xrTableCellCoQuan";
            this.xrTableCellCoQuan.StylePriority.UseBorders = false;
            this.xrTableCellCoQuan.StylePriority.UseTextAlignment = false;
            this.xrTableCellCoQuan.Text = " ";
            this.xrTableCellCoQuan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellCoQuan.Weight = 0.4085350559334604D;
            // 
            // xrTableCellThoiGian
            // 
            this.xrTableCellThoiGian.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellThoiGian.Name = "xrTableCellThoiGian";
            this.xrTableCellThoiGian.StylePriority.UseBorders = false;
            this.xrTableCellThoiGian.StylePriority.UseTextAlignment = false;
            this.xrTableCellThoiGian.Text = " ";
            this.xrTableCellThoiGian.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellThoiGian.Weight = 0.28419830746237967D;
            // 
            // xrTableCellHeSoLuong
            // 
            this.xrTableCellHeSoLuong.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeSoLuong.Name = "xrTableCellHeSoLuong";
            this.xrTableCellHeSoLuong.StylePriority.UseBorders = false;
            this.xrTableCellHeSoLuong.StylePriority.UseTextAlignment = false;
            this.xrTableCellHeSoLuong.Text = " ";
            this.xrTableCellHeSoLuong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellHeSoLuong.Weight = 0.25755470954746562D;
            // 
            // xrTableCellBacLuong
            // 
            this.xrTableCellBacLuong.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellBacLuong.Name = "xrTableCellBacLuong";
            this.xrTableCellBacLuong.StylePriority.UseBorders = false;
            this.xrTableCellBacLuong.StylePriority.UseTextAlignment = false;
            this.xrTableCellBacLuong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellBacLuong.Weight = 0.25755471324258317D;
            // 
            // xrTableCellChucVuXa
            // 
            this.xrTableCellChucVuXa.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellChucVuXa.Name = "xrTableCellChucVuXa";
            this.xrTableCellChucVuXa.StylePriority.UseBorders = false;
            this.xrTableCellChucVuXa.StylePriority.UseTextAlignment = false;
            this.xrTableCellChucVuXa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellChucVuXa.Weight = 0.1953863389377406D;
            // 
            // xrTableCellTrachNhiem
            // 
            this.xrTableCellTrachNhiem.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTrachNhiem.Name = "xrTableCellTrachNhiem";
            this.xrTableCellTrachNhiem.StylePriority.UseBorders = false;
            this.xrTableCellTrachNhiem.StylePriority.UseTextAlignment = false;
            this.xrTableCellTrachNhiem.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTrachNhiem.Weight = 0.18828136889909883D;
            // 
            // xrTableCellKhuVuc
            // 
            this.xrTableCellKhuVuc.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellKhuVuc.Name = "xrTableCellKhuVuc";
            this.xrTableCellKhuVuc.StylePriority.UseBorders = false;
            this.xrTableCellKhuVuc.StylePriority.UseTextAlignment = false;
            this.xrTableCellKhuVuc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellKhuVuc.Weight = 0.21314872789725431D;
            // 
            // xrTableCellPhuCapVuotKhung
            // 
            this.xrTableCellPhuCapVuotKhung.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellPhuCapVuotKhung.Name = "xrTableCellPhuCapVuotKhung";
            this.xrTableCellPhuCapVuotKhung.StylePriority.UseBorders = false;
            this.xrTableCellPhuCapVuotKhung.StylePriority.UseTextAlignment = false;
            this.xrTableCellPhuCapVuotKhung.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellPhuCapVuotKhung.Weight = 0.23446360158839175D;
            // 
            // xrTableCellTongPhuCap
            // 
            this.xrTableCellTongPhuCap.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTongPhuCap.Name = "xrTableCellTongPhuCap";
            this.xrTableCellTongPhuCap.StylePriority.UseBorders = false;
            this.xrTableCellTongPhuCap.StylePriority.UseTextAlignment = false;
            this.xrTableCellTongPhuCap.Text = " ";
            this.xrTableCellTongPhuCap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTongPhuCap.Weight = 0.30551318111321069D;
            // 
            // xrTableCellGhiChu
            // 
            this.xrTableCellGhiChu.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellGhiChu.Name = "xrTableCellGhiChu";
            this.xrTableCellGhiChu.StylePriority.UseBorders = false;
            this.xrTableCellGhiChu.StylePriority.UseTextAlignment = false;
            this.xrTableCellGhiChu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellGhiChu.Weight = 0.33393282164313343D;
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
            this.tblReportHeader});
            this.ReportHeader.HeightF = 86F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // tblReportHeader
            // 
            this.tblReportHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tblReportHeader.Name = "tblReportHeader";
            this.tblReportHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow19,
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow4});
            this.tblReportHeader.SizeF = new System.Drawing.SizeF(1142.129F, 86F);
            // 
            // xrTableRow19
            // 
            this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCellTenBieuMau,
            this.xrCellReportName,
            this.xrCellOrganization});
            this.xrTableRow19.Name = "xrTableRow19";
            this.xrTableRow19.Weight = 0.79999990231146945D;
            // 
            // xrCellTenBieuMau
            // 
            this.xrCellTenBieuMau.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrCellTenBieuMau.Name = "xrCellTenBieuMau";
            this.xrCellTenBieuMau.StylePriority.UseFont = false;
            this.xrCellTenBieuMau.StylePriority.UseTextAlignment = false;
            this.xrCellTenBieuMau.Text = "BM04/BNV";
            this.xrCellTenBieuMau.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTenBieuMau.Weight = 1.5748560589095728D;
            // 
            // xrCellReportName
            // 
            this.xrCellReportName.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrCellReportName.Name = "xrCellReportName";
            this.xrCellReportName.StylePriority.UseFont = false;
            this.xrCellReportName.Text = "BÁO CÁO DANH SÁCH VÀ TIỀN LƯƠNG CÁN BỘ, CÔNG CHỨC CẤP XÃ NĂM {0}";
            this.xrCellReportName.Weight = 3.2928199055848522D;
            // 
            // xrCellOrganization
            // 
            this.xrCellOrganization.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrCellOrganization.Name = "xrCellOrganization";
            this.xrCellOrganization.StylePriority.UseFont = false;
            this.xrCellOrganization.StylePriority.UseTextAlignment = false;
            this.xrCellOrganization.Text = "Đơn vị báo cáo: {0}";
            this.xrCellOrganization.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCellOrganization.Weight = 1.1279513169596362D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell401,
            this.xrTableCell402,
            this.xrTableCell403});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 0.933333189760807D;
            // 
            // xrTableCell401
            // 
            this.xrTableCell401.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCell401.Multiline = true;
            this.xrTableCell401.Name = "xrTableCell401";
            this.xrTableCell401.StylePriority.UseFont = false;
            this.xrTableCell401.Text = "Ban hành kèm theo Thông tư số 11/2012/TT-BNV\r\nngày 17 tháng 12 năm 2012 của Bộ Nộ" +
    "i vụ";
            this.xrTableCell401.Weight = 1.5748560433256003D;
            // 
            // xrTableCell402
            // 
            this.xrTableCell402.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCell402.Name = "xrTableCell402";
            this.xrTableCell402.StylePriority.UseFont = false;
            this.xrTableCell402.Text = "(Áp dụng đối với tỉnh, thành phố trực thuộc Trung ương)";
            this.xrTableCell402.Weight = 3.2928199438412697D;
            // 
            // xrTableCell403
            // 
            this.xrTableCell403.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCell403.Name = "xrTableCell403";
            this.xrTableCell403.StylePriority.UseFont = false;
            this.xrTableCell403.StylePriority.UseTextAlignment = false;
            this.xrTableCell403.Text = "Đơn vị nhận báo cáo: Bộ Nội Vụ";
            this.xrTableCell403.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell403.Weight = 1.1279512942871914D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCellLastReceivedDate,
            this.xrCellToDate,
            this.xrTableCell503});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrTableRow3.StylePriority.UsePadding = false;
            this.xrTableRow3.Weight = 0.79999990685278288D;
            // 
            // xrCellLastReceivedDate
            // 
            this.xrCellLastReceivedDate.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrCellLastReceivedDate.Name = "xrCellLastReceivedDate";
            this.xrCellLastReceivedDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrCellLastReceivedDate.StylePriority.UseFont = false;
            this.xrCellLastReceivedDate.StylePriority.UsePadding = false;
            this.xrCellLastReceivedDate.Text = "Thời hạn nhận báo cáo ngày {0} tháng {1}";
            this.xrCellLastReceivedDate.Weight = 1.5748560433256003D;
            // 
            // xrCellToDate
            // 
            this.xrCellToDate.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrCellToDate.Name = "xrCellToDate";
            this.xrCellToDate.StylePriority.UseFont = false;
            this.xrCellToDate.Text = "Tính đến ngày {0} tháng {1} năm {2}";
            this.xrCellToDate.Weight = 3.2928199438412697D;
            // 
            // xrTableCell503
            // 
            this.xrTableCell503.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCell503.Name = "xrTableCell503";
            this.xrTableCell503.StylePriority.UseFont = false;
            this.xrTableCell503.StylePriority.UseTextAlignment = false;
            this.xrTableCell503.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell503.Weight = 1.1279512942871914D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell601,
            this.xrTableCell602,
            this.xrTableCell603});
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
            // xrTableCell603
            // 
            this.xrTableCell603.Name = "xrTableCell603";
            this.xrTableCell603.Weight = 1.1279512942871914D;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblPageHeader});
            this.PageHeader.HeightF = 100F;
            this.PageHeader.Name = "PageHeader";
            // 
            // tblPageHeader
            // 
            this.tblPageHeader.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tblPageHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tblPageHeader.Name = "tblPageHeader";
            this.tblPageHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
            this.tblPageHeader.SizeF = new System.Drawing.SizeF(1140F, 100F);
            this.tblPageHeader.StylePriority.UseBorders = false;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell241,
            this.xrTableCell242,
            this.xrTableCell2,
            this.xrTableCell1,
            this.xrTableCell8,
            this.xrTableCell4,
            this.xrTableCell9,
            this.xrTableCell5,
            this.xrTableCell6});
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
            this.xrTableCell241.Weight = 0.09800902190058941D;
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
            this.xrTableCell242.Weight = 0.68162955952934445D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblNgaySinh,
            this.xrTable3});
            this.xrTableCell2.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorderColor = false;
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Weight = 0.5113509752501787D;
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lblNgaySinh.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblNgaySinh.SizeF = new System.Drawing.SizeF(120F, 25F);
            this.lblNgaySinh.StylePriority.UseBorders = false;
            this.lblNgaySinh.Text = "Ngày tháng năm sinh";
            // 
            // xrTable3
            // 
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24.99999F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrTable3.SizeF = new System.Drawing.SizeF(120F, 75F);
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell7});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorderColor = false;
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.Text = "Nam";
            this.xrTableCell3.Weight = 1.1915405913495862D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBorderColor = false;
            this.xrTableCell7.StylePriority.UseBorders = false;
            this.xrTableCell7.Text = "Nữ";
            this.xrTableCell7.Weight = 1.191540535235484D;
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
            this.xrTableCell1.Text = "Chức vụ hoặc chức danh công tác";
            this.xrTableCell1.Weight = 0.35368439882950026D;
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
            this.xrTableCell8.Text = "Cơ quan, đơn vị đang làm việc";
            this.xrTableCell8.Weight = 0.49004465770936306D;
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
            this.xrTableCell4.Text = "Thời gian giữ chức vụ, chức danh";
            this.xrTableCell4.Weight = 0.34090062364638807D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell9.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblLuongHienHuong,
            this.xrTable2});
            this.xrTableCell9.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorderColor = false;
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.Text = "xrTableCell9";
            this.xrTableCell9.Weight = 0.6178823868811727D;
            // 
            // lblLuongHienHuong
            // 
            this.lblLuongHienHuong.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lblLuongHienHuong.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblLuongHienHuong.Name = "lblLuongHienHuong";
            this.lblLuongHienHuong.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblLuongHienHuong.SizeF = new System.Drawing.SizeF(145F, 25F);
            this.lblLuongHienHuong.StylePriority.UseBorders = false;
            this.lblLuongHienHuong.Text = "Mức lương hiện hưởng";
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25.00001F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrTable2.SizeF = new System.Drawing.SizeF(145F, 75F);
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell12});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBorders = false;
            this.xrTableCell11.Text = "Hệ số lương";
            this.xrTableCell11.Weight = 1.2608695743415912D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseBorders = false;
            this.xrTableCell12.Text = "Bậc lương hiện hưởng";
            this.xrTableCell12.Weight = 1.2608695560931915D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell5.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblPhuCap,
            this.xrTable12});
            this.xrTableCell5.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBorderColor = false;
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.Text = "xrTableCell5";
            this.xrTableCell5.Weight = 1.3637741451337162D;
            // 
            // lblPhuCap
            // 
            this.lblPhuCap.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lblPhuCap.LocationFloat = new DevExpress.Utils.PointFloat(6.103516E-05F, 0F);
            this.lblPhuCap.Name = "lblPhuCap";
            this.lblPhuCap.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblPhuCap.SizeF = new System.Drawing.SizeF(320F, 25F);
            this.lblPhuCap.StylePriority.UseBorders = false;
            this.lblPhuCap.Text = "Phụ cấp";
            // 
            // xrTable12
            // 
            this.xrTable12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrTable12.Name = "xrTable12";
            this.xrTable12.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow17});
            this.xrTable12.SizeF = new System.Drawing.SizeF(320F, 75F);
            // 
            // xrTableRow17
            // 
            this.xrTableRow17.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableRow17.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell42,
            this.xrTableCell43,
            this.xrTableCell44,
            this.xrTableCell46,
            this.xrTableCell13});
            this.xrTableRow17.Name = "xrTableRow17";
            this.xrTableRow17.StylePriority.UseBorders = false;
            this.xrTableRow17.Weight = 2.8000000000000003D;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.StylePriority.UseBorders = false;
            this.xrTableCell42.Text = "Chức vụ";
            this.xrTableCell42.Weight = 0.55000000061238885D;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.StylePriority.UseBorders = false;
            this.xrTableCell43.Text = "Trách nhiệm";
            this.xrTableCell43.Weight = 0.53000003346050739D;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.StylePriority.UseBorders = false;
            this.xrTableCell44.Text = "Khu vực";
            this.xrTableCell44.Weight = 0.60000003622615994D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.StylePriority.UseBorders = false;
            this.xrTableCell46.Text = "Phụ cấp vượt khung";
            this.xrTableCell46.Weight = 0.66000001656464591D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseBorders = false;
            this.xrTableCell13.Text = "Tổng phụ cấp theo phần trăm";
            this.xrTableCell13.Weight = 0.860000044553564D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell6.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseBorderColor = false;
            this.xrTableCell6.StylePriority.UseBorders = false;
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.Text = "Ghi chú";
            this.xrTableCell6.Weight = 0.40055820453728957D;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblReportDate,
            this.lblKyDongDau,
            this.xrTable1,
            this.lblKyHoTen,
            this.lblLapBang,
            this.lblThuTruong});
            this.ReportFooter.HeightF = 252.1667F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // lblReportDate
            // 
            this.lblReportDate.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Italic);
            this.lblReportDate.LocationFloat = new DevExpress.Utils.PointFloat(687.391F, 46.54177F);
            this.lblReportDate.Name = "lblReportDate";
            this.lblReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblReportDate.SizeF = new System.Drawing.SizeF(300F, 15F);
            this.lblReportDate.StylePriority.UseFont = false;
            this.lblReportDate.StylePriority.UseTextAlignment = false;
            this.lblReportDate.Text = "{0}, ngày {1} tháng {2} năm {3}";
            this.lblReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblKyDongDau
            // 
            this.lblKyDongDau.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Italic);
            this.lblKyDongDau.LocationFloat = new DevExpress.Utils.PointFloat(687.391F, 86.54176F);
            this.lblKyDongDau.Name = "lblKyDongDau";
            this.lblKyDongDau.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblKyDongDau.SizeF = new System.Drawing.SizeF(300F, 10F);
            this.lblKyDongDau.StylePriority.UseFont = false;
            this.lblKyDongDau.StylePriority.UseTextAlignment = false;
            this.lblKyDongDau.Text = "(Ký, đóng dấu)";
            this.lblKyDongDau.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1139.96F, 25F);
            this.xrTable1.StylePriority.UseBorders = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTextTongSo,
            this.xrTongNam,
            this.xrTongNu,
            this.xrTongCVu,
            this.xrTongCoQuan,
            this.xrTongThoiGian,
            this.xrTongHeSoLuong,
            this.xrTongBacLuong,
            this.xrTongChucVu,
            this.xrTongTrachNhiem,
            this.xrTongKhuVuc,
            this.xrTongPhuCapVuotKhung,
            this.xrTongPhuCap,
            this.xrTongGhiChu});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTextTongSo
            // 
            this.xrTextTongSo.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTextTongSo.Name = "xrTextTongSo";
            this.xrTextTongSo.StylePriority.UseBorders = false;
            this.xrTextTongSo.StylePriority.UseTextAlignment = false;
            this.xrTextTongSo.Text = "Tổng số";
            this.xrTextTongSo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTextTongSo.Weight = 0.90196070540578621D;
            // 
            // xrTongNam
            // 
            this.xrTongNam.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongNam.Name = "xrTongNam";
            this.xrTongNam.StylePriority.UseBorders = false;
            this.xrTongNam.Weight = 0.29578951999597436D;
            // 
            // xrTongNu
            // 
            this.xrTongNu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongNu.Name = "xrTongNu";
            this.xrTongNu.StylePriority.UseBorders = false;
            this.xrTongNu.Weight = 0.2957894447728211D;
            // 
            // xrTongCVu
            // 
            this.xrTongCVu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongCVu.Name = "xrTongCVu";
            this.xrTongCVu.StylePriority.UseBorders = false;
            this.xrTongCVu.Weight = 0.40917546108312774D;
            // 
            // xrTongCoQuan
            // 
            this.xrTongCoQuan.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongCoQuan.Name = "xrTongCoQuan";
            this.xrTongCoQuan.StylePriority.UseBorders = false;
            this.xrTongCoQuan.Weight = 0.56692979350842942D;
            // 
            // xrTongThoiGian
            // 
            this.xrTongThoiGian.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongThoiGian.Name = "xrTongThoiGian";
            this.xrTongThoiGian.StylePriority.UseBorders = false;
            this.xrTongThoiGian.Weight = 0.39438590869568946D;
            // 
            // xrTongHeSoLuong
            // 
            this.xrTongHeSoLuong.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongHeSoLuong.Name = "xrTongHeSoLuong";
            this.xrTongHeSoLuong.StylePriority.UseBorders = false;
            this.xrTongHeSoLuong.Weight = 0.35741229970831634D;
            // 
            // xrTongBacLuong
            // 
            this.xrTongBacLuong.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongBacLuong.Name = "xrTongBacLuong";
            this.xrTongBacLuong.StylePriority.UseBorders = false;
            this.xrTongBacLuong.Weight = 0.35741229970831612D;
            // 
            // xrTongChucVu
            // 
            this.xrTongChucVu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongChucVu.Name = "xrTongChucVu";
            this.xrTongChucVu.StylePriority.UseBorders = false;
            this.xrTongChucVu.Weight = 0.27114036988375478D;
            // 
            // xrTongTrachNhiem
            // 
            this.xrTongTrachNhiem.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongTrachNhiem.Name = "xrTongTrachNhiem";
            this.xrTongTrachNhiem.StylePriority.UseBorders = false;
            this.xrTongTrachNhiem.Weight = 0.261280720760948D;
            // 
            // xrTongKhuVuc
            // 
            this.xrTongKhuVuc.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongKhuVuc.Name = "xrTongKhuVuc";
            this.xrTongKhuVuc.StylePriority.UseBorders = false;
            this.xrTongKhuVuc.Weight = 0.29578949269077237D;
            // 
            // xrTongPhuCapVuotKhung
            // 
            this.xrTongPhuCapVuotKhung.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongPhuCapVuotKhung.Name = "xrTongPhuCapVuotKhung";
            this.xrTongPhuCapVuotKhung.StylePriority.UseBorders = false;
            this.xrTongPhuCapVuotKhung.Weight = 0.32536844005919335D;
            // 
            // xrTongPhuCap
            // 
            this.xrTongPhuCap.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongPhuCap.Name = "xrTongPhuCap";
            this.xrTongPhuCap.StylePriority.UseBorders = false;
            this.xrTongPhuCap.Weight = 0.42396493128726365D;
            // 
            // xrTongGhiChu
            // 
            this.xrTongGhiChu.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongGhiChu.Name = "xrTongGhiChu";
            this.xrTongGhiChu.StylePriority.UseBorders = false;
            this.xrTongGhiChu.Weight = 0.46340382867110408D;
            // 
            // lblKyHoTen
            // 
            this.lblKyHoTen.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Italic);
            this.lblKyHoTen.LocationFloat = new DevExpress.Utils.PointFloat(108.5785F, 86.54176F);
            this.lblKyHoTen.Name = "lblKyHoTen";
            this.lblKyHoTen.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblKyHoTen.SizeF = new System.Drawing.SizeF(180F, 10F);
            this.lblKyHoTen.StylePriority.UseFont = false;
            this.lblKyHoTen.StylePriority.UseTextAlignment = false;
            this.lblKyHoTen.Text = "(Ký, họ tên)";
            this.lblKyHoTen.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblLapBang
            // 
            this.lblLapBang.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.lblLapBang.LocationFloat = new DevExpress.Utils.PointFloat(108.5785F, 61.54176F);
            this.lblLapBang.Name = "lblLapBang";
            this.lblLapBang.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblLapBang.SizeF = new System.Drawing.SizeF(180F, 25F);
            this.lblLapBang.StylePriority.UseFont = false;
            this.lblLapBang.StylePriority.UseTextAlignment = false;
            this.lblLapBang.Text = "NGƯỜI LẬP BẢNG";
            this.lblLapBang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblThuTruong
            // 
            this.lblThuTruong.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.lblThuTruong.LocationFloat = new DevExpress.Utils.PointFloat(687.391F, 61.54176F);
            this.lblThuTruong.Name = "lblThuTruong";
            this.lblThuTruong.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblThuTruong.SizeF = new System.Drawing.SizeF(300F, 25F);
            this.lblThuTruong.StylePriority.UseFont = false;
            this.lblThuTruong.StylePriority.UseTextAlignment = false;
            this.lblThuTruong.Text = "THỦ TRƯỞNG ĐƠN VỊ";
            this.lblThuTruong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // rp_SalaryCommuneCivilServants
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
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(12, 12, 15, 0);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblReportHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
}