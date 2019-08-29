using System;
using System.Collections.Generic;
using System.Globalization;
using Web.Core.Object.Security;

namespace Web.Core.Service.Security
{
    public class SchedulerServices : BaseServices<Scheduler>
    {
        private const string ConditionDefault = @" 1=1 ";
        private const string SchedulerTimeSheet = "TimeSheet";
        /// <summary>
        /// Get Expired
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<Scheduler> GetExpired(DateTime fromTime, DateTime toTime, string order, int limit)
        {
            // init condition
            var condition = ConditionDefault;
            condition += " AND [Status] == {0}".FormatWith(SchedulerStatus.Running);
            condition += " AND [Enabled] == 1";
            condition += " AND [ExpiredTime] IS NOT NULL";
            condition += " AND [ExpiredTime] >= {0}".FormatWith(fromTime.ToString(CultureInfo.InvariantCulture));
            condition += " AND [ExpiredTime] < {0}".FormatWith(toTime.ToString(CultureInfo.InvariantCulture));
            // order
            if (string.IsNullOrEmpty(order))
                order = "NextRunTime";
            return GetAll(condition, order, limit);
        }

        /// <summary>
        /// Get All Scheduler
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
        public static List<Scheduler> GetAll(string keyword, int? type, SchedulerRepeatType? repeatType,
            SchedulerStatus? status, bool? enabled, SchedulerScope? scope, DateTime? fromTime, DateTime? toTime,
            string order, int limit)
        {
            var condition = "1=1";
            if (!string.IsNullOrEmpty(keyword))
            {
                condition += " AND [Name] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%'".FormatWith(keyword.EscapeQuote());
            }
            if (type != null)
            {
                condition += " AND [SchedulerTypeId] ='{0}'".FormatWith(type.Value);
            }
            if (repeatType != null)
            {
                condition += " AND [RepeatType] ='{0}'".FormatWith((int)repeatType.Value);
            }
            if (status != null)
            {
                condition += " AND [Status]='{0}'".FormatWith((int)status.Value);
            }
            if (enabled != null)
            {
                condition += " AND [Enabled]='{0}'".FormatWith(enabled.Value);
            }
            if (scope != null)
            {
                condition += " AND [Scope] ='{0}'".FormatWith((int)scope.Value);
            }
            condition += " AND [NextRunTime] IS NOT NULL";
            if (fromTime != null)
            {
                condition += " AND [NextRunTime]>='{0}'".FormatWith(fromTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (toTime != null)
            {
                condition += " AND [NextRunTime]<'{0}'".FormatWith(toTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (string.IsNullOrEmpty(order))
            {
                order = "NextRunTime";
            }
            return GetAll(condition, order, limit);
        }

        /// <summary>
        /// Get Paging 
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
        public static PageResult<Scheduler> GetPaging(string keyword, int? type, SchedulerRepeatType? repeatType,
            SchedulerStatus? status, bool? enabled, SchedulerScope? scope, DateTime? fromTime, DateTime? toTime, 
            string order, int start, int pageSize)
        {
            var condition = "1=1";
            if (!string.IsNullOrEmpty(keyword))
            {
                condition += " AND [Name] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%'".FormatWith(keyword.EscapeQuote());
            }
            if (type != null)
            {
                condition += " AND [SchedulerTypeId] ='{0}'".FormatWith(type.Value);
            }
            if (repeatType != null)
            {
                condition += " AND [RepeatType] ='{0}'".FormatWith((int)repeatType.Value);
            }
            if (status != null)
            {
                condition += " AND [Status]='{0}'".FormatWith((int)status.Value);
            }
            if (enabled != null)
            {
                condition += " AND [Enabled]='{0}'".FormatWith(enabled.Value);
            }
            if (scope != null)
            {
                condition += " AND [Scope] ='{0}'".FormatWith((int)scope.Value);
            }
            condition += " AND [NextRunTime] IS NOT NULL";
            if (fromTime != null)
            {
                condition += " AND [NextRunTime]>='{0}'".FormatWith(fromTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (toTime != null)
            {
                condition += " AND [NextRunTime]<'{0}'".FormatWith(toTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (string.IsNullOrEmpty(order))
            {
                order = "NextRunTime";
            }
            return GetPaging(condition, order, start, pageSize);
        }


        /// <summary>
        /// Create Scheduler
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="type"></param>
        /// <param name="repeatType"></param>
        /// <param name="intervalTime"></param>
        /// <param name="expiredAfter"></param>
        /// <param name="status"></param>
        /// <param name="enabled"></param>
        /// <param name="scope"></param>
        /// <param name="lastRunTime"></param>
        /// <param name="nextRunTime"></param>
        /// <param name="expiredTime"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static Scheduler Create(string name, string description, int type, SchedulerRepeatType repeatType, 
            int intervalTime, int expiredAfter, SchedulerStatus status, bool enabled, SchedulerScope scope, 
            DateTime? lastRunTime, DateTime? nextRunTime, DateTime? expiredTime, string arguments)
        {
            var obj = new Scheduler
            {
                Name = name,
                Description = description,
                SchedulerTypeId = type,
                RepeatType = repeatType,
                IntervalTime = intervalTime,
                Status = status,
                Enabled = enabled,
                Scope = scope,
                ExpiredAfter = expiredAfter,
                LastRunTime = lastRunTime,
                NextRunTime = nextRunTime,
                ExpiredTime = expiredTime,
                Arguments = arguments
            };
            return Create(obj);
        }

        /// <summary>
        /// Update scheduler
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static Scheduler Update(int id, SchedulerStatus status)
        {
            // get data from db
            var scheduler = GetById(id);
            // has valid data
            if (scheduler == null) return null;
            // set new value
            scheduler.Status = status;
            // update
            Update(scheduler);
            // return
            return scheduler;
        }

        /// <summary>
        /// Update scheduler
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="lastRunTime"></param>
        /// <param name="nextRunTime"></param>
        /// <param name="expiredTime"></param>
        /// <returns></returns>
        public static Scheduler Update(int id, SchedulerStatus status, DateTime? lastRunTime, DateTime? nextRunTime, DateTime? expiredTime)
        {
            var scheduler = GetById(id);
            if (scheduler == null) return null;
            scheduler.Status = status;
            if (lastRunTime != null)
            {
                scheduler.LastRunTime = lastRunTime;
            }
            if (nextRunTime != null)
            {
                scheduler.NextRunTime = nextRunTime;
            }
            if (expiredTime != null)
            {
                scheduler.ExpiredTime = expiredTime;
            }
            Update(scheduler);

            return scheduler;
        }

        /// <summary>
        /// Update scheduler
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <param name="repeatType"></param>
        /// <param name="intervalTime"></param>
        /// <param name="expiredAfter"></param>
        /// <param name="status"></param>
        /// <param name="enabled"></param>
        /// <param name="scope"></param>
        /// <param name="lastRunTime"></param>
        /// <param name="nextRunTime"></param>
        /// <param name="expiredTime"></param>
        /// <returns></returns>
        public static Scheduler Update(int id, string name, string description, int type, string args, SchedulerRepeatType repeatType,
            int intervalTime, int expiredAfter, SchedulerStatus status, bool enabled, SchedulerScope scope, DateTime? lastRunTime, DateTime? nextRunTime, DateTime? expiredTime)
        {
            // get data from db
            var scheduler = GetById(id);
            // has valid data
            if (scheduler == null) return null;
            // set new value
            scheduler.Name = name;
            scheduler.Description = description;
            scheduler.SchedulerTypeId = type;
            scheduler.Arguments = args;
            scheduler.RepeatType = repeatType;
            scheduler.IntervalTime = intervalTime;
            scheduler.ExpiredAfter = expiredAfter;
            scheduler.Status = status;
            scheduler.Enabled = enabled;
            scheduler.Scope = scope;
            scheduler.LastRunTime = lastRunTime;
            scheduler.NextRunTime = nextRunTime;
            scheduler.ExpiredTime = expiredTime;
            // update
            Update(scheduler);
            // return
            return scheduler;
        }

        public static Scheduler GetSchedulerByName()
        {
            // init condition
            var condition = ConditionDefault;
            condition += " AND [Name] = '{0}'".FormatWith(SchedulerTimeSheet);
            return GetByCondition(condition);
        }

    }

}
