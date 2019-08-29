using System;

namespace Web.Core.Object.HumanRecord
{
    public class hr_RewardDepartment : BaseEntity//HOSO_KHENTHUONG_PHONGBAN
    {
        /// <summary>
        /// So quyet dinh
        /// </summary>        
        public string DecisionNumber { get; set; }//SO_QD
        /// <summary>
        /// Ma don vi khen thuong
        /// </summary>
        public int DepartmentId { get; set; } //MA_DONVIKT
        /// <summary>
        /// Ten quyet dinh
        /// </summary>
        public string DecisionName { get; set; }//TEN_QD
        /// <summary>
        /// Ngay quyet dinh
        /// </summary>
        public DateTime? DecisionDate { get; set; }//NGAY_QD
        /// <summary>
        /// Ly do khen thuong
        /// </summary>
        public string Reason { get; set; }//LYDO_KTHUONG
        /// <summary>
        /// So tien
        /// </summary>        
        public Decimal MoneyAmount { get; set; }//SO_TIEN
        /// <summary>
        /// Ghi chu
        /// </summary>
        public string Note { get; set; }//GHI_CHU
        /// <summary>
        /// Nguoi quyet dinh
        /// </summary>
        public string DecisionMaker { get; set; }//NguoiQuyetDinh
        /// <summary>
        /// Ten file dinh kem
        /// </summary>
        public string AttachFileName { get; set; }//TepTinDinhKem
        /// <summary>
        /// Duyet
        /// </summary>
        public bool IsApproved { get; set; }//Duyet
        /// <summary>
        /// Id cap khen thuong ky luat
        /// </summary>
        public int LevelRewardId { get; set; }//MA_CAPKHENTHUONGKYLUAT

        public int FormRewardId { get; set; }//MA_HT_KHENTHUONGKYLUAT

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
