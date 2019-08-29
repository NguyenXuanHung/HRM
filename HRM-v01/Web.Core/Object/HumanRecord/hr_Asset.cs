using System;

namespace Web.Core.Object.HumanRecord
{
    /// <summary>
    /// Ho so - tai san
    /// </summary>
    public class hr_Asset : BaseEntity//HOSO_TAISAN
    {
        /// <summary>
        /// id ho so
        /// </summary>
        public int RecordId { get; set; }//FR_KEY
        /// <summary>
        /// Ma tai san
        /// </summary>
        public string AssetCode { get; set; }//MA_TAISAN
        /// <summary>
        /// ten tai san
        /// </summary>
        public string AssetName { get; set; }//TEN_VTHH
        /// <summary>
        /// so luong
        /// </summary>
        public int Quantity { get; set; }//SoLuong
        /// <summary>
        /// ma don vi tinh
        /// </summary>
        public string UnitCode { get; set; }//MaDonViTinh
        /// <summary>
        /// ngay nhan
        /// </summary>
        public DateTime? ReceiveDate { get; set; }//NGAY_NHAN
        /// <summary>
        /// tinh trang
        /// </summary>
        public string Status { get; set; }//TINH_TRANG
        /// <summary>
        /// ghi chu
        /// </summary>
        public string Note { get; set; }//GhiChu
        /// <summary>
        /// duyet
        /// </summary>
        public bool IsApproved { get; set; }//Duyet
        /// <summary>
        /// ngay ban giao
        /// </summary>
        public DateTime? DeliveryDate { get; set; }//NgayBanGiao
        /// <summary>
        /// ghi chu sau ban giao
        /// </summary>
        public string NoteDeliveryAfter { get; set; }//GhiChuSauBanGiao
        /// <summary>
        /// tep tin dinh kem
        /// </summary>
        public string AttachFileName { get; set; }//TinTinDinhKem
        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
