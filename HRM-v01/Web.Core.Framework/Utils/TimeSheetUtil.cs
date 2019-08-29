using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Framework.Common;
using Web.Core.Framework.Model;

namespace Web.Core.Framework.Utils
{
    class TimeSheetUtil
    {
        public static double CaculateTotal(List<TimeSheetEventModel> timeSheetEvents, string group, DateTime startDate, DateTime endDate)
        {
            var result = 0.0;
            for (var day = startDate.Date; day.Date <= endDate.Date; day = day.AddDays(1))
            {
                foreach (var timeSheetEvent in timeSheetEvents.Where(tse => tse.GroupSymbolGroup == group && tse.StartDate.Date == day).ToList())
                {
                    switch (group)
                    {
                        // Tổng công
                        case Constant.TimesheetDayShift:
                            result += timeSheetEvent.WorkConvert;
                            break;
                        // Tổng công nghỉ phép
                        case Constant.TimesheetLeave:
                            result += timeSheetEvent.WorkConvert;
                            break;
                        //Tong so cong khong phep
                        case Constant.TimesheetUnLeave:
                            result += timeSheetEvent.WorkConvert;
                            break;
                        //Tong so cong nghi le
                        case Constant.TimesheetHoliday:
                            result += timeSheetEvent.WorkConvert;
                            break;
                        //Tong so cong khong luong
                        case Constant.TimesheetNotPaySalary:
                            result += timeSheetEvent.WorkConvert;
                            break;
                        //Tong so cong muon
                        case Constant.TimesheetLate:
                            result++;
                            break;
                        //Tong so cong cong tac
                        case Constant.TimesheetGoWork:
                            result += timeSheetEvent.WorkConvert;
                            break;
                    }
                }
            }

            return result;
        }
    }
}
