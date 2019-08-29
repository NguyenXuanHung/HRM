using System.Collections.Generic;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for AttachFileController
    /// </summary>
    public class AttachFileController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static AttachFileModel GetById(int id)
        {
            var recordAttachFile = hr_AttachFileServices.GetById(id);
            return new AttachFileModel(recordAttachFile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static List<AttachFileModel> GetAll(int? recordId)
        {
            var fileModels = new List<AttachFileModel>();
            var files = hr_AttachFileServices.GetAll(recordId);
            foreach (var file in files)
            {
                fileModels.Add(new AttachFileModel(file));
            }
            return fileModels;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attach"></param>
        public void Insert(hr_AttachFile attach)
        {
            hr_AttachFileServices.Create(attach);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Update(hr_AttachFile obj)
        {
            var record = hr_AttachFileServices.GetById(obj.Id);
            if(record != null)
            {
                record.AttachFileName = obj.AttachFileName;
                record.Note = obj.Note;
                record.CreatedBy = obj.CreatedBy;
                record.CreatedDate = obj.CreatedDate;
                record.EditedDate = obj.EditedDate;
                record.SizeKB = obj.SizeKB;
                record.URL = obj.URL;
                record.IsApproved = obj.IsApproved;
                record.RecordId = obj.RecordId;
                hr_AttachFileServices.Update(obj);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            hr_AttachFileServices.Delete(id);
        }
    }
}
