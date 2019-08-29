using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.Kpi;
using Web.Core.Service.Kpi;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for EvaluationController
    /// </summary>
    public class EvaluationController
    {
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static EvaluationModel GetById(int id)
        {
            // get entity
            var entity = kpi_EvaluationServices.GetById(id);    
            
            // return
            return entity != null ? new EvaluationModel(entity) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="criterionId"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static EvaluationModel GetUnique(int recordId, int criterionId, int month, int year)
        {
            var condition = Constant.ConditionDefault;
            condition += " AND [RecordId] = {0} ".FormatWith(recordId) +
                         " AND [CriterionId] = {0}".FormatWith(criterionId) +
                         " AND [Month] = {0}".FormatWith(month) +
                         " AND [Year] = {0}".FormatWith(year);
              
            // get entity
            var entity = kpi_EvaluationServices.GetByCondition(condition);    
            
            // return
            return entity != null ? new EvaluationModel(entity) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="criterionId"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static EvaluationCalculateModel CheckExist(int recordId, int criterionId, int month, int year)
        {
            var condition = Constant.ConditionDefault;
            condition += " AND [RecordId] = {0} ".FormatWith(recordId) +
                         " AND [CriterionId] = {0}".FormatWith(criterionId) +
                         " AND [Month] = {0}".FormatWith(month) +
                         " AND [Year] = {0}".FormatWith(year);

            // get entity
            var entity = kpi_EvaluationServices.GetByCondition(condition);

            // return
            return entity != null ? new EvaluationCalculateModel(entity) : null;
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="criterionId"></param>
        /// <param name="groupId"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<EvaluationModel> GetAll(string keyword,string departmentIds, int? criterionId, int groupId, int? month, int? year, string order, int? limit)
        {
            // init default condition
            var condition = Constant.ConditionDefault;

            // keyword
            if (!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ([RecordId] IN (SELECT Id FROM hr_Record WHERE [EmployeeCode] LIKE N'%{0}%' OR [FullName] LIKE N'%{0}%'))".FormatWith(keyword.EscapeQuote());
            }
            //departments
            if (!string.IsNullOrEmpty(departmentIds))
            {
                condition += @" AND ([RecordId] IN (SELECT Id FROM hr_Record WHERE [DepartmentId] IN ({0})))".FormatWith(departmentIds);
            }

            if (criterionId != null)
            {
                condition += " AND [CriterionId] = {0}".FormatWith(criterionId);
            }

            if (groupId != 0)
            {
                condition += " AND [CriterionId] IN (SELECT CriterionId FROM kpi_CriterionGroup WHERE [GroupId] = {0})".FormatWith(groupId);
            }

            // month
            if (month != null)
            {
                condition += " AND [Month] = {0}".FormatWith(month);
            }

            if (year != null)
            {
                condition += " AND [Year] = {0}".FormatWith(year);
            }

            // return
            return kpi_EvaluationServices.GetAll(condition, order, limit).Select(g => new EvaluationModel(g)).ToList();
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="criterionId"></param>
        /// <param name="groupId"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<EvaluationDisplayModel> GetPaging(string keyword, string departmentIds, int? criterionId, int groupId, int? month, int? year, string order, int start, int limit)
        {
            // get result
            var result = GetAll(keyword, departmentIds, criterionId, groupId, month, year, order, null);

            var listRecordIds = result.Select(rc => new { rc.RecordId, rc.FullName, rc.EmployeeCode, rc.DepartmentName }).Distinct().OrderBy(rc => rc.FullName).ToList();
            var startPage = start;
            var limitPage = limit;
            if (startPage + limit > listRecordIds.Count)
                limitPage = listRecordIds.Count - startPage;

            var resultModel = new List<EvaluationDisplayModel>();
            foreach (var record in listRecordIds.GetRange(startPage, limitPage))
            {
                var displayModel = new EvaluationDisplayModel(result, record.RecordId, groupId)
                {
                    RecordId = record.RecordId,
                    FullName = record.FullName,
                    EmployeeCode = record.EmployeeCode,
                    DepartmentName = record.DepartmentName
                };
                resultModel.Add(displayModel);
            }

            // return
            return new PageResult<EvaluationDisplayModel>(listRecordIds.Count, resultModel);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EvaluationModel Create(EvaluationModel model)
        {
            // init entity
            var entity = new kpi_Evaluation();

            // fill entity
            model.FillEntity(ref entity);

            // create
            return new EvaluationModel(kpi_EvaluationServices.Create(entity));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EvaluationModel Update(EvaluationModel model)
        {
            // int entity
            var entity = new kpi_Evaluation();

            // fill entity
            model.FillEntity(ref entity);

            // update
            return new EvaluationModel(kpi_EvaluationServices.Update(entity));
        }

        /// <summary>
        /// Update deleted status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void Delete(int id)
        {
            kpi_EvaluationServices.Delete(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criterionIds"></param>
        /// <param name="criterionId"></param>
        public static void DeleteByCondition(string criterionIds, int? criterionId)
        {
            var condition = Constant.ConditionDefault;
            if (!string.IsNullOrEmpty(criterionIds))
            {
                condition += " AND [CriterionId] IN ({0})".FormatWith(criterionIds);
            }

            if (criterionId != null)
            {
                condition += " AND [CriterionId] = {0}".FormatWith(criterionId);
            }

            //delete
            kpi_EvaluationServices.Delete(condition);
        }
    }
}
