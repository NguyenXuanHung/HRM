using System;

namespace Web.Core.Object.TimeSheet
{
    /// <summary>
    /// Tổng hợp công
    /// </summary>
    public class hr_TimeSheetLog : BaseEntity
    {
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
        /// Ngay log
        /// </summary>
        public DateTime TimeDate { get; set; }
        
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }
      
    }
}
