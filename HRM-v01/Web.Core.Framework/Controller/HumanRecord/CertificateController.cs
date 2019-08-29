using System.Collections.Generic;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for CertificateController
    /// </summary>
    public class CertificateController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CertificateModel GetById(int id)
        {
            var recordCertificate = hr_CertificateServices.GetById(id);
            return new CertificateModel(recordCertificate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static List<CertificateModel> GetAll(int? recordId)
        {
            var certificateModels = new List<CertificateModel>();
            var certificates = hr_CertificateServices.GetAll(recordId);
            foreach (var certificate in certificates)
            {
                certificateModels.Add(new CertificateModel(certificate));
            }
            return certificateModels;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Update(hr_Certificate obj)
        {
            var record = hr_CertificateServices.GetById(obj.Id);
            if (record == null) return;
            record.RecordId = obj.RecordId;
            record.Note = obj.Note;
            record.CertificationId = obj.CertificationId;
            record.GraduationTypeId = obj.GraduationTypeId;
            record.ExpirationDate = obj.ExpirationDate;
            record.IssueDate = obj.IssueDate;
            record.IsApproved = obj.IsApproved;
            record.EditedDate = obj.EditedDate;
            record.EducationPlace = obj.EducationPlace;

            hr_CertificateServices.Update(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="certify"></param>
        public void Insert(hr_Certificate certify)
        {
            hr_CertificateServices.Create(certify);
        }
    }
}