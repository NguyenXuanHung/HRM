namespace Web.Core.Object.Catalog
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class cat_GroupQuantumGrade : cat_Base
    {
        /// <summary>
        /// Nhóm ngạch
        /// </summary>
        public int GroupQuantumId { get; set; }

        /// <summary>
        /// Số tháng nâng lương
        /// </summary>
        public int MonthStep { get; set; }

        /// <summary>
        /// Bậc
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// Hệ số
        /// </summary>
        public decimal Factor { get; set; }
    }
}
