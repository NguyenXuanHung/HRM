using System;
using Web.Core.Object.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for BusinessHistoryModel
    /// </summary>
    public class BusinessHistoryModel
    {
        private readonly hr_BusinessHistory _businessHistory;

        public BusinessHistoryModel(hr_BusinessHistory business)
        {
            _businessHistory = business ?? new hr_BusinessHistory();

            RecordId = _businessHistory.RecordId;
            Id = _businessHistory.Id;
            DecisionDate = _businessHistory.DecisionDate;
            DecisionMaker = _businessHistory.DecisionMaker;
            DecisionNumber = _businessHistory.DecisionNumber;
            DecisionPosition = _businessHistory.DecisionPosition;
            ShortDecision = _businessHistory.ShortDecision;
            EffectiveDate = _businessHistory.EffectiveDate;
            ExpireDate = _businessHistory.ExpireDate;
            LeaveDate = _businessHistory.LeaveDate;
            LeaveProject = _businessHistory.LeaveProject;
            Description = _businessHistory.Description;
            CurrentPosition = _businessHistory.CurrentPosition;
            CurrentDepartment = _businessHistory.CurrentDepartment;
            OldPosition = _businessHistory.OldPosition;
            OldDepartment = _businessHistory.OldDepartment;
            NewPosition = _businessHistory.NewPosition;
            SourceDepartment = _businessHistory.SourceDepartment;
            DestinationDepartment = _businessHistory.DestinationDepartment;
            FileScan = _businessHistory.FileScan;
            BusinessType = _businessHistory.BusinessType;
            EmulationTitle = _businessHistory.EmulationTitle;
            Money = _businessHistory.Money;
            PlanJobTitleId = _businessHistory.PlanJobTitleId;
            PlanPhaseId = _businessHistory.PlanPhaseId;
        }
        public int Id { get; set; }
        /// <summary>
        /// id ho so
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
    }
}
