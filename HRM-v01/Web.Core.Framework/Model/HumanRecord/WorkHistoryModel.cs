using System;
using Web.Core.Object.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for WorkHistoryModel
    /// </summary>
    public class WorkHistoryModel
    {
        private readonly hr_WorkHistory _workHistory;

        public WorkHistoryModel(hr_WorkHistory workHistory)
        {
            _workHistory = workHistory ?? new hr_WorkHistory();
            RecordId = _workHistory.RecordId;
            WorkPlace = _workHistory.WorkPlace;
            WorkPosition = _workHistory.WorkPosition;
            ReasonLeave = _workHistory.ReasonLeave;
            ExperienceWork = _workHistory.ExperienceWork;
            IsApproved = _workHistory.IsApproved;
            Note = _workHistory.Note;
            SalaryLevel = _workHistory.SalaryLevel;
            AddressCompany = _workHistory.AddressCompany;
            FromDate = _workHistory.FromDate;
            ToDate = _workHistory.ToDate;
            Id = _workHistory.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WorkPlace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WorkPosition { get; set; }
        
        /// <summary>
        /// Lý do thôi việc
        /// </summary>
        public string ReasonLeave { get; set; }
        
        /// <summary>
        /// Từ ngày
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Đến ngày
        /// </summary>
        public DateTime? ToDate { get; set; }
        
        /// <summary>
        /// Kinh nghiệm đạt được
        /// </summary>
        public string ExperienceWork { get; set; }
        
        /// <summary>
        /// Duyệt
        /// </summary>
        public bool IsApproved { get; set; }
        
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Mức lương
        /// </summary>
        public decimal SalaryLevel { get; set; }
        
        /// <summary>
        /// Địa chỉ công ty
        /// </summary>
        public string AddressCompany { get; set; }
    }
}
