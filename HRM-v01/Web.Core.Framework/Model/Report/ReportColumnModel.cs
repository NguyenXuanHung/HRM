using System;
using System.Linq;
using Web.Core.Object.Report;
using Web.Core.Service.Report;

namespace Web.Core.Framework
{
    public class ReportColumnModel : BaseModel<ReportColumn>
    {
        private const string Condition = @"[ReportId]={0} AND [ParentId]={1} AND [IsDeleted]={2}";

        /// <summary>
        /// Constructor without initial value
        /// </summary>
        public ReportColumnModel()
        {
            // init model props
            Init(new ReportColumn());
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reportColumn"></param>
        public ReportColumnModel(ReportColumn reportColumn)
        {
            // init report column object
            reportColumn = reportColumn ?? new ReportColumn();

            // get children report column
            var childrenReportColumn = ReportColumnServices.GetAll(Condition.FormatWith(reportColumn.ReportId, reportColumn.Id, 0));

            // set width remain
            if (childrenReportColumn.Count > 0)
            {
                WidthRemain = reportColumn.Width - childrenReportColumn.Sum(r => r.Width);
            }
            else
            {
                WidthRemain = reportColumn.Width;
            }

            // init model props
            Init(reportColumn);

            // set name to config name
            ConfigName = Name;
        }

        /// <summary>
        /// Report ID
        /// </summary>
        public int ReportId { get; set; }

        /// <summary>
        /// Column name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Data field name
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Parent ID, 0 -> root 
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Parent name
        /// </summary>
        public string ParentName => ParentId > 0 ? ReportColumnServices.GetById(ParentId).Name : string.Empty;

        /// <summary>
        /// Column data type String | Number
        /// </summary>
        public ReportColumnDataType DataType { get; set; }

        /// <summary>
        /// Column data type name
        /// </summary>
        public string DataTypeName => DataType.Description();

        /// <summary>
        /// Cell / Label text alignment
        /// </summary>
        public ReportTextAlign TextAlign { get; set; }

        /// <summary>
        /// Cell / Label text alignment name
        /// </summary>
        public string TextAlignName => TextAlign.Description();

        /// <summary>
        /// Cell / Label font size
        /// </summary>
        public int FontSize { get; set; }

        /// <summary>
        /// Cell / Label output string format
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Column order in report
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Group of label
        /// </summary>
        public bool IsGroup { get; set; }

        /// <summary>
        /// Group status
        /// </summary>
        public string GroupName => IsGroup ? "Nhãn" : "Cột";

        /// <summary>
        /// Column type (Header & Detail | Footer | GroupHeader | GroupFooter)
        /// </summary>
        public ReportColumnType Type { get; set; }

        /// <summary>
        /// Column type name (Header & Detail | Footer | GroupHeader | GroupFooter)
        /// </summary>
        public string TypeName => Type.Description();

        /// <summary>
        /// Column summary running scope, only effect for Footer, GroupHeader, GroupFooter
        /// </summary>
        public ReportSummaryRunning SummaryRunning { get; set; }

        /// <summary>
        /// Column summary running scope name, only effect for Footer, GroupHeader, GroupFooter
        /// </summary>
        public string SummaryRunningName => SummaryRunning.Description();

        /// <summary>
        /// Column summary function, only effect for Footer, GroupHeader, GroupFooter
        /// </summary>
        public ReportSummaryFunction SummaryFunction { get; set; }

        /// <summary>
        /// Column summary function name, only effect for Footer, GroupHeader, GroupFooter
        /// </summary>
        public string SummaryFunctionName => SummaryFunction.Description();

        /// <summary>
        /// Column summary value for comparision in custom count function
        /// </summary>
        public string SummaryValue { get; set; }

        /// <summary>
        /// Column status (Active | Locked)
        /// </summary>
        public ReportColumnStatus Status { get; set; }

        /// <summary>
        /// Column status name (Active | Locked)
        /// </summary>
        public string StatusName => Status.Description();

        /// <summary>
        /// Column was deleted
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
        /// Tên hiển thị trên bảng config
        /// </summary>
        public string ConfigName { get; set; }

        /// <summary>
        /// Chiều rộng cột con có thể set
        /// </summary>
        public int WidthRemain { get; }

        /// <summary>
        /// Tên loại cột đã sắp xếp
        /// </summary>
        public string OrderedTypeName => EnumHelper.GetOrderedType(Type);

        /// <summary>
        /// Cấp
        /// </summary>
        public int Level { get; set; }

        #endregion
    }
}
