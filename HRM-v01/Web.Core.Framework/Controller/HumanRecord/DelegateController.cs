using System.Collections.Generic;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for DelegateController
    /// </summary>
    public class DelegateController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DelegateModel GetById(int id)
        {
            var recordDelegate = hr_DelegateServices.GetById(id);
            return new DelegateModel(recordDelegate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static List<DelegateModel> GetAll(int? recordId)
        {
            var delegateModels = new List<DelegateModel>();
            var delegates = hr_DelegateServices.GetAll(recordId);
            foreach (var dele in delegates)
            {
                delegateModels.Add(new DelegateModel(dele));
            }
            return delegateModels;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dele"></param>
        public void Update(hr_Delegate dele)
        {
            var record = hr_DelegateServices.GetById(dele.Id);
            if (record == null) return;
            record.RecordId = dele.RecordId;
            record.Term = dele.Term;
            record.Note = dele.Note;
            record.FromDate = dele.FromDate;
            record.ToDate = dele.ToDate;
            record.CreatedDate = dele.CreatedDate;
            record.EditedDate = dele.EditedDate;
            record.IsApproved = dele.IsApproved;
            record.Type = dele.Type;

            hr_DelegateServices.Update(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dele"></param>
        public void Insert(hr_Delegate dele)
        {
            hr_DelegateServices.Create(dele);
        }
    }
}
