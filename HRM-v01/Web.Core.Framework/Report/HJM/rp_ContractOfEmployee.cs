using System;
using DevExpress.XtraReports.UI;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Adapter;
using Web.Core.Object.Report;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Summary description for rp_ContractOfEmployee
    /// </summary>
    public class rp_ContractOfEmployee : DevExpress.XtraReports.UI.XtraReport
    {
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private ReportHeaderBand ReportHeader;
        private PageHeaderBand PageHeader;
        private ReportFooterBand ReportFooter;
        private XRLabel xrl_TitleBC;
        private XRLabel xrl_TenCongTy;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell4;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell7;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCell3;
        private XRTableCell xrTableCell12;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrt_sttt;
        private XRTableCell xrt_ContractNumber;
        private XRTableCell xrt_ContractType;
        private XRTableCell xrt_Job;
        private XRTableCell xrt_ContractDate;
        private XRTableCell xrt_ContractEndDate;
        private XRTableCell xrt_ContractStatus;
        private XRLabel xrl_FullName;
        private XRLabel xrl_DepartmentName;
        private XRLabel xrl_Occupation;
        private XRLabel xrl_Position;
        private XRTableCell xrTableCell5;
        private XRLabel xr_ParticipationDate;
        private XRLabel xrt_Seniority;
        private XRLabel xrLabel1;
        private XRLabel xrLabel2;
        private XRLabel xrLabel3;
        private XRLabel xrLabel4;
        private XRLabel xrLabel5;
        private XRLabel xrLabel6;
        private XRTableCell xrTableCell6;
        private XRTableCell xrt_EffectiveDate;
        private XRPictureBox xrLogo;
        private XRLabel xrl_ten3;
        private XRLabel xrl_ten2;
        private XRLabel xrl_ten1;
        private XRLabel xrtReportDate;
        private XRLabel xrl_footer1;
        private XRLabel xrl_footer3;
        private XRLabel xrl_footer2;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public rp_ContractOfEmployee()
        {
            InitializeComponent();
        }
        int STT = 1;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrt_sttt.Text = STT.ToString();
            STT++;
        }
        public void BindData(ReportFilter filter)
        {
            try
            {
                ReportController rpCtr = new ReportController();
                xrl_TenCongTy.Text = rpCtr.GetCompanyName(filter.SessionDepartment);
                var location = new ReportController().GetCityName(filter.SessionDepartment);
                xrtReportDate.Text = string.Format(xrtReportDate.Text, location, DateTime.Now.Day,
                    DateTime.Now.Month, DateTime.Now.Year);

                //Lấy thông tin của nhân viên
                var record = RecordController.GetById(filter.RecordId);
                if (record != null)
                {
                    xrl_FullName.Text = record.FullName;
                    xrl_DepartmentName.Text = record.DepartmentName;
                    xrl_Position.Text = record.PositionName;
                    xrl_Occupation.Text = "";
                    xr_ParticipationDate.Text = record.ParticipationDate.ToString();
                }

                //Tính thâm niên của nhân viên
                var seniority =
                    SQLHelper.ExecuteTable(
                        SQLManagementAdapter.GetStore_CalculateSeniorityByRecordId(filter.RecordId));
                if (seniority.Rows.Count > 0)
                {
                    xrt_Seniority.Text = seniority.Rows[0]["Seniority"].ToString();
                }

                var table = SQLHelper.ExecuteTable(
                    SQLManagementAdapter.GetStore_ListContractOfEmployee(filter.RecordId));
                DataSource = table;
                xrt_ContractNumber.DataBindings.Add("Text", DataSource, "ContractNumber");
                xrt_ContractType.DataBindings.Add("Text", DataSource, "ContractTypeName");
                xrt_Job.DataBindings.Add("Text", DataSource, "JobName");
                xrt_ContractDate.DataBindings.Add("Text", DataSource, "ContractDate", "{0:dd/MM/yyyy}");
                xrt_EffectiveDate.DataBindings.Add("Text", DataSource, "EffectiveDate", "{0:dd/MM/yyyy}");
                xrt_ContractEndDate.DataBindings.Add("Text", DataSource, "ContractEndDate", "{0:dd/MM/yyyy}");
                xrt_ContractStatus.DataBindings.Add("Text", DataSource, "ContractStatusName");

            }
            catch (Exception ex)
            {
                Dialog.ShowError("Có lỗi xảy ra: " + ex.Message);
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
            string resourceFileName = "rp_ContractOfEmployee.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrt_sttt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_ContractNumber = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_ContractType = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_Job = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_ContractDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_EffectiveDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_ContractEndDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_ContractStatus = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrt_Seniority = new DevExpress.XtraReports.UI.XRLabel();
            this.xr_ParticipationDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_Occupation = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_Position = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_DepartmentName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_FullName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TitleBC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TenCongTy = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrl_ten3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ten2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ten1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrtReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer2 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1044.958F, 25F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrt_sttt,
            this.xrt_ContractNumber,
            this.xrt_ContractType,
            this.xrt_Job,
            this.xrt_ContractDate,
            this.xrt_EffectiveDate,
            this.xrt_ContractEndDate,
            this.xrt_ContractStatus});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrt_sttt
            // 
            this.xrt_sttt.Name = "xrt_sttt";
            this.xrt_sttt.StylePriority.UseTextAlignment = false;
            this.xrt_sttt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_sttt.Weight = 0.096422348839708683D;
            this.xrt_sttt.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrt_ContractNumber
            // 
            this.xrt_ContractNumber.Name = "xrt_ContractNumber";
            this.xrt_ContractNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_ContractNumber.SnapLineMargin = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_ContractNumber.StylePriority.UsePadding = false;
            this.xrt_ContractNumber.StylePriority.UseTextAlignment = false;
            this.xrt_ContractNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_ContractNumber.Weight = 0.2954840395505004D;
            // 
            // xrt_ContractType
            // 
            this.xrt_ContractType.Name = "xrt_ContractType";
            this.xrt_ContractType.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_ContractType.StylePriority.UsePadding = false;
            this.xrt_ContractType.StylePriority.UseTextAlignment = false;
            this.xrt_ContractType.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_ContractType.Weight = 0.6254364431133298D;
            // 
            // xrt_Job
            // 
            this.xrt_Job.Name = "xrt_Job";
            this.xrt_Job.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_Job.StylePriority.UsePadding = false;
            this.xrt_Job.StylePriority.UseTextAlignment = false;
            this.xrt_Job.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_Job.Weight = 0.456369945373317D;
            // 
            // xrt_ContractDate
            // 
            this.xrt_ContractDate.Name = "xrt_ContractDate";
            this.xrt_ContractDate.StylePriority.UseTextAlignment = false;
            this.xrt_ContractDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_ContractDate.Weight = 0.24094738800203636D;
            // 
            // xrt_EffectiveDate
            // 
            this.xrt_EffectiveDate.Name = "xrt_EffectiveDate";
            this.xrt_EffectiveDate.StylePriority.UseTextAlignment = false;
            this.xrt_EffectiveDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_EffectiveDate.Weight = 0.24093636913793445D;
            // 
            // xrt_ContractEndDate
            // 
            this.xrt_ContractEndDate.Name = "xrt_ContractEndDate";
            this.xrt_ContractEndDate.StylePriority.UseTextAlignment = false;
            this.xrt_ContractEndDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_ContractEndDate.Weight = 0.23548234874855148D;
            // 
            // xrt_ContractStatus
            // 
            this.xrt_ContractStatus.Name = "xrt_ContractStatus";
            this.xrt_ContractStatus.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_ContractStatus.StylePriority.UsePadding = false;
            this.xrt_ContractStatus.StylePriority.UseTextAlignment = false;
            this.xrt_ContractStatus.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_ContractStatus.Weight = 0.54441424295042706D;
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
            this.BottomMargin.HeightF = 64F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLogo,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel1,
            this.xrt_Seniority,
            this.xr_ParticipationDate,
            this.xrl_Occupation,
            this.xrl_Position,
            this.xrl_DepartmentName,
            this.xrl_FullName,
            this.xrl_TitleBC,
            this.xrl_TenCongTy,
            this.xrLabel2});
            this.ReportHeader.HeightF = 247F;
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
            // xrLabel6
            // 
            this.xrLabel6.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(670.4173F, 209.0417F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(156.1577F, 23F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "Thâm niên :";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(670.4172F, 186.0417F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(156.1578F, 22.99998F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Ngày tuyển chính thức :";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(314.3752F, 209.0417F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(114.5833F, 23F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "Vị trí công việc :";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(314.3752F, 186.0417F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(114.5832F, 23F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Phòng ban :";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 186.0417F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(80.20834F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Họ tên : ";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrt_Seniority
            // 
            this.xrt_Seniority.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrt_Seniority.LocationFloat = new DevExpress.Utils.PointFloat(826.5751F, 209.0417F);
            this.xrt_Seniority.Name = "xrt_Seniority";
            this.xrt_Seniority.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrt_Seniority.SizeF = new System.Drawing.SizeF(218.3829F, 23F);
            this.xrt_Seniority.StylePriority.UseFont = false;
            this.xrt_Seniority.StylePriority.UseTextAlignment = false;
            this.xrt_Seniority.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xr_ParticipationDate
            // 
            this.xr_ParticipationDate.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xr_ParticipationDate.LocationFloat = new DevExpress.Utils.PointFloat(826.5751F, 186.0417F);
            this.xr_ParticipationDate.Name = "xr_ParticipationDate";
            this.xr_ParticipationDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xr_ParticipationDate.SizeF = new System.Drawing.SizeF(218.3829F, 23F);
            this.xr_ParticipationDate.StylePriority.UseFont = false;
            this.xr_ParticipationDate.StylePriority.UseTextAlignment = false;
            this.xr_ParticipationDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_Occupation
            // 
            this.xrl_Occupation.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrl_Occupation.LocationFloat = new DevExpress.Utils.PointFloat(428.9585F, 209.0417F);
            this.xrl_Occupation.Name = "xrl_Occupation";
            this.xrl_Occupation.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_Occupation.SizeF = new System.Drawing.SizeF(241.4586F, 23F);
            this.xrl_Occupation.StylePriority.UseFont = false;
            this.xrl_Occupation.StylePriority.UseTextAlignment = false;
            this.xrl_Occupation.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_Position
            // 
            this.xrl_Position.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrl_Position.LocationFloat = new DevExpress.Utils.PointFloat(80.20834F, 209.0417F);
            this.xrl_Position.Name = "xrl_Position";
            this.xrl_Position.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_Position.SizeF = new System.Drawing.SizeF(234.1669F, 23F);
            this.xrl_Position.StylePriority.UseFont = false;
            this.xrl_Position.StylePriority.UseTextAlignment = false;
            this.xrl_Position.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_DepartmentName
            // 
            this.xrl_DepartmentName.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrl_DepartmentName.LocationFloat = new DevExpress.Utils.PointFloat(428.9585F, 186.0417F);
            this.xrl_DepartmentName.Name = "xrl_DepartmentName";
            this.xrl_DepartmentName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_DepartmentName.SizeF = new System.Drawing.SizeF(241.4586F, 23F);
            this.xrl_DepartmentName.StylePriority.UseFont = false;
            this.xrl_DepartmentName.StylePriority.UseTextAlignment = false;
            this.xrl_DepartmentName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_FullName
            // 
            this.xrl_FullName.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrl_FullName.LocationFloat = new DevExpress.Utils.PointFloat(80.20834F, 186.0417F);
            this.xrl_FullName.Name = "xrl_FullName";
            this.xrl_FullName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_FullName.SizeF = new System.Drawing.SizeF(234.1669F, 23F);
            this.xrl_FullName.StylePriority.UseFont = false;
            this.xrl_FullName.StylePriority.UseTextAlignment = false;
            this.xrl_FullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_TitleBC
            // 
            this.xrl_TitleBC.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_TitleBC.LocationFloat = new DevExpress.Utils.PointFloat(0F, 146.625F);
            this.xrl_TitleBC.Name = "xrl_TitleBC";
            this.xrl_TitleBC.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_TitleBC.SizeF = new System.Drawing.SizeF(1044.958F, 23F);
            this.xrl_TitleBC.StylePriority.UseFont = false;
            this.xrl_TitleBC.StylePriority.UseTextAlignment = false;
            this.xrl_TitleBC.Text = "BÁO CÁO DANH SÁCH HỢP ĐỒNG CỦA NHÂN VIÊN";
            this.xrl_TitleBC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrl_TenCongTy
            // 
            this.xrl_TenCongTy.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_TenCongTy.LocationFloat = new DevExpress.Utils.PointFloat(0F, 110.7917F);
            this.xrl_TenCongTy.Name = "xrl_TenCongTy";
            this.xrl_TenCongTy.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 2, 0, 0, 100F);
            this.xrl_TenCongTy.SizeF = new System.Drawing.SizeF(574.3446F, 21.875F);
            this.xrl_TenCongTy.StylePriority.UseFont = false;
            this.xrl_TenCongTy.StylePriority.UsePadding = false;
            this.xrl_TenCongTy.StylePriority.UseTextAlignment = false;
            this.xrl_TenCongTy.Text = "CÔNG TY TNHH THƯƠNG MẠI VÀ XÂY DỰNG TRUNG CHÍNH";
            this.xrl_TenCongTy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 209.0417F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(80.20834F, 23F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Chức vụ : ";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.PageHeader.HeightF = 35F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1044.958F, 34.625F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell1,
            this.xrTableCell7,
            this.xrTableCell2,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell3,
            this.xrTableCell12});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "STT";
            this.xrTableCell4.Weight = 0.096422355212466268D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "Số hợp đồng";
            this.xrTableCell1.Weight = 0.2954839803715959D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Text = "Loại hợp đồng";
            this.xrTableCell7.Weight = 0.62543627430773951D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "Công việc";
            this.xrTableCell2.Weight = 0.45637012307178776D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "Ngày ký";
            this.xrTableCell5.Weight = 0.24094738231432739D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Text = "Ngày hiệu lực";
            this.xrTableCell6.Weight = 0.24093636541416669D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "Ngày hết hạn";
            this.xrTableCell3.Weight = 0.23548234502478369D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.Text = "Tình trạng hợp đồng";
            this.xrTableCell12.Weight = 0.544414299998938D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrl_ten3,
            this.xrl_ten2,
            this.xrl_ten1,
            this.xrtReportDate,
            this.xrl_footer1,
            this.xrl_footer3,
            this.xrl_footer2});
            this.ReportFooter.HeightF = 175F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrl_ten3
            // 
            this.xrl_ten3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten3.LocationFloat = new DevExpress.Utils.PointFloat(775F, 137.5F);
            this.xrl_ten3.Name = "xrl_ten3";
            this.xrl_ten3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten3.SizeF = new System.Drawing.SizeF(269.9579F, 23F);
            this.xrl_ten3.StylePriority.UseFont = false;
            this.xrl_ten3.StylePriority.UseTextAlignment = false;
            this.xrl_ten3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_ten2
            // 
            this.xrl_ten2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten2.LocationFloat = new DevExpress.Utils.PointFloat(362.5F, 137.5F);
            this.xrl_ten2.Name = "xrl_ten2";
            this.xrl_ten2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten2.SizeF = new System.Drawing.SizeF(289.3651F, 23F);
            this.xrl_ten2.StylePriority.UseFont = false;
            this.xrl_ten2.StylePriority.UseTextAlignment = false;
            this.xrl_ten2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_ten1
            // 
            this.xrl_ten1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 137.5F);
            this.xrl_ten1.Name = "xrl_ten1";
            this.xrl_ten1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten1.SizeF = new System.Drawing.SizeF(289.3651F, 23F);
            this.xrl_ten1.StylePriority.UseFont = false;
            this.xrl_ten1.StylePriority.UseTextAlignment = false;
            this.xrl_ten1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrtReportDate
            // 
            this.xrtReportDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Italic);
            this.xrtReportDate.LocationFloat = new DevExpress.Utils.PointFloat(775F, 12.5F);
            this.xrtReportDate.Name = "xrtReportDate";
            this.xrtReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrtReportDate.SizeF = new System.Drawing.SizeF(269.9579F, 23F);
            this.xrtReportDate.StylePriority.UseFont = false;
            this.xrtReportDate.StylePriority.UseTextAlignment = false;
            this.xrtReportDate.Text = "{0}, ngày {1} tháng {2} năm {3}";
            this.xrtReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer1
            // 
            this.xrl_footer1.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 37.5F);
            this.xrl_footer1.Name = "xrl_footer1";
            this.xrl_footer1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer1.SizeF = new System.Drawing.SizeF(291.366F, 23F);
            this.xrl_footer1.StylePriority.UseFont = false;
            this.xrl_footer1.StylePriority.UseTextAlignment = false;
            this.xrl_footer1.Text = "NGƯỜI LẬP";
            this.xrl_footer1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer3
            // 
            this.xrl_footer3.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer3.LocationFloat = new DevExpress.Utils.PointFloat(775F, 37.5F);
            this.xrl_footer3.Name = "xrl_footer3";
            this.xrl_footer3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer3.SizeF = new System.Drawing.SizeF(269.9581F, 23F);
            this.xrl_footer3.StylePriority.UseFont = false;
            this.xrl_footer3.StylePriority.UseTextAlignment = false;
            this.xrl_footer3.Text = "GIÁM ĐỐC";
            this.xrl_footer3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer2
            // 
            this.xrl_footer2.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer2.LocationFloat = new DevExpress.Utils.PointFloat(362.5F, 37.5F);
            this.xrl_footer2.Name = "xrl_footer2";
            this.xrl_footer2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer2.SizeF = new System.Drawing.SizeF(291.366F, 23F);
            this.xrl_footer2.StylePriority.UseFont = false;
            this.xrl_footer2.StylePriority.UseTextAlignment = false;
            this.xrl_footer2.Text = "PHÒNG TCHC";
            this.xrl_footer2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // rp_ContractOfEmployee
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(58, 64, 50, 64);
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
