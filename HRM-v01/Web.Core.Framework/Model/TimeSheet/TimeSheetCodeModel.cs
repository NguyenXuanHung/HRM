using System;
using System.ComponentModel;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for TimeSheetCodeModel
    /// </summary>
    public class TimeSheetCodeModel : BaseModel<hr_TimeSheetCode>
    {
        private readonly hr_TimeSheetCode _timeSheetCode;
        private readonly hr_TimeSheetMachine _timeSheetMachine;
        public TimeSheetCodeModel()
        {
            // init object
            _timeSheetCode = new hr_TimeSheetCode();

            // set props
            Init(_timeSheetCode);

            // get relation data
            _timeSheetCode = new hr_TimeSheetCode();
        }
        public TimeSheetCodeModel(hr_TimeSheetCode timeSheet)
        {
            // init object
            _timeSheetCode = timeSheet ?? new hr_TimeSheetCode();
            // set props
            Init(_timeSheetCode);

            // get relation data
            _timeSheetMachine = hr_TimeSheetMachineService.GetById(_timeSheetCode.MachineId);
            _timeSheetMachine = _timeSheetMachine ?? new hr_TimeSheetMachine();
        }        

        #region Custom Properties    

        /// <summary>
        /// Mã NV
        /// </summary>
        [Description("Mã nhân viên")]
        public string EmployeeCode => hr_RecordServices.GetFieldValueById(_timeSheetCode.RecordId, "EmployeeCode");
        
        /// <summary>
        /// Họ tên
        /// </summary>
        [Description("Họ Tên")]
        public string FullName => hr_RecordServices.GetFieldValueById(_timeSheetCode.RecordId, "FullName");

        /// <summary>
        /// SerialNumber
        /// </summary>
        public string MachineName => _timeSheetMachine.Name;

        /// <summary>
        /// SerialNumber
        /// </summary>
        public string SerialNumber => _timeSheetMachine.SerialNumber;

        #endregion

        /// <summary>
        /// Mã chấm công
        /// </summary>
        [Description("Mã chấm công")]
        public string Code { get; set; }

        /// <summary>
        /// Id hồ sơ
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// Thời gian vào
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Thời gian ra
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Ngày làm việc
        /// </summary>
        public DateTime? WorkingDay { get; set; }

        /// <summary>
        /// Trạng thái hoạt động mã chấm công
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Id máy chấm công
        /// </summary>
        [Description("Seri máy chấm công")]
        public int MachineId { get; set; }

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
        public DateTime? EditedDate { get; set; }
    }
}
