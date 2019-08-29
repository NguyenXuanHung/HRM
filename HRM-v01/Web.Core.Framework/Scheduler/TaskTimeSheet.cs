using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Framework.Controller.TimeSheet;
using Web.Core.Framework.Model;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.Security;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework
{
    public class TaskTimeSheet : BaseTask
    {
        private int _groupSymbolUndefinedId = 0;
        private int _symbolUndefinedId = 0;
        private int _groupSymbolUnleaveId = 0;
        private int _symbolUnleaveId = 0;
        private string _symbolUnleaveDescription = "";
        private double _symbolUnleaveWorkConvert = 0;
        public override void Excute(string args)
        {
            // create history
            var schedulerHistory = SchedulerHistoryServices.Create(Id, Name.ToUpper(), false, string.Empty, string.Empty, DateTime.Now, DateTime.MaxValue);
            // start process
            if (schedulerHistory == null) return;
            // log start
            SchedulerLogServices.Create(schedulerHistory.Id, "START");

            // Chạy 0:05 ngày 1 hàng tháng            
            var month = !string.IsNullOrEmpty(GetArgValue("-m")) ? Convert.ToInt32(GetArgValue("-m")) : DateTime.Now.Month;
            var year = !string.IsNullOrEmpty(GetArgValue("-y")) ? Convert.ToInt32(GetArgValue("-y")) : DateTime.Now.Year;
            var groupWorkShiftId = !string.IsNullOrEmpty(GetArgValue("-groupWorkShiftId")) ? Convert.ToInt32(GetArgValue("-groupWorkShiftId")) : 0;

            //get all detail workShift
            var order = " [StartDate] ASC ";
            var lstWorkShift =
                TimeSheetWorkShiftController.GetAll(null, false, groupWorkShiftId, null, null, null, null, month, year, order, null);
            var listWorkShiftIds = lstWorkShift.Select(ws => ws.Id).ToList();
            if (listWorkShiftIds.Count > 0)
            {
                TimeSheetEventController.DeleteByCondition(listWorkShiftIds, null, null, null, TimeSheetAdjustmentType.Default);
            }

            //get all employee in group
            var listEmployeeGroupWorkShift =
                TimeSheetEmployeeGroupWorkShiftController.GetAll(null, null, null, groupWorkShiftId, null, null);
            //list recordId
            var listRecordIds = listEmployeeGroupWorkShift.Select(gws => gws.RecordId).Distinct().ToList();

            //get symbolId undefined
            GetSymbolUndefined();

            //get symbolId unleave
            GetSymbolUnLeave();

            //create new timeSheet for employee
            foreach (var recordId in listRecordIds)
            {
                //get timeSheetCode by recordId
                var timeSheetCodeCondition = " [RecordId] = {0} ".FormatWith(recordId)+
                                             " AND [IsActive] = 1 ";
                var listTimeSheetCodes = hr_TimeSheetCodeServices.GetAll(timeSheetCodeCondition);
                if (listTimeSheetCodes.Count > 0)
                {
                    //ma cham cong
                    var timeSheetCodes = string.Join(",", listTimeSheetCodes.Select(t => t.Code).ToList());
                    var arrTimeSheetCode = FormatList(timeSheetCodes);
                   
                    //serial may cham cong
                    var machineIds = listTimeSheetCodes.Select(tc => tc.MachineId).ToList();
                    var machineCondition = Constant.ConditionDefault;
                    machineCondition += " AND [Id] IN ({0})".FormatWith(string.Join(",", machineIds));
                    var machines = hr_TimeSheetMachineService.GetAll(machineCondition);
                    var serialNumbers =
                        string.Join(",", machines.Select(t => t.SerialNumber).ToList());
                    var arrSerialNumber = FormatList(serialNumbers);

                    foreach (var workShiftModel in lstWorkShift)
                    {
                        //calculate timeSheet
                        CalculateTimeSheet(workShiftModel, recordId, string.Join(",", arrTimeSheetCode), string.Join(",", arrSerialNumber));
                    }
                }
            }

            SchedulerLogServices.Create(schedulerHistory.Id, "END");
        }

        /// <summary>
        /// get symbolId undefined
        /// </summary>
        private void GetSymbolUndefined()
        {
            var groupSymbolCondition = "[Group]='{0}'".FormatWith(Constant.TimesheetUndefined);
            var groupSymbol = hr_TimeSheetGroupSymbolServices.GetByCondition(groupSymbolCondition);
            if (groupSymbol != null)
            {
                _groupSymbolUndefinedId = groupSymbol.Id;
                var symbolCondition = Constant.ConditionDefault;
                symbolCondition += " AND [GroupSymbolId] = {0}".FormatWith(groupSymbol.Id);
                //get symbol
                var symbol = hr_TimeSheetSymbolServices.GetByCondition(symbolCondition);
                if (symbol != null)
                {
                    _symbolUndefinedId = symbol.Id;
                }
            }
        }

        /// <summary>
        /// get symbolId unleave
        /// </summary>
        private void GetSymbolUnLeave()
        {
            var condition = "[Group]='{0}'".FormatWith(Constant.TimesheetUnLeave);
            var groupSymbol = hr_TimeSheetGroupSymbolServices.GetByCondition(condition);
            if (groupSymbol != null)
            {
                _groupSymbolUnleaveId = groupSymbol.Id;
                var symbolCondition = Constant.ConditionDefault;
                symbolCondition += " AND [GroupSymbolId] = {0}".FormatWith(groupSymbol.Id);
                //get symbol
                var symbol = hr_TimeSheetSymbolServices.GetByCondition(symbolCondition);
                if (symbol != null)
                {
                    _symbolUnleaveId = symbol.Id;
                    _symbolUnleaveDescription = symbol.Description;
                    _symbolUnleaveWorkConvert = symbol.WorkConvert;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        private static string[] FormatList(string codes)
        {
            var arrCode = string.IsNullOrEmpty(codes)
                ? new string[] { }
                : codes.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < arrCode.Length; i++)
            {
                arrCode[i] = "'{0}'".FormatWith(arrCode[i]);
            }

            return arrCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workShiftModel"></param>
        /// <param name="recordId"></param>
        /// <param name="timeSheetCodes"></param>
        /// <param name="serialNumbers"></param>
        private void CalculateTimeSheet(TimeSheetWorkShiftModel workShiftModel, int recordId, string timeSheetCodes,
            string serialNumbers)
        {
            //get logs
            var condition = Constant.ConditionDefault;
            if (!string.IsNullOrEmpty(serialNumbers))
            {
                condition += " AND [MachineSerialNumber] IN ({0}) ".FormatWith(serialNumbers);
            }
            if (!string.IsNullOrEmpty(timeSheetCodes))
            {
                condition += " AND [TimeSheetCode] IN ({0})".FormatWith(timeSheetCodes);
            }
            condition += " AND [TimeDate] IS NOT NULL AND [TimeDate] >= '{0}' "
                .FormatWith(workShiftModel.StartDate.ToString("yyyy-MM-dd"));
        
            condition += " AND [TimeDate] IS NOT NULL AND [TimeDate] <= '{0}' "
                .FormatWith(workShiftModel.EndDate.AddDays(1).ToString("yyyy-MM-dd"));
            
            var order = " [TimeDate] ASC ";
            //get logs foreach timeSheetWorkShift
            var listLogs = hr_TimeSheetLogServices.GetAll(condition, order);
              
            //case has log
            if (listLogs.Count > 0)
            {
                // Update logging time
                UpdateTimeLogs(recordId, workShiftModel.StartDate, workShiftModel.EndDate, listLogs);
                
                // Validate logging time
                if (listLogs.Count > 1)
                {
                    //get log between StartInTime and EndOutTime
                    var timeLogs = listLogs.Where(d => d.TimeDate >= workShiftModel.StartInTime && d.TimeDate <= workShiftModel.EndOutTime).ToList();

                    if (timeLogs.Count > 1)
                    {
                        //normalize data logs
                        var timeStandardLogs = new List<hr_TimeSheetLog>();
                        for (var i = 0; i < timeLogs.Count - 1; i++)
                        {
                            if (timeLogs[i].TimeDate.AddSeconds(30) < timeLogs[i + 1].TimeDate)
                            {
                                timeStandardLogs.Add(timeLogs[i]);
                            }
                        }
                        timeStandardLogs.Add(timeLogs[timeLogs.Count - 1]);

                        //get list rule wrong time
                        var sort = " [Order] ASC ";
                        var lstTimeRules = TimeSheetRuleWrongTimeController.GetAll(null, null, null, false, sort, null);

                        //get list check
                        GetListCheckValidation(workShiftModel, timeStandardLogs, out var listInTime, out var listOutTime, out var listLate, out var listEarly);
                        //hasInOut
                        if (workShiftModel.HasInOutTime)
                        {
                            //TH du lieu log le
                            if (timeStandardLogs.Count == 0 || timeStandardLogs.Count % 2 != 0)
                            {
                                //TH không phải là tăng ca
                                if (!IsOverTime(workShiftModel))
                                {
                                    //undefined
                                    CreateTimeSheetEvent(recordId, workShiftModel.Id, _groupSymbolUndefinedId,
                                        _symbolUndefinedId, 0, 0, Constant.Undefined);
                                }
                            }
                            else
                            {
                                //TH du lieu log chan
                                var timeActual = 0.0;
                                var timeLimit = workShiftModel.TimeConvert;
                                var totalWorkTime = 0.0;
                                //Tinh thoi lam viec thuc te trong ngay
                                for (var i = 0; i < timeStandardLogs.Count - 1; i++)
                                {
                                    totalWorkTime += timeStandardLogs[i + 1].TimeDate
                                        .Subtract(timeStandardLogs[i].TimeDate).TotalMinutes;
                                }
                                
                                //check maxTime
                                if (workShiftModel.HasLimitTime)
                                {
                                    timeActual = totalWorkTime > timeLimit ? timeLimit : Math.Round(totalWorkTime, 2);
                                }
                                else
                                {
                                    timeActual = Math.Round(totalWorkTime, 2);
                                }

                                //process
                                MainProcess(recordId, workShiftModel, listInTime, listOutTime, listLate, listEarly, lstTimeRules, timeActual);
                            }


                        }
                        else
                        {
                            //only process start log and end log
                            var timeActual = 0.0;
                            var timeConvert = workShiftModel.TimeConvert;
                            var timeLimit = workShiftModel.TimeConvert;
                            
                            //Check trong khoang cham cong
                            if (listInTime.Count > 0 && listOutTime.Count > 0)
                            {
                                var firstLog = listInTime.FirstOrDefault();
                                var lastLog = listOutTime.LastOrDefault();
                                var startTime = firstLog.TimeDate;
                                var endTime = lastLog.TimeDate;
                                //cap nhat thoi gian lam viec
                                timeConvert = Math.Round(endTime.Subtract(startTime).TotalMinutes, 2);
                            }

                            //check maxTime
                            if (workShiftModel.HasLimitTime)
                            {
                                timeActual = timeConvert > timeLimit ? timeLimit : Math.Round(timeConvert, 2);
                            }
                            else
                            {
                                timeActual = Math.Round(timeConvert, 2);
                            }

                            //process
                            MainProcess(recordId, workShiftModel, listInTime, listOutTime, listLate, listEarly, lstTimeRules, timeActual);
                        }
                    }
                    else
                    {
                        //TH không phải là tăng ca
                        
                        if (!IsOverTime(workShiftModel))
                        {
                            //data invalid undefined
                            CreateTimeSheetEvent(recordId, workShiftModel.Id, _groupSymbolUndefinedId, _symbolUndefinedId,
                                0, 0, Constant.UndefinedInvalid);
                        } 
                    }
                }
                else
                {
                    //TH không phải là tăng ca
                    if (listLogs.Count == 1)
                    {
                        if (!IsOverTime(workShiftModel))
                        {
                            //data invalid undefined
                            CreateTimeSheetEvent(recordId, workShiftModel.Id, _groupSymbolUndefinedId, _symbolUndefinedId,
                                0, 0, Constant.UndefinedInvalid);
                        }
                    }
                }
            }
            else
            {
                if (workShiftModel.StartDate < DateTime.Now && workShiftModel.SymbolCode != Constant.SymbolOverTime)
                {
                    //TH un leaved
                    CreateTimeSheetEvent(recordId, workShiftModel.Id, _groupSymbolUnleaveId, _symbolUnleaveId,
                        _symbolUnleaveWorkConvert, 0, _symbolUnleaveDescription);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="listLogs"></param>
        private void UpdateTimeLogs(int recordId, DateTime startDate, DateTime endDate, List<hr_TimeSheetLog> listLogs)
        {
            //Check if not save log into DB => create else update
            var condition = Constant.ConditionDefault;
            condition += " AND [RecordId] = {0}".FormatWith(recordId) +
                         " AND [StartDate] >= '{0}'".FormatWith(startDate.ToString("yyyy-MM-dd")) +
                         " AND [StartDate] <= '{0}'".FormatWith(endDate.AddDays(1).ToString("yyyy-MM-dd"));
            
            var checkTimeSheet = hr_TimeSheetServices.GetByCondition(condition);
            var timeLogs = string.Join(" | ", listLogs.Select(d => d.TimeDate.ToString("HH:mm:ss")).ToArray());
            if (checkTimeSheet == null)
            {
                var timeSheet = new hr_TimeSheet()
                {
                    RecordId = recordId,
                    StartDate = startDate,
                    EndDate = endDate,
                    CreatedDate = DateTime.Now,
                    TimeLogs = timeLogs
                };
                hr_TimeSheetServices.Create(timeSheet);
            }
            else
            {
                checkTimeSheet.RecordId = recordId;
                checkTimeSheet.TimeLogs = timeLogs;
                checkTimeSheet.StartDate = startDate;
                checkTimeSheet.EndDate = endDate;
                checkTimeSheet.EditedDate = DateTime.Now;
                hr_TimeSheetServices.Update(checkTimeSheet);
            }
        }

        /// <summary>
        /// check overtime
        /// </summary>
        /// <param name="workShiftModel"></param>
        /// <returns></returns>
        private static bool IsOverTime(TimeSheetWorkShiftModel workShiftModel)
        {
            var groupSymbol = TimeSheetGroupSymbolController.GetById(workShiftModel.GroupSymbolId);
            return groupSymbol != null && (groupSymbol.Group == Constant.TimesheetOverTimeDay
                                           || groupSymbol.Group == Constant.TimesheetOverTimeNight
                                           || groupSymbol.Group == Constant.TimesheetOverTimeWeekend
                                           || groupSymbol.Group == Constant.TimesheetOverTimeHoliday);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="workShiftModel"></param>
        /// <param name="listInTime"></param>
        /// <param name="listOutTime"></param>
        /// <param name="listLate"></param>
        /// <param name="listEarly"></param>
        /// <param name="lstTimeRules"></param>
        /// <param name="timeConvert"></param>
        private void MainProcess(int recordId, TimeSheetWorkShiftModel workShiftModel, List<hr_TimeSheetLog> listInTime,
            List<hr_TimeSheetLog> listOutTime, List<hr_TimeSheetLog> listLate, List<hr_TimeSheetLog> listEarly, List<TimeSheetRuleWrongTimeModel> lstTimeRules, double timeConvert)
        {
            //Check trong khoang cham cong
            if (listInTime.Count > 0 && listOutTime.Count > 0)
            {
                var firstLog = listInTime.FirstOrDefault();
                var lastLog = listOutTime.LastOrDefault();
                var startTime = firstLog.TimeDate;
                var endTime = lastLog.TimeDate;

                //TH giới hạn thời gian làm việc tối đa
                if (workShiftModel.HasLimitTime)
                {
                    if (DateTime.Compare(startTime, workShiftModel.StartDate) <= 0)
                    {
                        startTime = workShiftModel.StartDate;
                    }

                    if (DateTime.Compare(endTime, workShiftModel.EndDate) >= 0)
                    {
                        endTime = workShiftModel.EndDate;
                    }
                }

                //update tang ca
                if (IsOverTime(workShiftModel))
                {
                        //Thoi gian tang ca
                        var overWorkShiftConvert = Math.Round(endTime.Subtract(startTime).TotalMinutes, 2);
                    //create event
                    CreateTimeSheetEvent(recordId, workShiftModel.Id, workShiftModel.GroupSymbolId, workShiftModel.SymbolId, 0, overWorkShiftConvert, workShiftModel.SymbolName);
                }
                else
                {
                    //TH di lam dung gio
                    CreateTimeSheetEvent(recordId, workShiftModel.Id, workShiftModel.GroupSymbolId, workShiftModel.SymbolId, workShiftModel.WorkConvert, timeConvert, workShiftModel.SymbolName);

                    if (workShiftModel.HasOverTime)
                    {
                        //Thêm giờ
                        ProcessOverTime(recordId, workShiftModel, timeConvert);
                    }

                    //check come late && leave early
                    if (listLate.Count > 0 && listEarly.Count > 0)
                    {
                        //check late
                        CheckComeLate(recordId, listLate, lstTimeRules, workShiftModel);

                        //check early
                        CheckLeaveEarly(recordId, listEarly, lstTimeRules, workShiftModel);
                    }
                    else
                    {
                        if (listLate.Count > 0)
                        {
                            //check late
                            CheckComeLate(recordId, listLate, lstTimeRules, workShiftModel);
                        }

                        if (listEarly.Count > 0)
                        {
                            //check early
                            CheckLeaveEarly(recordId, listEarly, lstTimeRules, workShiftModel);
                        }
                    }
                }
            }
            else
            {
                //undefined
                CreateTimeSheetEvent(recordId, workShiftModel.Id, _groupSymbolUndefinedId,
                    _symbolUndefinedId, 0, 0, Constant.Undefined);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="workShiftModel"></param>
        /// <param name="timeConvert"></param>
        private void ProcessOverTime(int recordId, TimeSheetWorkShiftModel workShiftModel, double timeConvert)
        {
           var overTimeConvert = Math.Round((timeConvert - workShiftModel.TimeConvert), 2);

            //Add moi timeSheetEvent
            var groupSymbolCondition = "[Group]='{0}'".FormatWith(Constant.TimesheetOverTime);
            var groupSymbol = hr_TimeSheetGroupSymbolServices.GetByCondition(groupSymbolCondition);
            if (groupSymbol != null)
            {
                var symbol = TimeSheetSymbolController.GetByCondition(null, groupSymbol.Id);
                if (symbol != null)
                {
                    CreateTimeSheetEvent(recordId, workShiftModel.Id, groupSymbol.Id, symbol.Id, 0, overTimeConvert, symbol.Name);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="listLate"></param>
        /// <param name="lstTimeRules"></param>
        /// <param name="workShiftModel"></param>
        private void CheckComeLate(int recordId, List<hr_TimeSheetLog> listLate, List<TimeSheetRuleWrongTimeModel> lstTimeRules, TimeSheetWorkShiftModel workShiftModel)
        {
            var firstLate = listLate.FirstOrDefault();
            var totalMinuteOriginal = firstLate.TimeDate.Subtract(workShiftModel.StartDate).TotalMinutes;
            if (totalMinuteOriginal > 0)
            {
                var totalMinute = Math.Round(totalMinuteOriginal, 2);
                if (lstTimeRules.Count > 0)
                {
                    //Lay danh sach rule di muon
                    var lstRuleLate = lstTimeRules.Where(d => d.Type == TimeSheetRuleWrongTimeType.ComeLate);
                    foreach (var item in lstRuleLate)
                    {
                        if (totalMinute > Convert.ToDouble(item.FromMinute) && totalMinute < Convert.ToDouble(item.ToMinute))
                        {
                            //Add moi timeSheetEvent
                            CreateTimeSheetEvent(recordId,workShiftModel.Id, item.GroupSymbolId, item.SymbolId,item.WorkConvert, totalMinute, item.SymbolName);
                            break;
                        }
                    }  
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="listEarly"></param>
        /// <param name="lstTimeRules"></param>
        /// <param name="workShiftModel"></param>
        private void CheckLeaveEarly(int recordId, List<hr_TimeSheetLog> listEarly, List<TimeSheetRuleWrongTimeModel> lstTimeRules, TimeSheetWorkShiftModel workShiftModel)
        {
            var firstEarly = listEarly.FirstOrDefault();
            var totalMinuteOriginal = workShiftModel.EndDate.Subtract(firstEarly.TimeDate).TotalMinutes;
            if (totalMinuteOriginal > 0)
            {
                var totalMinute = Math.Round(totalMinuteOriginal, 2);
                if (lstTimeRules.Count > 0)
                {
                    //Lay danh sach rule ve som
                    var lstRuleEarly = lstTimeRules.Where(d => d.Type == TimeSheetRuleWrongTimeType.LeaveEarly);
                    foreach (var item in lstRuleEarly)
                    {
                        if (totalMinute > Convert.ToDouble(item.FromMinute) && totalMinute < Convert.ToDouble(item.ToMinute))
                        {
                            //Add moi timeSheetEvent
                            CreateTimeSheetEvent(recordId, workShiftModel.Id, item.GroupSymbolId, item.SymbolId, item.WorkConvert, totalMinute, item.SymbolName);
                            break;
                        }
                    }  
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="workShiftModel"></param>
        /// <param name="timeStandardLogs"></param>
        /// <param name="listInTime"></param>
        /// <param name="listOutTime"></param>
        /// <param name="listLate"></param>
        /// <param name="listEarly"></param>
        private static void GetListCheckValidation(TimeSheetWorkShiftModel workShiftModel, List<hr_TimeSheetLog> timeStandardLogs, out List<hr_TimeSheetLog> listInTime, out List<hr_TimeSheetLog> listOutTime, out List<hr_TimeSheetLog> listLate, out List<hr_TimeSheetLog> listEarly)
        {
            listInTime = timeStandardLogs.Where(d => d.TimeDate >= workShiftModel.StartInTime &&
                                                     d.TimeDate <= workShiftModel.EndInTime).ToList();
            listOutTime = timeStandardLogs.Where(d => d.TimeDate >= workShiftModel.StartOutTime &&
                                                      d.TimeDate <= workShiftModel.EndOutTime ).ToList();
            listLate = timeStandardLogs.Where(d => d.TimeDate >= workShiftModel.StartInTime &&
                                                   d.TimeDate <= workShiftModel.EndInTime ).ToList();
            listEarly = timeStandardLogs.Where(d => d.TimeDate > workShiftModel.StartOutTime &&
                                                    d.TimeDate < workShiftModel.EndDate).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="workShiftId"></param>
        /// <param name="groupSymbolId"></param>
        /// <param name="symbolId"></param>
        /// <param name="description"></param>
        /// <param name="workConvert"></param>
        /// <param name="timeConvert"></param>
        private void CreateTimeSheetEvent(int recordId, int workShiftId, int groupSymbolId, int symbolId, double workConvert, double timeConvert, string description)
        {
            var timeSheetEvent = new hr_TimeSheetEvent()
            {
                RecordId = recordId,
                WorkShiftId = workShiftId,
                Description = description,
                GroupSymbolId = groupSymbolId,
                SymbolId = symbolId,
                WorkConvert = workConvert,
                TimeConvert = timeConvert,
                Status = EventStatus.Active,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                EditedDate = DateTime.Now,
            };

            //Create
            hr_TimeSheetEventServices.Create(timeSheetEvent);
        }

    }
}