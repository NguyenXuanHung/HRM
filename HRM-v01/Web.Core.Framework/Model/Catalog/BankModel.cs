using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for BankModel
    /// </summary>
    public class BankModel
    {
        private readonly hr_Bank _bank;

        public BankModel(hr_Bank bank)
        {
            _bank = bank ?? new hr_Bank();

            Id = _bank.Id;
            RecordId = _bank.RecordId;
            IsInUsed = _bank.IsInUsed;
            AccountName = _bank.AccountName;
            AccountNumber = _bank.AccountNumber;
            Note = _bank.Note;
            BankId = _bank.BankId;
            
            CreatedDate = _bank.CreatedDate;
            EditedDate = _bank.EditedDate;
        }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// RecordId
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// BankId
        /// </summary>
        public int BankId { get; set; }

        /// <summary>
        /// BankName
        /// </summary>
        public string BankName
        {
            get
            {
                return cat_BankServices.GetFieldValueById(_bank.BankId);
            }
        }

        /// <summary>
        /// AccountName
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// AccountNumber
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// IsInUser
        /// </summary>
        public bool IsInUsed { get; set; }

        /// <summary>
        /// Note
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Số hiệu CB
        /// </summary>
        public string EmployeeCode
        {
            get { return hr_RecordServices.GetFieldValueById(_bank.RecordId, "EmployeeCode"); }
        }

        /// <summary>
        /// Họ và tên cán bộ
        /// </summary>
        public string FullName
        {
            get { return hr_RecordServices.GetFieldValueById(_bank.RecordId, "FullName"); }
        }

        /// <summary>
        /// CreatedDate
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// EditedDate
        /// </summary>
        public DateTime EditedDate { get; set; }
    }
}

