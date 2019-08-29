using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core;

namespace Web.Core.Framework.SQLAdapter
{
    public class HRMTrainingAdapter
    {
        private const string NationName = "%Việt Nam%";

        /// <summary>
        /// Báo cáo danh sách được cử đi đào tạo
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeTraining(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += "IF OBJECT_ID('tempdb..#tmpA') IS NOT NULL DROP Table #tmpA" +
                   " SELECT rc.EmployeeCode," +
                   " rc.FullName," +
                   " id.Name AS IndustryName," +
                   " dp.Name AS DepartmentName," +
                   " rc.DepartmentId," +
                   " th.StartDate," +
                   " th.EndDate" +
                   " INTO #tmpA" +
                   " FROM hr_TrainingHistory th" +
                   " LEFT JOIN hr_Record rc" +
                   " ON th.RecordId = rc.Id" +
                   " LEFT JOIN cat_Industry id" +
                   " ON rc.IndustryId = id.Id" +
                   " LEFT JOIN cat_Department dp" +
                   " ON rc.DepartmentId = dp.Id" +
                   " WHERE 1 = 1";
            if (!string.IsNullOrEmpty(department))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
            }
            if (!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND th.StartDate >= '{0}'".FormatWith(fromDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                sql += " AND th.StartDate <= '{0}'".FormatWith(toDate);
            }
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += " SELECT* FROM #tmpA";
            return sql;
        }

        /// <summary>
        /// Báo cáo danh sách đi công tác
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeOnsite(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += "IF OBJECT_ID('tempdb..#tmpA') IS NOT NULL DROP Table #tmpA" +
                   " SELECT rc.EmployeeCode," +
                   " rc.FullName," +
                   " na.Name AS NationName," +
                   " dp.Name AS DepartmentName," +
                   " rc.DepartmentId," +
                   " ga.StartDate," +
                   " ga.EndDate" +
                   " INTO #tmpA" +
                   " FROM hr_GoAboard ga" +
                   " LEFT JOIN hr_Record rc" +
                   " ON ga.RecordId = rc.Id" +
                   " LEFT JOIN cat_Department dp" +
                   " ON rc.DepartmentId = dp.Id" +
                   " LEFT JOIN cat_Nation na" +
                   " ON ga.NationId = na.Id" +
                   " WHERE 1 = 1 AND NationId not in (SELECT Id FROM cat_Nation WHERE Name Like N'{0}')".FormatWith(NationName);
            if (!string.IsNullOrEmpty(department))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
            }
            if (!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND ga.StartDate >= '{0}'".FormatWith(fromDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                sql += " AND ga.StartDate <= '{0}'".FormatWith(toDate);
            }
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += "SELECT* FROM #tmpA";
            return sql;
        }
    }    
}
