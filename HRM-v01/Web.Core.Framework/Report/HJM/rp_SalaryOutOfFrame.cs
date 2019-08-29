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
    /// Summary description for rp_CCVC_BaoCaoDanhSachCanBoDenHanXetVuotKhung
    /// </summary>
    public class rp_SalaryOutOfFrame : XtraReport
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
        private XRTable tblDetail;
        private XRTableRow xrDetailRow1;
        private XRTableCell xrTableCellHoVaTen;
        private XRTableCell xrTableCellDonViDangCongTac;
        private XRTableCell xrTableCellDonViNoiDen;
        private XRLabel lblReportDate;
        private XRLabel lblKyDongDau;
        private XRTable tblPageHeader;
        private XRTableRow xrTableRow11;
        private XRTableCell xrTableCell241;
        private XRTableCell xrTableCell242;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCellSoThuTu;
        private XRLabel xrLabel2;
        private XRLabel xrLabel1;
        private XRTableCell xrTableCellSoHieuCBCC;
        private XRTableCell xrTableCell11;
        private XRTableCell xrTableCellChucVu;
        private XRTableCell xrTableCell12;
        private XRTableCell xrTableCellGhiChu;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCellQuyetDinhGanNhat;
        private XRTableCell xrTableCellNgayKy;
        private XRTableCell xrTableCellNgayHuong;
        private XRTableCell xrTableCellThoiGianHuong;
        private XRTableCell xrTableCellTenNgach;
        private XRTableCell xrTableCellMaSoNgach;
        private XRTableCell xrTableCellBacNgach;
        private XRTableCell xrTableCellHeSo;
        private XRTableCell xrTableCellPhuCapChucVu;
        private XRTableCell xrTableCellThoiHanXetVuotKhung;
        private XRTableCell xrTableCell24;
        private XRTableCell xrTableCell23;
        private XRTableCell xrTableCell22;
        private XRTableCell xrTableCell18;
        private XRTableCell xrTableCell15;
        private XRTableCell xrTableCell13;
        private XRTableCell xrTableCell9;
        private XRTableCell xrTableCell8;
        private XRTableCell xrTableCell7;
        private XRTableCell xrTableCell6;
        private XRTableCell xrTableCell4;
        private XRTableCell xrTableCell3;
        private XRTableCell xrTableCellPhuCapKhac;
        private XRTableCell xrTableCellKhungBacToiDa;
        private XRLabel xrTenCoQuanCapTren;
        private XRLabel xrLabel4;
        private XRLabel xrTenCoQuanDonVi;
        private XRLabel xrLabel6;
        private XRLabel xrLabel7;
        private XRLabel xrDate;
        private XRLabel xrLabel9;
        private GroupHeaderBand GroupHeader1;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrTableCell27;
        private XRTableCell xrTableCellGroupHead;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
        private IContainer components = null;

        public rp_SalaryOutOfFrame()
        {
            InitializeComponent();
        }
        int _stt;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _stt++;
            xrTableCellSoThuTu.Text = _stt.ToString();

        }
        private void Group_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _stt = 0;
            xrTableCellSoThuTu.Text = _stt.ToString();

        }
        public void BindData(ReportFilter filter)
        {
            try
            {
                var control = new ReportController();
                xrTenCoQuanDonVi.Text = control.GetCompanyName(filter.SessionDepartment);
                var toDate = filter.ReportedDate;
                xrDate.Text = string.Format(xrDate.Text, toDate.Day, toDate.Month, toDate.Year);

                var location = new ReportController().GetCityName(filter.SessionDepartment);
                lblReportDate.Text = string.Format(lblReportDate.Text, location, DateTime.Now.Day,
                    DateTime.Now.Month, DateTime.Now.Year);

                // get organization
                var organization = cat_DepartmentServices.GetByDepartments(filter.SessionDepartment);
                if (organization == null) return;
                //select form db
                var departments = filter.SelectedDepartment;
                var arrDepartment = departments.Split(new[] { ',' }, StringSplitOptions.None);
                for (var i = 0; i < arrDepartment.Length; i++)
                {
                    arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
                }
                var table = SQLHelper.ExecuteTable(
                    SQLManagementSalaryAdapter.GetStore_ListEmployeeExpirationOutOfFrame(
                        string.Join(",", arrDepartment), filter.WhereClause));
                DataSource = table;
                    
                //binding data
                xrTableCellSoHieuCBCC.DataBindings.Add("Text", DataSource, "EmployeeCode");
                xrTableCellHoVaTen.DataBindings.Add("Text", DataSource, "FullName");
                xrTableCellDonViDangCongTac.DataBindings.Add("Text", DataSource, "DepartmentName");
                xrTableCellChucVu.DataBindings.Add("Text", DataSource, "PositionName");
                xrTableCellQuyetDinhGanNhat.DataBindings.Add("Text", DataSource, "DecisionNumber");
                xrTableCellNgayKy.DataBindings.Add("Text", DataSource, "DecisionDate", "{0:dd/MM/yyyy}");
                xrTableCellNgayHuong.DataBindings.Add("Text", DataSource, "EffectiveDate","{0:dd/MM/yyyy}");
                xrTableCellThoiGianHuong.DataBindings.Add("Text", DataSource, "MonthNumber");
                xrTableCellTenNgach.DataBindings.Add("Text", DataSource, "QuantumName");
                xrTableCellMaSoNgach.DataBindings.Add("Text", DataSource, "QuantumId");
                xrTableCellBacNgach.DataBindings.Add("Text", DataSource, "SalaryGrade");
                xrTableCellHeSo.DataBindings.Add("Text", DataSource, "SalaryFactor");
                xrTableCellPhuCapChucVu.DataBindings.Add("Text", DataSource, "PositionAllowance");
                xrTableCellPhuCapKhac.DataBindings.Add("Text", DataSource, "OtherAllowance");
                xrTableCellKhungBacToiDa.DataBindings.Add("Text", DataSource, "QuantumGrade");
                xrTableCellThoiHanXetVuotKhung.DataBindings.Add("Text", DataSource, "FrameOutcome", "{0:dd/MM/yyyy}");
                xrTableCellGhiChu.DataBindings.Add("Text", DataSource, "Note");

                GroupHeader1.GroupFields.AddRange(new[] { new GroupField("DepartmentId", XRColumnSortOrder.Ascending)});
                xrTableCellGroupHead.DataBindings.Add("Text", DataSource, "DepartmentName");
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
            string resourceFileName = "rp_SalaryOutOfFrame.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.tblDetail = new DevExpress.XtraReports.UI.XRTable();
            this.xrDetailRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellSoThuTu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellSoHieuCBCC = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHoVaTen = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDonViDangCongTac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChucVu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellQuyetDinhGanNhat = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNgayKy = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNgayHuong = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellThoiGianHuong = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTenNgach = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellMaSoNgach = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellBacNgach = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHeSo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellPhuCapChucVu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellPhuCapKhac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellKhungBacToiDa = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellThoiHanXetVuotKhung = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellGhiChu = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTenCoQuanCapTren = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTenCoQuanDonVi = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.tblPageHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell241 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell242 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblKyDongDau = new DevExpress.XtraReports.UI.XRLabel();
            this.lblKyHoTen = new DevExpress.XtraReports.UI.XRLabel();
            this.lblLapBang = new DevExpress.XtraReports.UI.XRLabel();
            this.lblThuTruong = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellGroupHead = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
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
            this.xrTableCellSoThuTu,
            this.xrTableCellSoHieuCBCC,
            this.xrTableCellHoVaTen,
            this.xrTableCellDonViDangCongTac,
            this.xrTableCellChucVu,
            this.xrTableCellQuyetDinhGanNhat,
            this.xrTableCellNgayKy,
            this.xrTableCellNgayHuong,
            this.xrTableCellThoiGianHuong,
            this.xrTableCellTenNgach,
            this.xrTableCellMaSoNgach,
            this.xrTableCellBacNgach,
            this.xrTableCellHeSo,
            this.xrTableCellPhuCapChucVu,
            this.xrTableCellPhuCapKhac,
            this.xrTableCellKhungBacToiDa,
            this.xrTableCellThoiHanXetVuotKhung,
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
            this.xrTableCellSoThuTu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSoThuTu.Weight = 0.064916771220082936D;
            this.xrTableCellSoThuTu.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrTableCellSoHieuCBCC
            // 
            this.xrTableCellSoHieuCBCC.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSoHieuCBCC.Name = "xrTableCellSoHieuCBCC";
            this.xrTableCellSoHieuCBCC.StylePriority.UseBorders = false;
            this.xrTableCellSoHieuCBCC.StylePriority.UseTextAlignment = false;
            this.xrTableCellSoHieuCBCC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSoHieuCBCC.Weight = 0.20473656702276216D;
            // 
            // xrTableCellHoVaTen
            // 
            this.xrTableCellHoVaTen.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHoVaTen.Name = "xrTableCellHoVaTen";
            this.xrTableCellHoVaTen.StylePriority.UseBorders = false;
            this.xrTableCellHoVaTen.StylePriority.UseTextAlignment = false;
            this.xrTableCellHoVaTen.Text = " ";
            this.xrTableCellHoVaTen.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellHoVaTen.Weight = 0.46745590566709411D;
            // 
            // xrTableCellDonViDangCongTac
            // 
            this.xrTableCellDonViDangCongTac.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDonViDangCongTac.Name = "xrTableCellDonViDangCongTac";
            this.xrTableCellDonViDangCongTac.StylePriority.UseBorders = false;
            this.xrTableCellDonViDangCongTac.StylePriority.UseTextAlignment = false;
            this.xrTableCellDonViDangCongTac.Text = " ";
            this.xrTableCellDonViDangCongTac.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDonViDangCongTac.Weight = 0.27635199005562194D;
            // 
            // xrTableCellChucVu
            // 
            this.xrTableCellChucVu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellChucVu.Name = "xrTableCellChucVu";
            this.xrTableCellChucVu.StylePriority.UseBorders = false;
            this.xrTableCellChucVu.StylePriority.UseTextAlignment = false;
            this.xrTableCellChucVu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellChucVu.Weight = 0.20705924263473172D;
            // 
            // xrTableCellQuyetDinhGanNhat
            // 
            this.xrTableCellQuyetDinhGanNhat.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellQuyetDinhGanNhat.Name = "xrTableCellQuyetDinhGanNhat";
            this.xrTableCellQuyetDinhGanNhat.StylePriority.UseBorders = false;
            this.xrTableCellQuyetDinhGanNhat.StylePriority.UseTextAlignment = false;
            this.xrTableCellQuyetDinhGanNhat.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellQuyetDinhGanNhat.Weight = 0.27092376379671179D;
            // 
            // xrTableCellNgayKy
            // 
            this.xrTableCellNgayKy.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNgayKy.Name = "xrTableCellNgayKy";
            this.xrTableCellNgayKy.StylePriority.UseBorders = false;
            this.xrTableCellNgayKy.StylePriority.UseTextAlignment = false;
            this.xrTableCellNgayKy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNgayKy.Weight = 0.21832917806106317D;
            // 
            // xrTableCellNgayHuong
            // 
            this.xrTableCellNgayHuong.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNgayHuong.Name = "xrTableCellNgayHuong";
            this.xrTableCellNgayHuong.StylePriority.UseBorders = false;
            this.xrTableCellNgayHuong.StylePriority.UseTextAlignment = false;
            this.xrTableCellNgayHuong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNgayHuong.Weight = 0.21832917806106311D;
            // 
            // xrTableCellThoiGianHuong
            // 
            this.xrTableCellThoiGianHuong.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellThoiGianHuong.Name = "xrTableCellThoiGianHuong";
            this.xrTableCellThoiGianHuong.StylePriority.UseBorders = false;
            this.xrTableCellThoiGianHuong.StylePriority.UseTextAlignment = false;
            this.xrTableCellThoiGianHuong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellThoiGianHuong.Weight = 0.21832902134039073D;
            // 
            // xrTableCellTenNgach
            // 
            this.xrTableCellTenNgach.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTenNgach.Name = "xrTableCellTenNgach";
            this.xrTableCellTenNgach.StylePriority.UseBorders = false;
            this.xrTableCellTenNgach.StylePriority.UseTextAlignment = false;
            this.xrTableCellTenNgach.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTenNgach.Weight = 0.21832939818358244D;
            // 
            // xrTableCellMaSoNgach
            // 
            this.xrTableCellMaSoNgach.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellMaSoNgach.Name = "xrTableCellMaSoNgach";
            this.xrTableCellMaSoNgach.StylePriority.UseBorders = false;
            this.xrTableCellMaSoNgach.StylePriority.UseTextAlignment = false;
            this.xrTableCellMaSoNgach.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellMaSoNgach.Weight = 0.17324808711038151D;
            // 
            // xrTableCellBacNgach
            // 
            this.xrTableCellBacNgach.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellBacNgach.Name = "xrTableCellBacNgach";
            this.xrTableCellBacNgach.StylePriority.UseBorders = false;
            this.xrTableCellBacNgach.StylePriority.UseTextAlignment = false;
            this.xrTableCellBacNgach.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellBacNgach.Weight = 0.18451857822945145D;
            // 
            // xrTableCellHeSo
            // 
            this.xrTableCellHeSo.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHeSo.Name = "xrTableCellHeSo";
            this.xrTableCellHeSo.StylePriority.UseBorders = false;
            this.xrTableCellHeSo.StylePriority.UseTextAlignment = false;
            this.xrTableCellHeSo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellHeSo.Weight = 0.18451835371541736D;
            // 
            // xrTableCellPhuCapChucVu
            // 
            this.xrTableCellPhuCapChucVu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellPhuCapChucVu.Name = "xrTableCellPhuCapChucVu";
            this.xrTableCellPhuCapChucVu.StylePriority.UseBorders = false;
            this.xrTableCellPhuCapChucVu.StylePriority.UseTextAlignment = false;
            this.xrTableCellPhuCapChucVu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellPhuCapChucVu.Weight = 0.25589684181423289D;
            // 
            // xrTableCellPhuCapKhac
            // 
            this.xrTableCellPhuCapKhac.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellPhuCapKhac.Name = "xrTableCellPhuCapKhac";
            this.xrTableCellPhuCapKhac.StylePriority.UseBorders = false;
            this.xrTableCellPhuCapKhac.StylePriority.UseTextAlignment = false;
            this.xrTableCellPhuCapKhac.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellPhuCapKhac.Weight = 0.18076175556436153D;
            // 
            // xrTableCellKhungBacToiDa
            // 
            this.xrTableCellKhungBacToiDa.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellKhungBacToiDa.Name = "xrTableCellKhungBacToiDa";
            this.xrTableCellKhungBacToiDa.StylePriority.UseBorders = false;
            this.xrTableCellKhungBacToiDa.StylePriority.UseTextAlignment = false;
            this.xrTableCellKhungBacToiDa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellKhungBacToiDa.Weight = 0.24086995153267751D;
            // 
            // xrTableCellThoiHanXetVuotKhung
            // 
            this.xrTableCellThoiHanXetVuotKhung.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellThoiHanXetVuotKhung.Name = "xrTableCellThoiHanXetVuotKhung";
            this.xrTableCellThoiHanXetVuotKhung.StylePriority.UseBorders = false;
            this.xrTableCellThoiHanXetVuotKhung.StylePriority.UseTextAlignment = false;
            this.xrTableCellThoiHanXetVuotKhung.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellThoiHanXetVuotKhung.Weight = 0.30849112169073462D;
            // 
            // xrTableCellGhiChu
            // 
            this.xrTableCellGhiChu.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellGhiChu.Name = "xrTableCellGhiChu";
            this.xrTableCellGhiChu.StylePriority.UseBorders = false;
            this.xrTableCellGhiChu.StylePriority.UseTextAlignment = false;
            this.xrTableCellGhiChu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellGhiChu.Weight = 0.21832987973900825D;
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
            this.xrTenCoQuanCapTren,
            this.xrLabel4,
            this.xrTenCoQuanDonVi,
            this.xrLabel6,
            this.xrLabel7,
            this.xrDate,
            this.xrLabel9});
            this.ReportHeader.HeightF = 106F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrTenCoQuanCapTren
            // 
            this.xrTenCoQuanCapTren.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTenCoQuanCapTren.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTenCoQuanCapTren.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTenCoQuanCapTren.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTenCoQuanCapTren.Name = "xrTenCoQuanCapTren";
            this.xrTenCoQuanCapTren.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTenCoQuanCapTren.SizeF = new System.Drawing.SizeF(204.3843F, 24F);
            this.xrTenCoQuanCapTren.StylePriority.UseBorderColor = false;
            this.xrTenCoQuanCapTren.StylePriority.UseBorders = false;
            this.xrTenCoQuanCapTren.StylePriority.UseFont = false;
            this.xrTenCoQuanCapTren.StylePriority.UseTextAlignment = false;
            this.xrTenCoQuanCapTren.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.BorderColor = System.Drawing.Color.DarkGray;
            this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(204.3843F, 0F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(935.6157F, 24F);
            this.xrLabel4.StylePriority.UseBorderColor = false;
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTenCoQuanDonVi
            // 
            this.xrTenCoQuanDonVi.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTenCoQuanDonVi.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTenCoQuanDonVi.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTenCoQuanDonVi.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24F);
            this.xrTenCoQuanDonVi.Name = "xrTenCoQuanDonVi";
            this.xrTenCoQuanDonVi.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTenCoQuanDonVi.SizeF = new System.Drawing.SizeF(204.3843F, 24F);
            this.xrTenCoQuanDonVi.StylePriority.UseBorderColor = false;
            this.xrTenCoQuanDonVi.StylePriority.UseBorders = false;
            this.xrTenCoQuanDonVi.StylePriority.UseFont = false;
            this.xrTenCoQuanDonVi.StylePriority.UseTextAlignment = false;
            this.xrTenCoQuanDonVi.Text = "TÊN CƠ QUAN ĐƠN VỊ";
            this.xrTenCoQuanDonVi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel6
            // 
            this.xrLabel6.BorderColor = System.Drawing.Color.DarkGray;
            this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel6.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(204.3843F, 24F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(935.6157F, 24F);
            this.xrLabel6.StylePriority.UseBorderColor = false;
            this.xrLabel6.StylePriority.UseBorders = false;
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel7
            // 
            this.xrLabel7.BorderColor = System.Drawing.Color.DarkGray;
            this.xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel7.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 48F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(1140F, 24F);
            this.xrLabel7.StylePriority.UseBorderColor = false;
            this.xrLabel7.StylePriority.UseBorders = false;
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "BÁO CÁO DANH SÁCH CÁN BỘ ĐẾN HẠN XÉT VƯỢT KHUNG";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrDate
            // 
            this.xrDate.BorderColor = System.Drawing.Color.DarkGray;
            this.xrDate.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrDate.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrDate.LocationFloat = new DevExpress.Utils.PointFloat(0F, 72F);
            this.xrDate.Name = "xrDate";
            this.xrDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrDate.SizeF = new System.Drawing.SizeF(1140F, 24F);
            this.xrDate.StylePriority.UseBorderColor = false;
            this.xrDate.StylePriority.UseBorders = false;
            this.xrDate.StylePriority.UseFont = false;
            this.xrDate.StylePriority.UseTextAlignment = false;
            this.xrDate.Text = "Tính đến ngày {0}/{1}/{2}";
            this.xrDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel9
            // 
            this.xrLabel9.BorderColor = System.Drawing.Color.DarkGray;
            this.xrLabel9.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel9.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 96F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(1140F, 10F);
            this.xrLabel9.StylePriority.UseBorderColor = false;
            this.xrLabel9.StylePriority.UseBorders = false;
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            this.xrTableCell24,
            this.xrTableCell23,
            this.xrTableCell22,
            this.xrTableCell18,
            this.xrTableCell15,
            this.xrTableCell13,
            this.xrTableCell9,
            this.xrTableCell8,
            this.xrTableCell7,
            this.xrTableCell6,
            this.xrTableCell4,
            this.xrTableCell3,
            this.xrTableCell2});
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
            this.xrTableCell241.Weight = 0.094209470756133937D;
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
            this.xrTableCell11.Weight = 0.29712080392146978D;
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
            this.xrTableCell242.Weight = 0.678388277796351D;
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
            this.xrTableCell1.Weight = 0.40105162522403331D;
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
            this.xrTableCell12.Text = "Chức vụ";
            this.xrTableCell12.Weight = 0.30049138386020163D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell24.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell24.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.StylePriority.UseBorderColor = false;
            this.xrTableCell24.StylePriority.UseBorders = false;
            this.xrTableCell24.StylePriority.UseFont = false;
            this.xrTableCell24.Text = "Quyết định gần nhất";
            this.xrTableCell24.Weight = 0.39317404954230395D;
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
            this.xrTableCell23.Text = "Ngày ký";
            this.xrTableCell23.Weight = 0.31684689588634335D;
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
            this.xrTableCell22.Text = "Ngày hưởng";
            this.xrTableCell22.Weight = 0.3168471754046065D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell18.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell18.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseBorderColor = false;
            this.xrTableCell18.StylePriority.UseBorders = false;
            this.xrTableCell18.StylePriority.UseFont = false;
            this.xrTableCell18.Text = "Thời gian hưởng";
            this.xrTableCell18.Weight = 0.31684703564547489D;
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
            this.xrTableCell15.Text = "Tên ngạch";
            this.xrTableCell15.Weight = 0.31684703564547489D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell13.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell13.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseBorderColor = false;
            this.xrTableCell13.StylePriority.UseBorders = false;
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.Text = "Mã số ngạch";
            this.xrTableCell13.Weight = 0.25142364984636312D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell9.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorderColor = false;
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.Text = "Bậc ngạch";
            this.xrTableCell9.Weight = 0.26777978080580178D;
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
            this.xrTableCell8.Text = "Hệ số";
            this.xrTableCell8.Weight = 0.26777944139076792D;
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
            this.xrTableCell7.Text = "Phụ cấp chức vụ";
            this.xrTableCell7.Weight = 0.37136635410388452D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell6.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseBorderColor = false;
            this.xrTableCell6.StylePriority.UseBorders = false;
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.Text = "Phụ cấp khác";
            this.xrTableCell6.Weight = 0.26232773715265556D;
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
            this.xrTableCell4.Text = "Khung bậc tối đa";
            this.xrTableCell4.Weight = 0.34955881839018677D;
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
            this.xrTableCell3.Text = "Thời hạn xét vượt khung";
            this.xrTableCell3.Weight = 0.44769346782866459D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorderColor = false;
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Text = "Ghi chú";
            this.xrTableCell2.Weight = 0.31684703564547489D;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrLabel1,
            this.lblReportDate,
            this.lblKyDongDau,
            this.lblKyHoTen,
            this.lblLapBang,
            this.lblThuTruong});
            this.ReportFooter.HeightF = 252.1667F;
            this.ReportFooter.Name = "ReportFooter";
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
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(423.6537F, 39.82006F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(180F, 25F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "NGƯỜI DUYỆT";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            this.lblReportDate.Text = "{0}, ngày {1} tháng {2} năm {3}";
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
            // lblLapBang
            // 
            this.lblLapBang.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.lblLapBang.LocationFloat = new DevExpress.Utils.PointFloat(24.38424F, 39.82006F);
            this.lblLapBang.Name = "lblLapBang";
            this.lblLapBang.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblLapBang.SizeF = new System.Drawing.SizeF(180F, 25F);
            this.lblLapBang.StylePriority.UseFont = false;
            this.lblLapBang.StylePriority.UseTextAlignment = false;
            this.lblLapBang.Text = "NGƯỜI LẬP";
            this.lblLapBang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblThuTruong
            // 
            this.lblThuTruong.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.lblThuTruong.LocationFloat = new DevExpress.Utils.PointFloat(821.3763F, 39.82006F);
            this.lblThuTruong.Name = "lblThuTruong";
            this.lblThuTruong.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblThuTruong.SizeF = new System.Drawing.SizeF(300F, 25F);
            this.lblThuTruong.StylePriority.UseFont = false;
            this.lblThuTruong.StylePriority.UseTextAlignment = false;
            this.lblThuTruong.Text = "THỦ TRƯỞNG CƠ QUAN, ĐƠN VỊ";
            this.lblThuTruong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.GroupHeader1.HeightF = 29.16667F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(3.973643E-05F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1140F, 28.95831F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UsePadding = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell27,
            this.xrTableCellGroupHead});
            this.xrTableRow2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
            this.xrTableRow2.StylePriority.UseFont = false;
            this.xrTableRow2.StylePriority.UsePadding = false;
            this.xrTableRow2.StylePriority.UseTextAlignment = false;
            this.xrTableRow2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.StylePriority.UseFont = false;
            this.xrTableCell27.StylePriority.UseTextAlignment = false;
            this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell27.Weight = 0.19199990123341795D;
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
            this.xrTableCellGroupHead.Weight = 11.32799434236102D;
            this.xrTableCellGroupHead.BeforePrint += new System.Drawing.Printing.PrintEventHandler(Group_BeforePrint);
            // 
            // rp_SalaryOutOfFrame
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
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}