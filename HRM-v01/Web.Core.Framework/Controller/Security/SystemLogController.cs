using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Object.Security;
using Web.Core.Service.Security;

namespace Web.Core.Framework
{
    public class SystemLogController
    {
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SystemLogModel GetById(int id)
        {
            var entity = SystemLogServices.GetById(id);
            return entity != null ? new SystemLogModel(entity) : null;
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="action"></param>
        /// <param name="type"></param>
        /// <param name="isException"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<SystemLogModel> GetAll(string keyword, SystemAction? action, SystemLogType? type, bool? isException, 
            DateTime? fromDate, DateTime? toDate, string order, int? limit)
        {
            // init condition
            var condition = "1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
                condition += " AND ([Username] LIKE N'%{0}%' OR [Thread] LIKE N'%{0}%' OR [ShortDescription] LIKE N'%{0}%' OR [LongDescription] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());

            // action
            if(action != null)
                condition += " AND [Action]='{0}'".FormatWith((int)action.Value);

            // type
            if(type != null)
                condition += " AND [Type]='{0}'".FormatWith((int)type.Value);

            // is exception
            if(isException != null)
                condition += " AND [IsException]='{0}'".FormatWith(isException.Value ? 1 : 0);
            
            // from date
            if(fromDate != null)
                condition += " AND [CreatedDate]>'{0} 00:00:00.000'".FormatWith(fromDate.Value.ToString("yyyy-MM-dd"));

            // to date
            if(toDate != null)
                condition += " AND [CreatedDate]<'{0} 23:59:59.999'".FormatWith(toDate.Value.ToString("yyyy-MM-dd"));

            // return
            return SystemLogServices.GetAll(condition, order, limit).Select(sl => new SystemLogModel(sl)).ToList();
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="action"></param>
        /// <param name="type"></param>
        /// <param name="isException"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<SystemLogModel> GetPaging(string keyword, SystemAction? action, SystemLogType? type, bool? isException,
            DateTime? fromDate, DateTime? toDate, string order, int start, int limit)
        {
            // init condition
            var condition = "1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
                condition += " AND ([Username] LIKE N'%{0}%' OR [Thread] LIKE N'%{0}%' OR [ShortDescription] LIKE N'%{0}%' OR [LongDescription] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());

            // action
            if(action != null)
                condition += " AND [Action]='{0}'".FormatWith((int)action.Value);

            // type
            if(type != null)
                condition += " AND [Type]='{0}'".FormatWith((int)type.Value);

            // is exception
            if(isException != null)
                condition += " AND [IsException]='{0}'".FormatWith(isException.Value ? 1 : 0);

            // from date
            if(fromDate != null)
                condition += " AND [CreatedDate]>='{0} 00:00:00.000'".FormatWith(fromDate.Value.ToString("yyyy-MM-dd"));

            // to date
            if(toDate != null)
                condition += " AND [CreatedDate]<='{0} 23:59:59.999'".FormatWith(toDate.Value.ToString("yyyy-MM-dd"));

            // get data
            var result = SystemLogServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<SystemLogModel>(result.Total, result.Data.Select(sl => new SystemLogModel(sl)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SystemLogModel Create(SystemLogModel model)
        {
            // init entity
            var entity = new SystemLog();
            // set properties from model
            model.FillEntity(ref entity);
            // insert entity
            return new SystemLogModel(SystemLogServices.Create(entity));
        }
    }
}
