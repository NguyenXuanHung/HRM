using System.Collections.Generic;
using System.Linq;
using Web.Core.Object.Report;
using Web.Core.Service.Report;

namespace Web.Core.Framework
{
    public class ReportDynamicController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReportDynamicModel GetById(int id)
        {
            var entity = ReportDynamicServices.GetById(id);
            return entity != null ? new ReportDynamicModel(entity) : null;
        }

        /// <summary>
        /// Get report by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ReportDynamicModel GetByName(string name)
        {
            // check report name
            if(!string.IsNullOrEmpty(name))
            {
                var condition = "[Name]='{0}'".FormatWith(name);
                var entity = ReportDynamicServices.GetByCondition(condition);
                return entity != null ? new ReportDynamicModel(entity) : null;
            }
            // invalid name
            return null;
        }

        /// <summary>
        /// Get all reports by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="template"></param>
        /// <param name="status"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<ReportDynamicModel> GetAll(string keyword, ReportTemplate? template, ReportStatus? status,
            bool? isDeleted, string order, int? limit)
        {
            // init condition
            var condition = "1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
                condition += " AND ([Name] LIKE N'%{0}%' OR [Title] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());

            // template
            if(template != null)
                condition += " AND [Template]='{0}'".FormatWith((int)template.Value);

            // status
            if(status != null)
                condition += " AND [Status]='{0}'".FormatWith((int)status.Value);

            // template
            if(isDeleted != null)
                condition += " AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value ? 1 : 0);

            // return
            return ReportDynamicServices.GetAll(condition, order, limit).Select(r => new ReportDynamicModel(r)).ToList();
        }

        /// <summary>
        /// Get reports paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="template"></param>
        /// <param name="status"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<ReportDynamicModel> GetPaging(string keyword, ReportTemplate? template,
            ReportStatus? status, bool? isDeleted, string order, int start, int limit)
        {
            // init condition
            var condition = "1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
                condition += " AND ([Name] LIKE N'%{0}%' OR [Title] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());

            // template
            if(template != null)
                condition += " AND [Template]='{0}'".FormatWith((int)template.Value);

            // status
            if(status != null)
                condition += " AND [Status]='{0}'".FormatWith((int)status.Value);

            // template
            if(isDeleted != null)
                condition += " AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value ? 1 : 0);

            // get result
            var result = ReportDynamicServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<ReportDynamicModel>(result.Total, result.Data.Select(r => new ReportDynamicModel(r)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ReportDynamicModel Create(ReportDynamicModel model)
        {
            // get existed report by name
            var existedEntity = GetByName(model.Name);
            // check existed
            if (existedEntity == null)
            {
                // report was not created
                var entity = new ReportDynamic();
                model.FillEntity(ref entity);
                return new ReportDynamicModel(ReportDynamicServices.Create(entity));
            }
            // report name existed
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ReportDynamicModel Update(ReportDynamicModel model)
        {
            // get existed report by name
            var existedEntity = GetByName(model.Name);
            // check existed
            if (existedEntity == null || existedEntity.Id == model.Id)
            {
                var entity = new ReportDynamic();
                model.FillEntity(ref entity);
                return new ReportDynamicModel(ReportDynamicServices.Update(entity));
            }
            // report name existed
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReportDynamicModel Delete(int id)
        {
            var model = GetById(id);
            if (model != null)
            {
                model.IsDeleted = true;
                return Update(model);
            }
            return null;
        }
    }
}
