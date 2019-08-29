using System.Collections.Generic;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for FamilyRelationshipController
    /// </summary>
    public class FamilyRelationshipController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static FamilyRelationshipModel GetById(int id)
        {
            var recordRelation = hr_FamilyRelationshipServices.GetById(id);
            return new FamilyRelationshipModel(recordRelation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static List<FamilyRelationshipModel> GetAll(int? recordId)
        {
            var relationModels = new List<FamilyRelationshipModel>();
            var relations = hr_FamilyRelationshipServices.GetAll(recordId);
            foreach (var relation in relations)
            {
                relationModels.Add(new FamilyRelationshipModel(relation));
            }
            return relationModels;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public void Insert(hr_FamilyRelationship info)
        {
            hr_FamilyRelationshipServices.Create(info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public void Update(hr_FamilyRelationship info)
        {
            var record = hr_FamilyRelationshipServices.GetById(info.Id);
            if (record == null) return;
            //set new properties
            record.FullName = info.FullName;
            record.BirthYear = info.BirthYear;
            record.RelationshipId = info.RelationshipId;
            record.Occupation = info.Occupation;
            record.WorkPlace = info.WorkPlace;
            record.Sex = info.Sex;
            record.Note = info.Note;
            record.IsDependent = info.IsDependent;
            record.IDNumber = info.IDNumber;
            record.ReduceStartDate = info.ReduceStartDate;
            record.ReduceEndDate = info.ReduceEndDate;
            record.CreatedDate = info.CreatedDate;
            record.EditedDate = info.EditedDate;
            hr_FamilyRelationshipServices.Update(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        public void Delete(int recordId)
        {
            hr_FamilyRelationshipServices.Delete(recordId);
        }
    }
}