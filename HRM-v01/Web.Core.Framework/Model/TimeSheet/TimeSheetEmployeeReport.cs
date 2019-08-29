using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework.Model.TimeSheet
{
    public class TimeSheetEmployeeReportModel : BaseModel<hr_TimeSheetEmployeeReport>
    {
        private readonly hr_TimeSheetEmployeeReport _timeSheetEmployeeReport;
        private readonly hr_Record _record;

        public TimeSheetEmployeeReportModel()
        {
            //init object
            _timeSheetEmployeeReport = new hr_TimeSheetEmployeeReport();

            // set props
            Init(_timeSheetEmployeeReport);

            // get relation data
            _record = new hr_Record();
        }

        public TimeSheetEmployeeReportModel(hr_TimeSheetEmployeeReport timeSheetEmployeeReport)
        {
            //init object
            _timeSheetEmployeeReport = timeSheetEmployeeReport ?? new hr_TimeSheetEmployeeReport();

            // set props
            Init(_timeSheetEmployeeReport);

            // get relation data
            _record = hr_RecordServices.GetById(_timeSheetEmployeeReport.RecordId);
            _record = _record ?? new hr_Record();
        }

        /// <summary>
        /// Record
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// Group work shift
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
        public string ReportName => hr_TimeSheetReportServices.GetFieldValueById(_timeSheetEmployeeReport.ReportId);

        #endregion
    }
}
