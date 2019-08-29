using System;
using System.Collections.Generic;
using System.ComponentModel;
using Web.Core.Catalog.Service;
using Web.Core.Object.Catalog;
using Web.Core.Object.HumanRecord;
using Web.Core.Object.Salary;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Salary;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for RecordModel
    /// </summary>
    public class RecordModel : BaseModel<hr_Record>
    {
        #region Ctors

        public RecordModel()
        {
            Init(new hr_Record());
        }

        public RecordModel(hr_Record record)
        {
            // init entity
            record = record ?? new hr_Record();

            // set model props
            Init(record);

            // init relation object
            var bank = hr_BankServices.GetBankByRecordId(record.Id) ?? new hr_Bank();
            var salaryDecision = sal_SalaryDecisionServices.GetCurrent(record.Id) ?? new sal_SalaryDecision();
            var quantum = cat_QuantumServices.GetById(salaryDecision.QuantumId) ?? new cat_Quantum();

            // set custom props
            DepartmentName = cat_DepartmentServices.GetFieldValueById(record.DepartmentId);
            ManagementDepartmentName = cat_DepartmentServices.GetFieldValueById(record.ManagementDepartmentId);
            MaritalStatusName = cat_MaritalStatusServices.GetFieldValueById(record.MaritalStatusId);
            BirthPlaceProvinceName = cat_LocationServices.GetFieldValueById(record.BirthPlaceProvinceId);
            BirthPlaceDistrictName = cat_LocationServices.GetFieldValueById(record.BirthPlaceDistrictId);
            BirthPlaceWardName = cat_LocationServices.GetFieldValueById(record.BirthPlaceWardId);
            HometownProvinceName = cat_LocationServices.GetFieldValueById(record.HometownProvinceId);
            HometownDistrictName = cat_LocationServices.GetFieldValueById(record.HometownDistrictId);
            HometownWardName = cat_LocationServices.GetFieldValueById(record.HometownWardId);
            FolkName = cat_FolkServices.GetFieldValueById(record.FolkId);
            ReligionName = cat_ReligionServices.GetFieldValueById(record.ReligionId);
            PersonalClassName = cat_PersonalClassServices.GetFieldValueById(record.PersonalClassId);
            FamilyClassName = cat_FamilyClassServices.GetFieldValueById(record.FamilyClassId);
            PositionName = cat_PositionServices.GetFieldValueById(record.PositionId);
            JobTitleName = cat_JobTitleServices.GetFieldValueById(record.JobTitleId);
            BasicEducationName = cat_BasicEducationServices.GetFieldValueById(record.BasicEducationId);
            EducationName = cat_EducationServices.GetFieldValueById(record.EducationId);
            PoliticLevelName = cat_PoliticLevelServices.GetFieldValueById(record.PoliticLevelId);
            ManagementLevelName = cat_ManagementLevelServices.GetFieldValueById(record.ManagementLevelId);
            LanguageLevelName = cat_LanguageLevelServices.GetFieldValueById(record.LanguageLevelId);
            ITLevelName = cat_ITLevelServices.GetFieldValueById(record.ITLevelId);
            CPVPositionName = cat_CPVPositionServices.GetFieldValueById(record.CPVPositionId);
            VYUPositionName = cat_VYUPositionServices.GetFieldValueById(record.VYUPositionId);
            ArmyLevelName = cat_ArmyLevelServices.GetFieldValueById(record.ArmyLevelId);
            HealthStatusName = cat_HealthStatusServices.GetFieldValueById(record.HealthStatusId);
            FamilyPolicyName = cat_FamilyPolicyServices.GetFieldValueById(record.FamilyPolicyId);
            IDIssuePlaceName = cat_IDIssuePlaceServices.GetFieldValueById(record.IDIssuePlaceId);
            WorkStatusName = cat_WorkStatusServices.GetFieldValueById(record.WorkStatusId);
            EmployeeTypeName = cat_EmployeeTypeServices.GetFieldValueById(record.EmployeeTypeId);
            IndustryName = cat_IndustryServices.GetFieldValueById(record.IndustryId);
            GovernmentPositionName = cat_PositionServices.GetFieldValueById(record.GovernmentPositionId);
            PluralityPositionName = cat_PositionServices.GetFieldValueById(record.PluralityPositionId);
            BankName = cat_BankServices.GetFieldValueById(bank.BankId);
            AccountNumber = bank.AccountNumber;
            AccountName = bank.AccountName;
            QuantumName = quantum.Name;
            QuantumCode = quantum.Code;
            SalaryFactor = salaryDecision.Factor;
            EffectiveDate = salaryDecision.EffectiveDate;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        [Description("Danh tính phòng ban")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// Mã đơn vị quản lý
        /// </summary>
        [Description("Danh tính phòng ban quản lý")]
        public int ManagementDepartmentId { get; set; }

        /// <summary>
        /// mã nhân viên
        /// </summary>
        [Description("Mã nhân viên")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// mã cán bộ công chức
        /// </summary>
        [Description("Mã chức năng")]
        public string FunctionaryCode { get; set; }

        /// <summary>
        /// tên nhân viên
        /// </summary>
        [Description("Tên")]
        public string Name { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        [Description("Họ tên")]
        public string FullName { get; set; }


        // bí danh
        [Description("Tên khác")]
        public string Alias { get; set; } 
        
        // ngày sinh
        [Description("Ngày sinh")]
        public DateTime? BirthDate { get; set; } 
        
        // giới tính
        [Description("Giới tính")]
        public bool Sex { get; set; } 


        // tình trạng hôn nhân
        [Description("Tình trạng hôn nhân")]
        public int MaritalStatusId { get; set; }  

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
        [Description("Thành phần cá nhân")]
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
        [Description("Ngày tuyển dụng")]
        public DateTime? RecruimentDate { get; set; } //NGAY_TUYEN_DTIEN

        // ngày vào chính thức
        [Description("Ngày tham gia")]
        public DateTime? ParticipationDate { get; set; } //NGAY_TUYEN_CHINHTHUC

        // ngày biên chế
        [Description("Ngày biên chế")]
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
        [Description("Chính trị")]
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
        [Description("Ngày vào đảng")]
        public DateTime? CPVJoinedDate { get; set; } //NGAYVAO_DANG

        // ngày vào đảng chính thức
        [Description("Ngày vào chính thức")]
        public DateTime? CPVOfficialJoinedDate { get; set; } //NGAY_CTHUC_DANG

        // thẻ đảng
        [Description("Số thẻ đảng")]
        public string CPVCardNumber { get; set; } //SOTHE_DANG

        // chức vụ đảng
        [Description("Chức vụ đảng")]
        public int CPVPositionId { get; set; } //MA_CHUCVU_DANG

        // nơi kết nạp đảng
        [Description("Nơi kết nạp đảng")]
        public string CPVJoinedPlace { get; set; } //NOI_KETNAP_DANG

        // ngày vào đoàn (Vietnam Youth Union)
        [Description("Ngày vào đoàn")]
        public DateTime? VYUJoinedDate { get; set; } //NGAYVAO_DOAN

        // chức vụ đoàn
        [Description("Chức vụ đoàn")]
        public int VYUPositionId { get; set; } //MA_CHUCVU_DOAN
        // nơi kết nạp đoàn
        [Description("Nơi kết nạp đoàn")]
        public string VYUJoinedPlace { get; set; } //NOI_KETNAP_DOAN

        // ngày vào quân đội
        [Description("Ngày nhập ngũ")]
        public DateTime? ArmyJoinedDate { get; set; } //NGAYVAO_QDOI

        // ngày ra quân đội
        [Description("Ngày xuất ngũ")]
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
        [Description("Ngày cấp")]
        public DateTime? IDIssueDate { get; set; } //NGAYCAP_CMND

        // nơi cấp
        [Description("Nơi cấp")]
        public int IDIssuePlaceId { get; set; } //MA_NOICAP_CMND

        // số bảo hiểm xã hội
        [Description("Số sổ BHXH")]
        public string InsuranceNumber { get; set; } //SOTHE_BHXH

        // ngày cấp bảo hiểm xã hội
        [Description("Ngày cấp sổ")]
        public DateTime? InsuranceIssueDate { get; set; } //NGAYCAP_BHXH

        // mã số thuế cá nhân
        [Description("Mã thuế cá nhân")]
        public string PersonalTaxCode { get; set; } //MST_CANHAN

        // trạng thái làm việc
        [Description("Trạng thái công việc")]
        public int WorkStatusId { get; set; } //TrangThai

        // trạng thái hồ sơ
        [Description("Trạng thái hồ sơ")]
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
        /// Ngay trang thai
        /// </summary>
        [Description("Ngày trạng thái")]
        public DateTime? WorkStatusDate { get; set; }

        /// <summary>
        /// Ma ly do trang thai
        /// </summary>
        [Description("Lý do trạng thái")]
        public string WorkStatusReason { get; set; }

        /// <summary>
        /// id loai can bo
        /// </summary>
        [Description("Loại cán bộ")]
        public int EmployeeTypeId { get; set; }

        /// <summary>
        /// id chuyên ngành
        /// </summary>
        [Description("Ngành")]
        public int IndustryId { get; set; }

        /// <summary>
        /// thu nhập gia đinh
        /// </summary>
        public decimal FamilyIncome { get; set; }

        /// <summary>
        /// các nguồn khác
        /// </summary>
        public string OtherIncome { get; set; }

        /// <summary>
        /// Nhà được cấp, được thuê, loại nhà
        /// </summary>
        public string AllocatedHouse { get; set; }

        /// <summary>
        /// Diện tích nhà được cấp, được thuê
        /// </summary>
        public decimal AllocatedHouseArea { get; set; }

        /// <summary>
        /// Nhà tự mua, tự xây, loại nhà
        /// </summary>
        public string House { get; set; }

        /// <summary>
        /// Diện tích nhà tự mua, tự xây
        /// </summary>
        public decimal HouseArea { get; set; }

        /// <summary>
        /// Diện tích đất được cấp, cho thuê
        /// </summary>
        public decimal AllocatedLandArea { get; set; }

        /// <summary>
        /// Đất sản xuất kinh doanh
        /// </summary>
        public decimal BusinessLandArea { get; set; }

        /// <summary>
        /// Diện tích đất tự mua
        /// </summary>
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
        public DateTime? RevolutionJoinedDate { get; set; }

        /// <summary>
        /// Công việc đã làm lâu nhất
        /// </summary>
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

        #endregion

        #region Custom props

        /// <summary>
        /// Đơn vị công tác
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Đơn vị quản lý
        /// </summary>
        public string ManagementDepartmentName { get; set; }
        
        /// <summary>
        /// Ngày sinh theo ngày Việt Nam
        /// </summary>
        public string BirthDateVn => BirthDate != null ? BirthDate.Value.ToVnDate() : "";

        /// <summary>
        /// Tuổi
        /// </summary>
        public string Age => BirthDate != null ? (DateTime.Now.Year - BirthDate.Value.Year).ToString() : ""; 
        

        /// <summary>
        /// Tên giới tính
        /// </summary>
        public string SexName => Sex ? "Nam" : "Nữ";

        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public string MaritalStatusName { get; set; }

        /// <summary>
        /// Tên nơi sinh xã
        /// </summary>
        public string BirthPlaceWardName { get; set; }

        /// <summary>
        /// Tên nơi sinh huyện
        /// </summary>
        public string BirthPlaceDistrictName { get; set; }

        /// <summary>
        /// Tên nơi sinh tỉnh
        /// </summary>
        public string BirthPlaceProvinceName { get; set; }

        /// <summary>
        /// Địa chỉ nơi sinh
        /// </summary>
        public string BirthPlaceAddress =>
            new AddressModel(BirthPlace, BirthPlaceWardName, BirthPlaceDistrictName, BirthPlaceProvinceName).Address;

        /// <summary>
        /// Tên quê quán xã
        /// </summary>
        public string HometownWardName { get; set; }

        /// <summary>
        /// Tên quê quán huyện
        /// </summary>
        public string HometownDistrictName { get; set; }

        /// <summary>
        /// Tên quê quán tỉnh
        /// </summary>
        public string HometownProvinceName { get; set; }

        /// <summary>
        /// Quê quán
        /// </summary>
        public string HometownAddress =>
            new AddressModel(Hometown, HometownWardName, HometownDistrictName, HometownProvinceName).Address;

        /// <summary>
        /// Tên dân tộc
        /// </summary>
        public string FolkName { get; set; }

        /// <summary>
        /// Tên tôn giáo
        /// </summary>
        public string ReligionName { get; set; }

        /// <summary>
        /// Tên thành phần bản thân
        /// </summary>
        public string PersonalClassName { get; set; }

        /// <summary>
        /// Tên thành phần gia đình
        /// </summary>
        public string FamilyClassName { get; set; }

        /// <summary>
        /// Ngày tuyển dụng
        /// </summary>
        public string RecruimentDateVn => RecruimentDate != null ? RecruimentDate.Value.ToVnDate() : "";

        /// <summary>
        /// Ngày vào chính thức
        /// </summary>
        public string ParticipationDateVn => ParticipationDate != null ? ParticipationDate.Value.ToVnDate() : "";

        /// <summary>
        /// Ngày biên chế
        /// </summary>
        public string FunctionaryDateVn => FunctionaryDate != null ? FunctionaryDate.Value.ToVnDate() : "";

        /// <summary>
        /// Tên chức vụ
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Tên chức danh
        /// </summary>
        public string JobTitleName { get; set; }

        /// <summary>
        /// Tên trình độ văn hóa
        /// </summary>
        public string BasicEducationName { get; set; }

        /// <summary>
        /// Tên trình độ chuyên môn cao nhất
        /// </summary>
        public string EducationName { get; set; }

        /// <summary>
        /// Tên trình độ lý luận chính trị
        /// </summary>
        public string PoliticLevelName { get; set; }

        /// <summary>
        /// Tên trình độ quản lý nhà nước
        /// </summary>
        public string ManagementLevelName { get; set; }

        /// <summary>
        /// Tên trình độ ngoại ngữ
        /// </summary>
        public string LanguageLevelName { get; set; }

        /// <summary>
        /// Tên trình độ tin học
        /// </summary>
        public string ITLevelName { get; set; }

        /// <summary>
        /// Tên chức vụ Đảng
        /// </summary>
        public string CPVPositionName { get; set; }

        /// <summary>
        /// Ngày vào đoàn (Vietnam Youth Union)
        /// </summary>
        public string VYUJoinedDateVn => VYUJoinedDate != null ? VYUJoinedDate.Value.ToVnDate() : "";

        /// <summary>
        /// Chức vụ đoàn
        /// </summary>
        public string VYUPositionName { get; set; }

        /// <summary>
        /// Ngày nhập ngũ
        /// </summary>
        public string ArmyJoinedDateVn => ArmyJoinedDate != null ? ArmyJoinedDate.Value.ToVnDate() : "";

        /// <summary>
        /// Ngày xuất ngũ
        /// </summary>
        public string ArmyLeftDateVn => ArmyLeftDate != null ? ArmyLeftDate.Value.ToVnDate() : "";

        /// <summary>
        /// Tên cấp bậc quân đội
        /// </summary>
        public string ArmyLevelName { get; set; }

        /// <summary>
        /// Tên tình trạng sức khỏe
        /// </summary>
        public string HealthStatusName { get; set; }

        /// <summary>
        /// Gia đình chính sách
        /// </summary>
        public string FamilyPolicyName { get; set; }

        /// <summary>
        /// Ngày cấp CMND
        /// </summary>
        public string IDIssueDateVn => IDIssueDate != null ? IDIssueDate.Value.ToVnDate() : "";

        /// <summary>
        /// Tên nơi cấp CMND
        /// </summary>
        public string IDIssuePlaceName { get; set; }

        /// <summary>
        /// Ngày cấp bảo hiểm xã hội
        /// </summary>
        public string InsuranceIssueDateVn => InsuranceIssueDate != null ? InsuranceIssueDate.Value.ToVnDate() : "";

        /// <summary>
        /// Ngày tham gia BHYT
        /// </summary>
        public string HealthJoinedDateVn => HealthJoinedDate != null ? HealthJoinedDate.Value.ToVnDate() : "";

        /// <summary>
        /// Ngày hết hạn BHYT
        /// </summary>
        public string HealthExpiredDateVn => HealthExpiredDate != null ? HealthExpiredDate.Value.ToVnDate() : "";

        /// <summary>
        /// Trạng thái làm việc
        /// </summary>
        public string WorkStatusName { get; set; }

        /// <summary>
        /// Ngày cập nhật trạng thái làm việc
        /// </summary>
        public string WorkStatusDateVn => WorkStatusDate != null ? WorkStatusDate.Value.ToVnDate() : "";

        /// <summary>
        /// Têm loại cán bộ
        /// </summary>
        public string EmployeeTypeName { get; set; }

        /// <summary>
        /// Tên chủ tài khoản
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Tên ngạch
        /// </summary>
        public string QuantumName { get; set; }

        /// <summary>
        /// Mã ngạch
        /// </summary>
        public string QuantumCode { get; set; }

        /// <summary>
        /// Hệ số lương
        /// </summary>
        public decimal SalaryFactor { get; set; }

        /// <summary>
        /// Ngày hưởng
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// Tên chuyên ngành
        /// </summary>
        public string IndustryName { get; set; }

        /// <summary>
        /// Tên chức vụ chính quyền
        /// </summary>
        public string GovernmentPositionName { get; set; }

        /// <summary>
        /// Tên chức vụ kiêm nhiệm
        /// </summary>
        public string PluralityPositionName { get; set; }

        /// <summary>
        /// Quá trình học tập
        /// </summary>
        public List<EducationHistoryModel> EducationHistories { get; set; }
        
        /// <summary>
        /// Quá trình công tác trước khi vào đơn vị
        /// </summary>
        public List<WorkHistoryModel> WorkHistories { get; set; }

        /// <summary>
        /// Quan hệ gia đình
        /// </summary>
        public List<FamilyRelationshipModel> FamilyRelationships { get; set; }

        /// <summary>
        /// Diễn biến lương
        /// </summary>
        public List<SalaryDecisionModel> Salaries { get; set; }

        /// <summary>
        /// Quá trình đào tạo
        /// </summary>
        public List<TrainingHistoryModel> TrainningHistories { get; set; }

        /// <summary>
        /// Khả năng
        /// </summary>
        public List<AbilityModel> Abilities { get; set; }

        /// <summary>
        /// Quá trình tham gia bảo hiểm
        /// </summary>
        public List<InsuranceModel> Insurances { get; set; }

        /// <summary>
        /// Khen thưởng
        /// </summary>
        public List<RewardModel> Rewards { get; set; }

        /// <summary>
        /// Kỷ luật
        /// </summary>
        public List<DisciplineModel> Disciplines { get; set; }

        /// <summary>
        /// Quá trình công tác tại đơn vị
        /// </summary>
        public List<WorkProcessModel> WorkProcess { get; set; }

        /// <summary>
        /// Hợp đồng
        /// </summary>
        public List<ContractModel> Contracts { get; set; }

        #endregion
    }
}

