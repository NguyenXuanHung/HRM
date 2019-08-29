using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.Framework.SQLAdapter
{
    public class HRMSalaryAdapter
    {
        
        /// <summary>
        ///  Báo cáo danh sách tiền lương
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeSalary(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += " SELECT rc.FullName, " +
                   " rc.BirthDate, " +
                   " rc.EmployeeCode, " +
                   " p.Name AS PositionName, " +
                   " dv.Id AS DepartmentId, " +
                   " dv.Name AS DepartmentName,  " +
                   " rc.ParticipationDate, " +
                   " CASE WHEN hs.SalaryBasic > 0 THEN hs.SalaryBasic " +
                   " ELSE hs.SalaryContract END AS SalaryBasic, " +
                   " (SELECT TOP 1 ct. Name FROM cat_ContractType ct LEFT JOIN hr_Contract hc ON ct.Id = hc.ContractTypeId WHERE hs.RecordId = hc.RecordId) AS ContractTypeName " +
                   " FROM hr_Salary hs " +
                   " LEFT JOIN hr_Record rc ON hs.RecordId = rc.Id " +
                   " LEFT JOIN cat_Position p on rc.PositionId = p.Id " +
                   " LEFT JOIN cat_Department dv ON dv.Id = rc.DepartmentId " +
                   " where hs.EffectiveDate = (select MAX(EffectiveDate) from hr_Salary sa where sa.RecordId = rc.Id ) ";
            if (!string.IsNullOrEmpty(department))
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
            if (!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND rc.ParticipationDate >= '{0}'".FormatWith(fromDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                sql += " AND rc.ParticipationDate <= '{0}'".FormatWith(toDate);
            }
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += " order by rc.EmployeeCode ";

            return sql;
        }


        /// <summary>
        /// Báo cáo diễn biến quá trình lương
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="recordId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_SalaryIncreaseProcess(string departments, int? recordId, string fromDate, string toDate,  string condition)
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
            sql += "SELECT * FROM #tmpA";

            return sql;
        }


        /// <summary>
        /// Báo cáo danh sách đến kỳ tăng lương
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_SalaryIncrease(string departments, string fromDate, string toDate, string condition)
        {
            var sql = "SELECT * FROM (  " +
                      " SELECT *, " +
                      " DATEADD(MONTH, #B.MonthNumber, CONVERT(datetime, #B.EffectiveDate)) AS RaisingSalaryTime," +
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
                      " qt.MonthNumber," +
                      " qt.Name AS QuantumName," +
                      " #A.SalaryFactor," +
                      " #A.PositionAllowance," +
                      " #A.OtherAllowance," +
                      " dp.Name AS DepartmentName," +
                      " po.Name AS PositionName" +
                      " FROM hr_Record rc" +
                      " RIGHT JOIN" +
                      " (SELECT DISTINCT * FROM" +
                      " (SELECT MAX(DecisionDate) AS MaxDecisionDate," +
                      " RecordId AS IdRecord" +
                      " FROM hr_Salary" +
                      " GROUP BY RecordId) #tmpA " +
                      " LEFT JOIN hr_Salary slr" +
                      " ON #tmpA.MaxDecisionDate = slr.DecisionDate " +
                      " AND slr.RecordId = #tmpA.IdRecord " +
                      " WHERE slr.SalaryFactor = (SELECT TOP 1 SalaryFactor" +
                      " FROM(" +
                      " SELECT MAX(SalaryFactor) AS SalaryFactor," +
                      " RecordId FROM hr_Salary" +
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
            if (!string.IsNullOrEmpty(departments))
            {
                sql += " AND DepartmentId IN({0}) ".FormatWith(departments);
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
        /// Báo cáo danh sách tài khoản ngân hàng
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeBank(string department, string fromDate, string toDate, string condition)
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
        /// Báo cáo danh sách mã số thuế
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeTaxCode(string department, string fromDate, string toDate, string condition)
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
        /// Báo cáo danh sách người phụ thuộc
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_DependentPerson(string department, string fromDate, string toDate, string condition)
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
        /// Danh sách cán bộ đến hạn xét vượt khung
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_SalaryOutOfFrame(string departments, string fromDate, string toDate, string condition)
        {
            var sql = "SELECT * FROM (" +
                      " SELECT DISTINCT rc.EmployeeCode," +
                      " rc.FullName, " +
                      " CASE WHEN CONVERT(INT,#A.SalaryGrade) < gq.GradeNumbeMax THEN 'NangLuong' ELSE 'VuotKhung' END AS TypeRecord,  " +
                      " qt.Code AS QuantumId,   " +
                      " #A.SalaryGrade," +
                      " gq.GradeNumbeMax AS QuantumGrade," +
                      " #A.DecisionDate,  " +
                      " #A.DecisionNumber, " +
                      " #A.EffectiveDate, " +
                      " DATEDIFF(MONTH,#A.EffectiveDate, GETDATE()) AS MonthlySalary, " +
                      " gq.MonthNumber," +
                      " qt.Name AS QuantumName," +
                      " #A.SalaryFactor, " +
                      " #A.PositionAllowance, " +
                      " #A.OtherAllowance, " +
                      " dp.Name AS DepartmentName," +
                      " po.Name AS PositionName," +
                      " DATEADD(MONTH, gq.MonthNumber, #A.EffectiveDate) AS FrameOutCome, " +
                      " #A.Note " +
                      " FROM hr_Record rc" +
                      " RIGHT JOIN(" +
                      " SELECT DISTINCT * FROM" +
                      " (SELECT MAX(DecisionDate) AS MaxDecisionDate," +
                      " RecordId AS IdRecord FROM hr_Salary" +
                      " GROUP BY RecordId) #tmpA  " +
                      " LEFT JOIN hr_Salary slr" +
                      " ON #tmpA.MaxDecisionDate = slr.DecisionDate  AND slr.RecordId = #tmpA.IdRecord  " +
                      " WHERE slr.SalaryFactor = (" +
                      " SELECT TOP 1 SalaryFactor FROM(" +
                      " SELECT MAX(SalaryFactor) AS SalaryFactor," +
                      " RecordId" +
                      " FROM hr_Salary" +
                      " GROUP BY RecordId)#tmp" +
                      " WHERE #tmp.RecordId = slr.RecordId) ) #A  " +
                      " ON #A.RecordId = rc.Id  " +
                      " LEFT JOIN cat_Quantum qt ON qt.Id = #A.QuantumId  " +
                      " LEFT JOIN cat_GroupQuantum gq on gq.Id = #A.GroupQuantumId" +
                      " LEFT JOIN cat_Department dp ON rc.DepartmentId = dp.Id" +
                      " LEFT JOIN hr_Contract ct ON rc.Id = ct.RecordId" +
                      " LEFT JOIN cat_Position po ON rc.PositionId = po.Id" +
                      " WHERE 1 = 1";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND DepartmentId IN({0})".FormatWith(departments);
            }
            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += " AND rc.WorkStatusId = (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Đang làm việc%')" +
                   " )#B" +
                   " WHERE TypeRecord = N'VuotKhung'" +
                   " AND QuantumGrade = SalaryGrade" +
                   " AND(MonthlySalary - MonthNumber) >= 0";
            return sql;
        }

    }
}
