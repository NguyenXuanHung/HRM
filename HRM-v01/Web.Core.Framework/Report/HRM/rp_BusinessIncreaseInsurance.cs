using System;
using System.Drawing.Printing;
using DevExpress.XtraReports.UI;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Object.Report;
using System.Globalization;
using Web.Core.Framework.SQLAdapter;

/// <summary>
/// Summary description for rp_BusinessIncreaseInsurance
/// </summary>
public class rp_BusinessIncreaseInsurance : XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private PageHeaderBand PageHeader;
    private ReportHeaderBand ReportHeader;
    private ReportFooterBand ReportFooter;
    private XRLabel xrl_TitleReport;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell4;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrCellIndex;
    private XRTableCell xrCellInsuranceNumber;
    private XRTableCell xrCellPosition;
    private XRLabel lblCreator;
    private XRLabel lblDepartment;
    private XRLabel xrl_footer3;
    private XRLabel xrl_footer1;
    private XRTableCell xrTableCell9;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrt_GroupDepartment;
    private XRLabel lblReportDate;
    private XRTableCell xrCellSalary;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell16;
    private XRTableCell xrCellSeniorityFactor;
    private XRTableCell xrTableCell5;
    private XRTableCell xrCellPositionFactor;
    private XRTableCell xrCellFullName;
    private XRTableCell xrTableCell2;
    private XRLabel xrLabel10;
    private XRLabel xrLabel9;
    private XRLabel xrLabel8;
    private XRLabel xrLabel7;
    private XRLabel xrLabel6;
    private XRLabel xrLabel5;
    private XRLabel xrLabel4;
    private XRLabel xrLabel3;
    private XRTableCell xrTableCell3;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
    private XRLabel lblAddress;
    private XRLabel lblIdCompany;
    private XRLabel lblCompany;
    private XRTableCell xrCellOccupationFactor;
    private XRTableCell xrCellSalaryFactor;
    private XRTableCell xrCellAddition;
    private XRTableCell xrCellFromDate;
    private XRTableCell xrCellToDate;
    private XRTableCell xrCellNote;
    private XRLabel xrLabel15;
    private XRLabel lblFooterReportDate;
    private XRLabel xrLabel13;
    private XRLabel lblInsuranceNumber;
    private XRLabel lblInsuranceBook;
    private XRTable xrTable4;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrCellTotalSalary;
    private XRTableCell xrCellTotalPositionFactor;
    private XRTableCell xrCellTotalSeniorityFactor;
    private XRTableCell xrCellTotalOccupationFactor;
    private XRTableCell xrCellTotalSalaryFactor;
    private XRTableCell xrCellTotalAddition;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rp_BusinessIncreaseInsurance()
    {
        InitializeComponent();

    }

    int STT = 1;
    private void Detail_BeforePrint(object sender, PrintEventArgs e)
    {
        xrCellIndex.Text = STT.ToString();
        STT++;
    }
    private void Group_BeforePrint(object sender, PrintEventArgs e)
    {
        STT = 1;
        xrCellIndex.Text = STT.ToString();

    }

    public void BindData(ReportFilter filter)
    {
        try
        {
            ReportController controller = new ReportController();
            lblReportDate.Text = string.Format(lblReportDate.Text, DateTime.Now.Day,
                DateTime.Now.Month, DateTime.Now.Year);
            lblFooterReportDate.Text = string.Format(lblFooterReportDate.Text, DateTime.Now.Day,
                DateTime.Now.Month, DateTime.Now.Year);
            lblCompany.Text = controller.GetCompanyName(filter.SessionDepartment);
            lblAddress.Text = controller.GetCompanyAddress(filter.SessionDepartment);
           
            var departments = filter.SelectedDepartment;
            var arrDepartment = departments.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < arrDepartment.Length; i++)
            {
                arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
            }
            // get data from date
            var fromDate = filter.StartDate != null
                ? filter.StartDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                : string.Empty;

            // get data from date
            var toDate = filter.EndDate != null
                ? filter.EndDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                : string.Empty;

            // select form db 
            var table = SQLHelper.ExecuteTable(HRMBusinessAdapter.GetStore_InsuranceIncreaseDecrease(string.Join(",", arrDepartment), false, null,fromDate,toDate));
            DataSource = table;

            //binding data
            xrCellFullName.DataBindings.Add("Text", DataSource, "FullName");
            xrCellAddition.DataBindings.Add("Text", DataSource, "AdditionAllowance", "{0:#,###}");
            xrCellFromDate.DataBindings.Add("Text", DataSource, "FromDate", "{0:MM/yyyy}");
            xrCellInsuranceNumber.DataBindings.Add("Text", DataSource, "InsuranceNumber");
            xrCellNote.DataBindings.Add("Text", DataSource, "Note");
            xrCellOccupationFactor.DataBindings.Add("Text", DataSource, "SeniorityJobAllowance", "{0:#,###}");
            xrCellPosition.DataBindings.Add("Text", DataSource, "PositionName");
            xrCellPositionFactor.DataBindings.Add("Text", DataSource, "PositionAllowance", "{0:#,###}");
            xrCellSalary.DataBindings.Add("Text", DataSource, "SalaryBasic", "{0:#,###}");
            xrCellSalaryFactor.DataBindings.Add("Text", DataSource, "SalaryAllowance", "{0:#,###}");
            xrCellToDate.DataBindings.Add("Text", DataSource, "ToDate", "{0:MM/yyyy}");
            xrCellSeniorityFactor.DataBindings.Add("Text", DataSource, "SeniorityOutOfFrameAllowance", "{0:#,###}");
            xrCellTotalSalary.DataBindings.Add("Text", DataSource, "TotalSalaryBasic", "{0:#,###}");

            GroupHeader1.GroupFields.AddRange(new[] {
                new GroupField("DepartmentId", XRColumnSortOrder.Ascending)});
            xrt_GroupDepartment.DataBindings.Add("Text", DataSource, "DepartmentName");
        }
        catch (Exception ex)
        {
            Dialog.ShowNotification("Có lỗi xảy ra: " + ex.Message);
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
            string resourceFileName = "rp_BusinessIncreaseInsurance.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCellIndex = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellFullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellInsuranceNumber = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellPosition = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellSalary = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellPositionFactor = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellSeniorityFactor = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellOccupationFactor = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellSalaryFactor = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellAddition = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellFromDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellToDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellNote = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblAddress = new DevExpress.XtraReports.UI.XRLabel();
            this.lblIdCompany = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCompany = new DevExpress.XtraReports.UI.XRLabel();
            this.lblReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TitleReport = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTotalSalary = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTotalPositionFactor = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTotalSeniorityFactor = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTotalOccupationFactor = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTotalSalaryFactor = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellTotalAddition = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblFooterReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblInsuranceNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.lblInsuranceBook = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCreator = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDepartment = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer1 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrt_GroupDepartment = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
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
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1146F, 25F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCellIndex,
            this.xrCellFullName,
            this.xrCellInsuranceNumber,
            this.xrCellPosition,
            this.xrCellSalary,
            this.xrCellPositionFactor,
            this.xrCellSeniorityFactor,
            this.xrCellOccupationFactor,
            this.xrCellSalaryFactor,
            this.xrCellAddition,
            this.xrCellFromDate,
            this.xrCellToDate,
            this.xrCellNote});
            this.xrTableRow2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
            this.xrTableRow2.StylePriority.UseFont = false;
            this.xrTableRow2.StylePriority.UsePadding = false;
            this.xrTableRow2.StylePriority.UseTextAlignment = false;
            this.xrTableRow2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow2.Weight = 1D;
            // 
            // xrCellIndex
            // 
            this.xrCellIndex.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellIndex.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellIndex.Name = "xrCellIndex";
            this.xrCellIndex.StylePriority.UseBorders = false;
            this.xrCellIndex.StylePriority.UseFont = false;
            this.xrCellIndex.StylePriority.UseTextAlignment = false;
            this.xrCellIndex.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellIndex.Weight = 0.066407880703515554D;
            this.xrCellIndex.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrCellFullName
            // 
            this.xrCellFullName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellFullName.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellFullName.Name = "xrCellFullName";
            this.xrCellFullName.StylePriority.UseBorders = false;
            this.xrCellFullName.StylePriority.UseFont = false;
            this.xrCellFullName.StylePriority.UseTextAlignment = false;
            this.xrCellFullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCellFullName.Weight = 0.33979896811899235D;
            // 
            // xrCellInsuranceNumber
            // 
            this.xrCellInsuranceNumber.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellInsuranceNumber.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellInsuranceNumber.Name = "xrCellInsuranceNumber";
            this.xrCellInsuranceNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellInsuranceNumber.StylePriority.UseBorders = false;
            this.xrCellInsuranceNumber.StylePriority.UseFont = false;
            this.xrCellInsuranceNumber.StylePriority.UsePadding = false;
            this.xrCellInsuranceNumber.StylePriority.UseTextAlignment = false;
            this.xrCellInsuranceNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellInsuranceNumber.Weight = 0.14968803693524957D;
            // 
            // xrCellPosition
            // 
            this.xrCellPosition.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellPosition.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellPosition.Name = "xrCellPosition";
            this.xrCellPosition.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellPosition.StylePriority.UseBorders = false;
            this.xrCellPosition.StylePriority.UseFont = false;
            this.xrCellPosition.StylePriority.UsePadding = false;
            this.xrCellPosition.StylePriority.UseTextAlignment = false;
            this.xrCellPosition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCellPosition.Weight = 0.24589787956895293D;
            // 
            // xrCellSalary
            // 
            this.xrCellSalary.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellSalary.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellSalary.Name = "xrCellSalary";
            this.xrCellSalary.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellSalary.StylePriority.UseBorders = false;
            this.xrCellSalary.StylePriority.UseFont = false;
            this.xrCellSalary.StylePriority.UsePadding = false;
            this.xrCellSalary.StylePriority.UseTextAlignment = false;
            this.xrCellSalary.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCellSalary.Weight = 0.1565863486977348D;
            // 
            // xrCellPositionFactor
            // 
            this.xrCellPositionFactor.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellPositionFactor.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellPositionFactor.Name = "xrCellPositionFactor";
            this.xrCellPositionFactor.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellPositionFactor.StylePriority.UseBorders = false;
            this.xrCellPositionFactor.StylePriority.UseFont = false;
            this.xrCellPositionFactor.StylePriority.UsePadding = false;
            this.xrCellPositionFactor.StylePriority.UseTextAlignment = false;
            this.xrCellPositionFactor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCellPositionFactor.Weight = 0.14883489501397695D;
            // 
            // xrCellSeniorityFactor
            // 
            this.xrCellSeniorityFactor.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellSeniorityFactor.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellSeniorityFactor.Name = "xrCellSeniorityFactor";
            this.xrCellSeniorityFactor.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellSeniorityFactor.StylePriority.UseBorders = false;
            this.xrCellSeniorityFactor.StylePriority.UseFont = false;
            this.xrCellSeniorityFactor.StylePriority.UsePadding = false;
            this.xrCellSeniorityFactor.StylePriority.UseTextAlignment = false;
            this.xrCellSeniorityFactor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCellSeniorityFactor.Weight = 0.14859743475443676D;
            // 
            // xrCellOccupationFactor
            // 
            this.xrCellOccupationFactor.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellOccupationFactor.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellOccupationFactor.Name = "xrCellOccupationFactor";
            this.xrCellOccupationFactor.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellOccupationFactor.StylePriority.UseBorders = false;
            this.xrCellOccupationFactor.StylePriority.UseFont = false;
            this.xrCellOccupationFactor.StylePriority.UsePadding = false;
            this.xrCellOccupationFactor.StylePriority.UseTextAlignment = false;
            this.xrCellOccupationFactor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCellOccupationFactor.Weight = 0.15078589887564686D;
            // 
            // xrCellSalaryFactor
            // 
            this.xrCellSalaryFactor.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellSalaryFactor.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellSalaryFactor.Name = "xrCellSalaryFactor";
            this.xrCellSalaryFactor.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellSalaryFactor.StylePriority.UseBorders = false;
            this.xrCellSalaryFactor.StylePriority.UseFont = false;
            this.xrCellSalaryFactor.StylePriority.UsePadding = false;
            this.xrCellSalaryFactor.StylePriority.UseTextAlignment = false;
            this.xrCellSalaryFactor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCellSalaryFactor.Weight = 0.14053972821149247D;
            // 
            // xrCellAddition
            // 
            this.xrCellAddition.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellAddition.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellAddition.Name = "xrCellAddition";
            this.xrCellAddition.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellAddition.StylePriority.UseBorders = false;
            this.xrCellAddition.StylePriority.UseFont = false;
            this.xrCellAddition.StylePriority.UsePadding = false;
            this.xrCellAddition.StylePriority.UseTextAlignment = false;
            this.xrCellAddition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCellAddition.Weight = 0.17622656619916083D;
            // 
            // xrCellFromDate
            // 
            this.xrCellFromDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellFromDate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellFromDate.Name = "xrCellFromDate";
            this.xrCellFromDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellFromDate.StylePriority.UseBorders = false;
            this.xrCellFromDate.StylePriority.UseFont = false;
            this.xrCellFromDate.StylePriority.UsePadding = false;
            this.xrCellFromDate.StylePriority.UseTextAlignment = false;
            this.xrCellFromDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellFromDate.Weight = 0.12910633576169389D;
            // 
            // xrCellToDate
            // 
            this.xrCellToDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellToDate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellToDate.Name = "xrCellToDate";
            this.xrCellToDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellToDate.StylePriority.UseBorders = false;
            this.xrCellToDate.StylePriority.UseFont = false;
            this.xrCellToDate.StylePriority.UsePadding = false;
            this.xrCellToDate.StylePriority.UseTextAlignment = false;
            this.xrCellToDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellToDate.Weight = 0.12910633576169389D;
            // 
            // xrCellNote
            // 
            this.xrCellNote.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellNote.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrCellNote.Name = "xrCellNote";
            this.xrCellNote.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellNote.StylePriority.UseBorders = false;
            this.xrCellNote.StylePriority.UseFont = false;
            this.xrCellNote.StylePriority.UsePadding = false;
            this.xrCellNote.StylePriority.UseTextAlignment = false;
            this.xrCellNote.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCellNote.Weight = 0.27281181795274045D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 46F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 61F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.PageHeader.HeightF = 100F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0.0002066294F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1146F, 100F);
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
            this.xrTableCell9,
            this.xrTableCell6,
            this.xrTableCell3,
            this.xrTableCell16,
            this.xrTableCell5});
            this.xrTableRow1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
            this.xrTableRow1.StylePriority.UseFont = false;
            this.xrTableRow1.StylePriority.UsePadding = false;
            this.xrTableRow1.StylePriority.UseTextAlignment = false;
            this.xrTableRow1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "STT";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell1.Weight = 0.069800694106641983D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "Họ và tên";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 0.35716157981723418D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell4.Multiline = true;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "Mã số BHXH";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell4.Weight = 0.15733669792429517D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "Cấp bậc, chức vụ, chức danh nghề, nơi làm việc";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell9.Weight = 0.2584622799735713D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel10,
            this.xrLabel9,
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseBorders = false;
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell6.Weight = 0.96866076916009114D;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(378.8898F, 50F);
            this.xrLabel10.Multiline = true;
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(89.58331F, 50F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "Các khoản bổ sung";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(307.4474F, 50F);
            this.xrLabel9.Multiline = true;
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(71.44232F, 50F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "Phụ cấp lương";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(230.7967F, 50F);
            this.xrLabel8.Multiline = true;
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(76.65076F, 50F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "Thâm niên nghề (%)";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(155.2583F, 50F);
            this.xrLabel7.Multiline = true;
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(75.53833F, 50F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "Thâm niên VK (%)";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(79.59937F, 50F);
            this.xrLabel6.Multiline = true;
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(75.659F, 50F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "Chức vụ";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel5.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(79.59949F, 25F);
            this.xrLabel5.Multiline = true;
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(388.8737F, 25F);
            this.xrLabel5.StylePriority.UseBorders = false;
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Phụ cấp";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(6.99365E-06F, 25F);
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(79.59946F, 75F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "Hệ số/Mức lương";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(6.99365E-06F, 0F);
            this.xrLabel3.Multiline = true;
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(468.4731F, 25F);
            this.xrLabel3.StylePriority.UseBorders = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Tiền lương";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "Từ tháng, năm";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell3.Weight = 0.13570296303289087D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseBorders = false;
            this.xrTableCell16.StylePriority.UseTextAlignment = false;
            this.xrTableCell16.Text = "Đến tháng, năm";
            this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell16.Weight = 0.13570296578687233D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "Ghi chú";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell5.Weight = 0.28675262715184152D;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrLabel1,
            this.lblAddress,
            this.lblIdCompany,
            this.lblCompany,
            this.lblReportDate,
            this.xrl_TitleReport});
            this.ReportHeader.HeightF = 190.9167F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(798.9745F, 23F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(347.0258F, 46F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "(Ban hành kèm theo QĐ số 595/QĐ-BHXH\n ngày 14/04/2017 của BHXH Việt Nam)";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(798.9745F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(347.0258F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Mẫu D02-TS";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblAddress
            // 
            this.lblAddress.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblAddress.LocationFloat = new DevExpress.Utils.PointFloat(0F, 46F);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblAddress.SizeF = new System.Drawing.SizeF(798.9745F, 23F);
            this.lblAddress.StylePriority.UseFont = false;
            this.lblAddress.StylePriority.UseTextAlignment = false;
            this.lblAddress.Text = "Địa chỉ: {0}";
            this.lblAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblIdCompany
            // 
            this.lblIdCompany.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblIdCompany.LocationFloat = new DevExpress.Utils.PointFloat(0F, 23F);
            this.lblIdCompany.Name = "lblIdCompany";
            this.lblIdCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblIdCompany.SizeF = new System.Drawing.SizeF(798.9745F, 23F);
            this.lblIdCompany.StylePriority.UseFont = false;
            this.lblIdCompany.StylePriority.UseTextAlignment = false;
            this.lblIdCompany.Text = "Số định danh: TI16682";
            this.lblIdCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblCompany
            // 
            this.lblCompany.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCompany.SizeF = new System.Drawing.SizeF(798.9745F, 23F);
            this.lblCompany.StylePriority.UseFont = false;
            this.lblCompany.StylePriority.UseTextAlignment = false;
            this.lblCompany.Text = "Tên đơn vị: {0}";
            this.lblCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblReportDate
            // 
            this.lblReportDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.lblReportDate.LocationFloat = new DevExpress.Utils.PointFloat(0F, 157.9167F);
            this.lblReportDate.Name = "lblReportDate";
            this.lblReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblReportDate.SizeF = new System.Drawing.SizeF(1146F, 23F);
            this.lblReportDate.StylePriority.UseFont = false;
            this.lblReportDate.StylePriority.UseTextAlignment = false;
            this.lblReportDate.Text = "(Thời gian cập nhật {0}/{1}/{2})";
            this.lblReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrl_TitleReport
            // 
            this.xrl_TitleReport.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_TitleReport.LocationFloat = new DevExpress.Utils.PointFloat(0F, 134.9167F);
            this.xrl_TitleReport.Name = "xrl_TitleReport";
            this.xrl_TitleReport.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_TitleReport.SizeF = new System.Drawing.SizeF(1146F, 23F);
            this.xrl_TitleReport.StylePriority.UseFont = false;
            this.xrl_TitleReport.StylePriority.UseTextAlignment = false;
            this.xrl_TitleReport.Text = "DANH SÁCH LAO ĐỘNG THAM GIA BHXH, BHYT, BHTN, BHTNLĐ, BNN";
            this.xrl_TitleReport.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4,
            this.xrLabel15,
            this.lblFooterReportDate,
            this.xrLabel13,
            this.lblInsuranceNumber,
            this.lblInsuranceBook,
            this.lblCreator,
            this.lblDepartment,
            this.xrl_footer3,
            this.xrl_footer1});
            this.ReportFooter.HeightF = 292.6667F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrTable4
            // 
            this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrTable4.SizeF = new System.Drawing.SizeF(1146F, 25F);
            this.xrTable4.StylePriority.UseBorders = false;
            this.xrTable4.StylePriority.UseTextAlignment = false;
            this.xrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrCellTotalSalary,
            this.xrCellTotalPositionFactor,
            this.xrCellTotalSeniorityFactor,
            this.xrCellTotalOccupationFactor,
            this.xrCellTotalSalaryFactor,
            this.xrCellTotalAddition,
            this.xrTableCell19,
            this.xrTableCell20,
            this.xrTableCell21});
            this.xrTableRow4.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
            this.xrTableRow4.StylePriority.UseFont = false;
            this.xrTableRow4.StylePriority.UsePadding = false;
            this.xrTableRow4.StylePriority.UseTextAlignment = false;
            this.xrTableRow4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell7.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBorders = false;
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell7.Weight = 0.066407880703515554D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell8.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 3, 0, 100F);
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.StylePriority.UsePadding = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "Cộng tăng";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell8.Weight = 0.33979896811899235D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell10.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell10.StylePriority.UseBorders = false;
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.StylePriority.UsePadding = false;
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell10.Weight = 0.14968803693524957D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell11.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell11.StylePriority.UseBorders = false;
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.StylePriority.UsePadding = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell11.Weight = 0.24589787956895293D;
            // 
            // xrCellTotalSalary
            // 
            this.xrCellTotalSalary.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalSalary.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalSalary.Name = "xrCellTotalSalary";
            this.xrCellTotalSalary.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellTotalSalary.StylePriority.UseBorders = false;
            this.xrCellTotalSalary.StylePriority.UseFont = false;
            this.xrCellTotalSalary.StylePriority.UsePadding = false;
            this.xrCellTotalSalary.StylePriority.UseTextAlignment = false;
            this.xrCellTotalSalary.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCellTotalSalary.Weight = 0.1565863486977348D;
            // 
            // xrCellTotalPositionFactor
            // 
            this.xrCellTotalPositionFactor.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalPositionFactor.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalPositionFactor.Name = "xrCellTotalPositionFactor";
            this.xrCellTotalPositionFactor.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellTotalPositionFactor.StylePriority.UseBorders = false;
            this.xrCellTotalPositionFactor.StylePriority.UseFont = false;
            this.xrCellTotalPositionFactor.StylePriority.UsePadding = false;
            this.xrCellTotalPositionFactor.StylePriority.UseTextAlignment = false;
            this.xrCellTotalPositionFactor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCellTotalPositionFactor.Weight = 0.14883489501397695D;
            // 
            // xrCellTotalSeniorityFactor
            // 
            this.xrCellTotalSeniorityFactor.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalSeniorityFactor.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalSeniorityFactor.Name = "xrCellTotalSeniorityFactor";
            this.xrCellTotalSeniorityFactor.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellTotalSeniorityFactor.StylePriority.UseBorders = false;
            this.xrCellTotalSeniorityFactor.StylePriority.UseFont = false;
            this.xrCellTotalSeniorityFactor.StylePriority.UsePadding = false;
            this.xrCellTotalSeniorityFactor.StylePriority.UseTextAlignment = false;
            this.xrCellTotalSeniorityFactor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCellTotalSeniorityFactor.Weight = 0.14859743475443676D;
            // 
            // xrCellTotalOccupationFactor
            // 
            this.xrCellTotalOccupationFactor.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalOccupationFactor.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalOccupationFactor.Name = "xrCellTotalOccupationFactor";
            this.xrCellTotalOccupationFactor.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellTotalOccupationFactor.StylePriority.UseBorders = false;
            this.xrCellTotalOccupationFactor.StylePriority.UseFont = false;
            this.xrCellTotalOccupationFactor.StylePriority.UsePadding = false;
            this.xrCellTotalOccupationFactor.StylePriority.UseTextAlignment = false;
            this.xrCellTotalOccupationFactor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCellTotalOccupationFactor.Weight = 0.15078589887564686D;
            // 
            // xrCellTotalSalaryFactor
            // 
            this.xrCellTotalSalaryFactor.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalSalaryFactor.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalSalaryFactor.Name = "xrCellTotalSalaryFactor";
            this.xrCellTotalSalaryFactor.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellTotalSalaryFactor.StylePriority.UseBorders = false;
            this.xrCellTotalSalaryFactor.StylePriority.UseFont = false;
            this.xrCellTotalSalaryFactor.StylePriority.UsePadding = false;
            this.xrCellTotalSalaryFactor.StylePriority.UseTextAlignment = false;
            this.xrCellTotalSalaryFactor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCellTotalSalaryFactor.Weight = 0.14053972821149247D;
            // 
            // xrCellTotalAddition
            // 
            this.xrCellTotalAddition.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTotalAddition.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrCellTotalAddition.Name = "xrCellTotalAddition";
            this.xrCellTotalAddition.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellTotalAddition.StylePriority.UseBorders = false;
            this.xrCellTotalAddition.StylePriority.UseFont = false;
            this.xrCellTotalAddition.StylePriority.UsePadding = false;
            this.xrCellTotalAddition.StylePriority.UseTextAlignment = false;
            this.xrCellTotalAddition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCellTotalAddition.Weight = 0.17622656619916083D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell19.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell19.StylePriority.UseBorders = false;
            this.xrTableCell19.StylePriority.UseFont = false;
            this.xrTableCell19.StylePriority.UsePadding = false;
            this.xrTableCell19.StylePriority.UseTextAlignment = false;
            this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell19.Weight = 0.12910633576169389D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell20.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell20.StylePriority.UseBorders = false;
            this.xrTableCell20.StylePriority.UseFont = false;
            this.xrTableCell20.StylePriority.UsePadding = false;
            this.xrTableCell20.StylePriority.UseTextAlignment = false;
            this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell20.Weight = 0.12910633576169389D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell21.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell21.StylePriority.UseBorders = false;
            this.xrTableCell21.StylePriority.UseFont = false;
            this.xrTableCell21.StylePriority.UsePadding = false;
            this.xrTableCell21.StylePriority.UseTextAlignment = false;
            this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell21.Weight = 0.27281181795274045D;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Italic);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(576.0577F, 146.9583F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(569.9419F, 23F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "Ký, ghi rõ họ tên, đóng dấu";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblFooterReportDate
            // 
            this.lblFooterReportDate.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.lblFooterReportDate.LocationFloat = new DevExpress.Utils.PointFloat(576.0577F, 100.9583F);
            this.lblFooterReportDate.Name = "lblFooterReportDate";
            this.lblFooterReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblFooterReportDate.SizeF = new System.Drawing.SizeF(569.9421F, 23F);
            this.lblFooterReportDate.StylePriority.UseFont = false;
            this.lblFooterReportDate.StylePriority.UseTextAlignment = false;
            this.lblFooterReportDate.Text = "Ngày {0} tháng {1} năm {2}";
            this.lblFooterReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Italic);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 146.9583F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(576.0578F, 23F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "Ký, ghi rõ họ tên";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblInsuranceNumber
            // 
            this.lblInsuranceNumber.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblInsuranceNumber.LocationFloat = new DevExpress.Utils.PointFloat(33.75791F, 76.74999F);
            this.lblInsuranceNumber.Name = "lblInsuranceNumber";
            this.lblInsuranceNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblInsuranceNumber.SizeF = new System.Drawing.SizeF(542.2999F, 23F);
            this.lblInsuranceNumber.StylePriority.UseFont = false;
            this.lblInsuranceNumber.StylePriority.UseTextAlignment = false;
            this.lblInsuranceNumber.Text = "Tổng số thẻ BHYT đề nghị cấp: {0}";
            this.lblInsuranceNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblInsuranceBook
            // 
            this.lblInsuranceBook.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblInsuranceBook.LocationFloat = new DevExpress.Utils.PointFloat(33.75791F, 53.74997F);
            this.lblInsuranceBook.Name = "lblInsuranceBook";
            this.lblInsuranceBook.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblInsuranceBook.SizeF = new System.Drawing.SizeF(542.2999F, 23F);
            this.lblInsuranceBook.StylePriority.UseFont = false;
            this.lblInsuranceBook.StylePriority.UseTextAlignment = false;
            this.lblInsuranceBook.Text = "Tổng số Sổ BHXH đề nghị cấp: {0}";
            this.lblInsuranceBook.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblCreator
            // 
            this.lblCreator.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblCreator.LocationFloat = new DevExpress.Utils.PointFloat(0F, 259.6667F);
            this.lblCreator.Name = "lblCreator";
            this.lblCreator.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCreator.SizeF = new System.Drawing.SizeF(576.0578F, 23F);
            this.lblCreator.StylePriority.UseFont = false;
            this.lblCreator.StylePriority.UseTextAlignment = false;
            this.lblCreator.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblDepartment
            // 
            this.lblDepartment.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblDepartment.LocationFloat = new DevExpress.Utils.PointFloat(576.0576F, 259.6667F);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDepartment.SizeF = new System.Drawing.SizeF(569.9421F, 23F);
            this.lblDepartment.StylePriority.UseFont = false;
            this.lblDepartment.StylePriority.UseTextAlignment = false;
            this.lblDepartment.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrl_footer3
            // 
            this.xrl_footer3.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer3.LocationFloat = new DevExpress.Utils.PointFloat(576.0577F, 123.9583F);
            this.xrl_footer3.Multiline = true;
            this.xrl_footer3.Name = "xrl_footer3";
            this.xrl_footer3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer3.SizeF = new System.Drawing.SizeF(569.9422F, 23F);
            this.xrl_footer3.StylePriority.UseFont = false;
            this.xrl_footer3.StylePriority.UseTextAlignment = false;
            this.xrl_footer3.Text = "Đơn vị";
            this.xrl_footer3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer1
            // 
            this.xrl_footer1.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 123.9583F);
            this.xrl_footer1.Name = "xrl_footer1";
            this.xrl_footer1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer1.SizeF = new System.Drawing.SizeF(576.0578F, 23F);
            this.xrl_footer1.StylePriority.UseFont = false;
            this.xrl_footer1.StylePriority.UseTextAlignment = false;
            this.xrl_footer1.Text = "Người lập biểu";
            this.xrl_footer1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.GroupHeader1.HeightF = 25F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable3
            // 
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1146F, 25F);
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrt_GroupDepartment});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrt_GroupDepartment
            // 
            this.xrt_GroupDepartment.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_GroupDepartment.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrt_GroupDepartment.Name = "xrt_GroupDepartment";
            this.xrt_GroupDepartment.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 3, 3, 3, 100F);
            this.xrt_GroupDepartment.StylePriority.UseBorders = false;
            this.xrt_GroupDepartment.StylePriority.UseFont = false;
            this.xrt_GroupDepartment.StylePriority.UsePadding = false;
            this.xrt_GroupDepartment.StylePriority.UseTextAlignment = false;
            this.xrt_GroupDepartment.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_GroupDepartment.Weight = 2D;
            this.xrt_GroupDepartment.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Group_BeforePrint);
            // 
            // rp_BusinessIncreaseInsurance
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.ReportHeader,
            this.ReportFooter,
            this.GroupHeader1});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(11, 12, 46, 61);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }
    #endregion
}