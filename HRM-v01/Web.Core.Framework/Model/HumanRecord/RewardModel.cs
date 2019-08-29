using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for RewardModel
    /// </summary>
    public class RewardModel: BaseModel<hr_Reward>
    {
        private readonly hr_Reward _reward;
        private readonly hr_Record _record;

        public RewardModel()
        {
            _reward = new hr_Reward();
            _record = new hr_Record();

            //set props
            Init(_reward);
        }

        public RewardModel(hr_Reward reward)
        {
            _reward = reward ?? new hr_Reward();
          
            //init props
            Init(_reward);

            //get data relation
            var record = hr_RecordServices.GetById(_reward.RecordId);
            _record = record ?? new hr_Record();
        }

        /// <summary>
        /// 
        /// </summary>
        public int RecordId { get; set; }

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
        /// Ngày hiệu lực
        /// </summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// Lý do khen thưởng
        /// </summary>
        public string Reason { get; set; }

        //Ten hinh thuc khen thuong
        /// <summary>
        /// Tên hình thức khen thưởng
        /// </summary>
        public string FormName => cat_RewardServices.GetFieldValueById(_reward.FormRewardId);

        /// <summary>
        /// Số tiền
        /// </summary>
        public Decimal MoneyAmount { get; set; }

        /// <summary>
        /// ghi chú
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
        /// Tên khen thưởng kỉ luật
        /// </summary>
        public string LevelName => cat_LevelRewardDisciplineServices.GetFieldValueById(_reward.LevelRewardId);

        /// <summary>
        /// Id cap khen thuong ky luat
        /// </summary>
        public int LevelRewardId { get; set; }

        /// <summary>
        /// Id hinh thuc khen thuong
        /// </summary>
        public int FormRewardId { get; set; }

        /// <summary>
        /// Chức vụ người ký
        /// </summary>
        public string MakerPosition { get; set; }

        /// <summary>
        /// Cơ quan khen thưởng
        /// </summary>
        public string SourceDepartment { get; set; }

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }

        #region custom props
        /// <summary>
        /// EmployeeCode
        /// </summary>
        public string EmployeeCode => _record.EmployeeCode;

        /// <summary>
        /// FullName
        /// </summary>
        public string FullName => _record.FullName;

        /// <summary>
        /// DepartmentName
        /// </summary>
        public string DepartmentName => cat_DepartmentServices.GetFieldValueById(_record.DepartmentId);

        /// <summary>
        /// ParentDepartmentName
        /// </summary>
        public string ParentDepartmentName => cat_DepartmentServices.GetFieldValueById(_record.ManagementDepartmentId);

        #endregion
    }
}
