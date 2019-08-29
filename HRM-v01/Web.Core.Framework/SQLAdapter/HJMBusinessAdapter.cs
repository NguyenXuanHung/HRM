using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.Framework.SQLAdapter
{
    public class HJMBusinessAdapter
    {
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
                   " hs.DepartmentId, " +
                   " dmdv.Name AS 'DepartmentName', " +
                   " '' AS 'STT', " +
                   " hs.EmployeeCode AS 'EmployeeCode', " +
                   " hs.FullName AS 'FullName', " +
                   " hs.BirthDate AS 'BirthDate', " +
                   " DATEDIFF(YEAR, hs.BirthDate, GETDATE()) AS 'Age', " +
                   " CASE WHEN hs.Sex = '1' THEN 'Nam' " +
                   "       ELSE N'Nữ' " +
                   "       END " +
                   "   AS 'SexName', " +
                   " hs.Address AS 'Address', " +
                   " dmcv.Name AS 'JobTitleName', " +
                   " cv.Name AS 'PositionName', " +
                   " (select top 1 cq.Name from cat_Quantum cq, hr_Salary hr where cq.Id = hr.QuantumId and hr.RecordId = hs.Id) AS 'QuantumName', " +
                   " (select top 1 cq.Code from cat_Quantum cq, hr_Salary hr where cq.Id = hr.QuantumId and hr.RecordId = hs.Id) AS 'QuantumCode', " +
                   " (select top 1 hr.SalaryGrade from cat_Quantum cq, hr_Salary hr where cq.Id = hr.QuantumId and hr.RecordId = hs.Id) AS 'SalaryGrade', " +
                   " (select top 1 hr.SalaryFactor from hr_Salary hr where hr.RecordId = hs.Id order by hr.DecisionDate desc) AS 'SalaryFactor', " +
                   " (select top 1 hr.PositionAllowance from hr_Salary hr where hr.RecordId = hs.Id order by hr.DecisionDate desc) AS 'PositionAllowance', " +
                   " (select top 1 hr.OtherAllowance from hr_Salary hr where hr.RecordId = hs.Id order by hr.DecisionDate desc) AS 'OtherAllowance', " +
                   " td.Name AS 'EducationName', " +
                   " (select top 1 hr.ContractNumber from hr_Contract hr where hr.RecordId = hs.Id order by hr.ContractDate desc) AS 'ContractNumber', " +
                   " ct.Name AS 'ContractTypeName', " +
                   " (select top 1 hr.ContractDate from hr_Contract hr where hr.RecordId = hs.Id order by hr.ContractDate desc) AS 'ContractDate' " +

                   " FROM hr_Record hs " +
                   "   LEFT JOIN hr_Contract hd " +
                   "       ON hs.Id = hd.RecordId " +
                   "   LEFT JOIN cat_ContractType ct" +
                   "       ON ct.Id = hd.ContractTypeId" +
                   "   LEFT JOIN cat_JobTitle dmcv " +
                   "       ON hs.JobTitleId = dmcv.Id " +
                   "   LEFT JOIN cat_Position cv " +
                   "       ON hs.PositionId = cv.Id " +
                   "   LEFT JOIN hr_Salary hrs" +
                   "       ON hrs.RecordId = hs.Id" +
                   "   LEFT JOIN cat_Education td " +
                   "       ON hs.EducationId = td.Id " +
                   "   LEFT JOIN cat_Department dmdv " +
                   "       ON hs.DepartmentId = dmdv.Id ";

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
                   "rc.DepartmentId AS 'DepartmentId', " +
                   "DD.Name AS 'DepartmentName', " +
                   "rc.EmployeeCode AS 'EmployeeCode', " +
                   "rc.FullName AS 'FullName', " +
                   "DD.Name AS 'CurrentDepartment',	 " +
                   "DC.Name AS 'CurrentPosition', " +
                   "hc.ContractNumber AS 'ContractNumber',	 " +
                   "hc.ContractDate AS 'ContractDate', " +
                   "LHD.Name AS 'ContractTypeName', " +
                   "LHD.ContractMonth AS 'ContractTime',	 " +
                   "hc.PersonRepresent AS 'Signer', " +
                   "DC1.Name AS 'PositionSigner', " +
                   "LHD.ContractMonth - DATEDIFF(month, hc.ContractDate, getDate()) AS 'ContractTimeRemain' " +
                   "   INTO #tmpB " +
                   "FROM hr_Contract hc " +
                   "LEFT JOIN cat_ContractType LHD " +
                   " ON LHD.Id = hc.ContractTypeId " +
                   "LEFT JOIN hr_Record rc " +
                   " 	ON rc.Id = hc.RecordId " +
                   " LEFT JOIN cat_Position DC " +
                   " 	ON DC.Id = rc.PositionId " +
                   " LEFT JOIN cat_Department DD " +
                   " 	ON DD.Id = rc.DepartmentId " +
                   " LEFT JOIN cat_Position DC1 " +
                   " 	ON DC1.Id = hc.PersonPositionId " +
                   " WHERE rc.DepartmentId IN ({0}) ".FormatWith(department);
            if(!string.IsNullOrEmpty(condition))
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
                   "   h.EmployeeCode AS 'MaCanBo', " +
                   "   h.DepartmentId AS 'MaDonVi', " +
                   " h.FullName AS 'HoTen', " +
                   " h.BirthDate AS 'NgaySinh', " +
                   " (SELECT [Name] FROM cat_Department dd WHERE dd.Id = h.DepartmentId) AS 'DonViCongTac', " +
                   " qtdc.DecisionNumber AS 'SoQuyetDinh', " +
                   " qtdc.DecisionDate AS 'NgayQuyetDinh', " +
                   " qtdc.EffectiveDate AS 'NgayHieuLuc', " +
                   " (SELECT TOP 1 dc.Name FROM cat_Position dc WHERE dc.Id = qtdc.NewPositionId) AS 'ChucVuBoNhiem', " +
                   " qtdc.ExpireDate AS 'ThoiHanBoNhiem',	 " +
                   " qtdc.SourceDepartment AS 'CoQuanBoNhiem', " +
                   " qtdc.DecisionMaker AS 'NguoiKy', " +
                   " qtdc.MakerPosition AS 'MakerPosition', " +
                   "   (SELECT dp.Name FROM cat_Department dp WHERE dp.Id = h.DepartmentId) AS 'GROUP' " +
                   " INTO #tmpB " +
                   " FROM hr_Record h, " +
                   " 	hr_WorkProcess qtdc " +
                   " WHERE h.Id = qtdc.RecordId " +
                   " 	AND h.DepartmentId IN ({0}) ".FormatWith(department);
            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
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
            var sql = "SELECT  hs.EmployeeCode, " +
                      " hs.FullName," +
                      " hs.DepartmentId AS 'DepartmentId', " +
                      " dv.Name AS 'GROUP', " +
                      " dv.Name AS DepartmentName," +
                      " lhd.Name AS ContractTypeName," +
                      " lhd.ContractMonth, " +
                      " hshd.ContractNumber," +
                      " hshd.ContractDate," +
                      " hshd.ContractEndDate," +
                      " hshd.PersonRepresent," +
                      " lhd.ContractMonth - DATEDIFF(month, hshd.ContractDate, getDate()) AS 'ThoiHanConLai', " +
                      " dc.Name AS PositionName," +
                      " (SELECT TOP 1 cp.Name FROM cat_Position cp WHERE cp.Id = hshd.PersonPositionId) AS ChucVuNguoiKy " +
                      "   FROM hr_Contract hshd " +
                      "       LEFT JOIN hr_Record hs" +
                      " ON hshd.RecordId = hs.Id" +
                      "       LEFT JOIN cat_ContractType lhd" +
                      " ON  lhd.Id = hshd.ContractTypeId" +
                      "       LEFT JOIN cat_Department dv" +
                      " ON  dv.Id = hs.DepartmentId" +
                      "       LEFT JOIN cat_Position dc" +
                      " ON dc.Id = hs.PositionId" +
                      "   WHERE (DATEDIFF(DD, hshd.ContractEndDate,GETDATE()) > 0" +
                      " AND DATEDIFF(DD,hshd.ContractEndDate,GETDATE()) <= {0})".FormatWith(day) +
                      " and DATEDIFF(MONTH, hshd.ContractEndDate, GETDATE()) <= 3" +
                      " AND hs.WorkStatusId = (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Đang làm việc%')" +
                      " AND ISNULL(ContractEndDate, '0001-01-01') <> '0001-01-01'" +
                      " AND hs.DepartmentId IN ({0})".FormatWith(department);
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
                   " hs.DepartmentId AS 'MaDonVi', " +
                   " dmdv.Name AS 'TenDonVi', " +
                   " '' AS 'STT', " +
                   " hs.EmployeeCode AS 'SoHieuCCVC', " +
                   " hs.FullName AS 'HoTen', " +
                   " hs.BirthDate AS 'SinhNgay', " +
                   " DATEDIFF(YEAR, hs.BirthDate, GETDATE()) AS 'Tuoi', " +
                   " CASE WHEN hs.Sex = '1' THEN 'Nam' " +
                   "       ELSE N'Nữ' " +
                   "       END " +
                   "   AS 'GioiTinh', " +
                   " hs.Address AS 'DiaChiThuongTru', " +
                   " dmcv.Name AS 'ChucDanh', " +
                   " cv.Name AS 'ChucVu', " +
                   " (select top 1 cq.Name from cat_Quantum cq, hr_Salary hr where cq.Id = hr.QuantumId and hr.RecordId = hs.Id) AS 'TenNgach', " +
                   " (select top 1 cq.Code from cat_Quantum cq, hr_Salary hr where cq.Id = hr.QuantumId and hr.RecordId = hs.Id) AS 'MaNgach', " +
                   " (select top 1 hr.SalaryGrade from cat_Quantum cq, hr_Salary hr where cq.Id = hr.QuantumId and hr.RecordId = hs.Id) AS 'BacLuong', " +
                   " (select top 1 hr.SalaryFactor from hr_Salary hr where hr.RecordId = hs.Id order by hr.DecisionDate desc) AS 'HeSo', " +
                   " (select top 1 hr.PositionAllowance from hr_Salary hr where hr.RecordId = hs.Id order by hr.DecisionDate desc) AS 'PhuCapChucVu', " +
                   " (select top 1 hr.OtherAllowance from hr_Salary hr where hr.RecordId = hs.Id order by hr.DecisionDate desc) AS 'PhuCapKhac', " +
                   " td.Name AS 'TrinhDoChuyenMon', " +
                   " (select top 1 hr.ContractNumber from hr_Contract hr where hr.RecordId = hs.Id order by hr.ContractDate desc) AS 'SoHopDong', " +
                   " ct.Name AS 'LoaiHopDong', " +
                   " (select top 1 hr.ContractDate from hr_Contract hr where hr.RecordId = hs.Id order by hr.ContractDate desc) AS 'NgayKy' " +

                   " FROM hr_Record hs " +
                   "   LEFT JOIN hr_Contract hd " +
                   "       ON hs.Id = hd.RecordId " +
                   "   LEFT JOIN cat_JobTitle dmcv " +
                   "       ON hs.JobTitleId = dmcv.Id " +
                   "   LEFT JOIN cat_Position cv " +
                   "       ON hs.PositionId = cv.Id " +
                   "   LEFT JOIN cat_ContractType ct" +
                   "       ON ct.Id = hd.ContractTypeId" +
                   "   LEFT JOIN cat_Education td " +
                   "       ON hs.EducationId = td.Id " +
                   "   LEFT JOIN cat_Department dmdv " +
                   "       ON hs.DepartmentId = dmdv.Id " +
                   "   WHERE hs.DepartmentId IN ({0})".FormatWith(department);
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
        public static string GetStore_EmployeeMoveTo(string departments, string fromDate, string toDate, string businessType, string condition)
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
            if(!string.IsNullOrEmpty(condition))
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
                   " H.DepartmentId AS 'DepartmentId', " +
                   " DV.Name AS 'GROUP', " +
                   " H.EmployeeCode AS 'EmployeeCode', " +
                   " H.FullName AS 'FullName',   " +
                   " H.BirthDate AS 'BirthDate',  " +
                   " DATEDIFF(YEAR, H.BirthDate, GETDATE()) AS 'Age',   " +
                   " CASE WHEN H.Sex = '0' THEN N'Nữ' ELSE N'Nam' END AS 'SexName', " +
                   " H.ResidentPlace AS 'ResidentPlace', " +
                   " cv.Name AS 'JobTitleName',   " +
                   " dcv.Name AS 'PositionName', " +
                   " DCD.Name AS 'CPVPositionName', " +
                   " CVD.Name AS 'VYUPositionName',  " +
                   " CM.Name AS 'EducationName',  " +
                   " CT.Name AS 'PoliticlLevelName',  " +
                   " dtq.Name AS 'ManagementLevelName',  " +
                   " NN.Name AS 'LanguageLevelName',  " +
                   " TH.Name AS 'ITLevelName', " +
                   " H.RecruimentDate AS 'RecruimentDate',  " +
                   " HD.ContractNumber AS 'ContractNumber', " +
                   " LHD.Name AS 'ContractTypeName', " +
                   " HD.ContractDate AS 'ContractDate',	 ";
            if(string.IsNullOrEmpty(typeReport))
            {
                //Case of CCVC
                sql += "   CASE WHEN (DATEDIFF(DAY, H.FunctionaryDate, getDate())/365) = 0 THEN '' ELSE " +
                       "   CAST(DATEDIFF(DAY, H.FunctionaryDate, getDate())/365 AS NVARCHAR(10)) + N' Năm ' END + " +
                       "   CASE WHEN ((DATEDIFF(DAY, H.FunctionaryDate, getDate())%365) /30) = 0 THEN '' ELSE " +
                       "   CAST((DATEDIFF(DAY, H.FunctionaryDate, getDate())%365) /30 AS NVARCHAR(10)) + N' Tháng ' END + " +
                       "   CASE WHEN ((DATEDIFF(DAY, H.FunctionaryDate, getDate())%365) %30) = 0 THEN '' ELSE " +
                       "   CAST((DATEDIFF(DAY, H.FunctionaryDate, getDate())%365) %30 AS NVARCHAR(10)) + N' Ngày' END AS 'Seniority' ";
            }
            else
            {
                //Case of enterprise
                sql += "   CASE WHEN (DATEDIFF(DAY, H.ParticipationDate, getDate())/365) = 0 THEN '' ELSE " +
                       "   CAST(DATEDIFF(DAY, H.ParticipationDate, getDate())/365 AS NVARCHAR(10)) + N' Năm ' END + " +
                       "   CASE WHEN ((DATEDIFF(DAY, H.ParticipationDate, getDate())%365) /30) = 0 THEN '' ELSE " +
                       "   CAST((DATEDIFF(DAY, H.ParticipationDate, getDate())%365) /30 AS NVARCHAR(10)) + N' Tháng ' END + " +
                       "   CASE WHEN ((DATEDIFF(DAY, H.ParticipationDate, getDate())%365) %30) = 0 THEN '' ELSE " +
                       "   CAST((DATEDIFF(DAY, H.ParticipationDate, getDate())%365) %30 AS NVARCHAR(10)) + N' Ngày' END AS 'Seniority' ";
            }

            sql += " FROM hr_Record H  	 " +
                   " LEFT JOIN cat_Folk DT   " +
                   "     ON DT.Id = H.FolkId   " +
                   " LEFT JOIN cat_JobTitle cv   " +
                   "     ON cv.Id = H.JobTitleId   " +
                   " LEFT JOIN cat_Position DCV  	 " +
                   "     ON DCV.Id = H.PositionId  " +
                   " LEFT JOIN cat_Department DV  	 " +
                   "     ON DV.Id = H.DepartmentId   " +
                   " LEFT JOIN cat_Education CM   " +
                   "     ON CM.Id = H.EducationId   " +
                   " LEFT JOIN cat_PoliticLevel CT   " +
                   "     ON CT.Id = H.PoliticLevelId   " +
                   " LEFT JOIN cat_ManagementLevel dtq  	 " +
                   "     ON dtq.Id = H.ManagementLevelId  " +
                   " LEFT JOIN cat_LanguageLevel NN  		 " +
                   "     ON NN.Id = H.LanguageLevelId  	 " +
                   " LEFT JOIN cat_CPVPosition DCD  	 " +
                   "     ON DCD.Id = H.CPVPositionId   " +
                   " LEFT JOIN cat_VYUPosition CVD	 " +
                   " 	ON CVD.Id = H.VYUPositionId	 " +
                   " LEFT JOIN cat_ITLevel TH		 " +
                   " 	ON TH.Id = H.ITLevelId " +
                   " LEFT JOIN hr_Contract HD	 " +
                   " 	ON HD.RecordId = H.Id	 " +
                   " LEFT JOIN cat_ContractType LHD		 " +
                   " 	ON LHD.Id = HD.ContractTypeId	 " +
                   " WHERE H.DepartmentId IN ({0}) ".FormatWith(departments);
            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            sql += " ORDER BY H.DepartmentId, H.FullName	";

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
            if(!string.IsNullOrEmpty(condition))
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
                   " AND rc.WorkStatusId = (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Đang làm việc%')";
            if(!string.IsNullOrEmpty(condition))
            {
                sql += " AND {0}".FormatWith(condition);
            }
            return sql;
        }
    }
}
