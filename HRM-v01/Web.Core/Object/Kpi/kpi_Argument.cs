using System;
using System.ComponentModel;

namespace Web.Core.Object.Kpi
{
    /// <summary>
    /// kpi_Argument
    /// </summary>
    public class kpi_Argument : BaseEntity
    {
        public kpi_Argument()
        {
            Name = "";
            Code = "";
            CalculateCode = "";
            Description = "";
            ValueType = KpiValueType.Number;
            Status = KpiStatus.Active;
            IsDeleted = false;
            CreatedBy = "admin";
            CreatedDate = DateTime.Now;
            EditedBy = "admin";
            EditedDate = DateTime.Now;
        }
       
        /// <summary>
        /// ImportCode
        /// </summary>
        [DisplayName("Mã")]
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
    }
}
