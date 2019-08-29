using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for TimeSheetReportController
    /// </summary>
    public class TimeSheetReportController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TimeSheetReportModel GetById(int id)
        {
            var record = hr_TimeSheetReportServices.GetById(id);
            return new TimeSheetReportModel(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TimeSheetReportModel Create(TimeSheetReportModel model)
        {
            var entity = new hr_TimeSheetReport();
            model.FillEntity(ref entity);

            return new TimeSheetReportModel(hr_TimeSheetReportServices.Create(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TimeSheetReportModel Update(TimeSheetReportModel model)
        {
            var entity = hr_TimeSheetReportServices.GetById(model.Id);
            if (entity == null) return null;
            //Edit data
            model.FillEntity(ref entity);
            entity.EditedDate = DateTime.Now;
            return new TimeSheetReportModel(hr_TimeSheetReportServices.Update(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TimeSheetReportModel Delete(int id)
        {
            var model = GetById(id);
            if (model == null) return null;
            model.IsDeleted = true;
            return Update(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="id"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="isDeleted"></param>
        /// <param name="status"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<TimeSheetReportModel> GetAll(string keyword, int? id, DateTime? fromDate, DateTime? toDate, bool? isDeleted, TimeSheetStatus? status, string order, int start, int limit)
        {
            // init condition
            var condition = Constant.ConditionDefault;

            if (!string.IsNullOrEmpty(keyword))
            {
                condition += " AND ( [Name] LIKE N'%{0}%' ) ".FormatWith(keyword);
            }

            if (id != null)
            {
                condition += " AND [Id] = {0}".FormatWith(id);
            }

            if (fromDate.HasValue)
            {
                condition += " AND [StartDate] IS NOT NULL AND YEAR([StartDate]) = {0} AND MONTH([StartDate]) >= {1} AND DAY([StartDate]) >= {2}"
                    .FormatWith(fromDate.Value.Year, fromDate.Value.Month, fromDate.Value.Day);
            }

            if (toDate.HasValue)
            {
                condition += " AND [StartDate] IS NOT NULL AND YEAR([StartDate]) = {0} AND MONTH([StartDate]) <= {1} AND DAY([StartDate]) <= {2}"
                    .FormatWith(toDate.Value.Year, toDate.Value.Month, toDate.Value.Day);
            }

            if (isDeleted != null)
                condition += "AND [IsDeleted] = {0}".FormatWith((bool)isDeleted ? "1" : "0");

            if (status != null)
                condition += " AND [Status] = {0} ".FormatWith((int)status);
            
            return hr_TimeSheetReportServices.GetAll(condition, order, limit).Select(ts => new TimeSheetReportModel(ts)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="id"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="isDeleted"></param>
        /// <param name="status"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<TimeSheetReportModel> GetPaging(string keyword, int? id, DateTime? fromDate, DateTime? toDate, bool? isDeleted, TimeSheetStatus? status, string order, int start, int limit)
        {
            // init condition
            var condition = Constant.ConditionDefault;

            if (!string.IsNullOrEmpty(keyword))
            {
                condition += " AND ( [Name] LIKE N'%{0}%' ) ".FormatWith(keyword);
            }

            if (id != null)
            {
                condition += " AND [Id] = {0}".FormatWith(id);
            }

            if (fromDate.HasValue)
            {
                condition += " AND [StartDate] IS NOT NULL AND YEAR([StartDate]) = {0} AND MONTH([StartDate]) >= {1} AND DAY([StartDate]) >= {2}"
                    .FormatWith(fromDate.Value.Year, fromDate.Value.Month, fromDate.Value.Day);
            }

            if (toDate.HasValue)
            {
                condition += " AND [StartDate] IS NOT NULL AND YEAR([StartDate]) = {0} AND MONTH([StartDate]) <= {1} AND DAY([StartDate]) <= {2}"
                    .FormatWith(toDate.Value.Year, toDate.Value.Month, toDate.Value.Day);
            }

            if (isDeleted != null)
                condition += "AND [IsDeleted] = {0}".FormatWith((bool) isDeleted ? "1" : "0");

            if (status != null)
                condition += " AND [Status] = {0} ".FormatWith((int)status);

            var result = hr_TimeSheetReportServices.GetPaging(condition, order, start, limit);

            var listModels = new List<TimeSheetReportModel>();

            if (result.Data.Count > 0)
            {
                listModels.AddRange(result.Data.Select(record => new TimeSheetReportModel(record)));
            }
                            
            return new PageResult<TimeSheetReportModel>(result.Total, listModels);
        }        
        
    }
}
