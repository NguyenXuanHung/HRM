using System;

namespace Web.Core.Object.HumanRecord
{
    /// <summary>
    /// Tăng giảm bảo hiểm
    /// </summary>
    public class hr_FluctuationInsurance : BaseEntity
    {
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
    }
}
