using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework.Model.TimeSheet
{
    public class TimeSheetEmployeeGroupWorkShiftModel : BaseModel<hr_TimeSheetEmployeeGroupWorkShift>
    {
        private readonly hr_TimeSheetEmployeeGroupWorkShift _timeSheetUserGroupWorkShift;
        private readonly hr_Record _record;

        public TimeSheetEmployeeGroupWorkShiftModel()
        {
            //init object
            _timeSheetUserGroupWorkShift = new hr_TimeSheetEmployeeGroupWorkShift();

            // set props
            Init(_timeSheetUserGroupWorkShift);

            // get relation data
            _record = new hr_Record();
        }

        public TimeSheetEmployeeGroupWorkShiftModel(hr_TimeSheetEmployeeGroupWorkShift timeSheetUserGroupWorkShift)
        {
            //init object
            _timeSheetUserGroupWorkShift = timeSheetUserGroupWorkShift ?? new hr_TimeSheetEmployeeGroupWorkShift();

            // set props
            Init(_timeSheetUserGroupWorkShift);

            // get relation data
            _record = hr_RecordServices.GetById(_timeSheetUserGroupWorkShift.RecordId);
            _record = _record ?? new hr_Record();
        }

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

        #region Custom Properties

        /// <summary>
        /// Employee name
        /// </summary>
        public string FullName => _record.FullName;

        /// <summary>
        /// Employee code
        /// </summary>
        public string EmployeeCode => _record.EmployeeCode;

        /// <summary>
        /// Department ID
        /// </summary>
        public int DepartmentId => _record.DepartmentId;

        /// <summary>
        /// Department name
        /// </summary>
        public string DepartmentName => cat_DepartmentServices.GetFieldValueById(_record.DepartmentId);

        /// <summary>
        /// Group work shift name
        /// </summary>
        public string GroupWorkShiftName => hr_TimeSheetGroupWorkShiftServices.GetFieldValueById(_timeSheetUserGroupWorkShift.GroupWorkShiftId);

        /// <summary>
        /// Position name
        /// </summary>
        public string PositionName => cat_PositionServices.GetFieldValueById(_record.PositionId);

        #endregion
    }
}
