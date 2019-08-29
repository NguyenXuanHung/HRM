using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    public class RecordController
    {
        #region Variables

        private static readonly string _ConnectionString = ConfigurationManager.ConnectionStrings["StandardConfig"].ConnectionString;
        private static readonly string _SelectColumns = "rc.Id," +
                                                        "ISNULL(rc.DepartmentId, 0) AS DepartmentId," +
                                                        "ISNULL(rc.ManagementDepartmentId, 0) AS ManagementDepartmentId," +
                                                        "ISNULL(rc.EmployeeCode, '') AS EmployeeCode," +
                                                        "ISNULL(rc.FunctionaryCode, '') AS FunctionaryCode," +
                                                        "ISNULL(rc.[Name], '') AS [Name]," +
                                                        "ISNULL(rc.FullName, '') AS FullName," +
                                                        "ISNULL(rc.Alias, '') AS Alias," +
                                                        "rc.BirthDate," +
                                                        "ISNULL(rc.Sex, 1) AS Sex," +
                                                        "ISNULL(rc.MaritalStatusId, 0) AS MaritalStatusId," +
                                                        "ISNULL(rc.BirthPlace, '') AS BirthPlace," +
                                                        "ISNULL(rc.BirthPlaceWardId, 0) AS BirthPlaceWardId," +
                                                        "ISNULL(rc.BirthPlaceDistrictId, 0) AS BirthPlaceDistrictId," +
                                                        "ISNULL(rc.BirthPlaceProvinceId, 0) AS BirthPlaceProvinceId," +
                                                        "ISNULL(rc.Hometown, '') AS Hometown," +
                                                        "ISNULL(rc.HometownWardId, 0) AS HometownWardId," +
                                                        "ISNULL(rc.HometownDistrictId, 0) AS HometownDistrictId," +
                                                        "ISNULL(rc.HometownProvinceId, 0) AS HometownProvinceId," +
                                                        "ISNULL(rc.ImageUrl, '') AS ImageUrl," +
                                                        "ISNULL(rc.OriginalFile, '') AS OriginalFile," +
                                                        "ISNULL(rc.FolkId, 0) AS FolkId," +
                                                        "ISNULL(rc.ReligionId, 0) AS ReligionId," +
                                                        "ISNULL(rc.PersonalClassId, 0) AS PersonalClassId," +
                                                        "ISNULL(rc.FamilyClassId, 0) AS FamilyClassId," +
                                                        "ISNULL(rc.ResidentPlace, '') AS ResidentPlace," +
                                                        "ISNULL(rc.[Address], '') AS [Address]," +
                                                        "ISNULL(rc.PreviousJob, '') AS PreviousJob," +
                                                        "ISNULL(rc.RecruimentDepartment, '') AS RecruimentDepartment," +
                                                        "rc.RecruimentDate," +
                                                        "rc.ParticipationDate," +
                                                        "rc.FunctionaryDate," +
                                                        "ISNULL(rc.PositionId, 0) AS PositionId," +
                                                        "ISNULL(rc.JobTitleId, 0) AS JobTitleId," +
                                                        "ISNULL(rc.AssignedWork, '') AS AssignedWork," +
                                                        "ISNULL(rc.BasicEducationId, 0) AS BasicEducationId," +
                                                        "ISNULL(rc.EducationId, 0) AS EducationId," +
                                                        "ISNULL(rc.PoliticLevelId, 0) AS PoliticLevelId," +
                                                        "ISNULL(rc.ManagementLevelId, 0) AS ManagementLevelId," +
                                                        "ISNULL(rc.LanguageLevelId, 0) AS LanguageLevelId," +
                                                        "ISNULL(rc.ITLevelId, 0) AS ITLevelId," +
                                                        "rc.CPVJoinedDate," +
                                                        "rc.CPVOfficialJoinedDate," +
                                                        "ISNULL(rc.CPVCardNumber, '') AS CPVCardNumber," +
                                                        "ISNULL(rc.CPVPositionId, 0) AS CPVPositionId," +
                                                        "ISNULL(rc.CPVJoinedPlace, '') AS CPVJoinedPlace," +
                                                        "rc.VYUJoinedDate," +
                                                        "ISNULL(rc.VYUPositionId, 0) AS VYUPositionId," +
                                                        "ISNULL(rc.VYUJoinedPlace, '') AS VYUJoinedPlace," +
                                                        "rc.ArmyJoinedDate," +
                                                        "rc.ArmyLeftDate," +
                                                        "ISNULL(rc.ArmyLevelId, 0) AS ArmyLevelId," +
                                                        "ISNULL(rc.TitleAwarded, '') AS TitleAwarded," +
                                                        "ISNULL(rc.Skills, '') AS Skills," +
                                                        "ISNULL(rc.HealthStatusId, 0) AS HealthStatusId," +
                                                        "ISNULL(rc.BloodGroup, '') AS BloodGroup," +
                                                        "ISNULL(rc.[Height], 0) AS [Height]," +
                                                        "ISNULL(rc.[Weight], 0) AS [Weight]," +
                                                        "ISNULL(rc.RankWounded, '') AS RankWounded," +
                                                        "ISNULL(rc.FamilyPolicyId, 0) AS FamilyPolicyId," +
                                                        "ISNULL(rc.IDNumber, '') AS IDNumber," +
                                                        "rc.IDIssueDate," +
                                                        "ISNULL(rc.IDIssuePlaceId, 0) AS IDIssuePlaceId," +
                                                        "ISNULL(rc.InsuranceNumber, '') AS InsuranceNumber," +
                                                        "ISNULL(rc.HealthInsuranceNumber, '') AS HealthInsuranceNumber," +
                                                        "rc.InsuranceIssueDate," +
                                                        "rc.HealthJoinedDate," +
                                                        "rc.HealthExpiredDate," +
                                                        "ISNULL(rc.PersonalTaxCode, '') AS PersonalTaxCode," +
                                                        "ISNULL(rc.WorkStatusId, 0) AS WorkStatusId," +
                                                        "ISNULL(rc.Status, 0) AS Status," +
                                                        "ISNULL(rc.CellPhoneNumber, '') AS CellPhoneNumber," +
                                                        "ISNULL(rc.HomePhoneNumber, '') AS HomePhoneNumber," +
                                                        "ISNULL(rc.WorkPhoneNumber, '') AS WorkPhoneNumber," +
                                                        "ISNULL(rc.WorkEmail, '') AS WorkEmail," +
                                                        "ISNULL(rc.PersonalEmail, '') AS PersonalEmail," +
                                                        "ISNULL(rc.Biography, '') AS Biography," +
                                                        "ISNULL(rc.ForeignOrganizationJoined, '') AS ForeignOrganizationJoined," +
                                                        "ISNULL(rc.RelativesAboard, '') AS RelativesAboard," +
                                                        "ISNULL(rc.Review, '') AS Review," +
                                                        "ISNULL(rc.ContactPersonName, '') AS ContactPersonName," +
                                                        "ISNULL(rc.ContactRelation, '') AS ContactRelation," +
                                                        "ISNULL(rc.ContactPhoneNumber, '') AS ContactPhoneNumber," +
                                                        "ISNULL(rc.ContactAddress, '') AS ContactAddress," +
                                                        "rc.WorkStatusDate," +
                                                        "ISNULL(rc.WorkStatusReason, '') AS WorkStatusReason," +
                                                        "ISNULL(rc.EmployeeTypeId, 0) AS EmployeeTypeId," +
                                                        "ISNULL(rc.IndustryId, 0) AS IndustryId," +
                                                        "ISNULL(rc.FamilyIncome, 0) AS FamilyIncome," +
                                                        "ISNULL(rc.OtherIncome, '') AS OtherIncome," +
                                                        "ISNULL(rc.AllocatedHouse, '') AS AllocatedHouse," +
                                                        "ISNULL(rc.AllocatedHouseArea, 0) AS AllocatedHouseArea," +
                                                        "ISNULL(rc.House, '') AS House," +
                                                        "ISNULL(rc.HouseArea, 0) AS HouseArea," +
                                                        "ISNULL(rc.AllocatedLandArea, 0) AS AllocatedLandArea," +
                                                        "ISNULL(rc.BusinessLandArea, 0) AS BusinessLandArea," +
                                                        "ISNULL(rc.LandArea, 0) AS LandArea," +
                                                        "ISNULL(rc.GovernmentPositionId, 0) AS GovernmentPositionId," +
                                                        "ISNULL(rc.PluralityPositionId, 0) AS PluralityPositionId," +
                                                        "rc.RevolutionJoinedDate," +
                                                        "ISNULL(rc.LongestJob, '') AS LongestJob," +
                                                        "ISNULL(rc.IsDeleted, 0) AS IsDeleted," +
                                                        "ISNULL(rc.CreatedBy, '') AS CreatedBy," +
                                                        "rc.CreatedDate," +
                                                        "ISNULL(rc.EditedBy, '') AS EditedBy," +
                                                        "rc.EditedDate," +
                                                        "ISNULL(de.[Name], '') AS DepartmentName," +
                                                        "ISNULL(mde.[Name], '') AS ManagementDepartmentName," +
                                                        "ISNULL(ms.[Name], '') AS MaritalStatusName," +
                                                        "ISNULL(lbw.[Name], '') AS BirthPlaceWardName," +
                                                        "ISNULL(lbd.[Name], '') AS BirthPlaceDistrictName," +
                                                        "ISNULL(lbp.[Name], '') AS BirthPlaceProvinceName," +
                                                        "ISNULL(lhw.[Name], '') AS HometownWardName," +
                                                        "ISNULL(lhd.[Name], '') AS HometownDistrictName," +
                                                        "ISNULL(lhp.[Name], '') AS HometownProvinceName," +
                                                        "ISNULL(fo.[Name], '') AS FolkName," +
                                                        "ISNULL(re.[Name], '') AS ReligionName," +
                                                        "ISNULL(pc.[Name], '') AS PersonalClassName," +
                                                        "ISNULL(fc.[Name], '') AS FamilyClassName," +
                                                        "ISNULL(po.[Name], '') AS PositionName," +
                                                        "ISNULL(jt.[Name], '') AS JobTitleName," +
                                                        "ISNULL(be.[Name], '') AS BasicEducationName," +
                                                        "ISNULL(edu.[Name], '') AS EducationName," +
                                                        "ISNULL(pl.[Name], '') AS PoliticLevelName," +
                                                        "ISNULL(ml.[Name], '') AS ManagementLevelName," +
                                                        "ISNULL(ll.[Name], '') AS LanguageLevelName," +
                                                        "ISNULL(il.[Name], '') AS ITLevelName," +
                                                        "ISNULL(cp.[Name], '') AS CPVPositionName," +
                                                        "ISNULL(vp.[Name], '') AS VYUPositionName," +
                                                        "ISNULL(al.[Name], '') AS ArmyLevelName," +
                                                        "ISNULL(hs.[Name], '') AS HealthStatusName," +
                                                        "ISNULL(fp.[Name], '') AS FamilyPolicyName," +
                                                        "ISNULL(idp.[Name], '') AS IDIssuePlaceName," +
                                                        "ISNULL(ws.[Name], '') AS WorkStatusName," +
                                                        "ISNULL(et.[Name], '') AS EmployeeTypeName," +
                                                        "ISNULL(ind.[Name], '') AS IndustryName," +
                                                        "ISNULL(gp.[Name], '') AS GovernmentPositionName," +
                                                        "ISNULL(pp.[Name], '') AS PluralityPositionName," +
                                                        "ISNULL(hbat.[AccountName], '') AS AccountName," +
                                                        "ISNULL(hbat.[AccountNumber], '') AS AccountNumber," +
                                                        "ISNULL(ba.[Name], '') AS BankName," +
                                                        "ISNULL(sdt.[Factor], 0) AS SalaryFactor," +
                                                        "sdt.[EffectiveDate] AS EffectiveDate," +
                                                        " ISNULL(rc.UniversityId, 0) AS UniversityId, " +
                                                        " rc.GraduationYear, " +
                                                        " ISNULL(rc.GraduationTypeId, 0) AS GraduationTypeId, " +
                                                        "ISNULL(qa.[Name], '') AS QuantumName," +
                                                        "ISNULL(qa.[Code], '') AS QuantumCode";
        private static readonly string _Tables = "hr_Record rc " +
                                                    "LEFT JOIN cat_Department de on rc.DepartmentId = de.Id " +
                                                    "LEFT JOIN cat_Department mde on rc.ManagementDepartmentId = mde.Id " +
                                                    "LEFT JOIN cat_MaritalStatus ms on rc.MaritalStatusId = ms.Id " +
                                                    "LEFT JOIN cat_Location lbw on rc.BirthPlaceWardId = lbw.Id " +
                                                    "LEFT JOIN cat_Location lbd on rc.BirthPlaceDistrictId = lbd.Id " +
                                                    "LEFT JOIN cat_Location lbp on rc.BirthPlaceProvinceId = lbp.Id " +
                                                    "LEFT JOIN cat_Location lhw on rc.HometownWardId = lhw.Id " +
                                                    "LEFT JOIN cat_Location lhd on rc.HometownDistrictId = lhd.Id " +
                                                    "LEFT JOIN cat_Location lhp on rc.HometownProvinceId = lhp.Id " +
                                                    "LEFT JOIN cat_Folk fo on rc.FolkId = fo.Id " +
                                                    "LEFT JOIN cat_Religion re on rc.ReligionId = re.Id " +
                                                    "LEFT JOIN cat_PersonalClass pc on rc.PersonalClassId = pc.Id " +
                                                    "LEFT JOIN cat_FamilyClass fc on rc.FamilyClassId = fc.Id " +
                                                    "LEFT JOIN cat_Position po on rc.PositionId = po.Id " +
                                                    "LEFT JOIN cat_JobTitle jt on rc.JobTitleId = jt.Id " +
                                                    "LEFT JOIN cat_BasicEducation be on rc.BasicEducationId = be.Id " +
                                                    "LEFT JOIN cat_Education edu on rc.EducationId = edu.Id " +
                                                    "LEFT JOIN cat_PoliceLevel pl on rc.PoliticLevelId = pl.Id " +
                                                    "LEFT JOIN cat_ManagementLevel ml on rc.ManagementLevelId = ml.Id " +
                                                    "LEFT JOIN cat_LanguageLevel ll on rc.LanguageLevelId = ll.Id " +
                                                    "LEFT JOIN cat_ITLevel il on rc.ITLevelId = il.Id " +
                                                    "LEFT JOIN cat_CPVPosition cp on rc.CPVPositionId = cp.Id " +
                                                    "LEFT JOIN cat_VYUPosition vp on rc.VYUPositionId = vp.Id " +
                                                    "LEFT JOIN cat_ArmyLevel al on rc.ArmyLevelId = al.Id " +
                                                    "LEFT JOIN cat_HealthStatus hs on rc.HealthStatusId = hs.Id " +
                                                    "LEFT JOIN cat_FamilyPolicy fp on rc.FamilyPolicyId = fp.Id " +
                                                    "LEFT JOIN cat_IDIssuePlace idp on rc.IDIssuePlaceId = idp.Id " +
                                                    "LEFT JOIN cat_WorkStatus ws on rc.WorkStatusId = ws.Id " +
                                                    "LEFT JOIN cat_EmployeeType et on rc.EmployeeTypeId = et.Id " +
                                                    "LEFT JOIN cat_Industry ind on rc.IndustryId = ind.Id " +
                                                    "LEFT JOIN cat_Position gp on rc.GovernmentPositionId = gp.Id " +
                                                    "LEFT JOIN cat_Position pp on rc.PluralityPositionId = pp.Id " +
                                                    "OUTER APPLY (SELECT TOP 1 * FROM hr_Bank hba (NOLOCK) WHERE rc.[Id] = hba.[RecordId] and hba.[IsInUsed] = 1 ORDER BY hba.Id DESC) as hbat " +
                                                    "LEFT JOIN [cat_Bank] ba ON hbat.[BankId] = ba.[Id] " +
                                                    "OUTER APPLY (SELECT TOP 1 * FROM sal_SalaryDecision sd (NOLOCK) WHERE rc.[Id] = sd.[RecordId] AND sd.[Status] = {0} AND sd.[EffectiveDate] IS NOT NULL AND sd.[EffectiveDate] < getdate() ORDER BY sd.[EffectiveDate] DESC) AS sdt ".FormatWith((int)SalaryDecisionStatus.Approved) +
                                                    "LEFT JOIN [cat_Quantum] qa ON sdt.[QuantumId] = qa.[Id]";

        #endregion

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static RecordModel GetById(int id)
        {
            // get result
            var result = Search(null, " AND rc.[Id]='{0}'".FormatWith(id), null, null, null, 0, 1);

            // return
            return result.Total > 0 ? result.Data[0] : null;
        }

        /// <summary>
        /// Get by code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="departmentIds"></param>
        /// <returns></returns>
        public static RecordModel GetByCode(string code, string departmentIds)
        {
            // get result
            var result = Search(null, " AND rc.[EmployeeCode]='{0}'".FormatWith(code.EscapeQuote()), departmentIds, null, null, 0, 1);
            
            // return
            return result.Total > 0 ? result.Data[0] : null;
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="filter"></param>
        /// <param name="departmentIds"></param>
        /// <param name="type"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<RecordModel> GetAll(string keyword, string filter, string departmentIds, RecordType? type, string order, int? limit)
        {
            // search result
            var result = Search(keyword, filter, departmentIds, type, order, null, null);

            // return data
            return result.Data;
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="filter"></param>
        /// <param name="departmentIds"></param>
        /// <param name="type"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<RecordModel> GetPaging(string keyword, string filter, string departmentIds, RecordType? type, string order, int start, int limit)
        {
            // return data
            return Search(keyword, filter, departmentIds, type, order, start, limit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<RecordModel> GetInsurance(string keyword, string departmentIds, DateTime? fromDate, DateTime? toDate, string order,
            int start, int limit)
        {
            var condition = " AND ISNULL(rc.[InsuranceNumber], '') != ''";

            if (!string.IsNullOrEmpty(keyword))
                condition += " AND (rc.[Name] LIKE N'%{0}%' OR rc.[FullName] LIKE N'%{0}%' OR rc.[EmployeeCode] LIKE N'%{0}%' OR rc.[InsuranceNumber] LIKE N'%{0}%' OR rc.[HealthInsuranceNumber] LIKE N'%{0}%')"
                    .FormatWith(keyword);

            return Search(null, condition, departmentIds, null, order, start, limit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="filter"></param>
        /// <param name="departmentIds"></param>
        /// <param name="type"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private static PageResult<RecordModel> Search(string keyword, string filter, string departmentIds, RecordType? type, string order, int? start, int? limit)
        {
            // init condition
            var condition = "1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
                condition +=
                    @" AND (rc.[Name] LIKE N'%{0}%' OR rc.[FullName] LIKE N'%{0}%' OR rc.[EmployeeCode] LIKE N'%{0}%' OR rc.[FunctionaryCode] LIKE N'%{0}%')"
                        .FormatWith(keyword.EscapeQuote());

            // filter condition
            if(!string.IsNullOrEmpty(filter))
                condition += filter;

            // departments
            if(!string.IsNullOrEmpty(departmentIds))
                condition += @" AND rc.[DepartmentId] IN ({0})".FormatWith(departmentIds);

            // record type
            if (type != null)
                condition += @" AND rc.[Type] = {0}".FormatWith((int) type);

            // count total
            var countQuery = @"SELECT COUNT(*) AS TOTAL FROM [hr_Record] rc WHERE {0}".FormatWith(condition);
            var objCount = SQLHelper.ExecuteScalar(countQuery);
            var total = (int?)objCount ?? 0;

            // init return value
            var models = new List<RecordModel>();

            // init reader
            SqlDataReader dr = null;

            // init connection string
            var conn = new SqlConnection(_ConnectionString);
            try
            {
                // init start, limit
                if(start == null || start < 0) start = 0;
                if(limit == null || limit < 0) limit = total;

                // init order
                if (string.IsNullOrEmpty(order)) order = "rc.[FullName]";

                // int get paging sql
                var sql = "SELECT * FROM (SELECT {0}, ROW_NUMBER() OVER (ORDER BY {1}) as row_number FROM {2} WHERE {3}) t0 WHERE t0.row_number>{4} AND  t0.row_number<={5}"
                        .FormatWith(_SelectColumns, order.EscapeQuote(), _Tables, condition, start, start + limit);

                // init command
                var command = new SqlCommand
                {
                    Connection = conn,
                    CommandText = sql,
                    CommandType = CommandType.Text
                };

                // open connection
                conn.Open();

                // execute reader
                dr = command.ExecuteReader();
                while(dr.Read())
                {
                    // fill object
                    var model = FillObject(dr);

                    // add to return list
                    models.Add(model);
                }

                // return
                return new PageResult<RecordModel>(total, models);
            }
            catch
            {
                // return blank result
                return new PageResult<RecordModel>(0, new List<RecordModel>());
            }
            finally
            {
                // close reader
                dr.Close();

                // close connection
                if(conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }


        /// <summary>
        /// get list record for handler record
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="searchKey"></param>
        /// <param name="query"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static DataTable GetAllListRecords(string departments, string searchKey, string query, string order, int? start, int? limit)
        {
            return hr_RecordServices.GetListRecords(departments, RecordType.Default, searchKey, query, order, start, limit);
        }

        /// <summary>
        /// get paging
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="searchQuery"></param>
        /// <param name="query"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PageResult<RecordModel> GetPaging(string departments, string searchQuery, string query, string order, int? start, int? pageSize)
        {
            var lstRecordModel = new List<RecordModel>();

            var hrPageResult = hr_RecordServices.GetPaging(departments, searchQuery, query, order, start, pageSize);

            if(hrPageResult.Data.Count > 0)
            {
                lstRecordModel.AddRange(hrPageResult.Data.Select(record => new RecordModel(record)));
            }

            return new PageResult<RecordModel>(hrPageResult.Total, lstRecordModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="departments"></param>
        /// <returns></returns>
        public static List<RecordModel> GetByName(string name, string departments)
        {
            //init search query
            var searchQuery = @" [FullName] like N'%{0}%'".FormatWith(name.EscapeQuote());
            if(!string.IsNullOrEmpty(departments))
            {
                searchQuery += @" AND [DepartmentId] IN ({0}) ".FormatWith(departments);
            }

            var result = hr_RecordServices.GetAll(searchQuery);
            return result.Select(record => new RecordModel(record)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        public static List<RecordModel> GetAll(string searchQuery)
        {
            var records = hr_RecordServices.GetAll(searchQuery);
            return records.Select(record => new RecordModel(record)).ToList();

        }

        #region Report

        /// <summary>
        /// Danh sách cán bộ sinh nhật trong tháng
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="condition"></param>
        /// <param name="month"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PageResult<RecordModel> GetEmployeeHasBirthOfDateInMonth(string departments, string condition, string month, string order, int? start, int? pageSize)
        {
            // init search query
            var searchQuery = @" AND [BirthDate] IS NOT NULL AND MONTH([BirthDate])='{0}'".FormatWith(month);
            searchQuery += " AND [WorkStatusId] = (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Đang làm việc%') ";
            if(!string.IsNullOrEmpty(condition))
            {
                searchQuery += " AND {0}".FormatWith(condition);
            }
            // init order
            order = "DAY([BirthDate])";
            return GetPaging(departments, searchQuery, null, order, start, pageSize);
        }

        public static List<RecordModel> GetAllEmployeeInOrganization(string departments)
        {
            //init condition
            const string condition = "";
            // add order 
            const string order = "[DepartmentId], [FullName]";

            var result = hr_RecordServices.GetAllEmployee(departments, condition, order, null);

            return result.Select(record => new RecordModel(record)).ToList();
        }

        /// <summary>
        /// Danh sách cán bộ là Đoàn viên
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static List<RecordModel> GetEmployeeIsVietNamYoungUnion(string departments, string sql)
        {
            //init condition
            var condition = @" AND [VYUJoinedDate] IS NOT NULL ";
            if(!string.IsNullOrEmpty(sql))
            {
                condition += " AND {0}".FormatWith(sql);

            }
            // add order 
            const string order = "[DepartmentId], [FullName]";

            var result = hr_RecordServices.GetAllEmployee(departments, condition, order, null);

            return result.Select(record => new RecordModel(record)).ToList();
        }

        /// <summary>
        /// Danh sách cán bộ là quân nhân
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="condition"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PageResult<RecordModel> GetEmployeeIsMilitary(string departments, string fromDate, string toDate, string condition, string order, int? start, int? pageSize)
        {
            //init search query
            var searchQuery = @" AND ArmyJoinedDate IS NOT NULL";
            if(!string.IsNullOrEmpty(fromDate))
            {
                searchQuery += " AND [ArmyJoinedDate] >= '{0}'".FormatWith(fromDate);

            }
            if(!string.IsNullOrEmpty(toDate))
            {
                searchQuery += " AND [ArmyJoinedDate] <= '{0}'".FormatWith(toDate);

            }
            if(!string.IsNullOrEmpty(condition))
            {
                searchQuery += " AND {0}".FormatWith(condition);

            }
            //init order
            order = "DepartmentId, EmployeeCode";
            return GetPaging(departments, searchQuery, null, order, start, pageSize);
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        public static RecordModel GetByEmployeeCode(string employeeCode)
        {
            var condition = " [EmployeeCode] = N'{0}'".FormatWith(employeeCode);
            var record = hr_RecordServices.GetByCondition(condition);
            return new RecordModel(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static RecordModel GetByEmployeeCodeGenerate(string prefix, int number)
        {
            var condition = " [EmployeeCode] LIKE N'%{0}%'".FormatWith(prefix);
            condition += @" AND LEN([EmployeeCode]) = (LEN('{0}') + {1}) ".FormatWith(prefix, number);
            const string order = " [EmployeeCode] DESC";
            var record = hr_RecordServices.GetByCondition(condition, order);
            return new RecordModel(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static RecordModel Create(RecordModel model)
        {
            // init entity
            var entity = new hr_Record();

            // set entity props
            model.FillEntity(ref entity);

            // insert
            return new RecordModel(hr_RecordServices.Create(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public static RecordModel Update(RecordModel model)
        {
            // get entity
            var record = hr_RecordServices.GetById(model.Id);

            // check ojbect return
            if (record != null)
            {
                // set new properties
                model.FillEntity(ref record);

                // update
                return new RecordModel(hr_RecordServices.Update(record));
            }

            // invalid param
            return null;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="idNumber"></param>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        public static List<RecordModel> CheckExistIDNumberAndEmployeeCode(int? recordId, string idNumber, string employeeCode)
        {
            var recordModels = new List<RecordModel>();
            var records = hr_RecordServices.GetIDNumberAndEmployeeCode(recordId, idNumber, employeeCode);
            foreach(var record in records)
            {
                recordModels.Add(new RecordModel(record));
            }
            return recordModels;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            hr_RecordServices.Delete(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <param name="query"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PageResult<RecordModel> GetPagingSearchUserReport(string searchQuery, string query, string order, int start, int pageSize)
        {
            var lstRecordModel = new List<RecordModel>();

            var hrPageResult = hr_RecordServices.GetPagingForReport(searchQuery, query, order, start, pageSize);

            if(hrPageResult.Data.Count > 0)
            {
                foreach(var record in hrPageResult.Data)
                {
                    lstRecordModel.Add(new RecordModel(record));
                }
            }

            return new PageResult<RecordModel>(hrPageResult.Total, lstRecordModel);
        }

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static RecordModel FillObject(IDataRecord dr)
        {
            try
            {
                var model = new RecordModel
                {
                    // entity props
                    Id = (int) dr["Id"],
                    DepartmentId = (int) dr["DepartmentId"],
                    ManagementDepartmentId = (int) dr["ManagementDepartmentId"],
                    EmployeeCode = (string) dr["EmployeeCode"],
                    FunctionaryCode = (string) dr["FunctionaryCode"],
                    Name = (string) dr["Name"],
                    FullName = (string) dr["FullName"],
                    Alias = (string) dr["Alias"],
                    Sex = (bool) dr["Sex"],
                    MaritalStatusId = (int) dr["MaritalStatusId"],
                    BirthPlace = (string) dr["BirthPlace"],
                    BirthPlaceWardId = (int) dr["BirthPlaceWardId"],
                    BirthPlaceDistrictId = (int) dr["BirthPlaceDistrictId"],
                    BirthPlaceProvinceId = (int) dr["BirthPlaceProvinceId"],
                    Hometown = (string) dr["Hometown"],
                    HometownWardId = (int) dr["HometownWardId"],
                    HometownDistrictId = (int) dr["HometownDistrictId"],
                    HometownProvinceId = (int) dr["HometownProvinceId"],
                    ImageUrl = (string) dr["ImageUrl"],
                    OriginalFile = (string) dr["OriginalFile"],
                    FolkId = (int) dr["FolkId"],
                    ReligionId = (int) dr["ReligionId"],
                    PersonalClassId = (int) dr["PersonalClassId"],
                    FamilyClassId = (int) dr["FamilyClassId"],
                    ResidentPlace = (string) dr["ResidentPlace"],
                    Address = (string) dr["Address"],
                    PreviousJob = (string) dr["PreviousJob"],
                    RecruimentDepartment = (string) dr["RecruimentDepartment"],
                    PositionId = (int) dr["PositionId"],
                    JobTitleId = (int) dr["JobTitleId"],
                    AssignedWork = (string) dr["AssignedWork"],
                    BasicEducationId = (int) dr["BasicEducationId"],
                    EducationId = (int) dr["EducationId"],
                    PoliticLevelId = (int) dr["PoliticLevelId"],
                    ManagementLevelId = (int) dr["ManagementLevelId"],
                    LanguageLevelId = (int) dr["LanguageLevelId"],
                    ITLevelId = (int) dr["ITLevelId"],
                    CPVCardNumber = (string) dr["CPVCardNumber"],
                    CPVPositionId = (int) dr["CPVPositionId"],
                    CPVJoinedPlace = (string) dr["CPVJoinedPlace"],
                    VYUPositionId = (int) dr["VYUPositionId"],
                    VYUJoinedPlace = (string) dr["VYUJoinedPlace"],
                    ArmyLevelId = (int) dr["ArmyLevelId"],
                    TitleAwarded = (string) dr["TitleAwarded"],
                    Skills = (string) dr["Skills"],
                    HealthStatusId = (int) dr["HealthStatusId"],
                    BloodGroup = (string) dr["BloodGroup"],
                    Height = (decimal) dr["Height"],
                    Weight = (decimal) dr["Weight"],
                    RankWounded = (string) dr["RankWounded"],
                    FamilyPolicyId = (int) dr["FamilyPolicyId"],
                    IDNumber = (string) dr["IDNumber"],
                    IDIssuePlaceId = (int) dr["IDIssuePlaceId"],
                    InsuranceNumber = (string) dr["InsuranceNumber"],
                    HealthInsuranceNumber = (string) dr["HealthInsuranceNumber"],
                    PersonalTaxCode = (string) dr["PersonalTaxCode"],
                    WorkStatusId = (int) dr["WorkStatusId"],
                    Status = (RecordStatus) (int) dr["Status"],
                    CellPhoneNumber = (string) dr["CellPhoneNumber"],
                    HomePhoneNumber = (string) dr["HomePhoneNumber"],
                    WorkPhoneNumber = (string) dr["WorkPhoneNumber"],
                    WorkEmail = (string) dr["WorkEmail"],
                    PersonalEmail = (string) dr["PersonalEmail"],
                    Biography = (string) dr["Biography"],
                    ForeignOrganizationJoined = (string) dr["ForeignOrganizationJoined"],
                    RelativesAboard = (string) dr["RelativesAboard"],
                    Review = (string) dr["Review"],
                    ContactPersonName = (string) dr["ContactPersonName"],
                    ContactRelation = (string) dr["ContactRelation"],
                    ContactPhoneNumber = (string) dr["ContactPhoneNumber"],
                    ContactAddress = (string) dr["ContactAddress"],
                    WorkStatusReason = (string) dr["WorkStatusReason"],
                    EmployeeTypeId = (int) dr["EmployeeTypeId"],
                    IndustryId = (int) dr["IndustryId"],
                    FamilyIncome = (decimal) dr["FamilyIncome"],
                    OtherIncome = (string) dr["OtherIncome"],
                    AllocatedHouse = (string) dr["AllocatedHouse"],
                    AllocatedHouseArea = (decimal) dr["AllocatedHouseArea"],
                    House = (string) dr["House"],
                    HouseArea = (decimal) dr["HouseArea"],
                    AllocatedLandArea = (decimal) dr["AllocatedLandArea"],
                    BusinessLandArea = (decimal) dr["BusinessLandArea"],
                    LandArea = (decimal) dr["LandArea"],
                    GovernmentPositionId = (int) dr["GovernmentPositionId"],
                    PluralityPositionId = (int) dr["PluralityPositionId"],
                    LongestJob = (string) dr["LongestJob"],
                    IsDeleted = (bool) dr["IsDeleted"],
                    CreatedBy = (string) dr["CreatedBy"],
                    CreatedDate = (DateTime) dr["CreatedDate"],
                    EditedBy = (string) dr["EditedBy"],
                    EditedDate = (DateTime) dr["EditedDate"],
                    // custom props
                    DepartmentName = (string) dr["DepartmentName"],
                    ManagementDepartmentName = (string) dr["ManagementDepartmentName"],
                    MaritalStatusName = (string) dr["MaritalStatusName"],
                    BirthPlaceWardName = (string) dr["BirthPlaceWardName"],
                    BirthPlaceDistrictName = (string) dr["BirthPlaceDistrictName"],
                    BirthPlaceProvinceName = (string) dr["BirthPlaceProvinceName"],
                    HometownWardName = (string) dr["HometownWardName"],
                    HometownDistrictName = (string) dr["HometownDistrictName"],
                    HometownProvinceName = (string) dr["HometownProvinceName"],
                    FolkName = (string) dr["FolkName"],
                    ReligionName = (string) dr["ReligionName"],
                    PersonalClassName = (string) dr["PersonalClassName"],
                    FamilyClassName = (string) dr["FamilyClassName"],
                    PositionName = (string) dr["PositionName"],
                    JobTitleName = (string) dr["JobTitleName"],
                    BasicEducationName = (string) dr["BasicEducationName"],
                    EducationName = (string) dr["EducationName"],
                    PoliticLevelName = (string) dr["PoliticLevelName"],
                    ManagementLevelName = (string) dr["ManagementLevelName"],
                    LanguageLevelName = (string) dr["LanguageLevelName"],
                    ITLevelName = (string) dr["ITLevelName"],
                    CPVPositionName = (string) dr["CPVPositionName"],
                    VYUPositionName = (string) dr["VYUPositionName"],
                    ArmyLevelName = (string) dr["ArmyLevelName"],
                    HealthStatusName = (string) dr["HealthStatusName"],
                    FamilyPolicyName = (string) dr["FamilyPolicyName"],
                    IDIssuePlaceName = (string) dr["IDIssuePlaceName"],
                    WorkStatusName = (string) dr["WorkStatusName"],
                    EmployeeTypeName = (string) dr["EmployeeTypeName"],
                    AccountNumber = (string) dr["AccountNumber"],
                    BankName = (string) dr["BankName"],
                    QuantumName = (string) dr["QuantumName"],
                    QuantumCode = (string) dr["QuantumCode"],
                    SalaryFactor = (decimal) dr["SalaryFactor"],
                    IndustryName = (string) dr["IndustryName"],
                    GovernmentPositionName = (string) dr["GovernmentPositionName"],
                    PluralityPositionName = (string) dr["PluralityPositionName"],
                    UniversityId = (int)dr["UniversityId"],
                    GraduationTypeId = (int)dr["GraduationTypeId"],
                    GraduationYear = (int) dr["GraduationYear"],

                };


                // Birth Date
                if(!(dr["BirthDate"] is DBNull)) model.BirthDate = (DateTime)dr["BirthDate"];

                // Recruiment Date
                if(!(dr["RecruimentDate"] is DBNull)) model.RecruimentDate = (DateTime)dr["RecruimentDate"];

                // Participation Date
                if(!(dr["ParticipationDate"] is DBNull)) model.ParticipationDate = (DateTime)dr["ParticipationDate"];

                // Functionary Date
                if(!(dr["FunctionaryDate"] is DBNull)) model.FunctionaryDate = (DateTime)dr["FunctionaryDate"];

                // CPV Joined Date
                if(!(dr["CPVJoinedDate"] is DBNull)) model.CPVJoinedDate = (DateTime)dr["CPVJoinedDate"];

                // CPV Official Joined Date
                if(!(dr["CPVOfficialJoinedDate"] is DBNull)) model.CPVOfficialJoinedDate = (DateTime) dr["CPVOfficialJoinedDate"];

                // VYU Joined Date
                if(!(dr["VYUJoinedDate"] is DBNull)) model.VYUJoinedDate = (DateTime) dr["VYUJoinedDate"];

                // Army Joined Date
                if(!(dr["ArmyJoinedDate"] is DBNull)) model.ArmyJoinedDate = (DateTime) dr["ArmyJoinedDate"];

                // Army Left Date
                if(!(dr["ArmyLeftDate"] is DBNull)) model.ArmyLeftDate = (DateTime) dr["ArmyLeftDate"];

                // ID Issue Date
                if(!(dr["IDIssueDate"] is DBNull)) model.IDIssueDate = (DateTime) dr["IDIssueDate"];

                // Insurance Issue Date
                if(!(dr["InsuranceIssueDate"] is DBNull)) model.InsuranceIssueDate = (DateTime) dr["InsuranceIssueDate"];

                // Health Joined Date
                if(!(dr["HealthJoinedDate"] is DBNull)) model.HealthJoinedDate = (DateTime) dr["HealthJoinedDate"];

                // Health Expired Date
                if(!(dr["HealthExpiredDate"] is DBNull)) model.HealthExpiredDate = (DateTime) dr["HealthExpiredDate"];

                // WorkStatus Date
                if(!(dr["WorkStatusDate"] is DBNull)) model.WorkStatusDate = (DateTime) dr["WorkStatusDate"];

                // Revolution Joined Date
                if(!(dr["RevolutionJoinedDate"] is DBNull)) model.RevolutionJoinedDate = (DateTime) dr["RevolutionJoinedDate"];

                // Effective Date
                if(!(dr["EffectiveDate"] is DBNull)) model.EffectiveDate = (DateTime) dr["EffectiveDate"];

                return model;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Erro on fill object: " + ex.Message);
                return new RecordModel();
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetCountRecords(int type)
        {
            var countQuery = " SELECT COUNT(*) FROM hr_Record WHERE [Type] = {0}".FormatWith(type);
            var objCount = SQLHelper.ExecuteScalar(countQuery);
            var total = (int?)objCount ?? 0;
            return total;
        }
    }
}