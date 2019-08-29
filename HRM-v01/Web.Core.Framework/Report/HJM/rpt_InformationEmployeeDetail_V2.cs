using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Adapter;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Summary description for rpt_InformationEmployeeDetail_V2
    /// </summary>
    public class rpt_InformationEmployeeDetail_V2 : XtraReport
    {
        private DetailBand Detail;
        private TopMarginBand TopMargin;
        private BottomMarginBand BottomMargin;
        private XRLabel xrLabel5;
        private XRPictureBox xrPictureBox1;
        private XRLabel lbl_Alias;
        private XRLabel xrl_NGAY_SINH;
        private XRLabel xrl_HO_TEN;
        private XRLabel lbl_Birthday;
        private XRLabel xrl_Alias;
        private XRLabel xrl_Hometown;
        private XRLabel xrl_DependentUnits;
        private XRLabel xrl_DAN_TOC;
        private XRLabel xrl_Province;
        private XRLabel xrl_BaseUnit;
        private XRLabel xrl_CurrentCommittees;
        private XRLabel xrlHOTEN_CBCC;
        private XRLabel xrl_Residence;
        private XRLabel xrl_CellPhoneNumber;
        private XRLabel xrl_HighestEducation;
        private XRLabel xrl_ArmyJoinedDate;
        private XRLabel xrl_TRINHDO;
        private XRLabel lblNote;
        private XRLabel xrl_PreviousJob;
        private XRLabel xrl_TrinhDoPhoT;
        private XRLabel lbMaNgachCC;
        private XRLabel xrLabel4;
        private XRLabel xrl_NANGLUC;
        private XRLabel xrl_Skills;
        private XRLabel xrLabel3;
        private XRLabel xrl_RewardName;
        private XRLabel xrl_RelativesAboard;
        private XRLabel xrl_ForeignOrganizationJoined;
        private XRLabel xrLabel52;
        private XRLabel xrLabel54;
        private XRSubreport xrl_sub_FamilyHusband;
        private XRSubreport xrl_subProcessSalary;
        private XRLabel xrLabel115;
        private XRLabel xrLabel113;
        private XRLabel xrtngayketxuat;
        private XRLabel xrLabel30;
        private XRLabel xrLabel112;
        private XRLabel xrLabel1;
        private XRLabel xrLabel51;
        private XRLabel xrLabel28;
        private XRLabel xrLabel9;
        private sub_ProcessSalary sub_DienBienLuong1;
        private ReportHeaderBand ReportHeader;
        private XRLabel xrLabel55;
        private XRLabel xrLabel53;
        private XRLabel xrl_Sex;
        private XRLabel xrl_FullName;
        private XRLabel xrl_PositionName;
        private XRLabel xrLabel57;
        private XRLabel xrLabel56;
        private XRLabel xrl_Committees;
        private XRLabel xrLabel32;
        private XRLabel xrl_BirthPlace;
        private XRLabel xrl_Allowance;
        private XRLabel xrLabel59;
        private XRLabel xrLabel74;
        private XRLabel xrl_FamilyClassName;
        private XRLabel xrLabel8;
        private XRLabel xrl_Religiousness;
        private XRLabel xrLabel79;
        private XRLabel xrl_Folk;
        private XRLabel xrLabel77;
        private XRLabel xrl_ResidentPlace;
        private XRLabel xrLabel14;
        private XRLabel xrLabel2;
        private XRLabel xrLabel7;
        private XRLabel xrLabel75;
        private XRLabel xrLabel12;
        private XRLabel xrl_RecruimentDepartment;
        private XRLabel xrLabel82;
        private XRLabel xrLabel10;
        private XRLabel xrLabel13;
        private XRLabel xrl_CPVJoinedDate;
        private XRLabel xrLabel36;
        private XRLabel xrl_CPVOfficialJoinedDate;
        private XRLabel xrl_RecruimentDate;
        private XRLabel xrl_FunctionaryDate;
        private XRLabel xrLabel87;
        private XRLabel xrLabel15;
        private XRLabel xrl_PoliticDate;
        private XRLabel xrLabel91;
        private XRLabel xrl_ArmyLeftDate;
        private XRLabel xrLabel89;
        private XRLabel xrLabel38;
        private XRLabel xrLabel41;
        private XRLabel lbl_BasicEducation;
        private XRLabel xrl_BasicEducationName;
        private XRLabel xrLabel37;
        private XRLabel xrl_AssignedWork;
        private XRLabel xrLabel42;
        private XRLabel xrLabel102;
        private XRLabel xrLabel101;
        private XRLabel xrLabel100;
        private XRLabel xrLabel99;
        private XRLabel xrLabel98;
        private XRLabel xrLabel97;
        private XRLabel xrLabel96;
        private XRLabel xrLabel95;
        private XRLabel xrLabel94;
        private XRLabel xrLabel92;
        private XRLabel xrLabel16;
        private XRLabel xrLabel11;
        private XRLabel xrl_TitleAwarded;
        private XRLabel xrLabel104;
        private XRLabel xrLabel103;
        private XRLabel xrLabel40;
        private XRLabel xrLabel17;
        private XRLabel xrLabel114;
        private XRLabel xrLabel111;
        private XRLabel xrLabel110;
        private XRLabel xrLabel109;
        private XRLabel xrLabel107;
        private XRLabel xrLabel106;
        private XRLabel xrLabel108;
        private XRLabel xrLabel139;
        private XRLabel xrLabel131;
        private XRLabel xrLabel137;
        private XRLabel xrLabel138;
        private XRLabel xrLabel136;
        private XRLabel xrLabel125;
        private XRLabel xrLabel132;
        private XRLabel xrLabel133;
        private XRLabel xrLabel134;
        private XRLabel xrLabel135;
        private XRLabel xrLabel126;
        private XRLabel xrLabel127;
        private XRLabel xrLabel128;
        private XRLabel xrLabel129;
        private XRLabel xrLabel130;
        private XRLabel xrLabel124;
        private XRLabel xrLabel123;
        private XRLabel xrLabel122;
        private XRLabel xrLabel121;
        private XRLabel xrLabel120;
        private XRLabel xrLabel119;
        private XRLabel xrl_OtherIncome;
        private XRLabel xrLabel117;
        private XRLabel xrl_Salary;
        private XRLabel xrl_Birthday;
        private XRLabel xrl_ArmyLevelName;
        private XRLabel xrl_LanguageLevelName;
        private XRLabel xrLabel35;
        private XRLabel xrl_PoliticLevelName;
        private XRLabel xrLabel33;
        private XRLabel xrl_FamilyPolicyName;
        private XRLabel xrLabel44;
        private XRLabel xrl_RankWounded;
        private XRLabel xrLabel43;
        private XRLabel xrLabel18;
        private XRLabel xrl_DisciplineName;
        private XRLabel xrLabel6;
        private XRLabel xrl_IDNumber;
        private XRLabel xrLabel25;
        private XRLabel xrLabel23;
        private XRLabel xrl_Weight;
        private XRLabel xrl_BloodGroup;
        private XRLabel xrl_Height;
        private XRLabel xrl_CAN_NANG;
        private XRLabel xrl_CHIEU_CAO;
        private XRLabel xrl_NHOM_MAU;
        private XRLabel xrl_TT_SUCKHOE;
        private XRLabel xrl_SO_CMND;
        private XRSubreport xrl_sub_TrainingProcess;
        private XRSubreport xrl_sub_ProcessWorking;
        private XRLabel xrLabel20;
        private XRLabel xrLabel19;
        private XRLabel xrLabel24;
        private XRLabel xrLabel22;
        private XRLabel xrl_Biography;
        private XRSubreport xrl_sub_FamilyWife;
        private XRLabel xrLabel31;
        private XRLabel xrLabel29;
        private XRLabel xrLabel27;
        private XRLabel xrLabel26;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        public string MaDonVi { get; set; }
        public rpt_InformationEmployeeDetail_V2()
        {
            InitializeComponent();
        }

        public void BindData(RecordModel hs)
        {
            if (hs == null)
                return;
            string emptyData = "...";
            var util = SoftCore.Util.GetInstance();
            var subProcessTraining = new sub_ProcessTraining();
            //var subProcessWorking = new sub_ProcessWorking();
            //var subFamilyHusband = new sub_FamilyRelationship();
            var subProcessSalary = new sub_ProcessSalary();

            //subProcessTraining.BindData(hs.Id);
            //subProcessWorking.BindData(hs.Id);
            //subFamilyHusband.BindData(hs.Id, 1);
            //subFamilyWife.BindData(hs.Id, 0);
            //subProcessSalary.BindData(hs.Id);

            ((sub_FamilyRelationship)xrl_sub_FamilyHusband.ReportSource).BindData(hs.Id, 1);
            ((sub_FamilyRelationship)xrl_sub_FamilyWife.ReportSource).BindData(hs.Id, 0);
            ((sub_ProcessTraining)xrl_sub_TrainingProcess.ReportSource).BindData(hs.Id);
            ((sub_ProcessWorking)xrl_sub_ProcessWorking.ReportSource).BindData(hs.Id);
            ((sub_ProcessSalary)xrl_subProcessSalary.ReportSource).BindData(hs.Id);



            RewardModel kt = RewardController.GetAll(hs.Id).FirstOrDefault();
            SalaryModel hsl = SalaryController.GetAllOrderBy(hs.Id).FirstOrDefault();
            List<RecordModel> ds = new List<RecordModel>();
            ds.Add(hs);
            ReportController controller = new ReportController();
            //ReportFilter filter = new ReportFilter();
            var location = string.Empty;
            xrtngayketxuat.Text = string.Format(xrtngayketxuat.Text, location, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
            var table = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_CurriculumVitaeDetail(hs.Id));
            DataSource = table;

            xrl_FullName.DataBindings.Add("Text", DataSource, "FullName");
            xrl_Sex.Text = hs.SexName;
            xrl_Alias.Text = hs.Alias;
            xrl_PositionName.DataBindings.Add("Text", DataSource, "PositionName");
            xrl_Birthday.Text = hs.BirthDate;
            xrl_BirthPlace.Text = @"5) Nơi sinh: " + controller.GetVillageName(table.Rows[0]["BirthPlaceWardGroup"].ToString()) + @" " + table.Rows[0]["BirthPlaceWardName"] + @", " +
                                  controller.GetDistrictName(table.Rows[0]["BirthPlaceDistrictGroup"].ToString()) + @" " + table.Rows[0]["BirthPlaceDistrictName"] + @", " +
                                  controller.GetProvinceName(table.Rows[0]["BirthPlaceProvinceGroup"].ToString()) + @" " + table.Rows[0]["BirthPlaceProvinceName"] + @".";
            xrl_Hometown.Text = @"6) Quê quán: " + controller.GetVillageName(table.Rows[0]["HometownWardGroup"].ToString()) + @" " + table.Rows[0]["HometownWardName"] + @", " +
                                controller.GetDistrictName(table.Rows[0]["HometownDistrictGroup"].ToString()) + @" " + table.Rows[0]["HometownDistrictName"] + @", " +
                                controller.GetProvinceName(table.Rows[0]["HometownProvinceGroup"].ToString()) + @" " + table.Rows[0]["HometownProvinceName"] + @".";
            xrl_ResidentPlace.Text = @"Nơi đăng ký hộ khẩu thường trú:  " + hs.ResidentPlace;
            xrl_Residence.Text = hs.Address;
            xrl_CellPhoneNumber.Text = hs.CellPhoneNumber;
            xrl_Folk.DataBindings.Add("Text", DataSource, "FolkName");
            xrl_Religiousness.DataBindings.Add("Text", DataSource, "ReligionName");
            xrl_FamilyClassName.DataBindings.Add("Text", DataSource, "FamilyClassName");
            xrl_PreviousJob.DataBindings.Add("Text", DataSource, "PreviousJob");
            xrl_RecruimentDate.Text = !string.IsNullOrEmpty(hs.RecruimentDate) ? string.Format("{0:dd/MM/yyyy}", hs.RecruimentDate) : emptyData;
            xrl_RecruimentDepartment.Text = hs.RecruimentDepartment;            
            xrl_FunctionaryDate.Text = hs.FunctionaryDate;
            xrl_CPVJoinedDate.Text = !util.IsDateNull(hs.CPVJoinedDate) ? string.Format("{0:dd/MM/yyyy}", hs.CPVJoinedDate) : emptyData;
            xrl_CPVOfficialJoinedDate.Text = !util.IsDateNull(hs.CPVOfficialJoinedDate) ? string.Format("{0:dd/MM/yyyy}", hs.CPVOfficialJoinedDate) : emptyData;
            xrl_ArmyJoinedDate.Text = !string.IsNullOrEmpty(hs.ArmyJoinedDate) ? string.Format("{0:dd/MM/yyyy}", hs.ArmyJoinedDate) : emptyData;
            xrl_ArmyLeftDate.Text = !string.IsNullOrEmpty(hs.ArmyLeftDate) ? string.Format("{0:dd/MM/yyyy}", hs.ArmyLeftDate) : emptyData;
            xrl_ArmyLevelName.DataBindings.Add("Text", DataSource, "ArmyLevelName");
            xrl_BasicEducationName.DataBindings.Add("Text", DataSource, "BasicEducationName");
            xrl_HighestEducation.DataBindings.Add("Text", DataSource, "EducationName");
            xrl_PoliticLevelName.DataBindings.Add("Text", DataSource, "PoliticLevelName");
            xrl_LanguageLevelName.DataBindings.Add("Text", DataSource, "LanguageLevelName");
            xrl_AssignedWork.DataBindings.Add("Text", DataSource, "AssignedWork");
            xrl_TitleAwarded.DataBindings.Add("Text", DataSource, "TitleAwarded");
            xrl_RewardName.DataBindings.Add("Text", DataSource, "RewardName");
            xrl_DisciplineName.DataBindings.Add("Text", DataSource, "DisciplineName");
            xrl_DisciplineName.DataBindings.Add("Text", DataSource, "DisciplineName");    
            xrl_Weight.Text = hs.Weight + @"kg";
            xrl_Height.Text = hs.Height + @"cm";
            xrl_BloodGroup.DataBindings.Add("Text", DataSource, "BloodGroup");
            xrl_IDNumber.DataBindings.Add("Text", DataSource, "IDNumber");
            xrl_RankWounded.DataBindings.Add("Text", DataSource, "RankWounded");
            xrl_FamilyPolicyName.DataBindings.Add("Text", DataSource, "FamilyPolicyName");
            xrl_Skills.Text = hs.Skills;
            if (string.IsNullOrEmpty(table.Rows[0]["RewardDecisionDate"].ToString()))
            { xrl_RewardName.Text += table.Rows[0]["RewardName"].ToString(); }
            else
            {
                xrl_RewardName.Text += table.Rows[0]["RewardName"] + @"   Năm: " + table.Rows[0]["RewardDecisionDate"];
            }
            if (!string.IsNullOrEmpty(table.Rows[0]["DisciplineDecisionDate"].ToString()))
            { xrl_DisciplineName.Text += table.Rows[0]["DisciplineName"] + @"  Năm: " + table.Rows[0]["DisciplineDecisionDate"]; }
            else
            {
                xrl_DisciplineName.Text += table.Rows[0]["DisciplineName"].ToString();
            }
            xrl_ForeignOrganizationJoined.Text = hs.ForeignOrganizationJoined;
            xrl_RelativesAboard.Text = hs.RelativesAboard;
            xrl_PreviousJob.Text = hs.RecruimentDepartment;           
            xrl_HighestEducation.Text = hs.ManagementLevelName;
            xrl_Biography.Text = hs.Biography;
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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel51 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel115 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel113 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrtngayketxuat = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel112 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_RelativesAboard = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ForeignOrganizationJoined = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel52 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel54 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NANGLUC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_Skills = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_RewardName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_HighestEducation = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ArmyJoinedDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TRINHDO = new DevExpress.XtraReports.UI.XRLabel();
            this.lblNote = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_PreviousJob = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TrinhDoPhoT = new DevExpress.XtraReports.UI.XRLabel();
            this.lbMaNgachCC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.lbl_Alias = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NGAY_SINH = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_HO_TEN = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_Birthday = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_Alias = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_Hometown = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_DependentUnits = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_DAN_TOC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_Province = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_BaseUnit = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_CurrentCommittees = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlHOTEN_CBCC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_Residence = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_CellPhoneNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_sub_FamilyWife = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_Biography = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_LanguageLevelName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_PoliticLevelName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ArmyLevelName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_Birthday = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel139 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel131 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel137 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel138 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel136 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel125 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel132 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel133 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel134 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel135 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel126 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel127 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel128 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel129 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel130 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel124 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel123 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel122 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel121 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel120 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel119 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_OtherIncome = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel117 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_Salary = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel108 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel114 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel111 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel110 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel109 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel107 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel106 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_FamilyPolicyName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel44 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_RankWounded = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel43 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TitleAwarded = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel104 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel103 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel40 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel102 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel101 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel100 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel99 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel98 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel97 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel96 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel95 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel94 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel92 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_AssignedWork = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel42 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel41 = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_BasicEducation = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_BasicEducationName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel91 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ArmyLeftDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel89 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_PoliticDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_CPVJoinedDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_CPVOfficialJoinedDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_RecruimentDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_FunctionaryDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel87 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_RecruimentDepartment = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel82 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel75 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_FamilyClassName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_Religiousness = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel79 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_Folk = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel77 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ResidentPlace = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel74 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_BirthPlace = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_Allowance = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel59 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_PositionName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel57 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel56 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_Committees = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_Sex = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_FullName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel55 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel53 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_DisciplineName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_IDNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_Weight = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_BloodGroup = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_Height = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_CAN_NANG = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_CHIEU_CAO = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NHOM_MAU = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TT_SUCKHOE = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_SO_CMND = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_sub_TrainingProcess = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrl_sub_ProcessWorking = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrl_subProcessSalary = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrl_sub_FamilyHusband = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel51
            // 
            this.xrLabel51.Dpi = 254F;
            this.xrLabel51.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrLabel51.LocationFloat = new DevExpress.Utils.PointFloat(18.89161F, 4444.798F);
            this.xrLabel51.Name = "xrLabel51";
            this.xrLabel51.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel51.SizeF = new System.Drawing.SizeF(1640F, 58.41992F);
            this.xrLabel51.StylePriority.UseFont = false;
            this.xrLabel51.StylePriority.UseTextAlignment = false;
            this.xrLabel51.Text = "b) Về bên vợ (hoặc chồng): Cha, Mẹ, anh chị em ruột";
            this.xrLabel51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel28
            // 
            this.xrLabel28.Dpi = 254F;
            this.xrLabel28.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4235.286F);
            this.xrLabel28.Name = "xrLabel28";
            this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel28.SizeF = new System.Drawing.SizeF(1640F, 58.41992F);
            this.xrLabel28.StylePriority.UseFont = false;
            this.xrLabel28.StylePriority.UseTextAlignment = false;
            this.xrLabel28.Text = "a) Về bản thân: (Bố, Mẹ, Vợ (chồng), các con, anh chị em ruột";
            this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Dpi = 254F;
            this.xrLabel9.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4158.344F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(1640F, 58.41992F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "30) QUAN HỆ GIA ĐÌNH";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel115
            // 
            this.xrLabel115.Dpi = 254F;
            this.xrLabel115.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel115.LocationFloat = new DevExpress.Utils.PointFloat(866.3195F, 5727.909F);
            this.xrLabel115.Name = "xrLabel115";
            this.xrLabel115.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel115.SizeF = new System.Drawing.SizeF(768.5209F, 58.41992F);
            this.xrLabel115.StylePriority.UseFont = false;
            this.xrLabel115.StylePriority.UseTextAlignment = false;
            this.xrLabel115.Text = "(Ký tên, đóng dấu)";
            this.xrLabel115.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel113
            // 
            this.xrLabel113.Dpi = 254F;
            this.xrLabel113.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel113.LocationFloat = new DevExpress.Utils.PointFloat(801.0209F, 5534.48F);
            this.xrLabel113.Name = "xrLabel113";
            this.xrLabel113.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel113.SizeF = new System.Drawing.SizeF(768.5195F, 55.98584F);
            this.xrLabel113.StylePriority.UseFont = false;
            this.xrLabel113.StylePriority.UseTextAlignment = false;
            this.xrLabel113.Text = "Xác nhận của cơ quan quản lý";
            this.xrLabel113.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrtngayketxuat
            // 
            this.xrtngayketxuat.Dpi = 254F;
            this.xrtngayketxuat.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrtngayketxuat.LocationFloat = new DevExpress.Utils.PointFloat(924.0505F, 5431.292F);
            this.xrtngayketxuat.Name = "xrtngayketxuat";
            this.xrtngayketxuat.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrtngayketxuat.SizeF = new System.Drawing.SizeF(621.8975F, 58.41992F);
            this.xrtngayketxuat.StylePriority.UseFont = false;
            this.xrtngayketxuat.StylePriority.UseTextAlignment = false;
            this.xrtngayketxuat.Text = "..., Ngày {1} tháng {2} năm {3}";
            this.xrtngayketxuat.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel30
            // 
            this.xrLabel30.Dpi = 254F;
            this.xrLabel30.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(49.33924F, 5648.887F);
            this.xrLabel30.Multiline = true;
            this.xrLabel30.Name = "xrLabel30";
            this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel30.SizeF = new System.Drawing.SizeF(565.7455F, 166.8989F);
            this.xrLabel30.StylePriority.UseFont = false;
            this.xrLabel30.StylePriority.UseTextAlignment = false;
            this.xrLabel30.Text = "Tôi xin cam đoan những lời khai trên đây là đúng sự thật\r\n(Ký tên)";
            this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel112
            // 
            this.xrLabel112.Dpi = 254F;
            this.xrLabel112.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel112.LocationFloat = new DevExpress.Utils.PointFloat(48.64876F, 5532.045F);
            this.xrLabel112.Name = "xrLabel112";
            this.xrLabel112.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel112.SizeF = new System.Drawing.SizeF(570.4418F, 58.42F);
            this.xrLabel112.StylePriority.UseFont = false;
            this.xrLabel112.StylePriority.UseTextAlignment = false;
            this.xrLabel112.Text = "Người khai";
            this.xrLabel112.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0.009366353F, 4842.336F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(15, 5, 0, 0, 254F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(1084.092F, 58.41992F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UsePadding = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Nguồn thu nhập chính của gia đình ( hàng năm): + lương";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_RelativesAboard
            // 
            this.xrl_RelativesAboard.Dpi = 254F;
            this.xrl_RelativesAboard.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_RelativesAboard.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4052.462F);
            this.xrl_RelativesAboard.Name = "xrl_RelativesAboard";
            this.xrl_RelativesAboard.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_RelativesAboard.SizeF = new System.Drawing.SizeF(1689.999F, 58.42F);
            this.xrl_RelativesAboard.StylePriority.UseFont = false;
            this.xrl_RelativesAboard.StylePriority.UseTextAlignment = false;
            this.xrl_RelativesAboard.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrl_ForeignOrganizationJoined
            // 
            this.xrl_ForeignOrganizationJoined.Dpi = 254F;
            this.xrl_ForeignOrganizationJoined.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_ForeignOrganizationJoined.LocationFloat = new DevExpress.Utils.PointFloat(0F, 3907.245F);
            this.xrl_ForeignOrganizationJoined.Name = "xrl_ForeignOrganizationJoined";
            this.xrl_ForeignOrganizationJoined.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_ForeignOrganizationJoined.SizeF = new System.Drawing.SizeF(1690F, 58.42F);
            this.xrl_ForeignOrganizationJoined.StylePriority.UseFont = false;
            this.xrl_ForeignOrganizationJoined.StylePriority.UseTextAlignment = false;
            this.xrl_ForeignOrganizationJoined.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel52
            // 
            this.xrLabel52.Dpi = 254F;
            this.xrLabel52.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel52.LocationFloat = new DevExpress.Utils.PointFloat(0.0009689331F, 3761.513F);
            this.xrLabel52.Name = "xrLabel52";
            this.xrLabel52.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel52.SizeF = new System.Drawing.SizeF(1689.992F, 76.94067F);
            this.xrLabel52.StylePriority.UseFont = false;
            this.xrLabel52.StylePriority.UseTextAlignment = false;
            this.xrLabel52.Text = " -Tham gia hoặc có quan hệ với các tổ chức chính trị, kinh tế, xã hội nào ở nước " +
    "ngoài";
            this.xrLabel52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel54
            // 
            this.xrLabel54.Dpi = 254F;
            this.xrLabel54.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel54.LocationFloat = new DevExpress.Utils.PointFloat(0F, 3969.665F);
            this.xrLabel54.Name = "xrLabel54";
            this.xrLabel54.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel54.SizeF = new System.Drawing.SizeF(339.6395F, 72.60913F);
            this.xrLabel54.StylePriority.UseFont = false;
            this.xrLabel54.StylePriority.UseTextAlignment = false;
            this.xrLabel54.Text = "- Có thân nhân";
            this.xrLabel54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrl_NANGLUC
            // 
            this.xrl_NANGLUC.Dpi = 254F;
            this.xrl_NANGLUC.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NANGLUC.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2504.079F);
            this.xrl_NANGLUC.Name = "xrl_NANGLUC";
            this.xrl_NANGLUC.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NANGLUC.SizeF = new System.Drawing.SizeF(446F, 58.42041F);
            this.xrl_NANGLUC.StylePriority.UseFont = false;
            this.xrl_NANGLUC.StylePriority.UseTextAlignment = false;
            this.xrl_NANGLUC.Text = "21) Sở trường công tác:   ";
            this.xrl_NANGLUC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_Skills
            // 
            this.xrl_Skills.Dpi = 254F;
            this.xrl_Skills.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_Skills.LocationFloat = new DevExpress.Utils.PointFloat(445.9999F, 2504.079F);
            this.xrl_Skills.Name = "xrl_Skills";
            this.xrl_Skills.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_Skills.SizeF = new System.Drawing.SizeF(312.0639F, 58.41968F);
            this.xrl_Skills.StylePriority.UseFont = false;
            this.xrl_Skills.StylePriority.UseTextAlignment = false;
            this.xrl_Skills.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Dpi = 254F;
            this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2594.791F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(339.6393F, 58.42041F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "22) Khen thưởng:    ";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_RewardName
            // 
            this.xrl_RewardName.Dpi = 254F;
            this.xrl_RewardName.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_RewardName.LocationFloat = new DevExpress.Utils.PointFloat(358.8941F, 2594.791F);
            this.xrl_RewardName.Name = "xrl_RewardName";
            this.xrl_RewardName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_RewardName.SizeF = new System.Drawing.SizeF(1306.106F, 58.42065F);
            this.xrl_RewardName.StylePriority.UseFont = false;
            this.xrl_RewardName.StylePriority.UseTextAlignment = false;
            this.xrl_RewardName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_HighestEducation
            // 
            this.xrl_HighestEducation.Dpi = 254F;
            this.xrl_HighestEducation.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_HighestEducation.LocationFloat = new DevExpress.Utils.PointFloat(1376.068F, 1936.327F);
            this.xrl_HighestEducation.Name = "xrl_HighestEducation";
            this.xrl_HighestEducation.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_HighestEducation.SizeF = new System.Drawing.SizeF(301.0497F, 58.42029F);
            this.xrl_HighestEducation.StylePriority.UseFont = false;
            this.xrl_HighestEducation.StylePriority.UseTextAlignment = false;
            this.xrl_HighestEducation.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_ArmyJoinedDate
            // 
            this.xrl_ArmyJoinedDate.Dpi = 254F;
            this.xrl_ArmyJoinedDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_ArmyJoinedDate.LocationFloat = new DevExpress.Utils.PointFloat(324.4653F, 1841.459F);
            this.xrl_ArmyJoinedDate.Name = "xrl_ArmyJoinedDate";
            this.xrl_ArmyJoinedDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_ArmyJoinedDate.SizeF = new System.Drawing.SizeF(194.1567F, 58.4198F);
            this.xrl_ArmyJoinedDate.StylePriority.UseFont = false;
            this.xrl_ArmyJoinedDate.StylePriority.UseTextAlignment = false;
            this.xrl_ArmyJoinedDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_TRINHDO
            // 
            this.xrl_TRINHDO.Dpi = 254F;
            this.xrl_TRINHDO.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_TRINHDO.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1841.459F);
            this.xrl_TRINHDO.Name = "xrl_TRINHDO";
            this.xrl_TRINHDO.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_TRINHDO.SizeF = new System.Drawing.SizeF(312.1042F, 58.41992F);
            this.xrl_TRINHDO.StylePriority.UseFont = false;
            this.xrl_TRINHDO.StylePriority.UseTextAlignment = false;
            this.xrl_TRINHDO.Text = "16) Ngày nhập ngũ";
            this.xrl_TRINHDO.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblNote
            // 
            this.lblNote.Dpi = 254F;
            this.lblNote.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.LocationFloat = new DevExpress.Utils.PointFloat(48.64884F, 1216.425F);
            this.lblNote.Name = "lblNote";
            this.lblNote.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblNote.SizeF = new System.Drawing.SizeF(1457.447F, 45.1908F);
            this.lblNote.StylePriority.UseFont = false;
            this.lblNote.StylePriority.UseTextAlignment = false;
            this.lblNote.Text = "(ghi là công nhân, nông dân, cán bộ, công chức, trí thức, quân nhân, dân nghèo th" +
    "ành thị, tiểu thương, tiểu chủ, tư sản....)";
            this.lblNote.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_PreviousJob
            // 
            this.xrl_PreviousJob.Dpi = 254F;
            this.xrl_PreviousJob.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_PreviousJob.LocationFloat = new DevExpress.Utils.PointFloat(1068.104F, 1292.409F);
            this.xrl_PreviousJob.Name = "xrl_PreviousJob";
            this.xrl_PreviousJob.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_PreviousJob.SizeF = new System.Drawing.SizeF(621.8947F, 58.42029F);
            this.xrl_PreviousJob.StylePriority.UseFont = false;
            this.xrl_PreviousJob.StylePriority.UseTextAlignment = false;
            this.xrl_PreviousJob.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_TrinhDoPhoT
            // 
            this.xrl_TrinhDoPhoT.Dpi = 254F;
            this.xrl_TrinhDoPhoT.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_TrinhDoPhoT.LocationFloat = new DevExpress.Utils.PointFloat(480.0001F, 1754.217F);
            this.xrl_TrinhDoPhoT.Name = "xrl_TrinhDoPhoT";
            this.xrl_TrinhDoPhoT.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_TrinhDoPhoT.SizeF = new System.Drawing.SizeF(633.5844F, 58.42004F);
            this.xrl_TrinhDoPhoT.StylePriority.UseFont = false;
            this.xrl_TrinhDoPhoT.StylePriority.UseTextAlignment = false;
            this.xrl_TrinhDoPhoT.Text = "(Ngày vào Đoàn TNCSHCM, Công đoàn, Hội .........  ) ";
            this.xrl_TrinhDoPhoT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbMaNgachCC
            // 
            this.lbMaNgachCC.Dpi = 254F;
            this.lbMaNgachCC.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMaNgachCC.LocationFloat = new DevExpress.Utils.PointFloat(1399.336F, 1520.931F);
            this.lbMaNgachCC.Name = "lbMaNgachCC";
            this.lbMaNgachCC.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbMaNgachCC.SizeF = new System.Drawing.SizeF(290.6648F, 58.42004F);
            this.lbMaNgachCC.StylePriority.UseFont = false;
            this.lbMaNgachCC.StylePriority.UseTextAlignment = false;
            this.lbMaNgachCC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Dpi = 254F;
            this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1521.931F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(668.0014F, 58.42004F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "13) Ngày vào cơ quan hiện đang công tác:";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel5.Dpi = 254F;
            this.xrLabel5.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(492.0001F, 101.6876F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(615.3113F, 71.86069F);
            this.xrLabel5.StylePriority.UseBorders = false;
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "SƠ YẾU LÝ LỊCH ";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPictureBox1.Dpi = 254F;
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 295.3321F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(278.8706F, 380.3652F);
            this.xrPictureBox1.StylePriority.UseBorders = false;
            // 
            // lbl_Alias
            // 
            this.lbl_Alias.Dpi = 254F;
            this.lbl_Alias.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Alias.LocationFloat = new DevExpress.Utils.PointFloat(301.4293F, 380.0453F);
            this.lbl_Alias.Name = "lbl_Alias";
            this.lbl_Alias.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbl_Alias.SizeF = new System.Drawing.SizeF(406.8415F, 58.4201F);
            this.lbl_Alias.StylePriority.UseFont = false;
            this.lbl_Alias.StylePriority.UseTextAlignment = false;
            this.lbl_Alias.Text = "2) Các tên gọi khác: ";
            this.lbl_Alias.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NGAY_SINH
            // 
            this.xrl_NGAY_SINH.Dpi = 254F;
            this.xrl_NGAY_SINH.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NGAY_SINH.LocationFloat = new DevExpress.Utils.PointFloat(301.4296F, 457.0884F);
            this.xrl_NGAY_SINH.Name = "xrl_NGAY_SINH";
            this.xrl_NGAY_SINH.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NGAY_SINH.SizeF = new System.Drawing.SizeF(356.5706F, 58.42004F);
            this.xrl_NGAY_SINH.StylePriority.UseFont = false;
            this.xrl_NGAY_SINH.StylePriority.UseTextAlignment = false;
            this.xrl_NGAY_SINH.Text = "3) Cấp ủy hiện tại:  ";
            this.xrl_NGAY_SINH.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_HO_TEN
            // 
            this.xrl_HO_TEN.Dpi = 254F;
            this.xrl_HO_TEN.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_HO_TEN.LocationFloat = new DevExpress.Utils.PointFloat(301.4293F, 302.8338F);
            this.xrl_HO_TEN.Name = "xrl_HO_TEN";
            this.xrl_HO_TEN.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_HO_TEN.SizeF = new System.Drawing.SizeF(366.5721F, 58.4198F);
            this.xrl_HO_TEN.StylePriority.UseFont = false;
            this.xrl_HO_TEN.StylePriority.UseTextAlignment = false;
            this.xrl_HO_TEN.Text = "1) Họ và tên khai sinh: ";
            this.xrl_HO_TEN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl_Birthday
            // 
            this.lbl_Birthday.Dpi = 254F;
            this.lbl_Birthday.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Birthday.LocationFloat = new DevExpress.Utils.PointFloat(2.837036F, 679.3444F);
            this.lbl_Birthday.Name = "lbl_Birthday";
            this.lbl_Birthday.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbl_Birthday.SizeF = new System.Drawing.SizeF(239.9806F, 58.41992F);
            this.lbl_Birthday.StylePriority.UseFont = false;
            this.lbl_Birthday.StylePriority.UseTextAlignment = false;
            this.lbl_Birthday.Text = "4) Sinh ngày";
            this.lbl_Birthday.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lbl_Birthday.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrl_NoiSinh_BeforePrint);
            // 
            // xrl_Alias
            // 
            this.xrl_Alias.Dpi = 254F;
            this.xrl_Alias.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_Alias.LocationFloat = new DevExpress.Utils.PointFloat(708.2708F, 380.0456F);
            this.xrl_Alias.Name = "xrl_Alias";
            this.xrl_Alias.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_Alias.SizeF = new System.Drawing.SizeF(970F, 58.42004F);
            this.xrl_Alias.StylePriority.UseFont = false;
            this.xrl_Alias.StylePriority.UseTextAlignment = false;
            this.xrl_Alias.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_Hometown
            // 
            this.xrl_Hometown.Dpi = 254F;
            this.xrl_Hometown.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_Hometown.LocationFloat = new DevExpress.Utils.PointFloat(0F, 737.7642F);
            this.xrl_Hometown.Name = "xrl_Hometown";
            this.xrl_Hometown.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_Hometown.SizeF = new System.Drawing.SizeF(1677.117F, 58.42004F);
            this.xrl_Hometown.StylePriority.UseFont = false;
            this.xrl_Hometown.StylePriority.UseTextAlignment = false;
            this.xrl_Hometown.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_DependentUnits
            // 
            this.xrl_DependentUnits.Dpi = 254F;
            this.xrl_DependentUnits.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_DependentUnits.LocationFloat = new DevExpress.Utils.PointFloat(0F, 110.1283F);
            this.xrl_DependentUnits.Name = "xrl_DependentUnits";
            this.xrl_DependentUnits.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_DependentUnits.SizeF = new System.Drawing.SizeF(388.2501F, 58.41998F);
            this.xrl_DependentUnits.StylePriority.UseFont = false;
            this.xrl_DependentUnits.StylePriority.UseTextAlignment = false;
            this.xrl_DependentUnits.Text = "Đơn vị trực thuộc..........";
            this.xrl_DependentUnits.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_DAN_TOC
            // 
            this.xrl_DAN_TOC.Dpi = 254F;
            this.xrl_DAN_TOC.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_DAN_TOC.LocationFloat = new DevExpress.Utils.PointFloat(0F, 806.7679F);
            this.xrl_DAN_TOC.Name = "xrl_DAN_TOC";
            this.xrl_DAN_TOC.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_DAN_TOC.SizeF = new System.Drawing.SizeF(289.3687F, 58.42004F);
            this.xrl_DAN_TOC.StylePriority.UseFont = false;
            this.xrl_DAN_TOC.StylePriority.UseTextAlignment = false;
            this.xrl_DAN_TOC.Text = "7) Nơi ở hiện nay";
            this.xrl_DAN_TOC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_Province
            // 
            this.xrl_Province.Dpi = 254F;
            this.xrl_Province.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_Province.LocationFloat = new DevExpress.Utils.PointFloat(0F, 51.70834F);
            this.xrl_Province.Name = "xrl_Province";
            this.xrl_Province.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_Province.SizeF = new System.Drawing.SizeF(388.25F, 58.41998F);
            this.xrl_Province.StylePriority.UseFont = false;
            this.xrl_Province.StylePriority.UseTextAlignment = false;
            this.xrl_Province.Text = "Bộ, tỉnh..........................";
            this.xrl_Province.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_BaseUnit
            // 
            this.xrl_BaseUnit.Dpi = 254F;
            this.xrl_BaseUnit.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_BaseUnit.LocationFloat = new DevExpress.Utils.PointFloat(0F, 168.5483F);
            this.xrl_BaseUnit.Name = "xrl_BaseUnit";
            this.xrl_BaseUnit.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_BaseUnit.SizeF = new System.Drawing.SizeF(388.2501F, 58.42F);
            this.xrl_BaseUnit.StylePriority.UseFont = false;
            this.xrl_BaseUnit.StylePriority.UseTextAlignment = false;
            this.xrl_BaseUnit.Text = "Đơn vị cơ sở..............";
            this.xrl_BaseUnit.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_CurrentCommittees
            // 
            this.xrl_CurrentCommittees.Dpi = 254F;
            this.xrl_CurrentCommittees.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_CurrentCommittees.LocationFloat = new DevExpress.Utils.PointFloat(673.2487F, 457.0884F);
            this.xrl_CurrentCommittees.Name = "xrl_CurrentCommittees";
            this.xrl_CurrentCommittees.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_CurrentCommittees.SizeF = new System.Drawing.SizeF(312.3125F, 58.41998F);
            this.xrl_CurrentCommittees.StylePriority.UseFont = false;
            this.xrl_CurrentCommittees.StylePriority.UseTextAlignment = false;
            this.xrl_CurrentCommittees.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrlHOTEN_CBCC
            // 
            this.xrlHOTEN_CBCC.Dpi = 254F;
            this.xrlHOTEN_CBCC.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlHOTEN_CBCC.LocationFloat = new DevExpress.Utils.PointFloat(1117.147F, 302.8338F);
            this.xrlHOTEN_CBCC.Name = "xrlHOTEN_CBCC";
            this.xrlHOTEN_CBCC.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrlHOTEN_CBCC.SizeF = new System.Drawing.SizeF(188.4651F, 58.42004F);
            this.xrlHOTEN_CBCC.StylePriority.UseFont = false;
            this.xrlHOTEN_CBCC.StylePriority.UseTextAlignment = false;
            this.xrlHOTEN_CBCC.Text = "Nam, nữ";
            this.xrlHOTEN_CBCC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_Residence
            // 
            this.xrl_Residence.Dpi = 254F;
            this.xrl_Residence.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_Residence.LocationFloat = new DevExpress.Utils.PointFloat(850.7032F, 806.7676F);
            this.xrl_Residence.Name = "xrl_Residence";
            this.xrl_Residence.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_Residence.SizeF = new System.Drawing.SizeF(834.2888F, 58.42004F);
            this.xrl_Residence.StylePriority.UseFont = false;
            this.xrl_Residence.StylePriority.UseTextAlignment = false;
            this.xrl_Residence.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_CellPhoneNumber
            // 
            this.xrl_CellPhoneNumber.Dpi = 254F;
            this.xrl_CellPhoneNumber.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_CellPhoneNumber.LocationFloat = new DevExpress.Utils.PointFloat(888.8557F, 875.7715F);
            this.xrl_CellPhoneNumber.Name = "xrl_CellPhoneNumber";
            this.xrl_CellPhoneNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_CellPhoneNumber.SizeF = new System.Drawing.SizeF(801.1339F, 58.42004F);
            this.xrl_CellPhoneNumber.StylePriority.UseFont = false;
            this.xrl_CellPhoneNumber.StylePriority.UseTextAlignment = false;
            this.xrl_CellPhoneNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 155F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 192F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel31,
            this.xrLabel29,
            this.xrLabel27,
            this.xrLabel26,
            this.xrl_sub_FamilyWife,
            this.xrLabel24,
            this.xrLabel22,
            this.xrl_Biography,
            this.xrLabel20,
            this.xrLabel19,
            this.xrl_LanguageLevelName,
            this.xrLabel35,
            this.xrl_PoliticLevelName,
            this.xrLabel33,
            this.xrl_ArmyLevelName,
            this.xrl_Birthday,
            this.xrLabel139,
            this.xrLabel131,
            this.xrLabel137,
            this.xrLabel138,
            this.xrLabel136,
            this.xrLabel125,
            this.xrLabel132,
            this.xrLabel133,
            this.xrLabel134,
            this.xrLabel135,
            this.xrLabel126,
            this.xrLabel127,
            this.xrLabel128,
            this.xrLabel129,
            this.xrLabel130,
            this.xrLabel124,
            this.xrLabel123,
            this.xrLabel122,
            this.xrLabel121,
            this.xrLabel120,
            this.xrLabel119,
            this.xrl_OtherIncome,
            this.xrLabel117,
            this.xrl_Salary,
            this.xrLabel108,
            this.xrLabel114,
            this.xrLabel111,
            this.xrLabel110,
            this.xrLabel109,
            this.xrLabel107,
            this.xrLabel106,
            this.xrl_FamilyPolicyName,
            this.xrLabel44,
            this.xrl_RankWounded,
            this.xrLabel43,
            this.xrLabel18,
            this.xrLabel17,
            this.xrLabel16,
            this.xrLabel11,
            this.xrl_TitleAwarded,
            this.xrLabel104,
            this.xrLabel103,
            this.xrLabel40,
            this.xrLabel102,
            this.xrLabel101,
            this.xrLabel100,
            this.xrLabel99,
            this.xrLabel98,
            this.xrLabel97,
            this.xrLabel96,
            this.xrLabel95,
            this.xrLabel94,
            this.xrLabel92,
            this.xrl_AssignedWork,
            this.xrLabel42,
            this.xrLabel38,
            this.xrLabel41,
            this.lbl_BasicEducation,
            this.xrl_BasicEducationName,
            this.xrLabel37,
            this.xrLabel91,
            this.xrl_ArmyLeftDate,
            this.xrLabel89,
            this.xrLabel15,
            this.xrl_PoliticDate,
            this.xrLabel13,
            this.xrl_CPVJoinedDate,
            this.xrLabel36,
            this.xrl_CPVOfficialJoinedDate,
            this.xrl_RecruimentDate,
            this.xrl_FunctionaryDate,
            this.xrLabel87,
            this.xrl_RecruimentDepartment,
            this.xrLabel82,
            this.xrLabel10,
            this.xrLabel75,
            this.xrLabel12,
            this.xrl_FamilyClassName,
            this.xrLabel8,
            this.xrl_Religiousness,
            this.xrLabel79,
            this.xrl_Folk,
            this.xrLabel77,
            this.xrl_ResidentPlace,
            this.xrLabel14,
            this.xrLabel2,
            this.xrLabel74,
            this.xrl_BirthPlace,
            this.xrl_Allowance,
            this.xrLabel59,
            this.xrl_PositionName,
            this.xrLabel57,
            this.xrLabel56,
            this.xrl_Committees,
            this.xrLabel32,
            this.xrl_Sex,
            this.xrl_FullName,
            this.xrLabel55,
            this.xrLabel53,
            this.xrLabel7,
            this.xrl_CellPhoneNumber,
            this.xrl_Residence,
            this.xrlHOTEN_CBCC,
            this.xrl_CurrentCommittees,
            this.xrl_BaseUnit,
            this.xrl_Province,
            this.xrl_DAN_TOC,
            this.xrl_DependentUnits,
            this.xrl_Hometown,
            this.xrl_Alias,
            this.lbl_Birthday,
            this.xrl_HO_TEN,
            this.xrl_NGAY_SINH,
            this.lbl_Alias,
            this.xrPictureBox1,
            this.xrLabel5,
            this.xrLabel4,
            this.lbMaNgachCC,
            this.xrl_TrinhDoPhoT,
            this.xrl_PreviousJob,
            this.lblNote,
            this.xrl_TRINHDO,
            this.xrl_ArmyJoinedDate,
            this.xrl_HighestEducation,
            this.xrl_DisciplineName,
            this.xrLabel6,
            this.xrl_RewardName,
            this.xrLabel3,
            this.xrl_IDNumber,
            this.xrLabel25,
            this.xrLabel23,
            this.xrl_Weight,
            this.xrl_BloodGroup,
            this.xrl_Height,
            this.xrl_Skills,
            this.xrl_CAN_NANG,
            this.xrl_CHIEU_CAO,
            this.xrl_NHOM_MAU,
            this.xrl_TT_SUCKHOE,
            this.xrl_SO_CMND,
            this.xrl_NANGLUC,
            this.xrl_sub_TrainingProcess,
            this.xrl_sub_ProcessWorking,
            this.xrLabel54,
            this.xrLabel52,
            this.xrl_ForeignOrganizationJoined,
            this.xrl_RelativesAboard,
            this.xrl_subProcessSalary,
            this.xrl_sub_FamilyHusband,
            this.xrLabel1,
            this.xrLabel112,
            this.xrtngayketxuat,
            this.xrLabel113,
            this.xrLabel115,
            this.xrLabel9,
            this.xrLabel28,
            this.xrLabel51,
            this.xrLabel30});
            this.ReportHeader.Dpi = 254F;
            this.ReportHeader.HeightF = 5952.786F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel31
            // 
            this.xrLabel31.Dpi = 254F;
            this.xrLabel31.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(1297.24F, 2114.191F);
            this.xrLabel31.Name = "xrLabel31";
            this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel31.SizeF = new System.Drawing.SizeF(288.8221F, 41.14917F);
            this.xrLabel31.StylePriority.UseFont = false;
            this.xrLabel31.StylePriority.UseTextAlignment = false;
            this.xrLabel31.Text = "Pháp (A/B/C/D)....)";
            this.xrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel29
            // 
            this.xrLabel29.Dpi = 254F;
            this.xrLabel29.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(1001.462F, 2114.191F);
            this.xrLabel29.Name = "xrLabel29";
            this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel29.SizeF = new System.Drawing.SizeF(208.8566F, 41.14917F);
            this.xrLabel29.StylePriority.UseFont = false;
            this.xrLabel29.StylePriority.UseTextAlignment = false;
            this.xrLabel29.Text = "Nga (A/B/C/D)";
            this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel27
            // 
            this.xrLabel27.Dpi = 254F;
            this.xrLabel27.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(747.1644F, 2114.191F);
            this.xrLabel27.Name = "xrLabel27";
            this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel27.SizeF = new System.Drawing.SizeF(208.8566F, 41.14917F);
            this.xrLabel27.StylePriority.UseFont = false;
            this.xrLabel27.StylePriority.UseTextAlignment = false;
            this.xrLabel27.Text = "(Anh (A/B/C/D)";
            this.xrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel26
            // 
            this.xrLabel26.Dpi = 254F;
            this.xrLabel26.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(146.5602F, 2114.191F);
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel26.SizeF = new System.Drawing.SizeF(473.5817F, 41.14917F);
            this.xrLabel26.StylePriority.UseFont = false;
            this.xrLabel26.StylePriority.UseTextAlignment = false;
            this.xrLabel26.Text = "(Cử nhân, Cao cấp, Trung cấp, Sơ cấp)";
            this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_sub_FamilyWife
            // 
            this.xrl_sub_FamilyWife.Dpi = 254F;
            this.xrl_sub_FamilyWife.LocationFloat = new DevExpress.Utils.PointFloat(16.18304F, 4516.625F);
            this.xrl_sub_FamilyWife.Name = "xrl_sub_FamilyWife";
            this.xrl_sub_FamilyWife.ReportSource = new Web.Core.Framework.Report.sub_FamilyRelationship();
            this.xrl_sub_FamilyWife.SizeF = new System.Drawing.SizeF(1640.479F, 111.7598F);
            // 
            // xrLabel24
            // 
            this.xrLabel24.Dpi = 254F;
            this.xrLabel24.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(0F, 3499.153F);
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel24.SizeF = new System.Drawing.SizeF(1651.58F, 76.94067F);
            this.xrLabel24.StylePriority.UseFont = false;
            this.xrLabel24.StylePriority.UseTextAlignment = false;
            this.xrLabel24.Text = "b) Bản thân có làm việc trong chế độ cũ (Cơ quan, đơn vị nào, địa điểm, chức danh" +
    ", chức vụ, thời gian làm việc ...) ";
            this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel22
            // 
            this.xrLabel22.Dpi = 254F;
            this.xrLabel22.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(2.837036F, 3602.983F);
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel22.SizeF = new System.Drawing.SizeF(1651.58F, 76.94067F);
            this.xrLabel22.StylePriority.UseFont = false;
            this.xrLabel22.StylePriority.UseTextAlignment = false;
            this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrl_Biography
            // 
            this.xrl_Biography.Dpi = 254F;
            this.xrl_Biography.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_Biography.LocationFloat = new DevExpress.Utils.PointFloat(11.07814F, 3401.899F);
            this.xrl_Biography.Name = "xrl_Biography";
            this.xrl_Biography.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_Biography.SizeF = new System.Drawing.SizeF(1651.58F, 76.94067F);
            this.xrl_Biography.StylePriority.UseFont = false;
            this.xrl_Biography.StylePriority.UseTextAlignment = false;
            this.xrl_Biography.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel20
            // 
            this.xrLabel20.Dpi = 254F;
            this.xrLabel20.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(7.311569F, 3304.003F);
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(1651.58F, 76.94067F);
            this.xrLabel20.StylePriority.UseFont = false;
            this.xrLabel20.StylePriority.UseTextAlignment = false;
            this.xrLabel20.Text = "a) Khai rõ: bị bắt, bị tù (từ ngày tháng năm nào đến ngày tháng năm nào, ở đâu), " +
    "đã khai báo cho ai, những vấn đề gì ";
            this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel19
            // 
            this.xrLabel19.Dpi = 254F;
            this.xrLabel19.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(0F, 3224.417F);
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(1680.924F, 58.42017F);
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.StylePriority.UseTextAlignment = false;
            this.xrLabel19.Text = "28) ĐẶC ĐIỂM LỊCH SỬ BẢN THÂN";
            this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrl_LanguageLevelName
            // 
            this.xrl_LanguageLevelName.Dpi = 254F;
            this.xrl_LanguageLevelName.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_LanguageLevelName.LocationFloat = new DevExpress.Utils.PointFloat(924.0505F, 2055.771F);
            this.xrl_LanguageLevelName.Name = "xrl_LanguageLevelName";
            this.xrl_LanguageLevelName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_LanguageLevelName.SizeF = new System.Drawing.SizeF(738.1311F, 58.41992F);
            this.xrl_LanguageLevelName.StylePriority.UseFont = false;
            this.xrl_LanguageLevelName.StylePriority.UseTextAlignment = false;
            this.xrl_LanguageLevelName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel35
            // 
            this.xrLabel35.Dpi = 254F;
            this.xrLabel35.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(584.391F, 2055.771F);
            this.xrLabel35.Name = "xrLabel35";
            this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel35.SizeF = new System.Drawing.SizeF(307.0221F, 58.41992F);
            this.xrLabel35.StylePriority.UseFont = false;
            this.xrLabel35.StylePriority.UseTextAlignment = false;
            this.xrLabel35.Text = "- Ngoại ngữ";
            this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_PoliticLevelName
            // 
            this.xrl_PoliticLevelName.Dpi = 254F;
            this.xrl_PoliticLevelName.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_PoliticLevelName.LocationFloat = new DevExpress.Utils.PointFloat(336.8615F, 2055.771F);
            this.xrl_PoliticLevelName.Name = "xrl_PoliticLevelName";
            this.xrl_PoliticLevelName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_PoliticLevelName.SizeF = new System.Drawing.SizeF(216.1565F, 58.41992F);
            this.xrl_PoliticLevelName.StylePriority.UseFont = false;
            this.xrl_PoliticLevelName.StylePriority.UseTextAlignment = false;
            this.xrl_PoliticLevelName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel33
            // 
            this.xrLabel33.Dpi = 254F;
            this.xrLabel33.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(5.082054F, 2055.771F);
            this.xrLabel33.Name = "xrLabel33";
            this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel33.SizeF = new System.Drawing.SizeF(307.0221F, 58.41992F);
            this.xrLabel33.StylePriority.UseFont = false;
            this.xrLabel33.StylePriority.UseTextAlignment = false;
            this.xrLabel33.Text = "- Lý luận chính trị";
            this.xrLabel33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_ArmyLevelName
            // 
            this.xrl_ArmyLevelName.Dpi = 254F;
            this.xrl_ArmyLevelName.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_ArmyLevelName.LocationFloat = new DevExpress.Utils.PointFloat(1481.492F, 1844.333F);
            this.xrl_ArmyLevelName.Name = "xrl_ArmyLevelName";
            this.xrl_ArmyLevelName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_ArmyLevelName.SizeF = new System.Drawing.SizeF(197.7125F, 58.41992F);
            this.xrl_ArmyLevelName.StylePriority.UseFont = false;
            this.xrl_ArmyLevelName.StylePriority.UseTextAlignment = false;
            this.xrl_ArmyLevelName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_Birthday
            // 
            this.xrl_Birthday.Dpi = 254F;
            this.xrl_Birthday.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_Birthday.LocationFloat = new DevExpress.Utils.PointFloat(280.4971F, 679.3444F);
            this.xrl_Birthday.Name = "xrl_Birthday";
            this.xrl_Birthday.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_Birthday.SizeF = new System.Drawing.SizeF(317.9584F, 58.41998F);
            this.xrl_Birthday.StylePriority.UseFont = false;
            this.xrl_Birthday.StylePriority.UseTextAlignment = false;
            this.xrl_Birthday.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel139
            // 
            this.xrLabel139.Dpi = 254F;
            this.xrLabel139.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel139.LocationFloat = new DevExpress.Utils.PointFloat(708.2708F, 5613.646F);
            this.xrLabel139.Name = "xrLabel139";
            this.xrLabel139.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel139.SizeF = new System.Drawing.SizeF(968.8468F, 58.41943F);
            this.xrLabel139.StylePriority.UseFont = false;
            this.xrLabel139.StylePriority.UseTextAlignment = false;
            this.xrLabel139.Text = "(hoặc của Công an xã, phường, thị trấn nơi cư trú)";
            this.xrLabel139.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel131
            // 
            this.xrLabel131.Dpi = 254F;
            this.xrLabel131.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel131.LocationFloat = new DevExpress.Utils.PointFloat(0F, 5329.377F);
            this.xrLabel131.Name = "xrLabel131";
            this.xrLabel131.Padding = new DevExpress.XtraPrinting.PaddingInfo(15, 5, 0, 0, 254F);
            this.xrLabel131.SizeF = new System.Drawing.SizeF(1438.946F, 58.41992F);
            this.xrLabel131.StylePriority.UseFont = false;
            this.xrLabel131.StylePriority.UsePadding = false;
            this.xrLabel131.StylePriority.UseTextAlignment = false;
            this.xrLabel131.Text = "- Đất sản xuất, kinh doanh: (Tổng diện tích đất được cấp, tự mua, tự khai phá...";
            this.xrLabel131.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel137
            // 
            this.xrLabel137.Dpi = 254F;
            this.xrLabel137.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel137.LocationFloat = new DevExpress.Utils.PointFloat(1455.367F, 5329.377F);
            this.xrLabel137.Name = "xrLabel137";
            this.xrLabel137.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel137.SizeF = new System.Drawing.SizeF(223.8375F, 58.41992F);
            this.xrLabel137.StylePriority.UseFont = false;
            this.xrLabel137.StylePriority.UseTextAlignment = false;
            this.xrLabel137.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel138
            // 
            this.xrLabel138.Dpi = 254F;
            this.xrLabel138.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel138.LocationFloat = new DevExpress.Utils.PointFloat(1595.009F, 5193.046F);
            this.xrLabel138.Name = "xrLabel138";
            this.xrLabel138.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel138.SizeF = new System.Drawing.SizeF(82.1084F, 67.6084F);
            this.xrLabel138.StylePriority.UseFont = false;
            this.xrLabel138.StylePriority.UseTextAlignment = false;
            this.xrLabel138.Text = "m2";
            this.xrLabel138.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel136
            // 
            this.xrLabel136.Dpi = 254F;
            this.xrLabel136.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel136.LocationFloat = new DevExpress.Utils.PointFloat(445.9999F, 5192.755F);
            this.xrLabel136.Name = "xrLabel136";
            this.xrLabel136.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel136.SizeF = new System.Drawing.SizeF(173.0906F, 67.6084F);
            this.xrLabel136.StylePriority.UseFont = false;
            this.xrLabel136.StylePriority.UseTextAlignment = false;
            this.xrLabel136.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel125
            // 
            this.xrLabel125.Dpi = 254F;
            this.xrLabel125.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel125.LocationFloat = new DevExpress.Utils.PointFloat(53.34494F, 5187.755F);
            this.xrLabel125.Name = "xrLabel125";
            this.xrLabel125.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel125.SizeF = new System.Drawing.SizeF(131.7594F, 72.60889F);
            this.xrLabel125.StylePriority.UseFont = false;
            this.xrLabel125.StylePriority.UseTextAlignment = false;
            this.xrLabel125.Text = "- Đất ở";
            this.xrLabel125.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel132
            // 
            this.xrLabel132.Dpi = 254F;
            this.xrLabel132.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel132.LocationFloat = new DevExpress.Utils.PointFloat(185.1043F, 5187.755F);
            this.xrLabel132.Name = "xrLabel132";
            this.xrLabel132.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel132.SizeF = new System.Drawing.SizeF(245.172F, 72.60889F);
            this.xrLabel132.StylePriority.UseFont = false;
            this.xrLabel132.StylePriority.UseTextAlignment = false;
            this.xrLabel132.Text = "+ Đất được cấp:";
            this.xrLabel132.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel133
            // 
            this.xrLabel133.Dpi = 254F;
            this.xrLabel133.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel133.LocationFloat = new DevExpress.Utils.PointFloat(838.0141F, 5182.755F);
            this.xrLabel133.Name = "xrLabel133";
            this.xrLabel133.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel133.SizeF = new System.Drawing.SizeF(533.0836F, 72.60889F);
            this.xrLabel133.StylePriority.UseFont = false;
            this.xrLabel133.StylePriority.UseTextAlignment = false;
            this.xrLabel133.Text = "+ Đất tự mua";
            this.xrLabel133.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel134
            // 
            this.xrLabel134.Dpi = 254F;
            this.xrLabel134.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel134.LocationFloat = new DevExpress.Utils.PointFloat(1376.068F, 5187.755F);
            this.xrLabel134.Name = "xrLabel134";
            this.xrLabel134.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel134.SizeF = new System.Drawing.SizeF(209.9937F, 67.6084F);
            this.xrLabel134.StylePriority.UseFont = false;
            this.xrLabel134.StylePriority.UseTextAlignment = false;
            this.xrLabel134.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel135
            // 
            this.xrLabel135.Dpi = 254F;
            this.xrLabel135.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel135.LocationFloat = new DevExpress.Utils.PointFloat(626.5031F, 5192.755F);
            this.xrLabel135.Name = "xrLabel135";
            this.xrLabel135.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel135.SizeF = new System.Drawing.SizeF(82.1084F, 67.6084F);
            this.xrLabel135.StylePriority.UseFont = false;
            this.xrLabel135.StylePriority.UseTextAlignment = false;
            this.xrLabel135.Text = "m2";
            this.xrLabel135.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel126
            // 
            this.xrLabel126.Dpi = 254F;
            this.xrLabel126.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel126.LocationFloat = new DevExpress.Utils.PointFloat(191.4269F, 5092.505F);
            this.xrLabel126.Name = "xrLabel126";
            this.xrLabel126.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel126.SizeF = new System.Drawing.SizeF(606.6202F, 72.60889F);
            this.xrLabel126.StylePriority.UseFont = false;
            this.xrLabel126.StylePriority.UseTextAlignment = false;
            this.xrLabel126.Text = "+ Nhà tự mua, tự xây, loại nhà";
            this.xrLabel126.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel127
            // 
            this.xrLabel127.Dpi = 254F;
            this.xrLabel127.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel127.LocationFloat = new DevExpress.Utils.PointFloat(801.0209F, 5092.505F);
            this.xrLabel127.Name = "xrLabel127";
            this.xrLabel127.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel127.SizeF = new System.Drawing.SizeF(209.9937F, 67.6084F);
            this.xrLabel127.StylePriority.UseFont = false;
            this.xrLabel127.StylePriority.UseTextAlignment = false;
            this.xrLabel127.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel128
            // 
            this.xrLabel128.Dpi = 254F;
            this.xrLabel128.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel128.LocationFloat = new DevExpress.Utils.PointFloat(1013.186F, 5087.505F);
            this.xrLabel128.Name = "xrLabel128";
            this.xrLabel128.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel128.SizeF = new System.Drawing.SizeF(349.9743F, 72.60889F);
            this.xrLabel128.StylePriority.UseFont = false;
            this.xrLabel128.StylePriority.UseTextAlignment = false;
            this.xrLabel128.Text = ", tổng diện tích sử dụng";
            this.xrLabel128.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel129
            // 
            this.xrLabel129.Dpi = 254F;
            this.xrLabel129.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel129.LocationFloat = new DevExpress.Utils.PointFloat(1368.131F, 5092.505F);
            this.xrLabel129.Name = "xrLabel129";
            this.xrLabel129.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel129.SizeF = new System.Drawing.SizeF(209.9937F, 67.6084F);
            this.xrLabel129.StylePriority.UseFont = false;
            this.xrLabel129.StylePriority.UseTextAlignment = false;
            this.xrLabel129.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel130
            // 
            this.xrLabel130.Dpi = 254F;
            this.xrLabel130.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel130.LocationFloat = new DevExpress.Utils.PointFloat(1595.009F, 5092.505F);
            this.xrLabel130.Name = "xrLabel130";
            this.xrLabel130.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel130.SizeF = new System.Drawing.SizeF(82.1084F, 67.6084F);
            this.xrLabel130.StylePriority.UseFont = false;
            this.xrLabel130.StylePriority.UseTextAlignment = false;
            this.xrLabel130.Text = "m2";
            this.xrLabel130.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel124
            // 
            this.xrLabel124.Dpi = 254F;
            this.xrLabel124.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel124.LocationFloat = new DevExpress.Utils.PointFloat(1595.009F, 4994.609F);
            this.xrLabel124.Name = "xrLabel124";
            this.xrLabel124.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel124.SizeF = new System.Drawing.SizeF(82.1084F, 67.6084F);
            this.xrLabel124.StylePriority.UseFont = false;
            this.xrLabel124.StylePriority.UseTextAlignment = false;
            this.xrLabel124.Text = "m2";
            this.xrLabel124.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel123
            // 
            this.xrLabel123.Dpi = 254F;
            this.xrLabel123.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel123.LocationFloat = new DevExpress.Utils.PointFloat(1368.131F, 4994.609F);
            this.xrLabel123.Name = "xrLabel123";
            this.xrLabel123.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel123.SizeF = new System.Drawing.SizeF(209.9937F, 67.6084F);
            this.xrLabel123.StylePriority.UseFont = false;
            this.xrLabel123.StylePriority.UseTextAlignment = false;
            this.xrLabel123.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel122
            // 
            this.xrLabel122.Dpi = 254F;
            this.xrLabel122.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel122.LocationFloat = new DevExpress.Utils.PointFloat(1013.186F, 4989.609F);
            this.xrLabel122.Name = "xrLabel122";
            this.xrLabel122.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel122.SizeF = new System.Drawing.SizeF(349.9743F, 72.60889F);
            this.xrLabel122.StylePriority.UseFont = false;
            this.xrLabel122.StylePriority.UseTextAlignment = false;
            this.xrLabel122.Text = ", tổng diện tích sử dụng";
            this.xrLabel122.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel121
            // 
            this.xrLabel121.Dpi = 254F;
            this.xrLabel121.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel121.LocationFloat = new DevExpress.Utils.PointFloat(798.047F, 4999.609F);
            this.xrLabel121.Name = "xrLabel121";
            this.xrLabel121.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel121.SizeF = new System.Drawing.SizeF(209.9937F, 67.6084F);
            this.xrLabel121.StylePriority.UseFont = false;
            this.xrLabel121.StylePriority.UseTextAlignment = false;
            this.xrLabel121.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel120
            // 
            this.xrLabel120.Dpi = 254F;
            this.xrLabel120.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel120.LocationFloat = new DevExpress.Utils.PointFloat(177.1668F, 4994.609F);
            this.xrLabel120.Name = "xrLabel120";
            this.xrLabel120.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel120.SizeF = new System.Drawing.SizeF(620.8802F, 72.60889F);
            this.xrLabel120.StylePriority.UseFont = false;
            this.xrLabel120.StylePriority.UseTextAlignment = false;
            this.xrLabel120.Text = "+ Được cấp, được thuê, loại nhà";
            this.xrLabel120.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel119
            // 
            this.xrLabel119.Dpi = 254F;
            this.xrLabel119.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel119.LocationFloat = new DevExpress.Utils.PointFloat(45.40744F, 4994.609F);
            this.xrLabel119.Name = "xrLabel119";
            this.xrLabel119.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel119.SizeF = new System.Drawing.SizeF(131.7594F, 72.60889F);
            this.xrLabel119.StylePriority.UseFont = false;
            this.xrLabel119.StylePriority.UseTextAlignment = false;
            this.xrLabel119.Text = "- Nhà ở";
            this.xrLabel119.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrl_OtherIncome
            // 
            this.xrl_OtherIncome.Dpi = 254F;
            this.xrl_OtherIncome.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_OtherIncome.LocationFloat = new DevExpress.Utils.PointFloat(406.6974F, 4900.756F);
            this.xrl_OtherIncome.Name = "xrl_OtherIncome";
            this.xrl_OtherIncome.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_OtherIncome.SizeF = new System.Drawing.SizeF(1272.507F, 58.41992F);
            this.xrl_OtherIncome.StylePriority.UseFont = false;
            this.xrl_OtherIncome.StylePriority.UseTextAlignment = false;
            this.xrl_OtherIncome.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel117
            // 
            this.xrLabel117.Dpi = 254F;
            this.xrLabel117.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel117.LocationFloat = new DevExpress.Utils.PointFloat(18.89161F, 4900.756F);
            this.xrLabel117.Name = "xrLabel117";
            this.xrLabel117.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel117.SizeF = new System.Drawing.SizeF(369.3585F, 58.41992F);
            this.xrLabel117.StylePriority.UseFont = false;
            this.xrLabel117.StylePriority.UseTextAlignment = false;
            this.xrLabel117.Text = "+ Các nguồn khác:";
            this.xrLabel117.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_Salary
            // 
            this.xrl_Salary.Dpi = 254F;
            this.xrl_Salary.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_Salary.LocationFloat = new DevExpress.Utils.PointFloat(1106.676F, 4842.336F);
            this.xrl_Salary.Name = "xrl_Salary";
            this.xrl_Salary.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_Salary.SizeF = new System.Drawing.SizeF(570.4418F, 58.42F);
            this.xrl_Salary.StylePriority.UseFont = false;
            this.xrl_Salary.StylePriority.UseTextAlignment = false;
            this.xrl_Salary.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel108
            // 
            this.xrLabel108.Dpi = 254F;
            this.xrLabel108.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.xrLabel108.LocationFloat = new DevExpress.Utils.PointFloat(6.172104F, 4649.621F);
            this.xrLabel108.Name = "xrLabel108";
            this.xrLabel108.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel108.SizeF = new System.Drawing.SizeF(1650.49F, 58.41992F);
            this.xrLabel108.StylePriority.UseFont = false;
            this.xrLabel108.StylePriority.UseTextAlignment = false;
            this.xrLabel108.Text = "31) HOÀN CẢNH KINH TẾ GIA ĐÌNH";
            this.xrLabel108.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel114
            // 
            this.xrLabel114.Dpi = 254F;
            this.xrLabel114.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel114.LocationFloat = new DevExpress.Utils.PointFloat(1338.653F, 3979.853F);
            this.xrLabel114.Name = "xrLabel114";
            this.xrLabel114.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel114.SizeF = new System.Drawing.SizeF(38.01453F, 62.42065F);
            this.xrLabel114.StylePriority.UseFont = false;
            this.xrLabel114.StylePriority.UseTextAlignment = false;
            this.xrLabel114.Text = "?";
            this.xrLabel114.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel111
            // 
            this.xrLabel111.Dpi = 254F;
            this.xrLabel111.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel111.LocationFloat = new DevExpress.Utils.PointFloat(1106.676F, 4005.021F);
            this.xrLabel111.Name = "xrLabel111";
            this.xrLabel111.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel111.SizeF = new System.Drawing.SizeF(221.5048F, 37.25366F);
            this.xrLabel111.StylePriority.UseFont = false;
            this.xrLabel111.StylePriority.UseTextAlignment = false;
            this.xrLabel111.Text = "(làm gì, địa chỉ....)";
            this.xrLabel111.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel110
            // 
            this.xrLabel110.Dpi = 254F;
            this.xrLabel110.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel110.LocationFloat = new DevExpress.Utils.PointFloat(870.6791F, 3979.854F);
            this.xrLabel110.Name = "xrLabel110";
            this.xrLabel110.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel110.SizeF = new System.Drawing.SizeF(236.6323F, 72.60913F);
            this.xrLabel110.StylePriority.UseFont = false;
            this.xrLabel110.StylePriority.UseTextAlignment = false;
            this.xrLabel110.Text = "Ở nước ngoài";
            this.xrLabel110.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel109
            // 
            this.xrLabel109.Dpi = 254F;
            this.xrLabel109.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel109.LocationFloat = new DevExpress.Utils.PointFloat(245.1233F, 3983.854F);
            this.xrLabel109.Name = "xrLabel109";
            this.xrLabel109.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel109.SizeF = new System.Drawing.SizeF(481.2258F, 47.83691F);
            this.xrLabel109.StylePriority.UseFont = false;
            this.xrLabel109.StylePriority.UseTextAlignment = false;
            this.xrLabel109.Text = "(Bố, mẹ, vợ, chồng, con, anh chị em ruột)";
            this.xrLabel109.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel107
            // 
            this.xrLabel107.Dpi = 254F;
            this.xrLabel107.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel107.LocationFloat = new DevExpress.Utils.PointFloat(12.16819F, 3838.454F);
            this.xrLabel107.Name = "xrLabel107";
            this.xrLabel107.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel107.SizeF = new System.Drawing.SizeF(925.4202F, 58.42017F);
            this.xrLabel107.StylePriority.UseFont = false;
            this.xrLabel107.StylePriority.UseTextAlignment = false;
            this.xrLabel107.Text = "(làm gì, tổ chức nào, đặt trụ sở ở đâu...?)";
            this.xrLabel107.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel106
            // 
            this.xrLabel106.Dpi = 254F;
            this.xrLabel106.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel106.LocationFloat = new DevExpress.Utils.PointFloat(5.076887F, 3691.875F);
            this.xrLabel106.Name = "xrLabel106";
            this.xrLabel106.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel106.SizeF = new System.Drawing.SizeF(1680.924F, 58.42017F);
            this.xrLabel106.StylePriority.UseFont = false;
            this.xrLabel106.StylePriority.UseTextAlignment = false;
            this.xrLabel106.Text = "29) QUAN HỆ VỚI NGƯỜI NƯỚC NGOÀI";
            this.xrLabel106.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrl_FamilyPolicyName
            // 
            this.xrl_FamilyPolicyName.Dpi = 254F;
            this.xrl_FamilyPolicyName.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_FamilyPolicyName.LocationFloat = new DevExpress.Utils.PointFloat(1414.359F, 2954.042F);
            this.xrl_FamilyPolicyName.Name = "xrl_FamilyPolicyName";
            this.xrl_FamilyPolicyName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_FamilyPolicyName.SizeF = new System.Drawing.SizeF(261.2529F, 58.41992F);
            this.xrl_FamilyPolicyName.StylePriority.UseFont = false;
            this.xrl_FamilyPolicyName.StylePriority.UseTextAlignment = false;
            this.xrl_FamilyPolicyName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel44
            // 
            this.xrLabel44.Dpi = 254F;
            this.xrLabel44.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel44.LocationFloat = new DevExpress.Utils.PointFloat(1065.475F, 2954.042F);
            this.xrLabel44.Name = "xrLabel44";
            this.xrLabel44.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel44.SizeF = new System.Drawing.SizeF(297.6851F, 58.42017F);
            this.xrLabel44.StylePriority.UseFont = false;
            this.xrLabel44.StylePriority.UseTextAlignment = false;
            this.xrLabel44.Text = "Gia đình liệt sĩ";
            this.xrLabel44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_RankWounded
            // 
            this.xrl_RankWounded.Dpi = 254F;
            this.xrl_RankWounded.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_RankWounded.LocationFloat = new DevExpress.Utils.PointFloat(888.856F, 2953.459F);
            this.xrl_RankWounded.Name = "xrl_RankWounded";
            this.xrl_RankWounded.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_RankWounded.SizeF = new System.Drawing.SizeF(162.6195F, 58.41992F);
            this.xrl_RankWounded.StylePriority.UseFont = false;
            this.xrl_RankWounded.StylePriority.UseTextAlignment = false;
            this.xrl_RankWounded.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel43
            // 
            this.xrLabel43.Dpi = 254F;
            this.xrLabel43.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel43.LocationFloat = new DevExpress.Utils.PointFloat(222.2448F, 2872.506F);
            this.xrLabel43.Name = "xrLabel43";
            this.xrLabel43.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel43.SizeF = new System.Drawing.SizeF(575.8023F, 42.54541F);
            this.xrLabel43.StylePriority.UseFont = false;
            this.xrLabel43.StylePriority.UseTextAlignment = false;
            this.xrLabel43.Text = "(tốt, bình thường, yếu hoặc có bệnh mãn tính gì)";
            this.xrLabel43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel18
            // 
            this.xrLabel18.Dpi = 254F;
            this.xrLabel18.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(258.7549F, 2737.666F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(899.2504F, 58.42017F);
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            this.xrLabel18.Text = "(Đảng, Chính quyền, Đoàn thể, Cấp quyết định, năm nào, lý do, hình thức, ...):";
            this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel17
            // 
            this.xrLabel17.Dpi = 254F;
            this.xrLabel17.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(429.5894F, 2653.212F);
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(352.4239F, 39.89941F);
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.StylePriority.UseTextAlignment = false;
            this.xrLabel17.Text = "(Huân, huy chương năm nào)";
            this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel16
            // 
            this.xrLabel16.Dpi = 254F;
            this.xrLabel16.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(1202.638F, 2504.204F);
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(451.7797F, 58.41968F);
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Dpi = 254F;
            this.xrLabel11.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(757.5494F, 2504.204F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(441.1847F, 58.42041F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "Công việc đã làm lâu nhất";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_TitleAwarded
            // 
            this.xrl_TitleAwarded.Dpi = 254F;
            this.xrl_TitleAwarded.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_TitleAwarded.LocationFloat = new DevExpress.Utils.PointFloat(626.5031F, 2360.119F);
            this.xrl_TitleAwarded.Name = "xrl_TitleAwarded";
            this.xrl_TitleAwarded.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_TitleAwarded.SizeF = new System.Drawing.SizeF(1036.155F, 58.42017F);
            this.xrl_TitleAwarded.StylePriority.UseFont = false;
            this.xrl_TitleAwarded.StylePriority.UseTextAlignment = false;
            this.xrl_TitleAwarded.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel104
            // 
            this.xrLabel104.Dpi = 254F;
            this.xrLabel104.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel104.LocationFloat = new DevExpress.Utils.PointFloat(480.0001F, 2417.539F);
            this.xrLabel104.Name = "xrLabel104";
            this.xrLabel104.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel104.SizeF = new System.Drawing.SizeF(1174.417F, 47.83716F);
            this.xrLabel104.StylePriority.UseFont = false;
            this.xrLabel104.StylePriority.UseTextAlignment = false;
            this.xrLabel104.Text = "(Anh hùng lao động, anh hùng lực lượng vũ trang, nhà giáo, thấy thuốc, nghệ sĩ nh" +
    "ân dân, ưu tú)";
            this.xrLabel104.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel103
            // 
            this.xrLabel103.Dpi = 254F;
            this.xrLabel103.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel103.LocationFloat = new DevExpress.Utils.PointFloat(445.9999F, 2359.119F);
            this.xrLabel103.Name = "xrLabel103";
            this.xrLabel103.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel103.SizeF = new System.Drawing.SizeF(139.0455F, 58.42017F);
            this.xrLabel103.StylePriority.UseFont = false;
            this.xrLabel103.StylePriority.UseTextAlignment = false;
            this.xrLabel103.Text = "Năm nào";
            this.xrLabel103.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel40
            // 
            this.xrLabel40.Dpi = 254F;
            this.xrLabel40.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel40.LocationFloat = new DevExpress.Utils.PointFloat(3.479116F, 2356.285F);
            this.xrLabel40.Name = "xrLabel40";
            this.xrLabel40.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel40.SizeF = new System.Drawing.SizeF(442.5209F, 58.41992F);
            this.xrLabel40.StylePriority.UseFont = false;
            this.xrLabel40.StylePriority.UseTextAlignment = false;
            this.xrLabel40.Text = "20) Danh hiệu được phong";
            this.xrLabel40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel102
            // 
            this.xrLabel102.Dpi = 254F;
            this.xrLabel102.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel102.LocationFloat = new DevExpress.Utils.PointFloat(1565.812F, 2278.556F);
            this.xrLabel102.Name = "xrLabel102";
            this.xrLabel102.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel102.SizeF = new System.Drawing.SizeF(113.3928F, 58.41968F);
            this.xrLabel102.StylePriority.UseFont = false;
            this.xrLabel102.StylePriority.UseTextAlignment = false;
            this.xrLabel102.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel101
            // 
            this.xrLabel101.Dpi = 254F;
            this.xrLabel101.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel101.LocationFloat = new DevExpress.Utils.PointFloat(1414.359F, 2279.557F);
            this.xrLabel101.Name = "xrLabel101";
            this.xrLabel101.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel101.SizeF = new System.Drawing.SizeF(142.497F, 58.41968F);
            this.xrLabel101.StylePriority.UseFont = false;
            this.xrLabel101.StylePriority.UseTextAlignment = false;
            this.xrLabel101.Text = "Từ tháng";
            this.xrLabel101.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel100
            // 
            this.xrLabel100.Dpi = 254F;
            this.xrLabel100.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel100.LocationFloat = new DevExpress.Utils.PointFloat(1260.022F, 2279.557F);
            this.xrLabel100.Name = "xrLabel100";
            this.xrLabel100.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel100.SizeF = new System.Drawing.SizeF(142.497F, 58.41968F);
            this.xrLabel100.StylePriority.UseFont = false;
            this.xrLabel100.StylePriority.UseTextAlignment = false;
            this.xrLabel100.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel99
            // 
            this.xrLabel99.Dpi = 254F;
            this.xrLabel99.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel99.LocationFloat = new DevExpress.Utils.PointFloat(1106.676F, 2279.557F);
            this.xrLabel99.Name = "xrLabel99";
            this.xrLabel99.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel99.SizeF = new System.Drawing.SizeF(142.497F, 58.41968F);
            this.xrLabel99.StylePriority.UseFont = false;
            this.xrLabel99.StylePriority.UseTextAlignment = false;
            this.xrLabel99.Text = "Hệ số";
            this.xrLabel99.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel98
            // 
            this.xrLabel98.Dpi = 254F;
            this.xrLabel98.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel98.LocationFloat = new DevExpress.Utils.PointFloat(956.0211F, 2279.557F);
            this.xrLabel98.Name = "xrLabel98";
            this.xrLabel98.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel98.SizeF = new System.Drawing.SizeF(142.497F, 58.41968F);
            this.xrLabel98.StylePriority.UseFont = false;
            this.xrLabel98.StylePriority.UseTextAlignment = false;
            this.xrLabel98.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel97
            // 
            this.xrLabel97.Dpi = 254F;
            this.xrLabel97.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel97.LocationFloat = new DevExpress.Utils.PointFloat(782.0134F, 2279.557F);
            this.xrLabel97.Name = "xrLabel97";
            this.xrLabel97.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel97.SizeF = new System.Drawing.SizeF(174.0076F, 58.41968F);
            this.xrLabel97.StylePriority.UseFont = false;
            this.xrLabel97.StylePriority.UseTextAlignment = false;
            this.xrLabel97.Text = "Bậc lương";
            this.xrLabel97.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel96
            // 
            this.xrLabel96.Dpi = 254F;
            this.xrLabel96.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel96.LocationFloat = new DevExpress.Utils.PointFloat(639.5164F, 2279.557F);
            this.xrLabel96.Name = "xrLabel96";
            this.xrLabel96.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel96.SizeF = new System.Drawing.SizeF(142.497F, 58.41968F);
            this.xrLabel96.StylePriority.UseFont = false;
            this.xrLabel96.StylePriority.UseTextAlignment = false;
            this.xrLabel96.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel95
            // 
            this.xrLabel95.Dpi = 254F;
            this.xrLabel95.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel95.LocationFloat = new DevExpress.Utils.PointFloat(468.3821F, 2278.556F);
            this.xrLabel95.Name = "xrLabel95";
            this.xrLabel95.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel95.SizeF = new System.Drawing.SizeF(163.45F, 58.41968F);
            this.xrLabel95.StylePriority.UseFont = false;
            this.xrLabel95.StylePriority.UseTextAlignment = false;
            this.xrLabel95.Text = "(mã số)";
            this.xrLabel95.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel94
            // 
            this.xrLabel94.Dpi = 254F;
            this.xrLabel94.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel94.LocationFloat = new DevExpress.Utils.PointFloat(358.8941F, 2279.557F);
            this.xrLabel94.Name = "xrLabel94";
            this.xrLabel94.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel94.SizeF = new System.Drawing.SizeF(103.3946F, 58.41968F);
            this.xrLabel94.StylePriority.UseFont = false;
            this.xrLabel94.StylePriority.UseTextAlignment = false;
            this.xrLabel94.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel92
            // 
            this.xrLabel92.Dpi = 254F;
            this.xrLabel92.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel92.LocationFloat = new DevExpress.Utils.PointFloat(5.076887F, 2279.556F);
            this.xrLabel92.Name = "xrLabel92";
            this.xrLabel92.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel92.SizeF = new System.Drawing.SizeF(347.1691F, 58.41992F);
            this.xrLabel92.StylePriority.UseFont = false;
            this.xrLabel92.StylePriority.UseTextAlignment = false;
            this.xrLabel92.Text = "19) Ngạch công chức";
            this.xrLabel92.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_AssignedWork
            // 
            this.xrl_AssignedWork.Dpi = 254F;
            this.xrl_AssignedWork.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_AssignedWork.LocationFloat = new DevExpress.Utils.PointFloat(584.391F, 2207.598F);
            this.xrl_AssignedWork.Name = "xrl_AssignedWork";
            this.xrl_AssignedWork.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_AssignedWork.SizeF = new System.Drawing.SizeF(1099.596F, 58.42017F);
            this.xrl_AssignedWork.StylePriority.UseFont = false;
            this.xrl_AssignedWork.StylePriority.UseTextAlignment = false;
            this.xrl_AssignedWork.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel42
            // 
            this.xrLabel42.Dpi = 254F;
            this.xrLabel42.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel42.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2207.598F);
            this.xrLabel42.Name = "xrLabel42";
            this.xrLabel42.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel42.SizeF = new System.Drawing.SizeF(531.0181F, 58.41992F);
            this.xrLabel42.StylePriority.UseFont = false;
            this.xrLabel42.StylePriority.UseTextAlignment = false;
            this.xrLabel42.Text = "18) Công tác chính đang làm";
            this.xrLabel42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel38
            // 
            this.xrLabel38.Dpi = 254F;
            this.xrLabel38.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(782.0133F, 1997.351F);
            this.xrLabel38.Name = "xrLabel38";
            this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel38.SizeF = new System.Drawing.SizeF(885.8577F, 41.14917F);
            this.xrLabel38.StylePriority.UseFont = false;
            this.xrLabel38.StylePriority.UseTextAlignment = false;
            this.xrLabel38.Text = "(GS, PGS, TS, PTS, Thạc sĩ, Cử nhân, Kỹ sư ... năm nào, chuyên ngành gì)";
            this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel41
            // 
            this.xrLabel41.Dpi = 254F;
            this.xrLabel41.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel41.LocationFloat = new DevExpress.Utils.PointFloat(584.391F, 1994.747F);
            this.xrLabel41.Name = "xrLabel41";
            this.xrLabel41.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel41.SizeF = new System.Drawing.SizeF(113.5871F, 42.54541F);
            this.xrLabel41.StylePriority.UseFont = false;
            this.xrLabel41.StylePriority.UseTextAlignment = false;
            this.xrLabel41.Text = "Lớp mấy";
            this.xrLabel41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl_BasicEducation
            // 
            this.lbl_BasicEducation.Dpi = 254F;
            this.lbl_BasicEducation.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BasicEducation.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1936.327F);
            this.lbl_BasicEducation.Name = "lbl_BasicEducation";
            this.lbl_BasicEducation.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbl_BasicEducation.SizeF = new System.Drawing.SizeF(687.0001F, 58.42004F);
            this.lbl_BasicEducation.StylePriority.UseFont = false;
            this.lbl_BasicEducation.StylePriority.UseTextAlignment = false;
            this.lbl_BasicEducation.Text = "17) Trình độ học vấn: Giáo dục phổ thông";
            this.lbl_BasicEducation.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_BasicEducationName
            // 
            this.xrl_BasicEducationName.Dpi = 254F;
            this.xrl_BasicEducationName.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_BasicEducationName.LocationFloat = new DevExpress.Utils.PointFloat(708.2708F, 1938.931F);
            this.xrl_BasicEducationName.Name = "xrl_BasicEducationName";
            this.xrl_BasicEducationName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_BasicEducationName.SizeF = new System.Drawing.SizeF(162.7056F, 58.42004F);
            this.xrl_BasicEducationName.StylePriority.UseFont = false;
            this.xrl_BasicEducationName.StylePriority.UseTextAlignment = false;
            this.xrl_BasicEducationName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel37
            // 
            this.xrLabel37.Dpi = 254F;
            this.xrLabel37.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(893.4739F, 1936.327F);
            this.xrLabel37.Name = "xrLabel37";
            this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel37.SizeF = new System.Drawing.SizeF(469.6862F, 58.42004F);
            this.xrLabel37.StylePriority.UseFont = false;
            this.xrLabel37.StylePriority.UseTextAlignment = false;
            this.xrLabel37.Text = ", Học hàm, học vị cao nhất:";
            this.xrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel91
            // 
            this.xrLabel91.Dpi = 254F;
            this.xrLabel91.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel91.LocationFloat = new DevExpress.Utils.PointFloat(967.3702F, 1844.332F);
            this.xrLabel91.Name = "xrLabel91";
            this.xrLabel91.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel91.SizeF = new System.Drawing.SizeF(479.4885F, 58.42017F);
            this.xrLabel91.StylePriority.UseFont = false;
            this.xrLabel91.StylePriority.UseTextAlignment = false;
            this.xrLabel91.Text = "Quân hàm, chức vụ cao nhất";
            this.xrLabel91.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_ArmyLeftDate
            // 
            this.xrl_ArmyLeftDate.Dpi = 254F;
            this.xrl_ArmyLeftDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_ArmyLeftDate.LocationFloat = new DevExpress.Utils.PointFloat(769.6578F, 1844.332F);
            this.xrl_ArmyLeftDate.Name = "xrl_ArmyLeftDate";
            this.xrl_ArmyLeftDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_ArmyLeftDate.SizeF = new System.Drawing.SizeF(197.7125F, 58.41992F);
            this.xrl_ArmyLeftDate.StylePriority.UseFont = false;
            this.xrl_ArmyLeftDate.StylePriority.UseTextAlignment = false;
            this.xrl_ArmyLeftDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel89
            // 
            this.xrLabel89.Dpi = 254F;
            this.xrLabel89.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel89.LocationFloat = new DevExpress.Utils.PointFloat(518.6221F, 1844.332F);
            this.xrLabel89.Name = "xrLabel89";
            this.xrLabel89.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel89.SizeF = new System.Drawing.SizeF(239.4418F, 58.4198F);
            this.xrLabel89.StylePriority.UseFont = false;
            this.xrLabel89.StylePriority.UseTextAlignment = false;
            this.xrLabel89.Text = "Ngày xuất ngũ";
            this.xrLabel89.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Dpi = 254F;
            this.xrLabel15.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1695.797F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(768.5208F, 58.42004F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "15) Ngày tham gia các tổ chức chính trị, xã hội:";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_PoliticDate
            // 
            this.xrl_PoliticDate.Dpi = 254F;
            this.xrl_PoliticDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_PoliticDate.LocationFloat = new DevExpress.Utils.PointFloat(801.0209F, 1695.797F);
            this.xrl_PoliticDate.Name = "xrl_PoliticDate";
            this.xrl_PoliticDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_PoliticDate.SizeF = new System.Drawing.SizeF(879.9032F, 58.42004F);
            this.xrl_PoliticDate.StylePriority.UseFont = false;
            this.xrl_PoliticDate.StylePriority.UseTextAlignment = false;
            this.xrl_PoliticDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Dpi = 254F;
            this.xrLabel13.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1610.89F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(631.8321F, 58.42004F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "14) Ngày vào đảng cộng sản Việt Nam:";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_CPVJoinedDate
            // 
            this.xrl_CPVJoinedDate.Dpi = 254F;
            this.xrl_CPVJoinedDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_CPVJoinedDate.LocationFloat = new DevExpress.Utils.PointFloat(642.9646F, 1610.89F);
            this.xrl_CPVJoinedDate.Name = "xrl_CPVJoinedDate";
            this.xrl_CPVJoinedDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_CPVJoinedDate.SizeF = new System.Drawing.SizeF(228.0118F, 58.42004F);
            this.xrl_CPVJoinedDate.StylePriority.UseFont = false;
            this.xrl_CPVJoinedDate.StylePriority.UseTextAlignment = false;
            this.xrl_CPVJoinedDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel36
            // 
            this.xrLabel36.Dpi = 254F;
            this.xrLabel36.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(888.8557F, 1610.89F);
            this.xrLabel36.Name = "xrLabel36";
            this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel36.SizeF = new System.Drawing.SizeF(309.8786F, 58.42004F);
            this.xrLabel36.StylePriority.UseFont = false;
            this.xrLabel36.StylePriority.UseTextAlignment = false;
            this.xrLabel36.Text = ", Ngày chính thức:";
            this.xrLabel36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_CPVOfficialJoinedDate
            // 
            this.xrl_CPVOfficialJoinedDate.Dpi = 254F;
            this.xrl_CPVOfficialJoinedDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_CPVOfficialJoinedDate.LocationFloat = new DevExpress.Utils.PointFloat(1239.004F, 1610.889F);
            this.xrl_CPVOfficialJoinedDate.Name = "xrl_CPVOfficialJoinedDate";
            this.xrl_CPVOfficialJoinedDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_CPVOfficialJoinedDate.SizeF = new System.Drawing.SizeF(446.9974F, 58.42004F);
            this.xrl_CPVOfficialJoinedDate.StylePriority.UseFont = false;
            this.xrl_CPVOfficialJoinedDate.StylePriority.UseTextAlignment = false;
            this.xrl_CPVOfficialJoinedDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_RecruimentDate
            // 
            this.xrl_RecruimentDate.Dpi = 254F;
            this.xrl_RecruimentDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_RecruimentDate.LocationFloat = new DevExpress.Utils.PointFloat(449.0001F, 1431.425F);
            this.xrl_RecruimentDate.Name = "xrl_RecruimentDate";
            this.xrl_RecruimentDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_RecruimentDate.SizeF = new System.Drawing.SizeF(254.2707F, 58.42004F);
            this.xrl_RecruimentDate.StylePriority.UseFont = false;
            this.xrl_RecruimentDate.StylePriority.UseTextAlignment = false;
            this.xrl_RecruimentDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_FunctionaryDate
            // 
            this.xrl_FunctionaryDate.Dpi = 254F;
            this.xrl_FunctionaryDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_FunctionaryDate.LocationFloat = new DevExpress.Utils.PointFloat(687.0001F, 1520.931F);
            this.xrl_FunctionaryDate.Name = "xrl_FunctionaryDate";
            this.xrl_FunctionaryDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_FunctionaryDate.SizeF = new System.Drawing.SizeF(228.0118F, 58.42004F);
            this.xrl_FunctionaryDate.StylePriority.UseFont = false;
            this.xrl_FunctionaryDate.StylePriority.UseTextAlignment = false;
            this.xrl_FunctionaryDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel87
            // 
            this.xrLabel87.Dpi = 254F;
            this.xrLabel87.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel87.LocationFloat = new DevExpress.Utils.PointFloat(924.0505F, 1520.931F);
            this.xrLabel87.Name = "xrLabel87";
            this.xrLabel87.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel87.SizeF = new System.Drawing.SizeF(447.5464F, 58.42004F);
            this.xrLabel87.StylePriority.UseFont = false;
            this.xrLabel87.StylePriority.UseTextAlignment = false;
            this.xrLabel87.Text = ", Ngày tham gia cách mạng:";
            this.xrLabel87.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_RecruimentDepartment
            // 
            this.xrl_RecruimentDepartment.Dpi = 254F;
            this.xrl_RecruimentDepartment.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_RecruimentDepartment.LocationFloat = new DevExpress.Utils.PointFloat(1116.116F, 1434.008F);
            this.xrl_RecruimentDepartment.Name = "xrl_RecruimentDepartment";
            this.xrl_RecruimentDepartment.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_RecruimentDepartment.SizeF = new System.Drawing.SizeF(573.8854F, 58.42004F);
            this.xrl_RecruimentDepartment.StylePriority.UseFont = false;
            this.xrl_RecruimentDepartment.StylePriority.UseTextAlignment = false;
            this.xrl_RecruimentDepartment.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel82
            // 
            this.xrLabel82.Dpi = 254F;
            this.xrLabel82.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel82.LocationFloat = new DevExpress.Utils.PointFloat(708.8967F, 1431.425F);
            this.xrLabel82.Name = "xrLabel82";
            this.xrLabel82.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel82.SizeF = new System.Drawing.SizeF(398.4147F, 58.42004F);
            this.xrLabel82.StylePriority.UseFont = false;
            this.xrLabel82.StylePriority.UseTextAlignment = false;
            this.xrLabel82.Text = "Vào cơ quan nào, ở đâu:";
            this.xrLabel82.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Dpi = 254F;
            this.xrLabel10.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1434.008F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(439.7123F, 58.42004F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "12) Ngày được tuyển dụng";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel75
            // 
            this.xrLabel75.Dpi = 254F;
            this.xrLabel75.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel75.LocationFloat = new DevExpress.Utils.PointFloat(656.55F, 1350.829F);
            this.xrLabel75.Name = "xrLabel75";
            this.xrLabel75.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel75.SizeF = new System.Drawing.SizeF(1027.438F, 45.19092F);
            this.xrLabel75.StylePriority.UseFont = false;
            this.xrLabel75.StylePriority.UseTextAlignment = false;
            this.xrLabel75.Text = "(Ghi nghề được đào tạo hoặc công nhân (thợ gì), làm ruộng, buôn bán, học sinh...)" +
    "";
            this.xrLabel75.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Dpi = 254F;
            this.xrLabel12.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(3.479116F, 1291.567F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(1027.438F, 58.42004F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "11) Nghề nghiệp bản thân trước khi được tuyển dụng:";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_FamilyClassName
            // 
            this.xrl_FamilyClassName.Dpi = 254F;
            this.xrl_FamilyClassName.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_FamilyClassName.LocationFloat = new DevExpress.Utils.PointFloat(584.391F, 1158.005F);
            this.xrl_FamilyClassName.Name = "xrl_FamilyClassName";
            this.xrl_FamilyClassName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_FamilyClassName.SizeF = new System.Drawing.SizeF(1077.792F, 58.42004F);
            this.xrl_FamilyClassName.StylePriority.UseFont = false;
            this.xrl_FamilyClassName.StylePriority.UseTextAlignment = false;
            this.xrl_FamilyClassName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Dpi = 254F;
            this.xrLabel8.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(5.082054F, 1158.005F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(567.4374F, 58.42004F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "10) Thành phần gia đình xuất thân:";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_Religiousness
            // 
            this.xrl_Religiousness.Dpi = 254F;
            this.xrl_Religiousness.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_Religiousness.LocationFloat = new DevExpress.Utils.PointFloat(1181.191F, 1077.299F);
            this.xrl_Religiousness.Name = "xrl_Religiousness";
            this.xrl_Religiousness.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_Religiousness.SizeF = new System.Drawing.SizeF(495.9263F, 58.41992F);
            this.xrl_Religiousness.StylePriority.UseFont = false;
            this.xrl_Religiousness.StylePriority.UseTextAlignment = false;
            this.xrl_Religiousness.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel79
            // 
            this.xrLabel79.Dpi = 254F;
            this.xrLabel79.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel79.LocationFloat = new DevExpress.Utils.PointFloat(967.3702F, 1077.299F);
            this.xrLabel79.Name = "xrLabel79";
            this.xrLabel79.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel79.SizeF = new System.Drawing.SizeF(193.4884F, 58.41998F);
            this.xrLabel79.StylePriority.UseFont = false;
            this.xrLabel79.StylePriority.UseTextAlignment = false;
            this.xrLabel79.Text = "9) Tôn giáo";
            this.xrLabel79.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_Folk
            // 
            this.xrl_Folk.Dpi = 254F;
            this.xrl_Folk.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_Folk.LocationFloat = new DevExpress.Utils.PointFloat(570.7413F, 1077.299F);
            this.xrl_Folk.Name = "xrl_Folk";
            this.xrl_Folk.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_Folk.SizeF = new System.Drawing.SizeF(366.847F, 58.42004F);
            this.xrl_Folk.StylePriority.UseFont = false;
            this.xrl_Folk.StylePriority.UseTextAlignment = false;
            this.xrl_Folk.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel77
            // 
            this.xrLabel77.Dpi = 254F;
            this.xrLabel77.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel77.LocationFloat = new DevExpress.Utils.PointFloat(222.2448F, 1077.299F);
            this.xrLabel77.Name = "xrLabel77";
            this.xrLabel77.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel77.SizeF = new System.Drawing.SizeF(320.5638F, 58.42004F);
            this.xrLabel77.StylePriority.UseFont = false;
            this.xrLabel77.StylePriority.UseTextAlignment = false;
            this.xrLabel77.Text = "(Kinh, Tày, Mông, Ê đê...)";
            this.xrLabel77.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_ResidentPlace
            // 
            this.xrl_ResidentPlace.Dpi = 254F;
            this.xrl_ResidentPlace.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_ResidentPlace.LocationFloat = new DevExpress.Utils.PointFloat(0F, 949.7747F);
            this.xrl_ResidentPlace.Name = "xrl_ResidentPlace";
            this.xrl_ResidentPlace.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_ResidentPlace.SizeF = new System.Drawing.SizeF(1675.898F, 58.42004F);
            this.xrl_ResidentPlace.StylePriority.UseFont = false;
            this.xrl_ResidentPlace.StylePriority.UseTextAlignment = false;
            this.xrl_ResidentPlace.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Dpi = 254F;
            this.xrLabel14.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(258.7549F, 1006.195F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(680.1121F, 50.48248F);
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.Text = "(Số nhà, đường phố, thành phố, xóm, thôn, xã, huyện, tỉnh)";
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(656.55F, 875.7715F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(197.7277F, 58.42004F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Điện thoại";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel74
            // 
            this.xrLabel74.Dpi = 254F;
            this.xrLabel74.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel74.LocationFloat = new DevExpress.Utils.PointFloat(301.429F, 806.7676F);
            this.xrLabel74.Name = "xrLabel74";
            this.xrLabel74.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel74.SizeF = new System.Drawing.SizeF(527.9045F, 58.42004F);
            this.xrLabel74.StylePriority.UseFont = false;
            this.xrLabel74.StylePriority.UseTextAlignment = false;
            this.xrLabel74.Text = "(xã, huyện, tỉnh hoặc số nhà, đường phố, TP)";
            this.xrLabel74.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_BirthPlace
            // 
            this.xrl_BirthPlace.Dpi = 254F;
            this.xrl_BirthPlace.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_BirthPlace.LocationFloat = new DevExpress.Utils.PointFloat(610.6281F, 679.3444F);
            this.xrl_BirthPlace.Name = "xrl_BirthPlace";
            this.xrl_BirthPlace.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_BirthPlace.SizeF = new System.Drawing.SizeF(1070F, 57.00323F);
            this.xrl_BirthPlace.StylePriority.UseFont = false;
            this.xrl_BirthPlace.StylePriority.UseTextAlignment = false;
            this.xrl_BirthPlace.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_Allowance
            // 
            this.xrl_Allowance.Dpi = 254F;
            this.xrl_Allowance.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_Allowance.LocationFloat = new DevExpress.Utils.PointFloat(656.55F, 600.8575F);
            this.xrl_Allowance.Name = "xrl_Allowance";
            this.xrl_Allowance.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_Allowance.SizeF = new System.Drawing.SizeF(1020.158F, 58.41998F);
            this.xrl_Allowance.StylePriority.UseFont = false;
            this.xrl_Allowance.StylePriority.UseTextAlignment = false;
            this.xrl_Allowance.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel59
            // 
            this.xrLabel59.Dpi = 254F;
            this.xrLabel59.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel59.LocationFloat = new DevExpress.Utils.PointFloat(301.4296F, 600.8575F);
            this.xrLabel59.Name = "xrLabel59";
            this.xrLabel59.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel59.SizeF = new System.Drawing.SizeF(318.7123F, 58.41998F);
            this.xrLabel59.StylePriority.UseFont = false;
            this.xrLabel59.StylePriority.UseTextAlignment = false;
            this.xrLabel59.Text = "Phụ cấp chức vụ:";
            this.xrLabel59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_PositionName
            // 
            this.xrl_PositionName.Dpi = 254F;
            this.xrl_PositionName.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_PositionName.LocationFloat = new DevExpress.Utils.PointFloat(1185.898F, 533.5084F);
            this.xrl_PositionName.Name = "xrl_PositionName";
            this.xrl_PositionName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_PositionName.SizeF = new System.Drawing.SizeF(490F, 58.42004F);
            this.xrl_PositionName.StylePriority.UseFont = false;
            this.xrl_PositionName.StylePriority.UseTextAlignment = false;
            this.xrl_PositionName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel57
            // 
            this.xrLabel57.Dpi = 254F;
            this.xrLabel57.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel57.LocationFloat = new DevExpress.Utils.PointFloat(301.4293F, 533.5083F);
            this.xrLabel57.Name = "xrLabel57";
            this.xrLabel57.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel57.SizeF = new System.Drawing.SizeF(154.2112F, 58.42004F);
            this.xrLabel57.StylePriority.UseFont = false;
            this.xrLabel57.StylePriority.UseTextAlignment = false;
            this.xrLabel57.Text = "Chức vụ:";
            this.xrLabel57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel56
            // 
            this.xrLabel56.Dpi = 254F;
            this.xrLabel56.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel56.LocationFloat = new DevExpress.Utils.PointFloat(466.0001F, 533.5084F);
            this.xrLabel56.Name = "xrLabel56";
            this.xrLabel56.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel56.SizeF = new System.Drawing.SizeF(679.2482F, 58.41986F);
            this.xrLabel56.StylePriority.UseFont = false;
            this.xrLabel56.StylePriority.UseTextAlignment = false;
            this.xrLabel56.Text = " (Đảng, đoàn thể, Chính quyền, kể cả chức vụ kiêm nhiệm)";
            this.xrLabel56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_Committees
            // 
            this.xrl_Committees.Dpi = 254F;
            this.xrl_Committees.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_Committees.LocationFloat = new DevExpress.Utils.PointFloat(1305.612F, 457.0883F);
            this.xrl_Committees.Name = "xrl_Committees";
            this.xrl_Committees.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_Committees.SizeF = new System.Drawing.SizeF(370F, 58.42004F);
            this.xrl_Committees.StylePriority.UseFont = false;
            this.xrl_Committees.StylePriority.UseTextAlignment = false;
            this.xrl_Committees.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel32
            // 
            this.xrLabel32.Dpi = 254F;
            this.xrLabel32.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(999.3124F, 457.0884F);
            this.xrLabel32.Name = "xrLabel32";
            this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel32.SizeF = new System.Drawing.SizeF(306.2998F, 58.42004F);
            this.xrLabel32.StylePriority.UseFont = false;
            this.xrLabel32.StylePriority.UseTextAlignment = false;
            this.xrLabel32.Text = ",  Cấp ủy kiêm:  ";
            this.xrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_Sex
            // 
            this.xrl_Sex.Dpi = 254F;
            this.xrl_Sex.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_Sex.LocationFloat = new DevExpress.Utils.PointFloat(1338.653F, 302.8341F);
            this.xrl_Sex.Name = "xrl_Sex";
            this.xrl_Sex.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_Sex.SizeF = new System.Drawing.SizeF(340F, 58.41995F);
            this.xrl_Sex.StylePriority.UseFont = false;
            this.xrl_Sex.StylePriority.UseTextAlignment = false;
            this.xrl_Sex.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_FullName
            // 
            this.xrl_FullName.Dpi = 254F;
            this.xrl_FullName.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_FullName.LocationFloat = new DevExpress.Utils.PointFloat(687.0001F, 302.834F);
            this.xrl_FullName.Name = "xrl_FullName";
            this.xrl_FullName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_FullName.SizeF = new System.Drawing.SizeF(411.518F, 58.41995F);
            this.xrl_FullName.StylePriority.UseFont = false;
            this.xrl_FullName.StylePriority.UseTextAlignment = false;
            this.xrl_FullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel55
            // 
            this.xrLabel55.Dpi = 254F;
            this.xrLabel55.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel55.LocationFloat = new DevExpress.Utils.PointFloat(1260.022F, 48.28825F);
            this.xrLabel55.Name = "xrLabel55";
            this.xrLabel55.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel55.SizeF = new System.Drawing.SizeF(355.1089F, 58.42004F);
            this.xrLabel55.StylePriority.UseFont = false;
            this.xrLabel55.StylePriority.UseTextAlignment = false;
            this.xrLabel55.Text = "Mẫu 2C/TCTW-98";
            this.xrLabel55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel53
            // 
            this.xrLabel53.Dpi = 254F;
            this.xrLabel53.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel53.LocationFloat = new DevExpress.Utils.PointFloat(1117.147F, 217.9683F);
            this.xrLabel53.Name = "xrLabel53";
            this.xrLabel53.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel53.SizeF = new System.Drawing.SizeF(497.9839F, 58.42004F);
            this.xrLabel53.StylePriority.UseFont = false;
            this.xrLabel53.StylePriority.UseTextAlignment = false;
            this.xrLabel53.Text = "Số hiệu cán bộ, công chức";
            this.xrLabel53.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Dpi = 254F;
            this.xrLabel7.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(5.082054F, 1077.299F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(193.3503F, 58.41992F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "8) Dân tộc:";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_DisciplineName
            // 
            this.xrl_DisciplineName.Dpi = 254F;
            this.xrl_DisciplineName.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_DisciplineName.LocationFloat = new DevExpress.Utils.PointFloat(1164.406F, 2736.895F);
            this.xrl_DisciplineName.Name = "xrl_DisciplineName";
            this.xrl_DisciplineName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_DisciplineName.SizeF = new System.Drawing.SizeF(485.725F, 58.42041F);
            this.xrl_DisciplineName.StylePriority.UseFont = false;
            this.xrl_DisciplineName.StylePriority.UseTextAlignment = false;
            this.xrl_DisciplineName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Dpi = 254F;
            this.xrLabel6.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(5.076887F, 2737.666F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(238.5026F, 58.42041F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "23) Kỷ luật:    ";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_IDNumber
            // 
            this.xrl_IDNumber.Dpi = 254F;
            this.xrl_IDNumber.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_IDNumber.LocationFloat = new DevExpress.Utils.PointFloat(352.2459F, 2953.459F);
            this.xrl_IDNumber.Name = "xrl_IDNumber";
            this.xrl_IDNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_IDNumber.SizeF = new System.Drawing.SizeF(200.7721F, 58.41992F);
            this.xrl_IDNumber.StylePriority.UseFont = false;
            this.xrl_IDNumber.StylePriority.UseTextAlignment = false;
            this.xrl_IDNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel25
            // 
            this.xrLabel25.Dpi = 254F;
            this.xrLabel25.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(573.2913F, 2954.042F);
            this.xrLabel25.Name = "xrLabel25";
            this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel25.SizeF = new System.Drawing.SizeF(297.6851F, 58.42017F);
            this.xrLabel25.StylePriority.UseFont = false;
            this.xrLabel25.StylePriority.UseTextAlignment = false;
            this.xrLabel25.Text = "Thương binh loại:";
            this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel23
            // 
            this.xrLabel23.Dpi = 254F;
            this.xrLabel23.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(454.0001F, 2814.086F);
            this.xrLabel23.Name = "xrLabel23";
            this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel23.SizeF = new System.Drawing.SizeF(233F, 58.41968F);
            this.xrLabel23.StylePriority.UseFont = false;
            this.xrLabel23.StylePriority.UseTextAlignment = false;
            this.xrLabel23.Text = "Bình thường";
            this.xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_Weight
            // 
            this.xrl_Weight.Dpi = 254F;
            this.xrl_Weight.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_Weight.LocationFloat = new DevExpress.Utils.PointFloat(1264F, 2814.086F);
            this.xrl_Weight.Name = "xrl_Weight";
            this.xrl_Weight.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_Weight.SizeF = new System.Drawing.SizeF(104F, 58.41968F);
            this.xrl_Weight.StylePriority.UseFont = false;
            this.xrl_Weight.StylePriority.UseTextAlignment = false;
            this.xrl_Weight.Text = "70kg";
            this.xrl_Weight.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_BloodGroup
            // 
            this.xrl_BloodGroup.Dpi = 254F;
            this.xrl_BloodGroup.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_BloodGroup.LocationFloat = new DevExpress.Utils.PointFloat(1615.131F, 2814.086F);
            this.xrl_BloodGroup.Name = "xrl_BloodGroup";
            this.xrl_BloodGroup.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_BloodGroup.SizeF = new System.Drawing.SizeF(73.87F, 58.41992F);
            this.xrl_BloodGroup.StylePriority.UseFont = false;
            this.xrl_BloodGroup.StylePriority.UseTextAlignment = false;
            this.xrl_BloodGroup.Text = "AB";
            this.xrl_BloodGroup.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_Height
            // 
            this.xrl_Height.Dpi = 254F;
            this.xrl_Height.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_Height.LocationFloat = new DevExpress.Utils.PointFloat(914.0002F, 2814.086F);
            this.xrl_Height.Name = "xrl_Height";
            this.xrl_Height.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_Height.SizeF = new System.Drawing.SizeF(133F, 58.41992F);
            this.xrl_Height.StylePriority.UseFont = false;
            this.xrl_Height.StylePriority.UseTextAlignment = false;
            this.xrl_Height.Text = "165cm";
            this.xrl_Height.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_CAN_NANG
            // 
            this.xrl_CAN_NANG.Dpi = 254F;
            this.xrl_CAN_NANG.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_CAN_NANG.LocationFloat = new DevExpress.Utils.PointFloat(1047F, 2814.086F);
            this.xrl_CAN_NANG.Name = "xrl_CAN_NANG";
            this.xrl_CAN_NANG.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_CAN_NANG.SizeF = new System.Drawing.SizeF(217F, 58.41992F);
            this.xrl_CAN_NANG.StylePriority.UseFont = false;
            this.xrl_CAN_NANG.StylePriority.UseTextAlignment = false;
            this.xrl_CAN_NANG.Text = ", Cân nặng:                  ";
            this.xrl_CAN_NANG.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_CHIEU_CAO
            // 
            this.xrl_CHIEU_CAO.Dpi = 254F;
            this.xrl_CHIEU_CAO.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_CHIEU_CAO.LocationFloat = new DevExpress.Utils.PointFloat(687.0001F, 2814.086F);
            this.xrl_CHIEU_CAO.Name = "xrl_CHIEU_CAO";
            this.xrl_CHIEU_CAO.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_CHIEU_CAO.SizeF = new System.Drawing.SizeF(227F, 58.41968F);
            this.xrl_CHIEU_CAO.StylePriority.UseFont = false;
            this.xrl_CHIEU_CAO.StylePriority.UseTextAlignment = false;
            this.xrl_CHIEU_CAO.Text = ",Cao:                        ";
            this.xrl_CHIEU_CAO.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NHOM_MAU
            // 
            this.xrl_NHOM_MAU.Dpi = 254F;
            this.xrl_NHOM_MAU.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NHOM_MAU.LocationFloat = new DevExpress.Utils.PointFloat(1368.131F, 2814.086F);
            this.xrl_NHOM_MAU.Name = "xrl_NHOM_MAU";
            this.xrl_NHOM_MAU.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NHOM_MAU.SizeF = new System.Drawing.SizeF(247F, 58.41968F);
            this.xrl_NHOM_MAU.StylePriority.UseFont = false;
            this.xrl_NHOM_MAU.StylePriority.UseTextAlignment = false;
            this.xrl_NHOM_MAU.Text = ", Nhóm máu: ";
            this.xrl_NHOM_MAU.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_TT_SUCKHOE
            // 
            this.xrl_TT_SUCKHOE.Dpi = 254F;
            this.xrl_TT_SUCKHOE.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_TT_SUCKHOE.LocationFloat = new DevExpress.Utils.PointFloat(5.382962E-05F, 2814.086F);
            this.xrl_TT_SUCKHOE.Name = "xrl_TT_SUCKHOE";
            this.xrl_TT_SUCKHOE.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_TT_SUCKHOE.SizeF = new System.Drawing.SizeF(454F, 58.41968F);
            this.xrl_TT_SUCKHOE.StylePriority.UseFont = false;
            this.xrl_TT_SUCKHOE.StylePriority.UseTextAlignment = false;
            this.xrl_TT_SUCKHOE.Text = "24) Tình trạng sức khỏe:    ";
            this.xrl_TT_SUCKHOE.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_SO_CMND
            // 
            this.xrl_SO_CMND.Dpi = 254F;
            this.xrl_SO_CMND.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_SO_CMND.LocationFloat = new DevExpress.Utils.PointFloat(5.076887F, 2953.459F);
            this.xrl_SO_CMND.Name = "xrl_SO_CMND";
            this.xrl_SO_CMND.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_SO_CMND.SizeF = new System.Drawing.SizeF(339.6394F, 58.41992F);
            this.xrl_SO_CMND.StylePriority.UseFont = false;
            this.xrl_SO_CMND.StylePriority.UseTextAlignment = false;
            this.xrl_SO_CMND.Text = "25) Số CMTND:         ";
            this.xrl_SO_CMND.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_sub_TrainingProcess
            // 
            this.xrl_sub_TrainingProcess.Dpi = 254F;
            this.xrl_sub_TrainingProcess.LocationFloat = new DevExpress.Utils.PointFloat(5.382962E-05F, 3084.299F);
            this.xrl_sub_TrainingProcess.Name = "xrl_sub_TrainingProcess";
            this.xrl_sub_TrainingProcess.ReportSource = new Web.Core.Framework.Report.sub_ProcessTraining();
            this.xrl_sub_TrainingProcess.SizeF = new System.Drawing.SizeF(1689.999F, 58.41992F);
            // 
            // xrl_sub_ProcessWorking
            // 
            this.xrl_sub_ProcessWorking.Dpi = 254F;
            this.xrl_sub_ProcessWorking.LocationFloat = new DevExpress.Utils.PointFloat(5.382962E-05F, 3142.719F);
            this.xrl_sub_ProcessWorking.Name = "xrl_sub_ProcessWorking";
            this.xrl_sub_ProcessWorking.ReportSource = new Web.Core.Framework.Report.sub_ProcessWorking();
            this.xrl_sub_ProcessWorking.SizeF = new System.Drawing.SizeF(1689.999F, 58.41968F);
            // 
            // xrl_subProcessSalary
            // 
            this.xrl_subProcessSalary.Dpi = 254F;
            this.xrl_subProcessSalary.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4708.041F);
            this.xrl_subProcessSalary.Name = "xrl_subProcessSalary";
            this.xrl_subProcessSalary.ReportSource = new Web.Core.Framework.Report.sub_ProcessSalary();
            this.xrl_subProcessSalary.SizeF = new System.Drawing.SizeF(1658.218F, 111.3369F);
            // 
            // xrl_sub_FamilyHusband
            // 
            this.xrl_sub_FamilyHusband.Dpi = 254F;
            this.xrl_sub_FamilyHusband.LocationFloat = new DevExpress.Utils.PointFloat(18.89161F, 4309.581F);
            this.xrl_sub_FamilyHusband.Name = "xrl_sub_FamilyHusband";
            this.xrl_sub_FamilyHusband.ReportSource = new Web.Core.Framework.Report.sub_FamilyRelationship();
            this.xrl_sub_FamilyHusband.SizeF = new System.Drawing.SizeF(1640F, 111.7598F);
            // 
            // rpt_InformationEmployeeDetail_V2
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader});
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(246, 164, 155, 192);
            this.PageHeight = 2970;
            this.PageWidth = 2100;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private void xrl_NoiSinh_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }

}
