using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Object.Salary;
using Web.Core.Service.Salary;

namespace Web.Core.Framework.Controller
{
    public class SalaryAllowanceController
    {
        private const string ConditionDefault = @"1=1";

        public static List<SalaryAllowanceModel> GetAll(string keyword, int? salaryDecisionId, string allowanceCode, string order, int? limit)
        {
            var condition = ConditionDefault;

            if (!string.IsNullOrEmpty(keyword))
                condition += " AND [AllowanceCode] LIKE N'%{0}%'".FormatWith(keyword);

            if (salaryDecisionId != null)
                condition += " AND [SalaryDecisionId] = {0}".FormatWith(salaryDecisionId);

            if (!string.IsNullOrEmpty(allowanceCode))
                condition += " AND [AllowanceCode] = '{0}'".FormatWith(allowanceCode);

            return sal_SalaryAllowanceServices.GetAll(condition, order, limit).Select(r => new SalaryAllowanceModel(r)).ToList();
        }

        public static SalaryAllowanceModel GetById(int id)
        {
            var entity = sal_SalaryAllowanceServices.GetById(id);

            return entity != null ? new SalaryAllowanceModel(entity) : null;
        }

        public static SalaryAllowanceModel Create(SalaryAllowanceModel model)
        {
            var entity = new sal_SalaryAllowance();
            model.FillEntity(ref entity);
            return new SalaryAllowanceModel(sal_SalaryAllowanceServices.Create(entity));
        }

        public static SalaryAllowanceModel Update(SalaryAllowanceModel model)
        {
            var entity = sal_SalaryAllowanceServices.GetById(model.Id);
            if (entity == null) return null;
            model.EditedDate = DateTime.Now;
            model.FillEntity(ref entity);
            return new SalaryAllowanceModel(sal_SalaryAllowanceServices.Update(entity));
        }

        public static void Delete(int id)
        {
            //var model = GetById(id);
            //if (model == null) return null;
            //model.IsDeleted = true;
            //return Update(model);
            sal_SalaryAllowanceServices.Delete(id);
        }
    }
}
