using System;
using Web.Core.Object.TimeSheet;

namespace Web.Core.Framework
{
    public class AnnualLeaveHistoryModel : BaseModel<hr_AnnualLeaveHistory>
    {
        private readonly hr_AnnualLeaveHistory _annualLeaveHistory;

        public AnnualLeaveHistoryModel()
        {
            _annualLeaveHistory = new hr_AnnualLeaveHistory();
            Init(_annualLeaveHistory);
        }

        public AnnualLeaveHistoryModel(hr_AnnualLeaveHistory annualLeaveHistory)
        {
            _annualLeaveHistory = annualLeaveHistory ?? new hr_AnnualLeaveHistory();
            Init(_annualLeaveHistory);
        }

        /// <summary>
        /// Id hồ sơ
        /// </summary>
        public int RecordId { get; set; }
        /// <summary>
        /// Id event
        /// </summary>
        public int TimeSheetEventId { get; set; }

        /// <summary>
        /// Ngày sử dụng
        /// </summary>
        public DateTime UsedLeaveDate { get; set; }

        /// <summary>
        /// Số ngày phép đã sử dụng
        /// </summary>
        public double UsedLeaveDay { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EditedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EditedDate { get; set; }
    }
}
