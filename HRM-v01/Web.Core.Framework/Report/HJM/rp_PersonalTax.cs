using System;
using DevExpress.XtraReports.UI;
using Web.Core.Framework.Adapter;
using Web.Core.Object.Report;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Summary description for rp_PersonalTax
    /// </summary>
    public class rp_PersonalTax : XtraReport
    {
        private DetailBand Detail;
        private TopMarginBand TopMargin;
        private BottomMarginBand BottomMargin;
        private ReportHeaderBand ReportHeader;
        private PageHeaderBand PageHeader;
        private ReportFooterBand ReportFooter;
        private GroupHeaderBand GroupHeader1;
        private XRLabel xrl_TitleBC;
        private XRLabel lblReportDate;
        private XRLabel xrl_TenCongTy;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCell4;
        private XRTableCell xrTableCell5;
        private XRTableCell xrTableCell15;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrTableCellIndex;
        private XRTableCell xrTableCellEmployeeCode;
        private XRTableCell xrTableCellFullName;
        private XRTableCell xrTableCellPositionName;
        private XRTableCell xrTableCellPersonalTax;
        private XRTable xrTable3;
        private XRTableRow xrTableRow3;
        private XRTableCell xrTableCell16;
        private XRTableCell xrTableCellDepartment;
        private XRLabel lblInformationFooter;
        private XRLabel xrl_footer3;
        private XRLabel xrl_ten3;
        private XRLabel xrl_footer1;
        private XRLabel xrl_ten1;
        private XRTableCell xrTableCell3;
        private XRTableCell xrTableCellGender;
        private XRTableCell xrTableCell10;
        private XRTableCell xrTableCellBirthDate;
        private XRPictureBox xrLogo;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public rp_PersonalTax()
        {
            InitializeComponent();
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

        private int _stt;

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _stt++;
            xrTableCellIndex.Text = _stt.ToString();

        }

        private void Group_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _stt = 0;
            xrTableCellIndex.Text = _stt.ToString();

        }

        public void BindData(ReportFilter filter)
        {
            try
            {
                var control = new ReportController();
                xrTableCellDepartment.Text = control.GetCompanyName(filter.SessionDepartment);

                //var toDate = new DateTime(filter.Year, filter.StartMonth, DateTime.DaysInMonth(filter.Year, filter.StartMonth));
                var toDate = filter.ReportedDate;
                lblReportDate.Text = string.Format(lblReportDate.Text, toDate.Day, toDate.Month, toDate.Year);

                var location = new ReportController().GetCityName(filter.SessionDepartment);
                lblInformationFooter.Text = string.Format(lblInformationFooter.Text, location, DateTime.Now.Day,
                    DateTime.Now.Month, DateTime.Now.Year);

                // get organization
                var organization = cat_DepartmentServices.GetByDepartments(filter.SessionDepartment);
                if (organization == null) return;
                var departments = filter.SelectedDepartment;
                var arrDepartment = departments.Split(new[] {','}, StringSplitOptions.None);
                for (var i = 0; i < arrDepartment.Length; i++)
                {
                    arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
                }

                var table = SQLHelper.ExecuteTable(
                    SQLManagementAdapter.GetStore_ListEmployeeHavePersonalTaxCode(string.Join(",", arrDepartment),
                        filter.WhereClause));
                DataSource = table;

                //binding data
                GroupHeader1.GroupFields.AddRange(new[]
                {
                    new GroupField("DepartmentId", XRColumnSortOrder.Ascending)
                });
                xrTableCellDepartment.DataBindings.Add("Text", DataSource, "DepartmentName");
                xrTableCellEmployeeCode.DataBindings.Add("Text", DataSource, "EmployeeCode");
                xrTableCellFullName.DataBindings.Add("Text", DataSource, "FullName");
                xrTableCellGender.DataBindings.Add("Text", DataSource, "Gender");
                xrTableCellBirthDate.DataBindings.Add("Text", DataSource, "BirthDate", "{0:dd/MM/yyyy}");
                xrTableCellPositionName.DataBindings.Add("Text", DataSource, "PositionName");
                xrTableCellPersonalTax.DataBindings.Add("Text", DataSource, "PersonalTaxCode");
            }
            catch
            {
            }

        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            string resourceFileName = "rp_PersonalTax.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellIndex = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellEmployeeCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellFullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellGender = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellBirthDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellPositionName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellPersonalTax = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrl_TitleBC = new DevExpress.XtraReports.UI.XRLabel();
            this.lblReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TenCongTy = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblInformationFooter = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ten3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ten1 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDepartment = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize) (this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrTable2
            });
            this.Detail.HeightF = 38.33331F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders =
                ((DevExpress.XtraPrinting.BorderSide) (((DevExpress.XtraPrinting.BorderSide.Left |
                                                         DevExpress.XtraPrinting.BorderSide.Right)
                                                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[]
            {
                this.xrTableRow2
            });
            this.xrTable2.SizeF = new System.Drawing.SizeF(709F, 38.33331F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[]
            {
                this.xrTableCellIndex,
                this.xrTableCellEmployeeCode,
                this.xrTableCellFullName,
                this.xrTableCellGender,
                this.xrTableCellBirthDate,
                this.xrTableCellPositionName,
                this.xrTableCellPersonalTax
            });
            this.xrTableRow2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.StylePriority.UseFont = false;
            this.xrTableRow2.StylePriority.UseTextAlignment = false;
            this.xrTableRow2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCellIndex
            // 
            this.xrTableCellIndex.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrTableCellIndex.Name = "xrTableCellIndex";
            this.xrTableCellIndex.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellIndex.StylePriority.UseFont = false;
            this.xrTableCellIndex.StylePriority.UsePadding = false;
            this.xrTableCellIndex.StylePriority.UseTextAlignment = false;
            this.xrTableCellIndex.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellIndex.Weight = 0.36394242884472094D;
            this.xrTableCellIndex.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrTableCellEmployeeCode
            // 
            this.xrTableCellEmployeeCode.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrTableCellEmployeeCode.Name = "xrTableCellEmployeeCode";
            this.xrTableCellEmployeeCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellEmployeeCode.StylePriority.UseFont = false;
            this.xrTableCellEmployeeCode.StylePriority.UsePadding = false;
            this.xrTableCellEmployeeCode.StylePriority.UseTextAlignment = false;
            this.xrTableCellEmployeeCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellEmployeeCode.Weight = 0.87708511804611355D;
            // 
            // xrTableCellFullName
            // 
            this.xrTableCellFullName.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrTableCellFullName.Name = "xrTableCellFullName";
            this.xrTableCellFullName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellFullName.StylePriority.UseFont = false;
            this.xrTableCellFullName.StylePriority.UsePadding = false;
            this.xrTableCellFullName.StylePriority.UseTextAlignment = false;
            this.xrTableCellFullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellFullName.Weight = 1.5302930731074416D;
            // 
            // xrTableCellGender
            // 
            this.xrTableCellGender.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrTableCellGender.Name = "xrTableCellGender";
            this.xrTableCellGender.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellGender.StylePriority.UseFont = false;
            this.xrTableCellGender.StylePriority.UsePadding = false;
            this.xrTableCellGender.StylePriority.UseTextAlignment = false;
            this.xrTableCellGender.Text = " ";
            this.xrTableCellGender.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellGender.Weight = 0.4894004166718906D;
            // 
            // xrTableCellBirthDate
            // 
            this.xrTableCellBirthDate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrTableCellBirthDate.Name = "xrTableCellBirthDate";
            this.xrTableCellBirthDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellBirthDate.StylePriority.UseFont = false;
            this.xrTableCellBirthDate.StylePriority.UsePadding = false;
            this.xrTableCellBirthDate.StylePriority.UseTextAlignment = false;
            this.xrTableCellBirthDate.Text = " ";
            this.xrTableCellBirthDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellBirthDate.Weight = 0.9143413356737965D;
            // 
            // xrTableCellPositionName
            // 
            this.xrTableCellPositionName.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrTableCellPositionName.Name = "xrTableCellPositionName";
            this.xrTableCellPositionName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellPositionName.StylePriority.UseFont = false;
            this.xrTableCellPositionName.StylePriority.UsePadding = false;
            this.xrTableCellPositionName.StylePriority.UseTextAlignment = false;
            this.xrTableCellPositionName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellPositionName.Weight = 1.373801610228448D;
            // 
            // xrTableCellPersonalTax
            // 
            this.xrTableCellPersonalTax.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrTableCellPersonalTax.Name = "xrTableCellPersonalTax";
            this.xrTableCellPersonalTax.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellPersonalTax.StylePriority.UseFont = false;
            this.xrTableCellPersonalTax.StylePriority.UsePadding = false;
            this.xrTableCellPersonalTax.StylePriority.UseTextAlignment = false;
            this.xrTableCellPersonalTax.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellPersonalTax.Weight = 1.104920134499596D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 100F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 100F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrLogo,
                this.xrl_TitleBC,
                this.lblReportDate,
                this.xrl_TenCongTy
            });
            this.ReportHeader.HeightF = 227F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLogo
            // 
            this.xrLogo.LocationFloat = new DevExpress.Utils.PointFloat(57.29167F, 0F);
            this.xrLogo.Name = "xrLogo";
            this.xrLogo.Padding = new DevExpress.XtraPrinting.PaddingInfo(1, 1, 1, 1, 100F);
            this.xrLogo.SizeF = new System.Drawing.SizeF(110F, 110F);
            this.xrLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrLogo.StylePriority.UsePadding = false;
            // 
            // xrl_TitleBC
            // 
            this.xrl_TitleBC.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_TitleBC.LocationFloat = new DevExpress.Utils.PointFloat(0F, 148F);
            this.xrl_TitleBC.Name = "xrl_TitleBC";
            this.xrl_TitleBC.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_TitleBC.SizeF = new System.Drawing.SizeF(662F, 23F);
            this.xrl_TitleBC.StylePriority.UseFont = false;
            this.xrl_TitleBC.StylePriority.UseTextAlignment = false;
            this.xrl_TitleBC.Text = "BÁO CÁO DANH SÁCH NHÂN VIÊN - MÃ SỐ THUẾ CÁ NHÂN";
            this.xrl_TitleBC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // lblReportDate
            // 
            this.lblReportDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Italic);
            this.lblReportDate.LocationFloat = new DevExpress.Utils.PointFloat(0F, 173F);
            this.lblReportDate.Name = "lblReportDate";
            this.lblReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblReportDate.SizeF = new System.Drawing.SizeF(662F, 23F);
            this.lblReportDate.StylePriority.UseFont = false;
            this.lblReportDate.StylePriority.UseTextAlignment = false;
            this.lblReportDate.Text = "Ngày {0} tháng {1} năm {2}";
            this.lblReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_TenCongTy
            // 
            this.xrl_TenCongTy.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_TenCongTy.LocationFloat = new DevExpress.Utils.PointFloat(6.357829E-05F, 111.125F);
            this.xrl_TenCongTy.Name = "xrl_TenCongTy";
            this.xrl_TenCongTy.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 2, 0, 0, 100F);
            this.xrl_TenCongTy.SizeF = new System.Drawing.SizeF(486.8489F, 23F);
            this.xrl_TenCongTy.StylePriority.UseFont = false;
            this.xrl_TenCongTy.StylePriority.UsePadding = false;
            this.xrl_TenCongTy.StylePriority.UseTextAlignment = false;
            this.xrl_TenCongTy.Text = "CÔNG TY CỔ PHẦN CÔNG NGHỆ DTH VÀ GIẢI PHÁP SỐ";
            this.xrl_TenCongTy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrTable1
            });
            this.PageHeader.HeightF = 38.33331F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders =
                ((DevExpress.XtraPrinting.BorderSide) ((((DevExpress.XtraPrinting.BorderSide.Left |
                                                          DevExpress.XtraPrinting.BorderSide.Top)
                                                         | DevExpress.XtraPrinting.BorderSide.Right)
                                                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(6.357829E-05F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[]
            {
                this.xrTableRow1
            });
            this.xrTable1.SizeF = new System.Drawing.SizeF(708.9999F, 38.33331F);
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
                this.xrTableCell2,
                this.xrTableCell4,
                this.xrTableCell3,
                this.xrTableCell10,
                this.xrTableCell5,
                this.xrTableCell15
            });
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
            this.xrTableCell1.Weight = 0.36394242884472094D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "Mã CB";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 0.87708511804611355D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "Họ và tên";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell4.Weight = 1.5302925003080716D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "Giới tính";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell3.Weight = 0.48940034507196928D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.Text = "Ngày sinh";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell10.Weight = 0.91434104927411164D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "Chức vụ";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell5.Weight = 1.373801968228054D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.Text = "Mã số thuế cá nhân";
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell15.Weight = 1.1049201344995963D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.lblInformationFooter,
                this.xrl_footer3,
                this.xrl_ten3,
                this.xrl_footer1,
                this.xrl_ten1
            });
            this.ReportFooter.HeightF = 206F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // lblInformationFooter
            // 
            this.lblInformationFooter.Font =
                new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Italic);
            this.lblInformationFooter.LocationFloat = new DevExpress.Utils.PointFloat(361.2365F, 28.125F);
            this.lblInformationFooter.Name = "lblInformationFooter";
            this.lblInformationFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblInformationFooter.SizeF = new System.Drawing.SizeF(296.5F, 23F);
            this.lblInformationFooter.StylePriority.UseFont = false;
            this.lblInformationFooter.StylePriority.UseTextAlignment = false;
            this.lblInformationFooter.Text = "{0}, ngày {1} tháng {2} năm {3}";
            this.lblInformationFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer3
            // 
            this.xrl_footer3.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer3.LocationFloat = new DevExpress.Utils.PointFloat(361.2365F, 53.125F);
            this.xrl_footer3.Name = "xrl_footer3";
            this.xrl_footer3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer3.SizeF = new System.Drawing.SizeF(296.5002F, 23F);
            this.xrl_footer3.StylePriority.UseFont = false;
            this.xrl_footer3.StylePriority.UseTextAlignment = false;
            this.xrl_footer3.Text = "GIÁM ĐỐC";
            this.xrl_footer3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_ten3
            // 
            this.xrl_ten3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten3.LocationFloat = new DevExpress.Utils.PointFloat(361.2365F, 153.125F);
            this.xrl_ten3.Name = "xrl_ten3";
            this.xrl_ten3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten3.SizeF = new System.Drawing.SizeF(296.5F, 23F);
            this.xrl_ten3.StylePriority.UseFont = false;
            this.xrl_ten3.StylePriority.UseTextAlignment = false;
            this.xrl_ten3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer1
            // 
            this.xrl_footer1.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer1.LocationFloat = new DevExpress.Utils.PointFloat(5.736574F, 53.125F);
            this.xrl_footer1.Name = "xrl_footer1";
            this.xrl_footer1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer1.SizeF = new System.Drawing.SizeF(304.1828F, 23F);
            this.xrl_footer1.StylePriority.UseFont = false;
            this.xrl_footer1.StylePriority.UseTextAlignment = false;
            this.xrl_footer1.Text = "NGƯỜI LẬP";
            this.xrl_footer1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_ten1
            // 
            this.xrl_ten1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten1.LocationFloat = new DevExpress.Utils.PointFloat(5.736574F, 153.125F);
            this.xrl_ten1.Name = "xrl_ten1";
            this.xrl_ten1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten1.SizeF = new System.Drawing.SizeF(302.1819F, 23F);
            this.xrl_ten1.StylePriority.UseFont = false;
            this.xrl_ten1.StylePriority.UseTextAlignment = false;
            this.xrl_ten1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrTable3
            });
            this.GroupHeader1.HeightF = 25.41666F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable3
            // 
            this.xrTable3.Borders =
                ((DevExpress.XtraPrinting.BorderSide) (((DevExpress.XtraPrinting.BorderSide.Left |
                                                         DevExpress.XtraPrinting.BorderSide.Right)
                                                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[]
            {
                this.xrTableRow3
            });
            this.xrTable3.SizeF = new System.Drawing.SizeF(709F, 25.41666F);
            this.xrTable3.SnapLineMargin = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseFont = false;
            this.xrTable3.StylePriority.UsePadding = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[]
            {
                this.xrTableCell16,
                this.xrTableCellDepartment
            });
            this.xrTableRow3.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.StylePriority.UseFont = false;
            this.xrTableRow3.StylePriority.UseTextAlignment = false;
            this.xrTableRow3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Borders =
                ((DevExpress.XtraPrinting.BorderSide) ((DevExpress.XtraPrinting.BorderSide.Left |
                                                        DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseBorders = false;
            this.xrTableCell16.Weight = 0.099999969401147248D;
            // 
            // xrTableCellDepartment
            // 
            this.xrTableCellDepartment.Borders =
                ((DevExpress.XtraPrinting.BorderSide) ((DevExpress.XtraPrinting.BorderSide.Right |
                                                        DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDepartment.Font =
                new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCellDepartment.Name = "xrTableCellDepartment";
            this.xrTableCellDepartment.StylePriority.UseBorders = false;
            this.xrTableCellDepartment.StylePriority.UseFont = false;
            this.xrTableCellDepartment.StylePriority.UseTextAlignment = false;
            this.xrTableCellDepartment.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCellDepartment.Weight = 12.259303663754043D;
            // 
            // rp_PersonalTax
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[]
            {
                this.Detail,
                this.TopMargin,
                this.BottomMargin,
                this.ReportHeader,
                this.PageHeader,
                this.ReportFooter,
                this.GroupHeader1
            });
            this.Margins = new System.Drawing.Printing.Margins(63, 55, 100, 100);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize) (this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this)).EndInit();

        }

        #endregion
    }
}