using System.Collections.Generic;
using System.Linq;
using Web.Core.Object.Catalog;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for CatalogController
    /// </summary>
    public class CatalogHolidayController
    {
        /// <summary>
        /// Get by ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public static CatalogHolidayModel GetById(int id)
        {
            // get entity
            var entity = cat_HolidayServices.GetById(id);

            // return
            return entity != null ? new CatalogHolidayModel(entity) : null;
        }

        /// <summary>
        /// Get by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static CatalogHolidayModel GetByName(string name)
        {
            // check report name
            if(!string.IsNullOrEmpty(name))
            {
                // get entity
                var entity = cat_HolidayServices.GetByCondition("[Name]='{0}'".FormatWith(name));

                // return
                return entity != null ? new CatalogHolidayModel(entity) : null;
            }
            // invalid name
            return null;
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
        public static List<CatalogHolidayModel> GetAll(string keyword, string group, CatalogStatus? status, bool? isDeleted, string order, int? limit)
        {
            // init condition
            var condition = "1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
                condition += @" AND ([Name] LIKE N'%{0}%' OR [Code] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());
            
            // group
            if(!string.IsNullOrEmpty(group))
                condition += @" AND [Group]=N'{0}'".FormatWith(group);

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
            return cat_HolidayServices.GetAll(condition, order, limit).Select(h => new CatalogHolidayModel(h)).ToList();
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
        public static PageResult<CatalogHolidayModel> GetPaging(string keyword, string group, CatalogStatus? status, bool? isDeleted, string order, int start, int limit)
        {
            // init condition
            var condition = "1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
                condition += @" AND ([Name] LIKE N'%{0}%' OR [Code] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());
            
            // group
            if(!string.IsNullOrEmpty(group))
                condition += @" AND [Group]=N'{0}'".FormatWith(group);

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
            var result = cat_HolidayServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<CatalogHolidayModel>(result.Total, result.Data.Select(h => new CatalogHolidayModel(h)).ToList());
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CatalogHolidayModel Create(CatalogHolidayModel model)
        {
            // init entity
            var entity = new cat_Holiday();

            // get entity from db
            model.FillEntity(ref entity);

            // return
            return new CatalogHolidayModel(cat_HolidayServices.Create(entity));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CatalogHolidayModel Update(CatalogHolidayModel model)
        {
            // init new entity
            var entity = new cat_Holiday();

            // set entity props
            model.FillEntity(ref entity);

            // update
            return new CatalogHolidayModel(cat_HolidayServices.Update(entity));
        }

        /// <summary>
        /// Update deleted status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CatalogHolidayModel Delete(int id)
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

