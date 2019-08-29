using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.Kpi;
using Web.Core.Service.Kpi;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for GroupKpiController
    /// </summary>
    public class GroupKpiController
    {
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static GroupKpiModel GetById(int id)
        {
            // get entity
            var entity = kpi_GroupServices.GetById(id);    
            
            // return
            return entity != null ? new GroupKpiModel(entity) : null;
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="isDeleted"></param>
        /// <param name="kpiStatus"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<GroupKpiModel> GetAll(string keyword, bool? isDeleted, KpiStatus? kpiStatus, string order, int? limit)
        {
            // init default condition
            var condition = Constant.ConditionDefault;

            // keyword
            if(!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ([Name] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());
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

            // return
            return kpi_GroupServices.GetAll(condition, order, limit).Select(g => new GroupKpiModel(g)).ToList();
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="isDeleted"></param>
        /// <param name="kpiStatus"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<GroupKpiModel> GetPaging(string keyword, bool? isDeleted, KpiStatus? kpiStatus, string order, int start, int limit)
        {
            // init default condition
            var condition = Constant.ConditionDefault;

            // keyword
            if(!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ([Name] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());
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

            // get result
            var result = kpi_GroupServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<GroupKpiModel>(result.Total, result.Data.Select(g => new GroupKpiModel(g)).ToList());
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static GroupKpiModel Create(GroupKpiModel model)
        {
            // init entity
            var entity = new kpi_Group();

            // fill entity
            model.FillEntity(ref entity);

            // create
            return new GroupKpiModel(kpi_GroupServices.Create(entity));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static GroupKpiModel Update(GroupKpiModel model)
        {
            // int entity
            var entity = new kpi_Group();

            // fill entity
            model.FillEntity(ref entity);

            // update
            return new GroupKpiModel(kpi_GroupServices.Update(entity));
        }

        /// <summary>
        /// Update deleted status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static GroupKpiModel Delete(int id)
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
