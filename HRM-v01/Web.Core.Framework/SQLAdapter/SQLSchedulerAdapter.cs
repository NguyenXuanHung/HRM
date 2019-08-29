namespace Web.Core.Framework.Adapter
{
    /// <summary>
    /// Summary description for SQLSchedulerAdapter
    /// </summary>
    public class SQLSchedulerAdapter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="type"></param>
        /// <param name="repeatType"></param>
        /// <param name="status"></param>
        /// <param name="scope"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static string GetStore_GetScheduler(string searchKey, int? type, int? repeatType, int? status, int? scope, int? start, int? limit)
        {
            var sql = "SELECT * FROM (" +
                      "SELECT sdl.Name," +
                      " sdl.[Description]," +
                      " st.Name AS SchedulerTypeName," +
                      " sdl.RepeatType, " +
                      " sdl.Arguments," +
                      " sdl.[Status]," +
                      " sdl.[Enabled]," +
                      " sdl.Scope," +
                      " sdl.LastRunTime," +
                      " sdl.NextRunTime," +
                      " sdl.ExpiredTime," +
                      " sdl.IntervalTime," +
                      " sdl.ExpiredAfter," +
                      " ROW_NUMBER() OVER(ORDER BY NextRunTime) RN" +
                      " FROM Scheduler sdl" +
                      " LEFT JOIN SchedulerType st" +
                      " ON sdl.SchedulerTypeId = st.Id) #tmp" +
                      " WHERE 1=1";
            if(!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND Name LIKE N'%{0}%' OR [Description] LIKE N'%{0}%'".FormatWith(searchKey);
            }
            if(type != null)
            {
                sql += " AND SchedulerTypeId = {0}".FormatWith(type);
            }
            if(repeatType != null)
            {
                sql += " AND RepeatType = {0}".FormatWith(repeatType);
            }
            if(status != null)
            {
                sql += " AND [Status] = {0}".FormatWith(status);
            }
            if(scope != null)
            {
                sql += " AND [Scope] = {0}".FormatWith(scope);
            }
            if(start != null && limit != null)
            {
                sql += " AND RN > {0}".FormatWith(start) +
                       " AND RN < {0}".FormatWith(start + limit);
            }
            return sql;
        }
    }
}

