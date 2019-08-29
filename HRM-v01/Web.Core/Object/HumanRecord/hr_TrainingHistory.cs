using System;

namespace Web.Core.Object.HumanRecord
{
    /// <summary>
    /// HOSO - danh muc nhan vien tham gia dao tao
    /// </summary>
    public class hr_TrainingHistory : BaseEntity//DM_NhanVienThamGiaDaoTao
    {
        /// <summary>
        /// id can bo
        /// </summary>
        public int RecordId { get; set; }//MaCanBo
        /// <summary>
        /// Ten khoa dao tao
        /// </summary>
        public string TrainingName { get; set; }//TenKhoaDaoTao
        /// <summary>
        /// ghi chu
        /// </summary>
        public string Note { get; set; }//GhiChu
        /// <summary>
        /// ngay bat dau
        /// </summary>
        public DateTime? StartDate { get; set; }//BatDau
        /// <summary>
        /// ngay ket thuc
        /// </summary>
        public DateTime? EndDate { get; set; }//KetThuc
        /// <summary>
        /// id quoc gia
        /// </summary>
        public int NationId { get; set; }//MaQuocGia
        /// <summary>
        /// ly do dao tao
        /// </summary>
        public string Reason { get; set; }//LyDoDaoTao
        /// <summary>
        /// noi dao tao
        /// </summary>
        public string TrainingPlace { get; set; }//NoiDaoTao
        /// <summary>
        /// so quyet dinh
        /// </summary>
        public string DecisionNumber { get; set; }//SoQuyetDinh
        /// <summary>
        /// ngay quyet dinh
        /// </summary>
        public DateTime? DecisionDate { get; set; }//NgayQuyetDinh
        /// <summary>
        /// id he thong dao tao
        /// </summary>
        public int TrainingSystemId { get; set; }//MA_HT_DAOTAO
        /// <summary>
        /// Đơn vị tài trợ
        /// </summary>
        public string SponsorDepartment { get; set; }
        /// <summary>
        /// Cơ quan quyết định
        /// </summary>
        public string SourceDepartment { get; set; }
        /// <summary>
        /// Người ký
        /// </summary>
        public string DecisionMaker { get; set; }
        /// <summary>
        /// Chức vụ người ký
        /// </summary>
        public string MakerPosition { get; set; }

        /// <summary>
        /// id lĩnh vực đào tạo
        /// </summary>
        public int FieldTrainingId { get; set; }

        /// <summary>
        /// id đơn vị tổ chức
        /// </summary>
        public int OrganizeDepartmentId { get; set; }

        /// <summary>
        /// Số hiệu văn bản
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Trạng thái đào tạo
        /// </summary>
        public TrainingStatus TrainingStatusId { get; set; }

        /// <summary>
        /// Loại đào tạo
        /// </summary>
        public string Type { get; set; }

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
