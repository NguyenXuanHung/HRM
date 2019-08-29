using System.Collections.Generic;
using System.Linq;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for RewardController
    /// </summary>
    public class DisciplineDepartmentController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DisciplineDepartmentModel GetById(int id)
        {
            var recordReward = hr_DisciplineDepartmentServices.GetById(id);
            return new DisciplineDepartmentModel(recordReward);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rewardNumber"></param>
        /// <returns></returns>
        public static DisciplineDepartmentModel GetRewardNumberByCondition(string rewardNumber)
        {
            var condition = @" [DecisionNumber] LIKE  N'%{0}%' ".FormatWith(rewardNumber);
            const string order = " [DecisionNumber] DESC ";
            var record = hr_DisciplineDepartmentServices.GetByCondition(condition, order);
            return new DisciplineDepartmentModel(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static List<DisciplineDepartmentModel> GetAll(int? recordId)
        {
            var rewards = hr_DisciplineDepartmentServices.GetAll(recordId);
            return rewards.Select(reward => new DisciplineDepartmentModel(reward)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Update(hr_DisciplineDepartment obj)
        {
            var record = hr_DisciplineDepartmentServices.GetById(obj.Id);
            if(record == null) return;
            record.DecisionName = obj.DecisionName;
            record.DepartmentId = obj.DepartmentId;
            record.DecisionNumber = obj.DecisionNumber;
            record.DecisionDate = obj.DecisionDate;
            record.Reason = obj.Reason;
            record.LevelDisciplineId = obj.LevelDisciplineId;
            record.MoneyAmount = obj.MoneyAmount;
            record.Note = obj.Note;
            record.DecisionMaker = obj.DecisionMaker;
            record.AttachFileName = obj.AttachFileName;
            record.FormDisciplineId = obj.FormDisciplineId;
            hr_DisciplineDepartmentServices.Update(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Insert(hr_DisciplineDepartment obj)
        {
            hr_DisciplineDepartmentServices.Create(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        public void Delete(int recordId)
        {
            hr_DisciplineDepartmentServices.Delete(recordId);
        }
    }
}