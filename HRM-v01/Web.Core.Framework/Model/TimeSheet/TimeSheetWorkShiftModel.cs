using System;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for TimeSheetWorkShiftModel
    /// </summary>
    public class TimeSheetWorkShiftModel: BaseModel<hr_TimeSheetWorkShift>
    {
        private readonly hr_TimeSheetWorkShift _timeSheetWorkShift;
        private readonly hr_TimeSheetSymbol _timeSheetSymbol;

        public TimeSheetWorkShiftModel()
        {
            // init object
            _timeSheetWorkShift = new hr_TimeSheetWorkShift();

            // set props
            Init(_timeSheetWorkShift);

            // get relation data
            _timeSheetSymbol = new hr_TimeSheetSymbol();
        }

        public TimeSheetWorkShiftModel(hr_TimeSheetWorkShift timeSheetWorkShift)
        {
            // init object
            _timeSheetWorkShift = timeSheetWorkShift ?? new hr_TimeSheetWorkShift();

            // set props
            Init(_timeSheetWorkShift);

            // get relation data
            _timeSheetSymbol = hr_TimeSheetSymbolServices.GetById(_timeSheetWorkShift.SymbolId);
            _timeSheetSymbol = _timeSheetSymbol ?? new hr_TimeSheetSymbol();
        }        

        /// <summary>
        /// id nhóm bảng phân ca
        /// </summary>
        public int GroupWorkShiftId { get; set; }

        /// <summary>
        /// Tên nhóm bảng phân ca
        /// </summary>
        public string GroupWorkShiftName => hr_TimeSheetGroupWorkShiftServices.GetFieldValueById(_timeSheetWorkShift.GroupWorkShiftId);

        /// <summary>
        /// id ký hiệu chấm công
        /// </summary>
        public int SymbolId { get; set; }

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
        /// id nhóm ký hiệu
        /// </summary>
        public int GroupSymbolId => _timeSheetSymbol.GroupSymbolId;

        /// <summary>
        /// Tên nhóm ký hiệu
        /// </summary>
        public string GroupSymbolName => hr_TimeSheetGroupSymbolServices.GetFieldValueById(_timeSheetSymbol.GroupSymbolId);

        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ngày bắt đầu chấm công
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Ngày kết thúc chấm công
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Thời gian bắt đầu vào chấm công
        /// </summary>
        public DateTime StartInTime { get; set; }

        /// <summary>
        /// Thời gian kết thúc vào chấm công
        /// </summary>
        public DateTime EndInTime { get; set; }

        /// <summary>
        /// Thời gian bắt đầu ra chấm công
        /// </summary>
        public DateTime StartOutTime { get; set; }

        /// <summary>
        /// Thời gian kết thúc ra chấm công
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
        /// Giới hạn thời gian làm việc tối đa
        /// </summary>
        public bool HasLimitTime { get; set; }

        /// <summary>
        /// Bỏ ngày lễ tết
        /// </summary>
        public bool HasHoliday { get; set; }

        /// <summary>
        /// Có xóa không
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// type normal/break
        /// </summary>
        public WorkShiftType Type { get; set; }

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
    }
}
