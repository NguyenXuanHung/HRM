using System;

namespace Web.Core.Object.Catalog
{
    /// <inheritdoc />
    /// <summary>
    /// Danh muc ngày lễ, tết
    /// </summary>
    public class cat_Holiday : cat_Base
    {
        public cat_Holiday()
        {
            Day = 1;
            Month = 1;
            Year = DateTime.Now.Year;
            Group = CatalogGroupHoliday.AL.ToString();
        }

        /// <summary>
        /// Ngày
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// Tháng
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Năm
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Ngày dương lịch
        /// </summary>
        public int DaySolar { get; set; }

        /// <summary>
        /// Tháng dương lịch
        /// </summary>
        public int MonthSolar { get; set; }

        /// <summary>
        /// Năm dương
        /// </summary>
        public  int YearSolar { get; set; }
    }
}
