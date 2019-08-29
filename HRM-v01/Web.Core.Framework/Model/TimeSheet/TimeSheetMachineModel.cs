using System;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework
{
    public class TimeSheetMachineModel: BaseModel<hr_TimeSheetMachine>
    {
        private readonly hr_TimeSheetMachine _machineTimeSheet;

        public TimeSheetMachineModel(hr_TimeSheetMachine machineTimeSheet)
        {
            _machineTimeSheet = machineTimeSheet ?? new hr_TimeSheetMachine();

            Init(_machineTimeSheet);
        }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public int? DepartmentId { get; set; }
        public string DepartmentName => cat_DepartmentServices.GetFieldValueById(_machineTimeSheet.DepartmentId, "Name");
        
        /// <summary>
        /// số serial máy 
        /// </summary>
        public string SerialNumber { get; set; }
        
        /// <summary>
        /// Tên máy
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
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }
        
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Người cập nhật
        /// </summary>
        public string UpdatedBy { get; set; }
    }
}
