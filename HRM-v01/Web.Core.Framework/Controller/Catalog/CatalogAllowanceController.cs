using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Object.Catalog;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for CatalogController
    /// </summary>
    public class CatalogAllowanceController
    {
        /// <summary>
        /// Get by ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public static CatalogAllowanceModel GetById(int id)
        {
            // get entity
            var entity = cat_AllowanceServices.GetById(id);

            // return
            return entity != null ? new CatalogAllowanceModel(entity) : null;
        }

        /// <summary>
        /// Get by name
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static CatalogAllowanceModel GetByCode(string code)
        {
            // check report name
            if(!string.IsNullOrEmpty(code))
            {
                // get entity
                var entity = cat_AllowanceServices.GetByCode(code);

                // return
                return entity != null ? new CatalogAllowanceModel(entity) : null;
            }
            // invalid name
            return null;
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="group"></param>
        /// <param name="valueType"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<CatalogAllowanceModel> GetAll(string keyword, string group, AllowanceValueType? valueType, AllowanceType? type, CatalogStatus? status, bool? isDeleted, string order, int? limit)
        {
            // init condition
            var condition = "1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
                condition += @" AND ([Name] LIKE N'%{0}%' OR [Code] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());

            // group
            if(!string.IsNullOrEmpty(group))
            {
                var arrGroup = string.IsNullOrEmpty(group) ? new string[] { } : group.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                condition += @" AND [Group] IN ({0})".FormatWith(string.Join(",", arrGroup.Select(g => "'{0}'".FormatWith(g.EscapeQuote()))));
            }

            // value type
            if(valueType != null)
                condition += @" AND [ValueType]='{0}'".FormatWith((int)valueType.Value);

            // type
            if(type != null)
                condition += @" AND [Type]='{0}'".FormatWith((int)type.Value);

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
            return cat_AllowanceServices.GetAll(condition, order, limit).Select(a => new CatalogAllowanceModel(a)).ToList();
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="group"></param>
        /// <param name="valueType"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<CatalogAllowanceModel> GetPaging(string keyword, string group, AllowanceValueType? valueType, AllowanceType? type, CatalogStatus? status, bool? isDeleted, string order, int start, int limit)
        {
            // init condition
            var condition = "1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
                condition += @" AND ([Name] LIKE N'%{0}%' OR [Code] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());

            // group
            if(!string.IsNullOrEmpty(group))
            {
                var arrGroup = string.IsNullOrEmpty(group) ? new string[] { } : group.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                condition += @" AND [Group] IN ({0})".FormatWith(string.Join(",", arrGroup.Select(g => "'{0}'".FormatWith(g.EscapeQuote()))));
            }

            // value type
            if (valueType != null)
                condition += @" AND [ValueType]='{0}'".FormatWith((int)valueType.Value);

            // type
            if (type != null)
                condition += @" AND [Type]='{0}'".FormatWith((int)type.Value);

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
            var result = cat_AllowanceServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<CatalogAllowanceModel>(result.Total, result.Data.Select(a => new CatalogAllowanceModel(a)).ToList());
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CatalogAllowanceModel Create(CatalogAllowanceModel model)
        {
            // get existed by name
            var existedEntity = GetByCode(model.Code);

            // check existed
            if (existedEntity == null)
            {
                // init entity
                var entity = new cat_Allowance();

                // get entity from db
                model.FillEntity(ref entity);

                // return
                return new CatalogAllowanceModel(cat_AllowanceServices.Create(entity));
            }

            // invalid param
            return null;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CatalogAllowanceModel Update(CatalogAllowanceModel model)
        {
            // get existed by name
            var existedEntity = GetByCode(model.Code);

            // check existed
            if(existedEntity == null || existedEntity.Id == model.Id)
            {
                // init new entity
                var entity = new cat_Allowance();

                // set entity props
                model.FillEntity(ref entity);

                // update
                return new CatalogAllowanceModel(cat_AllowanceServices.Update(entity));
            }

            // invalid param
            return null;
        }

        /// <summary>
        /// Update deleted status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CatalogAllowanceModel Delete(int id)
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

