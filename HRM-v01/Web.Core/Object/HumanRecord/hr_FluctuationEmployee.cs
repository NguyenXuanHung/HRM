using System;

namespace Web.Core.Object.HumanRecord
{
    /// <summary>
    /// Tăng giảm nhân sự
    /// </summary>
    public class hr_FluctuationEmployee : BaseEntity
    {
        /// <summary>
        /// Record id
        /// </summary>
        public int RecordId { get; set; }
        /// <summary>
        /// Lý do
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// Ngày
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Loại: 0: tăng, 1: giảm
        /// </summary>
        public bool Type { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime EditedDate { get; set; }
        public string EditedBy { get; set; }
    }
}
