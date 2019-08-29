using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Object.Catalog;
using Web.Core.Object.HumanRecord;
using Web.Core.Object;
using Web.Core.Object.Recruitment;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Recruitment;

namespace Web.Core.Framework
{
    public class CandidateModel : BaseModel<rec_Candidate>
    {
        private readonly rec_Candidate _candidate;
        private readonly hr_Record _record;
        private readonly rec_RequiredRecruitment _requiredRecruitment;

        public CandidateModel()
        {
            _candidate = new rec_Candidate();
            _record = new hr_Record();
            _requiredRecruitment = new rec_RequiredRecruitment();

            Init(_candidate);
        }

        public CandidateModel(rec_Candidate candidate)
        {
            _candidate = candidate ?? new rec_Candidate();
            _record = hr_RecordServices.GetById(_candidate.RecordId) ?? new hr_Record();
            _requiredRecruitment = rec_RequiredRecruitmentServices.GetById(_candidate.RequiredRecruitmentId) ??
                                   new rec_RequiredRecruitment();

            Init(_candidate);
        }

        /// <summary>
        /// id hồ sơ
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// Mã ứng viên
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// id yêu cầu tuyển dụng
        /// </summary>
        public int RequiredRecruitmentId { get; set; }

        /// <summary>
        /// Mức lương mong muốn
        /// </summary>
        public decimal DesiredSalary { get; set; }

        /// <summary>
        /// Ngày nộp hồ sơ
        /// </summary>
        public DateTime? ApplyDate { get; set; }

        /// <summary>
        /// Trạng thái hồ sơ ứng viên
        /// </summary>
        public CandidateType Status { get; set; }

        /// <summary>
        /// Xóa
        /// </summary>
        public bool IsDeleted { get; set; }

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedDate { get; set; }

        #region Custom Props
        /// <summary>
        /// 
        /// </summary>
        public string RequiredRecruitmentName => _requiredRecruitment.Name;

        /// <summary>
        /// Tên đầy đủ
        /// </summary>
        public string FullName => _record.FullName;

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode => _record.EmployeeCode;

        /// <summary>
        /// Giới tính
        /// </summary>
        public string SexName => _record.Sex ? "Nam" : "Nữ";

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName => cat_DepartmentServices.GetFieldValueById(_record.DepartmentId);

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public string BirthDate => _record.BirthDate != null ? _record.BirthDate.Value.ToVnDate() : "";

        /// <summary>
        /// Nơi sinh
        /// </summary>
        public string BirthPlace => _record.BirthPlace;

        /// <summary>
        /// 
        /// </summary>
        public string StatusName => _candidate.Status.Description();

        /// <summary>
        /// 
        /// </summary>
        public string ApplyVnDate => _candidate.ApplyDate != null ? _candidate.ApplyDate.Value.ToVnDate() : "";

        #endregion
    }
}
