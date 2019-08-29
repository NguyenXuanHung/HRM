using System;
using System.Linq;
using Web.Core;
using Web.Core.Service.Catalog;

/// <summary>
/// Summary description for SQLBusinessAdapter
/// </summary>
public class SQLBusinessAdapter
{
    private const string WorkStatusWorking = "%Đang làm việc%";
    private const string WorkStatusLeave = "%Đang nghỉ phép%";
    private const string BusinessTypeRetirement = "NghiHuu";
    private const string BusinessPersonelRotation = "ThuyenChuyenDieuChuyen";
    private const string RelationName = "%Con%";
    private const int Age = 12;
    private const string ReasonRetirement = "%Nghỉ hưu%";
    private const string ReasonTerminate = "%Đơn phương chấm dứt hđlđ/hđlv%";
    private const string ReasonFired = "%Kỷ luật sa thải%";
    private const string ReasonExpiredContract = "%Thỏa thuận chấm dứt%";
    private const string ReasonOther = "%Lý do khác%";
    private const string Dead = "%Từ trần%";
    //private static object[] fromDate;

    /// <summary>
    /// Danh sách tiền lương
    /// </summary>
    /// <returns></returns>
    public static string GetStore_AllSalary(string department, string fromDate, string toDate, string condition)
    {
        var sql = string.Empty;
        sql += " SELECT rc.FullName, " +
               " rc.BirthDate, " +
               " rc.EmployeeCode, " +
               " p.Name AS PositionName, " +
               " dv.Id AS DepartmentId, " +
               " dv.Name AS DepartmentName,  " +
               " rc.ParticipationDate, " +
               " CASE WHEN hs.Value > 0 THEN hs.Value " +
               " ELSE hs.SalaryContract END AS Value, " +
               " (SELECT TOP 1 ct. Name FROM cat_ContractType ct LEFT JOIN hr_Contract hc ON ct.Id = hc.ContractTypeId WHERE hs.RecordId = hc.RecordId) AS ContractTypeName " +
               " FROM sal_SalaryDecision hs " +
               " LEFT JOIN hr_Record rc ON hs.RecordId = rc.Id " +
               " LEFT JOIN cat_Position p on rc.PositionId = p.Id " +
               " LEFT JOIN cat_Department dv ON dv.Id = rc.DepartmentId " +
               " where hs.EffectiveDate = (select MAX(EffectiveDate) from sal_SalaryDecision sa where sa.RecordId = rc.Id ) ";
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
    /// BC danh sách cán bộ phòng ban
    /// </summary>
    /// <param name="department"></param>
    /// <param name="condition"></param>
    /// <param name="fromDate"></param>
    /// <param name="toDate"></param>
    /// <returns></returns>
    public static string GetStore_EmployeeByDepartment(string department, string condition,string fromDate,string toDate)
    {
        var sql = string.Empty;
        sql += " select " +
               " rc.FullName, " +
               " rc.EmployeeCode, " +
               " cp.Name as PositionName, " +
               " rc.CellPhoneNumber, " +
               " dv.Id as DepartmentId, " +
               " dv.Name as DepartmentName " +
               " from hr_Record rc " +
               " left join cat_Position cp on cp.Id = rc.PositionId " +
               " left join cat_Department dv on dv.Id = rc.DepartmentId " +
               " WHERE 1=1";
        if (!string.IsNullOrEmpty(department))
            sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
        if (!string.IsNullOrEmpty(condition))
            sql += " AND {0}".FormatWith(condition);
        if (!string.IsNullOrEmpty(fromDate))
            sql += " and rc.ParticipationDate >= '{0}'".FormatWith(fromDate);
        if (!string.IsNullOrEmpty(toDate))
            sql += " and rc.ParticipationDate <= '{0}'".FormatWith(toDate);
        return sql;
    }

    /// <summary>
    /// Báo cáo nhân viên sinh nhật trong tháng
    /// </summary>
    /// <param name="department"></param>
    /// <param name="month"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static string GetStore_EmployeeBirthDayInMonth(string department, int month, string condition,string fromDate,string toDate)
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
               " where 1 = 1 " +
               " and MONTH(BirthDate) = {0}".FormatWith(month);

        if (!string.IsNullOrEmpty(department))
            sql += " and DepartmentId IN ({0})".FormatWith(department);
        if (!string.IsNullOrEmpty(condition))
        {
            sql += " AND {0}".FormatWith(condition);
        }
        if (!string.IsNullOrEmpty(fromDate))
            sql += " and rc.BirthDate >= '{0}'".FormatWith(fromDate);
        if (!string.IsNullOrEmpty(toDate))
            sql += " and rc.BirthDate <= '{0}'".FormatWith(toDate);
        return sql;
    }
                                
    /// <summary>
    /// quản lý bổ nhiệm chức vụ
    /// </summary>
    /// <param name="departments"></param>
    /// <param name="searchQuery"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <param name="departmentSelected"></param>
    /// <returns></returns>
    public static string GetStore_ManageAppoitmentPosition(string departments, string searchQuery, int? start, int? limit, string departmentSelected)
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
        if (!string.IsNullOrEmpty(departments))
        {
            sql += " AND rc.DepartmentId IN({0})".FormatWith(departments);
        }
        if (!string.IsNullOrEmpty(departmentSelected))
        {
            var rootId = 0;
            int parseId;
            var selectedDepartment = "{0},".FormatWith(departmentSelected);
            if (int.TryParse(departmentSelected, out parseId))
            {
                rootId = parseId;
            }
            var lstDepartment = cat_DepartmentServices.GetTree(rootId).Select(d => d.Id).ToList();
            if (lstDepartment.Count > 0)
            {
                selectedDepartment = lstDepartment.Aggregate(selectedDepartment, (current, d) => current + "{0},".FormatWith(d));
            }
            selectedDepartment = "{0}".FormatWith(selectedDepartment.TrimEnd(','));
            sql += "AND rc.DepartmentId IN ({0}) ".FormatWith(selectedDepartment);
        }
        sql += "    ) A" +
               " WHERE 1=1 ";
        if (start != null & limit != null)
        {
            sql += " AND A.SK > {0}".FormatWith(start) +
                  " AND A.SK <= ({0})".FormatWith(start + limit);
        }

        if (!string.IsNullOrEmpty(searchQuery))
        {
            sql += " AND A.FullName LIKE N'%{0}%'".FormatWith(searchQuery) +
                   " OR A.EmployeeCode LIKE N'%{0}%'".FormatWith(searchQuery);
        }
        return sql;
    }

    /// <summary>
    /// Quản lý hợp đồng
    /// </summary>
    /// <param name="departments"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <param name="searchKey"></param>
    /// <param name="querySearch"></param>
    /// <param name="departmentSelected"></param>
    /// <returns></returns>
    public static string GetStore_Contract(string departments, int? start, int? limit, string searchKey, string querySearch, string departmentSelected)
    {
        var QueryCondition = string.Empty;
        if (!string.IsNullOrEmpty(querySearch))
        {
            string[] value = querySearch.Split(new[] { ';' }, StringSplitOptions.None);

            if (value.Length >= 1)
            {
                var ContractTypeId = value[0].ToString();
                if (!string.IsNullOrEmpty(ContractTypeId))
                    QueryCondition += @" AND hc.ContractTypeId = '{0}' ".FormatWith(ContractTypeId);
            }
            if (value.Length >= 2)
            {
                var jobTitleId = value[1].ToString();
                if (!string.IsNullOrEmpty(jobTitleId))
                    QueryCondition += @" AND hr.JobTitleId = '{0}' ".FormatWith(jobTitleId);
            }
            if (value.Length >= 3)
            {
                var PositionId = value[2].ToString();
                if (!string.IsNullOrEmpty(PositionId))
                    QueryCondition += @" AND hr.PositionId = '{0}' ".FormatWith(PositionId);
            }
            if (value.Length >= 4)
            {
                var contractStatusId = value[3].ToString();
                if (!string.IsNullOrEmpty(contractStatusId))
                    QueryCondition += @" AND hc.ContractStatusId = '{0}' ".FormatWith(contractStatusId);
            }
        }

        var sql = string.Empty;
        sql += " SELECT * FROM " +
                "(SELECT " +
               "	 hc.Id,	" +
                "	 hc.RecordId,	" +
                "	 hr.EmployeeCode, " +
                "	 hr.FullName ,	 " +
                "	 (SELECT TOP 1 cd.Name FROM cat_Department cd WHERE cd.Id = hr.DepartmentId) AS DepartmentName,	" +
                "	 (SELECT TOP 1 cd.Name FROM cat_Department cd WHERE cd.ParentId = '0') AS DepartmentManagementName,	" +
                "	 hc.ContractNumber,	 " +
                "	 (SELECT TOP 1 ct.Name FROM cat_ContractType ct WHERE ct.Id = hc.ContractTypeId	) AS ContractTypeName," +
                "	 (SELECT TOP 1 cj.Name FROM cat_JobTitle cj WHERE cj.Id = hr.JobTitleId ) AS JobTitleName,	" +
                "	 (SELECT TOP 1 cp.Name FROM cat_Position cp WHERE cp.Id = hr.PositionId ) AS PositionName,	" +
                "	 (SELECT TOP 1 cs.Name FROM cat_ContractStatus cs WHERE cs.Id = hc.ContractStatusId ) AS ContractStatusName, " +
                "	 hc.ContractDate,	" +
                "	 hc.EffectiveDate,	" +
                "	 hc.ContractEndDate,	" +
                "	 hc.PersonRepresent,	" +
                "	 hc.Note,	" +
                "	 hc.AttachFileName,	" +
                "	 ROW_NUMBER() OVER(ORDER BY hr.FullName) SK FROM hr_Contract hc	" +
                "	 LEFT JOIN hr_Record hr on hc.RecordId = hr.Id	" +

                "WHERE 1=1 ";
        if (!string.IsNullOrEmpty(departments))
            sql += "AND hr.DepartmentId IN ({0}) ".FormatWith(departments);
        if (!string.IsNullOrEmpty(departmentSelected))
        {
            var rootId = 0;
            var selectedDepartment = "{0},".FormatWith(departmentSelected);
            if (int.TryParse(departmentSelected, out var parseId))
            {
                rootId = parseId;
            }
            var lstDepartment = cat_DepartmentServices.GetTree(rootId).Select(d => d.Id).ToList();
            if (lstDepartment.Count > 0)
            {
                foreach (var d in lstDepartment)
                {
                    selectedDepartment += "{0},".FormatWith(d);
                }
            }
            selectedDepartment = "{0}".FormatWith(selectedDepartment.TrimEnd(','));
            sql += "AND hr.DepartmentId IN ({0}) ".FormatWith(selectedDepartment);
        }
        if (!string.IsNullOrEmpty(searchKey))
            sql += " AND hr.FullName LIKE N'%{0}%' ".FormatWith(searchKey) +
                    " OR hr.EmployeeCode LIKE N'%{0}%' ".FormatWith(searchKey);
        if (!string.IsNullOrEmpty(QueryCondition))
            sql += QueryCondition;
        sql += " ) A ";
        if (start != null && limit != null)
            sql += "WHERE A.SK > {0} ".FormatWith(start) +
                "AND A.SK <= {0} ".FormatWith(start + limit);

        return sql;
    }

    /// <summary>
    /// lay danh sach nghiep vu
    /// </summary>
    /// <param name="departments"></param>
    /// <param name="searchQuery"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <param name="departmentSelected"></param>
    /// <param name="businessType"></param>
    /// <returns></returns>
    public static string GetStore_GetBusinessHistoryList(string departments, string searchQuery, int? start, int? limit, string departmentSelected, string businessType)
    {
        var sql = "SELECT * FROM " +
                  " (SELECT bh.Id AS 'Id', " +
                  " bh.RecordId AS 'RecordId', " +
                  " rc.EmployeeCode AS 'EmployeeCode', " +
                  " rc.FullName AS 'FullName'," +
                  " bh.DecisionNumber AS DecisionNumber, " +
                  " bh.DecisionMaker AS 'DecisionMaker'," +
                  " bh.DecisionDate AS 'DecisionDate'," +
                  " bh.EffectiveDate AS 'EffectiveDate'," +
                  " bh.ShortDecision AS 'ShortDecision'," +
                  " bh.Description AS 'Description'," +
                  " bh.CurrentPosition AS 'CurrentPosition'," +
                  " bh.OldDepartment AS 'OldDepartment'," +
                  " bh.SourceDepartment AS 'SourceDepartment'," +
                  " bh.DestinationDepartment AS 'DestinationDepartment'," +
                  " bh.DecisionPosition AS 'DecisionPosition'," +
                  " bh.NewPosition AS 'NewPosition'," +
                  " bh.OldPosition AS 'OldPosition'," +
                  " bh.EmulationTitle AS 'EmulationTitle'," +
                  " bh.Money AS 'Money'," +
                  " bh.CurrentDepartment AS 'CurrentDepartment'," +
                  " bh.ExpireDate AS 'ExpireDate'," +
                  " bh.FileScan AS 'FileScan'," +
                  " ROW_NUMBER() OVER(ORDER BY rc.FullName ASC) SK" +
                  " FROM hr_BusinessHistory bh" +
                  " LEFT JOIN hr_Record rc" +
                  " ON bh.RecordId = rc.Id" +
                  " WHERE 1=1 ";
        if (!string.IsNullOrEmpty(departments))
        {
            sql += " AND rc.DepartmentId IN({0})".FormatWith(departments);
        }
        if (!string.IsNullOrEmpty(departmentSelected))
        {
            var rootId = 0;
            var selectedDepartment = "{0},".FormatWith(departmentSelected);
            if (int.TryParse(departmentSelected, out var parseId))
            {
                rootId = parseId;
            }
            var lstDepartment = cat_DepartmentServices.GetTree(rootId).Select(d => d.Id).ToList();
            if (lstDepartment.Count > 0)
            {
                selectedDepartment = lstDepartment.Aggregate(selectedDepartment, (current, d) => current + "{0},".FormatWith(d));
            }
            selectedDepartment = "{0}".FormatWith(selectedDepartment.TrimEnd(','));
            sql += "AND rc.DepartmentId IN ({0}) ".FormatWith(selectedDepartment);
        }
        if (!string.IsNullOrEmpty(businessType))
        {
            sql += "AND bh.BusinessType LIKE N'%{0}%' ".FormatWith(businessType);
        }
        sql += "    ) A" +
               " WHERE 1=1 ";
        if (start != null & limit != null)
        {
            sql += " AND A.SK > {0}".FormatWith(start) +
                  " AND A.SK <= ({0})".FormatWith(start + limit);
        }

        if (!string.IsNullOrEmpty(searchQuery))
        {
            sql += " AND A.FullName LIKE N'%{0}%'".FormatWith(searchQuery) +
                   " OR A.EmployeeCode LIKE N'%{0}%'".FormatWith(searchQuery);
        }
        return sql;
    }

    /// <summary>
    /// Danh sach nhan vien het han hop dong
    /// </summary>
    /// <param name="department"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static string GetStore_ListExpiredContract(string department, string condition)
    {
        var sql = "SELECT rc.DepartmentId," +
                  " dp.Name AS DepartmentName," +
                  " ht.ConstructionId," +
                  " ctt.Name AS ConstructionName," +
                  " rc.EmployeeCode," +
                  " rc.FullName," +
                  " po.Name AS PositionName," +
                  " rc.BirthDate," +
                  " rc.IDNumber," +
                  " rc.IDIssueDate," +
                  " cct.Name AS ContractTypeName," +
                  " hc.ContractDate," +
                  " hc.ContractEndDate" +
                  " FROM hr_Contract hc" +
                  " LEFT JOIN hr_Record rc" +
                  " ON hc.RecordId = rc.Id" +
                  " LEFT JOIN hr_Team ht" +
                  " ON rc.Id = ht.RecordId" +
                  " LEFT JOIN cat_Position po" +
                  " ON rc.PositionId = po.Id" +
                  " LEFT JOIN cat_ContractType cct" +
                  " ON hc.ContractTypeId = cct.Id" +
                  " LEFT JOIN cat_Department dp" +
                  " ON rc.DepartmentId = dp.Id" +
                  " LEFT JOIN cat_Construction ctt" +
                  " ON ht.ConstructionId = ctt.Id" +
                  //" WHERE 1=1";
                  " WHERE DATEDIFF(DAY, hc.ContractEndDate, GETDATE()) >= 0";
        if (!string.IsNullOrEmpty(department))
        {
            sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
        }
        if (!string.IsNullOrEmpty(condition))
        {
            sql += " AND {0}".FormatWith(condition);
        }
        return sql;
    }

    /// <summary>
    /// Quản lý tăng giảm nhân sự
    /// </summary>
    /// <param name="department"></param>
    /// <param name="condition"></param>
    /// <param name="searchKey"></param>
    /// <param name="type"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <param name="departmentSelected"></param>
    /// <param name="fromDate"></param>
    /// <param name="toDate"></param>
    /// <returns></returns>
    public static string GetStore_ManageFluctuationEmployee(string department, string condition, string searchKey, bool? type, int? start, int? limit, string departmentSelected, string fromDate, string toDate)
    {
        var sql = " SELECT * FROM(SELECT fe.Id, fe.RecordId, " +
                  " rc.DepartmentId," +
                  " dp.Name AS DepartmentName," +
                  " ht.TeamId," +
                  " ct.Name AS TeamName," +
                  " ht.ConstructionId," +
                  " ctt.Name AS ConstructionName," +
                  " rc.EmployeeCode," +
                  " rc.FullName," +
                  " rc.BirthDate," +
                  " rc.IDNumber," +
                  " rc.IDIssueDate," +
                  " po.Name AS PositionName," +
                  " jt.Name AS JobTitleName," +
                  " CASE WHEN (fe.Type = 1) THEN fe.Reason ELSE '' END AS ReasonDecrease," +
                  " CASE WHEN (fe.Type = 0) THEN fe.Reason ELSE '' END AS ReasonIncrease," +
                  " CASE WHEN (fe.Type = 1) THEN fe.Date ELSE NULL END AS DecreaseDate," +
                  " CASE WHEN (fe.Type = 0) THEN fe.Date ELSE NULL END AS IncreaseDate," +
                  " ROW_NUMBER() OVER(ORDER BY rc.EmployeeCode DESC) AS RN" +
                  " FROM hr_FluctuationEmployee fe" +
                  " LEFT JOIN hr_Record rc" +
                  " ON fe.RecordId = rc.Id" +
                  " LEFT JOIN cat_Department dp" +
                  " ON rc.DepartmentId = dp.Id" +
                  " LEFT JOIN cat_Position po" +
                  " ON rc.PositionId = po.Id" +
                  " LEFT JOIN cat_JobTitle jt" +
                  " ON rc.JobTitleId = jt.Id" +
                  " LEFT JOIN hr_Team ht" +
                  " ON rc.Id = ht.RecordId" +
                  " LEFT JOIN cat_Team ct" +
                  " ON ht.TeamId = ct.Id" +
                  " LEFT JOIN cat_Construction ctt" +
                  " ON ht.ConstructionId = ctt.Id" +
                  " WHERE 1 = 1";
        if (!string.IsNullOrEmpty(department))
        {
            sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
        }
        if (!string.IsNullOrEmpty(condition))
        {
            sql += " AND {0}".FormatWith(condition);
        }
        if (!string.IsNullOrEmpty(searchKey))
        {
            sql += " AND (rc.FullName LIKE N'%{0}%'".FormatWith(searchKey) +
                   " OR rc.EmployeeCode LIKE N'%{0}%'".FormatWith(searchKey) +
                   " OR fe.Reason LIKE N'%{0}%')".FormatWith(searchKey);
        }
        if (type != null)
        {
            sql += " AND fe.[Type] = '{0}'".FormatWith(type);
        }
        if (!string.IsNullOrEmpty(departmentSelected))
        {
            sql += " AND rc.DepartmentId = {0}".FormatWith(departmentSelected);
        }
        if (!string.IsNullOrEmpty(fromDate))
        {
            sql += " AND fe.Date >= '{0}'".FormatWith(fromDate);
        }
        if (!string.IsNullOrEmpty(toDate))
        {
            sql += " AND fe.Date <= '{0}'".FormatWith(toDate);
        }
        sql += ") #tmp" +
               " WHERE 1=1";
        if (start != null && limit != null)
        {
            sql += " AND (RN > {0} ".FormatWith(start) +
                   " AND RN <= {0})".FormatWith(start + limit);
        }
        return sql;
    }

    /// <summary>
    /// Quản lý tăng giảm bảo hiểm
    /// </summary>
    /// <param name="department"></param>
    /// <param name="condition"></param>
    /// <param name="searchKey"></param>
    /// <param name="type"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <param name="departmentSelected"></param>
    /// <returns></returns>
    public static string GetStore_ManageFluctuationInsurance(string department, string condition, string searchKey, bool? type,
        int? start, int? limit, string departmentSelected)
    {
        var sql = "IF OBJECT_ID('tempdb..#tmpB') IS NOT NULL DROP Table #tmpB" +
                  " SELECT *," +
                  " 0 AS xSalaryInsuranceIn," +
                  " 0 AS xInsuranceSubmitIn," +
                  " 0 AS xSalaryInsuranceDe," +
                  " 0 AS xInsuranceSubmitDe" +
                  " INTO #tmpB" +
                  " FROM(SELECT fi.Id," +
                  " fi.RecordId," +
                  " rc.EmployeeCode," +
                  " rc.FullName," +
                  " rc.IDNumber," +
                  " po.Name AS PositionName," +
                  " rc.DepartmentId," +
                  " dp.Name AS DepartmentName," +
                  " rc.BirthDate," +
                  " ctt.Name AS ConstructionName," +
                  " ct.Name AS TeamName," +
                  " hct.ContractNumber," +
                  " hct.ContractDate," +
                  " hct.EffectiveDate," +
                  " #A.SalaryInsurance," +
                  " (#A.SalaryInsurance*0.32) AS InsuranceSubmit," +
                  " CASE WHEN (fi.[Type] = 1) THEN fi.Reason ELSE '' END AS ReasonDecrease," +
                  " CASE WHEN(fi.[Type] = 0) THEN fi.Reason ELSE '' END AS ReasonIncrease," +
                  " fi.[Type]," +
                  " ROW_NUMBER() OVER(ORDER BY rc.Id DESC) AS RN" +
                  " FROM hr_FluctuationInsurance fi" +
                  " LEFT JOIN hr_Record rc" +
                  " ON fi.RecordId = rc.Id" +
                  " LEFT JOIN hr_Team ht" +
                  " ON rc.Id = ht.RecordId" +
                  " LEFT JOIN cat_Department dp" +
                  " ON rc.DepartmentId = dp.Id" +
                  " LEFT JOIN cat_Position po" +
                  " ON rc.PositionId = po.Id" +
                  " LEFT JOIN cat_Team ct" +
                  " ON ht.TeamId = ct.Id" +
                  " LEFT JOIN cat_Construction ctt" +
                  " ON ht.ConstructionId = ctt.Id" +
                  " LEFT JOIN hr_Contract hct" +
                  " ON rc.Id = hct.RecordId" +
                  " LEFT JOIN(" +
                  " SELECT DISTINCT * FROM" +
                  " (SELECT MAX(EffectiveDate) AS MaxDecisionDate," +
                  " RecordId AS IdRecord" +
                  " FROM sal_SalaryDecision" +
                  " GROUP BY RecordId) #tmpA " +
                  " LEFT JOIN sal_SalaryDecision slr" +
                  " ON #tmpA.MaxDecisionDate = slr.EffectiveDate" +
                  " AND slr.RecordId = #tmpA.IdRecord" +
                  " AND slr.SalaryFactor = (SELECT TOP 1 SalaryFactor" +
                  " FROM(SELECT MAX(SalaryFactor) AS SalaryFactor," +
                  " RecordId" +
                  " FROM sal_SalaryDecision" +
                  " GROUP BY RecordId)#tmp" +
                  " WHERE #tmp.RecordId = slr.RecordId)) #A" +
                  " ON #A.RecordId = rc.Id" +
                  " WHERE 1 = 1";
        if (!string.IsNullOrEmpty(department))
        {
            sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
        }
        if (!string.IsNullOrEmpty(departmentSelected))
        {
            sql += " AND rc.DepartmentId = {0}".FormatWith(departmentSelected);
        }
        if (!string.IsNullOrEmpty(searchKey))
        {
            sql += " AND (rc.EmployeeCode LIKE '%{0}%'".FormatWith(searchKey) +
                   " OR rc.FullName LIKE N'%{0}%'".FormatWith(searchKey) +
                   " OR fi.Reason LIKE N'%{0}%')".FormatWith(searchKey);
        }
        if (type != null)
        {
            sql += " AND fi.[Type] = '{0}'".FormatWith(type);
        }
        if (!string.IsNullOrEmpty(condition))
        {
            sql += " AND {0}".FormatWith(condition);
        }
        sql += ") #tmpFluc" +
               " WHERE 1=1";
        if (start != null && limit != null)
        {
            sql += " AND (RN > {0}".FormatWith(start) +
                   " AND RN <= {0})".FormatWith(start + limit);
        }
        sql += " IF OBJECT_ID('tempdb..#tmpIn') IS NOT NULL DROP Table #tmpIn" +
               " SELECT SUM(CASE WHEN #tmpB.SalaryInsurance IS NULL THEN 0 ELSE #tmpB.SalaryInsurance END) AS SalaryInsurance," +
               " SUM(CASE WHEN #tmpB.InsuranceSubmit IS NULL THEN 0 ELSE #tmpB.InsuranceSubmit END) AS InsuranceSubmit" +
               " INTO #tmpIn" +
               " FROM #tmpB" +
               " WHERE [Type]= 0";

        sql += " IF OBJECT_ID('tempdb..#tmpDe') IS NOT NULL DROP Table #tmpDe" +
               " SELECT SUM(CASE WHEN #tmpB.SalaryInsurance IS NULL THEN 0 ELSE #tmpB.SalaryInsurance END) AS SalaryInsurance," +
               " SUM(CASE WHEN #tmpB.InsuranceSubmit IS NULL THEN 0 ELSE #tmpB.InsuranceSubmit END) AS InsuranceSubmit" +
               " INTO #tmpDe" +
               " FROM #tmpB" +
               " WHERE[Type] = 1";

        sql += " UPDATE #tmpB " +
               " SET xSalaryInsuranceIn = #tmp.salaryInsuranceIn," +
               " xInsuranceSubmitIn = #tmp.insuranceSubmitIn," +
               " xSalaryInsuranceDe = #tmpC.salaryInsuranceDe," +
               " xInsuranceSubmitDe = #tmpC.insuranceSubmitDe" +
               " FROM #tmpB" +
               " INNER JOIN(" +
               " SELECT CASE WHEN #tmpIn.SalaryInsurance IS NULL  THEN 0 ELSE #tmpIn.SalaryInsurance END AS salaryInsuranceIn," +
               " CASE WHEN #tmpIn.InsuranceSubmit IS NULL  THEN 0 ELSE #tmpIn.InsuranceSubmit END AS insuranceSubmitIn" +
               " FROM #tmpIn" +
               " ) #tmp" +
               " ON 1 = 1" +
               " INNER JOIN(" +
               " SELECT CASE WHEN #tmpDe.SalaryInsurance IS NULL  THEN 0 ELSE #tmpDe.SalaryInsurance END AS salaryInsuranceDe," +
               " CASE WHEN #tmpDe.InsuranceSubmit IS NULL  THEN 0 ELSE #tmpDe.InsuranceSubmit END AS insuranceSubmitDe" +
               " FROM #tmpDe" +
               " ) #tmpC" +
               " ON 1 = 1" +
               " SELECT * FROM #tmpB";
        return sql;
    }
    
    public static string GetStore_ReportListRetirement(string departments, string businessType, string condition)
    {
        var sql = string.Empty;
        sql += "SELECT " +
            "   rc.DepartmentId AS 'DepartmentId', " +
            "   (SELECT TOP 1 dv.Name FROM cat_Department dv WHERE dv.Id = rc.DepartmentId ) AS 'DepartmentName', " +
            " rc.FullName," +
            " rc.EmployeeCode," +
            " rc.BirthDate," +
            " bh.DecisionDate," +
            " bh.DecisionMaker," +
            " bh.DecisionNumber," +
            " bh.DecisionPosition," +
            " bh.ShortDecision," +
            " bh.Description," +
            " bh.DestinationDepartment," +
            " bh.OldDepartment," +
            " bh.SourceDepartment," +
            " bh.CurrentDepartment," +
            " bh.CurrentPosition," +
            " bh.OldPosition," +
            " bh.NewPosition," +
            " bh.EffectiveDate," +
            " bh.ExpireDate," +
            " bh.FileScan" +
            " FROM hr_Record rc" +
            " LEFT JOIN hr_BusinessHistory bh ON bh.RecordId = rc.Id" +
            " WHERE 1=1 ";
        if (!string.IsNullOrEmpty(departments))
            sql += "AND RC.DepartmentId IN ({0}) ".FormatWith(departments);
        if (!string.IsNullOrEmpty(businessType))
            sql += "AND (bh.BusinessType = N'{0}' OR bh.BusinessType IS NULL ) ".FormatWith(businessType);
        sql += "AND rc.BirthDate IS NOT NULL AND ((DATEDIFF(YEAR, rc.BirthDate, GETDATE()) > 55 AND rc.Sex = '0')" +
            "OR (DATEDIFF(YEAR, rc.BirthDate, GETDATE()) > 60 AND rc.Sex = '1')) " +
            " AND rc.WorkStatusId = (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Đang làm việc%')";
        if (!string.IsNullOrEmpty(condition))
        {
            sql += " AND {0}".FormatWith(condition);
        }
        return sql;
    }

    #region Home

    /// <summary>
    /// 
    /// </summary>
    /// <param name="departments"></param>
    /// <param name="recordIds"></param>
    /// <returns></returns>
    public static string GetStore_PrintCurriculumVitae(string departments, string startDate, string endDate,  string recordIds)
    {
        var sql = string.Empty;
        sql += " SELECT	" +
           "	rc.EmployeeCode,	 " +
            "	rc.FullName,	 " +
            "	rc.BirthDate,	 " +
            "	DATEDIFF(YEAR, rc.BirthDate, GETDATE()) AS Age,	 " +
            "	CASE WHEN rc.Sex = 1 THEN N'Nam' ELSE N'Nữ' END AS SexName,	 " +
            "	(SELECT TOP 1 cf.Name FROM cat_Folk cf WHERE cf.Id = rc.FolkId ) AS FolkName,	 " +
            "	rc.Address,	 " +
            "	rc.RecruimentDate,	 " +
            "	(SELECT TOP 1 cq.Name FROM cat_Quantum cq  WHERE cq.Id = (SELECT TOP 1 hl.QuantumId FROM sal_SalaryDecision hl WHERE hl.DecisionDate =	 " +
            "	        (SELECT MAX(hl2.DecisionDate) FROM sal_SalaryDecision hl2 WHERE hl2.RecordId = rc.Id)	 " +
            "	        AND hl.RecordId = rc.Id)) AS QuantumName,	 " +
            "	(SELECT TOP 1 cq.Code FROM cat_Quantum cq WHERE cq.Id = (SELECT TOP 1 hl.QuantumId	 " +
            "	    FROM sal_SalaryDecision hl	 " +
            "	    WHERE hl.DecisionDate =	 " +
            "	        (SELECT MAX(hl2.DecisionDate)	 " +
            "	            FROM sal_SalaryDecision hl2	 " +
            "	            WHERE hl2.RecordId = rc.Id)	 " +
            "	        AND hl.RecordId = rc.Id) ) AS QuantumCode,	 " +
            "	(SELECT TOP 1 hl.Grade FROM sal_SalaryDecision hl WHERE hl.DecisionDate =	 " +
            "	    (SELECT MAX(hl2.DecisionDate)	 " +
            "	    FROM sal_SalaryDecision hl2	 " +
            "	    WHERE hl2.RecordId = rc.Id)	 " +
            "	    AND hl.RecordId = rc.Id ) AS SalaryGrade,	 " +
            "	(SELECT TOP 1 ce.Name FROM cat_Education ce WHERE ce.Id = rc.EducationId) AS EducationName,	 " +
            "	(SELECT TOP 1 cp.Name FROM cat_PoliticLevel cp WHERE cp.Id = rc.PoliticLevelId) AS PoliticLevelName,	 " +
            "	(SELECT TOP 1 cj.Name  FROM cat_JobTitle cj WHERE cj.Id = rc.JobTitleId) AS JobTitleName,	 " +
            "	(SELECT TOP 1 p.Name FROM cat_Position p WHERE p.Id = rc.PositionId) AS PositionName,	 " +
            "	rc.AssignedWork,	 " +
            "	cd.Name AS DepartmentName,	 " +
            "	(SELECT TOP 1 cc.Name FROM cat_ContractType cc WHERE cc.Id = hc.ContractTypeId) AS ContractTypeName,	 " +
            "	CASE WHEN(DATEDIFF(DAY, rc.ParticipationDate, getDate()) / 365) = 0 THEN ''	 " +
            "	    ELSE CAST(DATEDIFF(DAY, rc.ParticipationDate, getDate()) / 365 AS NVARCHAR(10)) + N' Năm '	 " +
            "	END + CASE WHEN((DATEDIFF(DAY, rc.ParticipationDate, getDate()) % 365) / 30) = 0 THEN ''	 " +
            "	            ELSE CAST((DATEDIFF(DAY, rc.ParticipationDate, getDate()) % 365) / 30 AS NVARCHAR(10)) + N' Tháng '	 " +
            "	        END + CASE WHEN((DATEDIFF(DAY, rc.ParticipationDate, getDate()) % 365) % 30) = 0 THEN ''	 " +
            "	                ELSE CAST((DATEDIFF(DAY, rc.ParticipationDate, getDate()) % 365) % 30 AS NVARCHAR(10)) + N' Ngày'	 " +
            "	            END AS Seniority,	 " +
            "	(SELECT TOP 1 cw.Name FROM cat_WorkStatus cw WHERE cw.Id = rc.WorkStatusId) AS WorkStatusName,	 " +
            "	rc.WorkEmail,	 " +
            "	rc.CellPhoneNumber,	 " +
            "	rc.OriginalFile	 " +
            "	FROM hr_Record rc	 " +
            "	LEFT JOIN cat_Department cd ON cd.Id = rc.DepartmentId	 " +
            "	LEFT JOIN hr_Contract hc ON hc.RecordId = rc.Id	 " +
            "   WHERE 1=1 ";
        if (!string.IsNullOrEmpty(departments))
        {
            sql += " AND rc.DepartmentId IN ({0}) ".FormatWith(departments);
        }
        if (!string.IsNullOrEmpty(recordIds))
        {
            sql += " AND rc.Id IN ({0}) ".FormatWith(recordIds);
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
                     " ps.Name AS PositionName," +
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
                     " #tmp.Grade AS SalaryGrade," +
                     " #tmp.Factor AS SalaryFactor, " +
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

    public static string GetStore_SRQuaTrinhDaoTao(int id)
    {
        var sql = "SELECT ts.Name AS TrainingSystemName," +
                     " un.Name AS UniversityName," +
                     " CASE WHEN eh.FromDate = '0001-01-01' THEN ''" +
                     " WHEN eh.FromDate != '0001-01-01' THEN " +
                     " CAST(MONTH(eh.FromDate) as NVARCHAR(200))+'/' + " +
                     " CAST(YEAR(eh.FromDate) as NVARCHAR(200)) + '-' + " +
                     " CAST(month(eh.toDate) as NVARCHAR(200)) + '/' + " +
                     " CAST(YEAR(eh.ToDate) as NVARCHAR(200)) END AS 'thang'," +
                     " id.Name AS IndustryName," +
                     " eh.FromDate," +
                     " ed.Name AS EducationName" +
                     " FROM hr_EducationHistory eh" +
                     " LEFT JOIN cat_TrainingSystem ts" +
                     " ON eh.TrainingSystemId = ts.Id" +
                     " LEFT JOIN cat_University un" +
                     " ON eh.UniversityId = un.Id" +
                     " LEFT JOIN hr_Record rc" +
                     " ON eh.RecordId = rc.Id" +
                     " LEFT JOIN cat_Industry id" +
                     " ON eh.IndustryId = id.Id" +
                     " LEFT JOIN cat_Education ed" +
                     " ON eh.EducationId = ed.Id" +
                     " WHERE rc.Id = {0}".FormatWith(id);
        return sql;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="relationship"></param>
    /// <returns></returns>
    public static string GetStore_SRQuanHeGiaDinh(int id, int relationship)
    {
        var sql = "SELECT fr.Id, " +
                  " fr.FullName," +
                  " fr.BirthYear AS Age," +
                  " rl.Name AS RelationshipName," +
                  " ISNULL(fr.Occupation ,'') + ' - '+ ISNULL(fr.WorkPlace,'') + ' - ' + ISNULL(fr.Note,'') AS Note, " +
                  " rl.HasHusband" +
                  " FROM hr_FamilyRelationship fr" +
                  " LEFT OUTER JOIN cat_Relationship rl" +
                  " ON fr.RelationshipId = rl.Id" +
                  " WHERE fr.RecordId = {0}".FormatWith(id) +
                  " AND rl.HasHusband = {0}".FormatWith(relationship) +
                  " ORDER BY rl.[Order]";
        return sql;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string GetStore_SRQuaTrinhCongTac(int id)
    {
        var sql = "SELECT CAST(MONTH(wh.FromDate) AS NVARCHAR(200))+'/'+ " +
                  " CAST(YEAR(wh.FromDate) AS NVARCHAR(200)) + N' - ' +" +
                  " ISNULL(CAST(month(wh.ToDate) as NVARCHAR(200)), '..... ') + '/' +" +
                  " ISNULL(CAST(YEAR(wh.ToDate) as NVARCHAR(200)), '.....') as 'Time', " +
                  " wh.Note" +
                  " FROM hr_WorkHistory wh" +
                  " WHERE wh.RecordId = {0}".FormatWith(id);
        return sql;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string GetStore_SRDienBienLuong(int id)
    {
        var sql = " SELECT " +
                  " sl.EffectiveDate," +
                  " CONVERT(nvarchar, CASE WHEN sl.QuantumId IS NULL THEN ''" +
                  " WHEN sl.QuantumId IS NOT NULL THEN qt.Code END)" +
                  " + '/' + CONVERT(nvarchar, CASE WHEN sl.Grade IS NULL THEN ''" +
                  " WHEN sl.Grade IS NOT NULL AND sl.Grade != '' THEN sl.Grade END) AS 'MaNgach'," +
                  " sl.Factor," +
                  " sl.QuantumId," +
                  " sl.Grade" +
                  " FROM hr_Record rc " +
                  " LEFT JOIN sal_SalaryDecision sl" +
                  " ON rc.Id = sl.RecordId" +
                  " LEFT JOIN cat_Quantum qt" +
                  " ON qt.Id = sl.QuantumId" +
                  " WHERE rc.Id = {0}".FormatWith(id) +
                  " ORDER BY sl.EffectiveDate";
        return sql;
    }
  
    #endregion

    /// <summary>
    /// Danh sách lao động và quỹ lương trích nộp BHXH, BHYT, BHTN
    /// </summary>
    /// <param name="department"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static string GetStore_ListLaborSalaryInsurance(string department, string condition)
    {
        var sql = " IF OBJECT_ID('tempdb..#tmpB') IS NOT NULL DROP Table #tmpB" +
                  " SELECT *," +
                  " 0 AS TotalSalaryInsurance," +
                  " 0 AS TotalEmployeePayInsurance," +
                  " 0 AS TotalCompanyPayInsurance," +
                  " 0 AS SumTotalInsurance" +
                  " INTO #tmpB" +
                  " FROM(" +
                  " SELECT *," +
                  " (EmployeePayInsurance +CompanyPayInsurance) AS TotalInsurance" +
                  " FROM(" +
                  " SELECT rc.EmployeeCode," +
                  " rc.FullName," +
                  " rc.DepartmentId," +
                  " dp.Name AS DepartmentName," +
                  " ht.ConstructionId," +
                  " ctt.Name AS ConstructionName," +
                  " ht.TeamId," +
                  " ct.Name AS TeamName," +
                  " rc.InsuranceNumber," +
                  " YEAR(BirthDate) AS YearOfBirthDate," +
                  " po.Name AS PositionName," +
                  " isnull(#A.SalaryInsurance, 0 ) as SalaryInsurance," +
                  " #A.SalaryPayDate," +
                  " isnull(#A.SalaryInsurance * 0.105, 0) AS EmployeePayInsurance," +
                  " isnull(#A.SalaryInsurance * 0.215, 0) AS CompanyPayInsurance," +
                  " rc.InsuranceIssueDate" +
                  " FROM" +
                  " (SELECT DISTINCT *" +
                  " FROM (SELECT MAX(EffectiveDate) AS MaxDecisionDate," +
                  " RecordId AS IdRecord" +
                  " FROM sal_SalaryDecision" +
                  " GROUP BY RecordId) #tmpA  " +
                  " LEFT JOIN sal_SalaryDecision slr" +
                  " ON #tmpA.MaxDecisionDate = slr.EffectiveDate " +
                  " AND slr.RecordId = #tmpA.IdRecord" +
                  ") #A" +
                  " LEFT JOIN hr_Record rc" +
                  " ON #A.RecordId = rc.Id" +
                  " LEFT JOIN cat_Department dp" +
                  " ON rc.DepartmentId = dp.Id" +
                  " LEFT JOIN hr_Team ht" +
                  " ON rc.Id = ht.RecordId" +
                  " LEFT JOIN cat_Construction ctt" +
                  " ON ht.ConstructionId = ctt.Id" +
                  " LEFT JOIN cat_Team ct" +
                  " ON ht.TeamId = ct.Id" +
                  " LEFT JOIN cat_Position po" +
                  " ON rc.PositionId = po.Id" +
                  " ) #tmp )#tmpC" +
                  " WHERE 1 = 1";
        if (!string.IsNullOrEmpty(department))
        {
            sql += " AND #tmpC.DepartmentId IN ({0})".FormatWith(department);
        }
        if (!string.IsNullOrEmpty(condition))
        {
            sql += " AND {0}".FormatWith(condition);
        }

          sql += " SELECT * FROM #tmpB ";

        return sql;
    }

    #region filter store
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static string GetBirthYear()
    {
        var sql = "SELECT distinct YEAR(BirthDate) AS BirthYear FROM hr_Record WHERE BirthDate IS NOT NULL ORDER BY BirthYear ASC";
        return sql;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static string GetRecruitmentYear()
    {
        var sql = "SELECT DISTINCT YEAR(RecruimentDate) AS RecruitmentYear FROM hr_Record WHERE [RecruimentDate] IS NOT NULL ORDER BY RecruitmentYear ASC";
        return sql;
    }
    

    #endregion
}