using System;
using System.Collections.Generic;
using Web.Core.Object.Security;

namespace Web.Core.Service.Security
{
    public class SchedulerHistoryServices : BaseServices<SchedulerHistory>
    {
        /// <summary>
        /// Get All
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="schedulerId"></param>
        /// <param name="schedulerTypeId"></param>
        /// <param name="hasError"></param>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<SchedulerHistory> GetAll(string keyword, int? schedulerId, int? schedulerTypeId, bool? hasError,
            DateTime? fromTime, DateTime? toTime, string order, int limit)
        {
            var condition = "1=1";
            if (!string.IsNullOrEmpty(keyword))
            {
                condition += " AND [Description] LIKE N'%{0}%'".FormatWith(keyword);
            }
            if (schedulerId != null)
            {
                condition += " AND [SchedulerId] = {0}".FormatWith(schedulerId);
            }
            //if (schedulerTypeId!=null)
            //{
            //    condition += " AND [SchedulerTypeId] = {0}".FormatWith(schedulerTypeId);
            //}
            if (hasError != null)
            {
                condition += " AND [HasError] = {0}".FormatWith(hasError);
            }
            if (fromTime != null)
            {
                condition += " AND [StartTime] >= '{0}'".FormatWith(fromTime);
            }
            if (toTime != null)
            {
                condition += " AND [StartTime] < '{0}'".FormatWith(toTime);
            }
            if (string.IsNullOrEmpty(order))
            {
                order = "StartTime";
            }
            return GetAll(condition, order, limit);
        }

        /// <summary>
        /// Get Paging
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="schedulerId"></param>
        /// <param name="schedulerTypeId"></param>
        /// <param name="hasError"></param>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PageResult<SchedulerHistory> GetPaging(string keyword, int? schedulerId, int? schedulerTypeId,
            bool? hasError, DateTime? fromTime, DateTime? toTime, string order, int pageIndex, int pageSize)
        {
            var condition = "1=1";
            if (!string.IsNullOrEmpty(keyword))
            {
                condition += " AND [Description] LIKE N'%{0}%'".FormatWith(keyword);
            }
            if (schedulerId != null)
            {
                condition += " AND [SchedulerId] = {0}".FormatWith(schedulerId);
            }
            
            if (hasError != null)
            {
                condition += " AND [HasError] = {0}".FormatWith(hasError);
            }
            if (fromTime != null)
            {
                condition += " AND [StartTime] >= '{0}'".FormatWith(fromTime);
            }
            if (toTime != null)
            {
                condition += " AND [StartTime] < '{0}'".FormatWith(toTime);
            }
            if (string.IsNullOrEmpty(order))
            {
                order = " [StartTime] DESC ";
            }
            return GetPaging(condition, order, pageIndex, pageSize);
        }

        /// <summary>
        /// Create scheduler
        /// </summary>
        /// <param name="schedulerId"></param>
        /// <param name="description"></param>
        /// <param name="hasError"></param>
        /// <param name="errorMessage"></param>
        /// <param name="errorDescription"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static SchedulerHistory Create(int schedulerId, string description, bool hasError, string errorMessage, string errorDescription, DateTime startTime, DateTime endTime)
        {
            // init object
            var schedulerHistory = new SchedulerHistory
            {
                SchedulerId = schedulerId,
                Description = description,
                HasError = hasError,
                ErrorMessage = errorMessage,
                ErrorDescription = errorDescription,
                StartTime = startTime,
                EndTime = endTime
            };
            Create(schedulerHistory);
            // return
            return schedulerHistory;
        }

        /// <summary>
        /// Update scheduler
        /// </summary>
        /// <param name="id"></param>
        /// <param name="schedulerId"></param>
        /// <param name="description"></param>
        /// <param name="hasError"></param>
        /// <param name="errorMessage"></param>
        /// <param name="errorDescription"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public SchedulerHistory Update(int id, int schedulerId, string description, bool hasError, string errorMessage, string errorDescription, DateTime startTime, DateTime endTime)
        {
            // get data from db
            var schedulerHistory = GetById(id);
            // has valid data
            if (schedulerHistory == null) return null;
            // set new value
            schedulerHistory.SchedulerId = schedulerId;
            schedulerHistory.Description = description;
            schedulerHistory.HasError = hasError;
            schedulerHistory.ErrorMessage = errorMessage;
            schedulerHistory.ErrorDescription = errorDescription;
            schedulerHistory.StartTime = startTime;
            schedulerHistory.EndTime = endTime;
            Update(schedulerHistory);
            // return
            return schedulerHistory;
        }
    }
}
