using System;

namespace Web.Core.Framework.SQLAdapter
{
    public class SQLReportSalaryAdapter
    {
        /// <summary>
        ///  Báo cáo danh sách tiền lương
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <param name="recordIds"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeSalary(string department, string fromDate, string toDate, string condition, string recordIds)
        {
            var sql = string.Empty;
            sql += " SELECT rc.FullName, " +
                   " rc.BirthDate, " +
                   " rc.EmployeeCode, " +
                   " p.Name AS PositionName, " +
                   " dv.Id AS DepartmentId, " +
                   " dv.Name AS DepartmentName,  " +
                   " rc.ParticipationDate, " +
                   " CASE WHEN (SELECT TOP 1 Value FROM cat_BasicSalary WHERE AppliedDate < '{0}' ".FormatWith(DateTime.Now.ToString("yyyy-MM-dd")) +
                   "					ORDER BY [AppliedDate] DESC) > 0 THEN (SELECT TOP 1 Value FROM cat_BasicSalary WHERE AppliedDate < '{0}' ".FormatWith(DateTime.Now.ToString("yyyy-MM-dd")) +
                   "					ORDER BY [AppliedDate] DESC) " +
                   " ELSE hs.ContractSalary END AS SalaryBasic, " +
                   " (SELECT TOP 1 ct. Name FROM cat_ContractType ct LEFT JOIN hr_Contract hc ON ct.Id = hc.ContractTypeId WHERE hs.RecordId = hc.RecordId) AS ContractTypeName " +
                   " FROM sal_SalaryDecision hs " +
                   " LEFT JOIN hr_Record rc ON hs.RecordId = rc.Id " +
                   " LEFT JOIN cat_Position p on rc.PositionId = p.Id " + // filter position
                   " LEFT JOIN cat_Department dv ON dv.Id = rc.DepartmentId " + // filter department
                   " WHERE hs.EffectiveDate = (select MAX(EffectiveDate) from sal_SalaryDecision sa where sa.RecordId = rc.Id ) ";
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

            if (!string.IsNullOrEmpty(recordIds))
            {
                sql += " AND rc.Id IN ({0})".FormatWith(recordIds);
            }

            sql += " ORDER BY rc.EmployeeCode ";

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
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        public static string GetStore_SalaryIncreaseProcess(string departments, int? recordId, string fromDate, string toDate,  string condition, bool? isDeleted)
        {
            var sql = string.Empty;
            sql += "IF OBJECT_ID('tempdb..#tmpA') IS NOT NULL DROP Table #tmpA ";
            sql += " SELECT " +
                   " rc.DepartmentId, " +
                   " dp.Name AS 'DepartmentName', " +
                   " '' AS 'STT', " +
                   " rc.EmployeeCode , " +
                   " rc.FullName, " +
                   " cp.Name AS 'PositionName', " +
                   " hs.DecisionNumber, " +
                   " hs.DecisionDate, " +
                   " hs.EffectiveDate , " +
                   " cq.Name AS 'QuantumName', " +
                   " cq.Code AS 'QuantumCode', " +
                   " hs.Grade , " +
                   " hs.Factor , " +
                   //" hs.PositionAllowance , " +
                   //" hs.OtherAllowance , " +
                   " hs.SignerName , " +
                   " (SELECT TOP 1 cp.Name FROM cat_Position cp WHERE cp.Id = hs.SignerPositionId) AS SignerPosition  " +
                   " INTO #tmpA " +
                   " FROM sal_SalaryDecision hs " +
                   "   LEFT JOIN hr_Record rc " +
                   "       ON hs.RecordId = rc.Id " +
                   "   LEFT JOIN cat_Department dp " +//filter department
                   "       ON rc.DepartmentId = dp.Id " +
                   "   LEFT JOIN cat_Position cp " +//filter position
                   "       ON rc.PositionId = cp.Id " +
                   "   LEFT JOIN cat_Quantum cq " +//filter quantum
                   "       ON cq.Id = hs.QuantumId " +
                   " WHERE 1=1 ";
            if (!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN ({0}) ".FormatWith(departments);
            }
            if (!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND hs.DecisionDate >= '{0}'".FormatWith(fromDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                sql += " AND hs.DecisionDate <= '{0}'".FormatWith(toDate);
            }
            if (recordId != 0)
            {
                sql += " AND hs.RecordId IN ({0}) ".FormatWith(recordId);
            }
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }

            if (isDeleted != null)
                sql += " AND hs.[IsDeleted] = {0}".FormatWith((bool)isDeleted ? "1" : "0");

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
        //public static string GetStore_SalaryIncrease(string departments, string fromDate, string toDate, string condition)
        //{
        //    var sql = "	SELECT 	 " +
        //              "	rc.DepartmentId, " +
        //              "	rc.EmployeeCode, " +
        //              "	rc.FullName, " +
        //              "	dp.Name AS DepartmentName, " +
        //              "	cp.Name AS PositionName, " +
        //              "	cq.Code AS QuantumCode, " +
        //              "	cq.Name AS QuantumName, " +
        //              "	cq.MonthStep, " +
        //              "	hs.SalaryFactor, " +
        //              "	hs.SalaryGrade, " +
        //              "	hs.PositionAllowance, " +
        //              "	hs.OtherAllowance, " +
        //              "	hs.DecisionDate, " +
        //              "	hs.EffectiveDate, " +
        //              "	hs.DecisionNumber, " +
        //              "	cq.SalaryGrade, " +
        //              "	DATEADD(MONTH, cq.MonthStep, CONVERT(date, hs.EffectiveDate)) AS RaisingSalaryTime " +
        //              "	FROM (SELECT DISTINCT slr.RecordId, " +
        //              "					slr.EffectiveDate,	 " +
        //              "					slr.SalaryGrade,	 " +
        //              "					slr.SalaryFactor,	 " +
        //              "					slr.QuantumId,	 " +
        //              "					slr.PositionAllowance,	 " +
        //              "					slr.OtherAllowance,	 " +
        //              "					slr.DecisionDate,	 " +
        //              "					slr.DecisionNumber	 " +
        //              "		FROM (SELECT MAX(DecisionDate) AS MaxDecisionDate, " +
        //              "	            RecordId " +
        //              "			FROM sal_SalaryDecision	  " +
        //              "			GROUP BY RecordId) #tmpA " +
        //              "		LEFT JOIN sal_SalaryDecision slr ON #tmpA.MaxDecisionDate = slr.DecisionDate " +
        //              "		AND slr.RecordId = #tmpA.RecordId " +
        //              "		WHERE slr.SalaryFactor = (SELECT TOP 1 SalaryFactor FROM " +
        //              "									(SELECT MAX(SalaryFactor) AS SalaryFactor, RecordId	 " +
        //              "									FROM sal_SalaryDecision						" +
        //              "									GROUP BY RecordId)#tmp " +
        //              "									WHERE #tmp.RecordId = slr.RecordId) " +
        //              "		) hs " +
        //              "	LEFT JOIN hr_Record rc ON hs.RecordId = rc.Id " +
        //              "	LEFT JOIN cat_Department dp ON rc.DepartmentId = dp.Id " +//filter department
        //              "	LEFT JOIN cat_Position cp on rc.PositionId = cp.Id " + //filter position
        //              "	LEFT JOIN cat_Quantum cq ON hs.QuantumId = cq.Id " + //filter quantum
        //              "	WHERE rc.Id IS NOT NULL " +
        //              "	AND CONVERT(INT,hs.SalaryGrade) < cq.SalaryGrade " +
        //              "	AND DATEADD(MONTH, cq.MonthStep, CONVERT(datetime, hs.EffectiveDate)) >= 0 ";

        //    if (!string.IsNullOrEmpty(departments))
        //    {
        //        sql += " AND rc.DepartmentId IN({0}) ".FormatWith(departments);
        //    }

        //    if (!string.IsNullOrEmpty(fromDate))
        //    {
        //        sql += " AND hs.DecisionDate >= '{0}'".FormatWith(fromDate);
        //    }
        //    if (!string.IsNullOrEmpty(toDate))
        //    {
        //        sql += " AND hs.DecisionDate <= '{0}'".FormatWith(toDate);
        //    }

        //    if (!string.IsNullOrEmpty(condition))
        //    {
        //        sql += " AND {0}".FormatWith(condition);
        //    }

        //    sql += " AND rc.WorkStatusId = (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%{0}%')".FormatWith(RecordStatus.Working.Description());

        //    return sql;
        //}

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
                   " LEFT JOIN cat_Department dp" + // department
                   " ON rc.DepartmentId = dp.Id" +
                   " WHERE 1 = 1" +
                   " AND rc.EmployeeCode IS NOT NULL " +
                   " AND ba.AccountNumber IS NOT NULL";
            if(!string.IsNullOrEmpty(department))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
            }
            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            if(!string.IsNullOrEmpty(fromDate))
                sql += " and rc.ParticipationDate >= '{0}'".FormatWith(fromDate);
            if(!string.IsNullOrEmpty(toDate))
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
                   " LEFT JOIN cat_Department dp" + // department
                   " ON rc.DepartmentId = dp.Id" +
                   " WHERE rc.PersonalTaxCode IS NOT NULL AND LEN(rc.PersonalTaxCode)> 0";
            if(!string.IsNullOrEmpty(department))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
            }
            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            if(!string.IsNullOrEmpty(fromDate))
                sql += " and rc.ParticipationDate >= '{0}'".FormatWith(fromDate);
            if(!string.IsNullOrEmpty(toDate))
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
                   " LEFT JOIN cat_Department dp" + // filter department
                   " ON rc.DepartmentId = dp.Id" +
                   " WHERE 1 = 1";

            if(!string.IsNullOrEmpty(department))
            {
                sql += " AND rc.DepartmentId IN({0})".FormatWith(department);
            }
            if(!string.IsNullOrEmpty(condition))
            {
                sql += "AND {0}".FormatWith(condition);
            }
            if(!string.IsNullOrEmpty(fromDate))
                sql += " and rc.ParticipationDate >= '{0}'".FormatWith(fromDate);
            if(!string.IsNullOrEmpty(toDate))
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
        //public static string GetStore_SalaryOutOfFrame(string departments, string fromDate, string toDate, string condition)
        //{
        //    var sql = "	SELECT  " +
        //              "	rc.DepartmentId, " +
        //              "	rc.EmployeeCode, " +
        //              "	rc.FullName, " +
        //              "	dp.Name AS DepartmentName, " +
        //              "	cp.Name AS PositionName, " +
        //              "	cq.Code AS QuantumCode, " +
        //              "	cq.Name AS QuantumName, " +
        //              "	gq.MonthStep, " +
        //              "	hs.SalaryFactor, " +
        //              "	hs.SalaryGrade, " +
        //              "	hs.PositionAllowance, " +
        //              "	hs.OtherAllowance, " +
        //              "	hs.DecisionDate, " +
        //              "	hs.EffectiveDate, " +
        //              "	hs.DecisionNumber, " +
        //              "	hs.Note, " +
        //              "	gq.GradeMax AS QuantumGrade, " +
        //              "	DATEDIFF(MONTH,hs.EffectiveDate, GETDATE()) AS MonthlySalary, " +
        //              "	DATEADD(MONTH, gq.MonthStep, hs.EffectiveDate) AS FrameOutCome " +
        //              "	FROM (SELECT DISTINCT slr.RecordId, " +
        //              "					slr.EffectiveDate, " +
        //              "					slr.SalaryGrade, " +
        //              "					slr.SalaryFactor, " +
        //              "					slr.QuantumId, " +
        //              "					slr.GroupQuantumId, " +
        //              "					slr.PositionAllowance, " +
        //              "					slr.OtherAllowance, " +
        //              "					slr.DecisionDate, " +
        //              "					slr.DecisionNumber, " +
        //              "					slr.Note " +
        //              "		FROM (SELECT MAX(DecisionDate) AS MaxDecisionDate,			 " +
        //              "	            RecordId " +
        //              "			FROM sal_SalaryDecision		 " +
        //              "			GROUP BY RecordId) #tmpA		 " +
        //              "		LEFT JOIN sal_SalaryDecision slr ON #tmpA.MaxDecisionDate = slr.DecisionDate " +
        //              "		AND slr.RecordId = #tmpA.RecordId			 " +
        //              "		WHERE slr.SalaryFactor = (SELECT TOP 1 SalaryFactor FROM			 " +
        //              "									(SELECT MAX(SalaryFactor) AS SalaryFactor, RecordId						" +
        //              "									FROM sal_SalaryDecision						" +
        //              "									GROUP BY RecordId)#tmp	 " +
        //              "									WHERE #tmp.RecordId = slr.RecordId)	 " +
        //              "		) hs			 " +
        //              "	LEFT JOIN hr_Record rc ON hs.RecordId = rc.Id " +
        //              "	LEFT JOIN cat_Department dp ON rc.DepartmentId = dp.Id " +
        //              "	LEFT JOIN cat_Position cp on rc.PositionId = cp.Id " +
        //              "	LEFT JOIN cat_Quantum cq ON hs.QuantumId = cq.Id " +
        //              "	LEFT JOIN cat_GroupQuantum gq on gq.Id = hs.GroupQuantumId " +
        //              "	WHERE rc.Id IS NOT NULL " +
        //              "	AND CONVERT(INT,hs.SalaryGrade) = gq.GradeMax " +
        //              "	AND(DATEDIFF(MONTH,hs.EffectiveDate, GETDATE()) - gq.MonthStep) >= 0 ";
                    
        //    if (!string.IsNullOrEmpty(departments))
        //    {
        //        sql += " AND rc.DepartmentId IN({0}) ".FormatWith(departments);
        //    }

        //    if (!string.IsNullOrEmpty(fromDate))
        //    {
        //        sql += " AND hs.DecisionDate >= '{0}'".FormatWith(fromDate);
        //    }
        //    if (!string.IsNullOrEmpty(toDate))
        //    {
        //        sql += " AND hs.DecisionDate <= '{0}'".FormatWith(toDate);
        //    }

        //    if (!string.IsNullOrEmpty(condition))
        //    {
        //        sql += " AND {0}".FormatWith(condition);
        //    }

        //    sql += " AND rc.WorkStatusId = (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%{0}%')".FormatWith(RecordStatus.Working.Description());

        //    return sql;
        //}
    }

}

