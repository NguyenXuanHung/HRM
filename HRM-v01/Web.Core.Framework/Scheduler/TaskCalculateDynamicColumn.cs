using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.HumanRecord;
using Web.Core.Object.Salary;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Salary;
using Web.Core.Service.Security;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework
{
    public class TaskCalculateDynamicColumn : BaseTask
    {
        public override void Excute(string args)
        {
            // create history
            var schedulerHistory = SchedulerHistoryServices.Create(Id, Name.ToUpper(), false, string.Empty, string.Empty, DateTime.Now, DateTime.MaxValue);
            // start process
            if (schedulerHistory == null) return;
            // log start
            SchedulerLogServices.Create(schedulerHistory.Id, "START");

            //get param
            var payrollId = !string.IsNullOrEmpty(GetArgValue("-payrollId")) ? Convert.ToInt32(GetArgValue("-payrollId")) : 0;
            var columnCode = GetArgValue("-column");
            var symbol = GetArgValue("-symbol");
            var symbolArr = symbol.Split(',');

            if (string.IsNullOrEmpty(columnCode)) return;
          
            var boardList = sal_PayrollServices.GetById(payrollId);

            //Lay danh sach nhan vien tu bang luong
            var conditionPayroll = " [SalaryBoardId] = {0} ".FormatWith(payrollId);
            var salaryInfos = hr_SalaryBoardInfoServices.GetAll(conditionPayroll);

            foreach (var salaryInfo in salaryInfos)
            {
                //Kiem tra column dong da duoc tao chua, neu chua co thi tao moi khong thi cap nhat lai
                var conditionBoardDynamic = Constant.ConditionDefault;
                    conditionBoardDynamic += " AND [RecordId] = {0} ".FormatWith(salaryInfo.RecordId)+
                                             " AND [ColumnCode] = '{0}' ".FormatWith(columnCode);
                
                var dynamic = hr_SalaryBoardDynamicColumnService.GetByCondition(conditionBoardDynamic);
                if (dynamic != null)
                {
                    // get value
                    GetValueDynamicColumn(boardList, salaryInfo, symbolArr, columnCode, dynamic);
                    //update
                    hr_SalaryBoardDynamicColumnService.Update(dynamic);
                }
                else
                {
                    //create
                    var colDynamic = new hr_SalaryBoardDynamicColumn()
                    {
                        RecordId = salaryInfo.RecordId,
                        SalaryBoardId = payrollId,
                        ColumnCode = columnCode,
                        CreatedDate = DateTime.Now
                    };
                    //set value
                    GetValueDynamicColumn(boardList, salaryInfo, symbolArr, columnCode, colDynamic);
                    //create
                    hr_SalaryBoardDynamicColumnService.Create(colDynamic);

                }
            }

            SchedulerLogServices.Create(schedulerHistory.Id, "END");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="boardList"></param>
        /// <param name="salaryInfo"></param>
        /// <param name="symbolArr"></param>
        /// <param name="columnCode"></param>
        /// <param name="dynamic"></param>
        private void GetValueDynamicColumn(sal_Payroll boardList, hr_SalaryBoardInfo salaryInfo, string[] symbolArr, string columnCode, hr_SalaryBoardDynamicColumn dynamic)
        {
            //get all event
            var listAllEvent = GetAllEvents(boardList,salaryInfo.RecordId);
            //get event by symbol
            var events = listAllEvent.Where(rc => symbolArr.Contains(rc.SymbolId.ToString())).ToList();
            //set value
            dynamic.SalaryBoardId = boardList.Id;
            dynamic.ColumnCode = columnCode;
            dynamic.Value = events.Count.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="boardList"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        private List<hr_TimeSheetEvent> GetAllEvents(sal_Payroll boardList, int recordId)
        {
            // get event by record id
            var startDate = new DateTime(boardList.Year, boardList.Month, 1);
            var endDate = startDate.AddMonths(1);
            var condition = Constant.ConditionDefault;
            condition += " AND [RecordId] = {0}".FormatWith(recordId) +
                         " AND [WorkShiftId] IN ( SELECT ws.Id FROM hr_TimeSheetWorkShift ws WHERE ws.StartDate IS NOT NULL AND ws.StartDate >= '{0}' AND [WorkShiftId] = ws.Id )"
                             .FormatWith(startDate.ToString("yyyy-MM-dd")) +
                         " AND [WorkShiftId] IN ( SELECT ws.Id FROM hr_TimeSheetWorkShift ws WHERE ws.StartDate IS NOT NULL AND ws.StartDate < '{0}' AND [WorkShiftId] = ws.Id )"
                             .FormatWith(endDate.ToString("yyyy-MM-dd")) +
                         " AND [IsDeleted] = 0 "+
                         " AND [Status] = {0} ".FormatWith((int)EventStatus.Active);

            var listEvents = hr_TimeSheetEventServices.GetAll(condition);
            return listEvents;
        }

    }
}