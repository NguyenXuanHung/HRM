namespace Web.Core.Framework.SQLAdapter
{
    public class SQLChartAdapter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        public static string GetStore_EducationChart(string departments)
        {
            var sql = "SELECT * FROM" +
                      "(SELECT ed.Id AS EducationId, " +
                      " ed.Name AS EducationName, " +
                      " COUNT(ed.Name) AS[Count]" +
                      " FROM hr_Record rc" +
                      " LEFT JOIN cat_Education ed" +
                      " ON rc.EducationId = ed.Id" +
                      " WHERE rc.EducationId IS NOT NULL" +
                      " AND ed.Name IS NOT NULL";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(departments);
            }

            sql += " AND rc.WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')" +
                   " GROUP BY ed.Id, ed.Name" +
                   " UNION" +
                   " SELECT 0 AS EducationId," +
                   " N'Không xác định' AS EducationName," +
                   " COUNT(rc.EmployeeCode) AS[Count]" +
                   " FROM hr_Record rc" +
                   " LEFT JOIN cat_Education ed" +
                   " ON rc.EducationId = ed.Id" +
                   " WHERE rc.WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(departments);
            }
            sql += " AND(rc.EducationId IS NULL OR ed.Name IS NULL)) #tmp";
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        public static string GetStore_MarriageStatusChart(string departments)
        {
            var sql = "SELECT ms.Name AS MaritalStatusName, " +
                      " COUNT(ms.Name) AS[Count]" +
                      " FROM hr_Record rc" +
                      " LEFT JOIN cat_MaritalStatus ms" +
                      " ON rc.MaritalStatusId = ms.Id" +
                      " WHERE (rc.MaritalStatusId <> 0 AND ms.Name IS NOT NULL)" +
                      " AND rc.WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(departments);
            }
            sql += " GROUP BY ms.Name" +
                   " UNION" +
                   " SELECT N'Không xác định' AS MaritalStatusName," +
                   " COUNT(rc.EmployeeCode) AS[Count]" +
                   " FROM hr_Record rc" +
                   " LEFT JOIN cat_MaritalStatus ms" +
                   " ON rc.MaritalStatusId = ms.Id" +
                   " WHERE (rc.MaritalStatusId = 0 OR ms.Name IS NULL)" +
                   " AND rc.WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(departments);
            }
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        public static string GetStore_ReligionChart(string departments)
        {
            var sql = "SELECT rl.Name AS ReligionName, " +
                      " COUNT(rl.Name) AS[Count]" +
                      " FROM hr_Record rc" +
                      " LEFT JOIN cat_Religion rl" +
                      " ON rc.ReligionId = rl.Id" +
                      " WHERE (rc.ReligionId <> 0 AND rl.Name IS NOT NULL)" +
                      " AND rc.WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(departments);
            }
            sql += " GROUP BY rl.Name" +
                   " UNION" +
                   " SELECT N'Không xác định' AS ReligionName," +
                   " COUNT(rc.EmployeeCode) AS[Count]" +
                   " FROM hr_Record rc" +
                   " LEFT JOIN cat_Religion rl" +
                   " ON rc.ReligionId = rl.Id" +
                   " WHERE (rc.ReligionId = 0 OR rl.Name IS NULL)" +
                   " AND rc.WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(departments);
            }
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        public static string GetStore_FolkChart(string departments)
        {
            var sql = "SELECT f.Name AS FolkName, " +
                      " COUNT(f.Name) AS[Count]" +
                      " FROM hr_Record rc" +
                      " LEFT JOIN cat_Folk f" +
                      " ON rc.FolkId = f.Id" +
                      " WHERE (rc.FolkId <> 0 AND f.Name IS NOT NULL)" +
                      " AND rc.WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(departments);
            }
            sql += " GROUP BY f.Name" +
                   " UNION" +
                   " SELECT N'Không xác định' AS FolkName," +
                   " COUNT(rc.EmployeeCode) AS[Count]" +
                   " FROM hr_Record rc" +
                   " LEFT JOIN cat_Folk f" +
                   " ON rc.FolkId = f.Id" +
                   " WHERE (rc.FolkId = 0 OR f.Name IS NULL)" +
                   " AND rc.WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(departments);
            }
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        public static string GetStore_ContractTypeChart(string departments)
        {
            var sql = "SELECT ctt.Name AS ContractTypeName, " +
                      " COUNT(ctt.Name) AS[Count]" +
                      " FROM hr_Record rc" +
                      " LEFT JOIN hr_Contract ct" +
                      " ON rc.Id = ct.RecordId" +
                      " LEFT JOIN cat_ContractType ctt" +
                      " ON ct.ContractTypeId = ctt.Id" +
                      " WHERE rc.WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')" +
                      " AND (ct.ContractTypeId <> 0 AND ctt.Name IS NOT NULL)";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(departments);
            }
            sql += " GROUP BY ctt.Name" +
                   " UNION" +
                   " SELECT N'Không xác định' AS ContractTypeName," +
                   " COUNT(rc.EmployeeCode) AS[Count]" +
                   " FROM hr_Record rc" +
                   " LEFT JOIN hr_Contract ct" +
                   " ON rc.Id = ct.RecordId" +
                   " LEFT JOIN cat_ContractType ctt" +
                   " ON ct.ContractTypeId = ctt.Id" +
                   " WHERE rc.WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')" +
                   " AND (ct.ContractTypeId = 0 OR ctt.Name IS NULL)";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(departments);
            }
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        public static string GetStore_VYUPositionChart(string departments)
        {
            var sql = "SELECT vyu.Name AS VYUPositionName, " +
                      " COUNT(vyu.Name) AS[Count]" +
                      " FROM hr_Record rc" +
                      " LEFT JOIN cat_VYUPosition vyu" +
                      " ON rc.VYUPositionId = vyu.Id" +
                      " WHERE (rc.VYUPositionId <> 0 AND vyu.Name IS NOT NULL)" +
                      " AND rc.WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(departments);
            }
            sql += " GROUP BY vyu.Name" +
                   " UNION" +
                   " SELECT N'Không xác định' AS VYUPositionName," +
                   " COUNT(rc.EmployeeCode) AS[Count]" +
                   " FROM hr_Record rc" +
                   " LEFT JOIN cat_VYUPosition vyu" +
                   " ON rc.VYUPositionId = vyu.Id" +
                   " WHERE (rc.VYUPositionId = 0 OR vyu.Name IS NULL)" +
                   " AND rc.WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(departments);
            }
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        public static string GetStore_CPVPositionChart(string departments)
        {
            var sql = "SELECT cpv.Name AS CPVPositionName, " +
                      " COUNT(cpv.Name) AS[Count]" +
                      " FROM hr_Record rc" +
                      " LEFT JOIN cat_CPVPosition cpv" +
                      " ON rc.CPVPositionId = cpv.Id" +
                      " WHERE (rc.CPVPositionId <> 0 AND cpv.Name IS NOT NULL)" +
                      " AND rc.WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(departments);
            }
            sql += " GROUP BY cpv.Name" +
                   " UNION" +
                   " SELECT N'Không xác định' AS CPVPositionName," +
                   " COUNT(rc.EmployeeCode) AS[Count]" +
                   " FROM hr_Record rc" +
                   " LEFT JOIN cat_CPVPosition cpv" +
                   " ON rc.CPVPositionId = cpv.Id" +
                   " WHERE (rc.CPVPositionId = 0 OR cpv.Name IS NULL)" +
                   " AND rc.WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(departments);
            }
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        public static string GetStore_ArmyLevelChart(string departments)
        {
            var sql = "SELECT al.Name AS ArmyLevelName, " +
                      " COUNT(al.Name) AS[Count]" +
                      " FROM hr_Record rc" +
                      " LEFT JOIN cat_ArmyLevel al" +
                      " ON rc.ArmyLevelId = al.Id" +
                      " WHERE (rc.ArmyLevelId <> 0 AND al.Name IS NOT NULL)" +
                      " AND rc.WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(departments);
            }
            sql += " GROUP BY al.Name" +
                   " UNION" +
                   " SELECT N'Không xác định' AS ArmyLevelName," +
                   " COUNT(rc.EmployeeCode) AS[Count]" +
                   " FROM hr_Record rc" +
                   " LEFT JOIN cat_ArmyLevel al" +
                   " ON rc.ArmyLevelId = al.Id" +
                   " WHERE (rc.ArmyLevelId = 0 OR al.Name IS NULL)" +
                   " AND rc.WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(departments);
            }
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        public static string GetStore_CountMaleRecordChart(string departments)
        {
            var sql = string.Empty;
            sql += "SELECT COUNT(*) " +
                   " FROM hr_Record " +
                   "WHERE 1 = 1 ";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND DepartmentId IN ({0}) ".FormatWith(departments);
            }
            sql += " AND Sex = '1' " +
                   " AND WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%') ";//Từ trần

            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        public static string GetStore_CountFemaleRecordChart(string departments)
        {
            var sql = string.Empty;
            sql += "SELECT COUNT(*) " +
                   " FROM hr_Record " +
                   "WHERE 1 = 1 ";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND DepartmentId IN ({0}) ".FormatWith(departments);
            }
            sql += " AND Sex = '0' " +
                   " AND WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%') ";//Từ trần
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        public static string GetStore_Seniority(string departments)
        {
            var sql = "SELECT YEAR(GETDATE()) - YEAR(RecruimentDate) AS years " +
                      " FROM hr_Record" +
                      " WHERE RecruimentDate IS NOT NULL" +
                      " AND WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND DepartmentId IN ({0})".FormatWith(departments);
            }
            sql += " UNION ALL" +
                   " SELECT - 1 AS years" +
                   " FROM hr_Record" +
                   " WHERE RecruimentDate IS NULL" +
                   " AND WorkStatusId<> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND DepartmentId IN ({0})".FormatWith(departments);
            }
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        public static string GetStore_AgeChart(string departments)
        {
            var sql = "SELECT * FROM (SELECT YEAR(GETDATE()) - YEAR(BirthDate) AS [Age], " +
                      " CASE WHEN(Sex = 0) THEN N'Nam' ELSE N'Nữ' END AS SexName" +
                      " FROM hr_Record" +
                      " WHERE WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND DepartmentId IN ({0})".FormatWith(departments);
            }
            sql += ")#tmp" +
                   " WHERE #tmp.[Age] IS NOT NULL";
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="year"></param>
        /// <param name="statusParams"></param>
        /// <returns></returns>
        public static string GetStore_HumanResourceChart(string departments, int year, int statusParams)
        {
            var sql = "SELECT rc.Id, " +
                      " rc.WorkStatusDate," +
                      " rc.RecruimentDate," +
                      " MONTH(rc.RecruimentDate) AS MonthOfRecruiment," +
                      " YEAR(rc.RecruimentDate) AS YearOfRecruiment" +
                      " FROM hr_Record rc" +
                      " WHERE rc.RecruimentDate IS NOT NULL" +
                      " AND YEAR(rc.RecruimentDate) <= {0}".FormatWith(year);
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN({0})".FormatWith(departments);
            }
            sql += " AND rc.WorkStatusId = {0}".FormatWith(statusParams);
            return sql;
        }
    }
}
