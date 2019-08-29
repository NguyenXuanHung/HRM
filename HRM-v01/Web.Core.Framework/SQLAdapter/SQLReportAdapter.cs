using System;
using System.Linq;
using System.Web;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework.Adapter
{
    /// <summary>
    /// Summary description for SQLReportAdapter
    /// </summary>
    public class SQLReportAdapter
    {
        private const string RelationName = "%Con%";
        private const string WorkStatusWorking = "%Đang làm việc%";
        private const int Age = 12;

        /// <summary>
        /// 
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
                      " LEFT JOIN hr_Record rc ON bh.RecordId = rc.Id" + 
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
            if(!string.IsNullOrEmpty(businessType))
            {
                sql += "AND bh.BusinessType LIKE N'%{0}%' ".FormatWith(businessType);
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

        /// <summary>
        /// Báo cáo tăng giảm nhân sự
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
        public static string GetStore_BaoCaoTangGiamNhanSu(string department, string condition, string searchKey, bool? type, int? start, int? limit, string departmentSelected, string fromDate, string toDate)
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
            if(!string.IsNullOrEmpty(department))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
            }
            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            if(!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND (rc.FullName LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR rc.EmployeeCode LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR fe.Reason LIKE N'%{0}%')".FormatWith(searchKey);
            }
            if(type != null)
            {
                sql += " AND fe.[Type] = '{0}'".FormatWith(type);
            }
            if(!string.IsNullOrEmpty(departmentSelected))
            {
                sql += " AND rc.DepartmentId = {0}".FormatWith(departmentSelected);
            }
            if(!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND fe.Date >= '{0}'".FormatWith(fromDate);
            }
            if(!string.IsNullOrEmpty(toDate))
            {
                sql += " AND fe.Date <= '{0}'".FormatWith(toDate);
            }
            sql += ") #tmp" +
                   " WHERE 1=1";
            if(start != null && limit != null)
            {
                sql += " AND (RN > {0} ".FormatWith(start) +
                       " AND RN <= {0})".FormatWith(start + limit);
            }
            return sql;
        }

        /// <summary>
        /// Báo cáo tăng giảm bảo hiểm
        /// </summary>
        /// <param name="department"></param>
        /// <param name="condition"></param>
        /// <param name="searchKey"></param>
        /// <param name="type"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="departmentSelected"></param>
        /// <returns></returns>
        public static string GetStore_BaoCaoTangGiamBh(string department, string condition, string searchKey, bool? type,
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
                      " #A.InsuranceSalary," +
                      " (#A.InsuranceSalary*0.32) AS InsuranceSubmit," +
                      " CASE WHEN (fi.[Type] = 2) THEN (SELECT TOP 1 ri.Name FROM cat_ReasonInsurance ri WHERE fi.ReasonId = ri.Id) ELSE '' END AS ReasonDecrease," +
                      " CASE WHEN(fi.[Type] = 1) THEN (SELECT TOP 1 ri.Name FROM cat_ReasonInsurance ri WHERE fi.ReasonId = ri.Id) ELSE '' END AS ReasonIncrease," +
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
                      " AND slr.Factor = (SELECT TOP 1 Factor" +
                      " FROM(SELECT MAX(Factor) AS SalaryFactor," +
                      " RecordId" +
                      " FROM sal_SalaryDecision" +
                      " GROUP BY RecordId)#tmp" +
                      " WHERE #tmp.RecordId = slr.RecordId)) #A" +
                      " ON #A.RecordId = rc.Id" +
                      " WHERE 1 = 1";
            if(!string.IsNullOrEmpty(department))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
            }
            if(!string.IsNullOrEmpty(departmentSelected))
            {
                sql += " AND rc.DepartmentId = {0}".FormatWith(departmentSelected);
            }
            if(!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND (rc.EmployeeCode LIKE '%{0}%'".FormatWith(searchKey) +
                       " OR rc.FullName LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR fi.Reason LIKE N'%{0}%')".FormatWith(searchKey);
            }
            if(type != null)
            {
                sql += " AND fi.[Type] = '{0}'".FormatWith(type);
            }
            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += ") #tmpFluc" +
                   " WHERE 1=1";
            if(start != null && limit != null)
            {
                sql += " AND (RN > {0}".FormatWith(start) +
                       " AND RN <= {0})".FormatWith(start + limit);
            }
            sql += " IF OBJECT_ID('tempdb..#tmpIn') IS NOT NULL DROP Table #tmpIn" +
                   " SELECT SUM(CASE WHEN #tmpB.InsuranceSalary IS NULL THEN 0 ELSE #tmpB.InsuranceSalary END) AS SalaryInsurance," +
                   " SUM(CASE WHEN #tmpB.InsuranceSubmit IS NULL THEN 0 ELSE #tmpB.InsuranceSubmit END) AS InsuranceSubmit" +
                   " INTO #tmpIn" +
                   " FROM #tmpB" +
                   " WHERE [Type]= 0";

            sql += " IF OBJECT_ID('tempdb..#tmpDe') IS NOT NULL DROP Table #tmpDe" +
                   " SELECT SUM(CASE WHEN #tmpB.InsuranceSalary IS NULL THEN 0 ELSE #tmpB.InsuranceSalary END) AS SalaryInsurance," +
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


        /// <summary>
        /// Báo cáo cán bộ, công chức, viên chức toàn cơ quan
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeList(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;

            sql += " SELECT DISTINCT " +
                   " rc.DepartmentId, " +
                   " dp.Name AS 'DepartmentName', " +
                   " '' AS 'STT', " +
                   " rc.EmployeeCode AS 'EmployeeCode', " +
                   " rc.FullName AS 'FullName', " +
                   " rc.BirthDate AS 'BirthDate', " +
                   " DATEDIFF(YEAR, rc.BirthDate, GETDATE()) AS 'Age', " +
                   " CASE WHEN rc.Sex = '1' THEN 'Nam' " +
                   "       ELSE N'Nữ' " +
                   "       END " +
                   "   AS 'SexName', " +
                   " rc.Address AS 'Address', " +
                   " cjt.Name AS 'JobTitleName', " +
                   " cp.Name AS 'PositionName', " +
                   " (SELECT TOP 1 cq.Name FROM cat_Quantum cq, sal_SalaryDecision hs WHERE hs.QuantumId = cq.Id and hs.RecordId = rc.Id) AS 'QuantumName', " +
                   " (SELECT TOP 1 cq.Code FROM cat_Quantum cq, sal_SalaryDecision hs WHERE hs.QuantumId = cq.Id and hs.RecordId = rc.Id) AS 'QuantumCode', " +
                   " (SELECT TOP 1 hs.SalaryGrade FROM cat_Quantum cq, sal_SalaryDecision hs WHERE hs.QuantumId = cq.Id and hs.RecordId = rc.Id) AS 'SalaryGrade', " +
                   " (SELECT TOP 1 hs.SalaryFactor FROM sal_SalaryDecision hs WHERE hs.RecordId = rc.Id ORDER BY hs.DecisionDate desc) AS 'SalaryFactor', " +
                   //" (SELECT TOP 1 hs.PositionAllowance FROM sal_SalaryDecision hs WHERE hs.RecordId = rc.Id ORDER BY hs.DecisionDate DESC) AS 'PositionAllowance', " +
                   //" (SELECT TOP 1 hs.OtherAllowance FROM sal_SalaryDecision hs WHERE hs.RecordId = rc.Id ORDER BY hs.DecisionDate DESC) AS 'OtherAllowance', " +
                   " ce.Name AS 'EducationName', " +
                   " (SELECT TOP 1 hs.ContractNumber FROM hr_Contract hs WHERE hs.RecordId = rc.Id ORDER BY hs.ContractDate DESC) AS 'ContractNumber', " +
                   " ctt.Name AS 'ContractTypeName', " +
                   " (SELECT TOP 1 hs.ContractDate FROM hr_Contract hs WHERE hs.RecordId = rc.Id ORDER BY hs.ContractDate DESC) AS 'ContractDate' " +

                   " FROM hr_Record rc " +
                   "   LEFT JOIN hr_Contract hc " +
                   "       ON hc.RecordId  = rc.Id " +
                   "   LEFT JOIN cat_ContractType ctt" + // filter contractType
                   "       ON hc.ContractTypeId = ctt.Id " +
                   "   LEFT JOIN cat_JobTitle cjt " + // filter jobtitle
                   "       ON rc.JobTitleId = cjt.Id " +
                   "   LEFT JOIN cat_Position cp " + //filter position
                   "       ON rc.PositionId = cp.Id " +
                   "   LEFT JOIN sal_SalaryDecision hs" +
                   "       ON hs.RecordId = rc.Id" +
                   "   LEFT JOIN cat_Education ce " + //filter education
                   "       ON rc.EducationId = ce.Id " +
                   "   LEFT JOIN cat_Department dp " + //filter department
                   "       ON rc.DepartmentId = dp.Id " +
                   " WHERE 1=1 ";
            if(!string.IsNullOrEmpty(department))
                sql += " and rc.DepartmentId IN ({0})".FormatWith(department);
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
                   " CASE WHEN rc.Sex = 1 THEN N'Nam' " +
                   " ELSE N'Nữ' END SexName, " +
                   "     rc.Address, " +
                   " rc.CPVJoinedDate, " +
                   " rc.CPVOfficialJoinedDate, " +
                   " rc.CPVJoinedPlace, " +
                   " rc.CPVCardNumber, " +
                   " (SELECT TOP 1 cp.Name FROM cat_CPVPosition ccp where rc.CPVPositionId = ccp.Id) AS CPVJoinedPosition, " + //filer cpvposition
                   " (SELECT TOP 1 cf.Name FROM cat_Folk cf WHERE rc.FolkId = cf.Id) AS FolkName, " + // filter folk
                   " (SELECT TOP 1 cjt.Name FROM cat_JobTitle cjt WHERE cjt.Id = rc.JobTitleId) AS JobTitleName, " + // filter jobtitle
                   " (SELECT TOP 1 cpl.Name FROM cat_PoliticLevel cpl WHERE rc.PoliticLevelId = cpl.Id) AS PoliticLevelName, " + // filter politiclevel
                   "     cp.Name AS PositionName, " +
                   "     dp.Id AS DepartmentId, " +
                   "     dp.Name AS DepartmentName,  " +
                   "     rc.CellPhoneNumber " +
                   "     FROM hr_Record rc " +
                   " LEFT JOIN cat_Position cp ON rc.PositionId = cp.Id " + // filter postion
                   " LEFT JOIN cat_Department dp ON rc.DepartmentId = dp.Id " + // filter department
                   " WHERE rc.CPVJoinedDate IS NOT NULL";

            if(!string.IsNullOrEmpty(department))
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
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
        /// Danh sách hợp đồng của nhân viên
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_ContractOfEmployee(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            //create SQL
            // xoa bang #tmpB neu da ton tai
            sql += "IF OBJECT_ID('tempdb..#tmpB') IS NOT NULL DROP Table #tmpB ";
            sql += " SELECT 	 " +
                   "'' AS 'STT', " +
                   "rc.DepartmentId, " +
                   "dp.Name AS 'DepartmentName', " +
                   "rc.EmployeeCode, " +
                   "rc.FullName, " +
                   "dp.Name AS 'CurrentDepartment',	 " +
                   "cp.Name AS 'CurrentPosition', " +
                   "hc.ContractNumber,	 " +
                   "hc.ContractDate, " +
                   "ct.Name AS 'ContractTypeName', " +
                   "ct.ContractMonth AS 'ContractTime',	 " +
                   "hc.PersonRepresent AS 'Signer', " +
                   "cpp.Name AS 'PositionSigner', " +
                   "ct.ContractMonth - DATEDIFF(month, hc.ContractDate, getDate()) AS 'ContractTimeRemain' " +
                   "   INTO #tmpB " +
                   "FROM hr_Contract hc " +
                   "LEFT JOIN cat_ContractType ct " +
                   " ON hc.ContractTypeId = ct.Id " + // filter contract type
                   "LEFT JOIN hr_Record rc " +
                   " 	ON hc.RecordId = rc.Id " +
                   " LEFT JOIN cat_Position cp " +
                   " 	ON rc.PositionId = cp.Id " + // filter position
                   " LEFT JOIN cat_Department dp " +
                   " 	ON rc.DepartmentId = dp.Id " + // filter department
                   " LEFT JOIN cat_Position cpp " +
                   " 	ON hc.PersonPositionId = cpp.Id " +
                   " WHERE 1=1 ";
            if (!string.IsNullOrEmpty(department))
            {
                sql += " AND rc.DepartmentId IN ({0}) ".FormatWith(department);
            }
            if (!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND hc.ContractDate >= '{0}'".FormatWith(fromDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                sql += " AND hc.ContractDate <= '{0}'".FormatWith(toDate);
            }
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += " SELECT * FROM #tmpB order by DepartmentId ";

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

            //create SQL
            // xoa bang #tmpB neu da ton tai
            sql += "IF OBJECT_ID('tempdb..#tmpB') IS NOT NULL DROP Table #tmpB ";
            sql += " SELECT " +
                   "   '' AS 'STT', " +
                   "   rc.EmployeeCode AS 'MaCanBo', " +
                   "   rc.DepartmentId AS 'MaDonVi', " +
                   " rc.FullName AS 'HoTen', " +
                   " rc.BirthDate AS 'NgaySinh', " +
                   " (SELECT [Name] FROM cat_Department dp WHERE rc.DepartmentId = dp.Id) AS 'DonViCongTac', " +
                   " wp.DecisionNumber AS 'SoQuyetDinh', " +
                   " wp.DecisionDate AS 'NgayQuyetDinh', " +
                   " wp.EffectiveDate AS 'NgayHieuLuc', " +
                   " (SELECT TOP 1 cp.Name FROM cat_Position cp WHERE wp.NewPositionId = cp.Id) AS 'ChucVuBoNhiem', " +
                   " wp.ExpireDate AS 'ThoiHanBoNhiem',	 " +
                   " wp.SourceDepartment AS 'CoQuanBoNhiem', " +
                   " wp.DecisionMaker AS 'NguoiKy', " +
                   " wp.MakerPosition AS 'MakerPosition', " +
                   "   (SELECT dp.Name FROM cat_Department dp WHERE rc.DepartmentId = dp.Id) AS 'GROUP' " + // filter department
                   " INTO #tmpB " +
                   " FROM hr_Record rc, " +
                   " 	hr_WorkProcess wp " +
                   " WHERE wp.RecordId = rc.Id " +
                   " 	AND rc.DepartmentId IN ({0}) ".FormatWith(department);
                    if(!string.IsNullOrEmpty(fromDate))
                        sql += " AND wp.DecisionDate >= '{0}'".FormatWith(fromDate);
                    if(!string.IsNullOrEmpty(toDate))
                        sql += " AND wp.DecisionDate <= '{0}'".FormatWith(toDate);
                    if(!string.IsNullOrEmpty(condition))
                        sql += " AND {0}".FormatWith(condition);

            sql += " SELECT * FROM #tmpB ";
                    return sql;
        }

        /// <summary>
        /// Báo cáo danh sách hết hạn hợp đồng
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeExpried(string department, string fromDate, string toDate, string condition, int day)
        {
            var sql = "SELECT  rc.EmployeeCode, " +
                      " rc.FullName," +
                      " rc.DepartmentId, " +
                      " dp.Name AS DepartmentName," +
                      " ct.Name AS ContractTypeName," +
                      " ct.ContractMonth, " +
                      " hc.ContractNumber," +
                      " hc.ContractDate," +
                      " hc.ContractEndDate," +
                      " hc.PersonRepresent," +
                      " ct.ContractMonth - DATEDIFF(month, hc.ContractDate, getDate()) AS 'ContractRemain', " +
                      " cp.Name AS PositionName," +
                      " (SELECT TOP 1 cp.Name FROM cat_Position cp WHERE cp.Id = hc.PersonPositionId) AS PersonPosition " +
                      "   FROM hr_Contract hc " +
                      "       LEFT JOIN hr_Record rc ON hc.RecordId = rc.Id" +
                      "       LEFT JOIN cat_ContractType ct ON hc.ContractTypeId = ct.Id " +
                      "       LEFT JOIN cat_Department dp ON rc.DepartmentId = dp.Id " +
                      "       LEFT JOIN cat_Position cp ON rc.PositionId = cp.Id " +
                      "   WHERE (DATEDIFF(DD, hc.ContractEndDate,GETDATE()) > 0" +
                      " AND DATEDIFF(DD,hc.ContractEndDate,GETDATE()) <= {0})".FormatWith(day) +
                      " and DATEDIFF(MONTH, hc.ContractEndDate, GETDATE()) <= 3" +
                      " AND rc.WorkStatusId = (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%{0}%')".FormatWith(RecordStatus.Working.Description()) +
                      " AND ISNULL(ContractEndDate, '0001-01-01') <> '0001-01-01'" +
                      " AND rc.DepartmentId IN ({0})".FormatWith(department);
            if (!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND hc.ContractDate >= '{0}'".FormatWith(fromDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                sql += " AND hc.ContractDate <= '{0}'".FormatWith(toDate);
            }
            if (!string.IsNullOrEmpty(condition))
                sql += " AND {0}".FormatWith(condition);

            return sql;
        }

        /// <summary>
        /// Báo cáo danh sách cán bộ theo phòng ban
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeByDepartment(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;

            sql += " SELECT DISTINCT " +
                     " rc.DepartmentId AS 'MaDonVi', " +
                   " dmdv.Name AS 'TenDonVi', " +
                   " '' AS 'STT', " +
                   " rc.EmployeeCode AS 'SoHieuCCVC', " +
                   " rc.FullName AS 'HoTen', " +
                   " rc.BirthDate AS 'SinhNgay', " +
                   " DATEDIFF(YEAR, rc.BirthDate, GETDATE()) AS 'Tuoi', " +
                   " CASE WHEN rc.Sex = '1' THEN 'Nam' " +
                   "       ELSE N'Nữ' " +
                   "       END " +
                   "   AS 'GioiTinh', " +
                   " rc.Address AS 'DiaChiThuongTru', " +
                   " dmcv.Name AS 'ChucDanh', " +
                   " cv.Name AS 'ChucVu', " +
                   " (select top 1 cq.Name from cat_Quantum cq, sal_SalaryDecision hr where cq.Id = hr.QuantumId and hr.RecordId = rc.Id) AS 'TenNgach', " +
                   " (select top 1 cq.Code from cat_Quantum cq, sal_SalaryDecision hr where cq.Id = hr.QuantumId and hr.RecordId = rc.Id) AS 'MaNgach', " +
                   " (select top 1 hr.SalaryGrade from cat_Quantum cq, sal_SalaryDecision hr where cq.Id = hr.QuantumId and hr.RecordId = rc.Id) AS 'BacLuong', " +
                   " (select top 1 hr.SalaryFactor from sal_SalaryDecision hr where hr.RecordId = rc.Id order by hr.DecisionDate desc) AS 'HeSo', " +
                   //" (select top 1 hr.PositionAllowance from sal_SalaryDecision hr where hr.RecordId = rc.Id order by hr.DecisionDate desc) AS 'PhuCapChucVu', " +
                   //" (select top 1 hr.OtherAllowance from sal_SalaryDecision hr where hr.RecordId = rc.Id order by hr.DecisionDate desc) AS 'PhuCapKhac', " +
                   " td.Name AS 'TrinhDoChuyenMon', " +
                   " (select top 1 hr.ContractNumber from hr_Contract hr where hr.RecordId = rc.Id order by hr.ContractDate desc) AS 'SoHopDong', " +
                   " ct.Name AS 'LoaiHopDong', " +
                   " (select top 1 hr.ContractDate from hr_Contract hr where hr.RecordId = rc.Id order by hr.ContractDate desc) AS 'NgayKy' " +

                   " FROM hr_Record rc " +
                   "   LEFT JOIN hr_Contract hc " +
                   "       ON rc.Id = hc.RecordId " +
                   "   LEFT JOIN cat_JobTitle dmcv " +
                   "       ON rc.JobTitleId = dmcv.Id " + // filter job tilte
                   "   LEFT JOIN cat_Position cv " +
                   "       ON rc.PositionId = cv.Id " + // filter position
                   "   LEFT JOIN cat_ContractType ct" +
                   "       ON ct.Id = hc.ContractTypeId" + // filter contract type
                   "   LEFT JOIN cat_Education td " +  
                   "       ON rc.EducationId = td.Id " + // filer education
                   "   LEFT JOIN cat_Department dmdv " +
                   "       ON rc.DepartmentId = dmdv.Id " + // filter department
                   "   WHERE rc.DepartmentId IN ({0})".FormatWith(department);
            return sql;
        }

        /// <summary>
        /// Báo cáo danh sách cán bộ được điều động (luân chuyển, biệt phái) đến/đi
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="businessType"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_ListBusinessHistory(string departments, string fromDate, string toDate, string businessType, string condition)
        {
            var sql = string.Empty;
            sql += " SELECT " +
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
                   " bh.EmulationTitle," +
                   " bh.Money," +
                   " bh.FileScan" +
                   " FROM hr_BusinessHistory bh" +
                   " LEFT JOIN hr_Record rc ON bh.RecordId = rc.Id" +
                   " WHERE 1=1 ";
            if(!string.IsNullOrEmpty(departments))
                sql += "AND RC.DepartmentId IN ({0}) ".FormatWith(departments);
            if(!string.IsNullOrEmpty(businessType))
                sql += "AND bh.BusinessType = N'{0}' ".FormatWith(businessType);
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

        /// <summary>
        /// Báo cáo thâm niên công tác của cán bộ
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <param name="typeReport"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeSeniority(string departments, string fromDate, string toDate, string condition, string typeReport)
        {
            var sql = string.Empty;
            sql += "   SELECT " +
                   " '' AS 'STT', " +
                   " rc.DepartmentId AS 'DepartmentId', " +
                   " dp.Name AS 'GROUP', " +
                   " rc.EmployeeCode AS 'EmployeeCode', " +
                   " rc.FullName AS 'FullName',   " +
                   " rc.BirthDate AS 'BirthDate',  " +
                   " DATEDIFF(YEAR, rc.BirthDate, GETDATE()) AS 'Age',   " +
                   " CASE WHEN rc.Sex = '0' THEN N'Nữ' ELSE N'Nam' END AS 'SexName', " +
                   " rc.ResidentPlace AS 'ResidentPlace', " +
                   " cj.Name AS 'JobTitleName',   " +
                   " cp.Name AS 'PositionName', " +
                   " cpv.Name AS 'CPVPositionName', " +
                   " vyu.Name AS 'VYUPositionName',  " +
                   " ce.Name AS 'EducationName',  " +
                   " pl.Name AS 'PoliticlLevelName',  " +
                   " ml.Name AS 'ManagementLevelName',  " +
                   " ll.Name AS 'LanguageLevelName',  " +
                   " ci.Name AS 'ITLevelName', " +
                   " rc.RecruimentDate AS 'RecruimentDate',  " +
                   " hc.ContractNumber AS 'ContractNumber', " +
                   " ct.Name AS 'ContractTypeName', " +
                   " hc.ContractDate AS 'ContractDate',	 ";
            if (string.IsNullOrEmpty(typeReport))
            {
                //Case of CCVC
                sql += "   CASE WHEN (DATEDIFF(DAY, rc.FunctionaryDate, getDate())/365) = 0 THEN '' ELSE " +
                       "   CAST(DATEDIFF(DAY, rc.FunctionaryDate, getDate())/365 AS NVARCHAR(10)) + N' Năm ' END + " +
                       "   CASE WHEN ((DATEDIFF(DAY, rc.FunctionaryDate, getDate())%365) /30) = 0 THEN '' ELSE " +
                       "   CAST((DATEDIFF(DAY, rc.FunctionaryDate, getDate())%365) /30 AS NVARCHAR(10)) + N' Tháng ' END + " +
                       "   CASE WHEN ((DATEDIFF(DAY, rc.FunctionaryDate, getDate())%365) %30) = 0 THEN '' ELSE " +
                       "   CAST((DATEDIFF(DAY, rc.FunctionaryDate, getDate())%365) %30 AS NVARCHAR(10)) + N' Ngày' END AS 'Seniority' ";
            }
            else
            {
                //Case of enterprise
                sql += "   CASE WHEN (DATEDIFF(DAY, rc.ParticipationDate, getDate())/365) = 0 THEN '' ELSE " +
                       "   CAST(DATEDIFF(DAY, rc.ParticipationDate, getDate())/365 AS NVARCHAR(10)) + N' Năm ' END + " +
                       "   CASE WHEN ((DATEDIFF(DAY, rc.ParticipationDate, getDate())%365) /30) = 0 THEN '' ELSE " +
                       "   CAST((DATEDIFF(DAY, rc.ParticipationDate, getDate())%365) /30 AS NVARCHAR(10)) + N' Tháng ' END + " +
                       "   CASE WHEN ((DATEDIFF(DAY, rc.ParticipationDate, getDate())%365) %30) = 0 THEN '' ELSE " +
                       "   CAST((DATEDIFF(DAY, rc.ParticipationDate, getDate())%365) %30 AS NVARCHAR(10)) + N' Ngày' END AS 'Seniority' ";
            }

            sql += " FROM hr_Record rc  	 " +
                   " LEFT JOIN cat_Folk cf   " +
                   "     ON rc.FolkId  = cf.Id " +
                   " LEFT JOIN cat_JobTitle cj   " +
                   "     ON rc.JobTitleId = cj.Id " +
                   " LEFT JOIN cat_Position cp  	 " +
                   "     ON rc.PositionId = cp.Id" +
                   " LEFT JOIN cat_Department dp  	 " +
                   "     ON rc.DepartmentId = dp.Id" +
                   " LEFT JOIN cat_Education ce   " +
                   "     ON rc.EducationId = ce.Id " +
                   " LEFT JOIN cat_PoliticLevel pl   " +
                   "     ON rc.PoliticLevelId = pl.Id" +
                   " LEFT JOIN cat_ManagementLevel ml  	 " +
                   "     ON rc.ManagementLevelId = ml.Id " +
                   " LEFT JOIN cat_LanguageLevel ll  		 " +
                   "     ON rc.LanguageLevelId = ll.Id" +
                   " LEFT JOIN cat_CPVPosition cpv  	 " +
                   "     ON rc.CPVPositionId = cpv.Id " +
                   " LEFT JOIN cat_VYUPosition vyu	 " +
                   " 	ON rc.VYUPositionId = vyu.Id " +
                   " LEFT JOIN cat_ITLevel ci		 " +
                   " 	ON rc.ITLevelId = ci.Id " +
                   " LEFT JOIN hr_Contract hc	 " +
                   " 	ON hc.RecordId = rc.Id	 " +
                   " LEFT JOIN cat_ContractType ct		 " +
                   " 	ON hc.ContractTypeId = ct.Id" +
                   " WHERE rc.DepartmentId IN ({0}) ".FormatWith(departments);
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += " ORDER BY rc.DepartmentId, rc.FullName	";

            return sql;
        }

        /// <summary>
        /// Báo cáo danh sách cán bộ được hưởng chính sách
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeCompensation(string department, string fromDate, string toDate, string condition, int start, int limit)
        {
            var sql = "SELECT * FROM (SELECT rc.EmployeeCode, " +
                      " rc.FullName," +
                      " dp.Name AS DepartmentName," +
                      " po.Name AS PositionName," +
                      " fp.Name AS FamilyPolicyName," +
                      " al.Name AS ArmyLevelName," +
                      " rc.RankWounded," +
                      " ROW_NUMBER() OVER(ORDER BY rc.Employeecode ASC) AS RN" +
                      " FROM hr_Record rc" +
                      " LEFT JOIN cat_Department dp" +
                      " ON rc.DepartmentId = dp.Id" +
                      " LEFT JOIN cat_Position po" +
                      " ON rc.PositionId = po.Id" +
                      " LEFT JOIN cat_FamilyPolicy fp" +
                      " ON rc.FamilyPolicyId = fp.Id" +
                      " LEFT JOIN cat_ArmyLevel al" +
                      " ON rc.ArmyLevelId = al.Id" +
                      " WHERE FamilyPolicyId != 0";
            if(!string.IsNullOrEmpty(department))
            {
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
            }
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
            sql += ") #tmp" +
                   " WHERE (RN > {0} ".FormatWith(start) +
                   "AND RN <= {0})".FormatWith(start + limit);
            return sql;
        }

        /// <summary>
        /// Báo cáo danh sách cán bộ nghỉ hưu
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="businessType"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeRetirement(string departments, string fromDate, string toDate, string businessType, string condition)
        {
            const string femaleAgeRetirement = "55";
            const string maleAgeRetrirement = "60";
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
            if(!string.IsNullOrEmpty(departments))
                sql += "AND RC.DepartmentId IN ({0}) ".FormatWith(departments);
            if(!string.IsNullOrEmpty(businessType))
                sql += "AND (bh.BusinessType = N'{0}' OR bh.BusinessType IS NULL ) ".FormatWith(businessType);
            sql += "AND rc.BirthDate IS NOT NULL AND ((DATEDIFF(YEAR, rc.BirthDate, GETDATE()) > " + femaleAgeRetirement + " AND rc.Sex = '0')" +
                   "OR (DATEDIFF(YEAR, rc.BirthDate, GETDATE()) > " + maleAgeRetrirement + " AND rc.Sex = '1')) " +
                   " AND rc.WorkStatusId = (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%{0}%')".FormatWith(RecordStatus.Working.Description());
            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            if (!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND bh.DecisionDate >= '{0}'".FormatWith(fromDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                sql += " AND bh.DecisionDate <= '{0}'".FormatWith(toDate);
            }
            return sql;
        }

        /// <summary>
        /// Báo cáo danh sách đảng viên
        /// </summary>
        /// <param name="department"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// /// <param name="condition"></param>
        /// <returns></returns>
        //public static string GetStore_PartyMember(string department, string fromDate, string toDate, string condition)
        //{
        //    var sql = string.Empty;
        //    sql += " SELECT rc.FullName, " +
        //           " rc.BirthDate, " +
        //           " rc.EmployeeCode, " +
        //           " rc.BirthDate, " +
        //           " case when rc.Sex = 1 then N'Nam' " +
        //           " else N'Nữ' end SexName, " +
        //           "     rc.Address, " +
        //           " rc.CPVJoinedDate, " +
        //           " rc.CPVOfficialJoinedDate, " +
        //           " rc.CPVJoinedPlace, " +
        //           " rc.CPVCardNumber, " +
        //           " (select top 1 cp.Name from cat_CPVPosition cp where cp.Id = rc.CPVPositionId) AS CPVJoinedPosition, " +
        //           " (select top 1 cf.Name from cat_Folk cf where cf.Id = rc.FolkId) AS FolkName, " +
        //           " (select top 1 cj.Name from cat_JobTitle cj where cj.Id = rc.JobTitleId) AS JobTitleName, " +
        //           " (select top 1 pl.Name from cat_PoliticLevel pl where pl.Id = rc.PoliticLevelId) AS PoliticLevelName, " +
        //           " (select top 1 hs.SalaryInsurance from sal_SalaryDecision hs where hs.RecordId = rc.Id " +
        //           "    and hs.EffectiveDate = (select MAX(hs2.EffectiveDate) from sal_SalaryDecision hs2 where hs2.RecordId = rc.Id)) " +
        //           " 		as SalaryInsurance, " +
        //           "     p.Name AS PositionName, " +
        //           "     dv.Id AS DepartmentId, " +
        //           "     dv.Name AS DepartmentName,  " +
        //           "     rc.CellPhoneNumber " +
        //           "     FROM hr_Record rc " +
        //           " LEFT JOIN cat_Position p on rc.PositionId = p.Id " +
        //           " LEFT JOIN cat_Department dv ON dv.Id = rc.DepartmentId " +
        //           " WHERE rc.CPVJoinedDate IS NOT NULL";

        //    if(!string.IsNullOrEmpty(department))
        //        sql += " and rc.DepartmentId IN ({0})".FormatWith(department);
        //    if(!string.IsNullOrEmpty(fromDate))
        //    {
        //        sql += " AND rc.CPVJoinedDate >= '{0}'".FormatWith(fromDate);
        //    }
        //    if(!string.IsNullOrEmpty(toDate))
        //    {
        //        sql += " AND rc.CPVJoinedDate <= '{0}'".FormatWith(toDate);
        //    }
        //    if(!string.IsNullOrEmpty(condition))
        //    {
        //        sql += " AND {0}".FormatWith(condition);
        //    }
        //    return sql;
        //}

        /// <summary>
        /// Báo cáo danh sách đối tượng bồi dưỡng cán bộ nguồn tỉnh ủy quản lý
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="businessType"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_EmployeeFostering(string departments, string fromDate, string toDate, string businessType, string condition)
        {
            var sql = string.Empty;
            sql += " SELECT " +
                   "	bh.Id, " +
                   "	rc.FullName, " +
                   "    rc.DepartmentId, " +
                   "    rc.PositionId, " +
                   "	rc.EmployeeCode, " +
                   "	rc.BirthDate, " +
                   "    rc.JobTitleId, " +
                   "	case when rc.Sex = 0 then N'Nữ' else N'Nam' end as SexName, " +
                   "	(select top 1 cd.Name from cat_Department cd where cd.Id = rc.DepartmentId) as DepartmentName, " +
                   "    (select top 1 cj.Name from cat_JobTitle cj where cj.Id = rc.JobTitleId) as JobTitleName, " +
                   "	(select top 1 cp.Name from cat_Position cp where cp.Id = rc.PositionId) as PositionName, " +
                   "	(select top 1 cf.Name from cat_Folk cf where cf.Id = rc.FolkId) as FolkName, " +
                   "    (select top 1 cc.Name from cat_CPVPosition cc where cc.Id = rc.CPVPositionId) as CPVPositionName, " +
                   "    (select top 1 ce.Name from cat_Education ce, hr_EducationHistory eh where ce.Id = eh.EducationId and eh.RecordId = rc.Id) as EducationName, " +
                   "    (select top 1 cp.Name from cat_PoliticLevel cp where cp.Id = rc.PoliticLevelId) as PoliticLevelName, " +
                   "    (select top 1 cl.Name from cat_LanguageLevel cl where cl.Id = rc.LanguageLevelId) as LanguageLevelName, " +
                   "    (select top 1 ci.Name from cat_ITLevel ci where ci.Id = rc.ITLevelId) as ITLevelName, " +
                   "    (select top 1 cm.Name from cat_ManagementLevel cm where cm.Id = rc.ManagementLevelId) as ManagementLevelName, " +
                   "	(select top 1 c.Name from cat_PlanJobTitle c where c.Id = bh.PlanJobTitleId) as PlanJobTitleName, " +
                   "	(select top 1 c.Name from cat_PlanPhase c where c.Id = bh.PlanPhaseId) as PlanPhaseName, " +
                   "    (select top 1 ct.Name from cat_TrainingSystem ct, hr_EducationHistory eh where ct.Id = eh.TrainingSystemId and eh.RecordId = rc.Id and eh.FromDate = (select MAX(eh2.FromDate) from hr_EducationHistory eh2 where eh2.RecordId = rc.Id)) as TrainingSystemName, " +
                   "    case when rc.PositionId > 0 and rc.DepartmentId > 0 then " +
                   "    CONCAT((SELECT TOP 1 cp.NAME FROM cat_position cp WHERE  cp.id = rc.positionid), ', ' ,(SELECT TOP 1 cd.NAME FROM cat_department cd WHERE  cd.id = rc.departmentid)) " +
                   "    when rc.PositionId = 0 then (SELECT TOP 1 cd.NAME FROM cat_department cd WHERE  cd.id = rc.departmentid) " +
                   "    when rc.DepartmentId = 0 then (SELECT TOP 1 cp.NAME FROM cat_position cp WHERE  cp.id = rc.positionid) " +
                   "    else '' end as PositionDepartmentName, " +
                   "	ROW_NUMBER() OVER(ORDER BY bh.EditedDate, bh.CreatedDate) SK " +
                   "	from hr_BusinessHistory bh " +
                   "	left join hr_Record rc on rc.Id = bh.RecordId ";
            sql += " WHERE 1=1 ";
            if (!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN({0})".FormatWith(departments);
            }
            if (!string.IsNullOrEmpty(businessType))
            {
                sql += " AND (bh.BusinessType = N'{0}' OR bh.BusinessType IS NULL ) ".FormatWith(businessType);
            }
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            if (!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND bh.DecisionDate >= '{0}'".FormatWith(fromDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                sql += " AND bh.DecisionDate <= '{0}'".FormatWith(toDate);
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
            sql += " SELECT	 " +
                   " rc.FullName,	 " +
                   " rc.EmployeeCode,	 " +
                   " cp.Name AS PositionName,	 " +
                   " cjt.Name AS JobTitleName,	 " +
                   " rc.VYUJoinedDate,	 " +
                   "     dp.Id AS DepartmentId, " +
                   "     dp.Name AS DepartmentName,  " +
                   " (SELECT TOP 1 hs.InsuranceSalary FROM sal_SalaryDecision hs WHERE hs.RecordId = rc.Id 	 " +
                   " 	AND hs.EffectiveDate = (select MAX(hs2.EffectiveDate) FROM sal_SalaryDecision hs2 WHERE hs2.RecordId = rc.Id))  " +
                   " 	AS InsuranceSalary " +
                   " FROM hr_Record rc	 " +
                   " LEFT JOIN cat_Position cp ON rc.PositionId = cp.Id	 " + // filter position
                   " LEFT JOIN cat_Department dp ON rc.DepartmentId = dp.Id	 " + // filter department
                   " LEFT JOIN cat_JobTitle cjt ON rc.JobTitleId = cjt.Id  " + // filter jobtitle
                   " WHERE rc.VYUJoinedDate IS NOT NULL";
            if(!string.IsNullOrEmpty(department))
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
            if(!string.IsNullOrEmpty(fromDate))
                sql += " AND rc.VYUJoinedDate >= '{0}'".FormatWith(fromDate);
            if(!string.IsNullOrEmpty(toDate))
                sql += " AND rc.VYUJoinedDate <= '{0}'".FormatWith(toDate);
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
            sql += " SELECT " +
                   " rc.FullName, " +
                   " rc.EmployeeCode, " +
                   " rc.ArmyJoinedDate, " +
                   " rc.ArmyLeftDate, " +
                   " rc.CellPhoneNumber, " +
                   " cpl.Name AS PoliticLevelName, " +
                   "     dp.Id AS DepartmentId, " +
                   "     dp.Name AS DepartmentName,  " +
                   " cal.Name AS ArmyLevelName, " +
                   " ce.Name AS EducationName " +
                   " FROM hr_Record rc " +
                   " LEFT JOIN cat_PoliticLevel cpl ON rc.PoliticLevelId = cpl.Id " + // filter politiclevel
                   " LEFT JOIN cat_ArmyLevel cal ON rc.ArmyLevelId = cal.Id " + // filter armylevel
                   " LEFT JOIN cat_Department dp ON rc.DepartmentId = dp.Id	 " + // filter department
                   " LEFT JOIN cat_Education ce ON rc.EducationId = ce.Id " + // filter education
                   " WHERE rc.ArmyJoinedDate IS NOT NULL";
            if(!string.IsNullOrEmpty(department))
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
            if(!string.IsNullOrEmpty(fromDate))
                sql += " AND rc.ArmyJoinedDate >= '{0}'".FormatWith(fromDate);
            if(!string.IsNullOrEmpty(toDate))
                sql += " AND rc.ArmyJoinedDate <= '{0}'".FormatWith(toDate);
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
            sql += " SELECT  " +
                   " hc.ContractNumber, " +
                   " cc.Name AS ContractTypeName, " +
                   " cj.Name AS JobName, " +
                   " hc.ContractDate, " +
                   " hc.EffectiveDate, " +
                   " hc.ContractEndDate, " +
                   " cs.Name AS ContractStatusName " +
                   " FROM hr_Contract hc " +
                   " LEFT JOIN cat_ContractType cc ON cc.Id = hc.ContractTypeId " + // filter contract type
                   " LEFT JOIN cat_ContractStatus cs ON cs.Id = hc.ContractStatusId " + //filter contract status
                   " LEFT JOIN cat_JobTitle cj ON cj.Id = hc.JobId " + // filter jobtitle
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
                var sql = " SELECT	" +
                         " rc.EmployeeCode,	" +
                         " rc.FullName,	" +
                         " cp.Name AS PositionName,	" +
                         " dv.Name AS DepartmentName,	" +
                         " rc.DepartmentId,	" +
                         " hc.ContractDate,	" +
                         " hc.ContractEndDate,	" +
                         " cc.Name AS ContractTypeName	" +
                         " FROM hr_Contract hc	" +
                         " LEFT JOIN hr_Record rc ON rc.Id = hc.RecordId	" +
                         " LEFT JOIN cat_Department dv ON rc.DepartmentId = dv.Id " + // filter department
                         " LEFT JOIN cat_Position cp ON rc.PositionId = cp.Id " + // filter positon
                         " LEFT JOIN cat_ContractType cc ON cc.Id = hc.ContractTypeId	" + // filter contract type
                         " WHERE hc.ContractEndDate IS NOT NULL AND ContractEndDate < GETDATE() ";
            if(!string.IsNullOrEmpty(departments))
                sql += " AND rc.DepartmentId IN ({0}) ".FormatWith(departments);
            if(!string.IsNullOrEmpty(fromDate))
                sql += " AND hc.ContractEndDate >= '{0}'".FormatWith(fromDate);
            if(!string.IsNullOrEmpty(toDate))
                sql += " AND hc.ContractEndDate <= '{0}'".FormatWith(toDate);
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
            sql += " SELECT " +
                   " rc.EmployeeCode, " +
                   " rc.FullName, " +
                   " cp.Name AS PositionName, " +
                   " dv.Id AS DepartmentId, " +
                   " dv.Name AS DepartmentName, " +
                   " rc.RecruimentDate AS ProbationDate, " +
                   " rc.ParticipationDate AS ProbationEndDate " +
                   " FROM hr_Record rc " +
                   " LEFT JOIN cat_Position cp ON rc.PositionId = cp.Id " + // filter position
                   " LEFT JOIN cat_Department dv ON rc.DepartmentId = dv.Id " + // filter department
                   " WHERE rc.RecruimentDate IS NOT NULL AND rc.RecruimentDate <= GETDATE() AND GETDATE() < rc.ParticipationDate";
            if(!string.IsNullOrEmpty(department))
                sql += " AND rc.DepartmentId IN ({0})".FormatWith(department);
            if(!string.IsNullOrEmpty(fromDate))
                sql += " AND rc.RecruimentDate >= '{0}'".FormatWith(fromDate);
            if(!string.IsNullOrEmpty(toDate))
                sql += " AND rc.RecruimentDate <= '{0}'".FormatWith(toDate);
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
            sql += " SELECT " +
                   " rc.EmployeeCode," +
                   " rc.FullName," +
                   " rc.BirthDate," +
                   " rc.InsuranceNumber," +
                   " cp.Name AS PositionName," +
                   " dv.Id AS DepartmentId," +
                   " dv.Name AS DeparmentName," +
                   " rc.InsuranceIssueDate," +
                   " '0' AS InsuranceAmount," +
                   " '0' AS LaborAmount," +
                   " '0' AS EnterpriseAmount," +
                   " '0' AS TotalAmount" +
                   " FROM hr_Record rc" +
                   " LEFT JOIN cat_Position cp ON cp.Id = rc.PositionId" + // filter position
                   " LEFT JOIN cat_Department dv ON dv.Id = rc.DepartmentId" + // filter department
                   " WHERE rc.InsuranceNumber IS NOT NULL and rc.InsuranceNumber <> ''";
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
                   "SELECT " +
                   " rc.FullName," +
                   " rc.InsuranceNumber," +
                   " (SELECT TOP 1 hs.InsuranceSalary FROM sal_SalaryDecision hs WHERE hs.RecordId = rc.Id " + // filter salaryinsurance
                   "    AND hs.EffectiveDate = (SELECT MAX(hs2.EffectiveDate) FROM sal_SalaryDecision hs2 WHERE hs2.RecordId = rc.Id)) " +
                   " 		AS InsuranceSalary, " +
                   " cp.Name AS PositionName," +
                   " dv.Id AS DepartmentId," +
                   " dv.Name AS DepartmentName," +
                   " '' AS Note," +
                   " CASE WHEN (SELECT TOP 1 Value FROM cat_BasicSalary WHERE AppliedDate < '{0}' ".FormatWith(DateTime.Now.ToString("yyyy-MM-dd")) +
                   "					ORDER BY [AppliedDate] DESC) > 0 THEN (SELECT TOP 1 Value FROM cat_BasicSalary WHERE AppliedDate < '{0}' ".FormatWith(DateTime.Now.ToString("yyyy-MM-dd")) +
                   "					ORDER BY [AppliedDate] DESC) " +
                   " ELSE hs.ContractSalary END AS SalaryBasic, " +
                   //" hs.PositionAllowance," +
                   " 0 AS SeniorityOutOfFrameAllowance," +
                   " 0 AS SeniorityJobAllowance," +
                   " 0 AS SalaryAllowance," +
                   " 0 AS AdditionAllowance," +
                   " hs.DecisionDate AS FromDate," +
                   " hs.EffectiveDate AS ToDate, " +
                   " 0 AS TotalSalaryBasic " +
                   " INTO #tmpA " +
                   " FROM hr_FluctuationInsurance fi" +
                   " LEFT JOIN hr_Record rc ON rc.Id = fi.RecordId" +
                   " LEFT JOIN cat_Position cp ON rc.PositionId = cp.Id" + // fitler position
                   " LEFT JOIN cat_Department dv ON rc.DepartmentId = dv.Id" + // filter department
                   " LEFT JOIN sal_SalaryDecision hs ON rc.Id = hs.RecordId " +  // generate new filter
                   " WHERE hs.EffectiveDate = (SELECT MAX(hs2.EffectiveDate) FROM sal_SalaryDecision hs2 WHERE hs2.RecordId = rc.Id)";
            if(direction == false)
            {
                sql += " AND fi.Type = 0 ";//Tang
            }
            else
            {
                sql += " AND fi.Type = 1 ";//Giam
            }
            if(!string.IsNullOrEmpty(department))
                sql += " AND rc.DepartmentId IN ({0}) ".FormatWith(department);
            if(!string.IsNullOrEmpty(condition))
                sql += " AND {0} ".FormatWith(condition);

            sql += " UPDATE #tmpA " +
                   " SET TotalSalaryBasic = xSalaryBasic " +
                   " FROM #tmpA " +
                   " INNER JOIN ( " +
                   " SELECT SUM(SalaryBasic) AS xSalaryBasic " +
                   " FROM #tmpA " +
                   " )#tmp ON 1 = 1 " +
                   " SELECT * FROM #tmpA ";
            if(!string.IsNullOrEmpty(fromDate))
            {
                sql += "hs.EffectiveDate >= '{0}'".FormatWith(fromDate);
            }
            if(!string.IsNullOrEmpty(toDate))
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
            sql += " SELECT " +
                   " rc.EmployeeCode," +
                   " rc.FullName ," +
                   " rc.BirthDate," +
                   " dp.Id AS DepartmentId," +
                   " dp.Name AS DepartmentName," +
                   " cp.Name AS PositionName," +
                   " cjt.Name AS JobTitleName," +
                   " ci.Name AS Occupation," +
                   " rc.ParticipationDate AS WorkDate" +
                   " FROM hr_Record rc" +
                   " LEFT JOIN cat_Department dp ON rc.DepartmentId = dp.Id" + //filter department
                   " LEFT JOIN cat_Position cp ON rc.PositionId =cp.Id" + // filter position
                   " LEFT JOIN cat_JobTitle cjt ON rc.JobTitleId = cjt.Id" + // filter jobtitle
                   " LEFT JOIN cat_Industry ci ON rc.IndustryId = ci.Id" + // generate new filter
                   " WHERE 1 = 1 ";

            if(!string.IsNullOrEmpty(department))
                sql += " AND rc.DepartmentId IN ({0}) ".FormatWith(department);
            if(!string.IsNullOrEmpty(fromDate))
            {
                sql += " AND rc.ParticipationDate >= '{0}'".FormatWith(fromDate);
            }
            if(!string.IsNullOrEmpty(toDate))
            {
                sql += " AND rc.ParticipationDate <= '{0}'".FormatWith(toDate);
            }
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
            sql += " SELECT " +
                   " rc.EmployeeCode," +
                   " rc.FullName ," +
                   " rc.BirthDate," +
                   " dv.Id AS DepartmentId," +
                   " dv.Name AS DepartmentName," +
                   " cp.Name AS PositionName," +
                   " rc.ParticipationDate AS StartDate," +
                   " '' AS Note" +
                   " FROM hr_Record rc" +
                   " LEFT JOIN cat_Department dv ON dv.Id = rc.DepartmentId" + //filer department
                   " LEFT JOIN cat_Position cp ON cp.Id = rc.PositionId" + //filter position
                   " WHERE rc.Sex = 0" +
                   " AND rc.RecruimentDate IS NOT NULL";

            if(!string.IsNullOrEmpty(department))
                sql += " AND rc.DepartmentId IN ({0}) ".FormatWith(department);
            if(!string.IsNullOrEmpty(condition))
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
        public static string GetStore_ChildernDayGift(string department, string fromDate, string toDate, string condition)
        {
            var sql = string.Empty;
            sql += " SELECT " +
                   " rc.EmployeeCode," +
                   " rc.FullName AS ParentName," +
                   " fr.FullName AS ChildName," +
                   " dv.Id AS DepartmentId," +
                   " dv.Name AS DepartmentName," +
                   " CASE WHEN fr.Sex = 1 THEN N'Nam' ELSE N'Nữ' END ChildSexName," +
                   " fr.BirthYear," +
                   " YEAR(GETDATE()) - fr.BirthYear AS Age, " +
                   " rc.Address," +
                   " rc.CellPhoneNumber" +
                   " FROM hr_Record rc" +
                   " LEFT JOIN hr_FamilyRelationship fr ON fr.RecordId = rc.Id" + // generate new filter
                   " LEFT JOIN cat_Department dv ON dv.Id = rc.DepartmentId" + // filter department
                   " LEFT JOIN cat_Relationship cr ON cr.Id = fr.RelationshipId" +
                   " LEFT JOIN cat_WorkStatus cw ON cw.Id = rc.WorkStatusId" + // generate new filter
                   " WHERE YEAR(getDate()) - fr.BirthYear <= {0}".FormatWith(Age) +
                   " AND cr.Name LIKE N'{0}'".FormatWith(RelationName) +
                   " AND cw.Name LIKE N'{0}'".FormatWith(WorkStatusWorking);

            if(!string.IsNullOrEmpty(department))
                sql += " AND rc.DepartmentId IN ({0}) ".FormatWith(department);
            if(!string.IsNullOrEmpty(condition))
                sql += " AND {0}".FormatWith(condition);

            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_SoLuongChatLuongCBCCCapHuyen(string departments, string condition)
        {
            var sql = string.Empty;
            // Lay so luong CCVC
            sql += "IF OBJECT_ID('tempdb..#tmpTongSo') IS NOT NULL DROP Table #tmpTongSo " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmpTongSo " +
                    "FROM hr_Record h " +
                    "WHERE h.DepartmentId != 0 " +
                    "GROUP BY h.DepartmentId ";
            // Lay so luong nu trong tung don vi
            sql += "IF OBJECT_ID('tempdb..#tmpNu') IS NOT NULL DROP Table #tmpNu " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmpNu " +
                    "FROM hr_Record h " +
                    "WHERE h.Sex='0' " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC dang vien
            sql += "IF OBJECT_ID('tempdb..#tmpDangVien') IS NOT NULL DROP Table #tmpDangVien " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmpDangVien " +
                    "FROM hr_Record h " +
                    "WHERE h.CPVJoinedDate IS NOT NULL " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC la dan toc thieu so
            sql += "IF OBJECT_ID('tempdb..#tmpDanTocThieuSo') IS NOT NULL DROP Table #tmpDanTocThieuSo " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmpDanTocThieuSo " +
                    "FROM hr_Record h " +
                        "LEFT JOIN cat_Folk dt ON dt.Id = h.FolkId " +
                    "WHERE " +
                        "dt.IsMinority = 1 " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC co ton giao
            sql += "IF OBJECT_ID('tempdb..#tmpTonGiao') IS NOT NULL DROP Table #tmpTonGiao " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmpTonGiao " +
                    "FROM hr_Record h " +
                    "WHERE h.ReligionId IS NOT NULL " +
                    "   AND h.ReligionId != 7 " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC theo chuyen mon (tien si)
            sql += "IF OBJECT_ID('tempdb..#tmpTienSi') IS NOT NULL DROP Table #tmpTienSi " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmpTienSi " +
                    "FROM hr_Record h LEFT JOIN cat_Education dt ON dt.Id = h.EducationId " +
                    "WHERE dt.[Group] = 'TS' " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC theo chuyen mon (thac si)
            sql += "IF OBJECT_ID('tempdb..#tmpThacSi') IS NOT NULL DROP Table #tmpThacSi " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmpThacSi " +
                    "FROM hr_Record h LEFT JOIN cat_Education dt ON dt.Id = h.EducationId " +
                    "WHERE dt.[Group] = 'ThS' " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC theo chuyen mon (dai hoc)
            sql += "IF OBJECT_ID('tempdb..#tmpDaiHoc') IS NOT NULL DROP Table #tmpDaiHoc " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmpDaiHoc " +
                    "FROM hr_Record h LEFT JOIN cat_Education dt ON dt.Id = h.EducationId " +
                    "WHERE dt.[Group] = 'DH' " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC theo chuyen mon (cao dang)
            sql += "IF OBJECT_ID('tempdb..#tmpCaoDang') IS NOT NULL DROP Table #tmpCaoDang " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmpCaoDang " +
                    "FROM hr_Record h LEFT JOIN cat_Education dt ON dt.Id = h.EducationId " +
                    "WHERE dt.[Group] = 'CD' " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC theo chuyen mon (trung cap)
            sql += "IF OBJECT_ID('tempdb..#tmpTrungCap') IS NOT NULL DROP Table #tmpTrungCap " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmpTrungCap " +
                    "FROM hr_Record h LEFT JOIN cat_Education dt ON dt.Id = h.EducationId " +
                    "WHERE dt.[Group] = 'TC' " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC theo chuyen mon con lai (so cap)
            sql += "IF OBJECT_ID('tempdb..#tmpSoCap') IS NOT NULL DROP Table #tmpSoCap " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmpSoCap " +
                    "FROM hr_Record h LEFT JOIN cat_Education dt ON dt.Id = h.EducationId " +
                    "WHERE dt.[Group] = 'SC' " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC theo trinh do cu nhan chinh tri
            sql += "IF OBJECT_ID('tempdb..#tmpCuNhanChinhTri') IS NOT NULL DROP Table #tmpCuNhanChinhTri " +
                    "SELECT h.DepartmentId ,COUNT(h.Id) as 'SoLuong' " +
                    "INTO #tmpCuNhanChinhTri " +
                    "FROM hr_Record h " +
                        "LEFT JOIN cat_PoliticLevel dtc on dtc.Id = h.PoliticLevelId " +
                    "WHERE dtc.[Group] = 'CN' " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC theo trinh do chinh tri cao cap chinh tri
            sql += "IF OBJECT_ID('tempdb..#tmpCaoCapChinhTri') IS NOT NULL DROP Table #tmpCaoCapChinhTri " +
                    "SELECT h.DepartmentId ,COUNT(h.Id) as 'SoLuong' " +
                    "INTO #tmpCaoCapChinhTri " +
                    "FROM hr_Record h " +
                        "LEFT JOIN cat_PoliticLevel dtc on dtc.Id = h.PoliticLevelId " +
                    "WHERE dtc.[Group] = 'CC' " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC theo trinh do trung cap
            sql += "IF OBJECT_ID('tempdb..#tmpTrungCapChinhTri') IS NOT NULL DROP Table #tmpTrungCapChinhTri " +
                    "SELECT h.DepartmentId ,COUNT(h.Id) as 'SoLuong' " +
                    "INTO #tmpTrungCapChinhTri " +
                    "FROM hr_Record h " +
                        "LEFT JOIN cat_PoliticLevel dtc on dtc.Id = h.PoliticLevelId " +
                    "WHERE dtc.[Group] = 'TC' " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC theo trinh do so cap
            sql += "IF OBJECT_ID('tempdb..#tmpSoCapChinhTri') IS NOT NULL DROP Table #tmpSoCapChinhTri " +
                    "SELECT h.DepartmentId ,COUNT(h.Id) as 'SoLuong' " +
                    "INTO #tmpSoCapChinhTri " +
                    "FROM hr_Record h " +
                        "LEFT JOIN cat_PoliticLevel dtc on dtc.Id = h.PoliticLevelId " +
                    "WHERE dtc.[Group] = 'SC' " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC co trinh do trung cap tin hoc tro len
            sql += "IF OBJECT_ID('tempdb..#tmpTrungCapTinHoc') IS NOT NULL DROP Table #tmpTrungCapTinHoc " +
                    "SELECT h.DepartmentId ,COUNT(h.Id) as 'SoLuong' " +
                    "INTO #tmpTrungCapTinHoc " +
                    "FROM hr_Record h " +
                        "LEFT JOIN cat_ITLevel dth on dth.Id = h.ITLevelId " +
                    "WHERE dth.[Group] = 'TC' " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC co chung chi tin hoc
            sql += "IF OBJECT_ID('tempdb..#tmpChungChiTinHoc') IS NOT NULL DROP Table #tmpChungChiTinHoc " +
                    "SELECT h.DepartmentId ,COUNT(h.Id) as 'SoLuong' " +
                    "INTO #tmpChungChiTinHoc " +
                    "FROM hr_Record h " +
                        "LEFT JOIN cat_ITLevel dth on dth.Id = h.ITLevelId " +
                    "WHERE dth.[Group] = 'CC' " +
                    "GROUP BY h.DepartmentId ";
            //lay so luong CCVC co trinh do tieng Anh dai hoc tro len
            sql += "IF OBJECT_ID('tempdb..#tmpDaiHocTiengAnh') IS NOT NULL DROP Table #tmpDaiHocTiengAnh " +
                    "SELECT DISTINCT tmp.DepartmentId,  " +
                    "COUNT(tmp.LanguageLevelId) AS 'SoLuong' " +
                    "INTO #tmpDaiHocTiengAnh " +
                    "FROM ( " +
                        "SELECT h.Id, h.DepartmentId, h.LanguageLevelId " +
                        "FROM hr_Record h " +
                        "LEFT JOIN cat_LanguageLevel dnn " +
                            "ON dnn.Id = h.LanguageLevelId " +
                        "UNION " +
                        "SELECT h.Id, h.DepartmentId, hn.LanguageId " +
                        "FROM hr_Record h  " +
                        "LEFT JOIN hr_Language hn " +
                            "on h.Id = hn.RecordId) AS tmp " +
                    "LEFT JOIN cat_LanguageLevel nn " +
                        "ON nn.Id = tmp.LanguageLevelId " +
                    "WHERE tmp.LanguageLevelId != 0 " +
                        "AND nn.[Group] = 'DHTA' " +
                    "GROUP BY tmp.DepartmentId ";

            //lay so luong CCVC co chung chi tieng Anh
            sql += "IF OBJECT_ID('tempdb..#tmpChungChiTiengAnh') IS NOT NULL DROP Table #tmpChungChiTiengAnh " +
                    "	SELECT DISTINCT tmp.DepartmentId,  " +
                    "COUNT(tmp.LanguageLevelId) AS 'SoLuong'	 " +
                    "   INTO #tmpChungChiTiengAnh " +
                    "FROM ( " +
                    "SELECT H.Id, H.DepartmentId, H.LanguageLevelId " +
                    "FROM hr_Record H	 " +
                    "LEFT JOIN cat_LanguageLevel DNN " +
                    "	ON DNN.Id = H.LanguageLevelId " +
                    "UNION " +
                    "SELECT h.Id, h.DepartmentId, hn.LanguageId	 " +
                    "FROM hr_Record h  " +
                    "LEFT JOIN hr_Language hn " +
                    "	on h.Id = hn.RecordId) AS tmp " +
                    "LEFT JOIN cat_LanguageLevel NN " +
                    "	ON NN.Id = tmp.LanguageLevelId " +
                    "WHERE tmp.LanguageLevelId != 0 " +
                    "	AND NN.[Group] = 'CCTA' " +
                    "GROUP BY tmp.DepartmentId ";
            //lay so luong CCVC co trinh do tieng Anh dai hoc ngoai ngu khac
            sql += "IF OBJECT_ID('tempdb..#tmpDaiHocNgoaiNguKhac') IS NOT NULL DROP Table #tmpDaiHocNgoaiNguKhac " +
                    "SELECT DISTINCT tmp.DepartmentId,  " +
                    "COUNT(tmp.LanguageLevelId) AS 'SoLuong'	 " +
                    "INTO #tmpDaiHocNgoaiNguKhac " +
                    "FROM ( " +
                    "SELECT H.Id, H.DepartmentId, H.LanguageLevelId " +
                    "FROM hr_Record H	 " +
                    "LEFT JOIN cat_LanguageLevel DNN " +
                        "ON DNN.Id = H.LanguageLevelId " +
                    "UNION " +
                    "SELECT h.Id, h.DepartmentId, hn.LanguageId	 " +
                    "FROM hr_Record h  " +
                    "LEFT JOIN hr_Language hn " +
                        "on h.Id = hn.RecordId) AS tmp " +
                    "LEFT JOIN cat_LanguageLevel NN " +
                        "ON NN.Id = tmp.LanguageLevelId " +
                    "WHERE tmp.LanguageLevelId != 0 " +
                        "AND NN.[Group] = 'DHNNK' " +
                    "GROUP BY tmp.DepartmentId ";
            //lay so luong CCVC co chung chi ngoai ngu khac
            sql += "IF OBJECT_ID('tempdb..#tmpChungChiNgoaiNguKhac') IS NOT NULL DROP Table #tmpChungChiNgoaiNguKhac " +
                    "SELECT DISTINCT tmp.DepartmentId,  " +
                    "COUNT(tmp.LanguageLevelId) AS 'SoLuong'	 " +
                    "INTO #tmpChungChiNgoaiNguKhac " +
                    "FROM ( " +
                    "SELECT H.Id, H.DepartmentId, H.LanguageLevelId " +
                    "FROM hr_Record H	 " +
                    "LEFT JOIN cat_LanguageLevel DNN " +
                        "ON DNN.Id = H.LanguageLevelId " +
                    "UNION " +
                    "SELECT h.Id, h.DepartmentId, hn.LanguageId	 " +
                    "FROM hr_Record h  " +
                    "LEFT JOIN hr_Language hn " +
                        "on h.Id = hn.RecordId) AS tmp " +
                    "LEFT JOIN cat_LanguageLevel NN " +
                        "ON NN.Id = tmp.LanguageLevelId " +
                    "WHERE tmp.LanguageLevelId != 0 " +
                        "AND NN.[Group] = 'CCNNK' " +
                    "GROUP BY tmp.DepartmentId ";
            //Lay so luong CCVC co chung chi tieng dan toc
            sql += "IF OBJECT_ID('tempdb..#tmpChungChiTiengDanToc') IS NOT NULL DROP Table #tmpChungChiTiengDanToc " +
                    "SELECT DISTINCT tmp.DepartmentId,  " +
                    "COUNT(tmp.LanguageLevelId) AS 'SoLuong' " +
                    "INTO #tmpChungChiTiengDanToc " +
                    "FROM ( " +
                    "SELECT H.Id, H.DepartmentId, H.LanguageLevelId " +
                    "FROM hr_Record H	 " +
                    "LEFT JOIN cat_LanguageLevel DNN " +
                        "ON DNN.Id = H.LanguageLevelId " +
                    "UNION " +
                    "SELECT h.Id, h.DepartmentId, hn.LanguageId	 " +
                    "FROM hr_Record h  " +
                    "LEFT JOIN hr_Language hn " +
                        "on h.Id = hn.RecordId) AS tmp " +
                    "LEFT JOIN cat_LanguageLevel NN " +
                        "ON NN.Id = tmp.LanguageLevelId " +
                    "WHERE tmp.LanguageLevelId != 0 " +
                        "AND NN.[Group] = 'CCTDT' " +
                    "GROUP BY tmp.DepartmentId ";
            //Lay so luong CCVC QLNN Chuyen vien cao cap
            sql += "IF OBJECT_ID('tempdb..#tmpChuyenVienCaoCap') IS NOT NULL DROP Table #tmpChuyenVienCaoCap " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmpChuyenVienCaoCap " +
                    "FROM hr_Record h " +
                        "LEFT JOIN cat_ManagementLevel dtq ON dtq.Id = h.ManagementLevelId " +
                    "WHERE dtq.[Group] = 'CVCC' " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC QLNN Chuyen vien chinh
            sql += "IF OBJECT_ID('tempdb..#tmpChuyenVienChinh') IS NOT NULL DROP Table #tmpChuyenVienChinh " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmpChuyenVienChinh " +
                    "FROM hr_Record h " +
                        "LEFT JOIN cat_ManagementLevel dtq ON dtq.Id = h.ManagementLevelId " +
                    "WHERE dtq.[Group] = 'CVC' " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC QLNN Chuyen vien
            sql += "IF OBJECT_ID('tempdb..#tmpChuyenVien') IS NOT NULL DROP Table #tmpChuyenVien " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmpChuyenVien " +
                    "FROM hr_Record h " +
                        "LEFT JOIN cat_ManagementLevel dtq ON dtq.Id = h.ManagementLevelId " +
                    "WHERE dtq.[Group] = 'CV' " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC QLNN Chua qua dao tao
            sql += "IF OBJECT_ID('tempdb..#tmpChuaQuaDaoTao') IS NOT NULL DROP Table #tmpChuaQuaDaoTao " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmpChuaQuaDaoTao " +
                    "FROM hr_Record h " +
                        "LEFT JOIN cat_ManagementLevel dtq ON dtq.Id = h.ManagementLevelId " +
                    "WHERE dtq.[Group] = 'CQDT' " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC theo do tuoi duoi 30
            sql += "IF OBJECT_ID('tempdb..#tmpDuoi30') IS NOT NULL DROP Table #tmpDuoi30 " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmpDuoi30 " +
                    "FROM hr_Record h " +
                    "WHERE h.BirthDate IS NOT NULL " +
                        "AND DATEDIFF(YEAR, h.BirthDate, GETDATE())<= 30 " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC theo do tuoi 31 - 40
            sql += "IF OBJECT_ID('tempdb..#tmp31Den40') IS NOT NULL DROP Table #tmp31Den40 " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmp31Den40 " +
                    "FROM hr_Record h " +
                    "WHERE h.BirthDate IS NOT NULL " +
                        "AND DATEDIFF(YEAR, h.BirthDate, GETDATE())> 30 " +
                        "AND DATEDIFF(YEAR, h.BirthDate, GETDATE())<= 40 " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC theo do tuoi 41 - 50
            sql += "IF OBJECT_ID('tempdb..#tmp41Den50') IS NOT NULL DROP Table #tmp41Den50 " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmp41Den50 " +
                    "FROM hr_Record h " +
                    "WHERE h.BirthDate IS NOT NULL " +
                        "AND DATEDIFF(YEAR, h.BirthDate, GETDATE())> 40 " +
                        "AND DATEDIFF(YEAR, h.BirthDate, GETDATE())<= 50 " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC tu 51 - 60
            sql += "IF OBJECT_ID('tempdb..#tmp51Den60') IS NOT NULL DROP Table #tmp51Den60 " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmp51Den60 " +
                    "FROM hr_Record h " +
                    "WHERE h.BirthDate IS NOT NULL " +
                        "AND " +
                            "((DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 50 AND" +
                                " DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 55 AND" +
                                " h.Sex = '0') " +
                        "OR " +
                            "(DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 55 " +
                            "AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 60 " +
                            "AND h.Sex = '1')) " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC theo do tuoi 51 - 55(nu)
            sql += "IF OBJECT_ID('tempdb..#tmpNu51Den55') IS NOT NULL DROP Table #tmpNu51Den55 " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmpNu51Den55 " +
                    "FROM hr_Record h " +
                    "WHERE h.BirthDate IS NOT NULL " +
                        "AND DATEDIFF(YEAR, h.BirthDate, GETDATE())> 50 " +
                        "AND DATEDIFF(YEAR, h.BirthDate, GETDATE())<= 55 " +
                        "AND h.Sex = '0' " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC theo do tuoi 55 - 60(nam)
            sql += "IF OBJECT_ID('tempdb..#tmpNam56Den60') IS NOT NULL DROP Table #tmpNam56Den60 " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmpNam56Den60 " +
                    "FROM hr_Record h " +
                    "WHERE h.BirthDate IS NOT NULL " +
                        "AND DATEDIFF(YEAR, h.BirthDate, GETDATE())> 55 " +
                        "AND DATEDIFF(YEAR, h.BirthDate, GETDATE())<= 60 " +
                        "AND h.Sex = '1' " +
                    "GROUP BY h.DepartmentId ";
            //Lay so luong CCVC tren tuoi nghi huu
            sql += "IF OBJECT_ID('tempdb..#tmpTrenTuoiNghiHuu') IS NOT NULL DROP Table #tmpTrenTuoiNghiHuu " +
                    "SELECT h.DepartmentId, COUNT(h.Id) AS 'SoLuong' " +
                    "INTO #tmpTrenTuoiNghiHuu " +
                    "FROM hr_Record h " +
                    " WHERE h.BirthDate IS NOT NULL " +
                        "AND " +
                            "((DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 55 AND h.Sex = '0') " +
                        "OR " +
                            "(DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 60 AND h.Sex = '1')) " +
                    "GROUP BY h.DepartmentId ";
            //Ngach CC
            //Chuyen vien cao cap & TD
            sql += "IF OBJECT_ID('tempdb..#tempNgach1') IS NOT NULL DROP Table #tempNgach1 " +
                    "SELECT h.DepartmentId, COUNT(*) AS 'SoLuong' " +
                    "INTO #tempNgach1 " +
                    "FROM hr_Record AS h " +
                    "   LEFT JOIN (SELECT RecordId, QuantumId FROM sal_SalaryDecision GROUP BY RecordId, QuantumId) s" +
                    "       ON h.Id = s.RecordId" +
                    " WHERE s.QuantumId IN (SELECT dn.Id " +
                    "FROM cat_Quantum dn,"+
                   "cat_GroupQuantum nn " +
                   " WHERE dn.GroupQuantumId = nn.Id " +
                   "AND nn.[Group] = 'CVCC') " +
                    "GROUP BY h.DepartmentId ";

            //Chuyen vien chinh & TD
            sql += "IF OBJECT_ID('tempdb..#tempNgach2') IS NOT NULL DROP Table #tempNgach2 " +
                    "SELECT h.DepartmentId, COUNT(*) AS 'SoLuong' " +
                    "INTO #tempNgach2 " +
                    "FROM hr_Record AS h " +
                   "   LEFT JOIN (SELECT RecordId, QuantumId FROM sal_SalaryDecision GROUP BY RecordId, QuantumId) s" +
                   "       ON h.Id = s.RecordId" +
                    " WHERE s.QuantumId IN (SELECT dn.Id " +
                    "FROM cat_Quantum dn," +
                       "cat_GroupQuantum nn " +
                    "WHERE dn.GroupQuantumId = nn.Id " +
                       "AND nn.[Group] = 'CVC') " +
                    "GROUP BY h.DepartmentId ";

            //Chuyen vien & TD
            sql += "IF OBJECT_ID('tempdb..#tempNgach3') IS NOT NULL DROP Table #tempNgach3 " +
                    "SELECT h.DepartmentId, COUNT(*) AS 'SoLuong' " +
                    "INTO #tempNgach3 " +
                    "FROM hr_Record AS h " +
                   "   LEFT JOIN (SELECT RecordId, QuantumId FROM sal_SalaryDecision GROUP BY RecordId, QuantumId) s" +
                   "       ON h.Id = s.RecordId" +
                    " WHERE s.QuantumId IN (SELECT dn.Id " +
                    "FROM cat_Quantum dn," +
                       "cat_GroupQuantum nn " +
                    "WHERE dn.GroupQuantumId = nn.Id " +
                       "AND nn.[Group] = 'CV') " +
                    "GROUP BY h.DepartmentId ";

            //Can su & TD
            sql += "IF OBJECT_ID('tempdb..#tempNgach4') IS NOT NULL DROP Table #tempNgach4 " +
                    "SELECT h.DepartmentId, COUNT(*) AS 'SoLuong' " +
                    "INTO #tempNgach4 " +
                    "FROM hr_Record AS h " +
                   "   LEFT JOIN (SELECT RecordId, QuantumId FROM sal_SalaryDecision GROUP BY RecordId, QuantumId) s" +
                   "       ON h.Id = s.RecordId" +
                    " WHERE s.QuantumId IN (SELECT dn.Id " +
                    "FROM cat_Quantum dn," +
                       "cat_GroupQuantum nn " +
                    "WHERE dn.GroupQuantumId = nn.Id " +
                       "AND nn.[Group] = 'CS') " +
                    "GROUP BY h.DepartmentId ";

            //Nhan vien
            sql += "IF OBJECT_ID('tempdb..#tempNgach5') IS NOT NULL DROP Table #tempNgach5 " +
                    "SELECT h.DepartmentId, COUNT(*) AS 'SoLuong' " +
                    "INTO #tempNgach5 " +
                    "FROM hr_Record AS h " +
                   "   LEFT JOIN (SELECT RecordId, QuantumId FROM sal_SalaryDecision GROUP BY RecordId, QuantumId) s" +
                   "       ON h.Id = s.RecordId" +
                    " WHERE s.QuantumId IN (SELECT dn.Id " +
                    "FROM cat_Quantum dn," +
                       "cat_GroupQuantum nn " +
                    "WHERE dn.GroupQuantumId = nn.Id " +
                       "AND nn.[Group] = 'NV') " +
                    "GROUP BY h.DepartmentId ";
            // Lay danh sach co quan to chuc truc thuoc
            sql += "IF OBJECT_ID('tempdb..#tmpA') IS NOT NULL DROP Table #tmpA " +
                   "SELECT dd.Id AS 'DepartmentId', dd.Name " +
                   "INTO #tmpA " +
                   "FROM cat_Department dd ";
            // Xoa bang #tmpB neu da ton tai
            sql += "IF OBJECT_ID('tempdb..#tmpB') IS NOT NULL DROP Table #tmpB ";
            // Tong hop
            sql += "SELECT " +
                   "'' AS 'STT', " +
                   "#tmpA.DepartmentId, " +
                   "#tmpA.Name, " +
                   // Tong so
                   "#tmpTongSo.SoLuong AS 'TongSo', " +
                   // Trong do
                   "#tmpNu.SoLuong AS 'Nu', " +
                   "#tmpDangVien.SoLuong AS 'DangVien', " +
                   "#tmpDanTocThieuSo.SoLuong AS 'DanTocThieuSo', " +
                   "#tmpTonGiao.SoLuong AS 'TonGiao', " +
                   // Trinh do chuyen mon
                   "#tmpTienSi.SoLuong AS 'TienSi', " +
                   "#tmpThacSi.SoLuong AS 'ThacSi', " +
                   "#tmpDaiHoc.SoLuong AS 'DaiHoc', " +
                   "#tmpCaoDang.SoLuong AS 'CaoDang', " +
                   "#tmpTrungCap.SoLuong AS 'TrungCap', " +
                   "#tmpSoCap.SoLuong AS 'SoCap', " +
                   // Trinh do chinh tri
                   "#tmpCuNhanChinhTri.SoLuong as 'CuNhanChinhTri', " +
                   "#tmpCaoCapChinhTri.SoLuong as 'CaoCapChinhTri', " +
                   "#tmpTrungCapChinhTri.SoLuong as 'TrungCapChinhTri', " +
                   "#tmpSoCapChinhTri.SoLuong as 'SoCapChinhTri'," +
                   // Trinh do tin hoc
                   "#tmpTrungCapTinHoc.SoLuong as 'TrungCapTinHoc', " +
                   "#tmpChungChiTinHoc.SoLuong as 'ChungChiTinHoc', " +
                   // Trinh do tieng Anh
                   "#tmpDaiHocTiengAnh.SoLuong as 'DaiHocTiengAnh', " +
                   "#tmpChungChiTiengAnh.SoLuong as 'ChungChiTiengAnh', " +
                   // Trinh do ngoai ngu khac
                   "#tmpDaiHocNgoaiNguKhac.SoLuong as 'DaiHocNgoaiNguKhac', " +
                   "#tmpChungChiNgoaiNguKhac.SoLuong as 'ChungChiNgoaiNguKhac', " +
                   //Chung chi tieng dan toc
                   "#tmpChungChiTiengDanToc.SoLuong as 'ChungChiTiengDanToc', " +
                   // Cap bac QLNN
                   "#tmpChuyenVienCaoCap.SoLuong AS 'ChuyenVienCaoCap', " +
                   "#tmpChuyenVienChinh.SoLuong AS 'ChuyenVienChinh', " +
                   "#tmpChuyenVien.SoLuong AS 'ChuyenVien', " +
                   "#tmpChuaQuaDaoTao.SoLuong AS 'ChuaQuaDaoTao', " +
                   // Do tuoi
                   "#tmpDuoi30.SoLuong AS 'Duoi30'," +
                   "#tmp31Den40.SoLuong AS 'Tu31Den40', " +
                   "#tmp41Den50.SoLuong AS 'Tu41Den50', " +
                   "#tmp51Den60.SoLuong AS 'Tu51Den60', " +
                   "#tmpNu51Den55.SoLuong AS 'Nu51Den55', " +
                   "#tmpNam56Den60.SoLuong AS 'Nam56Den60', " +
                   "#tmpTrenTuoiNghiHuu.SoLuong AS 'TrenTuoiNghiHuu', " +
                   // Ngach CCVC
                   "#tempNgach1.SoLuong AS 'NgachCVCC', " +
                   "#tempNgach2.SoLuong AS 'NgachCVC', " +
                   "#tempNgach3.SoLuong AS 'NgachCV', " +
                   "#tempNgach4.SoLuong AS 'NgachCS', " +
                   "#tempNgach5.SoLuong AS 'NgachNV', " +
                   // Tong hop
                   "0 AS 'xTongSo', " +
                   // Trong do
                   "0 AS 'xNu', " +
                   "0 AS 'xDangVien', " +
                   "0 AS 'xDanTocThieuSo', " +
                   "0 AS 'xTonGiao', " +
                   // Trinh do chuyen mon
                   "0 AS 'xTienSi', " +
                   "0 AS 'xThacSi', " +
                   "0 AS 'xDaiHoc', " +
                   "0 AS 'xCaoDang'," +
                   "0 AS 'xTrungCap', " +
                   "0 AS 'xSoCap', " +
                   // Trinh do chinh tri
                   "0 as 'xCuNhanChinhTri', " +
                   "0 as 'xCaoCapChinhTri', " +
                   "0 as 'xTrungCapChinhTri', " +
                   "0 as 'xSoCapChinhTri', " +
                   // Trinh do tin hoc
                   "0 as 'xTrungCapTinHoc', " +
                   "0 as 'xChungChiTinHoc', " +
                   // Trinh do tieng Anh
                   "0 as 'xDaiHocTiengAnh', " +
                   "0 as 'xChungChiTiengAnh', " +
                   // Trinh do ngoai ngu khac
                   "0 as 'xDaiHocNgoaiNguKhac', " +
                   "0 as 'xChungChiNgoaiNguKhac', " +
                   //Chung chi tieng dan toc
                   "0 as 'xChungChiTiengDanToc', " +
                   // Cap bac QLNN
                   "0 AS 'xChuyenVienCaoCap', " +
                   "0 AS 'xChuyenVienChinh', " +
                   "0 AS 'xChuyenVien', " +
                   "0 AS 'xChuaQuaDaoTao', " +
                   // Do tuoi
                   "0 AS 'xDuoi30', " +
                   "0 AS 'x31Den40', " +
                   "0 AS 'x41Den50', " +
                   "0 AS 'x51Den60', " +
                   "0 AS 'xNu51Den55', " +
                   "0 AS 'xNam56Den60', " +
                   "0 AS 'xTrenTuoiNghiHuu', " +
                   // Ngach CCVC
                   "0 AS 'xNgachCVCC', " +
                   "0 AS 'xNgachCVC', " +
                   "0 AS 'xNgachCV', " +
                   "0 AS 'xNgachCS', " +
                   "0 AS 'xNgachNV' " +
                   // Day du lieu vao bang #tmpB
                   "INTO #tmpB " +
                   //
                   // join table
                   //
                   "FROM #tmpA " +
                   // Tong so
                   "LEFT JOIN #tmpTongSo ON #tmpTongSo.DepartmentId=#tmpA.DepartmentId " +
                   // Trong do
                   "LEFT JOIN #tmpNu ON #tmpNu.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tmpDangVien ON #tmpDangVien.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tmpDanTocThieuSo ON #tmpDanTocThieuSo.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tmpTonGiao ON #tmpTonGiao.DepartmentId=#tmpA.DepartmentId " +
                   // Trinh do chuyen mon
                   "LEFT JOIN #tmpTienSi ON #tmpTienSi.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tmpThacSi ON #tmpThacSi.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tmpDaiHoc ON #tmpDaiHoc.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tmpCaoDang ON #tmpCaoDang.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tmpTrungCap ON #tmpTrungCap.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tmpSoCap ON #tmpSoCap.DepartmentId=#tmpA.DepartmentId " +
                   // Trinh do chinh tri
                   "LEFT JOIN #tmpCuNhanChinhTri ON #tmpCuNhanChinhTri.DepartmentId= #tmpA.DepartmentId " +
                   "LEFT JOIN #tmpCaoCapChinhTri ON #tmpCaoCapChinhTri.DepartmentId= #tmpA.DepartmentId " +
                   "LEFT JOIN #tmpTrungCapChinhTri ON #tmpTrungCapChinhTri.DepartmentId= #tmpA.DepartmentId " +
                   "LEFT JOIN #tmpSoCapChinhTri ON #tmpSoCapChinhTri.DepartmentId= #tmpA.DepartmentId " +
                   "LEFT JOIN #tmpTrungCapTinHoc ON #tmpTrungCapTinHoc.DepartmentId= #tmpA.DepartmentId " +
                   "LEFT JOIN #tmpChungChiTinHoc ON #tmpChungChiTinHoc.DepartmentId= #tmpA.DepartmentId " +
                   // Trinh do tieng anh
                   "LEFT JOIN #tmpDaiHocTiengAnh ON #tmpDaiHocTiengAnh.DepartmentId= #tmpA.DepartmentId " +
                   "LEFT JOIN #tmpChungChiTiengAnh ON #tmpChungChiTiengAnh.DepartmentId= #tmpA.DepartmentId " +
                   // Trinh do ngoai ngu khac
                   "LEFT JOIN #tmpDaiHocNgoaiNguKhac ON #tmpDaiHocNgoaiNguKhac.DepartmentId= #tmpA.DepartmentId " +
                   "LEFT JOIN #tmpChungChiNgoaiNguKhac ON #tmpChungChiNgoaiNguKhac.DepartmentId= #tmpA.DepartmentId " +
                   //Chung chi tieng dan toc
                   "LEFT JOIN #tmpChungChiTiengDanToc ON #tmpChungChiTiengDanToc.DepartmentId = #tmpA.DepartmentId " +
                   // Cap bac QLNN
                   "LEFT JOIN #tmpChuyenVienCaoCap ON #tmpChuyenVienCaoCap.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tmpChuyenVienChinh ON #tmpChuyenVienChinh.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tmpChuyenVien ON #tmpChuyenVien.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tmpChuaQuaDaoTao ON #tmpChuaQuaDaoTao.DepartmentId=#tmpA.DepartmentId " +
                   // Do tuoi
                   "LEFT JOIN #tmpDuoi30 ON #tmpDuoi30.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tmp31Den40 ON #tmp31Den40.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tmp41Den50 ON #tmp41Den50.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tmp51Den60 ON #tmp51Den60.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tmpNu51Den55 ON #tmpNu51Den55.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tmpNam56Den60 ON #tmpNam56Den60.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tmpTrenTuoiNghiHuu ON #tmpTrenTuoiNghiHuu.DepartmentId=#tmpA.DepartmentId " +
                   // Ngach CCVC
                   "LEFT JOIN #tempNgach1 ON #tempNgach1.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tempNgach2 ON #tempNgach2.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tempNgach3 ON #tempNgach3.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tempNgach4 ON #tempNgach4.DepartmentId=#tmpA.DepartmentId " +
                   "LEFT JOIN #tempNgach5 ON #tempNgach5.DepartmentId=#tmpA.DepartmentId " +
                   //
                   // where condition
                   //
                   "WHERE #tmpA.DepartmentId IN (SELECT [Id] FROM [cat_Department] WHERE [Id] IN ({0}) AND [IsLocked]='0') "
                       .FormatWith(departments);
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }

            // Tinh tong
            sql +=  "UPDATE	#tmpB " +
	                "SET " +
                    // Tong so
			        "xTongSo = xwTongSo, " +
                    // Trong do
			        "xNu = xwNu, " +
			        "xDangVien = xwDangVien, " +
			        "xDanTocThieuSo = xwDanTocThieuSo, " +
			        "xTonGiao = xwTonGiao, " +
			        // Tring do chuyen mon
                    "xTienSi = xwTienSi, " +
			        "xThacSi = xwThacSi, " +
			        "xDaiHoc = xwDaiHoc, " +
			        "xCaoDang = xwCaoDang, " +
			        "xTrungCap = xwTrungCap, " +
			        "xSoCap = xwSoCap, " +
			        // Trinh do chinh tri
                    "xCuNhanChinhTri = xwCuNhanChinhTri, " +
			        "xCaoCapChinhTri = xwCaoCapChinhTri, " +
			        "xTrungCapChinhTri = xwTrungCapChinhTri, " +
			        "xSoCapChinhTri = xwSocapChinhTri, " +
			        // Trinh do tin hoc
                    "xTrungCapTinHoc = xwTrungCapTinHoc, " +
			        "xChungChiTinHoc = xwChungChiTinHoc, " +
			        // Trinh do tieng anh
                    "xDaiHocTiengAnh = xwDaiHocTiengAnh, " +
			        "xChungChiTiengAnh = xwChungChiTiengAnh, " +
                    // Trinh do ngoai ngu khac
			        "xDaiHocNgoaiNguKhac = xwDaiHocNgoaiNguKhac, " +
			        "xChungChiNgoaiNguKhac = xwChungChiNgoaiNguKhac, " +
                    //Chung chi tieng dan toc
                    "xChungChiTiengDanToc = xwChungChiTiengDanToc, " +
                    // Trinh do QLNN
			        "xChuyenVienCaoCap = xwChuyenVienCaoCap, " +
                    "xChuyenVienChinh = xwChuyenVienChinh, " +
                    "xChuyenVien = xwChuyenVien, " +
                    "xChuaQuaDaoTao = xwChuaQuaDaoTao, " +
                    // Do tuoi
                    "xDuoi30 = xwDuoi30, " +
                    "x31Den40 = xw31Den40, " +
                    "x41Den50 = xw41Den50, " +
                    "x51Den60 = xw51Den60, " +
                    "xNu51Den55 = xwNu51Den55, " +
                    "xNam56Den60 = xwNam56Den60, " +
                    "xTrenTuoiNghiHuu = xwTrenTuoiNghiHuu, " +
                    // Ngach CCVC
                    "xNgachCVCC = xwNgachCVCC, " +
                    "xNgachCVC = xwNgachCVC, " +
                    "xNgachCV = xwNgachCV, " +
                    "xNgachCS = xwNgachCS, " +
                    "xNgachNV = xwNgachNV " +
                    //
                    // From table
                    //
                    "FROM " +
                    "#tmpB " +
                    //
                    // Join table
                    "INNER JOIN " +
                        "(SELECT " +
                        // Tong so
                        "SUM(CASE WHEN #tmpB.TongSo IS NULL THEN 0 ELSE #tmpB.TongSo END) AS xwTongSo, " +
                        // Trong do
                        "SUM(CASE WHEN #tmpB.Nu IS NULL THEN 0 ELSE #tmpB.Nu END) AS xwNu, " +
                        "SUM(CASE WHEN #tmpB.DangVien IS NULL THEN 0 ELSE #tmpB.DangVien END) AS xwDangVien, " +
                        "SUM(CASE WHEN #tmpB.DanTocThieuSo IS NULL THEN 0 ELSE #tmpB.DanTocThieuSo  END) AS xwDanTocThieuSo, " +
                        "SUM(CASE WHEN #tmpB.TonGiao IS NULL THEN 0 ELSE #tmpB.TonGiao END) AS xwTongiao, " +
                        // Trinh do chuyen mon
                        "SUM(CASE WHEN #tmpB.TienSi IS NULL THEN 0 ELSE #tmpB.TienSi END) AS xwTienSi, " +
                        "SUM(CASE WHEN #tmpB.ThacSi IS NULL THEN 0 ELSE #tmpB.ThacSi END) AS xwThacSi, " +
                        "SUM(CASE WHEN #tmpB.DaiHoc IS NULL THEN 0 ELSE #tmpB.DaiHoc END) AS xwDaiHoc, " +
                        "SUM(CASE WHEN #tmpB.CaoDang IS NULL THEN 0 ELSE #tmpB.CaoDang END) AS xwCaoDang, " +
                        "SUM(CASE WHEN #tmpB.TrungCap IS NULL THEN 0 ELSE #tmpB.TrungCap END ) AS xwTrungCap, " +
                        "SUM(CASE WHEN #tmpB.SoCap IS NULL THEN 0 ELSE  #tmpB.SoCap END) AS xwSoCap, " +
                        // Trinh do chinh tri
                        "SUM(CASE WHEN #tmpB.CuNhanChinhTri IS NULL THEN 0 ELSE #tmpB.CuNhanChinhTri END) AS xwCuNhanChinhTri, " +
                        "SUM(CASE WHEN #tmpB.CaoCapChinhTri IS NULL THEN 0 ELSE #tmpB.CaoCapChinhTri END) AS xwCaoCapChinhTri, " +
                        "SUM(CASE WHEN #tmpB.TrungCapChinhTri IS NULL THEN 0 ELSE #tmpB.TrungCapChinhTri END) AS xwTrungCapChinhTri, " +
                        "SUM(CASE WHEN #tmpB.SoCapChinhTri IS NULL THEN 0 ELSE #tmpB.SoCapChinhTri END) AS xwSoCapChinhTri, " +
                        // Trinh do tin hoc
                        "SUM(CASE WHEN #tmpB.TrungCapTinHoc IS NULL THEN 0 ELSE #tmpB.TrungCapTinHoc END) AS xwTrungCapTinHoc, " +
                        "SUM(CASE WHEN #tmpB.ChungChiTinHoc IS NULL THEN 0 ELSE #tmpB.ChungChiTinHoc END )AS xwChungChiTinHoc, " +
                        // Trinh do tieng anh
                        "SUM(CASE WHEN #tmpB.DaiHocTiengAnh IS NULL THEN 0 ELSE #tmpB.DaiHocTiengAnh END) AS xwDaiHocTiengAnh, " +
                        "SUM(CASE WHEN #tmpB.ChungChiTiengAnh IS NULL THEN 0 ELSE #tmpB.ChungChiTiengAnh END ) AS xwChungChiTiengAnh, " +
                        // Trinh do ngoai ngu khac
                        "SUM(CASE WHEN #tmpB.DaiHocNgoaiNguKhac IS NULL THEN 0 ELSE #tmpB.DaiHocNgoaiNguKhac END) AS xwDaiHocNgoaiNguKhac, " +
                        "SUM(CASE WHEN #tmpB.ChungChiNgoaiNguKhac IS NULL THEN 0 ELSE #tmpB.ChungChiNgoaiNguKhac END) AS xwChungChiNgoaiNguKhac, " +
                        //Chung chi tieng dan toc
                         "SUM(CASE WHEN #tmpB.ChungChiTiengDanToc IS NULL THEN 0 ELSE #tmpB.ChungChiTiengDanToc END) AS xwChungChiTiengDanToc, " +
                        // Trinh do QLNN
                        "SUM(CASE WHEN #tmpB.ChuyenVienCaoCap IS NULL THEN 0 ELSE #tmpB.ChuyenVienCaoCap END) AS xwChuyenVienCaoCap, " +
                        "SUM(CASE WHEN #tmpB.ChuyenVienChinh IS NULL THEN 0 ELSE #tmpB.ChuyenVienChinh  END) AS  xwChuyenVienChinh, " +
                        "SUM(CASE WHEN #tmpB.ChuyenVien IS NULL THEN 0 ELSE #tmpB.ChuyenVien  END) AS xwChuyenVien, " +
                        "SUM(CASE WHEN #tmpB.ChuaQuaDaoTao IS NULL THEN 0 ELSE #tmpB.ChuaQuaDaoTao END) AS xwChuaQuaDaoTao, " +
                        // Do tuoi
                        "SUM(CASE WHEN #tmpB.Duoi30 IS NULL THEN 0 ELSE #tmpB.Duoi30 END) AS xwDuoi30, " +
                        "SUM(CASE WHEN #tmpB.Tu31Den40 IS NULL THEN 0 ELSE #tmpB.Tu31Den40 END) AS xw31Den40, " +
                        "SUM(CASE WHEN #tmpB.Tu41Den50 IS NULL THEN 0 ELSE #tmpB.Tu41Den50 END) AS xw41Den50, " +
                        "SUM(CASE WHEN #tmpB.Tu51Den60 IS NULL THEN 0 ELSE #tmpB.Tu51Den60 END) AS xw51Den60, " +
                        "SUM(CASE WHEN #tmpB.Nu51Den55 IS NULL THEN 0 ELSE #tmpB.Nu51Den55 END) AS xwNu51Den55, " +
                        "SUM(CASE WHEN #tmpB.Nam56Den60 IS NULL THEN 0 ELSE #tmpB.Nam56Den60 END) AS xwNam56Den60, " +
                        "SUM(CASE WHEN #tmpB.TrenTuoiNghiHuu IS NULL THEN 0 ELSE #tmpB.TrenTuoiNghiHuu END) AS xwTrenTuoiNghiHuu, " +
                        // Ngach CCVC
                        "SUM(CASE WHEN #tmpB.NgachCVCC IS NULL THEN 0 ELSE #tmpB.NgachCVCC  END) AS xwNgachCVCC, " +
                        "SUM(CASE WHEN #tmpB.NgachCVC IS NULL THEN 0 ELSE  #tmpB.NgachCVC END) AS xwNgachCVC, " +
                        "SUM(CASE WHEN #tmpB.NgachCV IS NULL THEN 0 ELSE #tmpB.NgachCV END) AS xwNgachCV, " +
                        "SUM(CASE WHEN #tmpB.NgachCS IS NULL THEN 0 ELSE #tmpB.NgachCS  END) AS xwNgachCS, " +
                        "SUM(CASE WHEN #tmpB.NgachNV IS NULL THEN 0 ELSE #tmpB.NgachNV END) AS xwNgachNV " +
                        //
                        // From table
                        //
                        "FROM #tmpB) AS TMP " +
                    "ON 1=1 ";
            // select all from #tmpB
            sql +=  "SELECT * FROM #tmpB ";

            // return
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetStore_SoLuongChatLuongCBCCCapXa(string condition)
        {
            // init sql
            var sql = string.Empty;
            //
            // create sql
            //
            // xoa bang #tmpA neu da ton tai
            sql += "IF OBJECT_ID('tempdb..#tmpB') IS NOT NULL DROP Table #tmpB ";
            // lay du lieu vao bang #tmpB
            sql += "SELECT " +
                   "FullName AS HoTen, " +
                   "(SELECT dd.Name FROM cat_Department dd WHERE dd.Id = H.DepartmentId ) AS 'TenDonVi', " +
                   // Trong do
                   "CASE WHEN Sex = '0' THEN 'X' ELSE '' END AS Nu, " +
                   "CASE WHEN (h.CPVOfficialJoinedDate IS NOT NULL) THEN 'X' ELSE '' END AS DangVien, " +
                   "CASE WHEN (SELECT TOP 1 IsMinority FROM cat_Folk WHERE Id = H.FolkId) = '1' THEN 'X' ELSE '' END AS DanTocThieuSo, " +
                   "CASE WHEN ReligionId IS NOT NULL AND ReligionId != 0 THEN 'X' ELSE '' END AS TonGiao, " +
                   // Chuc danh
                   "CASE WHEN (SELECT TOP 1 Name FROM cat_Position DC WHERE DC.Id = H.PositionId ) IS NOT NULL THEN 'X' ELSE '' END AS CanBoCapXa, " +
                   "CASE WHEN (SELECT TOP 1 Name FROM cat_JobTitle DCV WHERE DCV.Id = H.JobTitleId ) IS NOT NULL THEN 'X' ELSE '' END AS CongChucChuyenMon, " +
                   // Trinh do chuyen mon
                   "CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = H.EducationId) = 'TS' THEN 'X' ELSE '' END AS TienSi, " +
                   "CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = H.EducationId) = 'ThS' THEN 'X' ELSE '' END AS ThacSi, " +
                   "CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = H.EducationId) = 'DH' THEN 'X' ELSE '' END AS DaiHoc, " +
                   "CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = H.EducationId) = 'CD' THEN 'X' ELSE '' END AS CaoDang, " +
                   "CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = H.EducationId) = 'TC' THEN 'X' ELSE '' END AS TrungCap, " +
                   "CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = H.EducationId) = 'SC' THEN 'X' ELSE '' END AS SoCap, " +
                   // Trinh do van hoa
                   "CASE WHEN (SELECT TOP 1 [Group] FROM cat_BasicEducation WHERE Id = H.BasicEducationId) = 'THPT' THEN 'X' ELSE '' END AS THPT, " +
                   "CASE WHEN (SELECT TOP 1 [Group] FROM cat_BasicEducation WHERE Id = H.BasicEducationId) = 'THCS' THEN 'X' ELSE '' END AS THCS, " +
                   "CASE WHEN (SELECT TOP 1 [Group] FROM cat_BasicEducation WHERE Id = H.BasicEducationId) = 'TieuHoc' THEN 'X' ELSE '' END AS TieuHoc, " +
                   // Trinh do chinh tri
                   "CASE WHEN (SELECT TOP 1 Id FROM cat_PoliticLevel pl WHERE pl.Id = H.PoliticLevelId) = '4' THEN 'X' ELSE '' END AS CuNhanChinhTri, " +
                   "CASE WHEN (SELECT TOP 1 Id FROM cat_PoliticLevel pl WHERE pl.Id = H.PoliticLevelId) = '3' THEN 'X' ELSE '' END AS CaoCapChinhTri, " +
                   "CASE WHEN (SELECT TOP 1 Id FROM cat_PoliticLevel pl WHERE pl.Id = H.PoliticLevelId) = '2' THEN 'X' ELSE '' END AS TrungCapChinhTri, " +
                   "CASE WHEN (SELECT TOP 1 Id FROM cat_PoliticLevel pl WHERE pl.Id = H.PoliticLevelId) = '1' THEN 'X' ELSE '' END AS SoCapChinhTri, " +
                   // Trinh do tin hoc
                   "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ITLevel itl WHERE itl.Id = H.ITLevelId) = 'TC' THEN 'X' ELSE '' END AS TrungCapTinHoc, " +
                   "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ITLevel itl WHERE itl.Id = H.ITLevelId) = 'CC' THEN 'X' ELSE '' END AS ChungChiTinHoc, " +
                   // Trinh do tieng anh
                   "CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = H.LanguageLevelId) = 'DHTA' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId  WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'DHTA') = 'DHTA' THEN 'X' ELSE '' END AS DaiHocTiengAnh," +
                   "CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = H.LanguageLevelId) = 'CCTA' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId  WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'CCTA') = 'CCTA' THEN 'X' ELSE '' END AS ChungChiTiengAnh," +
                   // Trinh do ngoai ngu khac
                   "CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = H.LanguageLevelId) = 'DHNNK' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId  WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'DHNNK') = 'DHNNK' THEN 'X' ELSE '' END AS DaiHocNgoaiNguKhac," +
                   "CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = H.LanguageLevelId) = 'CCNNK' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId  WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'CCNNK') = 'CCNNK' THEN 'X' ELSE '' END AS ChungChiNgoaiNguKhac, " +
                   // Trinh do tieng dan toc
                   "CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = H.LanguageLevelId) = 'CCTDT' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId  WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'CCTDT') = 'CCTDT' THEN 'X' ELSE '' END AS ChungChiTiengDanToc," +
                   // Trinh do QLNN
                   "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ManagementLevel ml WHERE ml.Id = H.ManagementLevelId) = 'CVCC' THEN 'X' ELSE '' END AS ChuyenVienCaoCap, " +
                   "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ManagementLevel ml WHERE ml.Id = H.ManagementLevelId) = 'CVC' THEN 'X' ELSE '' END AS ChuyenVienChinh, " +
                   "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ManagementLevel ml WHERE ml.Id = H.ManagementLevelId) = 'CV' THEN 'X' ELSE '' END AS ChuyenVien, " +
                   "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ManagementLevel ml WHERE ml.Id = H.ManagementLevelId) = 'CQDT' THEN 'X' ELSE '' END AS ChuaQuaDaoTao, " +
                   // Do tuoi
                   "CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 30) THEN 'X' ELSE '' END AS Duoi30, " +
                   "CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 30 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 40) THEN 'X' ELSE '' END AS Tu31Den40, " +
                   "CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 40 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 50) THEN 'X' ELSE '' END AS Tu41Den50, " +
                   "CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 50 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 60) THEN 'X' ELSE '' END AS Tu51Den60, " +
                   "CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 50 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 55 AND h.Sex = '0') THEN 'X' ELSE '' END AS Nu51Den55, " +
                   "CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 55 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 60 AND h.Sex = '1') THEN 'X' ELSE '' END AS Nam56Den60, " +
                   "CASE WHEN (h.BirthDate IS NOT NULL AND((DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 55 AND h.Sex = '1') OR " +
                   "(DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 60 AND h.Sex = '1'))) THEN 'X' ELSE '' END AS TrenTuoiNghiHuu, " +
                   // Luan chuyen tu huyen
                   "'' AS LuanChuyenTuHuyen, " +
                   // Tong so
                   "0 AS 'xTongSo', " +
                   // Trong do
                   "0 AS 'xNu', " +
                   "0 AS 'xDangVien', " +
                   "0 AS 'xDanTocThieuSo', " +
                   "0 AS 'xTonGiao', " +
                   // Chuc danh
                   "0 AS 'xCanBoCapXa', " +
                   "0 AS 'xCongChucChuyenMon', " +
                   // Trinh do chuyen mon
                   "0 AS 'xTienSi', " +
                   "0 AS 'xThacSi', " +
                   "0 AS 'xDaiHoc', " +
                   "0 AS 'xCaoDang'," +
                   "0 AS 'xTrungCap', " +
                   "0 AS 'xSoCap', " +
                   // Trinh do van hoa
                   "0 AS 'xTHPT', " +
                   "0 AS 'xTHCS', " +
                   "0 AS 'xTieuHoc', " +
                   // Trinh do chinh tri
                   "0 as 'xCuNhanChinhTri', " +
                   "0 as 'xCaoCapChinhTri', " +
                   "0 as 'xTrungCapChinhTri', " +
                   "0 as 'xSoCapChinhTri', " +
                   // Trinh do tin hoc
                   "0 as 'xTrungCapTinHoc', " +
                   "0 as 'xChungChiTinHoc', " +
                   // Trinh do tieng Anh
                   "0 as 'xDaiHocTiengAnh', " +
                   "0 as 'xChungChiTiengAnh', " +
                   // Trinh do ngoai ngu khac
                   "0 as 'xDaiHocNgoaiNguKhac', " +
                   "0 as 'xChungChiNgoaiNguKhac', " +
                   // Trinh do tieng dan toc
                   "0 AS 'xChungChiTiengDanToc', " +
                   // Cap bac QLNN
                   "0 AS 'xChuyenVienCaoCap', " +
                   "0 AS 'xChuyenVienChinh', " +
                   "0 AS 'xChuyenVien', " +
                   "0 AS 'xChuaQuaDaoTao', " +
                   // Do tuoi
                   "0 AS 'xDuoi30', " +
                   "0 AS 'x31Den40', " +
                   "0 AS 'x41Den50', " +
                   "0 AS 'x51Den60', " +
                   "0 AS 'xNu51Den55', " +
                   "0 AS 'xNam56Den60', " +
                   "0 AS 'xTrenTuoiNghiHuu', " +
                   // Luan chuyen tu huyen
                   "0 AS 'xLuanChuyenTuHuyen' " +
                   "INTO #tmpB " +
                   "FROM hr_Record H " +
                   "WHERE DepartmentId = @MaDonVi ";
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += "ORDER BY FullName ";

            // Tinh tong
            sql +=  "UPDATE	#tmpB " +
                    "SET " +
                        // Tong so
                        "xTongSo = xwTongSo, " +
                        // Trong do
                        "xNu = xwNu, " +
                        "xDangVien = xwDangVien, " +
                        "xDanTocThieuSo = xwDanTocThieuSo, " +
                        "xTonGiao = xwTonGiao, " +
                        // Chuc danh
                        "xCanBoCapXa = xwCanBoCapXa, " +
                        "xCongChucChuyenMon = xwCongChucChuyenMon, " +
                        // Trinh do chuyen mon
                        "xTienSi = xwTienSi, " +
                        "xThacSi = xwThacSi, " +
                        "xDaiHoc = xwDaiHoc, " +
                        "xCaoDang = xwCaoDang, " +
                        "xTrungCap = xwTrungCap, " +
                        "xSoCap = xwSoCap, " +
                        // Trinh do van hoa
                        "xTHPT = xwTHPT, " +
                        "xTHCS = xwTHCS, " +
                        "xTieuHoc = xwTieuHoc, " +
                        // Trinh do chinh tri
                        "xCuNhanChinhTri = xwCuNhanChinhTri, " +
                        "xCaoCapChinhTri = xwCaoCapChinhTri, " +
                        "xTrungCapChinhTri = xwTrungCapChinhTri, " +
                        "xSoCapChinhTri = xwSocapChinhTri, " +
                        // Trinh do tin hoc
                        "xTrungCapTinHoc = xwTrungCapTinHoc, " +
                        "xChungChiTinHoc = xwChungChiTinHoc, " +
                        // Trinh do tieng anh
                        "xDaiHocTiengAnh = xwDaiHocTiengAnh, " +
                        "xChungChiTiengAnh = xwChungChiTiengAnh, " +
                        // Trinh do ngoai ngu khac
                        "xDaiHocNgoaiNguKhac = xwDaiHocNgoaiNguKhac, " +
                        "xChungChiNgoaiNguKhac = xwChungChiNgoaiNguKhac, " +
                        // Trinh do tieng dan toc
                        "xChungChiTiengDanToc = xwChungChiTiengDanToc, " +
                        // Trinh do QLNN
                        "xChuyenVienCaoCap = xwChuyenVienCaoCap, " +
                        "xChuyenVienChinh = xwChuyenVienChinh, " +
                        "xChuyenVien = xwChuyenVien, " +
                        "xChuaQuaDaoTao = xwChuaQuaDaoTao, " +
                        // Do tuoi
                        "xDuoi30 = xwDuoi30, " +
                        "x31Den40 = xw31Den40, " +
                        "x41Den50 = xw41Den50, " +
                        "x51Den60 = xw51Den60, " +
                        "xNu51Den55 = xwNu51Den55, " +
                        "xNam56Den60 = xwNam56Den60, " +
                        "xTrenTuoiNghiHuu = xwTrenTuoiNghiHuu, " +
                        // Luan chuyen tu huyen
                        "xLuanChuyenTuHuyen = xwLuanChuyenTuHuyen " +
                    //
                    // From table
                    //
                    "FROM " +
                    "#tmpB " +
                    //
                    // Join table
                    //
                    "INNER JOIN " +
                        "(SELECT " +
                        // Tong so
                        "SUM(1) AS xwTongSo, " +
                        // Trong do
                        "SUM(CASE WHEN #tmpB.Nu = 'X' THEN 1 ELSE 0 END) AS xwNu, " +
                        "SUM(CASE WHEN #tmpB.DangVien = 'X' THEN 1 ELSE 0 END) AS xwDangVien, " +
                        "SUM(CASE WHEN #tmpB.DanTocThieuSo = 'X' THEN 1 ELSE 0 END) AS xwDanTocThieuSo, " +
                        "SUM(CASE WHEN #tmpB.TonGiao = 'X' THEN 1 ELSE 0 END) AS xwTongiao, " +
                        // Chuc danh
                        "SUM(CASE WHEN #tmpB.CanBoCapXa = 'X' THEN 1 ELSE 0 END) AS xwCanBoCapXa, " +
                        "SUM(CASE WHEN #tmpB.CongChucChuyenMon = 'X' THEN 1 ELSE 0 END) AS xwCongChucChuyenMon, " +
                        // Trinh do chuyen mon
                        "SUM(CASE WHEN #tmpB.TienSi = 'X' THEN 1 ELSE 0 END) AS xwTienSi, " +
                        "SUM(CASE WHEN #tmpB.ThacSi = 'X' THEN 1 ELSE 0 END) AS xwThacSi, " +
                        "SUM(CASE WHEN #tmpB.DaiHoc = 'X' THEN 1 ELSE 0 END) AS xwDaiHoc, " +
                        "SUM(CASE WHEN #tmpB.CaoDang = 'X' THEN 1 ELSE 0 END) AS xwCaoDang, " +
                        "SUM(CASE WHEN #tmpB.TrungCap = 'X' THEN 1 ELSE 0 END) AS xwTrungCap, " +
                        "SUM(CASE WHEN #tmpB.SoCap = 'X' THEN 1 ELSE 0 END) AS xwSoCap, " +
                        // Trinh do van hoa
                        "SUM(CASE WHEN #tmpB.THPT = 'X' THEN 1 ELSE 0 END) AS xwTHPT, " +
                        "SUM(CASE WHEN #tmpB.THCS = 'X' THEN 1 ELSE 0 END) AS xwTHCS, " +
                        "SUM(CASE WHEN #tmpB.TieuHoc = 'X' THEN 1 ELSE 0 END) AS xwTieuHoc, " +
                        // Trinh do chinh tri
                        "SUM(CASE WHEN #tmpB.CuNhanChinhTri = 'X' THEN 1 ELSE 0 END) AS xwCuNhanChinhTri, " +
                        "SUM(CASE WHEN #tmpB.CaoCapChinhTri = 'X' THEN 1 ELSE 0 END) AS xwCaoCapChinhTri, " +
                        "SUM(CASE WHEN #tmpB.TrungCapChinhTri = 'X' THEN 1 ELSE 0 END) AS xwTrungCapChinhTri, " +
                        "SUM(CASE WHEN #tmpB.SoCapChinhTri = 'X' THEN 1 ELSE 0 END) AS xwSoCapChinhTri, " +
                        // Trinh do tin hoc
                        "SUM(CASE WHEN #tmpB.TrungCapTinHoc = 'X' THEN 1 ELSE 0 END) AS xwTrungCapTinHoc, " +
                        "SUM(CASE WHEN #tmpB.ChungChiTinHoc = 'X' THEN 1 ELSE 0 END) AS xwChungChiTinHoc, " +
                        // Trinh do tieng anh
                        "SUM(CASE WHEN #tmpB.DaiHocTiengAnh = 'X' THEN 1 ELSE 0 END) AS xwDaiHocTiengAnh, " +
                        "SUM(CASE WHEN #tmpB.ChungChiTiengAnh = 'X' THEN 1 ELSE 0 END) AS xwChungChiTiengAnh, " +
                        // Trinh do ngoai ngu khac
                        "SUM(CASE WHEN #tmpB.DaiHocNgoaiNguKhac = 'X' THEN 1 ELSE 0 END) AS xwDaiHocNgoaiNguKhac, " +
                        "SUM(CASE WHEN #tmpB.ChungChiNgoaiNguKhac = 'X' THEN 1 ELSE 0 END) AS xwChungChiNgoaiNguKhac, " +
                        // Trinh do tieng dan toc
                        "SUM(CASE WHEN #tmpB.ChungChiTiengDanToc = 'X' THEN 1 ELSE 0 END) AS xwChungChiTiengDanToc, " +
                        // Trinh do QLNN
                        "SUM(CASE WHEN #tmpB.ChuyenVienCaoCap = 'X' THEN 1 ELSE 0 END) AS xwChuyenVienCaoCap, " +
                        "SUM(CASE WHEN #tmpB.ChuyenVienChinh = 'X' THEN 1 ELSE 0 END) AS  xwChuyenVienChinh, " +
                        "SUM(CASE WHEN #tmpB.ChuyenVien = 'X' THEN 1 ELSE 0 END) AS xwChuyenVien, " +
                        "SUM(CASE WHEN #tmpB.ChuaQuaDaoTao = 'X' THEN 1 ELSE 0 END) AS xwChuaQuaDaoTao, " +
                        // Do tuoi
                        "SUM(CASE WHEN #tmpB.Duoi30 = 'X' THEN 1 ELSE 0 END) AS xwDuoi30, " +
                        "SUM(CASE WHEN #tmpB.Tu31Den40 = 'X' THEN 1 ELSE 0 END) AS xw31Den40, " +
                        "SUM(CASE WHEN #tmpB.Tu41Den50 = 'X' THEN 1 ELSE 0 END) AS xw41Den50, " +
                        "SUM(CASE WHEN #tmpB.Tu51Den60 = 'X' THEN 1 ELSE 0 END) AS xw51Den60, " +
                        "SUM(CASE WHEN #tmpB.Nu51Den55 = 'X' THEN 1 ELSE 0 END) AS xwNu51Den55, " +
                        "SUM(CASE WHEN #tmpB.Nam56Den60 = 'X' THEN 1 ELSE 0 END) AS xwNam56Den60, " +
                        "SUM(CASE WHEN #tmpB.TrenTuoiNghiHuu = 'X' THEN 1 ELSE 0 END) AS xwTrenTuoiNghiHuu, " +
                        // Ngach CCVC
                        "SUM(CASE WHEN #tmpB.LuanChuyenTuHuyen = 'X' THEN 1 ELSE 0 END) AS xwLuanChuyenTuHuyen " +
                        //
                        // From table
                        //
                        "FROM #tmpB) AS TMP " +
                    "ON 1=1 ";

            sql += "SELECT * FROM #tmpB ";

            // return
            return sql;
        }

        public static string GetStore_DanhSachTienLuongCNCCCapHuyen(string maDonVi, string condition)
        {
            var sql = string.Empty;
            //create sql
            // xoa bang #tmpB neu da ton tai
            sql += "IF OBJECT_ID('tempdb..#tmpB') IS NOT NULL DROP Table #tempB ";
            sql += "	SELECT 	" +
                   "	FullName AS HoTen," +
                   "   DepartmentId AS MaDonVi, " +
                   "	CASE WHEN Sex = '1' THEN CONVERT( varchar,H.BirthDate, 103) ELSE '' END AS Nam," +
                   "	CASE WHEN Sex = '0' THEN CONVERT( varchar,H.BirthDate, 103) ELSE '' END AS Nu," +
                   "	CASE WHEN H.PositionId IS NOT NULL THEN	" +
                   "		(SELECT TOP 1 Name FROM cat_Position DC WHERE DC.Id = H.PositionId)" +
                   "		ELSE " +
                   "		(SELECT TOP 1 Name FROM cat_JobTitle DCV WHERE DCV.Id = H.JobTitleId)" +
                   "		END AS ChucVu," +
                   "	(SELECT TOP 1 Name FROM cat_Department DD WHERE DD.Id = H.DepartmentId) AS CoQuan,	" +
                   "   DATEDIFF(month,(SELECT TOP 1 EffectiveDate  FROM sal_SalaryDecision HL WHERE HL.RecordId = H.Id ORDER BY HL.ID DESC), getDate()) AS ThoiGianGiuChucVu," +
                   //Muc luong hien huong
                   "	(SELECT TOP 1 SalaryFactor  FROM sal_SalaryDecision HL WHERE HL.RecordId = H.Id ORDER BY HL.ID DESC ) AS HeSoLuong," +
                   "	(select TOP 1 cq.Code  from sal_SalaryDecision hl										" +
                   "	left join cat_Quantum cq on hl.QuantumId = cq.Id										" +
                   "	where hl.RecordId = H.Id										" +
                   "	and hl.DecisionDate = (select MAX(hs2.DecisionDate) from sal_SalaryDecision hs2 where hs2.RecordId = H.Id))	as MaSoNgach, " +

                   //Phu cap
                   //"	(SELECT TOP 1 PositionAllowance  FROM sal_SalaryDecision HL WHERE HL.RecordId = H.Id ORDER BY HL.ID DESC) AS PhuCapChucVu," +
                   "   (SELECT TOP 1 ResponsibilityAllowance FROM sal_SalaryDecision HL WHERE HL.RecordId = H.Id ORDER BY HL.ID DESC) AS PhuCapTrachNhiem," +
                   "   (SELECT TOP 1 AreaAllowance FROM sal_SalaryDecision HL WHERE HL.RecordId = H.Id ORDER BY HL.ID DESC) AS PhuCapKhuVuc," +
                   "	(SELECT TOP 1 OverGrade  FROM sal_SalaryDecision HL WHERE HL.RecordId = H.Id ORDER BY HL.ID DESC) AS PhuCapVuotKhung," +
                   //"	(SELECT TOP 1 (((PositionAllowance + ResponsibilityAllowance + AreaAllowance)/ SalaryFactor)*100 + OverGrade)" +
                   "         FROM sal_SalaryDecision HL WHERE HL.RecordId = H.Id AND HL.SalaryFactor IS NOT NULL AND HL.SalaryFactor > 0 " +
                   "         ORDER BY HL.ID DESC) AS TongPhuCapPhanTram," +
                   //Ghi chu
                   "   (SELECT TOP 1 Note  FROM sal_SalaryDecision HL WHERE HL.RecordId = H.Id ORDER BY HL.ID DESC) AS GhiChu," +
                   //Tong
                   "	cast('0' as float) AS 'xHeSoLuong'," +
                   "	cast('0' as float) AS 'xPhuCapChucVu'," +
                   "	cast('0' as float) AS 'xPhuCapVuotKhung'," +
                   "	cast('0' as float) AS 'xTongPhuCapPhanTram'," +
                   "   cast('0' as float) AS 'xPhuCapTrachNhiem'," +
                   "   cast('0' as float) AS 'xPhuCapKhuVuc'" +
                   "	INTO #tempB	" +
                   "	FROM hr_Record H	" +
                   "	WHERE H.DepartmentId IN ({0})".FormatWith(maDonVi) +
                   "	ORDER BY H.FullName " +

                   "	UPDATE #tempB " +
                   "	SET " +
                   "	xHeSoLuong = xwHeSoLuong," +
                   "	xPhuCapChucVu = xwPhuCapChucVu," +
                   "	xPhuCapVuotKhung = xwPhuCapVuotKhung," +
                   "	xTongPhuCapPhanTram = xwTongPhuCapPhanTram," +
                   "   xPhuCapTrachNhiem = xwPhuCapTrachNhiem," +
                   "   xPhuCapKhuVuc = xwPhuCapKhuVuc" +
                   "	FROM #tempB " +
                   "	INNER JOIN(SELECT " +
                   "	SUM(ISNULL(HeSoLuong, 0)) AS xwHeSoLuong," +
                   "	SUM(ISNULL(PhuCapChucVu,0)) AS xwPhuCapChucVu," +
                   "	SUM(ISNULL(PhuCapVuotKhung,0)) AS xwPhuCapVuotKhung," +
                   "	SUM(ISNULL(TongPhuCapPhanTram,0)) AS xwTongPhuCapPhanTram, " +
                   "   SUM(ISNULL(PhuCapTrachNhiem,0)) AS xwPhuCapTrachNhiem," +
                   "   SUM(ISNULL(PhuCapKhuVuc,0)) AS xwPhuCapKhuVuc " +
                   "	FROM #tempB " +
                   "	) AS TMP ON 1=1	" +
                   "	SELECT * FROM #tempB " +
                   "   WHERE #tempB.HeSoLuong IS NOT NULL ";
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += "   ORDER BY #tempB.MaDonVi, #tempB.HoTen";
            
            return sql;
        }

        public static string GetStore_DanhSachTienLuongCNCCCapXa(string condition)
        {
            var sql = string.Empty;
            //create SQL
            // xoa bang #tmpB neu da ton tai
            sql += "IF OBJECT_ID('tempdb..#tempB') IS NOT NULL DROP Table #tempB ";
            sql += "SELECT "+
                   " FullName AS HoTen,"+
                    "CASE WHEN Sex = '1' THEN CONVERT( varchar,H.BirthDate, 103) ELSE '' END AS Nam," +
                    "CASE WHEN Sex = '0' THEN CONVERT( varchar,H.BirthDate, 103) ELSE '' END AS Nu," +
                    "CASE WHEN H.PositionId IS NOT NULL THEN"+
	                 "   (SELECT TOP 1 Name FROM cat_Position DC WHERE DC.Id = H.PositionId)"+
	                  "  ELSE"+
	                   " (SELECT TOP 1 Name FROM cat_JobTitle DCV WHERE DCV.Id = H.JobTitleId)"+
	                    "END AS ChucVu,"+
                   " (SELECT TOP 1 Name FROM cat_Department DD WHERE DD.Id = H.DepartmentId) AS CoQuan,"+
                    " DATEDIFF(month,(SELECT TOP 1 EffectiveDate  FROM sal_SalaryDecision HL WHERE HL.RecordId = H.Id ORDER BY HL.ID DESC), getDate()) AS ThoiGianGiuChucVu," +
                   " (SELECT TOP 1 SalaryFactor  FROM sal_SalaryDecision HL WHERE HL.RecordId = H.Id ORDER BY HL.ID DESC) AS HeSoLuong," +
                   " (SELECT TOP 1 SalaryGrade  FROM sal_SalaryDecision HL WHERE HL.RecordId = H.Id ORDER BY HL.ID DESC) AS BacLuong," +
                   //" (SELECT TOP 1 PositionAllowance  FROM sal_SalaryDecision HL WHERE HL.RecordId = H.Id ORDER BY HL.ID DESC) AS PhuCapChucVu," +
                   " (SELECT TOP 1 ResponsibilityAllowance FROM sal_SalaryDecision HL WHERE HL.RecordId = H.Id ORDER BY HL.ID DESC) AS PhuCapTrachNhiem," +
                   " (SELECT TOP 1 AreaAllowance FROM sal_SalaryDecision HL WHERE HL.RecordId = H.Id ORDER BY HL.ID DESC) AS PhuCapKhuVuc," +
                   " (SELECT TOP 1 OverGrade  FROM sal_SalaryDecision HL WHERE HL.RecordId = H.Id ORDER BY HL.ID DESC) AS PhuCapVuotKhung," +
                   //" (SELECT TOP 1 (((PositionAllowance + ResponsibilityAllowance + AreaAllowance)/ SalaryFactor)*100 + OverGrade)  FROM sal_SalaryDecision HL WHERE HL.RecordId = H.Id AND HL.SalaryFactor IS NOT NULL AND HL.SalaryFactor > 0  ORDER BY HL.ID DESC) AS TongPhuCapPhanTram," +
                   " (SELECT TOP 1 Note  FROM sal_SalaryDecision HL WHERE HL.RecordId = H.Id ORDER BY HL.ID DESC) AS GhiChu," +
                   " cast('0' as float) AS 'xHeSoLuong'," +
                   " cast('0' as float) AS 'xPhuCapChucVu'," +
                   " cast('0' as float) AS 'xPhuCapVuotKhung'," +
                   " cast('0' as float) AS 'xTongPhuCapPhanTram'," +
                   " cast('0' as float) AS 'xPhuCapTrachNhiem',"+
                   " cast('0' as float) AS 'xPhuCapKhuVuc'"+
                   " INTO #tempB "+
                   " FROM hr_Record H "+
                   " WHERE H.DepartmentId = @MaDonVi " +
                   " ORDER BY H.FullName "+
                   " UPDATE #tempB "+
                   " SET "+
                   " xHeSoLuong = xwHeSoLuong,"+
                   " xPhuCapChucVu = xwPhuCapChucVu,"+
                   " xPhuCapVuotKhung = xwPhuCapVuotKhung,"+
                   " xTongPhuCapPhanTram = xwTongPhuCapPhanTram,"+
                   " xPhuCapTrachNhiem = xwPhuCapTrachNhiem,"+
                   " xPhuCapKhuVuc = xwPhuCapKhuVuc"+ 
                   " FROM #tempB "+
                   "  INNER JOIN(SELECT"+
                   "  SUM(ISNULL(HeSoLuong, 0)) AS xwHeSoLuong,"+
                   "  SUM(ISNULL(PhuCapChucVu,0)) AS xwPhuCapChucVu,"+
                   "  SUM(ISNULL(PhuCapVuotKhung,0)) AS xwPhuCapVuotKhung,"+
                   "  SUM(ISNULL(TongPhuCapPhanTram,0)) AS xwTongPhuCapPhanTram,"+
                   "  SUM(ISNULL(PhuCapTrachNhiem,0)) AS xwPhuCapTrachNhiem,"+  
                   "  SUM(ISNULL(PhuCapKhuVuc,0)) AS xwPhuCapKhuVuc"+
                   "  FROM #tempB"+
                   " ) AS TMP ON 1=1 " +
                  "SELECT * FROM #tempB " +
                  " WHERE #tempB.HeSoLuong IS NOT NULL";
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }

            return sql;
        }

        #region Bieu tong hop
        public static string GetStore_SLCLNguoiLamViecVaBienCheCountTotal(string departments, string kindTypeReport, string employeeType)
        {
            var sql = string.Empty;
            sql += "	IF OBJECT_ID('tempdb..#tmpB') IS NOT NULL DROP Table #tmpB 		" +
                    "	SELECT 		" +
                    "	h.EmployeeTypeId AS EmployeeTypeId,		" +
                    "   (SELECT TOP 1 emp.Name	FROM cat_EmployeeType emp WHERE emp.Id = h.EmployeeTypeId) AS TenLoaiCanBo, " +
                    "	(SELECT dd.Name FROM cat_Department dd WHERE dd.Id = h.DepartmentId ) AS 'TenDonVi', 		" +
                    "	CASE WHEN (h.CPVOfficialJoinedDate IS NOT NULL) THEN 'X' ELSE '' END AS DangVien, 		" +
                    "	CASE WHEN (SELECT TOP 1 IsMinority FROM cat_Folk WHERE Id = h.FolkId) = '1' THEN 'X' ELSE '' END AS DanTocThieuSo, 		" +
                    "	CASE WHEN h.Sex = '0' THEN 'X' ELSE '' END AS Nu, 		" +
                    "	CASE WHEN h.Sex = '1' THEN 'X' ELSE '' END AS Nam, 		" +
                    "	CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 30) THEN 'X' ELSE '' END AS Duoi30, 		" +
                    "	CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 30 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 40) THEN 'X' ELSE '' END AS Tu31Den40, 		" +
                    "	CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 40 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 50) THEN 'X' ELSE '' END AS Tu41Den50, 		" +
                    "	CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 50 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 55 AND h.Sex = '0') THEN 'X' ELSE '' END AS Nu51Den55, 		" +
                    "	CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 50 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 55 AND h.Sex = '1') THEN 'X' ELSE '' END AS Nam51Den55,		" +
                    "	CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 55 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 60 AND h.Sex = '1') THEN 'X' ELSE '' END AS Nam56Den60, 		" +
                    "	CASE WHEN (SELECT TOP 1 [Group] FROM cat_ManagementLevel ml WHERE ml.Id = h.ManagementLevelId) = 'CVC' THEN 'X' ELSE '' END AS ChuyenVienChinh, 		" +
                    "	CASE WHEN (SELECT TOP 1 [Group] FROM cat_ManagementLevel ml WHERE ml.Id = h.ManagementLevelId) = 'CV' THEN 'X' ELSE '' END AS ChuyenVien, 		" +
                    "	CASE WHEN (SELECT TOP 1 [Group] FROM cat_ManagementLevel ml WHERE ml.Id = h.ManagementLevelId) = 'CS' THEN 'X' ELSE '' END AS CanSu, 		" +
                    "	CASE WHEN (SELECT TOP 1 [Group] FROM cat_ManagementLevel ml WHERE ml.Id = h.ManagementLevelId) = 'CQDT' THEN 'X' ELSE '' END AS NgachConLai, 		" +
                    "	CASE WHEN (SELECT TOP 1 [Group] FROM cat_BasicEducation WHERE Id = H.BasicEducationId) = 'THCS' THEN 'X' ELSE '' END AS THCS, 		" +
                    "	CASE WHEN (SELECT TOP 1 [Group] FROM cat_BasicEducation WHERE Id = H.BasicEducationId) = 'THPT' THEN 'X' ELSE '' END AS THPT,		" +
                    "	CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = H.EducationId) = 'ThS' THEN 'X' ELSE '' END AS ThacSi, 		" +
                    "	CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = H.EducationId) = 'DH' THEN 'X' ELSE '' END AS DaiHoc, 		" +
                    "	CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = H.EducationId) = 'CD' THEN 'X' ELSE '' END AS CaoDang, 		" +
                    "	CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = H.EducationId) = 'TC' THEN 'X' ELSE '' END AS TrungCap, 		" +
                    "	CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = H.EducationId) = 'SC' OR (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = H.EducationId) = 'TS' THEN 'X' ELSE '' END AS TrinhDoChuyenMonConLai, 		" +
                    "	CASE WHEN (SELECT TOP 1 Id FROM cat_PoliticLevel pl WHERE pl.Id = H.PoliticLevelId) = '4' THEN 'X' ELSE '' END AS CuNhanChinhTri, 		" +
                    "	CASE WHEN (SELECT TOP 1 Id FROM cat_PoliticLevel pl WHERE pl.Id = H.PoliticLevelId) = '3' THEN 'X' ELSE '' END AS CaoCapChinhTri, 		" +
                    "	CASE WHEN (SELECT TOP 1 Id FROM cat_PoliticLevel pl WHERE pl.Id = H.PoliticLevelId) = '2' THEN 'X' ELSE '' END AS TrungCapChinhTri, 		" +
                    "	'' AS ChuongTrinhChuyenVien,		" +
                    "	'' AS QLGD,		" +
                    "	CASE WHEN (SELECT TOP 1 [Group] FROM cat_ITLevel itl WHERE itl.Id = H.ITLevelId) = 'DH' THEN 'X' ELSE '' END AS DaiHocTinHoc, 		" +
                    "	CASE WHEN (SELECT TOP 1 [Group] FROM cat_ITLevel itl WHERE itl.Id = H.ITLevelId) = 'CD' THEN 'X' ELSE '' END AS CaoDangTinHoc, 		" +
                    "	CASE WHEN (SELECT TOP 1 [Group] FROM cat_ITLevel itl WHERE itl.Id = H.ITLevelId) = 'TC' THEN 'X' ELSE '' END AS TrungCapTinHoc, 		" +
                    "	CASE WHEN (SELECT TOP 1 [Group] FROM cat_ITLevel itl WHERE itl.Id = H.ITLevelId) = 'CC' THEN 'X' ELSE '' END AS ChungChiTinHoc, 		" +
                    "	CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = H.LanguageLevelId) = 'DHTA' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId  WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'DHTA') = 'DHTA' THEN 'X' ELSE '' END AS DaiHocTiengAnh,		" +
                    "	CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = H.LanguageLevelId) = 'CCTA' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId  WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'CCTA') = 'CCTA' THEN 'X' ELSE '' END AS ChungChiTiengAnh,		" +
                    "	CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = H.LanguageLevelId) = 'DHNNK' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'DHNNK') = 'DHNNK' THEN 'X' ELSE '' END AS DaiHocNgoaiNguKhac,		" +
                    "	CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = H.LanguageLevelId) = 'CCNNK' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'CCNNK') = 'CCNNK' THEN 'X' ELSE '' END AS ChungChiNgoaiNguKhac, 		" +
                    "	0 AS 'xTongSo',		" +
                    "	0 AS 'xDangVien', 		" +
                    "	0 AS 'xDanTocThieuSo',		" +
                    "	0 AS 'xNu', 		" +
                    "	0 AS 'xNam', 		" +
                    "	0 AS 'xDuoi30', 		" +
                    "	0 AS 'x31Den40', 		" +
                    "	0 AS 'x41Den50', 		" +
                    "	0 AS 'xNu51Den55', 		" +
                    "	0 AS 'xNam51Den55', 		" +
                    "	0 AS 'xNam56Den60', 		" +
                    "	0 AS 'xChuyenVienChinh', 		" +
                    "	0 AS 'xChuyenVien', 		" +
                    "	0 AS 'xCanSu', 		" +
                    "	0 AS 'xNgachConLai',		" +
                    "	0 AS 'xTHCS', 		" +
                    "	0 AS 'xTHPT', 		" +
                    "	0 AS 'xThacSi', 		" +
                    "	0 AS 'xDaiHoc', 		" +
                    "	0 AS 'xCaoDang',		" +
                    "	0 AS 'xTrungCap', 		" +
                    "	0 AS 'xTrinhDoChuyenMonConLai', 		" +
                    "	0 as 'xCuNhanChinhTri', 		" +
                    "	0 as 'xCaoCapChinhTri',		" +
                    "	0 as 'xTrungCapChinhTri', 		" +
                    "	0 as 'xDaiHocTinHoc',		" +
                    "	0 as 'xCaoDangTinHoc',		" +
                    "	0 as 'xTrungCapTinHoc',		" +
                    "	0 as 'xChungChiTinHoc', 		" +
                    "	0 as 'xDaiHocTiengAnh', 		" +
                    "	0 as 'xChungChiTiengAnh', 		" +
                    "	0 as 'xDaiHocNgoaiNguKhac', 		" +
                    "	0 as 'xChungChiNgoaiNguKhac'		" +
                    "	INTO #tmpB 		" +
                    "	FROM hr_Record h " +
                    "   LEFT JOIN cat_EmployeeType ce ON ce.Id = h.EmployeeTypeId " +
                    "   WHERE h.DepartmentId IN ({0}) ".FormatWith(departments);
                    if ("LaNu" == kindTypeReport)
                    {
                        sql += "AND h.Sex = '0'  ";
                    }
                    if (!string.IsNullOrEmpty(employeeType))
                    {
                        sql += " AND ce.[Group] = '{0}' ".FormatWith(employeeType);
                    }
                    sql+= "	ORDER BY h.EmployeeTypeId 		" +
                    "	UPDATE	#tmpB 	" +
                    "	SET 		" +
                    "	xTongSo = xwTongSo, 		" +
                    "	xDangVien = xwDangVien, 		" +
                    "	xDanTocThieuSo = xwDanTocThieuSo, 		" +
                    "	xNu = xwNu, 		" +
                    "	xNam = xwNam, 		" +
                    "	xDuoi30 = xwDuoi30, 		" +
                    "	x31Den40 = xw31Den40, 		" +
                    "	x41Den50 = xw41Den50, 		" +
                    "	xNu51Den55 = xwNu51Den55, 		" +
                    "	xNam51Den55 = xwNam51Den55, 		" +
                    "	xNam56Den60 = xwNam56Den60, 		" +
                    "	xThacSi = xwThacSi, 		" +
                    "	xDaiHoc = xwDaiHoc, 		" +
                    "	xCaoDang = xwCaoDang, 		" +
                    "	xTrungCap = xwTrungCap, 		" +
                    "	xTrinhDoChuyenMonConLai = xwTrinhDoChuyenMonConLai, 		" +
                    "	xTHCS = xwTHCS, 		" +
                    "	xTHPT = xwTHPT, 		" +
                    "	xCuNhanChinhTri = xwCuNhanChinhTri,		" +
                    "	xCaoCapChinhTri = xwCaoCapChinhTri, 		" +
                    "	xTrungCapChinhTri = xwTrungCapChinhTri, 		" +
                    "	xDaiHocTinHoc = xwDaiHocTinHoc, 		" +
                    "	xCaoDangTinHoc = xwCaoDangTinHoc, 		" +
                    "	xTrungCapTinHoc = xwTrungCapTinHoc, 		" +
                    "	xChungChiTinHoc = xwChungChiTinHoc, 		" +
                    "	xDaiHocTiengAnh = xwDaiHocTiengAnh, 		" +
                    "	xChungChiTiengAnh = xwChungChiTiengAnh, 		" +
                    "	xDaiHocNgoaiNguKhac = xwDaiHocNgoaiNguKhac, 		" +
                    "	xChungChiNgoaiNguKhac = xwChungChiNgoaiNguKhac, 		" +
                    "	xChuyenVienChinh = xwChuyenVienChinh, 		" +
                    "	xChuyenVien = xwChuyenVien, 		" +
                    "	xCanSu = xwCanSu, 		" +
                    "	xNgachConLai = xwNgachConLai		" +
                    "	FROM #tmpB 		" +
                    "	INNER JOIN (SELECT 		" +
                    "	SUM(1) AS xwTongSo, 		" +
                    "	SUM(CASE WHEN #tmpB.DangVien = 'X' THEN 1 ELSE 0 END) AS xwDangVien, 		" +
                    "	SUM(CASE WHEN #tmpB.DanTocThieuSo = 'X' THEN 1 ELSE 0 END) AS xwDanTocThieuSo, 		" +
                    "	SUM(CASE WHEN #tmpB.Nu = 'X' THEN 1 ELSE 0 END) AS xwNu, 		" +
                    "	SUM(CASE WHEN #tmpB.Nam = 'X' THEN 1 ELSE 0 END) AS xwNam, 		" +
                    "	SUM(CASE WHEN #tmpB.Duoi30 = 'X' THEN 1 ELSE 0 END) AS xwDuoi30, 		" +
                    "	SUM(CASE WHEN #tmpB.Tu31Den40 = 'X' THEN 1 ELSE 0 END) AS xw31Den40, 		" +
                    "	SUM(CASE WHEN #tmpB.Tu41Den50 = 'X' THEN 1 ELSE 0 END) AS xw41Den50, 		" +
                    "	SUM(CASE WHEN #tmpB.Nu51Den55 = 'X' THEN 1 ELSE 0 END) AS xwNu51Den55, 		" +
                    "	SUM(CASE WHEN #tmpB.Nam51Den55 = 'X' THEN 1 ELSE 0 END) AS xwNam51Den55, 		" +
                    "	SUM(CASE WHEN #tmpB.Nam56Den60 = 'X' THEN 1 ELSE 0 END) AS xwNam56Den60, 		" +
                    "	SUM(CASE WHEN #tmpB.ThacSi = 'X' THEN 1 ELSE 0 END) AS xwThacSi, 		" +
                    "	SUM(CASE WHEN #tmpB.DaiHoc = 'X' THEN 1 ELSE 0 END) AS xwDaiHoc, 		" +
                    "	SUM(CASE WHEN #tmpB.CaoDang = 'X' THEN 1 ELSE 0 END) AS xwCaoDang, 		" +
                    "	SUM(CASE WHEN #tmpB.TrungCap = 'X' THEN 1 ELSE 0 END) AS xwTrungCap, 		" +
                    "	SUM(CASE WHEN #tmpB.TrinhDoChuyenMonConLai = 'X' THEN 1 ELSE 0 END) AS xwTrinhDoChuyenMonConLai, 		" +
                    "	SUM(CASE WHEN #tmpB.THCS = 'X' THEN 1 ELSE 0 END) AS xwTHCS, 		" +
                    "	SUM(CASE WHEN #tmpB.THPT = 'X' THEN 1 ELSE 0 END) AS xwTHPT, 		" +
                    "	SUM(CASE WHEN #tmpB.CuNhanChinhTri = 'X' THEN 1 ELSE 0 END) AS xwCuNhanChinhTri, 		" +
                    "	SUM(CASE WHEN #tmpB.CaoCapChinhTri = 'X' THEN 1 ELSE 0 END) AS xwCaoCapChinhTri, 		" +
                    "	SUM(CASE WHEN #tmpB.TrungCapChinhTri = 'X' THEN 1 ELSE 0 END) AS xwTrungCapChinhTri, 		" +
                    "	SUM(CASE WHEN #tmpB.DaiHocTinHoc = 'X' THEN 1 ELSE 0 END) AS xwDaiHocTinHoc, 		" +
                    "	SUM(CASE WHEN #tmpB.CaoDangTinHoc = 'X' THEN 1 ELSE 0 END) AS xwCaoDangTinHoc, 		" +
                    "	SUM(CASE WHEN #tmpB.TrungCapTinHoc = 'X' THEN 1 ELSE 0 END) AS xwTrungCapTinHoc, 		" +
                    "	SUM(CASE WHEN #tmpB.ChungChiTinHoc = 'X' THEN 1 ELSE 0 END) AS xwChungChiTinHoc, 		" +
                    "	SUM(CASE WHEN #tmpB.DaiHocTiengAnh = 'X' THEN 1 ELSE 0 END) AS xwDaiHocTiengAnh, 		" +
                    "	SUM(CASE WHEN #tmpB.ChungChiTiengAnh = 'X' THEN 1 ELSE 0 END) AS xwChungChiTiengAnh, 		" +
                    "	SUM(CASE WHEN #tmpB.DaiHocNgoaiNguKhac = 'X' THEN 1 ELSE 0 END) AS xwDaiHocNgoaiNguKhac, 		" +
                    "	SUM(CASE WHEN #tmpB.ChungChiNgoaiNguKhac = 'X' THEN 1 ELSE 0 END) AS xwChungChiNgoaiNguKhac, 		" +
                    "	SUM(CASE WHEN #tmpB.ChuyenVienChinh = 'X' THEN 1 ELSE 0 END) AS  xwChuyenVienChinh, 		" +
                    "	SUM(CASE WHEN #tmpB.ChuyenVien = 'X' THEN 1 ELSE 0 END) AS xwChuyenVien, 		" +
                    "	SUM(CASE WHEN #tmpB.CanSu = 'X' THEN 1 ELSE 0 END) AS xwCanSu, 		" +
                    "	SUM(CASE WHEN #tmpB.NgachConLai = 'X' THEN 1 ELSE 0 END) AS xwNgachConLai		" +
                    "	FROM #tmpB	" +
                    "	) AS TMP ON 1=1 	" +
                    "	SELECT * FROM #tmpB " +
                    " ORDER BY EmployeeTypeId"; 

            return sql;
        }

        public static string GetStore_SLCLNguoiLamViecVaBienCheCountGroupTotal(string departments, string kindTypeReport, string employeeType)
        {
            var sql = string.Empty;
            sql += "	IF OBJECT_ID('tempdb..#tmpBGroup') IS NOT NULL DROP Table #tmpBGroup 		" +
                "SELECT 		" +
                "h.EmployeeTypeId AS EmployeeTypeId,		" +
                "(SELECT TOP 1 emp.Name	FROM cat_EmployeeType emp WHERE emp.Id = h.EmployeeTypeId) AS TenLoaiCanBo,	" +
                "(SELECT dd.Name FROM cat_Department dd WHERE dd.Id = h.DepartmentId ) AS 'TenDonVi', 		" +
                "CASE WHEN (h.CPVOfficialJoinedDate IS NOT NULL) THEN 'X' ELSE '' END AS DangVien, 		" +
                "CASE WHEN (SELECT TOP 1 IsMinority FROM cat_Folk WHERE Id = h.FolkId) = '1' THEN 'X' ELSE '' END AS DanTocThieuSo, 		" +
                "CASE WHEN h.Sex = '0' THEN 'X' ELSE '' END AS Nu, 		" +
                "CASE WHEN h.Sex = '1' THEN 'X' ELSE '' END AS Nam,	" +
                "CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 30) THEN 'X' ELSE '' END AS Duoi30, 		" +
                "CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 30 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 40) THEN 'X' ELSE '' END AS Tu31Den40, 		" +
                "CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 40 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 50) THEN 'X' ELSE '' END AS Tu41Den50, 		" +
                "CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 50 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 55 AND h.Sex = '0') THEN 'X' ELSE '' END AS Nu51Den55, 		" +
                "CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 50 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 55 AND h.Sex = '1') THEN 'X' ELSE '' END AS Nam51Den55,		" +
                "CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 55 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 60 AND h.Sex = '1') THEN 'X' ELSE '' END AS Nam56Den60, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ManagementLevel ml WHERE ml.Id = h.ManagementLevelId) = 'CVC' THEN 'X' ELSE '' END AS ChuyenVienChinh, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ManagementLevel ml WHERE ml.Id = h.ManagementLevelId) = 'CV' THEN 'X' ELSE '' END AS ChuyenVien, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ManagementLevel ml WHERE ml.Id = h.ManagementLevelId) = 'CS' THEN 'X' ELSE '' END AS CanSu, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ManagementLevel ml WHERE ml.Id = h.ManagementLevelId) = 'CQDT' THEN 'X' ELSE '' END AS NgachConLai, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_BasicEducation WHERE Id = h.BasicEducationId) = 'THCS' THEN 'X' ELSE '' END AS THCS, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_BasicEducation WHERE Id = h.BasicEducationId) = 'THPT' THEN 'X' ELSE '' END AS THPT,		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = h.EducationId) = 'ThS' THEN 'X' ELSE '' END AS ThacSi, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = h.EducationId) = 'DH' THEN 'X' ELSE '' END AS DaiHoc, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = h.EducationId) = 'CD' THEN 'X' ELSE '' END AS CaoDang, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = h.EducationId) = 'TC' THEN 'X' ELSE '' END AS TrungCap, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = h.EducationId) = 'SC' OR (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = H.EducationId) = 'TS' THEN 'X' ELSE '' END AS TrinhDoChuyenMonConLai, 		" +
                "CASE WHEN (SELECT TOP 1 Id FROM cat_PoliticLevel pl WHERE pl.Id = h.PoliticLevelId) = '4' THEN 'X' ELSE '' END AS CuNhanChinhTri, 		" +
                "CASE WHEN (SELECT TOP 1 Id FROM cat_PoliticLevel pl WHERE pl.Id = h.PoliticLevelId) = '3' THEN 'X' ELSE '' END AS CaoCapChinhTri, 		" +
                "CASE WHEN (SELECT TOP 1 Id FROM cat_PoliticLevel pl WHERE pl.Id = h.PoliticLevelId) = '2' THEN 'X' ELSE '' END AS TrungCapChinhTri, 		" +
                "'' AS ChuongTrinhChuyenVien,		" +
                "'' AS QLGD,		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ITLevel itl WHERE itl.Id = h.ITLevelId) = 'DH' THEN 'X' ELSE '' END AS DaiHocTinHoc, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ITLevel itl WHERE itl.Id = h.ITLevelId) = 'CD' THEN 'X' ELSE '' END AS CaoDangTinHoc, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ITLevel itl WHERE itl.Id = h.ITLevelId) = 'TC' THEN 'X' ELSE '' END AS TrungCapTinHoc, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ITLevel itl WHERE itl.Id = h.ITLevelId) = 'CC' THEN 'X' ELSE '' END AS ChungChiTinHoc, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = h.LanguageLevelId) = 'DHTA' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId  WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'DHTA') = 'DHTA' THEN 'X' ELSE '' END AS DaiHocTiengAnh,	" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = h.LanguageLevelId) = 'CCTA' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId  WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'CCTA') = 'CCTA' THEN 'X' ELSE '' END AS ChungChiTiengAnh,	" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = h.LanguageLevelId) = 'DHNNK' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'DHNNK') = 'DHNNK' THEN 'X' ELSE '' END AS DaiHocNgoaiNguKhac,	" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = h.LanguageLevelId) = 'CCNNK' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'CCNNK') = 'CCNNK' THEN 'X' ELSE '' END AS ChungChiNgoaiNguKhac, 	" +
                " '' AS GhiChu " +
                "INTO #tmpBGroup 		" +
                "FROM hr_Record h 		" +
                "   LEFT JOIN cat_EmployeeType ce ON ce.Id = h.EmployeeTypeId " +
                "WHERE h.DepartmentId IN ({0}) ".FormatWith(departments);
                if("LaNu" == kindTypeReport)
                {
                    sql += "AND h.Sex = '0'  ";
                }
                if (!string.IsNullOrEmpty(employeeType))
                {
                    sql += " AND ce.[Group] = '{0}' ".FormatWith(employeeType);
                }
                sql += "ORDER BY h.EmployeeTypeId 		" +
                "SELECT  " +
                "MAX(#tmpBGroup.TenLoaiCanBo) AS TenLoaiCanBo, " +
                "SUM(CASE WHEN #tmpBGroup.DangVien = 'X' THEN 1 ELSE 0 END) AS xGroupDangVien,	" +
                "SUM(CASE WHEN #tmpBGroup.DanTocThieuSo = 'X' THEN 1 ELSE 0 END) AS xGroupDanTocThieuSo, 	" +
                "SUM(CASE WHEN #tmpBGroup.Nu = 'X' THEN 1 ELSE 0 END) AS xGroupNu,	" +
                "SUM(CASE WHEN #tmpBGroup.Nam = 'X' THEN 1 ELSE 0 END) AS xGroupNam, 	" +
                "SUM(CASE WHEN #tmpBGroup.Duoi30 = 'X' THEN 1 ELSE 0 END) AS xGroupDuoi30, 	" +
                "SUM(CASE WHEN #tmpBGroup.Tu31Den40 = 'X' THEN 1 ELSE 0 END) AS xGroup31Den40, 	" +
                "SUM(CASE WHEN #tmpBGroup.Tu41Den50 = 'X' THEN 1 ELSE 0 END) AS xGroup41Den50, 	" +
                "SUM(CASE WHEN #tmpBGroup.Nu51Den55 = 'X' THEN 1 ELSE 0 END) AS xGroupNu51Den55, 	" +
                "SUM(CASE WHEN #tmpBGroup.Nam51Den55 = 'X' THEN 1 ELSE 0 END) AS xGroupNam51Den55, 	" +
                "SUM(CASE WHEN #tmpBGroup.Nam56Den60 = 'X' THEN 1 ELSE 0 END) AS xGroupNam56Den60, 	" +
                "SUM(CASE WHEN #tmpBGroup.ThacSi = 'X' THEN 1 ELSE 0 END) AS xGroupThacSi, 	" +
                "SUM(CASE WHEN #tmpBGroup.DaiHoc = 'X' THEN 1 ELSE 0 END) AS xGroupDaiHoc, 	" +
                "SUM(CASE WHEN #tmpBGroup.CaoDang = 'X' THEN 1 ELSE 0 END) AS xGroupCaoDang, 	" +
                "SUM(CASE WHEN #tmpBGroup.TrungCap = 'X' THEN 1 ELSE 0 END) AS xGroupTrungCap, 	" +
                "SUM(CASE WHEN #tmpBGroup.TrinhDoChuyenMonConLai = 'X' THEN 1 ELSE 0 END) AS xGroupTrinhDoChuyenMonConLai, 	" +
                "SUM(CASE WHEN #tmpBGroup.THCS = 'X' THEN 1 ELSE 0 END) AS xGroupTHCS, 	" +
                "SUM(CASE WHEN #tmpBGroup.THPT = 'X' THEN 1 ELSE 0 END) AS xGroupTHPT, 	" +
                "SUM(CASE WHEN #tmpBGroup.CuNhanChinhTri = 'X' THEN 1 ELSE 0 END) AS xGroupCuNhanChinhTri, 	" +
                "SUM(CASE WHEN #tmpBGroup.CaoCapChinhTri = 'X' THEN 1 ELSE 0 END) AS xGroupCaoCapChinhTri, 	" +
                "SUM(CASE WHEN #tmpBGroup.TrungCapChinhTri = 'X' THEN 1 ELSE 0 END) AS xGroupTrungCapChinhTri, 	" +
                "SUM(CASE WHEN #tmpBGroup.DaiHocTinHoc = 'X' THEN 1 ELSE 0 END) AS xGroupDaiHocTinHoc, 	" +
                "SUM(CASE WHEN #tmpBGroup.CaoDangTinHoc = 'X' THEN 1 ELSE 0 END) AS xGroupCaoDangTinHoc, 	" +
                "SUM(CASE WHEN #tmpBGroup.TrungCapTinHoc = 'X' THEN 1 ELSE 0 END) AS xGroupTrungCapTinHoc, 	" +
                "SUM(CASE WHEN #tmpBGroup.ChungChiTinHoc = 'X' THEN 1 ELSE 0 END) AS xGroupChungChiTinHoc, 	" +
                "SUM(CASE WHEN #tmpBGroup.DaiHocTiengAnh = 'X' THEN 1 ELSE 0 END) AS xGroupDaiHocTiengAnh, 	" +
                "SUM(CASE WHEN #tmpBGroup.ChungChiTiengAnh = 'X' THEN 1 ELSE 0 END) AS xGroupChungChiTiengAnh, 	" +
                "SUM(CASE WHEN #tmpBGroup.DaiHocNgoaiNguKhac = 'X' THEN 1 ELSE 0 END) AS xGroupDaiHocNgoaiNguKhac, 	" +
                "SUM(CASE WHEN #tmpBGroup.ChungChiNgoaiNguKhac = 'X' THEN 1 ELSE 0 END) AS xGroupChungChiNgoaiNguKhac, 	" +
                "SUM(CASE WHEN #tmpBGroup.ChuyenVienChinh = 'X' THEN 1 ELSE 0 END) AS  xGroupChuyenVienChinh, 	" +
                "SUM(CASE WHEN #tmpBGroup.ChuyenVien = 'X' THEN 1 ELSE 0 END) AS xGroupChuyenVien, 	" +
                "SUM(CASE WHEN #tmpBGroup.CanSu = 'X' THEN 1 ELSE 0 END) AS xGroupCanSu, 	" +
                "SUM(CASE WHEN #tmpBGroup.NgachConLai = 'X' THEN 1 ELSE 0 END) AS xGroupNgachConLai,	" +
                " MAX(#tmpBGroup.GhiChu) AS GhiChu " +
                "INTO #tmpC	" +
                "FROM #tmpBGroup	" +
                "GROUP BY #tmpBGroup.EmployeeTypeId	" +
                "SELECT * FROM #tmpC	 ";

            return sql;
        }

        public static string GetStore_SLCLNguoiLamViecLaDanTocThieuSoCountTotal(string departments, string employeeType)
        {
            var sql = string.Empty;
            sql += "	IF OBJECT_ID('tempdb..#tmpB') IS NOT NULL DROP Table #tmpB 		" +
                    " SELECT 		" +
                    " h.EmployeeTypeId AS EmployeeTypeId,		" +
                    "   (SELECT TOP 1 emp.Name	FROM cat_EmployeeType emp WHERE emp.Id = h.EmployeeTypeId) AS TenLoaiCanBo, " +
                    " (SELECT dd.Name FROM cat_Department dd WHERE dd.Id = h.DepartmentId ) AS 'TenDonVi', 		" +
                    " CASE WHEN (h.CPVOfficialJoinedDate IS NOT NULL) THEN 'X' ELSE '' END AS DangVien, 		" +
                    " CASE WHEN cf.Name = N'Thái' THEN 'X' ELSE '' END AS DanTocThai,	" +
                    " CASE WHEN cf.Name = N'Mông' THEN 'X' ELSE '' END AS DanTocMong,	" +
                    " CASE WHEN cf.Name = N'Hà Nhì' THEN 'X' ELSE '' END AS DanTocHaNhi,	" +
                    " CASE WHEN cf.Name = N'Tày' THEN 'X' ELSE '' END AS DanTocTay,	" +
                    " CASE WHEN cf.Name = N'Mường' THEN 'X' ELSE '' END AS DanTocMuong,	" +
                    " CASE WHEN cf.Name = N'Dao' THEN 'X' ELSE '' END AS DanTocDao,	" +
                    " CASE WHEN cf.Name = N'Giáy' THEN 'X' ELSE '' END AS DanTocGiay,	" +
                    " CASE WHEN cf.Name = N'Cống' THEN 'X' ELSE '' END AS DanTocCong,	" +
                    " CASE WHEN cf.Name = N'Hoa' THEN 'X' ELSE '' END AS DanTocHoa,	" +
                    " CASE WHEN cf.Name = N'Si La' THEN 'X' ELSE '' END AS DanTocSiLa,	" +
                    " CASE WHEN cf.Name = N'Nùng' THEN 'X' ELSE '' END AS DanTocNung,	" +
                    " CASE WHEN cf.Name = N'Cao Lan' THEN 'X' ELSE '' END AS DanTocCaoLan,	" +
                    " CASE WHEN cf.Name = N'La Hủ' THEN 'X' ELSE '' END AS DanTocLaHu,	" +
                    " CASE WHEN cf.Name = N'Thổ' THEN 'X' ELSE '' END AS DanTocTho,	" +
                    " CASE WHEN cf.Name not in( N'Giáy', N'Mông', N'Hà Nhì',N'Tày', N'Mường', N'Dao' , N'Giáy', N'Cống', N'Hoa', N'Si La',N'Nùng',N'Cao Lan',N'La Hủ', N'Thổ' ) THEN 'X' ELSE '' END AS DanTocKhac, " +
                    " CASE WHEN h.Sex = '0' THEN 'X' ELSE '' END AS Nu, 		" +
                    " CASE WHEN h.Sex = '1' THEN 'X' ELSE '' END AS Nam, 		" +
                    " CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 30) THEN 'X' ELSE '' END AS Duoi30, 		" +
                    " CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 30 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 40) THEN 'X' ELSE '' END AS Tu31Den40, 		" +
                    " CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 40 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 50) THEN 'X' ELSE '' END AS Tu41Den50, 		" +
                    " CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 50 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 55 AND h.Sex = '0') THEN 'X' ELSE '' END AS Nu51Den55, 		" +
                    " CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 50 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 55 AND h.Sex = '1') THEN 'X' ELSE '' END AS Nam51Den55,		" +
                    " CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 55 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 60 AND h.Sex = '1') THEN 'X' ELSE '' END AS Nam56Den60, 		" +
                    " CASE WHEN (SELECT TOP 1 [Group] FROM cat_ManagementLevel ml WHERE ml.Id = h.ManagementLevelId) = 'CVC' THEN 'X' ELSE '' END AS ChuyenVienChinh, 		" +
                    " CASE WHEN (SELECT TOP 1 [Group] FROM cat_ManagementLevel ml WHERE ml.Id = h.ManagementLevelId) = 'CV' THEN 'X' ELSE '' END AS ChuyenVien, 		" +
                    " CASE WHEN (SELECT TOP 1 [Group] FROM cat_ManagementLevel ml WHERE ml.Id = h.ManagementLevelId) = 'CS' THEN 'X' ELSE '' END AS CanSu, 		" +
                    " CASE WHEN (SELECT TOP 1 [Group] FROM cat_ManagementLevel ml WHERE ml.Id = h.ManagementLevelId) = 'CQDT' THEN 'X' ELSE '' END AS NgachConLai, 		" +
                    " CASE WHEN (SELECT TOP 1 [Group] FROM cat_BasicEducation WHERE Id = H.BasicEducationId) = 'THCS' THEN 'X' ELSE '' END AS THCS, 		" +
                    " CASE WHEN (SELECT TOP 1 [Group] FROM cat_BasicEducation WHERE Id = H.BasicEducationId) = 'THPT' THEN 'X' ELSE '' END AS THPT,		" +
                    " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = H.EducationId) = 'ThS' THEN 'X' ELSE '' END AS ThacSi, 		" +
                    " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = H.EducationId) = 'DH' THEN 'X' ELSE '' END AS DaiHoc, 		" +
                    " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = H.EducationId) = 'CD' THEN 'X' ELSE '' END AS CaoDang, 		" +
                    " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = H.EducationId) = 'TC' THEN 'X' ELSE '' END AS TrungCap, 		" +
                    " CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = H.EducationId) = 'SC' OR (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = H.EducationId) = 'TS' THEN 'X' ELSE '' END AS TrinhDoChuyenMonConLai, 		" +
                    " CASE WHEN (SELECT TOP 1 Id FROM cat_PoliticLevel pl WHERE pl.Id = H.PoliticLevelId) = '4' THEN 'X' ELSE '' END AS CuNhanChinhTri, 		" +
                    " CASE WHEN (SELECT TOP 1 Id FROM cat_PoliticLevel pl WHERE pl.Id = H.PoliticLevelId) = '3' THEN 'X' ELSE '' END AS CaoCapChinhTri, 		" +
                    " CASE WHEN (SELECT TOP 1 Id FROM cat_PoliticLevel pl WHERE pl.Id = H.PoliticLevelId) = '2' THEN 'X' ELSE '' END AS TrungCapChinhTri, 		" +
                    " '' AS ChuongTrinhChuyenVien,		" +
                    " '' AS QLGD,		" +
                    " CASE WHEN (SELECT TOP 1 [Group] FROM cat_ITLevel itl WHERE itl.Id = H.ITLevelId) = 'DH' THEN 'X' ELSE '' END AS DaiHocTinHoc, 		" +
                    " CASE WHEN (SELECT TOP 1 [Group] FROM cat_ITLevel itl WHERE itl.Id = H.ITLevelId) = 'CD' THEN 'X' ELSE '' END AS CaoDangTinHoc, 		" +
                    " CASE WHEN (SELECT TOP 1 [Group] FROM cat_ITLevel itl WHERE itl.Id = H.ITLevelId) = 'TC' THEN 'X' ELSE '' END AS TrungCapTinHoc, 		" +
                    " CASE WHEN (SELECT TOP 1 [Group] FROM cat_ITLevel itl WHERE itl.Id = H.ITLevelId) = 'CC' THEN 'X' ELSE '' END AS ChungChiTinHoc, 		" +
                    " CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = H.LanguageLevelId) = 'DHTA' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId  WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'DHTA') = 'DHTA' THEN 'X' ELSE '' END AS DaiHocTiengAnh,		" +
                    " CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = H.LanguageLevelId) = 'CCTA' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId  WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'CCTA') = 'CCTA' THEN 'X' ELSE '' END AS ChungChiTiengAnh,		" +
                    " CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = H.LanguageLevelId) = 'DHNNK' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'DHNNK') = 'DHNNK' THEN 'X' ELSE '' END AS DaiHocNgoaiNguKhac,		" +
                    " CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = H.LanguageLevelId) = 'CCNNK' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'CCNNK') = 'CCNNK' THEN 'X' ELSE '' END AS ChungChiNgoaiNguKhac, 		" +
                    " 0 AS 'xTongSo',		" +
                    " 0 AS 'xDangVien', 		" +
                    " 0 AS 'xDanTocThai',  " +
                    " 0 AS 'xDanTocMong',  " +
                    " 0 AS 'xDanTocHaNhi',  " +
                    " 0 AS 'xDanTocTay',  " +
                    " 0 AS 'xDanTocMuong',  " +
                    " 0 AS 'xDanTocDao',  " +
                    " 0 AS 'xDanTocGiay',  " +
                    " 0 AS 'xDanTocCong',  " +
                    " 0 AS 'xDanTocHoa',  " +
                    " 0 AS 'xDanTocSiLa',  " +
                    " 0 AS 'xDanTocNung',  " +
                    " 0 AS 'xDanTocCaoLan',  " +
                    " 0 AS 'xDanTocLaHu',  " +
                    " 0 AS 'xDanTocTho',  " +
                    " 0 AS 'xDanTocKhac',  " +
                    " 0 AS 'xNu', 		" +
                    " 0 AS 'xNam', 		" +
                    " 0 AS 'xDuoi30', 		" +
                    " 0 AS 'x31Den40', 		" +
                    " 0 AS 'x41Den50', 		" +
                    " 0 AS 'xNu51Den55', 		" +
                    " 0 AS 'xNam51Den55', 		" +
                    " 0 AS 'xNam56Den60', 		" +
                    " 0 AS 'xChuyenVienChinh', 		" +
                    " 0 AS 'xChuyenVien', 		" +
                    " 0 AS 'xCanSu', 		" +
                    " 0 AS 'xNgachConLai',		" +
                    " 0 AS 'xTHCS', 		" +
                    " 0 AS 'xTHPT', 		" +
                    " 0 AS 'xThacSi', 		" +
                    " 0 AS 'xDaiHoc', 		" +
                    " 0 AS 'xCaoDang',		" +
                    " 0 AS 'xTrungCap', 		" +
                    " 0 AS 'xTrinhDoChuyenMonConLai', 		" +
                    " 0 as 'xCuNhanChinhTri', 		" +
                    " 0 as 'xCaoCapChinhTri',		" +
                    " 0 as 'xTrungCapChinhTri', 		" +
                    " 0 as 'xDaiHocTinHoc',		" +
                    " 0 as 'xCaoDangTinHoc',		" +
                    " 0 as 'xTrungCapTinHoc',		" +
                    " 0 as 'xChungChiTinHoc', 		" +
                    " 0 as 'xDaiHocTiengAnh', 		" +
                    " 0 as 'xChungChiTiengAnh', 		" +
                    " 0 as 'xDaiHocNgoaiNguKhac', 		" +
                    " 0 as 'xChungChiNgoaiNguKhac'		" +
                    " INTO #tmpB 		" +
                    " FROM hr_Record h " +
                    "   LEFT JOIN cat_Folk cf on cf.Id = h.FolkId " +
                    "   LEFT JOIN cat_EmployeeType ce ON ce.Id = h.EmployeeTypeId " +
                    "   WHERE h.DepartmentId IN ({0}) ".FormatWith(departments) +
                    "   AND cf.IsMinority = '1' ";
                     if (!string.IsNullOrEmpty(employeeType))
                    {
                        sql += " AND ce.[Group] = '{0}' ".FormatWith(employeeType);
                    }
                    sql += " ORDER BY h.EmployeeTypeId 		" +
                    " UPDATE	#tmpB 	" +
                    " SET 		" +
                    " xTongSo = xwTongSo, 		" +
                    " xDangVien = xwDangVien, 		" +
                    " xDanTocThai = xwDanTocThai	, " +
                    " xDanTocMong = xwDanTocMong	, " +
                    " xDanTocHaNhi = xwDanTocHaNhi	, " +
                    " xDanTocTay = xwDanTocTay	, " +
                    " xDanTocMuong = xwDanTocMuong	, " +
                    " xDanTocDao = xwDanTocDao	, " +
                    " xDanTocGiay = xwDanTocGiay	, " +
                    " xDanTocCong = xwDanTocCong	, " +
                    " xDanTocHoa = xwDanTocHoa	, " +
                    " xDanTocSiLa = xwDanTocSiLa	, " +
                    " xDanTocNung = xwDanTocNung	, " +
                    " xDanTocCaoLan = xwDanTocCaoLan	, " +
                    " xDanTocLaHu = xwDanTocLaHu	, " +
                    " xDanTocTho = xwDanTocTho	, " +
                    " xDanTocKhac = xwDanTocKhac	, " +
                    " xNu = xwNu, 		" +
                    " xNam = xwNam, 		" +
                    " xDuoi30 = xwDuoi30, 		" +
                    " x31Den40 = xw31Den40, 		" +
                    " x41Den50 = xw41Den50, 		" +
                    " xNu51Den55 = xwNu51Den55, 		" +
                    " xNam51Den55 = xwNam51Den55, 		" +
                    " xNam56Den60 = xwNam56Den60, 		" +
                    " xThacSi = xwThacSi, 		" +
                    " xDaiHoc = xwDaiHoc, 		" +
                    " xCaoDang = xwCaoDang, 		" +
                    " xTrungCap = xwTrungCap, 		" +
                    " xTrinhDoChuyenMonConLai = xwTrinhDoChuyenMonConLai, 		" +
                    " xTHCS = xwTHCS, 		" +
                    " xTHPT = xwTHPT, 		" +
                    " xCuNhanChinhTri = xwCuNhanChinhTri,		" +
                    " xCaoCapChinhTri = xwCaoCapChinhTri, 		" +
                    " xTrungCapChinhTri = xwTrungCapChinhTri, 		" +
                    " xDaiHocTinHoc = xwDaiHocTinHoc, 		" +
                    " xCaoDangTinHoc = xwCaoDangTinHoc, 		" +
                    " xTrungCapTinHoc = xwTrungCapTinHoc, 		" +
                    " xChungChiTinHoc = xwChungChiTinHoc, 		" +
                    " xDaiHocTiengAnh = xwDaiHocTiengAnh, 		" +
                    " xChungChiTiengAnh = xwChungChiTiengAnh, 		" +
                    " xDaiHocNgoaiNguKhac = xwDaiHocNgoaiNguKhac, 		" +
                    " xChungChiNgoaiNguKhac = xwChungChiNgoaiNguKhac, 		" +
                    " xChuyenVienChinh = xwChuyenVienChinh, 		" +
                    " xChuyenVien = xwChuyenVien, 		" +
                    " xCanSu = xwCanSu, 		" +
                    " xNgachConLai = xwNgachConLai		" +
                    " FROM #tmpB 		" +
                    " INNER JOIN (SELECT 		" +
                    " SUM(1) AS xwTongSo, 		" +
                    " SUM(CASE WHEN #tmpB.DangVien = 'X' THEN 1 ELSE 0 END) AS xwDangVien, 		" +
                    " SUM(CASE WHEN #tmpB.DanTocThai = 'X' THEN 1 ELSE 0 END) AS xwDanTocThai , " +
                    " SUM(CASE WHEN #tmpB.DanTocMong = 'X' THEN 1 ELSE 0 END) AS xwDanTocMong , " +
                    " SUM(CASE WHEN #tmpB.DanTocHaNhi = 'X' THEN 1 ELSE 0 END) AS xwDanTocHaNhi , " +
                    " SUM(CASE WHEN #tmpB.DanTocTay = 'X' THEN 1 ELSE 0 END) AS xwDanTocTay , " +
                    " SUM(CASE WHEN #tmpB.DanTocMuong = 'X' THEN 1 ELSE 0 END) AS xwDanTocMuong , " +
                    " SUM(CASE WHEN #tmpB.DanTocDao = 'X' THEN 1 ELSE 0 END) AS xwDanTocDao , " +
                    " SUM(CASE WHEN #tmpB.DanTocGiay = 'X' THEN 1 ELSE 0 END) AS xwDanTocGiay , " +
                    " SUM(CASE WHEN #tmpB.DanTocCong = 'X' THEN 1 ELSE 0 END) AS xwDanTocCong , " +
                    " SUM(CASE WHEN #tmpB.DanTocHoa = 'X' THEN 1 ELSE 0 END) AS xwDanTocHoa , " +
                    " SUM(CASE WHEN #tmpB.DanTocSiLa = 'X' THEN 1 ELSE 0 END) AS xwDanTocSiLa , " +
                    " SUM(CASE WHEN #tmpB.DanTocNung = 'X' THEN 1 ELSE 0 END) AS xwDanTocNung , " +
                    " SUM(CASE WHEN #tmpB.DanTocCaoLan = 'X' THEN 1 ELSE 0 END) AS xwDanTocCaoLan , " +
                    " SUM(CASE WHEN #tmpB.DanTocLaHu = 'X' THEN 1 ELSE 0 END) AS xwDanTocLaHu , " +
                    " SUM(CASE WHEN #tmpB.DanTocTho = 'X' THEN 1 ELSE 0 END) AS xwDanTocTho , " +
                    " SUM(CASE WHEN #tmpB.DanTocKhac = 'X' THEN 1 ELSE 0 END) AS xwDanTocKhac , " +
                    " SUM(CASE WHEN #tmpB.Nu = 'X' THEN 1 ELSE 0 END) AS xwNu, 		" +
                    " SUM(CASE WHEN #tmpB.Nam = 'X' THEN 1 ELSE 0 END) AS xwNam, 		" +
                    " SUM(CASE WHEN #tmpB.Duoi30 = 'X' THEN 1 ELSE 0 END) AS xwDuoi30, 		" +
                    " SUM(CASE WHEN #tmpB.Tu31Den40 = 'X' THEN 1 ELSE 0 END) AS xw31Den40, 		" +
                    " SUM(CASE WHEN #tmpB.Tu41Den50 = 'X' THEN 1 ELSE 0 END) AS xw41Den50, 		" +
                    " SUM(CASE WHEN #tmpB.Nu51Den55 = 'X' THEN 1 ELSE 0 END) AS xwNu51Den55, 		" +
                    " SUM(CASE WHEN #tmpB.Nam51Den55 = 'X' THEN 1 ELSE 0 END) AS xwNam51Den55, 		" +
                    " SUM(CASE WHEN #tmpB.Nam56Den60 = 'X' THEN 1 ELSE 0 END) AS xwNam56Den60, 		" +
                    " SUM(CASE WHEN #tmpB.ThacSi = 'X' THEN 1 ELSE 0 END) AS xwThacSi, 		" +
                    " SUM(CASE WHEN #tmpB.DaiHoc = 'X' THEN 1 ELSE 0 END) AS xwDaiHoc, 		" +
                    " SUM(CASE WHEN #tmpB.CaoDang = 'X' THEN 1 ELSE 0 END) AS xwCaoDang, 		" +
                    " SUM(CASE WHEN #tmpB.TrungCap = 'X' THEN 1 ELSE 0 END) AS xwTrungCap, 		" +
                    " SUM(CASE WHEN #tmpB.TrinhDoChuyenMonConLai = 'X' THEN 1 ELSE 0 END) AS xwTrinhDoChuyenMonConLai, 		" +
                    " SUM(CASE WHEN #tmpB.THCS = 'X' THEN 1 ELSE 0 END) AS xwTHCS, 		" +
                    " SUM(CASE WHEN #tmpB.THPT = 'X' THEN 1 ELSE 0 END) AS xwTHPT, 		" +
                    " SUM(CASE WHEN #tmpB.CuNhanChinhTri = 'X' THEN 1 ELSE 0 END) AS xwCuNhanChinhTri, 		" +
                    " SUM(CASE WHEN #tmpB.CaoCapChinhTri = 'X' THEN 1 ELSE 0 END) AS xwCaoCapChinhTri, 		" +
                    " SUM(CASE WHEN #tmpB.TrungCapChinhTri = 'X' THEN 1 ELSE 0 END) AS xwTrungCapChinhTri, 		" +
                    " SUM(CASE WHEN #tmpB.DaiHocTinHoc = 'X' THEN 1 ELSE 0 END) AS xwDaiHocTinHoc, 		" +
                    " SUM(CASE WHEN #tmpB.CaoDangTinHoc = 'X' THEN 1 ELSE 0 END) AS xwCaoDangTinHoc, 		" +
                    " SUM(CASE WHEN #tmpB.TrungCapTinHoc = 'X' THEN 1 ELSE 0 END) AS xwTrungCapTinHoc, 		" +
                    " SUM(CASE WHEN #tmpB.ChungChiTinHoc = 'X' THEN 1 ELSE 0 END) AS xwChungChiTinHoc, 		" +
                    " SUM(CASE WHEN #tmpB.DaiHocTiengAnh = 'X' THEN 1 ELSE 0 END) AS xwDaiHocTiengAnh, 		" +
                    " SUM(CASE WHEN #tmpB.ChungChiTiengAnh = 'X' THEN 1 ELSE 0 END) AS xwChungChiTiengAnh, 		" +
                    " SUM(CASE WHEN #tmpB.DaiHocNgoaiNguKhac = 'X' THEN 1 ELSE 0 END) AS xwDaiHocNgoaiNguKhac, 		" +
                    " SUM(CASE WHEN #tmpB.ChungChiNgoaiNguKhac = 'X' THEN 1 ELSE 0 END) AS xwChungChiNgoaiNguKhac, 		" +
                    " SUM(CASE WHEN #tmpB.ChuyenVienChinh = 'X' THEN 1 ELSE 0 END) AS  xwChuyenVienChinh, 		" +
                    " SUM(CASE WHEN #tmpB.ChuyenVien = 'X' THEN 1 ELSE 0 END) AS xwChuyenVien, 		" +
                    " SUM(CASE WHEN #tmpB.CanSu = 'X' THEN 1 ELSE 0 END) AS xwCanSu, 		" +
                    " SUM(CASE WHEN #tmpB.NgachConLai = 'X' THEN 1 ELSE 0 END) AS xwNgachConLai		" +
                    " FROM #tmpB	" +
                    " ) AS TMP ON 1=1 	" +
                    " SELECT * FROM #tmpB " +
                    " ORDER BY EmployeeTypeId";

            return sql;
        }

        public static string GetStore_SLCLNguoiLamViecLaDanTocThieuSoCountGroupTotal(string departments, string employeeType)
        {
            var sql = string.Empty;
            sql += " IF OBJECT_ID('tempdb..#tmpBGroup') IS NOT NULL DROP Table #tmpBGroup 		" +
                "SELECT 		" +
                "h.EmployeeTypeId AS EmployeeTypeId,		" +
                "(SELECT TOP 1 emp.Name	FROM cat_EmployeeType emp WHERE emp.Id = h.EmployeeTypeId) AS TenLoaiCanBo,	" +
                "(SELECT dd.Name FROM cat_Department dd WHERE dd.Id = h.DepartmentId ) AS 'TenDonVi', 		" +
                "CASE WHEN (h.CPVOfficialJoinedDate IS NOT NULL) THEN 'X' ELSE '' END AS DangVien, 		" +
                 " CASE WHEN cf.Name = N'Thái' THEN 'X' ELSE '' END AS DanTocThai,	" +
                " CASE WHEN cf.Name = N'Mông' THEN 'X' ELSE '' END AS DanTocMong,	" +
                " CASE WHEN cf.Name = N'Hà Nhì' THEN 'X' ELSE '' END AS DanTocHaNhi,	" +
                " CASE WHEN cf.Name = N'Tày' THEN 'X' ELSE '' END AS DanTocTay,	" +
                " CASE WHEN cf.Name = N'Mường' THEN 'X' ELSE '' END AS DanTocMuong,	" +
                " CASE WHEN cf.Name = N'Dao' THEN 'X' ELSE '' END AS DanTocDao,	" +
                " CASE WHEN cf.Name = N'Giáy' THEN 'X' ELSE '' END AS DanTocGiay,	" +
                " CASE WHEN cf.Name = N'Cống' THEN 'X' ELSE '' END AS DanTocCong,	" +
                " CASE WHEN cf.Name = N'Hoa' THEN 'X' ELSE '' END AS DanTocHoa,	" +
                " CASE WHEN cf.Name = N'Si La' THEN 'X' ELSE '' END AS DanTocSiLa,	" +
                " CASE WHEN cf.Name = N'Nùng' THEN 'X' ELSE '' END AS DanTocNung,	" +
                " CASE WHEN cf.Name = N'Cao Lan' THEN 'X' ELSE '' END AS DanTocCaoLan,	" +
                " CASE WHEN cf.Name = N'La Hủ' THEN 'X' ELSE '' END AS DanTocLaHu,	" +
                " CASE WHEN cf.Name = N'Thổ' THEN 'X' ELSE '' END AS DanTocTho,	" +
                " CASE WHEN cf.Name not in( N'Giáy', N'Mông', N'Hà Nhì',N'Tày', N'Mường', N'Dao' , N'Giáy', N'Cống', N'Hoa', N'Si La',N'Nùng',N'Cao Lan',N'La Hủ', N'Thổ' ) THEN 'X' ELSE '' END AS DanTocKhac, " +
                "CASE WHEN h.Sex = '0' THEN 'X' ELSE '' END AS Nu, 		" +
                "CASE WHEN h.Sex = '1' THEN 'X' ELSE '' END AS Nam,	" +
                "CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 30) THEN 'X' ELSE '' END AS Duoi30, 		" +
                "CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 30 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 40) THEN 'X' ELSE '' END AS Tu31Den40, 		" +
                "CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 40 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 50) THEN 'X' ELSE '' END AS Tu41Den50, 		" +
                "CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 50 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 55 AND h.Sex = '0') THEN 'X' ELSE '' END AS Nu51Den55, 		" +
                "CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 50 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 55 AND h.Sex = '1') THEN 'X' ELSE '' END AS Nam51Den55,		" +
                "CASE WHEN (h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 55 AND DATEDIFF(YEAR, h.BirthDate, GETDATE()) <= 60 AND h.Sex = '1') THEN 'X' ELSE '' END AS Nam56Den60, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ManagementLevel ml WHERE ml.Id = h.ManagementLevelId) = 'CVC' THEN 'X' ELSE '' END AS ChuyenVienChinh, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ManagementLevel ml WHERE ml.Id = h.ManagementLevelId) = 'CV' THEN 'X' ELSE '' END AS ChuyenVien, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ManagementLevel ml WHERE ml.Id = h.ManagementLevelId) = 'CS' THEN 'X' ELSE '' END AS CanSu, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ManagementLevel ml WHERE ml.Id = h.ManagementLevelId) = 'CQDT' THEN 'X' ELSE '' END AS NgachConLai, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_BasicEducation WHERE Id = h.BasicEducationId) = 'THCS' THEN 'X' ELSE '' END AS THCS, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_BasicEducation WHERE Id = h.BasicEducationId) = 'THPT' THEN 'X' ELSE '' END AS THPT,		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = h.EducationId) = 'ThS' THEN 'X' ELSE '' END AS ThacSi, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = h.EducationId) = 'DH' THEN 'X' ELSE '' END AS DaiHoc, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = h.EducationId) = 'CD' THEN 'X' ELSE '' END AS CaoDang, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = h.EducationId) = 'TC' THEN 'X' ELSE '' END AS TrungCap, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = h.EducationId) = 'SC' OR (SELECT TOP 1 [Group] FROM cat_Education DT  WHERE DT.Id = H.EducationId) = 'TS' THEN 'X' ELSE '' END AS TrinhDoChuyenMonConLai, 		" +
                "CASE WHEN (SELECT TOP 1 Id FROM cat_PoliticLevel pl WHERE pl.Id = h.PoliticLevelId) = '4' THEN 'X' ELSE '' END AS CuNhanChinhTri, 		" +
                "CASE WHEN (SELECT TOP 1 Id FROM cat_PoliticLevel pl WHERE pl.Id = h.PoliticLevelId) = '3' THEN 'X' ELSE '' END AS CaoCapChinhTri, 		" +
                "CASE WHEN (SELECT TOP 1 Id FROM cat_PoliticLevel pl WHERE pl.Id = h.PoliticLevelId) = '2' THEN 'X' ELSE '' END AS TrungCapChinhTri, 		" +
                "'' AS ChuongTrinhChuyenVien,		" +
                "'' AS QLGD,		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ITLevel itl WHERE itl.Id = h.ITLevelId) = 'DH' THEN 'X' ELSE '' END AS DaiHocTinHoc, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ITLevel itl WHERE itl.Id = h.ITLevelId) = 'CD' THEN 'X' ELSE '' END AS CaoDangTinHoc, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ITLevel itl WHERE itl.Id = h.ITLevelId) = 'TC' THEN 'X' ELSE '' END AS TrungCapTinHoc, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_ITLevel itl WHERE itl.Id = h.ITLevelId) = 'CC' THEN 'X' ELSE '' END AS ChungChiTinHoc, 		" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = h.LanguageLevelId) = 'DHTA' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId  WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'DHTA') = 'DHTA' THEN 'X' ELSE '' END AS DaiHocTiengAnh,	" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = h.LanguageLevelId) = 'CCTA' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId  WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'CCTA') = 'CCTA' THEN 'X' ELSE '' END AS ChungChiTiengAnh,	" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = h.LanguageLevelId) = 'DHNNK' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'DHNNK') = 'DHNNK' THEN 'X' ELSE '' END AS DaiHocNgoaiNguKhac,	" +
                "CASE WHEN (SELECT TOP 1 [Group] FROM cat_LanguageLevel ll WHERE ll.Id = h.LanguageLevelId) = 'CCNNK' OR (SELECT DN.[Group] FROM cat_LanguageLevel DN LEFT JOIN hr_Language HN ON H.Id = HN.RecordId WHERE DN.Id = HN.LanguageId AND DN.[Group] = 'CCNNK') = 'CCNNK' THEN 'X' ELSE '' END AS ChungChiNgoaiNguKhac, 	" +
                " '' AS GhiChu " +
                "INTO #tmpBGroup 		" +
                "FROM hr_Record h 		" +
                "   LEFT JOIN cat_Folk cf on cf.Id = h.FolkId " +
                "   LEFT JOIN cat_EmployeeType ce ON ce.Id = h.EmployeeTypeId " +
                "WHERE h.DepartmentId IN ({0}) ".FormatWith(departments) +
                 "   AND cf.IsMinority = '1' ";
                if (!string.IsNullOrEmpty(employeeType))
                {
                    sql += " AND ce.[Group] = '{0}' ".FormatWith(employeeType);
                }
                sql += "ORDER BY h.EmployeeTypeId 		" +
                "SELECT  " +
                "MAX(#tmpBGroup.TenLoaiCanBo) AS TenLoaiCanBo, " +
                "SUM(CASE WHEN #tmpBGroup.DangVien = 'X' THEN 1 ELSE 0 END) AS xGroupDangVien,	" +
                 " SUM(CASE WHEN #tmpBGroup.DanTocThai = 'X' THEN 1 ELSE 0 END) AS xGroupDanTocThai , " +
                " SUM(CASE WHEN #tmpBGroup.DanTocMong = 'X' THEN 1 ELSE 0 END) AS xGroupDanTocMong , " +
                " SUM(CASE WHEN #tmpBGroup.DanTocHaNhi = 'X' THEN 1 ELSE 0 END) AS xGroupDanTocHaNhi , " +
                " SUM(CASE WHEN #tmpBGroup.DanTocTay = 'X' THEN 1 ELSE 0 END) AS xGroupDanTocTay , " +
                " SUM(CASE WHEN #tmpBGroup.DanTocMuong = 'X' THEN 1 ELSE 0 END) AS xGroupDanTocMuong , " +
                " SUM(CASE WHEN #tmpBGroup.DanTocDao = 'X' THEN 1 ELSE 0 END) AS xGroupDanTocDao , " +
                " SUM(CASE WHEN #tmpBGroup.DanTocGiay = 'X' THEN 1 ELSE 0 END) AS xGroupDanTocGiay , " +
                " SUM(CASE WHEN #tmpBGroup.DanTocCong = 'X' THEN 1 ELSE 0 END) AS xGroupDanTocCong , " +
                " SUM(CASE WHEN #tmpBGroup.DanTocHoa = 'X' THEN 1 ELSE 0 END) AS xGroupDanTocHoa , " +
                " SUM(CASE WHEN #tmpBGroup.DanTocSiLa = 'X' THEN 1 ELSE 0 END) AS xGroupDanTocSiLa , " +
                " SUM(CASE WHEN #tmpBGroup.DanTocNung = 'X' THEN 1 ELSE 0 END) AS xGroupDanTocNung , " +
                " SUM(CASE WHEN #tmpBGroup.DanTocCaoLan = 'X' THEN 1 ELSE 0 END) AS xGroupDanTocCaoLan , " +
                " SUM(CASE WHEN #tmpBGroup.DanTocLaHu = 'X' THEN 1 ELSE 0 END) AS xGroupDanTocLaHu , " +
                " SUM(CASE WHEN #tmpBGroup.DanTocTho = 'X' THEN 1 ELSE 0 END) AS xGroupDanTocTho , " +
                " SUM(CASE WHEN #tmpBGroup.DanTocKhac = 'X' THEN 1 ELSE 0 END) AS xGroupDanTocKhac , " +
                "SUM(CASE WHEN #tmpBGroup.Nu = 'X' THEN 1 ELSE 0 END) AS xGroupNu,	" +
                "SUM(CASE WHEN #tmpBGroup.Nam = 'X' THEN 1 ELSE 0 END) AS xGroupNam, 	" +
                "SUM(CASE WHEN #tmpBGroup.Duoi30 = 'X' THEN 1 ELSE 0 END) AS xGroupDuoi30, 	" +
                "SUM(CASE WHEN #tmpBGroup.Tu31Den40 = 'X' THEN 1 ELSE 0 END) AS xGroup31Den40, 	" +
                "SUM(CASE WHEN #tmpBGroup.Tu41Den50 = 'X' THEN 1 ELSE 0 END) AS xGroup41Den50, 	" +
                "SUM(CASE WHEN #tmpBGroup.Nu51Den55 = 'X' THEN 1 ELSE 0 END) AS xGroupNu51Den55, 	" +
                "SUM(CASE WHEN #tmpBGroup.Nam51Den55 = 'X' THEN 1 ELSE 0 END) AS xGroupNam51Den55, 	" +
                "SUM(CASE WHEN #tmpBGroup.Nam56Den60 = 'X' THEN 1 ELSE 0 END) AS xGroupNam56Den60, 	" +
                "SUM(CASE WHEN #tmpBGroup.ThacSi = 'X' THEN 1 ELSE 0 END) AS xGroupThacSi, 	" +
                "SUM(CASE WHEN #tmpBGroup.DaiHoc = 'X' THEN 1 ELSE 0 END) AS xGroupDaiHoc, 	" +
                "SUM(CASE WHEN #tmpBGroup.CaoDang = 'X' THEN 1 ELSE 0 END) AS xGroupCaoDang, 	" +
                "SUM(CASE WHEN #tmpBGroup.TrungCap = 'X' THEN 1 ELSE 0 END) AS xGroupTrungCap, 	" +
                "SUM(CASE WHEN #tmpBGroup.TrinhDoChuyenMonConLai = 'X' THEN 1 ELSE 0 END) AS xGroupTrinhDoChuyenMonConLai, 	" +
                "SUM(CASE WHEN #tmpBGroup.THCS = 'X' THEN 1 ELSE 0 END) AS xGroupTHCS, 	" +
                "SUM(CASE WHEN #tmpBGroup.THPT = 'X' THEN 1 ELSE 0 END) AS xGroupTHPT, 	" +
                "SUM(CASE WHEN #tmpBGroup.CuNhanChinhTri = 'X' THEN 1 ELSE 0 END) AS xGroupCuNhanChinhTri, 	" +
                "SUM(CASE WHEN #tmpBGroup.CaoCapChinhTri = 'X' THEN 1 ELSE 0 END) AS xGroupCaoCapChinhTri, 	" +
                "SUM(CASE WHEN #tmpBGroup.TrungCapChinhTri = 'X' THEN 1 ELSE 0 END) AS xGroupTrungCapChinhTri, 	" +
                "SUM(CASE WHEN #tmpBGroup.DaiHocTinHoc = 'X' THEN 1 ELSE 0 END) AS xGroupDaiHocTinHoc, 	" +
                "SUM(CASE WHEN #tmpBGroup.CaoDangTinHoc = 'X' THEN 1 ELSE 0 END) AS xGroupCaoDangTinHoc, 	" +
                "SUM(CASE WHEN #tmpBGroup.TrungCapTinHoc = 'X' THEN 1 ELSE 0 END) AS xGroupTrungCapTinHoc, 	" +
                "SUM(CASE WHEN #tmpBGroup.ChungChiTinHoc = 'X' THEN 1 ELSE 0 END) AS xGroupChungChiTinHoc, 	" +
                "SUM(CASE WHEN #tmpBGroup.DaiHocTiengAnh = 'X' THEN 1 ELSE 0 END) AS xGroupDaiHocTiengAnh, 	" +
                "SUM(CASE WHEN #tmpBGroup.ChungChiTiengAnh = 'X' THEN 1 ELSE 0 END) AS xGroupChungChiTiengAnh, 	" +
                "SUM(CASE WHEN #tmpBGroup.DaiHocNgoaiNguKhac = 'X' THEN 1 ELSE 0 END) AS xGroupDaiHocNgoaiNguKhac, 	" +
                "SUM(CASE WHEN #tmpBGroup.ChungChiNgoaiNguKhac = 'X' THEN 1 ELSE 0 END) AS xGroupChungChiNgoaiNguKhac, 	" +
                "SUM(CASE WHEN #tmpBGroup.ChuyenVienChinh = 'X' THEN 1 ELSE 0 END) AS  xGroupChuyenVienChinh, 	" +
                "SUM(CASE WHEN #tmpBGroup.ChuyenVien = 'X' THEN 1 ELSE 0 END) AS xGroupChuyenVien, 	" +
                "SUM(CASE WHEN #tmpBGroup.CanSu = 'X' THEN 1 ELSE 0 END) AS xGroupCanSu, 	" +
                "SUM(CASE WHEN #tmpBGroup.NgachConLai = 'X' THEN 1 ELSE 0 END) AS xGroupNgachConLai,	" +
                " MAX(#tmpBGroup.GhiChu) AS GhiChu " +
                "INTO #tmpC	" +
                "FROM #tmpBGroup	" +
                "GROUP BY #tmpBGroup.EmployeeTypeId	" +
                "SELECT * FROM #tmpC	 ";

            return sql;
        }

        #endregion

        public static string GetStore_QuantyDistrictDetail(string departments)
        {
            var sql = string.Empty;
            // Xoa bang #tmpB neu da ton tai
            sql += "IF OBJECT_ID('tempdb..#tmpB') IS NOT NULL DROP Table #tmpB ";
            // Tong hop
            sql += " select h.departmentId, h.fullname,d.Name as DepartmentName,h.RecruimentDepartment, " +
                    " case when h.Sex = '0' then 'X' end as Nu, " +
                    " case when h.CPVJoinedDate is not null then 'X' end as DangVien, " +
                    " case when dt.IsMinority = 1 then 'X'end as DanTocThieuSo ," +
                    " case when h.ReligionId IS NOT NULL and tg.Name like N'%Không%' then 'X' end as TonGiao ," +
                    " case when cm.[Group] = 'TS' then 'X' end as TienSi, " +
                    " case when cm.[Group] = 'ThS' then 'X' end as ThacSi," +
                    " case when cm.[Group] = 'DH' then 'X' end as DaiHoc," +
                    " case when cm.[Group] = 'CD' then 'X' end as CaoDang," +
                    " case when cm.[Group] = 'TC' then 'X' end as TrungCap," +
                    " case when cm.[Group] = 'SC' then 'X' end as SoCap," +
                    " case when dtc.[Group] = 'CN' then 'X' end as CuNhanChinhTri," +
                    " case when dtc.[Group] = 'CC' then 'X' end as CaoCapChinhTri," +
                    " case when dtc.[Group] = 'TC' then 'X' end as TrungCapChinhTri," +
                    " case when dtc.[Group] = 'SC' then 'X' end as SoCapChinhTri," +
                    " case when dth.[Group] = 'TC' then 'X' end as TrungCapTinHoc," +
                    " case when dth.[Group] = 'CC' then 'X' end as ChungChiTinHoc," +
                    " case when h.LanguageLevelId<> 0 and dnn.[Group] = 'DHTA' then 'X' end as DaiHocTiengAnh," +
                    " case when h.LanguageLevelId<> 0 and dnn.[Group] = 'CCTA' then 'X' end as ChungChiTiengAnh," +
                    " case when h.LanguageLevelId<> 0 and dnn.[Group] = 'DHNNK' then 'X' end as DaiHocNgoaiNguKhac," +
                    " case when h.LanguageLevelId<> 0 and dnn.[Group] = 'CCNNK' then 'X' end as ChungChiNgoaiNguKhac," +
                    " case when h.LanguageLevelId<> 0 and dnn.[Group] = 'CCTDT' then 'X' end as ChungChiTiengDanToc," +
                    " case when dtq.[Group] = 'CVCC' then 'X' end as ChuyenVienCaoCap," +
                    " case when dtq.[Group] = 'CVC' then 'X' end as ChuyenVienChinh," +
                    " case when dtq.[Group] = 'CV' then 'X' end as ChuyenVien," +
                    " case when h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE())<= 30 then 'X' end as Duoi30, " +
                    " case when h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE())> 30 AND DATEDIFF(YEAR, h.BirthDate, GETDATE())<= 40  then 'X' end as Tu31Den40, " +
                    " case when h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE())> 40 AND DATEDIFF(YEAR, h.BirthDate, GETDATE())<= 50  then 'X' end as Tu41Den50, " +
                    " case when h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE())> 50 AND DATEDIFF(YEAR, h.BirthDate, GETDATE())<= 55 AND h.Sex = '0' then 'X' end as Nu51Den55,   " +
                    " case when h.BirthDate IS NOT NULL AND DATEDIFF(YEAR, h.BirthDate, GETDATE())> 55 AND DATEDIFF(YEAR, h.BirthDate, GETDATE())<= 60 AND h.Sex = '1' then 'X' end as Nam56Den60,  " +
                    " case when h.BirthDate IS NOT NULL AND ((DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 55 AND h.Sex = '0') OR(DATEDIFF(YEAR, h.BirthDate, GETDATE()) > 60 AND h.Sex = '1')) then 'X' end as TrenTuoiNghiHuu,  " +
                    " case when s.QuantumId IN (SELECT dn.Id FROM cat_Quantum dn, cat_GroupQuantum nn WHERE dn.GroupQuantumId = nn.Id AND nn.[Group] = 'CVCC') then 'X' end as NgachCVCC,                  " +
                    " case when s.QuantumId IN (SELECT dn.Id FROM cat_Quantum dn, cat_GroupQuantum nn WHERE dn.GroupQuantumId = nn.Id AND nn.[Group] = 'CVC') then 'X' end as NgachCVC,                    " +
                    " case when s.QuantumId IN (SELECT dn.Id FROM cat_Quantum dn, cat_GroupQuantum nn WHERE dn.GroupQuantumId = nn.Id AND nn.[Group] = 'CV') then 'X' end as NgachCV,                          " +
                    " case when s.QuantumId IN (SELECT dn.Id FROM cat_Quantum dn, cat_GroupQuantum nn WHERE dn.GroupQuantumId = nn.Id AND nn.[Group] = 'CS') then 'X' end as NgachCS,                               " +
                    " case when s.QuantumId IN (SELECT dn.Id FROM cat_Quantum dn, cat_GroupQuantum nn WHERE dn.GroupQuantumId = nn.Id AND nn.[Group] = 'NV') then 'X' end as NgachNV,                             " +
                  
                   // Trong do
                   "0 AS 'xNu', " +
                   "0 AS 'xDangVien', " +
                   "0 AS 'xDanTocThieuSo', " +
                   "0 AS 'xTonGiao', " +
                   // Trinh do chuyen mon
                   "0 AS 'xTienSi', " +
                   "0 AS 'xThacSi', " +
                   "0 AS 'xDaiHoc', " +
                   "0 AS 'xCaoDang'," +
                   "0 AS 'xTrungCap', " +
                   "0 AS 'xSoCap', " +
                   // Trinh do chinh tri
                   "0 as 'xCuNhanChinhTri', " +
                   "0 as 'xCaoCapChinhTri', " +
                   "0 as 'xTrungCapChinhTri', " +
                   "0 as 'xSoCapChinhTri', " +
                   // Trinh do tin hoc
                   "0 as 'xTrungCapTinHoc', " +
                   "0 as 'xChungChiTinHoc', " +
                   // Trinh do tieng Anh
                   "0 as 'xDaiHocTiengAnh', " +
                   "0 as 'xChungChiTiengAnh', " +
                   // Trinh do ngoai ngu khac
                   "0 as 'xDaiHocNgoaiNguKhac', " +
                   "0 as 'xChungChiNgoaiNguKhac', " +
                   //Chung chi tieng dan toc
                   "0 as 'xChungChiTiengDanToc', " +
                   // Cap bac QLNN
                   "0 AS 'xChuyenVienCaoCap', " +
                   "0 AS 'xChuyenVienChinh', " +
                   "0 AS 'xChuyenVien', " +
                   // Do tuoi
                   "0 AS 'xDuoi30', " +
                   "0 AS 'x31Den40', " +
                   "0 AS 'x41Den50', " +
                   "0 AS 'xNu51Den55', " +
                   "0 AS 'xNam56Den60', " +
                   "0 AS 'xTrenTuoiNghiHuu', " +
                   // Ngach CCVC
                   "0 AS 'xNgachCVCC', " +
                   "0 AS 'xNgachCVC', " +
                   "0 AS 'xNgachCV', " +
                   "0 AS 'xNgachCS', " +
                   "0 AS 'xNgachNV' " +
                   // Day du lieu vao bang #tmpB
                   "INTO #tmpB " +

                    " from hr_Record h " +
                    " left join cat_Folk dt ON dt.Id = h.FolkId " +
                    "left join cat_Department d on h.DepartmentId = d.Id " +
                    " left join cat_Religion tg on tg.id = h.ReligionId " +
                    " LEFT JOIN cat_Education cm ON dt.Id = h.EducationId " +
                    " LEFT JOIN cat_PoliticLevel dtc on dtc.Id = h.PoliticLevelId " +
                    " LEFT JOIN cat_ITLevel dth on dth.Id = h.ITLevelId " +
                    " LEFT JOIN cat_LanguageLevel dnn ON dnn.Id = h.LanguageLevelId " +
                    " LEFT JOIN(SELECT RecordId, QuantumId FROM sal_SalaryDecision GROUP BY RecordId, QuantumId) s ON h.Id = s.RecordId " +
                    " LEFT JOIN cat_ManagementLevel dtq ON dtq.Id = h.ManagementLevelId " +
                    " WHERE h.DepartmentId IN ({0})".FormatWith(departments);

            // Tinh tong
            sql += "UPDATE	#tmpB " +
                    "SET " +
                    // Trong do
                    "xNu = xwNu, " +
                    "xDangVien = xwDangVien, " +
                    "xDanTocThieuSo = xwDanTocThieuSo, " +
                    "xTonGiao = xwTonGiao, " +
                    // Tring do chuyen mon
                    "xTienSi = xwTienSi, " +
                    "xThacSi = xwThacSi, " +
                    "xDaiHoc = xwDaiHoc, " +
                    "xCaoDang = xwCaoDang, " +
                    "xTrungCap = xwTrungCap, " +
                    "xSoCap = xwSoCap, " +
                    // Trinh do chinh tri
                    "xCuNhanChinhTri = xwCuNhanChinhTri, " +
                    "xCaoCapChinhTri = xwCaoCapChinhTri, " +
                    "xTrungCapChinhTri = xwTrungCapChinhTri, " +
                    "xSoCapChinhTri = xwSocapChinhTri, " +
                    // Trinh do tin hoc
                    "xTrungCapTinHoc = xwTrungCapTinHoc, " +
                    "xChungChiTinHoc = xwChungChiTinHoc, " +
                    // Trinh do tieng anh
                    "xDaiHocTiengAnh = xwDaiHocTiengAnh, " +
                    "xChungChiTiengAnh = xwChungChiTiengAnh, " +
                    // Trinh do ngoai ngu khac
                    "xDaiHocNgoaiNguKhac = xwDaiHocNgoaiNguKhac, " +
                    "xChungChiNgoaiNguKhac = xwChungChiNgoaiNguKhac, " +
                    //Chung chi tieng dan toc
                    "xChungChiTiengDanToc = xwChungChiTiengDanToc, " +
                    // Trinh do QLNN
                    "xChuyenVienCaoCap = xwChuyenVienCaoCap, " +
                    "xChuyenVienChinh = xwChuyenVienChinh, " +
                    "xChuyenVien = xwChuyenVien, " +
                    // Do tuoi
                    "xDuoi30 = xwDuoi30, " +
                    "x31Den40 = xw31Den40, " +
                    "x41Den50 = xw41Den50, " +
                    "xNu51Den55 = xwNu51Den55, " +
                    "xNam56Den60 = xwNam56Den60, " +
                    "xTrenTuoiNghiHuu = xwTrenTuoiNghiHuu, " +
                    // Ngach CCVC
                    "xNgachCVCC = xwNgachCVCC, " +
                    "xNgachCVC = xwNgachCVC, " +
                    "xNgachCV = xwNgachCV, " +
                    "xNgachCS = xwNgachCS, " +
                    "xNgachNV = xwNgachNV " +
                    //
                    // From table
                    //
                    "FROM " +
                    "#tmpB " +
                    //
                    // Join table
                    "INNER JOIN " +
                        "(SELECT " +
                        // Trong do
                        "SUM(CASE WHEN #tmpB.Nu = 'X' THEN 1 ELSE 0 END) AS xwNu, " +
                        "SUM(CASE WHEN #tmpB.DangVien = 'X' THEN 1 ELSE 0 END) AS xwDangVien, " +
                        "SUM(CASE WHEN #tmpB.DanTocThieuSo  = 'X' THEN 1 ELSE 0  END) AS xwDanTocThieuSo, " +
                        "SUM(CASE WHEN #tmpB.TonGiao = 'X' THEN 0 ELSE 0 END) AS xwTongiao, " +
                        // Trinh do chuyen mon
                        "SUM(CASE WHEN #tmpB.TienSi = 'X' THEN 1 ELSE 0 END) AS xwTienSi, " +
                        "SUM(CASE WHEN #tmpB.ThacSi = 'X' THEN 1 ELSE 0 END) AS xwThacSi, " +
                        "SUM(CASE WHEN #tmpB.DaiHoc = 'X' THEN 1 ELSE 0 END) AS xwDaiHoc, " +
                        "SUM(CASE WHEN #tmpB.CaoDang = 'X' THEN 1 ELSE 0 END) AS xwCaoDang, " +
                        "SUM(CASE WHEN #tmpB.TrungCap = 'X' THEN 1 ELSE 0 END ) AS xwTrungCap, " +
                        "SUM(CASE WHEN #tmpB.SoCap = 'X' THEN 1 ELSE  0 END) AS xwSoCap, " +
                        // Trinh do chinh tri
                        "SUM(CASE WHEN #tmpB.CuNhanChinhTri = 'X' THEN 1 ELSE 0 END) AS xwCuNhanChinhTri, " +
                        "SUM(CASE WHEN #tmpB.CaoCapChinhTri = 'X' THEN 1 ELSE 0 END) AS xwCaoCapChinhTri, " +
                        "SUM(CASE WHEN #tmpB.TrungCapChinhTri = 'X' THEN 1 ELSE 0 END) AS xwTrungCapChinhTri, " +
                        "SUM(CASE WHEN #tmpB.SoCapChinhTri = 'X' THEN 1 ELSE 0 END) AS xwSoCapChinhTri, " +
                        // Trinh do tin hoc
                        "SUM(CASE WHEN #tmpB.TrungCapTinHoc = 'X' THEN 1 ELSE 0 END) AS xwTrungCapTinHoc, " +
                        "SUM(CASE WHEN #tmpB.ChungChiTinHoc = 'X' THEN 1 ELSE 0 END )AS xwChungChiTinHoc, " +
                        // Trinh do tieng anh
                        "SUM(CASE WHEN #tmpB.DaiHocTiengAnh = 'X' THEN 1 ELSE 0 END) AS xwDaiHocTiengAnh, " +
                        "SUM(CASE WHEN #tmpB.ChungChiTiengAnh = 'X' THEN 1 ELSE 0 END ) AS xwChungChiTiengAnh, " +
                        // Trinh do ngoai ngu khac
                        "SUM(CASE WHEN #tmpB.DaiHocNgoaiNguKhac = 'X' THEN 1 ELSE 0 END) AS xwDaiHocNgoaiNguKhac, " +
                        "SUM(CASE WHEN #tmpB.ChungChiNgoaiNguKhac = 'X' THEN 1 ELSE 0 END) AS xwChungChiNgoaiNguKhac, " +
                         //Chung chi tieng dan toc
                         "SUM(CASE WHEN #tmpB.ChungChiTiengDanToc = 'X' THEN 1 ELSE 0 END) AS xwChungChiTiengDanToc, " +
                        // Trinh do QLNN
                        "SUM(CASE WHEN #tmpB.ChuyenVienCaoCap = 'X' THEN 1 ELSE 0 END) AS xwChuyenVienCaoCap, " +
                        "SUM(CASE WHEN #tmpB.ChuyenVienChinh = 'X' THEN 1 ELSE 0  END) AS  xwChuyenVienChinh, " +
                        "SUM(CASE WHEN #tmpB.ChuyenVien = 'X' THEN 1 ELSE 0  END) AS xwChuyenVien, " +
                        // Do tuoi
                        "SUM(CASE WHEN #tmpB.Duoi30 = 'X' THEN 1 ELSE 0 END) AS xwDuoi30, " +
                        "SUM(CASE WHEN #tmpB.Tu31Den40 = 'X' THEN 1 ELSE 0 END) AS xw31Den40, " +
                        "SUM(CASE WHEN #tmpB.Tu41Den50 = 'X' THEN 1 ELSE 0 END) AS xw41Den50, " +
                        "SUM(CASE WHEN #tmpB.Nu51Den55 = 'X' THEN 1 ELSE 0 END) AS xwNu51Den55, " +
                        "SUM(CASE WHEN #tmpB.Nam56Den60 = 'X' THEN 1 ELSE 0 END) AS xwNam56Den60, " +
                        "SUM(CASE WHEN #tmpB.TrenTuoiNghiHuu = 'X' THEN 1 ELSE 0 END) AS xwTrenTuoiNghiHuu, " +
                        // Ngach CCVC
                        "SUM(CASE WHEN #tmpB.NgachCVCC = 'X' THEN 1 ELSE 0  END) AS xwNgachCVCC, " +
                        "SUM(CASE WHEN #tmpB.NgachCVC = 'X' THEN 1 ELSE  0 END) AS xwNgachCVC, " +
                        "SUM(CASE WHEN #tmpB.NgachCV = 'X' THEN 1 ELSE 0 END) AS xwNgachCV, " +
                        "SUM(CASE WHEN #tmpB.NgachCS = 'X' THEN 1 ELSE 0  END) AS xwNgachCS, " +
                        "SUM(CASE WHEN #tmpB.NgachNV = 'X' THEN 1 ELSE 0 END) AS xwNgachNV " +
                        //
                        // From table
                        //
                        "FROM #tmpB) AS TMP " +
                        " ON 1=1 ";
                        // select all from #tmpB
                        sql += "SELECT * FROM #tmpB ";

            // return
            return sql;
        }
    }
}
