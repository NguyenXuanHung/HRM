using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Web.Core.Object.Report;

namespace Web.Core.Framework.Report
{

    /// <summary>
    /// Summary description for rp_SalaryDetailByMonth
    /// </summary>
    public class rp_SalaryDetailByMonth : XtraReport
    {
        private DetailBand Detail;
        private TopMarginBand TopMargin;
        private BottomMarginBand BottomMargin;
        private ReportHeaderBand ReportHeader;
        private PageHeaderBand PageHeader;
        private FormattingRule formattingRule1;
        private ReportFooterBand ReportFooter;
        private XRLabel lblLapBang;
        private XRLabel lblThuTruong;
        private XRTable tblReportHeader;
        private XRTableRow xrTableRow4;
        private XRTableCell xrTableCell601;
        private XRTableCell xrTableCell602;
        private XRTableRow xrTableRow19;
        private XRTableCell xrCellTenBieuMau;
        private XRTable tblDetail;
        private XRTableRow xrDetailRow1;
        private XRTableCell xrTableCellStaffNumber;
        private XRTableCell xrTableCellFullName;
        private XRTableCell xrTableCellTitle;
        private XRTableCell xrTableCellTaxCode;
        private XRTableCell xrTableCellSalaryLevel;
        private XRTableCell xrTableCellTotalIncome;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCellTotalSalaryLevel;
        private XRTableCell xrTableCellTotalBasicSalary;
        private XRTableCell xrTableCellTotalResponsibilityAllowances;
        private XRTableCell xrTableCellTotalOtherSubsidies;
        private XRTableCell xrTableCellTotalWorkOfDay;
        private XRTableCell xrTableCellTotalSalaryByDay;
        private XRTableCell xrTableCellTotalWorkOfDayOvertime;
        private XRTableCell xrTableCellTotalIntoMoney;
        private XRTableCell xrTableCellAmountIncome;
        private XRTableCell xrTableCellTotalInsurance;
        private XRTableCell xrTableCellTotalTẫbleIncome;
        private XRTableCell xrTableCellTotalPersonalIncomeTax;
        private XRTableCell xrTongGhiChu;
        private XRLabel lblReportDate;
        private XRTable tblPageHeader;
        private XRTableRow xrTableRow11;
        private XRTableCell xrTableCell241;
        private XRTableCell xrTableCell242;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell9;
        private XRLabel lblLuongHienHuong;
        private XRTable xrTable2;
        private XRTableRow xrTableRow6;
        private XRTableCell xrTableCell11;
        private XRTableCell xrTableCell12;
        private XRTableCell xrTableCell5;
        private XRTableCell xrTableCell6;
        private XRTableCell xrTableCellOrder;
        private XRTableCell xrTableCellBasicSalary;
        private XRTableCell xrTableCellResponsibilityAllowances;
        private XRTableCell xrTableCellOtherSubsidies;
        private XRTableCell xrTableCellWorkOfDay;
        private XRTableCell xrTableCellSalaryByDay;
        private XRTableCell xrTableCellWorkOfDayOvertime;
        private XRTableCell xrTableCellIntoMoney;
        private XRTableCell xrTableCellNote;
        private XRTableCell xrTextTongSo;
        private XRTableCell xrTableCell10;
        private XRTableCell xrTableCell14;
        private XRTableCell xrTableCell15;
        private XRTableCell xrTableCell16;
        private XRTableCell xrTableCell2;
        private XRLabel lblNgaySinh;
        private XRTable xrTable3;
        private XRTableRow xrTableRow5;
        private XRTableCell xrTableCell3;
        private XRTableCell xrTableCell7;
        private XRTableCell xrTableCell4;
        private XRTableCell xrTableCell13;
        private XRTableCell xrTableCell18;
        private XRTableCell xrTableCell19;
        private XRTableCell xrTableCell20;
        private XRTableCell xrTableCellNetWage;
        private XRTableCell xrTableCell22;
        private XRTableCell xrTableCell23;
        private XRLabel xrLabel1;
        private XRTableCell xrTableCell17;
        private XRTableCell xrTableCell8;
        private XRTableCell xrTableCellInsurance;
        private XRTableCell xrTableCellTaxbleIncome;
        private XRTableCell xrTableCellPersonalIncomeTax;
        private XRTableCell xrTableCellOtherIncome;
        private XRTableCell xrTableCellAdditionalSalary;
        private XRTableCell xrTableCellSalaryAdvance;
        private XRTableCell xrTableCellTheWorkCost;
        private XRTableCell xrTableCellNetWages;
        private XRTableCell xrTableCellTotalOtherIncome;
        private XRTableCell xrTableCellTotalAdditionalSalary;
        private XRTableCell xrTableCellTotalSalaryAdvance;
        private XRTableCell xrTableCellTotalTheWorkCost;
        private XRTableCell xrTableCellTotalNetWage;
        private XRTable xrTable4;
        private XRTableRow xrTableRow2;
        private XRTableCell xrTableCell25;
        private XRTableRow xrTableRow3;
        private XRTableCell xrTableCell26;
        private XRTableCell xrTableCell27;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
        private IContainer components = null;

        public rp_SalaryDetailByMonth()
        {
            InitializeComponent();
            //
            // TODO: Add constructor logic here
            //
        }
        int _stt = 1;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCellOrder.Text = _stt.ToString();
            _stt++;
        }
        public void BindData(ReportFilter filter)
        {
            try
            {

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
            string resourceFileName = "rp_SalaryDetailByMonth.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.tblDetail = new DevExpress.XtraReports.UI.XRTable();
            this.xrDetailRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellOrder = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellStaffNumber = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellFullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTitle = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTaxCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellSalaryLevel = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellBasicSalary = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellResponsibilityAllowances = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOtherSubsidies = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellWorkOfDay = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellSalaryByDay = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellWorkOfDayOvertime = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellIntoMoney = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalIncome = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellInsurance = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTaxbleIncome = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellPersonalIncomeTax = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOtherIncome = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellAdditionalSalary = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellSalaryAdvance = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTheWorkCost = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNetWages = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNote = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tblReportHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCellTenBieuMau = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell601 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell602 = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.tblPageHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell241 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell242 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblNgaySinh = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblLuongHienHuong = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNetWage = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTextTongSo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalSalaryLevel = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalBasicSalary = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalResponsibilityAllowances = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalOtherSubsidies = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalWorkOfDay = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalSalaryByDay = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalWorkOfDayOvertime = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalIntoMoney = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellAmountIncome = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalInsurance = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalTẫbleIncome = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalPersonalIncomeTax = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalOtherIncome = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalAdditionalSalary = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalSalaryAdvance = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalTheWorkCost = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalNetWage = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongGhiChu = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblLapBang = new DevExpress.XtraReports.UI.XRLabel();
            this.lblThuTruong = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblReportHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
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
            this.tblDetail.SizeF = new System.Drawing.SizeF(1139.361F, 25F);
            this.tblDetail.StylePriority.UseBorders = false;
            // 
            // xrDetailRow1
            // 
            this.xrDetailRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellOrder,
            this.xrTableCellStaffNumber,
            this.xrTableCellFullName,
            this.xrTableCellTitle,
            this.xrTableCellTaxCode,
            this.xrTableCellSalaryLevel,
            this.xrTableCellBasicSalary,
            this.xrTableCellResponsibilityAllowances,
            this.xrTableCellOtherSubsidies,
            this.xrTableCellWorkOfDay,
            this.xrTableCellSalaryByDay,
            this.xrTableCellWorkOfDayOvertime,
            this.xrTableCellIntoMoney,
            this.xrTableCellTotalIncome,
            this.xrTableCellInsurance,
            this.xrTableCellTaxbleIncome,
            this.xrTableCellPersonalIncomeTax,
            this.xrTableCellOtherIncome,
            this.xrTableCellAdditionalSalary,
            this.xrTableCellSalaryAdvance,
            this.xrTableCellTheWorkCost,
            this.xrTableCellNetWages,
            this.xrTableCellNote});
            this.xrDetailRow1.Name = "xrDetailRow1";
            this.xrDetailRow1.Weight = 1D;
            // 
            // xrTableCellOrder
            // 
            this.xrTableCellOrder.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOrder.Name = "xrTableCellOrder";
            this.xrTableCellOrder.StylePriority.UseBorders = false;
            this.xrTableCellOrder.StylePriority.UseFont = false;
            this.xrTableCellOrder.StylePriority.UseTextAlignment = false;
            this.xrTableCellOrder.Text = " ";
            this.xrTableCellOrder.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOrder.Weight = 0.1421445286561015D;
            this.xrTableCellOrder.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrTableCellStaffNumber
            // 
            this.xrTableCellStaffNumber.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellStaffNumber.Name = "xrTableCellStaffNumber";
            this.xrTableCellStaffNumber.StylePriority.UseBorders = false;
            this.xrTableCellStaffNumber.StylePriority.UseTextAlignment = false;
            this.xrTableCellStaffNumber.Text = " ";
            this.xrTableCellStaffNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellStaffNumber.Weight = 0.30898923934632588D;
            // 
            // xrTableCellFullName
            // 
            this.xrTableCellFullName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellFullName.Name = "xrTableCellFullName";
            this.xrTableCellFullName.StylePriority.UseBorders = false;
            this.xrTableCellFullName.StylePriority.UseTextAlignment = false;
            this.xrTableCellFullName.Text = " ";
            this.xrTableCellFullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellFullName.Weight = 0.37079946882551229D;
            // 
            // xrTableCellTitle
            // 
            this.xrTableCellTitle.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTitle.Name = "xrTableCellTitle";
            this.xrTableCellTitle.StylePriority.UseBorders = false;
            this.xrTableCellTitle.StylePriority.UseTextAlignment = false;
            this.xrTableCellTitle.Text = " ";
            this.xrTableCellTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTitle.Weight = 0.30898926262235682D;
            // 
            // xrTableCellTaxCode
            // 
            this.xrTableCellTaxCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTaxCode.Name = "xrTableCellTaxCode";
            this.xrTableCellTaxCode.StylePriority.UseBorders = false;
            this.xrTableCellTaxCode.StylePriority.UseTextAlignment = false;
            this.xrTableCellTaxCode.Text = " ";
            this.xrTableCellTaxCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTaxCode.Weight = 0.18543073305254165D;
            // 
            // xrTableCellSalaryLevel
            // 
            this.xrTableCellSalaryLevel.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSalaryLevel.Name = "xrTableCellSalaryLevel";
            this.xrTableCellSalaryLevel.StylePriority.UseBorders = false;
            this.xrTableCellSalaryLevel.StylePriority.UseTextAlignment = false;
            this.xrTableCellSalaryLevel.Text = " ";
            this.xrTableCellSalaryLevel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSalaryLevel.Weight = 0.24470168776441323D;
            // 
            // xrTableCellBasicSalary
            // 
            this.xrTableCellBasicSalary.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellBasicSalary.Name = "xrTableCellBasicSalary";
            this.xrTableCellBasicSalary.StylePriority.UseBorders = false;
            this.xrTableCellBasicSalary.StylePriority.UseTextAlignment = false;
            this.xrTableCellBasicSalary.Text = " ";
            this.xrTableCellBasicSalary.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellBasicSalary.Weight = 0.31129524038478D;
            // 
            // xrTableCellResponsibilityAllowances
            // 
            this.xrTableCellResponsibilityAllowances.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellResponsibilityAllowances.Name = "xrTableCellResponsibilityAllowances";
            this.xrTableCellResponsibilityAllowances.StylePriority.UseBorders = false;
            this.xrTableCellResponsibilityAllowances.StylePriority.UseTextAlignment = false;
            this.xrTableCellResponsibilityAllowances.Text = " ";
            this.xrTableCellResponsibilityAllowances.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellResponsibilityAllowances.Weight = 0.30544458869804997D;
            // 
            // xrTableCellOtherSubsidies
            // 
            this.xrTableCellOtherSubsidies.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOtherSubsidies.Name = "xrTableCellOtherSubsidies";
            this.xrTableCellOtherSubsidies.StylePriority.UseBorders = false;
            this.xrTableCellOtherSubsidies.StylePriority.UseTextAlignment = false;
            this.xrTableCellOtherSubsidies.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOtherSubsidies.Weight = 0.30459190591779239D;
            // 
            // xrTableCellWorkOfDay
            // 
            this.xrTableCellWorkOfDay.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellWorkOfDay.Name = "xrTableCellWorkOfDay";
            this.xrTableCellWorkOfDay.StylePriority.UseBorders = false;
            this.xrTableCellWorkOfDay.StylePriority.UseTextAlignment = false;
            this.xrTableCellWorkOfDay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellWorkOfDay.Weight = 0.30898921762421128D;
            // 
            // xrTableCellSalaryByDay
            // 
            this.xrTableCellSalaryByDay.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSalaryByDay.Name = "xrTableCellSalaryByDay";
            this.xrTableCellSalaryByDay.StylePriority.UseBorders = false;
            this.xrTableCellSalaryByDay.StylePriority.UseTextAlignment = false;
            this.xrTableCellSalaryByDay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSalaryByDay.Weight = 0.30898924687607643D;
            // 
            // xrTableCellWorkOfDayOvertime
            // 
            this.xrTableCellWorkOfDayOvertime.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellWorkOfDayOvertime.Name = "xrTableCellWorkOfDayOvertime";
            this.xrTableCellWorkOfDayOvertime.StylePriority.UseBorders = false;
            this.xrTableCellWorkOfDayOvertime.StylePriority.UseTextAlignment = false;
            this.xrTableCellWorkOfDayOvertime.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellWorkOfDayOvertime.Weight = 0.24773638791709973D;
            // 
            // xrTableCellIntoMoney
            // 
            this.xrTableCellIntoMoney.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellIntoMoney.Name = "xrTableCellIntoMoney";
            this.xrTableCellIntoMoney.StylePriority.UseBorders = false;
            this.xrTableCellIntoMoney.StylePriority.UseTextAlignment = false;
            this.xrTableCellIntoMoney.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellIntoMoney.Weight = 0.25147778902658952D;
            // 
            // xrTableCellTotalIncome
            // 
            this.xrTableCellTotalIncome.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalIncome.Name = "xrTableCellTotalIncome";
            this.xrTableCellTotalIncome.StylePriority.UseBorders = false;
            this.xrTableCellTotalIncome.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalIncome.Text = " ";
            this.xrTableCellTotalIncome.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalIncome.Weight = 0.24720799764743406D;
            // 
            // xrTableCellInsurance
            // 
            this.xrTableCellInsurance.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellInsurance.Name = "xrTableCellInsurance";
            this.xrTableCellInsurance.StylePriority.UseBorders = false;
            this.xrTableCellInsurance.StylePriority.UseTextAlignment = false;
            this.xrTableCellInsurance.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellInsurance.Weight = 0.247179055808009D;
            // 
            // xrTableCellTaxbleIncome
            // 
            this.xrTableCellTaxbleIncome.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTaxbleIncome.Name = "xrTableCellTaxbleIncome";
            this.xrTableCellTaxbleIncome.StylePriority.UseBorders = false;
            this.xrTableCellTaxbleIncome.StylePriority.UseTextAlignment = false;
            this.xrTableCellTaxbleIncome.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTaxbleIncome.Weight = 0.24717905580800897D;
            // 
            // xrTableCellPersonalIncomeTax
            // 
            this.xrTableCellPersonalIncomeTax.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellPersonalIncomeTax.Name = "xrTableCellPersonalIncomeTax";
            this.xrTableCellPersonalIncomeTax.StylePriority.UseBorders = false;
            this.xrTableCellPersonalIncomeTax.StylePriority.UseTextAlignment = false;
            this.xrTableCellPersonalIncomeTax.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellPersonalIncomeTax.Weight = 0.24726524342451742D;
            // 
            // xrTableCellOtherIncome
            // 
            this.xrTableCellOtherIncome.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOtherIncome.Name = "xrTableCellOtherIncome";
            this.xrTableCellOtherIncome.StylePriority.UseBorders = false;
            this.xrTableCellOtherIncome.StylePriority.UseTextAlignment = false;
            this.xrTableCellOtherIncome.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellOtherIncome.Weight = 0.24720725342870611D;
            // 
            // xrTableCellAdditionalSalary
            // 
            this.xrTableCellAdditionalSalary.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellAdditionalSalary.Name = "xrTableCellAdditionalSalary";
            this.xrTableCellAdditionalSalary.StylePriority.UseBorders = false;
            this.xrTableCellAdditionalSalary.StylePriority.UseTextAlignment = false;
            this.xrTableCellAdditionalSalary.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellAdditionalSalary.Weight = 0.24717906762100494D;
            // 
            // xrTableCellSalaryAdvance
            // 
            this.xrTableCellSalaryAdvance.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSalaryAdvance.Name = "xrTableCellSalaryAdvance";
            this.xrTableCellSalaryAdvance.StylePriority.UseBorders = false;
            this.xrTableCellSalaryAdvance.StylePriority.UseTextAlignment = false;
            this.xrTableCellSalaryAdvance.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSalaryAdvance.Weight = 0.24329404503951954D;
            // 
            // xrTableCellTheWorkCost
            // 
            this.xrTableCellTheWorkCost.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTheWorkCost.Name = "xrTableCellTheWorkCost";
            this.xrTableCellTheWorkCost.StylePriority.UseBorders = false;
            this.xrTableCellTheWorkCost.StylePriority.UseTextAlignment = false;
            this.xrTableCellTheWorkCost.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTheWorkCost.Weight = 0.31295295388871536D;
            // 
            // xrTableCellNetWages
            // 
            this.xrTableCellNetWages.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNetWages.Name = "xrTableCellNetWages";
            this.xrTableCellNetWages.StylePriority.UseBorders = false;
            this.xrTableCellNetWages.StylePriority.UseTextAlignment = false;
            this.xrTableCellNetWages.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNetWages.Weight = 0.309012469292507D;
            // 
            // xrTableCellNote
            // 
            this.xrTableCellNote.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNote.Name = "xrTableCellNote";
            this.xrTableCellNote.StylePriority.UseBorders = false;
            this.xrTableCellNote.StylePriority.UseTextAlignment = false;
            this.xrTableCellNote.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNote.Weight = 1.1084730115491215D;
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
            this.xrTable4,
            this.tblReportHeader});
            this.ReportHeader.HeightF = 86F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrTable4
            // 
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 42F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2,
            this.xrTableRow3});
            this.xrTable4.SizeF = new System.Drawing.SizeF(1140F, 34F);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell25});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 0.79999990231146945D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.StylePriority.UseFont = false;
            this.xrTableCell25.Text = "BẢNG LƯƠNG THÁNG 9 / 2017";
            this.xrTableCell25.Weight = 4.867675964494425D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell26,
            this.xrTableCell27});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 0.33333324536656561D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrTableCell26.StylePriority.UsePadding = false;
            this.xrTableCell26.Weight = 1.5748560433256003D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.Weight = 3.2928199438412697D;
            // 
            // tblReportHeader
            // 
            this.tblReportHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tblReportHeader.Name = "tblReportHeader";
            this.tblReportHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow19,
            this.xrTableRow4});
            this.tblReportHeader.SizeF = new System.Drawing.SizeF(1140F, 34F);
            // 
            // xrTableRow19
            // 
            this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCellTenBieuMau});
            this.xrTableRow19.Name = "xrTableRow19";
            this.xrTableRow19.Weight = 0.79999990231146945D;
            // 
            // xrCellTenBieuMau
            // 
            this.xrCellTenBieuMau.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrCellTenBieuMau.Name = "xrCellTenBieuMau";
            this.xrCellTenBieuMau.StylePriority.UseFont = false;
            this.xrCellTenBieuMau.StylePriority.UseTextAlignment = false;
            this.xrCellTenBieuMau.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCellTenBieuMau.Weight = 4.867675964494425D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell601,
            this.xrTableCell602});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 0.33333324536656561D;
            // 
            // xrTableCell601
            // 
            this.xrTableCell601.Name = "xrTableCell601";
            this.xrTableCell601.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrTableCell601.StylePriority.UsePadding = false;
            this.xrTableCell601.Weight = 1.5748560433256003D;
            // 
            // xrTableCell602
            // 
            this.xrTableCell602.Name = "xrTableCell602";
            this.xrTableCell602.Weight = 3.2928199438412697D;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblPageHeader});
            this.PageHeader.HeightF = 100F;
            this.PageHeader.Name = "PageHeader";
            // 
            // tblPageHeader
            // 
            this.tblPageHeader.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tblPageHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tblPageHeader.Name = "tblPageHeader";
            this.tblPageHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
            this.tblPageHeader.SizeF = new System.Drawing.SizeF(1140F, 100F);
            this.tblPageHeader.StylePriority.UseBorders = false;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell241,
            this.xrTableCell10,
            this.xrTableCell242,
            this.xrTableCell14,
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell2,
            this.xrTableCell1,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell4,
            this.xrTableCell13,
            this.xrTableCell18,
            this.xrTableCell19,
            this.xrTableCell20,
            this.xrTableCellNetWage,
            this.xrTableCell22,
            this.xrTableCell23});
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
            this.xrTableCell241.Weight = 0.09800902190058941D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell10.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseBorderColor = false;
            this.xrTableCell10.StylePriority.UseBorders = false;
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.Text = "Số hiệu cán bộ";
            this.xrTableCell10.Weight = 0.21306288811730695D;
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
            this.xrTableCell242.Weight = 0.25567546770048D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell14.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseBorderColor = false;
            this.xrTableCell14.StylePriority.UseBorders = false;
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.Text = "Chức danh";
            this.xrTableCell14.Weight = 0.21306296950360582D;
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
            this.xrTableCell15.Text = "Mã số thuế TNCN";
            this.xrTableCell15.Weight = 0.12783764912034157D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell16.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell16.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseBorderColor = false;
            this.xrTableCell16.StylePriority.UseBorders = false;
            this.xrTableCell16.StylePriority.UseFont = false;
            this.xrTableCell16.Text = "Mức lương";
            this.xrTableCell16.Weight = 0.17045030931197369D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblNgaySinh,
            this.xrTable3});
            this.xrTableCell2.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorderColor = false;
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Weight = 0.63568395161843427D;
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lblNgaySinh.LocationFloat = new DevExpress.Utils.PointFloat(3.051758E-05F, 0F);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblNgaySinh.SizeF = new System.Drawing.SizeF(149.1775F, 25F);
            this.lblNgaySinh.StylePriority.UseBorders = false;
            this.lblNgaySinh.Text = "Trong đó:";
            // 
            // xrTable3
            // 
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24.99999F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrTable3.SizeF = new System.Drawing.SizeF(148.3549F, 75F);
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell17,
            this.xrTableCell7});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorderColor = false;
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.Text = "Lương cơ bản";
            this.xrTableCell3.Weight = 0.99295049746749708D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell17.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.StylePriority.UseBorderColor = false;
            this.xrTableCell17.StylePriority.UseBorders = false;
            this.xrTableCell17.Text = "Phụ cấp trách nhiệm";
            this.xrTableCell17.Weight = 0.97661562017560943D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBorderColor = false;
            this.xrTableCell7.StylePriority.UseBorders = false;
            this.xrTableCell7.Text = "Các khoản trợ cấp khác";
            this.xrTableCell7.Weight = 0.97661562017560943D;
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
            this.xrTableCell1.Text = "Ngày công";
            this.xrTableCell1.Weight = 0.21306297584986009D;
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
            this.xrTableCell8.Text = "Lương theo ngày công";
            this.xrTableCell8.Weight = 0.21306297584986009D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell9.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblLuongHienHuong,
            this.xrTable2});
            this.xrTableCell9.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorderColor = false;
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.Text = "xrTableCell9";
            this.xrTableCell9.Weight = 0.34201055055712803D;
            // 
            // lblLuongHienHuong
            // 
            this.lblLuongHienHuong.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lblLuongHienHuong.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblLuongHienHuong.Name = "lblLuongHienHuong";
            this.lblLuongHienHuong.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblLuongHienHuong.SizeF = new System.Drawing.SizeF(79.99997F, 25F);
            this.lblLuongHienHuong.StylePriority.UseBorders = false;
            this.lblLuongHienHuong.Text = "Làm thêm giờ";
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25.00001F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrTable2.SizeF = new System.Drawing.SizeF(80F, 75F);
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell12});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBorders = false;
            this.xrTableCell11.Text = "Ngày công";
            this.xrTableCell11.Weight = 0.69565218303724341D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseBorders = false;
            this.xrTableCell12.Text = "Thành tiền";
            this.xrTableCell12.Weight = 0.69565216478884362D;
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
            this.xrTableCell5.Text = "Tổng thu nhập";
            this.xrTableCell5.Weight = 0.17045032916714487D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell6.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseBorderColor = false;
            this.xrTableCell6.StylePriority.UseBorders = false;
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.Text = "BHYT, BHXH, BHTN (10,5%)";
            this.xrTableCell6.Weight = 0.1704502496789766D;
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
            this.xrTableCell4.Text = "Thu nhập tính thuế TNCN";
            this.xrTableCell4.Weight = 0.17045024967897615D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell13.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell13.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseBorderColor = false;
            this.xrTableCell13.StylePriority.UseBorders = false;
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.Text = "Thuế thu nhập cá nhân";
            this.xrTableCell13.Weight = 0.1704502496789766D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell18.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell18.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseBorderColor = false;
            this.xrTableCell18.StylePriority.UseBorders = false;
            this.xrTableCell18.StylePriority.UseFont = false;
            this.xrTableCell18.Text = "Thu khác";
            this.xrTableCell18.Weight = 0.1704502496789766D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell19.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell19.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.StylePriority.UseBorderColor = false;
            this.xrTableCell19.StylePriority.UseBorders = false;
            this.xrTableCell19.StylePriority.UseFont = false;
            this.xrTableCell19.Text = "Bổ sung lương tháng";
            this.xrTableCell19.Weight = 0.17045024967897637D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell20.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell20.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseBorderColor = false;
            this.xrTableCell20.StylePriority.UseBorders = false;
            this.xrTableCell20.StylePriority.UseFont = false;
            this.xrTableCell20.Text = "Tạm ứng lương";
            this.xrTableCell20.Weight = 0.17045076985204544D;
            // 
            // xrTableCellNetWage
            // 
            this.xrTableCellNetWage.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCellNetWage.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNetWage.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellNetWage.Name = "xrTableCellNetWage";
            this.xrTableCellNetWage.StylePriority.UseBorderColor = false;
            this.xrTableCellNetWage.StylePriority.UseBorders = false;
            this.xrTableCellNetWage.StylePriority.UseFont = false;
            this.xrTableCellNetWage.Text = "Công tác phí tháng 9 / 2017";
            this.xrTableCellNetWage.Weight = 0.21306297489205678D;
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
            this.xrTableCell22.Text = "Thực lãnh c/khoản";
            this.xrTableCell22.Weight = 0.21306297489205681D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell23.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell23.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StylePriority.UseBorderColor = false;
            this.xrTableCell23.StylePriority.UseBorders = false;
            this.xrTableCell23.StylePriority.UseFont = false;
            this.xrTableCell23.Text = "Ghi chú";
            this.xrTableCell23.Weight = 0.7670276148392593D;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.lblReportDate,
            this.xrTable1,
            this.lblLapBang,
            this.lblThuTruong});
            this.ReportFooter.HeightF = 252.1667F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(385.9599F, 61.54176F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(180F, 25F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "PHÒNG KẾ TOÁN";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblReportDate
            // 
            this.lblReportDate.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Italic);
            this.lblReportDate.LocationFloat = new DevExpress.Utils.PointFloat(687.391F, 46.54177F);
            this.lblReportDate.Name = "lblReportDate";
            this.lblReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblReportDate.SizeF = new System.Drawing.SizeF(300F, 15F);
            this.lblReportDate.StylePriority.UseFont = false;
            this.lblReportDate.StylePriority.UseTextAlignment = false;
            this.lblReportDate.Text = "{0}, ngày {1} tháng {2} năm {3}";
            this.lblReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            this.xrTextTongSo,
            this.xrTableCellTotalSalaryLevel,
            this.xrTableCellTotalBasicSalary,
            this.xrTableCellTotalResponsibilityAllowances,
            this.xrTableCellTotalOtherSubsidies,
            this.xrTableCellTotalWorkOfDay,
            this.xrTableCellTotalSalaryByDay,
            this.xrTableCellTotalWorkOfDayOvertime,
            this.xrTableCellTotalIntoMoney,
            this.xrTableCellAmountIncome,
            this.xrTableCellTotalInsurance,
            this.xrTableCellTotalTẫbleIncome,
            this.xrTableCellTotalPersonalIncomeTax,
            this.xrTableCellTotalOtherIncome,
            this.xrTableCellTotalAdditionalSalary,
            this.xrTableCellTotalSalaryAdvance,
            this.xrTableCellTotalTheWorkCost,
            this.xrTableCellTotalNetWage,
            this.xrTongGhiChu});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTextTongSo
            // 
            this.xrTextTongSo.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTextTongSo.Name = "xrTextTongSo";
            this.xrTextTongSo.StylePriority.UseBorders = false;
            this.xrTextTongSo.StylePriority.UseTextAlignment = false;
            this.xrTextTongSo.Text = "TỔNG CỘNG";
            this.xrTextTongSo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTextTongSo.Weight = 1.0500524863497502D;
            // 
            // xrTableCellTotalSalaryLevel
            // 
            this.xrTableCellTotalSalaryLevel.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalSalaryLevel.Name = "xrTableCellTotalSalaryLevel";
            this.xrTableCellTotalSalaryLevel.StylePriority.UseBorders = false;
            this.xrTableCellTotalSalaryLevel.Weight = 0.19719277373462407D;
            // 
            // xrTableCellTotalBasicSalary
            // 
            this.xrTableCellTotalBasicSalary.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalBasicSalary.Name = "xrTableCellTotalBasicSalary";
            this.xrTableCellTotalBasicSalary.StylePriority.UseBorders = false;
            this.xrTableCellTotalBasicSalary.Weight = 0.24629441009020736D;
            // 
            // xrTableCellTotalResponsibilityAllowances
            // 
            this.xrTableCellTotalResponsibilityAllowances.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalResponsibilityAllowances.Name = "xrTableCellTotalResponsibilityAllowances";
            this.xrTableCellTotalResponsibilityAllowances.StylePriority.UseBorders = false;
            this.xrTableCellTotalResponsibilityAllowances.Weight = 0.24263382704377121D;
            // 
            // xrTableCellTotalOtherSubsidies
            // 
            this.xrTableCellTotalOtherSubsidies.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalOtherSubsidies.Name = "xrTableCellTotalOtherSubsidies";
            this.xrTableCellTotalOtherSubsidies.StylePriority.UseBorders = false;
            this.xrTableCellTotalOtherSubsidies.Weight = 0.24649127553842243D;
            // 
            // xrTableCellTotalWorkOfDay
            // 
            this.xrTableCellTotalWorkOfDay.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalWorkOfDay.Name = "xrTableCellTotalWorkOfDay";
            this.xrTableCellTotalWorkOfDay.StylePriority.UseBorders = false;
            this.xrTableCellTotalWorkOfDay.Weight = 0.24649108079341353D;
            // 
            // xrTableCellTotalSalaryByDay
            // 
            this.xrTableCellTotalSalaryByDay.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalSalaryByDay.Name = "xrTableCellTotalSalaryByDay";
            this.xrTableCellTotalSalaryByDay.StylePriority.UseBorders = false;
            this.xrTableCellTotalSalaryByDay.Weight = 0.24649168653794717D;
            // 
            // xrTableCellTotalWorkOfDayOvertime
            // 
            this.xrTableCellTotalWorkOfDayOvertime.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalWorkOfDayOvertime.Name = "xrTableCellTotalWorkOfDayOvertime";
            this.xrTableCellTotalWorkOfDayOvertime.StylePriority.UseBorders = false;
            this.xrTableCellTotalWorkOfDayOvertime.Weight = 0.19719305952910282D;
            // 
            // xrTableCellTotalIntoMoney
            // 
            this.xrTableCellTotalIntoMoney.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalIntoMoney.Name = "xrTableCellTotalIntoMoney";
            this.xrTableCellTotalIntoMoney.StylePriority.UseBorders = false;
            this.xrTableCellTotalIntoMoney.Weight = 0.19847660156610714D;
            // 
            // xrTableCellAmountIncome
            // 
            this.xrTableCellAmountIncome.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellAmountIncome.Name = "xrTableCellAmountIncome";
            this.xrTableCellAmountIncome.StylePriority.UseBorders = false;
            this.xrTableCellAmountIncome.Weight = 0.19719299459999817D;
            // 
            // xrTableCellTotalInsurance
            // 
            this.xrTableCellTotalInsurance.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalInsurance.Name = "xrTableCellTotalInsurance";
            this.xrTableCellTotalInsurance.StylePriority.UseBorders = false;
            this.xrTableCellTotalInsurance.Weight = 0.19719238911940651D;
            // 
            // xrTableCellTotalTẫbleIncome
            // 
            this.xrTableCellTotalTẫbleIncome.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalTẫbleIncome.Name = "xrTableCellTotalTẫbleIncome";
            this.xrTableCellTotalTẫbleIncome.StylePriority.UseBorders = false;
            this.xrTableCellTotalTẫbleIncome.Weight = 0.19719328862993857D;
            // 
            // xrTableCellTotalPersonalIncomeTax
            // 
            this.xrTableCellTotalPersonalIncomeTax.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalPersonalIncomeTax.Name = "xrTableCellTotalPersonalIncomeTax";
            this.xrTableCellTotalPersonalIncomeTax.StylePriority.UseBorders = false;
            this.xrTableCellTotalPersonalIncomeTax.Weight = 0.197192074501354D;
            // 
            // xrTableCellTotalOtherIncome
            // 
            this.xrTableCellTotalOtherIncome.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalOtherIncome.Name = "xrTableCellTotalOtherIncome";
            this.xrTableCellTotalOtherIncome.StylePriority.UseBorders = false;
            this.xrTableCellTotalOtherIncome.Weight = 0.197192074501354D;
            // 
            // xrTableCellTotalAdditionalSalary
            // 
            this.xrTableCellTotalAdditionalSalary.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalAdditionalSalary.Name = "xrTableCellTotalAdditionalSalary";
            this.xrTableCellTotalAdditionalSalary.StylePriority.UseBorders = false;
            this.xrTableCellTotalAdditionalSalary.Weight = 0.197192074501354D;
            // 
            // xrTableCellTotalSalaryAdvance
            // 
            this.xrTableCellTotalSalaryAdvance.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalSalaryAdvance.Name = "xrTableCellTotalSalaryAdvance";
            this.xrTableCellTotalSalaryAdvance.StylePriority.UseBorders = false;
            this.xrTableCellTotalSalaryAdvance.Weight = 0.197192074501354D;
            // 
            // xrTableCellTotalTheWorkCost
            // 
            this.xrTableCellTotalTheWorkCost.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalTheWorkCost.Name = "xrTableCellTotalTheWorkCost";
            this.xrTableCellTotalTheWorkCost.StylePriority.UseBorders = false;
            this.xrTableCellTotalTheWorkCost.Weight = 0.2464955054340211D;
            // 
            // xrTableCellTotalNetWage
            // 
            this.xrTableCellTotalNetWage.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalNetWage.Name = "xrTableCellTotalNetWage";
            this.xrTableCellTotalNetWage.StylePriority.UseBorders = false;
            this.xrTableCellTotalNetWage.Weight = 0.2464955054340211D;
            // 
            // xrTongGhiChu
            // 
            this.xrTongGhiChu.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongGhiChu.Name = "xrTongGhiChu";
            this.xrTongGhiChu.StylePriority.UseBorders = false;
            this.xrTongGhiChu.Weight = 0.88736914856913773D;
            // 
            // lblLapBang
            // 
            this.lblLapBang.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.lblLapBang.LocationFloat = new DevExpress.Utils.PointFloat(108.5785F, 61.54176F);
            this.lblLapBang.Name = "lblLapBang";
            this.lblLapBang.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblLapBang.SizeF = new System.Drawing.SizeF(180F, 25F);
            this.lblLapBang.StylePriority.UseFont = false;
            this.lblLapBang.StylePriority.UseTextAlignment = false;
            this.lblLapBang.Text = "TỔNG GIÁM ĐỐC";
            this.lblLapBang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblThuTruong
            // 
            this.lblThuTruong.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.lblThuTruong.LocationFloat = new DevExpress.Utils.PointFloat(687.391F, 61.54176F);
            this.lblThuTruong.Name = "lblThuTruong";
            this.lblThuTruong.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblThuTruong.SizeF = new System.Drawing.SizeF(300F, 25F);
            this.lblThuTruong.StylePriority.UseFont = false;
            this.lblThuTruong.StylePriority.UseTextAlignment = false;
            this.lblThuTruong.Text = "NGƯỜI LẬP BẢNG";
            this.lblThuTruong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // rp_SalaryDetailByMonth
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter});
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
            ((System.ComponentModel.ISupportInitialize)(this.tblReportHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}