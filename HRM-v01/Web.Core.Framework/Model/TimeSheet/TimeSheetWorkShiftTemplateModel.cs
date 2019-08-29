using System;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for TimeSheetWorkShiftTemplateModel
    /// </summary>
    public class TimeSheetWorkShiftTemplateModel: BaseModel<hr_TimeSheetWorkShiftTemplate>
    {
        private readonly hr_TimeSheetWorkShiftTemplate _timeSheetWorkShift;
        private readonly hr_TimeSheetSymbol _timeSheetSymbol;

        public TimeSheetWorkShiftTemplateModel()
        {
            // init object
            _timeSheetWorkShift = new hr_TimeSheetWorkShiftTemplate();

            // set props
            Init(_timeSheetWorkShift);

            // get relation data
            _timeSheetSymbol = new hr_TimeSheetSymbol();
        }

        public TimeSheetWorkShiftTemplateModel(hr_TimeSheetWorkShiftTemplate timeSheetWorkShift)
        {
            // init object
            _timeSheetWorkShift = timeSheetWorkShift ?? new hr_TimeSheetWorkShiftTemplate();

            // set props
            Init(_timeSheetWorkShift);

            // get relation data
            _timeSheetSymbol = hr_TimeSheetSymbolServices.GetById(_timeSheetWorkShift.SymbolId);
            _timeSheetSymbol = _timeSheetSymbol ?? new hr_TimeSheetSymbol();
        }

        /// <summary>
        /// Tên template
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// id nhóm ký hiệu
        /// </summary>
        public int GroupSymbolId { get; set; }

        /// <summary>
        /// id ký hiệu chấm công
        /// </summary>
        public int SymbolId { get; set; }

        /// <summary>
        /// Ngày bắt đầu làm việc
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Ngày kết thúc làm việc
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Thời gian bắt đầu vào hiểu ca
        /// </summary>
        public DateTime StartInTime { get; set; }

        /// <summary>
        /// Thời gian kết thúc vào hiểu ca
        /// </summary>
        public DateTime EndInTime { get; set; }

        /// <summary>
        /// Thời gian bắt đầu ra hiểu ca
        /// </summary>
        public DateTime StartOutTime { get; set; }

        /// <summary>
        /// Thời gian kết thúc ra hiểu ca
        /// </summary>
        public DateTime EndOutTime { get; set; }

        /// <summary>
        /// Công quy đổi
        /// </summary>
        public double WorkConvert { get; set; }

        /// <summary>
        /// Thời gian quy đổi
        /// </summary>
        public double TimeConvert { get; set; }

        /// <summary>
        /// Có thêm giờ không
        /// </summary>
        public bool HasOverTime { get; set; }

        /// <summary>
        /// Số phút tối thiểu thêm giờ
        /// </summary>
        public int MinOverTime { get; set; }

        /// <summary>
        /// Có tinh gio vao ra khong
        /// </summary>
        public bool HasInOutTime { get; set; }

        /// <summary>
        /// Co tinh le tet
        /// </summary>
        public bool HasHoliday { get; set; }

        /// <summary>
        /// Có xóa không
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Giới hạn thời gian làm việc tối đa
        /// </summary>
        public bool HasLimitTime { get; set; }

        /// <summary>
        /// has monday
        /// </summary>
        public bool HasMonday { get; set; }

        /// <summary>
        /// has tuesday
        /// </summary>
        public bool HasTuesday { get; set; }

        /// <summary>
        /// has Wednesday
        /// </summary>
        public bool HasWednesday { get; set; }

        /// <summary>
        /// has thursday
        /// </summary>
        public bool HasThursday { get; set; }

        /// <summary>
        /// has friday
        /// </summary>
        public bool HasFriday { get; set; }

        /// <summary>
        /// has tuesday
        /// </summary>
        public bool HasSaturday { get; set; }

        /// <summary>
        /// has tuesday
        /// </summary>
        public bool HasSunday { get; set; }

        /// <summary>
        /// type normal/break
        /// </summary>
        public WorkShiftType Type { get; set; }

        /// <summary>
        /// cbo same day or other day
        /// </summary>
        public int TypeEnd { get; set; }

        /// <summary>
        /// cbo same day or other day
        /// </summary>
        public int TypeStartIn { get; set; }

        /// <summary>
        /// cbo same day or other day
        /// </summary>
        public int TypeEndIn { get; set; }

        /// <summary>
        /// cbo same day or other day
        /// </summary>
        public int TypeStartOut { get; set; }

        /// <summary>
        /// cbo same day or other day
        /// </summary>
        public int TypeEndOut { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày thay đổi
        /// </summary>
        public DateTime EditedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string EditedBy { get; set; }

        #region custom props

        /// <summary>
        /// Diễn giải
        /// </summary>
        public string SymbolName => _timeSheetSymbol.Name;

        /// <summary>
        /// Ký hiệu hiện thị trên bảng công
        /// </summary>
        public string SymbolCode => _timeSheetSymbol.Code;

        /// <summary>
        /// Màu background
        /// </summary>
        public string SymbolColor => _timeSheetSymbol.Color;

        /// <summary>
        /// Tên nhóm ký hiệu
        /// </summary>
        public string GroupSymbolName => hr_TimeSheetGroupSymbolServices.GetFieldValueById(_timeSheetWorkShift.GroupSymbolId);


        #endregion
    }
}
