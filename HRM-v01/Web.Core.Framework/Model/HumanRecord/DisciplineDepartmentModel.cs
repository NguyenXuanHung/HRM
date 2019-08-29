using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for RewardModel
    /// </summary>
    public class DisciplineDepartmentModel
    {
        public hr_DisciplineDepartment DisciplineDepartment;

        public DisciplineDepartmentModel(hr_DisciplineDepartment reward)
        {
            DisciplineDepartment = reward ?? new hr_DisciplineDepartment();
            DepartmentId = DisciplineDepartment.DepartmentId;
            DecisionNumber = DisciplineDepartment.DecisionNumber;
            DecisionName = DisciplineDepartment.DecisionName;
            DecisionDate = DisciplineDepartment.DecisionDate;
            Reason = DisciplineDepartment.Reason;
            MoneyAmount = DisciplineDepartment.MoneyAmount;
            Note = DisciplineDepartment.Note;
            DecisionMaker = DisciplineDepartment.DecisionMaker;
            IsApproved = DisciplineDepartment.IsApproved;
            AttachFileName = DisciplineDepartment.AttachFileName;
            LevelRewardId = DisciplineDepartment.LevelDisciplineId;
            FormRewardId = DisciplineDepartment.FormDisciplineId;
            Id = DisciplineDepartment.Id;
        }

        public int DepartmentId { get; set; }

        public string DepartmentName => cat_DepartmentServices.GetFieldValueById(DisciplineDepartment.DepartmentId);

        public int Id { get; set; }

        /// <summary>
        /// Số quyết định
        /// </summary>
        public string DecisionNumber { get; set; }

        /// <summary>
        /// Tên quyết định
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
        /// Tên file đính kèm
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
        /// Tên cấp khen thưởng kỉ luật
        /// </summary>
        public string LevelRewardName => cat_LevelRewardDisciplineServices.GetFieldValueById(DisciplineDepartment.LevelDisciplineId);

        /// <summary>
        /// 
        /// </summary>
        public string FormRewardName => cat_DisciplineServices.GetFieldValueById(DisciplineDepartment.FormDisciplineId);

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
