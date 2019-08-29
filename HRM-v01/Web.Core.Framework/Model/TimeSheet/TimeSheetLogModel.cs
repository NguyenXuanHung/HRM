using System;
using Web.Core.Object.TimeSheet;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for TimeSheetLogModel
    /// </summary>
    public class TimeSheetLogModel : BaseModel<hr_TimeSheetLog>
    {
        private readonly hr_TimeSheetLog _timeSheetLog;

        public TimeSheetLogModel(hr_TimeSheetLog timeSheetLog)
        {
            _timeSheetLog = timeSheetLog ?? new hr_TimeSheetLog();
            Init(_timeSheetLog);
        }        

        /// <summary>
        /// Mã chấm công
        /// </summary>
        public string TimeSheetCode { get; set; }

        /// <summary>
        /// Thời gian
        /// </summary>
        public string TimeString { get; set; }        

        /// <summary>
        /// Serial máy chấm công
        /// </summary>
        public string MachineSerialNumber { get; set; }

        /// <summary>
        /// Tên máy chấm công
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// Địa chỉ IP connect máy chấm công
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// Địa điểm đặt máy chấm công
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Ngay log
        /// </summary>
        public DateTime TimeDate { get; set; }
    }
}
