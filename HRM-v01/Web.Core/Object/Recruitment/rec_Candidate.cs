using System;

namespace Web.Core.Object
{
    public class rec_Candidate : BaseEntity
    {
        public rec_Candidate()
        {
            RecordId = 0;
            Code = "";
            RequiredRecruitmentId = 0;
            DesiredSalary = 0;
            Status = CandidateType.Interview;
            IsDeleted = false;
            CreatedDate = DateTime.Now;
            CreatedBy = "admin";
            EditedDate = DateTime.Now;
            EditedBy = "admin";
        }

        /// <summary>
        /// id hồ sơ
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// Mã ứng viên
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// id yêu cầu tuyển dụng
        /// </summary>
        public int RequiredRecruitmentId { get; set; }

        /// <summary>
        /// Mức lương mong muốn
        /// </summary>
        public decimal DesiredSalary { get; set; }

        /// <summary>
        /// Ngày nộp hồ sơ
        /// </summary>
        public DateTime? ApplyDate { get; set; }

        /// <summary>
        /// Trạng thái hồ sơ ứng viên
        /// </summary>
        public CandidateType Status { get; set; }

        /// <summary>
        /// Xóa
        /// </summary>
        public bool IsDeleted { get; set; }

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedDate { get; set; }
    }
}
