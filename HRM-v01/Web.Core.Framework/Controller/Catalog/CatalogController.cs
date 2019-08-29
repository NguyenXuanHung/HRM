using System.Collections.Generic;
using System.Linq;
using Web.Core.Object.Catalog;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for CatalogController
    /// </summary>
    public class CatalogController
    {
        /// <summary>
        /// Get catalog by ID
        /// </summary>
        /// <param name="objName">catalog type</param>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public static CatalogModel GetById(string objName, int id)
        {
            var entity = cat_BaseServices.GetById(objName, id);
            return entity != null ? new CatalogModel(objName, entity) : null;
        }

        public static CatalogModel GetByName(string objName, string name)
        {
            // check report name
            if(!string.IsNullOrEmpty(name))
            {
                var entity = cat_BaseServices.GetByName(objName, name);
                return entity != null ? new CatalogModel(objName, entity) : null;
            }
            // invalid name
            return null;
        }

        /// <summary>
        /// Get all catalog by condition
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="keyword"></param>
        /// <param name="group"></param>
        /// <param name="status"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<CatalogModel> GetAll(string objName, string keyword, string group, CatalogStatus? status, bool? isDeleted, string order, int? limit)
        {
            var catalogModels = new List<CatalogModel>();
            var lstObjects = cat_BaseServices.GetAll(objName, keyword, null, null, false, order, limit);
            if(lstObjects.Count > 0)
                catalogModels.AddRange(lstObjects.Select(o => new CatalogModel(objName, o)));
            return catalogModels;
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="keyword"></param>
        /// <param name="group"></param>
        /// <param name="status"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<CatalogModel> GetPaging(string objName, string keyword, string group, CatalogStatus? status, bool? isDeleted, string order, int start, int limit)
        {
            var catalogModels = new List<CatalogModel>();
            var pageResult = cat_BaseServices.GetPaging(objName, keyword, null, null, false, order, start, limit);
            if(pageResult.Data.Count > 0)
                catalogModels.AddRange(pageResult.Data.Select(o => new CatalogModel(objName, o)));
            return new PageResult<CatalogModel>(pageResult.Total, catalogModels);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CatalogModel Create(string objName, CatalogModel model)
        {
            // get existed by name
            var existedEntity = GetByName(objName, model.Name);
            // check existed
            if(existedEntity == null)
            {
                // report was not created
                var entity = new cat_Base();
                model.FillEntity(ref entity);
                return new CatalogModel(objName, cat_BaseServices.Create(objName, entity));
            }
            // report name existed
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CatalogModel Update(string objName, CatalogModel model)
        {
            // get existed by name
            var existedEntity = GetByName(objName, model.Name);
            // check existed
            if(existedEntity == null || existedEntity.Id == model.Id)
            {
                var entity = new cat_Base();
                model.FillEntity(ref entity);
                return new CatalogModel(objName, cat_BaseServices.Update(objName, entity));
            }
            // report name existed
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CatalogModel Delete(string objName, int id)
        {
            var model = GetById(objName, id);
            if(model != null)
            {
                model.IsDeleted = true;
                return Update(objName, model);
            }
            return null;
        }
    }

}

