using System;
using DevExpress.XtraReports.UI;
using Web.Core;
using Web.Core.Service.Catalog;
using Web.Core.Object.Report;
using Web.Core.Framework;
using Web.Core.Framework.Adapter;

namespace Web.Core.Framework.Report
{

    /// <summary>
    /// Summary description for rp_EmployeeAppointment
    /// </summary>
    public class rp_EmployeeAppointment : DevExpress.XtraReports.UI.XtraReport
    {
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private ReportHeaderBand ReportHeader;
        private PageHeaderBand PageHeader;
        private ReportFooterBand ReportFooter;
        private XRLabel xrl_TitleBC;
        private XRLabel xrl_TenCongTy;
        private XRLabel lblNguoiDuyet;
        private XRLabel lblChucVuKyHoTen1;
        private XRLabel lblReportDate;
        private XRLabel lblLanhDaoCoQuan;
        private XRLabel lblNguoiLap;
        private GroupHeaderBand GroupHeader1;
        private PageFooterBand PageFooter;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCellSoThuTu;
        private XRTableCell xrTableCellMaCanBo;
        private XRTableCell xrTableCellHoTen;
        private XRTableCell xrTableCellNgaySinh;
        private XRTableCell xrTableCellDonViCongTac;
        private XRTableCell xrTableCellChucVuNguoiKy;
        private XRPageInfo xrPageInfo1;
        private XRTableCell xrTableCellThoiHanBoNhiem;
        private XRTableCell xrTableCellSoQuyetDinh;
        private XRTableCell xrTableCellNgayQuyetDinh;
        private XRTableCell xrTableCellNgayHieuLuc;
        private XRTableCell xrTableCellChucVuBoNhiem;
        private XRTableCell xrTableCellNguoiKy;
        private XRTableCell xrTableCellCoQuanBoNhiem;
        private XRLabel xrToDate;
        private XRTable xrTable4;
        private XRTableRow xrTableRow4;
        private XRTableCell xrt_stt;
        private XRTableCell xrt_macb;
        private XRTableCell xrt_hoten;
        private XRTableCell xrt_ngaysinh;
        private XRTableCell xrt_donvicongtac;
        private XRTableCell xrt_soquyetdinh;
        private XRTableCell xrt_ngayquyetdinh;
        private XRTableCell xrt_ngayhieuluc;
        private XRTableCell xrt_chucvubonhiem;
        private XRTableCell xrt_thoihanbonhiem;
        private XRTableCell xrt_coquanbonhiem;
        private XRTableCell xrt_nguoiky;
        private XRTableCell xrt_chucvunguoiky;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrGroup;
        private XRTableCell xrTableCellGroupHead;
        private XRLabel xrl_TenCoQuanDonVi;
        private XRLabel xrLabel3;
        private XRLabel lblChucVuKyHoTen2;
        private XRLabel lblKyHoTen;
        private GroupHeaderBand GroupHeader2;
        private XRTable xrTable3;
        private XRTableRow xrTableRow3;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCellGroupSoHieuCB;
        private XRTableCell xrTableCellGroupHoTen;
        private XRTableCell xrTableCell4;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public rp_EmployeeAppointment()
        {
            InitializeComponent();
        }
        int _stt;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _stt++;
            xrt_stt.Text = _stt.ToString();
        }
        private void Group_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _stt = 0;
            xrt_stt.Text = _stt.ToString();
        }
        public void BindData(ReportFilter filter)
        {

            try
            {
                var control = new ReportController();
                xrl_TenCoQuanDonVi.Text = control.GetCompanyName(filter.SessionDepartment);
                xrLabel3.Text = control.GetCompanyAddress(filter.SessionDepartment);
                var location = control.GetCityName(filter.SessionDepartment);
                lblReportDate.Text = string.Format(lblReportDate.Text, location, DateTime.Now.Day,
                    DateTime.Now.Month, DateTime.Now.Year);

               // var toDate = new DateTime(filter.Year, filter.StartMonth, DateTime.Now.Day);
                var toDate = filter.ReportedDate;
                xrToDate.Text = string.Format(xrToDate.Text, toDate.Day, toDate.Month, toDate.Year);
                // get organization
                var organization =  cat_DepartmentServices.GetByDepartments(filter.SessionDepartment);
                if (organization == null) return;
                // select form db               
                var arrOrgCode = string.IsNullOrEmpty(filter.SelectedDepartment)
                    ? new string[] { }
                    : filter.SelectedDepartment.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (var i = 0; i < arrOrgCode.Length; i++ )
                {
                    arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
                }
                var condition = filter.WhereClause;
                var table = SQLHelper.ExecuteTable(
                    SQLManagementAdapter.GetStore_DanhSachCanBoDuocBoNhiem(string.Join(",", arrOrgCode),
                        condition));
                DataSource = table;

                //binding detail
                xrt_ngaysinh.DataBindings.Add("Text", DataSource, "NgaySinh", "{0:dd/MM/yyyy}");
                xrt_donvicongtac.DataBindings.Add("Text", DataSource, "DonViCongTac");
                xrt_soquyetdinh.DataBindings.Add("Text", DataSource, "SoQuyetDinh");
                xrt_ngayquyetdinh.DataBindings.Add("Text", DataSource, "NgayQuyetDinh", "{0:dd/MM/yyyy}");
                xrt_ngayhieuluc.DataBindings.Add("Text", DataSource, "NgayHieuLuc", "{0:dd/MM/yyyy}");
                xrt_chucvubonhiem.DataBindings.Add("Text", DataSource, "ChucVuBoNhiem");
                xrt_thoihanbonhiem.DataBindings.Add("Text", DataSource, "ThoiHanBoNhiem", "{0:dd/MM/yyyy}");
                xrt_coquanbonhiem.DataBindings.Add("Text", DataSource, "CoQuanBoNhiem");
                xrt_nguoiky.DataBindings.Add("Text", DataSource, "NguoiKy");
                xrt_chucvunguoiky.DataBindings.Add("Text", DataSource, "MakerPosition");
                GroupHeader1.GroupFields.AddRange(new[] { new GroupField("MaDonVi", XRColumnSortOrder.Ascending)});
                xrTableCellGroupHead.DataBindings.Add("Text", DataSource, "GROUP");
                GroupHeader2.GroupFields.AddRange(new[] { new GroupField("MaCanBo", XRColumnSortOrder.Ascending) });
                xrTableCellGroupSoHieuCB.DataBindings.Add("Text", DataSource, "MaCanBo");
                xrTableCellGroupHoTen.DataBindings.Add("Text", DataSource, "HoTen");
            }
            catch
            {
            }
        }

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
            string resourceFileName = "rp_EmployeeAppointment.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrt_stt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_macb = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_hoten = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_ngaysinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_donvicongtac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_soquyetdinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_ngayquyetdinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_ngayhieuluc = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_chucvubonhiem = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_thoihanbonhiem = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_coquanbonhiem = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_nguoiky = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_chucvunguoiky = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrl_TenCoQuanDonVi = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrToDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TitleBC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TenCongTy = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellSoThuTu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellMaCanBo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHoTen = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNgaySinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDonViCongTac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellSoQuyetDinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNgayQuyetDinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNgayHieuLuc = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChucVuBoNhiem = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellThoiHanBoNhiem = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCoQuanBoNhiem = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNguoiKy = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChucVuNguoiKy = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblKyHoTen = new DevExpress.XtraReports.UI.XRLabel();
            this.lblChucVuKyHoTen2 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblNguoiDuyet = new DevExpress.XtraReports.UI.XRLabel();
            this.lblChucVuKyHoTen1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblLanhDaoCoQuan = new DevExpress.XtraReports.UI.XRLabel();
            this.lblNguoiLap = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrGroup = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellGroupHead = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellGroupSoHieuCB = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellGroupHoTen = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
            this.Detail.HeightF = 28.95831F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable4
            // 
            this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable4.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(3.973643E-05F, 0F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrTable4.SizeF = new System.Drawing.SizeF(1152F, 28.95831F);
            this.xrTable4.StylePriority.UseBorders = false;
            this.xrTable4.StylePriority.UseFont = false;
            this.xrTable4.StylePriority.UsePadding = false;
            this.xrTable4.StylePriority.UseTextAlignment = false;
            this.xrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrt_stt,
            this.xrt_macb,
            this.xrt_hoten,
            this.xrt_ngaysinh,
            this.xrt_donvicongtac,
            this.xrt_soquyetdinh,
            this.xrt_ngayquyetdinh,
            this.xrt_ngayhieuluc,
            this.xrt_chucvubonhiem,
            this.xrt_thoihanbonhiem,
            this.xrt_coquanbonhiem,
            this.xrt_nguoiky,
            this.xrt_chucvunguoiky});
            this.xrTableRow4.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
            this.xrTableRow4.StylePriority.UseFont = false;
            this.xrTableRow4.StylePriority.UsePadding = false;
            this.xrTableRow4.StylePriority.UseTextAlignment = false;
            this.xrTableRow4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow4.Weight = 1D;
            // 
            // xrt_stt
            // 
            this.xrt_stt.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrt_stt.Name = "xrt_stt";
            this.xrt_stt.StylePriority.UseFont = false;
            this.xrt_stt.StylePriority.UseTextAlignment = false;
            this.xrt_stt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_stt.Weight = 0.2847222097900875D;
            this.xrt_stt.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrt_macb
            // 
            this.xrt_macb.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrt_macb.Name = "xrt_macb";
            this.xrt_macb.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrt_macb.StylePriority.UseFont = false;
            this.xrt_macb.StylePriority.UsePadding = false;
            this.xrt_macb.StylePriority.UseTextAlignment = false;
            this.xrt_macb.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_macb.Weight = 0.796296122540843D;
            // 
            // xrt_hoten
            // 
            this.xrt_hoten.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrt_hoten.Name = "xrt_hoten";
            this.xrt_hoten.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrt_hoten.StylePriority.UseFont = false;
            this.xrt_hoten.StylePriority.UsePadding = false;
            this.xrt_hoten.StylePriority.UseTextAlignment = false;
            this.xrt_hoten.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_hoten.Weight = 1.04629643179116D;
            // 
            // xrt_ngaysinh
            // 
            this.xrt_ngaysinh.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrt_ngaysinh.Name = "xrt_ngaysinh";
            this.xrt_ngaysinh.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrt_ngaysinh.StylePriority.UseFont = false;
            this.xrt_ngaysinh.StylePriority.UsePadding = false;
            this.xrt_ngaysinh.StylePriority.UseTextAlignment = false;
            this.xrt_ngaysinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_ngaysinh.Weight = 0.682117930927601D;
            // 
            // xrt_donvicongtac
            // 
            this.xrt_donvicongtac.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrt_donvicongtac.Name = "xrt_donvicongtac";
            this.xrt_donvicongtac.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrt_donvicongtac.StylePriority.UseFont = false;
            this.xrt_donvicongtac.StylePriority.UsePadding = false;
            this.xrt_donvicongtac.StylePriority.UseTextAlignment = false;
            this.xrt_donvicongtac.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_donvicongtac.Weight = 0.87546408556302313D;
            // 
            // xrt_soquyetdinh
            // 
            this.xrt_soquyetdinh.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrt_soquyetdinh.Name = "xrt_soquyetdinh";
            this.xrt_soquyetdinh.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrt_soquyetdinh.StylePriority.UseFont = false;
            this.xrt_soquyetdinh.StylePriority.UsePadding = false;
            this.xrt_soquyetdinh.StylePriority.UseTextAlignment = false;
            this.xrt_soquyetdinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_soquyetdinh.Weight = 0.71805554750184619D;
            // 
            // xrt_ngayquyetdinh
            // 
            this.xrt_ngayquyetdinh.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrt_ngayquyetdinh.Name = "xrt_ngayquyetdinh";
            this.xrt_ngayquyetdinh.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrt_ngayquyetdinh.StylePriority.UseFont = false;
            this.xrt_ngayquyetdinh.StylePriority.UsePadding = false;
            this.xrt_ngayquyetdinh.StylePriority.UseTextAlignment = false;
            this.xrt_ngayquyetdinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_ngayquyetdinh.Weight = 0.98992538371976357D;
            // 
            // xrt_ngayhieuluc
            // 
            this.xrt_ngayhieuluc.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrt_ngayhieuluc.Name = "xrt_ngayhieuluc";
            this.xrt_ngayhieuluc.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrt_ngayhieuluc.StylePriority.UseFont = false;
            this.xrt_ngayhieuluc.StylePriority.UsePadding = false;
            this.xrt_ngayhieuluc.StylePriority.UseTextAlignment = false;
            this.xrt_ngayhieuluc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_ngayhieuluc.Weight = 0.8954451357420341D;
            // 
            // xrt_chucvubonhiem
            // 
            this.xrt_chucvubonhiem.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrt_chucvubonhiem.Name = "xrt_chucvubonhiem";
            this.xrt_chucvubonhiem.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrt_chucvubonhiem.StylePriority.UseFont = false;
            this.xrt_chucvubonhiem.StylePriority.UsePadding = false;
            this.xrt_chucvubonhiem.StylePriority.UseTextAlignment = false;
            this.xrt_chucvubonhiem.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_chucvubonhiem.Weight = 1.114581843410513D;
            // 
            // xrt_thoihanbonhiem
            // 
            this.xrt_thoihanbonhiem.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrt_thoihanbonhiem.Name = "xrt_thoihanbonhiem";
            this.xrt_thoihanbonhiem.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrt_thoihanbonhiem.StylePriority.UseFont = false;
            this.xrt_thoihanbonhiem.StylePriority.UsePadding = false;
            this.xrt_thoihanbonhiem.StylePriority.UseTextAlignment = false;
            this.xrt_thoihanbonhiem.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_thoihanbonhiem.Weight = 0.97139783394445423D;
            // 
            // xrt_coquanbonhiem
            // 
            this.xrt_coquanbonhiem.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrt_coquanbonhiem.Name = "xrt_coquanbonhiem";
            this.xrt_coquanbonhiem.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrt_coquanbonhiem.StylePriority.UseFont = false;
            this.xrt_coquanbonhiem.StylePriority.UsePadding = false;
            this.xrt_coquanbonhiem.StylePriority.UseTextAlignment = false;
            this.xrt_coquanbonhiem.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_coquanbonhiem.Weight = 1.2738055455785837D;
            // 
            // xrt_nguoiky
            // 
            this.xrt_nguoiky.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrt_nguoiky.Name = "xrt_nguoiky";
            this.xrt_nguoiky.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrt_nguoiky.StylePriority.UseFont = false;
            this.xrt_nguoiky.StylePriority.UsePadding = false;
            this.xrt_nguoiky.StylePriority.UseTextAlignment = false;
            this.xrt_nguoiky.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_nguoiky.Weight = 0.94827407726016322D;
            // 
            // xrt_chucvunguoiky
            // 
            this.xrt_chucvunguoiky.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrt_chucvunguoiky.Name = "xrt_chucvunguoiky";
            this.xrt_chucvunguoiky.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrt_chucvunguoiky.StylePriority.UseFont = false;
            this.xrt_chucvunguoiky.StylePriority.UsePadding = false;
            this.xrt_chucvunguoiky.StylePriority.UseTextAlignment = false;
            this.xrt_chucvunguoiky.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_chucvunguoiky.Weight = 0.92361019444748926D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 31F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 30F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrl_TenCoQuanDonVi,
            this.xrLabel3,
            this.xrToDate,
            this.xrl_TitleBC,
            this.xrl_TenCongTy});
            this.ReportHeader.HeightF = 143.0833F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrl_TenCoQuanDonVi
            // 
            this.xrl_TenCoQuanDonVi.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrl_TenCoQuanDonVi.LocationFloat = new DevExpress.Utils.PointFloat(0F, 28.20834F);
            this.xrl_TenCoQuanDonVi.Name = "xrl_TenCoQuanDonVi";
            this.xrl_TenCoQuanDonVi.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_TenCoQuanDonVi.SizeF = new System.Drawing.SizeF(340.3648F, 23F);
            this.xrl_TenCoQuanDonVi.StylePriority.UseFont = false;
            this.xrl_TenCoQuanDonVi.StylePriority.UseTextAlignment = false;
            this.xrl_TenCoQuanDonVi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 51.20834F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(340.3648F, 23F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrToDate
            // 
            this.xrToDate.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrToDate.LocationFloat = new DevExpress.Utils.PointFloat(0F, 113.335F);
            this.xrToDate.Name = "xrToDate";
            this.xrToDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrToDate.SizeF = new System.Drawing.SizeF(1152F, 28.20834F);
            this.xrToDate.StylePriority.UseFont = false;
            this.xrToDate.StylePriority.UseTextAlignment = false;
            this.xrToDate.Text = "(Tính đến ngày {0}/{1}/{2})";
            this.xrToDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrl_TitleBC
            // 
            this.xrl_TitleBC.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrl_TitleBC.LocationFloat = new DevExpress.Utils.PointFloat(0F, 84.375F);
            this.xrl_TitleBC.Name = "xrl_TitleBC";
            this.xrl_TitleBC.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_TitleBC.SizeF = new System.Drawing.SizeF(1152F, 28.96F);
            this.xrl_TitleBC.StylePriority.UseFont = false;
            this.xrl_TitleBC.StylePriority.UseTextAlignment = false;
            this.xrl_TitleBC.Text = "BÁO CÁO DANH SÁCH CÁN BỘ ĐƯỢC BỔ NHIỆM";
            this.xrl_TitleBC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrl_TenCongTy
            // 
            this.xrl_TenCongTy.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrl_TenCongTy.LocationFloat = new DevExpress.Utils.PointFloat(6.357829E-05F, 0F);
            this.xrl_TenCongTy.Name = "xrl_TenCongTy";
            this.xrl_TenCongTy.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_TenCongTy.SizeF = new System.Drawing.SizeF(340.3647F, 28.20834F);
            this.xrl_TenCongTy.StylePriority.UseFont = false;
            this.xrl_TenCongTy.StylePriority.UseTextAlignment = false;
            this.xrl_TenCongTy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.PageHeader.HeightF = 38.33331F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1152F, 38.33331F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellSoThuTu,
            this.xrTableCellMaCanBo,
            this.xrTableCellHoTen,
            this.xrTableCellNgaySinh,
            this.xrTableCellDonViCongTac,
            this.xrTableCellSoQuyetDinh,
            this.xrTableCellNgayQuyetDinh,
            this.xrTableCellNgayHieuLuc,
            this.xrTableCellChucVuBoNhiem,
            this.xrTableCellThoiHanBoNhiem,
            this.xrTableCellCoQuanBoNhiem,
            this.xrTableCellNguoiKy,
            this.xrTableCellChucVuNguoiKy});
            this.xrTableRow1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
            this.xrTableRow1.StylePriority.UseFont = false;
            this.xrTableRow1.StylePriority.UsePadding = false;
            this.xrTableRow1.StylePriority.UseTextAlignment = false;
            this.xrTableRow1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCellSoThuTu
            // 
            this.xrTableCellSoThuTu.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellSoThuTu.Name = "xrTableCellSoThuTu";
            this.xrTableCellSoThuTu.StylePriority.UseFont = false;
            this.xrTableCellSoThuTu.StylePriority.UseTextAlignment = false;
            this.xrTableCellSoThuTu.Text = "STT";
            this.xrTableCellSoThuTu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSoThuTu.Weight = 0.28472226701051789D;
            // 
            // xrTableCellMaCanBo
            // 
            this.xrTableCellMaCanBo.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellMaCanBo.Name = "xrTableCellMaCanBo";
            this.xrTableCellMaCanBo.StylePriority.UseFont = false;
            this.xrTableCellMaCanBo.StylePriority.UseTextAlignment = false;
            this.xrTableCellMaCanBo.Text = "Số hiệu CBCC";
            this.xrTableCellMaCanBo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellMaCanBo.Weight = 0.7962963900741058D;
            // 
            // xrTableCellHoTen
            // 
            this.xrTableCellHoTen.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellHoTen.Name = "xrTableCellHoTen";
            this.xrTableCellHoTen.StylePriority.UseFont = false;
            this.xrTableCellHoTen.StylePriority.UseTextAlignment = false;
            this.xrTableCellHoTen.Text = "Họ và tên ";
            this.xrTableCellHoTen.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellHoTen.Weight = 1.0462960771342238D;
            // 
            // xrTableCellNgaySinh
            // 
            this.xrTableCellNgaySinh.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellNgaySinh.Name = "xrTableCellNgaySinh";
            this.xrTableCellNgaySinh.StylePriority.UseFont = false;
            this.xrTableCellNgaySinh.StylePriority.UseTextAlignment = false;
            this.xrTableCellNgaySinh.Text = "Ngày sinh";
            this.xrTableCellNgaySinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNgaySinh.Weight = 0.68211855655834219D;
            // 
            // xrTableCellDonViCongTac
            // 
            this.xrTableCellDonViCongTac.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellDonViCongTac.Name = "xrTableCellDonViCongTac";
            this.xrTableCellDonViCongTac.StylePriority.UseFont = false;
            this.xrTableCellDonViCongTac.StylePriority.UseTextAlignment = false;
            this.xrTableCellDonViCongTac.Text = "Đơn vị công tác";
            this.xrTableCellDonViCongTac.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDonViCongTac.Weight = 0.87546301991059139D;
            // 
            // xrTableCellSoQuyetDinh
            // 
            this.xrTableCellSoQuyetDinh.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellSoQuyetDinh.Name = "xrTableCellSoQuyetDinh";
            this.xrTableCellSoQuyetDinh.StylePriority.UseFont = false;
            this.xrTableCellSoQuyetDinh.StylePriority.UseTextAlignment = false;
            this.xrTableCellSoQuyetDinh.Text = "Số quyết định";
            this.xrTableCellSoQuyetDinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSoQuyetDinh.Weight = 0.71874894082546281D;
            // 
            // xrTableCellNgayQuyetDinh
            // 
            this.xrTableCellNgayQuyetDinh.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellNgayQuyetDinh.Name = "xrTableCellNgayQuyetDinh";
            this.xrTableCellNgayQuyetDinh.StylePriority.UseFont = false;
            this.xrTableCellNgayQuyetDinh.StylePriority.UseTextAlignment = false;
            this.xrTableCellNgayQuyetDinh.Text = "Ngày quyết định";
            this.xrTableCellNgayQuyetDinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNgayQuyetDinh.Weight = 0.98992525255361452D;
            // 
            // xrTableCellNgayHieuLuc
            // 
            this.xrTableCellNgayHieuLuc.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellNgayHieuLuc.Name = "xrTableCellNgayHieuLuc";
            this.xrTableCellNgayHieuLuc.StylePriority.UseFont = false;
            this.xrTableCellNgayHieuLuc.StylePriority.UseTextAlignment = false;
            this.xrTableCellNgayHieuLuc.Text = "Ngày hiệu lực";
            this.xrTableCellNgayHieuLuc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNgayHieuLuc.Weight = 0.89544500835033136D;
            // 
            // xrTableCellChucVuBoNhiem
            // 
            this.xrTableCellChucVuBoNhiem.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellChucVuBoNhiem.Name = "xrTableCellChucVuBoNhiem";
            this.xrTableCellChucVuBoNhiem.StylePriority.UseFont = false;
            this.xrTableCellChucVuBoNhiem.StylePriority.UseTextAlignment = false;
            this.xrTableCellChucVuBoNhiem.Text = "Chức vụ bổ nhiệm";
            this.xrTableCellChucVuBoNhiem.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellChucVuBoNhiem.Weight = 1.1145818762464739D;
            // 
            // xrTableCellThoiHanBoNhiem
            // 
            this.xrTableCellThoiHanBoNhiem.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellThoiHanBoNhiem.Name = "xrTableCellThoiHanBoNhiem";
            this.xrTableCellThoiHanBoNhiem.StylePriority.UseFont = false;
            this.xrTableCellThoiHanBoNhiem.StylePriority.UseTextAlignment = false;
            this.xrTableCellThoiHanBoNhiem.Text = "Thời hạn bổ nhiệm";
            this.xrTableCellThoiHanBoNhiem.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellThoiHanBoNhiem.Weight = 0.97139772001338776D;
            // 
            // xrTableCellCoQuanBoNhiem
            // 
            this.xrTableCellCoQuanBoNhiem.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellCoQuanBoNhiem.Name = "xrTableCellCoQuanBoNhiem";
            this.xrTableCellCoQuanBoNhiem.StylePriority.UseFont = false;
            this.xrTableCellCoQuanBoNhiem.StylePriority.UseTextAlignment = false;
            this.xrTableCellCoQuanBoNhiem.Text = "Cơ quan bổ nhiệm";
            this.xrTableCellCoQuanBoNhiem.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellCoQuanBoNhiem.Weight = 1.2738047741508491D;
            // 
            // xrTableCellNguoiKy
            // 
            this.xrTableCellNguoiKy.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellNguoiKy.Name = "xrTableCellNguoiKy";
            this.xrTableCellNguoiKy.StylePriority.UseFont = false;
            this.xrTableCellNguoiKy.StylePriority.UseTextAlignment = false;
            this.xrTableCellNguoiKy.Text = "Người ký";
            this.xrTableCellNguoiKy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNguoiKy.Weight = 0.94827395730174224D;
            // 
            // xrTableCellChucVuNguoiKy
            // 
            this.xrTableCellChucVuNguoiKy.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellChucVuNguoiKy.Name = "xrTableCellChucVuNguoiKy";
            this.xrTableCellChucVuNguoiKy.StylePriority.UseFont = false;
            this.xrTableCellChucVuNguoiKy.StylePriority.UseTextAlignment = false;
            this.xrTableCellChucVuNguoiKy.Text = "Chức vụ người ký";
            this.xrTableCellChucVuNguoiKy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellChucVuNguoiKy.Weight = 0.92291944028505912D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblKyHoTen,
            this.lblChucVuKyHoTen2,
            this.lblNguoiDuyet,
            this.lblChucVuKyHoTen1,
            this.lblReportDate,
            this.lblLanhDaoCoQuan,
            this.lblNguoiLap});
            this.ReportFooter.HeightF = 183F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // lblKyHoTen
            // 
            this.lblKyHoTen.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic);
            this.lblKyHoTen.LocationFloat = new DevExpress.Utils.PointFloat(42.40182F, 62.58335F);
            this.lblKyHoTen.Name = "lblKyHoTen";
            this.lblKyHoTen.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblKyHoTen.SizeF = new System.Drawing.SizeF(271.875F, 23F);
            this.lblKyHoTen.StylePriority.UseFont = false;
            this.lblKyHoTen.StylePriority.UseTextAlignment = false;
            this.lblKyHoTen.Text = "(Ký, ghi rõ họ và tên)";
            this.lblKyHoTen.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // lblChucVuKyHoTen2
            // 
            this.lblChucVuKyHoTen2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Italic);
            this.lblChucVuKyHoTen2.LocationFloat = new DevExpress.Utils.PointFloat(787.5002F, 62.58332F);
            this.lblChucVuKyHoTen2.Name = "lblChucVuKyHoTen2";
            this.lblChucVuKyHoTen2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblChucVuKyHoTen2.SizeF = new System.Drawing.SizeF(285.5F, 23F);
            this.lblChucVuKyHoTen2.StylePriority.UseFont = false;
            this.lblChucVuKyHoTen2.StylePriority.UseTextAlignment = false;
            this.lblChucVuKyHoTen2.Text = "(Chức vụ, ký, ghi rõ họ và tên)";
            this.lblChucVuKyHoTen2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // lblNguoiDuyet
            // 
            this.lblNguoiDuyet.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.lblNguoiDuyet.LocationFloat = new DevExpress.Utils.PointFloat(368.4898F, 39.58333F);
            this.lblNguoiDuyet.Name = "lblNguoiDuyet";
            this.lblNguoiDuyet.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblNguoiDuyet.SizeF = new System.Drawing.SizeF(302.1819F, 23F);
            this.lblNguoiDuyet.StylePriority.UseFont = false;
            this.lblNguoiDuyet.StylePriority.UseTextAlignment = false;
            this.lblNguoiDuyet.Text = "NGƯỜI DUYỆT";
            this.lblNguoiDuyet.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblChucVuKyHoTen1
            // 
            this.lblChucVuKyHoTen1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Italic);
            this.lblChucVuKyHoTen1.LocationFloat = new DevExpress.Utils.PointFloat(368.4898F, 62.58335F);
            this.lblChucVuKyHoTen1.Name = "lblChucVuKyHoTen1";
            this.lblChucVuKyHoTen1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblChucVuKyHoTen1.SizeF = new System.Drawing.SizeF(302.1819F, 23F);
            this.lblChucVuKyHoTen1.StylePriority.UseFont = false;
            this.lblChucVuKyHoTen1.StylePriority.UseTextAlignment = false;
            this.lblChucVuKyHoTen1.Text = "(Chức vụ, ký, ghi rõ họ và tên)";
            this.lblChucVuKyHoTen1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblReportDate
            // 
            this.lblReportDate.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportDate.LocationFloat = new DevExpress.Utils.PointFloat(787.5F, 14.58333F);
            this.lblReportDate.Name = "lblReportDate";
            this.lblReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblReportDate.SizeF = new System.Drawing.SizeF(295.4998F, 23F);
            this.lblReportDate.StylePriority.UseFont = false;
            this.lblReportDate.StylePriority.UseTextAlignment = false;
            this.lblReportDate.Text = "{0}, Ngày {1} tháng {2} năm {3}";
            this.lblReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblLanhDaoCoQuan
            // 
            this.lblLanhDaoCoQuan.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.lblLanhDaoCoQuan.LocationFloat = new DevExpress.Utils.PointFloat(787.5F, 39.58333F);
            this.lblLanhDaoCoQuan.Name = "lblLanhDaoCoQuan";
            this.lblLanhDaoCoQuan.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblLanhDaoCoQuan.SizeF = new System.Drawing.SizeF(285.5F, 23F);
            this.lblLanhDaoCoQuan.StylePriority.UseFont = false;
            this.lblLanhDaoCoQuan.StylePriority.UseTextAlignment = false;
            this.lblLanhDaoCoQuan.Text = "THỦ TRƯỞNG CƠ QUAN, ĐƠN VỊ";
            this.lblLanhDaoCoQuan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // lblNguoiLap
            // 
            this.lblNguoiLap.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.lblNguoiLap.LocationFloat = new DevExpress.Utils.PointFloat(42.40182F, 39.58333F);
            this.lblNguoiLap.Name = "lblNguoiLap";
            this.lblNguoiLap.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblNguoiLap.SizeF = new System.Drawing.SizeF(271.875F, 23F);
            this.lblNguoiLap.StylePriority.UseFont = false;
            this.lblNguoiLap.StylePriority.UseTextAlignment = false;
            this.lblNguoiLap.Text = "NGƯỜI LẬP";
            this.lblNguoiLap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.GroupHeader1.HeightF = 28.95831F;
            this.GroupHeader1.Level = 1;
            this.GroupHeader1.Name = "GroupHeader1";
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
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1152F, 28.95831F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UsePadding = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrGroup,
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
            // xrGroup
            // 
            this.xrGroup.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrGroup.Name = "xrGroup";
            this.xrGroup.StylePriority.UseFont = false;
            this.xrGroup.StylePriority.UseTextAlignment = false;
            this.xrGroup.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrGroup.Weight = 0.28472226701051789D;
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
            this.xrTableCellGroupHead.Weight = 11.235271976583919D;
            this.xrTableCellGroupHead.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Group_BeforePrint);
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
            this.PageFooter.HeightF = 41.66667F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrPageInfo1.Format = "Trang {0} của {1}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1015.958F, 18.66665F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(126.0417F, 23.00001F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.GroupHeader2.HeightF = 29.16667F;
            this.GroupHeader2.Name = "GroupHeader2";
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1152F, 28.95831F);
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseFont = false;
            this.xrTable3.StylePriority.UsePadding = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCellGroupSoHieuCB,
            this.xrTableCellGroupHoTen,
            this.xrTableCell4});
            this.xrTableRow3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
            this.xrTableRow3.StylePriority.UseFont = false;
            this.xrTableRow3.StylePriority.UsePadding = false;
            this.xrTableRow3.StylePriority.UseTextAlignment = false;
            this.xrTableRow3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell1.Weight = 0.2847222097900875D;
            // 
            // xrTableCellGroupSoHieuCB
            // 
            this.xrTableCellGroupSoHieuCB.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellGroupSoHieuCB.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTableCellGroupSoHieuCB.Name = "xrTableCellGroupSoHieuCB";
            this.xrTableCellGroupSoHieuCB.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCellGroupSoHieuCB.StylePriority.UseBorders = false;
            this.xrTableCellGroupSoHieuCB.StylePriority.UseFont = false;
            this.xrTableCellGroupSoHieuCB.StylePriority.UsePadding = false;
            this.xrTableCellGroupSoHieuCB.StylePriority.UseTextAlignment = false;
            this.xrTableCellGroupSoHieuCB.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellGroupSoHieuCB.Weight = 0.796296122540843D;
            this.xrTableCellGroupSoHieuCB.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Group_BeforePrint);
            // 
            // xrTableCellGroupHoTen
            // 
            this.xrTableCellGroupHoTen.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellGroupHoTen.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTableCellGroupHoTen.Name = "xrTableCellGroupHoTen";
            this.xrTableCellGroupHoTen.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCellGroupHoTen.StylePriority.UseBorders = false;
            this.xrTableCellGroupHoTen.StylePriority.UseFont = false;
            this.xrTableCellGroupHoTen.StylePriority.UsePadding = false;
            this.xrTableCellGroupHoTen.StylePriority.UseTextAlignment = false;
            this.xrTableCellGroupHoTen.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellGroupHoTen.Weight = 1.04629643179116D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.StylePriority.UsePadding = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell4.Weight = 9.3926777660323122D;
            // 
            // rp_EmployeeAppointment
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter,
            this.GroupHeader1,
            this.PageFooter,
            this.GroupHeader2});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(7, 0, 31, 30);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        //so thu tu cua group
        int STT2 = 1;
        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrGroup.Text = STT2.ToString();
            STT2++;
        }
    }
}