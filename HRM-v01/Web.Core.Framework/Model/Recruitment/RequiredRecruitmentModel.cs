using System;
using Web.Core.Object.Recruitment;
using Web.Core.Service.Catalog;
using Web.Core.Service.Recruitment;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for RequiredRecruitmentModel
    /// </summary>
    public class RequiredRecruitmentModel : BaseModel<rec_RequiredRecruitment>
    {
        private readonly rec_RequiredRecruitment _requiredRecruitment;
       
        public RequiredRecruitmentModel()
        {
            _requiredRecruitment = new rec_RequiredRecruitment();
            // set model default props
            Init(_requiredRecruitment);
        }

        public RequiredRecruitmentModel(rec_RequiredRecruitment requiredRecruitment)
        {
            // init entity
            _requiredRecruitment = requiredRecruitment ?? new rec_RequiredRecruitment();

            // set model props
            Init(_requiredRecruitment);
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

        #region custom props
        /// <summary>
        /// Tên đơn vị tuyển dụng
        /// </summary>
        public string RecruitmentDepartmentName =>
            cat_RecruitmentDepartmentServices.GetFieldValueById(_requiredRecruitment.DepartmentId);

        /// <summary>
        /// Tên chức vụ
        /// </summary>
        public string PositionName => cat_PositionServices.GetFieldValueById(_requiredRecruitment.PositionId);

        /// <summary>
        /// Vị trí TD
        /// </summary>
        public string JobTitlePositionName =>
            cat_JobTitleServices.GetFieldValueById(_requiredRecruitment.JobTitlePositionId);

        /// <summary>
        /// Tên trình độ
        /// </summary>
        public string EducationName => cat_EducationServices.GetFieldValueById(_requiredRecruitment.EducationId);

        /// <summary>
        /// Tên tình trạng
        /// </summary>
        public string StatusName => _requiredRecruitment.Status.Description();

        /// <summary>
        /// Count trúng tuyển
        /// </summary>
        public int PassedCount
        {
            get
            {
                if (_requiredRecruitment.Id > 0)
                {
                    var condition =
                        "[RequiredRecruitmentId] = {0} AND [IsDeleted] = 0 AND ([Status] = {1} OR [Status] = {2})"
                            .FormatWith(_requiredRecruitment.Id, (int)CandidateType.Passed,
                                (int)CandidateType.PassedNotWork);
                    var candidates = rec_CandidateServices.GetAll(condition);
                    return candidates.Count;
                }
                else
                {
                    return 0;
                }
            }
        } 
        
        /// <summary>
        /// Count ứng tuyển
        /// </summary>
        public int CandidateCount
        {
            get
            {
                if (_requiredRecruitment.Id > 0)
                {
                    var candidates = rec_CandidateServices.GetAll(
                        "[RequiredRecruitmentId] = {0} AND [IsDeleted] = 0 ".FormatWith(_requiredRecruitment.Id));
                    return candidates.Count;
                }
                else
                {
                    return 0;
                }
            }
        }

        #endregion
    }
}
