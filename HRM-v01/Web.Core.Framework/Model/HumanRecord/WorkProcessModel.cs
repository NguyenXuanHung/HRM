using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for WorkProcessModel
    /// </summary>
    public class WorkProcessModel
    {
        private readonly hr_WorkProcess _workProcess;

        public WorkProcessModel(hr_WorkProcess workProcess)
        {
            _workProcess = workProcess ?? new hr_WorkProcess();
            RecordId = _workProcess.RecordId;
            DecisionNumber = _workProcess.DecisionNumber;
            DecisionMaker = _workProcess.DecisionMaker;
            NewPositionId = _workProcess.NewPositionId;
            OldPositionId = _workProcess.OldPositionId;
            NewJobId = _workProcess.NewJobId;
            OldJobId = _workProcess.OldJobId;
            NewDepartmentId = _workProcess.NewDepartmentId;
            OldDepartmentId = _workProcess.OldDepartmentId;
            AttachFileName = _workProcess.AttachFileName;
            Note = _workProcess.Note;
            IsApproved = _workProcess.IsApproved;
            DecisionDate = _workProcess.DecisionDate;
            EffectiveDate = _workProcess.EffectiveDate;
            EffectiveEndDate = _workProcess.EffectiveEndDate;
            Id = _workProcess.Id;
            ExpireDate = _workProcess.ExpireDate;
            MakerPosition = _workProcess.MakerPosition;
            SourceDepartment = _workProcess.SourceDepartment;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// id hồ sơ
        /// </summary>
        public int RecordId { get; set; } 
        
        /// <summary>
        /// So quyet dinh
        /// </summary>
        public string DecisionNumber { get; set; }

        /// <summary>
        /// Ngay quyet dinh
        /// </summary>
        public DateTime? DecisionDate { get; set; }

        /// <summary>
        /// Nguoi quyet dinh
        /// </summary>
        public string DecisionMaker { get; set; }

        /// <summary>
        /// Ngay co hieu luc
        /// </summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// Ngay het hieu luc
        /// </summary>
        public DateTime? EffectiveEndDate { get; set; }   

        /// <summary>
        /// Id bo phan moi
        /// </summary>
        public int NewDepartmentId { get; set; }

        /// <summary>
        /// ten bo phan moi
        /// </summary>
        public string NewDepartmentName
        {
            get { return cat_DepartmentServices.GetFieldValueById(_workProcess.NewDepartmentId); }
        }

        /// <summary>
        /// Id bo phan cu
        /// </summary>
        public int OldDepartmentId { get; set; }

        /// <summary>
        /// ten bo phan cu
        /// </summary>
        public string OldDepartmentName
        {
            get { return cat_DepartmentServices.GetFieldValueById(_workProcess.OldDepartmentId); }
        }

        /// <summary>
        /// id cong viec cu
        /// </summary>
        public int OldJobId { get; set; }

        /// <summary>
        /// Ten cong viec cu
        /// </summary>
        public string OldJobName
        {
            get { return cat_JobTitleServices.GetFieldValueById(_workProcess.OldJobId); }
        }

        /// <summary>
        /// id cong viec moi
        /// </summary>
        public int NewJobId { get; set; }

        /// <summary>
        /// Ten cong viec moi
        /// </summary>
        public string NewJobName
        {
            get { return cat_JobTitleServices.GetFieldValueById(_workProcess.NewJobId); }
        }

        /// <summary>
        /// Tep tin dinh kem
        /// </summary>
        public string AttachFileName { get; set; }

        /// <summary>
        /// Duyet
        /// </summary>        
        public bool IsApproved { get; set; }

        /// <summary>
        /// Ghi chu
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// id chuc vu moi
        /// </summary>
        public int NewPositionId { get; set; }

        /// <summary>
        /// Ten chuc vu moi
        /// </summary>
        public string NewPositionName
        {
            get { return cat_PositionServices.GetFieldValueById(_workProcess.NewPositionId); }
        }
        
        /// <summary>
        /// id chuc vu cu
        /// </summary>
        public int OldPositionId { get; set; }
        
        /// <summary>
        /// Ten chuc vu cu
        /// </summary>
        public string OldPositionName
        {
            get { return cat_PositionServices.GetFieldValueById(_workProcess.OldPositionId); }
        }
        
        /// <summary>
        /// Thời hạn
        /// </summary>
        public DateTime? ExpireDate { get; set; }
        
        /// <summary>
        /// Chức vụ người kí
        /// </summary>
        public string MakerPosition { get; set; }
        
        /// <summary>
        /// Cơ quan
        /// </summary>
        public string SourceDepartment { get; set; }
    }
}
