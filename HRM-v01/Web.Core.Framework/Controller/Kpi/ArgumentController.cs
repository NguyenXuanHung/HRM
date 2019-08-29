using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.Kpi;
using Web.Core.Service.Kpi;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for ArgumentController
    /// </summary>
    public class ArgumentController
    {
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ArgumentModel GetById(int id)
        {
            // get entity
            var entity = kpi_ArgumentServices.GetById(id);    
            
            // return
            return entity != null ? new ArgumentModel(entity) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="calculateCode"></param>
        /// <returns></returns>
        public static ArgumentModel GetUnique(string code, string calculateCode)
        {
            // get entity
            var condition = Constant.ConditionDefault;
            if (string.IsNullOrEmpty(code) && string.IsNullOrEmpty(calculateCode))
            {
                return null;
            }

            if (!string.IsNullOrEmpty(code))
            {
                condition += " AND [Code] = '{0}'".FormatWith(code);
            }
            if (!string.IsNullOrEmpty(calculateCode))
            {
                condition += " AND [CalculateCode] = '{0}'".FormatWith(calculateCode);
            }

            condition += " AND [IsDeleted] = 0";
            var entity = kpi_ArgumentServices.GetByCondition(condition);    
            
            // return
            return entity != null ? new ArgumentModel(entity) : null;
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="isDeleted"></param>
        /// <param name="kpiStatus"></param>
        /// <param name="valueType"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<ArgumentModel> GetAll(string keyword, bool? isDeleted, KpiStatus? kpiStatus, KpiValueType? valueType, string order, int? limit)
        {
            // init default condition
            var condition = Constant.ConditionDefault;

            // keyword
            if(!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ([Code] LIKE N'%{0}%' OR [CalculateCode] LIKE N'%{0}%' OR [Name] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());
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

            if (valueType != null)
            {
                condition += " AND [ValueType] = {0}".FormatWith((int)valueType);
            }

            // return
            return kpi_ArgumentServices.GetAll(condition, order, limit).Select(c => new ArgumentModel(c)).ToList();
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="isDeleted"></param>
        /// <param name="kpiStatus"></param>
        /// <param name="valueType"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<ArgumentModel> GetPaging(string keyword, bool? isDeleted, KpiStatus? kpiStatus, KpiValueType? valueType, string order, int start, int limit)
        {
            // init default condition
            var condition = Constant.ConditionDefault;

            // keyword
            if(!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ([Code] LIKE N'%{0}%' OR [CalculateCode] LIKE N'%{0}%' OR [Name] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());
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

            if (valueType != null)
            {
                condition += " AND [ValueType] = {0}".FormatWith((int)valueType);
            }

            // get result
            var result = kpi_ArgumentServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<ArgumentModel>(result.Total, result.Data.Select(c => new ArgumentModel(c)).ToList());
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ArgumentModel Create(ArgumentModel model)
        {
            // init entity
            var entity = new kpi_Argument();

            // check code unique
            var existedCode = GetUnique(model.Code, null);

            // check calculate code unique
            var existCalculateCode = GetUnique(null, model.CalculateCode);

            if (existCalculateCode != null || existedCode != null) return null;

            // fill entity
            model.FillEntity(ref entity);

            // create
            return new ArgumentModel(kpi_ArgumentServices.Create(entity));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ArgumentModel Update(ArgumentModel model)
        {
            // int entity
            var entity = new kpi_Argument();

            // check code unique
            var existedCode = GetUnique(model.Code, null);

            if (existedCode != null && existedCode.Id != model.Id) return null;

            // check calculate code unique
            var existCalculateCode = GetUnique(null, model.CalculateCode);

            if (existCalculateCode != null && existCalculateCode.Id != model.Id) return null;

            // fill entity
            model.FillEntity(ref entity);

            // update
            return new ArgumentModel(kpi_ArgumentServices.Update(entity));
        }

        /// <summary>
        /// Update deleted status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ArgumentModel Delete(int id)
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
