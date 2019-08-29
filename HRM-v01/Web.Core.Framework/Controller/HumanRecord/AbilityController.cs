using System.Collections.Generic;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for AbilityController
    /// </summary>
    public class AbilityController
    {        
        /// <summary>
        /// Get ability by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static AbilityModel GetById(int id)
        {
            var recordAbility = hr_AbilityServices.GetById(id);
            return new AbilityModel(recordAbility);
        }

        /// <summary>
        /// Get all ability
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static List<AbilityModel> GetAll(int? recordId)
        {
            var abilityModels = new List<AbilityModel>();
            var abilitys = hr_AbilityServices.GetAll(recordId);
            foreach (var ability in abilitys)
            {
                abilityModels.Add(new AbilityModel(ability));
            }
            return abilityModels;
        }

        /// <summary>
        /// Insert ability
        /// </summary>
        /// <param name="info"></param>
        public void Insert(hr_Ability info)
        {
            hr_AbilityServices.Create(info);
        }

        /// <summary>
        /// Update ability
        /// </summary>
        /// <param name="info"></param>
        public void Update(hr_Ability info)
        {
            var record = hr_AbilityServices.GetById(info.Id);
            if(record != null)
            {
                record.Note = info.Note;
                record.AbilityId = info.AbilityId;
                record.GraduationTypeId = info.GraduationTypeId;
                hr_AbilityServices.Update(record);
            }
        }

        /// <summary>
        /// Delete ability
        /// </summary>
        /// <param name="recordId"></param>
        public void Delete(int recordId)
        {
            hr_AbilityServices.Delete(recordId);
        }
    }
}
