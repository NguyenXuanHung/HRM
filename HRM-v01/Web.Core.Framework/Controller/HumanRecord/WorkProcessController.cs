using System.Collections.Generic;
using System.Linq;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for WorkProcessController
    /// </summary>
    public class WorkProcessController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static WorkProcessModel GetById(int id)
        {
            var recordWork = hr_WorkProcessServices.GetById(id);
            return new WorkProcessModel(recordWork);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static List<WorkProcessModel> GetAll(int? recordId)
        {
            var workModels = new List<WorkProcessModel>();
            var works = hr_WorkProcessServices.GetAll(recordId);
            foreach (var work in works)
            {
                workModels.Add(new WorkProcessModel(work));
            }
            return workModels;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        public void Delete(int recordId)
        {
            hr_WorkProcessServices.Delete(recordId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public void Update(hr_WorkProcess info)
        {
            var record = hr_WorkProcessServices.GetById(info.Id);
            if (record == null) return;
            // set new properties
            record.EffectiveDate = info.EffectiveDate;
            record.EffectiveEndDate = info.EffectiveEndDate;
            record.DecisionDate = info.DecisionDate;
            record.DecisionMaker = info.DecisionMaker;
            record.DecisionNumber = info.DecisionNumber;
            record.NewDepartmentId = info.NewDepartmentId;
            record.NewJobId = info.NewJobId;
            record.NewPositionId = info.NewPositionId;
            record.CreatedDate = info.CreatedDate;
            record.EditedDate = info.EditedDate;
            record.AttachFileName = info.AttachFileName;
            record.RecordId = info.RecordId;
            record.Note = info.Note;
            record.MakerPosition = info.MakerPosition;
            record.SourceDepartment = info.SourceDepartment;
            record.ExpireDate = info.ExpireDate;
            hr_WorkProcessServices.Update(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public void Insert(hr_WorkProcess info)
        {
            hr_WorkProcessServices.Create(info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<WorkProcessModel> GetNewestDecision(int id)
        {
            //init search query
            var searchQuery = @" [RecordId] = '{0}'".FormatWith(id);
            const string order = @" [DecisionDate] DESC";
            var lstWorkProcess = new List<WorkProcessModel>();
            var result = hr_WorkProcessServices.GetAll(searchQuery, order);
            foreach (var record in result)
            {
                lstWorkProcess.Add(new WorkProcessModel(record));
            }
            return new List<WorkProcessModel> {lstWorkProcess.FirstOrDefault()};
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rewardNumber"></param>
        /// <returns></returns>
        public static WorkProcessModel GetWorkProcessNumberByCondition(string rewardNumber)
        {
            var condition = @" [DecisionNumber] LIKE  N'%{0}%' ".FormatWith(rewardNumber);
            const string order = " [DecisionNumber] DESC ";
            var record = hr_WorkProcessServices.GetByCondition(condition, order);
            return new WorkProcessModel(record);
        }
    }
}