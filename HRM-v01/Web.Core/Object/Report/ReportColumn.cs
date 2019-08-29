using System;

namespace Web.Core.Object.Report
{
    public class ReportColumn : BaseEntity
    {
        public ReportColumn()
        {
            Id = 0;
            ReportId = 0;
            Name = "";
            FieldName = "";
            ParentId = 0;
            DataType = ReportColumnDataType.String;
            TextAlign = ReportTextAlign.MiddleCenter;
            FontSize = 10;
            Format = "";
            Width = 100;
            Height = 25;
            Order = 0;
            IsGroup = false;
            Type = ReportColumnType.Header;
            SummaryRunning = ReportSummaryRunning.None;
            SummaryFunction = ReportSummaryFunction.None;
            SummaryValue = "";
            Status = ReportColumnStatus.Active;
            IsDeleted = false;
            CreatedBy = "system";
            CreatedDate = DateTime.Now;
            EditedBy = "system";
            EditedDate = DateTime.Now;
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
        /// Column data type String | Number
        /// </summary>
        public ReportColumnDataType DataType { get; set; }

        /// <summary>
        /// Cell / Label text alignment
        /// </summary>
        public ReportTextAlign TextAlign { get; set; }

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
        /// Config type (Header & Detail | Footer | GroupHeader | GroupFooter)
        /// </summary>
        public ReportColumnType Type { get; set; }

        /// <summary>
        /// Config summary running scope, only effect for Footer, GroupHeader, GroupFooter
        /// </summary>
        public ReportSummaryRunning SummaryRunning { get; set; }

        /// <summary>
        /// Config summary function, only effect for Footer, GroupHeader, GroupFooter
        /// </summary>
        public ReportSummaryFunction SummaryFunction { get; set; }

        /// <summary>
        /// Config summary value for comparision in custom count function
        /// </summary>
        public string SummaryValue { get; set; }

        /// <summary>
        /// Column status (Active | Locked)
        /// </summary>
        public ReportColumnStatus Status { get; set; }

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
    }
}
