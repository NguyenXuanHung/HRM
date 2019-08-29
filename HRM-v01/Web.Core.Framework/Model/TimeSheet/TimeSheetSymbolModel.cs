using System;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for TimeSheetSymbolModel
    /// </summary>
    public class TimeSheetSymbolModel : BaseModel<hr_TimeSheetSymbol>
    {
        private readonly hr_TimeSheetSymbol _timeSheet;

        public TimeSheetSymbolModel(hr_TimeSheetSymbol timeSheet)
        {
            _timeSheet = timeSheet ?? new hr_TimeSheetSymbol();
            Init(_timeSheet);
        }
        /// <summary>
        /// Category code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Category description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Chọn loại công cộng hay trừ (Cộng = 0; Trừ = 1)
        /// </summary>
        public bool TypeWork { get; set; }

        /// <summary>
        /// Số công quy đổi
        /// </summary>
        public double WorkConvert { get; set; }

        /// <summary>
        /// Thời gian quy đổi
        /// </summary>
        public double TimeConvert { get; set; }

        /// <summary>
        /// Nhóm ký hiệu
        /// </summary>
        public int GroupSymbolId { get; set; }

        /// <summary>
        /// Tên nhóm ký hiệu
        /// </summary>
        public string GroupSymbolName => hr_TimeSheetGroupSymbolServices.GetFieldValueById(_timeSheet.GroupSymbolId);    

        /// <summary>
        /// Màu ký hiệu
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Category order
        /// </summary>
        public int? Order { get; set; }

        /// <summary>
        /// TimeSheet status
        /// </summary>
        public TimeSheetStatus Status { get; set; }

        /// <summary>
        /// True if object was deleted
        /// </summary>
        public bool? IsDeleted { get; set; }

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
    }
}
