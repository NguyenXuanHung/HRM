using System.Collections.Generic;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for BankController
    /// </summary>
    public class BankController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BankModel GetById(int id)
        {
            var recordBank = hr_BankServices.GetById(id);
            return new BankModel(recordBank);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static List<BankModel> GetAll(int? recordId)
        {
            var bankModels = new List<BankModel>();
            var banks = hr_BankServices.GetAll(recordId);
            foreach(var bank in banks)
            {
                bankModels.Add(new BankModel(bank));
            }
            return bankModels;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bank"></param>
        public void Update(BankModel bank)
        {
            var record = hr_BankServices.GetById(bank.Id);
            if (record == null) return;
            record.RecordId = bank.RecordId;
            record.Note = bank.Note;
            record.AccountNumber = bank.AccountNumber;
            record.AccountName = bank.AccountName;
            record.BankId = bank.BankId;
            record.CreatedDate = bank.CreatedDate;
            record.EditedDate = bank.EditedDate;
            record.IsInUsed = bank.IsInUsed;
            hr_BankServices.Update(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bank"></param>
        public void Insert(hr_Bank bank)
        {
            hr_BankServices.Create(bank);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            hr_BankServices.Delete(id);
        }
    }
}