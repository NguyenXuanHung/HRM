using System;
using Web.Core.Object.Kpi;
using Web.Core.Service.Kpi;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for CriterionGroupModel
    /// </summary>
    public class CriterionGroupModel : BaseModel<kpi_CriterionGroup>
    {
        private readonly kpi_Criterion _criterion;
        private readonly kpi_Group _group;
        public CriterionGroupModel()
        {
            // set model default props
            Init(new kpi_CriterionGroup());
            _criterion = new kpi_Criterion();
            _group = new kpi_Group();
        }

        public CriterionGroupModel(kpi_CriterionGroup criterionGroup)
        {
            // init entity
            criterionGroup = criterionGroup ?? new kpi_CriterionGroup();

            // set model props
            Init(criterionGroup);
            _criterion = kpi_CriterionServices.GetById(criterionGroup.CriterionId);
            _criterion = _criterion ?? new kpi_Criterion();
            _group = kpi_GroupServices.GetById(criterionGroup.GroupId);
            _group = _group ?? new kpi_Group();
        }

        /// <summary>
        /// CriterionId
        /// </summary>
        public int CriterionId { get; set; }

        /// <summary>
        /// GroupId
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string CriterionName => _criterion.Name;
        
        /// <summary>
        /// Code
        /// </summary>
        public string CriterionCode => _criterion.Code;

        /// <summary>
        /// Description
        /// </summary>
        public string Description => _criterion.Description;

        /// <summary>
        /// ValueType
        /// </summary>
        public KpiValueType ValueType => _criterion.ValueType;

        /// <summary>
        /// ValueTypeName
        /// </summary>
        public string ValueTypeName => _criterion.ValueType.Description();

        /// <summary>
        /// GroupName
        /// </summary>
        public string GroupName => _group.Name;

    }
}
