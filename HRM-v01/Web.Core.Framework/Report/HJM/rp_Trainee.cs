using System;
using DevExpress.XtraReports.UI;
using Web.Core.Framework;
using Web.Core.Object.Report;
using Web.Core;
using Web.Core.Framework.Adapter;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Summary description for rp_Trainee
    /// </summary>
    public class rp_Trainee : XtraReport
    {
        private DetailBand Detail;
        private TopMarginBand TopMargin;
        private BottomMarginBand BottomMargin;
        private ReportHeaderBand ReportHeader;
        private XRLabel xrl_TenCongTy;
        private XRLabel xrl_NgayBaoCao;
        private XRLabel xrl_TitleBC;
        private PageHeaderBand PageHeader;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCell4;
        private XRTableCell xrTableCell3;
        private XRTableCell xrTableCell5;
        private XRTableCell xrTableCell6;
        private XRTableCell xrTableCell8;
        private XRTableCell xrTableCell12;
        private XRTableCell xrTableCell15;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrtOrderNumber;
        private XRTableCell xrtEmployeeCode;
        private XRTableCell xrtFullName;
        private XRTableCell xrtBirthDate;
        private XRTableCell xrtSex;
        private XRTableCell xrtAddress;
        private XRTableCell xrtRecruimentDate;
        private XRTableCell xrtJobName;
        private XRTableCell xrtPositionName;
        private ReportFooterBand ReportFooter;
        private XRLabel xrl_ten3;
        private XRLabel xrl_ten2;
        private XRLabel xrl_ten1;
        private XRLabel xrt_ReportDate;
        private XRLabel xrl_footer1;
        private XRLabel xrl_footer3;
        private XRLabel xrl_footer2;
        private GroupHeaderBand GroupHeader1;
        private GroupFooterBand GroupFooter1;
        private XRTable xrTable3;
        private XRTableRow xrTableRow3;
        private XRTableCell xrlDepartmentName;
        private XRPictureBox xrLogo;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public rp_Trainee()
        {
            InitializeComponent();
        }
        int STT;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            STT++;
            xrtOrderNumber.Text = STT.ToString();
        }
        private void Group_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            STT = 0;
            xrtOrderNumber.Text = STT.ToString();

        }
        public void BindData(ReportFilter filter)
        {
            try
            {
                ReportController rpCtr = new ReportController();

                xrl_TenCongTy.Text = rpCtr.GetCompanyName(filter.SessionDepartment);
                var arrDepartment = string.IsNullOrEmpty(filter.SelectedDepartment)
                    ? new string[] { }
                    : filter.SelectedDepartment.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < arrDepartment.Length; i++)
                {
                    arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
                }
                var control = new ReportController();
                var location = control.GetCityName(filter.SessionDepartment);
                xrt_ReportDate.Text = string.Format(xrt_ReportDate.Text, location, DateTime.Now.Day, DateTime.Now.Month,
                    DateTime.Now.Year);
                var table = SQLHelper.ExecuteTable(
                    SQLManagementAdapter.GetStore_ReportlistemployeeProbation(String.Join(",", arrDepartment)));
                DataSource = table;
                //Binding data
                xrtEmployeeCode.DataBindings.Add("Text", DataSource, "EmployeeCode");
                xrtFullName.DataBindings.Add("Text", DataSource, "FullName");
                xrtBirthDate.DataBindings.Add("Text", DataSource, "BirthDate", "{0:dd/MM/yyyy}");
                xrtSex.DataBindings.Add("Text", DataSource, "Sex");
                xrtAddress.DataBindings.Add("Text", DataSource, "Address");
                xrtRecruimentDate.DataBindings.Add("Text", DataSource, "RecruimentDate", "{0:dd/MM/yyyy}");
                xrtJobName.DataBindings.Add("Text", DataSource, "JobName");
                xrtPositionName.DataBindings.Add("Text", DataSource, "PositionName");
                GroupHeader1.GroupFields.AddRange(new[] {
                        new GroupField("DepartmentId", XRColumnSortOrder.Ascending)});
                xrlDepartmentName.DataBindings.Add("Text", DataSource, "DepartmentName");
            }
            catch(Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra " + ex.Message);
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
            string resourceFileName = "rp_Trainee.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrtOrderNumber = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtEmployeeCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtFullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtBirthDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtSex = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtAddress = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtRecruimentDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtJobName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtPositionName = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrl_TenCongTy = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NgayBaoCao = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TitleBC = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrl_ten3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ten2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ten1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrt_ReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer2 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrlDepartmentName = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
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
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1071.5F, 25.41666F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UsePadding = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrtOrderNumber,
            this.xrtEmployeeCode,
            this.xrtFullName,
            this.xrtBirthDate,
            this.xrtSex,
            this.xrtAddress,
            this.xrtRecruimentDate,
            this.xrtJobName,
            this.xrtPositionName});
            this.xrTableRow2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.StylePriority.UseFont = false;
            this.xrTableRow2.StylePriority.UseTextAlignment = false;
            this.xrTableRow2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow2.Weight = 1D;
            // 
            // xrtOrderNumber
            // 
            this.xrtOrderNumber.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtOrderNumber.Name = "xrtOrderNumber";
            this.xrtOrderNumber.StylePriority.UseFont = false;
            this.xrtOrderNumber.StylePriority.UseTextAlignment = false;
            this.xrtOrderNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtOrderNumber.Weight = 0.35416665980020634D;
            // 
            // xrtEmployeeCode
            // 
            this.xrtEmployeeCode.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtEmployeeCode.Name = "xrtEmployeeCode";
            this.xrtEmployeeCode.StylePriority.UseFont = false;
            this.xrtEmployeeCode.StylePriority.UseTextAlignment = false;
            this.xrtEmployeeCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrtEmployeeCode.Weight = 1.0018496537150026D;
            // 
            // xrtFullName
            // 
            this.xrtFullName.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtFullName.Name = "xrtFullName";
            this.xrtFullName.StylePriority.UseFont = false;
            this.xrtFullName.StylePriority.UseTextAlignment = false;
            this.xrtFullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrtFullName.Weight = 1.3513321490219215D;
            // 
            // xrtBirthDate
            // 
            this.xrtBirthDate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtBirthDate.Name = "xrtBirthDate";
            this.xrtBirthDate.StylePriority.UseFont = false;
            this.xrtBirthDate.StylePriority.UseTextAlignment = false;
            this.xrtBirthDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtBirthDate.Weight = 1.0504393924663653D;
            // 
            // xrtSex
            // 
            this.xrtSex.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtSex.Name = "xrtSex";
            this.xrtSex.StylePriority.UseFont = false;
            this.xrtSex.StylePriority.UseTextAlignment = false;
            this.xrtSex.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtSex.Weight = 0.61383325241460251D;
            // 
            // xrtAddress
            // 
            this.xrtAddress.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtAddress.Name = "xrtAddress";
            this.xrtAddress.StylePriority.UseFont = false;
            this.xrtAddress.StylePriority.UseTextAlignment = false;
            this.xrtAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrtAddress.Weight = 2.2163704830282653D;
            // 
            // xrtRecruimentDate
            // 
            this.xrtRecruimentDate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtRecruimentDate.Name = "xrtRecruimentDate";
            this.xrtRecruimentDate.StylePriority.UseFont = false;
            this.xrtRecruimentDate.StylePriority.UseTextAlignment = false;
            this.xrtRecruimentDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtRecruimentDate.Weight = 1.1016420686709785D;
            // 
            // xrtJobName
            // 
            this.xrtJobName.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtJobName.Name = "xrtJobName";
            this.xrtJobName.StylePriority.UseFont = false;
            this.xrtJobName.StylePriority.UseTextAlignment = false;
            this.xrtJobName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrtJobName.Weight = 1.4113269749886375D;
            // 
            // xrtPositionName
            // 
            this.xrtPositionName.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtPositionName.Name = "xrtPositionName";
            this.xrtPositionName.StylePriority.UseFont = false;
            this.xrtPositionName.StylePriority.UseTextAlignment = false;
            this.xrtPositionName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrtPositionName.Weight = 1.6338582386491096D;
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
            this.BottomMargin.HeightF = 47F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLogo,
            this.xrl_TenCongTy,
            this.xrl_NgayBaoCao,
            this.xrl_TitleBC});
            this.ReportHeader.HeightF = 209F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLogo
            // 
            this.xrLogo.LocationFloat = new DevExpress.Utils.PointFloat(56.20833F, 0F);
            this.xrLogo.Name = "xrLogo";
            this.xrLogo.Padding = new DevExpress.XtraPrinting.PaddingInfo(1, 1, 1, 1, 100F);
            this.xrLogo.SizeF = new System.Drawing.SizeF(110F, 110F);
            this.xrLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrLogo.StylePriority.UsePadding = false;
            // 
            // xrl_TenCongTy
            // 
            this.xrl_TenCongTy.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_TenCongTy.LocationFloat = new DevExpress.Utils.PointFloat(0F, 114F);
            this.xrl_TenCongTy.Name = "xrl_TenCongTy";
            this.xrl_TenCongTy.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 2, 0, 0, 100F);
            this.xrl_TenCongTy.SizeF = new System.Drawing.SizeF(544.7917F, 23F);
            this.xrl_TenCongTy.StylePriority.UseFont = false;
            this.xrl_TenCongTy.StylePriority.UsePadding = false;
            this.xrl_TenCongTy.StylePriority.UseTextAlignment = false;
            this.xrl_TenCongTy.Text = "CÔNG TY TNHH THƯƠNG MẠI VÀ XÂY DỰNG TRUNG CHÍNH";
            this.xrl_TenCongTy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NgayBaoCao
            // 
            this.xrl_NgayBaoCao.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic);
            this.xrl_NgayBaoCao.LocationFloat = new DevExpress.Utils.PointFloat(0F, 179F);
            this.xrl_NgayBaoCao.Name = "xrl_NgayBaoCao";
            this.xrl_NgayBaoCao.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_NgayBaoCao.SizeF = new System.Drawing.SizeF(1071.5F, 23.00001F);
            this.xrl_NgayBaoCao.StylePriority.UseFont = false;
            this.xrl_NgayBaoCao.StylePriority.UseTextAlignment = false;
            this.xrl_NgayBaoCao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_TitleBC
            // 
            this.xrl_TitleBC.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_TitleBC.LocationFloat = new DevExpress.Utils.PointFloat(0F, 154F);
            this.xrl_TitleBC.Name = "xrl_TitleBC";
            this.xrl_TitleBC.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_TitleBC.SizeF = new System.Drawing.SizeF(1071.5F, 23F);
            this.xrl_TitleBC.StylePriority.UseFont = false;
            this.xrl_TitleBC.StylePriority.UseTextAlignment = false;
            this.xrl_TitleBC.Text = "BÁO CÁO DANH SÁCH NHÂN VIÊN THỬ VIỆC";
            this.xrl_TitleBC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.PageHeader.HeightF = 48.33333F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1071.5F, 48.33333F);
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
            this.xrTableCell8,
            this.xrTableCell12,
            this.xrTableCell15});
            this.xrTableRow1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 5, 0, 100F);
            this.xrTableRow1.StylePriority.UseFont = false;
            this.xrTableRow1.StylePriority.UsePadding = false;
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
            this.xrTableCell2.Multiline = true;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "Mã nhân viên";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 1.0018496351292998D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "Họ tên";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell4.Weight = 1.3513413076263903D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "Ngày sinh";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell3.Weight = 1.0504394071054914D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "Giới tính";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell5.Weight = 0.61383324139272166D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "Địa chỉ";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell6.Weight = 2.2163611864636534D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Multiline = true;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "Ngày thử việc";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell8.Weight = 1.1016420451433482D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "Vị trí công việc";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell12.Weight = 1.4113275588114178D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.Text = "Chức vụ";
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell15.Weight = 1.6338578312825594D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrl_ten3,
            this.xrl_ten2,
            this.xrl_ten1,
            this.xrt_ReportDate,
            this.xrl_footer1,
            this.xrl_footer3,
            this.xrl_footer2});
            this.ReportFooter.HeightF = 228F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrl_ten3
            // 
            this.xrl_ten3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten3.LocationFloat = new DevExpress.Utils.PointFloat(789.5834F, 169.375F);
            this.xrl_ten3.Name = "xrl_ten3";
            this.xrl_ten3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten3.SizeF = new System.Drawing.SizeF(282.4167F, 23F);
            this.xrl_ten3.StylePriority.UseFont = false;
            this.xrl_ten3.StylePriority.UseTextAlignment = false;
            this.xrl_ten3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_ten2
            // 
            this.xrl_ten2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten2.LocationFloat = new DevExpress.Utils.PointFloat(377.0833F, 169.375F);
            this.xrl_ten2.Name = "xrl_ten2";
            this.xrl_ten2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten2.SizeF = new System.Drawing.SizeF(302.1819F, 23F);
            this.xrl_ten2.StylePriority.UseFont = false;
            this.xrl_ten2.StylePriority.UseTextAlignment = false;
            this.xrl_ten2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_ten1
            // 
            this.xrl_ten1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten1.LocationFloat = new DevExpress.Utils.PointFloat(2.083333F, 169.375F);
            this.xrl_ten1.Name = "xrl_ten1";
            this.xrl_ten1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten1.SizeF = new System.Drawing.SizeF(302.1819F, 23F);
            this.xrl_ten1.StylePriority.UseFont = false;
            this.xrl_ten1.StylePriority.UseTextAlignment = false;
            this.xrl_ten1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrt_ReportDate
            // 
            this.xrt_ReportDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Italic);
            this.xrt_ReportDate.LocationFloat = new DevExpress.Utils.PointFloat(789.5833F, 22.50004F);
            this.xrt_ReportDate.Name = "xrt_ReportDate";
            this.xrt_ReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrt_ReportDate.SizeF = new System.Drawing.SizeF(282.4167F, 23F);
            this.xrt_ReportDate.StylePriority.UseFont = false;
            this.xrt_ReportDate.StylePriority.UseTextAlignment = false;
            this.xrt_ReportDate.Text = "{0}, ngày {1} tháng {2} năm {3}";
            this.xrt_ReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer1
            // 
            this.xrl_footer1.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer1.LocationFloat = new DevExpress.Utils.PointFloat(2.083333F, 47.50004F);
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
            this.xrl_footer3.LocationFloat = new DevExpress.Utils.PointFloat(789.5833F, 47.50004F);
            this.xrl_footer3.Name = "xrl_footer3";
            this.xrl_footer3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer3.SizeF = new System.Drawing.SizeF(281.9166F, 23F);
            this.xrl_footer3.StylePriority.UseFont = false;
            this.xrl_footer3.StylePriority.UseTextAlignment = false;
            this.xrl_footer3.Text = "GIÁM ĐỐC";
            this.xrl_footer3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer2
            // 
            this.xrl_footer2.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer2.LocationFloat = new DevExpress.Utils.PointFloat(377.0833F, 47.50004F);
            this.xrl_footer2.Name = "xrl_footer2";
            this.xrl_footer2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer2.SizeF = new System.Drawing.SizeF(304.1828F, 23F);
            this.xrl_footer2.StylePriority.UseFont = false;
            this.xrl_footer2.StylePriority.UseTextAlignment = false;
            this.xrl_footer2.Text = "PHÒNG TCHC";
            this.xrl_footer2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.GroupHeader1.HeightF = 26F;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Group_BeforePrint);
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0.4999558F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1071.5F, 25.41666F);
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UsePadding = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrlDepartmentName});
            this.xrTableRow3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.StylePriority.UseFont = false;
            this.xrTableRow3.StylePriority.UseTextAlignment = false;
            this.xrTableRow3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow3.Weight = 1D;
            // 
            // xrlDepartmentName
            // 
            this.xrlDepartmentName.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrlDepartmentName.Name = "xrlDepartmentName";
            this.xrlDepartmentName.Padding = new DevExpress.XtraPrinting.PaddingInfo(11, 3, 3, 3, 100F);
            this.xrlDepartmentName.StylePriority.UseFont = false;
            this.xrlDepartmentName.StylePriority.UsePadding = false;
            this.xrlDepartmentName.StylePriority.UseTextAlignment = false;
            this.xrlDepartmentName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrlDepartmentName.Weight = 10.73481887275509D;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.HeightF = 100F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // rp_Trainee
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter,
            this.GroupHeader1,
            this.GroupFooter1});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(12, 16, 51, 47);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}