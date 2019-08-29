using System;

namespace Web.Core.Object.TimeSheet
{
    /// <summary>
    /// User & Group Work Shift relation
    /// </summary>
    public class hr_TimeSheetEmployeeGroupWorkShift : BaseEntity
    {
        /// <summary>
        /// Record
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// Group work shift
        /// </summary>
        public int GroupWorkShiftId { get; set; }

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
