using System;

namespace Web.Core.Object.Catalog
{
    /// <inheritdoc />
    /// <summary>
    /// Danh mục quan hệ gia đình (Bố đẻ, mẹ đẻ, vợ, chồng,...)
    /// </summary>
    public class cat_Relationship: cat_Base
    { 
        /// <summary>
        /// 
        /// </summary>
        public  bool HasHusband { get; set; }
    }
}
