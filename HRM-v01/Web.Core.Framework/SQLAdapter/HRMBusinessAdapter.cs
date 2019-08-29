using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.Framework.SQLAdapter
{
    public class HRMBusinessAdapter
    {
        private const string RelationName = "%Con%";
        private const string WorkStatusWorking = "%Đang làm việc%";
        private const int Age = 12;

        /// <summary>
        /// Báo cáo danh sách đảng viên
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_PartyMember(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += " SELECT rc.FullName, " +
                   " rc.BirthDate, " +
                   " rc.EmployeeCode, " +
                   " rc.BirthDate, " +
                   " case when rc.Sex = 1 then N'Nam' " +
                   " else N'Nữ' end SexName, " +
                   "     rc.Address, " +
                   " rc.CPVJoinedDate, " +
                   " rc.CPVOfficialJoinedDate, " +
                   " rc.CPVJoinedPlace, " +
                   " rc.CPVCardNumber, " +
                   " (select top 1 cp.Name from cat_CPVPosition cp where cp.Id = rc.CPVPositionId) AS CPVJoinedPosition, " +
                   " (select top 1 cf.Name from cat_Folk cf where cf.Id = rc.FolkId) AS FolkName, " +
                   " (select top 1 cj.Name from cat_JobTitle cj where cj.Id = rc.JobTitleId) AS JobTitleName, " +
                   " (select top 1 pl.Name from cat_PoliticLevel pl where pl.Id = rc.PoliticLevelId) AS PoliticLevelName, " +
                   " (select top 1 hs.SalaryInsurance from hr_Salary hs where hs.RecordId = rc.Id " +
                   "    and hs.EffectiveDate = (select MAX(hs2.EffectiveDate) from hr_Salary hs2 where hs2.RecordId = rc.Id)) " +
                   " 		as SalaryInsurance, " +
                   "     p.Name AS PositionName, " +
                   "     dv.Id AS DepartmentId, " +
                   "     dv.Name AS DepartmentName,  " +
                   "     rc.CellPhoneNumber " +
                   "     FROM hr_Record rc " +
                   " LEFT JOIN cat_Position p on rc.PositionId = p.Id " +
                   " LEFT JOIN cat_Department dv ON dv.Id = rc.DepartmentId " +
                   " WHERE rc.CPVJoinedDate IS NOT NULL";

            if(!string.IsNullOrEmpty(department))
                sql += " and rc.DepartmentId IN ({0})".FormatWith(department);
            if(!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND rc.CPVJoinedDate >= '{0}'".FormatWith(fromDate);
            }
            if(!string.IsNullOrEmpty(toDate))
            {
                sql += " AND rc.CPVJoinedDate <= '{0}'".FormatWith(toDate);
            }
            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            return sql;
        }

        /// <summary>
        /// Báo cáo danh sách đoàn viên
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_UnionMember(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += " select	 " +
                   " rc.FullName,	 " +
                   " rc.EmployeeCode,	 " +
                   " cp.Name as PositionName,	 " +
                   " cj.Name as JobTitleName,	 " +
                   " rc.VYUJoinedDate,	 " +
                   "     dv.Id AS DepartmentId, " +
                   "     dv.Name AS DepartmentName,  " +
                   " (select top 1 hs.SalaryInsurance from hr_Salary hs where hs.RecordId = rc.Id 	 " +
                   " 	and hs.EffectiveDate = (select MAX(hs2.EffectiveDate) from hr_Salary hs2 where hs2.RecordId = rc.Id))  " +
                   " 	as SalaryInsurance " +
                   " from hr_Record rc	 " +
                   " left join cat_Position cp on cp.Id = rc.PositionId	 " +
                   " left join cat_Department dv on dv.Id = rc.DepartmentId	 " +
                   " left join cat_JobTitle cj on cj.Id = rc.JobTitleId	 " +
                   " WHERE rc.VYUJoinedDate IS NOT NULL";
            if(!string.IsNullOrEmpty(department))
                sql += " and rc.DepartmentId IN ({0})".FormatWith(department);
            if(!string.IsNullOrEmpty(fromDate))
                sql += " and rc.VYUJoinedDate >= '{0}'".FormatWith(fromDate);
            if (!string.IsNullOrEmpty(toDate))
                sql += " and rc.VYUJoinedDate <= '{0}'".FormatWith(toDate);
            if(!string.IsNullOrEmpty(condition))
                sql += " AND {0}".FormatWith(condition);
            return sql;
        }

        /// <summary>
        /// Báo cáo danh sách quân nhân
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_MilitaryList(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += " select " +
                   " rc.FullName, " +
                   " rc.EmployeeCode, " +
                   " rc.ArmyJoinedDate, " +
                   " rc.ArmyLeftDate, " +
                   " rc.CellPhoneNumber, " +
                   " cp.Name as PoliticLevelName, " +
                   "     dv.Id AS DepartmentId, " +
                   "     dv.Name AS DepartmentName,  " +
                   " ca.Name as ArmyLevelName, " +
                   " ce.Name as EducationName " +
                   " from hr_Record rc " +
                   " left join cat_PoliticLevel cp on cp.Id = rc.PoliticLevelId " +
                   " left join cat_ArmyLevel ca on ca.Id = rc.ArmyLevelId " +
                   " left join cat_Department dv on dv.Id = rc.DepartmentId	 " +
                   " left join cat_Education ce on ce.Id = rc.EducationId " +
                   " WHERE rc.ArmyJoinedDate IS NOT NULL";
            if(!string.IsNullOrEmpty(department))
                sql += " and rc.DepartmentId IN ({0})".FormatWith(department);
            if(!string.IsNullOrEmpty(fromDate))
                sql += " and rc.ArmyJoinedDate >= '{0}'".FormatWith(fromDate);
            if(!string.IsNullOrEmpty(toDate))
                sql += " and rc.ArmyJoinedDate <= '{0}'".FormatWith(toDate);
            if(!string.IsNullOrEmpty(condition))
                sql += " AND {0}".FormatWith(condition);
            return sql;
        }

        /// <summary>
        /// Danh sách hợp đồng của nhân viên
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_ContractOfEmployee(int recordId, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += " select  " +
                   " hc.ContractNumber, " +
                   " cc.Name as ContractTypeName, " +
                   " cj.Name as JobName, " +
                   " hc.ContractDate, " +
                   " hc.EffectiveDate, " +
                   " hc.ContractEndDate, " +
                   " cs.Name as ContractStatusName " +
                   " from hr_Contract hc " +
                   " left join cat_ContractType cc on cc.Id = hc.ContractTypeId " +
                   " left join cat_ContractStatus cs on cs.Id = hc.ContractStatusId " +
                   " left join cat_JobTitle cj on cj.Id = hc.JobId " +
                   " where hc.RecordId = {0} ".FormatWith(recordId);
            if(!string.IsNullOrEmpty(fromDate))
                sql += " and hc.ContractDate >= '{0}'".FormatWith(fromDate);
            if(!string.IsNullOrEmpty(toDate))
                sql += " and hc.ContractDate <='{0}'".FormatWith(toDate);
            if(!string.IsNullOrEmpty(condition))
                sql += " AND {0}".FormatWith(condition);
            return sql;
        }

        /// <summary>
        /// Calculate seniority
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static string GetStore_CalculateSeniority(int recordId)
        {
            var sql = string.Empty;
            sql += " SELECT " +
                   "   CASE WHEN (DATEDIFF(DAY, hr.ParticipationDate, getDate())/365) = 0 THEN '' ELSE " +
                   "   CAST(DATEDIFF(DAY, hr.ParticipationDate, getDate())/365 AS NVARCHAR(10)) + N' Năm ' END + " +
                   "   CASE WHEN ((DATEDIFF(DAY, hr.ParticipationDate, getDate())%365) /30) = 0 THEN '' ELSE " +
                   "   CAST((DATEDIFF(DAY, hr.ParticipationDate, getDate())%365) /30 AS NVARCHAR(10)) + N' Tháng ' END + " +
                   "   CASE WHEN ((DATEDIFF(DAY, hr.ParticipationDate, getDate())%365) %30) = 0 THEN '' ELSE " +
                   "   CAST((DATEDIFF(DAY, hr.ParticipationDate, getDate())%365) %30 AS NVARCHAR(10)) + N' Ngày' END AS 'Seniority' " +
                   " FROM hr_Record hr " +
                   " WHERE hr.Id = {0}".FormatWith(recordId);

            return sql;
        }

        /// <summary>
        /// Báo cáo danh sách hết hạn hợp đồng lao động
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeExpried(string departments, string fromDate, string toDate, string condition)
        {
            var sql = " select	" +
                         " rc.EmployeeCode,	" +
                         " rc.FullName,	" +
                         " cp.Name as PositionName,	" +
                         " dv.Name as DepartmentName,	" +
                         " rc.DepartmentId,	" +
                         " hc.ContractDate,	" +
                         " hc.ContractEndDate,	" +
                         " cc.Name as ContractTypeName	" +
                         " from hr_Contract hc	" +
                         " left join hr_Record rc on rc.Id = hc.RecordId	" +
                         " left join cat_Department dv on dv.Id = rc.DepartmentId	" +
                         " left join cat_Position cp on cp.Id = rc.PositionId	" +
                         " left join cat_ContractType cc on cc.Id = hc.ContractTypeId	" +
                         " where hc.ContractEndDate is not null AND ContractEndDate < GETDATE() ";
            if(!string.IsNullOrEmpty(departments))
                sql += " AND rc.DepartmentId IN ({0}) ".FormatWith(departments);
            if(!string.IsNullOrEmpty(fromDate))
                sql += " and hc.ContractEndDate >= '{0}'".FormatWith(fromDate);
            if(!string.IsNullOrEmpty(toDate))
                sql += " and hc.ContractEndDate <= '{0}'".FormatWith(toDate);
            if(!string.IsNullOrEmpty(condition))
                sql += " AND {0}".FormatWith(condition);
            return sql;
        }

        /// <summary>
        /// Báo cáo danh sách nhân viên đang trong thời gian thử việc
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeTrainee(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += " select " +
                   " rc.EmployeeCode, " +
                   " rc.FullName, " +
                   " cp.Name as PositionName, " +
                   " dv.Id as DepartmentId, " +
                   " dv.Name as DepartmentName, " +
                   " rc.RecruimentDate as ProbationDate, " +
                   " rc.ParticipationDate as ProbationEndDate " +
                   " from hr_Record rc " +
                   " left join cat_Position cp on cp.Id = rc.PositionId " +
                   " left join cat_Department dv on dv.Id = rc.DepartmentId " +
                   " where rc.RecruimentDate IS NOT NULL AND  rc.RecruimentDate <= GETDATE() AND GETDATE() < rc.ParticipationDate";
            if(!string.IsNullOrEmpty(department))
                sql += " and rc.DepartmentId IN ({0})".FormatWith(department);
            if(!string.IsNullOrEmpty(fromDate))
                sql += " and rc.RecruimentDate >= '{0}'".FormatWith(fromDate);
            if(!string.IsNullOrEmpty(toDate))
                sql += " and rc.RecruimentDate <= '{0}'".FormatWith(toDate);
            if(!string.IsNullOrEmpty(condition))
                sql += " AND {0}".FormatWith(condition);
            return sql;
        }

        /// <summary>
        /// Báo cáo danh sách đóng BHXH
        /// </summary>
        /// <param name="department"></param>
        /// <param name="condition"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static string GetStore_InsuranceList(string department, string condition, string fromDate, string toDate)
        {
            var sql = string.Empty;
            sql += " select " +
                   " rc.EmployeeCode," +
                   " rc.FullName," +
                   " rc.BirthDate," +
                   " rc.InsuranceNumber," +
                   " cp.Name as PositionName," +
                   " dv.Id as DepartmentId," +
                   " dv.Name as DeparmentName," +
                   " rc.InsuranceIssueDate," +
                   " '0' as InsuranceAmount," +
                   " '0' as LaborAmount," +
                   " '0' as EnterpriseAmount," +
                   " '0' as TotalAmount" +
                   " from hr_Record rc" +
                   " left join cat_Position cp on cp.Id = rc.PositionId" +
                   " left join cat_Department dv on dv.Id = rc.DepartmentId" +
                   " where rc.InsuranceNumber is not null and rc.InsuranceNumber <> ''";
            if(!string.IsNullOrEmpty(department))
                sql += " AND rc.DepartmentId IN ({0}) ".FormatWith(department);
            if(!string.IsNullOrEmpty(fromDate))
                sql += " AND rc.InsuranceIssueDate >= '{0}'".FormatWith(fromDate);
            if(!string.IsNullOrEmpty(toDate))
                sql += " AND rc.InsuranceIssueDate <= '{0}'".FormatWith(toDate);
            if(!string.IsNullOrEmpty(condition))
                sql += " AND {0}".FormatWith(condition);
            return sql;
        }

        /// <summary>
        /// Báo cáo danh sách tăng / giảm BHXH
        /// </summary>
        /// <param name="department"></param>
        /// <param name="direction"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_InsuranceIncreaseDecrease(string department, bool direction, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += "IF OBJECT_ID('tempdb..#tmpA') IS NOT NULL DROP Table #tmpA " +
                   "select " +
                   " rc.FullName," +
                   " rc.InsuranceNumber," +
                   " cp.Name as PositionName," +
                   " dv.Id as DepartmentId," +
                   " dv.Name as DepartmentName," +
                   " '' as Note," +
                   " hs.SalaryBasic," +
                   " hs.PositionAllowance," +
                   " 0 as SeniorityOutOfFrameAllowance," +
                   " 0 as SeniorityJobAllowance," +
                   " 0 as SalaryAllowance," +
                   " 0 as AdditionAllowance," +
                   " hs.EffectiveDate as FromDate," +
                   " hs.EffectiveEndDate as ToDate, " +
                   " 0 as TotalSalaryBasic " +
                   " INTO #tmpA " +
                   " from  hr_FluctuationInsurance fi" +
                   " left join hr_Record rc on rc.Id = fi.RecordId" +
                   " left join cat_Position cp on cp.Id = rc.PositionId" +
                   " left join cat_Department dv on dv.Id = rc.DepartmentId" +
                   " left join hr_Salary hs on hs.RecordId = rc.Id" +
                   " where hs.EffectiveDate = (select MAX(hs2.EffectiveDate) from hr_Salary hs2 where hs2.RecordId = rc.Id)";
            if (direction == false)
            {
                sql += " and fi.Type = 0 ";//Tang
            }
            else
            {
                sql += " and fi.Type = 1 ";//Giam
            }
            if (!string.IsNullOrEmpty(department))
                sql += " AND rc.DepartmentId IN ({0}) ".FormatWith(department);
            if (!string.IsNullOrEmpty(condition))
                sql += " AND {0} ".FormatWith(condition);

            sql += " UPDATE #tmpA " +
                   " set TotalSalaryBasic = xSalaryBasic " +
                   " FROM #tmpA " +
                   " INNER JOIN ( " +
                   " select SUM(SalaryBasic) as xSalaryBasic " +
                   " from #tmpA " +
                   " )#tmp ON 1 = 1 " +
                   " select * from #tmpA ";
            if (!string.IsNullOrEmpty(fromDate))
            {
                sql += "hs.EffectiveDate >= '{0}'".FormatWith(fromDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                sql += "hs.EffectiveDate <= '{0}'".FormatWith(toDate);
            }
            return sql;
        }

        /// <summary>
        ///Báo cáo thống kê nghề nghiệp
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_StatisticOccupation(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += " select " +
                   " rc.EmployeeCode," +
                   " rc.FullName ," +
                   " rc.BirthDate," +
                   " dv.Id as DepartmentId," +
                   " dv.Name as DepartmentName," +
                   " cp.Name as PositionName," +
                   " cj.Name as JobTitleName," +
                   " ci.Name as Occupation," +
                   " rc.ParticipationDate as WorkDate" +
                   " from hr_Record rc" +
                   " left join cat_Department dv on dv.Id = rc.DepartmentId" +
                   " left join cat_Position cp on cp.Id = rc.PositionId" +
                   " left join cat_JobTitle cj on cj.Id = rc.JobTitleId" +
                   " left join cat_Industry ci on ci.Id = rc.IndustryId" +
                   " where 1 = 1 ";

            if (!string.IsNullOrEmpty(department))
                sql += " AND rc.DepartmentId IN ({0}) ".FormatWith(department);
            if (!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND rc.ParticipationDate >= '{0}'".FormatWith(fromDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                sql += " AND rc.ParticipationDate <= '{0}'".FormatWith(toDate);
            }
            if (!string.IsNullOrEmpty(condition))
                sql += " AND {0}".FormatWith(condition);

            return sql;
        }


        /// <summary>
        /// Báo cáo nhân viên sinh nhật trong tháng
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_BornInMonth(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += " select * from (select " +
                   " rc.EmployeeCode, " +
                   " rc.FullName, " +
                   " rc.BirthDate, " +
                   " dv.Id as DepartmentId, " +
                   " dv.Name as DepartmentName, " +
                   " cp.Name as PositionName, " +
                   "  te.WorkingFormId, " +
                   " CASE WHEN te.UnionJoinedDate IS NULL THEN 0 ELSE 1 END AS IsUnionMember," +
                   " CAST(DATEDIFF(YEAR, rc.BirthDate, GETDATE()) AS nvarchar(10)) + N' Tuổi' as Age " +
                   " from hr_Record rc " +
                   " left join cat_Department dv on dv.Id = rc.DepartmentId " +
                   " left join cat_Position cp on cp.Id = rc.PositionId " +
                   " left join hr_Team te on rc.Id = te.RecordId )#tmpA" +
                   " where 1 = 1 ";
            if (!string.IsNullOrEmpty(department))
                sql += " and DepartmentId IN ({0})".FormatWith(department);
            if (!string.IsNullOrEmpty(fromDate))
                sql += " and rc.BirthDate >= '{0}'".FormatWith(fromDate);
            if (!string.IsNullOrEmpty(toDate))
                sql += " and rc.BirthDate <= '{0}'".FormatWith(toDate);
            if(!string.IsNullOrEmpty(condition))
                sql += " AND {0}".FormatWith(condition);
            return sql;
        }


        /// <summary>
        /// Danh sách cán bộ nhân viên là nữ
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeIsFemale(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += " select " +
                   " rc.EmployeeCode," +
                   " rc.FullName ," +
                   " rc.BirthDate," +
                   " dv.Id as DepartmentId," +
                   " dv.Name as DepartmentName," +
                   " cp.Name as PositionName," +
                   " rc.ParticipationDate as StartDate," +
                   " '' as Note" +
                   " from hr_Record rc" +
                   " left join cat_Department dv on dv.Id = rc.DepartmentId" +
                   " left join cat_Position cp on cp.Id = rc.PositionId" +
                   " where rc.Sex = 0" +
                   " and rc.RecruimentDate IS NOT NULL";

            if (!string.IsNullOrEmpty(department))
                sql += " AND rc.DepartmentId IN ({0}) ".FormatWith(department);
            if (!string.IsNullOrEmpty(condition))
                sql += " AND {0}".FormatWith(condition);

            return sql;
        }


        /// <summary>
        /// DANH SÁCH CÁC CON ĐƯỢC TẶNG QUÀ 01/06/
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_ChildernDayGift(string department,string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += " select " +
                   " rc.EmployeeCode," +
                   " rc.FullName as ParentName," +
                   " fr.FullName as ChildName," +
                   " dv.Id as DepartmentId," +
                   " dv.Name as DepartmentName," +
                   " case when fr.Sex = 1 then N'Nam' else N'Nữ' end ChildSexName," +
                   " fr.BirthYear," +
                   " YEAR(GETDATE()) - fr.BirthYear AS Age, " +
                   " rc.Address," +
                   " rc.CellPhoneNumber" +
                   " from hr_Record rc" +
                   " left join hr_FamilyRelationship fr on fr.RecordId = rc.Id" +
                   " left join cat_Department dv on dv.Id = rc.DepartmentId" +
                   " left join cat_Relationship cr on cr.Id = fr.RelationshipId" +
                   " left join cat_WorkStatus cw on cw.Id = rc.WorkStatusId" +
                   " where YEAR(getDate()) - fr.BirthYear <= {0}".FormatWith(Age) +
                   " AND cr.Name LIKE N'{0}'".FormatWith(RelationName) +
                   " AND cw.Name LIKE N'{0}'".FormatWith(WorkStatusWorking);

            if (!string.IsNullOrEmpty(department))
                sql += " AND rc.DepartmentId IN ({0}) ".FormatWith(department);
            if (!string.IsNullOrEmpty(condition))
                sql += " AND {0}".FormatWith(condition);

            return sql;
        }
    }
}
