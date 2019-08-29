using System;
using Web.Core.Object.TimeSheet;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for TimeSheetModel
    /// </summary>
    public class TimeSheetModel: BaseModel<hr_TimeSheet>
    {
        private readonly hr_TimeSheet _timeSheet;

        public TimeSheetModel()
        {
            _timeSheet = new hr_TimeSheet();
            Init(_timeSheet);
        }

        public TimeSheetModel(hr_TimeSheet timeSheet)
        {
            _timeSheet = timeSheet ?? new hr_TimeSheet();
            Init(_timeSheet);
        }

        /// <summary>
        /// id hồ sơ
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// Ngày bắt đầu làm việc
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Ngày kết thúc làm việc
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Thoi gian log
        /// </summary>
        public string TimeLogs { get; set; }

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedDate { get; set; }
    }
}
