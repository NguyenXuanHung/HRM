using System.Collections.Generic;
using System.Linq;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for RewardController
    /// </summary>
    public class RewardDepartmentController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static RewardDepartmentModel GetById(int id)
        {
            var recordReward = hr_RewardDepartmentServices.GetById(id);
            return new RewardDepartmentModel(recordReward);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rewardNumber"></param>
        /// <returns></returns>
        public static RewardDepartmentModel GetRewardNumberByCondition(string rewardNumber)
        {
            var condition = @" [DecisionNumber] LIKE  N'%{0}%' ".FormatWith(rewardNumber);
            const string order = " [DecisionNumber] DESC ";
            var record = hr_RewardDepartmentServices.GetByCondition(condition, order);
            return new RewardDepartmentModel(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static List<RewardDepartmentModel> GetAll(int? recordId)
        {
            var rewards = hr_RewardDepartmentServices.GetAll(recordId);
            return rewards.Select(reward => new RewardDepartmentModel(reward)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Update(hr_RewardDepartment obj)
        {
            var record = hr_RewardDepartmentServices.GetById(obj.Id);
            if (record == null) return;
            record.DecisionName = obj.DecisionName;
            record.DepartmentId = obj.DepartmentId;
            record.DecisionNumber = obj.DecisionNumber;
            record.DecisionDate = obj.DecisionDate;
            record.Reason = obj.Reason;
            record.LevelRewardId = obj.LevelRewardId;
            record.MoneyAmount = obj.MoneyAmount;
            record.Note = obj.Note;
            record.DecisionMaker = obj.DecisionMaker;
            record.AttachFileName = obj.AttachFileName;
            record.FormRewardId = obj.FormRewardId;
            hr_RewardDepartmentServices.Update(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Insert(hr_RewardDepartment obj)
        {
            hr_RewardDepartmentServices.Create(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        public void Delete(int recordId)
        {
            hr_RewardDepartmentServices.Delete(recordId);
        }
    }
}