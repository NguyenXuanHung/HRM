using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.Security;
using Web.Core.Service.Security;

namespace Web.Core.Framework
{
    public class SchedulerController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SchedulerModel GetById(int id)
        {
            var entity = SchedulerServices.GetById(id);
            return new SchedulerModel(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedulerName"></param>
        /// <returns></returns>
        public static SchedulerModel GetByName(string schedulerName)
        {
            // init condition
            var condition = Constant.ConditionDefault;
            if (string.IsNullOrEmpty(schedulerName)) return null;
            condition += " AND [Name] = '{0}'".FormatWith(schedulerName);
            var entity = SchedulerServices.GetByCondition(condition);
            return entity != null ? new SchedulerModel(entity) : null;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="type"></param>
        /// <param name="repeatType"></param>
        /// <param name="status"></param>
        /// <param name="enabled"></param>
        /// <param name="scope"></param>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<SchedulerModel> GetAll(string keyword, int? type, SchedulerRepeatType? repeatType, SchedulerStatus? status, bool? enabled,
            SchedulerScope? scope, DateTime? fromTime, DateTime? toTime, string order, int limit)
        {
            var entities = SchedulerServices.GetAll(keyword, type, repeatType, status, enabled, scope, fromTime, toTime, order, limit);
            return entities.Select(e => new SchedulerModel(e)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="type"></param>
        /// <param name="repeatType"></param>
        /// <param name="status"></param>
        /// <param name="enabled"></param>
        /// <param name="scope"></param>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PageResult<SchedulerModel> GetPaging(string keyword, int? type, SchedulerRepeatType? repeatType, SchedulerStatus? status, bool? enabled,
            SchedulerScope? scope, DateTime? fromTime, DateTime? toTime, string order, int start, int pageSize)
        {
            var result = SchedulerServices.GetPaging(keyword, type, repeatType, status, enabled, scope, fromTime, toTime, order, start, pageSize);
            return new PageResult<SchedulerModel>(result.Total, result.Data.Select(e => new SchedulerModel(e)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SchedulerModel Update(SchedulerModel model)
        {
            // get entity from database
            var entity = SchedulerServices.GetById(model.Id);
            // set new properties from model
            model.FillEntity(ref entity);
            // update entity
            return new SchedulerModel(SchedulerServices.Update(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SchedulerModel Create(SchedulerModel model)
        {
            // init entity
            var entity = new Scheduler();
            // set properties from model
            model.FillEntity(ref entity);
            // insert entity
            return new SchedulerModel(SchedulerServices.Create(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(int id)
        {
            // delete entity
            return SchedulerServices.Delete(id);
        }
    }
}
