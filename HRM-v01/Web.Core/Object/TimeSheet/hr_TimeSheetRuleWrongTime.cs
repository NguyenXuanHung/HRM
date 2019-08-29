using System;

namespace Web.Core.Object.TimeSheet
{
    /// <summary>
    /// Rule đi muộn, về sớm
    /// </summary>
    public class hr_TimeSheetRuleWrongTime : BaseEntity
    {
        public hr_TimeSheetRuleWrongTime()
        {
            Id = 0;
            SymbolId = 0;
            FromMinute = 0;
            ToMinute = 0;
            WorkConvert = 0;
            TimeConvert = 0;
            Type = TimeSheetRuleWrongTimeType.ComeLate;
            Order = 0;
            IsMinus = true;
            IsDeleted = false;
            CreatedBy = "system";
            CreatedDate = DateTime.Now;
            EditedBy = "system";
            EditedDate = DateTime.Now;

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
        /// Deleted status
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// system, created user
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// system, created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// system edited user
        /// </summary>
        public string EditedBy { get; set; }

        /// <summary>
        /// system edited date
        /// </summary>
        public DateTime EditedDate { get; set; }
    }
}
