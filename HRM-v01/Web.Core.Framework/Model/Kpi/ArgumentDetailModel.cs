using System;
using Web.Core.Object.Kpi;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for ArgumentDetailModel
    /// </summary>
    public class ArgumentDetailModel
    {
        /// <summary>
        /// ArgumentId
        /// </summary>
        public int ArgumentId { get; set; }

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
    }
}
