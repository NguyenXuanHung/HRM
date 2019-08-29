using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.Object.HumanRecord
{
    public class hr_BusinessHistory: BaseEntity
    {
        /// <summary>
        /// id hồ sơ
        /// </summary>
        public int RecordId { get; set; }
        /// <summary>
        /// Số quyết định
        /// </summary>
        public string DecisionNumber { get; set; }
        /// <summary>
        /// Trích yếu quyết định
        /// </summary>
        public string ShortDecision { get; set; }
        /// <summary>
        /// Ngày quyết định
        /// </summary>
        public DateTime? DecisionDate { get; set; }
        /// <summary>
        /// Người ký
        /// </summary>
        public string DecisionMaker { get; set; }
        /// <summary>
        ///  chức vụ người ký
        /// </summary>
        public string DecisionPosition { get; set; }
        /// <summary>
        /// Ngày hiệu lực
        /// </summary>
        public DateTime? EffectiveDate { get; set; }
        /// <summary>
        /// Thời hạn
        /// </summary>
        public DateTime? ExpireDate { get; set; }
        /// <summary>
        /// Ngày đi
        /// </summary>
        public DateTime? LeaveDate { get; set; }
        /// <summary>
        /// Dự án đi
        /// </summary>
        public string LeaveProject { get; set; }
        /// <summary>
        ///  chức vụ hiện tại
        /// </summary>
        public string CurrentPosition { get; set; }
        /// <summary>
        ///  chức vụ cũ
        /// </summary>
        public string OldPosition { get; set; }
        /// <summary>
        ///  chức vụ mới
        /// </summary>
        public string NewPosition { get; set; }
        /// <summary>
        ///  đơn vị đang công tác
        /// </summary>
        public string CurrentDepartment { get; set; }
        /// <summary>
        ///  đơn vị cũ
        /// </summary>
        public string OldDepartment { get; set; }
        /// <summary>
        /// Cơ quan
        /// </summary>
        public string SourceDepartment { get; set; }
        /// <summary>
        ///  đơn vị đích đến
        /// </summary>
        public string DestinationDepartment { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// File scan
        /// </summary>
        public string FileScan { get; set; }
        /// <summary>
        /// Loại điều động đến, hoặc điều động đi, hoặc luân chuyển,....
        /// </summary>
        public string BusinessType { get; set; }
        /// <summary>
        /// Danh hiệu thi đua
        /// </summary>
        public string EmulationTitle { get; set; }
        /// <summary>
        /// Số tiền
        /// </summary>
        public double? Money { get; set; }

        /// <summary>
        /// Chức danh quy hoạch
        /// </summary>
        public int PlanJobTitleId { get; set; }

        /// <summary>
        /// Giai đoạn quy hoạch
        /// </summary>
        public int PlanPhaseId { get; set; }

        /// <summary>
        /// Được tạo bởi
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Được chỉnh sửa bởi
        /// </summary>
        public string EditedBy { get; set; }

        /// <summary>
        /// Thời gian chỉnh sửa
        /// </summary>
        public DateTime? EditedDate { get; set; }
    }
}
