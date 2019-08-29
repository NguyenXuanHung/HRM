namespace Web.Core.Framework.Report
{
    public class SelectedFilterCondition
    {
        public string FilterItemName { get; set; }
        public string ConditionName { get; set; }
        public string ConditionClause { get; set; }

        public SelectedFilterCondition(string itemName, string conditionName, string clause)
        {
            FilterItemName = itemName;
            ConditionName = conditionName;
            ConditionClause = clause;
        }
    }
}
