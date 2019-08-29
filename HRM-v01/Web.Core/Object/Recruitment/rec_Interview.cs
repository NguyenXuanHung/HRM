using System;

namespace Web.Core.Object.Recruitment
{
    public class rec_Interview: BaseEntity
    {
        public rec_Interview()
        {
            RequiredRecruitmentId = 0;
            IsDeleted = false;
            CreatedDate = DateTime.Now;
            CreatedBy = "admin";
            EditedDate = DateTime.Now;
            EditedBy = "admin";
        }

        /// <summary>
        /// id yêu cầu tuyển dụng
        /// </summary>
        public int RequiredRecruitmentId { get; set; }

        /// <summary>
        /// Tên lịch phỏng vấn
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ngày PV
        /// </summary>
        public DateTime InterviewDate { get; set; }

        /// <summary>
        /// Giờ bắt đầu pv
        /// </summary>
        public DateTime FromTime { get; set; }

        /// <summary>
        /// Giờ kết thúc pv
        /// </summary>
        public DateTime ToTime { get; set; }

        /// <summary>
        /// Người pv
        /// </summary>
        public string Interviewer { get; set; }

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
