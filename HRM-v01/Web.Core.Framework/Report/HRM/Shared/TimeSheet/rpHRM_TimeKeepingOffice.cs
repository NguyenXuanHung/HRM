using System;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Controller.TimeSheet;
using Web.Core.Framework.Interface;
using Web.Core.Framework.Utils;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Báo cáo chấm công hành chính
    /// </summary>
    public class rpHRM_TimeKeepingOffice : XtraReport, IBaseReport
    {
        private DetailBand Detail;
        private TopMarginBand TopMargin;
        private BottomMarginBand BottomMargin;
        private ReportHeaderBand ReportHeader;
        private PageHeaderBand PageHeader;
        private FormattingRule formattingRule1;
        private ReportFooterBand ReportFooter;
        private XRLabel lblCreatedByName;
        private XRLabel lblCreatedByTitle;
        private XRLabel lblSignedByTitle;
        private XRTable tblReportHeader;
        private XRTableRow xrTableRow2;
        private XRTableCell xrTableCell401;
        private XRTableCell xrTableCell402;
        private XRTableCell xrTableCell403;
        private XRTableRow xrTableRow4;
        private XRTableCell xrTableCell601;
        private XRTableCell xrTableCell602;
        private XRTable tblPageHeader;
        private XRTableRow xrTableRow11;
        private XRTableCell xrTableCell241;
        private XRTableCell xrTableCell242;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell603;
        private XRTableRow xrTableRow19;
        private XRTableCell xrCellTenBieuMau;
        private XRTableCell xrCellReportName;
        private XRTableCell xrCellOrganization;
        private XRTable tblDetail;
        private XRTableRow xrDetailRow1;
        private XRTableCell xrtIndex;
        private XRTableCell xrtFullName;
        private XRTableCell xrtEmployeeCode;
        private XRTableCell xrtDay1;
        private XRTableCell xrtDay2;
        private XRTableCell xrtDay3;
        private XRTableCell xrtDay4;
        private XRTableCell xrtDay5;
        private XRTableCell xrtDay6;
        private XRTableCell xrtDay7;
        private XRTableCell xrtDay8;
        private XRTableCell xrtDay9;
        private XRTableCell xrtDay10;
        private XRTableCell xrtDay11;
        private XRTableCell xrtDay12;
        private XRTableCell xrtDay13;
        private XRTableCell xrtDay14;
        private XRTableCell xrtDay15;
        private XRTableCell xrtDay16;
        private XRTableCell xrtDay17;
        private XRTableCell xrtDay18;
        private XRTableCell xrtDay19;
        private XRTableCell xrtDay20;
        private XRTableCell xrtDay21;
        private XRTableCell xrtDay22;
        private XRTableCell xrtDay23;
        private XRTableCell xrtDay24;
        private XRTableCell xrtDay25;
        private XRTableCell xrtDay26;
        private XRTableCell xrtDay27;
        private XRTableCell xrtDay28;
        private XRTableCell xrtDay29;
        private XRTableCell xrtDay30;
        private XRTableCell xrtDay31;
        private XRTableCell xrtWorkActual;
        private XRTableCell xrtGoWork;
        private XRTableCell xrtLate;
        private XRTableCell xrtLeaveL;
        private XRLabel lblReportDate;
        private XRLabel lblSignedByName;
        private XRTableCell xrTableCell5;
        private XRTableCell xrTableCell38;
        private XRTableCell xrTableCell39;
        private XRTableCell xrTableCell40;
        private XRTableCell xrTableCell41;
        private XRTable xrTable3;
        private XRTableRow xrTableRow5;
        private XRTableCell xrTableCell50;
        private XRTableCell xrTableCell51;
        private XRTableCell xrTableCell52;
        private XRTableCell xrTableCell53;
        private XRTableRow xrTableRow6;
        private XRTableCell xrTableCell57;
        private XRTableCell xrtLeaveR;
        private XRTableCell xrtLeaveK;
        private XRTableCell xrtLeaveP;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
        private readonly IContainer components = null;
        private XRTableCell xrTableCell2;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrlabel_1;
        private XRTableCell xrlabel_2;
        private XRTableCell xrlabel_3;
        private XRTableCell xrlabel_4;
        private XRTableCell xrlabel_5;
        private XRTableCell xrlabel_6;
        private XRTableCell xrlabel_7;
        private XRTableCell xrlabel_8;
        private XRTableCell xrlabel_9;
        private XRTableCell xrlabel_10;
        private XRTableCell xrlabel_11;
        private XRTableCell xrlabel_12;
        private XRTableCell xrlabel_13;
        private XRTableCell xrlabel_14;
        private XRTableCell xrlabel_15;
        private XRTableCell xrlabel_16;
        private XRTableCell xrlabel_17;
        private XRTableCell xrlabel_18;
        private XRTableCell xrlabel_19;
        private XRTableCell xrlabel_20;
        private XRTableCell xrlabel_21;
        private XRTableCell xrlabel_22;
        private XRTableCell xrlabel_23;
        private XRTableCell xrlabel_24;
        private XRTableCell xrlabel_25;
        private XRTableCell xrlabel_26;
        private XRTableCell xrlabel_27;
        private XRTableCell xrlabel_28;
        private XRTableCell xrlabel_29;
        private XRTableCell xrlabel_30;
        private XRTableCell xrlabel_31;
        private XRTableRow xrTableRow3;
        private XRTableCell xrTableCell66;
        private XRTableCell xrTableCell35;
        private XRTableCell xrTableCell67;
        private XRTableCell xrTableCell36;
        private XRTableCell xrTableCell68;
        private XRTableCell xrTableCell37;
        private XRTableCell xrTableCell69;
        private XRTableCell xrTableCell79;
        private XRTableCell xrTableCell56;
        private XRTableCell xrTableCell58;
        private XRTableCell xrTableCell80;
        private XRTableCell xrTableCell47;
        private XRTableCell xrTableCell70;
        private XRTableCell xrTableCell48;
        private XRTableCell xrTableCell71;
        private XRTableCell xrTableCell45;
        private XRTableCell xrTableCell77;
        private XRTableCell xrTableCell72;
        private XRTableCell xrTableCell78;
        private XRTableCell xrTableCell49;
        private XRTableCell xrTableCell73;
        private XRTableCell xrTableCell81;
        private XRTableCell xrTableCell46;
        private XRTableCell xrTableCell82;
        private XRTableCell xrTableCell74;
        private XRTableCell xrTableCell83;
        private XRTableCell xrTableCell54;
        private XRTableCell xrTableCell76;
        private XRTableCell xrlabel_Day29;
        private XRTableCell xrlabel_Day30;
        private XRTableCell xrlabel_Day31;

        /// <summary>
        /// Filter data
        /// </summary>
        private Filter _filter;

        /// <summary>
        /// Get Filter, implement IBaseReport interface
        /// </summary>
        /// <returns></returns>
        public Filter GetFilter()
        {
            return _filter;
        }

        /// <summary>
        /// Set Filter, implement IBaseReport interface
        /// </summary>
        /// <param name="filter"></param>
        public void SetFilter(Filter filter)
        {
            _filter = filter;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitFilter()
        {
            // init filter
            _filter = new Filter
            {
                Items = new List<FilterItem>()
            };
        }

        public rpHRM_TimeKeepingOffice()
        {
            InitializeComponent();
            // init Filter
            InitFilter();
        }

        int _index;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _index++;
            xrtIndex.Text = _index.ToString();
        }

        public void BindData()
        {
            var startDate = _filter.StartDate ?? ConvertUtils.GetStartDayOfMonth();
            var endDate = _filter.EndDate ?? ConvertUtils.GetLastDayOfMonth();
            var timeSheetReport = TimeSheetReportController.GetById(_filter.TimeSheetReportId);
            if (timeSheetReport != null)
            {
                xrCellReportName.Text = timeSheetReport.Name;
                if (timeSheetReport.StartDate != null)
                {
                    startDate = timeSheetReport.StartDate.Value;
                    SetColorByDay(startDate.Month, startDate.Year);
                }
                if (timeSheetReport.EndDate != null) endDate = timeSheetReport.EndDate.Value;
           
                var records =
                    TimeSheetEmployeeReportController.GetAll(null, null, null, _filter.TimeSheetReportId, null, null);
                if (records.Count > 0)
                {
                    var data = TimeSheetEventController.GetTimeSheetReport(records, startDate, endDate);
                   
                    DataSource = data;
                    xrtFullName.DataBindings.Add("Text", DataSource, "FullName");
                    xrtEmployeeCode.DataBindings.Add("Text", DataSource, "EmployeeCode");
                    xrtDay1.DataBindings.Add("Text", DataSource, "Day1.WorkConvert");
                    xrtDay2.DataBindings.Add("Text", DataSource, "Day2.WorkConvert");
                    xrtDay3.DataBindings.Add("Text", DataSource, "Day3.WorkConvert");
                    xrtDay4.DataBindings.Add("Text", DataSource, "Day4.WorkConvert");
                    xrtDay5.DataBindings.Add("Text", DataSource, "Day5.WorkConvert");
                    xrtDay6.DataBindings.Add("Text", DataSource, "Day6.WorkConvert");
                    xrtDay7.DataBindings.Add("Text", DataSource, "Day7.WorkConvert");
                    xrtDay8.DataBindings.Add("Text", DataSource, "Day8.WorkConvert");
                    xrtDay9.DataBindings.Add("Text", DataSource, "Day9.WorkConvert");
                    xrtDay10.DataBindings.Add("Text", DataSource, "Day10.WorkConvert");
                    xrtDay11.DataBindings.Add("Text", DataSource, "Day11.WorkConvert");
                    xrtDay12.DataBindings.Add("Text", DataSource, "Day12.WorkConvert");
                    xrtDay13.DataBindings.Add("Text", DataSource, "Day13.WorkConvert");
                    xrtDay14.DataBindings.Add("Text", DataSource, "Day14.WorkConvert");
                    xrtDay15.DataBindings.Add("Text", DataSource, "Day15.WorkConvert");
                    xrtDay16.DataBindings.Add("Text", DataSource, "Day16.WorkConvert");
                    xrtDay17.DataBindings.Add("Text", DataSource, "Day17.WorkConvert");
                    xrtDay18.DataBindings.Add("Text", DataSource, "Day18.WorkConvert");
                    xrtDay19.DataBindings.Add("Text", DataSource, "Day19.WorkConvert");
                    xrtDay20.DataBindings.Add("Text", DataSource, "Day20.WorkConvert");
                    xrtDay21.DataBindings.Add("Text", DataSource, "Day21.WorkConvert");
                    xrtDay22.DataBindings.Add("Text", DataSource, "Day22.WorkConvert");
                    xrtDay23.DataBindings.Add("Text", DataSource, "Day23.WorkConvert");
                    xrtDay24.DataBindings.Add("Text", DataSource, "Day24.WorkConvert");
                    xrtDay25.DataBindings.Add("Text", DataSource, "Day25.WorkConvert");
                    xrtDay26.DataBindings.Add("Text", DataSource, "Day26.WorkConvert");
                    xrtDay27.DataBindings.Add("Text", DataSource, "Day27.WorkConvert");
                    xrtDay28.DataBindings.Add("Text", DataSource, "Day28.WorkConvert");
                    xrtDay29.DataBindings.Add("Text", DataSource, "Day29.WorkConvert");
                    xrtDay30.DataBindings.Add("Text", DataSource, "Day30.WorkConvert");
                    xrtDay31.DataBindings.Add("Text", DataSource, "Day31.WorkConvert");
                    xrtWorkActual.DataBindings.Add("Text", DataSource, "TotalActual");
                    xrtLeaveK.DataBindings.Add("Text", DataSource, "TotalUnleaveK");
                    xrtLeaveL.DataBindings.Add("Text", DataSource, "TotalHolidayL");
                    xrtLeaveR.DataBindings.Add("Text", DataSource, "TotalUnpaidLeaveP");
                    xrtLeaveP.DataBindings.Add("Text", DataSource, "TotalPaidLeaveT");
                    xrtLate.DataBindings.Add("Text", DataSource, "TotalLateM");
                    xrtGoWork.DataBindings.Add("Text", DataSource, "TotalGoWorkC");
                }
            }
            // other items
            // label report date
            lblReportDate.Text = lblReportDate.Text.FormatWith(_filter.ReportDate.Day, _filter.ReportDate.Month, _filter.ReportDate.Year);
            // created by
            if (!string.IsNullOrEmpty(_filter.CreatedByTitle)) lblCreatedByTitle.Text = _filter.CreatedByTitle;
            if (!string.IsNullOrEmpty(_filter.CreatedByName)) lblCreatedByName.Text = _filter.CreatedByName;
           
            // signed by
            if (!string.IsNullOrEmpty(_filter.SignedByTitle)) lblSignedByTitle.Text = _filter.SignedByTitle;
            if (!string.IsNullOrEmpty(_filter.SignedByName)) lblSignedByName.Text = _filter.SignedByName;
        }

        /// <summary>
        /// Set color Saturday and Sunday
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        private void SetColorByDay(int month, int year)
        {
            var daysInMonth = DateTime.DaysInMonth(year, month);
            //init
            xrlabel_Day29.Text = "";
            xrlabel_Day30.Text = "";
            xrlabel_Day31.Text = "";
            for (var i = 1; i <= daysInMonth; i++)
            {
                var datetime = new DateTime(year, month, i);
                SetLabelByDay(i, datetime);
            }
        }

        /// <summary>
        /// set label foreach day
        /// </summary>
        /// <param name="i"></param>
        /// <param name="datetime"></param>
        private void SetLabelByDay(int i, DateTime datetime)
        {
            if ("xrlabel_1".Substring(8, 1) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_1.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_1.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_1.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_1.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_1.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_1.Text = @"T7";
                        xrlabel_1.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_1.Text = @"CN";
                        xrlabel_1.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_2".Substring(8, 1) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_2.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_2.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_2.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_2.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_2.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_2.Text = @"T7";
                        xrlabel_2.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_2.Text = @"CN";
                        xrlabel_2.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_3".Substring(8, 1) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_3.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_3.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_3.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_3.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_3.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_3.Text = @"T7";
                        xrlabel_3.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_3.Text = @"CN";
                        xrlabel_3.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_4".Substring(8, 1) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_4.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_4.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_4.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_4.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_4.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_4.Text = @"T7";
                        xrlabel_4.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_4.Text = @"CN";
                        xrlabel_4.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_5".Substring(8, 1) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_5.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_5.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_5.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_5.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_5.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_5.Text = @"T7";
                        xrlabel_5.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_5.Text = @"CN";
                        xrlabel_5.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_6".Substring(8, 1) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_6.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_6.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_6.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_6.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_6.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_6.Text = @"T7";
                        xrlabel_6.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_6.Text = @"CN";
                        xrlabel_6.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_7".Substring(8, 1) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_7.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_7.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_7.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_7.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_7.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_7.Text = @"T7";
                        xrlabel_7.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_7.Text = @"CN";
                        xrlabel_7.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_8".Substring(8, 1) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_8.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_8.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_8.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_8.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_8.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_8.Text = @"T7";
                        xrlabel_8.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_8.Text = @"CN";
                        xrlabel_8.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_9".Substring(8, 1) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_9.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_9.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_9.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_9.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_9.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_9.Text = @"T7";
                        xrlabel_9.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_9.Text = @"CN";
                        xrlabel_9.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_10".Substring(8, 2) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_10.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_10.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_10.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_10.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_10.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_10.Text = @"T7";
                        xrlabel_10.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_10.Text = @"CN";
                        xrlabel_10.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_11".Substring(8, 2) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_11.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_11.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_11.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_11.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_11.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_11.Text = @"T7";
                        xrlabel_11.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_11.Text = @"CN";
                        xrlabel_11.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_12".Substring(8, 2) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_12.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_12.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_12.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_12.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_12.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_12.Text = @"T7";
                        xrlabel_12.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_12.Text = @"CN";
                        xrlabel_12.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_13".Substring(8, 2) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_13.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_13.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_13.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_13.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_13.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_13.Text = @"T7";
                        xrlabel_13.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_13.Text = @"CN";
                        xrlabel_13.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_14".Substring(8, 2) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_14.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_14.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_14.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_14.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_14.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_14.Text = @"T7";
                        xrlabel_14.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_14.Text = @"CN";
                        xrlabel_14.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_15".Substring(8, 2) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_15.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_15.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_15.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_15.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_15.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_15.Text = @"T7";
                        xrlabel_15.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_15.Text = @"CN";
                        xrlabel_15.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_16".Substring(8, 2) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_16.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_16.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_16.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_16.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_16.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_16.Text = @"T7";
                        xrlabel_16.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_16.Text = @"CN";
                        xrlabel_16.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_17".Substring(8, 2) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_17.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_17.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_17.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_17.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_17.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_17.Text = @"T7";
                        xrlabel_17.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_17.Text = @"CN";
                        xrlabel_17.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_18".Substring(8, 2) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_18.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_18.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_18.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_18.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_18.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_18.Text = @"T7";
                        xrlabel_18.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_18.Text = @"CN";
                        xrlabel_18.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_19".Substring(8, 2) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_19.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_19.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_19.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_19.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_19.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_19.Text = @"T7";
                        xrlabel_19.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_19.Text = @"CN";
                        xrlabel_19.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_20".Substring(8, 2) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_20.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_20.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_20.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_20.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_20.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_20.Text = @"T7";
                        xrlabel_20.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_20.Text = @"CN";
                        xrlabel_20.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_21".Substring(8, 2) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_21.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_21.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_21.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_21.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_21.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_21.Text = @"T7";
                        xrlabel_21.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_21.Text = @"CN";
                        xrlabel_21.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_22".Substring(8, 2) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_22.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_22.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_22.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_22.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_22.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_22.Text = @"T7";
                        xrlabel_22.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_22.Text = @"CN";
                        xrlabel_22.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_23".Substring(8, 2) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_23.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_23.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_23.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_23.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_23.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_23.Text = @"T7";
                        xrlabel_23.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_23.Text = @"CN";
                        xrlabel_23.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_24".Substring(8, 2) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_24.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_24.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_24.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_24.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_24.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_24.Text = @"T7";
                        xrlabel_24.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_24.Text = @"CN";
                        xrlabel_24.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_25".Substring(8, 2) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_25.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_25.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_25.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_25.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_25.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_25.Text = @"T7";
                        xrlabel_25.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_25.Text = @"CN";
                        xrlabel_25.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_26".Substring(8, 2) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_26.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_26.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_26.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_26.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_26.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_26.Text = @"T7";
                        xrlabel_26.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_26.Text = @"CN";
                        xrlabel_26.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_27".Substring(8, 2) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_27.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_27.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_27.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_27.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_27.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_27.Text = @"T7";
                        xrlabel_27.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_27.Text = @"CN";
                        xrlabel_27.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_28".Substring(8, 2) == i.ToString())
            {
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_28.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_28.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_28.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_28.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_28.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_28.Text = @"T7";
                        xrlabel_28.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_28.Text = @"CN";
                        xrlabel_28.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_29".Substring(8, 2) == i.ToString())
            {
                xrlabel_Day29.Text = "{0}".FormatWith(i);
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_29.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_29.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_29.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_29.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_29.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_29.Text = @"T7";
                        xrlabel_29.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_29.Text = @"CN";
                        xrlabel_29.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_30".Substring(8, 2) == i.ToString())
            {
                xrlabel_Day30.Text = "{0}".FormatWith(i);
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_30.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_30.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_30.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_30.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_30.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_30.Text = @"T7";
                        xrlabel_30.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_30.Text = @"CN";
                        xrlabel_30.ForeColor = Color.Red;
                        break;
                }
            }
            if ("xrlabel_31".Substring(8, 2) == i.ToString())
            {
                xrlabel_Day31.Text = "{0}".FormatWith(i);
                switch (datetime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        xrlabel_31.Text = @"T2";
                        break;
                    case DayOfWeek.Tuesday:
                        xrlabel_31.Text = @"T3";
                        break;
                    case DayOfWeek.Wednesday:
                        xrlabel_31.Text = @"T4";
                        break;
                    case DayOfWeek.Thursday:
                        xrlabel_31.Text = @"T5";
                        break;
                    case DayOfWeek.Friday:
                        xrlabel_31.Text = @"T6";
                        break;
                    case DayOfWeek.Saturday:
                        xrlabel_31.Text = @"T7";
                        xrlabel_31.ForeColor = Color.Red;
                        break;
                    case DayOfWeek.Sunday:
                        xrlabel_31.Text = @"CN";
                        xrlabel_31.ForeColor = Color.Red;
                        break;
                }
            }
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.tblDetail = new DevExpress.XtraReports.UI.XRTable();
            this.xrDetailRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrtIndex = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtFullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtEmployeeCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtDay31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtWorkActual = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtGoWork = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtLate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtLeaveL = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtLeaveR = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtLeaveK = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtLeaveP = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.tblReportHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCellTenBieuMau = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellReportName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellOrganization = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell401 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell402 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell403 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell601 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell602 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell603 = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.tblPageHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell241 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell242 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrlabel_1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell67 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell69 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell79 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell70 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell77 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell78 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell73 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell82 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell74 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell83 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell76 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_Day29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_Day30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlabel_Day31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSignedByName = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCreatedByName = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCreatedByTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSignedByTitle = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblReportHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblDetail});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // tblDetail
            // 
            this.tblDetail.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tblDetail.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tblDetail.Name = "tblDetail";
            this.tblDetail.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrDetailRow1});
            this.tblDetail.SizeF = new System.Drawing.SizeF(1136.003F, 25F);
            this.tblDetail.StylePriority.UseBorders = false;
            // 
            // xrDetailRow1
            // 
            this.xrDetailRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrtIndex,
            this.xrtFullName,
            this.xrtEmployeeCode,
            this.xrtDay1,
            this.xrtDay2,
            this.xrtDay3,
            this.xrtDay4,
            this.xrtDay5,
            this.xrtDay6,
            this.xrtDay7,
            this.xrtDay8,
            this.xrtDay9,
            this.xrtDay10,
            this.xrtDay11,
            this.xrtDay12,
            this.xrtDay13,
            this.xrtDay14,
            this.xrtDay15,
            this.xrtDay16,
            this.xrtDay17,
            this.xrtDay18,
            this.xrtDay19,
            this.xrtDay20,
            this.xrtDay21,
            this.xrtDay22,
            this.xrtDay23,
            this.xrtDay24,
            this.xrtDay25,
            this.xrtDay26,
            this.xrtDay27,
            this.xrtDay28,
            this.xrtDay29,
            this.xrtDay30,
            this.xrtDay31,
            this.xrtWorkActual,
            this.xrtGoWork,
            this.xrtLate,
            this.xrtLeaveL,
            this.xrtLeaveR,
            this.xrtLeaveK,
            this.xrtLeaveP});
            this.xrDetailRow1.Name = "xrDetailRow1";
            this.xrDetailRow1.Weight = 1D;
            // 
            // xrtIndex
            // 
            this.xrtIndex.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtIndex.Name = "xrtIndex";
            this.xrtIndex.StylePriority.UseBorders = false;
            this.xrtIndex.StylePriority.UseTextAlignment = false;
            this.xrtIndex.Text = " ";
            this.xrtIndex.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtIndex.Weight = 0.22999999999999998D;
            this.xrtIndex.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrtFullName
            // 
            this.xrtFullName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtFullName.Name = "xrtFullName";
            this.xrtFullName.StylePriority.UseBorders = false;
            this.xrtFullName.StylePriority.UseTextAlignment = false;
            this.xrtFullName.Text = " ";
            this.xrtFullName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrtFullName.Weight = 1.5666665649414053D;
            // 
            // xrtEmployeeCode
            // 
            this.xrtEmployeeCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtEmployeeCode.Name = "xrtEmployeeCode";
            this.xrtEmployeeCode.StylePriority.UseBorders = false;
            this.xrtEmployeeCode.StylePriority.UseTextAlignment = false;
            this.xrtEmployeeCode.Text = " ";
            this.xrtEmployeeCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtEmployeeCode.Weight = 0.68833343505859423D;
            // 
            // xrtDay1
            // 
            this.xrtDay1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay1.Name = "xrtDay1";
            this.xrtDay1.StylePriority.UseBorders = false;
            this.xrtDay1.StylePriority.UseTextAlignment = false;
            this.xrtDay1.Text = " ";
            this.xrtDay1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay1.Weight = 0.23D;
            // 
            // xrtDay2
            // 
            this.xrtDay2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay2.Name = "xrtDay2";
            this.xrtDay2.StylePriority.UseBorders = false;
            this.xrtDay2.StylePriority.UseTextAlignment = false;
            this.xrtDay2.Text = " ";
            this.xrtDay2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay2.Weight = 0.22999999999999998D;
            // 
            // xrtDay3
            // 
            this.xrtDay3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay3.Name = "xrtDay3";
            this.xrtDay3.StylePriority.UseBorders = false;
            this.xrtDay3.StylePriority.UseTextAlignment = false;
            this.xrtDay3.Text = " ";
            this.xrtDay3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay3.Weight = 0.22999999999999998D;
            // 
            // xrtDay4
            // 
            this.xrtDay4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay4.Name = "xrtDay4";
            this.xrtDay4.StylePriority.UseBorders = false;
            this.xrtDay4.StylePriority.UseTextAlignment = false;
            this.xrtDay4.Text = " ";
            this.xrtDay4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay4.Weight = 0.22999999999999998D;
            // 
            // xrtDay5
            // 
            this.xrtDay5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay5.Name = "xrtDay5";
            this.xrtDay5.StylePriority.UseBorders = false;
            this.xrtDay5.StylePriority.UseTextAlignment = false;
            this.xrtDay5.Text = " ";
            this.xrtDay5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay5.Weight = 0.22999999999999998D;
            // 
            // xrtDay6
            // 
            this.xrtDay6.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay6.Name = "xrtDay6";
            this.xrtDay6.StylePriority.UseBorders = false;
            this.xrtDay6.StylePriority.UseTextAlignment = false;
            this.xrtDay6.Text = " ";
            this.xrtDay6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay6.Weight = 0.22999999999999998D;
            // 
            // xrtDay7
            // 
            this.xrtDay7.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay7.Name = "xrtDay7";
            this.xrtDay7.StylePriority.UseBorders = false;
            this.xrtDay7.StylePriority.UseTextAlignment = false;
            this.xrtDay7.Text = " ";
            this.xrtDay7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay7.Weight = 0.23000000000000004D;
            // 
            // xrtDay8
            // 
            this.xrtDay8.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay8.Name = "xrtDay8";
            this.xrtDay8.StylePriority.UseBorders = false;
            this.xrtDay8.StylePriority.UseTextAlignment = false;
            this.xrtDay8.Text = " ";
            this.xrtDay8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay8.Weight = 0.23000000000000004D;
            // 
            // xrtDay9
            // 
            this.xrtDay9.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay9.Name = "xrtDay9";
            this.xrtDay9.StylePriority.UseBorders = false;
            this.xrtDay9.StylePriority.UseTextAlignment = false;
            this.xrtDay9.Text = " ";
            this.xrtDay9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay9.Weight = 0.23000000000000004D;
            // 
            // xrtDay10
            // 
            this.xrtDay10.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay10.Name = "xrtDay10";
            this.xrtDay10.StylePriority.UseBorders = false;
            this.xrtDay10.StylePriority.UseTextAlignment = false;
            this.xrtDay10.Text = " ";
            this.xrtDay10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay10.Weight = 0.23000000000000004D;
            // 
            // xrtDay11
            // 
            this.xrtDay11.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay11.Name = "xrtDay11";
            this.xrtDay11.StylePriority.UseBorders = false;
            this.xrtDay11.StylePriority.UseTextAlignment = false;
            this.xrtDay11.Text = " ";
            this.xrtDay11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay11.Weight = 0.23000000000000004D;
            // 
            // xrtDay12
            // 
            this.xrtDay12.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay12.Name = "xrtDay12";
            this.xrtDay12.StylePriority.UseBorders = false;
            this.xrtDay12.StylePriority.UseTextAlignment = false;
            this.xrtDay12.Text = " ";
            this.xrtDay12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay12.Weight = 0.23000061035156255D;
            // 
            // xrtDay13
            // 
            this.xrtDay13.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay13.Name = "xrtDay13";
            this.xrtDay13.StylePriority.UseBorders = false;
            this.xrtDay13.StylePriority.UseTextAlignment = false;
            this.xrtDay13.Text = " ";
            this.xrtDay13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay13.Weight = 0.22999999999999993D;
            // 
            // xrtDay14
            // 
            this.xrtDay14.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay14.Name = "xrtDay14";
            this.xrtDay14.StylePriority.UseBorders = false;
            this.xrtDay14.StylePriority.UseTextAlignment = false;
            this.xrtDay14.Text = " ";
            this.xrtDay14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay14.Weight = 0.22999938964843741D;
            // 
            // xrtDay15
            // 
            this.xrtDay15.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay15.Name = "xrtDay15";
            this.xrtDay15.StylePriority.UseBorders = false;
            this.xrtDay15.StylePriority.UseTextAlignment = false;
            this.xrtDay15.Text = " ";
            this.xrtDay15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay15.Weight = 0.23000000000000009D;
            // 
            // xrtDay16
            // 
            this.xrtDay16.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay16.Name = "xrtDay16";
            this.xrtDay16.StylePriority.UseBorders = false;
            this.xrtDay16.StylePriority.UseTextAlignment = false;
            this.xrtDay16.Text = " ";
            this.xrtDay16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay16.Weight = 0.22999999999999998D;
            // 
            // xrtDay17
            // 
            this.xrtDay17.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay17.Name = "xrtDay17";
            this.xrtDay17.StylePriority.UseBorders = false;
            this.xrtDay17.StylePriority.UseTextAlignment = false;
            this.xrtDay17.Text = " ";
            this.xrtDay17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay17.Weight = 0.22999999999999998D;
            // 
            // xrtDay18
            // 
            this.xrtDay18.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay18.Name = "xrtDay18";
            this.xrtDay18.StylePriority.UseBorders = false;
            this.xrtDay18.StylePriority.UseTextAlignment = false;
            this.xrtDay18.Text = " ";
            this.xrtDay18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay18.Weight = 0.22999999999999998D;
            // 
            // xrtDay19
            // 
            this.xrtDay19.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay19.Name = "xrtDay19";
            this.xrtDay19.StylePriority.UseBorders = false;
            this.xrtDay19.StylePriority.UseTextAlignment = false;
            this.xrtDay19.Text = " ";
            this.xrtDay19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay19.Weight = 0.22999999999999998D;
            // 
            // xrtDay20
            // 
            this.xrtDay20.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay20.Name = "xrtDay20";
            this.xrtDay20.StylePriority.UseBorders = false;
            this.xrtDay20.StylePriority.UseTextAlignment = false;
            this.xrtDay20.Text = " ";
            this.xrtDay20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay20.Weight = 0.22999999999999998D;
            // 
            // xrtDay21
            // 
            this.xrtDay21.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay21.Name = "xrtDay21";
            this.xrtDay21.StylePriority.UseBorders = false;
            this.xrtDay21.StylePriority.UseTextAlignment = false;
            this.xrtDay21.Text = " ";
            this.xrtDay21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay21.Weight = 0.22999999999999998D;
            // 
            // xrtDay22
            // 
            this.xrtDay22.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay22.Name = "xrtDay22";
            this.xrtDay22.StylePriority.UseBorders = false;
            this.xrtDay22.StylePriority.UseTextAlignment = false;
            this.xrtDay22.Text = " ";
            this.xrtDay22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay22.Weight = 0.22999999999999998D;
            // 
            // xrtDay23
            // 
            this.xrtDay23.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay23.Name = "xrtDay23";
            this.xrtDay23.StylePriority.UseBorders = false;
            this.xrtDay23.StylePriority.UseTextAlignment = false;
            this.xrtDay23.Text = " ";
            this.xrtDay23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay23.Weight = 0.22999999999999998D;
            // 
            // xrtDay24
            // 
            this.xrtDay24.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay24.Name = "xrtDay24";
            this.xrtDay24.StylePriority.UseBorders = false;
            this.xrtDay24.StylePriority.UseTextAlignment = false;
            this.xrtDay24.Text = " ";
            this.xrtDay24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay24.Weight = 0.22999999999999998D;
            // 
            // xrtDay25
            // 
            this.xrtDay25.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay25.Name = "xrtDay25";
            this.xrtDay25.StylePriority.UseBorders = false;
            this.xrtDay25.StylePriority.UseTextAlignment = false;
            this.xrtDay25.Text = " ";
            this.xrtDay25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay25.Weight = 0.22999999999999998D;
            // 
            // xrtDay26
            // 
            this.xrtDay26.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay26.Name = "xrtDay26";
            this.xrtDay26.StylePriority.UseBorders = false;
            this.xrtDay26.StylePriority.UseTextAlignment = false;
            this.xrtDay26.Text = " ";
            this.xrtDay26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay26.Weight = 0.22999999999999998D;
            // 
            // xrtDay27
            // 
            this.xrtDay27.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay27.Name = "xrtDay27";
            this.xrtDay27.StylePriority.UseBorders = false;
            this.xrtDay27.StylePriority.UseTextAlignment = false;
            this.xrtDay27.Text = " ";
            this.xrtDay27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay27.Weight = 0.22999999999999998D;
            // 
            // xrtDay28
            // 
            this.xrtDay28.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay28.Name = "xrtDay28";
            this.xrtDay28.StylePriority.UseBorders = false;
            this.xrtDay28.StylePriority.UseTextAlignment = false;
            this.xrtDay28.Text = " ";
            this.xrtDay28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay28.Weight = 0.22999999999999998D;
            // 
            // xrtDay29
            // 
            this.xrtDay29.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay29.Name = "xrtDay29";
            this.xrtDay29.StylePriority.UseBorders = false;
            this.xrtDay29.StylePriority.UseTextAlignment = false;
            this.xrtDay29.Text = " ";
            this.xrtDay29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay29.Weight = 0.22999999999999998D;
            // 
            // xrtDay30
            // 
            this.xrtDay30.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay30.Name = "xrtDay30";
            this.xrtDay30.StylePriority.UseBorders = false;
            this.xrtDay30.StylePriority.UseTextAlignment = false;
            this.xrtDay30.Text = " ";
            this.xrtDay30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay30.Weight = 0.220000073245065D;
            // 
            // xrtDay31
            // 
            this.xrtDay31.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtDay31.Name = "xrtDay31";
            this.xrtDay31.StylePriority.UseBorders = false;
            this.xrtDay31.StylePriority.UseTextAlignment = false;
            this.xrtDay31.Text = " ";
            this.xrtDay31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtDay31.Weight = 0.23999992675493498D;
            // 
            // xrtWorkActual
            // 
            this.xrtWorkActual.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtWorkActual.Name = "xrtWorkActual";
            this.xrtWorkActual.StylePriority.UseBorders = false;
            this.xrtWorkActual.StylePriority.UseTextAlignment = false;
            this.xrtWorkActual.Text = " ";
            this.xrtWorkActual.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtWorkActual.Weight = 0.23000000000000004D;
            // 
            // xrtGoWork
            // 
            this.xrtGoWork.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtGoWork.Name = "xrtGoWork";
            this.xrtGoWork.StylePriority.UseBorders = false;
            this.xrtGoWork.StylePriority.UseTextAlignment = false;
            this.xrtGoWork.Text = " ";
            this.xrtGoWork.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtGoWork.Weight = 0.22999999999999993D;
            // 
            // xrtLate
            // 
            this.xrtLate.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtLate.Name = "xrtLate";
            this.xrtLate.StylePriority.UseBorders = false;
            this.xrtLate.StylePriority.UseTextAlignment = false;
            this.xrtLate.Text = " ";
            this.xrtLate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtLate.Weight = 0.22999938964843741D;
            // 
            // xrtLeaveL
            // 
            this.xrtLeaveL.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtLeaveL.Name = "xrtLeaveL";
            this.xrtLeaveL.StylePriority.UseBorders = false;
            this.xrtLeaveL.StylePriority.UseTextAlignment = false;
            this.xrtLeaveL.Text = " ";
            this.xrtLeaveL.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtLeaveL.Weight = 0.24383542936082553D;
            // 
            // xrtLeaveR
            // 
            this.xrtLeaveR.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtLeaveR.Name = "xrtLeaveR";
            this.xrtLeaveR.StylePriority.UseBorders = false;
            this.xrtLeaveR.StylePriority.UseTextAlignment = false;
            this.xrtLeaveR.Text = " ";
            this.xrtLeaveR.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtLeaveR.Weight = 0.27709996915762258D;
            // 
            // xrtLeaveK
            // 
            this.xrtLeaveK.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtLeaveK.Name = "xrtLeaveK";
            this.xrtLeaveK.StylePriority.UseBorders = false;
            this.xrtLeaveK.StylePriority.UseTextAlignment = false;
            this.xrtLeaveK.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtLeaveK.Weight = 0.26699998645778283D;
            // 
            // xrtLeaveP
            // 
            this.xrtLeaveP.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtLeaveP.Name = "xrtLeaveP";
            this.xrtLeaveP.StylePriority.UseBorders = false;
            this.xrtLeaveP.StylePriority.UseTextAlignment = false;
            this.xrtLeaveP.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrtLeaveP.Weight = 0.26709920672859766D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 14.99999F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblReportHeader});
            this.ReportHeader.HeightF = 63.08333F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // tblReportHeader
            // 
            this.tblReportHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tblReportHeader.Name = "tblReportHeader";
            this.tblReportHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow19,
            this.xrTableRow2,
            this.xrTableRow4});
            this.tblReportHeader.SizeF = new System.Drawing.SizeF(1142.129F, 62F);
            // 
            // xrTableRow19
            // 
            this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCellTenBieuMau,
            this.xrCellReportName,
            this.xrCellOrganization});
            this.xrTableRow19.Name = "xrTableRow19";
            this.xrTableRow19.Weight = 0.79999990231146945D;
            // 
            // xrCellTenBieuMau
            // 
            this.xrCellTenBieuMau.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrCellTenBieuMau.Name = "xrCellTenBieuMau";
            this.xrCellTenBieuMau.StylePriority.UseFont = false;
            this.xrCellTenBieuMau.StylePriority.UseTextAlignment = false;
            this.xrCellTenBieuMau.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTenBieuMau.Weight = 1.5748560589095728D;
            // 
            // xrCellReportName
            // 
            this.xrCellReportName.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrCellReportName.Name = "xrCellReportName";
            this.xrCellReportName.StylePriority.UseFont = false;
            this.xrCellReportName.Text = "Báo cáo chấm công hàng chính {0}/{1}";
            this.xrCellReportName.Weight = 3.2928199055848522D;
            // 
            // xrCellOrganization
            // 
            this.xrCellOrganization.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrCellOrganization.Name = "xrCellOrganization";
            this.xrCellOrganization.StylePriority.UseFont = false;
            this.xrCellOrganization.StylePriority.UseTextAlignment = false;
            this.xrCellOrganization.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCellOrganization.Weight = 1.1279513169596362D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell401,
            this.xrTableCell402,
            this.xrTableCell403});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 0.933333189760807D;
            // 
            // xrTableCell401
            // 
            this.xrTableCell401.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCell401.Multiline = true;
            this.xrTableCell401.Name = "xrTableCell401";
            this.xrTableCell401.StylePriority.UseFont = false;
            this.xrTableCell401.Weight = 1.5748560433256003D;
            // 
            // xrTableCell402
            // 
            this.xrTableCell402.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCell402.Name = "xrTableCell402";
            this.xrTableCell402.StylePriority.UseFont = false;
            this.xrTableCell402.Weight = 3.2928199438412697D;
            // 
            // xrTableCell403
            // 
            this.xrTableCell403.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCell403.Name = "xrTableCell403";
            this.xrTableCell403.StylePriority.UseFont = false;
            this.xrTableCell403.StylePriority.UseTextAlignment = false;
            this.xrTableCell403.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell403.Weight = 1.1279512942871914D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell601,
            this.xrTableCell602,
            this.xrTableCell603});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 0.33333324536656561D;
            // 
            // xrTableCell601
            // 
            this.xrTableCell601.Name = "xrTableCell601";
            this.xrTableCell601.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrTableCell601.StylePriority.UsePadding = false;
            this.xrTableCell601.Weight = 1.5748560433256003D;
            // 
            // xrTableCell602
            // 
            this.xrTableCell602.Name = "xrTableCell602";
            this.xrTableCell602.Weight = 3.2928199438412697D;
            // 
            // xrTableCell603
            // 
            this.xrTableCell603.Name = "xrTableCell603";
            this.xrTableCell603.Weight = 1.1279512942871914D;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblPageHeader});
            this.PageHeader.HeightF = 60.87508F;
            this.PageHeader.Name = "PageHeader";
            // 
            // tblPageHeader
            // 
            this.tblPageHeader.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tblPageHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tblPageHeader.Name = "tblPageHeader";
            this.tblPageHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
            this.tblPageHeader.SizeF = new System.Drawing.SizeF(1138F, 60.87508F);
            this.tblPageHeader.StylePriority.UseBorders = false;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell241,
            this.xrTableCell242,
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell5,
            this.xrTableCell39,
            this.xrTableCell40,
            this.xrTableCell41,
            this.xrTableCell38});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 9.2D;
            // 
            // xrTableCell241
            // 
            this.xrTableCell241.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell241.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell241.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell241.Name = "xrTableCell241";
            this.xrTableCell241.StylePriority.UseBorderColor = false;
            this.xrTableCell241.StylePriority.UseBorders = false;
            this.xrTableCell241.StylePriority.UseFont = false;
            this.xrTableCell241.Text = "STT";
            this.xrTableCell241.Weight = 0.23D;
            // 
            // xrTableCell242
            // 
            this.xrTableCell242.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell242.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell242.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell242.Name = "xrTableCell242";
            this.xrTableCell242.StylePriority.UseBorderColor = false;
            this.xrTableCell242.StylePriority.UseBorders = false;
            this.xrTableCell242.StylePriority.UseFont = false;
            this.xrTableCell242.Text = "Họ và tên";
            this.xrTableCell242.Weight = 1.5666665649414067D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorderColor = false;
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "Mã NV";
            this.xrTableCell1.Weight = 0.688332977294922D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.xrTableCell2.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorderColor = false;
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Weight = 7.1299998474121136D;
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0.4166718F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow3});
            this.xrTable1.SizeF = new System.Drawing.SizeF(711.9999F, 60.87508F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrlabel_1,
            this.xrlabel_2,
            this.xrlabel_3,
            this.xrlabel_4,
            this.xrlabel_5,
            this.xrlabel_6,
            this.xrlabel_7,
            this.xrlabel_8,
            this.xrlabel_9,
            this.xrlabel_10,
            this.xrlabel_11,
            this.xrlabel_12,
            this.xrlabel_13,
            this.xrlabel_14,
            this.xrlabel_15,
            this.xrlabel_16,
            this.xrlabel_17,
            this.xrlabel_18,
            this.xrlabel_19,
            this.xrlabel_20,
            this.xrlabel_21,
            this.xrlabel_22,
            this.xrlabel_23,
            this.xrlabel_24,
            this.xrlabel_25,
            this.xrlabel_26,
            this.xrlabel_27,
            this.xrlabel_28,
            this.xrlabel_29,
            this.xrlabel_30,
            this.xrlabel_31});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 0.14143566555447051D;
            // 
            // xrlabel_1
            // 
            this.xrlabel_1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_1.Name = "xrlabel_1";
            this.xrlabel_1.StylePriority.UseBorders = false;
            this.xrlabel_1.Weight = 0.0077167760502132622D;
            // 
            // xrlabel_2
            // 
            this.xrlabel_2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_2.Name = "xrlabel_2";
            this.xrlabel_2.StylePriority.UseBorders = false;
            this.xrlabel_2.Weight = 0.00771672411450058D;
            // 
            // xrlabel_3
            // 
            this.xrlabel_3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_3.Name = "xrlabel_3";
            this.xrlabel_3.StylePriority.UseBorders = false;
            this.xrlabel_3.Weight = 0.0077167759493675424D;
            // 
            // xrlabel_4
            // 
            this.xrlabel_4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_4.Name = "xrlabel_4";
            this.xrlabel_4.StylePriority.UseBorders = false;
            this.xrlabel_4.Weight = 0.0077167765744557762D;
            // 
            // xrlabel_5
            // 
            this.xrlabel_5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_5.Name = "xrlabel_5";
            this.xrlabel_5.StylePriority.UseBorders = false;
            this.xrlabel_5.Weight = 0.0077167553256495859D;
            // 
            // xrlabel_6
            // 
            this.xrlabel_6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_6.Name = "xrlabel_6";
            this.xrlabel_6.StylePriority.UseBorders = false;
            this.xrlabel_6.Weight = 0.007716704130719251D;
            // 
            // xrlabel_7
            // 
            this.xrlabel_7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_7.Name = "xrlabel_7";
            this.xrlabel_7.StylePriority.UseBorders = false;
            this.xrlabel_7.Weight = 0.0077167758036217164D;
            // 
            // xrlabel_8
            // 
            this.xrlabel_8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_8.Name = "xrlabel_8";
            this.xrlabel_8.StylePriority.UseBorders = false;
            this.xrlabel_8.Weight = 0.0077167860426077851D;
            // 
            // xrlabel_9
            // 
            this.xrlabel_9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_9.Name = "xrlabel_9";
            this.xrlabel_9.StylePriority.UseBorders = false;
            this.xrlabel_9.Weight = 0.0077167553256495859D;
            // 
            // xrlabel_10
            // 
            this.xrlabel_10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_10.Name = "xrlabel_10";
            this.xrlabel_10.StylePriority.UseBorders = false;
            this.xrlabel_10.Weight = 0.0077167650903727115D;
            // 
            // xrlabel_11
            // 
            this.xrlabel_11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_11.Name = "xrlabel_11";
            this.xrlabel_11.StylePriority.UseBorders = false;
            this.xrlabel_11.Weight = 0.0077167334542840929D;
            // 
            // xrlabel_12
            // 
            this.xrlabel_12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_12.Name = "xrlabel_12";
            this.xrlabel_12.StylePriority.UseBorders = false;
            this.xrlabel_12.Weight = 0.0077167539322544809D;
            // 
            // xrlabel_13
            // 
            this.xrlabel_13.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_13.Name = "xrlabel_13";
            this.xrlabel_13.StylePriority.UseBorders = false;
            this.xrlabel_13.Weight = 0.0077167955643025789D;
            // 
            // xrlabel_14
            // 
            this.xrlabel_14.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_14.Name = "xrlabel_14";
            this.xrlabel_14.StylePriority.UseBorders = false;
            this.xrlabel_14.Weight = 0.00771675393225448D;
            // 
            // xrlabel_15
            // 
            this.xrlabel_15.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_15.Name = "xrlabel_15";
            this.xrlabel_15.StylePriority.UseBorders = false;
            this.xrlabel_15.Weight = 0.0077167334542840946D;
            // 
            // xrlabel_16
            // 
            this.xrlabel_16.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_16.Name = "xrlabel_16";
            this.xrlabel_16.StylePriority.UseBorders = false;
            this.xrlabel_16.Weight = 0.00771675393225448D;
            // 
            // xrlabel_17
            // 
            this.xrlabel_17.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_17.Name = "xrlabel_17";
            this.xrlabel_17.StylePriority.UseBorders = false;
            this.xrlabel_17.Weight = 0.007716712976313711D;
            // 
            // xrlabel_18
            // 
            this.xrlabel_18.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_18.Name = "xrlabel_18";
            this.xrlabel_18.StylePriority.UseBorders = false;
            this.xrlabel_18.Weight = 0.0077167744102248654D;
            // 
            // xrlabel_19
            // 
            this.xrlabel_19.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_19.Name = "xrlabel_19";
            this.xrlabel_19.StylePriority.UseBorders = false;
            this.xrlabel_19.Weight = 0.00771677505016144D;
            // 
            // xrlabel_20
            // 
            this.xrlabel_20.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_20.Name = "xrlabel_20";
            this.xrlabel_20.StylePriority.UseBorders = false;
            this.xrlabel_20.Weight = 0.0077166924983433248D;
            // 
            // xrlabel_21
            // 
            this.xrlabel_21.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_21.Name = "xrlabel_21";
            this.xrlabel_21.StylePriority.UseBorders = false;
            this.xrlabel_21.Weight = 0.00771677505016144D;
            // 
            // xrlabel_22
            // 
            this.xrlabel_22.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_22.Name = "xrlabel_22";
            this.xrlabel_22.StylePriority.UseBorders = false;
            this.xrlabel_22.Weight = 0.0077168160061022095D;
            // 
            // xrlabel_23
            // 
            this.xrlabel_23.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_23.Name = "xrlabel_23";
            this.xrlabel_23.StylePriority.UseBorders = false;
            this.xrlabel_23.Weight = 0.0077167340942206706D;
            // 
            // xrlabel_24
            // 
            this.xrlabel_24.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_24.Name = "xrlabel_24";
            this.xrlabel_24.StylePriority.UseBorders = false;
            this.xrlabel_24.Weight = 0.0077167136162502852D;
            // 
            // xrlabel_25
            // 
            this.xrlabel_25.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_25.Name = "xrlabel_25";
            this.xrlabel_25.StylePriority.UseBorders = false;
            this.xrlabel_25.Weight = 0.0077167891287660793D;
            // 
            // xrlabel_26
            // 
            this.xrlabel_26.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_26.Name = "xrlabel_26";
            this.xrlabel_26.StylePriority.UseBorders = false;
            this.xrlabel_26.Weight = 0.00771675393225448D;
            // 
            // xrlabel_27
            // 
            this.xrlabel_27.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_27.Name = "xrlabel_27";
            this.xrlabel_27.StylePriority.UseBorders = false;
            this.xrlabel_27.Weight = 0.0077166720203729429D;
            // 
            // xrlabel_28
            // 
            this.xrlabel_28.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_28.Name = "xrlabel_28";
            this.xrlabel_28.StylePriority.UseBorders = false;
            this.xrlabel_28.Weight = 0.0077168569982137324D;
            // 
            // xrlabel_29
            // 
            this.xrlabel_29.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_29.Name = "xrlabel_29";
            this.xrlabel_29.StylePriority.UseBorders = false;
            this.xrlabel_29.Weight = 0.0077166726964802675D;
            // 
            // xrlabel_30
            // 
            this.xrlabel_30.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_30.Name = "xrlabel_30";
            this.xrlabel_30.StylePriority.UseBorders = false;
            this.xrlabel_30.Weight = 0.0077167539322544826D;
            // 
            // xrlabel_31
            // 
            this.xrlabel_31.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_31.Name = "xrlabel_31";
            this.xrlabel_31.StylePriority.UseBorders = false;
            this.xrlabel_31.Weight = 0.00771677952971746D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell66,
            this.xrTableCell35,
            this.xrTableCell67,
            this.xrTableCell36,
            this.xrTableCell68,
            this.xrTableCell37,
            this.xrTableCell69,
            this.xrTableCell79,
            this.xrTableCell56,
            this.xrTableCell58,
            this.xrTableCell80,
            this.xrTableCell47,
            this.xrTableCell70,
            this.xrTableCell48,
            this.xrTableCell71,
            this.xrTableCell45,
            this.xrTableCell77,
            this.xrTableCell72,
            this.xrTableCell78,
            this.xrTableCell49,
            this.xrTableCell73,
            this.xrTableCell81,
            this.xrTableCell46,
            this.xrTableCell82,
            this.xrTableCell74,
            this.xrTableCell83,
            this.xrTableCell54,
            this.xrTableCell76,
            this.xrlabel_Day29,
            this.xrlabel_Day30,
            this.xrlabel_Day31});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 0.14960334190619798D;
            // 
            // xrTableCell66
            // 
            this.xrTableCell66.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell66.Name = "xrTableCell66";
            this.xrTableCell66.StylePriority.UseBorders = false;
            this.xrTableCell66.Text = "1";
            this.xrTableCell66.Weight = 0.0077167344855776981D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.StylePriority.UseBorders = false;
            this.xrTableCell35.Text = "2";
            this.xrTableCell35.Weight = 0.0077167549814484123D;
            // 
            // xrTableCell67
            // 
            this.xrTableCell67.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell67.Name = "xrTableCell67";
            this.xrTableCell67.StylePriority.UseBorders = false;
            this.xrTableCell67.Text = "3";
            this.xrTableCell67.Weight = 0.007716775659714902D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.StylePriority.UseBorders = false;
            this.xrTableCell36.Text = "4";
            this.xrTableCell36.Weight = 0.0077167449427577939D;
            // 
            // xrTableCell68
            // 
            this.xrTableCell68.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell68.Name = "xrTableCell68";
            this.xrTableCell68.StylePriority.UseBorders = false;
            this.xrTableCell68.Text = "5";
            this.xrTableCell68.Weight = 0.00771677546604899D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.StylePriority.UseBorders = false;
            this.xrTableCell37.Text = "6";
            this.xrTableCell37.Weight = 0.0077167345101047244D;
            // 
            // xrTableCell69
            // 
            this.xrTableCell69.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell69.Name = "xrTableCell69";
            this.xrTableCell69.StylePriority.UseBorders = false;
            this.xrTableCell69.Text = "7";
            this.xrTableCell69.Weight = 0.0077167957369733194D;
            // 
            // xrTableCell79
            // 
            this.xrTableCell79.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell79.Name = "xrTableCell79";
            this.xrTableCell79.StylePriority.UseBorders = false;
            this.xrTableCell79.Text = "8";
            this.xrTableCell79.Weight = 0.0077167557072922721D;
            // 
            // xrTableCell56
            // 
            this.xrTableCell56.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell56.Name = "xrTableCell56";
            this.xrTableCell56.StylePriority.UseBorders = false;
            this.xrTableCell56.Text = "9";
            this.xrTableCell56.Weight = 0.0077167249903340755D;
            // 
            // xrTableCell58
            // 
            this.xrTableCell58.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell58.Name = "xrTableCell58";
            this.xrTableCell58.StylePriority.UseBorders = false;
            this.xrTableCell58.Text = "10";
            this.xrTableCell58.Weight = 0.00771675458346335D;
            // 
            // xrTableCell80
            // 
            this.xrTableCell80.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell80.Name = "xrTableCell80";
            this.xrTableCell80.StylePriority.UseBorders = false;
            this.xrTableCell80.Text = "11";
            this.xrTableCell80.Weight = 0.007716774745398931D;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.StylePriority.UseBorders = false;
            this.xrTableCell47.Text = "12";
            this.xrTableCell47.Weight = 0.0077167542674285482D;
            // 
            // xrTableCell70
            // 
            this.xrTableCell70.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell70.Name = "xrTableCell70";
            this.xrTableCell70.StylePriority.UseBorders = false;
            this.xrTableCell70.Text = "13";
            this.xrTableCell70.Weight = 0.007716774476807178D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.StylePriority.UseBorders = false;
            this.xrTableCell48.Text = "14";
            this.xrTableCell48.Weight = 0.007716714322769172D;
            // 
            // xrTableCell71
            // 
            this.xrTableCell71.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell71.Name = "xrTableCell71";
            this.xrTableCell71.StylePriority.UseBorders = false;
            this.xrTableCell71.Text = "15";
            this.xrTableCell71.Weight = 0.0077167539988367909D;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.StylePriority.UseBorders = false;
            this.xrTableCell45.Text = "16";
            this.xrTableCell45.Weight = 0.0077167539988367943D;
            // 
            // xrTableCell77
            // 
            this.xrTableCell77.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell77.Name = "xrTableCell77";
            this.xrTableCell77.StylePriority.UseBorders = false;
            this.xrTableCell77.Text = "17";
            this.xrTableCell77.Weight = 0.0077167542674285439D;
            // 
            // xrTableCell72
            // 
            this.xrTableCell72.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell72.Name = "xrTableCell72";
            this.xrTableCell72.StylePriority.UseBorders = false;
            this.xrTableCell72.Text = "18";
            this.xrTableCell72.Weight = 0.007716795223369325D;
            // 
            // xrTableCell78
            // 
            this.xrTableCell78.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell78.Name = "xrTableCell78";
            this.xrTableCell78.StylePriority.UseBorders = false;
            this.xrTableCell78.Text = "19";
            this.xrTableCell78.Weight = 0.0077166928335173912D;
            // 
            // xrTableCell49
            // 
            this.xrTableCell49.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell49.Name = "xrTableCell49";
            this.xrTableCell49.StylePriority.UseBorders = false;
            this.xrTableCell49.Text = "20";
            this.xrTableCell49.Weight = 0.0077168361793100888D;
            // 
            // xrTableCell73
            // 
            this.xrTableCell73.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell73.Name = "xrTableCell73";
            this.xrTableCell73.StylePriority.UseBorders = false;
            this.xrTableCell73.Text = "21";
            this.xrTableCell73.Weight = 0.0077167341608029814D;
            // 
            // xrTableCell81
            // 
            this.xrTableCell81.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell81.Name = "xrTableCell81";
            this.xrTableCell81.StylePriority.UseBorders = false;
            this.xrTableCell81.Text = "22";
            this.xrTableCell81.Weight = 0.0077167741054623568D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.StylePriority.UseBorders = false;
            this.xrTableCell46.Text = "23";
            this.xrTableCell46.Weight = 0.0077167337894581628D;
            // 
            // xrTableCell82
            // 
            this.xrTableCell82.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell82.Name = "xrTableCell82";
            this.xrTableCell82.StylePriority.UseBorders = false;
            this.xrTableCell82.Text = "24";
            this.xrTableCell82.Weight = 0.0077167133114877783D;
            // 
            // xrTableCell74
            // 
            this.xrTableCell74.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell74.Name = "xrTableCell74";
            this.xrTableCell74.StylePriority.UseBorders = false;
            this.xrTableCell74.Text = "25";
            this.xrTableCell74.Weight = 0.00771679522336932D;
            // 
            // xrTableCell83
            // 
            this.xrTableCell83.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell83.Name = "xrTableCell83";
            this.xrTableCell83.StylePriority.UseBorders = false;
            this.xrTableCell83.Text = "26";
            this.xrTableCell83.Weight = 0.0077166928335173929D;
            // 
            // xrTableCell54
            // 
            this.xrTableCell54.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell54.Name = "xrTableCell54";
            this.xrTableCell54.StylePriority.UseBorders = false;
            this.xrTableCell54.Text = "27";
            this.xrTableCell54.Weight = 0.0077167542674285473D;
            // 
            // xrTableCell76
            // 
            this.xrTableCell76.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell76.Name = "xrTableCell76";
            this.xrTableCell76.StylePriority.UseBorders = false;
            this.xrTableCell76.Text = "28";
            this.xrTableCell76.Weight = 0.0077168361793100871D;
            // 
            // xrlabel_Day29
            // 
            this.xrlabel_Day29.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_Day29.Name = "xrlabel_Day29";
            this.xrlabel_Day29.StylePriority.UseBorders = false;
            this.xrlabel_Day29.Text = "29";
            this.xrlabel_Day29.Weight = 0.0077166723555470093D;
            // 
            // xrlabel_Day30
            // 
            this.xrlabel_Day30.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_Day30.Name = "xrlabel_Day30";
            this.xrlabel_Day30.StylePriority.UseBorders = false;
            this.xrlabel_Day30.Text = "30";
            this.xrlabel_Day30.Weight = 0.0077167542674285482D;
            // 
            // xrlabel_Day31
            // 
            this.xrlabel_Day31.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlabel_Day31.Multiline = true;
            this.xrlabel_Day31.Name = "xrlabel_Day31";
            this.xrlabel_Day31.StylePriority.UseBorders = false;
            this.xrlabel_Day31.Text = "31";
            this.xrlabel_Day31.Weight = 0.0077167747453989318D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell5.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBorderColor = false;
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.Text = "Số ngày công";
            this.xrTableCell5.Weight = 0.2300006103515635D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell39.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell39.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.StylePriority.UseBorderColor = false;
            this.xrTableCell39.StylePriority.UseBorders = false;
            this.xrTableCell39.StylePriority.UseFont = false;
            this.xrTableCell39.Text = "Số ngày công tác";
            this.xrTableCell39.Weight = 0.23000000000000007D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell40.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell40.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.StylePriority.UseBorderColor = false;
            this.xrTableCell40.StylePriority.UseBorders = false;
            this.xrTableCell40.StylePriority.UseFont = false;
            this.xrTableCell40.Text = "Số lần trễ";
            this.xrTableCell40.Weight = 0.23000000000000007D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell41.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell41.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.xrTableCell41.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.StylePriority.UseBorderColor = false;
            this.xrTableCell41.StylePriority.UseBorders = false;
            this.xrTableCell41.StylePriority.UseFont = false;
            this.xrTableCell41.Weight = 1.045000000000001D;
            // 
            // xrTable3
            // 
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6,
            this.xrTableRow5});
            this.xrTable3.SizeF = new System.Drawing.SizeF(104.5F, 60.87508F);
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell57});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 0.14143566555447051D;
            // 
            // xrTableCell57
            // 
            this.xrTableCell57.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell57.Name = "xrTableCell57";
            this.xrTableCell57.StylePriority.UseBorders = false;
            this.xrTableCell57.Text = "NGHỈ PHÉP";
            this.xrTableCell57.Weight = 0.54000297746046511D;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell50,
            this.xrTableCell51,
            this.xrTableCell52,
            this.xrTableCell53});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 0.14960334190619798D;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.StylePriority.UseBorders = false;
            this.xrTableCell50.Text = "L";
            this.xrTableCell50.Weight = 0.1260024647640641D;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.StylePriority.UseBorders = false;
            this.xrTableCell51.Text = "R";
            this.xrTableCell51.Weight = 0.13799992675765702D;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell52.Name = "xrTableCell52";
            this.xrTableCell52.StylePriority.UseBorders = false;
            this.xrTableCell52.Text = "K";
            this.xrTableCell52.Weight = 0.13799992675765704D;
            // 
            // xrTableCell53
            // 
            this.xrTableCell53.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell53.Multiline = true;
            this.xrTableCell53.Name = "xrTableCell53";
            this.xrTableCell53.StylePriority.UseBorders = false;
            this.xrTableCell53.Text = "P";
            this.xrTableCell53.Weight = 0.13800065918108695D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell38.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell38.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.StylePriority.UseBorderColor = false;
            this.xrTableCell38.StylePriority.UseBorders = false;
            this.xrTableCell38.StylePriority.UseFont = false;
            this.xrTableCell38.Weight = 0.029999999999999888D;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblReportDate,
            this.lblSignedByName,
            this.lblCreatedByName,
            this.lblCreatedByTitle,
            this.lblSignedByTitle});
            this.ReportFooter.HeightF = 252.1667F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // lblReportDate
            // 
            this.lblReportDate.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Italic);
            this.lblReportDate.LocationFloat = new DevExpress.Utils.PointFloat(687.391F, 46.54177F);
            this.lblReportDate.Name = "lblReportDate";
            this.lblReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblReportDate.SizeF = new System.Drawing.SizeF(300F, 15F);
            this.lblReportDate.StylePriority.UseFont = false;
            this.lblReportDate.StylePriority.UseTextAlignment = false;
            this.lblReportDate.Text = "Ngày {0} tháng {1} năm {2}";
            this.lblReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblSignedByName
            // 
            this.lblSignedByName.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Italic);
            this.lblSignedByName.LocationFloat = new DevExpress.Utils.PointFloat(687.391F, 86.54176F);
            this.lblSignedByName.Name = "lblSignedByName";
            this.lblSignedByName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblSignedByName.SizeF = new System.Drawing.SizeF(300F, 10F);
            this.lblSignedByName.StylePriority.UseFont = false;
            this.lblSignedByName.StylePriority.UseTextAlignment = false;
            this.lblSignedByName.Text = "(Ký, đóng dấu)";
            this.lblSignedByName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblCreatedByName
            // 
            this.lblCreatedByName.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Italic);
            this.lblCreatedByName.LocationFloat = new DevExpress.Utils.PointFloat(108.5785F, 86.54176F);
            this.lblCreatedByName.Name = "lblCreatedByName";
            this.lblCreatedByName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCreatedByName.SizeF = new System.Drawing.SizeF(180F, 10F);
            this.lblCreatedByName.StylePriority.UseFont = false;
            this.lblCreatedByName.StylePriority.UseTextAlignment = false;
            this.lblCreatedByName.Text = "(Ký, họ tên)";
            this.lblCreatedByName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblCreatedByTitle
            // 
            this.lblCreatedByTitle.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.lblCreatedByTitle.LocationFloat = new DevExpress.Utils.PointFloat(108.5785F, 61.54176F);
            this.lblCreatedByTitle.Name = "lblCreatedByTitle";
            this.lblCreatedByTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCreatedByTitle.SizeF = new System.Drawing.SizeF(180F, 25F);
            this.lblCreatedByTitle.StylePriority.UseFont = false;
            this.lblCreatedByTitle.StylePriority.UseTextAlignment = false;
            this.lblCreatedByTitle.Text = "NGƯỜI LẬP BẢNG";
            this.lblCreatedByTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblSignedByTitle
            // 
            this.lblSignedByTitle.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.lblSignedByTitle.LocationFloat = new DevExpress.Utils.PointFloat(687.391F, 61.54176F);
            this.lblSignedByTitle.Name = "lblSignedByTitle";
            this.lblSignedByTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblSignedByTitle.SizeF = new System.Drawing.SizeF(300F, 25F);
            this.lblSignedByTitle.StylePriority.UseFont = false;
            this.lblSignedByTitle.StylePriority.UseTextAlignment = false;
            this.lblSignedByTitle.Text = "THỦ TRƯỞNG ĐƠN VỊ";
            this.lblSignedByTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // rpHRM_TimeKeepingOffice
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter});
            this.BorderColor = System.Drawing.Color.DarkGray;
            this.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(12, 12, 15, 0);
            this.PageHeight = 1169;
            this.PageWidth = 1654;
            this.PaperKind = System.Drawing.Printing.PaperKind.A3;
            this.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblReportHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}
