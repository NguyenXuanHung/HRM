using System;

namespace Web.Core.Object.HumanRecord
{
    public class hr_Certificate : BaseEntity//HOSO_UNGVIEN_CHUNGCHI
    {
        /// <summary>
        /// id hồ sơ
        /// </summary>        
        public int RecordId { get; set; } //FR_KEY_HOSO
        /// <summary>
        /// id chung chi
        /// </summary>
        public int CertificationId { get; set; }//MaChungChi
        /// <summary>
        /// id xep loai
        /// </summary>
        public int GraduationTypeId { get; set; }//MA_XEPLOAI
        /// <summary>
        /// Noi dao tao
        /// </summary>
       public string EducationPlace { get; set; }//NoiDaoTao
        /// <summary>
        /// Ngay cap
        /// </summary>
        public DateTime? IssueDate { get; set; }//NgayCap
        /// <summary>
        /// Ngay het han
        /// </summary>
        public DateTime? ExpirationDate { get; set; }//NgayHetHan
        /// <summary>
        /// Duyet
        /// </summary>
        public bool IsApproved { get; set; }//Duyet
        /// <summary>
        /// Ghi chu
        /// </summary>
        public string Note { get; set; }//GhiChu

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
