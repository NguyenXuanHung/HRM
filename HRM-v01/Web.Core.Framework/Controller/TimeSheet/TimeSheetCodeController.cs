using System;
using System.Collections.Generic;
using System.Linq;
using Ext.Net.Utilities;
using Web.Core.Framework.Common;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework.Controller.TimeSheet
{
    public class TimeSheetCodeController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="recordId"></param>
        /// <param name="selectedDepartmentId"></param>
        /// <param name="departmentIds"></param>
        /// <param name="code"></param>
        /// <param name="isActive"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<TimeSheetCodeModel> GetAll(string keyword, int? recordId, int? selectedDepartmentId, string departmentIds, string code, bool? isActive, DateTime? startTime, DateTime? endTime, string order, int? limit)
        {
            var condition = Constant.ConditionDefault;
            if (!string.IsNullOrEmpty(keyword))
            {
                condition += " AND [Code] LIKE N'%{0}%' ".FormatWith(keyword);
            }

            if (recordId != null)
            {
                condition += " AND [RecordId] = '{0}'".FormatWith(recordId);
            }

            if (selectedDepartmentId != null)
            {
                condition += " AND (SELECT cd.[Id] FROM cat_Department cd LEFT JOIN hr_Record rc ON rc.[DepartmentId] = cd.[Id] WHERE [RecordId] = rc.[Id]) = {0} ".FormatWith(selectedDepartmentId);
            }

            if (departmentIds != null)
            {
                condition += " AND [RecordId] IN (SELECT Id FROM hr_Record WHERE [DepartmentId] IN ({0})) ".FormatWith(departmentIds);
            }

            if (!string.IsNullOrEmpty(code))
            {
                condition += " AND [Code] = '{0}'".FormatWith(code);
            }

            if (isActive != null)
            {
                condition += " AND [IsActive] = {0}".FormatWith((bool) isActive ? "1" : "0");
            }

            if (startTime.HasValue)
            {
                condition +=
                    " AND ([StartTime] <= '{0}' OR [StartTime] <= '{1}')".FormatWith(startTime?.ToString("yyyy-MM-dd"),
                        DateTime.Now.ToString("yyyy-MM-dd"));
            }

            if (endTime.HasValue)
            {
                condition +=
                    " AND (([EndTime] IS NOT NULL AND [EndTime] <= '{0}' AND [EndTime] >= '{1}') OR [EndTime] IS NULL)"
                        .FormatWith(endTime?.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
            }
            var listModels = new List<TimeSheetCodeModel>();
            var result = hr_TimeSheetCodeServices.GetAll(condition, order, limit);
            foreach (var timeSheetCode in result)
            {
                listModels.Add(new TimeSheetCodeModel(timeSheetCode));
            }
            return listModels;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="selectedDepartmentId"></param>
        /// <param name="recordId"></param>
        /// <param name="isActive"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<TimeSheetCodeModel> GetPaging(string keyword, string departmentIds,int? selectedDepartmentId, int? recordId, bool? isActive, string order, int start, int limit)
        {
            var condition = Constant.ConditionDefault;
            if (!string.IsNullOrEmpty(keyword))
            {
                condition += " AND ([Code] LIKE N'%{0}%' ".FormatWith(keyword)+
                             " OR [RecordId] IN ((SELECT Id FROM hr_Record WHERE [EmployeeCode] LIKE N'%{0}%' OR [FullName] LIKE N'%{0}%')) ".FormatWith(keyword) +
                             " )";
            }

            if (selectedDepartmentId != null && selectedDepartmentId != 0)
            {
                condition += " AND [RecordId] IN (SELECT Id FROM hr_Record WHERE [DepartmentId] = {0}) ".FormatWith(selectedDepartmentId);
            }

            if (!string.IsNullOrEmpty(departmentIds))
            {
                condition += " AND [RecordId] IN ((SELECT Id FROM hr_Record WHERE [DepartmentId] IN ({0}))) ".FormatWith(departmentIds);
            }

            if(recordId != null)
            {
                condition += " AND [RecordId] = {0}".FormatWith(recordId);
            }

            if(isActive != null)
            {
                condition += " AND [IsActive] = {0}".FormatWith((bool)isActive ? "1" : "0");
            }

            var result = hr_TimeSheetCodeServices.GetPaging(condition, order, start, limit);
            
            return new PageResult<TimeSheetCodeModel>(result.Total, result.Data.Select(m=> new TimeSheetCodeModel(m)).ToList());            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public static List<TimeSheetCodeModel> GetByRecordId(int? recordId, bool? isActive)
        {
            var condition = Constant.ConditionDefault;
            if (recordId != null)
            {
                condition += @" AND [RecordId] = '{0}' ".FormatWith(recordId);
            }

            if (isActive != null)
            {
                condition += @" AND [IsActive] = {0} ".FormatWith((bool) isActive ? "1" : "0");
            }

            return hr_TimeSheetCodeServices.GetAll(condition).Select(r => new TimeSheetCodeModel(r)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="machineId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public static TimeSheetCodeModel GetUnique(int? recordId, int? machineId, bool? isActive)
        {
            var condition = Constant.ConditionDefault;
            if (recordId != null)
            {
                condition += @" AND [RecordId] = '{0}' ".FormatWith(recordId);
            }
            if (machineId != null)
            {
                condition += @" AND [MachineId] = '{0}' ".FormatWith(machineId);
            }

            if (isActive != null)
            {
                condition += @" AND [IsActive] = {0} ".FormatWith((bool)isActive ? "1" : "0");
            }

            var entity = hr_TimeSheetCodeServices.GetByCondition(condition);
            return entity != null ? new TimeSheetCodeModel(entity) : null;
        }

        /// <summary>
        /// Get time sheet code by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TimeSheetCodeModel GetById(int id)
        {
            var result = hr_TimeSheetCodeServices.GetById(id);
            return new TimeSheetCodeModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TimeSheetCodeModel Create(TimeSheetCodeModel model)
        {
            var entity = new hr_TimeSheetCode();
            model.FillEntity(ref entity);
            return new TimeSheetCodeModel(hr_TimeSheetCodeServices.Create(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void Update(TimeSheetCodeModel model)
        {
            var record = hr_TimeSheetCodeServices.GetById(model.Id);
            if (record == null) return;
            model.FillEntity(ref record);
            record.EditedDate = DateTime.Now;

            hr_TimeSheetCodeServices.Update(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void Delete(int id)
        {
            hr_TimeSheetCodeServices.Delete(id);

        }
    }
}
