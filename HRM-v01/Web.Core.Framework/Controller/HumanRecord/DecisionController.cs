using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for DecisionController
    /// </summary>
    public class DecisionController
    {
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DecisionModel GetById(int id)
        {
            // get entity
            var entity = hr_DecisionServices.GetById(id);    
            
            // return
            return entity != null ? new DecisionModel(entity) : null;
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="type"></param>
        /// <param name="recordId"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<DecisionModel> GetAll(string keyword, string departmentIds, DecisionType? type,  int? recordId, string order, int? limit)
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

            if (type != null)
            {
                condition += " AND [Type] = {0}".FormatWith(type.Value);
            }

            // recordId
            if (recordId != null)
            {
                condition += " AND [RecordId] = {0}".FormatWith(recordId);
            }

            // return
            return hr_DecisionServices.GetAll(condition, order, limit).Select(g => new DecisionModel(g)).ToList();
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="type"></param>
        /// <param name="recordId"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<DecisionModel> GetPaging(string keyword, string departmentIds, DecisionType? type, int? recordId, string order, int start, int limit)
        {
            // init default condition
            var condition = Constant.ConditionDefault;

            // keyword
            if (!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ([RecordId] IN (SELECT Id FROM hr_Record WHERE [EmployeeCode] LIKE N'%{0}%' OR [FullName] LIKE N'%{0}%'))".FormatWith(keyword.EscapeQuote());
            }

            if (!string.IsNullOrEmpty(departmentIds))
            {
                condition += @" AND ([RecordId] IN (SELECT Id FROM hr_Record WHERE [DepartmentId] IN ({0})))".FormatWith(departmentIds);
            }

            if (type != null)
            {
                condition += " AND [Type] = {0}".FormatWith((int)type);
            }

            // recordId
            if (recordId != null)
            {
                condition += " AND [RecordId] = {0}".FormatWith(recordId);
            }

            var result = hr_DecisionServices.GetPaging(condition, order, start, limit);
            // return
            return new PageResult<DecisionModel>(result.Total, result.Data.Select(dc => new DecisionModel(dc)).ToList());
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DecisionModel Create(DecisionModel model)
        {
            // init entity
            var entity = new hr_Decision();

            // fill entity
            model.FillEntity(ref entity);

            // create
            return new DecisionModel(hr_DecisionServices.Create(entity));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DecisionModel Update(DecisionModel model)
        {
            // int entity
            var entity = new hr_Decision();

            // fill entity
            model.FillEntity(ref entity);

            // update
            return new DecisionModel(hr_DecisionServices.Update(entity));
        }

        /// <summary>
        /// Update deleted status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void Delete(int id)
        {
            //delete
            hr_DecisionServices.Delete(id);
        }

    }
}
