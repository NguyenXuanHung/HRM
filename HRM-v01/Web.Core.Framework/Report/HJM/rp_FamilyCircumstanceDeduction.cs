using System;
using System.Drawing.Printing;
using DevExpress.XtraReports.UI;
using Web.Core.Framework.Adapter;
using Web.Core.Object.Report;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Summary description for rp_FamilyCircumstanceDeduction
    /// </summary>
    public class rp_FamilyCircumstanceDeduction : XtraReport
    {
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private TopMarginBand topMarginBand;
        private DetailBand detailBand;

        private BottomMarginBand bottomMarginBand;
        private GroupHeaderBand GroupHeader;
        private PageHeaderBand PageHeader;
        private PageFooterBand PageFooter;
        private ReportHeaderBand ReportHeader;
        private XRLabel xrl_TitleBC;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCell7;
        private XRTableCell xrTableCell4;
        private XRTableCell xrTableCell6;
        private XRTable xrTable3;
        private XRTableRow xrTableRow3;
        private XRTableCell xrTableCell10;
        private XRTableCell xrt_DepartmentName;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrt_STT;
        private XRTableCell xrt_EmployeeCode;
        private XRTableCell xrt_FullName;
        private XRTableCell xrt_Position;
        private XRTableCell xrt_NumberPeople;
        private XRLabel xrl_TenCongTy;
        private ReportFooterBand ReportFooter;
        private XRLabel xrl_footer1;
        private XRLabel xrl_ten1;
        private XRLabel xrt_ReportDate;
        private XRLabel xrl_footer2;
        private XRLabel xrl_ten2;
        private XRLabel xrl_ten3;
        private XRLabel xrl_footer3;
        private XRPageInfo xrPageInfo1;
        private XRPictureBox xrLogo;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
        private System.ComponentModel.IContainer components = null;

        public rp_FamilyCircumstanceDeduction()
        {
            InitializeComponent();
        }

        int STT = 1;

        private void Detail_BeforePrint(object sender, PrintEventArgs e)
        {
            xrt_STT.Text = STT.ToString();
            STT++;
        }

        private void Group_BeforePrint(object sender, PrintEventArgs e)
        {
            STT = 1;
            xrt_STT.Text = STT.ToString();

        }

        public void BindData(ReportFilter filter)
        {
            try
            {
                var controler = new ReportController();
                xrl_TenCongTy.Text = ReportController.GetInstance().GetCompanyName(filter.SessionDepartment);
                var location = controler.GetCityName(filter.SessionDepartment);
                xrt_ReportDate.Text = string.Format(xrt_ReportDate.Text, location, DateTime.Now.Day, DateTime.Now.Month,
                    DateTime.Now.Year);

                //select form db
                var departments = string.IsNullOrEmpty(filter.SelectedDepartment)
                    ? new string[] { }
                    : filter.SelectedDepartment.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < departments.Length; i++)
                {
                    departments[i] = "'{0}'".FormatWith(departments[i]);
                }

                var table = SQLHelper.ExecuteTable(
                    SQLManagementAdapter.GetStore_ListEmployeeHaveDeduction(string.Join(",", departments)));
                DataSource = table;
                xrt_FullName.DataBindings.Add("Text", DataSource, "FullName");
                xrt_EmployeeCode.DataBindings.Add("Text", DataSource, "EmployeeCode");
                xrt_Position.DataBindings.Add("Text", DataSource, "PositionName");
                xrt_NumberPeople.DataBindings.Add("Text", DataSource, "");
                GroupHeader.GroupFields.AddRange(new GroupField[]
                {
                    new GroupField("DepartmentId", XRColumnSortOrder.Ascending)
                });
                xrt_DepartmentName.DataBindings.Add("Text", DataSource, "DepartmentName");
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Có lỗi xảy ra: " + ex.Message);
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
            string resourceFileName = "rp_FamilyCircumstanceDeduction.resx";
            this.topMarginBand = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrl_TenCongTy = new DevExpress.XtraReports.UI.XRLabel();
            this.detailBand = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrt_STT = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_EmployeeCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_FullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_Position = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_NumberPeople = new DevExpress.XtraReports.UI.XRTableCell();
            this.bottomMarginBand = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.GroupHeader = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_DepartmentName = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrl_TitleBC = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrl_footer1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ten1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrt_ReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ten2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ten3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer3 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // topMarginBand
            // 
            this.topMarginBand.HeightF = 50F;
            this.topMarginBand.Name = "topMarginBand";
            // 
            // xrl_TenCongTy
            // 
            this.xrl_TenCongTy.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_TenCongTy.LocationFloat = new DevExpress.Utils.PointFloat(0.7916451F, 120.4167F);
            this.xrl_TenCongTy.Name = "xrl_TenCongTy";
            this.xrl_TenCongTy.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_TenCongTy.SizeF = new System.Drawing.SizeF(737.2084F, 23F);
            this.xrl_TenCongTy.StylePriority.UseFont = false;
            this.xrl_TenCongTy.StylePriority.UseTextAlignment = false;
            this.xrl_TenCongTy.Text = "\t\tCÔNG TY TNHH THƯƠNG MẠI VÀ XÂY DỰNG TRUNG CHÍNH";
            this.xrl_TenCongTy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // detailBand
            // 
            this.detailBand.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrTable2
            });
            this.detailBand.HeightF = 25F;
            this.detailBand.Name = "detailBand";
            // 
            // xrTable2
            // 
            this.xrTable2.Borders =
                ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left |
                                                         DevExpress.XtraPrinting.BorderSide.Right)
                                                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[]
            {
                this.xrTableRow2
            });
            this.xrTable2.SizeF = new System.Drawing.SizeF(737.9999F, 25F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Borders =
                ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left |
                                                          DevExpress.XtraPrinting.BorderSide.Top)
                                                         | DevExpress.XtraPrinting.BorderSide.Right)
                                                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[]
            {
                this.xrt_STT,
                this.xrt_EmployeeCode,
                this.xrt_FullName,
                this.xrt_Position,
                this.xrt_NumberPeople
            });
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.StylePriority.UseBorders = false;
            this.xrTableRow2.Weight = 1D;
            // 
            // xrt_STT
            // 
            this.xrt_STT.Borders =
                ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left |
                                                         DevExpress.XtraPrinting.BorderSide.Right)
                                                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_STT.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_STT.Name = "xrt_STT";
            this.xrt_STT.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_STT.StylePriority.UseBorders = false;
            this.xrt_STT.StylePriority.UseFont = false;
            this.xrt_STT.StylePriority.UsePadding = false;
            this.xrt_STT.StylePriority.UseTextAlignment = false;
            this.xrt_STT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_STT.Weight = 0.5661495163423087D;
            this.xrt_STT.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrt_EmployeeCode
            // 
            this.xrt_EmployeeCode.Borders =
                ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left |
                                                         DevExpress.XtraPrinting.BorderSide.Right)
                                                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_EmployeeCode.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_EmployeeCode.Name = "xrt_EmployeeCode";
            this.xrt_EmployeeCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_EmployeeCode.StylePriority.UseBorders = false;
            this.xrt_EmployeeCode.StylePriority.UseFont = false;
            this.xrt_EmployeeCode.StylePriority.UsePadding = false;
            this.xrt_EmployeeCode.StylePriority.UseTextAlignment = false;
            this.xrt_EmployeeCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_EmployeeCode.Weight = 0.98158845547474038D;
            // 
            // xrt_FullName
            // 
            this.xrt_FullName.Borders =
                ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left |
                                                         DevExpress.XtraPrinting.BorderSide.Right)
                                                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_FullName.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_FullName.Name = "xrt_FullName";
            this.xrt_FullName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_FullName.StylePriority.UseBorders = false;
            this.xrt_FullName.StylePriority.UseFont = false;
            this.xrt_FullName.StylePriority.UsePadding = false;
            this.xrt_FullName.StylePriority.UseTextAlignment = false;
            this.xrt_FullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_FullName.Weight = 2.0671607384165842D;
            // 
            // xrt_Position
            // 
            this.xrt_Position.Borders =
                ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left |
                                                         DevExpress.XtraPrinting.BorderSide.Right)
                                                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_Position.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_Position.Name = "xrt_Position";
            this.xrt_Position.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_Position.StylePriority.UseBorders = false;
            this.xrt_Position.StylePriority.UseFont = false;
            this.xrt_Position.StylePriority.UsePadding = false;
            this.xrt_Position.StylePriority.UseTextAlignment = false;
            this.xrt_Position.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_Position.Weight = 1.8067393535756369D;
            // 
            // xrt_NumberPeople
            // 
            this.xrt_NumberPeople.Borders =
                ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left |
                                                         DevExpress.XtraPrinting.BorderSide.Right)
                                                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_NumberPeople.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_NumberPeople.Name = "xrt_NumberPeople";
            this.xrt_NumberPeople.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_NumberPeople.StylePriority.UseBorders = false;
            this.xrt_NumberPeople.StylePriority.UseFont = false;
            this.xrt_NumberPeople.StylePriority.UsePadding = false;
            this.xrt_NumberPeople.StylePriority.UseTextAlignment = false;
            this.xrt_NumberPeople.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrt_NumberPeople.Weight = 1.1122151558218731D;
            // 
            // bottomMarginBand
            // 
            this.bottomMarginBand.HeightF = 28F;
            this.bottomMarginBand.Name = "bottomMarginBand";
            // 
            // GroupHeader
            // 
            this.GroupHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrTable3
            });
            this.GroupHeader.HeightF = 25.41666F;
            this.GroupHeader.Name = "GroupHeader";
            // 
            // xrTable3
            // 
            this.xrTable3.Borders =
                ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left |
                                                         DevExpress.XtraPrinting.BorderSide.Right)
                                                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(1.589457E-05F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[]
            {
                this.xrTableRow3
            });
            this.xrTable3.SizeF = new System.Drawing.SizeF(738F, 25.41666F);
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[]
            {
                this.xrTableCell10,
                this.xrt_DepartmentName
            });
            this.xrTableRow3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.StylePriority.UseFont = false;
            this.xrTableRow3.StylePriority.UseTextAlignment = false;
            this.xrTableRow3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Borders =
                ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left |
                                                        DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseBorders = false;
            this.xrTableCell10.Weight = 0.093749659522817774D;
            // 
            // xrt_DepartmentName
            // 
            this.xrt_DepartmentName.Borders =
                ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right |
                                                        DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_DepartmentName.Name = "xrt_DepartmentName";
            this.xrt_DepartmentName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrt_DepartmentName.StylePriority.UseBorders = false;
            this.xrt_DepartmentName.StylePriority.UseFont = false;
            this.xrt_DepartmentName.StylePriority.UsePadding = false;
            this.xrt_DepartmentName.StylePriority.UseTextAlignment = false;
            this.xrt_DepartmentName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_DepartmentName.Weight = 9.76155540166222D;
            xrt_DepartmentName.BeforePrint += new PrintEventHandler(Group_BeforePrint);
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrTable1
            });
            this.PageHeader.HeightF = 32.29166F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(7.947286E-06F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[]
            {
                this.xrTableRow1
            });
            this.xrTable1.SizeF = new System.Drawing.SizeF(738F, 32.29166F);
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Borders =
                ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left |
                                                          DevExpress.XtraPrinting.BorderSide.Top)
                                                         | DevExpress.XtraPrinting.BorderSide.Right)
                                                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[]
            {
                this.xrTableCell1,
                this.xrTableCell2,
                this.xrTableCell7,
                this.xrTableCell4,
                this.xrTableCell6
            });
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.StylePriority.UseBorders = false;
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "STT";
            this.xrTableCell1.Weight = 0.53125D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Text = "Mã cán bộ";
            this.xrTableCell2.Weight = 0.92638155452899262D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.Text = "Họ và tên";
            this.xrTableCell7.Weight = 1.9508976308430799D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.Text = "Chức vụ";
            this.xrTableCell4.Weight = 1.7081813965869064D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.Text = "Số người giảm trừ";
            this.xrTableCell6.Weight = 1.0496624481541756D;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrPageInfo1
            });
            this.PageFooter.HeightF = 38F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrPageInfo1.Format = "Trang {0} của {1}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(601.9583F, 9.999974F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(126.0417F, 23.00001F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrLogo,
                this.xrl_TitleBC,
                this.xrl_TenCongTy
            });
            this.ReportHeader.HeightF = 230F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLogo
            // 
            this.xrLogo.LocationFloat = new DevExpress.Utils.PointFloat(57.95149F, 0F);
            this.xrLogo.Name = "xrLogo";
            this.xrLogo.Padding = new DevExpress.XtraPrinting.PaddingInfo(1, 1, 1, 1, 100F);
            this.xrLogo.SizeF = new System.Drawing.SizeF(110F, 110F);
            this.xrLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrLogo.StylePriority.UsePadding = false;
            // 
            // xrl_TitleBC
            // 
            this.xrl_TitleBC.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_TitleBC.LocationFloat = new DevExpress.Utils.PointFloat(0.7916451F, 150.4583F);
            this.xrl_TitleBC.Name = "xrl_TitleBC";
            this.xrl_TitleBC.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_TitleBC.SizeF = new System.Drawing.SizeF(737.2084F, 25.08333F);
            this.xrl_TitleBC.StylePriority.UseFont = false;
            this.xrl_TitleBC.StylePriority.UseTextAlignment = false;
            this.xrl_TitleBC.Text = "DANH SÁCH NHÂN VIÊN CÓ NGƯỜI GIẢM TRỪ GIA CẢNH";
            this.xrl_TitleBC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrl_footer1,
                this.xrl_ten1,
                this.xrt_ReportDate,
                this.xrl_footer2,
                this.xrl_ten2,
                this.xrl_ten3,
                this.xrl_footer3
            });
            this.ReportFooter.HeightF = 196F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrl_footer1
            // 
            this.xrl_footer1.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer1.LocationFloat = new DevExpress.Utils.PointFloat(9.536743E-05F, 61.45833F);
            this.xrl_footer1.Name = "xrl_footer1";
            this.xrl_footer1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer1.SizeF = new System.Drawing.SizeF(161.9514F, 23F);
            this.xrl_footer1.StylePriority.UseFont = false;
            this.xrl_footer1.StylePriority.UseTextAlignment = false;
            this.xrl_footer1.Text = "NGƯỜI LẬP";
            this.xrl_footer1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_ten1
            // 
            this.xrl_ten1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 150F);
            this.xrl_ten1.Name = "xrl_ten1";
            this.xrl_ten1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten1.SizeF = new System.Drawing.SizeF(161.9514F, 23F);
            this.xrl_ten1.StylePriority.UseFont = false;
            this.xrl_ten1.StylePriority.UseTextAlignment = false;
            this.xrl_ten1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrt_ReportDate
            // 
            this.xrt_ReportDate.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrt_ReportDate.LocationFloat = new DevExpress.Utils.PointFloat(472.9167F, 27.00001F);
            this.xrt_ReportDate.Name = "xrt_ReportDate";
            this.xrt_ReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrt_ReportDate.SizeF = new System.Drawing.SizeF(265.0829F, 23F);
            this.xrt_ReportDate.StylePriority.UseFont = false;
            this.xrt_ReportDate.StylePriority.UseTextAlignment = false;
            this.xrt_ReportDate.Text = "{0}, ngày {1} tháng {2} năm {3}";
            this.xrt_ReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrl_footer2
            // 
            this.xrl_footer2.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer2.LocationFloat = new DevExpress.Utils.PointFloat(225.0001F, 61.45833F);
            this.xrl_footer2.Name = "xrl_footer2";
            this.xrl_footer2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer2.SizeF = new System.Drawing.SizeF(222.7652F, 23F);
            this.xrl_footer2.StylePriority.UseFont = false;
            this.xrl_footer2.StylePriority.UseTextAlignment = false;
            this.xrl_footer2.Text = "KẾ TOÁN TRƯỞNG";
            this.xrl_footer2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_ten2
            // 
            this.xrl_ten2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten2.LocationFloat = new DevExpress.Utils.PointFloat(225F, 150F);
            this.xrl_ten2.Name = "xrl_ten2";
            this.xrl_ten2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten2.SizeF = new System.Drawing.SizeF(222.7653F, 23F);
            this.xrl_ten2.StylePriority.UseFont = false;
            this.xrl_ten2.StylePriority.UseTextAlignment = false;
            this.xrl_ten2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_ten3
            // 
            this.xrl_ten3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten3.LocationFloat = new DevExpress.Utils.PointFloat(500F, 150F);
            this.xrl_ten3.Name = "xrl_ten3";
            this.xrl_ten3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten3.SizeF = new System.Drawing.SizeF(238F, 23F);
            this.xrl_ten3.StylePriority.UseFont = false;
            this.xrl_ten3.StylePriority.UseTextAlignment = false;
            this.xrl_ten3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer3
            // 
            this.xrl_footer3.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer3.LocationFloat = new DevExpress.Utils.PointFloat(499.0001F, 61.45833F);
            this.xrl_footer3.Name = "xrl_footer3";
            this.xrl_footer3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer3.SizeF = new System.Drawing.SizeF(238F, 23F);
            this.xrl_footer3.StylePriority.UseFont = false;
            this.xrl_footer3.StylePriority.UseTextAlignment = false;
            this.xrl_footer3.Text = "PHÒNG HCNS";
            this.xrl_footer3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // rp_FamilyCircumstanceDeduction
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[]
            {
                this.topMarginBand,
                this.detailBand,
                this.bottomMarginBand,
                this.GroupHeader,
                this.PageHeader,
                this.PageFooter,
                this.ReportHeader,
                this.ReportFooter
            });
            this.Margins = new System.Drawing.Printing.Margins(47, 42, 50, 28);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}