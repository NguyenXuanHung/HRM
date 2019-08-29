using System;

namespace Web.Core.Object.HumanRecord
{
    public class hr_WorkProcess : BaseEntity//HOSO_QT_DIEUCHUYEN
    {
        /// <summary>
        /// id hồ sơ
        /// </summary>
        public int RecordId { get; set; } //FR_KEY
        /// <summary>
        /// So quyet dinh
        /// </summary>
        public string DecisionNumber { get; set; }//SoQuyetDinh
        /// <summary>
        /// Ngay quyet dinh
        /// </summary>
        public DateTime? DecisionDate { get; set; }//NgayQuyetDinh
        /// <summary>
        /// Nguoi quyet dinh
        /// </summary>
        public string DecisionMaker { get; set; }//NguoiQuyetDinh
        /// <summary>
        /// Ngay co hieu luc
        /// </summary>
        public DateTime? EffectiveDate { get; set; }//NgayCoHieuLuc
        /// <summary>
        /// Ngay het hieu luc
        /// </summary>
        public DateTime? EffectiveEndDate { get; set; }//NgayHetHieuLuc
        /// <summary>
        /// Id bo phan moi
        /// </summary>
        public int NewDepartmentId { get; set; }//MaBoPhanMoi
        /// <summary>
        /// Id bo phan cu
        /// </summary>
        public int OldDepartmentId { get; set; }//MaBoPhanCu
        /// <summary>
        /// id cong viec cu
        /// </summary>
        public int OldJobId { get; set; }//MaCongViecCu
        /// <summary>
        /// id cong viec moi
        /// </summary>
        public int NewJobId { get; set; }//MaCongViecMoi
        /// <summary>
        /// Tep tin dinh kem
        /// </summary>
        public string AttachFileName { get; set; }//TepTinDinhKem
        /// <summary>
        /// Duyet
        /// </summary>        
        public bool IsApproved { get; set; }//Duyet
        /// <summary>
        /// Ghi chu
        /// </summary>
        public string Note { get; set; }// GhiChu
        /// <summary>
        /// id chuc vu moi
        /// </summary>
        public int NewPositionId { get; set; }//MaChucVuMoi
         /// <summary>
        /// id chuc vu cu
        /// </summary>
        public int OldPositionId { get; set; }//MaChucVuCu
        /// <summary>
        /// Thời hạn
        /// </summary>
        public DateTime? ExpireDate { get; set; }
        /// <summary>
        /// Chức vụ người kí
        /// </summary>
        public string MakerPosition { get; set; }
        /// <summary>
        /// Cơ quan
        /// </summary>
        public string SourceDepartment { get; set; }

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
