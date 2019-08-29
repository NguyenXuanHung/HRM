using System;
using Web.Core.Object.TimeSheet;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for TimeSheetGroupWorkShiftModel
    /// </summary>
    public class TimeSheetGroupWorkShiftModel : BaseModel<hr_TimeSheetGroupWorkShift>
    {
        public TimeSheetGroupWorkShiftModel()
        {
            // set model default props
            Init(new hr_TimeSheetGroupWorkShift());
        }

        public TimeSheetGroupWorkShiftModel(hr_TimeSheetGroupWorkShift timeSheetGroupWorkShift)
        {
            // init entity
            timeSheetGroupWorkShift = timeSheetGroupWorkShift ?? new hr_TimeSheetGroupWorkShift();

            // set model props
            Init(timeSheetGroupWorkShift);
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Deleted status
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// StartDate
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// EndDate
        /// </summary>
        public DateTime EndDate { get; set; }

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
