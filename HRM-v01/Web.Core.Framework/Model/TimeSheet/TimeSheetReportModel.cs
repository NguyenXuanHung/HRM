using System;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.Catalog;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for TimeSheetReportModel
    /// </summary>
    public class TimeSheetReportModel : BaseModel<hr_TimeSheetReport>
    {
        private readonly hr_TimeSheetReport _timeSheetReport;

        public TimeSheetReportModel()
        {
            _timeSheetReport = new hr_TimeSheetReport();
            // set model props
            Init(_timeSheetReport);
        }

        public TimeSheetReportModel(hr_TimeSheetReport timeSheetReport)
        {
            // init entity
            _timeSheetReport = timeSheetReport ?? new hr_TimeSheetReport();

            // set model props
            Init(_timeSheetReport);
        }

        /// <summary>
        /// Report name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Trạng thái lock/unlock
        /// </summary>
        public TimeSheetStatus Status { get; set; }

        /// <summary>
        /// Ngày bắt đầu
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Deleted status
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// System, created user
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// System, created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// System, edited user
        /// </summary>
        public string EditedBy { get; set; }

        /// <summary>
        /// System, edited date
        /// </summary>
        public DateTime EditedDate { get; set; }
        
    }
}
