using System;
using Web.Core.Object.Kpi;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for CriterionModel
    /// </summary>
    public class CriterionModel : BaseModel<kpi_Criterion>
    {
        public CriterionModel()
        {
            // set model default props
            Init(new kpi_Criterion());
        }

        public CriterionModel(kpi_Criterion criterion)
        {
            // init entity
            criterion = criterion ?? new kpi_Criterion();

            // set model props
            Init(criterion);
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

        #region custom properties
        /// <summary>
        /// ValueTypeName
        /// </summary>
        public string ValueTypeName => ValueType.Description();
        
        #endregion
    }
}
