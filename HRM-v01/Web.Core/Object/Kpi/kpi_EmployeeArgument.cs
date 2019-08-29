using System;

namespace Web.Core.Object.Kpi
{
    /// <summary>
    /// kpi_EmployeeArgument
    /// </summary>
    public class kpi_EmployeeArgument : BaseEntity
    {
        public kpi_EmployeeArgument()
        {
            CreatedBy = "admin";
            CreatedDate = DateTime.Now;
            EditedBy = "admin";
            EditedDate = DateTime.Now;
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
    }
}
