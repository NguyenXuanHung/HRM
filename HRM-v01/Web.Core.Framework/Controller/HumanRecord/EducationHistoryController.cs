using System.Collections.Generic;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    public class EducationHistoryController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static EducationHistoryModel GetById(int id)
        {
            var recordCertificate = hr_EducationHistoryServices.GetById(id);
            return new EducationHistoryModel(recordCertificate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static List<EducationHistoryModel> GetAll(int? recordId)
        {
            var educationHistoryModels = new List<EducationHistoryModel>();
            var educationHistories = hr_EducationHistoryServices.GetAll(recordId);
            foreach (var educationHistory in educationHistories)
            {
                educationHistoryModels.Add(new EducationHistoryModel(educationHistory));
            }
            return educationHistoryModels;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void UpdateEducation(hr_EducationHistory obj)
        {
            var record = hr_EducationHistoryServices.GetById(obj.Id);
            if (record == null) return;
            //set new properties
            record.UniversityId = obj.UniversityId;
            record.TrainingSystemId = obj.TrainingSystemId;
            record.NationId = obj.NationId;
            record.IndustryId = obj.IndustryId;
            record.EducationId = obj.EducationId;
            record.FromDate = obj.FromDate;
            record.ToDate = obj.ToDate;
            record.GraduationTypeId = obj.GraduationTypeId;
            record.IsApproved = obj.IsApproved;
            record.IsGraduated = obj.IsGraduated;

            hr_EducationHistoryServices.Update(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void InsertEducation(hr_EducationHistory obj)
        {
            hr_EducationHistoryServices.Create(obj);           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteEducationHistory(int Id)
        {
            hr_EducationHistoryServices.Delete(Id);
        }
    }
}

