using System;

namespace Web.Core.Object.TimeSheet
{
    /// <inheritdoc />
    /// <summary>
    /// Danh mục ký hiệu chấm công
    /// </summary>
    public class hr_TimeSheetSymbol : BaseEntity
    {
        public hr_TimeSheetSymbol()
        {
            Code = "";
            Name = "";
            Description = "";
            Order = 0;
            Status = TimeSheetStatus.Active;
            IsDeleted = false;
            CreatedBy = "system";
            CreatedDate = DateTime.Now;
            EditedBy = "system";
            EditedDate = DateTime.Now;
        }

        /// <summary>
        /// Category code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Category description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Chọn loại công cộng hay trừ (Cộng = 0; Trừ = 1)
        /// </summary>
        public bool TypeWork { get; set; }

        /// <summary>
        /// Số công quy đổi
        /// </summary>
        public double WorkConvert { get; set; }

        /// <summary>
        /// Thời gian quy đổi
        /// </summary>
        public double TimeConvert { get; set; }

        /// <summary>
        /// Nhóm ký hiệu
        /// </summary>
        public int GroupSymbolId { get; set; }

        /// <summary>
        /// Màu ký hiệu
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Category order
        /// </summary>
        public int? Order { get; set; }

        /// <summary>
        /// TimeSheet status
        /// </summary>
        public TimeSheetStatus Status { get; set; }

        /// <summary>
        /// True if object was deleted
        /// </summary>
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// Created by, default system user
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Edited by, default system user
        /// </summary>
        public string EditedBy { get; set; }

        /// <summary>
        /// Edited date
        /// </summary>
        public DateTime EditedDate { get; set; }

    }
}
