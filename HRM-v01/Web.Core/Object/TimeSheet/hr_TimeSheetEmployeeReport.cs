using System;

namespace Web.Core.Object.TimeSheet
{
    /// <summary>
    /// bảng phân ca cho nhân viên
    /// </summary>
    public class hr_TimeSheetEmployeeReport : BaseEntity
    {
        public hr_TimeSheetEmployeeReport()
        {
            CreatedBy = "system";
            CreatedDate = DateTime.Now;
            EditedBy = "system";
            EditedDate = DateTime.Now;
        }

        /// <summary>
        /// Record ID
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// Report ID
        /// </summary>
        public int ReportId { get; set; }

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
