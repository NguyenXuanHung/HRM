using System;
using System.ComponentModel;

namespace Web.Core.Object.HumanRecord
{
    public class hr_Record : BaseEntity  //HOSO
    {
        public hr_Record()
        {
            DepartmentId = 0;
            ManagementDepartmentId = 0;
            EmployeeCode = "";
            FunctionaryCode = "";
            Name = "";
            FullName = "";
            Alias = "";
            Sex = false;
            MaritalStatusId = 0;
            BirthPlace = "";
            BirthPlaceWardId = 0;
            BirthPlaceDistrictId = 0;
            BirthPlaceProvinceId = 0;
            Hometown = "";
            HometownWardId = 0;
            HometownDistrictId = 0;
            HometownProvinceId = 0;
            ImageUrl = "";
            OriginalFile = "";
            FolkId = 0;
            ReligionId = 0;
            PersonalClassId = 0;
            FamilyClassId = 0;
            ResidentPlace = "";
            Address = "";
            PreviousJob = "";
            RecruimentDepartment = "";
            PositionId = 0;
            JobTitleId = 0;
            AssignedWork = "";
            BasicEducationId = 0;
            EducationId = 0;
            PoliticLevelId = 0;
            ManagementLevelId = 0;
            LanguageLevelId = 0;
            ITLevelId = 0;
            CPVCardNumber = "";
            CPVPositionId = 0;
            CPVJoinedPlace = "";
            VYUPositionId = 0;
            VYUJoinedPlace = "";
            ArmyLevelId = 0;
            TitleAwarded = "";
            Skills = "";
            HealthStatusId = 0;
            BloodGroup = "";
            Height = 0;
            Weight = 0;
            RankWounded = "";
            FamilyPolicyId = 0;
            IDNumber = "";
            IDIssuePlaceId = 0;
            InsuranceNumber = "";
            PersonalTaxCode = "";
            CellPhoneNumber = "";
            HomePhoneNumber = "";
            WorkPhoneNumber = "";
            WorkEmail = "";
            PersonalEmail = "";
            Biography = "";
            ForeignOrganizationJoined = "";
            RelativesAboard = "";
            Review = "";
            ContactPersonName = "";
            ContactRelation = "";
            ContactPhoneNumber = "";
            ContactAddress = "";
            WorkStatusId = 0;
            WorkStatusReason = "";
            EmployeeTypeId = 0;
            IndustryId = 0;
            FamilyIncome = 0;
            OtherIncome = "";
            AllocatedHouse = "";
            AllocatedHouseArea = 0;
            House = "";
            HouseArea = 0;
            AllocatedLandArea = 0;
            BusinessLandArea = 0;
            LandArea = 0;
            GovernmentPositionId = 0;
            PluralityPositionId = 0;
            LongestJob = "";
            Status = RecordStatus.Working;
            IsDeleted = false;
            WorkingFormId = WorkingFormType.FullTime;
            TeamId = 0;
            ConstructionId = 0;
            ProbationWorkingTime = 0;
            StudyWorkingDay = 0;
            WorkLocationId = 0;
            UnionJoinedPlace = "";
            UnionJoinedPositionId = 0;
            GraduationYear = 0;
            GraduationTypeId = 0;
            UniversityId = 0;
            HealthInsuranceNumber = "";
            Type = RecordType.Default;

            CreatedDate = DateTime.Now;
            CreatedBy = "system";
            EditedDate = DateTime.Now;
            EditedBy = "system";
        }

        // mã đơn vị
        [Description("Danh tính phòng ban")]
        public int DepartmentId { get; set; }

        // mã đơn vị quản lý
        [Description("Danh tính phòng ban quản lý")]
        public int ManagementDepartmentId { get; set; }

        // mã nhân viên
        [Description("Mã nhân viên")]
        public string EmployeeCode { get; set; } //MA_CB

        // mã cán bộ công chức
        [Description("Mã chức năng")]
        public string FunctionaryCode { get; set; } //SOHIEU_CCVC

        // tên
        [Description("Tên")]
        public string Name { get; set; } //TEN_CB

        // họ tên
        [Description("Họ tên")]
        public string FullName { get; set; } //HO_TEN

        // bí danh
        [Description("Tên khác")]
        public string Alias { get; set; } //BI_DANH

        // ngày sinh
        [Description("Ngày sinh (dd/MM/yyyy)")]
        public DateTime? BirthDate { get; set; } //NGAY_SINH

        // giới tính
        [Description("Giới tính")]
        public bool Sex { get; set; } //MA_GIOITINH

        // tình trạng hôn nhân
        [Description("Tình trạng hôn nhân")]
        public int MaritalStatusId { get; set; } //MA_TT_HN       
        
        // nơi sinh
        [Description("Nơi sinh")]
        public string BirthPlace { get; set; }

        [Description("Xã nơi sinh")]
        public int BirthPlaceWardId { get; set; } //NOI_SINH_XA

        [Description("Huyện nơi sinh")]
        public int BirthPlaceDistrictId { get; set; } //NOI_SINH_HUYEN

        [Description("Tỉnh nơi sinh")]
        public int BirthPlaceProvinceId { get; set; } //NOI_SINH_TINH

        // quê quán
        [Description("Quê quán")]
        public string Hometown { get; set; }

        [Description("Xã quê quán")]
        public int HometownWardId { get; set; } //QUEQUAN_XA

        [Description("Huyện quê quán")]
        public int HometownDistrictId { get; set; } //QUEQUAN_HUYEN

        [Description("Tỉnh quê quán")]
        public int HometownProvinceId { get; set; } //QUEQUAN_TINH

        // ảnh đại diện
        [Description("URL ảnh")]
        public string ImageUrl { get; set; } //PHOTO

        // hồ sơ gốc
        [Description("File gốc")]
        public string OriginalFile { get; set; } //HOSOGOC

        // dân tộc
        [Description("Dân tộc")]
        public int FolkId { get; set; } //MA_DANTOC

        // tôn giáo
        [Description("Tôn giáo")]
        public int ReligionId { get; set; } //MA_TONGIAO

        // thành phần bản thân
        [Description("Thành phần bản thân")]
        public int PersonalClassId { get; set; } //MA_TP_BANTHAN

        // thành phần gia đình
        [Description("Thành phần gia đình")]
        public int FamilyClassId { get; set; } //MA_TP_GIADINH

        // hộ khẩu thường trú
        [Description("Hộ khẩu thường trú")]
        public string ResidentPlace { get; set; } //HO_KHAU

        // địa chỉ liên hệ
        [Description("Địa chỉ liên hệ")]
        public string Address { get; set; } //DIA_CHI_LH

        // nghề trước tuyển dụng
        [Description("Công việc trước đây")]
        public string PreviousJob { get; set; } //NGHE_TRUOC_TUYEN

        // đơn vị tuyển dụng
        [Description("Phòng tuyển dụng")]
        public string RecruimentDepartment { get; set; } //COQUAN_TUYENDUNG

        // ngày tuyển dụng đầu tiên
        [Description("Ngày tuyển dụng (dd/MM/yyyy)")]
        public DateTime? RecruimentDate { get; set; } //NGAY_TUYEN_DTIEN

        // ngày vào chính thức
        [Description("Ngày tham gia (dd/MM/yyyy)")]
        public DateTime? ParticipationDate { get; set; } //NGAY_TUYEN_CHINHTHUC

        // ngày biên chế
        [Description("Ngày biên chế (dd/MM/yyyy)")]
        public DateTime? FunctionaryDate { get; set; } //NgayBienChe

        // chức vụ
        [Description("Chức vụ")]
        public int PositionId { get; set; } //MA_CHUCVU 

        // chức danh
        [Description("Chức danh")]
        public int JobTitleId { get; set; } //MA_CONGVIEC

        // công việc được giao
        [Description("Công việc được giao")]
        public string AssignedWork { get; set; } //CONGVIEC_DUOCGIAO

        // trình độ văn hóa
        [Description("Trình độ giáo dục phổ thông")]
        public int BasicEducationId { get; set; } //MA_TD_VANHOA

        // chuyên môn cao nhất
        [Description("Trình độ chuyên môn cao nhất")]
        public int EducationId { get; set; } //MA_TRINHDO

        // lý luận chính trị
        [Description("Trình độ lý luận chính trị")]
        public int PoliticLevelId { get; set; } //MA_TD_CHINHTRI

        // quản lý nhà nước
        [Description("Quản lý nhà nước")]
        public int ManagementLevelId { get; set; } //MA_TD_QUANLY

        // ngoại ngữ
        [Description("Ngoại ngữ")]
        public int LanguageLevelId { get; set; } //MA_NGOAINGU

        // tin học
        [Description("Tin học")]
        public int ITLevelId { get; set; } //MA_TINHOC

        // ngày vào đảng (Commnunity Party of Vietnam)
        [Description("Ngày vào đảng (dd/MM/yyyy)")]
        public DateTime? CPVJoinedDate { get; set; } //NGAYVAO_DANG

        // ngày vào đảng chính thức
        [Description("Ngày vào chính thức (dd/MM/yyyy)")]
        public DateTime? CPVOfficialJoinedDate { get; set; } //NGAY_CTHUC_DANG

        // thẻ đảng
        [Description("Số thẻ Đảng")]
        public string CPVCardNumber { get; set; } //SOTHE_DANG

        // chức vụ đảng
        [Description("Chức vụ Đảng")]
        public int CPVPositionId { get; set; } //MA_CHUCVU_DANG

        // nơi kết nạp đảng
        [Description("Nơi kết nạp Đảng")]
        public string CPVJoinedPlace { get; set; } //NOI_KETNAP_DANG

        // ngày vào đoàn (Vietnam Youth Union)
        [Description("Ngày vào Đoàn (dd/MM/yyyy)")]
        public DateTime? VYUJoinedDate { get; set; } //NGAYVAO_DOAN

        // chức vụ đoàn
        [Description("Chức vụ Đoàn")]
        public int VYUPositionId { get; set; } //MA_CHUCVU_DOAN

        // nơi kết nạp đoàn
        [Description("Nơi kết nạp Đoàn")]
        public string VYUJoinedPlace { get; set; } //NOI_KETNAP_DOAN

        // ngày vào quân đội
        [Description("Ngày nhập ngũ (dd/MM/yyyy)")]
        public DateTime? ArmyJoinedDate { get; set; } //NGAYVAO_QDOI

        // ngày ra quân đội
        [Description("Ngày xuất ngũ (dd/MM/yyyy)")]
        public DateTime? ArmyLeftDate { get; set; } //NGAYRA_QDOI

        // cấp bậc quân đội
        [Description("Quân hàm cao nhất")]
        public int ArmyLevelId { get; set; } //MA_CAPBAC_QDOI

        // danh hiệu được phong tặng
        [Description("Danh hiệu được trao cao nhất")]
        public string TitleAwarded { get; set; }  //DANHHIEU_PHONGTANG

        // sở trường, kỹ năng
        [Description("Sở trường công tác")]
        public string Skills { get; set; } //SOTRUONG_CONGTAC

        // tình trạng sức khỏe
        [Description("Tình trạng sức khỏe")]
        public int HealthStatusId { get; set; } //MA_TT_SUCKHOE

        // nhóm máu
        [Description("Nhóm máu")]
        public string BloodGroup { get; set; } //NHOM_MAU

        // chiều cao
        [Description("Chiều cao")]
        public decimal Height { get; set; } //CHIEU_CAO

        // cân nặng
        [Description("Cân nặng")]
        public decimal Weight { get; set; } //CAN_NANG

        // hạng thương binh
        [Description("Xếp hạng thương binh")]
        public string RankWounded { get; set; } //HangThuongTat 

        // gia đình chính sách
        [Description("Gia đình chích sách")]
        public int FamilyPolicyId { get; set; }  //MA_LOAI_CS

        // số chứng minh nhân dân
        [Description("Số CMND")]
        public string IDNumber { get; set; } //SO_CMND

        // ngày cấp
        [Description("Ngày cấp (dd/MM/yyyy)")]
        public DateTime? IDIssueDate { get; set; } //NGAYCAP_CMND

        // nơi cấp
        [Description("Nơi cấp")]
        public int IDIssuePlaceId { get; set; } //MA_NOICAP_CMND

        // số bảo hiểm xã hội
        [Description("Số sổ BHXH")]
        public string InsuranceNumber { get; set; } //SOTHE_BHXH

        // ngày cấp bảo hiểm xã hội
        [Description("Ngày cấp sổ (dd/MM/yyyy)")]
        public DateTime? InsuranceIssueDate { get; set; } //NGAYCAP_BHXH

        // mã số thuế cá nhân
        [Description("Mã thuế cá nhân")]
        public string PersonalTaxCode { get; set; } //MST_CANHAN

        // trạng thái làm việc
        [Description("")]
        public int WorkStatusId { get; set; } //TrangThai

        /// <summary>
        /// Ngay trang thai
        /// </summary>
        [Description("Ngày trạng thái (dd/MM/yyyy)")]
        public DateTime? WorkStatusDate { get; set; }//NgayTrangThai

        // trạng thái hồ sơ
        [Description("")]
        public RecordStatus Status { get; set; }

        // số điện thoại di động
        [Description("Di động")]
        public string CellPhoneNumber { get; set; } //DI_DONG

        // số điện thoại nhà riêng
        [Description("Điện thoại nhà")]
        public string HomePhoneNumber { get; set; } //DT_NHA

        // số điện thoại cơ quan
        [Description("Điện thoại cơ quan")]
        public string WorkPhoneNumber { get; set; } //DT_CQUAN

        // email nội bộ
        [Description("Email cơ quan")]
        public string WorkEmail { get; set; } //EMAIL

        // email riêng
        [Description("Email cá nhân")]
        public string PersonalEmail { get; set; } //EMAIL_RIENG

        // tiểu sử tiền án, tiền sự của bản thân (nếu có)
        [Description("Tiểu sử")]
        public string Biography { get; set; } //LICHSU_BANTHAN

        // tiểu sử đã làm việc cho tổ chức nước ngoài
        [Description("Tổ chức nước ngoài đã tham gia")]
        public string ForeignOrganizationJoined { get; set; } //THAMGIA_TOCHUC

        // thân nhân ở nước ngoài (nếu có)
        [Description("Người thân ở nước ngoài")]
        public string RelativesAboard { get; set; } //LICHSU_THANNHAN

        // nhận xét đánh giá
        [Description("Đánh giá")]
        public string Review { get; set; } //NHANXET

        // tên người liên hệ
        [Description("Tên người liên lạc")]
        public string ContactPersonName { get; set; } //NguoiLienHe

        // quan hệ với cán bộ
        [Description("Quan hệ với người liên lạc")]
        public string ContactRelation { get; set; } //QuanHeVoiCanBo

        // số điện thoại người liên hệ
        [Description("SĐT liên hệ")]
        public string ContactPhoneNumber { get; set; } //SDTNguoiLienHe

        // địa chỉ người liên hệ
        [Description("Địa chỉ liên hệ")]
        public string ContactAddress { get; set; }
        
        /// <summary>
        /// Ma ly do trang thai
        /// </summary>
        [Description("Lý do trạng thái")]
        public string WorkStatusReason { get; set; }//MaLyDoTrangThai

        /// <summary>
        /// id loai can bo
        /// </summary>
        [Description("Loại cán bộ")]
        public int EmployeeTypeId { get; set; }//LoaiCanBo

        /// <summary>
        /// id chuyên ngành
        /// </summary>
        [Description("Ngành")]
        public int IndustryId { get; set; }

        /// <summary>
        /// thu nhập gia đinh
        /// </summary>
        [Description("Thu nhập gia đinh")]
        public decimal FamilyIncome { get; set; }

        /// <summary>
        /// Các nguồn thu nhập khác
        /// </summary>
        [Description("Các nguồn thu nhập khác")]
        public string OtherIncome { get; set; }

        /// <summary>
        /// Nhà được cấp, được thuê, loại nhà
        /// </summary>
        [Description("Nhà được cấp, được thuê, loại nhà")]
        public string AllocatedHouse { get; set; }

        /// <summary>
        /// Diện tích nhà được cấp, được thuê
        /// </summary>
        [Description("Diện tích nhà được cấp, được thuê")]
        public decimal AllocatedHouseArea { get; set; }

        /// <summary>
        /// Nhà tự mua, tự xây, loại nhà
        /// </summary>
        [Description("Nhà tự mua, tự xây, loại nhà")]
        public string House { get; set; }

        /// <summary>
        /// Diện tích nhà tự mua, tự xây
        /// </summary>
        [Description("Diện tích nhà tự mua, tự xây")]
        public decimal HouseArea { get; set; }

        /// <summary>
        /// Diện tích đất được cấp, cho thuê
        /// </summary>
        [Description("Diện tích đất được cấp, cho thuê")]
        public decimal AllocatedLandArea { get; set; }

        /// <summary>
        /// Đất sản xuất kinh doanh
        /// </summary>
        [Description("Diện tích đất sản xuất kinh doanh")]
        public decimal BusinessLandArea { get; set; }

        /// <summary>
        /// Diện tích đất tự mua
        /// </summary>
        [Description("Diện tích đất tự mua")]
        public decimal LandArea { get; set; }

        /// <summary>
        /// Chức vụ chính quyền
        /// </summary>
        public int GovernmentPositionId { get; set; }

        /// <summary>
        /// Chức vụ kiêm nhiệm
        /// </summary>
        public int PluralityPositionId { get; set; }

        /// <summary>
        /// Ngày tham gia cách mạng
        /// </summary>
        [Description("Ngày tham gia cách mạng (dd/MM/yyyy)")]
        public DateTime? RevolutionJoinedDate { get; set; }

        /// <summary>
        /// Công việc đã làm lâu nhất
        /// </summary>
        [Description("Công việc đã làm lâu nhất")]
        public string LongestJob { get; set; }

        /// <summary>
        /// True if object was deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// id tổ, đội
        /// </summary>
        public int TeamId { get; set; }
        /// <summary>
        /// id công trình
        /// </summary>
        public int ConstructionId { get; set; }
        /// <summary>
        /// Hình thức làm việc
        /// </summary>
        public WorkingFormType WorkingFormId { get; set; }
        /// <summary>
        /// Thời gian thử việc
        /// </summary>
        public int ProbationWorkingTime { get; set; }
        /// <summary>
        /// Số ngày học việc
        /// </summary>
        public int StudyWorkingDay { get; set; }
        /// <summary>
        /// Id địa điểm làm việc
        /// </summary>
        public int WorkLocationId { get; set; }
        /// <summary>
        /// Id xếp loại
        /// </summary>
        public int GraduationTypeId { get; set; }
        /// <summary>
        /// Id trường đào tạo
        /// </summary>
        public int UniversityId { get; set; }
        /// <summary>
        /// Năm tốt nghiệp
        /// </summary>
        public int GraduationYear { get; set; }
        /// <summary>
        /// ngày vào công đoàn
        /// </summary>
        public DateTime? UnionJoinedDate { get; set; }
        /// <summary>
        /// nơi vào công đoàn
        /// </summary>
        public string UnionJoinedPlace { get; set; }
        /// <summary>
        /// Chức vụ công đoàn
        /// </summary>
        public int UnionJoinedPositionId { get; set; }

        /// <summary>
        /// Số thẻ BHYT
        /// </summary>
        public string HealthInsuranceNumber { get; set; }

        /// <summary>
        /// Ngày đóng BHYT
        /// </summary>
        public DateTime? HealthJoinedDate { get; set; }

        /// <summary>
        /// Ngày hết hạn BHYT
        /// </summary>
        public DateTime? HealthExpiredDate { get; set; }

        /// <summary>
        /// Loại hồ sơ
        /// </summary>
        public RecordType Type { get; set; }

        /// <summary>
        /// Created by, default system user
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Edited by, default system user
        /// </summary>
        public string EditedBy { get; set; }

        /// <summary>
        /// Edited date
        /// </summary>
        public DateTime EditedDate { get; set; }
    }
}
