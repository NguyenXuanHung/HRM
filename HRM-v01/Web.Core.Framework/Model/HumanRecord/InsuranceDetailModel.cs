namespace Web.Core.Framework.Model.HumanRecord
{
    public class InsuranceDetailModel
    {
        /// <summary>
        /// year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// year
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Lương bảo hiểm DN đóng
        /// </summary>
        public decimal EnterpriseSocial { get; set; }

        /// <summary>
        /// Lương bảo hiểm người lao động đóng
        /// </summary>
        public decimal LaborerSocial { get; set; }
    }
}
