using System;

namespace Web.Core.Object.HumanRecord
{
    /// <summary>
    /// Hồ sơ - đại biểu
    /// </summary>
    public class hr_Delegate : BaseEntity//HOSO_DAIBIEU
    {
        /// <summary>
        /// id ho so
        /// </summary>
        public int RecordId { get; set; }//FR_KEY
        /// <summary>
        /// tu ngay
        /// </summary>
        public DateTime? FromDate { get; set; }//TU_NGAY
        /// <summary>
        /// den ngay
        /// </summary>
        public DateTime? ToDate { get; set; }//DEN_NGAY
        /// <summary>
        /// loai hinh
        /// </summary>
        public string Type { get; set; }//LOAI_HINH
        /// <summary>
        /// nhiem ky
        /// </summary>
        public string Term { get; set; }//NHIEM_KY
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
