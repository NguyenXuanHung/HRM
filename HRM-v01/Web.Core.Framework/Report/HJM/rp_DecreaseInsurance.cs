using System;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Adapter;
using Web.Core.Service.Catalog;
using Web.Core.Object.Report;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Summary description for rp_DecreaseInsurance
    /// </summary>
    public class rp_DecreaseInsurance : XtraReport
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
        private XRTable tblDetail;
        private XRTableRow xrDetailRow1;
        private XRTableCell xrTableCellHoVaTen;
        private XRTableCell xrTableCellNgaySinh;
        private XRTableCell xrTableCellChucVu;
        private XRTable tblPageHeader;
        private XRTableRow xrTableRow11;
        private XRTableCell xrTableCell241;
        private XRTableCell xrTableCell242;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell8;
        private XRTableCell xrTableCell5;
        private XRTableCell xrTableCellSoThuTu;
        private XRTableCell xrTableCellMucDongBH;
        private XRTableCell xrTableCellLyDoTang;
        private XRTableCell xrTableCell3;
        private XRTableCell xrTableCellMaNV;
        private XRTableCell xrTableCell11;
        private XRTableCell xrTableCellSoHDLD;
        private XRTableCell xrTableCell12;
        private XRTableCell xrTableCellNgayThangHDLD;
        private XRTableCell xrTableCell15;
        private XRTableCell xrTableCellTuNamThang;
        private XRTableCell xrTableCell22;
        private XRTableCell xrTableCellBHPhaiNop;
        private XRTableCell xrTableCell2;
        private XRLabel xrTenCoQuanCapTren;
        private XRLabel xrLabel4;
        private XRLabel xrTableCell;
        private XRLabel xrLabel6;
        private XRLabel xrTitle;
        private XRLabel xrDate;
        private XRLabel xrLabel9;
        private GroupHeaderBand GroupHeader1;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrTableCell27;
        private XRTableCell xrGroupDepartment;
        private XRLabel xrLabel5;
        private XRLabel xrLabel3;
        private XRLabel xrLabel1;
        private XRTable xrTable3;
        private XRTableRow xrTableRow3;
        private XRTableCell xrTableCell7;
        private XRTableCell xrTableCell9;
        private XRTableCell xrTableCell10;
        private XRTableCell xrTableCellTotalNgaySinh;
        private XRTableCell xrTableCellTotalMucDongBH;
        private XRTableCell xrTableCellTotalBHPhaiNop;
        private XRTableCell xrTableCellTotalSoHDLD;
        private XRTableCell xrTableCellTotalNgayThangHDLD;
        private XRTableCell xrTableCellTotalTuNamThang;
        private XRTableCell xrTableCellTotalChucVu;
        private XRTableCell xrTableCellTotalLyDoTang;
        private GroupHeaderBand GroupHeader2;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell4;
        private XRTableCell xrGroupConstruction;
        private GroupHeaderBand GroupHeader3;
        private XRTable xrTable4;
        private XRTableRow xrTableRow4;
        private XRTableCell xrTableCell13;
        private XRTableCell xrGroupTeam;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
        private IContainer components = null;

        public rp_DecreaseInsurance()
        {
            InitializeComponent();
        }
        int _stt;
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
                var toDate = filter.ReportedDate;
                xrDate.Text = string.Format(xrDate.Text, toDate.Month, toDate.Year);

                var organization = cat_DepartmentServices.GetByDepartments(filter.SessionDepartment);
                if(organization==null) return;
                var arrDepartment = string.IsNullOrEmpty(filter.SelectedDepartment)
                    ? new string[] { }
                    : filter.SelectedDepartment.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (var i = 0; i < arrDepartment.Length; i++)
                {
                    arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
                }
                var table = SQLHelper.ExecuteTable(
                    SQLManagementAdapter.GetStore_BaoCaoTangGiamBh(
                        string.Join(",", arrDepartment), filter.WhereClause, null, true, null, null, null));
                DataSource = table;

                xrTableCellMaNV.DataBindings.Add("Text", DataSource, "EmployeeCode");
                xrTableCellHoVaTen.DataBindings.Add("Text", DataSource, "FullName");
                xrTableCellNgaySinh.DataBindings.Add("Text", DataSource, "BirthDate", "{0:dd/MM/yyyy}");
                xrTableCellMucDongBH.DataBindings.Add("Text", DataSource, "SalaryInsurance");
                xrTableCellBHPhaiNop.DataBindings.Add("Text", DataSource, "InsuranceSubmit");
                xrTableCellSoHDLD.DataBindings.Add("Text", DataSource, "ContractNumber");
                xrTableCellNgayThangHDLD.DataBindings.Add("Text", DataSource, "ContractDate", "{0:dd/MM/yyyy}");
                xrTableCellTuNamThang.DataBindings.Add("Text", DataSource, "EffectiveDate", "{0:dd/MM/yyyy}");
                xrTableCellChucVu.DataBindings.Add("Text", DataSource, "PositionName");
                xrTableCellLyDoTang.DataBindings.Add("Text", DataSource, "ReasonDecrease");

                GroupHeader1.GroupFields.AddRange(new[] {
                    new GroupField("DepartmentId", XRColumnSortOrder.Ascending)});
                xrGroupDepartment.DataBindings.Add("Text", DataSource, "DepartmentName");
                GroupHeader2.GroupFields.AddRange(new[] {
                    new GroupField("ConstructionId", XRColumnSortOrder.Ascending)});
                xrGroupConstruction.DataBindings.Add("Text", DataSource, "ConstructionName");
                GroupHeader3.GroupFields.AddRange(new[] {
                    new GroupField("TeamId", XRColumnSortOrder.Ascending)});
                xrGroupTeam.DataBindings.Add("Text", DataSource, "TeamName");
                xrTableCellTotalMucDongBH.DataBindings.Add("Text", DataSource, "xSalaryInsuranceDe");
                xrTableCellTotalBHPhaiNop.DataBindings.Add("Text", DataSource, "xInsuranceSubmitDe");
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
            string resourceFileName = "rp_DecreaseInsurance.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.tblDetail = new DevExpress.XtraReports.UI.XRTable();
            this.xrDetailRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellSoThuTu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellMaNV = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHoVaTen = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNgaySinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellMucDongBH = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellBHPhaiNop = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellSoHDLD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNgayThangHDLD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTuNamThang = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChucVu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellLyDoTang = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTenCoQuanCapTren = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.tblPageHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell241 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell242 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalNgaySinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalMucDongBH = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalBHPhaiNop = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalSoHDLD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalNgayThangHDLD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalTuNamThang = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalChucVu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalLyDoTang = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblLapBang = new DevExpress.XtraReports.UI.XRLabel();
            this.lblThuTruong = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGroupDepartment = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGroupConstruction = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGroupTeam = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
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
            this.tblDetail.SizeF = new System.Drawing.SizeF(1140F, 25F);
            this.tblDetail.StylePriority.UseBorders = false;
            // 
            // xrDetailRow1
            // 
            this.xrDetailRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellSoThuTu,
            this.xrTableCellMaNV,
            this.xrTableCellHoVaTen,
            this.xrTableCellNgaySinh,
            this.xrTableCellMucDongBH,
            this.xrTableCellBHPhaiNop,
            this.xrTableCellSoHDLD,
            this.xrTableCellNgayThangHDLD,
            this.xrTableCellTuNamThang,
            this.xrTableCellChucVu,
            this.xrTableCellLyDoTang});
            this.xrDetailRow1.Name = "xrDetailRow1";
            this.xrDetailRow1.Weight = 1D;
            // 
            // xrTableCellSoThuTu
            // 
            this.xrTableCellSoThuTu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSoThuTu.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellSoThuTu.Name = "xrTableCellSoThuTu";
            this.xrTableCellSoThuTu.StylePriority.UseBorders = false;
            this.xrTableCellSoThuTu.StylePriority.UseFont = false;
            this.xrTableCellSoThuTu.StylePriority.UseTextAlignment = false;
            this.xrTableCellSoThuTu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSoThuTu.Weight = 0.077511557006487111D;
            this.xrTableCellSoThuTu.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrTableCellMaNV
            // 
            this.xrTableCellMaNV.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellMaNV.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellMaNV.Name = "xrTableCellMaNV";
            this.xrTableCellMaNV.StylePriority.UseBorders = false;
            this.xrTableCellMaNV.StylePriority.UseFont = false;
            this.xrTableCellMaNV.StylePriority.UseTextAlignment = false;
            this.xrTableCellMaNV.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellMaNV.Weight = 0.19874094116912908D;
            // 
            // xrTableCellHoVaTen
            // 
            this.xrTableCellHoVaTen.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHoVaTen.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellHoVaTen.Name = "xrTableCellHoVaTen";
            this.xrTableCellHoVaTen.StylePriority.UseBorders = false;
            this.xrTableCellHoVaTen.StylePriority.UseFont = false;
            this.xrTableCellHoVaTen.StylePriority.UseTextAlignment = false;
            this.xrTableCellHoVaTen.Text = " ";
            this.xrTableCellHoVaTen.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellHoVaTen.Weight = 0.44377636195756925D;
            // 
            // xrTableCellNgaySinh
            // 
            this.xrTableCellNgaySinh.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNgaySinh.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellNgaySinh.Name = "xrTableCellNgaySinh";
            this.xrTableCellNgaySinh.StylePriority.UseBorders = false;
            this.xrTableCellNgaySinh.StylePriority.UseFont = false;
            this.xrTableCellNgaySinh.StylePriority.UseTextAlignment = false;
            this.xrTableCellNgaySinh.Text = " ";
            this.xrTableCellNgaySinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNgaySinh.Weight = 0.26519686884895532D;
            // 
            // xrTableCellMucDongBH
            // 
            this.xrTableCellMucDongBH.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellMucDongBH.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellMucDongBH.Name = "xrTableCellMucDongBH";
            this.xrTableCellMucDongBH.StylePriority.UseBorders = false;
            this.xrTableCellMucDongBH.StylePriority.UseFont = false;
            this.xrTableCellMucDongBH.StylePriority.UseTextAlignment = false;
            this.xrTableCellMucDongBH.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellMucDongBH.Weight = 0.28086080642455996D;
            // 
            // xrTableCellBHPhaiNop
            // 
            this.xrTableCellBHPhaiNop.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellBHPhaiNop.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellBHPhaiNop.Name = "xrTableCellBHPhaiNop";
            this.xrTableCellBHPhaiNop.StylePriority.UseBorders = false;
            this.xrTableCellBHPhaiNop.StylePriority.UseFont = false;
            this.xrTableCellBHPhaiNop.StylePriority.UseTextAlignment = false;
            this.xrTableCellBHPhaiNop.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellBHPhaiNop.Weight = 0.29092508588082566D;
            // 
            // xrTableCellSoHDLD
            // 
            this.xrTableCellSoHDLD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSoHDLD.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellSoHDLD.Name = "xrTableCellSoHDLD";
            this.xrTableCellSoHDLD.StylePriority.UseBorders = false;
            this.xrTableCellSoHDLD.StylePriority.UseFont = false;
            this.xrTableCellSoHDLD.StylePriority.UseTextAlignment = false;
            this.xrTableCellSoHDLD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSoHDLD.Weight = 0.25100490981562262D;
            // 
            // xrTableCellNgayThangHDLD
            // 
            this.xrTableCellNgayThangHDLD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNgayThangHDLD.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellNgayThangHDLD.Name = "xrTableCellNgayThangHDLD";
            this.xrTableCellNgayThangHDLD.StylePriority.UseBorders = false;
            this.xrTableCellNgayThangHDLD.StylePriority.UseFont = false;
            this.xrTableCellNgayThangHDLD.StylePriority.UseTextAlignment = false;
            this.xrTableCellNgayThangHDLD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNgayThangHDLD.Weight = 0.27155765394082548D;
            // 
            // xrTableCellTuNamThang
            // 
            this.xrTableCellTuNamThang.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTuNamThang.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTuNamThang.Name = "xrTableCellTuNamThang";
            this.xrTableCellTuNamThang.StylePriority.UseBorders = false;
            this.xrTableCellTuNamThang.StylePriority.UseFont = false;
            this.xrTableCellTuNamThang.StylePriority.UseTextAlignment = false;
            this.xrTableCellTuNamThang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTuNamThang.Weight = 0.2336409286763062D;
            // 
            // xrTableCellChucVu
            // 
            this.xrTableCellChucVu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellChucVu.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellChucVu.Name = "xrTableCellChucVu";
            this.xrTableCellChucVu.StylePriority.UseBorders = false;
            this.xrTableCellChucVu.StylePriority.UseFont = false;
            this.xrTableCellChucVu.StylePriority.UseTextAlignment = false;
            this.xrTableCellChucVu.Text = " ";
            this.xrTableCellChucVu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellChucVu.Weight = 0.31479220995272406D;
            // 
            // xrTableCellLyDoTang
            // 
            this.xrTableCellLyDoTang.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellLyDoTang.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellLyDoTang.Name = "xrTableCellLyDoTang";
            this.xrTableCellLyDoTang.StylePriority.UseBorders = false;
            this.xrTableCellLyDoTang.StylePriority.UseFont = false;
            this.xrTableCellLyDoTang.StylePriority.UseTextAlignment = false;
            this.xrTableCellLyDoTang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellLyDoTang.Weight = 0.31479223787492749D;
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
            this.xrTableCell,
            this.xrLabel6,
            this.xrTitle,
            this.xrDate,
            this.xrLabel9});
            this.ReportHeader.HeightF = 106F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrTenCoQuanCapTren
            // 
            this.xrTenCoQuanCapTren.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTenCoQuanCapTren.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTenCoQuanCapTren.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Bold);
            this.xrTenCoQuanCapTren.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTenCoQuanCapTren.Name = "xrTenCoQuanCapTren";
            this.xrTenCoQuanCapTren.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTenCoQuanCapTren.SizeF = new System.Drawing.SizeF(529.3843F, 24F);
            this.xrTenCoQuanCapTren.StylePriority.UseBorderColor = false;
            this.xrTenCoQuanCapTren.StylePriority.UseBorders = false;
            this.xrTenCoQuanCapTren.StylePriority.UseFont = false;
            this.xrTenCoQuanCapTren.StylePriority.UseTextAlignment = false;
            this.xrTenCoQuanCapTren.Text = "CÔNG TY TNHH THƯƠNG MẠI & XÂY DỰNG TRUNG CHÍNH";
            this.xrTenCoQuanCapTren.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel4
            // 
            this.xrLabel4.BorderColor = System.Drawing.Color.DarkGray;
            this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(529.3843F, 0F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(610.6157F, 24F);
            this.xrLabel4.StylePriority.UseBorderColor = false;
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "Mẫu :  02 NS - HC";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrTableCell
            // 
            this.xrTableCell.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCell.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24F);
            this.xrTableCell.Name = "xrTableCell";
            this.xrTableCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTableCell.SizeF = new System.Drawing.SizeF(204.3843F, 24F);
            this.xrTableCell.StylePriority.UseBorderColor = false;
            this.xrTableCell.StylePriority.UseBorders = false;
            this.xrTableCell.StylePriority.UseFont = false;
            this.xrTableCell.StylePriority.UseTextAlignment = false;
            this.xrTableCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel6
            // 
            this.xrLabel6.BorderColor = System.Drawing.Color.DarkGray;
            this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel6.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(204.3843F, 24F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(935.6157F, 24F);
            this.xrLabel6.StylePriority.UseBorderColor = false;
            this.xrLabel6.StylePriority.UseBorders = false;
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTitle
            // 
            this.xrTitle.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTitle.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTitle.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTitle.LocationFloat = new DevExpress.Utils.PointFloat(0F, 48F);
            this.xrTitle.Name = "xrTitle";
            this.xrTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTitle.SizeF = new System.Drawing.SizeF(1140F, 24F);
            this.xrTitle.StylePriority.UseBorderColor = false;
            this.xrTitle.StylePriority.UseBorders = false;
            this.xrTitle.StylePriority.UseFont = false;
            this.xrTitle.StylePriority.UseTextAlignment = false;
            this.xrTitle.Text = "BÁO CÁO GIẢM THAM GIA BHXH, BHYT, BHTN";
            this.xrTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrDate
            // 
            this.xrDate.BorderColor = System.Drawing.Color.DarkGray;
            this.xrDate.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrDate.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrDate.LocationFloat = new DevExpress.Utils.PointFloat(0F, 72F);
            this.xrDate.Name = "xrDate";
            this.xrDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrDate.SizeF = new System.Drawing.SizeF(1140F, 24F);
            this.xrDate.StylePriority.UseBorderColor = false;
            this.xrDate.StylePriority.UseBorders = false;
            this.xrDate.StylePriority.UseFont = false;
            this.xrDate.StylePriority.UseTextAlignment = false;
            this.xrDate.Text = "Tháng {0} năm {1}";
            this.xrDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            this.xrLabel5,
            this.xrLabel3,
            this.tblPageHeader});
            this.PageHeader.HeightF = 75.41315F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Italic);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(30.02692F, 25F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(180F, 10F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(30.02691F, 0F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(180F, 25F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "B. Biến động giảm";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // tblPageHeader
            // 
            this.tblPageHeader.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tblPageHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 35.00001F);
            this.tblPageHeader.Name = "tblPageHeader";
            this.tblPageHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
            this.tblPageHeader.SizeF = new System.Drawing.SizeF(1140F, 40.41314F);
            this.tblPageHeader.StylePriority.UseBorders = false;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell241,
            this.xrTableCell11,
            this.xrTableCell242,
            this.xrTableCell1,
            this.xrTableCell8,
            this.xrTableCell3,
            this.xrTableCell5,
            this.xrTableCell2,
            this.xrTableCell12,
            this.xrTableCell15,
            this.xrTableCell22});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 9.2D;
            // 
            // xrTableCell241
            // 
            this.xrTableCell241.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell241.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell241.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell241.Name = "xrTableCell241";
            this.xrTableCell241.StylePriority.UseBorderColor = false;
            this.xrTableCell241.StylePriority.UseBorders = false;
            this.xrTableCell241.StylePriority.UseFont = false;
            this.xrTableCell241.Text = "TT";
            this.xrTableCell241.Weight = 0.10177821312026929D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell11.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBorderColor = false;
            this.xrTableCell11.StylePriority.UseBorders = false;
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.Text = "MÃ NV";
            this.xrTableCell11.Weight = 0.26096103716076474D;
            // 
            // xrTableCell242
            // 
            this.xrTableCell242.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell242.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell242.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell242.Name = "xrTableCell242";
            this.xrTableCell242.StylePriority.UseBorderColor = false;
            this.xrTableCell242.StylePriority.UseBorders = false;
            this.xrTableCell242.StylePriority.UseFont = false;
            this.xrTableCell242.Text = "HỌ VÀ TÊN";
            this.xrTableCell242.Weight = 0.58271001611928319D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorderColor = false;
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "NGÀY SINH";
            this.xrTableCell1.Weight = 0.34822240731291076D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell8.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseBorderColor = false;
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.Text = "MỨC ĐÓNG BH";
            this.xrTableCell8.Weight = 0.36879027362600303D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorderColor = false;
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.Text = "BH PHẢI NỘP (32%)";
            this.xrTableCell3.Weight = 0.38200553054791547D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell5.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBorderColor = false;
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.Text = "SỐ HĐLĐ";
            this.xrTableCell5.Weight = 0.32958734707282289D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorderColor = false;
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Text = "NGÀY THÁNG HĐLĐ";
            this.xrTableCell2.Weight = 0.35657465488690776D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell12.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseBorderColor = false;
            this.xrTableCell12.StylePriority.UseBorders = false;
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.Text = "TỪ NĂM, THÁNG";
            this.xrTableCell12.Weight = 0.30678720089084183D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell15.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseBorderColor = false;
            this.xrTableCell15.StylePriority.UseBorders = false;
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.Text = "CHỨC VỤ";
            this.xrTableCell15.Weight = 0.4133445598815364D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell22.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell22.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseBorderColor = false;
            this.xrTableCell22.StylePriority.UseBorders = false;
            this.xrTableCell22.StylePriority.UseFont = false;
            this.xrTableCell22.Text = "LÝ DO GIẢM";
            this.xrTableCell22.Weight = 0.4133445598815364D;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrTable3,
            this.lblLapBang,
            this.lblThuTruong});
            this.ReportFooter.HeightF = 252.1667F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(834.9999F, 85.606F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(300F, 25F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Trần Thị Việt Hồng";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1140F, 25F);
            this.xrTable3.StylePriority.UseBorders = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCellTotalNgaySinh,
            this.xrTableCellTotalMucDongBH,
            this.xrTableCellTotalBHPhaiNop,
            this.xrTableCellTotalSoHDLD,
            this.xrTableCellTotalNgayThangHDLD,
            this.xrTableCellTotalTuNamThang,
            this.xrTableCellTotalChucVu,
            this.xrTableCellTotalLyDoTang});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell7.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBorders = false;
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell7.Weight = 0.077511557006487111D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell9.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell9.Weight = 0.19874094116912908D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell10.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseBorders = false;
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.Text = " Cộng:";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell10.Weight = 0.4437764407357408D;
            // 
            // xrTableCellTotalNgaySinh
            // 
            this.xrTableCellTotalNgaySinh.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalNgaySinh.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCellTotalNgaySinh.Name = "xrTableCellTotalNgaySinh";
            this.xrTableCellTotalNgaySinh.StylePriority.UseBorders = false;
            this.xrTableCellTotalNgaySinh.StylePriority.UseFont = false;
            this.xrTableCellTotalNgaySinh.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalNgaySinh.Text = " ";
            this.xrTableCellTotalNgaySinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalNgaySinh.Weight = 0.26519679007078378D;
            // 
            // xrTableCellTotalMucDongBH
            // 
            this.xrTableCellTotalMucDongBH.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalMucDongBH.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalMucDongBH.Name = "xrTableCellTotalMucDongBH";
            this.xrTableCellTotalMucDongBH.StylePriority.UseBorders = false;
            this.xrTableCellTotalMucDongBH.StylePriority.UseFont = false;
            this.xrTableCellTotalMucDongBH.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalMucDongBH.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalMucDongBH.Weight = 0.28086080642455996D;
            // 
            // xrTableCellTotalBHPhaiNop
            // 
            this.xrTableCellTotalBHPhaiNop.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalBHPhaiNop.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCellTotalBHPhaiNop.Name = "xrTableCellTotalBHPhaiNop";
            this.xrTableCellTotalBHPhaiNop.StylePriority.UseBorders = false;
            this.xrTableCellTotalBHPhaiNop.StylePriority.UseFont = false;
            this.xrTableCellTotalBHPhaiNop.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalBHPhaiNop.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalBHPhaiNop.Weight = 0.29092508588082566D;
            // 
            // xrTableCellTotalSoHDLD
            // 
            this.xrTableCellTotalSoHDLD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalSoHDLD.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCellTotalSoHDLD.Name = "xrTableCellTotalSoHDLD";
            this.xrTableCellTotalSoHDLD.StylePriority.UseBorders = false;
            this.xrTableCellTotalSoHDLD.StylePriority.UseFont = false;
            this.xrTableCellTotalSoHDLD.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalSoHDLD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalSoHDLD.Weight = 0.25100490981562262D;
            // 
            // xrTableCellTotalNgayThangHDLD
            // 
            this.xrTableCellTotalNgayThangHDLD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalNgayThangHDLD.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCellTotalNgayThangHDLD.Name = "xrTableCellTotalNgayThangHDLD";
            this.xrTableCellTotalNgayThangHDLD.StylePriority.UseBorders = false;
            this.xrTableCellTotalNgayThangHDLD.StylePriority.UseFont = false;
            this.xrTableCellTotalNgayThangHDLD.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalNgayThangHDLD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalNgayThangHDLD.Weight = 0.27155765394082548D;
            // 
            // xrTableCellTotalTuNamThang
            // 
            this.xrTableCellTotalTuNamThang.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalTuNamThang.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCellTotalTuNamThang.Name = "xrTableCellTotalTuNamThang";
            this.xrTableCellTotalTuNamThang.StylePriority.UseBorders = false;
            this.xrTableCellTotalTuNamThang.StylePriority.UseFont = false;
            this.xrTableCellTotalTuNamThang.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalTuNamThang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalTuNamThang.Weight = 0.2336409286763062D;
            // 
            // xrTableCellTotalChucVu
            // 
            this.xrTableCellTotalChucVu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalChucVu.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCellTotalChucVu.Name = "xrTableCellTotalChucVu";
            this.xrTableCellTotalChucVu.StylePriority.UseBorders = false;
            this.xrTableCellTotalChucVu.StylePriority.UseFont = false;
            this.xrTableCellTotalChucVu.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalChucVu.Text = " ";
            this.xrTableCellTotalChucVu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalChucVu.Weight = 0.31479220995272406D;
            // 
            // xrTableCellTotalLyDoTang
            // 
            this.xrTableCellTotalLyDoTang.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalLyDoTang.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCellTotalLyDoTang.Name = "xrTableCellTotalLyDoTang";
            this.xrTableCellTotalLyDoTang.StylePriority.UseBorders = false;
            this.xrTableCellTotalLyDoTang.StylePriority.UseFont = false;
            this.xrTableCellTotalLyDoTang.StylePriority.UseTextAlignment = false;
            this.xrTableCellTotalLyDoTang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTotalLyDoTang.Weight = 0.31479223787492749D;
            // 
            // lblLapBang
            // 
            this.lblLapBang.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblLapBang.LocationFloat = new DevExpress.Utils.PointFloat(78.09596F, 60.606F);
            this.lblLapBang.Name = "lblLapBang";
            this.lblLapBang.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblLapBang.SizeF = new System.Drawing.SizeF(180F, 25F);
            this.lblLapBang.StylePriority.UseFont = false;
            this.lblLapBang.StylePriority.UseTextAlignment = false;
            this.lblLapBang.Text = "NGƯỜI LẬP BIỂU";
            this.lblLapBang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblThuTruong
            // 
            this.lblThuTruong.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblThuTruong.LocationFloat = new DevExpress.Utils.PointFloat(834.9999F, 60.606F);
            this.lblThuTruong.Name = "lblThuTruong";
            this.lblThuTruong.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblThuTruong.SizeF = new System.Drawing.SizeF(300F, 25F);
            this.lblThuTruong.StylePriority.UseFont = false;
            this.lblThuTruong.StylePriority.UseTextAlignment = false;
            this.lblThuTruong.Text = "P. NHÂN SỰ - HÀNH CHÍNH";
            this.lblThuTruong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.GroupHeader1.HeightF = 28.95831F;
            this.GroupHeader1.Level = 2;
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
            this.xrTable2.SizeF = new System.Drawing.SizeF(1140F, 28.95831F);
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
            this.xrGroupDepartment});
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
            this.xrTableCell27.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.StylePriority.UseFont = false;
            this.xrTableCell27.StylePriority.UseTextAlignment = false;
            this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell27.Weight = 0.31326299853441203D;
            // 
            // xrGroupDepartment
            // 
            this.xrGroupDepartment.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrGroupDepartment.Name = "xrGroupDepartment";
            this.xrGroupDepartment.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrGroupDepartment.StylePriority.UseFont = false;
            this.xrGroupDepartment.StylePriority.UsePadding = false;
            this.xrGroupDepartment.StylePriority.UseTextAlignment = false;
            this.xrGroupDepartment.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrGroupDepartment.Weight = 11.206731245060025D;
            this.xrGroupDepartment.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Group_BeforePrint);
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.GroupHeader2.HeightF = 28.95831F;
            this.GroupHeader2.Level = 1;
            this.GroupHeader2.Name = "GroupHeader2";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1140F, 28.95831F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UsePadding = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrGroupConstruction});
            this.xrTableRow1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
            this.xrTableRow1.StylePriority.UseFont = false;
            this.xrTableRow1.StylePriority.UsePadding = false;
            this.xrTableRow1.StylePriority.UseTextAlignment = false;
            this.xrTableRow1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell4.Weight = 0.31326299853441203D;
            // 
            // xrGroupConstruction
            // 
            this.xrGroupConstruction.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrGroupConstruction.Name = "xrGroupConstruction";
            this.xrGroupConstruction.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrGroupConstruction.StylePriority.UseFont = false;
            this.xrGroupConstruction.StylePriority.UsePadding = false;
            this.xrGroupConstruction.StylePriority.UseTextAlignment = false;
            this.xrGroupConstruction.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrGroupConstruction.Weight = 11.206731245060025D;
            // 
            // GroupHeader3
            // 
            this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
            this.GroupHeader3.HeightF = 28.95831F;
            this.GroupHeader3.Name = "GroupHeader3";
            // 
            // xrTable4
            // 
            this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable4.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrTable4.SizeF = new System.Drawing.SizeF(1140F, 28.95831F);
            this.xrTable4.StylePriority.UseBorders = false;
            this.xrTable4.StylePriority.UseFont = false;
            this.xrTable4.StylePriority.UsePadding = false;
            this.xrTable4.StylePriority.UseTextAlignment = false;
            this.xrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrGroupTeam});
            this.xrTableRow4.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
            this.xrTableRow4.StylePriority.UseFont = false;
            this.xrTableRow4.StylePriority.UsePadding = false;
            this.xrTableRow4.StylePriority.UseTextAlignment = false;
            this.xrTableRow4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell13.Weight = 0.31326299853441203D;
            // 
            // xrGroupTeam
            // 
            this.xrGroupTeam.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrGroupTeam.Name = "xrGroupTeam";
            this.xrGroupTeam.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrGroupTeam.StylePriority.UseFont = false;
            this.xrGroupTeam.StylePriority.UsePadding = false;
            this.xrGroupTeam.StylePriority.UseTextAlignment = false;
            this.xrGroupTeam.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrGroupTeam.Weight = 11.206731245060025D;
            // 
            // rp_DecreaseInsurance
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter,
            this.GroupHeader1,
            this.GroupHeader2,
            this.GroupHeader3});
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
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}