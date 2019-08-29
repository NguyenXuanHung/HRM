using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Framework.Model;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework.Controller
{
    public class FluctuationInsuranceController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static FluctuationInsuranceModel GetById(int id)
        {
            var fluctuation = hr_FluctuationInsuranceServices.GetById(id);
            return new FluctuationInsuranceModel(fluctuation);
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="recordId"></param>
       /// <param name="month"></param>
       /// <param name="year"></param>
       /// <returns></returns>
        public static FluctuationInsuranceModel GetByRecordId(int recordId, int month, int year)
        {
            var condition = Constant.ConditionDefault;
            condition += " AND [RecordId] = {0}".FormatWith(recordId)+
                         " AND MONTH(EffectiveDate) = {0}".FormatWith(month)+
                         " AND YEAR(EffectiveDate) = {0}".FormatWith(year);

            var fluctuation = hr_FluctuationInsuranceServices.GetByCondition(condition);
            return fluctuation != null ? new FluctuationInsuranceModel(fluctuation) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="recordId"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<FluctuationInsuranceModel> GetAll(string keyword, string departmentIds, int? recordId, string order, int? limit)
        {
            var condition = Constant.ConditionDefault;
            // keyword
            if (!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ([RecordId] IN (SELECT Id FROM hr_Record WHERE [EmployeeCode] LIKE N'%{0}%' OR [FullName] LIKE N'%{0}%'))".FormatWith(keyword.EscapeQuote());
            }

            if (!string.IsNullOrEmpty(departmentIds))
            {
                condition += @" AND ([RecordId] IN (SELECT Id FROM hr_Record WHERE [DepartmentId] IN ({0})))".FormatWith(departmentIds);
            }

            // recordId
            if (recordId != null)
            {
                condition += " AND [RecordId] = {0}".FormatWith(recordId);
            }
           
            return hr_FluctuationInsuranceServices.GetAll(condition, order, null).Select(fi => new FluctuationInsuranceModel(fi)).ToList();
        }

        public static PageResult<FluctuationInsuranceModel> GetPaging(string keyword, string departmentIds, int? recordId, string order, int start, int limit)
        {
            // init default condition
            var condition = Constant.ConditionDefault;

            // keyword
            if (!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ([RecordId] IN (SELECT Id FROM hr_Record WHERE [EmployeeCode] LIKE N'%{0}%' OR [FullName] LIKE N'%{0}%'))".FormatWith(keyword.EscapeQuote());
            }

            if (!string.IsNullOrEmpty(departmentIds))
            {
                condition += @" AND ([RecordId] IN (SELECT Id FROM hr_Record WHERE [DepartmentId] IN ({0})))".FormatWith(departmentIds);
            }

            // recordId
            if (recordId != null)
            {
                condition += " AND [RecordId] = {0}".FormatWith(recordId);
            }

            // get result
            var result = hr_FluctuationInsuranceServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<FluctuationInsuranceModel>(result.Total,
                result.Data.Select(fi => new FluctuationInsuranceModel(fi)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public static void Update(FluctuationInsuranceModel model)
        {
            // int entity
            var entity = new hr_FluctuationInsurance();

            // fill entity
            model.FillEntity(ref entity);
           
            hr_FluctuationInsuranceServices.Update(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public static FluctuationInsuranceModel Create(FluctuationInsuranceModel model)
        {
            // init entity
            var entity = new hr_FluctuationInsurance();

            // fill entity
            model.FillEntity(ref entity);

            // create
            return new FluctuationInsuranceModel(hr_FluctuationInsuranceServices.Create(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            hr_FluctuationInsuranceServices.Delete(id);
        }
    }
}
