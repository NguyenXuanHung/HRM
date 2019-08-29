using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for AttachFileModel
    /// </summary>
    public class AttachFileModel
    {
        private readonly hr_AttachFile _attachFile;

        public AttachFileModel(hr_AttachFile file)
        {
            _attachFile = file ?? new hr_AttachFile();

            Id = _attachFile.Id;
            RecordId = _attachFile.RecordId;
            AttachFileName = _attachFile.AttachFileName;
            URL = _attachFile.URL;
            SizeKB = _attachFile.SizeKB;
            Note = _attachFile.Note;
            IsApproved = _attachFile.IsApproved;
            CreatedDate = _attachFile.CreatedDate;
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
        /// ten tep tin dinh kem
        /// </summary>
        public string AttachFileName { get; set; }

        /// <summary>
        /// duong dan URL
        /// </summary>
        public string URL { get; set; }
        
        /// <summary>
        /// kich thuoc file
        /// </summary>
        public double SizeKB { get; set; }
        
        /// <summary>
        /// ghi chu
        /// </summary>
        public string Note { get; set; }
        
        /// <summary>
        /// Số hiệu CB
        /// </summary>
        public string EmployeeCode
        {
            get { return hr_RecordServices.GetFieldValueById(_attachFile.RecordId, "EmployeeCode"); }
        }
        
        /// <summary>
        /// Họ và tên cán bộ
        /// </summary>
        public string FullName
        {
            get { return hr_RecordServices.GetFieldValueById(_attachFile.RecordId, "FullName"); }
        }
        
        /// <summary>
        /// duyet
        /// </summary>
        public bool IsApproved { get; set; }
        
        /// <summary>
        /// ngay tao
        /// </summary>
        public DateTime? CreatedDate { get; set; }
    }
}
