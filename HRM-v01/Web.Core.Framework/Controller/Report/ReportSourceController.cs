using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework;
using Web.Core.Framework.Controller;

namespace Web.Core.Framework
{
    public class ReportSourceController
    {
        public List<ReportSourceModel> GetAll()
        {
            // init list of report source model
            var reportSourceModels = new List<ReportSourceModel>();

            // get all payroll config
            var payrollConfigs = CatalogController.GetAll("sal_PayrollConfig", null, null, null, false, null, null);
            // check available payroll
            if (payrollConfigs.Count > 0)
            {
                foreach (var payrollConfig in payrollConfigs)
                {
                    // get list of columns
                    var columns = SalaryBoardConfigController.GetAll(payrollConfig.Id, null, null, null, null,
                        "[ColumnCode]", null).Select(s => s.ColumnCode).ToList();
                    // init report source model
                    var reportSourceModel = new ReportSourceModel {Name = payrollConfig.Name};
                    // set model props
                    // add column in to report source
                    reportSourceModel.Columns.AddRange(columns);
                    // add model into source list
                    reportSourceModels.Add(reportSourceModel);
                }
            }

            // return
            return reportSourceModels;
        }
    }
}
