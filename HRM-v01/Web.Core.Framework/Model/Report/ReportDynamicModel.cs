using System;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Framework.Report;
using Web.Core.Object.Report;
using Web.Core.Service.Report;

namespace Web.Core.Framework
{
    public class ReportDynamicModel : BaseModel<ReportDynamic>
    {
        public ReportDynamicModel(ReportDynamic reportDynamic)
        {
            reportDynamic = reportDynamic ?? new ReportDynamic();

            // init report
            Init(reportDynamic);

            // get columns
            var reportColumns = ReportColumnServices.GetAll().Where(r => r.ReportId == reportDynamic.Id && !r.IsDeleted).ToList();

            // get report width
            Width = ReportHelper.ReportWidth(PaperKind, Orientation);

            // get header width
            HeaderWidth = ReportHelper.ReportConlumnTypeWidth(reportColumns, ReportColumnType.Header);

            // get header group width
            HeaderGroupWidth = ReportHelper.ReportConlumnTypeWidth(reportColumns, ReportColumnType.HeaderGroup);

            // get detail width
            DetailWidth = ReportHelper.ReportConlumnTypeWidth(reportColumns, ReportColumnType.Detail);

            // get footer group width
            FooterGroupWidth = ReportHelper.ReportConlumnTypeWidth(reportColumns, ReportColumnType.FooterGroup);

            // get footer width
            FooterWidth = ReportHelper.ReportConlumnTypeWidth(reportColumns, ReportColumnType.Footer) - Constant.IndexColumnWidth;
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
        /// Report template
        /// </summary>
        public ReportTemplate Template { get; set; }

        /// <summary>
        /// Template name
        /// </summary>
        public string TemplateName => Template.Description();

        /// <summary>
        /// Loại giấy A2 | A3 | A4
        /// </summary>
        public ReportPaperKind PaperKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PaperKindName => PaperKind.Description();

        /// <summary>
        /// Xoay Ngang | Dọc
        /// </summary>
        public ReportOrientation Orientation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrientationName => Orientation.Description();

        /// <summary>
        /// Report group header 1
        /// </summary>
        public ReportGroupHeader GroupHeader1 { get; set; }

        /// <summary>
        /// Group header name 1
        /// </summary>
        public string GroupHeaderName1 => GroupHeader1.Description();

        /// <summary>
        /// Report group header 2
        /// </summary>
        public ReportGroupHeader GroupHeader2 { get; set; }

        /// <summary>
        /// Group header name 2
        /// </summary>
        public string GroupHeaderName2 => GroupHeader2.Description();

        /// <summary>
        /// Report group header 3
        /// </summary>
        public ReportGroupHeader GroupHeader3 { get; set; }

        /// <summary>
        /// Group header name 3
        /// </summary>
        public string GroupHeaderName3 => GroupHeader3.Description();

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
        /// Report status name
        /// </summary>
        public string StatusName => Status.Description();

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

        #region Custom Properties

        /// <summary>
        /// Độ rộng báo cáo
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Độ rộng đầu báo cáo
        /// </summary>
        public int HeaderWidth { get; set; }

        /// <summary>
        /// Độ rộng đầu nhóm
        /// </summary>
        public int HeaderGroupWidth { get; set; }

        /// <summary>
        /// Độ rộng nội dung
        /// </summary>
        public int DetailWidth { get; set; }

        /// <summary>
        /// Độ rộng chân nhóm
        /// </summary>
        public int FooterGroupWidth { get; set; }

        /// <summary>
        /// Độ rộng chân báo cáo
        /// </summary>
        public int FooterWidth { get; set; }

        #endregion
    }
}
