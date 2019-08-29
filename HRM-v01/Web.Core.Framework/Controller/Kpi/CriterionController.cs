using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.Kpi;
using Web.Core.Service.Kpi;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for CriterionController
    /// </summary>
    public class CriterionController
    {
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CriterionModel GetById(int id)
        {
            // get entity
            var entity = kpi_CriterionServices.GetById(id);    
            
            // return
            return entity != null ? new CriterionModel(entity) : null;
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="groupId"></param>
        /// <param name="isDeleted"></param>
        /// <param name="kpiStatus"></param>
        /// <param name="valueType"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<CriterionModel> GetAll(string keyword, int? groupId, bool? isDeleted, KpiStatus? kpiStatus, KpiValueType? valueType, string order, int? limit)
        {
            // init default condition
            var condition = Constant.ConditionDefault;

            // keyword
            if(!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ([Code] LIKE N'%{0}%' OR [Name] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());
            }
            if (groupId != null)
            {
                condition += " AND [Id] IN (SELECT [CriterionId] FROM kpi_CriterionGroup WHERE [GroupId] = {0})".FormatWith(groupId);
            }
            // is deleted
            if (isDeleted != null)
            {
                condition += " AND [IsDeleted] = {0}".FormatWith((bool)isDeleted ? "1" : "0");
            }

            if (kpiStatus != null)
            {
                condition += " AND [Status] = {0}".FormatWith((int)kpiStatus);
            }

            if (valueType != null)
            {
                condition += " AND [ValueType] = {0}".FormatWith((int)valueType);
            }

            // return
            return kpi_CriterionServices.GetAll(condition, order, limit).Select(c => new CriterionModel(c)).ToList();
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="groupId"></param>
        /// <param name="isDeleted"></param>
        /// <param name="kpiStatus"></param>
        /// <param name="valueType"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<CriterionModel> GetPaging(string keyword,int? groupId, bool? isDeleted, KpiStatus? kpiStatus, KpiValueType? valueType, string order, int start, int limit)
        {
            // init default condition
            var condition = Constant.ConditionDefault;

            // keyword
            if(!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ([Code] LIKE N'%{0}%' OR [Name] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());
            }

            if (groupId != null)
            {
                condition += " AND [Id] IN (SELECT [CriterionId] FROM kpi_CriterionGroup WHERE [GroupId] = {0})".FormatWith(groupId);
            }
            // is deleted
            if(isDeleted != null)
            {
                condition += " AND [IsDeleted] = {0}".FormatWith((bool)isDeleted ? "1" : "0");
            }

            if (kpiStatus != null)
            {
                condition += " AND [Status] = {0}".FormatWith((int)kpiStatus);
            }

            if (valueType != null)
            {
                condition += " AND [ValueType] = {0}".FormatWith((int)valueType);
            }

            // get result
            var result = kpi_CriterionServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<CriterionModel>(result.Total, result.Data.Select(c => new CriterionModel(c)).ToList());
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CriterionModel Create(CriterionModel model)
        {
            // init entity
            var entity = new kpi_Criterion();

            // fill entity
            model.FillEntity(ref entity);

            // create
            return new CriterionModel(kpi_CriterionServices.Create(entity));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CriterionModel Update(CriterionModel model)
        {
            // int entity
            var entity = new kpi_Criterion();

            // fill entity
            model.FillEntity(ref entity);

            // update
            return new CriterionModel(kpi_CriterionServices.Update(entity));
        }

        /// <summary>
        /// Update deleted status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CriterionModel Delete(int id)
        {
            // get model
            var model = GetById(id);

            // check existed
            if(model != null)
            {
                // set deleted status
                model.IsDeleted = true;

                // update
                return Update(model);
            }

            // no record found
            return null;
        }
    }
}
