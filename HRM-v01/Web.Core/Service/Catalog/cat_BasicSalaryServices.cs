using System;
using Web.Core.Object.Catalog;


namespace Web.Core.Service.Catalog
{
    public class  cat_BasicSalaryServices: BaseServices<cat_BasicSalary>
    {
        public static cat_BasicSalary GetCurrent()
        {
            // init condition
            var condition = @"[AppliedDate]<'{0}'".FormatWith(DateTime.Now.ToString("yyyy-MM-dd"));

            // get entity
            return cat_BasicSalaryServices.GetByCondition(condition, "[AppliedDate] DESC");
        }
    }
}
