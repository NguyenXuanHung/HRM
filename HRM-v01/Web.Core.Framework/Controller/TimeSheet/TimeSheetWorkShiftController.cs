using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for TimeSheetWorkShiftController
    /// </summary>
    public class TimeSheetWorkShiftController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="isDeleted"></param>
        /// <param name="groupWorkShiftId"></param>
        /// <param name="type"></param>
        /// <param name="symbolId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<TimeSheetWorkShiftModel> GetAll(string keyword, bool? isDeleted, int? groupWorkShiftId, WorkShiftType? type, int? symbolId, DateTime? fromDate, DateTime? toDate, int? month, int? year, string order, int? limit)
        {
            // init condition
            var condition = Constant.ConditionDefault;

            if(!string.IsNullOrEmpty(keyword))
            {
                condition += " AND ( [Name] LIKE N'%{0}%' OR [Code] LIKE N'%{0}%' ) ".FormatWith(keyword);
            }

            if(groupWorkShiftId != null)
            {
                condition += " AND [GroupWorkShiftId] = {0}".FormatWith(groupWorkShiftId);
            }

            if (type != null)
            {
                condition += " AND [Type] = {0}".FormatWith(type.Value);
            }

            if(symbolId != null)
            {
                condition += " AND [SymbolId] = {0}".FormatWith(symbolId);
            }

            if (fromDate.HasValue)
            {
                condition += " AND [StartDate] >= '{0}'".FormatWith(fromDate.Value.ToString("yyyy-MM-dd"));
            }

            if (toDate.HasValue)
            {
                condition += " AND [StartDate] <= '{0}'".FormatWith(toDate.Value.AddDays(1).ToString("yyyy-MM-dd"));
            }

            if (month != null)
            {
                condition += " AND [StartDate] IS NOT NULL AND MONTH([StartDate]) = {0}".FormatWith(month);
            }

            if (year != null)
            {
                condition += " AND [StartDate] IS NOT NULL AND YEAR([StartDate]) = {0}".FormatWith(year);
            }
            // is deleted
            if (isDeleted != null)
            {
                condition += " AND [IsDeleted] = {0}".FormatWith((bool)isDeleted ? "1" : "0");
            }
            return hr_TimeSheetWorkShiftServices.GetAll(condition, order, limit).Select(r => new TimeSheetWorkShiftModel(r)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="isDeleted"></param>
        /// <param name="groupWorkShiftId"></param>
        /// <param name="type"></param>
        /// <param name="symbolId"></param>
        /// <param name="order"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<TimeSheetWorkShiftModel> GetPaging(string keyword, bool? isDeleted, int? groupWorkShiftId, WorkShiftType? type, int? symbolId, DateTime? fromDate, DateTime? toDate, string order, int start, int limit)
        {
            // init condition
            var condition = Constant.ConditionDefault;

            if (!string.IsNullOrEmpty(keyword))
            {
                condition += " AND ( [Name] LIKE N'%{0}%' ) ".FormatWith(keyword);
            }

            if(groupWorkShiftId != null)
            {
                condition += " AND [GroupWorkShiftId] = {0}".FormatWith(groupWorkShiftId);
            }

            if (type != null)
            {
                condition += " AND [Type] = {0}".FormatWith(type.Value);
            }

            if (symbolId != null)
            {
                condition += " AND [SymbolId] = {0}".FormatWith(symbolId);
            }

            if (fromDate.HasValue)
            {
                condition += " AND [StartDate] >= '{0}'".FormatWith(fromDate.Value.ToString("yyyy-MM-dd"));
            }

            if (toDate.HasValue)
            {
                condition += " AND [StartDate] <= '{0}'".FormatWith(toDate.Value.AddDays(1).ToString("yyyy-MM-dd"));
            }

            // is deleted
            if (isDeleted != null)
            {
                condition += " AND [IsDeleted] = {0}".FormatWith((bool)isDeleted ? "1" : "0");
            }

            var result = hr_TimeSheetWorkShiftServices.GetPaging(condition, order, start, limit);

            var listModels = new List<TimeSheetWorkShiftModel>();

            if (result.Data.Count > 0)
            {
                listModels.AddRange(result.Data.Select(record => new TimeSheetWorkShiftModel(record)));
            }
                            
            return new PageResult<TimeSheetWorkShiftModel>(result.Total, listModels);
        }        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TimeSheetWorkShiftModel GetById(int id)
        {
            var record = hr_TimeSheetWorkShiftServices.GetById(id);
            return new TimeSheetWorkShiftModel(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TimeSheetWorkShiftModel Create(TimeSheetWorkShiftModel model)
        {
            var entity = new hr_TimeSheetWorkShift();
            model.FillEntity(ref entity);

            return new TimeSheetWorkShiftModel(hr_TimeSheetWorkShiftServices.Create(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void Update(TimeSheetWorkShiftModel model)
        {
            var record = hr_TimeSheetWorkShiftServices.GetById(model.Id);
            if (record == null) return;
            //Edit data
            model.FillEntity(ref record);
            record.EditedDate = DateTime.Now;

            //update
            hr_TimeSheetWorkShiftServices.Update(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void Delete(int id)
        {
            hr_TimeSheetWorkShiftServices.Delete(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupWorkShiftId"></param>
        /// <param name="symbolId"></param>
        public static void DeleteByCondition(int? groupWorkShiftId, int? symbolId)
        {
            var condition = Constant.ConditionDefault;
            if (groupWorkShiftId != null)
            {
                condition += " AND [GroupWorkShiftId] = {0}".FormatWith(groupWorkShiftId);
            }

            if (symbolId != null)
            {
                condition += " AND [SymbolId] = {0}".FormatWith(symbolId);
            }

            var entity = hr_TimeSheetWorkShiftServices.GetByCondition(condition);
            if (entity != null)
            {
                //Delete
                hr_TimeSheetWorkShiftServices.Delete(condition);
            }
        }
    }
}
