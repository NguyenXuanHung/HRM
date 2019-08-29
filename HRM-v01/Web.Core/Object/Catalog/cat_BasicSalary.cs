using System;

namespace Web.Core.Object.Catalog
{
    /// <summary>
    /// Danh mục lương cơ bản
    /// </summary>
    public class cat_BasicSalary : cat_Base
    {
        /// <summary>
        /// Mức lương cơ bản
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Ngày áp dụng
        /// </summary>
        public DateTime AppliedDate { get; set; }
    }
}
