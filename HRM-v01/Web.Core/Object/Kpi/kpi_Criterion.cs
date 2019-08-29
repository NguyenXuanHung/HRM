using System;

namespace Web.Core.Object.Kpi
{
    /// <summary>
    /// kpi_Criterion
    /// </summary>
    public class kpi_Criterion : BaseEntity
    {
        public kpi_Criterion()
        {
            Code = "";
            Name = "";
            Description = "";
            Formula = "";
            ValueType = KpiValueType.Number;
            Status = KpiStatus.Active;
            IsDeleted = false;
            CreatedBy = "admin";
            CreatedDate = DateTime.Now;
            EditedBy = "admin";
            EditedDate = DateTime.Now;
        }

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
        public string Description { get; set; }

        /// <summary>
        /// Formula
        /// </summary>
        public string Formula { get; set; }

        /// <summary>
        /// Formula ranges
        /// </summary>
        public string FormulaRange { get; set; }

        /// <summary>
        /// data type ValueType
        /// </summary>
        public KpiValueType ValueType { get; set; }

        /// <summary>
        /// Order
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// kpi status
        /// </summary>
        public KpiStatus Status { get; set; }

        /// <summary>
        /// Deleted status
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// System, created user
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// System, created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// System, edited user
        /// </summary>
        public string EditedBy { get; set; }

        /// <summary>
        /// System, edited date
        /// </summary>
        public DateTime EditedDate { get; set; }
    }
}
