using System;
using Web.Core.Object.Catalog;
using Web.Core.Object.HumanRecord;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework
{
    public class TimeSheetEventModel: BaseModel<hr_TimeSheetEvent>
    {
        public TimeSheetEventModel()
        {
            // init obj
            var entity = new hr_TimeSheetEvent();

            //set pros
            Init(entity);
        }

        public TimeSheetEventModel(hr_TimeSheetEvent timeSheetEvent)
        {
            //init obj
            var entity = timeSheetEvent ?? new hr_TimeSheetEvent();

            // get relation data
            var record = hr_RecordServices.GetById(entity.RecordId) ?? new hr_Record();
            var department = cat_DepartmentServices.GetById(record.DepartmentId) ?? new cat_Department();

            var timeSheetSymbol = hr_TimeSheetSymbolServices.GetById(entity.SymbolId) ?? new hr_TimeSheetSymbol();
            var timeSheetGroupSymbol = hr_TimeSheetGroupSymbolServices.GetById(entity.GroupSymbolId) ?? new hr_TimeSheetGroupSymbol();

            var timeSheetWorkShift = hr_TimeSheetWorkShiftServices.GetById(entity.WorkShiftId) ?? new hr_TimeSheetWorkShift();
            var timeSheetGroupWorkShift =
                hr_TimeSheetGroupWorkShiftServices.GetById(timeSheetWorkShift.GroupWorkShiftId) ??
                new hr_TimeSheetGroupWorkShift();

            // set private props
            EmployeeCode = record.EmployeeCode;
            FullName = record.FullName;
            DepartmentName = department.Name;
            GroupWorkShiftName = timeSheetGroupWorkShift.Name;
            WorkShiftName = timeSheetWorkShift.Name;
            StartDate = timeSheetWorkShift.StartDate;
            EndDate = timeSheetWorkShift.EndDate;
            SymbolName = timeSheetSymbol.Name;
            SymbolCode = timeSheetSymbol.Code;
            SymbolColor = timeSheetSymbol.Color;
            GroupSymbolName = timeSheetGroupSymbol.Name;
            GroupSymbolGroup = timeSheetGroupSymbol.Group;

            //set pros
            Init(entity);
        }

        /// <summary>
        /// Id hồ sơ
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// id ca chi tiết
        /// </summary>
        public int WorkShiftId { get; set; }

        /// <summary>
        /// id nhóm ký hiệu
        /// </summary>
        public int GroupSymbolId { get; set; }

        /// <summary>
        /// id ký hiệu
        /// </summary>
        public int SymbolId { get; set; }

        /// <summary>
        /// Công quy đổi
        /// </summary>
        public double WorkConvert { get; set; }

        /// <summary>
        /// Thời gian quy đổi
        /// </summary>
        public double TimeConvert { get; set; }

        /// <summary>
        /// Loại hiệu chỉnh
        /// </summary>
        public TimeSheetAdjustmentType Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// True if object was deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Trạng thái event (active, delete, pending,... )
        /// </summary>
        public EventStatus Status { get; set; }

        /// <summary>
        /// Created by, default system user
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Edited by, default system user
        /// </summary>
        public string EditedBy { get; set; }

        /// <summary>
        /// Edited date
        /// </summary>
        public DateTime EditedDate { get; set; }

        #region Custom Properties

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Tên nhóm phân ca
        /// </summary>
        public string GroupWorkShiftName { get; set; }

        /// <summary>
        /// Tên ca
        /// </summary>
        public string WorkShiftName { get; set; }

        /// <summary>
        /// Giờ bắt đầu
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Giờ kết thúc
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Tên ký hiệu
        /// </summary>
        public string SymbolName { get; set; }

        /// <summary>
        /// Ký hiệu hiển thị
        /// </summary>
        public string SymbolCode { get; set; }

        /// <summary>
        /// Màu ký hiệu
        /// </summary>
        public string SymbolColor { get; set; }


        /// <summary>
        /// Tên nhóm ký hiệu
        /// </summary>
        public string GroupSymbolName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GroupSymbolGroup { get; set; }

        #endregion
    }
}
