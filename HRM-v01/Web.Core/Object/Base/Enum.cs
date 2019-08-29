using System.ComponentModel;

namespace Web.Core
{
    /// <summary>
    /// Giới tính
    /// </summary>
    public enum CatalogGroupSex
    {
        [Description("Nam")]
        Male,
        [Description("Nữ")]
        Female,
        [Description("Không xác định")]
        Unknown
    }

    /// <summary>
    /// Ngày lễ tết
    /// </summary>
    public enum CatalogGroupHoliday
    {
        [Description("Dương lịch")]
        DL,
        [Description("Âm lịch")]
        AL
    }

    /// <summary>
    /// Địa điểm
    /// </summary>
    public enum CatalogGroupLocation
    {
        [Description("Tỉnh")]
        Tinh,
        [Description("Thành phố trung ương")]
        ThanhPhoTW,
        [Description("Thành phố")]
        ThanhPho,
        [Description("Quận")]
        Quan,
        [Description("Huyện")]
        Huyen,
        [Description("Phường")]
        Phuong,
        [Description("Xã")]
        Xa,
        [Description("Thị xã")]
        ThiXa,
        [Description("Thị trấn")]
        ThiTran
    }

    /// <summary>
    /// Trình độ văn hóa (giáo dục phổ thông)
    /// </summary>
    public enum CatalogGroupBasicEducation
    {
        [Description("Tiểu học")]
        TieuHoc,
        [Description("Trung học cơ sở")]
        THCS,
        [Description("Trung học phổ thông")]
        THPT
    }

    /// <summary>
    /// Trình độ chuyên môn
    /// </summary>
    public enum CatalogGroupEducation
    {
        [Description("Giáo sư")]
        GS,
        [Description("Tiến sĩ")]
        TS,
        [Description("Thạc sĩ")]
        ThS,
        [Description("Đại học")]
        DH,
        [Description("Cao đẳng")]
        CD,
        [Description("Trung cấp")]
        TC,
        [Description("Sơ cấp")]
        SC,
        [Description("Chưa qua đào tạo")]
        CQDT,
        [Description("Dạy nghề thường xuyên")]
        DNTX
    }

    /// <summary>
    /// Nhóm hợp đồng
    /// </summary>
    public enum CatalogGroupContractType
    {
        [Description("Không xác định thời hạn")]
        KXDTH,
        [Description("Xác định thời hạn")]
        XDTH,
        [Description("Theo mùa vụ hoặc công việc")]
        HDTV
    }

    /// <summary>
    /// Trình độ ngoại ngữ
    /// </summary>
    public enum CatalogGroupLanguageLevel
    {
        [Description("Đại học trở lên (Tiếng Anh)")]
        DHTA,
        [Description("Chứng chỉ (Tiếng Anh)")]
        CCTA,
        [Description("Đai học trở lên (Ngoại ngữ khác)")]
        DHNNK,
        [Description("Chứng chỉ (Ngoại ngữ khác)")]
        CCNNK,
        [Description("Chứng chỉ tiếng dân tộc")]
        CCTDT
    }

    /// <summary>
    /// Trình độ tin học
    /// </summary>
    public enum CatalogGroupITLevel
    {
        [Description("Trung cấp trở lên")]
        TC,
        [Description("Chứng chỉ")]
        CC
    }

    /// <summary>
    /// Trình độ quản lý
    /// </summary>
    public enum CatalogGroupManagementLevel
    {
        [Description("Chuyên viên cao cấp hoặc Tương đương")]
        CVCC,
        [Description("Chuyên viên chính hoặc Tương đương")]
        CVC,
        [Description("Chuyên viên hoặc Tương đương")]
        CV,
        [Description("Chưa qua đào tạo")]
        CQDT
    }

    /// <summary>
    /// Trình độ chính trị
    /// </summary>
    public enum CatalogGroupPoliticLevel
    {
        [Description("Cử nhân")]
        CN,
        [Description("Cao cấp")]
        CC,
        [Description("Trung cấp")]
        TC,
        [Description("Sơ cấp")]
        SC
    }

    /// <summary>
    /// Loại nhân viên
    /// </summary>
    public enum CatalogGroupEmployeeType
    {
        [Description("Nhân viên")]
        NV,
        [Description("Công chức")]
        CC,
        [Description("Viên chức")]
        VC,
        [Description("Cán bộ")]
        CB
    }

    /// <summary>
    /// Nhóm ngạch
    /// </summary>
    public enum CatalogGroupGroupQuantum
    {
        [Description("Không xác định")]
        UNKNOWN,
        [Description("Ngạch nhân viên")]
        NV,
        [Description("Ngạch cán sự và tương đương")]
        CS,
        [Description("Ngạch chuyên viên và tương đương")]
        CV,
        [Description("Ngạch chuyên viên chính và tương đương")]
        CVC,
        [Description("Ngạch chuyên viên cao cấp và tương đương")]
        CVCC
    }

    /// <summary>
    /// Nhóm ngạch
    /// </summary>
    public enum CatalogGroupTimeSheetGroupSymbol
    {
        [Description("Không xác định")]
        Undefined,
        [Description("Công tác")]
        CongTac,
        [Description("Ca ngày")]
        CaNgay,
        [Description("Hưởng lương")]
        HuongLuong,
        [Description("Không lương")]
        KhongLuong,
        [Description("Không phép")]
        KhongPhep,
        [Description("Muộn")]
        Muon,
        [Description("Nghỉ lễ")]
        NghiLe,
        [Description("Nghỉ phép")]
        NghiPhep,
        [Description("Thêm giờ")]
        ThemGio,
        [Description("Tăng ca ngày")]
        TangCaNgay,
        [Description("Tăng ca đêm")]
        TangCaDem,
        [Description("Tăng ca ngày lễ")]
        TangCaNgayLe,
        [Description("Tăng ca ngày nghỉ")]
        TangCaNgayNghi
    }

    /// <summary>
    /// Phụ cấp
    /// </summary>
    public enum CatalogGroupAllowance
    {
        [Description("Phụ cấp tính theo hệ số")]
        PhuCapTinhTheoHeSo,
        [Description("Phụ cấp khác")]
        PhuCapKhac,
    }

    /// <summary>
    /// Trạng thái danh mục
    /// </summary>
    public enum CatalogStatus
    {
        [Description("Kích hoạt")]
        Active = 1,
        [Description("Khóa")]
        Locked = 2
    }

    /// <summary>
    /// Loại phụ cấp
    /// </summary>
    public enum AllowanceType
    {
        [Description("Phụ cấp chức vụ")]
        PhuCapChucVu,
        [Description("Phụ cấp ưu đãi nghề")]
        PhuCapUuDaiNghe,
        [Description("Phụ cấp trách nhiệm")]
        PhuCapTrachNhiem,
        [Description("Phụ cấp thâm niên vượt khung")]
        PhuCapThamNienVuotKhung,
        [Description("Phụ cấp thâm niên ngành")]
        PhuCapThamNienNganh,
        [Description("Phụ cấp công vụ")]
        PhuCapCongVu,
        [Description("Phụ cấp kiêm nhiệm")]
        PhuCapKiemNhiem,
        [Description("Phụ cấp độc hại")]
        PhuCapDocHai,
        [Description("Phụ cấp bảo mật")]
        PhuCapBaoMat,
        [Description("Phụ cấp khu vực")]
        PhuCapKhuVuc,
        [Description("Phụ cấp bảo lưu")]
        PhuCapBaoLuu,
        [Description("Phụ cấp đặc biệt")]
        PhuCapDacBiet,
        [Description("Phụ cấp Đảng")]
        PhuCapDang,
        [Description("Phụ cấp an ninh quốc phòng")]
        PhuCapAnNinhQuocPhong,
        [Description("Phụ cấp cựu chiến binh")]
        PhuCapCuuChienBinh,
        [Description("Phụ cấp dân quân tự vệ")]
        PhuCapDanQuanTuVe,
        [Description("Phụ cấp hệ số lương 0.5")]
        PhuCapHeSoLuong,
        [Description("Phụ cấp Đoàn")]
        PhuCapDoan,
        [Description("Phụ cấp cơ yếu")]
        PhuCapCoYeu,
        [Description("Phụ cấp tin học")]
        PhuCapTinHoc,
        [Description("Phụ cấp website")]
        PhuCapWebsite,
        [Description("Phụ cấp hướng dẫn tập sự")]
        PhuCapHuongDanTapSu,
        [Description("Phụ cấp văn thư")]
        PhuCapVanThu,
        [Description("Phụ cấp tổng hợp")]
        PhuCapTongHop,
        [Description("Phụ cấp ăn trưa")]
        PhuCapAnTrua,
        [Description("Phụ cấp nước uống")]
        PhuCapNuocUong,
        [Description("Phụ cấp điện thoại")]
        PhuCapDienThoai,
        [Description("Phụ cấp công tác phí")]
        PhuCapCongTacPhi,
        [Description("Phụ cấp tiền khoán xe")]
        PhuCapTienKhoanXe,
        [Description("Phụ cấp giới")]
        PhuCapGioi,
        [Description("Phụ cấp đề tài, nhiệm vụ khoa học")]
        PhuCapDeTaiNhiemVuKhoaHoc,
        [Description("Phụ cấp hội thảo")]
        PhuCapHoiThao,
        [Description("Phụ cấp khác")]
        PhuCapKhac
    }

    /// <summary>
    /// Loại giá trị phụ cấp
    /// </summary>
    public enum AllowanceValueType
    {
        [Description("Giá trị cố định")]
        FixValue = 1,
        [Description("Giá trị hệ số lương")]
        FactorValue = 2,
        [Description("Giá trị phần trăm lương")]
        PercentValue = 3,
        [Description("Công thức hệ số lương")]
        FormulaFactor = 4,
        [Description("Công thức phần trăm lương")]
        FormulaPercent = 5
    }

    /// <summary>
    /// Loại tham số đầu vào cho công thức tính phụ cấp
    /// </summary>
    public enum AllowanceFormulaInputType
    {
        [Description("Hệ số lương")]
        Factor,
        [Description("Lương tính theo hệ số")]
        Salary,
        [Description("Lương cơ bản")]
        BasicSalary,
        [Description("Lương bảo hiểm")]
        InsuranceSalary,
        [Description("Lương hợp đồng")]
        ContractSalary,
        [Description("Lương tổng")]
        GrossSalary,
        [Description("Lương thực lĩnh")]
        NetSalary,
        [Description("% Hưởng lương")]
        PercentageSalary,
        [Description("% Vượt khung")]
        PercentageOverGrade,
    }


    public enum MenuGroup
    {
        [Description("Bàn làm việc")]
        Dashboard = 1,
        [Description("Menu trái")]
        MenuLeft = 2,
        [Description("Menu hệ thống")]
        MenuTop = 3
    }

    public enum MenuStatus
    {
        [Description("Kích hoạt")]
        Active = 1,
        [Description("Khóa")]
        Locked = 2
    }

    public enum RecordStatus
    {
        [Description("Đang làm việc")]
        Working = 1,
        [Description("Đã rời khỏi đơn vị")]
        Left = 2,
        [Description("Đang nghỉ chế độ")]
        OffMode = 3,
        [Description("Khác")]
        Other = 4
    }

    public enum EventStatus
    {
        [Description("Giá trị mặc định")]
        Default = 0,
        [Description("Đang sử dụng")]
        Active = 1,
        [Description("Đã xóa")]
        Delete = 2,
        [Description("Đang chờ duyệt")]
        Pending = 3,
        [Description("Đã duyệt")]
        Approve = 4
    }

    public enum SalaryConfigDataType
    {
        [Description("Giá trị từ trường")]
        FieldValue = 1,
        [Description("Giá trị cố định")]
        FixedValue = 2,
        [Description("Giá trị động")]
        DynamicValue = 3,
        [Description("Giá trị công thức")]
        Formula = 4,
        [Description("Giá trị từ trường - công thức")]
        FieldFormula = 5
    }

    public enum TrainingStatus
    {
        [Description("Giá trị mặc định")]
        Default = 0,
        [Description("Đã hoàn thành")]
        Complete = 1,
        [Description("Đang kế hoạch")]
        Pending = 2
    }

    public enum SalaryDecisionType
    {
        [Description("Thường xuyên")]
        Regular = 0,
        [Description("Vượt khung")]
        OverGrade = 1,
        [Description("Trước thời hạn")]
        ExceedTime = 2
    }

    public enum SalaryDecisionStatus
    {
        [Description("Đang chờ duyệt")]
        Pending = 1,
        [Description("Đã duyệt")]
        Approved = 2,
        [Description("Tạm dừng")]
        Paused = 3,
        [Description("Khóa")]
        Locked = 4
    }

    public enum PayrollStatus
    {
        [Description("Kích hoạt")]
        Active = 1,
        [Description("Khóa")]
        Locked = 2
    }

    public enum ReportTemplate
    {
        [Description("Bảng lương doanh nghiệp")]
        EnterprisePayroll = 1,
    }

    public enum ReportPaperKind
    {
        [Description("A4"), DefaultValue(1140)]
        A4 = 1,
        [Description("A3"), DefaultValue(1630)]
        A3 = 2,
        [Description("A2"), DefaultValue(2320)]
        A2 = 3,
    }

    public enum ReportOrientation
    {
        [Description("Ngang")]
        Landscape = 1,
        [Description("Dọc")]
        Portrait = 2,
    }

    public enum ReportGroupHeader
    {
        [Description("Không nhóm")]
        NoGroup = 1,
        [Description("Phòng ban")]
        Department = 2,
        [Description("Chức vụ")]
        Position = 3,
    }

    public enum ReportStatus
    {
        [Description("Kích hoạt")]
        Active = 1,
        [Description("Khóa")]
        Locked = 2
    }

    public enum ReportType
    {
        [Description("Báo cáo lương")]
        Payroll = 1,
        [Description("Báo cáo chấm công")]
        TimeSheet = 2
    }

    public enum ReportColumnType
    {
        [Description("Đầu báo báo")]
        Header = 1,
        [Description("Đầu nhóm")]
        HeaderGroup = 3,
        [Description("Nội dung")]
        Detail = 5,
        [Description("Chân nhóm")]
        FooterGroup = 4,
        [Description("Chân báo báo")]
        Footer = 2,
    }

    public enum ReportColumnDataType
    {
        [Description("Chữ")]
        String = 1,
        [Description("Số")]
        Number = 2
    }

    public enum ReportColumnStatus
    {
        [Description("Kích hoạt")]
        Active = 1,
        [Description("Khóa")]
        Locked = 2
    }

    public enum ReportTextAlign
    {
        [Description("Chính giữa")]
        MiddleCenter = 9,
        [Description("Giữa trái")]
        MiddleLeft = 11,
        [Description("Giữa phải")]
        MiddleRight = 12,
        [Description("Giữa trên")]
        TopCenter = 5,
        [Description("Giữa dưới")]
        BottomCenter = 1,
        [Description("Canh lề trên")]
        TopJustify = 6,
        [Description("Canh lề giữa")]
        MiddleJustify = 10,
        [Description("Canh lề dưới")]
        BottomJustify = 2,
        [Description("Trái trên")]
        TopLeft = 7,
        [Description("Trái dưới")]
        BottomLeft = 3,
        [Description("Phải trên")]
        TopRight = 8,
        [Description("Phải dưới")]
        BottomRight = 4
    }

    public enum ReportSummaryRunning
    {
        [Description("Không tính tổng")]
        None = 1,
        [Description("Tính theo nhóm")]
        Group = 2,
        [Description("Tính trên toàn báo cáo")]
        Report = 3,
        [Description("Tính theo trang")]
        Page = 4
    }

    public enum ReportSummaryFunction
    {
        [Description("Không tính toán")]
        None = 1,
        [Description("Tính tổng")]
        Sum = 2,
        [Description("Đếm")]
        Count = 3,
        [Description("Đếm tùy biến")]
        CustomCount = 4
    }

    public enum DayOfWeekEnum
    {
        [Description("Chủ nhật")]
        Sunday = 0,
       [Description("Thứ 2")]
        Monday = 1,
        [Description("Thứ 3")]
        Tuesday = 2,
        [Description("Thứ 4")]
        Wednesday = 3,
        [Description("Thứ 5")]
        Thursday = 4,
        [Description("Thứ 6")]
        Friday = 5,
        [Description("Thứ 7")]
        Saturday = 6,
       
    }

    public enum TimeSheetHandlerType
    {
        [Description("Tự động")]
        Automatic = 0,
        [Description("Thủ công")]
        Manual = 1
    }

    public enum TimeSheetAdjustmentType
    {
        [Description("Mặc định")]
        Default = 0,
        [Description("Hiệu chỉnh")]
        Adjustment = 1,
        [Description("Hiệu chỉnh thêm giờ")]
        AdjustmentOverTime = 2,
    }

    public enum TimeSheetRuleWrongTimeType
    {
        [Description("Đi muộn")]
        ComeLate = 1,
        [Description("Về sớm")]
        LeaveEarly = 2
    }

    public enum TimeSheetGroupWorkShiftStatus
    {
        [Description("Kích hoạt")]
        Active = 1,
        [Description("Khóa")]
        Locked = 2
    }

    public enum TimeSheetStatus
    {
        [Description("Kích hoạt")]
        Active = 1,
        [Description("Khóa")]
        Locked = 2
    }

    public enum SystemAction
    {
        [Description("Đăng nhập")]
        Login = 1,
        [Description("Đăng xuất")]
        Logout = 2,
        [Description("Thêm mới")]
        Create = 3,
        [Description("Chỉnh sửa")]
        Edit = 4,
        [Description("Xóa")]
        Delete = 5,
        [Description("Khác")]
        Other = 6,
        [Description("Ghi nhận lỗi")]
        TrackError = 7,
        [Description("Thực hiện tác vụ")]
        ExcuteJob = 8
    }

    public enum SystemLogType
    {
        [Description("Hành vi của người dùng")]
        UserAction = 1,
        [Description("Lỗi hoạt động của người dùng")]
        UserError = 2,
        [Description("Hành vi của tiến trình")]
        ScheduleAction = 3,
        [Description("Lỗi hoạt động của tiến trình")]
        ScheduleError = 4,
        [Description("Lỗi trong quá trình hoạt động")]
        HandlerException = 5,
        [Description("Lỗi bất thường")]
        UnHandlerException = 6,
    }

    public enum WorkingFormType
    {
        [Description("Toàn thời gian")]
        FullTime = 1,
        [Description("Bán thời gian")]
        PartTime = 2,
        [Description("Theo ca")]
        WorkShift = 3,
        [Description("Theo giờ")]
        WorkTime = 4
    }

    public enum KpiStatus
    {
        [Description("Kích hoạt")]
        Active = 1,
        [Description("Khóa")]
        Locked = 2
    }

    public enum KpiValueType
    {
        [Description("Kiểu số")]
        Number = 1,
        [Description("Kiểu chữ")]
        String = 2,
        [Description("Phần trăm")]
        Percent = 3,
        [Description("Công thức")]
        Formula = 4,
    }

    public enum WorkShiftType
    {
        [Description("Ca thường")]
        Normal =1,
        [Description("Ca gãy")]
        Break =2
    }

    public enum ReportKindType
    {
        [Description("HJM")]
        HJM,
        [Description("HRM")]
        HRM
    }

    public enum InsuranceType
    {
        [Description("Tăng bảo hiểm")]
        Increase = 1,
        [Description("Giảm bảo hiểm")]
        Decrease = 2,
        [Description("Thay đổi bảo hiểm")]
        Change = 3
    }

    public enum DecisionType
    {
        [Description("Nghỉ việc")]
        Leave = 1,
        [Description("Nghỉ thai sản")]
        Maternity = 2,
        [Description("Nghỉ hưu")]
        Retire = 3,
        [Description("Nghỉ chế độ")]
        OffMode = 4
    }

    #region Recruitment
    public enum RecruitmentStatus
    {
        [Description("Tất cả")]
        All = 0,
        [Description("Chờ duyệt")]
        Pending = 2,
        [Description("Đã duyệt")]
        Approved = 3,
        [Description("Hoàn thành")]
        Complete = 4,
        [Description("Hủy bỏ")]
        Cancel = 5,
        [Description("Không duyệt")]
        UnApproved = 6
    }

    public enum ExperienceType
    {
        [Description("Chưa có kinh nghiệm")]
        None = 0,
        [Description("Dưới 1 năm")]
        UnderOneYear = 1,
        [Description("1 năm")]
        OneYear = 2,
        [Description("2 năm")]
        TwoYear = 3,
        [Description("4 năm")]
        FourYear = 4,
        [Description("5 năm")]
        FiveYear = 5,
        [Description("Trên 5 năm")]
        OverFiveYear = 6
    }

    public enum CandidateType
    {
        [Description("Phỏng vấn")]
        Interview = 0,
        [Description("Không trúng tuyển")]
        Fail = 1,
        [Description("Chuyển yêu cầu tuyển dụng")]
        TransferRequirement = 2,
        [Description("Trúng tuyển")]
        Passed = 3,
        [Description("Trúng tuyển nhưng không đi làm")]
        PassedNotWork = 4
    }


    #endregion

    public enum RecordType
    {
        [Description("Hồ sơ")]
        Default = 0,
        [Description("Hồ sơ ứng viên")]
        Candidate = 1
    }

    /// <summary>
    /// Giới tính
    /// </summary>
    public enum SexType
    {
        [Description("Nam")]
        Male = 1,
        [Description("Nữ")]
        Female = 0,
        [Description("Không yêu cầu")]
        Unknown = 2
    }
}
