using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.Kpi;
using Web.Core.Service.Kpi;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for CriterionGroupController
    /// </summary>
    public class CriterionGroupController
    {
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CriterionGroupModel GetById(int id)
        {
            // get entity
            var entity = kpi_CriterionGroupServices.GetById(id);

            // return
            return entity != null ? new CriterionGroupModel(entity) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criterionId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static CriterionGroupModel GetUnique(int criterionId, int groupId)
        {
            var condition = Constant.ConditionDefault;
            condition += " AND [CriterionId] = {0}".FormatWith(criterionId) +
                         " AND [GroupId] = {0}".FormatWith(groupId);
            // get entity
            var entity = kpi_CriterionGroupServices.GetByCondition(condition);

            // return
            return entity != null ? new CriterionGroupModel(entity) : null;
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="criterionId"></param>
        /// <param name="groupId"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<CriterionGroupModel> GetAll(string keyword, int? criterionId, int? groupId, string order, int? limit)
        {
            // init default condition
            var condition = Constant.ConditionDefault;

            // keyword
            if (!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ( [CriterionId] IN ( SELECT Id FROM kpi_Criterion WHERE [Name] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')
                                    OR [GroupId] IN ( SELECT Id FROM kpi_Group WHERE [Name] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%'))".FormatWith(keyword.EscapeQuote());
            }

            // criterionId
            if (criterionId != null)
            {
                condition += " AND [CriterionId] = {0}".FormatWith(criterionId);
            }

            if (groupId != null)
            {
                condition += " AND [GroupId] = {0}".FormatWith(groupId);
            }

            // return
            return kpi_CriterionGroupServices.GetAll(condition, order, limit).Select(g => new CriterionGroupModel(g)).ToList();
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="criterionId"></param>
        /// <param name="groupId"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<CriterionGroupModel> GetPaging(string keyword, int? criterionId, int? groupId, string order, int start, int limit)
        {
            // init default condition
            var condition = Constant.ConditionDefault;

            // keyword
            if (!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ( [CriterionId] IN ( SELECT Id FROM kpi_Criterion WHERE [Name] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')
                                    OR [GroupId] IN ( SELECT Id FROM kpi_Group WHERE [Name] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%'))".FormatWith(keyword.EscapeQuote());
            }

            // criterionId
            if (criterionId != null)
            {
                condition += " AND [CriterionId] = {0}".FormatWith(criterionId);
            }

            if (groupId != null)
            {
                condition += " AND [GroupId] = {0}".FormatWith(groupId);
            }

            // get result
            var result = kpi_CriterionGroupServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<CriterionGroupModel>(result.Total, result.Data.Select(g => new CriterionGroupModel(g)).ToList());
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CriterionGroupModel Create(CriterionGroupModel model)
        {
            // init entity
            var entity = new kpi_CriterionGroup();

            // fill entity
            model.FillEntity(ref entity);

            // create
            return new CriterionGroupModel(kpi_CriterionGroupServices.Create(entity));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CriterionGroupModel Update(CriterionGroupModel model)
        {
            // int entity
            var entity = new kpi_CriterionGroup();

            // fill entity
            model.FillEntity(ref entity);

            // update
            return new CriterionGroupModel(kpi_CriterionGroupServices.Update(entity));
        }

        /// <summary>
        /// Update deleted status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void Delete(int id)
        {
            kpi_CriterionGroupServices.Delete(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="criterionId"></param>
        public static void DeleteByCondition(int? groupId, int? criterionId)
        {
            if (groupId == null && criterionId == null) return;
            var condition = Constant.ConditionDefault;
            if (groupId != null)
            {
                condition += " AND [GroupId] = {0}".FormatWith(groupId);
            }

            if (criterionId != null)
            {
                condition += " AND [CriterionId] = {0}".FormatWith(criterionId);
            }

            kpi_CriterionGroupServices.Delete(condition);
        }
    }
}
