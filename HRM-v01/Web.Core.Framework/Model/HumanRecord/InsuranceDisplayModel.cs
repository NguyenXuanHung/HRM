using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.Framework.Model.HumanRecord
{
    public class InsuranceDisplayModel
    {
        private readonly List<PayrollInfoModel> _payrollInfoModels;
        private readonly int _recordId;
        private readonly int _year;
        public InsuranceDisplayModel(List<PayrollInfoModel> payrollInfoModels, int recordId, int year)
        {
            _payrollInfoModels = payrollInfoModels;
            _recordId = recordId;
            _year = year;
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
        
        public List<InsuranceDetailModel> InsuranceDetailModels
        {
            get
            {
                var result = new List<InsuranceDetailModel>();
                for (var i = 1; i <= 12; i++)
                {
                    var payrollByMonth = _payrollInfoModels.Where(pi =>
                        pi.RecordId == _recordId && pi.Year == _year && pi.Month == i).ToList();
                    //create model
                    var detailModel = new InsuranceDetailModel()
                    {
                        Month = i,
                        Year = _year,
                        EnterpriseSocial = 0,
                        LaborerSocial = 0
                    };
                    
                    decimal totalEnterpriseSocial = 0;
                    decimal totalLaborerSocial = 0;
                    foreach (var item in payrollByMonth)
                    {
                        totalEnterpriseSocial += item.EnterpriseSocial;
                        totalLaborerSocial += item.LaborerSocial;
                    }
                    //set value
                    detailModel.EnterpriseSocial = totalEnterpriseSocial;
                    detailModel.LaborerSocial = totalLaborerSocial;
                    result.Add(detailModel);
                }
              
                return result;
            }
        }
    }
}
