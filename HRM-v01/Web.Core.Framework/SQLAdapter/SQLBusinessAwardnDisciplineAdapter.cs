using Web.Core;

/// <summary>
/// Summary description for SQLBusinessAwardnDisciplineAdapter
/// </summary>
public class SQLBusinessAwardnDisciplineAdapter
{
    private const string ConstBusinessType = "DanhHieuThiDua";
    public SQLBusinessAwardnDisciplineAdapter()
    {

    }

    public static string GetStore_BusinessAward(string department, string fromDate, string toDate, string condition)
    {
        var sql = string.Empty;
        sql += " SELECT	"  +
               " rc.EmployeeCode,	"  +
               " rc.FullName,	"  +
               " rc.DepartmentId,	"  +
               " rc.ParticipationDate," +
               " cp.Name AS PositionName,	"  +
               " dv.Name AS DepartmentName,	"  +
               " bh.EmulationTitle as Reason,	"  +
               " bh.DecisionDate	"  +
               " FROM hr_BusinessHistory bh	"  +
               " LEFT JOIN hr_Record rc  ON bh.RecordId = rc.Id	"  +
               " LEFT JOIN cat_Department dv ON dv.Id = rc.DepartmentId	"  +
               " LEFT JOIN cat_Position cp ON cp.Id = rc.PositionId	"  +
               " WHERE bh.BusinessType = N'{0}'	".FormatWith(ConstBusinessType) ;

        if (!string.IsNullOrEmpty(department))
        {
            sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
        }
        if (!string.IsNullOrEmpty(fromDate))
        {
            sql += " AND bh.DecisionDate >= '{0}'".FormatWith(fromDate);
        }
        if (!string.IsNullOrEmpty(toDate))
        {
            sql += " AND bh.DecisionDate <= '{0}'".FormatWith(toDate);
        }
        if (!string.IsNullOrEmpty(condition))
        {
            sql += " AND {0}".FormatWith(condition);
        }

        return sql;
    }

    public static string GetStore_BusinessDiscipline(string department, string fromDate, string toDate, string condition)
    {
        var sql = string.Empty;
        sql += "IF OBJECT_ID('tempdb..#tmpA') IS NOT NULL DROP Table #tmpA" +
               " SELECT rc.EmployeeCode," +
               " rc.FullName," +
               " po.Name AS PositionName," +
               " dp.Name AS DepartmentName," +
               " rc.ParticipationDate, " +
               " dc.Reason," +
               " cdc.Name AS FormDisciplineName," +
               " dc.DecisionDate" +
               " INTO #tmpA" +
               " FROM hr_Discipline dc" +
               " LEFT JOIN hr_Record rc" +
               " ON dc.RecordId = rc.Id" +
               " LEFT JOIN cat_Position po" +
               " ON rc.PositionId = po.Id" +
               " LEFT JOIN cat_Department dp" +
               " ON rc.DepartmentId = dp.Id" +
               " LEFT JOIN cat_Discipline cdc" +
               " ON dc.FormDisciplineId = cdc.Id" +
               " WHERE  1 = 1";
        if (!string.IsNullOrEmpty(department))
        {
            sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
        }
        if (!string.IsNullOrEmpty(fromDate))
        {
            sql += " AND dc.DecisionDate >= '{0}'".FormatWith(fromDate);
        }
        if (!string.IsNullOrEmpty(toDate))
        {
            sql += " AND dc.DecisionDate <= '{0}'".FormatWith(toDate);
        }
        if (!string.IsNullOrEmpty(condition))
        {
            sql += " AND {0}".FormatWith(condition);
        }
        sql += " SELECT* FROM #tmpA";
        return sql;
    }

    /// <summary>
    /// Danh sách cán bộ được khen thưởng
    /// </summary>
    /// <param name="department"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static string EmployeeReward(string department, string condition)
    {
        var sql = string.Empty;
        //create SQL
        // xoa bang #tmpB neu da ton tai
        sql += "IF OBJECT_ID('tempdb..#tmpB') IS NOT NULL DROP Table #tmpB ";
        sql += " SELECT " +
               " '' as 'STT', " +
               " h.DepartmentId as 'DepartmentId', " +
               " dd.Name as 'GROUP', " +
               " h.EmployeeCode as 'EmployeeCode', " +
               " h.FullName as 'FullName', " +
               " dd.Name as 'DepartmentName', " +
               " dc.Name as 'PositionName', " +
               " hk.DecisionNumber as 'DecisionNumber', " +
               " hk.EffectiveDate as 'EffectiveDate', " +
               " kt.Name as 'FormRewardName', " +
               " hk.MoneyAmount as 'MoneyAmount', " +
               " hk.SourceDepartment as 'SourceDepartment', " +
               " hk.DecisionMaker as 'DecisionMaker', " +
               " hk.MakerPosition as 'MakerPosition' " +
               " INTO #tmpB " +
               " FROM hr_Reward hk	 " +
               " LEFT JOIN hr_Record h " +
               " 	ON hk.RecordId = H.Id " +
               " LEFT JOIN cat_Position dc " +
               " 	ON dc.Id = h.PositionId " +
               " LEFT JOIN cat_Reward kt " +
               " 	ON kt.Id = hk.FormRewardId " +
               " LEFT JOIN cat_Department dd " +
               " 	ON dd.Id = h.DepartmentId " +
               " LEFT JOIN cat_LevelRewardDiscipline ckt " +
               " 	ON ckt.ID = hk.LevelRewardId " +
               " WHERE h.DepartmentId IN ({0}) ".FormatWith(department);
        if (!string.IsNullOrEmpty(condition))
        {
            sql += " AND {0}".FormatWith(condition);
        }
        sql += " SELECT * FROM #tmpB ";

        return sql;
    }
}