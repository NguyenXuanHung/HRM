using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;

namespace Web.Core.Service.HumanRecord
{
    public class hr_RecordServices : BaseServices<hr_Record>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="searchQuery"></param>
        /// <param name="keyword"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PageResult<hr_Record> GetPaging(string departments, string searchQuery, string keyword, string order, int? start, int? pageSize)
        {
            // init condition
            var condition = " 1=1 ";
            if (!string.IsNullOrEmpty(departments))
            {
                // department
                var arrDepartment = departments.Split(new[] { ',' }, StringSplitOptions.None);
                for (var i = 0; i < arrDepartment.Length; i++)
                {
                    arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
                }

                // init condition
                condition += @" AND [DepartmentId] IN ({0})".FormatWith(string.Join(",", arrDepartment));
            }
            // add search keyword
            if (!string.IsNullOrEmpty(searchQuery))
            {
                condition += searchQuery;
            }

            var queryCondition = getQueryCondition(keyword);
            if (!string.IsNullOrEmpty(queryCondition))
            {
                condition += queryCondition;
            }                          
          
            // return
            return SQLHelper.FindPaging<hr_Record>(condition, order, start, pageSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private static string getQueryCondition(string query)
        {
            var queryCondition = string.Empty;
            if (!string.IsNullOrEmpty(query))
            {
                string[] value = query.Split(new[] { ';' }, StringSplitOptions.None);

                // bird date
                if (value.Length >= 1)
                {
                    var birthDate = value[0].ToString();
                    if(!string.IsNullOrEmpty(birthDate))
                        queryCondition += @" AND [BirthDate] LIKE '%{0}%' ".FormatWith(birthDate);
                }

                // sex
                if (value.Length >= 2)
                {
                    var sexName = value[1].ToString();
                    if (!string.IsNullOrEmpty(sexName))
                    {
                        if(sexName == "M")
                            queryCondition += @" AND [Sex] = '1' ";
                        else
                            queryCondition += @" AND [Sex] = '0' ";
                    }else
                    {
                        queryCondition += @" AND ([Sex] = '0' OR [Sex] = '1' ) ";
                    }
                }

                // marital status
                if (value.Length >= 3)
                {
                    var maritalStatus = value[2].ToString();
                    if(!string.IsNullOrEmpty(maritalStatus))
                        queryCondition += @" AND [MaritalStatusId] = '{0}' ".FormatWith(maritalStatus);
                }

                // department
                if (value.Length >= 4)
                {
                    var department = value[3].ToString();
                    if (!string.IsNullOrEmpty(department))
                        queryCondition += @" AND [DepartmentId] = '{0}' ".FormatWith(department);
                }

                // folk
                if (value.Length >= 5)
                {
                    var folk = value[4].ToString();
                    if (!string.IsNullOrEmpty(folk))
                        queryCondition += @" AND [FolkId] = '{0}' ".FormatWith(folk);
                }

                // religion
                if (value.Length >= 6)
                {
                    var religion = value[5].ToString();
                    if (!string.IsNullOrEmpty(religion))
                        queryCondition += @" AND [ReligionId] = '{0}' ".FormatWith(religion);
                }

                // personal class
                if (value.Length >= 7)
                {
                    var personalClass = value[6].ToString();
                    if (!string.IsNullOrEmpty(personalClass))
                        queryCondition += @" AND [PersonalClassId] = '{0}' ".FormatWith(personalClass);
                }

                // family class
                if (value.Length >= 8)
                {
                    var familyClass = value[7].ToString();
                    if (!string.IsNullOrEmpty(familyClass))
                        queryCondition += @" AND [FamilyClassId] = '{0}' ".FormatWith(familyClass);
                }

                // recruiment
                if (value.Length >= 9)
                {
                    var recruimentDate = value[8].ToString();
                    if (!string.IsNullOrEmpty(recruimentDate))
                        queryCondition += @" AND [RecruimentDate] LIKE '%{0}%' ".FormatWith(recruimentDate);
                }

                // position
                if (value.Length >= 10)
                {
                    var position = value[9].ToString();
                    if (!string.IsNullOrEmpty(position))
                        queryCondition += @" AND [PositionId] = '{0}' ".FormatWith(position);
                }

                // job title
                if (value.Length >= 11)
                {
                    var jobTitle = value[10].ToString();
                    if (!string.IsNullOrEmpty(jobTitle))
                        queryCondition += @" AND [JobTitleId] = '{0}' ".FormatWith(jobTitle);
                }

                // basic education
                if (value.Length >= 12)
                {
                    var basicEducation = value[11].ToString();
                    if (!string.IsNullOrEmpty(basicEducation))
                        queryCondition += @" AND [BasicEducationId] = '{0}' ".FormatWith(basicEducation);
                }

                // education
                if (value.Length >= 13)
                {
                    var education = value[12].ToString();
                    if (!string.IsNullOrEmpty(education))
                        queryCondition += @" AND [EducationId] = '{0}' ".FormatWith(education);
                }

                // politic level
                if (value.Length >= 14)
                {
                    var politicLevel = value[13].ToString();
                    if (!string.IsNullOrEmpty(politicLevel))
                        queryCondition += @" AND [PoliticLevelId] = '{0}' ".FormatWith(politicLevel);
                }

                // manage level
                if (value.Length >= 15)
                {
                    var managementLevel = value[14].ToString();
                    if (!string.IsNullOrEmpty(managementLevel))
                        queryCondition += @" AND [ManagementLevelId] = '{0}' ".FormatWith(managementLevel);
                }

                // language level
                if (value.Length >= 16)
                {
                    var languageLevel = value[15].ToString();
                    if (!string.IsNullOrEmpty(languageLevel))
                        queryCondition += @" AND [LanguageLevelId] = '{0}' ".FormatWith(languageLevel);
                }

                // it level
                if (value.Length >= 17)
                {
                    var itLevel = value[16].ToString();
                    if (!string.IsNullOrEmpty(itLevel))
                        queryCondition += @" AND [ITLevelId] = '{0}' ".FormatWith(itLevel);
                }

                // cpv level
                if (value.Length >= 18)
                {
                    var cpvPosition = value[17].ToString();
                    if (!string.IsNullOrEmpty(cpvPosition))
                        queryCondition += @" AND [CPVPositionId] = '{0}' ".FormatWith(cpvPosition);
                }

                // vyu position
                if (value.Length >= 19)
                {
                    var vyuPosition = value[18].ToString();
                    if (!string.IsNullOrEmpty(vyuPosition))
                        queryCondition += @" AND [VYUPositionId] = '{0}' ".FormatWith(vyuPosition);
                }

                // army level
                if (value.Length >= 20)
                {
                    var armyLevel = value[19].ToString();
                    if (!string.IsNullOrEmpty(armyLevel))
                        queryCondition += @" AND [ArmyLevelId] = '{0}' ".FormatWith(armyLevel);
                }

                // cell phone number
                if (value.Length >= 21)
                {
                    var cellPhone = value[20].ToString();
                    if (!string.IsNullOrEmpty(cellPhone))
                        queryCondition += @" AND [CellPhoneNumber] LIKE '%{0}%' ".FormatWith(cellPhone);
                }

                // work email
                if (value.Length >= 22)
                {
                    var email = value[21].ToString();
                    if (!string.IsNullOrEmpty(email))
                        queryCondition += @" AND [WorkEmail] LIKE '%{0}%' ".FormatWith(email);
                }

                // email personal
                if (value.Length >= 23)
                {
                    var emailPersonal = value[22].ToString();
                    if (!string.IsNullOrEmpty(emailPersonal))
                        queryCondition += @" AND [PersonalEmail] LIKE '%{0}%' ".FormatWith(emailPersonal);
                }

                // work status
                if (value.Length >= 24)
                {
                    var workStatus = value[23].ToString();
                    if (!string.IsNullOrEmpty(workStatus))
                        queryCondition += @" AND [WorkStatusId] = '{0}' ".FormatWith(workStatus);
                }

                // department
                if (value.Length >= 25)
                {
                    if (string.IsNullOrEmpty(value[24])) return queryCondition;
                    var rootId = 0;
                    var selectedDepartment = "{0},".FormatWith(value[24]);
                    if (int.TryParse(value[24], out var parseId))
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
                    queryCondition += @" AND [DepartmentId] IN ({0})".FormatWith(selectedDepartment);
                }
            }

            return queryCondition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="condition"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<hr_Record> GetAllEmployee(string departments, string condition, string order, int? limit)
        {
            // department
            var arrDepartment = departments.Split(new[] { ',' }, StringSplitOptions.None);
            for (var i = 0; i < arrDepartment.Length; i++)
            {
                arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
            }

            // init condition
            var strCondition = "[DepartmentId] IN ({0})".FormatWith(string.Join(",", arrDepartment));
            //add condition
            if (!string.IsNullOrEmpty(condition))
                strCondition += condition;

            return GetAll(strCondition, order, limit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static List<hr_Record> GetAll(int? recordId)
        {
            var condition = "1=1";
            if (recordId != null)
                condition += @" AND [RecordId]='{0}'".FormatWith(recordId);
            return GetAll(condition, null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="IDNumber"></param>
        /// <param name="EmployeeCode"></param>
        /// <returns></returns>
        public static List<hr_Record> GetIDNumberAndEmployeeCode(int? recordId, string IDNumber, string EmployeeCode)
        {
            var condition = "1=1";
            if (recordId != null)
                condition += @" AND [RecordId]='{0}'".FormatWith(recordId);
            if(!string.IsNullOrEmpty(IDNumber))
                condition += @" AND [IDNumber]='{0}'".FormatWith(IDNumber);
            if (!string.IsNullOrEmpty(EmployeeCode))
                condition += @" AND [EmployeeCode]='{0}'".FormatWith(EmployeeCode);
            return GetAll(condition, null, null);
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
        public static PageResult<hr_Record> GetPagingForReport(string searchQuery, string query, string order, int start, int pageSize)
        {
            // init condition
            var condition = " 1=1 ";
           
            // add search keyword
            if (!string.IsNullOrEmpty(searchQuery))
            {
                condition += searchQuery;
            }

            var queryCondition = getQueryConditionSearchReport(query);
            if (!string.IsNullOrEmpty(queryCondition))
            {
                condition += queryCondition;
            }

            // return
            return SQLHelper.FindPaging<hr_Record>(condition, order, start, pageSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private static string getQueryConditionSearchReport(string query)
        {
            var queryCondition = string.Empty;
            if (!string.IsNullOrEmpty(query))
            {
                string[] value = query.Split(new[] { ';' }, StringSplitOptions.None);
                var name = string.Empty;
                if (value.Length >= 1)
                {
                    name = value[0].ToString();
                    if (!string.IsNullOrEmpty(name))
                        queryCondition += @" AND [FullName] LIKE N'%{0}%' ".FormatWith(name);
                }
            }
            return queryCondition;
        }

        /// <summary>
        /// get id record
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        public static int GetRecordIdByEmployeeCode(string employeeCode)
        {
            var recordId = 0;
            var empSplit = employeeCode.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < empSplit.Length; i++)
            {
                empSplit[i] = "'{0}'".FormatWith(empSplit[i]);
            }
            var condition = " [EmployeeCode] IN ({0}) ".FormatWith(string.Join(",", empSplit));
            var record = GetByCondition(condition);
            if (record != null)
            {
                recordId = record.Id;
            }
            return recordId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public static List<hr_Record> GetByDepartmentId(int? departmentId)
        {
            var condition = "1=1";
            if (departmentId != null)
                condition += " AND [DepartmentId] = {0} ".FormatWith(departmentId);
            return GetAll(condition);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="searchKey"></param>
        /// <param name="query"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static DataTable GetListRecords(string departments, RecordType? type, string searchKey, string query, string order, int? start, int? limit)
        {
            var sql = string.Empty;

            sql += "	SELECT * FROM(	 " +
                   "	SELECT 	" +
                   "	rc.Id,	" +
                   "	rc.EmployeeCode,	" +
                   "	rc.FullName,	" +
                   "	rc.Alias,	" +
                   "	rc.BirthDate,	" +
                   "	DATEDIFF(YEAR, rc.BirthDate, GETDATE()) as Age,	" +
                   "	rc.Sex,	" +
                   "	rc.FunctionaryCode,	" +
                   "	rc.ImageUrl,	" +
                   "	rc.OriginalFile,	" +
                   "	rc.ResidentPlace,	" +
                   "	rc.Address,	" +
                   "	rc.PreviousJob,	" +
                   "	rc.RecruimentDepartment,	" +
                   "	rc.RecruimentDate,	" +
                   "	rc.ParticipationDate,	" +
                   "	rc.FunctionaryDate,	" +
                   "	rc.AssignedWork,	" +
                   "	rc.CPVJoinedDate,	" +
                   "	rc.CPVOfficialJoinedDate,	" +
                   "	rc.CPVCardNumber,	" +
                   "	rc.CPVJoinedPlace,	" +
                   "	rc.VYUJoinedDate,	" +
                   "	rc.VYUJoinedPlace,	" +
                   "	rc.ArmyJoinedDate,	" +
                   "	rc.ArmyLeftDate,	" +
                   "	rc.TitleAwarded,	" +
                   "	rc.Skills,	" +
                   "	rc.BloodGroup,	" +
                   "	rc.Height,	" +
                   "	rc.Weight,	" +
                   "	rc.RankWounded,	" +
                   "	rc.IDNumber,	" +
                   "	rc.IDIssueDate,	" +
                   "	rc.InsuranceNumber,	" +
                   "	rc.InsuranceIssueDate,	" +
                   "	rc.PersonalTaxCode,	" +
                   "	rc.CellPhoneNumber,	" +
                   "	rc.HomePhoneNumber,	" +
                   "	rc.WorkPhoneNumber,	" +
                   "	rc.WorkEmail,	" +
                   "	rc.PersonalEmail,	" +
                   "	rc.Biography,	" +
                   "	rc.ForeignOrganizationJoined,	" +
                   "	rc.RelativesAboard,	" +
                   "	rc.Review,	" +
                   "	rc.ContactPersonName,	" +
                   "	rc.ContactRelation,	" +
                   "	rc.ContactPhoneNumber,	" +
                   "	rc.ContactAddress,	" +
                   "	rc.WorkStatusDate,	" +
                   "	rc.WorkStatusReason,	" +
                   "	(SELECT TOP 1 dv.Name FROM cat_Department dv WHERE dv.Id = rc.DepartmentId) as DepartmentName,	" +
                   "	(SELECT TOP 1 cf.Name FROM cat_Folk cf WHERE cf.Id = rc.FolkId) as FolkName,	" +
                   "	(SELECT TOP 1 cr.Name FROM cat_Religion cr WHERE cr.Id = rc.ReligionId) as ReligionName,	" +
                   "	(SELECT TOP 1 cm.Name FROM cat_MaritalStatus cm WHERE cm.Id = rc.MaritalStatusId) as MaritalStatusName,	" +
                   "	(SELECT TOP 1 cw.Name FROM cat_WorkStatus cw WHERE cw.Id = rc.WorkStatusId) as WorkStatusName,	" +
                   "	(SELECT TOP 1 cp.Name FROM cat_PersonalClass cp WHERE cp.Id = rc.PersonalClassId) as PersonalClassName,	" +
                   "	(SELECT TOP 1 cfa.Name FROM cat_FamilyClass cfa WHERE cfa.Id = rc.FamilyClassId) as FamilyClassName,	" +
                   "	(SELECT TOP 1 cpo.Name FROM cat_Position cpo WHERE cpo.Id = rc.PositionId) as PositionName,	" +
                   "	(SELECT TOP 1 cj.Name FROM cat_JobTitle cj WHERE cj.Id = rc.JobTitleId) as JobTitleName,	" +
                   "	(SELECT TOP 1 cbe.Name FROM cat_BasicEducation cbe WHERE cbe.Id = rc.BasicEducationId) as BasicEducationName,	" +
                   "	(SELECT TOP 1 ced.Name FROM cat_Education ced WHERE ced.Id = rc.EducationId) as EducationName,	" +
                   "	(SELECT TOP 1 cpl.Name FROM cat_PoliticLevel cpl WHERE cpl.Id = rc.PoliticLevelId) as PoliticLevelName,	" +
                   "	(SELECT TOP 1 cml.Name FROM cat_ManagementLevel cml WHERE cml.Id = rc.ManagementLevelId) as ManagementLevelName,	" +
                   "	(SELECT TOP 1 cll.Name FROM cat_LanguageLevel cll WHERE cll.Id = rc.LanguageLevelId) as LanguageLevelName,	" +
                   "	(SELECT TOP 1 cit.Name FROM cat_ITLevel cit WHERE cit.Id = rc.ITLevelId) as ITLevelName,	" +
                   "	(SELECT TOP 1 cpv.Name FROM cat_CPVPosition cpv WHERE cpv.Id = rc.CPVPositionId) as CPVPositionName,	" +
                   "	(SELECT TOP 1 vyu.Name FROM cat_VYUPosition vyu WHERE vyu.Id = rc.VYUPositionId) as VYUPositionName,	" +
                   "	(SELECT TOP 1 al.Name FROM cat_ArmyLevel al WHERE al.Id = rc.ArmyLevelId) as ArmyLevelName,	" +
                   "	(SELECT TOP 1 chs.Name FROM cat_HealthStatus chs WHERE chs.Id = rc.HealthStatusId) as HealthStatusName,	" +
                   "	(SELECT TOP 1 cfp.Name FROM cat_FamilyPolicy cfp WHERE cfp.Id = rc.FamilyPolicyId) as FamilyPolicyName,	" +
                   "	(SELECT TOP 1 ip.Name FROM cat_IDIssuePlace ip WHERE ip.Id = rc.IDIssuePlaceId) as IDIssuePlaceName,	" +
                   "	(SELECT TOP 1 et.Name FROM cat_EmployeeType et WHERE et.Id = rc.EmployeeTypeId) as EmployeeTypeName,	" +
                   "	(CASE WHEN rc.BirthPlaceWardId IS NOT NULL THEN ( ISNULL((SELECT TOP 1 bpw.Name FROM cat_Location bpw WHERE bpw.Id = rc.BirthPlaceWardId) + ',', '') 	" +
                   "	+ isnull((SELECT TOP 1 bpd.Name FROM cat_Location bpd WHERE bpd.Id = rc.BirthPlaceDistrictId) + ',', '') +	" +
                   "	ISNULL((SELECT TOP 1 bpp.Name FROM cat_Location bpp WHERE bpp.Id = rc.BirthPlaceProvinceId), ''))	" +
                   "	 END	" +
                   "	 ) AS BirthPlace,	" +
                   "	 (CASE WHEN rc.HometownWardId IS NOT NULL THEN ( ISNULL((SELECT TOP 1 bpw.Name FROM cat_Location bpw WHERE bpw.Id = rc.HometownWardId) + ',', '') 	" +
                   "	+ isnull((SELECT TOP 1 bpd.Name FROM cat_Location bpd WHERE bpd.Id = rc.HometownDistrictId) + ',', '') +	" +
                   "	ISNULL((SELECT TOP 1 bpp.Name FROM cat_Location bpp WHERE bpp.Id = rc.HometownProvinceId), ''))	" +
                   "	 END	" +
                   "	 ) AS Hometown,	" +
                   "	(SELECT TOP 1 hb.AccountNumber FROM hr_Bank hb WHERE hb.RecordId = rc.Id) as AccountNumber,	" +
                   "	(SELECT TOP 1 cb.Name FROM cat_Bank cb, hr_Bank hb WHERE cb.Id = hb.BankId AND hb.RecordId = rc.Id) as BankName,	" +
                   "	(SELECT TOP 1 sa.Factor FROM sal_SalaryDecision sa WHERE sa.RecordId = rc.Id ORDER BY sa.EffectiveDate DESC) as SalaryFactor,	" +
                   "	(SELECT TOP 1 sa.Grade FROM sal_SalaryDecision sa WHERE sa.RecordId = rc.Id ORDER BY sa.EffectiveDate DESC) as SalaryGrade,	" +
                   "	(SELECT TOP 1 sa.EffectiveDate FROM sal_SalaryDecision sa WHERE sa.RecordId = rc.Id ORDER BY sa.EffectiveDate DESC) as EffectiveDate,	" +
                   //"	(SELECT TOP 1 sa.PositionAllowance FROM sal_SalaryDecision sa WHERE sa.RecordId = rc.Id ORDER BY sa.EffectiveDate DESC) as PositionAllowance,	" +
                   //"	(SELECT TOP 1 sa.OtherAllowance FROM sal_SalaryDecision sa WHERE sa.RecordId = rc.Id ORDER BY sa.EffectiveDate DESC) as OtherAllowance,	" +
                   "	(SELECT TOP 1 cq.Code  FROM cat_Quantum cq, sal_SalaryDecision sa WHERE cq.Id = sa.QuantumId AND sa.RecordId = rc.Id ORDER BY sa.EffectiveDate DESC) as QuantumCode,	" +
                   "	(SELECT TOP 1 cq.Name  FROM cat_Quantum cq, sal_SalaryDecision sa WHERE cq.Id = sa.QuantumId AND sa.RecordId = rc.Id ORDER BY sa.EffectiveDate DESC) as QuantumName,	" +
                   "	ROW_NUMBER() OVER(ORDER BY rc.Id ASC) SK	 " +
                   "	FROM hr_Record rc	"+
                   "	WHERE 1 =1	 ";

            if (!string.IsNullOrEmpty(searchKey))
            {
                sql += " AND (rc.FullName LIKE N'%{0}%'".FormatWith(searchKey) +
                       " OR rc.IDNumber LIKE N'%{0}%' ".FormatWith(searchKey) +
                       " OR rc.EmployeeCode LIKE N'%{0}%') ".FormatWith(searchKey);
            }

            if (!string.IsNullOrEmpty(departments))
            {
                sql += " AND rc.DepartmentId IN({0})".FormatWith(departments);
            }

            if (type != null)
            {
                sql += " AND rc.Type = {0}".FormatWith((int) type);
            }
            var queryCondition = GetSearchQuery(query);
            if (!string.IsNullOrEmpty(queryCondition))
            {
                sql += queryCondition;
            }

            if (!string.IsNullOrEmpty(order))
            {
                sql += " ORDER BY {0} ".FormatWith(order);
            }

            sql += " )#tmp					";
            if (start != null && limit != null)
            {
                sql += " WHERE #tmp.SK > {0}".FormatWith(start) +
                       " AND #tmp.SK <= {0}".FormatWith(start + limit);
            }

            return SQLHelper.ExecuteTable(sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private static string GetSearchQuery(string query)
        {
            var condition = string.Empty;
            if (!string.IsNullOrEmpty(query))
            {
                var value = query.Split(new[] { ';' }, StringSplitOptions.None);

                // bird date
                if (value.Length >= 1)
                {
                    var birthDate = value[0].ToString();
                    if (!string.IsNullOrEmpty(birthDate))
                        condition += @" AND rc.[BirthDate] LIKE '%{0}%' ".FormatWith(birthDate);
                }

                // sex
                if (value.Length >= 2)
                {
                    var sexName = value[1].ToString();
                    if (!string.IsNullOrEmpty(sexName))
                    {
                        if (sexName == "M")
                            condition += @" AND rc.[Sex] = '1' ";
                        else
                            condition += @" AND rc.[Sex] = '0' ";
                    }
                    else
                    {
                        condition += @" AND (rc.[Sex] = '0' OR rc.[Sex] = '1' ) ";
                    }
                }

                // marital status
                if (value.Length >= 3)
                {
                    var maritalStatus = value[2].ToString();
                    if (!string.IsNullOrEmpty(maritalStatus))
                        condition += @" AND rc.[MaritalStatusId] = '{0}' ".FormatWith(maritalStatus);
                }

                // department
                if (value.Length >= 4)
                {
                    var department = value[3].ToString();
                    if (!string.IsNullOrEmpty(department))
                        condition += @" AND rc.[DepartmentId] = '{0}' ".FormatWith(department);
                }

                // folk
                if (value.Length >= 5)
                {
                    var folk = value[4].ToString();
                    if (!string.IsNullOrEmpty(folk))
                        condition += @" AND rc.[FolkId] = '{0}' ".FormatWith(folk);
                }

                // religion
                if (value.Length >= 6)
                {
                    var religion = value[5].ToString();
                    if (!string.IsNullOrEmpty(religion))
                        condition += @" AND rc.[ReligionId] = '{0}' ".FormatWith(religion);
                }

                // personal class
                if (value.Length >= 7)
                {
                    var personalClass = value[6].ToString();
                    if (!string.IsNullOrEmpty(personalClass))
                        condition += @" AND rc.[PersonalClassId] = '{0}' ".FormatWith(personalClass);
                }

                // family class
                if (value.Length >= 8)
                {
                    var familyClass = value[7].ToString();
                    if (!string.IsNullOrEmpty(familyClass))
                        condition += @" AND rc.[FamilyClassId] = '{0}' ".FormatWith(familyClass);
                }

                // recruiment date
                if (value.Length >= 9)
                {
                    var recruimentDate = value[8].ToString();
                    if (!string.IsNullOrEmpty(recruimentDate))
                        condition += @" AND rc.[RecruimentDate] LIKE '%{0}%' ".FormatWith(recruimentDate);
                }

                // position
                if (value.Length >= 10)
                {
                    var position = value[9].ToString();
                    if (!string.IsNullOrEmpty(position))
                        condition += @" AND rc.[PositionId] = '{0}' ".FormatWith(position);
                }

                // job title
                if (value.Length >= 11)
                {
                    var jobTitle = value[10].ToString();
                    if (!string.IsNullOrEmpty(jobTitle))
                        condition += @" AND rc.[JobTitleId] = '{0}' ".FormatWith(jobTitle);
                }

                // basic education
                if (value.Length >= 12)
                {
                    var basicEducation = value[11].ToString();
                    if (!string.IsNullOrEmpty(basicEducation))
                        condition += @" AND rc.[BasicEducationId] = '{0}' ".FormatWith(basicEducation);
                }

                // education
                if (value.Length >= 13)
                {
                    var education = value[12].ToString();
                    if (!string.IsNullOrEmpty(education))
                        condition += @" AND rc.[EducationId] = '{0}' ".FormatWith(education);
                }

                // policitc level
                if (value.Length >= 14)
                {
                    var politicLevel = value[13].ToString();
                    if (!string.IsNullOrEmpty(politicLevel))
                        condition += @" AND rc.[PoliticLevelId] = '{0}' ".FormatWith(politicLevel);
                }

                // management level
                if (value.Length >= 15)
                {
                    var managementLevel = value[14].ToString();
                    if (!string.IsNullOrEmpty(managementLevel))
                        condition += @" AND rc.[ManagementLevelId] = '{0}' ".FormatWith(managementLevel);
                }

                // language level
                if (value.Length >= 16)
                {
                    var languageLevel = value[15].ToString();
                    if (!string.IsNullOrEmpty(languageLevel))
                        condition += @" AND rc.[LanguageLevelId] = '{0}' ".FormatWith(languageLevel);
                }

                // it level
                if (value.Length >= 17)
                {
                    var itLevel = value[16].ToString();
                    if (!string.IsNullOrEmpty(itLevel))
                        condition += @" AND rc.[ITLevelId] = '{0}' ".FormatWith(itLevel);
                }

                // cpv position
                if (value.Length >= 18)
                {
                    var cpvPosition = value[17].ToString();
                    if (!string.IsNullOrEmpty(cpvPosition))
                        condition += @" AND rc.[CPVPositionId] = '{0}' ".FormatWith(cpvPosition);
                }

                // vyu position
                if (value.Length >= 19)
                {
                    var vyuPosition = value[18].ToString();
                    if (!string.IsNullOrEmpty(vyuPosition))
                        condition += @" AND rc.[VYUPositionId] = '{0}' ".FormatWith(vyuPosition);
                }

                // army level
                if (value.Length >= 20)
                {
                    var armyLevel = value[19].ToString();
                    if (!string.IsNullOrEmpty(armyLevel))
                        condition += @" AND rc.[ArmyLevelId] = '{0}' ".FormatWith(armyLevel);
                }

                // cell phone
                if (value.Length >= 21)
                {
                    var cellPhone = value[20].ToString();
                    if (!string.IsNullOrEmpty(cellPhone))
                        condition += @" AND rc.[CellPhoneNumber] LIKE '%{0}%' ".FormatWith(cellPhone);
                }

                // work email
                if (value.Length >= 22)
                {
                    var email = value[21].ToString();
                    if (!string.IsNullOrEmpty(email))
                        condition += @" AND rc.[WorkEmail] LIKE '%{0}%' ".FormatWith(email);
                }

                //email personal
                if (value.Length >= 23)
                {
                    var emailPersonal = value[22].ToString();
                    if (!string.IsNullOrEmpty(emailPersonal))
                        condition += @" AND rc.[PersonalEmail] LIKE '%{0}%' ".FormatWith(emailPersonal);
                }

                // work status
                if (value.Length >= 24)
                {
                    var workStatus = value[23].ToString();
                    if (!string.IsNullOrEmpty(workStatus))
                        condition += @" AND rc.[WorkStatusId] = '{0}' ".FormatWith(workStatus);
                }

                // department
                if (value.Length >= 25)
                {
                    if (!string.IsNullOrEmpty(value[24]))
                    {
                        var rootId = 0;
                        var selectedDepartment = "{0},".FormatWith(value[24]);
                        if (int.TryParse(value[24], out var parseId))
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
                        condition += @" AND rc.[DepartmentId] IN ({0})".FormatWith(selectedDepartment);
                    }
                }
            }

            return condition;
        }
    }
}
