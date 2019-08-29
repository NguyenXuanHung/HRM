using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework
{
    public class AnnualLeaveHistoryController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="recordIds"></param>
        /// <param name="timeSheetEventId"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<AnnualLeaveHistoryModel> GetAll(string keyword, string recordIds, int? timeSheetEventId, bool? isDeleted, string order, int? limit)
        {
            var condition = GetCondition(keyword, recordIds, timeSheetEventId, isDeleted);

            return hr_AnnualLeaveHistoryServices.GetAll(condition, order, limit).Select(r => new AnnualLeaveHistoryModel(r)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="recordIds"></param>
        /// <param name="timeSheetEventId"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<AnnualLeaveHistoryModel> GetPaging(string keyword, string recordIds, int? timeSheetEventId, bool? isDeleted, string order,
            int start, int limit)
        {
            var condition = GetCondition(keyword, recordIds, timeSheetEventId, isDeleted);

            var result = hr_AnnualLeaveHistoryServices.GetPaging(condition, order, start, limit);

            return new PageResult<AnnualLeaveHistoryModel>(result.Total, result.Data.Select(r => new AnnualLeaveHistoryModel(r)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="recordIds"></param>
        /// <param name="timeSheetEventId"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        private static string GetCondition(string keyword, string recordIds, int? timeSheetEventId, bool? isDeleted)
        {
            var condition = Constant.ConditionDefault;
            if (!string.IsNullOrEmpty(keyword))
                condition += "";

            if (!string.IsNullOrEmpty(recordIds))
                condition += " AND [RecordId] IN ({0})".FormatWith(recordIds);
            
            if (timeSheetEventId != null)
                condition += " AND [TimeSheetEventId] = {0}".FormatWith(timeSheetEventId);

            if (isDeleted != null)
                condition += " AND [IsDeleted] = {0}".FormatWith((bool) isDeleted ? 1 : 0);
            return condition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static AnnualLeaveHistoryModel GetById(int id)
        {
            var entity = hr_AnnualLeaveHistoryServices.GetById(id);
            return entity != null ? new AnnualLeaveHistoryModel(entity) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static AnnualLeaveHistoryModel Create(AnnualLeaveHistoryModel model)
        {
            var entity = new hr_AnnualLeaveHistory();
            model.FillEntity(ref entity);
            return new AnnualLeaveHistoryModel(hr_AnnualLeaveHistoryServices.Create(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static AnnualLeaveHistoryModel Update(AnnualLeaveHistoryModel model)
        {
            var entity = hr_AnnualLeaveHistoryServices.GetById(model.Id);
            if (entity == null) return null;
            //set props
            model.FillEntity(ref entity);
           
            return new AnnualLeaveHistoryModel(hr_AnnualLeaveHistoryServices.Update(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static AnnualLeaveHistoryModel Delete(int id)
        {
            var model = GetById(id);
            if (model == null) return null;
            model.IsDeleted = true;
            return Update(model);
        }
    }
}
