using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for TrainingHistoryModel
    /// </summary>
    public class TrainingHistoryModel: BaseModel<hr_TrainingHistory>
    {
        private readonly hr_TrainingHistory _trainingHistory;
        private readonly hr_Record _record;


        public TrainingHistoryModel()
        {
            _trainingHistory = new hr_TrainingHistory();
            _record = new hr_Record();

            // set model props
            Init(_trainingHistory);
        }

        public TrainingHistoryModel(hr_TrainingHistory trainingHistory)
        {
            _trainingHistory = trainingHistory ?? new hr_TrainingHistory();

            // set model props
            Init(trainingHistory);

            //get data relation
            var record = hr_RecordServices.GetById(_trainingHistory.RecordId);
            _record = record ?? new hr_Record();

        }

        /// <summary>
        /// id can bo
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// Ten khoa dao tao
        /// </summary>
        public string TrainingName { get; set; }

        /// <summary>
        /// ghi chu
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// ngay bat dau
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// ngay ket thuc
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// id quoc gia
        /// </summary>
        public int NationId { get; set; }

        /// <summary>
        /// ten quoc gia
        /// </summary>
        public string NationName => cat_NationServices.GetFieldValueById(_trainingHistory.NationId);

        /// <summary>
        /// ly do dao tao
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// noi dao tao
        /// </summary>
        public string TrainingPlace { get; set; }

        /// <summary>
        /// so quyet dinh
        /// </summary>
        public string DecisionNumber { get; set; }

        /// <summary>
        /// ngay quyet dinh
        /// </summary>
        public DateTime? DecisionDate { get; set; }

        /// <summary>
        /// id he thong dao tao
        /// </summary>
        public int TrainingSystemId { get; set; }

        /// <summary>
        /// ten he thong dao tao
        /// </summary>
        public string TrainingSystemName => cat_TrainingSystemServices.GetFieldValueById(_trainingHistory.TrainingSystemId);

        /// <summary>
        /// Đơn vị tài trợ
        /// </summary>
        public string SponsorDepartment { get; set; }

        /// <summary>
        /// Cơ quan quyết định
        /// </summary>
        public string SourceDepartment { get; set; }

        /// <summary>
        /// Người ký
        /// </summary>
        public string DecisionMaker { get; set; }

        /// <summary>
        /// Chức vụ người ký
        /// </summary>
        public string MakerPosition { get; set; }

        /// <summary>
        /// id lĩnh vực đào tạo
        /// </summary>
        public int FieldTrainingId { get; set; }

        /// <summary>
        /// id đơn vị tổ chức
        /// </summary>
        public int OrganizeDepartmentId { get; set; }

        /// <summary>
        /// Số hiệu văn bản
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Trạng thái đào tạo
        /// </summary>
        public TrainingStatus TrainingStatusId { get; set; }

        /// <summary>
        /// Loại đào tạo
        /// </summary>
        public string Type { get; set; }

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }

        #region custom props
        /// <summary>
        /// FullName
        /// </summary>
        public string FullName => _record.FullName;

        /// <summary>
        /// EmployeeCode
        /// </summary>
        public string EmployeeCode => _record.EmployeeCode;

        /// <summary>
        /// DepartmentName
        /// </summary>
        public string DepartmentName => cat_DepartmentServices.GetFieldValueById(_record.DepartmentId);

        #endregion
    }
}
