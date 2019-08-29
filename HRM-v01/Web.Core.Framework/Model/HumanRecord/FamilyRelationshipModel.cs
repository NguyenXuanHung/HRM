using System;
using Web.Core.Catalog.Service;
using Web.Core.Object.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for FamilyRelationshipModel
    /// </summary>
    public class FamilyRelationshipModel
    {
        private readonly hr_FamilyRelationship _familyRelationship;

        public FamilyRelationshipModel(hr_FamilyRelationship familyRelationship)
        {
            _familyRelationship = familyRelationship ?? new hr_FamilyRelationship();
            RecordId = _familyRelationship.RecordId;
            FullName = _familyRelationship.FullName;
            BirthYear = _familyRelationship.BirthYear;
            Occupation = _familyRelationship.Occupation;
            WorkPlace = _familyRelationship.WorkPlace;
            Sex = _familyRelationship.Sex;
            Note = _familyRelationship.Note;
            IsApproved = _familyRelationship.IsApproved;
            IsDependent = _familyRelationship.IsDependent;
            IDNumber = _familyRelationship.IDNumber;
            TaxCode = _familyRelationship.TaxCode;
            RelationshipId = _familyRelationship.RelationshipId;
            ReduceStartDate = _familyRelationship.ReduceStartDate;
            ReduceEndDate = _familyRelationship.ReduceEndDate;
            Id = _familyRelationship.Id;
        }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Mã hồ sơ
        /// </summary>        
        public int RecordId { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>        
        public string FullName { get; set; }

        /// <summary>
        /// Năm sinh
        /// </summary>
        public int BirthYear { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Occupation { get; set; }

        /// <summary>
        /// Nơi làm việc
        /// </summary>
        public string WorkPlace { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public bool Sex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SexName {
            get { return _familyRelationship.Sex ? "M" : "F"; }
            set { _familyRelationship.Sex = value == "M" ? true : false; }
        }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDependent { get; set; }

        /// <summary>
        /// Được duyệt
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// SO CMT
        /// </summary>        
        public string IDNumber { get; set; }

        /// <summary>
        /// Ma so thue
        /// </summary>        
        public string TaxCode { get; set; }

        /// <summary>
        /// Ngay bat dau giam tru
        /// </summary>        
        public DateTime? ReduceStartDate { get; set; }
        
        /// <summary>
        /// 
        /// Ngay ket thuc giam tru
        /// </summary>
        public DateTime? ReduceEndDate { get; set; }
        
        /// <summary>
        /// Ten quan he
        /// </summary>
        public string RelationName
        {
            get
            {
                var relation = cat_RelationshipServices.GetById(_familyRelationship.RelationshipId);
                return relation != null ? relation.Name : "";
            }
        }

        /// <summary>
        /// Id quan he
        /// </summary>       
        public int RelationshipId { get; set; }
    }
}
