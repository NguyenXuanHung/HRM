using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for ContractModel
    /// </summary>
    public class ContractModel
    {
        private readonly hr_Contract _contract;

        public ContractModel(hr_Contract contract)
        {
            _contract = contract ?? new hr_Contract();
            RecordId = _contract.RecordId;
            ContractNumber = _contract.ContractNumber;
            ContractTypeId = _contract.ContractTypeId;
            ContractStatusId = _contract.ContractStatusId;
            SalaryId = _contract.SalaryId;
            JobId = _contract.JobId;
            ContractCondition = _contract.ContractCondition;
            AttachFileName = _contract.AttachFileName;
            Note = _contract.Note;
            PersonRepresent = _contract.PersonRepresent;
            PersonPositionId = _contract.PersonPositionId;
            ContractDate = _contract.ContractDate;
            ContractEndDate = _contract.ContractEndDate;
            EffectiveDate = _contract.EffectiveDate;
            Id = _contract.Id;
        }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// id hồ sơ
        /// </summary>        
        public int RecordId { get; set; }

        /// <summary>
        /// so hop dong
        /// </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        /// Id loai hop dong
        /// </summary>
        public int ContractTypeId { get; set; }

        /// <summary>
        /// ten loai hop dong
        /// </summary>
        public string ContractTypeName => cat_ContractTypeServices.GetFieldValueById(_contract.ContractTypeId);

        /// <summary>
        /// id tinh trang hop dong
        /// </summary>
        public int ContractStatusId { get; set; }

        /// <summary>
        /// ten tinh trang hop dong
        /// </summary>
        public string ContractStatusName => cat_ContractStatusServices.GetFieldValueById(_contract.ContractStatusId);

        /// <summary>
        /// id luong
        /// </summary>
        public int SalaryId { get; set; }

        /// <summary>
        /// id cong viec
        /// </summary>
        public int JobId { get; set; }

        /// <summary>
        /// Ten cong viec
        /// </summary>
        public string JobName => cat_JobTitleServices.GetFieldValueById(_contract.JobId);

        /// <summary>
        /// trang thai hop dong
        /// </summary>
        public string ContractCondition { get; set; }

        /// <summary>
        /// ngay hop dong
        /// </summary>
        public DateTime? ContractDate { get; set; }

        /// <summary>
        /// ngay ket thuc hop dong
        /// </summary>
        public DateTime? ContractEndDate { get; set; }

        /// <summary>
        /// Ngay co hieu luc
        /// </summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// tep tin dinh kem
        /// </summary>
        public string AttachFileName { get; set; }

        /// <summary>
        /// ghi chu
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Nguoi dai dien ky hop dong
        /// </summary>
        public string PersonRepresent { get; set; }

        /// <summary>
        /// id chuc vu nguoi ky hop dong
        /// </summary>
        public int PersonPositionId { get; set; }

        /// <summary>
        /// ten chuc vu nguoi ky hop dong
        /// </summary>
        public string PersonPositionName => cat_PositionServices.GetFieldValueById(_contract.PersonPositionId);
    }
}
