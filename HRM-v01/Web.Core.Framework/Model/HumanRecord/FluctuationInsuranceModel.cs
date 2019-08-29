using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework.Model
{
    public class FluctuationInsuranceModel: BaseModel<hr_FluctuationInsurance>
    {
        private readonly hr_FluctuationInsurance _fluctuationEmployee;
        private readonly hr_Record _record;

        public FluctuationInsuranceModel()
        {
            // set model default props
            Init(new hr_FluctuationInsurance());
            _record = new hr_Record();
        }
        
        public FluctuationInsuranceModel (hr_FluctuationInsurance fluctuation)
        {
            _fluctuationEmployee = fluctuation ?? new hr_FluctuationInsurance();

            // set model props
            Init(_fluctuationEmployee);
            _record = hr_RecordServices.GetById(_fluctuationEmployee.RecordId);
            _record = _record ?? new hr_Record();
        }

        /// <summary>
        /// Record id
        /// </summary>
        public int RecordId { get; set; }
        /// <summary>
        /// Lý do
        /// </summary>
        public int ReasonId { get; set; }

        /// <summary>
        /// Ngày hiệu lực
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// Loại: 1: tăng, 2: giảm, 3: thay đổi
        /// </summary>
        public InsuranceType Type { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime EditedDate { get; set; }
        public string EditedBy { get; set; }

        #region custom props
        /// <summary>
        /// Tên lý do
        /// </summary>
        public string ReasonName => cat_ReasonInsuranceServices.GetFieldValueById(_fluctuationEmployee.ReasonId);
        /// <summary>
        /// FullName
        /// </summary>
        public string FullName => _record.FullName;
        /// <summary>
        /// EmployeeCode
        /// </summary>
        public string EmployeeCode => _record.EmployeeCode;
        /// <summary>
        /// DepartmentName
        /// </summary>
        public string DepartmentName => cat_DepartmentServices.GetFieldValueById(_record.DepartmentId);

        /// <summary>
        /// Tên loại bảo hiểm
        /// </summary>
        public string TypeName => _fluctuationEmployee.Type.Description();

        #endregion
    }
}
