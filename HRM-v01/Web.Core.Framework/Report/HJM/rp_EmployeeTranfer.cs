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
    /// Summary description for rp_EmployeeTranfer
    /// </summary>
    public class rp_EmployeeTranfer : DevExpress.XtraReports.UI.XtraReport
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
        private XRTableCell xrllSoThuTu;
        private XRTableCell xrTableCellMaCanBo;
        private XRTableCell xrTableCellHoTen;
        private XRTableCell xrlNgaySinh;
        private XRTableCell xrlDonViCongTac;
        private XRTableCell xrlChucVuNguoiKy;
        private XRPageInfo xrPageInfo1;
        private XRTableCell xrlSoQuyetDinh;
        private XRTableCell xrlNgayQuyetDinh;
        private XRTableCell xrlNgayHieuLuc;
        private XRTableCell xrlNguoiKy;
        private XRTableCell xrlCoQuanBoNhiem;
        private XRTable xrTable4;
        private XRTableRow xrTableRow4;
        private XRTableCell xrTableCellSoThuTu;
        private XRTableCell xrTableCellSoHieuCBCC;
        private XRTableCell xrTableCellHoVaTen;
        private XRTableCell xrTableCellNgaySinh;
        private XRTableCell xrTableCellDonViCongTac;
        private XRTableCell xrTableCellSoQuyetDinh;
        private XRTableCell xrTableCellNgayQuyetDinh;
        private XRTableCell xrTableCellNgayHieuLuc;
        private XRTableCell xrTableCellDonViNoiDen;
        private XRTableCell xrTableCellNguoiKy;
        private XRTableCell xrTableCellChucVuNguoiKy;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrTableCell27;
        private XRTableCell xrTableCellGroupTenDonVi;
        private XRLabel xrl_TenCoQuanDonVi;
        private XRLabel xrLabel3;
        private XRLabel lblChucVuKyHoTen2;
        private XRLabel lblKyHoTen;
        private XRTableCell xrTableCellChucVuHienTai;
        private XRTableCell xrTableCell1;
        private string CONST_BUSINESS_TYPE = "ThuyenChuyenDieuChuyen";
        private GroupHeaderBand GroupHeader2;
        private XRTable xrTable3;
        private XRTableRow xrTableRow3;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCellGroupSoHieuCB;
        private XRTableCell xrTableCellGroupHoTen;
        private XRTableCell xrTableCell5;
        private XRLabel xrToDate;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public rp_EmployeeTranfer()
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
                xrl_TenCoQuanDonVi.Text = control.GetCompanyName(filter.SessionDepartment);

                // get organization
                var organization = cat_DepartmentServices.GetByDepartments(filter.SessionDepartment);
                if (organization == null) return;
                // set end report info
                var location = control.GetCityName(filter.SessionDepartment);
                lblReportDate.Text = string.Format(lblReportDate.Text, location, DateTime.Now.Day,
                    DateTime.Now.Month, DateTime.Now.Year);

                //var toDate = new DateTime(filter.Year, filter.StartMonth, DateTime.Now.Day);
                var toDate = filter.ReportedDate;
                xrToDate.Text = string.Format(xrToDate.Text, toDate.Day, toDate.Month, toDate.Year);

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
                    SQLManagementAdapter.GetStore_ReportListBusinessHistory(string.Join(",", arrOrgCode),
                        CONST_BUSINESS_TYPE, condition));
                DataSource = table;

                //binding data
                xrTableCellNgaySinh.DataBindings.Add("Text", DataSource, "BirthDate", "{0:dd/MM/yyyy}");
                xrTableCellDonViCongTac.DataBindings.Add("Text", DataSource, "CurrentDepartment");
                xrTableCellChucVuHienTai.DataBindings.Add("Text", DataSource, "CurrentPosition");
                xrTableCellSoQuyetDinh.DataBindings.Add("Text", DataSource, "DecisionNumber");
                xrTableCellNgayQuyetDinh.DataBindings.Add("Text", DataSource, "DecisionDate", "{0:dd/MM/yyyy}");
                xrTableCellNgayHieuLuc.DataBindings.Add("Text", DataSource, "EffectiveDate", "{0:dd/MM/yyyy}");
                xrTableCellDonViNoiDen.DataBindings.Add("Text", DataSource, "DestinationDepartment");
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
            string resourceFileName = "rp_EmployeeTranfer.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellSoThuTu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellSoHieuCBCC = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHoVaTen = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNgaySinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDonViCongTac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChucVuHienTai = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellSoQuyetDinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNgayQuyetDinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNgayHieuLuc = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDonViNoiDen = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNguoiKy = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChucVuNguoiKy = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrToDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TenCoQuanDonVi = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TitleBC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TenCongTy = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrllSoThuTu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellMaCanBo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHoTen = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlNgaySinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlDonViCongTac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlSoQuyetDinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlNgayQuyetDinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlNgayHieuLuc = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlCoQuanBoNhiem = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlNguoiKy = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlChucVuNguoiKy = new DevExpress.XtraReports.UI.XRTableCell();
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
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellGroupTenDonVi = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellGroupSoHieuCB = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellGroupHoTen = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
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
            this.xrTable4.SizeF = new System.Drawing.SizeF(1140F, 28.95831F);
            this.xrTable4.StylePriority.UseBorders = false;
            this.xrTable4.StylePriority.UseFont = false;
            this.xrTable4.StylePriority.UsePadding = false;
            this.xrTable4.StylePriority.UseTextAlignment = false;
            this.xrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellSoThuTu,
            this.xrTableCellSoHieuCBCC,
            this.xrTableCellHoVaTen,
            this.xrTableCellNgaySinh,
            this.xrTableCellDonViCongTac,
            this.xrTableCellChucVuHienTai,
            this.xrTableCellSoQuyetDinh,
            this.xrTableCellNgayQuyetDinh,
            this.xrTableCellNgayHieuLuc,
            this.xrTableCellDonViNoiDen,
            this.xrTableCellNguoiKy,
            this.xrTableCellChucVuNguoiKy});
            this.xrTableRow4.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
            this.xrTableRow4.StylePriority.UseFont = false;
            this.xrTableRow4.StylePriority.UsePadding = false;
            this.xrTableRow4.StylePriority.UseTextAlignment = false;
            this.xrTableRow4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCellSoThuTu
            // 
            this.xrTableCellSoThuTu.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTableCellSoThuTu.Name = "xrTableCellSoThuTu";
            this.xrTableCellSoThuTu.StylePriority.UseFont = false;
            this.xrTableCellSoThuTu.StylePriority.UseTextAlignment = false;
            this.xrTableCellSoThuTu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSoThuTu.Weight = 0.2847222097900875D;
            this.xrTableCellSoThuTu.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrTableCellSoHieuCBCC
            // 
            this.xrTableCellSoHieuCBCC.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTableCellSoHieuCBCC.Name = "xrTableCellSoHieuCBCC";
            this.xrTableCellSoHieuCBCC.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCellSoHieuCBCC.StylePriority.UseFont = false;
            this.xrTableCellSoHieuCBCC.StylePriority.UsePadding = false;
            this.xrTableCellSoHieuCBCC.StylePriority.UseTextAlignment = false;
            this.xrTableCellSoHieuCBCC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellSoHieuCBCC.Weight = 0.796296122540843D;
            // 
            // xrTableCellHoVaTen
            // 
            this.xrTableCellHoVaTen.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTableCellHoVaTen.Name = "xrTableCellHoVaTen";
            this.xrTableCellHoVaTen.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCellHoVaTen.StylePriority.UseFont = false;
            this.xrTableCellHoVaTen.StylePriority.UsePadding = false;
            this.xrTableCellHoVaTen.StylePriority.UseTextAlignment = false;
            this.xrTableCellHoVaTen.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellHoVaTen.Weight = 1.04629643179116D;
            // 
            // xrTableCellNgaySinh
            // 
            this.xrTableCellNgaySinh.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTableCellNgaySinh.Name = "xrTableCellNgaySinh";
            this.xrTableCellNgaySinh.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCellNgaySinh.StylePriority.UseFont = false;
            this.xrTableCellNgaySinh.StylePriority.UsePadding = false;
            this.xrTableCellNgaySinh.StylePriority.UseTextAlignment = false;
            this.xrTableCellNgaySinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNgaySinh.Weight = 0.682117930927601D;
            // 
            // xrTableCellDonViCongTac
            // 
            this.xrTableCellDonViCongTac.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTableCellDonViCongTac.Name = "xrTableCellDonViCongTac";
            this.xrTableCellDonViCongTac.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCellDonViCongTac.StylePriority.UseFont = false;
            this.xrTableCellDonViCongTac.StylePriority.UsePadding = false;
            this.xrTableCellDonViCongTac.StylePriority.UseTextAlignment = false;
            this.xrTableCellDonViCongTac.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellDonViCongTac.Weight = 1.0852080981798138D;
            // 
            // xrTableCellChucVuHienTai
            // 
            this.xrTableCellChucVuHienTai.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTableCellChucVuHienTai.Name = "xrTableCellChucVuHienTai";
            this.xrTableCellChucVuHienTai.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCellChucVuHienTai.StylePriority.UseFont = false;
            this.xrTableCellChucVuHienTai.StylePriority.UsePadding = false;
            this.xrTableCellChucVuHienTai.StylePriority.UseTextAlignment = false;
            this.xrTableCellChucVuHienTai.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellChucVuHienTai.Weight = 0.90434008501445207D;
            // 
            // xrTableCellSoQuyetDinh
            // 
            this.xrTableCellSoQuyetDinh.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTableCellSoQuyetDinh.Name = "xrTableCellSoQuyetDinh";
            this.xrTableCellSoQuyetDinh.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCellSoQuyetDinh.StylePriority.UseFont = false;
            this.xrTableCellSoQuyetDinh.StylePriority.UsePadding = false;
            this.xrTableCellSoQuyetDinh.StylePriority.UseTextAlignment = false;
            this.xrTableCellSoQuyetDinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellSoQuyetDinh.Weight = 0.84658806695750577D;
            // 
            // xrTableCellNgayQuyetDinh
            // 
            this.xrTableCellNgayQuyetDinh.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTableCellNgayQuyetDinh.Name = "xrTableCellNgayQuyetDinh";
            this.xrTableCellNgayQuyetDinh.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCellNgayQuyetDinh.StylePriority.UseFont = false;
            this.xrTableCellNgayQuyetDinh.StylePriority.UsePadding = false;
            this.xrTableCellNgayQuyetDinh.StylePriority.UseTextAlignment = false;
            this.xrTableCellNgayQuyetDinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNgayQuyetDinh.Weight = 0.80848953617774222D;
            // 
            // xrTableCellNgayHieuLuc
            // 
            this.xrTableCellNgayHieuLuc.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTableCellNgayHieuLuc.Name = "xrTableCellNgayHieuLuc";
            this.xrTableCellNgayHieuLuc.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCellNgayHieuLuc.StylePriority.UseFont = false;
            this.xrTableCellNgayHieuLuc.StylePriority.UsePadding = false;
            this.xrTableCellNgayHieuLuc.StylePriority.UseTextAlignment = false;
            this.xrTableCellNgayHieuLuc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNgayHieuLuc.Weight = 0.72347204454981628D;
            // 
            // xrTableCellDonViNoiDen
            // 
            this.xrTableCellDonViNoiDen.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTableCellDonViNoiDen.Name = "xrTableCellDonViNoiDen";
            this.xrTableCellDonViNoiDen.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCellDonViNoiDen.StylePriority.UseFont = false;
            this.xrTableCellDonViNoiDen.StylePriority.UsePadding = false;
            this.xrTableCellDonViNoiDen.StylePriority.UseTextAlignment = false;
            this.xrTableCellDonViNoiDen.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellDonViNoiDen.Weight = 1.2523325056992185D;
            // 
            // xrTableCellNguoiKy
            // 
            this.xrTableCellNguoiKy.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTableCellNguoiKy.Name = "xrTableCellNguoiKy";
            this.xrTableCellNguoiKy.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCellNguoiKy.StylePriority.UseFont = false;
            this.xrTableCellNguoiKy.StylePriority.UsePadding = false;
            this.xrTableCellNguoiKy.StylePriority.UseTextAlignment = false;
            this.xrTableCellNguoiKy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellNguoiKy.Weight = 0.95600352434988833D;
            // 
            // xrTableCellChucVuNguoiKy
            // 
            this.xrTableCellChucVuNguoiKy.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTableCellChucVuNguoiKy.Name = "xrTableCellChucVuNguoiKy";
            this.xrTableCellChucVuNguoiKy.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCellChucVuNguoiKy.StylePriority.UseFont = false;
            this.xrTableCellChucVuNguoiKy.StylePriority.UsePadding = false;
            this.xrTableCellChucVuNguoiKy.StylePriority.UseTextAlignment = false;
            this.xrTableCellChucVuNguoiKy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellChucVuNguoiKy.Weight = 0.92361019444748926D;
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
            this.xrToDate,
            this.xrl_TenCoQuanDonVi,
            this.xrLabel3,
            this.xrl_TitleBC,
            this.xrl_TenCongTy});
            this.ReportHeader.HeightF = 143.0833F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrToDate
            // 
            this.xrToDate.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrToDate.LocationFloat = new DevExpress.Utils.PointFloat(1.000039F, 104.1233F);
            this.xrToDate.Name = "xrToDate";
            this.xrToDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrToDate.SizeF = new System.Drawing.SizeF(1152F, 28.96F);
            this.xrToDate.StylePriority.UseFont = false;
            this.xrToDate.StylePriority.UseTextAlignment = false;
            this.xrToDate.Text = "(Tính đến ngày {0}/{1}/{2})";
            this.xrToDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            this.xrl_TenCoQuanDonVi.Text = "TÊN CƠ QUAN, ĐƠN VỊ";
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
            // xrl_TitleBC
            // 
            this.xrl_TitleBC.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrl_TitleBC.LocationFloat = new DevExpress.Utils.PointFloat(1.000039F, 74.20836F);
            this.xrl_TitleBC.Name = "xrl_TitleBC";
            this.xrl_TitleBC.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_TitleBC.SizeF = new System.Drawing.SizeF(1152F, 28.96F);
            this.xrl_TitleBC.StylePriority.UseFont = false;
            this.xrl_TitleBC.StylePriority.UseTextAlignment = false;
            this.xrl_TitleBC.Text = "BÁO CÁO DANH SÁCH CÁN BỘ ĐƯỢC THUYÊN CHUYỂN, ĐIỀU CHUYỂN";
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
            this.xrTable1.SizeF = new System.Drawing.SizeF(1140F, 38.33331F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrllSoThuTu,
            this.xrTableCellMaCanBo,
            this.xrTableCellHoTen,
            this.xrlNgaySinh,
            this.xrlDonViCongTac,
            this.xrTableCell1,
            this.xrlSoQuyetDinh,
            this.xrlNgayQuyetDinh,
            this.xrlNgayHieuLuc,
            this.xrlCoQuanBoNhiem,
            this.xrlNguoiKy,
            this.xrlChucVuNguoiKy});
            this.xrTableRow1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
            this.xrTableRow1.StylePriority.UseFont = false;
            this.xrTableRow1.StylePriority.UsePadding = false;
            this.xrTableRow1.StylePriority.UseTextAlignment = false;
            this.xrTableRow1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow1.Weight = 1D;
            // 
            // xrllSoThuTu
            // 
            this.xrllSoThuTu.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrllSoThuTu.Name = "xrllSoThuTu";
            this.xrllSoThuTu.StylePriority.UseFont = false;
            this.xrllSoThuTu.StylePriority.UseTextAlignment = false;
            this.xrllSoThuTu.Text = "STT";
            this.xrllSoThuTu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrllSoThuTu.Weight = 0.28472226701051789D;
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
            // xrlNgaySinh
            // 
            this.xrlNgaySinh.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrlNgaySinh.Name = "xrlNgaySinh";
            this.xrlNgaySinh.StylePriority.UseFont = false;
            this.xrlNgaySinh.StylePriority.UseTextAlignment = false;
            this.xrlNgaySinh.Text = "Ngày sinh";
            this.xrlNgaySinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrlNgaySinh.Weight = 0.68211855655834219D;
            // 
            // xrlDonViCongTac
            // 
            this.xrlDonViCongTac.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrlDonViCongTac.Name = "xrlDonViCongTac";
            this.xrlDonViCongTac.StylePriority.UseFont = false;
            this.xrlDonViCongTac.StylePriority.UseTextAlignment = false;
            this.xrlDonViCongTac.Text = "Đơn vị đang công tác";
            this.xrlDonViCongTac.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrlDonViCongTac.Weight = 1.0852080665192627D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "Chức vụ hiện tại";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell1.Weight = 0.90434005416723751D;
            // 
            // xrlSoQuyetDinh
            // 
            this.xrlSoQuyetDinh.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrlSoQuyetDinh.Name = "xrlSoQuyetDinh";
            this.xrlSoQuyetDinh.StylePriority.UseFont = false;
            this.xrlSoQuyetDinh.StylePriority.UseTextAlignment = false;
            this.xrlSoQuyetDinh.Text = "Số quyết định";
            this.xrlSoQuyetDinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrlSoQuyetDinh.Weight = 0.84658598333727519D;
            // 
            // xrlNgayQuyetDinh
            // 
            this.xrlNgayQuyetDinh.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrlNgayQuyetDinh.Name = "xrlNgayQuyetDinh";
            this.xrlNgayQuyetDinh.StylePriority.UseFont = false;
            this.xrlNgayQuyetDinh.StylePriority.UseTextAlignment = false;
            this.xrlNgayQuyetDinh.Text = "Ngày quyết định";
            this.xrlNgayQuyetDinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrlNgayQuyetDinh.Weight = 0.80918295031454457D;
            // 
            // xrlNgayHieuLuc
            // 
            this.xrlNgayHieuLuc.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrlNgayHieuLuc.Name = "xrlNgayHieuLuc";
            this.xrlNgayHieuLuc.StylePriority.UseFont = false;
            this.xrlNgayHieuLuc.StylePriority.UseTextAlignment = false;
            this.xrlNgayHieuLuc.Text = "Ngày hiệu lực";
            this.xrlNgayHieuLuc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrlNgayHieuLuc.Weight = 0.72347205592279351D;
            // 
            // xrlCoQuanBoNhiem
            // 
            this.xrlCoQuanBoNhiem.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrlCoQuanBoNhiem.Name = "xrlCoQuanBoNhiem";
            this.xrlCoQuanBoNhiem.StylePriority.UseFont = false;
            this.xrlCoQuanBoNhiem.StylePriority.UseTextAlignment = false;
            this.xrlCoQuanBoNhiem.Text = "Đơn vị nơi đến";
            this.xrlCoQuanBoNhiem.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrlCoQuanBoNhiem.Weight = 1.2523322173377434D;
            // 
            // xrlNguoiKy
            // 
            this.xrlNguoiKy.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrlNguoiKy.Name = "xrlNguoiKy";
            this.xrlNguoiKy.StylePriority.UseFont = false;
            this.xrlNguoiKy.StylePriority.UseTextAlignment = false;
            this.xrlNguoiKy.Text = "Người ký";
            this.xrlNguoiKy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrlNguoiKy.Weight = 0.95600264540432589D;
            // 
            // xrlChucVuNguoiKy
            // 
            this.xrlChucVuNguoiKy.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrlChucVuNguoiKy.Name = "xrlChucVuNguoiKy";
            this.xrlChucVuNguoiKy.StylePriority.UseFont = false;
            this.xrlChucVuNguoiKy.StylePriority.UseTextAlignment = false;
            this.xrlChucVuNguoiKy.Text = "Chức vụ người ký";
            this.xrlChucVuNguoiKy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrlChucVuNguoiKy.Weight = 0.92291944028505912D;
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
            this.lblReportDate.Text = "{0}, ngày {1} tháng {2} năm {3}";
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
            this.xrTableCellGroupTenDonVi});
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
            this.xrTableCell27.Weight = 0.31479984292348995D;
            // 
            // xrTableCellGroupTenDonVi
            // 
            this.xrTableCellGroupTenDonVi.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellGroupTenDonVi.Name = "xrTableCellGroupTenDonVi";
            this.xrTableCellGroupTenDonVi.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellGroupTenDonVi.StylePriority.UseFont = false;
            this.xrTableCellGroupTenDonVi.StylePriority.UsePadding = false;
            this.xrTableCellGroupTenDonVi.StylePriority.UseTextAlignment = false;
            this.xrTableCellGroupTenDonVi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellGroupTenDonVi.Weight = 11.205194400670946D;
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
            this.GroupHeader2.HeightF = 28.95831F;
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
            this.xrTable3.SizeF = new System.Drawing.SizeF(1140F, 28.95831F);
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseFont = false;
            this.xrTable3.StylePriority.UsePadding = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.xrTableCellGroupSoHieuCB,
            this.xrTableCellGroupHoTen,
            this.xrTableCell5});
            this.xrTableRow3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
            this.xrTableRow3.StylePriority.UseFont = false;
            this.xrTableRow3.StylePriority.UsePadding = false;
            this.xrTableRow3.StylePriority.UseTextAlignment = false;
            this.xrTableRow3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 0.2847222097900875D;
            // 
            // xrTableCellGroupSoHieuCB
            // 
            this.xrTableCellGroupSoHieuCB.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTableCellGroupSoHieuCB.Name = "xrTableCellGroupSoHieuCB";
            this.xrTableCellGroupSoHieuCB.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCellGroupSoHieuCB.StylePriority.UseFont = false;
            this.xrTableCellGroupSoHieuCB.StylePriority.UsePadding = false;
            this.xrTableCellGroupSoHieuCB.StylePriority.UseTextAlignment = false;
            this.xrTableCellGroupSoHieuCB.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellGroupSoHieuCB.Weight = 0.796296122540843D;
            this.xrTableCellGroupSoHieuCB.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Group_BeforePrint);
            // 
            // xrTableCellGroupHoTen
            // 
            this.xrTableCellGroupHoTen.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTableCellGroupHoTen.Name = "xrTableCellGroupHoTen";
            this.xrTableCellGroupHoTen.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCellGroupHoTen.StylePriority.UseFont = false;
            this.xrTableCellGroupHoTen.StylePriority.UsePadding = false;
            this.xrTableCellGroupHoTen.StylePriority.UseTextAlignment = false;
            this.xrTableCellGroupHoTen.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellGroupHoTen.Weight = 1.04629643179116D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Font = new System.Drawing.Font("Times New Roman", 8.5F);
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.StylePriority.UsePadding = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell5.Weight = 8.1821631035338935D;
            // 
            // rp_EmployeeTranfer
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
            this.Margins = new System.Drawing.Printing.Margins(6, 0, 31, 30);
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
    }
}