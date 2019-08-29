using System;

namespace Web.Core.Object.Report
{
    public class ReportDynamic : BaseEntity
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ReportDynamic()
        {
            Id = 0;
            Name = "";
            Description = "";
            ReportSource = "[]";
            Argument = "";
            Template = ReportTemplate.EnterprisePayroll;
            PaperKind = ReportPaperKind.A4;
            Orientation = ReportOrientation.Landscape;
            GroupHeader1 = ReportGroupHeader.NoGroup;
            GroupHeader2 = ReportGroupHeader.NoGroup;
            GroupHeader3 = ReportGroupHeader.NoGroup;
            ParentDepartment = "";
            Department = "";
            Title = "";
            Duration = "";
            FilterCondition = "";
            CreatedByTitle = @"NGƯỜI LẬP";
            CreatedByNote = @"(Ký, ghi rõ họ và tên)";
            CreatedByName = "";
            ReviewedByTitle = @"NGƯỜI DUYỆT";
            ReviewedByNote = @"(Chức vụ, ký, ghi rõ họ và tên)";
            ReviewedByName = "";
            SignedByTitle = @"THỦ TRƯỞNG CƠ QUAN, ĐƠN VỊ";
            SignedByNote = @"(Chức vụ, ký, ghi rõ họ và tên)";
            SignedByName = "";
            ReportDate = DateTime.Now;
            StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            EndDate = StartDate.AddMonths(1).AddDays(-1);
            Status = ReportStatus.Active;
            IsDeleted = false;
            CreatedBy = "system";
            CreatedDate = DateTime.Now;
            EditedBy = "system";
            EditedDate = DateTime.Now;
        }

        /// <summary>
        /// Report name, unique value
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Report source (json data)
        /// </summary>
        public string ReportSource { get; set; }

        /// <summary>
        /// Report argument
        /// </summary>
        public string Argument { get; set; }

        /// <summary>
        /// report template
        /// </summary>
        public ReportTemplate Template { get; set; }

        /// <summary>
        /// Loại giấy A2 | A3 | A4
        /// </summary>
        public ReportPaperKind PaperKind { get; set; }

        /// <summary>
        /// Xoay Ngang | Dọc
        /// </summary>
        public ReportOrientation Orientation { get; set; }

        /// <summary>
        /// Report group header 1
        /// </summary>
        public ReportGroupHeader GroupHeader1 { get; set; }

        /// <summary>
        /// Report group header 2
        /// </summary>
        public ReportGroupHeader GroupHeader2 { get; set; }

        /// <summary>
        /// Report group header 3
        /// </summary>
        public ReportGroupHeader GroupHeader3 { get; set; }

        /// <summary>
        /// Management organization name
        /// </summary>
        public string ParentDepartment { get; set; }

        /// <summary>
        /// Organization name
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Report title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Report duration, From date ... to date ...
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// Report filter condition, each filter split by comma
        /// </summary>
        public string FilterCondition { get; set; }

        /// <summary>
        /// Footer label, user / department creates report
        /// </summary>
        public string CreatedByTitle { get; set; }

        /// <summary>
        /// Footer label, note sign & name
        /// </summary>
        public string CreatedByNote { get; set; }

        /// <summary>
        /// Footer label, name of user creates report
        /// </summary>
        public string CreatedByName { get; set; }

        /// <summary>
        /// Footer label, user / department reviews report
        /// </summary>
        public string ReviewedByTitle { get; set; }

        /// <summary>
        /// Footer label, note sign & name
        /// </summary>
        public string ReviewedByNote { get; set; }

        /// <summary>
        /// Footer label, name of user reviews report
        /// </summary>
        public string ReviewedByName { get; set; }

        /// <summary>
        /// Footer label, user / department signs report
        /// </summary>
        public string SignedByTitle { get; set; }

        /// <summary>
        /// Footer label, note sign & name
        /// </summary>
        public string SignedByNote { get; set; }

        /// <summary>
        /// Footer label, name of user signs report
        /// </summary>
        public string SignedByName { get; set; }

        /// <summary>
        /// Report date, default today
        /// </summary>
        public DateTime ReportDate { get; set; }

        /// <summary>
        /// From date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// To date
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Report status
        /// </summary>
        public ReportStatus Status { get; set; }

        /// <summary>
        /// Deleted status
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// System, created user
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// System, created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// System, edited user
        /// </summary>
        public string EditedBy { get; set; }

        /// <summary>
        /// System, edited date
        /// </summary>
        public DateTime EditedDate { get; set; }
    }
}
