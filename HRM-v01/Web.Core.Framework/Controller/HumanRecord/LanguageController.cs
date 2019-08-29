using System.Collections.Generic;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for LanguageController
    /// </summary>
    public class LanguageController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static LanguageModel GetById(int id)
        {
            var recordLanguage = hr_LanguageServices.GetById(id);
            return new LanguageModel(recordLanguage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static List<LanguageModel> GetAll(int? recordId)
        {
            var languageModels = new List<LanguageModel>();
            var languages = hr_LanguageServices.GetAll(recordId);
            foreach (var language in languages)
            {
                languageModels.Add(new LanguageModel(language));
            }
            return languageModels;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="language"></param>
        public void Update(hr_Language language)
        {
            var record = hr_LanguageServices.GetById(language.Id);
            if (record == null) return;
            record.Note = language.Note;
            record.LanguageId = language.LanguageId;
            record.Rank = language.Rank;
            record.EditedDate = language.EditedDate;
            record.CreatedDate = language.CreatedDate;
            hr_LanguageServices.Update(language);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="language"></param>
        public void Insert(hr_Language language)
        {
            hr_LanguageServices.Create(language);
        }
    }
}