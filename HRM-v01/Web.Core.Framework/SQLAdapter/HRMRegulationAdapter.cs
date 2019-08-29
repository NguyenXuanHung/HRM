namespace Web.Core.Framework.SQLAdapter
{
    public class HRMRegulationAdapter
    {
        private const string WorkStatusWorking = "%Đang làm việc%";
        private const string WorkStatusLeave = "%Đang nghỉ phép%";
        private const string BusinessTypeRetirement = "NghiHuu";
        private const string BusinessPersonelRotation = "ThuyenChuyenDieuChuyen";

        private const string ReasonRetirement = "%Nghỉ hưu%";
        private const string ReasonTerminate = "%Đơn phương chấm dứt hđlđ/hđlv%";
        private const string ReasonFired = "%Kỷ luật sa thải%";
        private const string ReasonExpiredContract = "%Thỏa thuận chấm dứt%";
        private const string ReasonOther = "%Lý do khác%";
        private const string Dead = "%Từ trần%";
        
        /// <summary>
        /// Khai trình lao động
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_LaborList(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += " SELECT rc.FullName,	 " +
                   " YEAR(rc.BirthDate) as Year,	 " +
                   " rc.EmployeeCode,	 " +
                   " case when rc.Sex = 1 then 'X' else '' end as SexMale, " +
                   " case when rc.Sex = 0 then 'X' else '' end as SexFemale, " +
                   " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT WHERE DT.Id = rc.EducationId) = 'DH' THEN 'X' ELSE '' END AS University,	 " +
                   " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT WHERE DT.Id = rc.EducationId) = 'CD' THEN 'X' ELSE '' END AS College,	 " +
                   " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT WHERE DT.Id = rc.EducationId) = 'TC' THEN 'X' ELSE '' END AS Intermadiate,	 " +
                   " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT WHERE DT.Id = rc.EducationId) = 'SC' THEN 'X' ELSE '' END AS PrimaryEducation,	 " +
                   " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT WHERE DT.Id = rc.EducationId) = 'CQDT' THEN 'X' ELSE '' END AS UnTraining,	 " +
                   " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT WHERE DT.Id = rc.EducationId) = 'DNTX' THEN 'X' ELSE '' END AS Vocational,	 " +
                   " CASE WHEN (SELECT TOP 1 cc.[Group] FROM cat_ContractType cc LEFT JOIN hr_Contract hc ON cc.Id = hc.ContractTypeId WHERE hc.RecordId = rc.Id) = 'KXDTH' THEN 'X' ELSE '' END AS IndefinitContract,	 " +
                   " CASE WHEN (SELECT TOP 1 cc.[Group] FROM cat_ContractType cc LEFT JOIN hr_Contract hc ON cc.Id = hc.ContractTypeId WHERE hc.RecordId = rc.Id) = 'XDTH' THEN 'X' ELSE '' END AS TermContract,	 " +
                   " CASE WHEN (SELECT TOP 1 cc.[Group] FROM cat_ContractType cc LEFT JOIN hr_Contract hc ON cc.Id = hc.ContractTypeId WHERE hc.RecordId = rc.Id) = 'HDTV' THEN 'X' ELSE '' END AS SeasonContract,	 " +
                   " p.Name AS PositionName,	 " +
                   " dv.Id as DepartmentId," +
                   " dv.Name as DepartmentName," +
                   " rc.ParticipationDate	 " +
                   " FROM hr_Record rc	 " +
                   " LEFT JOIN cat_Position p on rc.PositionId = p.Id	 " +
                   " LEFT JOIN cat_Department dv ON dv.Id = rc.DepartmentId";
            if(!string.IsNullOrEmpty(department))
            {
                sql += " WHERE rc.DepartmentId IN ({0})".FormatWith(department);
            }
            if(!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND rc.ParticipationDate >= '{0}'".FormatWith(fromDate);
            }
            if(!string.IsNullOrEmpty(toDate))
            {
                sql += " AND rc.ParticipationDate <= '{0}'".FormatWith(toDate);
            }
            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }

            return sql;
        }

        /// <summary>
        /// Khai trình tăng lao động
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_LabourIncrease(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += "  IF OBJECT_ID('tempdb..#tmpA') IS NOT NULL DROP Table #tmpA" +
                   " SELECT " +
                   " CASE WHEN rc.Sex = 1 THEN '' ELSE 'X' END AS Female, " +
                   " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = rc.EducationId) = 'DH' THEN 'X' ELSE '' END AS University,	 " +
                   " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = rc.EducationId) = 'CD' THEN 'X' ELSE '' END AS College,	 " +
                   " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = rc.EducationId) = 'TC' THEN 'X' ELSE '' END AS Intermediate,	 " +
                   " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = rc.EducationId) = 'SC' THEN 'X' ELSE '' END AS PrimaryEducation,	 " +
                   " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = rc.EducationId) = 'CQDT' THEN 'X' ELSE '' END AS UnTraining,	 " +
                   " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = rc.EducationId) = 'DNTX' THEN 'X' ELSE '' END AS Vocational,	 " +
                   " CASE WHEN (SELECT TOP 1 cc.[Group] FROM cat_ContractType cc LEFT JOIN hr_Contract hc ON cc.Id = hc.ContractTypeId WHERE hc.RecordId = rc.Id) = 'KXDTH' THEN 'X' ELSE '' END AS IndefinitContract,	 " +
                   " CASE WHEN (SELECT TOP 1 cc.[Group] FROM cat_ContractType cc LEFT JOIN hr_Contract hc ON cc.Id = hc.ContractTypeId WHERE hc.RecordId = rc.Id) = 'XDTH' THEN 'X' ELSE '' END AS TermContract,	 " +
                   " CASE WHEN (SELECT TOP 1 cc.[Group] FROM cat_ContractType cc LEFT JOIN hr_Contract hc ON cc.Id = hc.ContractTypeId WHERE hc.RecordId = rc.Id) = 'HDTV' THEN 'X' ELSE '' END AS SeasonContract,	 " +
                   " dv.Id as DepartmentId," +
                   " dv.Name as DepartmentName," +
                   " 0 AS xFemale, " +
                   " 0 AS xUniversity," +
                   " 0 AS xColleage," +
                   " 0 AS xIntermediate," +
                   " 0 AS xPrimaryEducation," +
                   " 0 AS xUnTraining," +
                   " 0 AS xVocational," +
                   " 0 AS xIndefinitContract," +
                   " 0 AS xTermContract," +
                   " 0 AS xSeasonContract" +
                   " INTO #tmpA" +
                   " FROM hr_FluctuationEmployee fe 	 " +
                   " left join hr_Record rc on rc.Id = fe.RecordId	 " +
                   " LEFT JOIN cat_Department dv ON dv.Id = rc.DepartmentId" +
                   " WHERE fe.Type = 0 ";
            if(!string.IsNullOrEmpty(department))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
            }
            if(!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND fe.Date >= '{0}'".FormatWith(fromDate);
            }
            if(!string.IsNullOrEmpty(toDate))
            {
                sql += " AND fe.Date <= '{0}'".FormatWith(toDate);
            }
            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += "UPDATE #tmpA" +
                   " SET xFemale = totalFemale," +
                   " xUniversity = totalUniversity," +
                   " xColleage = totalColleage," +
                   " xIntermediate = totalIntermediate," +
                   " xPrimaryEducation = totalPrimaryEducation," +
                   " xUnTraining = totalUnTraining," +
                   " xVocational = totalVocational," +
                   " xIndefinitContract = totalIndefinitContract," +
                   " xTermContract = totalTermContract," +
                   " xSeasonContract = totalSeasonContract" +
                   " FROM #tmpA" +
                   " INNER JOIN(" +
                   " SELECT SUM(CASE WHEN #tmpA.Female = 'X' THEN 1 ELSE 0 END) AS totalFemale," +
                   " SUM(CASE WHEN #tmpA.University = 'X' THEN 1 ELSE 0 END) AS totalUniversity," +
                   " SUM(CASE WHEN #tmpA.College = 'X' THEN 1 ELSE 0 END) AS totalColleage," +
                   " SUM(CASE WHEN #tmpA.Intermediate = 'X' THEN 1 ELSE 0 END) AS totalIntermediate," +
                   " SUM(CASE WHEN #tmpA.PrimaryEducation = 'X' THEN 1 ELSE 0 END) AS totalPrimaryEducation," +
                   " SUM(CASE WHEN #tmpA.UnTraining = 'X' THEN 1 ELSE 0 END) AS totalUnTraining," +
                   " SUM(CASE WHEN #tmpA.Vocational = 'X' THEN 1 ELSE 0 END) AS totalVocational," +
                   " SUM(CASE WHEN #tmpA.IndefinitContract = 'X' THEN 1 ELSE 0 END) AS totalIndefinitContract," +
                   " SUM(CASE WHEN #tmpA.TermContract = 'X' THEN 1 ELSE 0 END) AS totalTermContract," +
                   " SUM(CASE WHEN #tmpA.SeasonContract = 'X' THEN 1 ELSE 0 END) AS totalSeasonContract" +
                   " FROM #tmpA" +
                   " ) AS #tmp ON 1=1" +

                   " SELECT * FROM #tmpA";

            return sql;
        }

        /// <summary>
        /// Khai trình giảm lao động
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_LabourDecrease(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += " IF OBJECT_ID('tempdb..#tmpA') IS NOT NULL DROP Table #tmpA " +
                   " SELECT " +
                   " CASE WHEN rc.Sex = 1 THEN '' ELSE 'X' END AS Female, " +
                   " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = rc.EducationId) = 'DH' THEN 'X' ELSE '' END AS University,	 " +
                   " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = rc.EducationId) = 'CD' THEN 'X' ELSE '' END AS College,	 " +
                   " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = rc.EducationId) = 'TC' THEN 'X' ELSE '' END AS Intermediate,	 " +
                   " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = rc.EducationId) = 'SC' THEN 'X' ELSE '' END AS PrimaryEducation,	 " +
                   " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = rc.EducationId) = 'CQDT' THEN 'X' ELSE '' END AS UnTraining,	 " +
                   " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = rc.EducationId) = 'DNTX' THEN 'X' ELSE '' END AS Vocational,	 " +
                   " CASE WHEN (SELECT TOP 1 cc.[Group] FROM cat_ContractType cc LEFT JOIN hr_Contract hc ON cc.Id = hc.ContractTypeId WHERE hc.RecordId = rc.Id) = 'KXDTH' THEN 'X' ELSE '' END AS IndefinitContract,	 " +
                   " CASE WHEN (SELECT TOP 1 cc.[Group] FROM cat_ContractType cc LEFT JOIN hr_Contract hc ON cc.Id = hc.ContractTypeId WHERE hc.RecordId = rc.Id) = 'XDTH' THEN 'X' ELSE '' END AS TermContract,	 " +
                   " CASE WHEN (SELECT TOP 1 cc.[Group] FROM cat_ContractType cc LEFT JOIN hr_Contract hc ON cc.Id = hc.ContractTypeId WHERE hc.RecordId = rc.Id) = 'HDTV' THEN 'X' ELSE '' END AS SeasonContract,	 " +
                   " dv.Id as DepartmentId," +
                   " dv.Name as DepartmentName," +
                   " CASE WHEN (Select Count(*) from hr_FluctuationEmployee hf where hf.RecordId = rc.Id and hf.Reason LIKE N'{0}') > 0 THEN 'X' ELSE '' END as ReasonRetirement, "
                       .FormatWith(ReasonRetirement) +
                   " CASE WHEN (Select Count(*) from hr_FluctuationEmployee hf where hf.RecordId = rc.Id and hf.Reason LIKE N'{0}') > 0 THEN 'X' ELSE '' END as ReasonExpiredContract, "
                       .FormatWith(ReasonExpiredContract) +
                   " CASE WHEN (Select Count(*) from hr_FluctuationEmployee hf where hf.RecordId = rc.Id and hf.Reason LIKE N'{0}') > 0 THEN 'X' ELSE '' END as ReasonFired, "
                       .FormatWith(ReasonFired) +
                   " CASE WHEN (Select Count(*) from hr_FluctuationEmployee hf where hf.RecordId = rc.Id and hf.Reason LIKE N'{0}') > 0 THEN 'X' ELSE '' END as ReasonOther, "
                       .FormatWith(ReasonOther) +
                   " CASE WHEN (Select Count(*) from hr_FluctuationEmployee hf where hf.RecordId = rc.Id and hf.Reason LIKE N'{0}')  > 0 THEN 'X' ELSE '' END as ReasonTerminate, "
                       .FormatWith(ReasonTerminate) +
                   " 0 AS xFemale," +
                   " 0 AS xUniversity," +
                   " 0 AS xColleage," +
                   " 0 AS xIntermediate," +
                   " 0 AS xPrimaryEducation," +
                   " 0 AS xVocational," +
                   " 0 AS xUnTraining," +
                   " 0 AS xIndefinitContract," +
                   " 0 AS xTermContract," +
                   " 0 AS xSeasonContract," +
                   " 0 AS xReasonRetirement," +
                   " 0 AS xReasonExpiredContract," +
                   " 0 AS xReasonFired," +
                   " 0 As xReasonOther," +
                   " 0 As xReasonTerminate" +
                   " into #tmpA " +
                   " FROM hr_FluctuationEmployee fe 	 " +
                   " left join hr_Record rc on rc.Id = fe.RecordId	 " +
                   " LEFT JOIN cat_Department dv ON dv.Id = rc.DepartmentId" +
                   " WHERE fe.Type = 1 ";
            if(!string.IsNullOrEmpty(department))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
            }
            if(!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND fe.Date >= '{0}'".FormatWith(fromDate);
            }
            if(!string.IsNullOrEmpty(toDate))
            {
                sql += " AND fe.Date <= '{0}'".FormatWith(toDate);
            }
            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += "UPDATE #tmpA" +
                   " SET xFemale = totalFemale," +
                   " xUniversity = totalUniversity," +
                   " xColleage = totalColleage," +
                   " xIntermediate = totalIntermediate," +
                   " xPrimaryEducation = totalPrimaryEducation," +
                   " xUnTraining = totalUnTraining," +
                   " xVocational = totalVocational," +
                   " xIndefinitContract = totalIndefinitContract," +
                   " xTermContract = totalTermContract," +
                   " xSeasonContract = totalSeasonContract, " +
                   "  xReasonRetirement = totalReasonRetirement, " +
                   " xReasonExpiredContract = totalReasonExpiredContract," +
                   " xReasonFired = totalReasonFired," +
                   " xReasonOther = totalReasonOther," +
                   " xReasonTerminate = totalReasonTerminate" +
                   " FROM #tmpA" +
                   " INNER JOIN(" +
                   " SELECT SUM(CASE WHEN #tmpA.Female = 'X' THEN 1 ELSE 0 END) AS totalFemale," +
                   " SUM(CASE WHEN #tmpA.University = 'X' THEN 1 ELSE 0 END) AS totalUniversity," +
                   " SUM(CASE WHEN #tmpA.College = 'X' THEN 1 ELSE 0 END) AS totalColleage," +
                   " SUM(CASE WHEN #tmpA.Intermediate = 'X' THEN 1 ELSE 0 END) AS totalIntermediate," +
                   " SUM(CASE WHEN #tmpA.PrimaryEducation = 'X' THEN 1 ELSE 0 END) AS totalPrimaryEducation," +
                   " SUM(CASE WHEN #tmpA.UnTraining = 'X' THEN 1 ELSE 0 END) AS totalUnTraining," +
                   " SUM(CASE WHEN #tmpA.Vocational = 'X' THEN 1 ELSE 0 END) AS totalVocational," +
                   " SUM(CASE WHEN #tmpA.IndefinitContract = 'X' THEN 1 ELSE 0 END) AS totalIndefinitContract," +
                   " SUM(CASE WHEN #tmpA.TermContract = 'X' THEN 1 ELSE 0 END) AS totalTermContract," +
                   " SUM(CASE WHEN #tmpA.SeasonContract = 'X' THEN 1 ELSE 0 END) AS totalSeasonContract," +

                   " SUM(CASE WHEN #tmpA.ReasonRetirement = 'X' THEN 1 ELSE 0 END) AS totalReasonRetirement," +
                   " SUM(CASE WHEN #tmpA.ReasonExpiredContract = 'X' THEN 1 ELSE 0 END) AS totalReasonExpiredContract," +
                   " SUM(CASE WHEN #tmpA.ReasonFired = 'X' THEN 1 ELSE 0 END) AS totalReasonFired," +
                   " SUM(CASE WHEN #tmpA.ReasonOther = 'X' THEN 1 ELSE 0 END) AS totalReasonOther," +
                   " SUM(CASE WHEN #tmpA.ReasonTerminate = 'X' THEN 1 ELSE 0 END) AS totalReasonTerminate" +
                   " FROM #tmpA" +
                   ") AS #tmp ON 1=1" +

                   " SELECT * FROM #tmpA";

            return sql;
        }

        /// <summary>
        /// Danh sách nhân sự
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeList(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += "IF OBJECT_ID('tempdb..#tmpA') IS NOT NULL DROP Table #tmpA" +
                   " SELECT rc.FullName, " +
                   " rc.BirthDate, " +
                   " rc.EmployeeCode, " +
                   " p.Name AS PositionName, " +
                   " dv.Id AS DepartmentId, " +
                   " dv.Name AS DepartmentName, " +
                   " (SELECT TOP 1 dt.Name FROM cat_Education dt WHERE dt.Id = rc.EducationId) AS Certify, " +
                   " rc.CellPhoneNumber, " +
                   " CASE WHEN (select top 1 cw.Id from cat_WorkStatus cw where cw.Id = rc.WorkStatusId) = (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'{0}') then 1 ".FormatWith(WorkStatusWorking) +
                   "  ELSE 0 END as Working, " +
                   " CASE WHEN (select top 1 cw.Id from cat_WorkStatus cw where cw.Id = rc.WorkStatusId) = (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'{0}') then 1 ".FormatWith(WorkStatusLeave) +
                   "  ELSE 0 END as Leave, " +
                   " CASE WHEN (select top 1 cw.Id from cat_WorkStatus cw where cw.Id = rc.WorkStatusId) = (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'{0}') then 1 ".FormatWith(WorkStatusWorking) +
                   " WHEN (select top 1 cw.Id from cat_WorkStatus cw where cw.Id = rc.WorkStatusId) = (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'{0}') then 1 ".FormatWith(WorkStatusLeave) +
                   " ELSE 0 END as Total, " +
                   " '' AS Note," +
                   " 0 AS xEmployee " +
                   " INTO #tmpA" +
                   " FROM hr_Record rc " +
                   " LEFT JOIN cat_Position p on rc.PositionId = p.Id " +
                   " LEFT JOIN cat_Department dv ON dv.Id = rc.DepartmentId " +
                   " WHERE 1 = 1 ";
            if(!string.IsNullOrEmpty(department))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
            }
            if(!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND rc.ParticipationDate >= '{0}'".FormatWith(fromDate);
            }
            if(!string.IsNullOrEmpty(toDate))
            {
                sql += " AND rc.ParticipationDate <= '{0}'".FormatWith(toDate);
            }
            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql +=
                " UPDATE #tmpA " +
                " SET xEmployee = totalEmployee" +
                " FROM #tmpA" +
                " INNER JOIN (SELECT COUNT(EmployeeCode) AS totalEmployee FROM #tmpA) AS #tmp ON 1=1" +
                " SELECT * FROM #tmpA";
            return sql;
        }

        /// <summary>
        /// Báo cáo tăng/giảm lao động
        /// </summary>
        /// <param name="department"></param>
        /// <param name="direction"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeAdjust(string department, bool direction, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += " IF OBJECT_ID('tempdb..#tmpA') IS NOT NULL DROP Table #tmpA " +
                   " SELECT rc.FullName,	" +
                   " rc.BirthDate,	" +
                   " rc.EmployeeCode,	" +
                   " p.Name AS PositionName,	" +
                   " dv.Id AS DepartmentId,	" +
                   " dv.Name AS DepartmentName,	" +
                   " (SELECT TOP 1 dt.Name FROM cat_Education dt  WHERE dt.Id = rc.EducationId) AS Certify,	" +
                   " hf.Date,	" +
                   " 0 AS xEmployee" +
                   " INTO #tmpA " +
                   " FROM hr_FluctuationEmployee hf	" +
                   " LEFT JOIN hr_Record rc ON hf.RecordId = rc.Id	" +
                   " LEFT JOIN cat_Position p on rc.PositionId = p.Id	" +
                   " LEFT JOIN cat_Department dv ON dv.Id = rc.DepartmentId	" +
                   " WHERE 1 = 1 ";
            if(!string.IsNullOrEmpty(department))
            {
                sql += "AND rc.DepartmentId IN ({0})".FormatWith(department);
            }
            if(direction == false)
            {
                sql += " AND hf.Type = 0 "; //Tang
            }
            else
            {
                sql += " AND hf.Type = 1 "; //Giam
            }
            if(!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND hf.Date>='{0}'".FormatWith(fromDate);
            }
            if(!string.IsNullOrEmpty(toDate))
            {
                sql += " AND hf.Date<='{0}'".FormatWith(toDate);
            }
            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += "UPDATE #tmpA  " +
                   " SET xEmployee = totalEmployee" +
                   " FROM #tmpA " +
                   " INNER JOIN(SELECT case when COUNT(EmployeeCode) > 0 then COUNT(EmployeeCode) else 0 end AS totalEmployee FROM #tmpA) AS #tmp " +
                   " ON 1 = 1 " +
                   "SELECT * FROM #tmpA";

            return sql;
        }

        /// <summary>
        /// Báo cáo điều động nhân sự
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeTransferred(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += " select " +
                   " rc.FullName, " +
                   " rc.EmployeeCode, " +
                   " hb.CurrentPosition, " +
                   " cc.Name as CurrentProjectName, " +
                   " '' as NewProjectName, " +
                   " hb.NewPosition, " +
                   " dv.Id as DepartmentId, " +
                   " dv.Name as DepartmentName, " +
                   " hb.DecisionDate " +
                   " from hr_BusinessHistory hb " +
                   " left join hr_Record rc on hb.RecordId = rc.Id " +
                   " left join cat_Department dv on dv.Id = rc.DepartmentId	" +
                   " left join hr_Team ht on ht.RecordId = rc.Id " +
                   " left join cat_Construction cc on cc.Id = ht.ConstructionId " +
                   " where 1=1  " +
                   " and (hb.BusinessType = '{0}') ".FormatWith(BusinessPersonelRotation);
            if(!string.IsNullOrEmpty(department))
                sql += " AND rc.DepartmentId IN ({0}) ".FormatWith(department);
            if(!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND hb.DecisionDate >= '{0}'".FormatWith(fromDate);
            }
            if(!string.IsNullOrEmpty(toDate))
            {
                sql += " AND hb.DecisionDate <= '{0}'".FormatWith(toDate);
            }

            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            return sql;
        }

        /// <summary>
        /// Báo cáo biệt phái nhân sự
        /// </summary>
        /// <param name="department"></param>
        /// <param name="businessType"></param>
        /// <param name="condition"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeSent(string department, string businessType, string condition, string fromDate, string toDate)
        {
            var sql = string.Empty;
            sql += " select " +
                   " rc.FullName, " +
                   " rc.EmployeeCode, " +
                   " hb.DecisionDate as FromDate, " +
                   " hb.ExpireDate as ToDate, " +
                   " hb.CurrentPosition, " +
                   " dv.Name as DepartmentName, " +
                   " dv.Id as DepartmentId, " +
                   " hb.CurrentDepartment " +
                   " from hr_BusinessHistory hb " +
                   " left join hr_Record rc on hb.RecordId = rc.Id " +
                   " left join cat_Department dv on dv.Id = rc.DepartmentId " +
                   " where 1 = 1 ";
            if(!string.IsNullOrEmpty(department))
                sql += " and rc.DepartmentId IN ({0})".FormatWith(department);
            if(!string.IsNullOrEmpty(businessType))
                sql += " and hb.BusinessType = '{0}' ".FormatWith(businessType);
            if(!string.IsNullOrEmpty(condition))
                sql += " AND {0}".FormatWith(condition);
            if(!string.IsNullOrEmpty(fromDate))
                sql += " and hb.DecisionDate >= '{0}'".FormatWith(fromDate);
            if(!string.IsNullOrEmpty(toDate))
                sql += " and hb.DecisionDate <= '{0}'".FormatWith(toDate);
            return sql;
        }

        /// <summary>
        /// Báo cáo bổ nhiệm nhân sự
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeAssigned(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += " select " +
                   " rc.FullName, " +
                   " rc.EmployeeCode, " +
                   " dv.Id as DepartmentId, " +
                   " dv.Name as DepartmentName, " +
                   " (select top 1 name from cat_Position where Id = hw.NewPositionId) as NewPositionName, " +
                   " cp.Name as CurrentPositionName, " +
                   " hw.DecisionDate " +
                   " from hr_Record rc " +
                   " left join cat_Position cp on cp.Id = rc.PositionId " +
                   " left join cat_Department dv on dv.Id = rc.DepartmentId " +
                   " right join hr_WorkProcess hw on hw.RecordId = rc.Id " +
                   " WHERE 1=1 ";
            if(!string.IsNullOrEmpty(department))
                sql += " and rc.DepartmentId IN ({0})".FormatWith(department);
            if(!string.IsNullOrEmpty(fromDate))
                sql += " and hw.DecisionDate >= '{0}'".FormatWith(fromDate);
            if(!string.IsNullOrEmpty(toDate))
                sql += " and hw.DecisionDate <= '{0}'".FormatWith(toDate);
            if(!string.IsNullOrEmpty(condition))
                sql += " AND {0}".FormatWith(condition);
            return sql;
        }

        /// <summary>
        /// Báo cáo thâm niên công tác
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeSeniority(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;

            sql += " select  " +
                   " rc.FullName, " +
                   " rc.EmployeeCode, " +
                   " YEAR(rc.BirthDate) as Year, " +
                   " rc.Address, " +
                   " cp.Name as PositionName, " +
                   " ce.Name as EducationName, " +
                   " dv.Id as DepartmentId, " +
                   " dv.Name as DepartmentName, " +
                   " rc.CellPhoneNumber, " +
                   " rc.ParticipationDate, " +
                   "   CASE WHEN (DATEDIFF(DAY, rc.ParticipationDate, getDate())/365) = 0 THEN '' ELSE " +
                   "   CAST(DATEDIFF(DAY, rc.ParticipationDate, getDate())/365 AS NVARCHAR(10)) + N' Năm ' END + " +
                   "   CASE WHEN ((DATEDIFF(DAY, rc.ParticipationDate, getDate())%365) /30) = 0 THEN '' ELSE " +
                   "   CAST((DATEDIFF(DAY, rc.ParticipationDate, getDate())%365) /30 AS NVARCHAR(10)) + N' Tháng ' END + " +
                   "   CASE WHEN ((DATEDIFF(DAY, rc.ParticipationDate, getDate())%365) %30) = 0 THEN '' ELSE " +
                   "   CAST((DATEDIFF(DAY, rc.ParticipationDate, getDate())%365) %30 AS NVARCHAR(10)) + N' Ngày' END AS 'Seniority' " +
                   " from hr_Record rc " +
                   " left join cat_Position cp on cp.Id = rc.PositionId " +
                   " left join cat_Education ce on ce.Id = rc.EducationId " +
                   " left join cat_Department dv on dv.Id = rc.DepartmentId " +
                   " where 1=1 ";
            if(!string.IsNullOrEmpty(department))
                sql += " and rc.DepartmentId IN ({0})".FormatWith(department);
            if(!string.IsNullOrEmpty(fromDate))
                sql += " and rc.ParticipationDate >= '{0}'".FormatWith(fromDate);
            if(!string.IsNullOrEmpty(toDate))
                sql += " and rc.ParticipationDate <= '{0}'".FormatWith(toDate);
            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            return sql;
        }

        /// <summary>
        /// Báo cáo nhân sự nghỉ hưu
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeRetired(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += " select " +
                   " rc.EmployeeCode, " +
                   " rc.FullName, " +
                   " cp.Name as PositionName, " +
                   " dv.Id as DepartmentId, " +
                   " dv.Name as DepartmentName, " +
                   " bh.DecisionNumber, " +
                   " bh.DecisionDate, " +
                   "   CASE WHEN (DATEDIFF(DAY, rc.ParticipationDate, getDate())/365) = 0 THEN '' ELSE " +
                   "   CAST(DATEDIFF(DAY, rc.ParticipationDate, getDate())/365 AS NVARCHAR(10)) + N' Năm ' END + " +
                   "   CASE WHEN ((DATEDIFF(DAY, rc.ParticipationDate, getDate())%365) /30) = 0 THEN '' ELSE " +
                   "   CAST((DATEDIFF(DAY, rc.ParticipationDate, getDate())%365) /30 AS NVARCHAR(10)) + N' Tháng ' END + " +
                   "   CASE WHEN ((DATEDIFF(DAY, rc.ParticipationDate, getDate())%365) %30) = 0 THEN '' ELSE " +
                   "   CAST((DATEDIFF(DAY, rc.ParticipationDate, getDate())%365) %30 AS NVARCHAR(10)) + N' Ngày' END AS 'Seniority' " +
                   " from hr_BusinessHistory bh " +
                   " left join hr_Record rc on bh.RecordId = rc.Id " +
                   " left join cat_Position cp on cp.Id = rc.PositionId " +
                   " left join cat_Department dv on dv.Id = rc.DepartmentId " +
                   " where bh.BusinessType = '{0}' ".FormatWith(BusinessTypeRetirement) +
                   " and bh.DecisionDate IS NOT NULL ";

            if(!string.IsNullOrEmpty(department))
                sql += " and rc.DepartmentId IN ({0})".FormatWith(department);
            if(!string.IsNullOrEmpty(condition))
                sql += " AND {0}".FormatWith(condition);
            if(!string.IsNullOrEmpty(fromDate))
                sql += " and bh.DecisionDate >= '{0}'".FormatWith(fromDate);
            if(!string.IsNullOrEmpty(toDate))
                sql += " and bh.DecisionDate <= '{0}'".FormatWith(toDate);
            return sql;
        }
    }
}
