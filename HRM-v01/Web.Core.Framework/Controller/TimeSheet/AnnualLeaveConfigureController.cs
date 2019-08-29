using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for AnnualLeaveConfigureController
    /// </summary>
    public class AnnualLeaveConfigureController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="recordIds"></param>
        /// <param name="departmentIds"></param>
        /// <param name="year"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<AnnualLeaveConfigureModel> GetAll(string keyword, string recordIds, string departmentIds, int? year, bool? isDeleted, string order, int? limit)
        {
            var condition = GetCondition(keyword, recordIds, departmentIds, year, isDeleted);

            return hr_AnnualLeaveConfigureServices.GetAll(condition, order, limit)
                .Select(r => new AnnualLeaveConfigureModel(r)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="recordIds"></param>
        /// <param name="departmentIds"></param>
        /// <param name="year"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        private static string GetCondition(string keyword, string recordIds, string departmentIds, int? year, bool? isDeleted)
        {
            var condition = Constant.ConditionDefault;

            if (!string.IsNullOrEmpty(keyword))
                condition += " AND [RecordId] IN (SELECT Id FROM hr_Record WHERE [EmployeeCode] LIKE N'%{0}%' OR [FullName] LIKE N'%{0}%')"
                        .FormatWith(keyword.EscapeQuote());

            if (!string.IsNullOrEmpty(recordIds))
                condition += " AND [RecordId] IN ({0})".FormatWith(recordIds);

            //departments
            if (!string.IsNullOrEmpty(departmentIds))
            {
                condition += @" AND ([RecordId] IN (SELECT Id FROM hr_Record WHERE [DepartmentId] IN ({0})))".FormatWith(departmentIds);
            }
            
            if (isDeleted != null)
                condition += " AND [IsDeleted] = {0}".FormatWith((bool) isDeleted ? 1 : 0);

            if (year != null)
                condition += " AND [Year] = {0}".FormatWith(year);
            return condition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="recordIds"></param>
        /// <param name="departmentIds"></param>
        /// <param name="year"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<AnnualLeaveConfigureModel> GetPaging(string keyword, string recordIds, string departmentIds, int? year, bool? isDeleted, string order, int start, int limit)
        {
            //get condition
            var condition = GetCondition(keyword, recordIds, departmentIds, year, isDeleted);

            var result = hr_AnnualLeaveConfigureServices.GetPaging(condition, order, start, limit);

            return new PageResult<AnnualLeaveConfigureModel>(result.Total, result.Data.Select(r => new AnnualLeaveConfigureModel(r)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static AnnualLeaveConfigureModel GetById(int id)
        {
            var entity = hr_AnnualLeaveConfigureServices.GetById(id);
            return entity != null ? new AnnualLeaveConfigureModel(entity) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static AnnualLeaveConfigureModel Create(AnnualLeaveConfigureModel model)
        {
            var entity = new hr_AnnualLeaveConfigure();
            // fill data to entity
            model.FillEntity(ref entity);
            return new AnnualLeaveConfigureModel(hr_AnnualLeaveConfigureServices.Create(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static AnnualLeaveConfigureModel Update(AnnualLeaveConfigureModel model)
        {
            var entity = hr_AnnualLeaveConfigureServices.GetById(model.Id);
            if (entity == null) return null;
            // fill data to entity
            model.FillEntity(ref entity);
            return new AnnualLeaveConfigureModel(hr_AnnualLeaveConfigureServices.Update(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static AnnualLeaveConfigureModel Delete(int id)
        {
            var model = GetById(id);
            if (model == null) return null;
            // delete
            model.IsDeleted = true;
            return Update(model);
        }
    }
}
