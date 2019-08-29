using System;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Web.Core;
using Web.Core.Service.Catalog;
using Web.Core.Object.Report;
using Web.Core.Framework;
using Web.Core.Framework.Adapter;

namespace Web.Core.Framework.Report
{

    /// <summary>
    /// Summary description for rp_EmployeeHaveChildrenReceiveMidAutumnGift
    /// </summary>
    public class rp_EmployeeHaveChildrenReceiveMidAutumnGift : XtraReport
    {
        private DetailBand Detail;
        private TopMarginBand TopMargin;
        private BottomMarginBand BottomMargin;
        private ReportHeaderBand ReportHeader;
        private PageHeaderBand PageHeader;
        private FormattingRule formattingRule1;
        private ReportFooterBand ReportFooter;
        private XRLabel lblPhongHCNS;
        private XRLabel lblGiamDoc;
        private XRTable tblDetail;
        private XRTableRow xrDetailRow1;
        private XRTableCell xrTableCellFullName;
        private XRTableCell xrTableCellBirthDate;
        private XRTableCell xrTableCellDonViNoiDen;
        private XRLabel lblReportDate;
        private XRTable tblPageHeader;
        private XRTableRow xrTableRow11;
        private XRTableCell xrTableCell241;
        private XRTableCell xrTableCell242;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCellSoThuTu;
        private XRTableCell xrTableCellSex;
        private XRTableCell xrTableCell12;
        private XRTableCell xrTableCellTypeOfContract;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCellPosition;
        private XRTableCell xrTableCellNumberOfChildren;
        private XRTableCell xrTableCellGiftLevel;
        private XRTableCell xrTableCell22;
        private XRTableCell xrTableCell15;
        private XRTableCell xrTableCell13;
        private XRLabel xrTenCoQuanCapTren;
        private XRLabel xrLabel4;
        private XRLabel xrTenCoQuanDonVi;
        private XRLabel xrLabel6;
        private XRLabel xrLabel9;
        private GroupHeaderBand GroupHeader1;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrTableCell27;
        private XRTableCell xrTableCellGroupHead;
        private XRTableCell xrTableCellKyNhan;
        private XRTableCell xrTableCell10;
        private XRTableCell xrTableCellEmployeeCode;
        private XRTableCell xrTableCell11;
        private XRLabel xrLabel7;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
        private IContainer components = null;

        public rp_EmployeeHaveChildrenReceiveMidAutumnGift()
        {
            InitializeComponent();
        }
        int _stt = 0;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _stt++;
            xrTableCellSoThuTu.Text = _stt.ToString();

        }
        private void Group_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _stt = 0;
            xrTableCellSoThuTu.Text = _stt.ToString();

        }

        public void BindData(ReportFilter filter)
        {
            try
            {
                ReportController control = new ReportController();
                xrTenCoQuanDonVi.Text = control.GetCompanyName(filter.SessionDepartment);

                var location = new ReportController().GetCityName(filter.SessionDepartment);
                lblReportDate.Text = string.Format(lblReportDate.Text, location, DateTime.Now.Day,
                    DateTime.Now.Month, DateTime.Now.Year);

                // get organization
                var organization = cat_DepartmentServices.GetByDepartments(filter.SessionDepartment);
                if (organization != null)
                {
                    var age = 16;
                    var giftLevel = "300.000";
                    //select form db
                    var arrOrgCode = string.IsNullOrEmpty(filter.SelectedDepartment)
                          ? new string[] { }
                          : filter.SelectedDepartment.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < arrOrgCode.Length; i++)
                    {
                        arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
                    }

                    var table = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_ListEmployeeHaveChildrenReceiveMidAutumnFestivalGift(string.Join(",", arrOrgCode), age));


                    DataSource = table;
                    xrTableCellGiftLevel.Text = giftLevel;
                    //binding data
                    xrTableCellEmployeeCode.DataBindings.Add("Text", DataSource, "EmployeeCode");
                    xrTableCellFullName.DataBindings.Add("Text", DataSource, "FullName");
                    xrTableCellBirthDate.DataBindings.Add("Text", DataSource, "BirthDate", "{0:dd/MM/yyyy}");
                    xrTableCellSex.DataBindings.Add("Text", DataSource, "Sex");
                    xrTableCellPosition.DataBindings.Add("Text", DataSource, "Position");
                    xrTableCellTypeOfContract.DataBindings.Add("Text", DataSource, "TypeOfContract");
                    xrTableCellNumberOfChildren.DataBindings.Add("Text", DataSource, "NumberOfChildren");
                    GroupHeader1.GroupFields.AddRange(new GroupField[] {
                            new GroupField("DepartmentId", XRColumnSortOrder.Ascending)});
                    xrTableCellGroupHead.DataBindings.Add("Text", DataSource, "DepartmentName");
                }
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
            string resourceFileName = "rp_EmployeeHaveChildrenReceiveMidAutumnGift.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.tblDetail = new DevExpress.XtraReports.UI.XRTable();
            this.xrDetailRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellSoThuTu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellEmployeeCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellFullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellBirthDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellSex = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellPosition = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNumberOfChildren = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellGiftLevel = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTypeOfContract = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellKyNhan = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTenCoQuanCapTren = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTenCoQuanDonVi = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.tblPageHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell241 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell242 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblPhongHCNS = new DevExpress.XtraReports.UI.XRLabel();
            this.lblGiamDoc = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellGroupHead = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
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
            this.tblDetail.SizeF = new System.Drawing.SizeF(1012.917F, 25F);
            this.tblDetail.StylePriority.UseBorders = false;
            // 
            // xrDetailRow1
            // 
            this.xrDetailRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellSoThuTu,
            this.xrTableCellEmployeeCode,
            this.xrTableCellFullName,
            this.xrTableCellBirthDate,
            this.xrTableCellSex,
            this.xrTableCellPosition,
            this.xrTableCellNumberOfChildren,
            this.xrTableCellGiftLevel,
            this.xrTableCellTypeOfContract,
            this.xrTableCellKyNhan});
            this.xrDetailRow1.Name = "xrDetailRow1";
            this.xrDetailRow1.Weight = 1D;
            // 
            // xrTableCellSoThuTu
            // 
            this.xrTableCellSoThuTu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSoThuTu.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCellSoThuTu.Name = "xrTableCellSoThuTu";
            this.xrTableCellSoThuTu.StylePriority.UseBorders = false;
            this.xrTableCellSoThuTu.StylePriority.UseFont = false;
            this.xrTableCellSoThuTu.StylePriority.UseTextAlignment = false;
            this.xrTableCellSoThuTu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSoThuTu.Weight = 0.15313684800805708D;
            this.xrTableCellSoThuTu.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrTableCellEmployeeCode
            // 
            this.xrTableCellEmployeeCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellEmployeeCode.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCellEmployeeCode.Name = "xrTableCellEmployeeCode";
            this.xrTableCellEmployeeCode.StylePriority.UseBorders = false;
            this.xrTableCellEmployeeCode.StylePriority.UseFont = false;
            this.xrTableCellEmployeeCode.StylePriority.UseTextAlignment = false;
            this.xrTableCellEmployeeCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellEmployeeCode.Weight = 0.27830080192818524D;
            // 
            // xrTableCellFullName
            // 
            this.xrTableCellFullName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellFullName.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCellFullName.Name = "xrTableCellFullName";
            this.xrTableCellFullName.StylePriority.UseBorders = false;
            this.xrTableCellFullName.StylePriority.UseFont = false;
            this.xrTableCellFullName.StylePriority.UseTextAlignment = false;
            this.xrTableCellFullName.Text = " ";
            this.xrTableCellFullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellFullName.Weight = 0.50296508712017785D;
            // 
            // xrTableCellBirthDate
            // 
            this.xrTableCellBirthDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellBirthDate.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCellBirthDate.Name = "xrTableCellBirthDate";
            this.xrTableCellBirthDate.StylePriority.UseBorders = false;
            this.xrTableCellBirthDate.StylePriority.UseFont = false;
            this.xrTableCellBirthDate.StylePriority.UseTextAlignment = false;
            this.xrTableCellBirthDate.Text = " ";
            this.xrTableCellBirthDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellBirthDate.Weight = 0.31373958120095835D;
            // 
            // xrTableCellSex
            // 
            this.xrTableCellSex.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSex.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCellSex.Name = "xrTableCellSex";
            this.xrTableCellSex.StylePriority.UseBorders = false;
            this.xrTableCellSex.StylePriority.UseFont = false;
            this.xrTableCellSex.StylePriority.UseTextAlignment = false;
            this.xrTableCellSex.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSex.Weight = 0.21308193817340868D;
            // 
            // xrTableCellPosition
            // 
            this.xrTableCellPosition.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellPosition.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCellPosition.Name = "xrTableCellPosition";
            this.xrTableCellPosition.StylePriority.UseBorders = false;
            this.xrTableCellPosition.StylePriority.UseFont = false;
            this.xrTableCellPosition.StylePriority.UseTextAlignment = false;
            this.xrTableCellPosition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellPosition.Weight = 0.45995949239684558D;
            // 
            // xrTableCellNumberOfChildren
            // 
            this.xrTableCellNumberOfChildren.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNumberOfChildren.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCellNumberOfChildren.Name = "xrTableCellNumberOfChildren";
            this.xrTableCellNumberOfChildren.StylePriority.UseBorders = false;
            this.xrTableCellNumberOfChildren.StylePriority.UseFont = false;
            this.xrTableCellNumberOfChildren.StylePriority.UseTextAlignment = false;
            this.xrTableCellNumberOfChildren.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNumberOfChildren.Weight = 0.31567734930473917D;
            // 
            // xrTableCellGiftLevel
            // 
            this.xrTableCellGiftLevel.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellGiftLevel.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCellGiftLevel.Name = "xrTableCellGiftLevel";
            this.xrTableCellGiftLevel.StylePriority.UseBorders = false;
            this.xrTableCellGiftLevel.StylePriority.UseFont = false;
            this.xrTableCellGiftLevel.StylePriority.UseTextAlignment = false;
            this.xrTableCellGiftLevel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellGiftLevel.Weight = 0.34724469546519121D;
            // 
            // xrTableCellTypeOfContract
            // 
            this.xrTableCellTypeOfContract.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTypeOfContract.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCellTypeOfContract.Name = "xrTableCellTypeOfContract";
            this.xrTableCellTypeOfContract.StylePriority.UseBorders = false;
            this.xrTableCellTypeOfContract.StylePriority.UseFont = false;
            this.xrTableCellTypeOfContract.StylePriority.UseTextAlignment = false;
            this.xrTableCellTypeOfContract.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTypeOfContract.Weight = 0.834671806363016D;
            // 
            // xrTableCellKyNhan
            // 
            this.xrTableCellKyNhan.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellKyNhan.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCellKyNhan.Name = "xrTableCellKyNhan";
            this.xrTableCellKyNhan.StylePriority.UseBorders = false;
            this.xrTableCellKyNhan.StylePriority.UseFont = false;
            this.xrTableCellKyNhan.StylePriority.UseTextAlignment = false;
            this.xrTableCellKyNhan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellKyNhan.Weight = 0.41827184501877512D;
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
            this.xrTenCoQuanCapTren,
            this.xrLabel4,
            this.xrTenCoQuanDonVi,
            this.xrLabel6,
            this.xrLabel7,
            this.xrLabel9});
            this.ReportHeader.HeightF = 106F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrTenCoQuanCapTren
            // 
            this.xrTenCoQuanCapTren.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTenCoQuanCapTren.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTenCoQuanCapTren.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTenCoQuanCapTren.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTenCoQuanCapTren.Name = "xrTenCoQuanCapTren";
            this.xrTenCoQuanCapTren.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTenCoQuanCapTren.SizeF = new System.Drawing.SizeF(400.322F, 24F);
            this.xrTenCoQuanCapTren.StylePriority.UseBorderColor = false;
            this.xrTenCoQuanCapTren.StylePriority.UseBorders = false;
            this.xrTenCoQuanCapTren.StylePriority.UseFont = false;
            this.xrTenCoQuanCapTren.StylePriority.UseTextAlignment = false;
            this.xrTenCoQuanCapTren.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.BorderColor = System.Drawing.Color.DarkGray;
            this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(400.322F, 0F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(739.678F, 24F);
            this.xrLabel4.StylePriority.UseBorderColor = false;
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTenCoQuanDonVi
            // 
            this.xrTenCoQuanDonVi.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTenCoQuanDonVi.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTenCoQuanDonVi.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTenCoQuanDonVi.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTenCoQuanDonVi.Name = "xrTenCoQuanDonVi";
            this.xrTenCoQuanDonVi.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTenCoQuanDonVi.SizeF = new System.Drawing.SizeF(400.322F, 24F);
            this.xrTenCoQuanDonVi.StylePriority.UseBorderColor = false;
            this.xrTenCoQuanDonVi.StylePriority.UseBorders = false;
            this.xrTenCoQuanDonVi.StylePriority.UseFont = false;
            this.xrTenCoQuanDonVi.StylePriority.UseTextAlignment = false;
            this.xrTenCoQuanDonVi.Text = "TÊN CƠ QUAN ĐƠN VỊ";
            this.xrTenCoQuanDonVi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel6
            // 
            this.xrLabel6.BorderColor = System.Drawing.Color.DarkGray;
            this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel6.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(400.322F, 24.00001F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(739.678F, 24F);
            this.xrLabel6.StylePriority.UseBorderColor = false;
            this.xrLabel6.StylePriority.UseBorders = false;
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel7
            // 
            this.xrLabel7.BorderColor = System.Drawing.Color.DarkGray;
            this.xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel7.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 48F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(1012.917F, 24F);
            this.xrLabel7.StylePriority.UseBorderColor = false;
            this.xrLabel7.StylePriority.UseBorders = false;
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UsePadding = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "                   DANH SÁCH CÁN BỘ CÓ CON EM NHẬN QUÀ TRUNG THU";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel9
            // 
            this.xrLabel9.BorderColor = System.Drawing.Color.DarkGray;
            this.xrLabel9.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel9.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 96F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(1140F, 10F);
            this.xrLabel9.StylePriority.UseBorderColor = false;
            this.xrLabel9.StylePriority.UseBorders = false;
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblPageHeader});
            this.PageHeader.HeightF = 40.41314F;
            this.PageHeader.Name = "PageHeader";
            // 
            // tblPageHeader
            // 
            this.tblPageHeader.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tblPageHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tblPageHeader.Name = "tblPageHeader";
            this.tblPageHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
            this.tblPageHeader.SizeF = new System.Drawing.SizeF(1012.917F, 40F);
            this.tblPageHeader.StylePriority.UseBorders = false;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell241,
            this.xrTableCell11,
            this.xrTableCell242,
            this.xrTableCell1,
            this.xrTableCell12,
            this.xrTableCell22,
            this.xrTableCell15,
            this.xrTableCell13,
            this.xrTableCell2,
            this.xrTableCell10});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 9.2D;
            // 
            // xrTableCell241
            // 
            this.xrTableCell241.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell241.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell241.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCell241.Name = "xrTableCell241";
            this.xrTableCell241.StylePriority.UseBorderColor = false;
            this.xrTableCell241.StylePriority.UseBorders = false;
            this.xrTableCell241.StylePriority.UseFont = false;
            this.xrTableCell241.Text = "Stt";
            this.xrTableCell241.Weight = 0.22223739120910233D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell11.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBorderColor = false;
            this.xrTableCell11.StylePriority.UseBorders = false;
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.Text = "Mã nhân viên";
            this.xrTableCell11.Weight = 0.40387967827481608D;
            // 
            // xrTableCell242
            // 
            this.xrTableCell242.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell242.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell242.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCell242.Name = "xrTableCell242";
            this.xrTableCell242.StylePriority.UseBorderColor = false;
            this.xrTableCell242.StylePriority.UseBorders = false;
            this.xrTableCell242.StylePriority.UseFont = false;
            this.xrTableCell242.Text = "Họ và tên";
            this.xrTableCell242.Weight = 0.729920121528115D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorderColor = false;
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "Ngày sinh";
            this.xrTableCell1.Weight = 0.45530958613421579D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell12.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseBorderColor = false;
            this.xrTableCell12.StylePriority.UseBorders = false;
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.Text = "Giới tính";
            this.xrTableCell12.Weight = 0.30923179867784256D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell22.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell22.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseBorderColor = false;
            this.xrTableCell22.StylePriority.UseBorders = false;
            this.xrTableCell22.StylePriority.UseFont = false;
            this.xrTableCell22.Text = "Chức vụ";
            this.xrTableCell22.Weight = 0.66750877686143284D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell15.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCell15.Multiline = true;
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseBorderColor = false;
            this.xrTableCell15.StylePriority.UseBorders = false;
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.Text = "Số người con\r\n";
            this.xrTableCell15.Weight = 0.4581220176916454D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell13.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell13.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseBorderColor = false;
            this.xrTableCell13.StylePriority.UseBorders = false;
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.Text = "Mức quà";
            this.xrTableCell13.Weight = 0.5039332051797033D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorderColor = false;
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Text = "Loại HĐLĐ";
            this.xrTableCell2.Weight = 1.2113044861219648D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell10.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseBorderColor = false;
            this.xrTableCell10.StylePriority.UseBorders = false;
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.Text = "Ký nhận";
            this.xrTableCell10.Weight = 0.60700980264723015D;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblReportDate,
            this.lblPhongHCNS,
            this.lblGiamDoc});
            this.ReportFooter.HeightF = 252.1667F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // lblReportDate
            // 
            this.lblReportDate.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblReportDate.LocationFloat = new DevExpress.Utils.PointFloat(539.098F, 31.87501F);
            this.lblReportDate.Name = "lblReportDate";
            this.lblReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblReportDate.SizeF = new System.Drawing.SizeF(300F, 15F);
            this.lblReportDate.StylePriority.UseFont = false;
            this.lblReportDate.StylePriority.UseTextAlignment = false;
            this.lblReportDate.Text = "{0}, ngày {1} tháng {2} năm {3}";
            this.lblReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblPhongHCNS
            // 
            this.lblPhongHCNS.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblPhongHCNS.LocationFloat = new DevExpress.Utils.PointFloat(66.66665F, 46.875F);
            this.lblPhongHCNS.Name = "lblPhongHCNS";
            this.lblPhongHCNS.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblPhongHCNS.SizeF = new System.Drawing.SizeF(180F, 25F);
            this.lblPhongHCNS.StylePriority.UseFont = false;
            this.lblPhongHCNS.StylePriority.UseTextAlignment = false;
            this.lblPhongHCNS.Text = "PHÒNG HCNS";
            this.lblPhongHCNS.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblGiamDoc
            // 
            this.lblGiamDoc.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblGiamDoc.LocationFloat = new DevExpress.Utils.PointFloat(539.098F, 46.875F);
            this.lblGiamDoc.Name = "lblGiamDoc";
            this.lblGiamDoc.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblGiamDoc.SizeF = new System.Drawing.SizeF(300F, 25F);
            this.lblGiamDoc.StylePriority.UseFont = false;
            this.lblGiamDoc.StylePriority.UseTextAlignment = false;
            this.lblGiamDoc.Text = "GIÁM ĐỐC";
            this.lblGiamDoc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.GroupHeader1.HeightF = 28.95831F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1012.917F, 28.95831F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UsePadding = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell27,
            this.xrTableCellGroupHead});
            this.xrTableRow2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
            this.xrTableRow2.StylePriority.UseFont = false;
            this.xrTableRow2.StylePriority.UsePadding = false;
            this.xrTableRow2.StylePriority.UseTextAlignment = false;
            this.xrTableRow2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell27.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.StylePriority.UseBorders = false;
            this.xrTableCell27.StylePriority.UseFont = false;
            this.xrTableCell27.StylePriority.UseTextAlignment = false;
            this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell27.Weight = 0.40851067708270339D;
            // 
            // xrTableCellGroupHead
            // 
            this.xrTableCellGroupHead.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xrTableCellGroupHead.Name = "xrTableCellGroupHead";
            this.xrTableCellGroupHead.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCellGroupHead.StylePriority.UseFont = false;
            this.xrTableCellGroupHead.StylePriority.UsePadding = false;
            this.xrTableCellGroupHead.StylePriority.UseTextAlignment = false;
            this.xrTableCellGroupHead.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellGroupHead.Weight = 9.827273887492515D;
            this.xrTableCellGroupHead.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Group_BeforePrint);
            // 
            // rp_EmployeeHaveChildrenReceiveMidAutumnGift
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter,
            this.GroupHeader1});
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
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}