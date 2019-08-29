using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework
{
    public class TimeSheetEventSummaryModel
    {
        private readonly List<TimeSheetEventModel> _timeSheetEventModels;
        private readonly DateTime? _startDate;
        private readonly DateTime? _endDate;
        private double _totalActual;
        private double _totalWorkDay;
        private double _totalHolidayL;
        private double _totalPaidLeaveT;
        private double _totalUnpaidLeaveR;
        private double _totalUnleaveK;
        private double _totalLateM;
        private double _totalGoWorkC;
        private double _totalOverTime;
        private double _totalOverTimeDay;
        private double _totalOverTimeNight;
        private double _totalOverTimeHoliday;
        private double _totalOverTimeWeekend;
        private double _totalFullDay;
        private int _symbolFullDayId;

        public TimeSheetEventSummaryModel(List<TimeSheetEventModel> timeSheetEventModels, DateTime? startDate, DateTime? endDate)
        {
            _timeSheetEventModels = timeSheetEventModels;
            _startDate = startDate;
            _endDate = endDate;
            _totalActual = 0.0;
            _totalWorkDay = 0.0;
            _totalHolidayL = 0.0;
            _totalPaidLeaveT = 0.0;
            _totalUnpaidLeaveR = 0.0;
            _totalUnleaveK = 0.0;
            _totalLateM = 0.0;
            _totalGoWorkC = 0.0;
            _totalOverTime = 0.0;
            _totalOverTimeDay = 0.0;
            _totalOverTimeNight = 0.0;
            _totalOverTimeHoliday = 0.0;
            _totalOverTimeWeekend = 0.0;
            _totalFullDay = 0.0;
            _symbolFullDayId = 0;
            //get id symbol X
            var groupSymbolCondition = "[Group]='{0}'".FormatWith(Constant.TimesheetDayShift);
            var groupSymbol = hr_TimeSheetGroupSymbolServices.GetByCondition(groupSymbolCondition);
            if (groupSymbol != null)
            {
                var symbolCondition = Constant.ConditionDefault;
                symbolCondition += " AND [GroupSymbolId] = {0} AND [Code] = '{1}'".FormatWith(groupSymbol.Id, Constant.SymbolFullDay);
                //get symbol
                var symbol = hr_TimeSheetSymbolServices.GetByCondition(symbolCondition);
                if (symbol != null)
                {
                    _symbolFullDayId = symbol.Id;
                }
            }
        }

        /// <summary>
        /// id nhân viên
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<TimeSheetEventDayModel> TimeSheetEventDayModels
        {
            get
            {
                var result = new List<TimeSheetEventDayModel>();
                //check validate
                if (_startDate == null || _endDate == null) return result;
                for (var day = _startDate.Value.Date; day.Date <= _endDate.Value.Date; day = day.AddDays(1))
                {
                    var symbolDisplay = string.Empty;
                    var timeSheetEvents = _timeSheetEventModels.Where(tsem =>
                    {
                        if (!Equals(tsem.StartDate.Date, day)) return false;
                        // Tổng công
                        if (new[] { Constant.TimesheetUnLeave, Constant.TimesheetNotPaySalary }.All(x => x != tsem.GroupSymbolGroup))
                        {
                            _totalActual += tsem.WorkConvert;
                        }
                        if (_totalActual < 0)
                        {
                            _totalActual = 0;
                        }
                        switch (tsem.GroupSymbolGroup)
                        {
                            // Tổng công nghỉ phép
                            case Constant.TimesheetLeave:
                                _totalPaidLeaveT += tsem.WorkConvert;
                                break;
                            //Tong so cong khong phep
                            case Constant.TimesheetUnLeave:
                                _totalUnleaveK += tsem.WorkConvert;
                                break;
                            //Tong so cong nghi le
                            case Constant.TimesheetHoliday:
                                _totalHolidayL += tsem.WorkConvert;
                                break;
                            //Tong so cong khong luong
                            case Constant.TimesheetNotPaySalary:
                                _totalUnpaidLeaveR += tsem.WorkConvert;
                                break;
                            //Tong so cong muon
                            case Constant.TimesheetLate:
                                _totalLateM++;
                                break;
                            //Tong so cong cong tac
                            case Constant.TimesheetGoWork:
                                _totalGoWorkC += tsem.WorkConvert;
                                break;
                            //Tổng số ca ngày
                            case Constant.TimesheetDayShift:
                                _totalWorkDay += tsem.WorkConvert;
                                if (tsem.SymbolId == _symbolFullDayId)
                                {
                                    //Tổng số công một ngày công X
                                    _totalFullDay += tsem.WorkConvert;
                                }
                                break;
                            //Tổng tăng ca
                            case Constant.TimesheetOverTime:
                                _totalOverTime += tsem.TimeConvert;
                                break;
                            //Tổng tăng ca ngày
                            case Constant.TimesheetOverTimeDay:
                                _totalOverTimeDay += tsem.TimeConvert;
                                break;
                            //Tổng tăng ca đêm
                            case Constant.TimesheetOverTimeNight:
                                _totalOverTimeNight += tsem.TimeConvert;
                                break;
                            //Tổng tăng ca ngày lễ
                            case Constant.TimesheetOverTimeHoliday:
                                _totalOverTimeHoliday += tsem.TimeConvert;
                                break;
                            //Tổng tăng cuối tuần
                            case Constant.TimesheetOverTimeWeekend:
                                _totalOverTimeWeekend += tsem.TimeConvert;
                                break;
                        }
                        // get symbol display on board
                        symbolDisplay += string.IsNullOrEmpty(tsem.SymbolCode)
                            ? "<span class='badge' style='background:#FF0000'>!</span> "
                            : "<span class='badge' style='background:{0}' title='{1}'>{2}</span> ".FormatWith(tsem.SymbolColor, tsem.SymbolName, tsem.SymbolCode);
                        return true;
                    }).ToList();
                    var timeSheetEventDay = new TimeSheetEventDayModel
                    {
                        Day = day,
                        //TimeSheetEventModels = timeSheetEvents,
                        SymbolDisplay = symbolDisplay
                    };
                    result.Add(timeSheetEventDay);
                }

                return result;
            }
        }


        /// <summary>
        /// Tổng công thực tế
        /// </summary>
        public double? TotalActual => _totalActual;

        /// <summary>
        /// Tổng số ngày công
        /// </summary>
        public double? TotalWorkDay => _totalWorkDay;

        /// <summary>
        /// Tổng nghỉ lễ
        /// </summary>
        public double? TotalHolidayL => _totalHolidayL;

        /// <summary>
        /// Tổng số ngày nghỉ phép hưởng lương
        /// </summary>
        public double? TotalPaidLeaveT => _totalPaidLeaveT;

        /// <summary>
        /// Tổng số ngày nghỉ phép không hưởng lương
        /// </summary>
        public double? TotalUnpaidLeaveR => _totalUnpaidLeaveR;

        /// <summary>
        /// Tổng công nghỉ không phép
        /// </summary>
        public double? TotalUnleaveK => _totalUnleaveK;

        /// <summary>
        /// Tổng số ngày đi trễ
        /// </summary>
        public double? TotalLateM => _totalLateM;

        /// <summary>
        /// Tổng công tác
        /// </summary>
        public double? TotalGoWorkC => _totalGoWorkC;

        /// <summary>
        /// Tổng thời gian thêm giờ
        /// </summary>
        public double? TotalOverTime => _totalOverTime;

        /// <summary>
        /// Tổng thời gian tăng ca ngày
        /// </summary>
        public double? TotalOverTimeDay => _totalOverTimeDay;

        /// <summary>
        /// Tổng thời gian tăng ca đêm
        /// </summary>
        public double? TotalOverTimeNight => _totalOverTimeNight;

        /// <summary>
        /// Tổng thời gian tăng ca ngày lễ
        /// </summary>
        public double? TotalOverTimeHoliday => _totalOverTimeHoliday;

        /// <summary>
        /// Tổng thời gian tăng ca ngày nghỉ
        /// </summary>
        public double? TotalOverTimeWeekend => _totalOverTimeWeekend;

        /// <summary>
        /// Tổng tất cả các loại tăng ca
        /// </summary>
        public double? TotalAllOverTime => TotalOverTime + TotalOverTimeDay + TotalOverTimeNight + TotalOverTimeHoliday +
                                           TotalOverTimeWeekend;
        /// <summary>
        /// Tổng công một ngày công X
        /// </summary>
        public double? TotalFullDayX => _totalFullDay;
    }
}
