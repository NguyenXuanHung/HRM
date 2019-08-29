using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework.Controller.TimeSheet
{
    public class TimeSheetGroupSymbolController
    {
        private const string ConditionDefault = @" 1=1 ";

        /// <summary>
        /// Get time sheet group symbol by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TimeSheetGroupSymbolModel GetById(int id)
        {
            var entity = hr_TimeSheetGroupSymbolServices.GetById(id);
            return entity != null ? new TimeSheetGroupSymbolModel(entity) : null;
        }

        public static TimeSheetGroupSymbolModel GetByGroup(string group)
        {
            // check report name
            if(!string.IsNullOrEmpty(group))
            {
                var condition = "[Group]='{0}'".FormatWith(group);
                var entity = hr_TimeSheetGroupSymbolServices.GetByCondition(condition);
                return entity != null ? new TimeSheetGroupSymbolModel(entity) : null;
            }
            // invalid name
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="isDeleted"></param>
        /// <param name="group"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static List<TimeSheetGroupSymbolModel> GetAll(string keyword, TimeSheetStatus? status, bool? isDeleted, string group,
            string order, int start, int limit)
        {
            var condition = ConditionDefault;
            if (!string.IsNullOrEmpty(keyword))
                condition += " AND [Code] LIKE N'%{0}%' OR [Name] LIKE N'%{0}%'".FormatWith(keyword);

            if (status != null)
                condition += " AND [Status] = {0}".FormatWith((int)status);

            if (isDeleted != null)
                condition += " AND [IsDeleted] = {0}".FormatWith((bool)isDeleted ? "1" : "0");

            if (!string.IsNullOrEmpty(group))
                condition += " AND [Group] LIKE N'{0}'".FormatWith(group);

            return hr_TimeSheetGroupSymbolServices.GetAll(condition, order, limit).Select(r => new TimeSheetGroupSymbolModel(r)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="isDeleted"></param>
        /// <param name="group"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static PageResult<TimeSheetGroupSymbolModel> GetPaging(string keyword, TimeSheetStatus? status, bool? isDeleted, string group,
            string order, int start, int limit)
        {
            var condition = ConditionDefault;
            if (!string.IsNullOrEmpty(keyword))
                condition += " AND [Code] LIKE N'%{0}%' OR [Name] LIKE N'%{0}%'".FormatWith(keyword);

            if (status != null)
                condition += " AND [Status] = {0}".FormatWith((int)status);

            if (isDeleted != null)
                condition += " AND [IsDeleted] = {0}".FormatWith((bool)isDeleted ? "1" : "0");

            if (!string.IsNullOrEmpty(group))
                condition += " AND [Group] LIKE N'{0}'".FormatWith(group);

            var result = hr_TimeSheetGroupSymbolServices.GetPaging(condition, order, start, limit);

            return new PageResult<TimeSheetGroupSymbolModel>(result.Total, result.Data.Select(r => new TimeSheetGroupSymbolModel(r)).ToList());
        }

        /// <summary>
        /// Create 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void Create(TimeSheetGroupSymbolModel model)
        {
            var entity = new hr_TimeSheetGroupSymbol();
            model.FillEntity(ref entity);
            hr_TimeSheetGroupSymbolServices.Create(entity);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void Update(TimeSheetGroupSymbolModel model)
        {
            var entity = hr_TimeSheetGroupSymbolServices.GetById(model.Id);
            if (entity == null) return;
            model.FillEntity(ref entity);
            entity.EditedDate = DateTime.Now;
            hr_TimeSheetGroupSymbolServices.Update(entity);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void Delete(int id)
        {
            hr_TimeSheetGroupSymbolServices.Delete(id);
        }

        /// <summary>
        /// Delete many
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static void Delete(List<int> ids)
        {
            if (ids == null) return;
            foreach (var id in ids)
            {
                hr_TimeSheetGroupSymbolServices.Delete(id);
            }
        }

      
        public static TimeSheetGroupSymbolModel GetByCondition(string keyword, string group, bool? isDeleted, TimeSheetStatus? status)
        {
            var condition = ConditionDefault;
            if(!string.IsNullOrEmpty(keyword))
                condition += " AND [Code] LIKE N'%{0}%' OR [Name] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%'".FormatWith(keyword.EscapeQuote());

            if(status != null)
                condition += " AND [Status] = {0}".FormatWith((int)status);

            if(isDeleted != null)
                condition += " AND [IsDeleted] = {0}".FormatWith((bool)isDeleted ? "1" : "0");

            if(!string.IsNullOrEmpty(group))
                condition += " AND [Group] LIKE N'{0}'".FormatWith(group);
            var entity = hr_TimeSheetGroupSymbolServices.GetByCondition(condition);
            return entity != null ? new TimeSheetGroupSymbolModel(entity) : null;
        }

    }
}
