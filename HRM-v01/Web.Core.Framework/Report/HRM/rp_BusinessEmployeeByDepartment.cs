using System;
using DevExpress.XtraReports.UI;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Object.Report;
using Web.Core.Service.Catalog;
using System.Globalization;
/// <summary>
/// Summary description for rp_BankAccount
/// </summary>
public class rp_BusinessEmployeeByDepartment : XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private PageHeaderBand PageHeader;
    private ReportHeaderBand ReportHeader;
    private ReportFooterBand ReportFooter;
    private XRLabel lblTitle;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell4;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrCellIndex;
    private XRTableCell xrCellEmployeeCode;
    private XRTableCell xrCellFullName;
    private XRTableCell xrCellPosition;
    private XRLabel lblHRDepartment;
    private XRLabel lblCreator;
    private XRLabel xrl_footer3;
    private XRLabel xrl_footer1;
    private XRTableCell xrTableCell9;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrt_GroupDepartment;
    private XRLabel lblReportDate;
    private XRTableCell xrCellPhoneNumber;
    private XRTableCell xrCellDepartment;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell11;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rp_BusinessEmployeeByDepartment()
    {
        InitializeComponent();

    }

    int index = 1;
    private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrCellIndex.Text = index.ToString();
        index++;
    }
    private void Group_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        index = 1;
        xrCellIndex.Text = index.ToString();
    }

    public void BindData(ReportFilter filter)
    {
        try
        {
            
            var reportDate = filter.ReportedDate;
            lblReportDate.Text = string.Format(lblReportDate.Text, reportDate.Day, reportDate.Month, reportDate.Year);

            var organization = cat_DepartmentServices.GetByDepartments(filter.SessionDepartment);

            if (organization == null) return;
            var departments = filter.SelectedDepartment;
            var arrDepartment = departments.Split(new[] { ',' }, StringSplitOptions.None);
            for (var i = 0; i < arrDepartment.Length; i++)
            {
                arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
            }

            if (arrDepartment.Length > 2)
            {
                lblTitle.Text = @"BÁO CÁO CBCNV THEO PHÒNG BAN";
            }
            else
            {
                var orgSelected = cat_DepartmentServices.GetById(Convert.ToInt32(departments.TrimEnd(',')));
                lblTitle.Text = string.Format(lblTitle.Text, orgSelected.Name);
            }

            // get data from date
            var fromDate = filter.StartDate != null
                ? filter.StartDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                : string.Empty;
            // get data to date
            var toDate = filter.EndDate != null
                ? filter.EndDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                : string.Empty;

            var table = SQLHelper.ExecuteTable(
                SQLBusinessAdapter.GetStore_EmployeeByDepartment(string.Join(",", arrDepartment),
                    filter.WhereClause,fromDate,toDate));
            DataSource = table;

            xrCellEmployeeCode.DataBindings.Add("Text", DataSource, "EmployeeCode");
            xrCellFullName.DataBindings.Add("Text", DataSource, "FullName");
            xrCellPosition.DataBindings.Add("Text", DataSource, "PositionName");
            xrCellPhoneNumber.DataBindings.Add("Text", DataSource, "CellPhoneNumber");
            xrCellDepartment.DataBindings.Add("Text", DataSource, "DepartmentName");
            GroupHeader1.GroupFields.AddRange(new[] { new GroupField("DepartmentId", XRColumnSortOrder.Ascending) });
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
        string resourceFileName = "rp_BusinessEmployeeByDepartment.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrCellIndex = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrCellEmployeeCode = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrCellFullName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrCellPosition = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrCellPhoneNumber = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrCellDepartment = new DevExpress.XtraReports.UI.XRTableCell();
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
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.lblReportDate = new DevExpress.XtraReports.UI.XRLabel();
        this.lblTitle = new DevExpress.XtraReports.UI.XRLabel();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.lblHRDepartment = new DevExpress.XtraReports.UI.XRLabel();
        this.lblCreator = new DevExpress.XtraReports.UI.XRLabel();
        this.xrl_footer3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrl_footer1 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrt_GroupDepartment = new DevExpress.XtraReports.UI.XRTableCell();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
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
            this.xrCellEmployeeCode,
            this.xrCellFullName,
            this.xrCellPosition,
            this.xrCellPhoneNumber,
            this.xrCellDepartment});
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
        this.xrCellIndex.Weight = 0.082409108611371046D;
        this.xrCellIndex.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
        // 
        // xrCellEmployeeCode
        // 
        this.xrCellEmployeeCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrCellEmployeeCode.Font = new System.Drawing.Font("Times New Roman", 10F);
        this.xrCellEmployeeCode.Name = "xrCellEmployeeCode";
        this.xrCellEmployeeCode.StylePriority.UseBorders = false;
        this.xrCellEmployeeCode.StylePriority.UseFont = false;
        this.xrCellEmployeeCode.StylePriority.UseTextAlignment = false;
        this.xrCellEmployeeCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrCellEmployeeCode.Weight = 0.29641468058021292D;
        // 
        // xrCellFullName
        // 
        this.xrCellFullName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrCellFullName.Font = new System.Drawing.Font("Times New Roman", 10F);
        this.xrCellFullName.Name = "xrCellFullName";
        this.xrCellFullName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
        this.xrCellFullName.StylePriority.UseBorders = false;
        this.xrCellFullName.StylePriority.UseFont = false;
        this.xrCellFullName.StylePriority.UsePadding = false;
        this.xrCellFullName.StylePriority.UseTextAlignment = false;
        this.xrCellFullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrCellFullName.Weight = 0.68469855561891979D;
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
        this.xrCellPosition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrCellPosition.Weight = 0.418307674109494D;
        // 
        // xrCellPhoneNumber
        // 
        this.xrCellPhoneNumber.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrCellPhoneNumber.Font = new System.Drawing.Font("Times New Roman", 10F);
        this.xrCellPhoneNumber.Name = "xrCellPhoneNumber";
        this.xrCellPhoneNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
        this.xrCellPhoneNumber.StylePriority.UseBorders = false;
        this.xrCellPhoneNumber.StylePriority.UseFont = false;
        this.xrCellPhoneNumber.StylePriority.UsePadding = false;
        this.xrCellPhoneNumber.StylePriority.UseTextAlignment = false;
        this.xrCellPhoneNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrCellPhoneNumber.Weight = 0.45900474482241216D;
        // 
        // xrCellDepartment
        // 
        this.xrCellDepartment.Font = new System.Drawing.Font("Times New Roman", 10F);
        this.xrCellDepartment.Name = "xrCellDepartment";
        this.xrCellDepartment.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
        this.xrCellDepartment.StylePriority.UseFont = false;
        this.xrCellDepartment.StylePriority.UsePadding = false;
        this.xrCellDepartment.StylePriority.UseTextAlignment = false;
        this.xrCellDepartment.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrCellDepartment.Weight = 0.57673705041483159D;
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
        this.PageHeader.HeightF = 25F;
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
        this.xrTable1.SizeF = new System.Drawing.SizeF(1146F, 25F);
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
            this.xrTableCell11});
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
        this.xrTableCell1.Weight = 0.089945097705042457D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = "Mã NV";
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell2.Weight = 0.32352227843654346D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell4.Multiline = true;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.Text = "HỌ VÀ TÊN\r\n";
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell4.Weight = 0.74731563859970629D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseBorders = false;
        this.xrTableCell9.StylePriority.UseTextAlignment = false;
        this.xrTableCell9.Text = "CHỨC VỤ";
        this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell9.Weight = 0.4565627887064248D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseBorders = false;
        this.xrTableCell6.StylePriority.UseTextAlignment = false;
        this.xrTableCell6.Text = "SỐ ĐIỆN THOẠI";
        this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell6.Weight = 0.5009818687034302D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseBorders = false;
        this.xrTableCell11.StylePriority.UseTextAlignment = false;
        this.xrTableCell11.Text = "PHÒNG - BAN";
        this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell11.Weight = 0.62948112123445044D;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblReportDate,
            this.lblTitle});
        this.ReportHeader.HeightF = 95.08333F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // lblReportDate
        // 
        this.lblReportDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
        this.lblReportDate.LocationFloat = new DevExpress.Utils.PointFloat(0F, 51.12502F);
        this.lblReportDate.Name = "lblReportDate";
        this.lblReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblReportDate.SizeF = new System.Drawing.SizeF(1146F, 23F);
        this.lblReportDate.StylePriority.UseFont = false;
        this.lblReportDate.StylePriority.UseTextAlignment = false;
        this.lblReportDate.Text = "(Thời gian cập nhật {0}/{1}/{2})";
        this.lblReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // lblTitle
        // 
        this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
        this.lblTitle.LocationFloat = new DevExpress.Utils.PointFloat(0F, 28.12503F);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblTitle.SizeF = new System.Drawing.SizeF(1146F, 23F);
        this.lblTitle.StylePriority.UseFont = false;
        this.lblTitle.StylePriority.UseTextAlignment = false;
        this.lblTitle.Text = "BÁO CÁO CBCNV {0}";
        this.lblTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblHRDepartment,
            this.lblCreator,
            this.xrl_footer3,
            this.xrl_footer1});
        this.ReportFooter.HeightF = 226F;
        this.ReportFooter.Name = "ReportFooter";
        // 
        // lblHRDepartment
        // 
        this.lblHRDepartment.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
        this.lblHRDepartment.LocationFloat = new DevExpress.Utils.PointFloat(0F, 176.0417F);
        this.lblHRDepartment.Name = "lblHRDepartment";
        this.lblHRDepartment.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblHRDepartment.SizeF = new System.Drawing.SizeF(576.0578F, 23F);
        this.lblHRDepartment.StylePriority.UseFont = false;
        this.lblHRDepartment.StylePriority.UseTextAlignment = false;
        this.lblHRDepartment.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // lblCreator
        // 
        this.lblCreator.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
        this.lblCreator.LocationFloat = new DevExpress.Utils.PointFloat(576.0578F, 176.0417F);
        this.lblCreator.Name = "lblCreator";
        this.lblCreator.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblCreator.SizeF = new System.Drawing.SizeF(569.9421F, 23F);
        this.lblCreator.StylePriority.UseFont = false;
        this.lblCreator.StylePriority.UseTextAlignment = false;
        this.lblCreator.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrl_footer3
        // 
        this.xrl_footer3.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
        this.xrl_footer3.LocationFloat = new DevExpress.Utils.PointFloat(576.0577F, 63.54167F);
        this.xrl_footer3.Multiline = true;
        this.xrl_footer3.Name = "xrl_footer3";
        this.xrl_footer3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrl_footer3.SizeF = new System.Drawing.SizeF(569.9422F, 23F);
        this.xrl_footer3.StylePriority.UseFont = false;
        this.xrl_footer3.StylePriority.UseTextAlignment = false;
        this.xrl_footer3.Text = "NGƯỜI LẬP\r\n";
        this.xrl_footer3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrl_footer1
        // 
        this.xrl_footer1.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
        this.xrl_footer1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 63.54167F);
        this.xrl_footer1.Name = "xrl_footer1";
        this.xrl_footer1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrl_footer1.SizeF = new System.Drawing.SizeF(576.0578F, 23F);
        this.xrl_footer1.StylePriority.UseFont = false;
        this.xrl_footer1.StylePriority.UseTextAlignment = false;
        this.xrl_footer1.Text = "PHÒNG NHÂN SỰ";
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
        // rp_BusinessEmployeeByDepartment
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
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }
    #endregion
}