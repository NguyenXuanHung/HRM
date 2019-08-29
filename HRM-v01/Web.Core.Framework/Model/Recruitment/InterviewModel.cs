using System;
using Web.Core.Object.Recruitment;
using Web.Core.Service.Recruitment;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for InterviewModel
    /// </summary>
    public class InterviewModel : BaseModel<rec_Interview>
    {
        private readonly rec_Interview _interview;
        private readonly rec_RequiredRecruitment _requiredRecruitment;
       
        public InterviewModel()
        {
            _interview = new rec_Interview();
            _requiredRecruitment = new rec_RequiredRecruitment();
            // set model default props
            Init(_interview);
        }

        public InterviewModel(rec_Interview interview)
        {
            // init entity
            _interview = interview ?? new rec_Interview();

            // set model props
            Init(_interview);

            //get data relation
            _requiredRecruitment = rec_RequiredRecruitmentServices.GetById(_interview.RequiredRecruitmentId);
            _requiredRecruitment = _requiredRecruitment ?? new rec_RequiredRecruitment();
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

        #region custom props
        /// <summary>
        /// Tên yêu cầu tuyển dụng
        /// </summary>
        public string RequiredRecruitmentName => _requiredRecruitment.Name;

        /// <summary>
        /// Thời gian
        /// </summary>
        public string TimeDisplay =>
            _interview.FromTime.ToString("HH:mm") + " - " + _interview.ToTime.ToString("HH:mm");

        /// <summary>
        /// date display
        /// </summary>
        public string InterviewVnDate => _interview.InterviewDate.ToVnDate();
        
        /// <summary>
        /// Số lượng ứng viên
        /// </summary>
        public int CandidateCount
        {
            get
            {
                var candidates = rec_CandidateInterviewServices.GetAll(
                    "[InterviewId] = {0} AND [IsDeleted] = 0 ".FormatWith(_interview.Id));
                return candidates.Count;
            }
        }
        #endregion
    }
}
