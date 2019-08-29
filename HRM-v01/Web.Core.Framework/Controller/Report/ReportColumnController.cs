using System.Collections.Generic;
using System.Linq;
using Web.Core.Object.Report;
using Web.Core.Service.Report;

namespace Web.Core.Framework
{
    public class ReportColumnController
    {
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReportColumnModel GetById(int id)
        {
            var entity = ReportColumnServices.GetById(id);
            return entity != null ? new ReportColumnModel(entity) : null;
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="reportId"></param>
        /// <param name="parentId"></param>
        /// <param name="isGroup"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<ReportColumnModel> GetAll(string keyword, int reportId, int? parentId, bool? isGroup, ReportColumnType? type, ReportColumnStatus? status, 
            bool? isDeleted, string order, int? limit)
        {
            // init condition
            var condition = "1=1";

            // keyword
            if (keyword != null)
                condition += " AND ([Name] LIKE N'%{0}%' OR [FieldName] LIKE N'%{0}%')".FormatWith(keyword);

            // report id
            if(reportId > 0)
                condition += " AND [ReportId]={0}".FormatWith(reportId);

            // parent id
            if(parentId != null)
                condition += " AND [ParentId]={0}".FormatWith(parentId.Value);

            // is group
            if(isGroup != null)
                condition += " AND [IsGroup]={0}".FormatWith(isGroup.Value ? 1 : 0);

            // deleted
            if(isDeleted != null)
                condition += " AND [IsDeleted]={0}".FormatWith(isDeleted.Value ? 1 : 0);

            // status
            if(status != null)
                condition += " AND [Status]={0}".FormatWith((int)status.Value);

            // report column type
            if(type != null)
                condition += " AND [Type]={0}".FormatWith((int)type.Value);

            // return
            return ReportColumnServices.GetAll(condition, order, limit).Select(e => new ReportColumnModel(e)).ToList();
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="reportId"></param>
        /// <param name="parentId"></param>
        /// <param name="isGroup"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<ReportColumnModel> GetPaging(string keyword, int reportId, int? parentId, bool? isGroup, ReportColumnType? type, ReportColumnStatus? status,
            bool? isDeleted, string order, int start, int limit)
        {
            // init condition
            var condition = "1=1";

            // keyword
            if (!string.IsNullOrEmpty(keyword))
                condition += " AND [Name] LIKE N'%{0}%'".FormatWith(keyword.EscapeQuote());

            // report id
            if(reportId > 0)
                condition += " AND [ReportId]={0}".FormatWith(reportId);

            // parent id
            if(parentId != null)
                condition += " AND [ParentId]={0}".FormatWith(parentId.Value);

            // is group
            if(isGroup != null)
                condition += " AND [IsGroup]={0}".FormatWith(isGroup.Value ? 1 : 0);

            // deleted
            if(isDeleted != null)
                condition += " AND [IsDeleted]={0}".FormatWith(isDeleted.Value ? 1 : 0);

            // status
            if(status != null)
                condition += " AND [Status]={0}".FormatWith((int)status.Value);

            // report column type
            if(type != null)
                condition += " AND [Type]={0}".FormatWith((int)type.Value);

            // get result
            var result = ReportColumnServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<ReportColumnModel>(result.Total, result.Data.Select(r => new ReportColumnModel(r)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="reportId"></param>
        /// <param name="parentId"></param>
        /// <param name="isGroup"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<ReportColumnModel> GetTree(string keyword, int reportId, int? parentId, bool? isGroup, ReportColumnType? type, ReportColumnStatus? status,
            bool? isDeleted, string order, int? limit)
        {
            // get all report column
            var allReportColumns = GetAll(keyword, reportId, parentId, isGroup, type, status, isDeleted, order, limit);

            // init tree report column
            var treeReportColumns = new List<ReportColumnModel>();

            // get root
            var rootReportColumns = allReportColumns.Where(r => r.ParentId == 0).OrderBy(r => r.Order);

            foreach (var rootReportColumn in rootReportColumns)
            {
                // set level
                rootReportColumn.Level = 0;

                // add into menu tree
                treeReportColumns.Add(rootReportColumn);

                // get child menu
                var childReportColumns = allReportColumns.Where(m => m.ParentId == rootReportColumn.Id).OrderBy(m => m.Order);

                // loop
                foreach (var childReportColumn in childReportColumns)
                {
                    // populate child menu
                    PopulateChildReportColumn(allReportColumns, childReportColumn, 1, ref treeReportColumns);
                }
            }

            return treeReportColumns;
        }

        /// <summary>
        /// populate child menu
        /// </summary>
        /// <param name="allReportColumns"></param>
        /// <param name="reportColumn"></param>
        /// <param name="level"></param>
        /// <param name="treeReportColumns"></param>
        private static void PopulateChildReportColumn(List<ReportColumnModel> allReportColumns, ReportColumnModel reportColumn, int level, ref List<ReportColumnModel> treeReportColumns)
        {
            // init prefix
            var prefix = string.Empty;

            // set prefix value
            for (var levelCount = 1; levelCount <= level; levelCount++)
            {
                prefix += "+---";
            }

            // set display name
            reportColumn.ConfigName = "{0}{1}".FormatWith(prefix, reportColumn.Name);

            // set level
            reportColumn.Level = level;

            // add into menu tree
            treeReportColumns.Add(reportColumn);

            // get child
            var childReportColumns = allReportColumns.Where(m => m.ParentId == reportColumn.Id).OrderBy(m => m.Order);

            // loop
            foreach (var childReportColumn in childReportColumns)
            {
                PopulateChildReportColumn(allReportColumns, childReportColumn, level + 1, ref treeReportColumns);
            }
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ReportColumnModel Create(ReportColumnModel model)
        {
            var entity = new ReportColumn();
            // check validate parent column
            if (!ValidateCreateColumn(model.Width, model.ParentId)) return null;
            // fill entity
            model.FillEntity(ref entity);

            return new ReportColumnModel(ReportColumnServices.Create(entity));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ReportColumnModel Update(ReportColumnModel model)
        {
            var entity = new ReportColumn();
            // check validate parent column
            if (!ValidateUpdateColumn(model.Id, model.Width, model.ParentId)) return null;
            // fill entity
            model.FillEntity(ref entity);

            return new ReportColumnModel(ReportColumnServices.Update(entity));
        }

        /// <summary>
        /// Update deleted status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReportColumnModel Delete(int id)
        {
            var model = GetById(id);
            if (model == null) return null;
            model.IsDeleted = true;
            return Update(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private static bool ValidateCreateColumn(int width, int parentId)
        {
            // get parent model
            var parentModel = GetById(parentId);
            // check null
            if (parentModel == null) return true;

            return width <= parentModel.WidthRemain;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <param name="newWidth"></param>
        /// <returns></returns>
        private static bool ValidateUpdateColumn(int id, int newWidth, int parentId)
        {
            // get parent model
            var parentModel = GetById(parentId);
            // get model
            var model = GetById(id);
            // check null
            if (parentModel == null) return true;

            return newWidth - model.Width <= parentModel.WidthRemain && newWidth <= parentModel.Width;
        }
    }
}
