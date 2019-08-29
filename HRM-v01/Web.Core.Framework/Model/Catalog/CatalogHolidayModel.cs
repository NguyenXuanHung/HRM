using System;
using Web.Core.Object.Catalog;

namespace Web.Core.Framework
{
    public class CatalogHolidayModel : BaseModel<cat_Holiday>
    {
        private readonly cat_Holiday _holiday;
        public CatalogHolidayModel()
        {
            // init entity
            _holiday = new cat_Holiday();

            // set field
            Init(_holiday);
        }

        public CatalogHolidayModel(cat_Holiday catalog)
        {
            // init entity
            _holiday = catalog ?? new cat_Holiday();

            // set field
            Init(_holiday);
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
        /// Day
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// Month
        /// </summary>
        public  int Month { get; set; }

        /// <summary>
        /// Year
        /// </summary>
        public  int Year { get; set; }

        /// <summary>
        /// DaySolar
        /// </summary>
        public  int DaySolar { get; set; }

        /// <summary>
        /// MonthSolar
        /// </summary>
        public  int MonthSolar { get; set; }

        /// <summary>
        /// YearSolar
        /// </summary>
        public  int YearSolar { get; set; }

        /// <summary>
        /// Category group
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Priority
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Category status
        /// </summary>
        public CatalogStatus Status { get; set; }

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

        /// <summary>
        /// Group name
        /// </summary>
        public string GroupName => ((CatalogGroupHoliday) Enum.Parse(typeof(CatalogGroupHoliday), _holiday.Group)).Description();

        /// <summary>
        /// Status name
        /// </summary>
        public string StatusName => Status.Description();

        /// <summary>
        /// 
        /// </summary>
        public string DayMonth => "{0}-{1}".FormatWith(_holiday.Day, _holiday.Month);
    }
}