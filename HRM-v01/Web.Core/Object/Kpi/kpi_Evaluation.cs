using System;

namespace Web.Core.Object.Kpi
{
    /// <summary>
    /// kpi_Evaluation
    /// </summary>
    public class kpi_Evaluation : BaseEntity
    {
        /// <summary>
        /// RecordId
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// CriterionId
        /// </summary>
        public int CriterionId { get; set; }

        /// <summary>
        /// Month
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }

    }
}
