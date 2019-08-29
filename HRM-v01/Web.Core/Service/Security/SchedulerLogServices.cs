using System;
using System.Collections.Generic;
using Web.Core.Object.Security;

namespace Web.Core.Service.Security
{
    public class SchedulerLogServices:BaseServices<SchedulerLog>
    {
        /// <summary>
        /// Get All Scheduler Log
        /// </summary>
        /// <param name="schedulerHistoryId"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<SchedulerLog> GetAll(int? schedulerHistoryId, string order, int limit)
        {
            var condition = "1=1";
            if (schedulerHistoryId != null)
            {
                condition += " AND [SchedulerHistoryId] = {0}".FormatWith(schedulerHistoryId);
            }
            if (string.IsNullOrEmpty(order))
                order = "CreatedDate ASC";
            return GetAll(condition, order, limit);
        }

        /// <summary>
        /// Get Paging Scheduler Log
        /// </summary>
        /// <param name="schedulerHistoryId"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PageResult<SchedulerLog> GetPaging(int? schedulerHistoryId, string order, int pageIndex, int pageSize)
        {
            var condition = "1=1";
            if (schedulerHistoryId != null)
            {
                condition += " AND [SchedulerHistoryId] = {0}".FormatWith(schedulerHistoryId);
            }
            if (string.IsNullOrEmpty(order))
                order = "CreatedDate ASC";
            return GetPaging(condition, order, pageIndex, pageSize);
        }

        /// <summary>
        /// Create Scheduler Log
        /// </summary>
        /// <param name="schedulerHistoryId"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static SchedulerLog Create(int schedulerHistoryId, string description)
        {
            // init object
            var schedulerLog = new SchedulerLog
            {
                SchedulerHistoryId = schedulerHistoryId,
                Description = description,
                CreatedDate = DateTime.Now
            };
            Create(schedulerLog);
            // return
            return schedulerLog;
        }

        /// <summary>
        /// Update Schedule log
        /// </summary>
        /// <param name="id"></param>
        /// <param name="schedulerHistoryId"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public SchedulerLog Update(int id, int schedulerHistoryId, string description)
        {
            // get data from db
            var schedulerLog = GetById(id);
            // has valid data
            if (schedulerLog == null) return null;
            // set new value
            schedulerLog.SchedulerHistoryId = schedulerHistoryId;
            schedulerLog.Description = description;
            Update(schedulerLog);
            // return
            return schedulerLog;
        }
    }
}
