using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for GoAboardController
    /// </summary>
    public class GoAboardController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static GoAboardModel GetById(int id)
        {
            var recordGoAboard = hr_GoAboardServices.GetById(id);
            return new GoAboardModel(recordGoAboard);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="recordId"></param>
        /// <param name="nationId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<GoAboardModel> GetAll(string keyword, string departmentIds, int? recordId, int? nationId,
            DateTime? fromDate, DateTime? toDate, string order, int? limit)
        {
            var condition = Condition(keyword, departmentIds, recordId, nationId, fromDate, toDate);

            return hr_GoAboardServices.GetAll(condition, order, limit).Select(c => new GoAboardModel(c)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="recordId"></param>
        /// <param name="nationId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<GoAboardModel> GetPaging(string keyword, string departmentIds, int? recordId, int? nationId,
            DateTime? fromDate, DateTime? toDate, string order, int start, int limit)
        {
            var condition = Condition(keyword, departmentIds, recordId, nationId, fromDate, toDate);

            var pageResult = hr_GoAboardServices.GetPaging(condition, order, start, limit);

            return new PageResult<GoAboardModel>(pageResult.Total, pageResult.Data.Select(r => new GoAboardModel(r)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="recordId"></param>
        /// <param name="nationId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        private static string Condition(string keyword, string departmentIds, int? recordId, int? nationId, DateTime? fromDate, DateTime? toDate)
        {
            var condition = Constant.ConditionDefault;

            if (!string.IsNullOrEmpty(keyword))
                condition +=
                    " AND [RecordId] IN (SELECT Id FROM hr_Record WHERE [EmployeeCode] LIKE N'%{0}%' OR [FullName] LIKE N'%{0}%')"
                        .FormatWith(keyword.GetKeyword());
            if (!string.IsNullOrEmpty(departmentIds))
            {
                condition += @" AND ([RecordId] IN (SELECT Id FROM hr_Record WHERE [DepartmentId] IN ({0})))".FormatWith(departmentIds);
            }

            if (recordId != null)
                condition += " AND [RecordId] = {0}".FormatWith(recordId);

            if (nationId != null)
                condition += " AND [NationId] = {0}".FormatWith(nationId);

            if (fromDate.HasValue)
            {
                condition += " AND [StartDate] >= '{0}'".FormatWith(fromDate.Value.ToString("yyyy-MM-dd"));
            }

            if (toDate.HasValue)
            {
                condition += " AND [StartDate] <= '{0}'".FormatWith(toDate.Value.AddDays(1).ToString("yyyy-MM-dd"));
            }

            return condition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static GoAboardModel Update(GoAboardModel model)
        {
            // int entity
            var entity = hr_GoAboardServices.GetById(model.Id);
            if (entity == null) return null;

            // fill entity
            model.FillEntity(ref entity);

            // update
            return new GoAboardModel(hr_GoAboardServices.Update(entity));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static GoAboardModel Create(GoAboardModel model)
        {
            // init entity
            var entity = new hr_GoAboard();

            // fill entity
            model.FillEntity(ref entity);

            // create
            return new GoAboardModel(hr_GoAboardServices.Create(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            hr_GoAboardServices.Delete(id);
        }
    }
}
