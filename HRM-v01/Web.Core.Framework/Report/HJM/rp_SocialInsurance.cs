using System;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;
using Web.Core.Framework.Adapter;
using Web.Core.Object.Report;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Summary description for rp_SocialInsurance
    /// </summary>
    public class rp_SocialInsurance : XtraReport
    {
        private DetailBand Detail;
        private TopMarginBand TopMargin;
        private BottomMarginBand BottomMargin;
        private GroupHeaderBand GroupHeader1;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrtstt;
        private XRTableCell xrtEmployeeCode;
        private XRTableCell xrtFullName;
        private XRTableCell xrtBirthDate;
        private XRTableCell xrtSex;
        private XRTableCell xrtAddress;
        private XRTableCell xrtPhone;
        private XRTableCell xrtInsuranceNumber;
        private XRTableCell xrtInsuranceIssueDate;
        private XRTableCell xrtPosition;
        private ReportHeaderBand ReportHeader;
        private XRTable xrTable3;
        private XRTableRow xrTableRow3;
        private XRTableCell xrtDepartmentName;
        private ReportFooterBand ReportFooter;
        private XRLabel xrl_TitleBC;
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
        private GroupFooterBand GroupFooter1;
        private PageFooterBand PageFooter;
        private XRPageInfo xrPageInfo1;
        private XRTableCell xrTableCell9;
        private XRLabel xrl_ten3;
        private XRLabel xrl_ten2;
        private XRLabel xrl_ten1;
        private XRLabel xrtReportDate;
        private XRLabel xrl_footer1;
        private XRLabel xrl_footer3;
        private XRLabel xrl_footer2;
        private FormattingRule formattingRule1;
        private XRPictureBox xrLogo;
        private System.ComponentModel.IContainer components = null;

        public rp_SocialInsurance()
        {
            InitializeComponent();
        }

        int STT = 1;

        private void Detail_BeforePrint(object sender, PrintEventArgs e)
        {
            xrtstt.Text = STT.ToString();
            STT++;
        }

        private void Group_BeforePrint(object sender, PrintEventArgs e)
        {
            STT = 1;
            xrtstt.Text = STT.ToString();

        }

        public void BindData(ReportFilter filter)
        {
            try
            {
                ReportController rpCtr = new ReportController();
                xrl_TenCongTy.Text = rpCtr.GetCompanyName(filter.SessionDepartment);
                var location = rpCtr.GetCityName(filter.SessionDepartment);
                xrtReportDate.Text = string.Format(xrtReportDate.Text, location, DateTime.Now.Day, DateTime.Now.Month,
                    DateTime.Now.Year);

                var departments = string.IsNullOrEmpty(filter.SelectedDepartment)
                    ? new string[] { }
                    : filter.SelectedDepartment.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < departments.Length; i++)
                {
                    departments[i] = "'{0}'".FormatWith(departments[i]);
                }

                var table = SQLHelper.ExecuteTable(
                    SQLManagementAdapter.GetStore_ListEmployeeJoinSocialInsurance(string.Join(",", departments)));
                DataSource = table;
                //Binding data
                xrtEmployeeCode.DataBindings.Add("Text", DataSource, "EmployeeCode");
                xrtFullName.DataBindings.Add("Text", DataSource, "FullName");
                xrtBirthDate.DataBindings.Add("Text", DataSource, "BirthDate", "{0:dd/MM/yyyy}");
                xrtSex.DataBindings.Add("Text", DataSource, "SexName");
                xrtAddress.DataBindings.Add("Text", DataSource, "Address");
                xrtPhone.DataBindings.Add("Text", DataSource, "CellPhoneNumber");
                xrtInsuranceIssueDate.DataBindings.Add("Text", DataSource, "InsuranceIssueDate", "{0:dd/MM/yyyy}");
                xrtInsuranceNumber.DataBindings.Add("Text", DataSource, "InsuranceNumber");
                xrtPosition.DataBindings.Add("Text", DataSource, "PositionName");
                GroupHeader1.GroupFields.AddRange(new[] {new GroupField("DepartmentId", XRColumnSortOrder.Ascending)});
                xrtDepartmentName.DataBindings.Add("Text", DataSource, "DepartmentName");
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
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
            string resourceFileName = "rp_SocialInsurance.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrtstt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtEmployeeCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtFullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtBirthDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtSex = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtAddress = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtPhone = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtInsuranceNumber = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtInsuranceIssueDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtPosition = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDepartmentName = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrl_TitleBC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TenCongTy = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrl_ten3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ten2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ten1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrtReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer2 = new DevExpress.XtraReports.UI.XRLabel();
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
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            ((System.ComponentModel.ISupportInitialize) (this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrTable2
            });
            this.Detail.HeightF = 25.41666F;
            this.Detail.KeepTogether = true;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrTable2
            // 
            this.xrTable2.Borders =
                ((DevExpress.XtraPrinting.BorderSide) (((DevExpress.XtraPrinting.BorderSide.Left |
                                                         DevExpress.XtraPrinting.BorderSide.Right)
                                                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(2.000936F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[]
            {
                this.xrTableRow2
            });
            this.xrTable2.SizeF = new System.Drawing.SizeF(1082.999F, 25.41666F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UsePadding = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[]
            {
                this.xrtstt,
                this.xrtEmployeeCode,
                this.xrtFullName,
                this.xrtBirthDate,
                this.xrtSex,
                this.xrtAddress,
                this.xrtPhone,
                this.xrtInsuranceNumber,
                this.xrtInsuranceIssueDate,
                this.xrtPosition
            });
            this.xrTableRow2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.StylePriority.UseFont = false;
            this.xrTableRow2.StylePriority.UseTextAlignment = false;
            this.xrTableRow2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
            // xrtEmployeeCode
            // 
            this.xrtEmployeeCode.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtEmployeeCode.Name = "xrtEmployeeCode";
            this.xrtEmployeeCode.StylePriority.UseFont = false;
            this.xrtEmployeeCode.StylePriority.UseTextAlignment = false;
            this.xrtEmployeeCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrtEmployeeCode.Weight = 0.86457550153778651D;
            // 
            // xrtFullName
            // 
            this.xrtFullName.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtFullName.Name = "xrtFullName";
            this.xrtFullName.StylePriority.UseFont = false;
            this.xrtFullName.StylePriority.UseTextAlignment = false;
            this.xrtFullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrtFullName.Weight = 1.2500006236660197D;
            // 
            // xrtBirthDate
            // 
            this.xrtBirthDate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtBirthDate.Name = "xrtBirthDate";
            this.xrtBirthDate.StylePriority.UseFont = false;
            this.xrtBirthDate.StylePriority.UseTextAlignment = false;
            this.xrtBirthDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtBirthDate.Weight = 0.80729853147345287D;
            // 
            // xrtSex
            // 
            this.xrtSex.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtSex.Name = "xrtSex";
            this.xrtSex.StylePriority.UseFont = false;
            this.xrtSex.StylePriority.UseTextAlignment = false;
            this.xrtSex.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtSex.Weight = 0.63802091000874361D;
            // 
            // xrtAddress
            // 
            this.xrtAddress.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtAddress.Name = "xrtAddress";
            this.xrtAddress.StylePriority.UseFont = false;
            this.xrtAddress.StylePriority.UseTextAlignment = false;
            this.xrtAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrtAddress.Weight = 2.4648422184067971D;
            // 
            // xrtPhone
            // 
            this.xrtPhone.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtPhone.Name = "xrtPhone";
            this.xrtPhone.StylePriority.UseFont = false;
            this.xrtPhone.StylePriority.UseTextAlignment = false;
            this.xrtPhone.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrtPhone.Weight = 0.90429476301779521D;
            // 
            // xrtInsuranceNumber
            // 
            this.xrtInsuranceNumber.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtInsuranceNumber.Name = "xrtInsuranceNumber";
            this.xrtInsuranceNumber.StylePriority.UseFont = false;
            this.xrtInsuranceNumber.StylePriority.UseTextAlignment = false;
            this.xrtInsuranceNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrtInsuranceNumber.Weight = 1.0511087973976754D;
            // 
            // xrtInsuranceIssueDate
            // 
            this.xrtInsuranceIssueDate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtInsuranceIssueDate.Name = "xrtInsuranceIssueDate";
            this.xrtInsuranceIssueDate.StylePriority.UseFont = false;
            this.xrtInsuranceIssueDate.StylePriority.UseTextAlignment = false;
            this.xrtInsuranceIssueDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtInsuranceIssueDate.Weight = 1.0670792866941483D;
            // 
            // xrtPosition
            // 
            this.xrtPosition.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtPosition.Name = "xrtPosition";
            this.xrtPosition.StylePriority.UseFont = false;
            this.xrtPosition.StylePriority.UseTextAlignment = false;
            this.xrtPosition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrtPosition.Weight = 1.4286072963765166D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 49F;
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
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrTable3
            });
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[]
            {
                new DevExpress.XtraReports.UI.GroupField("", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)
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
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(2.000936F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[]
            {
                this.xrTableRow3
            });
            this.xrTable3.SizeF = new System.Drawing.SizeF(1082.999F, 25.41666F);
            this.xrTable3.StylePriority.UseBorders = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[]
            {
                this.xrTableCell9,
                this.xrtDepartmentName
            });
            this.xrTableRow3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.StylePriority.UseFont = false;
            this.xrTableRow3.StylePriority.UseTextAlignment = false;
            this.xrTableRow3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Borders =
                ((DevExpress.XtraPrinting.BorderSide) ((DevExpress.XtraPrinting.BorderSide.Left |
                                                        DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.Weight = 0.020000008040643349D;
            // 
            // xrtDepartmentName
            // 
            this.xrtDepartmentName.Borders =
                ((DevExpress.XtraPrinting.BorderSide) ((DevExpress.XtraPrinting.BorderSide.Right |
                                                        DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDepartmentName.Name = "xrtDepartmentName";
            this.xrtDepartmentName.Padding = new DevExpress.XtraPrinting.PaddingInfo(11, 0, 0, 0, 100F);
            this.xrtDepartmentName.StylePriority.UseBorders = false;
            this.xrtDepartmentName.StylePriority.UsePadding = false;
            this.xrtDepartmentName.StylePriority.UseTextAlignment = false;
            this.xrtDepartmentName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrtDepartmentName.Weight = 10.809994580338495D;
            xrtDepartmentName.BeforePrint += new PrintEventHandler(Group_BeforePrint);
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrLogo,
                this.xrl_TitleBC,
                this.xrl_TenCongTy
            });
            this.ReportHeader.HeightF = 207F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLogo
            // 
            this.xrLogo.LocationFloat = new DevExpress.Utils.PointFloat(58.33333F, 0F);
            this.xrLogo.Name = "xrLogo";
            this.xrLogo.Padding = new DevExpress.XtraPrinting.PaddingInfo(1, 1, 1, 1, 100F);
            this.xrLogo.SizeF = new System.Drawing.SizeF(110F, 110F);
            this.xrLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrLogo.StylePriority.UsePadding = false;
            // 
            // xrl_TitleBC
            // 
            this.xrl_TitleBC.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_TitleBC.LocationFloat = new DevExpress.Utils.PointFloat(1.00046F, 154.5417F);
            this.xrl_TitleBC.Name = "xrl_TitleBC";
            this.xrl_TitleBC.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_TitleBC.SizeF = new System.Drawing.SizeF(1083F, 23F);
            this.xrl_TitleBC.StylePriority.UseFont = false;
            this.xrl_TitleBC.StylePriority.UseTextAlignment = false;
            this.xrl_TitleBC.Text = "BÁO CÁO DANH SÁCH CÁN BỘ THAM GIA BHXH";
            this.xrl_TitleBC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrl_TenCongTy
            // 
            this.xrl_TenCongTy.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_TenCongTy.LocationFloat = new DevExpress.Utils.PointFloat(1.00046F, 112.875F);
            this.xrl_TenCongTy.Name = "xrl_TenCongTy";
            this.xrl_TenCongTy.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 2, 0, 0, 100F);
            this.xrl_TenCongTy.SizeF = new System.Drawing.SizeF(570.8333F, 23F);
            this.xrl_TenCongTy.StylePriority.UseFont = false;
            this.xrl_TenCongTy.StylePriority.UsePadding = false;
            this.xrl_TenCongTy.StylePriority.UseTextAlignment = false;
            this.xrl_TenCongTy.Text = "CÔNG TY TNHH THƯƠNG MẠI VÀ XÂY DỰNG TRUNG CHÍNH";
            this.xrl_TenCongTy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrl_ten3,
                this.xrl_ten2,
                this.xrl_ten1,
                this.xrtReportDate,
                this.xrl_footer1,
                this.xrl_footer3,
                this.xrl_footer2
            });
            this.ReportFooter.HeightF = 209F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrl_ten3
            // 
            this.xrl_ten3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten3.LocationFloat = new DevExpress.Utils.PointFloat(764.0483F, 149.375F);
            this.xrl_ten3.Name = "xrl_ten3";
            this.xrl_ten3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten3.SizeF = new System.Drawing.SizeF(302.1819F, 23F);
            this.xrl_ten3.StylePriority.UseFont = false;
            this.xrl_ten3.StylePriority.UseTextAlignment = false;
            this.xrl_ten3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_ten2
            // 
            this.xrl_ten2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten2.LocationFloat = new DevExpress.Utils.PointFloat(372.7372F, 149.375F);
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
            this.xrl_ten1.LocationFloat = new DevExpress.Utils.PointFloat(1.000458F, 149.375F);
            this.xrl_ten1.Name = "xrl_ten1";
            this.xrl_ten1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten1.SizeF = new System.Drawing.SizeF(302.1819F, 23F);
            this.xrl_ten1.StylePriority.UseFont = false;
            this.xrl_ten1.StylePriority.UseTextAlignment = false;
            this.xrl_ten1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrtReportDate
            // 
            this.xrtReportDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Italic);
            this.xrtReportDate.LocationFloat = new DevExpress.Utils.PointFloat(745.2785F, 24.58331F);
            this.xrtReportDate.Name = "xrtReportDate";
            this.xrtReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrtReportDate.SizeF = new System.Drawing.SizeF(339.7215F, 23F);
            this.xrtReportDate.StylePriority.UseFont = false;
            this.xrtReportDate.StylePriority.UseTextAlignment = false;
            this.xrtReportDate.Text = "{0} ngày {1} tháng {2} năm {3}";
            this.xrtReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrl_footer1
            // 
            this.xrl_footer1.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 49.58331F);
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
            this.xrl_footer3.LocationFloat = new DevExpress.Utils.PointFloat(763.5075F, 49.58331F);
            this.xrl_footer3.Name = "xrl_footer3";
            this.xrl_footer3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer3.SizeF = new System.Drawing.SizeF(303.2635F, 23F);
            this.xrl_footer3.StylePriority.UseFont = false;
            this.xrl_footer3.StylePriority.UseTextAlignment = false;
            this.xrl_footer3.Text = "TỔNG GIÁM ĐỐC";
            this.xrl_footer3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer2
            // 
            this.xrl_footer2.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer2.LocationFloat = new DevExpress.Utils.PointFloat(371.7368F, 49.58331F);
            this.xrl_footer2.Name = "xrl_footer2";
            this.xrl_footer2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer2.SizeF = new System.Drawing.SizeF(304.1828F, 23F);
            this.xrl_footer2.StylePriority.UseFont = false;
            this.xrl_footer2.StylePriority.UseTextAlignment = false;
            this.xrl_footer2.Text = "PHÒNG HCNS";
            this.xrl_footer2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrTable1
            });
            this.PageHeader.HeightF = 38.33333F;
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
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(2.000038F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 5, 0, 100F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[]
            {
                this.xrTableRow1
            });
            this.xrTable1.SizeF = new System.Drawing.SizeF(1083F, 38.33333F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UsePadding = false;
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
                this.xrTableCell5,
                this.xrTableCell6,
                this.xrTableCell7,
                this.xrTableCell8,
                this.xrTableCell12,
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
            this.xrTableCell1.Text = "STT";
            this.xrTableCell1.Weight = 0.35416665980020634D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "Mã nhân viên";
            this.xrTableCell2.Weight = 0.8645834853771206D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "Họ tên";
            this.xrTableCell4.Weight = 1.2499995319741089D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "Ngày sinh";
            this.xrTableCell3.Weight = 0.80729163932602965D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "Giới tính";
            this.xrTableCell5.Weight = 0.63802091000874361D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Text = "Địa chỉ";
            this.xrTableCell6.Weight = 2.4648454812932687D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Text = "Điện thoại";
            this.xrTableCell7.Weight = 0.90429460460702282D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Text = "Số thẻ BHXH";
            this.xrTableCell8.Weight = 1.0511072598253863D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.Text = "Ngày cấp BHXH";
            this.xrTableCell12.Weight = 1.0670777197907384D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.Text = "Chức vụ";
            this.xrTableCell15.Weight = 1.4286072963765166D;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.HeightF = 25F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrPageInfo1
            });
            this.PageFooter.HeightF = 100F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrPageInfo1.Format = "Trang {0} của {1}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(961.9583F, 38.54167F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(126.0417F, 23.00001F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // rp_SocialInsurance
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[]
            {
                this.Detail,
                this.TopMargin,
                this.BottomMargin,
                this.GroupHeader1,
                this.ReportHeader,
                this.ReportFooter,
                this.PageHeader,
                this.GroupFooter1,
                this.PageFooter
            });
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[]
            {
                this.formattingRule1
            });
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(6, 6, 49, 54);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize) (this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this)).EndInit();

        }

        #endregion
    }
}