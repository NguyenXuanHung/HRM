using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for RewardModel
    /// </summary>
    public class RewardDepartmentModel
    {
        private readonly hr_RewardDepartment _rewardDepartment;

        public RewardDepartmentModel(hr_RewardDepartment reward)
        {
            _rewardDepartment = reward ?? new hr_RewardDepartment();
            DepartmentId = _rewardDepartment.DepartmentId;
            DecisionNumber = _rewardDepartment.DecisionNumber;
            DecisionName = _rewardDepartment.DecisionName;
            DecisionDate = _rewardDepartment.DecisionDate;
            Reason = _rewardDepartment.Reason;
            MoneyAmount = _rewardDepartment.MoneyAmount;
            Note = _rewardDepartment.Note;
            DecisionMaker = _rewardDepartment.DecisionMaker;
            IsApproved = _rewardDepartment.IsApproved;
            AttachFileName = _rewardDepartment.AttachFileName;
            LevelRewardId = _rewardDepartment.LevelRewardId;
            FormRewardId = _rewardDepartment.FormRewardId;
            Id = _rewardDepartment.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DepartmentName => cat_DepartmentServices.GetFieldValueById(_rewardDepartment.DepartmentId);

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DecisionNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DecisionName { get; set; }

        /// <summary>
        /// Ngày quyết định
        /// </summary>
        public DateTime? DecisionDate { get; set; }

        /// <summary>
        /// Lý do khen thưởng
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Số tiền
        /// </summary>
        public Decimal MoneyAmount { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Người quyết định
        /// </summary>
        public string DecisionMaker { get; set; }

        /// <summary>
        /// Têm file đính kèm
        /// </summary>
        public string AttachFileName { get; set; }

        /// <summary>
        /// Duyệt
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Số điểm
        /// </summary>
        public double Point { get; set; }

        /// <summary>
        /// Tên cấp khen thưởng kỷ luật
        /// </summary>
        public string LevelRewardName => cat_LevelRewardDisciplineServices.GetFieldValueById(_rewardDepartment.LevelRewardId);

        /// <summary>
        /// 
        /// </summary>
        public string FormRewardName => cat_RewardServices.GetFieldValueById(_rewardDepartment.FormRewardId);

        /// <summary>
        /// Id cap khen thuong ky luat
        /// </summary>
        public int LevelRewardId { get; set; }

        /// <summary>
        /// Id hinh thuc khen thuong
        /// </summary>
        public int FormRewardId { get; set; }
    }
}
