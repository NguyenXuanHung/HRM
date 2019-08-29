using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for BusinessHistoryController
    /// </summary>
    public class BusinessHistoryController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BusinessHistoryModel GetById(int id)
        {
            var recordBusiness = hr_BusinessHistoryServices.GetById(id);
            return new BusinessHistoryModel(recordBusiness);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="businessType"></param>
        /// <returns></returns>
        public static List<BusinessHistoryModel> GetAll(int? recordId, string businessType)
        {
            var condition = Constant.ConditionDefault;
            if (recordId != null)
                condition += @" AND [RecordId]='{0}'".FormatWith(recordId);
            if (!string.IsNullOrEmpty(businessType))
                condition += @" AND [BusinessType] LIKE N'%{0}%'".FormatWith(businessType);
            
            var business = hr_BusinessHistoryServices.GetAll(condition);
            return business.Select(bs => new BusinessHistoryModel(bs)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public void Insert(hr_BusinessHistory info)
        {
            hr_BusinessHistoryServices.Create(info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Update(hr_BusinessHistory obj)
        {
            var record = hr_BusinessHistoryServices.GetById(obj.Id);
            if (record == null) return;
            record.RecordId = obj.RecordId;
            record.Id = obj.Id;
            record.DecisionDate = obj.DecisionDate;
            record.DecisionMaker = obj.DecisionMaker;
            record.DecisionNumber = obj.DecisionNumber;
            record.DecisionPosition = obj.DecisionPosition;
            record.ShortDecision = obj.ShortDecision;
            record.EffectiveDate = obj.EffectiveDate;
            record.ExpireDate = obj.ExpireDate;
            record.Description = obj.Description;
            record.CurrentPosition = obj.CurrentPosition;
            record.CurrentDepartment = obj.CurrentDepartment;
            record.OldPosition = obj.OldPosition;
            record.OldDepartment = obj.OldDepartment;
            record.NewPosition = obj.NewPosition;
            record.SourceDepartment = obj.SourceDepartment;
            record.DestinationDepartment = obj.DestinationDepartment;
            record.FileScan = obj.FileScan;
            record.BusinessType = obj.BusinessType;
            record.EmulationTitle = obj.EmulationTitle;
            record.Money = obj.Money;
            hr_BusinessHistoryServices.Update(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            hr_BusinessHistoryServices.Delete(id);
        }
    }
}
