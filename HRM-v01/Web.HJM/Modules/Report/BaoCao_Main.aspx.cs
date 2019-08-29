using System;
using System.ComponentModel;
using CCVC.Web;
using DevExpress.XtraReports.UI;
using Web.Core.Framework;
using Web.Core.Framework.Report;
using Web.Core.Object.Report;


namespace Web.HJM.Modules.Report
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
                //case ReportTypeEnum.QuantityCommuneCivilServants:
                //    return QuantityCommuneCivilServants();

                //case ReportTypeEnum.QuantityDistrictCivilServants:
                //    return QuantityDistrictCivilServants();

                //case ReportTypeEnum.QuantityDistrictCivilServantsDetail:
                //    return QuantityDistrictCivilServantsDetail();

                //case ReportTypeEnum.SalaryCommuneCivilServants:
                //    return SalaryCommuneCivilServants();

                //case ReportTypeEnum.SalaryDistrictCivilServants:
                //    return SalaryDistrictCivilServants();

                case ReportTypeEnum.QuantityOfEmployee:
                    return QuantityOfEmployee();

                //case ReportTypeEnum.QuantityFemaleEmployee:
                //    return QuantityFemaleEmployee();

                //case ReportTypeEnum.QuantityEthnicMinority:
                //    return QuantityEthnicMinority();

                //case ReportTypeEnum.QuantityStaff:
                //    return QuantityStaff();

                #endregion

                #region Nghiệp vụ cán bộ
                //1.  Báo cáo danh sách cán bộ toàn cơ quan
                //case ReportTypeEnum.AllEmployees:
                //    return AllEmployees();

                //2. Báo cáo danh sách cán bộ là Đảng viên
                //case ReportTypeEnum.CommunityPartyVietNamMember:
                //    return CommunityPartyVietNamMember();

                //3. Báo cáo danh sách cán bộ là Đoàn viên
                //case ReportTypeEnum.VietNamYoungUnionMember:
                //    return VietNamYoungUnionMember();

                //4. Báo cáo danh sách cán bộ là Quân nhân
                //case ReportTypeEnum.Military:
                //    return Military();

                ////5. Báo cáo danh sách cán bộ theo phòng, ban
                //case ReportTypeEnum.EmployeeByDepartment:
                //    return EmployeeByDepartment();

                //6. Báo cáo danh sách cán bộ được bổ nhiệm
                //case ReportTypeEnum.EmployeeAppointment:
                //    return EmployeeAppointment();

                //7. Báo cáo danh sách cán bộ được điều động đến
                //case ReportTypeEnum.EmployeeMoveTo:
                //    return EmployeeMoveTo();

                ////8. Báo cáo danh sách cán bộ được điều động đi
                //case ReportTypeEnum.EmployeeMoveFrom:
                //    return EmployeeMoveFrom();

                ////9. Báo cáo danh sách cán bộ được luân chuyển đến
                //case ReportTypeEnum.EmployeeTurnoverTo:
                //    return EmployeeTurnoverTo();

                ////10. Báo cáo danh sách cán bộ được luân chuyển đi
                //case ReportTypeEnum.EmployeeTurnoverFrom:
                //    return EmployeeTurnoverFrom();

                //11. Báo cáo danh sách cán bộ được biệt phái đến
                //case ReportTypeEnum.EmployeeSecondmentTo:
                //    return EmployeeSecondmentTo();

                ////12. Báo cáo danh sách cán bộ được cử đi biệt phái
                //case ReportTypeEnum.EmployeeSecondmentFrom:
                //    return EmployeeSecondmentFrom();

                //13. Báo cáo danh sách cán bộ được kiêm nhiệm
                //case ReportTypeEnum.EmployeePlurality:
                //    return EmployeePlurality();

                //14. Báo cáo danh sách cán bộ được miễn nhiệm, bãi nhiệm
                //case ReportTypeEnum.EmployeeDismissment:
                //    return EmployeeDismissment();

                //15. Báo cáo danh sách cán bộ được thuyên chuyển, điều chuyển
                //case ReportTypeEnum.EmployeeTranfer:
                //    return EmployeeTranfer();

                ////16. Báo cáo thâm niên công tác của cán bộ
                //case ReportTypeEnum.EmployeeSeniority:
                //    return EmployeeSeniority();

                //17. Báo cáo danh sách hợp đồng của cán bộ
                case ReportTypeEnum.EmployeeContract:
                    return EmployeeContract();

                ////18. Báo cáo danh sách cán bộ được hưởng chính sách
                //case ReportTypeEnum.EmployeeCompensation:
                //    return EmployeeCompensation();

                ////19. Báo cáo danh sách cán bộ sinh nhật trong tháng
                //case ReportTypeEnum.EmployeeBornInMonth:
                //    return EmployeeBornInMonth();

                ////20. Báo cáo danh sách cán bộ nghỉ hưu
                //case ReportTypeEnum.EmployeeRetirement:
                //    return EmployeeRetirement();

                //Báo cáo danh sách cán bộ nhân viên sắp hết hạn hợp đồng 
                //case ReportTypeEnum.EmployeeContractExpiration:
                //    return EmployeeContractExpiration();

                //21. Báo cáo tăng tham gia BHXH, BHYT, BHTN
                //case ReportTypeEnum.IncreaseInsurance:
                //    return IncreaseInsurance();

                //22. Báo cáo giảm tham gia BHXH, BHYT, BHTN
                //case ReportTypeEnum.DecreaseInsurance:
                //    return DecreaseInsurance();

                ////23. Báo cáo tăng nhân sự
                //case ReportTypeEnum.IncreaseEmployee:
                //    return IncreaseEmployee();

                ////24. Báo cáo giảm nhân sự
                //case ReportTypeEnum.DecreaseEmployee:
                //    return DecreaseEmployee();

                //25. BaoCaoDieuChuyenNhanSu
                //case ReportTypeEnum.TranferEmployee:
                //    return TranferEmployee();

                ////26. Danh sách lao động và trích quỹ lương nộp BHXH, BHYT, BHTN
                //case ReportTypeEnum.EmployeeSalaryInsurance:
                //    return EmployeeSalaryInsurance();

                ////27. Báo cáo thống kê nghề nghiệp nhân sự
                //case ReportTypeEnum.EmployeeOccupation:
                //    return EmployeeOccupation();

                ////28. Báo cáo tổng hợp nghề nghiệp nhân sự
                //case ReportTypeEnum.TotalEmployeeOccupation:
                //    return TotalEmployeeOccupation();

                ////29. Báo cáo thống kê nhân sự
                //case ReportTypeEnum.StatisticEmployee:
                //    return StatisticEmployee();

                ////30. Báo cáo tổng hợp nhân sự
                //case ReportTypeEnum.TotalStatisticEmployee:
                //    return TotalStatisticEmployee();

                //31. Báo cáo danh sách lao động hết hạn hợp đồng lao động
                //case ReportTypeEnum.ContractExpiration:
                //    return ContractExpiration();

                ////32.Danh sách cán bộ có con em nhận quà trung thu
                //case ReportTypeEnum.EmployeeHaveChildrenReceiveMidAutumnGift:
                //    return EmployeeHaveChildrenReceiveMidAutumnGift();

                ////33. Báo cáo danh sách nhân viên được tặng quà mồng 8-3
                //case ReportTypeEnum.EmployeeReceiveGift8_3:
                //    return EmployeeReceiveGift8_3();

                ////34. Báo cáo thông tin người thân nhận quà trung thu
                //case ReportTypeEnum.InfomationOfMidAutumnGiftReceiver:
                //    return InfomationOfMidAutumnGiftReceiver();

                //// 35.Báo cáo danh sách cán bộ chưa có sổ BHXH
                //case ReportTypeEnum.EmployeeNotHaveSocialInsuranceNumber:
                //    return EmployeeNotHaveSocialInsuranceNumber();

                ////36.Báo cáo danh sách cán bộ chưa có thẻ BHYT
                //case ReportTypeEnum.EmployeeNotHaveHealthInsuranceNumber:
                //    return EmployeeNotHaveHealthInsuranceNumber();

                ////37. Báo cáo danh sách cán bộ nhân viên thử việc
                //case ReportTypeEnum.Trainee:
                //    return Trainee();

                //// 38. Báo cáo danh sách nhân viên tham gia công đoàn
                //case ReportTypeEnum.EmployeeJoinUnion:
                //    return UnionMember();

                ////39. Báo cáo danh sách tài khoản ngân hàng của nhân viên
                ////case ReportTypeEnum.BankAccount:
                ////    return BankAccount();

                ////41. Báo cáo danh sách người phụ thuộc
                //case ReportTypeEnum.FamilyRelation:
                //    return FamilyRelation();

                ////42. Báo cáo danh sách hợp đồng của nhân viên
                ////case ReportTypeEnum.ContractOfEmployee:
                ////    return ContractOfEmployee();

                ////43. Báo cáo nhân viên có người giảm trừ gia cảnh
                //case ReportTypeEnum.FamilyCircumstanceDeduction:
                //    return FamilyCircumstanceDeduction();

                ////44.  Giấy đề nghị thuyên chuyển nhân sự
                //case ReportTypeEnum.PaperTransferEmployee:
                //    return PaperTransferEmployee();

                ////45  Báo cáo danh sách cán bộ có mã số thuế cá nhân
                //case ReportTypeEnum.PersonalTax:
                //    return PersonalTax();

                ////46 Báo cáo danh sách cán bộ tham gia BHXH
                //case ReportTypeEnum.SocialInsurance:
                //    return SocialInsurance();

                ////46 Báo cáo danh sách cán bộ hết hạn hợp đồng trong tháng
                //case ReportTypeEnum.ExpiredContractInMonth:
                //    return ExpiredContractInMonth();

                #endregion

                #region Diễn biến lương
                //1. Báo cáo diễn biến quá trình lương cán bộ
                //case ReportTypeEnum.ProcessSalaryEmployee:
                //    return ProcessSalaryEmployee();
                ////2. Báo cáo danh sách cán bộ đến kỳ nâng lương
                //case ReportTypeEnum.RaiseSalary:
                //    return RaiseSalary();
                ////3. Báo cáo danh sách cán bộ đến hạn xét vượt khung
                //case ReportTypeEnum.SalaryOutOfFrame:
                //    return SalaryOutOfFrame();
                #endregion

                #region Đào tạo công tác
                //1. Báo cáo danh sách cán bộ được đào tạo tại đơn vị
                //case ReportTypeEnum.OnsiteTraining:
                //    return OnsiteTraining();
                //2. Báo cáo danh sách cán bộ công tác nước ngoài
                //case ReportTypeEnum.ForeignTraining:
                //    return ForeignTraining();

                #endregion

                #region Khen thưởng kỷ luật
                //1. Báo cáo danh sách cán bộ được tặng danh hiệu thi đua
                //case ReportTypeEnum.EmulationTitle:
                //    return EmulationTitle();
                ////2. Báo cáo danh sách cán bộ được khen thưởng
                //case ReportTypeEnum.Reward:
                //    return EmployeeReward();
                ////3. Báo cáo danh sách cán bộ bị kỷ luật
                //case ReportTypeEnum.Discipline:
                //    return Discipline();
                #endregion

                #region Chấm công
                //1. Báo cáo chấm công hành chính
                case ReportTypeEnum.TimeKeepingOffice:
                    return TimeKeepingOffice();
                //2. Báo cáo chi tiết chấm công hành chính
                case ReportTypeEnum.TimeKeepingOfficeDetail:
                    return TimeKeepingOfficeDetail();

                #endregion

                #region Tiền lương
                //1. Báo cáo lương chi tiết hàng tháng
                //case ReportTypeEnum.SalaryDetail:
                //    return SalaryDetail();
                ////2. Báo cáo lương tháng
                //case ReportTypeEnum.SalaryMonth:
                //    return SalaryByMonth();
                //3. Báo cáo chuyển khoản ATM
                //case ReportTypeEnum.SalaryATM:
                //    return SalaryATM();
                //#endregion
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
            #region Báo cáo theo quy định
            [Description("SoLuongChatLuongCanBoCongChucCapXa")]
            QuantityCommuneCivilServants = 0,

            [Description("SoLuongChatLuongCanBoCongChucCapHuyen")]
            QuantityDistrictCivilServants = 1,

            [Description("SoLuongChatLuongCanBoCongChucCapHuyenChiTiet")]
            QuantityDistrictCivilServantsDetail = 70,

            [Description("DanhSachVaTienLuongCanBoCongChucCapXa")]
            SalaryCommuneCivilServants = 2,

            [Description("DanhSachVaTienLuongCanBoCongChucCapHuyen")]
            SalaryDistrictCivilServants = 3,

            [Description("SoLuongChatLuongNguoiLamViecTrongCacDonViSuNghiep")]
            QuantityOfEmployee = 4,

            [Description("SoLuongChatLuongNguoiLamViecLaNu")]
            QuantityFemaleEmployee = 5,

            [Description("SoLuongChatLuongNguoiLamViecLaNguoiDanTocThieuSo")]
            QuantityEthnicMinority = 6,

            [Description("SoLuongChatLuongBienCheSuNghiep")]
            QuantityStaff = 7,
            #endregion

            #region Nghiệp vụ cán bộ
            [Description("BaoCaoDanhSachCanBoToanCoQuan")]
            AllEmployees = 8,

            [Description("BaoCaoDanhSachCanBoLaDangVien")]
            CommunityPartyVietNamMember = 9,

            [Description("BaoCaoDanhSachCanBoLaDoanVien")]
            VietNamYoungUnionMember = 10,

            [Description("BaoCaoDanhSachCanBoLaQuanNhan")]
            Military =11,

            [Description("BaoCaoDanhSachCanBoTheoPhongBan")]
            EmployeeByDepartment = 12,

            [Description("BaoCaoDanhSachCanBoDuocBoNhiem")]
            EmployeeAppointment = 13,

            [Description("BaoCaoDanhSachCanBoDuocDieuDongDen")]
            EmployeeMoveTo = 14,

            [Description("BaoCaoDanhSachCanBoDuocDieuDongDi")]
            EmployeeMoveFrom = 15,

            [Description("BaoCaoDanhSachCanBoDuocLuanChuyenDen")]
            EmployeeTurnoverTo = 16,

            [Description("BaoCaoDanhSachCanBoDuocLuanChuyenDi")]
            EmployeeTurnoverFrom = 17,

            [Description("BaoCaoDanhSachCanBoDuocBietPhaiDen")]
            EmployeeSecondmentTo = 18,

            [Description("BaoCaoDanhSachCanBoDuocCuDiBietPhai")]
            EmployeeSecondmentFrom = 19,

            [Description("BaoCaoDanhSachCanBoDuocKiemNhiem")]
            EmployeePlurality = 20,

            [Description("BaoCaoDanhSachCanBoDuocMienNhiemBaiNhiem")]
            EmployeeDismissment = 21,

            [Description("BaoCaoDanhSachCanBoDuocThuyenChuyenDieuChuyen")]
            EmployeeTranfer = 22,

            [Description("BaoCaoThamNienCongTacCuaCanBo")]
            EmployeeSeniority = 23,

            [Description("BaoCaoDanhSachHopDongCuaCanBo")]
            EmployeeContract = 24,

            [Description("BaoCaoDanhSachCanBoDuocHuongChinhSach")]
            EmployeeCompensation = 25,

            [Description("BaoCaoDanhSachCanBoSinhNhatTrongThang")]
            EmployeeBornInMonth = 26,

            [Description("BaoCaoDanhSachCanBoNghiHuu")]
            EmployeeRetirement = 27,

            [Description("BaoCaoDanhSachCanBoHetHanHopDong")]
            EmployeeContractExpiration = 28,

            [Description("BaoCaoTangThamGiaBhxhBhytBhtn")]
            IncreaseInsurance = 29,

            [Description("BaoCaoGiamThamGiaBhxhBhytBhtn")]
            DecreaseInsurance = 30,

            [Description("BaoCaoTangNhanSu")]
            IncreaseEmployee = 31,

            [Description("BaoCaoGiamNhanSu")]
            DecreaseEmployee = 32,

            [Description("BaoCaoDieuChuyenNhanSu")]
            TranferEmployee = 33,

            [Description("DanhSachLaoDongVaQuyLuongTrichNopBhxhBhytBhtn")]
            EmployeeSalaryInsurance = 34,

            [Description("BaoCaoThongKeNgheNghiepNhanSu")]
            EmployeeOccupation = 35,

            [Description("BaoCaoTongHopNgheNghiepNhanSu")]
            TotalEmployeeOccupation = 36,

            [Description("BaoCaoThongKeNhanSu")]
            StatisticEmployee = 37,

            [Description("BaoCaoTongHopNhanSu")]
            TotalStatisticEmployee = 38,

            [Description("DanhSachLaoDongHetHanHopDongLaoDong")]
            ContractExpiration = 39,

            [Description("DanhSachCanBoCoConEmNhanQuaTrungThu")]
            EmployeeHaveChildrenReceiveMidAutumnGift = 40,

            [Description("BaoCaoDanhSachNhanVienDuocTangQuaMong8Thang3")]
            EmployeeReceiveGift8_3= 41,

            [Description("BaoCaoThongTinNguoiThanNhanQuaTrungThu")]
            InfomationOfMidAutumnGiftReceiver = 42,

            [Description("BaoCaoDanhSachCanBoChuaCoSoBhxh")]
            EmployeeNotHaveSocialInsuranceNumber = 43,

            [Description("BaoCaoDanhSachCanBoChuaCoTheBhyt")]
            EmployeeNotHaveHealthInsuranceNumber = 44,

            [Description("BaoCaoDanhSachCanBoNhanVienThuViec")]
            Trainee = 45,

            [Description("BaoCaoDanhSachNhanVienThamGiaCongDoan")]
            EmployeeJoinUnion = 46,

            [Description("BaoCaoDanhSachTaiKhoanNganHangCuaNhanVien")]
            BankAccount = 47,

            [Description("BaoCaoDanhSachNguoiPhuThuoc")]
            FamilyRelation = 49,

            [Description("BaoCaoDanhSachHopDongCuaNhanVien")]
            ContractOfEmployee = 50,

            [Description("BaoCaoNhanVienCoNguoiGiamTruGiaCanh")]
            FamilyCircumstanceDeduction = 51,

            [Description("GiayDeNghiThuyenChuyenNhanSu")]
            PaperTransferEmployee = 60,

            [Description("BaoCaoDanhSachCanBoCoMaSoThueCaNhan")]
            PersonalTax = 61,

            [Description("BaoCaoDanhSachCanBoThamGiaBhxh")]
            SocialInsurance = 62,

            [Description("BaoCaoDanhSachCanBoHetHanHopDongTrongThang")]
            ExpiredContractInMonth = 63,


            #endregion

            #region Diễn biến lương
            [Description("BaoCaoDienBienQuaTrinhLuongCanBo")]
            ProcessSalaryEmployee = 52,

            [Description("BaoCaoDanhSachCanBoDenKyNangLuong")]
            RaiseSalary = 53,

            [Description("BaoCaoDanhSachCanBoDenHanXetVuotKhung")]
            SalaryOutOfFrame = 54,
            #endregion

            #region Đào tạo công tác
            [Description("BaoCaoDanhSachCanBoDuocDaoTaoTaiDonVi")]
            OnsiteTraining = 55,

            [Description("BaoCaoDanhSachCanBoCongTacNuocNgoai")]
            ForeignTraining = 56,
            #endregion

            #region Khen thưởng kỷ luật
            [Description("BaoCaoDanhSachCanBoDuocTangDanhHieuThiDua")]
            EmulationTitle = 57,

            [Description("BaoCaoDanhSachCanBoDuocKhenThuong")]
            Reward = 58,

            [Description("BaoCaoDanhSachCanBoBiKyLuat")]
            Discipline = 59,
            #endregion

            #region Chấm công
            [Description("BaoCaoChamCongHanhChinh")]
            TimeKeepingOffice = 65,
           
            [Description("BaoCaoChiTietChamCongHanhChinh")]
            TimeKeepingOfficeDetail = 66,

            #endregion

            #region Tiền lương

            [Description("BaoCaoLuongChiTietHangThang")]
            SalaryDetail = 67,

            [Description("BaoCaoTongHopLuongHangThang")]
            SalaryMonth = 68,

            [Description("BaoCaoChuyenKhoanAtm")]
            SalaryATM = 69,

            #endregion
        }

        //private XtraReport EmployeeContractExpiration()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeeContractExpiration();
        //    report.BindData(filter);
        //    return report;
        //}

        #region Báo cáo theo quy định

        //private XtraReport QuantityCommuneCivilServants()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_QuantityCommuneCivilServants();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport QuantityDistrictCivilServants()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_QuantityDistrictCivilServants();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport QuantityDistrictCivilServantsDetail()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_QuantityDistrictCivilServantsDetail();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport SalaryCommuneCivilServants()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_SalaryCommuneCivilServants();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport SalaryDistrictCivilServants()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_SalaryDistrictCivilServants();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport QuantityStaff()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_QuantityStaff();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport QuantityEthnicMinority()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_QuantityEthnicMinority();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport QuantityFemaleEmployee()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_QuantityFemaleEmployee();
        //    report.BindData(filter);
        //    return report;
        //}

        private XtraReport QuantityOfEmployee()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_QuantityOfEmployee();
            report.BindData();
            return report;
        }
        
        #endregion

        private ReportFilter GetReportFilter()
        {
            if (Session["rp"] == null) return new ReportFilter();
            var filter = (ReportFilter)Session["rp"];
            return filter;
        }
  

        #region Khen thưởng và kỷ luật

        //private XtraReport EmulationTitle()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeeEmulationTitle();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport EmployeeReward()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeeReward();
        //    report.BindData();
        //    return report;
        //}

        //private XtraReport Discipline()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeeDiscipline();
        //    report.BindData(filter);
        //    return report;
        //}

        #endregion

        #region Diễn biến lương

        //private XtraReport ProcessSalaryEmployee()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_ProcessSalaryEmployee();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport RaiseSalary()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_RaiseSalary();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport SalaryOutOfFrame()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_SalaryOutOfFrame();
        //    report.BindData(filter);
        //    return report;
        //}

        #endregion

        #region Đào tạo công tác

        //private XtraReport OnsiteTraining()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_OnsiteTraining();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport ForeignTraining()
        // {
        //     var filter = (ReportFilter)Session["rp"];
        //     var report = new rp_ForeignTraining();
        //     report.BindData(filter);
        //     return report;
        // }

        #endregion

        #region Nghiệp vụ cán bộ

        //private XtraReport AllEmployees()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_AllEmployees();
        //    report.BindData(filter);
        //    return report;
        //}        

        //private XtraReport CommunityPartyVietNamMember()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_CommunityPartyVietNamMember();
        //    report.BindData(filter);
        //    return report;
        //}        

        //private XtraReport VietNamYoungUnionMember()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_VietNamYoungUnionMember();
        //    report.BindData(filter);
        //    return report;
        //}        

        //private XtraReport Military()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_Military();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport EmployeeByDepartment()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeeByDepartment();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport EmployeeAppointment()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeeAppointment();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport EmployeeMoveTo()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeeMoveTo();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport EmployeeMoveFrom()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeeMoveFrom();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport EmployeeTurnoverFrom()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeeTurnoverFrom();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport EmployeeTurnoverTo()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeeTurnoverTo();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport EmployeePlurality()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeePlurality();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport EmployeeDismissment()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeeDismissment();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport EmployeeTranfer(){
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeeTranfer();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport EmployeeSeniority()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeeSeniority();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport EmployeeCompensation()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeeCompensation();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport EmployeeBornInMonth()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeeBornInMonth();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport EmployeeSecondmentTo()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeeSecondmentTo();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport EmployeeSecondmentFrom()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeeSecondmentFrom();
        //    report.BindData(filter);
        //    return report;
        //}

        private XtraReport EmployeeContract()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_EmployeeContract();
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

        //private XtraReport ContractExpiration()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_ContractExpiration();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport TotalStatisticEmployee()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_TotalStatisticEmployee();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport StatisticEmployee()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_StatisticEmployee();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport TotalEmployeeOccupation()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_TotalEmployeeOccupation();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport EmployeeOccupation()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeeOccupation();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport EmployeeSalaryInsurance()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_EmployeeSalaryInsurance();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport TranferEmployee()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_TranferEmployee();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport IncreaseEmployee()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_IncreaseEmployee();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport DecreaseEmployee()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_DecreaseEmployee();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport IncreaseInsurance()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_IncreaseInsurance();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport DecreaseInsurance()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var decreaseInsurance = new rp_DecreaseInsurance();
        //    decreaseInsurance.BindData(filter);
        //    return decreaseInsurance;
        //}

        /// <summary>
        /// Báo cáo thông tin người thân nhận quà trung thu 
        /// </summary>
        /// <returns></returns>
        //private XtraReport InfomationOfMidAutumnGiftReceiver()
        //{
        //    var filter = (ReportFilter) Session["rp"];
        //    var info = new rp_InfomationOfMidAutumnGiftReceiver() ;
        //    info.BindData(filter);
        //    return info;
        //}

        /// <summary>
        /// Báo cáo danh sách nhân viên được tặng quà mồng 8-3
        /// </summary>
        /// <returns></returns>
        //private XtraReport EmployeeReceiveGift8_3()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var gift = new rp_EmployeeReceiveGift8_3();
        //    gift.BindData(filter);
        //    return gift;
        //}

        /// <summary>
        /// Báo cáo danh sách cán bộ có con em nhận quà trung thu
        /// </summary>
        /// <returns></returns>
        //private XtraReport EmployeeHaveChildrenReceiveMidAutumnGift()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var lstHaveGift = new rp_EmployeeHaveChildrenReceiveMidAutumnGift();
        //    lstHaveGift.BindData(filter);
        //    return lstHaveGift;
        //}

        /// <summary>
        /// Báo cáo danh sách cán bộ chưa có sổ BHXH
        /// </summary>
        /// <returns></returns>
        //private XtraReport EmployeeNotHaveSocialInsuranceNumber()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var socialReacord = new rp_EmployeeNotHaveSocialInsuranceNumber();
        //    socialReacord.BindData(filter);
        //    return socialReacord;
        //}

        /// <summary>
        /// Báo cáo danh sách cán bộ chưa có thẻ BHYT
        /// </summary>
        /// <returns></returns>
        //private XtraReport EmployeeNotHaveHealthInsuranceNumber()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var health = new rp_EmployeeNotHaveHealthInsuranceNumber();
        //    health.BindData(filter);
        //    return health;
        //}

        /// <summary>
        /// Báo cáo danh sách cán bộ nhân viên thử việc
        /// </summary>
        /// <returns></returns>
        //private XtraReport Trainee()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var emp = new rp_Trainee();
        //    emp.BindData(filter);
        //    return emp;
        //}

        /// <summary>
        /// Báo cáo danh sách nhân viên tham gia công đoàn
        /// </summary>
        /// <returns></returns>
        //private XtraReport UnionMember()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var emp = new rp_UnionMember();
        //    emp.BindData(filter);
        //    return emp;
        //}

        /// <summary>
        /// Báo cáo danh sách tài khoản ngân hàng của nhân viên
        /// </summary>
        /// <returns></returns>
        //private XtraReport BankAccount()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var emp = new rp_BankAccount();
        //    emp.BindData(filter);
        //    return emp;
        //}

        /// <summary>
        /// Báo cáo danh sách người phụ thuộc
        /// </summary>
        /// <returns></returns>
        //private XtraReport FamilyRelation()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    // rp_ListEmployeeDependence emp = new rp_ListEmployeeDependence();
        //    var emp = new rp_FamilyRelation();
        //    emp.BindData(filter);
        //    return emp;
        //}

        /// <summary>
        /// Báo cáo danh sách hợp đồng của nhân viên
        /// </summary>
        /// <returns></returns>
        //private XtraReport ContractOfEmployee()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var contractEmp = new rp_ContractOfEmployee();
        //    contractEmp.BindData(filter);
        //    return contractEmp;
        //}

        /// <summary>
        /// Báo cáo nhân viên có người giảm trừ gia cảnh
        /// </summary>
        /// <returns></returns>
        //private XtraReport FamilyCircumstanceDeduction()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var emp = new rp_FamilyCircumstanceDeduction();
        //    emp.BindData(filter);
        //    return emp;
        //}

        /// <summary>
        /// Giấy đề nghị thuyên chuyển nhân sự
        /// </summary>
        /// <returns></returns>
        //private XtraReport PaperTransferEmployee()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var emp = new rp_PaperTransferEmployee();
        //    emp.BindData(filter);
        //    return emp;
        //}

        /// <summary>
        /// Báo cáo danh sách cán bộ có mã số thuế cá nhân
        /// </summary>
        /// <returns></returns>
        //private XtraReport PersonalTax()
        //{
        //    var filter = (ReportFilter) Session["rp"];
        //    var tax = new rp_PersonalTax();
        //    tax.BindData(filter);
        //    return tax;
        //}

        /// <summary>
        /// Báo cáo danh sách cán bộ tham gia BHXH
        /// </summary>
        /// <returns></returns>
        //private XtraReport SocialInsurance()
        //{
        //    var filter = (ReportFilter) Session["rp"];
        //    var insurance = new rp_SocialInsurance();
        //    insurance.BindData(filter);
        //    return insurance;
        //}

        /// <summary>
        /// Báo cáo danh sách cán bộ hết hạn hợp đồng trong tháng
        /// </summary>
        /// <returns></returns>
        //private XtraReport ExpiredContractInMonth()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var contract = new rp_ExpiredContractInMonth();
        //    contract.BindData(filter);
        //    return contract;
        //}

        #endregion

        #region Chấm công
        private XtraReport TimeKeepingOffice()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_TimeKeepingOffice();
            //report.BindData(filter);
            return report;
        }

        private XtraReport TimeKeepingOfficeDetail()
        {
            var filter = (ReportFilter)Session["rp"];
            var report = new rp_TimeKeepingOfficeDetail();
            //report.BindData(filter);
            return report;
        }

        #endregion

        #region Tiền lương

        //private XtraReport SalaryDetail()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_SalaryDetailByMonth();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport SalaryByMonth()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_SalaryByMonth();
        //    report.BindData(filter);
        //    return report;
        //}

        //private XtraReport SalaryATM()
        //{
        //    var filter = (ReportFilter)Session["rp"];
        //    var report = new rp_SalaryATM();
        //    report.BindData(filter);
        //    return report;
        //}

        #endregion
    }
}