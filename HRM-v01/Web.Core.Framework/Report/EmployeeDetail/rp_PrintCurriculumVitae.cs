using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Web.Core;
using Web.Core.Framework.Interface;
using Web.Core.Framework.Report.Base;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Summary description for in lý lịch trích ngang
    /// </summary>
    public class rp_PrintCurriculumVitae : XtraReport, IBaseReport
    {
        private DetailBand Detail;
        private TopMarginBand TopMargin;
        private BottomMarginBand BottomMargin;
        private ReportHeaderBand ReportHeader;
        private PageHeaderBand PageHeader;
        private FormattingRule formattingRule1;
        private ReportFooterBand ReportFooter;
        private XRTable tblDetail;
        private XRTableRow xrDetailRow1;
        private XRTableCell xrt_FullName;
        private XRTableCell xrt_BirthDate;
        private XRTableCell xrt_QuantumDegree;
        private XRTable tblPageHeader;
        private XRTableRow xrTableRow11;
        private XRTableCell xrTableCell241;
        private XRTableCell xrTableCell242;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCell5;
        private XRTableCell xrTableCell6;
        private XRTableCell xrt_STT;
        private XRTableCell xrt_QuantumName;
        private XRTableCell xrt_QuantumCode;
        private XRTableCell xrt_Education;
        private XRTable xrTable4;
        private XRTableRow xrTableRow2;
        private XRTableCell xrTableCell10;
        private XRTableCell xrTableCell14;
        private XRTableRow xrTableRow3;
        private XRTableCell xrTableCell16;
        private XRTableCell xrTableCell17;
        private XRTableRow xrTableRow4;
        private XRTableCell xrTableCell20;
        private XRTableRow xrTableRow7;
        private XRTableCell xrTableCell19;
        private XRTableRow xrTableRow8;
        private XRTableCell xrTableCell21;
        private XRTableCell xrTableCell3;
        private XRTableCell xrTableCell7;
        private XRTableCell xrt_EmployeeCode;
        private XRTableCell xrTableCell11;
        private XRTableCell xrt_Age;
        private XRTableCell xrt_Sex;
        private XRTableCell xrTableCell13;
        private XRTableCell xrTableCell18;
        private XRTableCell xrt_Folk;
        private XRTableCell xrt_CurrentAddress;
        private XRTableCell xrTableCell23;
        private XRTableCell xrTableCell1;
        private XRTableCell xrt_RecruimentDate;
        private XRTableCell xrTableCell4;
        private XRTableCell xrt_Politic;
        private XRTableCell xrTableCell9;
        private XRTableCell xrt_JobTitle;
        private XRTableCell xrTableCell8;
        private XRTableCell xrt_Position;
        private XRTableCell xrt_AssignJob;
        private XRTableCell xrTableCell22;
        private XRTableCell xrTableCell12;
        private XRTableCell xrt_Department;
        private XRTableCell xrt_ContractType;
        private XRTableCell xrt_Seniority;
        private XRTableCell xrt_Status;
        private XRTableCell xrt_Email;
        private XRTableCell xrt_Phone;
        private XRTableCell xrt_OriginRecord;
        private XRTableCell xrTableCell37;
        private XRTableCell xrTableCell35;
        private XRTableCell xrTableCell33;
        private XRTableCell xrTableCell31;
        private XRTableCell xrTableCell29;
        private XRTableCell xrTableCell27;
        private XRTableCell xrTableCell25;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
        private IContainer components = null;
        #region Init

        /// <summary>
        /// Filter data
        /// </summary>
        private Filter _filter;

        /// <summary>
        /// Get Filter, implement IBaseReport interface
        /// </summary>
        /// <returns></returns>
        public Filter GetFilter()
        {
            return _filter;
        }

        /// <summary>
        /// Set Filter, implement IBaseReport interface
        /// </summary>
        /// <param name="filter"></param>
        public void SetFilter(Filter filter)
        {
            _filter = filter;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitFilter()
        {
            // init filter
            _filter = new Filter
            {
                Items = new List<FilterItem>()
            };
          
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public rp_PrintCurriculumVitae()
        {
            // init compoent
            InitializeComponent();

            // init Filter
            InitFilter();
        }

        #endregion
        int STT = 1;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrt_STT.Text = STT.ToString();
            STT++;
        }
        public void BindData()
        {
            try
            {
                // get organization
                var organization = cat_DepartmentServices.GetByDepartments(_filter.Departments);
                if (organization != null)
                {
                    // select form db               
                    var departments = _filter.Departments;
                    var arrDepartment = departments.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (var i = 0; i < arrDepartment.Length; i++)
                    {
                        arrDepartment[i] = "{0}".FormatWith(arrDepartment[i]);
                    }

                    //list recordId
                    var recordIds = _filter.RecordIds;
                    var arrRecordId = recordIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (var i = 0; i < arrRecordId.Length; i++)
                    {
                        arrRecordId[i] = "{0}".FormatWith(arrRecordId[i]);
                    }

                    // from date
                    var fromDate = _filter.StartDate != null ? _filter.StartDate.Value.ToString("yyyy-MM-dd 00:00:00") : string.Empty;
                    // to date
                    var toDate = _filter.EndDate != null ? _filter.EndDate.Value.ToString("yyyy-MM-dd 23:59:59") : string.Empty;
                    var table = SQLHelper.ExecuteTable(SQLBusinessAdapter.GetStore_PrintCurriculumVitae(string.Join(",", arrDepartment), fromDate, toDate, string.Join(",", arrRecordId)));

                    DataSource = table;
                    //binding data
                    xrt_EmployeeCode.DataBindings.Add("Text", DataSource, "EmployeeCode");
                    xrt_FullName.DataBindings.Add("Text", DataSource, "FullName");
                    xrt_BirthDate.DataBindings.Add("Text", DataSource, "BirthDate", "{0:dd/MM/yyyy}");
                    xrt_Age.DataBindings.Add("Text", DataSource, "Age");
                    xrt_Sex.DataBindings.Add("Text", DataSource, "SexName");
                    xrt_Folk.DataBindings.Add("Text", DataSource, "FolkName");
                    xrt_CurrentAddress.DataBindings.Add("Text", DataSource, "Address");
                    xrt_RecruimentDate.DataBindings.Add("Text", DataSource, "RecruimentDate", "{0:dd/MM/yyyy}");
                    xrt_QuantumName.DataBindings.Add("Text", DataSource, "QuantumName");
                    xrt_QuantumCode.DataBindings.Add("Text", DataSource, "QuantumCode");
                    xrt_QuantumDegree.DataBindings.Add("Text", DataSource, "SalaryGrade");
                    xrt_Education.DataBindings.Add("Text", DataSource, "EducationName");
                    xrt_Politic.DataBindings.Add("Text", DataSource, "PoliticLevelName");
                    xrt_JobTitle.DataBindings.Add("Text", DataSource, "JobTitleName");
                    xrt_Position.DataBindings.Add("Text", DataSource, "PositionName");
                    xrt_AssignJob.DataBindings.Add("Text", DataSource, "AssignedWork");
                    xrt_Department.DataBindings.Add("Text", DataSource, "DepartmentName");
                    xrt_ContractType.DataBindings.Add("Text", DataSource, "ContractTypeName");
                    xrt_Seniority.DataBindings.Add("Text", DataSource, "Seniority");
                    xrt_Status.DataBindings.Add("Text", DataSource, "WorkStatusName");
                    xrt_Email.DataBindings.Add("Text", DataSource, "WorkEmail");
                    xrt_Phone.DataBindings.Add("Text", DataSource, "CellPhoneNumber");
                    xrt_OriginRecord.DataBindings.Add("Text", DataSource, "OriginalFile");
                }
            }
            catch(Exception ex)
            {
                Dialog.ShowError(ex);
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
            string resourceFileName = "rp_PrintCurriculumVitae.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.tblDetail = new DevExpress.XtraReports.UI.XRTable();
            this.xrDetailRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrt_STT = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_EmployeeCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_FullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_BirthDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_Age = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_Sex = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_Folk = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_CurrentAddress = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_RecruimentDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_QuantumName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_QuantumCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_QuantumDegree = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_Education = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_Politic = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_JobTitle = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_Position = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_AssignJob = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_Department = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_ContractType = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_Seniority = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_Status = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_Email = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_Phone = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_OriginRecord = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.tblPageHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell241 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell242 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblDetail});
            this.Detail.HeightF = 30F;
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
            this.tblDetail.SizeF = new System.Drawing.SizeF(1159F, 30F);
            this.tblDetail.StylePriority.UseBorders = false;
            // 
            // xrDetailRow1
            // 
            this.xrDetailRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrt_STT,
            this.xrt_EmployeeCode,
            this.xrt_FullName,
            this.xrt_BirthDate,
            this.xrt_Age,
            this.xrt_Sex,
            this.xrt_Folk,
            this.xrt_CurrentAddress,
            this.xrt_RecruimentDate,
            this.xrt_QuantumName,
            this.xrt_QuantumCode,
            this.xrt_QuantumDegree,
            this.xrt_Education,
            this.xrt_Politic,
            this.xrt_JobTitle,
            this.xrt_Position,
            this.xrt_AssignJob,
            this.xrt_Department,
            this.xrt_ContractType,
            this.xrt_Seniority,
            this.xrt_Status,
            this.xrt_Email,
            this.xrt_Phone,
            this.xrt_OriginRecord});
            this.xrDetailRow1.Name = "xrDetailRow1";
            this.xrDetailRow1.Weight = 1D;
            // 
            // xrt_STT
            // 
            this.xrt_STT.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_STT.Name = "xrt_STT";
            this.xrt_STT.StylePriority.UseBorders = false;
            this.xrt_STT.StylePriority.UseFont = false;
            this.xrt_STT.StylePriority.UseTextAlignment = false;
            this.xrt_STT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_STT.Weight = 0.11151197798657776D;
            this.xrt_STT.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrt_EmployeeCode
            // 
            this.xrt_EmployeeCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_EmployeeCode.Name = "xrt_EmployeeCode";
            this.xrt_EmployeeCode.StylePriority.UseBorders = false;
            this.xrt_EmployeeCode.StylePriority.UseTextAlignment = false;
            this.xrt_EmployeeCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_EmployeeCode.Weight = 0.22534724782128984D;
            // 
            // xrt_FullName
            // 
            this.xrt_FullName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_FullName.Name = "xrt_FullName";
            this.xrt_FullName.StylePriority.UseBorders = false;
            this.xrt_FullName.StylePriority.UseTextAlignment = false;
            this.xrt_FullName.Text = " ";
            this.xrt_FullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_FullName.Weight = 0.44930025194228479D;
            // 
            // xrt_BirthDate
            // 
            this.xrt_BirthDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_BirthDate.Name = "xrt_BirthDate";
            this.xrt_BirthDate.StylePriority.UseBorders = false;
            this.xrt_BirthDate.StylePriority.UseTextAlignment = false;
            this.xrt_BirthDate.Text = " ";
            this.xrt_BirthDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_BirthDate.Weight = 0.22859957594985142D;
            // 
            // xrt_Age
            // 
            this.xrt_Age.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_Age.Name = "xrt_Age";
            this.xrt_Age.StylePriority.UseBorders = false;
            this.xrt_Age.StylePriority.UseTextAlignment = false;
            this.xrt_Age.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_Age.Weight = 0.14496558032128024D;
            // 
            // xrt_Sex
            // 
            this.xrt_Sex.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_Sex.Name = "xrt_Sex";
            this.xrt_Sex.StylePriority.UseBorders = false;
            this.xrt_Sex.StylePriority.UseTextAlignment = false;
            this.xrt_Sex.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_Sex.Weight = 0.15054117592931143D;
            // 
            // xrt_Folk
            // 
            this.xrt_Folk.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_Folk.Name = "xrt_Folk";
            this.xrt_Folk.StylePriority.UseBorders = false;
            this.xrt_Folk.StylePriority.UseTextAlignment = false;
            this.xrt_Folk.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_Folk.Weight = 0.21744835820261838D;
            // 
            // xrt_CurrentAddress
            // 
            this.xrt_CurrentAddress.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_CurrentAddress.Name = "xrt_CurrentAddress";
            this.xrt_CurrentAddress.StylePriority.UseBorders = false;
            this.xrt_CurrentAddress.StylePriority.UseTextAlignment = false;
            this.xrt_CurrentAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_CurrentAddress.Weight = 0.36798952889238057D;
            // 
            // xrt_RecruimentDate
            // 
            this.xrt_RecruimentDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_RecruimentDate.Name = "xrt_RecruimentDate";
            this.xrt_RecruimentDate.StylePriority.UseBorders = false;
            this.xrt_RecruimentDate.StylePriority.UseTextAlignment = false;
            this.xrt_RecruimentDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_RecruimentDate.Weight = 0.21187277321592724D;
            // 
            // xrt_QuantumName
            // 
            this.xrt_QuantumName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_QuantumName.Name = "xrt_QuantumName";
            this.xrt_QuantumName.StylePriority.UseBorders = false;
            this.xrt_QuantumName.StylePriority.UseTextAlignment = false;
            this.xrt_QuantumName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_QuantumName.Weight = 0.35126275670675783D;
            // 
            // xrt_QuantumCode
            // 
            this.xrt_QuantumCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_QuantumCode.Name = "xrt_QuantumCode";
            this.xrt_QuantumCode.StylePriority.UseBorders = false;
            this.xrt_QuantumCode.StylePriority.UseTextAlignment = false;
            this.xrt_QuantumCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_QuantumCode.Weight = 0.26762876557608589D;
            // 
            // xrt_QuantumDegree
            // 
            this.xrt_QuantumDegree.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_QuantumDegree.Name = "xrt_QuantumDegree";
            this.xrt_QuantumDegree.StylePriority.UseBorders = false;
            this.xrt_QuantumDegree.StylePriority.UseTextAlignment = false;
            this.xrt_QuantumDegree.Text = " ";
            this.xrt_QuantumDegree.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_QuantumDegree.Weight = 0.178419167732407D;
            // 
            // xrt_Education
            // 
            this.xrt_Education.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_Education.Name = "xrt_Education";
            this.xrt_Education.StylePriority.UseBorders = false;
            this.xrt_Education.StylePriority.UseTextAlignment = false;
            this.xrt_Education.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_Education.Weight = 0.27877996532634841D;
            // 
            // xrt_Politic
            // 
            this.xrt_Politic.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_Politic.Name = "xrt_Politic";
            this.xrt_Politic.StylePriority.UseBorders = false;
            this.xrt_Politic.StylePriority.UseTextAlignment = false;
            this.xrt_Politic.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_Politic.Weight = 0.26205316252119837D;
            // 
            // xrt_JobTitle
            // 
            this.xrt_JobTitle.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_JobTitle.Name = "xrt_JobTitle";
            this.xrt_JobTitle.StylePriority.UseBorders = false;
            this.xrt_JobTitle.StylePriority.UseTextAlignment = false;
            this.xrt_JobTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_JobTitle.Weight = 0.27320436083584931D;
            // 
            // xrt_Position
            // 
            this.xrt_Position.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_Position.Name = "xrt_Position";
            this.xrt_Position.StylePriority.UseBorders = false;
            this.xrt_Position.StylePriority.UseTextAlignment = false;
            this.xrt_Position.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_Position.Weight = 0.28435555915050026D;
            // 
            // xrt_AssignJob
            // 
            this.xrt_AssignJob.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_AssignJob.Name = "xrt_AssignJob";
            this.xrt_AssignJob.StylePriority.UseBorders = false;
            this.xrt_AssignJob.StylePriority.UseTextAlignment = false;
            this.xrt_AssignJob.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_AssignJob.Weight = 0.2732043587965613D;
            // 
            // xrt_Department
            // 
            this.xrt_Department.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_Department.Name = "xrt_Department";
            this.xrt_Department.StylePriority.UseBorders = false;
            this.xrt_Department.StylePriority.UseTextAlignment = false;
            this.xrt_Department.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_Department.Weight = 0.33453594952714166D;
            // 
            // xrt_ContractType
            // 
            this.xrt_ContractType.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_ContractType.Name = "xrt_ContractType";
            this.xrt_ContractType.StylePriority.UseBorders = false;
            this.xrt_ContractType.StylePriority.UseTextAlignment = false;
            this.xrt_ContractType.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_ContractType.Weight = 0.27877995999317479D;
            // 
            // xrt_Seniority
            // 
            this.xrt_Seniority.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_Seniority.Name = "xrt_Seniority";
            this.xrt_Seniority.StylePriority.UseBorders = false;
            this.xrt_Seniority.StylePriority.UseTextAlignment = false;
            this.xrt_Seniority.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_Seniority.Weight = 0.34011154868446719D;
            // 
            // xrt_Status
            // 
            this.xrt_Status.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_Status.Name = "xrt_Status";
            this.xrt_Status.StylePriority.UseBorders = false;
            this.xrt_Status.StylePriority.UseTextAlignment = false;
            this.xrt_Status.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_Status.Weight = 0.27877995999317479D;
            // 
            // xrt_Email
            // 
            this.xrt_Email.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_Email.Name = "xrt_Email";
            this.xrt_Email.StylePriority.UseBorders = false;
            this.xrt_Email.StylePriority.UseTextAlignment = false;
            this.xrt_Email.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_Email.Weight = 0.39029194110039661D;
            // 
            // xrt_Phone
            // 
            this.xrt_Phone.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_Phone.Name = "xrt_Phone";
            this.xrt_Phone.StylePriority.UseBorders = false;
            this.xrt_Phone.StylePriority.UseTextAlignment = false;
            this.xrt_Phone.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_Phone.Weight = 0.33453594748785365D;
            // 
            // xrt_OriginRecord
            // 
            this.xrt_OriginRecord.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrt_OriginRecord.Name = "xrt_OriginRecord";
            this.xrt_OriginRecord.StylePriority.UseBorders = false;
            this.xrt_OriginRecord.StylePriority.UseTextAlignment = false;
            this.xrt_OriginRecord.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_OriginRecord.Weight = 0.22859954964680598D;
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
            this.xrTable4});
            this.ReportHeader.HeightF = 106F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrTable4
            // 
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow4,
            this.xrTableRow7,
            this.xrTableRow8});
            this.xrTable4.SizeF = new System.Drawing.SizeF(1159F, 106F);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10,
            this.xrTableCell14});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 0.96D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Weight = 0.537853321276213D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.Weight = 2.4621466787237871D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell16,
            this.xrTableCell17});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 0.96D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.Weight = 0.53785336143092111D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.Weight = 2.4621466385690791D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 0.96000000000000008D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseFont = false;
            this.xrTableCell20.Text = "MẪU TRÍCH NGANG HỒ SƠ CÁN BỘ, CÔNG CHỨC";
            this.xrTableCell20.Weight = 3D;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell19});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 0.96000000000000008D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.Weight = 3D;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell21});
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 0.4D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.Weight = 3D;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblPageHeader});
            this.PageHeader.HeightF = 40F;
            this.PageHeader.Name = "PageHeader";
            // 
            // tblPageHeader
            // 
            this.tblPageHeader.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tblPageHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tblPageHeader.Name = "tblPageHeader";
            this.tblPageHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
            this.tblPageHeader.SizeF = new System.Drawing.SizeF(1159F, 40F);
            this.tblPageHeader.StylePriority.UseBorders = false;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell241,
            this.xrTableCell11,
            this.xrTableCell242,
            this.xrTableCell2,
            this.xrTableCell13,
            this.xrTableCell18,
            this.xrTableCell23,
            this.xrTableCell1,
            this.xrTableCell4,
            this.xrTableCell3,
            this.xrTableCell5,
            this.xrTableCell7,
            this.xrTableCell6,
            this.xrTableCell9,
            this.xrTableCell8,
            this.xrTableCell22,
            this.xrTableCell12,
            this.xrTableCell37,
            this.xrTableCell35,
            this.xrTableCell33,
            this.xrTableCell31,
            this.xrTableCell29,
            this.xrTableCell27,
            this.xrTableCell25});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 9.2D;
            // 
            // xrTableCell241
            // 
            this.xrTableCell241.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell241.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell241.Font = new System.Drawing.Font("Times New Roman", 5.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell241.Name = "xrTableCell241";
            this.xrTableCell241.StylePriority.UseBorderColor = false;
            this.xrTableCell241.StylePriority.UseBorders = false;
            this.xrTableCell241.StylePriority.UseFont = false;
            this.xrTableCell241.Text = "Stt";
            this.xrTableCell241.Weight = 0.17698495526153751D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell11.Font = new System.Drawing.Font("Times New Roman", 5.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBorderColor = false;
            this.xrTableCell11.StylePriority.UseBorders = false;
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.Text = "Số hiệu CBCC";
            this.xrTableCell11.Weight = 0.35765714520978259D;
            // 
            // xrTableCell242
            // 
            this.xrTableCell242.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell242.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell242.Font = new System.Drawing.Font("Times New Roman", 5.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell242.Name = "xrTableCell242";
            this.xrTableCell242.StylePriority.UseBorderColor = false;
            this.xrTableCell242.StylePriority.UseBorders = false;
            this.xrTableCell242.StylePriority.UseFont = false;
            this.xrTableCell242.Text = "Họ và tên";
            this.xrTableCell242.Weight = 0.713101824848497D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.Font = new System.Drawing.Font("Times New Roman", 5.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorderColor = false;
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Text = "Ngày sinh";
            this.xrTableCell2.Weight = 0.36281916452821611D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell13.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell13.Font = new System.Drawing.Font("Times New Roman", 5.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseBorderColor = false;
            this.xrTableCell13.StylePriority.UseBorders = false;
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.Text = "Tuổi";
            this.xrTableCell13.Weight = 0.23008043462808397D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell18.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell18.Font = new System.Drawing.Font("Times New Roman", 5.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseBorderColor = false;
            this.xrTableCell18.StylePriority.UseBorders = false;
            this.xrTableCell18.StylePriority.UseFont = false;
            this.xrTableCell18.Text = "Giới tính";
            this.xrTableCell18.Weight = 0.23892968233107667D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell23.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell23.Font = new System.Drawing.Font("Times New Roman", 5.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StylePriority.UseBorderColor = false;
            this.xrTableCell23.StylePriority.UseBorders = false;
            this.xrTableCell23.StylePriority.UseFont = false;
            this.xrTableCell23.Text = "Dân tộc";
            this.xrTableCell23.Weight = 0.34512067509183514D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.Font = new System.Drawing.Font("Times New Roman", 5.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorderColor = false;
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "Chỗ ở hiện nay";
            this.xrTableCell1.Weight = 0.58405036987697245D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell4.Font = new System.Drawing.Font("Times New Roman", 5.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseBorderColor = false;
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.Text = "Ngày tuyển dụng";
            this.xrTableCell4.Weight = 0.3362714273888423D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.Font = new System.Drawing.Font("Times New Roman", 5.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorderColor = false;
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.Text = "Tên ngạch";
            this.xrTableCell3.Weight = 0.55750260716367772D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell5.Font = new System.Drawing.Font("Times New Roman", 5.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBorderColor = false;
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.Text = "Mã ngạch";
            this.xrTableCell5.Weight = 0.42476388364566925D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell7.Font = new System.Drawing.Font("Times New Roman", 5.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBorderColor = false;
            this.xrTableCell7.StylePriority.UseBorders = false;
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.Text = "Bậc ngạch";
            this.xrTableCell7.Weight = 0.28317593602788288D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell6.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseBorderColor = false;
            this.xrTableCell6.StylePriority.UseBorders = false;
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.Text = "Trình độ chuyên môn";
            this.xrTableCell6.Weight = 0.45131162291705534D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell9.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorderColor = false;
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.Text = "Lý luận chính trị";
            this.xrTableCell9.Weight = 0.41591463210508456D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell8.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseBorderColor = false;
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.Text = "Chức danh";
            this.xrTableCell8.Weight = 0.43361312751106984D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell22.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell22.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseBorderColor = false;
            this.xrTableCell22.StylePriority.UseBorders = false;
            this.xrTableCell22.StylePriority.UseFont = false;
            this.xrTableCell22.Text = "Chức vụ\t";
            this.xrTableCell22.Weight = 0.4513116229170554D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell12.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseBorderColor = false;
            this.xrTableCell12.StylePriority.UseBorders = false;
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.Text = "Công việc được giao";
            this.xrTableCell12.Weight = 0.43361312079489245D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell37.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell37.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.StylePriority.UseBorderColor = false;
            this.xrTableCell37.StylePriority.UseBorders = false;
            this.xrTableCell37.StylePriority.UseFont = false;
            this.xrTableCell37.Text = "Bộ phận";
            this.xrTableCell37.Weight = 0.53095484552781214D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell35.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell35.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.StylePriority.UseBorderColor = false;
            this.xrTableCell35.StylePriority.UseBorders = false;
            this.xrTableCell35.StylePriority.UseFont = false;
            this.xrTableCell35.Text = "Loại hợp đồng";
            this.xrTableCell35.Weight = 0.44246237521406268D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell33.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell33.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.StylePriority.UseBorderColor = false;
            this.xrTableCell33.StylePriority.UseBorders = false;
            this.xrTableCell33.StylePriority.UseFont = false;
            this.xrTableCell33.Text = "Thâm niên công tác";
            this.xrTableCell33.Weight = 0.5398040932308048D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell31.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell31.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.StylePriority.UseBorderColor = false;
            this.xrTableCell31.StylePriority.UseBorders = false;
            this.xrTableCell31.StylePriority.UseFont = false;
            this.xrTableCell31.Text = "Hiện trạng";
            this.xrTableCell31.Weight = 0.44246237521406256D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell29.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell29.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.StylePriority.UseBorderColor = false;
            this.xrTableCell29.StylePriority.UseBorders = false;
            this.xrTableCell29.StylePriority.UseFont = false;
            this.xrTableCell29.Text = "Email nội bộ";
            this.xrTableCell29.Weight = 0.6194473563149403D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell27.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell27.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.StylePriority.UseBorderColor = false;
            this.xrTableCell27.StylePriority.UseBorders = false;
            this.xrTableCell27.StylePriority.UseFont = false;
            this.xrTableCell27.Text = "Số điện thoại";
            this.xrTableCell27.Weight = 0.53091946487128594D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell25.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell25.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.StylePriority.UseBorderColor = false;
            this.xrTableCell25.StylePriority.UseBorders = false;
            this.xrTableCell25.StylePriority.UseFont = false;
            this.xrTableCell25.Text = "Hồ sơ gốc";
            this.xrTableCell25.Weight = 0.35400534514835263D;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // ReportFooter
            // 
            this.ReportFooter.HeightF = 252.1667F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // rp_PrintCurriculumVitae
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
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 15, 0);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}