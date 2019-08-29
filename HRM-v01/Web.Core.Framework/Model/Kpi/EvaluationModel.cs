using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Object.Kpi;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Kpi;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for EvaluationModel
    /// </summary>
    public class EvaluationModel : BaseModel<kpi_Evaluation>
    {
        private readonly kpi_Criterion _criterion;
        private readonly hr_Record _record;
        public EvaluationModel()
        {
            // set model default props
            Init(new kpi_Evaluation());
            _criterion = new kpi_Criterion();
            _record = new hr_Record();
        }

        public EvaluationModel(kpi_Evaluation evaluation)
        {
            // init entity
            evaluation = evaluation ?? new kpi_Evaluation();

            // set model props
            Init(evaluation);

            _criterion = kpi_CriterionServices.GetById(evaluation.CriterionId);
            _criterion = _criterion ?? new kpi_Criterion();
            _record = hr_RecordServices.GetById(evaluation.RecordId);
            _record = _record ?? new hr_Record();
        }

        /// <summary>
        /// RecordId
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// CriterionId
        /// </summary>
        public int CriterionId { get; set; }

        /// <summary>
        /// Month
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }

        #region Custom Props
        /// <summary>
        /// FullName
        /// </summary>
        public string FullName => _record.FullName;

        /// <summary>
        /// EmployeeCode
        /// </summary>
        public string EmployeeCode => _record.EmployeeCode;

        /// <summary>
        /// CriterionName
        /// </summary>
        public string CriterionName => _criterion.Name;

        /// <summary>
        /// DepartmentId
        /// </summary>
        public int DepartmentId => _record.DepartmentId;

        /// <summary>
        /// DepartmentName
        /// </summary>
        public string DepartmentName => cat_DepartmentServices.GetFieldValueById(_record.DepartmentId);

        #endregion
    }
}
