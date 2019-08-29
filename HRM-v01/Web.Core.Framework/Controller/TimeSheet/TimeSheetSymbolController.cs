using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework.Controller.TimeSheet
{
    public class TimeSheetSymbolController
    {
        private const string ConditionDefault = @" 1=1 ";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="typeWork"></param>
        /// <param name="groupSymbolId"></param>
        /// <param name="status"></param>
        /// <param name="isDelete"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<TimeSheetSymbolModel> GetAll(string keyword, int? typeWork, int? groupSymbolId, TimeSheetStatus? status, bool? isDelete, string order, int start, int limit)
        {
            var condition = ConditionDefault;
            if (!string.IsNullOrEmpty(keyword))
            {
                condition += " AND [Code] LIKE N'%{0}%' OR [Name] LIKE N'%{0}%'".FormatWith(keyword);
            }

            if (typeWork != null)
            {
                condition += " AND [TypeWork] = {0}".FormatWith(typeWork);
            }

            if (isDelete != null)
            {
                condition += " AND [IsDeleted] = {0}".FormatWith((bool)isDelete ? "1" : "0");
            }

            if (groupSymbolId != null)
            {
                condition += " AND [GroupSymbolId] = {0}".FormatWith(groupSymbolId);
            }

            if (status != null)
            {
                condition += " AND [Status] = '{0}'".FormatWith(status.Value);
            }

            return hr_TimeSheetSymbolServices.GetAll(condition, order, limit).Select(r => new TimeSheetSymbolModel(r)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="typeWork"></param>
        /// <param name="groupSymbolId"></param>
        /// <param name="group"></param>
        /// <param name="status"></param>
        /// <param name="isDelete"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<TimeSheetSymbolModel> GetPaging(string keyword, int? typeWork, int? groupSymbolId, string group, TimeSheetStatus? status, bool? isDelete, string order, int start, int limit)
        {
            var condition = ConditionDefault;
            if (!string.IsNullOrEmpty(keyword))
            {
                condition += " AND [Code] LIKE N'%{0}%' OR [Name] LIKE N'%{0}%'".FormatWith(keyword);
            }

            if (typeWork != null)
            {
                condition += " AND [TypeWork] = {0}".FormatWith(typeWork);
            }

            if (isDelete != null)
            {
                condition += " AND [IsDeleted] = {0}".FormatWith((bool)isDelete ? "1" : "0");
            }

            if (groupSymbolId != null)
            {
                condition += " AND [GroupSymbolId] = {0}".FormatWith(groupSymbolId);
            }

            if (status != null)
            {
                condition += " AND [Status] = '{0}'".FormatWith(status.Value);
            }

            if (!string.IsNullOrEmpty(group))
            {
                condition += "AND (SELECT [Group] FROM hr_TimeSheetGroupSymbol Where hr_TimeSheetSymbol.GroupSymbolId = hr_TimeSheetGroupSymbol.Id) LIKE N'%{0}%'".FormatWith(group);
            }
            var result = hr_TimeSheetSymbolServices.GetPaging(condition, order, start, limit);

            return new PageResult<TimeSheetSymbolModel>(result.Total, result.Data.Select(r => new TimeSheetSymbolModel(r)).ToList());
        }

        /// <summary>
        /// Get time sheet symbol by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TimeSheetSymbolModel GetById(int id)
        {
            var entity = hr_TimeSheetSymbolServices.GetById(id);
            return entity != null ? new TimeSheetSymbolModel(entity) : null;
        }

        /// <summary>
        /// Get time sheet symbol by code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="groupSymbolId"></param>
        /// <returns></returns>
        public static TimeSheetSymbolModel GetByCondition(string code, int? groupSymbolId)
        {
            var condition = Constant.ConditionDefault;
            if (!string.IsNullOrEmpty(code))
            {
                condition = " [Code] = '{0}' ".FormatWith(code);
            }
            
            if (groupSymbolId != null)
            {
                condition += " AND [GroupSymbolId] = {0}".FormatWith(groupSymbolId);
            }
            var entity = hr_TimeSheetSymbolServices.GetByCondition(condition);
            return entity != null ? new TimeSheetSymbolModel(entity) : null;
        }

        /// <summary>
        /// Tạo mới
        /// </summary>
        /// <param name="model"></param>
        public static TimeSheetSymbolModel Create(TimeSheetSymbolModel model)
        {
            var existsEntity = GetByCondition(model.Code, null);
            if (existsEntity != null) return null;
            var entity = new hr_TimeSheetSymbol();
            model.FillEntity(ref entity);
            return new TimeSheetSymbolModel(hr_TimeSheetSymbolServices.Create(entity));
        }

        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <param name="model"></param>
        public static TimeSheetSymbolModel Update(TimeSheetSymbolModel model)
        {
            var existsEntity = GetByCondition(model.Code, null);
            if (existsEntity != null && existsEntity.Id != model.Id) return null;
            var entity = new hr_TimeSheetSymbol();
            model.FillEntity(ref entity);
            entity.EditedDate = DateTime.Now;
            return new TimeSheetSymbolModel(hr_TimeSheetSymbolServices.Update(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public static TimeSheetSymbolModel Delete(int id)
        {
            var model = GetById(id);
            if (model == null) return null;
            model.IsDeleted = true;
            return Update(model);
        }
    }
}
