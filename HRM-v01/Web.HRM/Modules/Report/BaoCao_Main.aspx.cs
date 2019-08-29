using System;
using System.ComponentModel;
using CCVC.Web;
using DevExpress.XtraReports.UI;
using Web.Core.Framework;
using Web.Core.Framework.Common;
using Web.Core.Framework.Report;
using Web.Core.Object.Report;


namespace Web.HRM.Modules.Report
{
    public partial class BaoCao_Main : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportViewer1.Report = CreateReport();
        }
        public XtraReport CreateReport()
        {
            var type = GetValueFromDescription<ReportTypeEnum>(Request.QueryString["type"]);
            switch (type)
            {
                #region Báo cáo theo quy định

                // Danh sách và tiền lương CBCNV Công ty"
                //case ReportTypeEnum.BusinessSalaryDistrictCivilServants:
                //    return BusinessSalaryDistrictCivilServants();
                    
                #endregion

                #region Nghiệp vụ cán bộ
                //20. Báo cáo danh sách cán bộ nghỉ hưu
                //case ReportTypeEnum.EmployeeRetirement:
                //    return EmployeeRetirement();

                //Báo cáo danh sách cán bộ nhân viên sắp hết hạn hợp đồng 
                case ReportTypeEnum.EmployeeContractExpiration:
                    return EmployeeContractExpiration();
                // Báo cáo danh sách nhân viên được tặng quà mồng 8-3
                case ReportTypeEnum.EmployeeReceiveGift8_3:
                    return EmployeeReceiveGift8_3();

                // Báo cáo nhân sự
                //case ReportTypeEnum.BusinessEmployee:
                //    return BusinessEmployee();

                // Báo cáo danh sách CBCNV là đảng viên
                //case ReportTypeEnum.BusinessCommunityPartyVietnamMember:
                //    return BusinessCommunityPartyVietnamMember();

                // Báo cáo danh sách CBCNV là đoàn viên
                //case ReportTypeEnum.BusinessVietnamYoungUnionMember:
                //    return BusinessVietnamYoungUnionMember();

                // Báo cáo danh sách CBCNV là quân nhân
                //case ReportTypeEnum.BusinessMilitary:
                //    return BusinessMilitary();

                // Báo cáo danh sách CBCNV theo phòng ban
                case ReportTypeEnum.BusinessEmployeeByDepartment:
                    return BusinessEmployeeByDepartment();

                // Báo cáo danh sách CBCNV được bổ nhiệm
                //case ReportTypeEnum.BusinessAppointment:
                //    return BusinessAppointment();

                // Báo cáo danh sách CBCNV được cử đi biệt phái
                //case ReportTypeEnum.BusinessOnSecondment:
                //    return BusinessOnSecondment();

                // Báo cáo thâm niên công tác của CBCNV
                //case ReportTypeEnum.BusinessSeniority:
                //    return BusinessSeniority();

                // Báo cáo danh sách hợp đồng của CBCNV
                //case ReportTypeEnum.BusinessContract:
                //    return BusinessContract();

                // Báo cáo danh sách CBCNV sinh nhật trong tháng
                //case ReportTypeEnum.BusinessBornInMonth:
                //    return BusinessBornInMonth();

                // Báo cáo danh sách CBCNV nghỉ hưu
                //case ReportTypeEnum.BusinessRetire:
                //    return BusinessRetirement();

                // Danh sách lao động hết hạn HĐLĐ
                //case ReportTypeEnum.BusinessExpiredContract:
                //    return BusinessExpiredContract();

                // Báo cáo tăng tham gia BHYT, BHXH, BHTN
                //case ReportTypeEnum.BusinessIncreaseInsurance:
                //    return BusinessIncreaseInsurance();

                // Báo cáo giảm tham gia BHYT, BHXH, BHTN
                //case ReportTypeEnum.BusinessDecreaseInsurance:
                //    return BusinessDecreaseInsurance();

                // Báo cáo tăng nhân sự 
                //case ReportTypeEnum.BusinessIncreaseHumanResource:
                //    return BusinessIncreaseHumanResource();

                // Báo cáo giảm nhân sự 
                //case ReportTypeEnum.BusinessDecreaseHumanResource:
                //    return BusinessDecreaseHumanResource();

                // Báo điều động nhân sự
                //case ReportTypeEnum.BusinessPersonnelRotation:
                //    return BusinessPersonnelRotation();

                // Danh sách lđ và quỹ lương trích nộp BHXH, BHYT,BHTN
                //case ReportTypeEnum.BusinessSocialInsurance:
                //    return BusinessSocialInsurance();

                // Báo cáo thống kê nghề nghiệp nhân sự
                //case ReportTypeEnum.BusinessOccupation:
                //    return BusinessOccupation();

                // Danh sách các con nhận quà 1/6
                //case ReportTypeEnum.BusinessInternationalChildrenDayGift:
                //    return BusinessInternationalChildrenDayGift();

                //  Danh sách CBCNV nữ
                //case ReportTypeEnum.BusinessFemale:
                //    return BusinessFemale();

                // Báo cáo CBCNV đang trong thời gian thử việc
                //case ReportTypeEnum.BusinessTrainee:
                //    return BusinessTrainee();

                // Báo cáo danh sách CBCNV toàn cơ quan
                case ReportTypeEnum.AllEmployee:
                    return AllEmployee();

                //Danh sách lao động và quỹ lương trích nộp BHXH, BHYT, BHTN
                case ReportTypeEnum.BusinessEmployeeSalaryInsurance:
                    return BusinessEmployeeSalaryInsurance();


                #endregion

                #region Diễn biến lương
                //2. Báo cáo danh sách cán bộ đến kỳ nâng lương
                case ReportTypeEnum.RaiseSalary:
                    return RaiseSalary();

                //3. Báo cáo danh sách cán bộ đến hạn xét vượt khung
                case ReportTypeEnum.SalaryOutOfFrame:
                    return SalaryOutOfFrame();

                // Danh sách mã số thuế CBCNV Công ty
                case ReportTypeEnum.BusinessPersonalTax:
                    return BusinessPersonalTax();

                //Báo cáo danh sách tài khoản ngân hàng của CBCNV
                case ReportTypeEnum.BusinessBankAccount:
                    return BusinessBankAccount();

                // Báo cáo danh sách số người phụ thuộc
                case ReportTypeEnum.BusinessFamilyRelation:
                    return BusinessFamilyRelation();

                // Báo cáo danh sanh CB đến kỳ lên lương
                case ReportTypeEnum.BusinessRaiseSalary:
                    return BusinessRaiseSalary();

                // Báo cáo danh sách CB đến kỳ hạn vượt khung
                case ReportTypeEnum.BusinessOutframe:
                    return BusinessOutframe();

                // Báo cáo diễn biến quá trình lương cán bộ
                case ReportTypeEnum.ProcessSalaryEmployee:
                    return ProcessSalaryEmployee();
                #endregion

                #region Đào tạo - công tác

                // Báo cáo danh sách CBCNV được cử đi đào tạo

                //case ReportTypeEnum.BusinessOnsiteTraining:
                //    return BusinessOnsiteTraining();

                // Báo cáo danh sách CB công tác nước ngoài
                //case ReportTypeEnum.BusinessForeignTraining:
                //    return BusinessForeignTraining();

                #endregion

                #region Khen thưởng - kỷ luật

                // Báo cáo danh sách CBCNV được tặng danh hiệu thi đua
                case ReportTypeEnum.BusinessAward:
                    return BusinessAward();

                // Báo cáo danh sách CBCNV bị kỷ luật
                case ReportTypeEnum.BusinessDiscipline:
                    return BusinessDiscipline();

                //Báo cáo DS CBCNV được khen thưởng
                case ReportTypeEnum.BusinessEmployeeReward:
                    return EmployeeReward();

                #endregion

                #region Chấm công
                //1. Báo cáo chấm công hành chính
                case ReportTypeEnum.TimeKeepingOffice:
                    return TimeKeepingOffice();
                //2. Báo cáo chi tiết chấm công hành chính
                case ReportTypeEnum.TimeKeepingOfficeDetail:
                    return TimeKeepingOfficeDetail();
                //3. Báo cáo chi tiết tăng ca
                case ReportTypeEnum.OvertimeDetail:
                    return OvertimeDetail();
                #endregion

                #region Tiền lương
                //1. Báo cáo lương chi tiết hàng tháng
                case ReportTypeEnum.SalaryDetail:
                    return SalaryDetail();
                //2. Báo cáo lương tháng
                case ReportTypeEnum.SalaryMonth:
                    return SalaryByMonth();
                //3. Báo cáo chuyển khoản ATM
                case ReportTypeEnum.SalaryATM:
                    return SalaryATM();
                #endregion
            }
            return new XtraReport();
        }

        /// <summary>
        /// Get value from title report (description)
        /// </summary>
        /// <typeparam name="ReportTypeEnum"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static ReportTypeEnum GetValueFromDescription<ReportTypeEnum>(string description)
        {
            var type = typeof(ReportTypeEnum);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (ReportTypeEnum)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (ReportTypeEnum)field.GetValue(null);
                }
            }
            return default(ReportTypeEnum);
        }

        public enum ReportTypeEnum
        {
            #region Nghiệp vụ cán bộ
            [Description("BaoCaoDanhSachCanBoNghiHuu")]
            EmployeeRetirement = 1,

            [Description("BaoCaoDanhSachCanBoHetHanHopDong")]
            EmployeeContractExpiration = 2,
            [Description(Constant.BusinessEmployeeReceiveGift8_3)]
            EmployeeReceiveGift8_3 = 3,
            #endregion

            #region Diễn biến lương

            [Description("BaoCaoDanhSachCanBoDenKyNangLuong")]
            RaiseSalary = 4,

            [Description("BaoCaoDanhSachCanBoDenHanXetVuotKhung")]
            SalaryOutOfFrame = 5,
            #endregion

            #region Đào tạo công tác
            [Description("BaoCaoDanhSachCanBoDuocDaoTaoTaiDonVi")]
            OnsiteTraining = 6,

            [Description("BaoCaoDanhSachCanBoCongTacNuocNgoai")]
            ForeignTraining = 7,
            #endregion

            #region Khen thưởng kỷ luật
            // Báo cáo danh sách CB được tặng danh hiệu thi đua
            [Description(Constant.BusinessAward)]
            BusinessAward = 8,

            // Báo cáo danh sách CB bị kỷ luật
            [Description(Constant.BusinessDiscipline)]
            BusinessDiscipline = 9,

            //Báo cáo danh sách CBNV được khen thưởng
            [Description(Constant.BusinessEmployeeReward)]
            BusinessEmployeeReward = 10,

            #endregion

            #region Business

            // Danh sách và tiền lương cán bộ, công chức cấp huyện
            [Description(Constant.BusinessSalaryDistrict)]
            BusinessSalaryDistrictCivilServants = 11,

            // Danh sách và tiền lương cán bộ, công chức cấp xã
            [Description(Constant.RegistrationOfLaborUtilization)]
            BusinessRegistrationOfLaborUtilization = 12,

            // Báo cáo nhân sự
            [Description(Constant.BusinessEmployee)]
            BusinessEmployee = 13,

            // Báo cáo danh sách CBCNV đảng viên
            [Description(Constant.BusinessCommunityPartyVietnamMember)]
            BusinessCommunityPartyVietnamMember = 14,

            // Báo cáo danh sách CBCNV đoàn viên
            [Description(Constant.BusinessVietnamYoungUnionMember)]
            BusinessVietnamYoungUnionMember = 15,

            // Báo cáo danh sách CBCNV quân nhân
            [Description(Constant.BusinessMilitary)]
            BusinessMilitary = 16,

            // Báo cáo danh sách CBCNV theo phòng ban
            [Description(Constant.BusinessEmployeeByDepartment)]
            BusinessEmployeeByDepartment = 17,

            // Báo cáo danh sách CBCNV được bổ nhiệm
            [Description(Constant.BusinessAppointment)]
            BusinessAppointment = 18,

            // Báo cáo danh sách CBCNV được cử đi biệt phái
            [Description(Constant.BusinessOnSecondment)]
            BusinessOnSecondment = 19,

            // Báo cáo thâm niên công tác của CBCNV
            [Description(Constant.BusinessSeniority)]
            BusinessSeniority = 20,

            // Báo cáo danh sách hợp đồng của CBCNV
            [Description(Constant.BusinessContract)]
            BusinessContract = 21,

            // Báo cáo danh sách CBCNV sinh nhật trong tháng
            [Description(Constant.BusinessBornInMonth)]
            BusinessBornInMonth = 22,

            // Báo cáo danh sách CBCNV nghỉ hưu
            [Description(Constant.BusinessRetirement)]
            BusinessRetire = 23,

            // Danh sách CBCNV hết hạn HĐLĐ
            [Description(Constant.BusinessExpiredContract)]
            BusinessExpiredContract = 24,

            // Báo cáo tăng tham gia BHYT, BHXH, BHTN
            [Description(Constant.BusinessIncreaseInsurance)]
            BusinessIncreaseInsurance = 25,

            // Báo cáo giảm tham gia BHYT, BHXH, BHTN
            [Description(Constant.BusinessDecreaseInsurance)]
            BusinessDecreaseInsurance = 26,

            // Báo cáo tăng nhân sự 
            [Description(Constant.BusinessIncreaseHumanResource)]
            BusinessIncreaseHumanResource = 27,

            // Báo cáo giảm nhân sự 
            [Description(Constant.BusinessDecreaseHumanResource)]
            BusinessDecreaseHumanResource = 28,

            // Báo điều động nhân sự
            [Description(Constant.BusinessPersonnelRotation)]
            BusinessPersonnelRotation = 29,

            // Danh sách lđ và quỹ lương trích nộp BHXH, BHYT,BHTN
            [Description(Constant.BusinessSocialInsurance)]
            BusinessSocialInsurance = 30,

            // Báo cáo thống kê nghề nghiệp nhân sự
            [Description(Constant.BusinessOccupation)]
            BusinessOccupation = 31,

            // Danh sách các con nhận quà 1/6
            [Description(Constant.BusinessInternationalChildrenDayGift)]
            BusinessInternationalChildrenDayGift = 32,

            // Danh sách CBCNV nữ
            [Description(Constant.BusinessFemale)]
            BusinessFemale = 33,

            // Báo cáo danh sách tài khoản ngân hàng của CBCNV
            [Description(Constant.BusinessBankAccount)]
            BusinessBankAccount = 34,

            // Báo cáo danh sách số người phụ thuộc
            [Description(Constant.BusinessFamilyRelation)]
            BusinessFamilyRelation = 35,

            // Báo cáo danh sanh CBCNV đến kỳ lên lương
            [Description(Constant.BusinessRaiseSalary)]
            BusinessRaiseSalary = 36,

            // Báo cáo danh sách CBCNV đến kỳ hạn vượt khung
            [Description(Constant.BusinessOutFrame)]
            BusinessOutframe = 37,

            // Báo cáo danh sách CBCNV được cử đi đào tạo
            [Description(Constant.BusinessOnsiteTraining)]
            BusinessOnsiteTraining = 38,

            // Báo cáo danh sách CB công tác nước ngoài
            [Description(Constant.BusinessForeignTraining)]
            BusinessForeignTraining = 39,

            // KT-Tăng LĐ
            [Description(Constant.IncreaseRegistrationOfLaborUtilization)]
            IncreaseRegistrationOfLaborUtilization = 40,

            // KT-Giảm LĐ
            [Description(Constant.DecreaseRegistrationOfLaborUtilization)]
            DecreaseRegistrationOfLaborUtilization = 41,

            // Báo cáo CBCNV đang trong thời gian thử việc
            [Description(Constant.BusinessTrainee)]
            BusinessTrainee = 42,

            // Danh sách mã số thuế CBCNV Công ty
            [Description(Constant.BusinessPersonalTax)]
            BusinessPersonalTax = 43,

            //Diễn biến quá trình lương
            [Description(Constant.BusinessProcessSalaryEmployee)]
            ProcessSalaryEmployee = 44,

            //Danh sach toan co quan
            [Description(Constant.AllEmployees)]
            AllEmployee = 45,

            //Danh sách lao động và quỹ lương trích nộp BHXH, BHYT, BHTN
            [Description(Constant.BusinessEmployeeSalaryInsurance)]
            BusinessEmployeeSalaryInsurance = 46,

            #endregion

            #region Chấm công
            [Description("BaoCaoChamCongHanhChinh")]
            TimeKeepingOffice = 47,
           
            [Description("BaoCaoChiTietChamCongHanhChinh")]
            TimeKeepingOfficeDetail = 48,

            [Description("BaoCaoChiTietTangCa")]
            OvertimeDetail = 52,

            #endregion

            #region Tiền lương

            [Description("BaoCaoLuongChiTietHangThang")]
            SalaryDetail = 49,

            [Description("BaoCaoTongHopLuongHangThang")]
            SalaryMonth = 50,

            [Description("BaoCaoChuyenKhoanAtm")]
            SalaryATM = 51,

            #endregion
        }

        private ReportFilter GetReportFilter()
        {
            if (Session["rp"] == null) return new ReportFilter();
            var filter = (ReportFilter)Session["rp"];
            return filter;
        }

        #region Diễn biến lương

        private XtraReport RaiseSalary()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_RaiseSalary();
            report.BindData(filter);
            return report;
        }

        private XtraReport SalaryOutOfFrame()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_SalaryOutOfFrame();
            report.BindData(filter);
            return report;
        }


        private XtraReport ProcessSalaryEmployee()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_ProcessSalaryEmployee();
            report.BindData(filter);
            return report;
        }

        #endregion

        #region Nghiệp vụ cán bộ

        private XtraReport EmployeeContractExpiration()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_EmployeeContractExpiration();
            report.BindData(filter);
            return report;
        }

        //private XtraReport EmployeeRetirement()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeeRetirement();
        //    report.BindData(filter);
        //    return report;
        //}
        /// <summary>
        /// Báo cáo danh sách nhân viên được tặng quà mồng 8-3
        /// </summary>
        /// <returns></returns>
        private XtraReport EmployeeReceiveGift8_3()
        {
            var filter = (ReportFilter)Session["rp"];
            var gift = new rp_EmployeeReceiveGift8_3();
            gift.BindData(filter);
            return gift;
        }
        #endregion

        #region Business

        //private XtraReport BusinessSalaryDistrictCivilServants()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_BusinessSalary();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessEmployee()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_BusinessHumanResource();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessRegistrationOfLaborUtilization()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_RegistrationOfLaborUtilization();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessCommunityPartyVietnamMember()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rpHRM_PartyMember();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessVietnamYoungUnionMember()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rpHRM_UnionMember();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessMilitary()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new global::rp_BusinessMilitary();
        //    report.BindData(filter);
        //    return report;
        //}

        private XtraReport BusinessEmployeeByDepartment()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new global::rp_BusinessEmployeeByDepartment();
            report.BindData(filter);
            return report;
        }

        //private XtraReport BusinessAppointment()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rpHRM_EmployeeAssigned();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessOnSecondment()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rpHRM_EmployeeSent();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessSeniority()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rpHRM_EmployeeSeniority();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessContract()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_ContractOfEmployee();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessBornInMonth()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_BusinessBornInMonth();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessRetirement()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rpHRM_EmployeeRetired();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessExpiredContract()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rpHRM_EmployeeExpired();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessIncreaseInsurance()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new global::rp_BusinessIncreaseInsurance();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessDecreaseInsurance()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new global::rp_BusinessDecreaseInsurance();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessIncreaseHumanResource()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_BusinessIncreaseHumanResource();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessDecreaseHumanResource()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_BusinessDecreaseHumanResource();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessPersonnelRotation()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_BusinessPersonnelRotation();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessSocialInsurance()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rpHRM_InsuranceList();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessOccupation()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_BusinessOccupation();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessInternationalChildrenDayGift()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_BusinessInternationalChildrensDayGift();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessFemale()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_BusinessFemale();
        //    report.BindData(filter);
        //    return report;
        //}

        private XtraReport BusinessBankAccount()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_BusinessBankAccount();
            report.BindData(filter);
            return report;
        }

        private XtraReport BusinessFamilyRelation()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_BusinessFamilyRelation();
            report.BindData(filter);
            return report;
        }

        private XtraReport BusinessRaiseSalary()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_BusinessRaiseSalary();
            report.BindData(filter);
            return report;
        }

        private XtraReport BusinessOutframe()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_BusinessOutFrame();
            report.BindData(filter);
            return report;
        }

        //private XtraReport BusinessOnsiteTraining()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_BusinessOnsiteTraining();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport BusinessForeignTraining()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_BusinessForeignTraining();
        //    report.BindData(filter);
        //    return report;
        //}

        private XtraReport BusinessAward()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_BusinessAward();
            report.BindData(filter);
            return report;
        }

        private XtraReport BusinessDiscipline()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_BusinessDiscipline();
            report.BindData(filter);
            return report;
        }

        //private XtraReport BusinessTrainee()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rpHRM_EmployeeTrainee();
        //    report.BindData(filter);
        //    return report;
        //}

        private XtraReport BusinessPersonalTax()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_BusinessPersonalTax();
            report.BindData(filter);
            return report;
        }
        private XtraReport EmployeeReward()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_EmployeeReward();
            report.BindData(filter);
            return report;
        }

        private XtraReport AllEmployee()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_AllEmployees();
            report.BindData(filter);
            return report;
        }

        private XtraReport BusinessEmployeeSalaryInsurance()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_BusinessEmployeeSalaryInsurance();
            report.BindData(filter);
            return report;
        }
        #endregion

        #region Chấm công
        private XtraReport TimeKeepingOffice()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_TimeKeepingOffice();
            report.BindData(filter);
            return report;
        }

        private XtraReport TimeKeepingOfficeDetail()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_TimeKeepingOfficeDetail();
            report.BindData(filter);
            return report;
        }


        private XtraReport OvertimeDetail()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_OvertimeDetail();
            report.BindData(filter);
            return report;
        }

        #endregion

        #region Tiền lương

        private XtraReport SalaryDetail()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_SalaryDetailByMonth();
            report.BindData(filter);
            return report;
        }

        private XtraReport SalaryByMonth()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_SalaryByMonth();
            report.BindData(filter);
            return report;
        }

        private XtraReport SalaryATM()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_SalaryATM();
            report.BindData(filter);
            return report;
        }

        #endregion
    }
}