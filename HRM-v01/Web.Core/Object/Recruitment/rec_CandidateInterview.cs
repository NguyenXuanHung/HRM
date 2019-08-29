using System;
using System.ComponentModel;

namespace Web.Core.Object.Recruitment
{
    public class rec_CandidateInterview: BaseEntity
    {
        public rec_CandidateInterview()
        {
            InterviewId = 0;
            IsDeleted = false;
            CreatedDate = DateTime.Now;
            CreatedBy = "admin";
            EditedDate = DateTime.Now;
            EditedBy = "admin";
        }

        /// <summary>
        /// Id PV
        /// </summary>
        public int InterviewId { get; set; }

        /// <summary>
        /// Id hồ sơ
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// Giờ PV
        /// </summary>
        [Description("Giờ phỏng vấn")]
        public DateTime TimeInterview { get; set; }

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
