using System.Linq;
using Web.Core;
using Web.Core.Service.Catalog;

/// <summary>
/// Summary description for SQLBusinessSalaryAdapter
/// </summary>
public class SQLBusinessSalaryAdapter
{
    public SQLBusinessSalaryAdapter()
    {
    }
    /// <summary>
    /// Mã số thuế
    /// </summary>
    /// <param name="department"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static string GetStore_BusinessPersonalTax(string department, string condition,string fromDate,string toDate)
    {
        var sql = string.Empty;
        sql += "IF OBJECT_ID('tempdb..#tmpA') IS NOT NULL DROP Table #tmpA" +
               " SELECT rc.EmployeeCode," +
               " rc.FullName," +
               " rc.DepartmentId," +
               " dp.Name AS DepartmentName," +
               " rc.PersonalTaxCode" +
               " INTO #tmpA" +
               " FROM hr_Record rc" +
               " LEFT JOIN cat_Department dp" +
               " ON rc.DepartmentId = dp.Id" +
               " WHERE rc.PersonalTaxCode IS NOT NULL AND LEN(rc.PersonalTaxCode)> 0";
        if (!string.IsNullOrEmpty(department))
        {
            sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
        }
        if (!string.IsNullOrEmpty(condition))
        {
            sql += " AND {0}".FormatWith(condition);
        }
        if (!string.IsNullOrEmpty(fromDate))
            sql += " and rc.ParticipationDate >= '{0}'".FormatWith(fromDate);
        if (!string.IsNullOrEmpty(toDate))
            sql += " and rc.ParticipationDate <= '{0}'".FormatWith(toDate);
        sql += "SELECT* FROM #tmpA";
        return sql;
    }

    /// <summary>
    /// Báo cáo danh sách tài khoản ngân hàng
    /// </summary>
    /// <param name="department"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static string GetStore_BusinessBankAccount(string department, string condition,string fromDate,string toDate)
    {
        var sql = string.Empty;
        sql += "IF OBJECT_ID('tempdb..#tmpA') IS NOT NULL DROP Table #tmpA" +
               " SELECT rc.EmployeeCode," +
               " rc.FullName," +
               " ba.AccountNumber," +
               " cb.Name AS BankName," +
               " dp.Name AS DepartmentName," +
               " rc.DepartmentId" +
               " INTO #tmpA" +
               " FROM hr_Bank ba" +
               " LEFT JOIN hr_Record rc" +
               " ON ba.RecordId = rc.Id" +
               " LEFT JOIN cat_Bank cb" +
               " ON ba.BankCode = cb.Code" +
               " LEFT JOIN cat_Department dp" +
               " ON rc.DepartmentId = dp.Id" +
               " WHERE 1 = 1" +
               " AND rc.EmployeeCode IS NOT NULL " +
               " AND ba.AccountNumber IS NOT NULL";
        if (!string.IsNullOrEmpty(department))
        {
            sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
        }
        if (!string.IsNullOrEmpty(condition))
        {
            sql += " AND {0}".FormatWith(condition);
        }
        if (!string.IsNullOrEmpty(fromDate))
            sql += " and rc.ParticipationDate >= '{0}'".FormatWith(fromDate);
        if (!string.IsNullOrEmpty(toDate))
            sql += " and rc.ParticipationDate <= '{0}'".FormatWith(toDate);
        sql += " SELECT* FROM #tmpA";
        return sql;
    }

    /// <summary>
    /// báo cáo danh sách số người phụ thuộc
    /// </summary>
    /// <param name="department"></param>
    /// <param name="condition"></param>
    /// <param name="fromDate"></param>
    /// <param name="toDate"></param>
    /// <returns></returns>
    public static string GetStore_BusinessFamilyRelation(string department, string condition,string fromDate,string toDate)
    {
        var sql = string.Empty;
        sql += "SELECT rc.EmployeeCode, " +
               " rc.FullName," +
               " CASE WHEN fr.DependenceNumber IS NULL THEN 0 ELSE fr.DependenceNumber END AS CountNumber," +
               " dp.Name AS DepartmentName," +
               " rc.DepartmentId" +
               " FROM hr_Record rc" +
               " LEFT JOIN" +
               " (SELECT RecordId," +
               " COUNT(Id) AS DependenceNumber FROM hr_FamilyRelationship " +
               " WHERE IsDependent = 1 " +
               " GROUP BY RecordId) fr" +
               " ON rc.Id = fr.RecordId" +
               " LEFT JOIN cat_Department dp" +
               " ON rc.DepartmentId = dp.Id" +
               " WHERE 1 = 1";
        
        if (!string.IsNullOrEmpty(department))
        {
            sql += " AND rc.DepartmentId IN({0})".FormatWith(department);
        }
        if (!string.IsNullOrEmpty(condition))
        {
            sql += "AND {0}".FormatWith(condition);
        }
        if (!string.IsNullOrEmpty(fromDate))
            sql += " and rc.ParticipationDate >= '{0}'".FormatWith(fromDate);
        if (!string.IsNullOrEmpty(toDate))
            sql += " and rc.ParticipationDate <= '{0}'".FormatWith(toDate);
        return sql;
    }

    /// <summary>
    /// kỳ vượt lương
    /// </summary>
    /// <param name="department"></param>
    /// <param name="condition"></param>
    /// <param name="fromDate"></param>
    /// <param name="toDate"></param>
    /// <returns></returns>
    public static string GetStore_BusinessRaiseSalary(string department, string condition,string fromDate,string toDate)
    {
        var sql = string.Empty;
        sql += "IF OBJECT_ID('tempdb..#tmpA') IS NOT NULL DROP Table #tmpA" +
               " SELECT rc.EmployeeCode," +
               " rc.FullName," +
               " YEAR(rc.BirthDate) AS BirthYear," +
               " po.Name AS PositionName," +
               " dp.Name AS DepartmentName," +
               " rc.DepartmentId," +
               " rc.ParticipationDate," +
               " #tmpA.EffectiveDate, " +
               " #tmpA.SalaryBasic" +
               " INTO #tmpA" +
               " FROM(SELECT sl.RecordId," +
               " sl.EffectiveDate," +
               " sl.SalaryBasic" +
               " FROM hr_Salary sl" +
               " RIGHT JOIN (SELECT RecordId, MAX(EffectiveDate) AS EffectiveDate" +
               " FROM hr_Salary" +
               " GROUP BY RecordId)#temp" +
               " ON sl.RecordId = #temp.RecordId" +
               " AND sl.EffectiveDate = #temp.EffectiveDate" +
               //" LEFT JOIN cat_Quantum qt" +
               //" ON sl.QuantumId = qt.Id" +
               //" WHERE DATEADD(MONTH, qt.MonthNumber, #temp.EffectiveDate) <= GETDATE()" +
               " )#tmpA" +
               " LEFT JOIN hr_Record rc" +
               " ON #tmpA.RecordId = rc.Id" +
               " LEFT JOIN cat_Position po" +
               " ON rc.PositionId = po.Id" +
               " LEFT JOIN cat_Department dp" +
               " ON rc.DepartmentId = dp.Id" +
               " WHERE 1 = 1 AND rc.EmployeeCode IS NOT NULL";

        if (!string.IsNullOrEmpty(department))
        {
            sql += " AND rc.DepartmentId IN({0})".FormatWith(department);
        }
        if (!string.IsNullOrEmpty(condition))
        {
            sql += "AND {0}".FormatWith(condition);
        }
        if (!string.IsNullOrEmpty(fromDate))
            sql += " and #tmpA.EffectiveDate >= '{0}'".FormatWith(fromDate);
        if (!string.IsNullOrEmpty(toDate))
            sql += " and #tmpA.EffectiveDate <= '{0}'".FormatWith(toDate);
        sql += " SELECT* FROM #tmpA";
        return sql;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="department"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static string GetStore_BusinessOutframe(string department, string condition)
    {
        var sql = string.Empty;
        sql += " SELECT rc.EmployeeCode," +
               " rc.FullName" +
               " FROM hr_Salary sl" +
               " LEFT JOIN hr_Record rc" +
               " ON sl.RecordId = rc.Id";
        if (!string.IsNullOrEmpty(department))
        {
            sql += " AND rc.DepartmentId IN({0})".FormatWith(department);
        }
        if (!string.IsNullOrEmpty(condition))
        {
            sql += "AND {0}".FormatWith(condition);
        }
        return sql;
    }

    /// <summary>
    /// Quyết định lương
    /// </summary>
    /// <param name="departments"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <param name="searchKey"></param>
    /// <param name="departmentSelected"></param>
    /// <returns></returns>
    public static string GetStore_DecisionSalary(string departments, int? start, int? limit, string searchKey, string departmentSelected)
    {
        var sql = string.Empty;
        sql += " SELECT * FROM " +
                "(SELECT 		" +
                "hs.Id,		" +
                "hs.DecisionNumber,		" +
                "hr.EmployeeCode,		" +
                "hr.FullName,		" +
                "(SELECT TOP 1 cd.Name FROM cat_Department cd WHERE cd.Id = hr.DepartmentId) AS 'DepartmentName',											" +
                "hs.SalaryFactor,		" +
                "hs.SalaryBasic,		" +
                "hs.SalaryInsurance,		" +
                "hs.SalaryGrade,		" +
                "hs.SalaryPayDate,		" +
                "hs.SalaryGradeLift,		" +
                "hs.DecisionDate,		" +
                "hs.EffectiveDate,		" +
                "hs.Note,		" +
                "hs.OtherAllowance,		" +
                "hs.OutFrame,		" +
                "hs.DecisionMaker,		" +
                "hr.Sex,		" +
                "hr.BirthDate,		" +
                "hs.PositionAllowance,		" +
                "hs.AttachFileName,		" +
               "hs.SalaryGross,		" +
               "hs.SalaryNet,		" +
               "hs.PercentageSalary, " +
               "hs.SalaryContract, " +
                "( SELECT TOP 1 cq.Name FROM cat_Quantum cq WHERE cq.Id = hs.QuantumId ) AS 'QuantumName', " +
                "  ROW_NUMBER() OVER(ORDER BY hs.EffectiveDate DESC) SK " +
                "FROM hr_Salary hs					" +
                "LEFT JOIN hr_Record hr on hs.RecordId = hr.Id					" +
                "WHERE 1=1 ";
        if (!string.IsNullOrEmpty(departments))
            sql += "AND hr.DepartmentId IN ({0}) ".FormatWith(departments);
        if (!string.IsNullOrEmpty(departmentSelected))
        {
            var rootId = 0;
            var SelectedDepartment = "{0},".FormatWith(departmentSelected);
            if (int.TryParse(departmentSelected, out var parseId))
            {
                rootId = parseId;
            }
            var lstDepartment = cat_DepartmentServices.GetTree(rootId).Select(d => d.Id).ToList();
            if (lstDepartment.Count > 0)
            {
                foreach (var d in lstDepartment)
                {
                    SelectedDepartment += "{0},".FormatWith(d);
                }
            }
            SelectedDepartment = "{0}".FormatWith(SelectedDepartment.TrimEnd(','));
            sql += "AND hr.DepartmentId IN ({0}) ".FormatWith(SelectedDepartment);
        }

        if (!string.IsNullOrEmpty(searchKey))
            sql += " AND hr.FullName LIKE N'%{0}%' ".FormatWith(searchKey) +
                    " OR hr.EmployeeCode LIKE N'%{0}%' ".FormatWith(searchKey);
        sql += " ) A ";
        if (start != null && limit != null)
            sql += "WHERE A.SK > {0} ".FormatWith(start) +
                "AND A.SK <= {0} ".FormatWith(start + limit);

        return sql;
    }

    /// <summary>
    /// Báo cáo diễn biến quá trình lương cán bộ
    /// </summary>
    /// <param name="departments"></param>
    /// <param name="recordId"></param>
    /// <param name="condition"></param>
    /// <param name="fromDate"></param>
    /// <param name="toDate"></param>
    /// <returns></returns>
    public static string GetStore_ProcessSalaryEmployee(string departments, int? recordId, string condition,string fromDate,string toDate1)
    {
        var sql = string.Empty;
        sql += "IF OBJECT_ID('tempdb..#tmpA') IS NOT NULL DROP Table #tmpA ";
        sql += " SELECT " +
               " hs.DepartmentId AS 'DepartmentId', " +
               " dv.Name AS 'DepartmentName', " +
               " '' AS 'STT', " +
               " hs.EmployeeCode AS 'EmployeeCode', " +
               " hs.FullName AS 'FullName', " +
               " cv.Name AS 'PositionName', " +
               " hsl.DecisionNumber AS 'DecisionNumber', " +
               " hsl.DecisionDate AS 'DecisionDate', " +
               " hsl.EffectiveDate AS 'EffectiveDate', " +
               " ngach.Name AS 'QuantumName', " +
               " ngach.Code AS 'QuantumId', " +
               " hsl.SalaryGrade AS 'QuantumGrade', " +
               " hsl.SalaryFactor AS 'SalaryFactor', " +
               " hsl.PositionAllowance AS 'PositionAllowance', " +
               " hsl.OtherAllowance AS 'OtherAllowance', " +
               " hsl.DecisionMaker AS 'DecisionMaker', " +
               " hsl.MakerPosition AS 'MakerPosition' " +
               " INTO #tmpA " +
               " FROM hr_Salary hsl " +
               "   LEFT JOIN hr_Record hs " +
               "       ON hsl.RecordId = hs.Id " +
               "   LEFT JOIN cat_Department dv " +
               "       ON hs.DepartmentId = dv.Id " +
               "   LEFT JOIN cat_Position cv " +
               "       ON hs.PositionId = cv.Id " +
               "   LEFT JOIN cat_Quantum ngach " +
               "       ON ngach.Id = hsl.QuantumId " +
               " WHERE 1=1 ";
        if (!string.IsNullOrEmpty(departments))
        {
            sql += " AND hs.DepartmentId IN ({0}) ".FormatWith(departments);
        }
        if (recordId != 0)
        {
            sql += " AND hsl.RecordId = {0} ".FormatWith(recordId);
        }
        if (!string.IsNullOrEmpty(condition))
        {
            sql += " AND {0}".FormatWith(condition);
        }
        if (!string.IsNullOrEmpty(fromDate))
            sql += " and hsl.EffectiveDate >= '{0}'".FormatWith(fromDate);
        if (!string.IsNullOrEmpty(toDate1))
            sql += " and hsl.EffectiveDate <= '{0}'".FormatWith(toDate1);
        sql += " SELECT * FROM #tmpA";

        return sql;
    }
}