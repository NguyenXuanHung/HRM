using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Object.Kpi;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Kpi;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for EmployeeArgumentModel
    /// </summary>
    public class EmployeeArgumentModel : BaseModel<kpi_EmployeeArgument>
    {
        private readonly kpi_Argument _argument;
        private readonly hr_Record _record;
        private readonly kpi_Group _group;

        public EmployeeArgumentModel()
        {
            // set model default props
            Init(new kpi_EmployeeArgument());
            _argument = new kpi_Argument();
            _record = new hr_Record();
            _group = new kpi_Group();
        }

        public EmployeeArgumentModel(kpi_EmployeeArgument employeeArgument)
        {
            // init entity
            employeeArgument = employeeArgument ?? new kpi_EmployeeArgument();

            // set model props
            Init(employeeArgument);

            //get date relation
            _argument = kpi_ArgumentServices.GetById(employeeArgument.ArgumentId);
            _argument = _argument ?? new kpi_Argument();
            _record = hr_RecordServices.GetById(employeeArgument.RecordId);
            _record = _record ?? new hr_Record();
            _group = kpi_GroupServices.GetById(employeeArgument.GroupId);
            _group = _group ?? new kpi_Group();
        }

        /// <summary>
        /// RecordId
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// ArgumentId
        /// </summary>
        public int ArgumentId { get; set; }

        /// <summary>
        /// GroupId
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Month
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }

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
        /// ArgumentName
        /// </summary>
        public string ArgumentName => _argument.Name;
        
        /// <summary>
        /// ArgumentName
        /// </summary>
        public string ArgumentCode => _argument.Code;
        
        /// <summary>
        /// ArgumentName
        /// </summary>
        public string ArgumentCalculateCode => _argument.CalculateCode;

        /// <summary>
        /// Value type
        /// </summary>
        public KpiValueType ValueType => _argument.ValueType;

        /// <summary>
        /// ValueTypeName
        /// </summary>
        public string ValueTypeName => _argument.ValueType.Description();

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
        /// GroupName
        /// </summary>
        public string GroupName => _group.Name;

        #endregion
    }
}
