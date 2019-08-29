using System;
using Web.Core.Object.Catalog;
using Web.Core.Object.TimeSheet;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for TimeSheetGroupSymbolModel
    /// </summary>
    public class TimeSheetGroupSymbolModel : BaseModel<hr_TimeSheetGroupSymbol>
    {
        private readonly hr_TimeSheetGroupSymbol _timeSheetGroupSymbol;

        public TimeSheetGroupSymbolModel(hr_TimeSheetGroupSymbol timeSheetGroupSymbol)
        {
            _timeSheetGroupSymbol = timeSheetGroupSymbol ?? new hr_TimeSheetGroupSymbol();
            Init(_timeSheetGroupSymbol);
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
        /// Category group
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Category order
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Category status
        /// </summary>
        public TimeSheetStatus Status { get; set; }

        /// <summary>
        /// True if object was deleted
        /// </summary>
        public bool IsDeleted { get; set; }

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
