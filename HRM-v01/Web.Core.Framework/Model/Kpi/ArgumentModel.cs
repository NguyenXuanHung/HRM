using System;
using System.ComponentModel;
using Web.Core.Object.Kpi;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for ArgumentModel
    /// </summary>
    public class ArgumentModel : BaseModel<kpi_Argument>
    {
        public ArgumentModel()
        {
            // set model default props
            Init(new kpi_Argument());
        }

        public ArgumentModel(kpi_Argument argument)
        {
            // init entity
            argument = argument ?? new kpi_Argument();

            // set model props
            Init(argument);
        }
       
        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// CalculateCode
        /// </summary>
        [DisplayName("Mã tính toán")]
        public string CalculateCode { get; set; }
       
        /// <summary>
        /// Name
        /// </summary>
        [DisplayName("Tên tham số")]
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [DisplayName("Mô tả")]
        public string Description { get; set; }

        /// <summary>
        /// data type ValueType
        /// </summary>
        [DisplayName("Loại dữ liệu"), TypeConverter(typeof(KpiValueType))]
        public KpiValueType ValueType { get; set; }

        /// <summary>
        /// Order
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// kpi status
        /// </summary>
        public KpiStatus Status { get; set; }

        /// <summary>
        /// Deleted status
        /// </summary>
        public bool IsDeleted { get; set; }

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

        #region custom properties
        /// <summary>
        /// ValueTypeName
        /// </summary>
        public string ValueTypeName => ValueType.Description();
        
        #endregion
    }
}
