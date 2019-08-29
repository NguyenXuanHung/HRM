using System;
using Web.Core;
using DevExpress.XtraReports.UI;
using Web.Core.Framework;
using Web.Core.Object.Report;
using Web.Core.Service.Catalog;
using Web.Core.Framework.Adapter;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Summary description for rp_BankAccount
    /// </summary>
    public class rp_BankAccount : XtraReport
    {
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private PageHeaderBand PageHeader;
        private ReportHeaderBand ReportHeader;
        private ReportFooterBand ReportFooter;
        private XRLabel xrl_TitleReport;
        private XRLabel xrl_CompanyName;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCell4;
        private XRTableCell xrTableCell3;
        private XRTableCell xrTableCell5;
        private XRTableCell xrTableCell8;
        private XRTableCell xrTableCell6;
        private XRTableCell xrTableCell12;
        private XRTableCell xrTableCell7;
        private GroupHeaderBand GroupHeader1;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrt_Id;
        private XRTableCell xrt_EmployeeCode;
        private XRTableCell xrt_FullName;
        private XRTableCell xrt_BirthDate;
        private XRTableCell xrt_Sex;
        private XRTableCell xrt_ParticipationDate;
        private XRTableCell xrt_ATMNumber;
        private XRTableCell xrt_Position;
        private XRTableCell xrt_Address;
        private XRLabel xrl_ten1;
        private XRLabel xrl_ten2;
        private XRLabel xrl_ten3;
        private XRLabel xrt_OutputDate;
        private XRLabel xrl_footer2;
        private XRLabel xrl_footer3;
        private XRLabel xrl_footer1;
        private XRTableCell xrTableCell9;
        private XRTableCell xrt_BankName;
        private XRTableCell xrTableCell10;
        private XRTableCell xrt_Decription;
        private XRTable xrTable3;
        private XRTableRow xrTableRow3;
        private XRTableCell xrt_GroupDepartment;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public rp_BankAccount()
        {
            InitializeComponent();

        }

        int _id = 0;

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _id++;
            xrt_Id.Text = _id.ToString();

        }

        private void Group_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _id = 0;
            xrt_Id.Text = _id.ToString();
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
                    var departments = string.IsNullOrEmpty(filter.SelectedDepartment)
                        ? new string[] { }
                        : filter.SelectedDepartment.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < departments.Length; i++)
                    {
                        departments[i] = "'{0}'".FormatWith(departments[i]);
                    }

                    var table = SQLHelper.ExecuteTable(
                        SQLManagementAdapter.GetStore_ReportListEmployeeATMNumber(string.Join(",", departments)));

                    DataSource = table;

                    //binding data

                    xrt_EmployeeCode.DataBindings.Add("Text", DataSource, "EmployeeCode");
                    xrt_FullName.DataBindings.Add("Text", DataSource, "FullName");
                    xrt_BirthDate.DataBindings.Add("Text", DataSource, "BirthDate", "{0:dd/MM/yyyy}");
                    xrt_Sex.DataBindings.Add("Text", DataSource, "SexName");
                    xrt_ParticipationDate.DataBindings.Add("Text", DataSource, "ParticipationDate", "{0:dd/MM/yyyy}");
                    xrt_ATMNumber.DataBindings.Add("Text", DataSource, "AccountNumber");
                    xrt_BankName.DataBindings.Add("Text", DataSource, "BankName");
                    xrt_Position.DataBindings.Add("Text", DataSource, "PositionName");
                    xrt_Address.DataBindings.Add("Text", DataSource, "Address");
                    GroupHeader1.GroupFields.AddRange(new[]
                    {
                        new GroupField("DepartmentId", XRColumnSortOrder.Ascending)
                    });
                    xrt_GroupDepartment.DataBindings.Add("Text", DataSource, "DepartmentName");
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
            string resourceFileName = "rp_BankAccount.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrt_Id = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_EmployeeCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_FullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_BirthDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_Sex = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_ParticipationDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_ATMNumber = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_BankName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_Position = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_Address = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_Decription = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrl_TitleReport = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_CompanyName = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrl_ten1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ten2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ten3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrt_OutputDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_footer2 = new DevExpress.XtraReports.UI.XRLabel();
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
                ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left |
                                                         DevExpress.XtraPrinting.BorderSide.Right)
                                                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[]
            {
                this.xrTableRow2
            });
            this.xrTable2.SizeF = new System.Drawing.SizeF(1146F, 31.04164F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[]
            {
                this.xrt_Id,
                this.xrt_EmployeeCode,
                this.xrt_FullName,
                this.xrt_BirthDate,
                this.xrt_Sex,
                this.xrt_ParticipationDate,
                this.xrt_ATMNumber,
                this.xrt_BankName,
                this.xrt_Position,
                this.xrt_Address,
                this.xrt_Decription
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
            // xrt_Id
            // 
            this.xrt_Id.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_Id.Name = "xrt_Id";
            this.xrt_Id.StylePriority.UseFont = false;
            this.xrt_Id.StylePriority.UseTextAlignment = false;
            this.xrt_Id.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_Id.Weight = 0.35416665980020634D;
            this.xrt_Id.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrt_EmployeeCode
            // 
            this.xrt_EmployeeCode.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_EmployeeCode.Name = "xrt_EmployeeCode";
            this.xrt_EmployeeCode.StylePriority.UseFont = false;
            this.xrt_EmployeeCode.StylePriority.UseTextAlignment = false;
            this.xrt_EmployeeCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_EmployeeCode.Weight = 0.72286765429288458D;
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
            this.xrt_FullName.Weight = 1.2285933690740742D;
            // 
            // xrt_BirthDate
            // 
            this.xrt_BirthDate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_BirthDate.Name = "xrt_BirthDate";
            this.xrt_BirthDate.StylePriority.UseFont = false;
            this.xrt_BirthDate.StylePriority.UseTextAlignment = false;
            this.xrt_BirthDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_BirthDate.Weight = 0.845389348308942D;
            // 
            // xrt_Sex
            // 
            this.xrt_Sex.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_Sex.Name = "xrt_Sex";
            this.xrt_Sex.StylePriority.UseFont = false;
            this.xrt_Sex.StylePriority.UseTextAlignment = false;
            this.xrt_Sex.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_Sex.Weight = 0.54418086236347818D;
            // 
            // xrt_ParticipationDate
            // 
            this.xrt_ParticipationDate.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_ParticipationDate.Name = "xrt_ParticipationDate";
            this.xrt_ParticipationDate.StylePriority.UseFont = false;
            this.xrt_ParticipationDate.StylePriority.UseTextAlignment = false;
            this.xrt_ParticipationDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_ParticipationDate.Weight = 0.885961286175112D;
            // 
            // xrt_ATMNumber
            // 
            this.xrt_ATMNumber.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_ATMNumber.Name = "xrt_ATMNumber";
            this.xrt_ATMNumber.StylePriority.UseFont = false;
            this.xrt_ATMNumber.StylePriority.UseTextAlignment = false;
            this.xrt_ATMNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_ATMNumber.Weight = 1.0998967991677404D;
            // 
            // xrt_BankName
            // 
            this.xrt_BankName.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_BankName.Name = "xrt_BankName";
            this.xrt_BankName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_BankName.StylePriority.UseFont = false;
            this.xrt_BankName.StylePriority.UsePadding = false;
            this.xrt_BankName.StylePriority.UseTextAlignment = false;
            this.xrt_BankName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_BankName.Weight = 1.4912763793217467D;
            // 
            // xrt_Position
            // 
            this.xrt_Position.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_Position.Name = "xrt_Position";
            this.xrt_Position.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_Position.StylePriority.UseFont = false;
            this.xrt_Position.StylePriority.UsePadding = false;
            this.xrt_Position.StylePriority.UseTextAlignment = false;
            this.xrt_Position.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_Position.Weight = 1.0262042318468194D;
            // 
            // xrt_Address
            // 
            this.xrt_Address.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_Address.Name = "xrt_Address";
            this.xrt_Address.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_Address.StylePriority.UseFont = false;
            this.xrt_Address.StylePriority.UsePadding = false;
            this.xrt_Address.StylePriority.UseTextAlignment = false;
            this.xrt_Address.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_Address.Weight = 1.5962833612216634D;
            // 
            // xrt_Decription
            // 
            this.xrt_Decription.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrt_Decription.Name = "xrt_Decription";
            this.xrt_Decription.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrt_Decription.StylePriority.UseFont = false;
            this.xrt_Decription.StylePriority.UsePadding = false;
            this.xrt_Decription.StylePriority.UseTextAlignment = false;
            this.xrt_Decription.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_Decription.Weight = 1.0351746368064754D;
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
                ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left |
                                                          DevExpress.XtraPrinting.BorderSide.Top)
                                                         | DevExpress.XtraPrinting.BorderSide.Right)
                                                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[]
            {
                this.xrTableRow1
            });
            this.xrTable1.SizeF = new System.Drawing.SizeF(1146F, 38.33331F);
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
                this.xrTableCell8,
                this.xrTableCell7,
                this.xrTableCell9,
                this.xrTableCell6,
                this.xrTableCell12,
                this.xrTableCell10
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
            this.xrTableCell2.Text = "Mã NV";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 0.72286765429288458D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "Họ và tên";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell4.Weight = 1.2285933690740742D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "Ngày sinh";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell3.Weight = 0.845389348308942D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "Giới tính";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell5.Weight = 0.54418086236347818D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "Ngày nhận việc";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell8.Weight = 0.885961286175112D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "Số tài khoản";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell7.Weight = 1.0998967991677404D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "Ngân hàng";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell9.Weight = 1.4912763793217467D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "Vị trí công việc";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell6.Weight = 1.0262042318468194D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "Địa chỉ";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell12.Weight = 1.5962833612216634D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.Text = "Ghi chú";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell10.Weight = 1.0351746368064754D;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrl_TitleReport,
                this.xrl_CompanyName
            });
            this.ReportHeader.HeightF = 114.875F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrl_TitleReport
            // 
            this.xrl_TitleReport.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_TitleReport.LocationFloat = new DevExpress.Utils.PointFloat(0F, 76.0417F);
            this.xrl_TitleReport.Name = "xrl_TitleReport";
            this.xrl_TitleReport.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_TitleReport.SizeF = new System.Drawing.SizeF(1146F, 23F);
            this.xrl_TitleReport.StylePriority.UseFont = false;
            this.xrl_TitleReport.StylePriority.UseTextAlignment = false;
            this.xrl_TitleReport.Text = "BÁO CÁO DANH SÁCH TÀI KHOẢN NHÂN VIÊN";
            this.xrl_TitleReport.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_CompanyName
            // 
            this.xrl_CompanyName.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_CompanyName.LocationFloat = new DevExpress.Utils.PointFloat(0F, 23.5417F);
            this.xrl_CompanyName.Name = "xrl_CompanyName";
            this.xrl_CompanyName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 2, 0, 0, 100F);
            this.xrl_CompanyName.SizeF = new System.Drawing.SizeF(484.7656F, 23F);
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
                this.xrl_ten1,
                this.xrl_ten2,
                this.xrl_ten3,
                this.xrt_OutputDate,
                this.xrl_footer2,
                this.xrl_footer3,
                this.xrl_footer1
            });
            this.ReportFooter.HeightF = 226F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrl_ten1
            // 
            this.xrl_ten1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten1.LocationFloat = new DevExpress.Utils.PointFloat(31.25F, 176.0417F);
            this.xrl_ten1.Name = "xrl_ten1";
            this.xrl_ten1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten1.SizeF = new System.Drawing.SizeF(302.1819F, 23F);
            this.xrl_ten1.StylePriority.UseFont = false;
            this.xrl_ten1.StylePriority.UseTextAlignment = false;
            this.xrl_ten1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_ten2
            // 
            this.xrl_ten2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten2.LocationFloat = new DevExpress.Utils.PointFloat(406.25F, 176.0417F);
            this.xrl_ten2.Name = "xrl_ten2";
            this.xrl_ten2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten2.SizeF = new System.Drawing.SizeF(302.1819F, 23F);
            this.xrl_ten2.StylePriority.UseFont = false;
            this.xrl_ten2.StylePriority.UseTextAlignment = false;
            this.xrl_ten2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_ten3
            // 
            this.xrl_ten3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrl_ten3.LocationFloat = new DevExpress.Utils.PointFloat(818.75F, 176.0417F);
            this.xrl_ten3.Name = "xrl_ten3";
            this.xrl_ten3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_ten3.SizeF = new System.Drawing.SizeF(295.4998F, 23F);
            this.xrl_ten3.StylePriority.UseFont = false;
            this.xrl_ten3.StylePriority.UseTextAlignment = false;
            this.xrl_ten3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrt_OutputDate
            // 
            this.xrt_OutputDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Italic);
            this.xrt_OutputDate.LocationFloat = new DevExpress.Utils.PointFloat(818.75F, 38.54167F);
            this.xrt_OutputDate.Name = "xrt_OutputDate";
            this.xrt_OutputDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrt_OutputDate.SizeF = new System.Drawing.SizeF(295.4998F, 23F);
            this.xrt_OutputDate.StylePriority.UseFont = false;
            this.xrt_OutputDate.StylePriority.UseTextAlignment = false;
            this.xrt_OutputDate.Text = "{0}, ngày {1} tháng {2} năm {3}";
            this.xrt_OutputDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer2
            // 
            this.xrl_footer2.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer2.LocationFloat = new DevExpress.Utils.PointFloat(406.25F, 63.54167F);
            this.xrl_footer2.Name = "xrl_footer2";
            this.xrl_footer2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer2.SizeF = new System.Drawing.SizeF(304.1828F, 23F);
            this.xrl_footer2.StylePriority.UseFont = false;
            this.xrl_footer2.StylePriority.UseTextAlignment = false;
            this.xrl_footer2.Text = "PHÒNG HCNS";
            this.xrl_footer2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer3
            // 
            this.xrl_footer3.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer3.LocationFloat = new DevExpress.Utils.PointFloat(818.75F, 63.54167F);
            this.xrl_footer3.Name = "xrl_footer3";
            this.xrl_footer3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer3.SizeF = new System.Drawing.SizeF(295.5F, 23F);
            this.xrl_footer3.StylePriority.UseFont = false;
            this.xrl_footer3.StylePriority.UseTextAlignment = false;
            this.xrl_footer3.Text = "TỔNG GIÁM ĐỐC";
            this.xrl_footer3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrl_footer1
            // 
            this.xrl_footer1.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrl_footer1.LocationFloat = new DevExpress.Utils.PointFloat(31.25F, 63.54167F);
            this.xrl_footer1.Name = "xrl_footer1";
            this.xrl_footer1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrl_footer1.SizeF = new System.Drawing.SizeF(304.1828F, 23F);
            this.xrl_footer1.StylePriority.UseFont = false;
            this.xrl_footer1.StylePriority.UseTextAlignment = false;
            this.xrl_footer1.Text = "NGƯỜI LẬP";
            this.xrl_footer1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrTable3
            });
            this.GroupHeader1.HeightF = 25F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable3
            // 
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[]
            {
                this.xrTableRow3
            });
            this.xrTable3.SizeF = new System.Drawing.SizeF(1146F, 25F);
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[]
            {
                this.xrt_GroupDepartment
            });
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrt_GroupDepartment
            // 
            this.xrt_GroupDepartment.Borders =
                ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left |
                                                         DevExpress.XtraPrinting.BorderSide.Right)
                                                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_GroupDepartment.Font =
                new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrt_GroupDepartment.Name = "xrt_GroupDepartment";
            this.xrt_GroupDepartment.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 3, 3, 3, 100F);
            this.xrt_GroupDepartment.StylePriority.UseBorders = false;
            this.xrt_GroupDepartment.StylePriority.UseFont = false;
            this.xrt_GroupDepartment.StylePriority.UsePadding = false;
            this.xrt_GroupDepartment.StylePriority.UseTextAlignment = false;
            this.xrt_GroupDepartment.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_GroupDepartment.Weight = 2D;
            this.xrt_GroupDepartment.BeforePrint +=
                new System.Drawing.Printing.PrintEventHandler(this.Group_BeforePrint);
            // 
            // rp_BankAccount
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[]
            {
                this.Detail,
                this.TopMargin,
                this.BottomMargin,
                this.PageHeader,
                this.ReportHeader,
                this.ReportFooter,
                this.GroupHeader1
            });
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
}