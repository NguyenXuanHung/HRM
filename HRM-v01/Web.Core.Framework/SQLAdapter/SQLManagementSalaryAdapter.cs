using System.Linq;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework.Adapter
{
    /// <summary>
    /// Summary description for SQLManagementSalaryAdapter
    /// </summary>
    public class SQLManagementSalaryAdapter
    {        
        /// <summary>
        /// Get list decision salary
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="searchKey"></param>
        /// <param name="trangThai"></param>
        /// <param name="departmentSelected"></param>
        /// <returns></returns>
        public static string GetStore_DecisionSalary(string departments, int? start, int? limit, string searchKey, string trangThai, string departmentSelected)
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
                    "hs.Value,		" +
                    "hs.SalaryInsurance,		" +
                    "hs.SalaryGrade,		" +
                    "hs.SalaryPayDate,		" +
                    "hs.SalaryGradeLift,		" +
                    "hs.DecisionDate,		" +
                    "hs.EffectiveDate,		" +
                    "hs.Note,		" +
                    "hs.OtherAllowance,		" +
                    "hs.OverGrade,		" +
                    "hs.SignerName,		" +
                    "hr.Sex,		" +
                    "hr.BirthDate,		" +
                    //"hs.PositionAllowance,		" +
                    //"hs.AttachFileName,		" +
                   "hs.SalaryGross,		" +
                   "hs.SalaryNet,		" +
                   "hs.PercentageSalary, " +
                   "hs.SalaryContract, " +
                    "( SELECT TOP 1 cq.Name FROM cat_Quantum cq WHERE cq.Id = hs.QuantumId ) AS 'QuantumName', " +
                    "  ROW_NUMBER() OVER(ORDER BY hs.EffectiveDate DESC) SK " +
                    "FROM sal_SalaryDecision hs					" +
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
        /// <returns></returns>
        public static string GetStore_ProcessSalaryOfEmployee(string departments, int? recordId, string condition)
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
                   //" hsl.PositionAllowance AS 'PositionAllowance', " +
                   //" hsl.OtherAllowance AS 'OtherAllowance', " +
                   " hsl.SignerName AS 'SignerName', " +
                   " hsl.SignerPosition AS 'SignerPosition' " +
                   " INTO #tmpA " +
                   " FROM sal_SalaryDecision hsl " +
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
            sql += "SELECT * FROM #tmpA";

            return sql;
        }

        /// <summary>
        /// Báo cáo danh sách cán bộ đến kỳ nâng lương
        /// </summary>
        /// <param name="maDonVi"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_ListEmployeeLiftSalary(string maDonVi, string condition)
        {
            var sql = "SELECT * FROM (  " +
                      " SELECT *, " +
                      " DATEADD(MONTH, #B.MonthStep, CONVERT(datetime, #B.EffectiveDate)) AS RaisingSalaryTime," +
                      " ROW_NUMBER() OVER(ORDER BY DecisionDate) AS RN FROM" +
                      " (SELECT DISTINCT rc.EmployeeCode," +
                      " rc.FullName," +
                      " CASE WHEN CONVERT(INT,#A.SalaryGrade) < qt.SalaryGrade THEN 'NangLuong' ELSE 'VuotKhung' END AS TypeRecord, " +
                      " qt.Code AS QuantumCode,  " +
                      " #A.SalaryGrade AS QuantumGrade," +
                      " #A.DecisionDate, " +
                      " #A.DecisionNumber," +
                      " #A.EffectiveDate," +
                      " DATEDIFF(MONTH,#A.EffectiveDate, GETDATE()) AS DayOfRiseSalary," +
                      " qt.MonthStep," +
                      " qt.Name AS QuantumName," +
                      " #A.SalaryFactor," +
                      //" #A.PositionAllowance," +
                      //" #A.OtherAllowance," +
                      " dp.Name AS DepartmentName," +
                      " po.Name AS PositionName" +
                      " FROM hr_Record rc" +
                      " RIGHT JOIN" +
                      " (SELECT DISTINCT * FROM" +
                      " (SELECT MAX(DecisionDate) AS MaxDecisionDate," +
                      " RecordId AS IdRecord" +
                      " FROM sal_SalaryDecision" +
                      " GROUP BY RecordId) #tmpA " +
                      " LEFT JOIN sal_SalaryDecision slr" +
                      " ON #tmpA.MaxDecisionDate = slr.DecisionDate " +
                      " AND slr.RecordId = #tmpA.IdRecord " +
                      " WHERE slr.SalaryFactor = (SELECT TOP 1 SalaryFactor" +
                      " FROM(" +
                      " SELECT MAX(SalaryFactor) AS SalaryFactor," +
                      " RecordId FROM sal_SalaryDecision" +
                      " GROUP BY RecordId)#tmp" +
                      " WHERE #tmp.RecordId = slr.RecordId)" +
                      " ) #A " +
                      " ON #A.RecordId = rc.Id " +
                      " LEFT JOIN cat_Quantum qt" +
                      " ON qt.Id = #A.QuantumId " +
                      " LEFT JOIN cat_Department dp" +
                      " ON rc.DepartmentId = dp.Id" +
                      " LEFT JOIN cat_Department dp2" +
                      " ON dp.ParentId = dp2.Id" +
                      " LEFT JOIN cat_Department dp3" +
                      " ON dp2.ParentId = dp3.Id" +
                      " LEFT JOIN hr_Contract ct" +
                      " ON rc.Id = ct.RecordId" +
                      " LEFT JOIN cat_Position po" +
                      " ON rc.PositionId = po.Id" +
                      " WHERE 1=1";
            if (!string.IsNullOrEmpty(maDonVi))
            {
                sql += " AND DepartmentId IN({0}) ".FormatWith(maDonVi);
            }
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }

            sql += " AND rc.WorkStatusId = (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Đang làm việc%')" +
                   " )#B )#C " +
                   " WHERE TypeRecord = N'NangLuong' AND RaisingSalaryTime >= 0";

            return sql;
        }

        /// <summary>
        /// Danh sách cán bộ đến hạn xét vượt khung
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_ListEmployeeExpirationOutOfFrame(string departments, string condition)
        {
            var sql = "SELECT * FROM (" +
                      " SELECT DISTINCT rc.EmployeeCode," +
                      " rc.FullName, " +
                      " CASE WHEN CONVERT(INT,#A.SalaryGrade) < gq.GradeMax THEN 'NangLuong' ELSE 'VuotKhung' END AS TypeRecord,  " +
                      " qt.Code AS QuantumId,   " +
                      " #A.SalaryGrade," +
                      " gq.GradeMax AS QuantumGrade," +
                      " #A.DecisionDate,  " +
                      " #A.DecisionNumber, " +
                      " #A.EffectiveDate, " +
                      " DATEDIFF(MONTH,#A.EffectiveDate, GETDATE()) AS MonthlySalary, " +
                      " gq.MonthStep," +
                      " qt.Name AS QuantumName," +
                      " #A.SalaryFactor, " +
                      //" #A.PositionAllowance, " +
                      //" #A.OtherAllowance, " +
                      " dp.Name AS DepartmentName," +
                      " po.Name AS PositionName," +
                      " DATEADD(MONTH, gq.MonthStep, #A.EffectiveDate) AS FrameOutCome, " +
                      " #A.Note " +
                      " FROM hr_Record rc" +
                      " RIGHT JOIN(" +
                      " SELECT DISTINCT * FROM" +
                      " (SELECT MAX(DecisionDate) AS MaxDecisionDate," +
                      " RecordId AS IdRecord FROM sal_SalaryDecision" +
                      " GROUP BY RecordId) #tmpA  " +
                      " LEFT JOIN sal_SalaryDecision slr" +
                      " ON #tmpA.MaxDecisionDate = slr.DecisionDate  AND slr.RecordId = #tmpA.IdRecord  " +
                      " WHERE slr.SalaryFactor = (" +
                      " SELECT TOP 1 SalaryFactor FROM(" +
                      " SELECT MAX(SalaryFactor) AS SalaryFactor," +
                      " RecordId" +
                      " FROM sal_SalaryDecision" +
                      " GROUP BY RecordId)#tmp" +
                      " WHERE #tmp.RecordId = slr.RecordId) ) #A  " +
                      " ON #A.RecordId = rc.Id  " +
                      " LEFT JOIN cat_Quantum qt ON qt.Id = #A.QuantumId  " +
                      " LEFT JOIN cat_GroupQuantum gq on gq.Id = #A.GroupQuantumId" +
                      " LEFT JOIN cat_Department dp ON rc.DepartmentId = dp.Id" +
                      " LEFT JOIN hr_Contract ct ON rc.Id = ct.RecordId" +
                      " LEFT JOIN cat_Position po ON rc.PositionId = po.Id" +
                      " WHERE 1 = 1";
            if (!string.IsNullOrEmpty(departments))
            {
                sql += " AND DepartmentId IN({0})".FormatWith(departments);
            }
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += " AND rc.WorkStatusId = (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Đang làm việc%')" +
                   " )#B" +
                   " WHERE TypeRecord = N'VuotKhung'" +
                   " AND QuantumGrade = SalaryGrade" +
                   " AND(MonthlySalary - MonthStep) >= 0";
            return sql;
        }
    }
}

