using System;
using DevExpress.XtraReports.UI;
using Web.Core.Object.Report;
using Web.Core.Service.Catalog;
using Web.Core.Framework.Adapter;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Summary description for rp_UnionMember
    /// </summary>
    public class rp_UnionMember : XtraReport
    {
        private DetailBand Detail;
        private TopMarginBand TopMargin;
        private BottomMarginBand BottomMargin;
        private ReportHeaderBand ReportHeader;
        private ReportFooterBand ReportFooter;
        private PageHeaderBand PageHeader;
        private XRLabel xrl_TitleReport;
        private XRLabel xrl_CompanyName;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCell4;
        private XRTableCell xrTableCell3;
        private XRTableCell xrTableCell5;
        private XRTableCell xrTableCell7;
        private XRTableCell xrTableCell8;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrtId;
        private XRTableCell xrt_EmployeeCode;
        private XRTableCell xrt_FullName;
        private XRTableCell xrt_BirthDate;
        private XRTableCell xrt_Sex;
        private XRTableCell xrt_PhoneNumber;
        private XRTableCell xrt_UnionJoinedDate;
        private XRLabel xrl_ten3;
        private XRLabel xrl_ten2;
        private XRLabel xrl_ten1;
        private XRLabel xrt_OutputDate;
        private XRLabel xrl_footer1;
        private XRLabel xrl_footer3;
        private XRLabel xrl_footer2;
        private XRTableCell xrTableCell6;
        private XRTableCell xrt_UnionPosition;
        private GroupHeaderBand GroupHeader1;
        private XRTable xrTable3;
        private XRTableRow xrTableRow3;
        private XRTableCell xrTableCell16;
        private XRTableCell xrl_DepartmentName;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public rp_UnionMember()
        {
            InitializeComponent();

        }

        int _id = 0;

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _id++;
            xrtId.Text = _id.ToString();

        }

        private void Group_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _id = 0;
            xrtId.Text = _id.ToString();

        }

        public void BindData(ReportFilter filter)
        {
            try
            {
                var location = new ReportController().GetCityName(filter.SessionDepartment);
                xrt_OutputDate.Text = string.Format(xrt_OutputDate.Text, location, DateTime.Now.Day,
                    DateTime.Now.Month, DateTime.Now.Year);
                xrl_CompanyName.Text = ReportController.GetInstance().GetCompanyName(filter.SessionDepartment);

                // get organization
                var organization = cat_DepartmentServices.GetByDepartments(filter.SessionDepartment);
                if (organization != null)
                {
                    //select form db
                    var arrOrgCode = string.IsNullOrEmpty(filter.SelectedDepartment)
                        ? new string[] { }
                        : filter.SelectedDepartment.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < arrOrgCode.Length; i++)
                    {
                        arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
                    }

                    var table = SQLHelper.ExecuteTable(
                        SQLManagementAdapter.GetStore_ReportListEmployeeJoinUnion(string.Join(",", arrOrgCode)));
                    DataSource = table;

                    //binding data

                    xrt_EmployeeCode.DataBindings.Add("Text", DataSource, "EmployeeCode");
                    xrt_FullName.DataBindings.Add("Text", DataSource, "FullName");
                    xrt_BirthDate.DataBindings.Add("Text", DataSource, "BirthDate", "{0:dd/MM/yyyy}");
                    xrt_Sex.DataBindings.Add("Text", DataSource, "Sex");
                    xrt_PhoneNumber.DataBindings.Add("Text", DataSource, "Phone");
                    xrt_UnionJoinedDate.DataBindings.Add("Text", DataSource, "UnionJoinedDate", "{0:dd/MM/yyyy}");
                    xrt_UnionPosition.DataBindings.Add("Text", DataSource, "Position");
                    GroupHeader1.GroupFields.AddRange(new[]
                        {new GroupField("DepartmentId", XRColumnSortOrder.Ascending)});
                    xrl_DepartmentName.DataBindings.Add("Text", DataSource, "DepartmentName");
                }
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
            string resourceFileName = "rp_UnionMember.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrtId = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_EmployeeCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_FullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_BirthDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_Sex = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_PhoneNumber = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_UnionJoinedDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_UnionPosition = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrl_TitleReport = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_CompanyName = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrl_ten3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ten2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ten1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrt_OutputDate = new DevExpress.XtraReports.UI.XRLabel();
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
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrl_DepartmentName = new DevExpress.XtraReports.UI.XRTableCell();
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
            this.Detail.HeightF = 31.04164F;
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
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[]
            {
                this.xrTableRow2
            });
            this.xrTable2.SizeF = new System.Drawing.SizeF(763.0001F, 31.04164F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[]
            {
                this.xrtId,
                this.xrt_EmployeeCode,
                this.xrt_FullName,
                this.xrt_BirthDate,
                this.xrt_Sex,
                this.xrt_PhoneNumber,
                this.xrt_UnionJoinedDate,
                this.xrt_UnionPosition
            });
            this.xrTableRow2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 3, 0, 100F);
            this.xrTableRow2.StylePriority.UseFont = false;
            this.xrTableRow2.StylePriority.UsePadding = false;
            this.xrTableRow2.StylePriority.UseTextAlignment = false;
            this.xrTableRow2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow2.Weight = 1D;
            // 
            // xrtId
            // 
            this.xrtId.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrtId.Name = "xrtId";
            this.xrtId.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtId.StylePriority.UseFont = false;
            this.xrtId.StylePriority.UsePadding = false;
            this.xrtId.StylePriority.UseTextAlignment = false;
            this.xrtId.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtId.Weight = 0.35416665980020634D;
            this.xrtId.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrt_EmployeeCode
            // 
            this.xrt_EmployeeCode.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_EmployeeCode.Name = "xrt_EmployeeCode";
            this.xrt_EmployeeCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_EmployeeCode.StylePriority.UseFont = false;
            this.xrt_EmployeeCode.StylePriority.UsePadding = false;
            this.xrt_EmployeeCode.StylePriority.UseTextAlignment = false;
            this.xrt_EmployeeCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_EmployeeCode.Weight = 0.850839861252338D;
            // 
            // xrt_FullName
            // 
            this.xrt_FullName.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_FullName.Name = "xrt_FullName";
            this.xrt_FullName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_FullName.StylePriority.UseFont = false;
            this.xrt_FullName.StylePriority.UsePadding = false;
            this.xrt_FullName.StylePriority.UseTextAlignment = false;
            this.xrt_FullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_FullName.Weight = 1.3073458056306979D;
            // 
            // xrt_BirthDate
            // 
            this.xrt_BirthDate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_BirthDate.Name = "xrt_BirthDate";
            this.xrt_BirthDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_BirthDate.StylePriority.UseFont = false;
            this.xrt_BirthDate.StylePriority.UsePadding = false;
            this.xrt_BirthDate.StylePriority.UseTextAlignment = false;
            this.xrt_BirthDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_BirthDate.Weight = 0.992142906539754D;
            // 
            // xrt_Sex
            // 
            this.xrt_Sex.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_Sex.Name = "xrt_Sex";
            this.xrt_Sex.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_Sex.StylePriority.UseFont = false;
            this.xrt_Sex.StylePriority.UsePadding = false;
            this.xrt_Sex.StylePriority.UseTextAlignment = false;
            this.xrt_Sex.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_Sex.Weight = 0.71620408716210271D;
            // 
            // xrt_PhoneNumber
            // 
            this.xrt_PhoneNumber.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_PhoneNumber.Name = "xrt_PhoneNumber";
            this.xrt_PhoneNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_PhoneNumber.StylePriority.UseFont = false;
            this.xrt_PhoneNumber.StylePriority.UsePadding = false;
            this.xrt_PhoneNumber.StylePriority.UseTextAlignment = false;
            this.xrt_PhoneNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_PhoneNumber.Weight = 0.79359645439110127D;
            // 
            // xrt_UnionJoinedDate
            // 
            this.xrt_UnionJoinedDate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_UnionJoinedDate.Name = "xrt_UnionJoinedDate";
            this.xrt_UnionJoinedDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_UnionJoinedDate.StylePriority.UseFont = false;
            this.xrt_UnionJoinedDate.StylePriority.UsePadding = false;
            this.xrt_UnionJoinedDate.StylePriority.UseTextAlignment = false;
            this.xrt_UnionJoinedDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_UnionJoinedDate.Weight = 0.9152635630237429D;
            // 
            // xrt_UnionPosition
            // 
            this.xrt_UnionPosition.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_UnionPosition.Name = "xrt_UnionPosition";
            this.xrt_UnionPosition.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_UnionPosition.StylePriority.UseFont = false;
            this.xrt_UnionPosition.StylePriority.UsePadding = false;
            this.xrt_UnionPosition.StylePriority.UseTextAlignment = false;
            this.xrt_UnionPosition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_UnionPosition.Weight = 1.2809879724891586D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 48F;
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
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrl_TitleReport,
                this.xrl_CompanyName
            });
            this.ReportHeader.HeightF = 99.54166F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrl_TitleReport
            // 
            this.xrl_TitleReport.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_TitleReport.LocationFloat = new DevExpress.Utils.PointFloat(0F, 59.12501F);
            this.xrl_TitleReport.Name = "xrl_TitleReport";
            this.xrl_TitleReport.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_TitleReport.SizeF = new System.Drawing.SizeF(753F, 23F);
            this.xrl_TitleReport.StylePriority.UseFont = false;
            this.xrl_TitleReport.StylePriority.UseTextAlignment = false;
            this.xrl_TitleReport.Text = "BÁO CÁO DANH SÁCH CÁN BỘ THAM GIA CÔNG ĐOÀN";
            this.xrl_TitleReport.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_CompanyName
            // 
            this.xrl_CompanyName.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_CompanyName.LocationFloat = new DevExpress.Utils.PointFloat(0F, 21.62501F);
            this.xrl_CompanyName.Name = "xrl_CompanyName";
            this.xrl_CompanyName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 2, 0, 0, 100F);
            this.xrl_CompanyName.SizeF = new System.Drawing.SizeF(530.5989F, 22.99999F);
            this.xrl_CompanyName.StylePriority.UseFont = false;
            this.xrl_CompanyName.StylePriority.UsePadding = false;
            this.xrl_CompanyName.StylePriority.UseTextAlignment = false;
            this.xrl_CompanyName.Text = "CÔNG TY TNHH THƯƠNG MẠI VÀ XÂY DỰNG TRUNG CHÍNH";
            this.xrl_CompanyName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrl_ten3,
                this.xrl_ten2,
                this.xrl_ten1,
                this.xrt_OutputDate,
                this.xrl_footer1,
                this.xrl_footer3,
                this.xrl_footer2
            });
            this.ReportFooter.HeightF = 227F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrl_ten3
            // 
            this.xrl_ten3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten3.LocationFloat = new DevExpress.Utils.PointFloat(526.3914F, 144.7917F);
            this.xrl_ten3.Name = "xrl_ten3";
            this.xrl_ten3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten3.SizeF = new System.Drawing.SizeF(209.1918F, 23F);
            this.xrl_ten3.StylePriority.UseFont = false;
            this.xrl_ten3.StylePriority.UseTextAlignment = false;
            this.xrl_ten3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_ten2
            // 
            this.xrl_ten2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten2.LocationFloat = new DevExpress.Utils.PointFloat(270.7912F, 144.7917F);
            this.xrl_ten2.Name = "xrl_ten2";
            this.xrl_ten2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten2.SizeF = new System.Drawing.SizeF(196.9735F, 23F);
            this.xrl_ten2.StylePriority.UseFont = false;
            this.xrl_ten2.StylePriority.UseTextAlignment = false;
            this.xrl_ten2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_ten1
            // 
            this.xrl_ten1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten1.LocationFloat = new DevExpress.Utils.PointFloat(22.14263F, 144.7917F);
            this.xrl_ten1.Name = "xrl_ten1";
            this.xrl_ten1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten1.SizeF = new System.Drawing.SizeF(186.4159F, 23F);
            this.xrl_ten1.StylePriority.UseFont = false;
            this.xrl_ten1.StylePriority.UseTextAlignment = false;
            this.xrl_ten1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrt_OutputDate
            // 
            this.xrt_OutputDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Italic);
            this.xrt_OutputDate.LocationFloat = new DevExpress.Utils.PointFloat(474.3086F, 20.83333F);
            this.xrt_OutputDate.Name = "xrt_OutputDate";
            this.xrt_OutputDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrt_OutputDate.SizeF = new System.Drawing.SizeF(261.2751F, 23F);
            this.xrt_OutputDate.StylePriority.UseFont = false;
            this.xrt_OutputDate.StylePriority.UseTextAlignment = false;
            this.xrt_OutputDate.Text = "{0}, ngày {1} tháng {2} năm {3}";
            this.xrt_OutputDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer1
            // 
            this.xrl_footer1.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer1.LocationFloat = new DevExpress.Utils.PointFloat(20.14163F, 64.58334F);
            this.xrl_footer1.Name = "xrl_footer1";
            this.xrl_footer1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer1.SizeF = new System.Drawing.SizeF(188.4169F, 23F);
            this.xrl_footer1.StylePriority.UseFont = false;
            this.xrl_footer1.StylePriority.UseTextAlignment = false;
            this.xrl_footer1.Text = "NGƯỜI LẬP";
            this.xrl_footer1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer3
            // 
            this.xrl_footer3.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer3.LocationFloat = new DevExpress.Utils.PointFloat(526.3914F, 64.58334F);
            this.xrl_footer3.Name = "xrl_footer3";
            this.xrl_footer3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer3.SizeF = new System.Drawing.SizeF(209.192F, 23F);
            this.xrl_footer3.StylePriority.UseFont = false;
            this.xrl_footer3.StylePriority.UseTextAlignment = false;
            this.xrl_footer3.Text = "TỔNG GIÁM ĐỐC";
            this.xrl_footer3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer2
            // 
            this.xrl_footer2.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer2.LocationFloat = new DevExpress.Utils.PointFloat(270.7912F, 64.58334F);
            this.xrl_footer2.Name = "xrl_footer2";
            this.xrl_footer2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer2.SizeF = new System.Drawing.SizeF(198.9744F, 23F);
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
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[]
            {
                this.xrTableRow1
            });
            this.xrTable1.SizeF = new System.Drawing.SizeF(763.0001F, 38.33331F);
            this.xrTable1.StylePriority.UseBorders = false;
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
                this.xrTableCell7,
                this.xrTableCell8,
                this.xrTableCell6
            });
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
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "STT";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell1.Weight = 0.35416665980020634D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "Mã nhân viên";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 0.850839861252338D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "Họ và tên";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell4.Weight = 1.3073458056306979D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "Ngày sinh";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell3.Weight = 0.992142906539754D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "Giới tính";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell5.Weight = 0.716203630876183D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "Điện thoại";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell7.Weight = 0.79359689974940806D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "Ngày vào công đoàn";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell8.Weight = 0.91526291443520846D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "Chức vụ công đoàn";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell6.Weight = 1.280988033708959D;
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
            this.xrTable3.SizeF = new System.Drawing.SizeF(763F, 25.41666F);
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
                this.xrl_DepartmentName
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
            // xrl_DepartmentName
            // 
            this.xrl_DepartmentName.Borders =
                ((DevExpress.XtraPrinting.BorderSide) ((DevExpress.XtraPrinting.BorderSide.Right |
                                                        DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrl_DepartmentName.Font =
                new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrl_DepartmentName.Name = "xrl_DepartmentName";
            this.xrl_DepartmentName.StylePriority.UseBorders = false;
            this.xrl_DepartmentName.StylePriority.UseFont = false;
            this.xrl_DepartmentName.StylePriority.UseTextAlignment = false;
            this.xrl_DepartmentName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrl_DepartmentName.Weight = 13.200632854431591D;
            this.xrl_DepartmentName.BeforePrint +=
                new System.Drawing.Printing.PrintEventHandler(this.Group_BeforePrint);
            // 
            // rp_UnionMember
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[]
            {
                this.Detail,
                this.TopMargin,
                this.BottomMargin,
                this.ReportHeader,
                this.ReportFooter,
                this.PageHeader,
                this.GroupHeader1
            });
            this.Margins = new System.Drawing.Printing.Margins(42, 22, 48, 0);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize) (this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this)).EndInit();

        }

        #endregion
    }
}