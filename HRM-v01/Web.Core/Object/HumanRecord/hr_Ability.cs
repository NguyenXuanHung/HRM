using System;

namespace Web.Core.Object.HumanRecord
{
    /// <summary>
    /// Hồ sơ - Khả năng
    /// </summary>
    public class hr_Ability : BaseEntity//HOSO_KHANANG
    {
        /// <summary>
        /// id ho so
        /// </summary>
        public int RecordId { get; set; }//FR_KEY
        /// <summary>
        /// id kha nang
        /// </summary>
        public int AbilityId { get; set; }//MA_KHANANG
        /// <summary>
        /// id xep loai
        /// </summary>
        public int GraduationTypeId { get; set; }//MA_XEPLOAI
        /// <summary>
        /// ghi chu
        /// </summary>
        public string Note { get; set; }//GhiChu
        /// <summary>
        /// duyet
        /// </summary>
        public bool IsApproved { get; set; }//Duyet
        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
