using System;

namespace Web.Core.Object.HumanRecord
{
    /// <summary>
    /// HOSO - HOP DONG
    /// </summary>
    public class hr_Contract : BaseEntity
    {
        public hr_Contract()
        {
            CreatedBy = "system";
            CreatedDate = DateTime.Now;
            EditedBy = "system";
            EditedDate = DateTime.Now;
        }
        /// <summary>
        /// id hồ sơ
        /// </summary>        
        public int RecordId { get; set; } //FR_KEY
        /// <summary>
        /// so hop dong
        /// </summary>
        public string ContractNumber { get; set; }//SO_HDONG
        /// <summary>
        /// Id loai hop dong
        /// </summary>
        public int ContractTypeId { get; set; }//MA_LOAIHDONG
        /// <summary>
        /// id tinh trang hop dong
        /// </summary>
        public int ContractStatusId { get; set; }//MA_TT_HDONG
        /// <summary>
        /// id luong
        /// </summary>
        public int SalaryId { get; set; }// PrkeyHoSoLuong
        /// <summary>
        /// id cong viec
        /// </summary>
        public int JobId { get; set; }//MA_CONGVIEC
        /// <summary>
        /// trang thai hop dong
        /// </summary>
        public string ContractCondition { get; set; }//TrangThaiHopDong
        /// <summary>
        /// ngay hop dong
        /// </summary>
        public DateTime? ContractDate { get; set; }//NGAY_HDONG
        /// <summary>
        /// ngay ket thuc hop dong
        /// </summary>
        public DateTime? ContractEndDate { get; set; }//NGAYKT_HDONG
        /// <summary>
        /// Ngay co hieu luc
        /// </summary>
        public DateTime? EffectiveDate { get; set; }//NgayCoHieuLuc
        /// <summary>
        /// tep tin dinh kem
        /// </summary>
        public string AttachFileName { get; set; }//TepTinDinhKem
        /// <summary>
        /// ghi chu
        /// </summary>
        public string Note { get; set; }//GhiChu
        /// <summary>
        /// Nguoi dai dien ky hop dong
        /// </summary>
        public string PersonRepresent { get; set; }//PrkeyNguoiDaiDienKyHD
        /// <summary>
        /// id chuc vu nguoi ky hop dong
        /// </summary>
        public int PersonPositionId { get; set; }//MaChucVuNguoiKyHD
        /// <summary>
        /// id hình thức tuyển dụng
        /// </summary>
        public int RecruitmentTypeId { get; set; }

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
