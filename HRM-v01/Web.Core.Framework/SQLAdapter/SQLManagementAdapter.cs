using System;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework.Adapter
{
    /// <summary>
    /// Summary description for SQLManagementAdapter
    /// </summary>
    public class SQLManagementAdapter
    {
        private const string BusinessTypePlanJobTitle = "PlanJobTitle";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstDepartment"></param>
        /// <param name="departmentId"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="departmentSelected"></param>
        /// <returns></returns>
        public static string GetStore_GetEmployeeIsCPV(string lstDepartment, string departmentId, string searchKey, int start,
            int limit, string departmentSelected)
        {
            var sql = "SELECT * FROM( " +
                      " SELECT rc.Id," +
                      " rc.EmployeeCode," +
                      " rc.FullName," +
                      " dp.Name AS DepartmentName, " +
                      "  rc.CPVOfficialJoinedDate," +
                      " rc.CPVJoinedDate," +
                      " rc.CPVCardNumber," +
                      " rc.CPVJoinedPlace," +
                      " rc.CPVPositionId," +
                      " cpv.Name AS CPVPositionName," +
                      "  dp3.Name AS ParentDepartmentName," +
                      " ROW_NUMBER() OVER(ORDER BY rc.FullName ASC) SK" +
                      " FROM hr_Record rc " +
                      " LEFT JOIN cat_Department dp" +
                      " ON rc.DepartmentId = dp.Id" +
                      " LEFT JOIN cat_CPVPosition cpv" +
                      " ON rc.CPVPositionId = cpv.Id" +
                      " LEFT JOIN cat_Department dp2" +
                      " ON dp.ParentId = dp2.Id" +
                      " LEFT JOIN cat_Department dp3" +
                      " ON dp2.ParentId = dp3.Id" +
                      " WHERE rc.CPVOfficialJoinedDate IS NOT NULL";
            if(!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND(LEN('{0}') = 0".FormatWith(searchKey) +
                       " OR rc.FullName like N'%{0}%'".FormatWith(searchKey) +
                       " OR rc.EmployeeCode like N'%{0}%'".FormatWith(searchKey) +
                       "    )";
            }
            if(!string.IsNullOrEmpty(departmentId))
            {
                sql += " AND (rc.DepartmentId IN({0}))".FormatWith(lstDepartment);
            }
            if(!string.IsNullOrEmpty(departmentSelected))
            {
                sql += "AND rc.DepartmentId = '{0}' ".FormatWith(departmentSelected);
            }

            // " AND (LEN({0}))".FormatWith(departmentId) +
            //" OR rc.DepartmentId = {0}".FormatWith(departmentId) +
            sql += "   )#tmp" +
            " WHERE #tmp.SK > {0}".FormatWith(start) + "" +
            " AND #tmp.SK <= {0}".FormatWith(start + limit) +
            " ORDER BY [Id]";
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstDepartment"></param>
        /// <param name="departmentId"></param>
        /// <param name="searchKey"></param>
        /// <param name="departmentSelected"></param>
        /// <returns></returns>
        public static string GetStore_CountGetEmployeeIsCPV(string lstDepartment, string departmentId, string searchKey, string departmentSelected)
        {
            var sql = "SELECT COUNT (*) FROM hr_Record rc" +
                         " WHERE rc.CPVOfficialJoinedDate IS NOT NULL";
            if(!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND(LEN('{0}') = 0".FormatWith(searchKey) +
                       " OR rc.FullName like N'%{0}%'".FormatWith(searchKey) +
                       " OR rc.EmployeeCode like N'%{0}%'".FormatWith(searchKey) +
                       "    )";
            }
            if(!string.IsNullOrEmpty(departmentId))
            {
                sql += " AND (rc.DepartmentId IN({0}))".FormatWith(lstDepartment);
            }
            if(!string.IsNullOrEmpty(departmentSelected))
            {
                sql += "AND rc.DepartmentId = '{0}' ".FormatWith(departmentSelected);
            }
         
            return sql;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="searchKey"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="form"></param>
        /// <param name="level"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static string GetStore_GetDepartmentReward(string departments, string searchKey, string fromDate,
            string toDate, int form, int level, int? start, int? limit)
        {
            var sql = "SELECT * FROM (SELECT rp.Id," +
                      " rp.Name," +
                      " rp.DecisionNumber," +
                      " rp.DecisionDate," +
                      " rp.Reason," +
                      " rw.Name AS FormName," +
                      " rp.Note," +
                      " rp.MoneyAmount," +
                      " rp.DecisionMaker," +
                      " dp.Name AS DepartmentName," +
                      " lrp.Name AS LevelName," +
                      " ROW_NUMBER() OVER(ORDER BY rp.Id ASC) SK" +
                      " FROM hr_RewardDepartment rp" +
                      " LEFT JOIN cat_Reward rw" +
                      " ON rp.FormRewardId = rw.Id" +
                      " LEFT JOIN cat_Department dp" +
                      " ON rp.DepartmentId = dp.Id" +
                      " LEFT JOIN cat_LevelRewardDiscipline lrp" +
                      " ON rp.LevelRewardId = lrp.Id" +
                      " WHERE 1 = 1";
            if(!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND(" +
                       " LEN('{0}') = 0".FormatWith(searchKey) +
                       " OR rp.Name LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR rp.DecisionNumber LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR rp.DecisionMaker LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR rp.Note LIKE N'%{0}%'".FormatWith(searchKey) +
                       " )";
            }
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND(rp.DepartmentId IN({0}))".FormatWith(departments);
            }

            sql += " AND('{0}' IS NULL ".FormatWith(fromDate) +
                   "OR rp.DecisionDate IS NULL " +
                   "OR YEAR('{0}') = 1 ".FormatWith(fromDate) +
                   "OR YEAR('{0}') = 1900 ".FormatWith(fromDate) +
                   "OR rp.DecisionDate >= '{0}')".FormatWith(fromDate) +
                   " AND('{0}' IS NULL ".FormatWith(toDate) +
                   "OR rp.DecisionDate IS NULL " +
                   "OR YEAR('{0}') = 1 ".FormatWith(toDate) +
                   "OR YEAR('{0}') = 1900 ".FormatWith(toDate) +
                   "OR rp.DecisionDate <= '{0}')".FormatWith(toDate);
            if(level != 0)
            {
                sql += " AND(rp.LevelRewardId = {0})".FormatWith(level);
            }
            if(form != 0)
            {
                sql += " AND(rp.FormRewardId = {0})".FormatWith(form);
            }
            sql += " )#tmp";
            if(start != null & limit != null)
            {
                sql += " WHERE #tmp.SK > {0}".FormatWith(start) +
                      "AND #tmp.SK <= {0}".FormatWith(start + limit);
            }
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="searchKey"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="form"></param>
        /// <param name="level"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static string GetStore_GetDepartmentDiscipline(string departments, string searchKey, string fromDate,
            string toDate, int form, int level, int? start, int? limit)
        {
            var sql = "SELECT * FROM (SELECT rp.Id," +
                      " rp.Name," +
                      " rp.DecisionNumber," +
                      " rp.DecisionDate," +
                      " rp.Reason," +
                      " rw.Name AS FormName," +
                      " rp.Note," +
                      " rp.MoneyAmount," +
                      " rp.DecisionMaker," +
                      " dp.Name AS DepartmentName," +
                      " lrp.Name AS LevelName," +
                      " ROW_NUMBER() OVER(ORDER BY rp.Id ASC) SK" +
                      " FROM hr_DisciplineDepartment rp" +
                      " LEFT JOIN cat_Discipline rw" +
                      " ON rp.FormDisciplineId = rw.Id" +
                      " LEFT JOIN cat_Department dp" +
                      " ON rp.DepartmentId = dp.Id" +
                      " LEFT JOIN cat_LevelRewardDiscipline lrp" +
                      " ON rp.LevelDisciplineId = lrp.Id" +
                      " WHERE 1 = 1";
            if(!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND(" +
                       " LEN('{0}') = 0".FormatWith(searchKey) +
                       " OR rp.Name LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR rp.DecisionNumber LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR rp.DecisionMaker LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR rp.Note LIKE N'%{0}%'".FormatWith(searchKey) +
                       " )";
            }
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND(rp.DepartmentId IN({0}))".FormatWith(departments);
            }

            sql += " AND('{0}' IS NULL ".FormatWith(fromDate) +
                   "OR rp.DecisionDate IS NULL " +
                   "OR YEAR('{0}') = 1 ".FormatWith(fromDate) +
                   "OR YEAR('{0}') = 1900 ".FormatWith(fromDate) +
                   "OR rp.DecisionDate >= '{0}')".FormatWith(fromDate) +
                   " AND('{0}' IS NULL ".FormatWith(toDate) +
                   "OR rp.DecisionDate IS NULL " +
                   "OR YEAR('{0}') = 1 ".FormatWith(toDate) +
                   "OR YEAR('{0}') = 1900 ".FormatWith(toDate) +
                   "OR rp.DecisionDate <= '{0}')".FormatWith(toDate);
            if(level != 0)
            {
                sql += " AND(rp.LevelDisciplineId = {0})".FormatWith(level);
            }
            if(form != 0)
            {
                sql += " AND(rp.FormDisciplineId = {0})".FormatWith(form);
            }
            sql += " )#tmp";
            if(start != null & limit != null)
            {
                sql += " WHERE #tmp.SK > {0}".FormatWith(start) +
                       "AND #tmp.SK <= {0}".FormatWith(start + limit);
            }
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static string GetStore_GetListAsset(string departments, string searchKey, int? start, int? limit)
        {
            var sql = " SELECT * FROM(" +
                    " SELECT " +
                    " ha.Id," +
                    " ha.RecordId," +
                    " rc.FullName," +
                    " rc.EmployeeCode," +
                    " ha.AssetName," +
                    " ha.AssetCode," +
                    " ha.Quantity," +
                    " ha.Status," +
                    " ha.ReceiveDate," +
                    " ha.UnitCode," +
                    " ha.Note," +
                    " ROW_NUMBER() OVER(ORDER BY rc.FullName ASC) SK" +
                    " FROM hr_Asset ha" +
                    " LEFT JOIN hr_Record rc ON rc.Id = ha.RecordId" +
                    " WHERE 1=1					";
            if(!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND (rc.FullName LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR rc.EmployeeCode LIKE N'%{0}%') ".FormatWith(searchKey);
            }
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN({0})".FormatWith(departments);
            }
            sql += " )#tmp					";
            if(start != null && limit != null)
            {
                sql += " WHERE #tmp.SK > {0}".FormatWith(start) +
                   " AND #tmp.SK <= {0}".FormatWith(start + limit);
            }
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static string GetStore_GetListAttachFile(string departments, string searchKey, int? start, int? limit)
        {
            var sql = " SELECT * FROM(" +
                    " SELECT " +
                    " ha.Id," +
                    " ha.RecordId," +
                    " rc.FullName," +
                    " rc.EmployeeCode," +
                     "ha.AttachFileName," +
                     "ha.URL," +
                     "ha.SizeKB," +
                     "ha.Note," +
                     "ha.CreatedDate," +
                    " ROW_NUMBER() OVER(ORDER BY rc.FullName ASC) SK" +
                    " FROM hr_AttachFile ha" +
                    " LEFT JOIN hr_Record rc ON rc.Id = ha.RecordId" +
                    " WHERE 1=1					";
            if(!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND (rc.FullName LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR rc.EmployeeCode LIKE N'%{0}%') ".FormatWith(searchKey);
            }
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN({0})".FormatWith(departments);
            }
            sql += " )#tmp					";
            if(start != null && limit != null)
            {
                sql += " WHERE #tmp.SK > {0}".FormatWith(start) +
                   " AND #tmp.SK <= {0}".FormatWith(start + limit);
            }
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static string GetStore_GetListBank(string departments, string searchKey, int? start, int? limit)
        {
            var sql = " SELECT * FROM(" +
                    " SELECT " +
                    " hb.Id," +
                    " hb.RecordId," +
                    " rc.FullName," +
                    " rc.EmployeeCode," +
                    " hb.AccountName," +
                    " hb.AccountNumber," +
                    " hb.IsInUsed," +
                    " (SELECT TOP 1 cb.Name FROM cat_Bank cb WHERE cb.Id = hb.BankId  ) As 'BankName'," +
                    " hb.Note," +
                    " ROW_NUMBER() OVER(ORDER BY rc.FullName ASC) SK" +
                    " FROM hr_Bank hb" +
                    " LEFT JOIN hr_Record rc ON rc.Id = hb.RecordId" +
                    " WHERE 1=1					";
            if(!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND (rc.FullName LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR rc.EmployeeCode LIKE N'%{0}%') ".FormatWith(searchKey);
            }
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN({0})".FormatWith(departments);
            }
            sql += " )#tmp					";
            if(start != null && limit != null)
            {
                sql += " WHERE #tmp.SK > {0}".FormatWith(start) +
                   " AND #tmp.SK <= {0}".FormatWith(start + limit);
            }
            return sql;
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetStore_GetListTimeSheetSymbol(string searchKey, int? start, int? limit, string type)
        {
            var sql = " SELECT * FROM(" +
                    " SELECT * , " +
                    " (SELECT cg.Name FROM hr_TimeSheetGroupSymbol cg WHERE cg.[Id] = ct.[GroupSymbolId]) AS GroupSymbolName, " +
                    " ROW_NUMBER() OVER(ORDER BY ct.[Order]) SK " +
                    " FROM hr_TimeSheetSymbol ct " +
                    " WHERE 1=1	";
            if(!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND (ct.Name LIKE N'%{0}%'".FormatWith(searchKey) +
                        " OR ct.Description LIKE N'%{0}%' )".FormatWith(searchKey);
            }

            if (!string.IsNullOrEmpty(type))
            {
                sql += " AND ct.[Group] LIKE N'%{0}%' ".FormatWith(Constant.TimesheetOverTime);
            }

            sql += " )#tmp					";
            if(start != null && limit != null)
            {
                sql += " WHERE #tmp.SK > {0}".FormatWith(start) +
                   " AND #tmp.SK <= {0}".FormatWith(start + limit);
            }
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="recordIds"></param>
        /// <param name="searchKey"></param>
        /// <param name="workStatus"></param>
        /// <param name="position"></param>
        /// <param name="job"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="selectedDepartment"></param>
        /// <param name="isCount"></param>
        /// <returns></returns>
        public static string GetStore_ChooseEmployee(string departments, string recordIds , string searchKey, int workStatus, int position,
            int job, int? start, int? limit, string selectedDepartment, bool isCount)
        {
            var sql = " IF OBJECT_ID('tempdb..#tmp') IS NOT NULL DROP Table #tmp "+
                      " SELECT rc.EmployeeCode," +
                      " rc.Id,"+
                      " rc.FullName," +
                      " rc.BirthDate, " +
                      " CASE WHEN(rc.Sex = 1) THEN 'Nam' ELSE N'Nữ' END AS SexName," +
                      " dp.Name AS DepartmentName," +
                      " po.Name AS PositionName," +
                      " jt.Name AS JobTitleName," +
                      " ROW_NUMBER() OVER(ORDER BY rc.EmployeeCode ASC) AS RN" +
                      " INTO #tmp " +
                      " FROM hr_Record rc" +
                      " LEFT JOIN cat_Department dp" +
                      " ON rc.DepartmentId = dp.Id" +
                      " LEFT JOIN cat_Position po" +
                      " ON rc.PositionId = po.Id" +
                      " LEFT JOIN cat_JobTitle jt" +
                      " ON rc.JobTitleId = jt.Id" +
                      " WHERE 1 = 1 and rc.[Type] = 0 ";//Case of record
            if(!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND (rc.FullName LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR rc.EmployeeCode LIKE N'%{0}%')".FormatWith(searchKey);
            }
            if(position != 0)
            {
                sql += " AND rc.PositionId = {0}".FormatWith(position);
            }
            if(job != 0)
            {
                sql += " AND rc.JobTitleId = {0}".FormatWith(job);
            }
            if(workStatus != 0)
            {
                sql += " AND rc.WorkStatusId = {0}".FormatWith(workStatus);
            }
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN({0})".FormatWith(departments);
            }

            if (!string.IsNullOrEmpty(recordIds))
            {
                sql += " AND rc.Id IN({0})".FormatWith(recordIds);
            }

            if (!string.IsNullOrEmpty(selectedDepartment))
            {
                sql += " AND rc.DepartmentId IN({0})".FormatWith(selectedDepartment);
            }

            if (!isCount)
            {
                sql += " SELECT * FROM #tmp " +
                       " WHERE 1 = 1 ";
                if (start != null && limit != null)
                {
                    sql += "AND(#tmp.RN > {0}".FormatWith(start) +
                           " AND #tmp.RN <= {0})".FormatWith(start + limit);
                }
            }
            else
            {
                sql += " SELECT COUNT(*) FROM #tmp ";
            }
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="departments"></param>
        /// <returns></returns>
        public static string GetStore_SearchUser(string searchKey, string departments)
        {
            var sql = " SELECT rc.Id, rc.EmployeeCode, rc.FullName, rc.BirthDate, dp.Name AS DepartmentName" +
                      " FROM hr_Record rc" +
                      " LEFT JOIN cat_Department dp" +
                      " ON rc.DepartmentId = dp.Id" +
                      " WHERE [FullName] LIKE N'%{0}%'".FormatWith(searchKey) +
                      " AND [DepartmentId] IN ({0})".FormatWith(departments);
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="typeTimeSheet"></param>
        /// <returns></returns>
        public static string GetStore_GetTimeSheetReportList(string searchKey, int? start, int? limit, string typeTimeSheet)
        {
            var sql = " SELECT * FROM(" +
                      " SELECT * , " +
                      " ROW_NUMBER() OVER(ORDER BY ts.CreatedDate) SK " +
                      " FROM hr_TimeSheetReport ts " +
                      " WHERE 1=1	";
            if(!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND (ts.Name LIKE N'%{0}%') ".FormatWith(searchKey);
            }
            //if(!string.IsNullOrEmpty(typeTimeSheet))
            //{
            //    sql += " AND ts.Type = N'{0}' ".FormatWith(typeTimeSheet);
            //}
            //else
            //{
            //    sql += " AND ts.Type IS NULL ";
            //}

            sql += " )#tmp					";
            if(start != null && limit != null)
            {
                sql += " WHERE #tmp.SK > {0}".FormatWith(start) +
                       " AND #tmp.SK <= {0}".FormatWith(start + limit);
            }
            return sql;
        }

        /// <summary>
        /// get list adjustment
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="searchKey"></param>
        /// <param name="departmentSelected"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="isCount"></param>
        /// <param name="adjustType"></param>
        /// <returns></returns>
        public static string GetStore_GetListTimeSheetAdjustment(string departments, string searchKey, string departmentSelected, int? start, int? limit, bool isCount, string adjustType)
        {
            var sql = " IF OBJECT_ID('tempdb..#tmp') IS NOT NULL DROP Table #tmp " +
                      "	SELECT	 " +
                      "	rc.FullName,	 " +
                      "	rc.EmployeeCode,	 " +
                      "	rc.DepartmentId,	 " +
                      "	ta.RecordId,	 " +
                      "	ta.TimeSheetCode,	 " +
                      "	ta.Id,	 " +
                      "	ta.Reason,	 " +
                      " ta.AdjustDate," +
                      " ta.CreatedDate, " +
                      " ta.CreatedBy, " +
                      " ta.Detail, " +
                      "	(SELECT TOP 1 cd.Name FROM cat_Department cd WHERE cd.Id = rc.DepartmentId) AS DepartmentName,	 " +
                      "	ROW_NUMBER() OVER(ORDER BY rc.FullName ASC) SK	 " +
                      " INTO #tmp " +
                      "	FROM hr_TimeSheetAdjustment ta	 " +
                      "	LEFT JOIN hr_Record rc ON rc.Id = ta.RecordId	 " +
                      "	WHERE 1 =1	 ";

            if(!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND (rc.FullName LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR rc.EmployeeCode LIKE N'%{0}%' ".FormatWith(searchKey) +
                       " OR ta.AdjustDate LIKE N'%{0}%' ".FormatWith(searchKey) +
                       " OR ta.Reason LIKE N'%{0}%') ".FormatWith(searchKey);
            }

            if(!string.IsNullOrEmpty(departmentSelected))
            {
                var rootId = 0;
                var selectedDepartment = "{0},".FormatWith(departmentSelected);
                if(int.TryParse(departmentSelected, out var parseId))
                {
                    rootId = parseId;
                }
                var lstDepartment = cat_DepartmentServices.GetTree(rootId).Select(d => d.Id).ToList();
                if(lstDepartment.Count > 0)
                {
                    selectedDepartment = lstDepartment.Aggregate(selectedDepartment, (current, d) => current + "{0},".FormatWith(d));
                }
                selectedDepartment = "{0}".FormatWith(selectedDepartment.TrimEnd(','));
                sql += " AND rc.DepartmentId IN ({0}) ".FormatWith(selectedDepartment);
            }

            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN({0})".FormatWith(departments);
            }

            //OverTime
            if (!string.IsNullOrEmpty(adjustType))
            {
                sql += " AND ta.Type = {0} ".FormatWith((int)TimeSheetAdjustmentType.AdjustmentOverTime);
            }

            if (!isCount)
            {
                sql += " SELECT * FROM #tmp ";
                if (start != null && limit != null)
                {
                    sql += " WHERE #tmp.SK > {0}".FormatWith(start) +
                           " AND #tmp.SK <= {0}".FormatWith(start + limit);
                }
            }
            else
            {
                sql += " SELECT COUNT(*) FROM #tmp ";
            }
           
            return sql;
        }

        /// <summary>
        /// Quản lý bổ nhiệm chức vụ
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="searchQuery"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="departmentSelected"></param>
        /// <returns></returns>
        public static string GetStore_ListWorkProcess(string departments, string searchQuery, int? start, int? limit, string departmentSelected)
        {
            var sql = "SELECT * FROM " +
                      " (SELECT rc.Id AS 'Id', " +
                      " wp.RecordId AS 'RecordId', " +
                      " rc.EmployeeCode AS 'EmployeeCode', " +
                      " rc.FullName AS 'FullName'," +
                      " dp.Name AS 'DepartmentName'," +
                      " wp.DecisionNumber AS DecisionNumber, " +
                      " wp.DecisionMaker AS 'DecisionMaker'," +
                      " wp.DecisionDate AS 'DecisionDate'," +
                      " wp.EffectiveDate AS 'EffectiveDate'," +
                      " ps.Name AS 'PositionName'," +
                      " wp.IsApproved AS 'IsApproved'," +
                      " wp.Id AS 'Key'," +
                      " (SELECT TOP 1 [Name] FROM cat_Department WHERE ParentId = 0) AS 'ManagementName'," +
                      " (DATEADD(YEAR, 5, wp.DecisionDate)) AS 'NewDecisionDate'," +
                      " wp.NewPositionId AS 'NewPositionId'," +
                      " ps2.Name AS 'NewPositionName'," +
                      " ROW_NUMBER() OVER(ORDER BY rc.FullName ASC) SK" +

                      " FROM hr_WorkProcess wp" +
                      " LEFT JOIN hr_Record rc" +
                      " ON wp.RecordId = rc.Id" +
                      " LEFT JOIN cat_Department dp" +
                      " ON rc.DepartmentId = dp.Id" +
                      " LEFT JOIN cat_Position ps" +
                      " ON ps.Id = rc.PositionId" +
                      " LEFT JOIN cat_Position ps2" +
                      " ON wp.NewPositionId = ps2.Id" +
                      " WHERE 1=1 ";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN({0})".FormatWith(departments);
            }
            if(!string.IsNullOrEmpty(departmentSelected))
            {
                var rootId = 0;
                var selectedDepartment = "{0},".FormatWith(departmentSelected);
                if(int.TryParse(departmentSelected, out var parseId))
                {
                    rootId = parseId;
                }
                var lstDepartment = cat_DepartmentServices.GetTree(rootId).Select(d => d.Id).ToList();
                if(lstDepartment.Count > 0)
                {
                    selectedDepartment = lstDepartment.Aggregate(selectedDepartment, (current, d) => current + "{0},".FormatWith(d));
                }
                selectedDepartment = "{0}".FormatWith(selectedDepartment.TrimEnd(','));
                sql += "AND rc.DepartmentId IN ({0}) ".FormatWith(selectedDepartment);
            }
            sql += "    ) A" +
                   " WHERE 1=1 ";
            if(start != null & limit != null)
            {
                sql += " AND A.SK > {0}".FormatWith(start) +
                      " AND A.SK <= ({0})".FormatWith(start + limit);
            }

            if(!string.IsNullOrEmpty(searchQuery))
            {
                sql += " AND A.FullName LIKE N'%{0}%'".FormatWith(searchQuery) +
                       " OR A.EmployeeCode LIKE N'%{0}%'".FormatWith(searchQuery);
            }
            return sql;
        }


        #region Home

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentIds"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static string GetStore_BirthdayOfEmployee(string departmentIds, DateTime? fromDate, DateTime? toDate, int? start, int? limit)
        {
            var sql = " SELECT ROW_NUMBER() OVER(ORDER BY day(rc.BirthDate)) RN," +
                      " rc.EmployeeCode," +
                      "  rc.FullName," +
                      "  CASE WHEN(rc.Sex = 1) THEN 'Nam' ELSE N'Nữ' END AS SexName," +
                      "  CASE rc.BirthDate WHEN '1900-01-01' THEN NULL ELSE rc.BirthDate END AS BirthDate," +
                      "  dv.Name AS 'DepartmentName'," +
                      "    (SELECT TOP 1 [Name] FROM cat_Department WHERE ParentId = 0) AS ParentDepartmentName" +
                      " INTO #tmp "+
                      "    FROM hr_Record rc" +
                      "        LEFT JOIN cat_Department dv" +
                      "  ON rc.DepartmentId = dv.Id" +
                      "    WHERE" +
                      "        LEN(rc.BirthDate) > 0" +
                      "        AND rc.WorkStatusId = (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Đang làm việc%')" +
                      "        AND rc.DepartmentId IN ({0})".FormatWith(departmentIds);
            if(fromDate != null)
                sql += " AND MONTH(rc.BirthDate) >= '{0}' AND DAY(rc.BirthDate) >= '{1}' ".FormatWith(fromDate.Value.Month, fromDate.Value.Day);
            if(toDate != null)
                sql += " AND MONTH(rc.BirthDate) <= '{0}' AND DAY(rc.BirthDate) <= '{1}' ".FormatWith(toDate.Value.Month, toDate.Value.Day);
            sql += " ORDER BY rc.BirthDate ASC ";

            sql += " SELECT * FROM #tmp ";
            if(start != null & limit != null)
            {
                sql += " WHERE RN > {0} ".FormatWith(start) +
                       " AND RN <= {0} ".FormatWith(start + limit);
            }           
            return sql;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="day"></param>
        /// <param name="dsDv"></param>
        /// <returns></returns>
        public static string GetStore_DanhSachNhanVienSapHetHopDong(int? start, int? limit, int day, string dsDv)
        {
            var sql = " SELECT * " +
                      " FROM(" +
                      "   SELECT  hs.EmployeeCode," +
                      " hs.FullName," +
                      " CASE WHEN(hs.Sex = 1) THEN 'Nam' ELSE N'Nữ' END AS SexName," +
                      " hs.[Address]," +
                      " hs.BirthDate," +
                      " hs.ParticipationDate," +
                      " dv.Name AS DepartmentName," +
                      " lhd.Name AS ContractTypeName," +
                      " hshd.ContractDate," +
                      " hshd.ContractEndDate," +
                      " dc.Name AS PositionName," +
                      " dt.Name AS EducationName," +
                      " ROW_NUMBER() OVER(ORDER BY hshd.ContractDate) RN" +
                      "   FROM hr_Contract hshd " +
                      "       LEFT JOIN hr_Record hs" +
                      " ON hshd.RecordId = hs.Id" +
                      "       LEFT JOIN cat_ContractType lhd" +
                      " ON  lhd.Id = hshd.ContractTypeId" +
                      "       LEFT JOIN cat_Department dv" +
                      " ON  dv.Id = hs.DepartmentId" +
                      "       LEFT JOIN cat_Position dc" +
                      " ON dc.Id = hs.PositionId" +
                      "       LEFT JOIN cat_Education dt" +
                      " ON dt.Id = hs.EducationId" +
                      "   WHERE (DATEDIFF(DD, hshd.ContractEndDate,GETDATE()) > 0" +
                      " AND DATEDIFF(DD,hshd.ContractEndDate,GETDATE()) <= {0})".FormatWith(day) +
                      " and DATEDIFF(MONTH, hshd.ContractEndDate, GETDATE()) <= 3" +
                      " AND hs.WorkStatusId = (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Đang làm việc%')" +
                      " AND ISNULL(ContractEndDate, '0001-01-01') <> '0001-01-01'" +
                      " AND hs.DepartmentId IN ({0})".FormatWith(dsDv) +
                      ") a";
            if(start != null & limit != null)
            {
                sql += " WHERE  RN BETWEEN {0}".FormatWith(start) +
                      " AND {0}".FormatWith(start + limit);
            }
            return sql;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        public static string GetStore_RetirementOfEmployee(int? start, int? limit, string department)
        {
            // TODO : move to constant
            const string businessType = "NghiHuu";
            const string workingStatus = "%Đang làm việc%";
            const string totalDayOfYear = "365";
            const string maleRetirement = "60";
            const string femaleRetirement = "55";
            var sql = "SELECT * FROM (" +
                "SELECT *, ROW_NUMBER() OVER(ORDER BY FullName, BirthDate) RN" +
                      " FROM(" +
                      " SELECT rc.EmployeeCode, rc.FullName, rc.Sex, CASE WHEN(Sex = 1) THEN N'Nam' ELSE N'Nữ' END AS SexName," +
                      "  CASE WHEN BirthDate IS NOT NULL THEN BirthDate ELSE NULL END AS BirthDate," +
                      "  CASE WHEN BirthDate IS NOT NULL THEN(YEAR(GETDATE()) - YEAR(rc.BirthDate)) ELSE NULL END AS Age," +
                      " rc.RecruimentDate, rc.FunctionaryDate," +
                      " CASE WHEN(DATEDIFF(DAY, rc.FunctionaryDate, getDate()) / " + totalDayOfYear + ") = 0 THEN '' ELSE" +
                      " CAST(DATEDIFF(DAY, rc.FunctionaryDate, getDate()) / " + totalDayOfYear + " AS NVARCHAR(10)) + N' Năm ' END +" +
                      "  CASE WHEN((DATEDIFF(DAY, rc.FunctionaryDate, getDate()) % " + totalDayOfYear + ") / 30) = 0 THEN '' ELSE" +
                      " CAST((DATEDIFF(DAY, rc.FunctionaryDate, getDate()) % " + totalDayOfYear + ") / 30 AS NVARCHAR(10)) + N' Tháng ' END +" +
                      " CASE WHEN((DATEDIFF(DAY, rc.FunctionaryDate, getDate()) % " + totalDayOfYear + ") % 30) = 0 THEN '' ELSE" +
                      " CAST((DATEDIFF(DAY, rc.FunctionaryDate, getDate()) % " + totalDayOfYear + ") % 30 AS NVARCHAR(10)) + N' Ngày' END AS Seniority," +
                      " dp.Name AS DepartmentName, (SELECT TOP 1 [Name] FROM cat_Department WHERE ParentId = 0) AS ParentDepartmentName" +
                      " FROM hr_Record rc LEFT JOIN cat_Department dp ON rc.DepartmentId = dp.Id" +
                      " LEFT JOIN hr_BusinessHistory bh ON bh.RecordId = rc.Id " +
                      " WHERE rc.DepartmentId IN({0})".FormatWith(department) +
                      " AND rc.WorkStatusId = (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'" + workingStatus + "')" +
                      " AND (bh.BusinessType = N'{0}' OR bh.BusinessType IS NULL)".FormatWith(businessType) +
                      " ) #tmpA " +
                      " WHERE(#tmpA.Age > " + maleRetirement + " AND #tmpA.Sex = 1) OR (#tmpA.Age > " + femaleRetirement + " AND #tmpA.Sex = 0)) #tmp";
            if(start != null & limit != null)
            {
                sql += " WHERE #tmp.RN > {0} ".FormatWith(start) +
                       " AND #tmp.RN <= {0}".FormatWith(start + limit);
            }
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetStore_GetCategoryByKeyword(string keyWord, string tableName)
        {
            var sql = "SELECT * FROM (" +
                      "SELECT Id, Name, " +
                      "ROW_NUMBER() OVER(ORDER BY Name ASC) SK " +
                      "FROM {0} ".FormatWith(tableName);
            if(!string.IsNullOrEmpty(keyWord))
            {
                sql += "WHERE(LEN({0}) = 0 ".FormatWith(keyWord) +
                         "OR Name LIKE N'%{0}%' ".FormatWith(keyWord) + "OR {0} = '*') ".FormatWith(keyWord);
            }
            sql += ")#tmp ";

            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetStore_GetCountCategoryByKeyword(string keyWord, string tableName)
        {
            var sql = "SELECT COUNT(*) FROM (" +
                      "SELECT Id, Name " +
                      "FROM {0} )#tmp".FormatWith(tableName);
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetStore_DeleteAttachFile(string tableName, int id)
        {
            var sql = "UPDATE {0} ".FormatWith(tableName) +
                         "SET AttachFileName = N'' " +
                         "WHERE Id = {0}".FormatWith(id);
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetStore_DeleteFileScan(string tableName, int id)
        {
            var sql = "UPDATE {0} ".FormatWith(tableName) +
                         "SET FileScan = N'' " +
                         "WHERE Id = {0}".FormatWith(id);
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="searchKey"></param>
        /// <param name="statusParam"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static string GetStore_GetRecords(string departments, string searchKey, int statusParam, int start,
            int limit)
        {
            var sql = "SELECT * FROM " +
                      " (SELECT rc.Id," +
                      " rc.EmployeeCode," +
                      " rc.FullName," +
                      " rc.BirthDate, " +
                      " CASE WHEN(rc.Sex = 1) THEN 'Nam' ELSE N'Nữ' END AS SexName," +
                      " rc.[Address]," +
                      " rc.CellPhoneNumber," +
                      " ctt.Name AS ContractTypeName," +
                      " rc.DepartmentId," +
                      " rc.IDNumber," +
                      " rc.PersonalEmail," +
                      " rc.RecruimentDate," +
                      " rc.ParticipationDate," +
                      " dp.Name AS DepartmentName," +
                      " ed.Name AS EducationName," +
                      " ws.Name AS WorkStatusName," +
                      " ROW_NUMBER() OVER(ORDER BY rc.DepartmentId ASC) SK" +
                      " FROM hr_Record rc" +
                      " LEFT JOIN cat_Department dp" +
                      " ON rc.DepartmentId = dp.Id" +
                      " LEFT JOIN cat_Education ed" +
                      " ON rc.EducationId = ed.Id" +
                      " LEFT JOIN cat_WorkStatus ws" +
                      " ON rc.WorkStatusId = ws.Id" +
                      " LEFT JOIN(" +
                      "    SELECT TOP 1 ContractTypeId, RecordId" +
                      "    FROM hr_Contract" +
                      "    ORDER BY EffectiveDate DESC) #tmpA" +
                      " ON rc.Id = #tmpA.RecordId" +
                      "     LEFT JOIN cat_ContractType ctt" +
                      "    ON #tmpA.ContractTypeId = ctt.Id" +
                      " WHERE (rc.FullName like N'%{0}%'".FormatWith(searchKey) +
                      " OR rc.EmployeeCode like N'%{0}%'".FormatWith(searchKey) +
                      " OR rc.CellPhoneNumber like N'%{0}%'".FormatWith(searchKey) +
                      " OR rc.PersonalEmail like N'%{0}%'".FormatWith(searchKey) +
                      " OR rc.IDNumber like N'%{0}%')".FormatWith(searchKey) +
                      " AND rc.DepartmentId IN({0})".FormatWith(departments);
            if(statusParam == 0)
            {
                sql += " AND rc.WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')) #tmp";
            }
            else
            {
                sql += " AND rc.WorkStatusId = {0}) #tmp".FormatWith(statusParam);
            }

            sql += " WHERE #tmp.SK > {0}".FormatWith(start) +
                   "    AND #tmp.SK <= {0}".FormatWith(start + limit) +
                   " ORDER BY #tmp.EmployeeCode";
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="searchKey"></param>
        /// <param name="statusParam"></param>
        /// <returns></returns>
        public static string GetStore_CountGetRecords(string departments, string searchKey, int statusParam)
        {
            var sql = "SELECT COUNT(*) FROM hr_Record rc" +
                      " WHERE (rc.FullName like N'%{0}%'".FormatWith(searchKey) +
                      " OR rc.EmployeeCode like N'%{0}%'".FormatWith(searchKey) +
                      " OR rc.CellPhoneNumber like N'%{0}%'".FormatWith(searchKey) +
                      " OR rc.PersonalEmail like N'%{0}%'".FormatWith(searchKey) +
                      " OR rc.IDNumber like N'%{0}%')".FormatWith(searchKey) +
                      " AND rc.DepartmentId IN({0})".FormatWith(departments);
            if(statusParam == 0)
            {
                sql += " AND rc.WorkStatusId <> (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Từ trần%')";
            }
            else
            {
                sql += " AND rc.WorkStatusId = {0}".FormatWith(statusParam);
            }
            return sql;
        }

        /// <summary>
        /// Danh sách nâng lương, vượt khung
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="salaryType"></param>
        /// <param name="condition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public static string GetStore_ListSalaryRaise(string department, string fromDate,
            string toDate, int? start, int? limit, int? salaryType, string condition, string searchKey
            )
        {
            var sql = "	SELECT * FROM (SELECT 	 " +
                      "	rc.DepartmentId, " +
                      "	rc.EmployeeCode, " +
                      "	rc.FullName, " +
                      "	dp.Name AS DepartmentName, " +
                      " (SELECT TOP 1 Name FROM cat_Department WHERE ParentId = 0) AS ParentDepartmentName," +
                      "	cp.Name AS PositionName, " +
                      "	cq.Code AS QuantumCode, " +
                      "	cq.Name AS QuantumName, " +
                      "	gq.MonthStep, " +
                      "	hs.Factor, " +
                      "	hs.Grade, " +
                      //"	hs.PositionAllowance, " +
                      //"	hs.OtherAllowance, " +
                      "	hs.DecisionDate, " +
                      "	hs.EffectiveDate, " +
                      "	hs.DecisionNumber, " +
                      "	hs.Note, " +
                      "	hs.Id, " +
                      "	gq.GradeMax AS QuantumGrade, ";
                    //Vượt khung
                    if (salaryType == (int) SalaryDecisionType.OverGrade)
                    {
                        sql +=
                            "	CASE WHEN hs.Grade IS NOT NULL AND hs.Grade > 0 THEN DATEADD(MONTH, {0}, hs.EffectiveDate)	".FormatWith(Constant.SalaryMonthRaiseOutFrame) +
                            "			  ELSE DATEADD(MONTH, gq.MonthStep, hs.EffectiveDate)	 " +
                            "			   END AS RaisingSalaryDate,  ";
                    }
                     else
                    {
                        sql += "	DATEADD(MONTH, gq.MonthStep, hs.EffectiveDate) AS RaisingSalaryDate, ";
                    }
                      sql += " ROW_NUMBER() OVER(ORDER BY hs.DecisionDate) AS RN " +
                      "	FROM (SELECT DISTINCT slr.RecordId, " +
                      "					slr.EffectiveDate,	 " +
                      "					slr.Grade,	 " +
                      "					slr.Factor,	 " +
                      "					slr.QuantumId,	 " +
                      "					slr.GroupQuantumId,	 " +
                      //"					slr.PositionAllowance,	 " +
                      //"					slr.OtherAllowance,	 " +
                      "					slr.DecisionDate,	 " +
                      "					slr.DecisionNumber,	 " +
                      "					slr.Id,	 " +
                      "					slr.Note " +
                      "		FROM (SELECT MAX(DecisionDate) AS MaxDecisionDate, " +
                      "	            RecordId " +
                      "			FROM sal_SalaryDecision	  " +
                      "			GROUP BY RecordId) #tmpA " +
                      "		LEFT JOIN sal_SalaryDecision slr ON #tmpA.MaxDecisionDate = slr.DecisionDate " +
                      "		AND slr.RecordId = #tmpA.RecordId " +
                      "		WHERE slr.Factor = (SELECT TOP 1 Factor FROM " +
                      "									(SELECT MAX(Factor) AS Factor, RecordId	 " +
                      "									FROM sal_SalaryDecision						" +
                      "									GROUP BY RecordId)#tmp " +
                      "									WHERE #tmp.RecordId = slr.RecordId) " +
                      "		) hs " +
                      "	LEFT JOIN hr_Record rc ON hs.RecordId = rc.Id " +
                      "	LEFT JOIN cat_Department dp ON rc.DepartmentId = dp.Id " +
                      "	LEFT JOIN cat_Position cp on rc.PositionId = cp.Id " +
                      "	LEFT JOIN cat_Quantum cq ON hs.QuantumId = cq.Id " +
                      " LEFT JOIN cat_GroupQuantum gq on gq.Id = cq.GroupQuantumId " +
                      "	WHERE rc.Id IS NOT NULL ";
            switch (salaryType)
            {
                //Nâng lương
                case (int)SalaryDecisionType.Regular:
                    sql += "	AND hs.Grade < gq.GradeMax ";
                    break;
                //Vượt khung
                case (int)SalaryDecisionType.OverGrade:
                    sql += "	AND hs.Grade = gq.GradeMax ";
                    break;
            }

            if(!string.IsNullOrEmpty(department))
            {
                sql += " AND rc.DepartmentId IN({0}) ".FormatWith(department);
            }

            if(!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND hs.DecisionDate >= '{0}'".FormatWith(fromDate);
            }
            if(!string.IsNullOrEmpty(toDate))
            {
                sql += " AND hs.DecisionDate <= '{0}'".FormatWith(toDate);
            }
            //filter
            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            //searchkey
            if(!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND (rc.FullName like N'%{0}%'".FormatWith(searchKey) +
                       " OR rc.EmployeeCode like N'%{0}%'".FormatWith(searchKey) +
                       " OR cq.Name like N'%{0}%'".FormatWith(searchKey) +
                       " OR cq.Code like N'%{0}%'".FormatWith(searchKey) +
                       " OR hs.Grade like N'%{0}%'".FormatWith(searchKey) +
                       " OR dp.Name like N'%{0}%')".FormatWith(searchKey);
            }

            sql += " AND rc.WorkStatusId = (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%{0}%')".FormatWith(RecordStatus.Working.Description());
            sql += " ) #tmp";

            if(start != null & limit != null)
            {
                sql += " WHERE (#tmp.RN > {0}".FormatWith(start) +
                       " AND #tmp.RN <= {0})".FormatWith(start + limit);
            }
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetStore_CurriculumVitaeDetail(int id)
        {
            var sql = " SELECT " +
                         " rc.FullName, " +
                         " ps.Name AS PositionName," +
                         " rc.EmployeeCode," +
                         " DP.Name AS DepartmentName," +
                         " rc.Alias," +
                         " (SELECT TOP 1 [Name] FROM cat_Department WHERE ParentId = 0) AS ParentDepartmentName," +
                         " rc.[Address]," +
                         " rc.CellPhoneNumber," +
                         " rc.ResidentPlace," +
                         " rc.RecruimentDate," +
                         // " #tmp.QuantumId," +
                         " rc.CPVJoinedPlace," +
                         " rc.VYUJoinedPlace," +
                         " rc.PreviousJob," +
                         " rc.AssignedWork," +
                         " rc.RecruimentDepartment," +
                         " cpv.Name AS CPVPositionName," +
                         " rc.ArmyJoinedDate," +
                         " rc.ArmyLeftDate," +
                         " rc.TitleAwarded," +
                         " rc.Skills," +
                         " rc.Height," +
                         " rc.[Weight]," +
                         " rc.BloodGroup," +
                         " rc.RankWounded," +
                         " rc.IDNumber," +
                         " rc.IDIssueDate," +
                         " rc.InsuranceNumber," +
                         " rc.InsuranceIssueDate," +
                         " rl.Name AS ReligionName," +
                         " pc.Name AS PersonalClassName," +
                         " fc.Name AS FamilyClassName," +
                         " qt.Name AS QuantumName," +
                         " ed.Name AS EducationName," +
                         " pl.Name AS PoliticLevelName," +
                         " ml.Name AS ManagementLevelName," +
                         " ll.Name AS LanguageLevelName," +
                         " itl.Name AS ITLevelName," +
                         " be.Name AS BasicEducationName," +
                         " cpv.Name AS CPVPositionName," +
                         " vyu.Name AS VYUPositionName," +
                         " al.Name AS ArmyLevelName," +
                         " rw2.Name AS RewardName," +
                         " dcl2.Name AS DisciplineName," +
                         " YEAR(rw.DecisionDate) AS RewardDecisionDate," +
                         " YEAR(dcl.DecisionDate) AS DisciplineDecisionDate," +
                         " hs.Name AS HealthStatusName," +
                         " fp.Name AS FamilyPolicyName," +
                         " f.Name AS FolkName," +
                         " ms.Name AS MaritalStatusName," +
                         " jt.Name AS JobTitleName," +
                         " ip.Name AS IDIssuePlaceName," +
                         " ws.Name AS WorkStatusName," +
                         " ISNULL(lc.Name, '.......') AS BirthPlaceWardName," +
                         " ISNULL(lc2.Name, '.......') AS BirthPlaceDistrictName," +
                         " ISNULL(lc3.Name, '.......') AS BirthPlaceProvinceName," +
                         " ISNULL(lc4.Name, '.......') AS HometownWardName," +
                         " ISNULL(lc5.Name, '.......') AS HometownDistrictName," +
                         " ISNULL(lc6.Name, '.......') AS HometownProvinceName," +
                         " lc.[Group]" +
                         " AS BirthPlaceWardGroup," +
                         " lc2.[Group]" +
                         " AS BirthPlaceDistrictGroup," +
                         " lc3.[Group]" +
                         " AS BirthPlaceProvinceGroup," +
                         " lc4.[Group]" +
                         " AS HometownWardGroup," +
                         " lc5.[Group]" +
                         " AS HometownDistrictGroup," +
                         " lc6.[Group]" +
                         " AS HometownProvinceGroup," +
                         " #tmp.DecisionDate," +
                         " qt.Code AS QuantumId, " +
                         " #tmp.Grade," +
                         " #tmp.Factor, " +
                         //" #tmp.FactorAllowance," +
                         //" #tmp.SalaryPayDate," +
                         //" #tmp.PositionAllowance," +
                         //" #tmp.OtherAllowance," +
                         " #tmp.EffectiveDate" +

                         " FROM hr_Record rc" +
                         " LEFT JOIN cat_Department dp" +
                         " ON rc.DepartmentId = dp.Id" +

                         " LEFT JOIN cat_CPVPosition cpv" +
                         " ON cpv.Id = rc.CPVPositionId" +
                         " LEFT JOIN cat_Religion rl" +
                         " ON rl.Id = rc.ReligionId" +
                         " LEFT JOIN cat_PersonalClass pc" +
                         " ON pc.Id = rc.PersonalClassId" +
                         " LEFT JOIN cat_FamilyClass fc" +
                         " ON fc.Id = rc.FamilyClassId" +
                         " LEFT JOIN cat_Position ps" +
                         " ON ps.Id = rc.PositionId" +
                         " LEFT JOIN cat_Education ed" +
                         " ON ed.Id = rc.EducationId" +
                         " LEFT JOIN cat_PoliticLevel pl" +
                         " ON pl.Id = rc.PoliticLevelId" +
                         " LEFT JOIN cat_ManagementLevel ml" +
                         " ON ml.Id = rc.ManagementLevelId" +
                         " LEFT JOIN cat_LanguageLevel ll" +
                         " ON ll.Id = rc.LanguageLevelId" +
                         " LEFT JOIN cat_ITLevel itl" +
                         " ON itl.Id = rc.ITLevelId" +
                         " LEFT JOIN cat_BasicEducation be" +
                         " ON be.Id = rc.BasicEducationId" +
                         " LEFT JOIN cat_VYUPosition vyu" +
                         " ON vyu.Id = rc.VYUPositionId" +
                         " LEFT JOIN cat_ArmyLevel al" +
                         " ON al.Id = rc.ArmyLevelId" +
                         " LEFT JOIN (" +
                         " SELECT rw.*" +
                         " FROM hr_Record rc1" +
                         " LEFT JOIN hr_Reward rw ON rc1.Id = rw.RecordId" +
                         " WHERE rw.DecisionDate = (SELECT MAX(rw1.DecisionDate) FROM hr_Reward rw1 WHERE rw1.RecordId = rc1.Id)" +
                         " ) rw" +
                         " ON rw.RecordId = rc.Id" +
                         " LEFT JOIN cat_Reward rw2" +
                         " ON rw.FormRewardId = rw2.Id" +
                         " LEFT JOIN(" +
                         " SELECT dcl.* " +
                         " FROM hr_Record rc1" +
                         " LEFT JOIN hr_Discipline dcl ON rc1.Id = dcl.RecordId" +
                         " WHERE dcl.DecisionDate = (SELECT MAX(dcl1.DecisionDate) FROM hr_Discipline dcl1 WHERE dcl1.RecordId = rc1.Id)" +
                         ") dcl" +
                         " ON dcl.RecordId = rc.Id" +
                         " LEFT JOIN cat_Discipline dcl2" +
                         " ON dcl.FormDisciplineId = dcl2.Id" +
                         " LEFT JOIN cat_HealthStatus hs" +
                         " ON hs.Id = rc.HealthStatusId" +
                         " LEFT JOIN cat_FamilyPolicy fp" +
                         " ON fp.Id = rc.FamilyPolicyId" +
                         " LEFT JOIN cat_Folk f" +
                         " ON f.Id = rc.FolkId" +
                         " LEFT JOIN cat_MaritalStatus ms" +
                         " ON ms.Id = rc.MaritalStatusId" +
                         " LEFT JOIN cat_JobTitle jt" +
                         " ON jt.Id = rc.JobTitleId" +
                         " LEFT JOIN cat_IDIssuePlace ip" +
                         " ON ip.Id = rc.IDIssuePlaceId" +
                         " LEFT JOIN cat_WorkStatus ws" +
                         " ON ws.Id = rc.WorkStatusId" +
                         " LEFT JOIN cat_Location lc" +
                         " ON lc.Id = rc.BirthPlaceWardId" +
                         " LEFT JOIN cat_Location lc2" +
                         " ON lc2.Id = rc.BirthPlaceDistrictId" +
                         " LEFT JOIN cat_Location lc3" +
                         " ON lc3.Id = rc.BirthPlaceProvinceId" +
                         " LEFT JOIN cat_Location lc4" +
                         " ON lc4.Id = rc.HometownWardId" +
                         " LEFT JOIN cat_Location lc5" +
                         " ON lc5.Id = rc.HometownDistrictId" +
                         " LEFT JOIN cat_Location lc6" +
                         " ON lc6.Id = rc.HometownProvinceId" +
                         " LEFT JOIN(" +
                         " SELECT hs.*" +
                         " FROM hr_Record rc" +
                         " LEFT JOIN sal_SalaryDecision hs ON hs.RecordId = rc.Id" +
                         " WHERE hs.DecisionDate = (SELECT MAX(hs1.DecisionDate) FROM sal_SalaryDecision hs1 WHERE hs1.RecordId = rc.Id)" +
                         " and hs.EffectiveDate = (select MAX(hs1.EffectiveDate) from sal_SalaryDecision hs1 where hs1.RecordId = rc.Id)" +
                         " ) #tmp ON #tmp.RecordId = rc.Id" +
                         " LEFT JOIN cat_Quantum qt" +
                         " ON qt.Id = #tmp.QuantumId" +
                         " WHERE rc.Id = {0}".FormatWith(id);
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="filterDepartment"></param>
        /// <param name="selectedDepartment"></param>
        /// <param name="searchKey"></param>
        /// <param name="birthDate"></param>
        /// <param name="sex"></param>
        /// <param name="folk"></param>
        /// <param name="religion"></param>
        /// <param name="familyClass"></param>
        /// <param name="personalClass"></param>
        /// <param name="recruimentDate"></param>
        /// <param name="position"></param>
        /// <param name="jobTitle"></param>
        /// <param name="basicEducation"></param>
        /// <param name="education"></param>
        /// <param name="politicLevel"></param>
        /// <param name="manegementLevel"></param>
        /// <param name="languageLevel"></param>
        /// <param name="itLevel"></param>
        /// <param name="cpvPosition"></param>
        /// <param name="vyuPosition"></param>
        /// <param name="armyLevel"></param>
        /// <param name="maritalStatus"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static string GetStore_ExcelLyLichTrichNgang(string departments, string filterDepartment, string selectedDepartment, string searchKey,
            string birthDate, string sex, string folk, string religion, string familyClass, string personalClass,
            string recruimentDate, string position, string jobTitle, string basicEducation, string education,
            string politicLevel, string manegementLevel, string languageLevel, string itLevel, string cpvPosition,
            string vyuPosition, string armyLevel, string maritalStatus, string phone, string email)
        {
            var sql = "SELECT " +
                      " '' AS 'STT'," +
                      " h.EmployeeCode AS N'Số hiệu CBCC'," +
                      " h.FullName AS N'Họ và tên khai sinh'," +
                      " h.Alias AS N'Tên gọi khác'," +
                      " CONVERT(varchar, h.BirthDate, 103) AS N'Ngày sinh'," +
                      " DATEDIFF(YEAR, h.BirthDate, GETDATE()) AS N'Tuổi'," +
                      " CASE WHEN h.Sex = 1 THEN N'Nam' ELSE N'Nữ' END AS N'Giới tính'," +
                      " dp2.Name AS N'Đơn vị công tác'," +
                      " ISNULL(nsx.Name, '') + ' - ' + ISNULL(nsh.Name, '') + ' - ' + ISNULL(nst.Name, '') AS N'Nơi sinh'," +
                      " ISNULL(qqx.Name, '') + ' - ' + ISNULL(qqh.Name, '') + ' - ' + ISNULL(qqt.Name, '') AS N'Quê quán'," +
                      " f.Name AS N'Dân tộc'," +
                      " rl.Name AS N'Tôn giáo'," +
                      " pc.Name AS N'Thành phần bản thân'," +
                      " fc.Name AS N'Thành phần gia đình'," +
                      " h.ResidentPlace AS N'Nơi đăng ký Hộ khẩu thường trú'," +
                      " h.[Address] AS N'Nơi ở hiện nay'," +
                      " h.PreviousJob AS N'Nghề nghiệp khi được tuyển dụng'," +
                      " CONVERT(varchar, h.RecruimentDate, 103) AS N'Ngày tuyển dụng'," +
                      " h.RecruimentDepartment AS N'Cơ quan tuyển dụng'," +
                      " po.Name AS N'Chức vụ hiện tại'," +
                      " jt.Name AS N'Chức danh hiện tại'," +
                      " h.AssignedWork AS N'Công việc chính được giao'," +
                      " qt.Name AS N'Tên ngạch hiện tại'," +
                      " qt.Code AS N'Mã ngạch hiện tại', " +
                      " #A.Grade AS N'Bậc lương hiện tại'," +
                      " #A.Factor AS N'Hệ số hiện tại'," +
                      " CONVERT(varchar, #A.EffectiveDate, 103) AS N'Ngày hưởng'," +
                      //" #A.PositionAllowance AS N'Phụ cấp chức vụ'," +
                      //" #A.OtherAllowance AS N'Phụ cấp khác'," +
                      " be.Name AS N'Trình độ giáo dục phổ thông'," +
                      " ed.Name AS N'Trình độ chuyên môn cao nhất'," +
                      " pl.Name AS N'Lý luận chính trị'," +
                      " ml.name AS N'Quản lý Nhà nước'," +
                      " ll.Name AS N'Ngoại ngữ'," +
                      " it.Name AS N'Tin học'," +
                      " CONVERT(varchar, h.CPVJoinedDate, 103) AS N'Ngày vào Đảng'," +
                      " CONVERT(varchar, h.CPVOfficialJoinedDate, 103) AS N'Ngày chính thức'," +
                      " CPVJoinedPlace AS N'Nơi kết nạp'," +
                      " CPVCardNumber AS N'Số thẻ Đảng'," +
                      " cpv.Name AS N'Chức vụ Đảng'," +
                      " vyu.Name AS N'Chức vụ Đoàn'," +
                      " CONVERT(varchar, h.ArmyJoinedDate, 103) AS N'Ngày nhập ngũ'," +
                      " CONVERT(varchar, h.ArmyLeftDate, 103) AS N'Ngày xuất ngũ'," +
                      " ap.Name AS N'Quân hàm cao nhất'," +
                      " al.Name AS N'Chế độ quân nhân'," +
                      " h.TitleAwarded AS N'Danh hiệu được phong tặng cao nhất'," +
                      " h.Skills AS N'Sở trường công tác'," +
                      " hs.Name AS N'Tình trạng sức khoẻ'," +
                      " h.Height AS N'Chiều cao'," +
                      " h.[Weight] AS N'Cân nặng'," +
                      " h.BloodGroup AS N'Nhóm máu'," +
                      " h.RankWounded AS N'Là thương binh hạng'," +
                      " '' AS N'Chế độ thương binh'," +
                      " CASE WHEN h.FamilyPolicyId != 0 THEN N'x' ELSE N'' END AS N'Là con gia đình chính sách'," +
                      " fp.Name AS N'Chế độ chính sách'," +
                      " h.IDNumber AS N'Số CMND'," +
                      " CONVERT(varchar, h.IDIssueDate, 103) AS N'Ngày cấp'," +
                      " ip.Name AS N'Nơi cấp'," +
                      " h.InsuranceNumber AS N'Số sổ BHXH'," +
                      " CONVERT(varchar, h.InsuranceIssueDate, 103) AS N'Ngày cấp sổ'," +
                      " ms.Name AS N'Tình trạng hôn nhân'," +
                      " h.PersonalTaxCode AS N'Mã số thuế cá nhân'," +
                      " h.CellPhoneNumber AS N'Di động'," +
                      " h.HomePhoneNumber AS N'Điện thoại nhà'," +
                      " h.WorkPhoneNumber AS N'Điện thoại cơ quan'," +
                      " h.WorkEmail AS N'Email nội bộ'," +
                      " h.PersonalEmail AS N'Email riêng'," +
                      " h.ContactPersonName AS N'Người liên hệ (khi cần)'," +
                      " h.ContactRelation AS N'Mối quan hệ'," +
                      " h.ContactPhoneNumber AS N'Số điện thoại'," +
                      " h.ContactAddress AS N'Địa chỉ người liên hệ'" +

                      " FROM hr_Record h" +
                      " LEFT JOIN cat_MaritalStatus ms ON ms.Id = h.MaritalStatusId" +
                      " LEFT JOIN cat_Folk f ON f.Id = h.FolkId" +
                      " LEFT JOIN cat_Religion rl ON rl.Id = h.ReligionId" +
                      " LEFT JOIN cat_BasicEducation be ON be.Id = h.BasicEducationId" +
                      " LEFT JOIN cat_HealthStatus hs ON hs.Id = h.HealthStatusId" +
                      " LEFT JOIN cat_LanguageLevel ll ON ll.Id = h.LanguageLevelId" +
                      " LEFT JOIN cat_ITLevel it ON it.Id = h.ITLevelId" +
                      " LEFT JOIN cat_Education ed ON ed.Id = h.EducationId" +
                      " LEFT JOIN cat_JobTitle jt ON jt.Id = h.JobTitleId" +
                      " LEFT JOIN cat_Position po ON po.Id = h.PositionId" +
                      " LEFT JOIN cat_FamilyClass fc ON fc.Id = h.FamilyClassId" +
                      " LEFT JOIN cat_PersonalClass pc ON pc.Id = h.PersonalClassId" +
                      " LEFT JOIN cat_PoliticLevel pl ON pl.Id = h.PoliticLevelId" +
                      " LEFT JOIN cat_ManagementLevel ml ON ml.Id = h.ManagementLevelId" +
                      " LEFT JOIN cat_IDIssuePlace ip ON ip.Id = h.IDIssuePlaceId" +
                      " LEFT JOIN cat_FamilyPolicy fp ON fp.Id = h.FamilyPolicyId" +
                      " LEFT JOIN cat_ArmyPosition ap ON ap.Id = h.ArmyLevelId" +
                      " LEFT JOIN cat_ArmyLevel al ON al.Id = h.ArmyLevelId" +
                      " LEFT JOIN cat_VYUPosition vyu ON vyu.Id = h.VYUPositionId" +
                      " LEFT JOIN cat_CPVPosition cpv ON cpv.Id = h.CPVPositionId" +
                      " LEFT JOIN cat_Department dp2 ON dp2.Id = h.DepartmentId" +
                      " LEFT JOIN cat_Department dp3 ON dp3.Id = dp2.ParentId" +
                      " LEFT JOIN cat_Location nsx ON nsx.Id = h.BirthPlaceWardId" +
                      " LEFT JOIN cat_Location nsh ON nsh.Id = h.BirthPlaceDistrictId" +
                      " LEFT JOIN cat_Location nst ON nst.Id = h.BirthPlaceProvinceId" +
                      " LEFT JOIN cat_Location qqx ON qqx.Id = h.HometownWardId" +
                      " LEFT JOIN cat_Location qqh ON qqh.Id = h.HometownDistrictId" +
                      " LEFT JOIN cat_Location qqt ON qqt.Id = h.HometownProvinceId" +
                      " LEFT JOIN(SELECT DISTINCT * FROM " +
                      " (SELECT MAX(DecisionDate) AS MaxDecisionDate," +
                      " RecordId AS IdRecord" +
                      " FROM sal_SalaryDecision" +
                      " GROUP BY RecordId) #tmpA " +
                      " LEFT JOIN sal_SalaryDecision slr" +
                      " ON #tmpA.MaxDecisionDate = slr.DecisionDate" +
                      " AND slr.RecordId = #tmpA.IdRecord" +
                      " WHERE slr.Factor = (SELECT TOP 1 Factor" +
                      " FROM(" +
                      " SELECT MAX(Factor) AS Factor," +
                      " RecordId FROM sal_SalaryDecision" +
                      " GROUP BY RecordId)#tmp" +
                      " WHERE #tmp.RecordId = slr.RecordId)" +
                      " ) #A" +
                      " ON #A.RecordId = h.Id" +
                      " LEFT JOIN cat_Quantum qt" +
                      " ON #A.QuantumId = qt.Id" +
                      " WHERE 1=1 ";
            if(!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND (h.FullName LIKE N'%{0}%' OR h.EmployeeCode LIKE N'%{0}%' OR h.IDNumber LIKE N'%{0}%')".FormatWith(searchKey);
            }
            if(!string.IsNullOrEmpty(filterDepartment))
            {
                sql += " AND h.DepartmentId IN ({0})".FormatWith(filterDepartment);
            }
            if(!string.IsNullOrEmpty(selectedDepartment))
            {
                sql += " AND h.DepartmentId = {0}".FormatWith(selectedDepartment);
            }
            if(!string.IsNullOrEmpty(sex))
            {
                sql += " AND(h.Sex = '{0}')".FormatWith(sex);
            }
            if(!string.IsNullOrEmpty(birthDate))
            {
                sql += " AND(YEAR(h.BirthDate) = {0})".FormatWith(birthDate);
            }
            if(!string.IsNullOrEmpty(folk))
            {
                sql += " AND(h.FolkId = {0})".FormatWith(folk);
            }
            if(!string.IsNullOrEmpty(religion))
            {
                sql += " AND(h.ReligionId = {0})".FormatWith(religion);
            }
            if(!string.IsNullOrEmpty(familyClass))
            {
                sql += " AND(h.FamilyClassId = {0})".FormatWith(familyClass);
            }
            if(!string.IsNullOrEmpty(personalClass))
            {
                sql += " AND(h.PersonalClassId = {0})".FormatWith(personalClass);
            }
            if(!string.IsNullOrEmpty(recruimentDate))
            {
                sql += " AND(YEAR(h.RecruimentDate) = CAST({0} AS NVARCHAR(5)))"
                    .FormatWith(recruimentDate);
            }
            if(!string.IsNullOrEmpty(position))
            {
                sql += " AND(h.PositionId = {0})".FormatWith(position);
            }
            if(!string.IsNullOrEmpty(jobTitle))
            {
                sql += " AND(h.JobTitleId = {0})".FormatWith(jobTitle);
            }
            //if (!string.IsNullOrEmpty(effectiveDate))
            //{
            //    sql += " AND(YEAR(#A.EffectiveDate) = CAST({0} AS NVARCHAR(5)))".FormatWith(effectiveDate);
            //}
            if(!string.IsNullOrEmpty(basicEducation))
            {
                sql += " AND(h.BasicEducationId = {0})".FormatWith(basicEducation);
            }
            if(!string.IsNullOrEmpty(education))
            {
                sql += " AND(h.EducationId = {0})".FormatWith(education);
            }
            if(!string.IsNullOrEmpty(politicLevel))
            {
                sql += " AND h.PoliticLevelId = {0}".FormatWith(politicLevel);
            }
            if(!string.IsNullOrEmpty(manegementLevel))
            {
                sql += " AND(h.ManagementLevelId = {0})".FormatWith(manegementLevel);
            }
            if(!string.IsNullOrEmpty(languageLevel))
            {
                sql += " AND( h.LanguageLevelId = {0})".FormatWith(languageLevel);
            }
            if(!string.IsNullOrEmpty(itLevel))
            {
                sql += " AND( h.ITLevelId = {0})".FormatWith(itLevel);
            }
            if(!string.IsNullOrEmpty(phone))
            {
                sql += " AND( h.CellPhoneNumber LIKE N'%{0}%')".FormatWith(phone);
            }
            if(!string.IsNullOrEmpty(email))
            {
                sql += " AND( h.WorkEmail LIKE N'%{0}%')".FormatWith(email);
            }
            if(!string.IsNullOrEmpty(cpvPosition))
            {
                sql += " AND( OR h.CPVPositionId = {0})".FormatWith(cpvPosition);
            }
            if(!string.IsNullOrEmpty(vyuPosition))
            {
                sql += " AND( h.VYUPositionId = {0})".FormatWith(vyuPosition);
            }
            if(!string.IsNullOrEmpty(armyLevel))
            {
                sql += " AND( h.ArmyLevelId = {0})".FormatWith(armyLevel);
            }
            if(!string.IsNullOrEmpty(maritalStatus))
            {
                sql += " AND( h.MaritalStatusId = {0})".FormatWith(maritalStatus);
            }
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND h.DepartmentId IN({0})".FormatWith(departments);
            }
            //" AND(CAST('' AS NVARCHAR(5)) = -1 OR YEAR(h.CPVJoinedDate) = CAST('' AS NVARCHAR(5)))"+
            //" AND(CAST('' AS NVARCHAR(5)) = -1 OR YEAR(h.CPVOfficialJoinedDate) = CAST('' AS NVARCHAR(5)))"+       
            //" AND(CAST('' AS NVARCHAR(5)) = -1 OR YEAR(h.ArmyJoinedDate) = CAST('' AS NVARCHAR(5)))"+
            //" AND(CAST('' AS NVARCHAR(5)) = -1 OR YEAR(h.ArmyLeftDate) = CAST('' AS NVARCHAR(5)))"+
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        public static string GetStore_ExcelTimeSheetCode(string departments)
        {
            var sql = string.Empty;
            sql += " IF OBJECT_ID('tempdb..#tmpA') IS NOT NULL DROP Table #tmpA ";
            sql += " SELECT " +
                    " ROW_NUMBER() OVER(ORDER BY rc.FullName ASC) AS STT, " +
                    " rc.EmployeeCode AS N'EmployeeCode (Mã nhân viên)', " +
                    " rc.FullName AS N'FullName (Tên nhân viên)', " +
                    " tc.Code AS N'Code (Mã chấm công)', " +
                    " (SELECT TOP 1 SerialNumber FROM hr_TimeSheetMachine WHERE Id = tc.MachineId) AS N'MachineSerialNumber (Số serial máy chấm công)' " +
                    " INTO #tmpA" +
                    " FROM hr_Record rc " +
                    " LEFT JOIN hr_TimeSheetCode tc ON rc.Id = tc.RecordId " +
                    " WHERE 1=1 ";
            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND h.DepartmentId IN({0})".FormatWith(departments);
            }
            sql += " SELECT * FROM #tmpA ";
            return sql;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        public static string GetStore_GetAllByDepartment(string departments)
        {
            var sql = "SELECT * FROM SystemConfig WHERE 1 = 1 ";
            if(!string.IsNullOrEmpty(departments))
                sql += " AND [DepartmentId] IN ({0}) ".FormatWith(departments);
            return sql;
        }

        /// <summary>
        /// get list quantum
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static string GetStore_GetAllQuantum(string searchKey, int? start, int? limit)
        {
            var sql = " SELECT * FROM(" +
                    " SELECT " +
                    " cq.Id," +
                      "	cq.Name,		" +
                      "	cq.Code,		" +
                      "	cq.MonthStep,		" +
                      "	cq.SalaryGrade,		" +
                      "	cq.Description,		" +
                      "	cq.[Percent],		" +
                    " gq.Name as GroupQuantumName," +
                    " ROW_NUMBER() OVER(ORDER BY cq.Name ASC) SK" +
                    " FROM cat_Quantum cq" +
                    " LEFT JOIN cat_GroupQuantum gq ON cq.GroupQuantumId = gq.Id" +
                    " WHERE 1=1					";
            if(!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND (cq.Name LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR cq.Code LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR gq.Name LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR cq.Description LIKE N'%{0}%') ".FormatWith(searchKey);
            }

            sql += " )#tmp					";
            if(start != null && limit != null)
            {
                sql += " WHERE #tmp.SK > {0}".FormatWith(start) +
                   " AND #tmp.SK <= {0}".FormatWith(start + limit);
            }
            return sql;
        }

        /// <summary>
        /// Get list planJobtitle
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static string GetStore_GetListPlanJobTitle(string departments, string searchKey, int? start, int? limit)
        {
            var sql = string.Empty;
            sql += " SELECT * FROM(" +
                   " SELECT " +
                   "	bh.Id, " +
                   "	rc.FullName, " +
                   "	rc.EmployeeCode, " +
                   "	rc.BirthDate, " +
                   "	case when rc.Sex = 0 then N'Nữ' else N'Nam' end as SexName, " +
                   "	(select top 1 cf.Name from cat_Folk cf where cf.Id = rc.FolkId) as FolkName, " +
                   "	(select top 1 c.Name from cat_PlanJobTitle c where c.Id = bh.PlanJobTitleId) as PlanJobTitleName, " +
                   "	(select top 1 c.Name from cat_PlanPhase c where c.Id = bh.PlanPhaseId) as PlanPhaseName, " +
                   "	ROW_NUMBER() OVER(ORDER BY bh.EditedDate, bh.CreatedDate) SK " +
                   "	from hr_BusinessHistory bh " +
                   "	left join hr_Record rc on rc.Id = bh.RecordId " +
                   "	where BusinessType = '{0}' ".FormatWith(BusinessTypePlanJobTitle);

            if(!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN({0})".FormatWith(departments);
            }
            sql += " )#tmp ";
            sql += " WHERE 1=1 ";
            if(!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND (#tmp.FullName LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR #tmp.FolkName LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR #tmp.PlanJobTitleName LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR #tmp.EmployeeCode LIKE N'%{0}%') ".FormatWith(searchKey);
            }

            if(start != null && limit != null)
            {
                sql += " AND #tmp.SK > {0}".FormatWith(start) +
                       " AND #tmp.SK <= {0}".FormatWith(start + limit);
            }
            return sql;
        }


    }
}

