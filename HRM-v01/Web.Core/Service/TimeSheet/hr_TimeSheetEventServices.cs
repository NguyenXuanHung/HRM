using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.Catalog;

namespace Web.Core.Service.TimeSheet
{
    public class hr_TimeSheetEventServices : BaseServices<hr_TimeSheetEvent>
    {
        private const string ConditionDefault = @" 1=1 ";
        public static List<hr_TimeSheetEvent> GetAllEventsByCondition(int? id, EventStatus? eventStatusId, string eventIds)
        {
            var condition = ConditionDefault;
            if (id != null)
            {
                condition += " AND [TimeSheetId] = {0}".FormatWith(id);
            }
          
            if (!string.IsNullOrEmpty(eventIds))
            {
                condition += " AND [Id] IN ({0}) ".FormatWith(eventIds);
            }
            if (eventStatusId != null)
            {
                condition += " AND [StatusId] = {0} ".FormatWith((int)eventStatusId);
            }

            return GetAll(condition);
        }

        /// <summary>
        /// delete event by timeSheetId
        /// </summary>
        /// <param name="selectedDepartment"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="typeHandleTimeSheet"></param>
        public static void DeleteTimeSheetEvent(string selectedDepartment, int? month, int? year, string typeHandleTimeSheet)
        {
            var condition = ConditionDefault;
            if (month != null && year != null && !string.IsNullOrEmpty(typeHandleTimeSheet))
            {
                condition += " AND [TimeSheetId] IN (SELECT ts.Id FROM hr_TimeSheet ts " +
                             " left join hr_Record rc on rc.Id = ts.RecordId " +
                             " WHERE ts.Month = {0} AND ts.Year = {1} AND ts.Type = '{2}' ".FormatWith(month, year,
                                 typeHandleTimeSheet);
                
                if (!string.IsNullOrEmpty(selectedDepartment))
                {
                    var rootId = 0;
                    var department = "{0},".FormatWith(selectedDepartment);
                    if (int.TryParse(selectedDepartment, out var parseId))
                    {
                        rootId = parseId;
                    }
                    var lstDepartment = cat_DepartmentServices.GetTree(rootId).Select(d => d.Id).ToList();
                    if (lstDepartment.Count > 0)
                    {
                        department = lstDepartment.Aggregate(department, (current, d) => current + "{0},".FormatWith(d));
                    }
                    department = "{0}".FormatWith(department.TrimEnd(','));
                    condition += "AND rc.DepartmentId IN ({0}) ".FormatWith(department);
                }

                condition += " ) ";
            }

            Delete(condition);
        }
    }
}
