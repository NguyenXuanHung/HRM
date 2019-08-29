using System;

namespace Web.Core.Object.HumanRecord
{
    public class hr_EducationHistory : BaseEntity //HOSO_BANGCAP_UNGVIEN
    {
        // id hồ sơ
        public int RecordId { get; set; } //FR_KEY
        // từ ngày
        public DateTime? FromDate { get; set; } //TU_NGAY
        // đến ngày
        public DateTime? ToDate { get; set; } //DEN_NGAY
        // khoa
        public string Faculty { get; set; } //KHOA
        // ngày đào tạo
        public int IndustryId { get; set; } //MA_CHUYENNGANH
        // trình độ chuyên môn
        public int EducationId { get; set; } //MA_TRINHDO
        // hệ đào tạo
        public int TrainingSystemId { get; set; } //MA_HT_DAOTAO
        // trường đào tạo
        public int UniversityId { get; set; } //MA_TRUONG_DAOTAO
        // nước đào tạo
        public int NationId { get; set; }
        // đã tốt nghiệp
        public bool IsGraduated { get; set; } //DA_TN
        // loại tốt nghiệp
        public int GraduationTypeId { get; set; } //MA_XEPLOAI
        // đã duyệt
        public bool IsApproved { get; set; } //Duyet
        // số quyết định
        public string DecisionNumber { get; set; } //SO_QD
        // ngày quyết định
        public DateTime? DecisionDate { get; set; } //NGAY_QD
        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
}
}
