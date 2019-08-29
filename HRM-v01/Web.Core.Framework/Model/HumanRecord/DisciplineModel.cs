using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for DisciplineModel
    /// </summary>
    public class DisciplineModel:BaseModel<hr_Discipline>
    {
        private readonly hr_Discipline _discipline;
        private readonly hr_Record _record;

        public DisciplineModel()
        {
            _discipline = new hr_Discipline();
            _record = new hr_Record();
            //init props
            Init(_discipline);
        }

        public DisciplineModel(hr_Discipline discipline)
        {
            _discipline = discipline ?? new hr_Discipline();
           
            //init props
            Init(_discipline);
            //get data relation
            var record = hr_RecordServices.GetById(_discipline.RecordId);
            _record = record ?? new hr_Record();
        }
        
        /// <summary>
        /// id ho so
        /// </summary>        
        public int RecordId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DecisionNumber { get; set; }

        /// <summary>
        /// Ngay quyet dinh
        /// </summary>
        public DateTime? DecisionDate { get; set; }

        /// <summary>
        /// Ly do ky luat
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Ten hinh thuc ky luat
        /// </summary>
        public string FormName => cat_DisciplineServices.GetFieldValueById(_discipline.FormDisciplineId);

        /// <summary>
        /// So tien
        /// </summary>        
        public Decimal? MoneyAmount { get; set; }

        /// <summary>
        /// Ghi chu
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Nguoi quyet dinh
        /// </summary>
        public string DecisionMaker { get; set; }

        /// <summary>
        /// Tên file đính kèm
        /// </summary>
        public string AttachFileName { get; set; }

        /// <summary>
        /// Duyệt
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Số điểm
        /// </summary>
        public double? Point { get; set; }

        /// <summary>
        /// Ten cap khen thuong ky luat
        /// </summary>
        public string LevelName => cat_LevelRewardDisciplineServices.GetFieldValueById(_discipline.LevelDisciplineId);

        /// <summary>
        /// Id cap khen thuong ky luat
        /// </summary>
        public int LevelDisciplineId { get; set; }

        /// <summary>
        /// Id hinh thuc khen thuong
        /// </summary>
        public int FormDisciplineId { get; set; }

        /// <summary>
        /// Thời hạn kỷ luật
        /// </summary>
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// Chức vụ người ký
        /// </summary>
        public string MakerPosition { get; set; }

        /// <summary>
        /// Cơ quan kỷ luật
        /// </summary>
        public string SourceDepartment { get; set; }

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }

        #region custom props
        /// <summary>
        /// EmployeeCode
        /// </summary>
        public string EmployeeCode => _record.EmployeeCode;

        /// <summary>
        /// FullName
        /// </summary>
        public string FullName => _record.FullName;

        /// <summary>
        /// DepartmentName
        /// </summary>
        public string DepartmentName => cat_DepartmentServices.GetFieldValueById(_record.DepartmentId);

        /// <summary>
        /// ParentDepartmentName
        /// </summary>
        public string ParentDepartmentName => cat_DepartmentServices.GetFieldValueById(_record.ManagementDepartmentId);

        #endregion
    }
}
