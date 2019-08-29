using System;
using Web.Core.Object.Kpi;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for GroupKpiModel
    /// </summary>
    public class GroupKpiModel : BaseModel<kpi_Group>
    {
        public GroupKpiModel()
        {
            // set model default props
            Init(new kpi_Group());
        }

        public GroupKpiModel(kpi_Group groupKpi)
        {
            // init entity
            groupKpi = groupKpi ?? new kpi_Group();

            // set model props
            Init(groupKpi);
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

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
