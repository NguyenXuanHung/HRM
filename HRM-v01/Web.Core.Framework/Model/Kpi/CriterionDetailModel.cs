using System;
using Web.Core.Object.Kpi;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for CriterionDetailModel
    /// </summary>
    public class CriterionDetailModel
    {
        /// <summary>
        /// CriterionId
        /// </summary>
        public int CriterionId { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Display
        /// </summary>
        public string Display => $"<b style='color:{Color}'>{Value}</b>";
    }
}
