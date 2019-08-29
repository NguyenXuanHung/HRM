using System.Collections.Generic;
using System.Linq;
using Web.Core.Catalog.Service;
using Web.Core.Object.Catalog;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for CatalogRelationshipController
    /// </summary>
    public class CatalogRelationshipController
    {
        /// <summary>
        /// get catalog relationship by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CatalogRelationshipModel GetById(int id)
        {
            // get entity
            var entity = cat_RelationshipServices.GetById(id);

            // return
            return entity != null ? new CatalogRelationshipModel(entity) : null;
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="group"></param>
        /// <param name="status"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<CatalogRelationshipModel> GetAll(string keyword, string group, CatalogStatus? status, bool? isDeleted, string order, int limit)
        {
            // init condition
            var condition = @"1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ([Name] LIKE N'%{0}%' OR [Code] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword);
            }

            // group
            if(!string.IsNullOrEmpty(group))
                condition += @" AND [Group]=N'{0}'".FormatWith(group);

            // status
            if(status != null)
            {               
                condition += @" AND [Status] = '{0}'".FormatWith((int)status);
            }

            // delete status
            if(isDeleted != null)
            {
                condition += @"  AND [IsDeleted] = '{0}'".FormatWith(isDeleted.Value ? 1 : 0);
            }

            // order
            if(string.IsNullOrEmpty(order))
                order = @"[Order],[Name]";

            // return
            return cat_RelationshipServices.GetAll(condition, order, limit).Select(r => new CatalogRelationshipModel(r)).ToList();
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="group"></param>
        /// <param name="status"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<CatalogRelationshipModel> GetPaging(string keyword, string group, CatalogStatus? status, bool? isDeleted, string order, int start, int limit)
        {
            // init condition
            var condition = @"1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ([Name] LIKE N'%{0}%' OR [Code] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword);
            }

            // group
            if(!string.IsNullOrEmpty(group))
                condition += @" AND [Group]=N'{0}'".FormatWith(group);

            // status
            if(status != null)
            {
                condition += @" AND [Status] = '{0}'".FormatWith((int)status);
            }

            // delete status
            if(isDeleted != null)
            {
                condition += @"  AND [IsDeleted] = '{0}'".FormatWith(isDeleted.Value ? 1 : 0);
            }

            // order
            if(string.IsNullOrEmpty(order))
                order = @"[Order],[Name]";

            // get result
            var result = cat_RelationshipServices.GetPaging(condition, order, start, limit);

            // return
            return  new PageResult<CatalogRelationshipModel>(result.Total, result.Data.Select(m => new CatalogRelationshipModel(m)).ToList());
        }


        /// <summary>
        /// insert catalog relationship by model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CatalogRelationshipModel Create(CatalogRelationshipModel model)
        {
            // init 
            var entity = new cat_Relationship();

            // fill
            model.FillEntity(ref entity);

            // return
            return new CatalogRelationshipModel(cat_RelationshipServices.Create(entity));
        }


        /// <summary>
        /// update catalog relationship by model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public  static  CatalogRelationshipModel Update(CatalogRelationshipModel model)
        {
            // init
            var entity = new cat_Relationship();

            // fill
            model.FillEntity(ref entity);

            // update entify
            return new CatalogRelationshipModel(cat_RelationshipServices.Update(entity));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CatalogRelationshipModel Delete(int id)
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

