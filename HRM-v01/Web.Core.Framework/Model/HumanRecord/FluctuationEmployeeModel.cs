using System;
using Web.Core.Object.HumanRecord;

namespace Web.Core.Framework.Model
{
    public class FluctuationEmployeeModel
    {
        private readonly hr_FluctuationEmployee _fluctuationEmployee;

        public FluctuationEmployeeModel(hr_FluctuationEmployee fluctuation)
        {
            _fluctuationEmployee = fluctuation ?? new hr_FluctuationEmployee();

            Id = _fluctuationEmployee.Id;
            RecordId = _fluctuationEmployee.RecordId;
            Reason = _fluctuationEmployee.Reason;
            Date = _fluctuationEmployee.Date;
            Type = _fluctuationEmployee.Type;

        }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// RecordId
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
    }
}
