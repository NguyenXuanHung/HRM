using System;
using System.Collections.Generic;

namespace Web.Core.Framework.Report
{
    public class Filter
    {
        /// <summary>
        /// 
        /// </summary>
        public Filter()
        {
            Items = new List<FilterItem>();
        }

        /// <summary>
        /// Column filter
        /// </summary>
        public List<FilterItem> Items { get; set; }

        public string Condition
        {
            get
            {
                // init condition
                var condition = "";
                // add filter intor condition
                if(Items.Count > 0)
                {
                    foreach(var filter in Items)
                    {
                        if(!string.IsNullOrEmpty(filter.GetClause()))
                            condition += filter.GetClause();
                    }
                }
                // remove first " AND "
                if(condition.Length > 4)
                    condition = condition.Substring(4);
                // return condition
                return condition;
            }
        }

        #region Common filter

        public int RecordId { get; set; }

        public string RecordIds { get; set; }

        public string EmployeeType { get; set; }

        public string OrganizationName { get; set; }

        public int TimeSheetReportId { get; set; }

        public string Departments { get; set; }

        public DateTime ReportDate { get; set; } 

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string ReportTitle { get; set; }

        public string CreatedByTitle { get; set; }

        public string CreatedByNote { get; set; }

        public string CreatedByName { get; set; }

        public string ReviewedByTitle { get; set; }

        public string ReviewedByNote { get; set; }

        public string ReviewedByName { get; set; }

        public string SignedByTitle { get; set; }

        public string SignedByNote { get; set; }

        public string SignedByName { get; set; }

        #endregion
    }
}
