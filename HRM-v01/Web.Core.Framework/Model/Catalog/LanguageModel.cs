using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for LanguageModel
    /// </summary>
    public class LanguageModel
    {
        private readonly hr_Language _language;

        public LanguageModel(hr_Language language)
        {
            _language = language ?? new hr_Language();

            Id = _language.Id;
            RecordId = _language.RecordId;
            Note = _language.Note;
            Rank = _language.Rank;
            LanguageId = _language.LanguageId;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LanguageName
        {
            get
            {
                return cat_LanguageLevelServices.GetFieldValueById(_language.LanguageId);
            }
        }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Cấp bậc
        /// </summary>
        public string Rank { get; set; }

        /// <summary>
        /// Ngoại ngữ
        /// </summary>
        public int LanguageId { get; set; }
    }
}

