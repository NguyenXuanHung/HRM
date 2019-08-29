using System;

namespace Web.Core.Object.HumanRecord
{
    public class hr_FamilyRelationship : BaseEntity//HOSO_QH_GIADINH
    {
        /// <summary>
        /// id hồ sơ
        /// </summary>
        public int RecordId { get; set; } //FR_KEY
        /// <summary>
        /// Ho ten
        /// </summary>        
        public string FullName { get; set; }//HO_TEN
        /// <summary>
        /// Nam sinh
        /// </summary>        
        public int BirthYear { get; set; }//TUOI
        /// <summary>
        /// Id quan he
        /// </summary>       
        public int RelationshipId { get; set; }
        /// <summary>
        /// Nghe nghiep
        /// </summary>        
        public string Occupation { get; set; }//NGHE_NGHIEP
        /// <summary>
        /// Noi lam viec
        /// </summary>        
        public string WorkPlace { get; set; }//NOI_LAMVIEC
        /// <summary>
        /// Gioi tinh
        /// </summary>        
        public bool Sex { get; set; }//GIOI_TINH
        /// <summary>
        /// Ghi chu
        /// </summary>        
        public string Note { get; set; }//GHI_CHU
        /// <summary>
        /// La Nguoi Phu Thuoc
        /// </summary>       
        public bool IsDependent { get; set; }//LaNguoiPhuThuoc
        /// <summary>
        /// Ngay Bat Dau Giam Tru
        /// </summary>        
        public DateTime? ReduceStartDate { get; set; }//NgayBatDauGiamTru
        /// <summary>
        /// Ngay Ket Thuc Giam Tru
        /// </summary>        
        public DateTime? ReduceEndDate { get; set; }//NgayKetThucGiamTru
        /// <summary>
        /// Duyet
        /// </summary>
        public bool IsApproved { get; set; }//Duyet
        /// <summary>
        /// So chung minh thu
        /// </summary>        
        public string IDNumber { get; set; }//SoCMT
        /// <summary>
        /// Ma so thue
        /// </summary>
        public string TaxCode { get; set; }//MaSoThue

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
