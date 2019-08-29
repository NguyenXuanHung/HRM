using System;
using DevExpress.XtraReports.UI;
using Web.Core.Framework.Adapter;
using Web.Core.Object.Report;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Summary description for rp_ListEmployeeDependence
    /// </summary>
    public class rp_FamilyRelation : XtraReport
    {
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private ReportHeaderBand ReportHeader;
        private PageHeaderBand PageHeader;
        private ReportFooterBand ReportFooter;
        private XRLabel xrl_TitleBC;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell4;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell7;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCell12;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrCellIndex;
        private XRTableCell xrt_hoten;
        private XRTableCell xrCellEmployeeCode;
        private XRTableCell xrCellFullName;
        private XRTableCell xrt_moiquanhe;
        private XRTableCell xrt_nghenghiep;
        private XRTableCell xrCellNote;
        private XRTableCell xrTableCell5;
        private XRLabel lblReportDate;
        private XRTableCell xrCellDependenceNumber;
        private XRTableCell xrCellDepartmentName;
        private XRLabel xrl_footer3;
        private XRLabel xrl_footer2;
        private XRLabel lblCreator;
        private XRLabel lblHRDepartment;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public rp_FamilyRelation()
        {
            InitializeComponent();
        }

        int STT = 1;

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrCellIndex.Text = STT.ToString();
            STT++;
        }

        public void BindData(ReportFilter filter)
        {
            try
            {
                const string hrDepartment = @"Trần Thị Việt Hồng";
                const string creator = @"Trần Thị Thu";

                var toDate = filter.ReportedDate;
                lblReportDate.Text = string.Format(lblReportDate.Text, toDate.Day, toDate.Month, toDate.Year);

                lblHRDepartment.Text = hrDepartment;
                lblCreator.Text = creator;
                // get organization
                var organization = cat_DepartmentServices.GetByDepartments(filter.SessionDepartment);
                if (organization == null) return;
                var departments = filter.SelectedDepartment;
                var arrDepartment = departments.Split(new[] { ',' }, StringSplitOptions.None);
                for (var i = 0; i < arrDepartment.Length; i++)
                {
                    arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
                }

                var table = SQLHelper.ExecuteTable(
                    SQLManagementAdapter.GetStore_ListEmployeeFamilyRelation(filter.RecordId,
                        string.Join(",", arrDepartment), filter.WhereClause));
                DataSource = table;

                //binding data
                xrCellEmployeeCode.DataBindings.Add("Text", DataSource, "EmployeeCode");
                xrCellFullName.DataBindings.Add("Text", DataSource, "FullName");
                xrCellDependenceNumber.DataBindings.Add("Text", DataSource, "DependenceNumber");
                xrCellDepartmentName.DataBindings.Add("Text", DataSource, "DepartmentName");
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra ", ex.Message);
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
            string resourceFileName = "rp_FamilyRelation.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCellIndex = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellEmployeeCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellFullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellDependenceNumber = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellDepartmentName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellNote = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.lblReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TitleBC = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblCreator = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHRDepartment = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer2 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrTable2
            });
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders =
                ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left |
                                                         DevExpress.XtraPrinting.BorderSide.Right)
                                                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[]
            {
                this.xrTableRow2
            });
            this.xrTable2.SizeF = new System.Drawing.SizeF(1044.958F, 25F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[]
            {
                this.xrCellIndex,
                this.xrCellEmployeeCode,
                this.xrCellFullName,
                this.xrCellDependenceNumber,
                this.xrCellDepartmentName,
                this.xrCellNote
            });
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrCellIndex
            // 
            this.xrCellIndex.Name = "xrCellIndex";
            this.xrCellIndex.StylePriority.UseTextAlignment = false;
            this.xrCellIndex.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellIndex.Weight = 0.042522734619797171D;
            this.xrCellIndex.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrCellEmployeeCode
            // 
            this.xrCellEmployeeCode.Name = "xrCellEmployeeCode";
            this.xrCellEmployeeCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellEmployeeCode.StylePriority.UsePadding = false;
            this.xrCellEmployeeCode.StylePriority.UseTextAlignment = false;
            this.xrCellEmployeeCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellEmployeeCode.Weight = 0.15210062632219851D;
            // 
            // xrCellFullName
            // 
            this.xrCellFullName.Name = "xrCellFullName";
            this.xrCellFullName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellFullName.StylePriority.UsePadding = false;
            this.xrCellFullName.StylePriority.UseTextAlignment = false;
            this.xrCellFullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellFullName.Weight = 0.38185195964191054D;
            // 
            // xrCellDependenceNumber
            // 
            this.xrCellDependenceNumber.Name = "xrCellDependenceNumber";
            this.xrCellDependenceNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellDependenceNumber.StylePriority.UsePadding = false;
            this.xrCellDependenceNumber.StylePriority.UseTextAlignment = false;
            this.xrCellDependenceNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellDependenceNumber.Weight = 0.093906706238515075D;
            // 
            // xrCellDepartmentName
            // 
            this.xrCellDepartmentName.Name = "xrCellDepartmentName";
            this.xrCellDepartmentName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellDepartmentName.StylePriority.UsePadding = false;
            this.xrCellDepartmentName.StylePriority.UseTextAlignment = false;
            this.xrCellDepartmentName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellDepartmentName.Weight = 0.36888023088185917D;
            // 
            // xrCellNote
            // 
            this.xrCellNote.Name = "xrCellNote";
            this.xrCellNote.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellNote.StylePriority.UsePadding = false;
            this.xrCellNote.StylePriority.UseTextAlignment = false;
            this.xrCellNote.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCellNote.Weight = 0.16710401221102203D;
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
            this.BottomMargin.HeightF = 54F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.lblReportDate,
                this.xrl_TitleBC
            });
            this.ReportHeader.HeightF = 95.00002F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // lblReportDate
            // 
            this.lblReportDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.lblReportDate.LocationFloat = new DevExpress.Utils.PointFloat(0F, 53.70833F);
            this.lblReportDate.Name = "lblReportDate";
            this.lblReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblReportDate.SizeF = new System.Drawing.SizeF(1044.958F, 23F);
            this.lblReportDate.StylePriority.UseFont = false;
            this.lblReportDate.StylePriority.UseTextAlignment = false;
            this.lblReportDate.Text = "(Thời gian cập nhật {0}/{1}/{2})";
            this.lblReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrl_TitleBC
            // 
            this.xrl_TitleBC.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_TitleBC.LocationFloat = new DevExpress.Utils.PointFloat(0F, 20.29165F);
            this.xrl_TitleBC.Name = "xrl_TitleBC";
            this.xrl_TitleBC.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_TitleBC.SizeF = new System.Drawing.SizeF(1044.958F, 23F);
            this.xrl_TitleBC.StylePriority.UseFont = false;
            this.xrl_TitleBC.StylePriority.UseTextAlignment = false;
            this.xrl_TitleBC.Text = "DANH SÁCH NGƯỜI PHỤ THUỘC CBCNV CÔNG TY";
            this.xrl_TitleBC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrTable1
            });
            this.PageHeader.HeightF = 34.625F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders =
                ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left |
                                                          DevExpress.XtraPrinting.BorderSide.Top)
                                                         | DevExpress.XtraPrinting.BorderSide.Right)
                                                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[]
            {
                this.xrTableRow1
            });
            this.xrTable1.SizeF = new System.Drawing.SizeF(1044.958F, 34.625F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[]
            {
                this.xrTableCell4,
                this.xrTableCell1,
                this.xrTableCell7,
                this.xrTableCell2,
                this.xrTableCell5,
                this.xrTableCell12
            });
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "STT";
            this.xrTableCell4.Weight = 0.058292275961961286D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "MÃ NV";
            this.xrTableCell1.Weight = 0.20850712998973459D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Text = "HỌ VÀ TÊN";
            this.xrTableCell7.Weight = 0.523461453780482D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "SỐ NPT";
            this.xrTableCell2.Weight = 0.12873203386011614D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "PHÒNG BAN";
            this.xrTableCell5.Weight = 0.50567933710338664D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.Text = "Ghi chú";
            this.xrTableCell12.Weight = 0.22907433001181676D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.lblCreator,
                this.lblHRDepartment,
                this.xrl_footer3,
                this.xrl_footer2
            });
            this.ReportFooter.HeightF = 175F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // lblCreator
            // 
            this.lblCreator.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblCreator.LocationFloat = new DevExpress.Utils.PointFloat(474.6993F, 137.5F);
            this.lblCreator.Name = "lblCreator";
            this.lblCreator.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCreator.SizeF = new System.Drawing.SizeF(570.2588F, 23F);
            this.lblCreator.StylePriority.UseFont = false;
            this.lblCreator.StylePriority.UseTextAlignment = false;
            this.lblCreator.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // lblHRDepartment
            // 
            this.lblHRDepartment.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblHRDepartment.LocationFloat = new DevExpress.Utils.PointFloat(0F, 137.5F);
            this.lblHRDepartment.Name = "lblHRDepartment";
            this.lblHRDepartment.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblHRDepartment.SizeF = new System.Drawing.SizeF(474.6993F, 23F);
            this.lblHRDepartment.StylePriority.UseFont = false;
            this.lblHRDepartment.StylePriority.UseTextAlignment = false;
            this.lblHRDepartment.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer3
            // 
            this.xrl_footer3.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer3.LocationFloat = new DevExpress.Utils.PointFloat(474.6993F, 37.5F);
            this.xrl_footer3.Name = "xrl_footer3";
            this.xrl_footer3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer3.SizeF = new System.Drawing.SizeF(570.2587F, 23F);
            this.xrl_footer3.StylePriority.UseFont = false;
            this.xrl_footer3.StylePriority.UseTextAlignment = false;
            this.xrl_footer3.Text = "NGƯỜI LẬP";
            this.xrl_footer3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer2
            // 
            this.xrl_footer2.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 37.5F);
            this.xrl_footer2.Name = "xrl_footer2";
            this.xrl_footer2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer2.SizeF = new System.Drawing.SizeF(474.6993F, 23F);
            this.xrl_footer2.StylePriority.UseFont = false;
            this.xrl_footer2.StylePriority.UseTextAlignment = false;
            this.xrl_footer2.Text = "PHÒNG NHÂN SỰ";
            this.xrl_footer2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // rp_FamilyRelation
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[]
            {
                this.Detail,
                this.TopMargin,
                this.BottomMargin,
                this.ReportHeader,
                this.PageHeader,
                this.ReportFooter
            });
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(58, 64, 50, 54);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}
