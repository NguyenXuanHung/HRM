using System;

namespace Web.Core.Object.Recruitment
{
    public class rec_RequiredRecruitment : BaseEntity
    {
        //init constructor
        public rec_RequiredRecruitment()
        {
            JobTitlePositionId = 0;
            PositionId = 0;
            DepartmentId = 0;
            Quantity = 0;
            WorkFormId = WorkingFormType.FullTime;
            SalaryLevelFrom = 0;
            SalaryLevelTo = 0;
            AgeFrom = 0;
            AgeTo = 0;
            EducationId = 0;
            ExperienceId = ExperienceType.None;
            Height = 0;
            Weight = 0;
            Status = RecruitmentStatus.Pending;
            Sex = SexType.Unknown;
            IsDeleted = false;
            CreatedDate = DateTime.Now;
            CreatedBy = "admin";
            EditedDate = DateTime.Now;
            EditedBy = "admin";
        }

        /// <summary>
        /// Mã tuyển dụng
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Tên TD
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// id vị trí tuyển dụng
        /// </summary>
        public int JobTitlePositionId { get; set; }

        /// <summary>
        /// Chức vụ TD
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// Đơn vị TD
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Hạn nộp hồ sơ
        /// </summary>
        public DateTime ExpiredDate { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Nơi làm việc
        /// </summary>
        public string WorkPlace { get; set; }

        /// <summary>
        /// hình thức làm việc
        /// </summary>
        public WorkingFormType WorkFormId { get; set; }

        /// <summary>
        /// Mức lương từ
        /// </summary>
        public decimal? SalaryLevelFrom { get; set; }
        
        /// <summary>
        /// Mức lương đến
        /// </summary>
        public decimal? SalaryLevelTo { get; set; }

        /// <summary>
        /// Tuổi từ
        /// </summary>
        public int AgeFrom { get; set; }
        
        /// <summary>
        /// Tuổi đến
        /// </summary>
        public int AgeTo { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public SexType Sex { get; set; }

        /// <summary>
        /// Trình độ
        /// </summary>
        public int EducationId { get; set; }

        /// <summary>
        /// Kinh nghiệm
        /// </summary>
        public ExperienceType ExperienceId { get; set; }

        /// <summary>
        /// Chiều cao
        /// </summary>
        public decimal? Height { get; set; }

        /// <summary>
        /// cân nặng
        /// </summary>
        public decimal? Weight { get; set; }

        /// <summary>
        /// Mô tả công việc
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Yêu cầu tuyển dụng
        /// </summary>
        public string Requirement { get; set; }

        /// <summary>
        /// Lý do tuyển dụng
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Người duyệt
        /// </summary>
        public string SignerApprove { get; set; }

        /// <summary>
        /// Trạng thái yêu cầu TD
        /// </summary>
        public RecruitmentStatus Status { get; set; }

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
