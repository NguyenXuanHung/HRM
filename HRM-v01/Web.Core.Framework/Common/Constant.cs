namespace Web.Core.Framework.Common
{
    /// <summary>
    /// Summary description for Constant
    /// </summary>
    public class Constant
    {
        #region Paging

        public const int DefaultStart = 0;
        public const int DefaultPage = 1;
        public const int DefaultPagesize = 12;

        #endregion

        #region Path

        public const string PathAttachFile = "File/AttachFile";
        public const string PathContract = "File/Contract";
        public const string PathTransfer = "File/Transfer";
        public const string PathDecisionSalary = "File/DecisionSalary";
        public const string PathTemplate = "File/Template";
        public const string PathReward = "File/Reward";
        public const string PathDiscipline = "File/Discipline";
        public const string PathImageEmployee = "File/ImagesEmployee/";
        public const string PathLocationImageEmployee = "File/ImagesEmployee";
        public const string PathProfileImageDefault = "File/ImagesEmployee/No_person.jpg";

        #endregion

        #region Work progcess

        public const string HO_TEN = "##{HO_TEN}##";
        public const string TEN_COQUAN = "##{TEN_COQUAN}##";
        public const string CHUC_VU = "##{CHUC_VU}##";
        public const string PHONG_BAN = "##{PHONG_BAN}##";
        public const string THUTRUONG = "##{THUTRUONG}##";
        public const string DAY = "{D}";
        public const string MONTH = "{M}";
        public const string YEAR = "{Y}";
        public const string NGAY_HIEN_TAI = "##{NGAY_HT}##";
        public const string CVM = "##{CVM}##";
        public const string SO_QD = "##{SO_QD}##";
        public const string TP = "##{TP}##";

        #endregion

        #region Enterprise for name report

        public const string BusinessSalaryDistrict = "DanhSachVaTienLuongCbcnvCongTy";
        public const string RegistrationOfLaborUtilization = "KhaiTrinhLaoDong";
        public const string BusinessEmployee = "BaoCaoNhanSu";
        public const string IncreaseRegistrationOfLaborUtilization = "Kt-tangLd";
        public const string DecreaseRegistrationOfLaborUtilization = "Kt-giamLd";
        public const string AllEmployees = "BaoCaoDanhSachCbcnvToanCoQuan";
        public const string BusinessCommunityPartyVietnamMember = "BaoCaoDanhSachCbcnvDangVien";
        public const string BusinessVietnamYoungUnionMember = "BaoCaoDanhSachCbcnvDoanVien";
        public const string BusinessMilitary = "BaoCaoDanhSachCbcnvQuanNhan";
        public const string BusinessEmployeeByDepartment = "BaoCaoDanhSachCbcnvTheoPhongBan";
        public const string BusinessAppointment = "BaoCaoDanhSachCbcnvDuocBoNhiem";
        public const string BusinessOnSecondment = "BaoCaoDanhSachCbcnvDuocCuDiBietPhai";
        public const string BusinessSeniority = "BaoCaoThamNienCongTacCuaCbcnv";
        public const string BusinessContract = "BaoCaoDanhSachHopDongCuaCbcnv";
        public const string BusinessBornInMonth = "BaoCaoDanhSachCbcnvSinhNhatTrongThang";
        public const string BusinessRetirement = "BaoCaoDanhSachCbcnvNghiHuu";
        public const string BusinessExpiredContract = "DanhSachCbcnvHetHanHdld";
        public const string BusinessIncreaseInsurance = "BaoCaoTangThamGiaBhytBhxhBhtn";
        public const string BusinessDecreaseInsurance = "BaoCaoGiamThamGiaBhytBhxhBhtn";
        public const string BusinessIncreaseHumanResource = "BaoCaoTangNhanSu";
        public const string BusinessDecreaseHumanResource = "BaoCaoGiamNhanSu";
        public const string BusinessPersonnelRotation = "BaoCaoDieuDongNhanSu";
        public const string BusinessSocialInsurance = "DanhSachCbcnvDongBaoHiemXaHoi";
        public const string BusinessOccupation = "BaoCaoThongKeNgheNghiepNhanSu";
        public const string BusinessInternationalChildrenDayGift = "DanhSachCacConDuocTangQua1/6";
        public const string BusinessFemale = "DanhSachCbcnvNu";
        public const string BusinessTrainee = "BaoCaoCbcnvDangTrongThoiGianThuViec";
        public const string BusinessPersonalTax = "DanhSachMaSoThueCbcnvCongTy";
        public const string BusinessBankAccount = "BaoCaoDanhSachTaiKhoanNganHangCuaCbcnv";
        public const string BusinessFamilyRelation = "BaoCaoDanhSachSoNguoiPhuThuoc";
        public const string BusinessRaiseSalary = "BaoCaoDanhSachCbcnvDenKyLenLuong";
        public const string BusinessOutFrame = "BaoCaoDanhSachCbcnvDenKyHanVuotKhung";
        public const string BusinessOnsiteTraining = "BaoCaoDanhSachCbcnvDuocCuDiDaoTao";
        public const string BusinessForeignTraining = "BaoCaoDanhSachCbcnvCongTacNuocNgoai";
        public const string BusinessAward = "BaoCaoDanhSachBdhDaDuocTangDanhHieuThiDua";
        public const string BusinessDiscipline = "BaoCaoDanhSachCbcnvBiKyLuat";
        public const string BusinessProcessSalaryEmployee = "BaoCaoDienBienQuaTrinhLuongCanBo";
        public const string BusinessEmployeeReceiveGift8_3 = "BaoCaoDanhSachNhanVienDuocTangQuaMong8Thang3";
        public const string BusinessEmployeeReward = "BaoCaoDanhSachCbcnvDuocKhenThuong";
        public const string BusinessEmployeeSalaryInsurance = "DanhSachLaoDongVaQuyLuongTrichNopBhxhBhytBhtn";
        #endregion

        #region Timekeeping

        public const double MinutesInHour = 60;
        public const string SchedulerTimeSheet = "TimeSheet";
        public const string Monday = "Monday";
        public const string Tuesday = "Tuesday";
        public const string Wednesday = "Wednesday";
        public const string Thursday = "Thursday";
        public const string Friday = "Friday";
        public const string Saturday = "Saturday";
        public const string Sunday = "Sunday";

        public const string TimesheetGoWork = "CongTac";
        public const string TimesheetDayShift = "CaNgay";
        public const string TimesheetPaySalary = "HuongLuong";
        public const string TimesheetNotPaySalary = "KhongLuong";
        public const string TimesheetUnLeave = "KhongPhep";
        public const string TimesheetLate = "Muon";
        public const string TimesheetHoliday = "NghiLe";
        public const string TimesheetLeave = "NghiPhep";
        public const string TimesheetTypeTimeSheet = "Automatic";
        public const string TimesheetOverTime = "ThemGio";
        public const string TimesheetOverTimeDay = "TangCaNgay";
        public const string TimesheetOverTimeNight = "TangCaDem";
        public const string TimesheetOverTimeHoliday = "TangCaNgayLe";
        public const string TimesheetOverTimeWeekend = "TangCaNgayNghi";
        public const string TimesheetUndefined = "Undefined";
        public const string Default = @"Mặc định";
        public const string Undefined = @"Không xác định";
        public const string UndefinedInvalid = @"Chấm công không hợp lệ (không có chấm công vào hoặc không có chấm công ra).";
        public const string SymbolUndefined = @"!";
        public const string SymbolFullDay = "X";
        public const string SymbolOverTime = "TC";


        #endregion

        #region Salary
        public const int SalaryMonthRaiseOutFrame = 12;
        /// <summary>
        /// Tổng thu nhập
        /// </summary>
        public const string TotalIncome = "TotalIncome"; 
        /// <summary>
        /// Thuế TNCN
        /// </summary>
        public const string IndividualTax = "IndividualTax";
        /// <summary>
        /// BHXH công ty đóng
        /// </summary>
        public const string EnterpriseSocialInsurance = "EnterpriseSocialInsurance";
        /// <summary>
        /// BHXH người lao động đóng
        /// </summary>
        public const string LaborerSocialInsurance = "LaborerSocialInsurance";
        /// <summary>
        /// Lương thực lĩnh
        /// </summary>
        public const string ActualSalary = "ActualSalary";


        #endregion

        #region SQL Query
        public const string ConditionDefault = @" 1=1 ";

        #endregion

        #region Import Excel

        public const int FromRow = 2;
        public const int ToRow = 2;

        #endregion

        #region Report

        public const int IndexColumnWidth = 40;

        public const int ReportWidthLandscapeA4 = 1140;

        public const int ReportWidthLandscapeA3 = 1630;

        public const int ReportWidthLandscapeA2 = 2310;

        public const int ReportWidthPotraitA4 = 800;

        public const int ReportWidthPotraitA3 = ReportWidthLandscapeA4;

        public const int ReportWidthPotraitA2 = ReportWidthLandscapeA3;


        #endregion

        #region System

        public const string LimitPackage = @"LIMIT_PACKAGE";


        #endregion
    }

}

