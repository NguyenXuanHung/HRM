using System;
using Web.Core.Object.Salary;

namespace Web.Core.Service.Salary
{
    public class sal_SalaryDecisionServices : BaseServices<sal_SalaryDecision>
    {
        public static sal_SalaryDecision GetCurrent(int recordId)
        {
            // init condition
            var condition = @"[EffectiveDate]<'{0}' AND [RecordId]='{1}' AND [Status]='{2}'".FormatWith(DateTime.Now.ToString("yyyy-MM-dd"), recordId, (int)SalaryDecisionStatus.Approved);

            // init order
            var order = "[DecisionDate] DESC";

            // return entity
            return GetByCondition(condition, order);
        }
    }
}
