namespace Web.Core.Object.Catalog
{
    /// <inheritdoc />
    /// <summary>
    /// Danh mục địa điểm
    /// </summary>
    public class cat_Location : cat_Base
    {
        /// <summary>
        /// Mã địa điểm cha
        /// </summary>
        public int ParentId { get; set; }
    }
}
