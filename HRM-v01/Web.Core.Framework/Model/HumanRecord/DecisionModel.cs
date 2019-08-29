using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for DecisionModel
    /// </summary>
    public class DecisionModel : BaseModel<hr_Decision>
    {
        private readonly hr_Record _record;
        private readonly hr_Decision _decision;
        public DecisionModel()
        {
            // set model default props
            Init(new hr_Decision());
            _record = new hr_Record();
        }

        public DecisionModel(hr_Decision decision)
        {
            // init entity
            _decision = decision ?? new hr_Decision();

            // set model props
            Init(_decision);

            //get data relation
            _record = hr_RecordServices.GetById(_decision.RecordId);
            _record = _record ?? new hr_Record();
           
        }

        /// <summary>
        /// RecordId
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// ReasonId
        /// </summary>
        public int ReasonId { get; set; }

        /// <summary>
        /// Tên quyết định
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Loại quyết định
        /// </summary>
        public DecisionType Type { get; set; }

        /// <summary>
        /// Ngày quyết định
        /// </summary>
        public DateTime DecisionDate { get; set; }

        /// <summary>
        /// System, created user
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// System, created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// System, edited user
        /// </summary>
        public string EditedBy { get; set; }

        /// <summary>
        /// System, edited date
        /// </summary>
        public DateTime EditedDate { get; set; }

        #region Custom props
       
        /// <summary>
        /// ValueTypeName
        /// </summary>
        public string DecisionTypeName => _decision.Type.Description();

        /// <summary>
        /// EmployeeCode
        /// </summary>
        public string EmployeeCode => _record.EmployeeCode;

        /// <summary>
        /// FullName
        /// </summary>
        public string FullName => _record.FullName;

        /// <summary>
        /// DepartmentId
        /// </summary>
        public int DepartmentId => _record.DepartmentId;

        /// <summary>
        /// DepartmentName
        /// </summary>
        public string DepartmentName => cat_DepartmentServices.GetFieldValueById(_record.DepartmentId);

        /// <summary>
        /// ReasonName
        /// </summary>
        public string ReasonName => cat_ReasonInsuranceServices.GetFieldValueById(_decision.ReasonId);

        #endregion
    }
}
