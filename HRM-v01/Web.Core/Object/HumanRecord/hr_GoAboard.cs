using System;

namespace Web.Core.Object.HumanRecord
{
    /// <summary>
    /// Hồ sơ - di nuoc ngoai
    /// </summary>
    public class hr_GoAboard : BaseEntity//HOSO_DINUOCNGOAI
    {
        /// <summary>
        /// id ho so
        /// </summary>
        public int RecordId { get; set; }//PrKeyHoSo
        /// <summary>
        /// id quoc gia
        /// </summary>
        public int NationId { get; set; }//MA_NUOC
        /// <summary>
        /// ngay bat dau
        /// </summary>
        public DateTime? StartDate { get; set; }//BAT_DAU
        /// <summary>
        /// ngay ket thuc
        /// </summary>
        public DateTime? EndDate { get; set; }//KET_THUC
        /// <summary>
        /// ly do
        /// </summary>
        public string Reason { get; set; }//LY_DO
        /// <summary>
        /// ghi chu
        /// </summary>
        public string Note { get; set; }//GhiChu
                                        /// <summary>
                                        /// Đơn vị tài trợ
                                        /// </summary>
        public string SponsorDepartment { get; set; }
        /// <summary>
        /// Cơ quan quyết định
        /// </summary>
        public string SourceDepartment { get; set; }
        /// <summary>
        /// Số quyết định
        /// </summary>
        public string DecisionNumber { get; set; }
        /// <summary>
        /// Ngày quyết định
        /// </summary>
        public DateTime? DecisionDate { get; set; }
        /// <summary>
        /// Người ký
        /// </summary>
        public string DecisionMaker { get; set; }
        /// <summary>
        /// Chức vụ người ký
        /// </summary>
        public string MakerPosition { get; set; }
        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
