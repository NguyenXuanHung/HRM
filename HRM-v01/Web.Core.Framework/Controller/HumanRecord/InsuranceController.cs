using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for InsuranceController
    /// </summary>
    public class InsuranceController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static InsuranceModel GetById(int id)
        {
            var entity = hr_InsuranceServices.GetById(id);
            return new InsuranceModel(entity);
        }

        public static List<InsuranceModel> GetAll(int? recordId)
        {
            var insurances = new List<InsuranceModel>();
            var insurancesModel = hr_InsuranceServices.GetAll(recordId);
            foreach (var insurance in insurancesModel)
            {
                insurances.Add(new InsuranceModel(insurance));
            }
            return insurances;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static List<InsuranceModel> GetAll(string keyword, string recordIds, int? positionId, int? departmentId, DateTime? fromDate, DateTime? toDate, string order, int? limit)
        {
            var condition = Constant.ConditionDefault;

            if (!string.IsNullOrEmpty(keyword))
            {
                condition += " AND ("
                             + "(SELECT rc.EmployeeCode FROM hr_Record rc WHERE rc.Id = hr_Insurance.RecordId) LIKE N'%{0}%'"
                                 .FormatWith(keyword)
                             + "OR (SELECT rc.FullName FROM hr_Record rc WHERE rc.Id = hr_Insurance.RecordId) LIKE N'%{0}%'"
                                 .FormatWith(keyword)
                             + ")";
            }

            if(!string.IsNullOrEmpty(recordIds))
                condition += " AND [RecordId] IN ({0})".FormatWith(recordIds);

            if (positionId != null)
                condition += " AND [PositionId] = {0}".FormatWith(positionId);

            if (departmentId != null)
                condition += " AND [DepartmentId] = {0}".FormatWith(departmentId);

            if (fromDate != null)
                condition += " AND [StartDate] IS NOT NULL AND YEAR([StartDate]) = {0} AND MONTH([StartDate]) >= {1} AND DAY([StartDate]) >= {2}"
                    .FormatWith(fromDate.Value.Year, fromDate.Value.Month, fromDate.Value.Day);

            if (toDate != null)
                condition += " AND [StartDate] IS NOT NULL AND YEAR([StartDate]) = {0} AND MONTH([StartDate]) <= {1} AND DAY([StartDate]) <= {2}"
                    .FormatWith(toDate.Value.Year, toDate.Value.Month, toDate.Value.Day);

            return hr_InsuranceServices.GetAll(condition, order, limit).Select(i => new InsuranceModel(i)).ToList();
        }

        public static PageResult<InsuranceModel> GetPaging(string keyword, string recordIds, int? positionId, int? departmentId, DateTime? fromDate, DateTime? toDate, string order, int start, int limit)
        {
            var condition = Constant.ConditionDefault;

            if(!string.IsNullOrEmpty(keyword))
            {
                condition += " AND ("
                             + "(SELECT rc.EmployeeCode FROM hr_Record rc WHERE rc.Id = hr_Insurance.RecordId) LIKE N'%{0}%'"
                                 .FormatWith(keyword)
                             + "OR (SELECT rc.FullName FROM hr_Record rc WHERE rc.Id = hr_Insurance.RecordId) LIKE N'%{0}%'"
                                 .FormatWith(keyword)
                             + ")";
            }

            if(!string.IsNullOrEmpty(recordIds))
                condition += " AND [RecordId] IN ({0})".FormatWith(recordIds);

            if(positionId != null)
                condition += " AND [PositionId] = {0}".FormatWith(positionId);

            if(departmentId != null)
                condition += " AND [DepartmentId] = {0}".FormatWith(departmentId);

            if(fromDate != null)
                condition += " AND [StartDate] IS NOT NULL AND YEAR([StartDate]) = {0} AND MONTH([StartDate]) >= {1} AND DAY([StartDate]) >= {2}"
                    .FormatWith(fromDate.Value.Year, fromDate.Value.Month, fromDate.Value.Day);

            if(toDate != null)
                condition += " AND [StartDate] IS NOT NULL AND YEAR([StartDate]) = {0} AND MONTH([StartDate]) <= {1} AND DAY([StartDate]) <= {2}"
                    .FormatWith(toDate.Value.Year, toDate.Value.Month, toDate.Value.Day);

            var result = hr_InsuranceServices.GetPaging(condition, order, start, limit);

            return new PageResult<InsuranceModel>(result.Total, result.Data.Select(r => new InsuranceModel(r)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public static InsuranceModel Insert(InsuranceModel model)
        {
            var entity = new hr_Insurance();
            model.FillEntity(ref entity);
            return new InsuranceModel(hr_InsuranceServices.Create(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public static InsuranceModel Update(InsuranceModel model)
        {
            var entity = hr_InsuranceServices.GetById(model.Id);
            if (entity == null) return null;
            model.FillEntity(ref entity);
            return new InsuranceModel(hr_InsuranceServices.Update(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            hr_InsuranceServices.Delete(id);
        }
    }
}
