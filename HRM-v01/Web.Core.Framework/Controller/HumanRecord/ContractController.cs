using System;
using System.Collections.Generic;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for ContractController
    /// </summary>
    public class ContractController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ContractModel GetById(int id)
        {
            var recordContract = hr_ContractServices.GetById(id);
            return new ContractModel(recordContract);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ContractModel GetByConditionRecord(int id)
        {
            var condition = @" [RecordId] = {0}".FormatWith(id);
            var record = hr_ContractServices.GetByCondition(condition);
            return new ContractModel(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static List<ContractModel> GetAll(int? recordId)
        {
            var contractModels = new List<ContractModel>();
            var contracts = hr_ContractServices.GetAll(recordId);
            foreach (var contract in contracts)
            {
                contractModels.Add(new ContractModel(contract));
            }
            return contractModels;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<ContractModel> GetAllByRecordCondition(int id)
        {
            var condition = @" [RecordId] = {0} ".FormatWith(id);
            condition += @" AND ([ContractEndDate] >= '{0}'  OR [ContractEndDate] IS NULL) ".FormatWith(DateTime.Now.ToString("yyyy-MM-dd"));
            var contractModels = new List<ContractModel>();
            var contracts = hr_ContractServices.GetAll(condition);
            foreach (var contract in contracts)
            {
                contractModels.Add(new ContractModel(contract));
            }
            return contractModels;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public void Insert(hr_Contract info)
        {
            hr_ContractServices.Create(info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public void Update(hr_Contract info)
        {
            var record = hr_ContractServices.GetById(info.Id);
            if (record == null) return;
            record.ContractNumber = info.ContractNumber;
            record.ContractStatusId = info.ContractStatusId;
            record.ContractTypeId = info.ContractTypeId;
            record.ContractDate = info.ContractDate;
            record.ContractEndDate = info.ContractEndDate;
            record.Note = info.Note;
            record.PersonPositionId = info.PersonPositionId;
            record.AttachFileName = info.AttachFileName;
            record.EffectiveDate = info.EffectiveDate;
            record.RecruitmentTypeId = info.RecruitmentTypeId;
            record.PersonRepresent = info.PersonRepresent;
            record.JobId = info.JobId;
            record.EditedDate = info.EditedDate;
            record.ContractCondition = info.ContractCondition;
            record.CreatedBy = info.CreatedBy;
            record.CreatedDate = info.CreatedDate;
            record.ContractEndDate = info.ContractEndDate;
                
            hr_ContractServices.Update(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        public void Delete(int recordId)
        {
            hr_ContractServices.Delete(recordId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="effectiveDate"></param>
        /// <returns></returns>
        public hr_Contract CheckContractBeforeInsert(int recordId, DateTime? effectiveDate)
        {
            var condition = @" [RecordId] = {0}".FormatWith(recordId);
            if (effectiveDate != null)
            {
                condition += @" AND ([ContractEndDate] >= '{0}'  OR [ContractEndDate] IS NULL) ".FormatWith(effectiveDate.Value.ToString("yyyy-MM-dd"));
            }
            return hr_ContractServices.GetByCondition(condition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractNumber"></param>
        /// <returns></returns>
        public static ContractModel GetContractNumberByCondition(string contractNumber)
        {
            var condition = @" [ContractNumber] LIKE  N'%{0}%' ".FormatWith(contractNumber);
            var order = " [ContractNumber] DESC ";
            var record = hr_ContractServices.GetByCondition(condition, order);
            return new ContractModel(record);
        }

    }
}