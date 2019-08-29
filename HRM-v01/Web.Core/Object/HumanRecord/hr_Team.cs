using System;

namespace Web.Core.Object.HumanRecord
{
    /// <summary>
    /// Hồ sơ - Tổ, đội, công trình (Thông tin công việc)
    /// </summary>
    public class hr_Team : BaseEntity
    {
        /// <summary>
        /// id ho so
        /// </summary>
        public int RecordId { get; set; }
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
        public int WorkingFormId { get; set; }
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
        public string UnionJoinedPosition { get; set; }

        /// <summary>
        /// Số thẻ BHYT
        /// </summary>
        public  string HealthInsuranceNumber { get; set; }

        /// <summary>
        /// Ngày đóng BHYT
        /// </summary>
        public DateTime? HealthJoinedDate { get; set; }

        /// <summary>
        /// Ngày hết hạn BHYT
        /// </summary>
        public DateTime? HealthExpiredDate { get; set; }

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
