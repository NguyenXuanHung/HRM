using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Object.Catalog;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework
{
    public class CatalogLocationController
    {
        private  const string ObjName = "cat_Location";

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CatalogLocationModel GetById(int id)
        {
            // get entity
            var entity = cat_LocationServices.GetById(id);

            // return
            return entity != null ? new CatalogLocationModel(entity) : null;
        }

        /// <summary>
        /// Get by name and parent
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static CatalogLocationModel GetUnique(int parentId, string name)
        {
            // check report name
            if(parentId >= 0 && !string.IsNullOrEmpty(name))
            {
                // get entity
                var entity = cat_LocationServices.GetByCondition("[ParentId]='{0}' AND [Name]='{1}'".FormatWith(parentId, name.EscapeQuote()));

                // return
                return entity != null ? new CatalogLocationModel(entity) : null;
            }

            // invalid params
            return null;
        }

        /// <summary>
        /// Get all catalog by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="parentId"></param>
        /// <param name="group"></param>
        /// <param name="status"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<CatalogLocationModel> GetAll(string keyword, int? parentId, string group, CatalogStatus? status, bool? isDeleted, string order, int? limit)
        {
            // init condition
            var condition = "1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
                condition += @" AND ([Name] LIKE N'%{0}%' OR [Code] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());

            // parent
            if(parentId != null)
                condition += @" AND [ParentId]='{0}'".FormatWith(parentId.Value);

            // group
            if(!string.IsNullOrEmpty(group))
            {
                var arrGroup = string.IsNullOrEmpty(group) ? new string[] { } : group.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                condition += @" AND [Group] IN ({0})".FormatWith(string.Join(",", arrGroup.Select(g => "'{0}'".FormatWith(g.EscapeQuote()))));
            }

            // status 
            if(status != null)
                condition += @" AND [Status]='{0}'".FormatWith((int)status.Value);

            // is deleted
            if(isDeleted != null)
                condition += @" AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value);

            // order
            if(string.IsNullOrEmpty(order))
                order = @"[Order],[Name]";

            // return
            return cat_LocationServices.GetAll(condition, order, limit).Select(l => new CatalogLocationModel(l)).ToList();
        }

        /// <summary>
        /// Get paging
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="parentId"></param>
        /// <param name="group"></param>
        /// <param name="status"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<CatalogLocationModel> GetPaging(string keyword, int? parentId, string group, CatalogStatus? status, bool? isDeleted, string order, int start, int limit)
        {
            // init condition
            var condition = "1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
                condition += @" AND ([Name] LIKE N'%{0}%' OR [Code] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());

            // parent
            if(parentId != null)
                condition += @" AND [ParentId]='{0}'".FormatWith(parentId.Value);

            // group
            if (!string.IsNullOrEmpty(group))
            {
                var arrGroup = string.IsNullOrEmpty(group) ? new string[] { } : group.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                condition += @" AND [Group] IN ({0})".FormatWith(string.Join(",", arrGroup.Select(g => "'{0}'".FormatWith(g.EscapeQuote()))));
            }

            // status 
            if(status != null)
                condition += @" AND [Status]='{0}'".FormatWith((int)status.Value);

            // is deleted
            if(isDeleted != null)
                condition += @" AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value);

            // order
            if(string.IsNullOrEmpty(order))
                order = @"[Order],[Name]";

            // get result
            var result = cat_LocationServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<CatalogLocationModel>(result.Total, result.Data.Select(l => new CatalogLocationModel(l)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CatalogLocationModel Create(CatalogLocationModel model)
        {
            // get existed by name
            var existedEntity = GetUnique(model.ParentId, model.Name);

            // check existed
            if(existedEntity == null)
            {
                // init entity
                var entity = new cat_Location();

                // get entity from db
                model.FillEntity(ref entity);

                // return
                return new CatalogLocationModel(cat_LocationServices.Create(entity));
            }

            // report name existed
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CatalogLocationModel Update(CatalogLocationModel model)
        {
            // get existed by name
            var existedEntity = GetUnique(model.ParentId, model.Name);

            // check existed
            if(existedEntity == null || existedEntity.Id == model.Id)
            {
                // init new entity
                var entity = new cat_Location();

                // set entity props
                model.FillEntity(ref entity);

                // return
                return new CatalogLocationModel(cat_LocationServices.Update(entity));
            }

            // report name existed
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CatalogLocationModel Delete(int id)
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
