using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for RewardController
    /// </summary>
    public class RewardController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static RewardModel GetById(int id)
        {
            var recordReward = hr_RewardServices.GetById(id);
            return new RewardModel(recordReward);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rewardNumber"></param>
        /// <returns></returns>
        public static RewardModel GetRewardNumberByCondition(string rewardNumber)
        {
            var condition = @" [DecisionNumber] LIKE  N'%{0}%' ".FormatWith(rewardNumber);
            const string order = " [DecisionNumber] DESC ";
            var record = hr_RewardServices.GetByCondition(condition, order);
            return new RewardModel(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="recordId"></param>
        /// <param name="levelId"></param>
        /// <param name="formRewardId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<RewardModel> GetAll(string keyword, string departmentIds, int? recordId, int? levelId, int? formRewardId,
            DateTime? fromDate, DateTime? toDate, string order, int? limit)
        {
            var condition = Condition(keyword, departmentIds, recordId, levelId, formRewardId, fromDate, toDate);

            return hr_RewardServices.GetAll(condition, order, limit).Select(c => new RewardModel(c)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="recordId"></param>
        /// <param name="levelId"></param>
        /// <param name="formRewardId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<RewardModel> GetPaging(string keyword, string departmentIds, int? recordId, int? levelId, int? formRewardId,
            DateTime? fromDate, DateTime? toDate, string order, int start, int limit)
        {
            var condition = Condition(keyword, departmentIds, recordId, levelId, formRewardId, fromDate, toDate);

            var pageResult = hr_RewardServices.GetPaging(condition, order, start, limit);

            return new PageResult<RewardModel>(pageResult.Total, pageResult.Data.Select(r => new RewardModel(r)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="recordId"></param>
        /// <param name="levelId"></param>
        /// <param name="formRewardId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        private static string Condition(string keyword, string departmentIds, int? recordId, int? levelId, int? formRewardId, DateTime? fromDate, DateTime? toDate)
        {
            var condition = Constant.ConditionDefault;

            if (!string.IsNullOrEmpty(keyword))
                condition +=
                    " AND [RecordId] IN (SELECT Id FROM hr_Record WHERE [EmployeeCode] LIKE N'%{0}%' OR [FullName] LIKE N'%{0}%')"
                        .FormatWith(keyword.GetKeyword());
            if (!string.IsNullOrEmpty(departmentIds))
            {
                condition += @" AND ([RecordId] IN (SELECT Id FROM hr_Record WHERE [DepartmentId] IN ({0})))".FormatWith(departmentIds);
            }

            if (recordId != null)
                condition += " AND [RecordId] = {0}".FormatWith(recordId);

            if (levelId != null)
                condition += " AND [LevelRewardId] = {0}".FormatWith(levelId);

            if (formRewardId != null)
                condition += " AND [FormRewardId] = {0}".FormatWith(formRewardId);
            
            if (fromDate.HasValue)
            {
                condition += " AND [StartDate] >= '{0}'".FormatWith(fromDate.Value.ToString("yyyy-MM-dd"));
            }

            if (toDate.HasValue)
            {
                condition += " AND [StartDate] <= '{0}'".FormatWith(toDate.Value.AddDays(1).ToString("yyyy-MM-dd"));
            }

            return condition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static RewardModel Update(RewardModel model)
        {
            // int entity
            var entity = hr_RewardServices.GetById(model.Id);
            if (entity == null) return null;

            // fill entity
            model.FillEntity(ref entity);

            // update
            return new RewardModel(hr_RewardServices.Update(entity));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static RewardModel Create(RewardModel model)
        {
            // init entity
            var entity = new hr_Reward();

            // fill entity
            model.FillEntity(ref entity);

            // create
            return new RewardModel(hr_RewardServices.Create(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        public void Delete(int recordId)
        {
            hr_RewardServices.Delete(recordId);
        }        
    }
}