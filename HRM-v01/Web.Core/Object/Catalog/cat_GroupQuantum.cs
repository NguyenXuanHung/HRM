namespace Web.Core.Object.Catalog
{
    /// <inheritdoc />
    /// <summary>
    /// Danh muc nhom ngach
    /// </summary>
    public class cat_GroupQuantum : cat_Base
    {
        /// <summary>
        /// Số bậc tối đa
        /// </summary>
        public int GradeMax { get; set; }

        /// <summary>
        /// Số tháng nâng lương
        /// </summary>
        public int MonthStep { get; set; }

        /// <summary>
        /// % vượt khung
        /// </summary>
        public decimal PercentageOverGrade { get; set; }

        /// <summary>
        /// Bước tăng của % vượt khung
        /// </summary>
        public decimal PercentageOverGradeStep { get; set; }
    }
}
