using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for AbilityModel
    /// </summary>
    public class AbilityModel
    {
        private readonly hr_Ability _ability;

        public AbilityModel(hr_Ability ability)
        {
            _ability = ability ?? new hr_Ability();

            RecordId = _ability.RecordId;
            AbilityId = _ability.AbilityId;
            GraduationTypeId = _ability.GraduationTypeId;
            Note = _ability.Note;
            IsApproved = _ability.IsApproved;
            Id = _ability.Id;
        }

        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// id ho so
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// id kha nang
        /// </summary>
        public int AbilityId { get; set; }

        /// <summary>
        /// ten kha nang
        /// </summary>
        public string AbilityName
        {
            get { return cat_AbilityServices.GetFieldValueById(_ability.AbilityId); }
        }

        /// <summary>
        /// id xep loai
        /// </summary>
        public int GraduationTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GraduationTypeName
        {
            get { return cat_GraduationTypeServices.GetFieldValueById(_ability.GraduationTypeId); }
        }

        /// <summary>
        /// ghi chu
        /// </summary>
        public string Note { get; set; }
        
        /// <summary>
        /// duyet
        /// </summary>
        public bool IsApproved { get; set; }

    }
}
