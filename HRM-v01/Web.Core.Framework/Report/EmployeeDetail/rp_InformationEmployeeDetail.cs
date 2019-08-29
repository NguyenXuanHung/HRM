using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Interface;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Summary description for rpt_InformationEmployeeDetail
    /// </summary>
    public class rp_InformationEmployeeDetail : XtraReport
    {
        private DetailBand Detail;
        private TopMarginBand TopMargin;
        private BottomMarginBand BottomMargin;
        private XRLabel xrLabel5;
        private XRPictureBox xrPictureBox1;
        private XRLabel xrl_TEN_KHAC;
        private XRLabel xrl_NGAY_SINH;
        private XRLabel xrl_HO_TEN;
        private XRLabel xrl_TON_GIAO;
        private XRLabel xrl_NoiSinh;
        private XRLabel xrl_BI_DANH;
        private XRLabel xrl_QueQuan;
        private XRLabel xrl_SHCCVC;
        private XRLabel xrl_DAN_TOC;
        private XRCheckBox xrc_NAM;
        private XRLabel xrl_CQDVCTQ;
        private XRCheckBox xrc_NU;
        private XRLabel xrl_CQDVSD;
        private XRLabel xrl_NGAYSINH;
        private XRLabel xrl_GIOI_TINH;
        private XRLabel xrlHOTEN_CBCC;
        private XRLabel xrlDanTOC;
        private XRLabel xrlTONGIAO;
        private XRLabel xrLabel2;
        private XRLabel xrl_ThanhPhanBT;
        private XRLabel xrLabel14;
        private XRLabel xrlTPGD;
        private XRLabel xrlHOKHAUTT;
        private XRLabel xrLabel7;
        private XRLabel xrLabel32;
        private XRLabel xrl_CQTDDAUTIEN;
        private XRLabel xrl_NGAY_TUYEN_DAU;
        private XRLabel xrl_CHUCVU;
        private XRLabel xrl_TD_QUANLY;
        private XRLabel xrl_CVChinhDuocGiao;
        private XRLabel xrl_TINHOC;
        private XRLabel xrl_NGOAI_NGU;
        private XRLabel xrl_TDQLNN;
        private XRLabel xrl_TD_CHINHTRI;
        private XRLabel xrl_NGAY_TUYEN_DAUTIEN;
        private XRLabel xrl_TDCHUYENMON;
        private XRLabel xrl_TRINHDO;
        private XRLabel xrl_NGAY_VAO_CT;
        private XRLabel xrl_TD_VH;
        private XRLabel xrl_TDQCHINHTRI;
        private XRLabel xrlNOI_O_HIENNAY;
        private XRLabel xrLabel8;
        private XRLabel xrl_NGHE_KHI_DC_TUYEN;
        private XRLabel xrl_CoQuanTD;
        private XRLabel xrlCHUCVUHT;
        private XRLabel xrLabel12;
        private XRLabel xrlChucDanhHT;
        private XRLabel xrLabel10;
        private XRLabel xrl_TrinhDoPhoT;
        private XRLabel xrl_NGOAINGUTD;
        private XRLabel xrl_Tin_HOCTD;
        private XRLabel xrLabel15;
        private XRLabel lbPhuCapCV;
        private XRLabel xrLabel39;
        private XRLabel lbPhuCap;
        private XRLabel xrLabel36;
        private XRLabel lbNgayHuongLuong;
        private XRLabel xrLabel34;
        private XRLabel lbHeSo;
        private XRLabel lbBacLuong;
        private XRLabel lbMaNgachCC;
        private XRLabel xrLabel31;
        private XRLabel lbNgachCC;
        private XRLabel xrLabel13;
        private XRLabel xrLabel4;
        private XRLabel xrLabel35;
        private XRLabel xrLabel33;
        private XRLabel xrLabel38;
        private XRLabel xrLabel37;
        private XRLabel xrl_NANGLUC;
        private XRLabel xrl_NGAY_RA_QD;
        private XRLabel xrl_NGAY_VAO_DOAN;
        private XRLabel xrl_NGAY_CT_VAO_DANG;
        private XRLabel xrl_LOAI_CS;
        private XRLabel xrLabel41;
        private XRLabel xrLabel40;
        private XRLabel xrl_SO_CMND;
        private XRLabel xrLabel44;
        private XRLabel xrl_TT_SUCKHOE;
        private XRLabel xrl_NGAY_VAO_DANG;
        private XRLabel xrl_NHOM_MAU;
        private XRLabel xrl_NGAYCAP_BHXH;
        private XRLabel xrl_SO_THE_BHXH;
        private XRLabel xrl_CHIEU_CAO;
        private XRLabel xrlDanhHieuPhongTang;
        private XRLabel xrLabel42;
        private XRLabel xrl_NGAY_CAP_BH;
        private XRLabel xrLabel43;
        private XRLabel xrl_CHUCVU_DOAN;
        private XRLabel xrl_NOI_KN_DANG;
        private XRLabel xrl_NGAYCAP_CMND;
        private XRLabel xrl_NGAY_TGCM;
        private XRLabel xrl_CAN_NANG;
        private XRLabel xrl_CHUCVU_DANG;
        private XRLabel xrl_CAPBAC_QD;
        private XRLabel xrlNGAYVAODANG;
        private XRLabel xrlNGAYCTVAODANG;
        private XRLabel xrlNgayVaoDoan;
        private XRLabel xrLabel11;
        private XRLabel xrLabel17;
        private XRLabel xrLabel18;
        private XRLabel xrLabel19;
        private XRLabel xrLabel20;
        private XRLabel xrLabel21;
        private XRLabel xrLabel22;
        private XRLabel xrLabel23;
        private XRLabel xrLabel24;
        private XRLabel xrLabel25;
        private XRLabel xrLabel26;
        private XRLabel xrLabel27;
        private XRLabel xrLabel29;
        private XRLabel xrLabel3;
        private XRLabel xrl_KHENTHUONG;
        private XRLabel xrLabel6;
        private XRLabel xrl_KYLUAT;
        private XRLabel xrLabel16;
        private XRLabel xrLabel45;
        private XRLabel xrLabel47;
        private XRLabel xrLabel46;
        private XRLabel xrLabel48;
        private XRSubreport xrsub_QTCT;
        private XRSubreport xrsub_QT_DT;
        private XRLabel xrl_LICHSUBANTHAN3;
        private XRLabel xrl_LICHSUBANTHAN2;
        private XRLabel xrl_LICHSUBANTHAN1;
        private XRLabel xrLabel52;
        private XRLabel xrLabel49;
        private XRLabel xrLabel54;
        private XRLabel xrLabel50;
        private XRSubreport xrsub_HOSO_QH_GIADINH;
        private XRSubreport xrSub_DienBienQTLuong;
        private XRSubreport sub_QuanHe_GiaDinh_Vo;
        private XRLabel xrLabel115;
        private XRLabel xrLabel113;
        private XRLabel xrtngayketxuat;
        private XRLabel xrLabel30;
        private XRLabel xrLabel112;
        private XRLabel xrNhanXet;
        private XRLabel xrLabel1;
        private XRLabel xrLabel51;
        private XRLabel xrLabel28;
        private XRLabel xrLabel9;
        private ReportHeaderBand ReportHeader;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public string MaDonVi { get; set; }

        public rp_InformationEmployeeDetail()
        {
            InitializeComponent();
        }

        public void BindData(RecordModel hs)
        {
            if (hs == null)
                return;
            var emptyData = "...";
            var util = SoftCore.Util.GetInstance();

            //sub_Quanhe_Giadinh1.BindData(hs.Id, 1);
            //sub_Quanhe_Giadinh_Vo_1.BindData(hs.Id, 0);
            //sub_DienBienLuong1.BindData(hs.Id);
            //sub_Quatrinh_Congtac1.DataBind(hs.Id);
            //sub_Quatrinh_Daotao1.BindData(hs.Id);

            var kt = RewardController.GetAll(null, null, hs.Id, null, null, null, null, null, null).FirstOrDefault();
            var hsl = SalaryDecisionController.GetCurrent(hs.Id);
            var ds = new List<RecordModel>();
            ds.Add(hs);
            var controller = new ReportController();
            //ReportFilter filter = new ReportFilter();
            var location = string.Empty;
            xrtngayketxuat.Text = string.Format(xrtngayketxuat.Text, location, DateTime.Now.Day, DateTime.Now.Month,
                DateTime.Now.Year);
            var table = SQLHelper.ExecuteTable(SQLBusinessAdapter.GetStore_CurriculumVitaeDetail(hs.Id));
            DataSource = table;

            xrlHOTEN_CBCC.DataBindings.Add("Text", DataSource, "FullName");
            xrl_ThanhPhanBT.DataBindings.Add("Text", DataSource, "PersonalClassName");
            xrlTPGD.DataBindings.Add("Text", DataSource, "FamilyClassName");
            xrl_CQDVCTQ.DataBindings.Add("Text", DataSource, "ParentDepartmentName");
            xrl_CQDVSD.DataBindings.Add("Text", DataSource, "DepartmentName");
            xrl_SHCCVC.DataBindings.Add("Text", DataSource, "EmployeeCode");

            xrl_NoiSinh.Text = @"4) Nơi sinh: " +
                               controller.GetVillageName(table.Rows[0]["BirthPlaceWardGroup"].ToString()) + @" " +
                               table.Rows[0]["BirthPlaceWardName"] + @", " +
                               controller.GetDistrictName(table.Rows[0]["BirthPlaceDistrictGroup"].ToString()) + @" " +
                               table.Rows[0]["BirthPlaceDistrictName"] + @", " +
                               controller.GetProvinceName(table.Rows[0]["BirthPlaceProvinceGroup"].ToString()) + @" " +
                               table.Rows[0]["BirthPlaceProvinceName"] + @".";
            xrNhanXet.Text = hs.Review;
            xrl_LICHSUBANTHAN3.Text = hs.RelativesAboard;
            xrl_BI_DANH.Text = hs.Alias;
            xrl_LICHSUBANTHAN2.Text = hs.ForeignOrganizationJoined;
            xrl_LICHSUBANTHAN1.Text = hs.Biography;
            xrlDanTOC.DataBindings.Add("Text", DataSource, "FolkName");
            xrlTONGIAO.DataBindings.Add("Text", DataSource, "ReligionName");
            xrl_QueQuan.Text = @"5) Quê quán: " +
                               controller.GetVillageName(table.Rows[0]["HometownWardGroup"].ToString()) + @" " +
                               table.Rows[0]["HometownWardName"] + @", " +
                               controller.GetDistrictName(table.Rows[0]["HometownDistrictGroup"].ToString()) + @" " +
                               table.Rows[0]["HometownDistrictName"] + @", " +
                               controller.GetProvinceName(table.Rows[0]["HometownProvinceGroup"].ToString()) + @" " +
                               table.Rows[0]["HometownProvinceName"] + @".";
            xrNhanXet.Text = hs.Review;
            xrLabel22.Text = hs.Weight + @"kg";
            xrLabel20.Text = hs.Height + @"cm";

            xrlHOKHAUTT.Text = @"8) Nơi đăng ký hộ khẩu thường trú: " + hs.ResidentPlace;
            xrl_CoQuanTD.Text = hs.RecruimentDepartment;
            xrl_NGHE_KHI_DC_TUYEN.Text = @"10) Nghề nghiệp khi được tuyển dụng: " + hs.PreviousJob;
            xrlChucDanhHT.Text = hs.JobTitleName;
            xrlCHUCVUHT.Text = hs.PositionName;
            xrl_CVChinhDuocGiao.Text = hs.AssignedWork;

            xrLabel19.Text = hs.Skills;
            if (string.IsNullOrEmpty(table.Rows[0]["RewardDecisionDate"].ToString()))
            {
                xrl_KHENTHUONG.Text += table.Rows[0]["RewardName"].ToString();
            }
            else
            {
                xrl_KHENTHUONG.Text += table.Rows[0]["RewardName"] + @"   Năm: " + table.Rows[0]["RewardDecisionDate"];
            }

            if (!string.IsNullOrEmpty(table.Rows[0]["DisciplineDecisionDate"].ToString()))
            {
                xrl_KYLUAT.Text += table.Rows[0]["DisciplineName"] + @"  Năm: " +
                                   table.Rows[0]["DisciplineDecisionDate"];
            }
            else
            {
                xrl_KYLUAT.Text += table.Rows[0]["DisciplineName"].ToString();
            }

            xrlNGAYCTVAODANG.Text = !util.IsDateNull(hs.CPVOfficialJoinedDate)
                ? string.Format("{0:dd/MM/yyyy}", hs.CPVOfficialJoinedDate)
                : emptyData;

            xrl_NGAY_CAP_BH.Text = !string.IsNullOrEmpty(hs.InsuranceIssueDateVn)
                ? string.Format("{0:dd/MM/yyyy}", hs.InsuranceIssueDate)
                : emptyData;
            xrl_NGAYSINH.Text = !string.IsNullOrEmpty(hs.BirthDateVn)
                ? string.Format("{0:dd/MM/yyyy}", hs.BirthDateVn)
                : emptyData;
            xrLabel11.Text = !string.IsNullOrEmpty(hs.ArmyJoinedDateVn)
                ? string.Format("{0:dd/MM/yyyy}", hs.ArmyJoinedDate)
                : emptyData;

            xrl_NGAY_TUYEN_DAUTIEN.Text = !string.IsNullOrEmpty(hs.RecruimentDateVn)
                ? string.Format("{0:dd/MM/yyyy}", hs.RecruimentDate)
                : emptyData;

            xrlNGAYVAODANG.Text = !util.IsDateNull(hs.CPVJoinedDate)
                ? string.Format("{0:dd/MM/yyyy}", hs.CPVJoinedDate)
                : emptyData;
            xrlNgayVaoDoan.Text = !string.IsNullOrEmpty(hs.VYUJoinedDateVn)
                ? string.Format("{0:dd/MM/yyyy}", hs.VYUJoinedDate)
                : emptyData;

            lbMaNgachCC.DataBindings.Add("Text", DataSource, "QuantumId");
            lbBacLuong.Text = @"Bậc " + table.Rows[0]["SalaryGrade"];
            lbNgachCC.DataBindings.Add("Text", DataSource, "QuantumName");
            //lbPhuCap.DataBindings.Add("Text", DataSource, "OtherAllowance");
            //lbPhuCapCV.DataBindings.Add("Text", DataSource, "PositionAllowance");
            lbHeSo.DataBindings.Add("Text", DataSource, "SalaryFactor");
            lbNgayHuongLuong.DataBindings.Add("Text", DataSource, "EffectiveDate", "{0:dd/MM/yyyy}");
            //lbNgayHuongLuong.Text = !string.IsNullOrEmpty(table.Rows[0]["EffectiveDate"].ToString()) ? string.Format("{0:dd/MM/yyyy}", table.Rows[0]["EffectiveDate"].ToString()) : emptyData;

            xrLabel26.Text = !util.IsDateNull(hs.IDIssueDate)
                ? string.Format("{0:dd/MM/yyyy}", hs.IDIssueDate)
                : emptyData;

            xrl_NGOAINGUTD.Text = hs.LanguageLevelName;
            xrLabel21.Text = hs.BloodGroup;
            xrLabel29.Text = hs.InsuranceNumber;
            xrl_NOI_KN_DANG.Text = hs.CPVJoinedPlace;
            xrlNOI_O_HIENNAY.Text = @"9) Nơi ở hiện nay: " + hs.Address;

            xrLabel27.Text = hs.IDNumber;
            xrl_Tin_HOCTD.Text = hs.ITLevelName;
            xrLabel23.Text = hs.HealthStatusName;
            if (hs.Sex)
            {
                xrc_NAM.Checked = true;
            }
            else
                xrc_NU.Checked = true;

            xrPictureBox1.ImageUrl = hs.ImageUrl;
            xrl_CHUCVU_DOAN.Text += hs.VYUPositionName;
            xrl_CHUCVU_DANG.Text = hs.CPVPositionName;
            xrlDanhHieuPhongTang.Text = hs.TitleAwarded;
            xrLabel18.Text = hs.ArmyLevelName;
            if (hs.ArmyLeftDate != null)
                xrLabel17.Text += string.Format("{0:dd/MM/yyyy}", hs.ArmyLeftDate);
            xrl_TrinhDoPhoT.Text = hs.BasicEducationName;
            xrl_TDCHUYENMON.Text = hs.EducationName;
            xrl_TDQCHINHTRI.Text = hs.PoliticLevelName;
            xrl_TDQLNN.Text = hs.ManagementLevelName;
            xrLabel25.Text += hs.FamilyPolicyName;
            xrLabel24.Text = hs.RankWounded;
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
            this.xrNhanXet = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_LICHSUBANTHAN3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_LICHSUBANTHAN2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_LICHSUBANTHAN1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel52 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel49 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel54 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel50 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NANGLUC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NGAY_RA_QD = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NGAY_VAO_DOAN = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NGAY_CT_VAO_DANG = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_LOAI_CS = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel41 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel40 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_SO_CMND = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel44 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TT_SUCKHOE = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NGAY_VAO_DANG = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NHOM_MAU = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NGAYCAP_BHXH = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_SO_THE_BHXH = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_CHIEU_CAO = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlDanhHieuPhongTang = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel42 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NGAY_CAP_BH = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel43 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_CHUCVU_DOAN = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NOI_KN_DANG = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NGAYCAP_CMND = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NGAY_TGCM = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_CAN_NANG = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_CHUCVU_DANG = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_CAPBAC_QD = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlNGAYVAODANG = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlNGAYCTVAODANG = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlNgayVaoDoan = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_KHENTHUONG = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_KYLUAT = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel45 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel47 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel46 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel48 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_CQTDDAUTIEN = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NGAY_TUYEN_DAU = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_CHUCVU = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TD_QUANLY = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_CVChinhDuocGiao = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TINHOC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NGOAI_NGU = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TDQLNN = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TD_CHINHTRI = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NGAY_TUYEN_DAUTIEN = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TDCHUYENMON = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TRINHDO = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NGAY_VAO_CT = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TD_VH = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TDQCHINHTRI = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlNOI_O_HIENNAY = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NGHE_KHI_DC_TUYEN = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_CoQuanTD = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlCHUCVUHT = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlChucDanhHT = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TrinhDoPhoT = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NGOAINGUTD = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_Tin_HOCTD = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.lbPhuCapCV = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel39 = new DevExpress.XtraReports.UI.XRLabel();
            this.lbPhuCap = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
            this.lbNgayHuongLuong = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
            this.lbHeSo = new DevExpress.XtraReports.UI.XRLabel();
            this.lbBacLuong = new DevExpress.XtraReports.UI.XRLabel();
            this.lbMaNgachCC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
            this.lbNgachCC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrl_TEN_KHAC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NGAY_SINH = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_HO_TEN = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_TON_GIAO = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NoiSinh = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_BI_DANH = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_QueQuan = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_SHCCVC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_DAN_TOC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrc_NAM = new DevExpress.XtraReports.UI.XRCheckBox();
            this.xrl_CQDVCTQ = new DevExpress.XtraReports.UI.XRLabel();
            this.xrc_NU = new DevExpress.XtraReports.UI.XRCheckBox();
            this.xrl_CQDVSD = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_NGAYSINH = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_GIOI_TINH = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlHOTEN_CBCC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlDanTOC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlTONGIAO = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrl_ThanhPhanBT = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlTPGD = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlHOKHAUTT = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrsub_QT_DT = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrsub_QTCT = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSub_DienBienQTLuong = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrsub_HOSO_QH_GIADINH = new DevExpress.XtraReports.UI.XRSubreport();
            this.sub_QuanHe_GiaDinh_Vo = new DevExpress.XtraReports.UI.XRSubreport();
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
            this.xrLabel51.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.xrLabel51.LocationFloat = new DevExpress.Utils.PointFloat(0.01094087F, 4352.375F);
            this.xrLabel51.Name = "xrLabel51";
            this.xrLabel51.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel51.SizeF = new System.Drawing.SizeF(1689.99F, 58.41992F);
            this.xrLabel51.StylePriority.UseFont = false;
            this.xrLabel51.StylePriority.UseTextAlignment = false;
            this.xrLabel51.Text = "b) Về bên vợ (hoặc chồng): Cha, Mẹ, anh chị em ruột";
            this.xrLabel51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel28
            // 
            this.xrLabel28.Dpi = 254F;
            this.xrLabel28.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(0.01287874F, 4182.196F);
            this.xrLabel28.Name = "xrLabel28";
            this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel28.SizeF = new System.Drawing.SizeF(1689.988F, 58.41992F);
            this.xrLabel28.StylePriority.UseFont = false;
            this.xrLabel28.StylePriority.UseTextAlignment = false;
            this.xrLabel28.Text = "a) Về bản thân: (Cha, Mẹ, Vợ (hoặc chồng), các con, anh chị em ruột";
            this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Dpi = 254F;
            this.xrLabel9.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4123.776F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(1689.996F, 58.41992F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "30) Quan hệ gia đình";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel115
            // 
            this.xrLabel115.Dpi = 254F;
            this.xrLabel115.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel115.LocationFloat = new DevExpress.Utils.PointFloat(918.3578F, 4965.679F);
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
            this.xrLabel113.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel113.LocationFloat = new DevExpress.Utils.PointFloat(918.3593F, 4848.839F);
            this.xrLabel113.Name = "xrLabel113";
            this.xrLabel113.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel113.SizeF = new System.Drawing.SizeF(768.5194F, 116.8398F);
            this.xrLabel113.StylePriority.UseFont = false;
            this.xrLabel113.StylePriority.UseTextAlignment = false;
            this.xrLabel113.Text = "Thủ trưởng cơ quan, đơn vị quản lý và sử dụng CBCC";
            this.xrLabel113.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrtngayketxuat
            // 
            this.xrtngayketxuat.Dpi = 254F;
            this.xrtngayketxuat.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrtngayketxuat.LocationFloat = new DevExpress.Utils.PointFloat(723.9834F, 4790.419F);
            this.xrtngayketxuat.Name = "xrtngayketxuat";
            this.xrtngayketxuat.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrtngayketxuat.SizeF = new System.Drawing.SizeF(962.8953F, 58.41992F);
            this.xrtngayketxuat.StylePriority.UseFont = false;
            this.xrtngayketxuat.StylePriority.UseTextAlignment = false;
            this.xrtngayketxuat.Text = "..., ngày {1} tháng {2} năm {3}";
            this.xrtngayketxuat.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel30
            // 
            this.xrLabel30.Dpi = 254F;
            this.xrLabel30.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4907.259F);
            this.xrLabel30.Multiline = true;
            this.xrLabel30.Name = "xrLabel30";
            this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel30.SizeF = new System.Drawing.SizeF(568.1312F, 166.8994F);
            this.xrLabel30.StylePriority.UseFont = false;
            this.xrLabel30.StylePriority.UseTextAlignment = false;
            this.xrLabel30.Text = "Tôi xin cam đoan những lời khai trên đây là đúng sự thật\r\n(Ký tên, ghi rõ họ tên)" +
    "";
            this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel112
            // 
            this.xrLabel112.Dpi = 254F;
            this.xrLabel112.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel112.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4848.839F);
            this.xrLabel112.Name = "xrLabel112";
            this.xrLabel112.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel112.SizeF = new System.Drawing.SizeF(570.4418F, 58.42F);
            this.xrLabel112.StylePriority.UseFont = false;
            this.xrLabel112.StylePriority.UseTextAlignment = false;
            this.xrLabel112.Text = "Người khai";
            this.xrLabel112.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrNhanXet
            // 
            this.xrNhanXet.Dpi = 254F;
            this.xrNhanXet.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrNhanXet.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4731.999F);
            this.xrNhanXet.Multiline = true;
            this.xrNhanXet.Name = "xrNhanXet";
            this.xrNhanXet.Padding = new DevExpress.XtraPrinting.PaddingInfo(15, 5, 0, 0, 254F);
            this.xrNhanXet.SizeF = new System.Drawing.SizeF(1689.988F, 58.41992F);
            this.xrNhanXet.StylePriority.UseFont = false;
            this.xrNhanXet.StylePriority.UsePadding = false;
            this.xrNhanXet.StylePriority.UseTextAlignment = false;
            this.xrNhanXet.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0.00948747F, 4633.892F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(15, 5, 0, 0, 254F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(1689.988F, 58.41992F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UsePadding = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "36) Nhận xét, đánh giá của cơ quan, đơn vị quản lý và sử dụng cán bộ ,công chức";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_LICHSUBANTHAN3
            // 
            this.xrl_LICHSUBANTHAN3.Dpi = 254F;
            this.xrl_LICHSUBANTHAN3.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_LICHSUBANTHAN3.LocationFloat = new DevExpress.Utils.PointFloat(0.001937866F, 4065.356F);
            this.xrl_LICHSUBANTHAN3.Name = "xrl_LICHSUBANTHAN3";
            this.xrl_LICHSUBANTHAN3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_LICHSUBANTHAN3.SizeF = new System.Drawing.SizeF(1689.999F, 58.42F);
            this.xrl_LICHSUBANTHAN3.StylePriority.UseFont = false;
            this.xrl_LICHSUBANTHAN3.StylePriority.UseTextAlignment = false;
            this.xrl_LICHSUBANTHAN3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrl_LICHSUBANTHAN2
            // 
            this.xrl_LICHSUBANTHAN2.Dpi = 254F;
            this.xrl_LICHSUBANTHAN2.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_LICHSUBANTHAN2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 3885.016F);
            this.xrl_LICHSUBANTHAN2.Name = "xrl_LICHSUBANTHAN2";
            this.xrl_LICHSUBANTHAN2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_LICHSUBANTHAN2.SizeF = new System.Drawing.SizeF(1690F, 58.42F);
            this.xrl_LICHSUBANTHAN2.StylePriority.UseFont = false;
            this.xrl_LICHSUBANTHAN2.StylePriority.UseTextAlignment = false;
            this.xrl_LICHSUBANTHAN2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrl_LICHSUBANTHAN1
            // 
            this.xrl_LICHSUBANTHAN1.Dpi = 254F;
            this.xrl_LICHSUBANTHAN1.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_LICHSUBANTHAN1.LocationFloat = new DevExpress.Utils.PointFloat(0.00948747F, 3712.614F);
            this.xrl_LICHSUBANTHAN1.Name = "xrl_LICHSUBANTHAN1";
            this.xrl_LICHSUBANTHAN1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_LICHSUBANTHAN1.SizeF = new System.Drawing.SizeF(1686.869F, 58.41992F);
            this.xrl_LICHSUBANTHAN1.StylePriority.UseFont = false;
            this.xrl_LICHSUBANTHAN1.StylePriority.UseTextAlignment = false;
            this.xrl_LICHSUBANTHAN1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel52
            // 
            this.xrLabel52.Dpi = 254F;
            this.xrLabel52.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel52.LocationFloat = new DevExpress.Utils.PointFloat(0F, 3771.034F);
            this.xrLabel52.Name = "xrLabel52";
            this.xrLabel52.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel52.SizeF = new System.Drawing.SizeF(1689.992F, 113.9824F);
            this.xrLabel52.StylePriority.UseFont = false;
            this.xrLabel52.StylePriority.UseTextAlignment = false;
            this.xrLabel52.Text = " -Tham gia hoặc quan hệ với các tổ chức chính trị, kinh tế, xã hội nào ở nước ngo" +
    "ài (Thời gian, làm gì, tổ chức nào, đặt trụ sở ở đâu....?)";
            this.xrLabel52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel49
            // 
            this.xrLabel49.Dpi = 254F;
            this.xrLabel49.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel49.LocationFloat = new DevExpress.Utils.PointFloat(0F, 3487.083F);
            this.xrLabel49.Name = "xrLabel49";
            this.xrLabel49.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel49.SizeF = new System.Drawing.SizeF(1690.001F, 58.42017F);
            this.xrLabel49.StylePriority.UseFont = false;
            this.xrLabel49.StylePriority.UseTextAlignment = false;
            this.xrLabel49.Text = "29) Đặc điểm lịch sử bản thân";
            this.xrLabel49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel54
            // 
            this.xrLabel54.Dpi = 254F;
            this.xrLabel54.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel54.LocationFloat = new DevExpress.Utils.PointFloat(0.001937866F, 3943.436F);
            this.xrLabel54.Name = "xrLabel54";
            this.xrLabel54.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel54.SizeF = new System.Drawing.SizeF(1689.999F, 121.9199F);
            this.xrLabel54.StylePriority.UseFont = false;
            this.xrLabel54.StylePriority.UseTextAlignment = false;
            this.xrLabel54.Text = "- Có thân nhân (Cha, Mẹ, Vợ, Chồng, con, anh chị em ruột) ở nước ngoài (thời gian" +
    ", làm gì, địa chỉ....)?";
            this.xrLabel54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel50
            // 
            this.xrLabel50.Dpi = 254F;
            this.xrLabel50.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel50.LocationFloat = new DevExpress.Utils.PointFloat(0F, 3545.503F);
            this.xrLabel50.Name = "xrLabel50";
            this.xrLabel50.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel50.SizeF = new System.Drawing.SizeF(1689.999F, 167.1108F);
            this.xrLabel50.StylePriority.UseFont = false;
            this.xrLabel50.StylePriority.UseTextAlignment = false;
            this.xrLabel50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrl_NANGLUC
            // 
            this.xrl_NANGLUC.Dpi = 254F;
            this.xrl_NANGLUC.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NANGLUC.LocationFloat = new DevExpress.Utils.PointFloat(5.382962E-05F, 2730.246F);
            this.xrl_NANGLUC.Name = "xrl_NANGLUC";
            this.xrl_NANGLUC.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NANGLUC.SizeF = new System.Drawing.SizeF(446F, 58.42041F);
            this.xrl_NANGLUC.StylePriority.UseFont = false;
            this.xrl_NANGLUC.StylePriority.UseTextAlignment = false;
            this.xrl_NANGLUC.Text = "20) Sở trường công tác:   ";
            this.xrl_NANGLUC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NGAY_RA_QD
            // 
            this.xrl_NGAY_RA_QD.Dpi = 254F;
            this.xrl_NGAY_RA_QD.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NGAY_RA_QD.LocationFloat = new DevExpress.Utils.PointFloat(604.2075F, 2478.47F);
            this.xrl_NGAY_RA_QD.Name = "xrl_NGAY_RA_QD";
            this.xrl_NGAY_RA_QD.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NGAY_RA_QD.SizeF = new System.Drawing.SizeF(318F, 58.41919F);
            this.xrl_NGAY_RA_QD.StylePriority.UseFont = false;
            this.xrl_NGAY_RA_QD.StylePriority.UseTextAlignment = false;
            this.xrl_NGAY_RA_QD.Text = ", Ngày xuất ngũ:       ";
            this.xrl_NGAY_RA_QD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NGAY_VAO_DOAN
            // 
            this.xrl_NGAY_VAO_DOAN.Dpi = 254F;
            this.xrl_NGAY_VAO_DOAN.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NGAY_VAO_DOAN.LocationFloat = new DevExpress.Utils.PointFloat(5.382962E-05F, 2353.902F);
            this.xrl_NGAY_VAO_DOAN.Name = "xrl_NGAY_VAO_DOAN";
            this.xrl_NGAY_VAO_DOAN.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NGAY_VAO_DOAN.SizeF = new System.Drawing.SizeF(819F, 58.42017F);
            this.xrl_NGAY_VAO_DOAN.StylePriority.UseFont = false;
            this.xrl_NGAY_VAO_DOAN.StylePriority.UseTextAlignment = false;
            this.xrl_NGAY_VAO_DOAN.Text = "17) Ngày tham gia tổ chức chính trị -xã hội:   ";
            this.xrl_NGAY_VAO_DOAN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NGAY_CT_VAO_DANG
            // 
            this.xrl_NGAY_CT_VAO_DANG.Dpi = 254F;
            this.xrl_NGAY_CT_VAO_DANG.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NGAY_CT_VAO_DANG.LocationFloat = new DevExpress.Utils.PointFloat(1068.104F, 2226.478F);
            this.xrl_NGAY_CT_VAO_DANG.Name = "xrl_NGAY_CT_VAO_DANG";
            this.xrl_NGAY_CT_VAO_DANG.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NGAY_CT_VAO_DANG.SizeF = new System.Drawing.SizeF(351.8389F, 58.41992F);
            this.xrl_NGAY_CT_VAO_DANG.StylePriority.UseFont = false;
            this.xrl_NGAY_CT_VAO_DANG.StylePriority.UseTextAlignment = false;
            this.xrl_NGAY_CT_VAO_DANG.Text = ", Ngày chính thức:    ";
            this.xrl_NGAY_CT_VAO_DANG.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_LOAI_CS
            // 
            this.xrl_LOAI_CS.Dpi = 254F;
            this.xrl_LOAI_CS.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_LOAI_CS.LocationFloat = new DevExpress.Utils.PointFloat(687.0001F, 3038.221F);
            this.xrl_LOAI_CS.Name = "xrl_LOAI_CS";
            this.xrl_LOAI_CS.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_LOAI_CS.SizeF = new System.Drawing.SizeF(539F, 58.42041F);
            this.xrl_LOAI_CS.StylePriority.UseFont = false;
            this.xrl_LOAI_CS.StylePriority.UseTextAlignment = false;
            this.xrl_LOAI_CS.Text = ", Là con gia đình chính sách:     ";
            this.xrl_LOAI_CS.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel41
            // 
            this.xrLabel41.Dpi = 254F;
            this.xrLabel41.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel41.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2284.898F);
            this.xrLabel41.Name = "xrLabel41";
            this.xrLabel41.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel41.SizeF = new System.Drawing.SizeF(355F, 58.41992F);
            this.xrLabel41.StylePriority.UseFont = false;
            this.xrLabel41.StylePriority.UseTextAlignment = false;
            this.xrLabel41.Text = "Nơi kết nạp Đảng:";
            this.xrLabel41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel40
            // 
            this.xrLabel40.Dpi = 254F;
            this.xrLabel40.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel40.LocationFloat = new DevExpress.Utils.PointFloat(1106.676F, 2353.902F);
            this.xrLabel40.Name = "xrLabel40";
            this.xrLabel40.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel40.SizeF = new System.Drawing.SizeF(313.2666F, 58.41968F);
            this.xrLabel40.StylePriority.UseFont = false;
            this.xrLabel40.StylePriority.UseTextAlignment = false;
            this.xrLabel40.Text = ", Chức vụ Đoàn:";
            this.xrLabel40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_SO_CMND
            // 
            this.xrl_SO_CMND.Dpi = 254F;
            this.xrl_SO_CMND.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_SO_CMND.LocationFloat = new DevExpress.Utils.PointFloat(8.074442E-05F, 3210.834F);
            this.xrl_SO_CMND.Name = "xrl_SO_CMND";
            this.xrl_SO_CMND.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_SO_CMND.SizeF = new System.Drawing.SizeF(339.6394F, 58.41992F);
            this.xrl_SO_CMND.StylePriority.UseFont = false;
            this.xrl_SO_CMND.StylePriority.UseTextAlignment = false;
            this.xrl_SO_CMND.Text = "25) Số CMTND:         ";
            this.xrl_SO_CMND.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel44
            // 
            this.xrLabel44.Dpi = 254F;
            this.xrLabel44.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel44.LocationFloat = new DevExpress.Utils.PointFloat(8.074442E-05F, 3038.221F);
            this.xrLabel44.Name = "xrLabel44";
            this.xrLabel44.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel44.SizeF = new System.Drawing.SizeF(480F, 58.42041F);
            this.xrLabel44.StylePriority.UseFont = false;
            this.xrLabel44.StylePriority.UseTextAlignment = false;
            this.xrLabel44.Text = "24) Là thương binh hạng:    ";
            this.xrLabel44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_TT_SUCKHOE
            // 
            this.xrl_TT_SUCKHOE.Dpi = 254F;
            this.xrl_TT_SUCKHOE.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_TT_SUCKHOE.LocationFloat = new DevExpress.Utils.PointFloat(5.382962E-05F, 2969.219F);
            this.xrl_TT_SUCKHOE.Name = "xrl_TT_SUCKHOE";
            this.xrl_TT_SUCKHOE.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_TT_SUCKHOE.SizeF = new System.Drawing.SizeF(454F, 58.41968F);
            this.xrl_TT_SUCKHOE.StylePriority.UseFont = false;
            this.xrl_TT_SUCKHOE.StylePriority.UseTextAlignment = false;
            this.xrl_TT_SUCKHOE.Text = "23) Tình trạng sức khỏe:    ";
            this.xrl_TT_SUCKHOE.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NGAY_VAO_DANG
            // 
            this.xrl_NGAY_VAO_DANG.Dpi = 254F;
            this.xrl_NGAY_VAO_DANG.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NGAY_VAO_DANG.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2226.479F);
            this.xrl_NGAY_VAO_DANG.Name = "xrl_NGAY_VAO_DANG";
            this.xrl_NGAY_VAO_DANG.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NGAY_VAO_DANG.SizeF = new System.Drawing.SizeF(740F, 58F);
            this.xrl_NGAY_VAO_DANG.StylePriority.UseFont = false;
            this.xrl_NGAY_VAO_DANG.StylePriority.UseTextAlignment = false;
            this.xrl_NGAY_VAO_DANG.Text = "16) Ngày vào Đảng cộng sản Việt Nam:    ";
            this.xrl_NGAY_VAO_DANG.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NHOM_MAU
            // 
            this.xrl_NHOM_MAU.Dpi = 254F;
            this.xrl_NHOM_MAU.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NHOM_MAU.LocationFloat = new DevExpress.Utils.PointFloat(1368.131F, 2969.219F);
            this.xrl_NHOM_MAU.Name = "xrl_NHOM_MAU";
            this.xrl_NHOM_MAU.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NHOM_MAU.SizeF = new System.Drawing.SizeF(247F, 58.41968F);
            this.xrl_NHOM_MAU.StylePriority.UseFont = false;
            this.xrl_NHOM_MAU.StylePriority.UseTextAlignment = false;
            this.xrl_NHOM_MAU.Text = ", Nhóm máu: ";
            this.xrl_NHOM_MAU.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NGAYCAP_BHXH
            // 
            this.xrl_NGAYCAP_BHXH.Dpi = 254F;
            this.xrl_NGAYCAP_BHXH.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NGAYCAP_BHXH.LocationFloat = new DevExpress.Utils.PointFloat(687.0001F, 3279.837F);
            this.xrl_NGAYCAP_BHXH.Name = "xrl_NGAYCAP_BHXH";
            this.xrl_NGAYCAP_BHXH.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NGAYCAP_BHXH.SizeF = new System.Drawing.SizeF(424.134F, 58.42017F);
            this.xrl_NGAYCAP_BHXH.StylePriority.UseFont = false;
            this.xrl_NGAYCAP_BHXH.StylePriority.UseTextAlignment = false;
            this.xrl_NGAYCAP_BHXH.Text = ", Ngày cấp sổ BHXH:";
            this.xrl_NGAYCAP_BHXH.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_SO_THE_BHXH
            // 
            this.xrl_SO_THE_BHXH.Dpi = 254F;
            this.xrl_SO_THE_BHXH.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_SO_THE_BHXH.LocationFloat = new DevExpress.Utils.PointFloat(5.382962E-05F, 3279.837F);
            this.xrl_SO_THE_BHXH.Name = "xrl_SO_THE_BHXH";
            this.xrl_SO_THE_BHXH.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_SO_THE_BHXH.SizeF = new System.Drawing.SizeF(339.6394F, 58.42017F);
            this.xrl_SO_THE_BHXH.StylePriority.UseFont = false;
            this.xrl_SO_THE_BHXH.StylePriority.UseTextAlignment = false;
            this.xrl_SO_THE_BHXH.Text = "26) Số sổ BHXH:     ";
            this.xrl_SO_THE_BHXH.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_CHIEU_CAO
            // 
            this.xrl_CHIEU_CAO.Dpi = 254F;
            this.xrl_CHIEU_CAO.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_CHIEU_CAO.LocationFloat = new DevExpress.Utils.PointFloat(687.0001F, 2969.219F);
            this.xrl_CHIEU_CAO.Name = "xrl_CHIEU_CAO";
            this.xrl_CHIEU_CAO.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_CHIEU_CAO.SizeF = new System.Drawing.SizeF(227F, 58.41968F);
            this.xrl_CHIEU_CAO.StylePriority.UseFont = false;
            this.xrl_CHIEU_CAO.StylePriority.UseTextAlignment = false;
            this.xrl_CHIEU_CAO.Text = ", Chiều cao:                        ";
            this.xrl_CHIEU_CAO.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrlDanhHieuPhongTang
            // 
            this.xrlDanhHieuPhongTang.Dpi = 254F;
            this.xrlDanhHieuPhongTang.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlDanhHieuPhongTang.LocationFloat = new DevExpress.Utils.PointFloat(772.0001F, 2547.471F);
            this.xrlDanhHieuPhongTang.Name = "xrlDanhHieuPhongTang";
            this.xrlDanhHieuPhongTang.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrlDanhHieuPhongTang.SizeF = new System.Drawing.SizeF(917.0008F, 58.41992F);
            this.xrlDanhHieuPhongTang.StylePriority.UseFont = false;
            this.xrlDanhHieuPhongTang.StylePriority.UseTextAlignment = false;
            this.xrlDanhHieuPhongTang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel42
            // 
            this.xrLabel42.Dpi = 254F;
            this.xrLabel42.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel42.LocationFloat = new DevExpress.Utils.PointFloat(975.6721F, 2284.898F);
            this.xrLabel42.Name = "xrLabel42";
            this.xrLabel42.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel42.SizeF = new System.Drawing.SizeF(315.9121F, 58.42041F);
            this.xrLabel42.StylePriority.UseFont = false;
            this.xrLabel42.StylePriority.UseTextAlignment = false;
            this.xrLabel42.Text = ", Chức vụ Đảng:";
            this.xrLabel42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NGAY_CAP_BH
            // 
            this.xrl_NGAY_CAP_BH.Dpi = 254F;
            this.xrl_NGAY_CAP_BH.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NGAY_CAP_BH.LocationFloat = new DevExpress.Utils.PointFloat(1117.147F, 3279.837F);
            this.xrl_NGAY_CAP_BH.Name = "xrl_NGAY_CAP_BH";
            this.xrl_NGAY_CAP_BH.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NGAY_CAP_BH.SizeF = new System.Drawing.SizeF(572.8512F, 58.42017F);
            this.xrl_NGAY_CAP_BH.StylePriority.UseFont = false;
            this.xrl_NGAY_CAP_BH.StylePriority.UseTextAlignment = false;
            this.xrl_NGAY_CAP_BH.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel43
            // 
            this.xrLabel43.Dpi = 254F;
            this.xrLabel43.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel43.LocationFloat = new DevExpress.Utils.PointFloat(5.382962E-05F, 2547.471F);
            this.xrLabel43.Name = "xrLabel43";
            this.xrLabel43.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel43.SizeF = new System.Drawing.SizeF(772F, 58.42041F);
            this.xrLabel43.StylePriority.UseFont = false;
            this.xrLabel43.StylePriority.UseTextAlignment = false;
            this.xrLabel43.Text = "19) Danh hiệu được phong tặng cao nhất:";
            this.xrLabel43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_CHUCVU_DOAN
            // 
            this.xrl_CHUCVU_DOAN.Dpi = 254F;
            this.xrl_CHUCVU_DOAN.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_CHUCVU_DOAN.LocationFloat = new DevExpress.Utils.PointFloat(1419.943F, 2353.902F);
            this.xrl_CHUCVU_DOAN.Name = "xrl_CHUCVU_DOAN";
            this.xrl_CHUCVU_DOAN.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_CHUCVU_DOAN.SizeF = new System.Drawing.SizeF(270.0577F, 58.41968F);
            this.xrl_CHUCVU_DOAN.StylePriority.UseFont = false;
            this.xrl_CHUCVU_DOAN.StylePriority.UseTextAlignment = false;
            this.xrl_CHUCVU_DOAN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NOI_KN_DANG
            // 
            this.xrl_NOI_KN_DANG.Dpi = 254F;
            this.xrl_NOI_KN_DANG.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NOI_KN_DANG.LocationFloat = new DevExpress.Utils.PointFloat(356.337F, 2284.898F);
            this.xrl_NOI_KN_DANG.Name = "xrl_NOI_KN_DANG";
            this.xrl_NOI_KN_DANG.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NOI_KN_DANG.SizeF = new System.Drawing.SizeF(619.0176F, 58.41992F);
            this.xrl_NOI_KN_DANG.StylePriority.UseFont = false;
            this.xrl_NOI_KN_DANG.StylePriority.UseTextAlignment = false;
            this.xrl_NOI_KN_DANG.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NGAYCAP_CMND
            // 
            this.xrl_NGAYCAP_CMND.Dpi = 254F;
            this.xrl_NGAYCAP_CMND.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NGAYCAP_CMND.LocationFloat = new DevExpress.Utils.PointFloat(687.0001F, 3210.834F);
            this.xrl_NGAYCAP_CMND.Name = "xrl_NGAYCAP_CMND";
            this.xrl_NGAYCAP_CMND.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NGAYCAP_CMND.SizeF = new System.Drawing.SizeF(226.515F, 58.41992F);
            this.xrl_NGAYCAP_CMND.StylePriority.UseFont = false;
            this.xrl_NGAYCAP_CMND.StylePriority.UseTextAlignment = false;
            this.xrl_NGAYCAP_CMND.Text = ", Ngày cấp:  ";
            this.xrl_NGAYCAP_CMND.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NGAY_TGCM
            // 
            this.xrl_NGAY_TGCM.Dpi = 254F;
            this.xrl_NGAY_TGCM.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NGAY_TGCM.LocationFloat = new DevExpress.Utils.PointFloat(5.382962E-05F, 2478.468F);
            this.xrl_NGAY_TGCM.Name = "xrl_NGAY_TGCM";
            this.xrl_NGAY_TGCM.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NGAY_TGCM.SizeF = new System.Drawing.SizeF(376F, 58.42017F);
            this.xrl_NGAY_TGCM.StylePriority.UseFont = false;
            this.xrl_NGAY_TGCM.StylePriority.UseTextAlignment = false;
            this.xrl_NGAY_TGCM.Text = "18) Ngày nhập ngũ: ";
            this.xrl_NGAY_TGCM.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_CAN_NANG
            // 
            this.xrl_CAN_NANG.Dpi = 254F;
            this.xrl_CAN_NANG.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_CAN_NANG.LocationFloat = new DevExpress.Utils.PointFloat(1047F, 2969.219F);
            this.xrl_CAN_NANG.Name = "xrl_CAN_NANG";
            this.xrl_CAN_NANG.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_CAN_NANG.SizeF = new System.Drawing.SizeF(217F, 58.41992F);
            this.xrl_CAN_NANG.StylePriority.UseFont = false;
            this.xrl_CAN_NANG.StylePriority.UseTextAlignment = false;
            this.xrl_CAN_NANG.Text = ", Cân nặng:                  ";
            this.xrl_CAN_NANG.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_CHUCVU_DANG
            // 
            this.xrl_CHUCVU_DANG.Dpi = 254F;
            this.xrl_CHUCVU_DANG.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_CHUCVU_DANG.LocationFloat = new DevExpress.Utils.PointFloat(1291.584F, 2284.898F);
            this.xrl_CHUCVU_DANG.Name = "xrl_CHUCVU_DANG";
            this.xrl_CHUCVU_DANG.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_CHUCVU_DANG.SizeF = new System.Drawing.SizeF(398.4161F, 58.41992F);
            this.xrl_CHUCVU_DANG.StylePriority.UseFont = false;
            this.xrl_CHUCVU_DANG.StylePriority.UseTextAlignment = false;
            this.xrl_CHUCVU_DANG.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_CAPBAC_QD
            // 
            this.xrl_CAPBAC_QD.Dpi = 254F;
            this.xrl_CAPBAC_QD.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_CAPBAC_QD.LocationFloat = new DevExpress.Utils.PointFloat(1153.52F, 2478.469F);
            this.xrl_CAPBAC_QD.Name = "xrl_CAPBAC_QD";
            this.xrl_CAPBAC_QD.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_CAPBAC_QD.SizeF = new System.Drawing.SizeF(401F, 58.41968F);
            this.xrl_CAPBAC_QD.StylePriority.UseFont = false;
            this.xrl_CAPBAC_QD.StylePriority.UseTextAlignment = false;
            this.xrl_CAPBAC_QD.Text = ", Quân hàm cao nhất:    ";
            this.xrl_CAPBAC_QD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrlNGAYVAODANG
            // 
            this.xrlNGAYVAODANG.Dpi = 254F;
            this.xrlNGAYVAODANG.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlNGAYVAODANG.LocationFloat = new DevExpress.Utils.PointFloat(745.2874F, 2226.477F);
            this.xrlNGAYVAODANG.Name = "xrlNGAYVAODANG";
            this.xrlNGAYVAODANG.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrlNGAYVAODANG.SizeF = new System.Drawing.SizeF(322.8169F, 58.42017F);
            this.xrlNGAYVAODANG.StylePriority.UseFont = false;
            this.xrlNGAYVAODANG.StylePriority.UseTextAlignment = false;
            this.xrlNGAYVAODANG.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrlNGAYCTVAODANG
            // 
            this.xrlNGAYCTVAODANG.Dpi = 254F;
            this.xrlNGAYCTVAODANG.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlNGAYCTVAODANG.LocationFloat = new DevExpress.Utils.PointFloat(1419.943F, 2226.477F);
            this.xrlNGAYCTVAODANG.Name = "xrlNGAYCTVAODANG";
            this.xrlNGAYCTVAODANG.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrlNGAYCTVAODANG.SizeF = new System.Drawing.SizeF(270F, 58.42017F);
            this.xrlNGAYCTVAODANG.StylePriority.UseFont = false;
            this.xrlNGAYCTVAODANG.StylePriority.UseTextAlignment = false;
            this.xrlNGAYCTVAODANG.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrlNgayVaoDoan
            // 
            this.xrlNgayVaoDoan.Dpi = 254F;
            this.xrlNgayVaoDoan.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlNgayVaoDoan.LocationFloat = new DevExpress.Utils.PointFloat(819.0001F, 2353.902F);
            this.xrlNgayVaoDoan.Name = "xrlNgayVaoDoan";
            this.xrlNgayVaoDoan.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrlNgayVaoDoan.SizeF = new System.Drawing.SizeF(287.6757F, 58.42017F);
            this.xrlNgayVaoDoan.StylePriority.UseFont = false;
            this.xrlNgayVaoDoan.StylePriority.UseTextAlignment = false;
            this.xrlNgayVaoDoan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Dpi = 254F;
            this.xrLabel11.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(376.0001F, 2478.469F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(228.2081F, 58.41968F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel17
            // 
            this.xrLabel17.Dpi = 254F;
            this.xrLabel17.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(922.2076F, 2478.468F);
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(231.3121F, 58.41992F);
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.StylePriority.UseTextAlignment = false;
            this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel18
            // 
            this.xrLabel18.Dpi = 254F;
            this.xrLabel18.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(1554.52F, 2478.469F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(135.4799F, 58.41968F);
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel19
            // 
            this.xrLabel19.Dpi = 254F;
            this.xrLabel19.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(446.0001F, 2730.246F);
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(1243.001F, 58.41968F);
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.StylePriority.UseTextAlignment = false;
            this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel20
            // 
            this.xrLabel20.Dpi = 254F;
            this.xrLabel20.Font = new System.Drawing.Font("Times New Roman", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(914.0002F, 2969.219F);
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(133F, 58.41992F);
            this.xrLabel20.StylePriority.UseFont = false;
            this.xrLabel20.StylePriority.UseTextAlignment = false;
            this.xrLabel20.Text = "165cm";
            this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel21
            // 
            this.xrLabel21.Dpi = 254F;
            this.xrLabel21.Font = new System.Drawing.Font("Times New Roman", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(1615.131F, 2969.219F);
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel21.SizeF = new System.Drawing.SizeF(73.87F, 58.41992F);
            this.xrLabel21.StylePriority.UseFont = false;
            this.xrLabel21.StylePriority.UseTextAlignment = false;
            this.xrLabel21.Text = "AB";
            this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel22
            // 
            this.xrLabel22.Dpi = 254F;
            this.xrLabel22.Font = new System.Drawing.Font("Times New Roman", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(1264F, 2969.219F);
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel22.SizeF = new System.Drawing.SizeF(104F, 58.41968F);
            this.xrLabel22.StylePriority.UseFont = false;
            this.xrLabel22.StylePriority.UseTextAlignment = false;
            this.xrLabel22.Text = "70kg";
            this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel23
            // 
            this.xrLabel23.Dpi = 254F;
            this.xrLabel23.Font = new System.Drawing.Font("Times New Roman", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(454.0001F, 2969.219F);
            this.xrLabel23.Name = "xrLabel23";
            this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel23.SizeF = new System.Drawing.SizeF(233F, 58.41968F);
            this.xrLabel23.StylePriority.UseFont = false;
            this.xrLabel23.StylePriority.UseTextAlignment = false;
            this.xrLabel23.Text = "Bình thường";
            this.xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel24
            // 
            this.xrLabel24.Dpi = 254F;
            this.xrLabel24.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(480.0001F, 3038.221F);
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel24.SizeF = new System.Drawing.SizeF(207F, 58.42041F);
            this.xrLabel24.StylePriority.UseFont = false;
            this.xrLabel24.StylePriority.UseTextAlignment = false;
            this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel25
            // 
            this.xrLabel25.Dpi = 254F;
            this.xrLabel25.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(1238.221F, 3038.221F);
            this.xrLabel25.Name = "xrLabel25";
            this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel25.SizeF = new System.Drawing.SizeF(451.7795F, 58.42017F);
            this.xrLabel25.StylePriority.UseFont = false;
            this.xrLabel25.StylePriority.UseTextAlignment = false;
            this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel26
            // 
            this.xrLabel26.Dpi = 254F;
            this.xrLabel26.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(918.3593F, 3210.834F);
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel26.SizeF = new System.Drawing.SizeF(768.5193F, 58.41992F);
            this.xrLabel26.StylePriority.UseFont = false;
            this.xrLabel26.StylePriority.UseTextAlignment = false;
            this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel27
            // 
            this.xrLabel27.Dpi = 254F;
            this.xrLabel27.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(339.6396F, 3210.834F);
            this.xrLabel27.Name = "xrLabel27";
            this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel27.SizeF = new System.Drawing.SizeF(339.8177F, 58.41992F);
            this.xrLabel27.StylePriority.UseFont = false;
            this.xrLabel27.StylePriority.UseTextAlignment = false;
            this.xrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel29
            // 
            this.xrLabel29.Dpi = 254F;
            this.xrLabel29.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(339.6387F, 3279.837F);
            this.xrLabel29.Name = "xrLabel29";
            this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel29.SizeF = new System.Drawing.SizeF(339.8185F, 58.42017F);
            this.xrLabel29.StylePriority.UseFont = false;
            this.xrLabel29.StylePriority.UseTextAlignment = false;
            this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Dpi = 254F;
            this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(5.382962E-05F, 2799.249F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(339.6393F, 58.42041F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "21) Khen thưởng:    ";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_KHENTHUONG
            // 
            this.xrl_KHENTHUONG.Dpi = 254F;
            this.xrl_KHENTHUONG.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_KHENTHUONG.LocationFloat = new DevExpress.Utils.PointFloat(339.6393F, 2799.249F);
            this.xrl_KHENTHUONG.Name = "xrl_KHENTHUONG";
            this.xrl_KHENTHUONG.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_KHENTHUONG.SizeF = new System.Drawing.SizeF(555.1061F, 58.42065F);
            this.xrl_KHENTHUONG.StylePriority.UseFont = false;
            this.xrl_KHENTHUONG.StylePriority.UseTextAlignment = false;
            this.xrl_KHENTHUONG.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Dpi = 254F;
            this.xrLabel6.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(894.7457F, 2799.249F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(238.5026F, 58.42041F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "22) Kỷ luật:    ";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_KYLUAT
            // 
            this.xrl_KYLUAT.Dpi = 254F;
            this.xrl_KYLUAT.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_KYLUAT.LocationFloat = new DevExpress.Utils.PointFloat(1134.414F, 2799.249F);
            this.xrl_KYLUAT.Name = "xrl_KYLUAT";
            this.xrl_KYLUAT.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_KYLUAT.SizeF = new System.Drawing.SizeF(555.5859F, 58.42041F);
            this.xrl_KYLUAT.StylePriority.UseFont = false;
            this.xrl_KYLUAT.StylePriority.UseTextAlignment = false;
            this.xrl_KYLUAT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel16
            // 
            this.xrLabel16.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel16.Dpi = 254F;
            this.xrLabel16.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2412.322F);
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(1690.001F, 55.5625F);
            this.xrLabel16.StylePriority.UseBorders = false;
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            this.xrLabel16.Text = "(Ngày tham gia tổ chức: Đoàn, Hội, ... và làm việc gì trong tổ chức đó)";
            this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel45
            // 
            this.xrLabel45.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel45.Dpi = 254F;
            this.xrLabel45.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel45.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2605.891F);
            this.xrLabel45.Name = "xrLabel45";
            this.xrLabel45.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel45.SizeF = new System.Drawing.SizeF(1690.001F, 111.125F);
            this.xrLabel45.StylePriority.UseBorders = false;
            this.xrLabel45.StylePriority.UseFont = false;
            this.xrLabel45.StylePriority.UseTextAlignment = false;
            this.xrLabel45.Text = "(Anh hùng lao động, anh hùng lực lượng vũ trang; nhà giáo, thày thuốc, nghệ sĩ nh" +
    "ân dân và ưu tú, ....)";
            this.xrLabel45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel47
            // 
            this.xrLabel47.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel47.Dpi = 254F;
            this.xrLabel47.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel47.LocationFloat = new DevExpress.Utils.PointFloat(894.7454F, 2857.67F);
            this.xrLabel47.Name = "xrLabel47";
            this.xrLabel47.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel47.SizeF = new System.Drawing.SizeF(795.2534F, 108.4792F);
            this.xrLabel47.StylePriority.UseBorders = false;
            this.xrLabel47.StylePriority.UseFont = false;
            this.xrLabel47.StylePriority.UseTextAlignment = false;
            this.xrLabel47.Text = "(về đảng, chính quyền, đoàn thể hình thức cao nhất, năm nào)";
            this.xrLabel47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel46
            // 
            this.xrLabel46.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel46.Dpi = 254F;
            this.xrLabel46.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel46.LocationFloat = new DevExpress.Utils.PointFloat(8.074442E-05F, 2857.67F);
            this.xrLabel46.Name = "xrLabel46";
            this.xrLabel46.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel46.SizeF = new System.Drawing.SizeF(894.7453F, 55.5625F);
            this.xrLabel46.StylePriority.UseBorders = false;
            this.xrLabel46.StylePriority.UseFont = false;
            this.xrLabel46.StylePriority.UseTextAlignment = false;
            this.xrLabel46.Text = "(Hình thức cao nhất, năm nào)";
            this.xrLabel46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel48
            // 
            this.xrLabel48.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel48.Dpi = 254F;
            this.xrLabel48.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel48.LocationFloat = new DevExpress.Utils.PointFloat(687.0001F, 3096.641F);
            this.xrLabel48.Name = "xrLabel48";
            this.xrLabel48.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel48.SizeF = new System.Drawing.SizeF(996.9878F, 108.4795F);
            this.xrLabel48.StylePriority.UseBorders = false;
            this.xrLabel48.StylePriority.UseFont = false;
            this.xrLabel48.StylePriority.UseTextAlignment = false;
            this.xrLabel48.Text = "(Con thương binh, con liệt sĩ, người nhiễm chất độc da cam Dioxin)";
            this.xrLabel48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_CQTDDAUTIEN
            // 
            this.xrl_CQTDDAUTIEN.Dpi = 254F;
            this.xrl_CQTDDAUTIEN.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_CQTDDAUTIEN.LocationFloat = new DevExpress.Utils.PointFloat(644.3428F, 1291.408F);
            this.xrl_CQTDDAUTIEN.Name = "xrl_CQTDDAUTIEN";
            this.xrl_CQTDDAUTIEN.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_CQTDDAUTIEN.SizeF = new System.Drawing.SizeF(423.7607F, 58.41992F);
            this.xrl_CQTDDAUTIEN.StylePriority.UseFont = false;
            this.xrl_CQTDDAUTIEN.StylePriority.UseTextAlignment = false;
            this.xrl_CQTDDAUTIEN.Text = ", Cơ quan tuyển dụng:     ";
            this.xrl_CQTDDAUTIEN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NGAY_TUYEN_DAU
            // 
            this.xrl_NGAY_TUYEN_DAU.Dpi = 254F;
            this.xrl_NGAY_TUYEN_DAU.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NGAY_TUYEN_DAU.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1291.409F);
            this.xrl_NGAY_TUYEN_DAU.Name = "xrl_NGAY_TUYEN_DAU";
            this.xrl_NGAY_TUYEN_DAU.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NGAY_TUYEN_DAU.SizeF = new System.Drawing.SizeF(410F, 58.42017F);
            this.xrl_NGAY_TUYEN_DAU.StylePriority.UseFont = false;
            this.xrl_NGAY_TUYEN_DAU.StylePriority.UseTextAlignment = false;
            this.xrl_NGAY_TUYEN_DAU.Text = "11) Ngày tuyển dụng:   ";
            this.xrl_NGAY_TUYEN_DAU.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_CHUCVU
            // 
            this.xrl_CHUCVU.Dpi = 254F;
            this.xrl_CHUCVU.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_CHUCVU.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1361.505F);
            this.xrl_CHUCVU.Name = "xrl_CHUCVU";
            this.xrl_CHUCVU.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_CHUCVU.SizeF = new System.Drawing.SizeF(410F, 58.41992F);
            this.xrl_CHUCVU.StylePriority.UseFont = false;
            this.xrl_CHUCVU.StylePriority.UseTextAlignment = false;
            this.xrl_CHUCVU.Text = "12) Chức vụ hiện tại:                               ";
            this.xrl_CHUCVU.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_TD_QUANLY
            // 
            this.xrl_TD_QUANLY.Dpi = 254F;
            this.xrl_TD_QUANLY.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_TD_QUANLY.LocationFloat = new DevExpress.Utils.PointFloat(771.9999F, 1910.169F);
            this.xrl_TD_QUANLY.Name = "xrl_TD_QUANLY";
            this.xrl_TD_QUANLY.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_TD_QUANLY.SizeF = new System.Drawing.SizeF(465F, 58.42029F);
            this.xrl_TD_QUANLY.StylePriority.UseFont = false;
            this.xrl_TD_QUANLY.StylePriority.UseTextAlignment = false;
            this.xrl_TD_QUANLY.Text = "15.4- Quản lý nhà nước:        ";
            this.xrl_TD_QUANLY.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_CVChinhDuocGiao
            // 
            this.xrl_CVChinhDuocGiao.Dpi = 254F;
            this.xrl_CVChinhDuocGiao.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_CVChinhDuocGiao.LocationFloat = new DevExpress.Utils.PointFloat(604.2076F, 1488.928F);
            this.xrl_CVChinhDuocGiao.Name = "xrl_CVChinhDuocGiao";
            this.xrl_CVChinhDuocGiao.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_CVChinhDuocGiao.SizeF = new System.Drawing.SizeF(1085.792F, 58.41968F);
            this.xrl_CVChinhDuocGiao.StylePriority.UseFont = false;
            this.xrl_CVChinhDuocGiao.StylePriority.UseTextAlignment = false;
            this.xrl_CVChinhDuocGiao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_TINHOC
            // 
            this.xrl_TINHOC.Dpi = 254F;
            this.xrl_TINHOC.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_TINHOC.LocationFloat = new DevExpress.Utils.PointFloat(838.014F, 2100.032F);
            this.xrl_TINHOC.Name = "xrl_TINHOC";
            this.xrl_TINHOC.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_TINHOC.SizeF = new System.Drawing.SizeF(268.6619F, 58.42017F);
            this.xrl_TINHOC.StylePriority.UseFont = false;
            this.xrl_TINHOC.StylePriority.UseTextAlignment = false;
            this.xrl_TINHOC.Text = "15.6- Tin học:    ";
            this.xrl_TINHOC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NGOAI_NGU
            // 
            this.xrl_NGOAI_NGU.Dpi = 254F;
            this.xrl_NGOAI_NGU.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NGOAI_NGU.LocationFloat = new DevExpress.Utils.PointFloat(5.382962E-05F, 2100.032F);
            this.xrl_NGOAI_NGU.Name = "xrl_NGOAI_NGU";
            this.xrl_NGOAI_NGU.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NGOAI_NGU.SizeF = new System.Drawing.SizeF(325F, 58.42017F);
            this.xrl_NGOAI_NGU.StylePriority.UseFont = false;
            this.xrl_NGOAI_NGU.StylePriority.UseTextAlignment = false;
            this.xrl_NGOAI_NGU.Text = "15.5- Ngoại ngữ:       ";
            this.xrl_NGOAI_NGU.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_TDQLNN
            // 
            this.xrl_TDQLNN.Dpi = 254F;
            this.xrl_TDQLNN.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_TDQLNN.LocationFloat = new DevExpress.Utils.PointFloat(1238.221F, 1910.169F);
            this.xrl_TDQLNN.Name = "xrl_TDQLNN";
            this.xrl_TDQLNN.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_TDQLNN.SizeF = new System.Drawing.SizeF(451.7793F, 58.42017F);
            this.xrl_TDQLNN.StylePriority.UseFont = false;
            this.xrl_TDQLNN.StylePriority.UseTextAlignment = false;
            this.xrl_TDQLNN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_TD_CHINHTRI
            // 
            this.xrl_TD_CHINHTRI.Dpi = 254F;
            this.xrl_TD_CHINHTRI.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_TD_CHINHTRI.LocationFloat = new DevExpress.Utils.PointFloat(5.382962E-05F, 1910.169F);
            this.xrl_TD_CHINHTRI.Name = "xrl_TD_CHINHTRI";
            this.xrl_TD_CHINHTRI.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_TD_CHINHTRI.SizeF = new System.Drawing.SizeF(437F, 58.42029F);
            this.xrl_TD_CHINHTRI.StylePriority.UseFont = false;
            this.xrl_TD_CHINHTRI.StylePriority.UseTextAlignment = false;
            this.xrl_TD_CHINHTRI.Text = "15.3- Lý luận chính trị:         ";
            this.xrl_TD_CHINHTRI.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NGAY_TUYEN_DAUTIEN
            // 
            this.xrl_NGAY_TUYEN_DAUTIEN.Dpi = 254F;
            this.xrl_NGAY_TUYEN_DAUTIEN.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NGAY_TUYEN_DAUTIEN.LocationFloat = new DevExpress.Utils.PointFloat(412.1222F, 1291.408F);
            this.xrl_NGAY_TUYEN_DAUTIEN.Name = "xrl_NGAY_TUYEN_DAUTIEN";
            this.xrl_NGAY_TUYEN_DAUTIEN.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NGAY_TUYEN_DAUTIEN.SizeF = new System.Drawing.SizeF(226.3649F, 58.42029F);
            this.xrl_NGAY_TUYEN_DAUTIEN.StylePriority.UseFont = false;
            this.xrl_NGAY_TUYEN_DAUTIEN.StylePriority.UseTextAlignment = false;
            this.xrl_NGAY_TUYEN_DAUTIEN.Text = "29/12/2006";
            this.xrl_NGAY_TUYEN_DAUTIEN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_TDCHUYENMON
            // 
            this.xrl_TDCHUYENMON.Dpi = 254F;
            this.xrl_TDCHUYENMON.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_TDCHUYENMON.LocationFloat = new DevExpress.Utils.PointFloat(693.0131F, 1841.459F);
            this.xrl_TDCHUYENMON.Name = "xrl_TDCHUYENMON";
            this.xrl_TDCHUYENMON.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_TDCHUYENMON.SizeF = new System.Drawing.SizeF(996.9859F, 58.41968F);
            this.xrl_TDCHUYENMON.StylePriority.UseFont = false;
            this.xrl_TDCHUYENMON.StylePriority.UseTextAlignment = false;
            this.xrl_TDCHUYENMON.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_TRINHDO
            // 
            this.xrl_TRINHDO.Dpi = 254F;
            this.xrl_TRINHDO.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_TRINHDO.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1841.459F);
            this.xrl_TRINHDO.Name = "xrl_TRINHDO";
            this.xrl_TRINHDO.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_TRINHDO.SizeF = new System.Drawing.SizeF(693F, 58.41992F);
            this.xrl_TRINHDO.StylePriority.UseFont = false;
            this.xrl_TRINHDO.StylePriority.UseTextAlignment = false;
            this.xrl_TRINHDO.Text = "15.2- Trình độ chuyên môn cao nhất:  ";
            this.xrl_TRINHDO.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NGAY_VAO_CT
            // 
            this.xrl_NGAY_VAO_CT.Dpi = 254F;
            this.xrl_NGAY_VAO_CT.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NGAY_VAO_CT.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1488.928F);
            this.xrl_NGAY_VAO_CT.Name = "xrl_NGAY_VAO_CT";
            this.xrl_NGAY_VAO_CT.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NGAY_VAO_CT.SizeF = new System.Drawing.SizeF(604.2082F, 58.41968F);
            this.xrl_NGAY_VAO_CT.StylePriority.UseFont = false;
            this.xrl_NGAY_VAO_CT.StylePriority.UseTextAlignment = false;
            this.xrl_NGAY_VAO_CT.Text = "13) Công việc chính được giao: ";
            this.xrl_NGAY_VAO_CT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_TD_VH
            // 
            this.xrl_TD_VH.Dpi = 254F;
            this.xrl_TD_VH.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_TD_VH.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1769.81F);
            this.xrl_TD_VH.Name = "xrl_TD_VH";
            this.xrl_TD_VH.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_TD_VH.SizeF = new System.Drawing.SizeF(658F, 58.42004F);
            this.xrl_TD_VH.StylePriority.UseFont = false;
            this.xrl_TD_VH.StylePriority.UseTextAlignment = false;
            this.xrl_TD_VH.Text = "15.1- Trình độ giáo dục phổ thông:   ";
            this.xrl_TD_VH.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_TDQCHINHTRI
            // 
            this.xrl_TDQCHINHTRI.Dpi = 254F;
            this.xrl_TDQCHINHTRI.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_TDQCHINHTRI.LocationFloat = new DevExpress.Utils.PointFloat(437.0001F, 1910.169F);
            this.xrl_TDQCHINHTRI.Name = "xrl_TDQCHINHTRI";
            this.xrl_TDQCHINHTRI.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_TDQCHINHTRI.SizeF = new System.Drawing.SizeF(332F, 58.42017F);
            this.xrl_TDQCHINHTRI.StylePriority.UseFont = false;
            this.xrl_TDQCHINHTRI.StylePriority.UseTextAlignment = false;
            this.xrl_TDQCHINHTRI.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrlNOI_O_HIENNAY
            // 
            this.xrlNOI_O_HIENNAY.Dpi = 254F;
            this.xrlNOI_O_HIENNAY.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlNOI_O_HIENNAY.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1084.398F);
            this.xrlNOI_O_HIENNAY.Name = "xrlNOI_O_HIENNAY";
            this.xrlNOI_O_HIENNAY.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrlNOI_O_HIENNAY.SizeF = new System.Drawing.SizeF(1689.999F, 58.41992F);
            this.xrlNOI_O_HIENNAY.StylePriority.UseFont = false;
            this.xrlNOI_O_HIENNAY.StylePriority.UseTextAlignment = false;
            this.xrlNOI_O_HIENNAY.Text = "9) Nơi ở hiện nay: ";
            this.xrlNOI_O_HIENNAY.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Dpi = 254F;
            this.xrLabel8.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1153.402F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(1690.001F, 58.42004F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "(Số nhà, đường phố, thành phố, xóm, thôn, xã, huyện, tỉnh)";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NGHE_KHI_DC_TUYEN
            // 
            this.xrl_NGHE_KHI_DC_TUYEN.Dpi = 254F;
            this.xrl_NGHE_KHI_DC_TUYEN.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NGHE_KHI_DC_TUYEN.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1222.404F);
            this.xrl_NGHE_KHI_DC_TUYEN.Name = "xrl_NGHE_KHI_DC_TUYEN";
            this.xrl_NGHE_KHI_DC_TUYEN.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NGHE_KHI_DC_TUYEN.SizeF = new System.Drawing.SizeF(1689.999F, 58.42004F);
            this.xrl_NGHE_KHI_DC_TUYEN.StylePriority.UseFont = false;
            this.xrl_NGHE_KHI_DC_TUYEN.StylePriority.UseTextAlignment = false;
            this.xrl_NGHE_KHI_DC_TUYEN.Text = "10) Nghề nghiệp khi được tuyển dụng: ";
            this.xrl_NGHE_KHI_DC_TUYEN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_CoQuanTD
            // 
            this.xrl_CoQuanTD.Dpi = 254F;
            this.xrl_CoQuanTD.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_CoQuanTD.LocationFloat = new DevExpress.Utils.PointFloat(1068.104F, 1291.409F);
            this.xrl_CoQuanTD.Name = "xrl_CoQuanTD";
            this.xrl_CoQuanTD.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_CoQuanTD.SizeF = new System.Drawing.SizeF(621.8947F, 58.42029F);
            this.xrl_CoQuanTD.StylePriority.UseFont = false;
            this.xrl_CoQuanTD.StylePriority.UseTextAlignment = false;
            this.xrl_CoQuanTD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrlCHUCVUHT
            // 
            this.xrlCHUCVUHT.Dpi = 254F;
            this.xrlCHUCVUHT.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlCHUCVUHT.LocationFloat = new DevExpress.Utils.PointFloat(410F, 1361.505F);
            this.xrlCHUCVUHT.Name = "xrlCHUCVUHT";
            this.xrlCHUCVUHT.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrlCHUCVUHT.SizeF = new System.Drawing.SizeF(440.7033F, 58.42004F);
            this.xrlCHUCVUHT.StylePriority.UseFont = false;
            this.xrlCHUCVUHT.StylePriority.UseTextAlignment = false;
            this.xrlCHUCVUHT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Dpi = 254F;
            this.xrLabel12.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(852.8254F, 1361.505F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(370.6859F, 58.41992F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "Chức danh hiện tại:                               ";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrlChucDanhHT
            // 
            this.xrlChucDanhHT.Dpi = 254F;
            this.xrlChucDanhHT.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlChucDanhHT.LocationFloat = new DevExpress.Utils.PointFloat(1225.024F, 1361.505F);
            this.xrlChucDanhHT.Name = "xrlChucDanhHT";
            this.xrlChucDanhHT.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrlChucDanhHT.SizeF = new System.Drawing.SizeF(464.9752F, 58.4198F);
            this.xrlChucDanhHT.StylePriority.UseFont = false;
            this.xrlChucDanhHT.StylePriority.UseTextAlignment = false;
            this.xrlChucDanhHT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Dpi = 254F;
            this.xrLabel10.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1419.925F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(1690.001F, 58.42004F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "(Về chính quyền hoặc Đảng, đoàn thể, kể cả chức vụ kiêm nhiệm)";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_TrinhDoPhoT
            // 
            this.xrl_TrinhDoPhoT.Dpi = 254F;
            this.xrl_TrinhDoPhoT.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_TrinhDoPhoT.LocationFloat = new DevExpress.Utils.PointFloat(657.9999F, 1769.81F);
            this.xrl_TrinhDoPhoT.Name = "xrl_TrinhDoPhoT";
            this.xrl_TrinhDoPhoT.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_TrinhDoPhoT.SizeF = new System.Drawing.SizeF(1031.001F, 58.42004F);
            this.xrl_TrinhDoPhoT.StylePriority.UseFont = false;
            this.xrl_TrinhDoPhoT.StylePriority.UseTextAlignment = false;
            this.xrl_TrinhDoPhoT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NGOAINGUTD
            // 
            this.xrl_NGOAINGUTD.Dpi = 254F;
            this.xrl_NGOAINGUTD.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NGOAINGUTD.LocationFloat = new DevExpress.Utils.PointFloat(325F, 2100.032F);
            this.xrl_NGOAINGUTD.Name = "xrl_NGOAINGUTD";
            this.xrl_NGOAINGUTD.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NGOAINGUTD.SizeF = new System.Drawing.SizeF(513.014F, 58.42017F);
            this.xrl_NGOAINGUTD.StylePriority.UseFont = false;
            this.xrl_NGOAINGUTD.StylePriority.UseTextAlignment = false;
            this.xrl_NGOAINGUTD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_Tin_HOCTD
            // 
            this.xrl_Tin_HOCTD.Dpi = 254F;
            this.xrl_Tin_HOCTD.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_Tin_HOCTD.LocationFloat = new DevExpress.Utils.PointFloat(1106.676F, 2100.032F);
            this.xrl_Tin_HOCTD.Name = "xrl_Tin_HOCTD";
            this.xrl_Tin_HOCTD.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_Tin_HOCTD.SizeF = new System.Drawing.SizeF(583.3243F, 58.42017F);
            this.xrl_Tin_HOCTD.StylePriority.UseFont = false;
            this.xrl_Tin_HOCTD.StylePriority.UseTextAlignment = false;
            this.xrl_Tin_HOCTD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Dpi = 254F;
            this.xrLabel15.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1698.161F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(335.5901F, 58.42004F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "Phụ cấp chức vụ:";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbPhuCapCV
            // 
            this.lbPhuCapCV.Dpi = 254F;
            this.lbPhuCapCV.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPhuCapCV.LocationFloat = new DevExpress.Utils.PointFloat(335.5895F, 1698.161F);
            this.lbPhuCapCV.Name = "lbPhuCapCV";
            this.lbPhuCapCV.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbPhuCapCV.SizeF = new System.Drawing.SizeF(343.8677F, 58.42004F);
            this.lbPhuCapCV.StylePriority.UseFont = false;
            this.lbPhuCapCV.StylePriority.UseTextAlignment = false;
            this.lbPhuCapCV.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel39
            // 
            this.xrLabel39.Dpi = 254F;
            this.xrLabel39.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel39.LocationFloat = new DevExpress.Utils.PointFloat(679.457F, 1698.161F);
            this.xrLabel39.Name = "xrLabel39";
            this.xrLabel39.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel39.SizeF = new System.Drawing.SizeF(306.1042F, 58.42004F);
            this.xrLabel39.StylePriority.UseFont = false;
            this.xrLabel39.StylePriority.UseTextAlignment = false;
            this.xrLabel39.Text = ", Phụ cấp khác:";
            this.xrLabel39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbPhuCap
            // 
            this.lbPhuCap.Dpi = 254F;
            this.lbPhuCap.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPhuCap.LocationFloat = new DevExpress.Utils.PointFloat(985.561F, 1698.161F);
            this.lbPhuCap.Name = "lbPhuCap";
            this.lbPhuCap.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbPhuCap.SizeF = new System.Drawing.SizeF(704.3818F, 58.42004F);
            this.lbPhuCap.StylePriority.UseFont = false;
            this.lbPhuCap.StylePriority.UseTextAlignment = false;
            this.lbPhuCap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel36
            // 
            this.xrLabel36.Dpi = 254F;
            this.xrLabel36.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(679.457F, 1626.723F);
            this.xrLabel36.Name = "xrLabel36";
            this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel36.SizeF = new System.Drawing.SizeF(281.9528F, 58.42004F);
            this.xrLabel36.StylePriority.UseFont = false;
            this.xrLabel36.StylePriority.UseTextAlignment = false;
            this.xrLabel36.Text = ", Ngày hưởng:";
            this.xrLabel36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbNgayHuongLuong
            // 
            this.lbNgayHuongLuong.Dpi = 254F;
            this.lbNgayHuongLuong.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNgayHuongLuong.LocationFloat = new DevExpress.Utils.PointFloat(961.41F, 1626.723F);
            this.lbNgayHuongLuong.Name = "lbNgayHuongLuong";
            this.lbNgayHuongLuong.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbNgayHuongLuong.SizeF = new System.Drawing.SizeF(728.5799F, 58.42004F);
            this.lbNgayHuongLuong.StylePriority.UseFont = false;
            this.lbNgayHuongLuong.StylePriority.UseTextAlignment = false;
            this.lbNgayHuongLuong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel34
            // 
            this.xrLabel34.Dpi = 254F;
            this.xrLabel34.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(374.0179F, 1626.723F);
            this.xrLabel34.Name = "xrLabel34";
            this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel34.SizeF = new System.Drawing.SizeF(157F, 58.42004F);
            this.xrLabel34.StylePriority.UseFont = false;
            this.xrLabel34.StylePriority.UseTextAlignment = false;
            this.xrLabel34.Text = ", Hệ số:";
            this.xrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbHeSo
            // 
            this.lbHeSo.Dpi = 254F;
            this.lbHeSo.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeSo.LocationFloat = new DevExpress.Utils.PointFloat(531.1703F, 1626.723F);
            this.lbHeSo.Name = "lbHeSo";
            this.lbHeSo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbHeSo.SizeF = new System.Drawing.SizeF(148.287F, 58.42004F);
            this.lbHeSo.StylePriority.UseFont = false;
            this.lbHeSo.StylePriority.UseTextAlignment = false;
            this.lbHeSo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbBacLuong
            // 
            this.lbBacLuong.Dpi = 254F;
            this.lbBacLuong.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBacLuong.LocationFloat = new DevExpress.Utils.PointFloat(222.2448F, 1626.723F);
            this.lbBacLuong.Name = "lbBacLuong";
            this.lbBacLuong.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbBacLuong.SizeF = new System.Drawing.SizeF(151.7731F, 58.42004F);
            this.lbBacLuong.StylePriority.UseFont = false;
            this.lbBacLuong.StylePriority.UseTextAlignment = false;
            this.lbBacLuong.Text = "Bac 12";
            this.lbBacLuong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbMaNgachCC
            // 
            this.lbMaNgachCC.Dpi = 254F;
            this.lbMaNgachCC.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMaNgachCC.LocationFloat = new DevExpress.Utils.PointFloat(1449.607F, 1557.931F);
            this.lbMaNgachCC.Name = "lbMaNgachCC";
            this.lbMaNgachCC.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbMaNgachCC.SizeF = new System.Drawing.SizeF(240.3939F, 58.42004F);
            this.lbMaNgachCC.StylePriority.UseFont = false;
            this.lbMaNgachCC.StylePriority.UseTextAlignment = false;
            this.lbMaNgachCC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel31
            // 
            this.xrLabel31.Dpi = 254F;
            this.xrLabel31.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(1218.221F, 1557.931F);
            this.xrLabel31.Name = "xrLabel31";
            this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel31.SizeF = new System.Drawing.SizeF(228.6379F, 58.42004F);
            this.xrLabel31.StylePriority.UseFont = false;
            this.xrLabel31.StylePriority.UseTextAlignment = false;
            this.xrLabel31.Text = ", Mã ngạch:";
            this.xrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbNgachCC
            // 
            this.lbNgachCC.Dpi = 254F;
            this.lbNgachCC.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNgachCC.LocationFloat = new DevExpress.Utils.PointFloat(638.4869F, 1557.931F);
            this.lbNgachCC.Name = "lbNgachCC";
            this.lbNgachCC.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbNgachCC.SizeF = new System.Drawing.SizeF(579.7344F, 58.42004F);
            this.lbNgachCC.StylePriority.UseFont = false;
            this.lbNgachCC.StylePriority.UseTextAlignment = false;
            this.lbNgachCC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Dpi = 254F;
            this.xrLabel13.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1626.723F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(222.2455F, 58.42004F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "Bậc lương:";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Dpi = 254F;
            this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1557.931F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(638F, 58.42004F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "14) Ngạch công chức (viên chức):";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel35
            // 
            this.xrLabel35.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel35.Dpi = 254F;
            this.xrLabel35.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(771.9999F, 1968.589F);
            this.xrLabel35.Name = "xrLabel35";
            this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel35.SizeF = new System.Drawing.SizeF(918.0003F, 116.4171F);
            this.xrLabel35.StylePriority.UseBorders = false;
            this.xrLabel35.StylePriority.UseFont = false;
            this.xrLabel35.StylePriority.UseTextAlignment = false;
            this.xrLabel35.Text = "(Chuyên viên cao cấp, chuyên viên chính, chuyên viên, cán sự,...)";
            this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel33
            // 
            this.xrLabel33.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel33.Dpi = 254F;
            this.xrLabel33.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(8.074442E-05F, 1968.589F);
            this.xrLabel33.Name = "xrLabel33";
            this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel33.SizeF = new System.Drawing.SizeF(771.9999F, 116.4171F);
            this.xrLabel33.StylePriority.UseBorders = false;
            this.xrLabel33.StylePriority.UseFont = false;
            this.xrLabel33.StylePriority.UseTextAlignment = false;
            this.xrLabel33.Text = "(Cao cấp, trung cấp, sơ cấp và tương đương)";
            this.xrLabel33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel38
            // 
            this.xrLabel38.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel38.Dpi = 254F;
            this.xrLabel38.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(838.014F, 2158.452F);
            this.xrLabel38.Name = "xrLabel38";
            this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel38.SizeF = new System.Drawing.SizeF(851.9847F, 55.5625F);
            this.xrLabel38.StylePriority.UseBorders = false;
            this.xrLabel38.StylePriority.UseFont = false;
            this.xrLabel38.StylePriority.UseTextAlignment = false;
            this.xrLabel38.Text = "(Trình độ A, B, C, ...)";
            this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel37
            // 
            this.xrLabel37.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel37.Dpi = 254F;
            this.xrLabel37.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(8.074442E-05F, 2158.452F);
            this.xrLabel37.Name = "xrLabel37";
            this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel37.SizeF = new System.Drawing.SizeF(838.0143F, 55.5625F);
            this.xrLabel37.StylePriority.UseBorders = false;
            this.xrLabel37.StylePriority.UseFont = false;
            this.xrLabel37.StylePriority.UseTextAlignment = false;
            this.xrLabel37.Text = "(Tên ngoại ngữ + Trình độ A, B, C, D...)";
            this.xrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel5.Dpi = 254F;
            this.xrLabel5.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 319.0876F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(1690.001F, 71.86069F);
            this.xrLabel5.StylePriority.UseBorders = false;
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "SƠ YẾU LÝ LỊCH CÁN BỘ, CÔNG CHỨC";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPictureBox1.Dpi = 254F;
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 408.834F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(278.8706F, 380.3652F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.AutoSize;
            this.xrPictureBox1.StylePriority.UseBorders = false;
            // 
            // xrl_TEN_KHAC
            // 
            this.xrl_TEN_KHAC.Dpi = 254F;
            this.xrl_TEN_KHAC.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_TEN_KHAC.LocationFloat = new DevExpress.Utils.PointFloat(301.4294F, 487.0453F);
            this.xrl_TEN_KHAC.Name = "xrl_TEN_KHAC";
            this.xrl_TEN_KHAC.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_TEN_KHAC.SizeF = new System.Drawing.SizeF(313.2633F, 58.4201F);
            this.xrl_TEN_KHAC.StylePriority.UseFont = false;
            this.xrl_TEN_KHAC.StylePriority.UseTextAlignment = false;
            this.xrl_TEN_KHAC.Text = "2) Tên gọi khác: ";
            this.xrl_TEN_KHAC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NGAY_SINH
            // 
            this.xrl_NGAY_SINH.Dpi = 254F;
            this.xrl_NGAY_SINH.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NGAY_SINH.LocationFloat = new DevExpress.Utils.PointFloat(301.4295F, 569.2773F);
            this.xrl_NGAY_SINH.Name = "xrl_NGAY_SINH";
            this.xrl_NGAY_SINH.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NGAY_SINH.SizeF = new System.Drawing.SizeF(266.701F, 58.42004F);
            this.xrl_NGAY_SINH.StylePriority.UseFont = false;
            this.xrl_NGAY_SINH.StylePriority.UseTextAlignment = false;
            this.xrl_NGAY_SINH.Text = "3) Sinh ngày:  ";
            this.xrl_NGAY_SINH.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_HO_TEN
            // 
            this.xrl_HO_TEN.Dpi = 254F;
            this.xrl_HO_TEN.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_HO_TEN.LocationFloat = new DevExpress.Utils.PointFloat(301.4294F, 408.8338F);
            this.xrl_HO_TEN.Name = "xrl_HO_TEN";
            this.xrl_HO_TEN.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_HO_TEN.SizeF = new System.Drawing.SizeF(430.9656F, 58.41983F);
            this.xrl_HO_TEN.StylePriority.UseFont = false;
            this.xrl_HO_TEN.StylePriority.UseTextAlignment = false;
            this.xrl_HO_TEN.Text = "1) Họ và tên khai sinh:  ";
            this.xrl_HO_TEN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_TON_GIAO
            // 
            this.xrl_TON_GIAO.Dpi = 254F;
            this.xrl_TON_GIAO.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_TON_GIAO.LocationFloat = new DevExpress.Utils.PointFloat(679.4589F, 806.7676F);
            this.xrl_TON_GIAO.Name = "xrl_TON_GIAO";
            this.xrl_TON_GIAO.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_TON_GIAO.SizeF = new System.Drawing.SizeF(242.0205F, 58.42004F);
            this.xrl_TON_GIAO.StylePriority.UseFont = false;
            this.xrl_TON_GIAO.StylePriority.UseTextAlignment = false;
            this.xrl_TON_GIAO.Text = "7) Tôn giáo:           ";
            this.xrl_TON_GIAO.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NoiSinh
            // 
            this.xrl_NoiSinh.Dpi = 254F;
            this.xrl_NoiSinh.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NoiSinh.LocationFloat = new DevExpress.Utils.PointFloat(301.4295F, 648.7585F);
            this.xrl_NoiSinh.Name = "xrl_NoiSinh";
            this.xrl_NoiSinh.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NoiSinh.SizeF = new System.Drawing.SizeF(1388.571F, 58.41986F);
            this.xrl_NoiSinh.StylePriority.UseFont = false;
            this.xrl_NoiSinh.StylePriority.UseTextAlignment = false;
            this.xrl_NoiSinh.Text = "4) Nơi sinh: ";
            this.xrl_NoiSinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_BI_DANH
            // 
            this.xrl_BI_DANH.Dpi = 254F;
            this.xrl_BI_DANH.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_BI_DANH.LocationFloat = new DevExpress.Utils.PointFloat(614.6926F, 487.0455F);
            this.xrl_BI_DANH.Name = "xrl_BI_DANH";
            this.xrl_BI_DANH.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_BI_DANH.SizeF = new System.Drawing.SizeF(1075.307F, 58.42001F);
            this.xrl_BI_DANH.StylePriority.UseFont = false;
            this.xrl_BI_DANH.StylePriority.UseTextAlignment = false;
            this.xrl_BI_DANH.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_QueQuan
            // 
            this.xrl_QueQuan.Dpi = 254F;
            this.xrl_QueQuan.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_QueQuan.LocationFloat = new DevExpress.Utils.PointFloat(301.4295F, 730.7791F);
            this.xrl_QueQuan.Name = "xrl_QueQuan";
            this.xrl_QueQuan.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_QueQuan.SizeF = new System.Drawing.SizeF(1388.571F, 58.41998F);
            this.xrl_QueQuan.StylePriority.UseFont = false;
            this.xrl_QueQuan.StylePriority.UseTextAlignment = false;
            this.xrl_QueQuan.Text = "5) Quê quán: ";
            this.xrl_QueQuan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_SHCCVC
            // 
            this.xrl_SHCCVC.Dpi = 254F;
            this.xrl_SHCCVC.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_SHCCVC.LocationFloat = new DevExpress.Utils.PointFloat(0F, 180.1283F);
            this.xrl_SHCCVC.Name = "xrl_SHCCVC";
            this.xrl_SHCCVC.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_SHCCVC.SizeF = new System.Drawing.SizeF(1690.001F, 58.41998F);
            this.xrl_SHCCVC.StylePriority.UseFont = false;
            this.xrl_SHCCVC.StylePriority.UseTextAlignment = false;
            this.xrl_SHCCVC.Text = "Số hiệu công chức, viên chức:         ";
            this.xrl_SHCCVC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_DAN_TOC
            // 
            this.xrl_DAN_TOC.Dpi = 254F;
            this.xrl_DAN_TOC.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_DAN_TOC.LocationFloat = new DevExpress.Utils.PointFloat(0F, 806.7678F);
            this.xrl_DAN_TOC.Name = "xrl_DAN_TOC";
            this.xrl_DAN_TOC.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_DAN_TOC.SizeF = new System.Drawing.SizeF(222.2455F, 58.41998F);
            this.xrl_DAN_TOC.StylePriority.UseFont = false;
            this.xrl_DAN_TOC.StylePriority.UseTextAlignment = false;
            this.xrl_DAN_TOC.Text = "6) Dân tộc:                   ";
            this.xrl_DAN_TOC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrc_NAM
            // 
            this.xrc_NAM.Dpi = 254F;
            this.xrc_NAM.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrc_NAM.LocationFloat = new DevExpress.Utils.PointFloat(1233.421F, 569.2775F);
            this.xrc_NAM.Name = "xrc_NAM";
            this.xrc_NAM.SizeF = new System.Drawing.SizeF(154.5095F, 58.41998F);
            this.xrc_NAM.StylePriority.UseFont = false;
            this.xrc_NAM.StylePriority.UseTextAlignment = false;
            this.xrc_NAM.Text = "Nam";
            this.xrc_NAM.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrl_CQDVCTQ
            // 
            this.xrl_CQDVCTQ.Dpi = 254F;
            this.xrl_CQDVCTQ.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_CQDVCTQ.LocationFloat = new DevExpress.Utils.PointFloat(0F, 121.7083F);
            this.xrl_CQDVCTQ.Name = "xrl_CQDVCTQ";
            this.xrl_CQDVCTQ.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_CQDVCTQ.SizeF = new System.Drawing.SizeF(1690F, 58.41998F);
            this.xrl_CQDVCTQ.StylePriority.UseFont = false;
            this.xrl_CQDVCTQ.StylePriority.UseTextAlignment = false;
            this.xrl_CQDVCTQ.Text = "Cơ quan, đơn vị có thẩm quyền quản lý CBCC:  ";
            this.xrl_CQDVCTQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrc_NU
            // 
            this.xrc_NU.Dpi = 254F;
            this.xrc_NU.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrc_NU.LocationFloat = new DevExpress.Utils.PointFloat(1389.31F, 569.2773F);
            this.xrc_NU.Name = "xrc_NU";
            this.xrc_NU.SizeF = new System.Drawing.SizeF(130.3877F, 58.42004F);
            this.xrc_NU.StylePriority.UseFont = false;
            this.xrc_NU.StylePriority.UseTextAlignment = false;
            this.xrc_NU.Text = "Nữ";
            this.xrc_NU.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrl_CQDVSD
            // 
            this.xrl_CQDVSD.Dpi = 254F;
            this.xrl_CQDVSD.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_CQDVSD.LocationFloat = new DevExpress.Utils.PointFloat(0F, 238.5483F);
            this.xrl_CQDVSD.Name = "xrl_CQDVSD";
            this.xrl_CQDVSD.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_CQDVSD.SizeF = new System.Drawing.SizeF(1690F, 58.42F);
            this.xrl_CQDVSD.StylePriority.UseFont = false;
            this.xrl_CQDVSD.StylePriority.UseTextAlignment = false;
            this.xrl_CQDVSD.Text = "Cơ quan, đơn vị Cơ quan, đơn vị sử dụng CBCC:  ";
            this.xrl_CQDVSD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_NGAYSINH
            // 
            this.xrl_NGAYSINH.Dpi = 254F;
            this.xrl_NGAYSINH.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_NGAYSINH.LocationFloat = new DevExpress.Utils.PointFloat(568.1303F, 569.2775F);
            this.xrl_NGAYSINH.Name = "xrl_NGAYSINH";
            this.xrl_NGAYSINH.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_NGAYSINH.SizeF = new System.Drawing.SizeF(403.3561F, 58.41998F);
            this.xrl_NGAYSINH.StylePriority.UseFont = false;
            this.xrl_NGAYSINH.StylePriority.UseTextAlignment = false;
            this.xrl_NGAYSINH.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_GIOI_TINH
            // 
            this.xrl_GIOI_TINH.Dpi = 254F;
            this.xrl_GIOI_TINH.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_GIOI_TINH.LocationFloat = new DevExpress.Utils.PointFloat(971.4866F, 569.2775F);
            this.xrl_GIOI_TINH.Name = "xrl_GIOI_TINH";
            this.xrl_GIOI_TINH.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_GIOI_TINH.SizeF = new System.Drawing.SizeF(261.9344F, 58.4201F);
            this.xrl_GIOI_TINH.StylePriority.UseFont = false;
            this.xrl_GIOI_TINH.StylePriority.UseTextAlignment = false;
            this.xrl_GIOI_TINH.Text = "Giới tính: (v)";
            this.xrl_GIOI_TINH.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrlHOTEN_CBCC
            // 
            this.xrlHOTEN_CBCC.Dpi = 254F;
            this.xrlHOTEN_CBCC.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlHOTEN_CBCC.LocationFloat = new DevExpress.Utils.PointFloat(732.3952F, 408.8338F);
            this.xrlHOTEN_CBCC.Name = "xrlHOTEN_CBCC";
            this.xrlHOTEN_CBCC.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrlHOTEN_CBCC.SizeF = new System.Drawing.SizeF(957.6039F, 58.42001F);
            this.xrlHOTEN_CBCC.StylePriority.UseFont = false;
            this.xrlHOTEN_CBCC.StylePriority.UseTextAlignment = false;
            this.xrlHOTEN_CBCC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrlDanTOC
            // 
            this.xrlDanTOC.Dpi = 254F;
            this.xrlDanTOC.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlDanTOC.LocationFloat = new DevExpress.Utils.PointFloat(222.2449F, 806.7678F);
            this.xrlDanTOC.Name = "xrlDanTOC";
            this.xrlDanTOC.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrlDanTOC.SizeF = new System.Drawing.SizeF(457.2124F, 58.42004F);
            this.xrlDanTOC.StylePriority.UseFont = false;
            this.xrlDanTOC.StylePriority.UseTextAlignment = false;
            this.xrlDanTOC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrlTONGIAO
            // 
            this.xrlTONGIAO.Dpi = 254F;
            this.xrlTONGIAO.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlTONGIAO.LocationFloat = new DevExpress.Utils.PointFloat(921.4795F, 806.7676F);
            this.xrlTONGIAO.Name = "xrlTONGIAO";
            this.xrlTONGIAO.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrlTONGIAO.SizeF = new System.Drawing.SizeF(768.5208F, 58.42004F);
            this.xrlTONGIAO.StylePriority.UseFont = false;
            this.xrlTONGIAO.StylePriority.UseTextAlignment = false;
            this.xrlTONGIAO.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 875.7711F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(410F, 58.41992F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Thành phần bản thân:";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrl_ThanhPhanBT
            // 
            this.xrl_ThanhPhanBT.Dpi = 254F;
            this.xrl_ThanhPhanBT.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrl_ThanhPhanBT.LocationFloat = new DevExpress.Utils.PointFloat(410F, 875.7714F);
            this.xrl_ThanhPhanBT.Name = "xrl_ThanhPhanBT";
            this.xrl_ThanhPhanBT.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrl_ThanhPhanBT.SizeF = new System.Drawing.SizeF(474.1492F, 58.41998F);
            this.xrl_ThanhPhanBT.StylePriority.UseFont = false;
            this.xrl_ThanhPhanBT.StylePriority.UseTextAlignment = false;
            this.xrl_ThanhPhanBT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Dpi = 254F;
            this.xrLabel14.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(890.2787F, 875.7711F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(401.3056F, 58.41998F);
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.Text = "Thành phần gia đình:";
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrlTPGD
            // 
            this.xrlTPGD.Dpi = 254F;
            this.xrlTPGD.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlTPGD.LocationFloat = new DevExpress.Utils.PointFloat(1291.584F, 875.7714F);
            this.xrlTPGD.Name = "xrlTPGD";
            this.xrlTPGD.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrlTPGD.SizeF = new System.Drawing.SizeF(398.4058F, 58.42004F);
            this.xrlTPGD.StylePriority.UseFont = false;
            this.xrlTPGD.StylePriority.UseTextAlignment = false;
            this.xrlTPGD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrlHOKHAUTT
            // 
            this.xrlHOKHAUTT.Dpi = 254F;
            this.xrlHOKHAUTT.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlHOKHAUTT.LocationFloat = new DevExpress.Utils.PointFloat(0F, 947.4207F);
            this.xrlHOKHAUTT.Name = "xrlHOKHAUTT";
            this.xrlHOKHAUTT.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrlHOKHAUTT.SizeF = new System.Drawing.SizeF(1690.001F, 58.42004F);
            this.xrlHOKHAUTT.StylePriority.UseFont = false;
            this.xrlHOKHAUTT.StylePriority.UseTextAlignment = false;
            this.xrlHOKHAUTT.Text = "8) Nơi đăng ký hộ khẩu thường trú: ";
            this.xrlHOKHAUTT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Dpi = 254F;
            this.xrLabel7.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1016.424F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(1690F, 58.41998F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "(Số nhà, đường phố, thành phố, xóm, thôn, xã, huyện, tỉnh)";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel32
            // 
            this.xrLabel32.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel32.Dpi = 254F;
            this.xrLabel32.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel32.Name = "xrLabel32";
            this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel32.SizeF = new System.Drawing.SizeF(1690.001F, 121.7083F);
            this.xrLabel32.StylePriority.UseBorders = false;
            this.xrLabel32.StylePriority.UseFont = false;
            this.xrLabel32.StylePriority.UseTextAlignment = false;
            this.xrLabel32.Text = "Mẫu 2C-BNV/2008 ban hành kèm theo Quyết định số 02/2008/QĐ-BNV ngày 06/10/2008 củ" +
    "a Bộ trưởng Bộ Nội vụ";
            this.xrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 154.7813F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 156.5451F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel32,
            this.xrLabel7,
            this.xrlHOKHAUTT,
            this.xrlTPGD,
            this.xrLabel14,
            this.xrl_ThanhPhanBT,
            this.xrLabel2,
            this.xrlTONGIAO,
            this.xrlDanTOC,
            this.xrlHOTEN_CBCC,
            this.xrl_GIOI_TINH,
            this.xrl_NGAYSINH,
            this.xrl_CQDVSD,
            this.xrc_NU,
            this.xrl_CQDVCTQ,
            this.xrc_NAM,
            this.xrl_DAN_TOC,
            this.xrl_SHCCVC,
            this.xrl_QueQuan,
            this.xrl_BI_DANH,
            this.xrl_NoiSinh,
            this.xrl_TON_GIAO,
            this.xrl_HO_TEN,
            this.xrl_NGAY_SINH,
            this.xrl_TEN_KHAC,
            this.xrPictureBox1,
            this.xrLabel5,
            this.xrLabel37,
            this.xrLabel38,
            this.xrLabel33,
            this.xrLabel35,
            this.xrLabel4,
            this.xrLabel13,
            this.lbNgachCC,
            this.xrLabel31,
            this.lbMaNgachCC,
            this.lbBacLuong,
            this.lbHeSo,
            this.xrLabel34,
            this.lbNgayHuongLuong,
            this.xrLabel36,
            this.lbPhuCap,
            this.xrLabel39,
            this.lbPhuCapCV,
            this.xrLabel15,
            this.xrl_Tin_HOCTD,
            this.xrl_NGOAINGUTD,
            this.xrl_TrinhDoPhoT,
            this.xrLabel10,
            this.xrlChucDanhHT,
            this.xrLabel12,
            this.xrlCHUCVUHT,
            this.xrl_CoQuanTD,
            this.xrl_NGHE_KHI_DC_TUYEN,
            this.xrLabel8,
            this.xrlNOI_O_HIENNAY,
            this.xrl_TDQCHINHTRI,
            this.xrl_TD_VH,
            this.xrl_NGAY_VAO_CT,
            this.xrl_TRINHDO,
            this.xrl_TDCHUYENMON,
            this.xrl_NGAY_TUYEN_DAUTIEN,
            this.xrl_TD_CHINHTRI,
            this.xrl_TDQLNN,
            this.xrl_NGOAI_NGU,
            this.xrl_TINHOC,
            this.xrl_CVChinhDuocGiao,
            this.xrl_TD_QUANLY,
            this.xrl_CHUCVU,
            this.xrl_NGAY_TUYEN_DAU,
            this.xrl_CQTDDAUTIEN,
            this.xrLabel48,
            this.xrLabel46,
            this.xrLabel47,
            this.xrLabel45,
            this.xrLabel16,
            this.xrl_KYLUAT,
            this.xrLabel6,
            this.xrl_KHENTHUONG,
            this.xrLabel3,
            this.xrLabel29,
            this.xrLabel27,
            this.xrLabel26,
            this.xrLabel25,
            this.xrLabel24,
            this.xrLabel23,
            this.xrLabel22,
            this.xrLabel21,
            this.xrLabel20,
            this.xrLabel19,
            this.xrLabel18,
            this.xrLabel17,
            this.xrLabel11,
            this.xrlNgayVaoDoan,
            this.xrlNGAYCTVAODANG,
            this.xrlNGAYVAODANG,
            this.xrl_CAPBAC_QD,
            this.xrl_CHUCVU_DANG,
            this.xrl_CAN_NANG,
            this.xrl_NGAY_TGCM,
            this.xrl_NGAYCAP_CMND,
            this.xrl_NOI_KN_DANG,
            this.xrl_CHUCVU_DOAN,
            this.xrLabel43,
            this.xrl_NGAY_CAP_BH,
            this.xrLabel42,
            this.xrlDanhHieuPhongTang,
            this.xrl_CHIEU_CAO,
            this.xrl_SO_THE_BHXH,
            this.xrl_NGAYCAP_BHXH,
            this.xrl_NHOM_MAU,
            this.xrl_NGAY_VAO_DANG,
            this.xrl_TT_SUCKHOE,
            this.xrLabel44,
            this.xrl_SO_CMND,
            this.xrLabel40,
            this.xrLabel41,
            this.xrl_LOAI_CS,
            this.xrl_NGAY_CT_VAO_DANG,
            this.xrl_NGAY_VAO_DOAN,
            this.xrl_NGAY_RA_QD,
            this.xrl_NANGLUC,
            this.xrsub_QT_DT,
            this.xrsub_QTCT,
            this.xrLabel50,
            this.xrLabel54,
            this.xrLabel49,
            this.xrLabel52,
            this.xrl_LICHSUBANTHAN1,
            this.xrl_LICHSUBANTHAN2,
            this.xrl_LICHSUBANTHAN3,
            this.xrSub_DienBienQTLuong,
            this.xrsub_HOSO_QH_GIADINH,
            this.xrLabel1,
            this.xrNhanXet,
            this.xrLabel112,
            this.xrLabel30,
            this.xrtngayketxuat,
            this.xrLabel113,
            this.xrLabel115,
            this.sub_QuanHe_GiaDinh_Vo,
            this.xrLabel9,
            this.xrLabel28,
            this.xrLabel51});
            this.ReportHeader.Dpi = 254F;
            this.ReportHeader.HeightF = 5367.6F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrsub_QT_DT
            // 
            this.xrsub_QT_DT.Dpi = 254F;
            this.xrsub_QT_DT.LocationFloat = new DevExpress.Utils.PointFloat(5.382962E-05F, 3348.875F);
            this.xrsub_QT_DT.Name = "xrsub_QT_DT";
            this.xrsub_QT_DT.ReportSource = new Web.Core.Framework.Report.sub_ProcessTraining();
            this.xrsub_QT_DT.SizeF = new System.Drawing.SizeF(1689.999F, 58.41992F);
            // 
            // xrsub_QTCT
            // 
            this.xrsub_QTCT.Dpi = 254F;
            this.xrsub_QTCT.LocationFloat = new DevExpress.Utils.PointFloat(5.382962E-05F, 3420.524F);
            this.xrsub_QTCT.Name = "xrsub_QTCT";
            this.xrsub_QTCT.ReportSource = new Web.Core.Framework.Report.sub_ProcessWorking();
            this.xrsub_QTCT.SizeF = new System.Drawing.SizeF(1689.999F, 58.41968F);
            // 
            // xrSub_DienBienQTLuong
            // 
            this.xrSub_DienBienQTLuong.Dpi = 254F;
            this.xrSub_DienBienQTLuong.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4522.555F);
            this.xrSub_DienBienQTLuong.Name = "xrSub_DienBienQTLuong";
            this.xrSub_DienBienQTLuong.ReportSource = new Web.Core.Framework.Report.sub_ProcessSalary();
            this.xrSub_DienBienQTLuong.SizeF = new System.Drawing.SizeF(1689.993F, 111.3369F);
            // 
            // xrsub_HOSO_QH_GIADINH
            // 
            this.xrsub_HOSO_QH_GIADINH.Dpi = 254F;
            this.xrsub_HOSO_QH_GIADINH.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4240.616F);
            this.xrsub_HOSO_QH_GIADINH.Name = "xrsub_HOSO_QH_GIADINH";
            this.xrsub_HOSO_QH_GIADINH.ReportSource = new Web.Core.Framework.Report.sub_FamilyRelationship();
            this.xrsub_HOSO_QH_GIADINH.SizeF = new System.Drawing.SizeF(1690.001F, 111.7599F);
            // 
            // sub_QuanHe_GiaDinh_Vo
            // 
            this.sub_QuanHe_GiaDinh_Vo.Dpi = 254F;
            this.sub_QuanHe_GiaDinh_Vo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4410.795F);
            this.sub_QuanHe_GiaDinh_Vo.Name = "sub_QuanHe_GiaDinh_Vo";
            this.sub_QuanHe_GiaDinh_Vo.ReportSource = new Web.Core.Framework.Report.sub_FamilyRelationship();
            this.sub_QuanHe_GiaDinh_Vo.SizeF = new System.Drawing.SizeF(1689.982F, 111.7598F);
            // 
            // rp_InformationEmployeeDetail
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader});
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(247, 163, 155, 157);
            this.PageHeight = 2970;
            this.PageWidth = 2100;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}
