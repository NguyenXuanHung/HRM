using System;

namespace Web.Core.Object.HumanRecord
{
    /// <summary>
    /// Hồ sơ - tep tin dinh kem
    /// </summary>
    public class hr_AttachFile : BaseEntity//HOSO_TepTinDinhKem
    {
        /// <summary>
        /// id ho so
        /// </summary>
        public int RecordId { get; set; }//FR_KEY
        /// <summary>
        /// ten tep tin dinh kem
        /// </summary>
        public string AttachFileName { get; set; }// TenTepTin
        /// <summary>
        /// duong dan URL
        /// </summary>
        public string URL { get; set; }//Link
        /// <summary>
        /// kich thuoc file
        /// </summary>
        public double SizeKB { get; set; }//SizeKB
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
