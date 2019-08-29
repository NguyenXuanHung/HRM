using System;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for TimeSheetRuleWrongTimeModel
    /// </summary>
    public class TimeSheetRuleWrongTimeModel: BaseModel<hr_TimeSheetRuleWrongTime>
    {
        private readonly hr_TimeSheetRuleWrongTime _timeSheet;
        private readonly hr_TimeSheetSymbol _symbol;

        public TimeSheetRuleWrongTimeModel()
        {
            // init entity
            _timeSheet = new hr_TimeSheetRuleWrongTime();
            

            // set props
            Init(_timeSheet);

            // init relation
            _symbol = new hr_TimeSheetSymbol();
        }

        public TimeSheetRuleWrongTimeModel(hr_TimeSheetRuleWrongTime timeSheet)
        {
            // init entity
            _timeSheet = timeSheet ?? new hr_TimeSheetRuleWrongTime();

            // set props
            Init(_timeSheet);

            // init relation
            _symbol = hr_TimeSheetSymbolServices.GetById(_timeSheet.SymbolId) ?? new hr_TimeSheetSymbol();
        }

        /// <summary>
        /// Thời gian từ
        /// </summary>
        public int FromMinute { get; set; }

        /// <summary>
        /// Thời gian đến
        /// </summary>
        public int ToMinute { get; set; }

        /// <summary>
        /// Công quy đổi
        /// </summary>
        public double WorkConvert { get; set; }

        /// <summary>
        /// Loại 1: cộng, 0: trừ
        /// </summary>
        public bool IsMinus { get; set; }

        /// <summary>
        /// 1: đi muộn, 2: về sớm,
        /// </summary>
        public TimeSheetRuleWrongTimeType Type { get; set; }

        /// <summary>
        ///  name
        /// </summary>
        public string TypeName => Type.Description();

        /// <summary>
        /// Thứ tự
        /// </summary>
        public int Order { get; set; }
        
        /// <summary>
        /// Thời gian qui đổi
        /// </summary>
        public double TimeConvert { get; set; }

        /// <summary>
        /// id ký hiệu chấm công
        /// </summary>
        public int SymbolId { get; set; }

        /// <summary>
        /// id nhóm ký hiệu chấm công
        /// </summary>
        public int GroupSymbolId => _symbol.GroupSymbolId;

        /// <summary>
        /// Ký hiệu hiển thị trên bảng công
        /// </summary>
        public string SymbolName => _symbol.Code;

        /// <summary>
        /// Màu background
        /// </summary>
        public string SymbolColor => _symbol.Color;

        /// <summary>
        /// Deleted status
        /// </summary>
        public bool IsDeleted { get; set; }

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
    }
}
