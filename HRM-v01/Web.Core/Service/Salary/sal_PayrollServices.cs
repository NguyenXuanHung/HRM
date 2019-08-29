using System;
using Web.Core.Object.Salary;

namespace Web.Core.Service.Salary
{
    public class sal_PayrollServices : BaseServices<sal_Payroll>
    {
        public static PageResult<sal_Payroll> GetPaging(string departments, int start, int pageSize)
        {
            // init condition
            var condition = " 1=1 ";
            if (!string.IsNullOrEmpty(departments))
            {
                // department
                var arrDepartment = departments.Split(new[] { ',' }, StringSplitOptions.None);
                for (var i = 0; i < arrDepartment.Length; i++)
                {
                    arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
                }
                condition += @" AND [DepartmentId] IN ({0})".FormatWith(string.Join(",", arrDepartment));
            }
            // return
            return SQLHelper.FindPaging<sal_Payroll>(condition, null, start, pageSize);
        }
    }
}
