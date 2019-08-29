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
    public class CatalogBasicSalaryController
    {
        /// <summary>
        /// Get by ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public static CatalogBasicSalaryModel GetById(int id)
        {
            // get entity
            var entity = cat_BasicSalaryServices.GetById(id);

            // return
            return entity != null ? new CatalogBasicSalaryModel(entity) : null;
        }

        /// <summary>
        /// Get by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static CatalogBasicSalaryModel GetByName(string name)
        {
            // check report name
            if(!string.IsNullOrEmpty(name))
            {
                // get entity
                var entity = cat_BasicSalaryServices.GetByCondition("[Name]='{0}'".FormatWith(name));

                // return
                return entity != null ? new CatalogBasicSalaryModel(entity) : null;
            }
            // invalid name
            return null;
        }

        /// <summary>
        /// Get active basic salary
        /// </summary>
        /// <returns></returns>
        public static CatalogBasicSalaryModel GetCurrent()
        {
            // get entity
            var entity = cat_BasicSalaryServices.GetCurrent();

            // return
            return entity != null ? new CatalogBasicSalaryModel(entity) : null;
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
        public static List<CatalogBasicSalaryModel> GetAll(string keyword, string group, CatalogStatus? status, bool? isDeleted, string order, int? limit)
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
                condition += @" AND [Status]='{0}'".FormatWith(status.Value);

            // is deleted
            if(isDeleted != null)
                condition += @" AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value);

            // order
            if(string.IsNullOrEmpty(order))
                order = @"[Order],[Name]";

            // return
            return cat_BasicSalaryServices.GetAll(condition, order, limit).Select(bs => new CatalogBasicSalaryModel(bs)).ToList();
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
        public static PageResult<CatalogBasicSalaryModel> GetPaging(string keyword, string group, CatalogStatus? status, bool? isDeleted, string order, int start, int limit)
        {
            // init condition
            var condition = "1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
                condition += @" AND ([Name] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());

            // group
            if(!string.IsNullOrEmpty(group))
                condition += @" AND [Group]='{0}'".FormatWith(group);

            // status 
            if(status != null)
                condition += @" AND [Status]='{0}'".FormatWith(status.Value);

            // is deleted
            if(isDeleted != null)
                condition += @" AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value);

            // order
            if(string.IsNullOrEmpty(order))
                order = @"[Order], [Name]";

            // get result
            var result = cat_BasicSalaryServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<CatalogBasicSalaryModel>(result.Total, result.Data.Select(bs => new CatalogBasicSalaryModel(bs)).ToList());
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CatalogBasicSalaryModel Create(CatalogBasicSalaryModel model)
        {
            // init entity
            var entity = new cat_BasicSalary();

            // get entity from db
            model.FillEntity(ref entity);

            // return
            return new CatalogBasicSalaryModel(cat_BasicSalaryServices.Create(entity));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CatalogBasicSalaryModel Update(CatalogBasicSalaryModel model)
        {
            // init new entity
            var entity = new cat_BasicSalary();

            // set entity props
            model.FillEntity(ref entity);

            // update
            return new CatalogBasicSalaryModel(cat_BasicSalaryServices.Update(entity));
        }

        /// <summary>
        /// Update deleted status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CatalogBasicSalaryModel Delete(int id)
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

