using System.Collections.Generic;

namespace Web.Core.Framework.Report
{
    public class FilterItem
    {
        public string Name { get; set; }

        public List<FilterCondition> Items { get; set; }
        public List<FilterCondition> SelectedItems { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public FilterItem()
        {
            Name = "";
            Items = new List<FilterCondition>();
            SelectedItems = new List<FilterCondition>();
        }

        /// <summary>
        /// Get group of OR condition
        /// </summary>
        /// <returns></returns>
        public string GetClause()
        {
            if (SelectedItems.Count > 0)
            {
                // init condition
                var condition = "";
                // add OR before each condition
                foreach (var item in SelectedItems)
                {
                    condition += " OR {0}".FormatWith(item.Clause);
                }
                // remove first " OR "
                if (condition.StartsWith(" OR "))
                {
                    condition = condition.Substring(4);
                }
                // group OR condition and add AND condition
                return " AND ({0})".FormatWith(condition);
            }
            return string.Empty;
        }

    }
}
