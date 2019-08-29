using Web.Core;

/// <summary>
/// Summary description for SQLBusinessTrainingAdapter
/// </summary>
public class SQLBusinessTrainingAdapter
{
    private const string NationName = "%Việt Nam%";

    public SQLBusinessTrainingAdapter()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string GetStore_BusinessOnsiteTraining(string department, string fromDate, string toDate, string condition)
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

    public static string GetStore_BusinessForeignTraining(string department, string fromDate, string toDate, string condition)
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
               " WHERE 1 = 1 AND NationId != (SELECT Id FROM cat_Nation WHERE Name Like N'{0}')".FormatWith(NationName);
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