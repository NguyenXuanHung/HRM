using System;

namespace Web.Core.Object.TimeSheet
{
    /// <summary>
    /// Machine time sheet
    /// </summary>
    public class hr_TimeSheetMachine : BaseEntity
    {
        /// <summary>
        /// Default contructor
        /// </summary>
        public hr_TimeSheetMachine()
        {
            Id = 0;
            Name = "";
            IPAddress = "";
            Location = "";
            CreatedBy = "";
            UpdatedBy = "";
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            SerialNumber = "";
        }

        /// <summary>
        /// số serial máy, unique value
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Địa chỉ IP
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// Địa điểm
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Id đơn vị
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Người cập nhật
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}
