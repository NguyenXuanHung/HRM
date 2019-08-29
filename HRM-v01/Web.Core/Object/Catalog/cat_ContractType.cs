namespace Web.Core.Object.Catalog
{
    /// <inheritdoc />
    /// <summary>
    /// Danh muc loai hop dong
    /// </summary>
    public class cat_ContractType : cat_Base
    {
        /// <summary>
        /// Thời hạn hợp đồng
        /// </summary>
        public int ContractMonth { get; set; }

        /// <summary>
        /// Hệ số hợp đồng
        /// </summary>
        public double ContractFactor { get; set; }
    }
}
