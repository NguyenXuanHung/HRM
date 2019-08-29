namespace Web.Core.Object.Catalog
{
    /// <inheritdoc />
    /// <summary>
    /// Danh mục loại GROUP - ListItem
    /// </summary>
    public class cat_GroupEnum : cat_Base
    {
        /// <summary>
        /// Loai group
        /// </summary>
        public string ItemType { get; set; }
        public int Priority { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
    }
}
