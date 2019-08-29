using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for TimeSheetWorkShiftTemplateController
    /// </summary>
    public class TimeSheetWorkShiftTemplateController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="isDeleted"></param>
        /// <param name="groupSymbolTypeId"></param>
        /// <param name="symbolId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<TimeSheetWorkShiftTemplateModel> GetAll(string keyword, bool? isDeleted, int? groupSymbolTypeId, int? symbolId, DateTime? fromDate, DateTime? toDate, int? month, int? year, string order, int? limit)
        {
            // init condition
            var condition = Constant.ConditionDefault;

            if(!string.IsNullOrEmpty(keyword))
            {
                condition += " AND [Name] LIKE N'%{0}%' ".FormatWith(keyword);
            }

            if(groupSymbolTypeId != null)
            {
                condition += " AND [GroupSymbolId] = {0}".FormatWith(groupSymbolTypeId);
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
            return hr_TimeSheetWorkShiftTemplateServices.GetAll(condition, order, limit).Select(r => new TimeSheetWorkShiftTemplateModel(r)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="isDeleted"></param>
        /// <param name="groupSymbolTypeId"></param>
        /// <param name="symbolId"></param>
        /// <param name="order"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<TimeSheetWorkShiftTemplateModel> GetPaging(string keyword, bool? isDeleted, int? groupSymbolTypeId, int? symbolId, DateTime? fromDate, DateTime? toDate, string order, int start, int limit)
        {
            // init condition
            var condition = Constant.ConditionDefault;

            if (!string.IsNullOrEmpty(keyword))
            {
                condition += " AND ( [Name] LIKE N'%{0}%' ) ".FormatWith(keyword);
            }

            if (groupSymbolTypeId != null)
            {
                condition += " AND [GroupSymbolId] = {0}".FormatWith(groupSymbolTypeId);
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

            var result = hr_TimeSheetWorkShiftTemplateServices.GetPaging(condition, order, start, limit);

            var listModels = new List<TimeSheetWorkShiftTemplateModel>();

            if (result.Data.Count > 0)
            {
                listModels.AddRange(result.Data.Select(record => new TimeSheetWorkShiftTemplateModel(record)));
            }
                            
            return new PageResult<TimeSheetWorkShiftTemplateModel>(result.Total, listModels);
        }        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TimeSheetWorkShiftTemplateModel GetById(int id)
        {
            var record = hr_TimeSheetWorkShiftTemplateServices.GetById(id);
            return new TimeSheetWorkShiftTemplateModel(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TimeSheetWorkShiftTemplateModel Create(TimeSheetWorkShiftTemplateModel model)
        {
            var entity = new hr_TimeSheetWorkShiftTemplate();
            model.FillEntity(ref entity);

            return new TimeSheetWorkShiftTemplateModel(hr_TimeSheetWorkShiftTemplateServices.Create(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void Update(TimeSheetWorkShiftTemplateModel model)
        {
            var record = hr_TimeSheetWorkShiftTemplateServices.GetById(model.Id);
            if (record == null) return;
            //Edit data
            model.FillEntity(ref record);
            record.EditedDate = DateTime.Now;

            //update
            hr_TimeSheetWorkShiftTemplateServices.Update(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void Delete(int id)
        {
            var entity = hr_TimeSheetWorkShiftTemplateServices.GetById(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
            }
            //update
            hr_TimeSheetWorkShiftTemplateServices.Update(entity); 
        }

    }
}
