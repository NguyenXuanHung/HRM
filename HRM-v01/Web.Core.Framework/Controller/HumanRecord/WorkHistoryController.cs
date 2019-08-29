using System.Collections.Generic;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for WorkHistoryController
    /// </summary>
    public class WorkHistoryController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static WorkHistoryModel GetById(int id)
        {
            var recordWork = hr_WorkHistoryServices.GetById(id);
            return new WorkHistoryModel(recordWork);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static List<WorkHistoryModel> GetAll(int? recordId)
        {
            var workModels = new List<WorkHistoryModel>();
            var works = hr_WorkHistoryServices.GetAll(recordId);
            foreach (var work in works)
            {
                workModels.Add(new WorkHistoryModel(work));
            }
            return workModels;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void UpdateWorkHistory(hr_WorkHistory obj)
        {
            var record = hr_WorkHistoryServices.GetById(obj.Id);
            if (record == null) return;
            //set new properties
            record.FromDate = obj.FromDate;
            record.ToDate = obj.ToDate;
            record.Note = obj.Note;
            record.CreatedBy = obj.CreatedBy;
            record.CreatedDate = obj.CreatedDate;
            record.EditedDate = obj.EditedDate;
            record.WorkPlace = obj.WorkPlace;
            record.WorkPosition = obj.WorkPosition;
            record.SalaryLevel = obj.SalaryLevel;
            record.ReasonLeave = obj.ReasonLeave;
            record.RecordId = obj.RecordId;
            record.ExperienceWork = obj.ExperienceWork;

            hr_WorkHistoryServices.Update(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void InsertWorkHistory(hr_WorkHistory obj)
        {
            hr_WorkHistoryServices.Create(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        public void DeleteWorkHistory(int recordId)
        {
            hr_WorkHistoryServices.Delete(recordId);
        }
    }
}