using System;
using Web.Core.Object.TimeSheet;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for TimeSheetEventReportModel
    /// </summary>
    public class TimeSheetEventReportModel : BaseModel<hr_TimeSheetEvent>
    {
        public TimeSheetEventReportModel()
        {
            TotalActual = 0;
            // init obj
            var entity = new hr_TimeSheetEvent();

            //set pros
            Init(entity);
        }

        public TimeSheetEventReportModel(hr_TimeSheetEvent timeSheet)
        {
            var entity = timeSheet ?? new hr_TimeSheetEvent();
            Init(entity);
        }

        /// <summary>
        /// id hồ sơ
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// Họ và tên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Số hiệu CBCC
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Công quy đổi
        /// </summary>
        public double WorkConvert { get; set; }

        /// <summary>
        /// Thời gian quy đổi
        /// </summary>
        public double TimeConvert { get; set; }

        /// <summary>
        /// Thời gian thêm giờ quy đổi
        /// </summary>
        public double OverTimeConvert { get; set; }

        /// <summary>
        /// Thời gian tăng ca quy đổi
        /// </summary>
        public double OverWorkShiftConvert { get; set; }

        public TimeSheetItemModel Day1 { get; set; }
        public TimeSheetItemModel Day2 { get; set; }
        public TimeSheetItemModel Day3 { get; set; }
        public TimeSheetItemModel Day4 { get; set; }
        public TimeSheetItemModel Day5 { get; set; }
        public TimeSheetItemModel Day6 { get; set; }
        public TimeSheetItemModel Day7 { get; set; }
        public TimeSheetItemModel Day8 { get; set; }
        public TimeSheetItemModel Day9 { get; set; }
        public TimeSheetItemModel Day10 { get; set; }
        public TimeSheetItemModel Day11 { get; set; }
        public TimeSheetItemModel Day12 { get; set; }
        public TimeSheetItemModel Day13 { get; set; }
        public TimeSheetItemModel Day14 { get; set; }
        public TimeSheetItemModel Day15 { get; set; }
        public TimeSheetItemModel Day16 { get; set; }
        public TimeSheetItemModel Day17 { get; set; }
        public TimeSheetItemModel Day18 { get; set; }
        public TimeSheetItemModel Day19 { get; set; }
        public TimeSheetItemModel Day20 { get; set; }
        public TimeSheetItemModel Day21 { get; set; }
        public TimeSheetItemModel Day22 { get; set; }
        public TimeSheetItemModel Day23 { get; set; }
        public TimeSheetItemModel Day24 { get; set; }
        public TimeSheetItemModel Day25 { get; set; }
        public TimeSheetItemModel Day26 { get; set; }
        public TimeSheetItemModel Day27 { get; set; }
        public TimeSheetItemModel Day28 { get; set; }
        public TimeSheetItemModel Day29 { get; set; }
        public TimeSheetItemModel Day30 { get; set; }
        public TimeSheetItemModel Day31 { get; set; }

        /// <summary>
        /// Tổng công thực tế
        /// </summary>
        public double? TotalActual { get; set; }

        /// <summary>
        /// Tổng nghỉ lễ
        /// </summary>
        public double? TotalHolidayL { get; set; }

        /// <summary>
        /// Tổng số ngày nghỉ phép hưởng lương
        /// </summary>
        public double? TotalPaidLeaveT { get; set; }

        /// <summary>
        /// Tổng số ngày nghỉ phép không hưởng lương
        /// </summary>
        public double? TotalUnpaidLeaveP { get; set; }

        /// <summary>
        /// Tổng công nghỉ không phép
        /// </summary>
        public double? TotalUnleaveK { get; set; }

        /// <summary>
        /// Tổng số ngày đi trễ
        /// </summary>
        public double? TotalLateM { get; set; }
        /// <summary>
        /// Tổng công tác
        /// </summary>
        public double? TotalGoWorkC { get; set; }

        /// <summary>
        /// Tổng thời gian thêm giờ
        /// </summary>
        public double? TotalOverTime { get; set; }

        /// <summary>
        /// Tổng thời gian tăng ca ngày
        /// </summary>
        public double? TotalOverTimeDay { get; set; }

        /// <summary>
        /// Tổng thời gian tăng ca đêm
        /// </summary>
        public double? TotalOverTimeNight { get; set; }

        /// <summary>
        /// Tổng thời gian tăng ca ngày lễ
        /// </summary>
        public double? TotalOverTimeHoliday { get; set; }

        /// <summary>
        /// Tổng thời gian tăng ca ngày nghỉ
        /// </summary>
        public double? TotalOverTimeWeekend { get; set; }

        /// <summary>
        /// Tổng tất cả các loại tăng ca
        /// </summary>
        public double? TotalAllOverTime => TotalOverTime + TotalOverTimeDay + TotalOverTimeNight + TotalOverTimeHoliday +
                                           TotalOverTimeWeekend;
    }
}
