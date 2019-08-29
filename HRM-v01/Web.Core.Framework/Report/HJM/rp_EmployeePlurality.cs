using System;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Web.Core;
using Web.Core.Object.Report;
using Web.Core.Framework;
using Web.Core.Framework.Adapter;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Summary description for rp_CCVC_BaoCaoDanhSachCanBoDuocKiemNhiem
    /// </summary>
    public class rp_EmployeePlurality : XtraReport
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
        private XRTableCell xrTableCellNgaySinh;
        private XRTableCell xrTableCellDonViDangCongTac;
        private XRTableCell xrTableCellChucVuHienTai;
        private XRTableCell xrTableCellDonViKiemNhiem;
        private XRTableCell xrTableCellNguoiKy;
        private XRLabel lblReportDate;
        private XRLabel lblKyDongDau;
        private XRTable tblPageHeader;
        private XRTableRow xrTableRow11;
        private XRTableCell xrTableCell241;
        private XRTableCell xrTableCell242;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell8;
        private XRTableCell xrTableCell4;
        private XRTableCell xrTableCell9;
        private XRTableCell xrTableCell5;
        private XRTableCell xrTableCell6;
        private XRTableCell xrTableCellSoThuTu;
        private XRTableCell xrTableCellChucVuKiemNhiem;
        private XRTableCell xrTableCellThoiHanKiemNhiem;
        private XRTableCell xrTableCellSoQuyetDinh;
        private XRTableCell xrTableCellChucVuNguoiKy;
        private XRTable xrTable4;
        private XRTableRow xrTableRow2;
        private XRTableCell xrTableCell10;
        private XRTableCell xrTableCell14;
        private XRTableRow xrTableRow3;
        private XRTableCell xrTableCellTenCoQuanDonVi;
        private XRTableCell xrTableCell17;
        private XRTableRow xrTableRow4;
        private XRTableCell xrTableCell20;
        private XRTableRow xrTableRow7;
        private XRTableCell xrTableCellTinhDenNgay;
        private XRTableRow xrTableRow8;
        private XRTableCell xrTableCell21;
        private XRTableCell xrTableCell3;
        private XRTableCell xrTableCell7;
        private XRLabel xrLabel2;
        private XRLabel xrLabel1;
        private XRTableCell xrTableCellMaCanBo;
        private XRTableCell xrTableCell11;
        private XRTableCell xrTableCellNgayQuyetDinh;
        private XRTableCell xrTableCell12;
        private XRTableCell xrTableCellNgayHieuLuc;
        private XRTableCell xrTableCell15;
        private XRTableCell xrTableCellCoQuanRaQuyetDinh;
        private XRTableCell xrTableCell22;
        private string CONST_BUSINESS_TYPE = "KiemNhiem";
        private GroupHeaderBand GroupHeader1;
        private GroupHeaderBand GroupHeader2;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell13;
        private XRTableCell xrTableCellGroupTenDonVi;
        private XRTable xrTable2;
        private XRTableRow xrTableRow5;
        private XRTableCell xrTableCell35;
        private XRTableCell xrTableCellGroupSoHieuCB;
        private XRTableCell xrTableCellGroupHoTen;
        private XRTableCell xrTableCell38;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
        private IContainer components = null;

        public rp_EmployeePlurality()
        {
            InitializeComponent();
        }
        int _stt = 1;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCellSoThuTu.Text = _stt.ToString();
            _stt++;
        }
        private void Group_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _stt = 1;
            xrTableCellSoThuTu.Text = _stt.ToString();
        }
        public void BindData(ReportFilter filter)
        {
            try
            {
                var control = new ReportController();
                xrTableCellTenCoQuanDonVi.Text = control.GetCompanyName(filter.SessionDepartment);
                xrTableCellTinhDenNgay.Text = string.Format(xrTableCellTinhDenNgay.Text, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
                var location = control.GetCityName(filter.SessionDepartment);
                lblReportDate.Text = string.Format(lblReportDate.Text, location, DateTime.Now.Day,
                    DateTime.Now.Month, DateTime.Now.Year);

                // get organization
                var organization = cat_DepartmentServices.GetByDepartments(filter.SessionDepartment);
                if (organization == null) return;
                // select form db               
                var departments = filter.SelectedDepartment;
                var arrDepartment = departments.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (var i = 0; i < arrDepartment.Length; i++)
                {
                    arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
                }
                var condition = filter.WhereClause;
                var table = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_ReportListBusinessHistory(string.Join(",", arrDepartment), CONST_BUSINESS_TYPE,condition));
                DataSource = table;

                //binding data
                xrTableCellNgaySinh.DataBindings.Add("Text", DataSource, "BirthDate", "{0:dd/MM/yyyy}");
                xrTableCellDonViDangCongTac.DataBindings.Add("Text", DataSource, "CurrentDepartment");
                xrTableCellChucVuHienTai.DataBindings.Add("Text", DataSource, "CurrentPosition");
                xrTableCellDonViKiemNhiem.DataBindings.Add("Text", DataSource, "DestinationDepartment");
                xrTableCellChucVuKiemNhiem.DataBindings.Add("Text", DataSource, "NewPosition");
                xrTableCellThoiHanKiemNhiem.DataBindings.Add("Text", DataSource, "ExpireDate", "{0:dd/MM/yyyy}");
                xrTableCellSoQuyetDinh.DataBindings.Add("Text", DataSource, "DecisionNumber");
                xrTableCellNgayQuyetDinh.DataBindings.Add("Text", DataSource, "DecisionDate", "{0:dd/MM/yyyy}");
                xrTableCellNgayHieuLuc.DataBindings.Add("Text", DataSource, "EffectiveDate", "{0:dd/MM/yyyy}");
                xrTableCellCoQuanRaQuyetDinh.DataBindings.Add("Text", DataSource, "SourceDepartment");
                xrTableCellNguoiKy.DataBindings.Add("Text", DataSource, "DecisionMaker");
                xrTableCellChucVuNguoiKy.DataBindings.Add("Text", DataSource, "DecisionPosition");
                GroupHeader1.GroupFields.AddRange(new[] { new GroupField("DepartmentId", XRColumnSortOrder.Ascending) });
                xrTableCellGroupTenDonVi.DataBindings.Add("Text", DataSource, "DepartmentName");
                GroupHeader2.GroupFields.AddRange(new[] { new GroupField("EmployeeCode", XRColumnSortOrder.Ascending) });
                xrTableCellGroupSoHieuCB.DataBindings.Add("Text", DataSource, "EmployeeCode");
                xrTableCellGroupHoTen.DataBindings.Add("Text", DataSource, "FullName");
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
            string resourceFileName = "rp_EmployeePlurality.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.tblDetail = new DevExpress.XtraReports.UI.XRTable();
            this.xrDetailRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellSoThuTu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellMaCanBo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHoVaTen = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNgaySinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDonViDangCongTac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChucVuHienTai = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDonViKiemNhiem = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChucVuKiemNhiem = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellThoiHanKiemNhiem = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellSoQuyetDinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNgayQuyetDinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNgayHieuLuc = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCoQuanRaQuyetDinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNguoiKy = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChucVuNguoiKy = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellTenCoQuanDonVi = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellTinhDenNgay = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.tblPageHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell241 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell242 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
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
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellGroupTenDonVi = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellGroupSoHieuCB = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellGroupHoTen = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
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
            this.xrTableCellMaCanBo,
            this.xrTableCellHoVaTen,
            this.xrTableCellNgaySinh,
            this.xrTableCellDonViDangCongTac,
            this.xrTableCellChucVuHienTai,
            this.xrTableCellDonViKiemNhiem,
            this.xrTableCellChucVuKiemNhiem,
            this.xrTableCellThoiHanKiemNhiem,
            this.xrTableCellSoQuyetDinh,
            this.xrTableCellNgayQuyetDinh,
            this.xrTableCellNgayHieuLuc,
            this.xrTableCellCoQuanRaQuyetDinh,
            this.xrTableCellNguoiKy,
            this.xrTableCellChucVuNguoiKy});
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
            this.xrTableCellSoThuTu.Weight = 0.0857540865445233D;
            this.xrTableCellSoThuTu.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrTableCellMaCanBo
            // 
            this.xrTableCellMaCanBo.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellMaCanBo.Name = "xrTableCellMaCanBo";
            this.xrTableCellMaCanBo.StylePriority.UseBorders = false;
            this.xrTableCellMaCanBo.StylePriority.UseTextAlignment = false;
            this.xrTableCellMaCanBo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellMaCanBo.Weight = 0.1914051162463763D;
            // 
            // xrTableCellHoVaTen
            // 
            this.xrTableCellHoVaTen.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHoVaTen.Name = "xrTableCellHoVaTen";
            this.xrTableCellHoVaTen.StylePriority.UseBorders = false;
            this.xrTableCellHoVaTen.StylePriority.UseTextAlignment = false;
            this.xrTableCellHoVaTen.Text = " ";
            this.xrTableCellHoVaTen.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellHoVaTen.Weight = 0.30611566561059722D;
            // 
            // xrTableCellNgaySinh
            // 
            this.xrTableCellNgaySinh.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNgaySinh.Name = "xrTableCellNgaySinh";
            this.xrTableCellNgaySinh.StylePriority.UseBorders = false;
            this.xrTableCellNgaySinh.StylePriority.UseTextAlignment = false;
            this.xrTableCellNgaySinh.Text = " ";
            this.xrTableCellNgaySinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNgaySinh.Weight = 0.26042026959543774D;
            // 
            // xrTableCellDonViDangCongTac
            // 
            this.xrTableCellDonViDangCongTac.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDonViDangCongTac.Name = "xrTableCellDonViDangCongTac";
            this.xrTableCellDonViDangCongTac.StylePriority.UseBorders = false;
            this.xrTableCellDonViDangCongTac.StylePriority.UseTextAlignment = false;
            this.xrTableCellDonViDangCongTac.Text = " ";
            this.xrTableCellDonViDangCongTac.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDonViDangCongTac.Weight = 0.30985357397555208D;
            // 
            // xrTableCellChucVuHienTai
            // 
            this.xrTableCellChucVuHienTai.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellChucVuHienTai.Name = "xrTableCellChucVuHienTai";
            this.xrTableCellChucVuHienTai.StylePriority.UseBorders = false;
            this.xrTableCellChucVuHienTai.StylePriority.UseTextAlignment = false;
            this.xrTableCellChucVuHienTai.Text = " ";
            this.xrTableCellChucVuHienTai.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellChucVuHienTai.Weight = 0.2435281317946042D;
            // 
            // xrTableCellDonViKiemNhiem
            // 
            this.xrTableCellDonViKiemNhiem.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDonViKiemNhiem.Name = "xrTableCellDonViKiemNhiem";
            this.xrTableCellDonViKiemNhiem.StylePriority.UseBorders = false;
            this.xrTableCellDonViKiemNhiem.StylePriority.UseTextAlignment = false;
            this.xrTableCellDonViKiemNhiem.Text = " ";
            this.xrTableCellDonViKiemNhiem.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDonViKiemNhiem.Weight = 0.275239635239082D;
            // 
            // xrTableCellChucVuKiemNhiem
            // 
            this.xrTableCellChucVuKiemNhiem.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellChucVuKiemNhiem.Name = "xrTableCellChucVuKiemNhiem";
            this.xrTableCellChucVuKiemNhiem.StylePriority.UseBorders = false;
            this.xrTableCellChucVuKiemNhiem.StylePriority.UseTextAlignment = false;
            this.xrTableCellChucVuKiemNhiem.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellChucVuKiemNhiem.Weight = 0.28249740979280796D;
            // 
            // xrTableCellThoiHanKiemNhiem
            // 
            this.xrTableCellThoiHanKiemNhiem.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellThoiHanKiemNhiem.Name = "xrTableCellThoiHanKiemNhiem";
            this.xrTableCellThoiHanKiemNhiem.StylePriority.UseBorders = false;
            this.xrTableCellThoiHanKiemNhiem.StylePriority.UseTextAlignment = false;
            this.xrTableCellThoiHanKiemNhiem.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellThoiHanKiemNhiem.Weight = 0.22644439193522747D;
            // 
            // xrTableCellSoQuyetDinh
            // 
            this.xrTableCellSoQuyetDinh.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSoQuyetDinh.Name = "xrTableCellSoQuyetDinh";
            this.xrTableCellSoQuyetDinh.StylePriority.UseBorders = false;
            this.xrTableCellSoQuyetDinh.StylePriority.UseTextAlignment = false;
            this.xrTableCellSoQuyetDinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSoQuyetDinh.Weight = 0.21974453115532444D;
            // 
            // xrTableCellNgayQuyetDinh
            // 
            this.xrTableCellNgayQuyetDinh.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNgayQuyetDinh.Name = "xrTableCellNgayQuyetDinh";
            this.xrTableCellNgayQuyetDinh.StylePriority.UseBorders = false;
            this.xrTableCellNgayQuyetDinh.StylePriority.UseTextAlignment = false;
            this.xrTableCellNgayQuyetDinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNgayQuyetDinh.Weight = 0.2225363871319001D;
            // 
            // xrTableCellNgayHieuLuc
            // 
            this.xrTableCellNgayHieuLuc.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNgayHieuLuc.Name = "xrTableCellNgayHieuLuc";
            this.xrTableCellNgayHieuLuc.StylePriority.UseBorders = false;
            this.xrTableCellNgayHieuLuc.StylePriority.UseTextAlignment = false;
            this.xrTableCellNgayHieuLuc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNgayHieuLuc.Weight = 0.27558786594166912D;
            // 
            // xrTableCellCoQuanRaQuyetDinh
            // 
            this.xrTableCellCoQuanRaQuyetDinh.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCoQuanRaQuyetDinh.Name = "xrTableCellCoQuanRaQuyetDinh";
            this.xrTableCellCoQuanRaQuyetDinh.StylePriority.UseBorders = false;
            this.xrTableCellCoQuanRaQuyetDinh.StylePriority.UseTextAlignment = false;
            this.xrTableCellCoQuanRaQuyetDinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellCoQuanRaQuyetDinh.Weight = 0.27558786594166912D;
            // 
            // xrTableCellNguoiKy
            // 
            this.xrTableCellNguoiKy.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNguoiKy.Name = "xrTableCellNguoiKy";
            this.xrTableCellNguoiKy.StylePriority.UseBorders = false;
            this.xrTableCellNguoiKy.StylePriority.UseTextAlignment = false;
            this.xrTableCellNguoiKy.Text = " ";
            this.xrTableCellNguoiKy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNguoiKy.Weight = 0.35680167339269409D;
            // 
            // xrTableCellChucVuNguoiKy
            // 
            this.xrTableCellChucVuNguoiKy.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellChucVuNguoiKy.Name = "xrTableCellChucVuNguoiKy";
            this.xrTableCellChucVuNguoiKy.StylePriority.UseBorders = false;
            this.xrTableCellChucVuNguoiKy.StylePriority.UseTextAlignment = false;
            this.xrTableCellChucVuNguoiKy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellChucVuNguoiKy.Weight = 0.32263084207970771D;
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
            this.xrTableCellTenCoQuanDonVi,
            this.xrTableCell17});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 0.96D;
            // 
            // xrTableCellTenCoQuanDonVi
            // 
            this.xrTableCellTenCoQuanDonVi.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCellTenCoQuanDonVi.Name = "xrTableCellTenCoQuanDonVi";
            this.xrTableCellTenCoQuanDonVi.StylePriority.UseFont = false;
            this.xrTableCellTenCoQuanDonVi.Weight = 0.53785336143092111D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.Weight = 2.4621466385690791D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 0.96000000000000008D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseFont = false;
            this.xrTableCell20.Text = "BÁO CÁO DANH SÁCH CÁN BỘ ĐƯỢC KIÊM NHIỆM";
            this.xrTableCell20.Weight = 3D;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellTinhDenNgay});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 0.96000000000000008D;
            // 
            // xrTableCellTinhDenNgay
            // 
            this.xrTableCellTinhDenNgay.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCellTinhDenNgay.Name = "xrTableCellTinhDenNgay";
            this.xrTableCellTinhDenNgay.StylePriority.UseFont = false;
            this.xrTableCellTinhDenNgay.Text = "Tính đến ngày {0}/{1}/{2}";
            this.xrTableCellTinhDenNgay.Weight = 3D;
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
            this.PageHeader.HeightF = 40.41316F;
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
            this.xrTableCell2,
            this.xrTableCell1,
            this.xrTableCell8,
            this.xrTableCell4,
            this.xrTableCell9,
            this.xrTableCell3,
            this.xrTableCell5,
            this.xrTableCell12,
            this.xrTableCell15,
            this.xrTableCell22,
            this.xrTableCell7,
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
            this.xrTableCell241.Weight = 0.12861955224491464D;
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
            this.xrTableCell11.Weight = 0.28708170032876196D;
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
            this.xrTableCell242.Weight = 0.45913216593182421D;
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
            this.xrTableCell2.Text = "Sinh Ngày";
            this.xrTableCell2.Weight = 0.39059532882299D;
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
            this.xrTableCell1.Weight = 0.46473862616227596D;
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
            this.xrTableCell8.Text = "Chức vụ hiện tại";
            this.xrTableCell8.Weight = 0.36525943209661615D;
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
            this.xrTableCell4.Text = "Đơn vị kiêm nhiệm";
            this.xrTableCell4.Weight = 0.4128220093110756D;
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
            this.xrTableCell9.Text = "Chức vụ kiêm nhiệm";
            this.xrTableCell9.Weight = 0.42370773321583022D;
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
            this.xrTableCell3.Text = "Thời hạn kiêm nhiệm";
            this.xrTableCell3.Weight = 0.33963600112275355D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell5.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBorderColor = false;
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.Text = "Số quyết định";
            this.xrTableCell5.Weight = 0.32958734707282289D;
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
            this.xrTableCell12.Text = "Ngày quyết định";
            this.xrTableCell12.Weight = 0.3337745087049267D;
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
            this.xrTableCell15.Text = "Ngày hiệu lực";
            this.xrTableCell15.Weight = 0.4133445598815364D;
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
            this.xrTableCell22.Text = "Cơ quan ra quyết định";
            this.xrTableCell22.Weight = 0.4133445598815364D;
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
            this.xrTableCell7.Text = "Người ký";
            this.xrTableCell7.Weight = 0.53515400502172783D;
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
            this.xrTableCell6.Text = "Chức vụ người ký";
            this.xrTableCell6.Weight = 0.48390233795099641D;
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
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(423.6537F, 46.02269F);
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
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(423.6537F, 21.0227F);
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
            this.lblReportDate.LocationFloat = new DevExpress.Utils.PointFloat(821.3763F, 21.0227F);
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
            this.lblKyDongDau.LocationFloat = new DevExpress.Utils.PointFloat(821.3763F, 61.02269F);
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
            this.lblKyHoTen.LocationFloat = new DevExpress.Utils.PointFloat(24.38423F, 46.54176F);
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
            this.lblLapBang.LocationFloat = new DevExpress.Utils.PointFloat(24.38424F, 21.0227F);
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
            this.lblThuTruong.LocationFloat = new DevExpress.Utils.PointFloat(821.3763F, 36.0227F);
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
            this.xrTable1});
            this.GroupHeader1.HeightF = 25F;
            this.GroupHeader1.Level = 1;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.GroupHeader2.HeightF = 26.04167F;
            this.GroupHeader2.Name = "GroupHeader2";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1140F, 25F);
            this.xrTable1.StylePriority.UseBorders = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCellGroupTenDonVi});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseBorders = false;
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell13.Weight = 0.0857540865445233D;
            // 
            // xrTableCellGroupTenDonVi
            // 
            this.xrTableCellGroupTenDonVi.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellGroupTenDonVi.Name = "xrTableCellGroupTenDonVi";
            this.xrTableCellGroupTenDonVi.StylePriority.UseBorders = false;
            this.xrTableCellGroupTenDonVi.StylePriority.UseTextAlignment = false;
            this.xrTableCellGroupTenDonVi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellGroupTenDonVi.Weight = 3.7683940752339455D;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0.5F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1140F, 25F);
            this.xrTable2.StylePriority.UseBorders = false;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell35,
            this.xrTableCellGroupSoHieuCB,
            this.xrTableCellGroupHoTen,
            this.xrTableCell38});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.StylePriority.UseBorders = false;
            this.xrTableCell35.StylePriority.UseFont = false;
            this.xrTableCell35.StylePriority.UseTextAlignment = false;
            this.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell35.Weight = 0.0857540865445233D;
            // 
            // xrTableCellGroupSoHieuCB
            // 
            this.xrTableCellGroupSoHieuCB.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellGroupSoHieuCB.Name = "xrTableCellGroupSoHieuCB";
            this.xrTableCellGroupSoHieuCB.StylePriority.UseBorders = false;
            this.xrTableCellGroupSoHieuCB.StylePriority.UseTextAlignment = false;
            this.xrTableCellGroupSoHieuCB.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellGroupSoHieuCB.Weight = 0.1914051162463763D;
            // 
            // xrTableCellGroupHoTen
            // 
            this.xrTableCellGroupHoTen.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellGroupHoTen.Name = "xrTableCellGroupHoTen";
            this.xrTableCellGroupHoTen.StylePriority.UseBorders = false;
            this.xrTableCellGroupHoTen.StylePriority.UseTextAlignment = false;
            this.xrTableCellGroupHoTen.Text = " ";
            this.xrTableCellGroupHoTen.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellGroupHoTen.Weight = 0.30611566561059722D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.StylePriority.UseBorders = false;
            this.xrTableCell38.StylePriority.UseTextAlignment = false;
            this.xrTableCell38.Text = " ";
            this.xrTableCell38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell38.Weight = 3.2708737143668905D;
            // 
            // rp_EmployeePlurality
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter,
            this.GroupHeader1,
            this.GroupHeader2});
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
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}