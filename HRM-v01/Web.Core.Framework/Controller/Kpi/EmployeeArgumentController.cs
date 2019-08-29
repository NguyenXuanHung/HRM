using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.Kpi;
using Web.Core.Service.Kpi;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for EmployeeArgumentController
    /// </summary>
    public class EmployeeArgumentController
    {
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static EmployeeArgumentModel GetById(int id)
        {
            // get entity
            var entity = kpi_EmployeeArgumentServices.GetById(id);    
            
            // return
            return entity != null ? new EmployeeArgumentModel(entity) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="recordId"></param>
        /// <param name="argumentId"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static EmployeeArgumentModel GetUnique(int? groupId, int? recordId, int? argumentId, int? month, int? year)
        {
            if (recordId == null && argumentId == null && month == null && year == null && groupId == null)
                return null; 

            // get entity
            var condition = Constant.ConditionDefault;
            if (groupId != null)
                condition += " AND [GroupId] = {0}".FormatWith(groupId);

            if (recordId != null)
                condition += " AND [RecordId] = {0}".FormatWith(recordId);

            if (argumentId != null)
                condition += " AND [ArgumentId] = {0}".FormatWith(argumentId);

            if (month != null)
                condition += " AND [Month] = {0}".FormatWith(month);

            if (year != null)
                condition += " AND [Year] = {0}".FormatWith(year);

            var entity = kpi_EmployeeArgumentServices.GetByCondition(condition);    
            
            // return
            return entity != null ? new EmployeeArgumentModel(entity) : null;
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="groupId"></param>
        /// <param name="recordId"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<EmployeeArgumentModel> GetAll(string keyword, string departmentIds, int? groupId,  int? recordId, int? month, int? year, string order, int? limit)
        {
            // init default condition
            var condition = Constant.ConditionDefault;

            // keyword
            if(!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ([RecordId] IN (SELECT Id FROM hr_Record WHERE [EmployeeCode] LIKE N'%{0}%' OR [FullName] LIKE N'%{0}%'))".FormatWith(keyword.EscapeQuote());
            }

            if (!string.IsNullOrEmpty(departmentIds))
            {
                condition += @" AND ([RecordId] IN (SELECT Id FROM hr_Record WHERE [DepartmentId] IN ({0})))".FormatWith(departmentIds);
            }

            if (groupId != null)
            {
                condition += " AND [GroupId] = {0}".FormatWith(groupId);
            }

            // recordId
            if (recordId != null)
            {
                condition += " AND [RecordId] = {0}".FormatWith(recordId);
            }

            if (month != null)
            {
                condition += " AND [Month] = {0}".FormatWith(month);
            }

            if (year != null)
            {
                condition += " AND [Year] = {0}".FormatWith(year);
            }

            // return
            return kpi_EmployeeArgumentServices.GetAll(condition, order, limit).Select(g => new EmployeeArgumentModel(g)).ToList();
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="groupId"></param>
        /// <param name="recordId"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<EmployeeArgumentDisplayModel> GetPaging(string keyword, string departmentIds, int? groupId, int? recordId, int? month, int? year, string order, int start, int limit)
        {
            // get result
            var listModels = GetAll(keyword, departmentIds, groupId, recordId, month, year, order, null);
            var listRecordIds = listModels.Select(rc => new{rc.RecordId,rc.FullName, rc.EmployeeCode, rc.DepartmentName }).Distinct().OrderBy(rc => rc.FullName).ToList();
            var startPage = start;
            var limitPage = limit;
            if (startPage + limit > listRecordIds.Count)
                limitPage = listRecordIds.Count - startPage;

            var resultModel = new List<EmployeeArgumentDisplayModel>();
            foreach (var record in listRecordIds.GetRange(startPage, limitPage))
            {
                var displayModel = new EmployeeArgumentDisplayModel(listModels, record.RecordId )
                {
                    RecordId = record.RecordId,
                    FullName = record.FullName,
                    EmployeeCode = record.EmployeeCode,
                    DepartmentName = record.DepartmentName
                };
                resultModel.Add(displayModel);
            }

            // return
            return new PageResult<EmployeeArgumentDisplayModel>(listRecordIds.Count, resultModel);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EmployeeArgumentModel Create(EmployeeArgumentModel model)
        {
            // init entity
            var entity = new kpi_EmployeeArgument();

            // fill entity
            model.FillEntity(ref entity);

            // create
            return new EmployeeArgumentModel(kpi_EmployeeArgumentServices.Create(entity));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EmployeeArgumentModel Update(EmployeeArgumentModel model)
        {
            // int entity
            var entity = new kpi_EmployeeArgument();

            // fill entity
            model.FillEntity(ref entity);

            // update
            return new EmployeeArgumentModel(kpi_EmployeeArgumentServices.Update(entity));
        }

        /// <summary>
        /// Update deleted status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void Delete(int id)
        {
            //delete
            kpi_EmployeeArgumentServices.Delete(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordIds"></param>
        /// <param name="recordId"></param>
        /// <param name="argumentId"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        public static void DeleteByCondition(List<string> recordIds,int? recordId, int? argumentId, int? month, int? year)
        {
            var condition = Constant.ConditionDefault;
            if (recordIds.Count > 0)
            {
                condition += " AND [RecordId] IN ({0}) ".FormatWith(string.Join(",", recordIds));
            }
            // recordId
            if (recordId != null)
            {
                condition += " AND [RecordId] = {0}".FormatWith(recordId);
            }

            if (argumentId != null)
                condition += " AND [ArgumentId] = {0}".FormatWith(argumentId);

            if (month != null)
            {
                condition += " AND [Month] = {0}".FormatWith(month);
            }

            if (year != null)
            {
                condition += " AND [Year] = {0}".FormatWith(year);
            }
            //delete
            kpi_EmployeeArgumentServices.Delete(condition);
        }
    }
}
