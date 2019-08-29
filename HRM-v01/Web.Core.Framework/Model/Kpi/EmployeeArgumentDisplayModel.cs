using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Object.HumanRecord;
using Web.Core.Object.Kpi;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Kpi;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for EmployeeArgumentDisplayModel
    /// </summary>
    public class EmployeeArgumentDisplayModel
    {
        private readonly List<EmployeeArgumentModel> _employeeArgumentModels;
        private readonly int _recordId;
        public EmployeeArgumentDisplayModel(List<EmployeeArgumentModel> employeeArgumentModels, int recordId)
        {
            _employeeArgumentModels = employeeArgumentModels;
            _recordId = recordId;
        }

        /// <summary>
        /// RecordId
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// DepartmentName
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string FullName { get; set; }

        public List<ArgumentDetailModel> ArgumentDetailModels
        {
            get
            {
                var result = new List<ArgumentDetailModel>();
                var arguments = ArgumentController.GetAll(null, false, KpiStatus.Active, null, null, null);
                foreach (var argument in arguments)
                {
                    var argumentDetailModel = new ArgumentDetailModel()
                    {
                        ArgumentId = argument.Id,
                        Code = argument.Code,
                        Name = argument.Name,
                        Value = ""
                    };
                    var employeeArg = _employeeArgumentModels.Where(rc => rc.RecordId == _recordId && rc.ArgumentId == argument.Id).ToList();
                    if (employeeArg.Count > 0)
                    {
                        argumentDetailModel.Value = employeeArg[0].Value;
                    }
                    
                    result.Add(argumentDetailModel);
                }
              
                return result;
            }
        }
    }
}
