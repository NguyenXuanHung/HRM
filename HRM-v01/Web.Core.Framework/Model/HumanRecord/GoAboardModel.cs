using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for GoAboardModel
    /// </summary>
    public class GoAboardModel: BaseModel<hr_GoAboard>
    {
        private readonly hr_GoAboard _goAboard;
        private readonly hr_Record _record;

        public GoAboardModel()
        {
            _goAboard = new hr_GoAboard();
            _record = new hr_Record();

            Init(_goAboard);
        }

        public GoAboardModel(hr_GoAboard goAboard)
        {
            _goAboard = goAboard ?? new hr_GoAboard();

            //set props
            Init(_goAboard);

            //get data relation
            var record = hr_RecordServices.GetById(_goAboard.RecordId);
            _record = record ?? new hr_Record();
        }

        /// <summary>
        /// id ho so
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// id quoc gia
        /// </summary>
        public int NationId { get; set; }

        /// <summary>
        /// ten quoc gia
        /// </summary>
        public string NationName => cat_NationServices.GetFieldValueById(_goAboard.NationId);

        /// <summary>
        /// ngay bat dau
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// ngay ket thuc
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// ly do
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// ghi chu
        /// </summary>
        public string Note { get; set; }

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
        /// Số quyết định
        /// </summary>
        public string DecisionNumber { get; set; }
        /// <summary>
        /// Ngày quyết định
        /// </summary>
        public DateTime? DecisionDate { get; set; }
        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }

        #region custome props
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
