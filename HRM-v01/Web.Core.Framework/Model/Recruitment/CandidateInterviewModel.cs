using System;
using System.ComponentModel;
using Web.Core.Object;
using Web.Core.Object.HumanRecord;
using Web.Core.Object.Recruitment;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Recruitment;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for CandidateInterviewModel
    /// </summary>
    public class CandidateInterviewModel : BaseModel<rec_CandidateInterview>
    {
        private readonly rec_CandidateInterview _candidateInterview;
        private readonly rec_Interview _interview;
        private readonly hr_Record _record;
        private readonly rec_Candidate _candidate;
        public CandidateInterviewModel()
        {
            _candidateInterview = new rec_CandidateInterview();
            _interview = new rec_Interview();
            _record = new hr_Record();
            _candidate = new rec_Candidate();
            // set model default props
            Init(_candidateInterview);
        }

        public CandidateInterviewModel(rec_CandidateInterview interview)
        {
            // init entity
            _candidateInterview = interview ?? new rec_CandidateInterview();

            // set model props
            Init(_candidateInterview);

            //get data relation
            _interview = rec_InterviewServices.GetById(_candidateInterview.InterviewId);
            _interview = _interview ?? new rec_Interview();
            _record = hr_RecordServices.GetById(_candidateInterview.RecordId) ?? new hr_Record();
            _candidate =
                rec_CandidateServices.GetByCondition("[RecordId] = {0}".FormatWith(_candidateInterview.RecordId));
            _candidate = _candidate ?? new rec_Candidate();
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

        #region custom props
        /// <summary>
        /// Tên lịch phỏng vấn
        /// </summary>
        public string InterviewName => _interview.Name;

        /// <summary>
        /// date display
        /// </summary>
        public string InterviewVnDate => _interview.InterviewDate.ToVnDate();

        /// <summary>
        /// Tên đầy đủ
        /// </summary>
        public string FullName => _record.FullName;
        
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? BirthDate => _record.BirthDate;

        /// <summary>
        /// Thời gian phỏng vấn
        /// </summary>
        public string TimeInterviewDisplay => _candidateInterview.TimeInterview.ToString("HH:mm");

        /// <summary>
        /// Mã ứng viên
        /// </summary>
        public string CandidateCode => _candidate.Code;

        #endregion
    }
}
