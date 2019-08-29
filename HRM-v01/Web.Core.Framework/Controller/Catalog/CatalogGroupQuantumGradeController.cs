using System.Collections.Generic;
using System.Linq;
using Web.Core.Object.Catalog;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework
{
    public class CatalogGroupQuantumGradeController
    {
        /// <summary>
        /// Get by ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public static CatalogGroupQuantumGradeModel GetById(int id)
        {
            // get entity
            var entity = cat_GroupQuantumGradeServices.GetById(id);

            // return
            return entity != null ? new CatalogGroupQuantumGradeModel(entity) : null;
        }

        /// <summary>
        /// Get unique
        /// </summary>
        /// <param name="groupQuantumId"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static CatalogGroupQuantumGradeModel GetUnique(int groupQuantumId, int grade)
        {
            // check report name
            if(groupQuantumId > 0 && grade > 0)
            {
                // get entity
                var entity = cat_GroupQuantumGradeServices.GetByCondition("[GroupQuantumId]='{0}' AND [Grade]='{1}'".FormatWith(groupQuantumId, grade));

                // return
                return entity != null ? new CatalogGroupQuantumGradeModel(entity) : null;
            }

            // invalid params
            return null;
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="groupQuantumId"></param>
        /// <param name="group"></param>
        /// <param name="status"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<CatalogGroupQuantumGradeModel> GetAll(string keyword, int? groupQuantumId, string group, CatalogStatus? status, bool? isDeleted, string order, int? limit)
        {
            // init condition
            var condition = "1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
                condition += @" AND ([Name] LIKE N'%{0}%' OR [Code] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());

            // group quantum
            if(groupQuantumId != null)
                condition += @" AND [GroupQuantumId]='{0}'".FormatWith(groupQuantumId.Value);

            // group
            if(!string.IsNullOrEmpty(group))
                condition += @" AND [Group]='{0}'".FormatWith(group.EscapeQuote());

            // status 
            if(status != null)
                condition += @" AND [Status]='{0}'".FormatWith((int)status.Value);

            // is deleted
            if(isDeleted != null)
                condition += @" AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value);

            // order
            if(string.IsNullOrEmpty(order))
                order = @"[GroupQuantumId],[Grade]";

            // return
            return cat_GroupQuantumGradeServices.GetAll(condition, order, limit).Select(gq => new CatalogGroupQuantumGradeModel(gq)).ToList();
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="groupQuantumId"></param>
        /// <param name="group"></param>
        /// <param name="status"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<CatalogGroupQuantumGradeModel> GetPaging(string keyword, int? groupQuantumId, string group, CatalogStatus? status, bool? isDeleted, string order, int start, int limit)
        {
            // init condition
            var condition = "1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
                condition += @" AND ([Name] LIKE N'%{0}%' OR [Code] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());

            // group quantum
            if(groupQuantumId != null)
                condition += @" AND [GroupQuantumId]='{0}'".FormatWith(groupQuantumId.Value);

            // group
            if(!string.IsNullOrEmpty(group))
                condition += @" AND [Group]='{0}'".FormatWith(group.EscapeQuote());

            // status 
            if(status != null)
                condition += @" AND [Status]='{0}'".FormatWith((int)status.Value);

            // is deleted
            if(isDeleted != null)
                condition += @" AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value);

            // order
            if(string.IsNullOrEmpty(order))
                order = @"[GroupQuantumId],[Grade]";

            // get result
            var result = cat_GroupQuantumGradeServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<CatalogGroupQuantumGradeModel>(result.Total, result.Data.Select(gq => new CatalogGroupQuantumGradeModel(gq)).ToList());
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CatalogGroupQuantumGradeModel Create(CatalogGroupQuantumGradeModel model)
        {
            // get existed by name
            var existedEntity = GetUnique(model.GroupQuantumId, model.Grade);

            // check existed
            if(existedEntity == null)
            {
                // init entity
                var entity = new cat_GroupQuantumGrade();

                // get entity from db
                model.FillEntity(ref entity);

                // return
                return new CatalogGroupQuantumGradeModel(cat_GroupQuantumGradeServices.Create(entity));
            }

            // report name existed
            return null;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CatalogGroupQuantumGradeModel Update(CatalogGroupQuantumGradeModel model)
        {
            // get existed by name
            var existedEntity = GetUnique(model.GroupQuantumId, model.Grade);

            // check existed
            if(existedEntity == null || existedEntity.Id == model.Id)
            {
                // init new entity
                var entity = new cat_GroupQuantumGrade();

                // set entity props
                model.FillEntity(ref entity);

                // update
                return new CatalogGroupQuantumGradeModel(cat_GroupQuantumGradeServices.Update(entity));
            }

            // report name existed
            return null;
        }

        /// <summary>
        /// Update deleted status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CatalogGroupQuantumGradeModel Delete(int id)
        {
            // get model
            var model = GetById(id);

            // check result
            if(model != null)
            {
                // set props
                model.IsDeleted = true;

                // update
                return Update(model);
            }

            // invalid param
            return null;
        }
    }
}
