using System;
using System.Linq;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework.SQLAdapter
{
    public class SQLTimeTrackingAdapter
    {
        private const string DefaultSymbol = @"D";
        private const string OverTimeSymbolGroup = "ThemGio";

        #region TimeKeeping

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="timeSheetId"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetStore_GetTimeSheetEventsByTimeSheetId(string searchKey, int timeSheetId, int? start, int? limit, string type)
        {
            var sql = "	select te.TimeSheetId, " +
                      " te.Id, " +
                      " te.Description, " +
                      " te.WorkConvert, " +
                      " te.MoneyConvert, " +
                      " te.Symbol, " +
                      " te.SymbolDisplay, " +
                      " te.StatusId, " +
                      " te.CreatedDate, " +
                      " (SELECT cg.Name " +
                      "    FROM hr_TimeSheetGroupSymbol cg " +
                      "    WHERE cg.[Group] = te.TypeGroup) AS GroupSymbolName " +
                      " from hr_TimeSheetEvent te " +
                      " where te.TimeSheetId = {0} ".FormatWith(timeSheetId) +
                      " and te.Symbol <> '{0}' ".FormatWith(DefaultSymbol);

            return sql;
        }

        /// <summary>
        /// danh sách nhóm ký hiệu đang sử dụng
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static string GetStore_GetListGrouSymbolUsed(string searchKey, int? start, int? limit)
        {
            var sql = " SELECT * FROM(" +
                      " SELECT ch.Id , ch.Name, ch.[Group], " +
                      " ROW_NUMBER() OVER(ORDER BY ch.[Order]) SK " +
                      " FROM hr_TimeSheetGroupSymbol ch " +
                      " WHERE 1=1 AND ch.IsInUsed = 1	";
            if(!String.IsNullOrEmpty(searchKey))
            {
                sql += " AND (ch.Name LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR ch.Description LIKE N'%{0}%' )".FormatWith(searchKey);
            }

            sql += " )#tmp					";
            if(start != null && limit != null)
            {
                sql += " WHERE #tmp.SK > {0}".FormatWith(start) +
                       " AND #tmp.SK <= {0}".FormatWith(start + limit);
            }
            return sql;
        }

        /// <summary>
        /// danh sách ký hiệu đang sử dụng
        /// </summary>
        /// <param name="groupSymbolType"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static string GetStore_GetListSymbolUsed(string groupSymbolType, string searchKey, int? start, int? limit)
        {
            var sql = " SELECT * FROM(" +
                      " SELECT ch.Id ,ch.Code, ch.Name," +
                      " ROW_NUMBER() OVER(ORDER BY ch.[Order]) SK " +
                      " FROM hr_TimeSheetSymbol ch " +
                      " WHERE 1=1 ";
            //if(!string.IsNullOrEmpty(groupSymbolType))
            //{
            //    sql += " AND [Group] = '{0}' ".FormatWith(groupSymbolType);
            //}
            if(!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND (ch.Name LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR ch.Description LIKE N'%{0}%' )".FormatWith(searchKey);
            }

            sql += " )#tmp					";
            if(start != null && limit != null)
            {
                sql += " WHERE #tmp.SK > {0}".FormatWith(start) +
                       " AND #tmp.SK <= {0}".FormatWith(start + limit);
            }
            return sql;
        }

        /// <summary>
        /// danh sách nhóm ký hiệu
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static string GetStore_GetListGrouSymbol(string searchKey, int? start, int? limit)
        {
            var sql = " SELECT * FROM(" +
                      " SELECT * , " +
                      " ROW_NUMBER() OVER(ORDER BY ch.[Order]) SK " +
                      " FROM hr_TimeSheetGroupSymbol ch " +
                      " WHERE 1=1	";
            if(!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND (ch.Name LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR ch.Description LIKE N'%{0}%' )".FormatWith(searchKey);
            }

            sql += " )#tmp					";
            if(start != null && limit != null)
            {
                sql += " WHERE #tmp.SK > {0}".FormatWith(start) +
                       " AND #tmp.SK <= {0}".FormatWith(start + limit);
            }
            return sql;
        }
        #endregion

        #region Handler Log
        /// <summary>
        /// edit query
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="logValues"></param>
        /// <returns></returns>
        public static string EditSql(string sql, string logValues)
        {
            sql += " INSERT INTO [dbo].[hr_TimeSheetLog]	 " +
                   " ([TimeSheetCode]	 " +
                   " ,[TimeString]	 " +
                   " ,[TimeDate]	 " +
                   " ,[MachineSerialNumber]	 " +
                   " ,[MachineName]	 " +
                   " ,[IPAddress]	 " +
                   " ,[LocationName]	 " +
                   " ,[CreatedDate])	 " +
                   " VALUES	{0} ".FormatWith(logValues.TrimStart(','));
            return sql;
        }


        #endregion
    }
}
