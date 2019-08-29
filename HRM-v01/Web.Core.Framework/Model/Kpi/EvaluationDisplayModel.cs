using System;
using System.Collections.Generic;
using System.Linq;
using Ext.Net;
using Web.Core.Object.HumanRecord;
using Web.Core.Object.Kpi;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Kpi;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for EvaluationDisplayModel
    /// </summary>
    public class EvaluationDisplayModel
    {
        private readonly List<EvaluationModel> _evaluationModels;
        private readonly int _recordId;
        private readonly int _groupId;
        public EvaluationDisplayModel(List<EvaluationModel> evaluationModels, int recordId, int groupId)
        {
            _evaluationModels = evaluationModels;
            _recordId = recordId;
            _groupId = groupId;
        }

        /// <summary>
        /// RecordId
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// DepartmentName
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<CriterionDetailModel> CriterionDetailModels
        {
            get
            {
                var result = new List<CriterionDetailModel>();
                var criterionGroups = CriterionGroupController.GetAll(null, null, _groupId, null, null);

                foreach (var item in criterionGroups)
                {
                    // get criterion
                    var criterion = CriterionController.GetById(item.CriterionId);
                    var criterionDetailModel = new CriterionDetailModel
                    {
                        CriterionId = item.CriterionId,
                        Code = item.CriterionCode,
                        Name = item.CriterionName,
                        Value = ""
                    };
                    var evaluation = _evaluationModels.Where(rc => rc.RecordId == _recordId && rc.CriterionId == item.CriterionId).ToList();
                    if (evaluation.Count > 0)
                    {
                        criterionDetailModel.Value = evaluation[0].Value;
                    }

                    if (criterion != null)
                    {
                        if (!string.IsNullOrEmpty(criterion.FormulaRange))
                        {
                            // get formula ranges
                            var formulaRanges = JSON.Deserialize<List<CriterionFormulaModel>>(criterion.FormulaRange);
                            // get formula range
                            var formulaRange = formulaRanges.Find(f => f.Result == criterionDetailModel.Value);
                            // set color
                            criterionDetailModel.Color = formulaRange != null ? formulaRange.Color : "";
                        }
                    }
                    result.Add(criterionDetailModel);
                }
              
                return result;
            }
        }
    }
}
