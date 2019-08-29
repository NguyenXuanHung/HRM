using System;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Adapter;
using Web.Core.Object.Report;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Summary description for rp_EmployeeNotHaveHealthInsuranceNumber
    /// </summary>
    public class rp_EmployeeNotHaveHealthInsuranceNumber : XtraReport
    {
        private DetailBand Detail;
        private TopMarginBand TopMargin;
        private BottomMarginBand BottomMargin;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrtstt;
        private XRTableCell xrtmanhanvien;
        private XRTableCell xrthoten;
        private XRTableCell xrt_ngaysinh;
        private XRTableCell xrtgioitinh;
        private XRTableCell xrtdiachi;
        private XRTableCell xrtdienthoai;
        private XRTableCell xrtngayvaocongty;
        private XRTableCell xrt_trinhdo;
        private XRTableCell xrtchucvu;
        private ReportHeaderBand ReportHeader;
        private XRLabel xrl_TitleBC;
        private XRLabel xrl_NgayBaoCao;
        private XRLabel xrl_TenCongTy;
        private PageHeaderBand PageHeader;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCell4;
        private XRTableCell xrTableCell3;
        private XRTableCell xrTableCell5;
        private XRTableCell xrTableCell6;
        private XRTableCell xrTableCell7;
        private XRTableCell xrTableCell8;
        private XRTableCell xrTableCell12;
        private XRTableCell xrTableCell15;
        private ReportFooterBand ReportFooter;
        private XRLabel xrl_ten1;
        private XRLabel xrl_ten2;
        private XRLabel xrl_ten3;
        private XRLabel xrtngayketxuat;
        private XRLabel xrl_footer1;
        private XRLabel xrl_footer3;
        private XRLabel xrl_footer2;
        private GroupHeaderBand GroupHeader1;
        private PageFooterBand PageFooter;
        private XRPageInfo xrPageInfo1;
        private XRTable xrTable3;
        private XRTableRow xrTableRow3;
        private XRTableCell xrDepartmentName;
        private XRPictureBox xrLogo;

        private IContainer components = null;

        public rp_EmployeeNotHaveHealthInsuranceNumber()
        {
            InitializeComponent();
        }

        int STT = 1;

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrtstt.Text = STT.ToString();
            STT++;
        }

        private void Group_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            STT = 1;
            xrtstt.Text = STT.ToString();

        }

        public void BindData(ReportFilter filter)
        {
            try
            {
                ReportController rpCtr = new ReportController();
                xrl_NgayBaoCao.Text = "Ngày báo cáo: " + string.Format("{0:dd/MM/yyyy}", filter.ReportedDate);
                var location = new ReportController().GetCityName(filter.SessionDepartment);
                xrtngayketxuat.Text = string.Format(xrtngayketxuat.Text, location, DateTime.Now.Day,
                    DateTime.Now.Month, DateTime.Now.Year);
                xrl_TenCongTy.Text = rpCtr.GetCompanyName(filter.SessionDepartment);
                // get organization
                var organization = cat_DepartmentServices.GetByDepartments(filter.SessionDepartment);
                if (organization != null)
                {
                    //select form db
                    var arrOrgCode = string.IsNullOrEmpty(filter.SelectedDepartment)
                        ? new string[] { }
                        : filter.SelectedDepartment.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < arrOrgCode.Length; i++)
                    {
                        arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
                    }
                    var table = SQLHelper.ExecuteTable(
                    SQLManagementAdapter.GetStore_ListEmployeeNotHaveHealthInsuranceRecord(string.Join(",",
                        arrOrgCode)));

                    DataSource = table;
                    xrtmanhanvien.DataBindings.Add("Text", DataSource, "EmployeeCode");
                    xrthoten.DataBindings.Add("Text", DataSource, "FullName");
                    xrt_ngaysinh.DataBindings.Add("Text", DataSource, "BirthDate", "{0:dd/MM/yyyy}");
                    xrtgioitinh.DataBindings.Add("Text", DataSource, "Sex");
                    xrtdiachi.DataBindings.Add("Text", DataSource, "Address");
                    xrtdienthoai.DataBindings.Add("Text", DataSource, "Phone");
                    xrtngayvaocongty.DataBindings.Add("Text", DataSource, "ParticipationDate", "{0:dd/MM/yyyy}");
                    xrt_trinhdo.DataBindings.Add("Text", DataSource, "Education");
                    xrtchucvu.DataBindings.Add("Text", DataSource, "Position");
                    GroupHeader1.GroupFields.AddRange(new[] { new GroupField("DepartmentId", XRColumnSortOrder.Ascending) });
                    xrDepartmentName.DataBindings.Add("Text", DataSource, "DepartmentName");
                }
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra: " + ex.Message);
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
            string resourceFileName = "rp_EmployeeNotHaveHealthInsuranceNumber.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrtstt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtmanhanvien = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrthoten = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_ngaysinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtgioitinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtdiachi = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtdienthoai = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtngayvaocongty = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_trinhdo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtchucvu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrDepartmentName = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrl_TitleBC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NgayBaoCao = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TenCongTy = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrl_ten1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ten2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ten3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrtngayketxuat = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer2 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.HeightF = 25.41666F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0.001144409F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1082.999F, 25.41666F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UsePadding = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrtstt,
            this.xrtmanhanvien,
            this.xrthoten,
            this.xrt_ngaysinh,
            this.xrtgioitinh,
            this.xrtdiachi,
            this.xrtdienthoai,
            this.xrtngayvaocongty,
            this.xrt_trinhdo,
            this.xrtchucvu});
            this.xrTableRow2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.StylePriority.UseFont = false;
            this.xrTableRow2.StylePriority.UseTextAlignment = false;
            this.xrTableRow2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow2.Weight = 1D;
            // 
            // xrtstt
            // 
            this.xrtstt.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtstt.Name = "xrtstt";
            this.xrtstt.StylePriority.UseFont = false;
            this.xrtstt.StylePriority.UseTextAlignment = false;
            this.xrtstt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtstt.Weight = 0.35416665980020634D;
            // 
            // xrtmanhanvien
            // 
            this.xrtmanhanvien.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtmanhanvien.Name = "xrtmanhanvien";
            this.xrtmanhanvien.StylePriority.UseFont = false;
            this.xrtmanhanvien.StylePriority.UseTextAlignment = false;
            this.xrtmanhanvien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrtmanhanvien.Weight = 0.90000039676117249D;
            // 
            // xrthoten
            // 
            this.xrthoten.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrthoten.Name = "xrthoten";
            this.xrthoten.StylePriority.UseFont = false;
            this.xrthoten.StylePriority.UseTextAlignment = false;
            this.xrthoten.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrthoten.Weight = 1.3000004911796745D;
            // 
            // xrt_ngaysinh
            // 
            this.xrt_ngaysinh.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_ngaysinh.Name = "xrt_ngaysinh";
            this.xrt_ngaysinh.StylePriority.UseFont = false;
            this.xrt_ngaysinh.StylePriority.UseTextAlignment = false;
            this.xrt_ngaysinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_ngaysinh.Weight = 0.88332326601731936D;
            // 
            // xrtgioitinh
            // 
            this.xrtgioitinh.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtgioitinh.Name = "xrtgioitinh";
            this.xrtgioitinh.StylePriority.UseFont = false;
            this.xrtgioitinh.StylePriority.UseTextAlignment = false;
            this.xrtgioitinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtgioitinh.Weight = 0.6385508098189483D;
            // 
            // xrtdiachi
            // 
            this.xrtdiachi.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtdiachi.Name = "xrtdiachi";
            this.xrtdiachi.StylePriority.UseFont = false;
            this.xrtdiachi.StylePriority.UseTextAlignment = false;
            this.xrtdiachi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrtdiachi.Weight = 1.8341132432149279D;
            // 
            // xrtdienthoai
            // 
            this.xrtdienthoai.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtdienthoai.Name = "xrtdienthoai";
            this.xrtdienthoai.StylePriority.UseFont = false;
            this.xrtdienthoai.StylePriority.UseTextAlignment = false;
            this.xrtdienthoai.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtdienthoai.Weight = 1.0397113824010129D;
            // 
            // xrtngayvaocongty
            // 
            this.xrtngayvaocongty.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtngayvaocongty.Name = "xrtngayvaocongty";
            this.xrtngayvaocongty.StylePriority.UseFont = false;
            this.xrtngayvaocongty.StylePriority.UseTextAlignment = false;
            this.xrtngayvaocongty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtngayvaocongty.Weight = 1.1761104498251884D;
            // 
            // xrt_trinhdo
            // 
            this.xrt_trinhdo.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_trinhdo.Name = "xrt_trinhdo";
            this.xrt_trinhdo.StylePriority.UseFont = false;
            this.xrt_trinhdo.StylePriority.UseTextAlignment = false;
            this.xrt_trinhdo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_trinhdo.Weight = 1.275410592984175D;
            // 
            // xrtchucvu
            // 
            this.xrtchucvu.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtchucvu.Name = "xrtchucvu";
            this.xrtchucvu.StylePriority.UseFont = false;
            this.xrtchucvu.StylePriority.UseTextAlignment = false;
            this.xrtchucvu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrtchucvu.Weight = 1.4286072963765166D;
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1082.999F, 25.41666F);
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseFont = false;
            this.xrTable3.StylePriority.UsePadding = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrDepartmentName});
            this.xrTableRow3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.StylePriority.UseFont = false;
            this.xrTableRow3.StylePriority.UseTextAlignment = false;
            this.xrTableRow3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow3.Weight = 1D;
            // 
            // xrDepartmentName
            // 
            this.xrDepartmentName.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrDepartmentName.Name = "xrDepartmentName";
            this.xrDepartmentName.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 3, 3, 3, 100F);
            this.xrDepartmentName.StylePriority.UseFont = false;
            this.xrDepartmentName.StylePriority.UsePadding = false;
            this.xrDepartmentName.StylePriority.UseTextAlignment = false;
            this.xrDepartmentName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrDepartmentName.Weight = 10.829994588379142D;
            this.xrDepartmentName.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Group_BeforePrint);
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 50F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 48F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLogo,
            this.xrl_TitleBC,
            this.xrl_NgayBaoCao,
            this.xrl_TenCongTy});
            this.ReportHeader.HeightF = 257F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLogo
            // 
            this.xrLogo.LocationFloat = new DevExpress.Utils.PointFloat(53.25F, 7.708343F);
            this.xrLogo.Name = "xrLogo";
            this.xrLogo.Padding = new DevExpress.XtraPrinting.PaddingInfo(1, 1, 1, 1, 100F);
            this.xrLogo.SizeF = new System.Drawing.SizeF(110F, 110F);
            this.xrLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrLogo.StylePriority.UsePadding = false;
            // 
            // xrl_TitleBC
            // 
            this.xrl_TitleBC.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_TitleBC.LocationFloat = new DevExpress.Utils.PointFloat(0F, 181.6667F);
            this.xrl_TitleBC.Name = "xrl_TitleBC";
            this.xrl_TitleBC.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_TitleBC.SizeF = new System.Drawing.SizeF(1083F, 23F);
            this.xrl_TitleBC.StylePriority.UseFont = false;
            this.xrl_TitleBC.StylePriority.UseTextAlignment = false;
            this.xrl_TitleBC.Text = "BÁO CÁO DANH SÁCH CÁN BỘ CHƯA CÓ THẺ BHYT";
            this.xrl_TitleBC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_NgayBaoCao
            // 
            this.xrl_NgayBaoCao.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Italic);
            this.xrl_NgayBaoCao.LocationFloat = new DevExpress.Utils.PointFloat(0F, 206.6666F);
            this.xrl_NgayBaoCao.Name = "xrl_NgayBaoCao";
            this.xrl_NgayBaoCao.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_NgayBaoCao.SizeF = new System.Drawing.SizeF(1083F, 23F);
            this.xrl_NgayBaoCao.StylePriority.UseFont = false;
            this.xrl_NgayBaoCao.StylePriority.UseTextAlignment = false;
            this.xrl_NgayBaoCao.Text = "Từ ngày 14/3/2013 đến ngày 14/3/2013";
            this.xrl_NgayBaoCao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_TenCongTy
            // 
            this.xrl_TenCongTy.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_TenCongTy.LocationFloat = new DevExpress.Utils.PointFloat(0F, 117.7083F);
            this.xrl_TenCongTy.Name = "xrl_TenCongTy";
            this.xrl_TenCongTy.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 2, 0, 0, 100F);
            this.xrl_TenCongTy.SizeF = new System.Drawing.SizeF(518.7501F, 22.99999F);
            this.xrl_TenCongTy.StylePriority.UseFont = false;
            this.xrl_TenCongTy.StylePriority.UsePadding = false;
            this.xrl_TenCongTy.StylePriority.UseTextAlignment = false;
            this.xrl_TenCongTy.Text = "CÔNG TY TNHH THƯƠNG MẠI VÀ XÂY DỰNG TRUNG CHÍNH";
            this.xrl_TenCongTy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.PageHeader.HeightF = 38.49998F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(3.973643E-05F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1083F, 38.49998F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell4,
            this.xrTableCell3,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell12,
            this.xrTableCell15});
            this.xrTableRow1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.StylePriority.UseFont = false;
            this.xrTableRow1.StylePriority.UseTextAlignment = false;
            this.xrTableRow1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "STT";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell1.Weight = 0.35416665980020634D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "Mã nhân viên";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 0.89999957485785176D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "Họ tên";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell4.Weight = 1.2999993544018953D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "Ngày sinh";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell3.Weight = 0.88333339515210119D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "Giới tính";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell5.Weight = 0.63854192110093955D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "Địa chỉ";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell6.Weight = 1.8341155871109893D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "Điện thoại";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell7.Weight = 1.039712475172728D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "Ngày vào công ty";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell8.Weight = 1.1761077314217387D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "Trình độ";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell12.Weight = 1.275410592984175D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.Text = "Chức vụ";
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell15.Weight = 1.4286072963765166D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrl_ten1,
            this.xrl_ten2,
            this.xrl_ten3,
            this.xrtngayketxuat,
            this.xrl_footer1,
            this.xrl_footer3,
            this.xrl_footer2});
            this.ReportFooter.HeightF = 194F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrl_ten1
            // 
            this.xrl_ten1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 148.5416F);
            this.xrl_ten1.Name = "xrl_ten1";
            this.xrl_ten1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten1.SizeF = new System.Drawing.SizeF(302.1819F, 23F);
            this.xrl_ten1.StylePriority.UseFont = false;
            this.xrl_ten1.StylePriority.UseTextAlignment = false;
            this.xrl_ten1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_ten2
            // 
            this.xrl_ten2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten2.LocationFloat = new DevExpress.Utils.PointFloat(374.9999F, 148.5416F);
            this.xrl_ten2.Name = "xrl_ten2";
            this.xrl_ten2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten2.SizeF = new System.Drawing.SizeF(302.1819F, 23F);
            this.xrl_ten2.StylePriority.UseFont = false;
            this.xrl_ten2.StylePriority.UseTextAlignment = false;
            this.xrl_ten2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_ten3
            // 
            this.xrl_ten3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten3.LocationFloat = new DevExpress.Utils.PointFloat(770.8332F, 148.5416F);
            this.xrl_ten3.Name = "xrl_ten3";
            this.xrl_ten3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten3.SizeF = new System.Drawing.SizeF(294.4176F, 23F);
            this.xrl_ten3.StylePriority.UseFont = false;
            this.xrl_ten3.StylePriority.UseTextAlignment = false;
            this.xrl_ten3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrtngayketxuat
            // 
            this.xrtngayketxuat.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Italic);
            this.xrtngayketxuat.LocationFloat = new DevExpress.Utils.PointFloat(748.9582F, 22.49998F);
            this.xrtngayketxuat.Name = "xrtngayketxuat";
            this.xrtngayketxuat.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrtngayketxuat.SizeF = new System.Drawing.SizeF(334.0406F, 23F);
            this.xrtngayketxuat.StylePriority.UseFont = false;
            this.xrtngayketxuat.StylePriority.UseTextAlignment = false;
            this.xrtngayketxuat.Text = "{0}, ngày {1} tháng {2} năm {3}";
            this.xrtngayketxuat.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer1
            // 
            this.xrl_footer1.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 47.49997F);
            this.xrl_footer1.Name = "xrl_footer1";
            this.xrl_footer1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer1.SizeF = new System.Drawing.SizeF(304.1828F, 23F);
            this.xrl_footer1.StylePriority.UseFont = false;
            this.xrl_footer1.StylePriority.UseTextAlignment = false;
            this.xrl_footer1.Text = "NGƯỜI LẬP";
            this.xrl_footer1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer3
            // 
            this.xrl_footer3.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer3.LocationFloat = new DevExpress.Utils.PointFloat(770.8332F, 47.49997F);
            this.xrl_footer3.Name = "xrl_footer3";
            this.xrl_footer3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer3.SizeF = new System.Drawing.SizeF(295.4991F, 23F);
            this.xrl_footer3.StylePriority.UseFont = false;
            this.xrl_footer3.StylePriority.UseTextAlignment = false;
            this.xrl_footer3.Text = "TỔNG GIÁM ĐỐC";
            this.xrl_footer3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer2
            // 
            this.xrl_footer2.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer2.LocationFloat = new DevExpress.Utils.PointFloat(374.9999F, 47.49997F);
            this.xrl_footer2.Name = "xrl_footer2";
            this.xrl_footer2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer2.SizeF = new System.Drawing.SizeF(304.1828F, 23F);
            this.xrl_footer2.StylePriority.UseFont = false;
            this.xrl_footer2.StylePriority.UseTextAlignment = false;
            this.xrl_footer2.Text = "PHÒNG HCNS";
            this.xrl_footer2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.GroupHeader1.HeightF = 25.41666F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
            this.PageFooter.HeightF = 100F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrPageInfo1.Format = "Trang {0} của {1}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(959.9583F, 76.99998F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(126.0417F, 23.00001F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // rp_EmployeeNotHaveHealthInsuranceNumber
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter,
            this.GroupHeader1,
            this.PageFooter});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(7, 7, 50, 48);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}
