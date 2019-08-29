using System;
using System.Linq;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework.SQLAdapter
{
    public class SQLContractAdapter
    {
        public static string GetStore_Contract(string departments, int? start, int? limit, string searchKey, string querySearch, string departmentSelected)
        {
            var QueryCondition = string.Empty;
            if(!string.IsNullOrEmpty(querySearch))
            {
                string[] value = querySearch.Split(new[] { ';' }, StringSplitOptions.None);

                if(value.Length >= 1)
                {
                    var contractTypeId = value[0].ToString();
                    if(!string.IsNullOrEmpty(contractTypeId))
                        QueryCondition += @" AND hc.ContractTypeId = '{0}' ".FormatWith(contractTypeId);
                }
                if(value.Length >= 2)
                {
                    var jobTitleId = value[1].ToString();
                    if(!string.IsNullOrEmpty(jobTitleId))
                        QueryCondition += @" AND hr.JobTitleId = '{0}' ".FormatWith(jobTitleId);
                }
                if(value.Length >= 3)
                {
                    var positionId = value[2].ToString();
                    if(!string.IsNullOrEmpty(positionId))
                        QueryCondition += @" AND hr.PositionId = '{0}' ".FormatWith(positionId);
                }
                if(value.Length >= 4)
                {
                    var contractStatusId = value[3].ToString();
                    if(!string.IsNullOrEmpty(contractStatusId))
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
            if(!string.IsNullOrEmpty(departments))
                sql += "AND hr.DepartmentId IN ({0}) ".FormatWith(departments);
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
                    foreach(var d in lstDepartment)
                    {
                        selectedDepartment += "{0},".FormatWith(d);
                    }
                }
                selectedDepartment = "{0}".FormatWith(selectedDepartment.TrimEnd(','));
                sql += "AND hr.DepartmentId IN ({0}) ".FormatWith(selectedDepartment);
            }
            if(!string.IsNullOrEmpty(searchKey))
                sql += " AND hr.FullName LIKE N'%{0}%' ".FormatWith(searchKey.GetKeyword()) +
                        " OR hr.EmployeeCode LIKE N'%{0}%' ".FormatWith(searchKey.GetKeyword());
            if(!string.IsNullOrEmpty(QueryCondition))
                sql += QueryCondition;
            sql += " ) A ";
            if(start != null && limit != null)
                sql += "WHERE A.SK > {0} ".FormatWith(start) +
                    "AND A.SK <= {0} ".FormatWith(start + limit);

            return sql;
        }
    }
}
