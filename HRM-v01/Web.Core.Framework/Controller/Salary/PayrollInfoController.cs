using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Framework.Model.HumanRecord;
using Web.Core.Object.Salary;
using Web.Core.Service.Salary;

namespace Web.Core.Framework.Controller
{
    public class PayrollInfoController
    {
        /// <summary>
        /// Get payroll by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static PayrollInfoModel GetById(int id)
        {
            var entity = sal_PayrollInfoServices.GetById(id);
            return entity != null ? new PayrollInfoModel(entity) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="salaryBoardId"></param>
        /// <param name="recordId"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static PayrollInfoModel GetUnique(int salaryBoardId, int recordId, int month, int year)
        {
            var condition = " [SalaryBoardId]= {0}".FormatWith(salaryBoardId)+
                            " AND [RecordId]= {0}".FormatWith(recordId) +
                            " AND [Month]= {0}".FormatWith(month)+
                            " AND [Year]= {0} ".FormatWith(year);
            var entity = sal_PayrollInfoServices.GetByCondition(condition);
            return entity != null ? new PayrollInfoModel(entity) : null;
        }

        /// <summary>
        /// get all
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departments"></param>
        /// <param name="salaryBoardId"></param>
        /// <param name="recordId"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<PayrollInfoModel> GetAll(string keyword, string departments, int? salaryBoardId, int? recordId, int? month, int? year,
            bool? isDeleted, string order, int? limit)
        {
            // init condition
            var condition = Constant.ConditionDefault;
            // keyword
            if(!string.IsNullOrEmpty(keyword))
                condition += " AND [RecordId] IN (SELECT Id FROM hr_Record WHERE [FullName] LIKE N'%{0}%' OR [EmployeeCode] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());

            if(!string.IsNullOrEmpty(departments))
                condition += " AND [RecordId] IN (SELECT Id FROM hr_Record WHERE [DepartmentId] IN ({0}))".FormatWith(departments);
            
            // salaryBoardId
            if (salaryBoardId != null)
                condition += " AND [SalaryBoardId]= {0}".FormatWith(salaryBoardId);
            
            // recordId
            if (recordId != null)
                condition += " AND [RecordId]= {0}".FormatWith(recordId);
            
            // month
            if(month != null)
                condition += " AND [Month]= {0}".FormatWith(month);

            // year
            if(year != null)
                condition += " AND [Year]= {0} ".FormatWith(year);

            // deleted
            if(isDeleted != null)
                condition += " AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value ? 1 : 0);

            // order
            if (string.IsNullOrEmpty(order))
                order = "[Year],[Month] DESC";

            // return
            return sal_PayrollInfoServices.GetAll(condition, order, limit).Select(r => new PayrollInfoModel(r)).ToList();
        }

       /// <summary>
       /// get paging
       /// </summary>
       /// <param name="keyword"></param>
       /// <param name="departments"></param>
       /// <param name="salaryBoardId"></param>
       /// <param name="recordId"></param>
       /// <param name="month"></param>
       /// <param name="year"></param>
       /// <param name="isDeleted"></param>
       /// <param name="order"></param>
       /// <param name="start"></param>
       /// <param name="limit"></param>
       /// <returns></returns>
        public static PageResult<PayrollInfoModel> GetPaging(string keyword, string departments, int? salaryBoardId, int? recordId, int? month, int? year,
            bool? isDeleted, string order, int start, int limit)
        {
            // init condition
            var condition = Constant.ConditionDefault;
            // keyword
            if (!string.IsNullOrEmpty(keyword))
                condition += " AND [RecordId] IN (SELECT Id FROM hr_Record WHERE [FullName] LIKE N'%{0}%' OR [EmployeeCode] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());

            if (!string.IsNullOrEmpty(departments))
                condition += " AND [RecordId] IN (SELECT Id FROM hr_Record WHERE [DepartmentId] IN ({0}))".FormatWith(departments);

            // salaryBoardId
            if (salaryBoardId != null)
                condition += " AND [SalaryBoardId]= {0}".FormatWith(salaryBoardId);

            // recordId
            if (recordId != null)
                condition += " AND [RecordId]= {0}".FormatWith(recordId);

            // month
            if (month != null)
                condition += " AND [Month]= {0}".FormatWith(month);

            // year
            if (year != null)
                condition += " AND [Year]= {0} ".FormatWith(year);

            // deleted
            if (isDeleted != null)
                condition += " AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value ? 1 : 0);

            // order
            if(string.IsNullOrEmpty(order))
                order = "[Year],[Month] DESC";

            // get result
            var result = sal_PayrollInfoServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<PayrollInfoModel>(result.Total, result.Data.Select(p => new PayrollInfoModel(p)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departments"></param>
        /// <param name="salaryBoardId"></param>
        /// <param name="recordId"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<InsuranceDisplayModel> GetInsuranceProcessList(string keyword, string departments, int? salaryBoardId, int? recordId, int? month, int? year,
            bool? isDeleted, string order, int start, int limit)
        {
            var listPayroll = GetAll(keyword, departments, salaryBoardId, recordId, month, year, isDeleted, order,
                null);
            var listRecordIds = listPayroll.Select(rc => new { rc.RecordId, rc.FullName, rc.EmployeeCode, rc.DepartmentName }).Distinct().OrderBy(rc => rc.FullName).ToList();
            var startPage = start;
            var limitPage = limit;
            if (startPage + limit > listRecordIds.Count)
                limitPage = listRecordIds.Count - startPage;
            var yearFilter = DateTime.Now.Year;
            if (year != null)
                yearFilter = year.Value;
            var listResult = new List<InsuranceDisplayModel>();
            foreach (var record in listRecordIds.GetRange(startPage, limitPage))
            {
                var displayModel = new InsuranceDisplayModel(listPayroll, record.RecordId, yearFilter)
                {
                    RecordId = record.RecordId,
                    FullName = record.FullName,
                    EmployeeCode = record.EmployeeCode,
                    DepartmentName = record.DepartmentName
                };
                listResult.Add(displayModel);
            }

            // return
            return new PageResult<InsuranceDisplayModel>(listResult.Count, listResult);
        }

        /// <summary>
        /// Create payroll
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static PayrollInfoModel Create(PayrollInfoModel model)
        {
            // init new entity
            var entity = new sal_PayrollInfo();
            // set entity props
            model.FillEntity(ref entity);
            // insert
            return new PayrollInfoModel(sal_PayrollInfoServices.Create(entity));
            
        }

        /// <summary>
        /// Update payroll
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static PayrollInfoModel Update(PayrollInfoModel model)
        {
            var entity = new sal_PayrollInfo();
            // set entity props
            model.FillEntity(ref entity);
            // update
            return new PayrollInfoModel(sal_PayrollInfoServices.Update(entity));
        }

        /// <summary>
        /// Change to deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static PayrollInfoModel Delete(int id)
        {
            // get payroll by id
            var model = GetById(id);
            // check result
            if(model != null)
            {
                // update deleted status
                model.IsDeleted = true;
                // update
                return Update(model);
            }
            // invalid id
            return null;
        }
    }
}
