using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for TimeSheetLogController
    /// </summary>
    public class TimeSheetLogController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TimeSheetLogModel GetById(int id)
        {
            var record = hr_TimeSheetLogServices.GetById(id);
            return new TimeSheetLogModel(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TimeSheetLogModel Create(TimeSheetLogModel model)
        {
            var entity = new hr_TimeSheetLog();
            model.FillEntity(ref entity);

            return new TimeSheetLogModel(hr_TimeSheetLogServices.Create(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void Update(TimeSheetLogModel model)
        {
            var record = hr_TimeSheetLogServices.GetById(model.Id);
            if (record == null) return;
            //Edit data
            model.FillEntity(ref record);

            //update
            hr_TimeSheetLogServices.Update(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void Delete(int id)
        {
            hr_TimeSheetLogServices.Delete(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="timeSheetCode"></param>
        /// <param name="machineNumber"></param>
        /// <param name="location"></param>
        /// <param name="timeDate"></param>
        public static void DeleteByCondition(int? year, int? month, string timeSheetCode, string machineNumber, string location, DateTime? timeDate)
        {
            var condition = " 1=1 ";
            if (month != null)
            {
                condition += " AND MONTH([TimeDate]) = {0} ".FormatWith(month);
            }
            if (year != null)
            {
                condition += @" AND YEAR([TimeDate]) = {0} ".FormatWith(year);
            }

            if (!string.IsNullOrEmpty(timeSheetCode))
            {
                condition += " AND [TimeSheetCode] = '{0}'".FormatWith(timeSheetCode);
            }

            if (!string.IsNullOrEmpty(machineNumber))
            {
                condition += " AND [MachineSerialNumber] = '{0}'".FormatWith(machineNumber);
            }

            if (!string.IsNullOrEmpty(location))
            {
                condition += " AND [LocationName] = '{0}'".FormatWith(location);
            }

            if (timeDate.HasValue)
            {
                condition += " AND [TimeDate] IS NOT NULL AND YEAR([TimeDate]) = {0} AND MONTH([TimeDate]) = {1} AND DAY([TimeDate]) = {2}"
                    .FormatWith(timeDate.Value.Year, timeDate.Value.Month, timeDate.Value.Day);
            }
                         
            //Delete
            hr_TimeSheetLogServices.Delete(condition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="machineSerialNumber"></param>
        /// <param name="timeSheetCode"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static TimeSheetLogModel GetTimeSheetLog(string machineSerialNumber, string timeSheetCode, string order)
        {
            var condition = " 1=1 ";
            if (!string.IsNullOrEmpty(machineSerialNumber))
            {
                condition += " AND [MachineSerialNumber] = '{0}'".FormatWith(machineSerialNumber);
            }

            if (!string.IsNullOrEmpty(timeSheetCode))
            {
                condition += " AND [TimeSheetCode] = '{0}'".FormatWith(timeSheetCode);
            }

            var result = hr_TimeSheetLogServices.GetByCondition(condition, order);
            return result != null ? new TimeSheetLogModel(result) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="machineSerialNumbers"></param>
        /// <param name="timeSheetCodes"></param>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<TimeSheetLogModel> GetAll(string keyword, string machineSerialNumbers, string timeSheetCodes, DateTime? startDateTime, DateTime? endDateTime, string order, int? limit)
        {
            // init condition
            var condition = @" 1=1 ";
            if (!string.IsNullOrEmpty(keyword))
            {
                condition += " AND ( [MachineName] LIKE N'%{0}%' OR  [LocationName] LIKE N'%{0}%' OR [IPAddress] LIKE N'%{0}%') ".FormatWith(keyword);
            }

            if (!string.IsNullOrEmpty(machineSerialNumbers))
            {
                condition += " AND [MachineSerialNumber] IN ({0}) ".FormatWith(machineSerialNumbers);
            }

            if (!string.IsNullOrEmpty(timeSheetCodes))
            {
                condition += " AND [TimeSheetCode] IN ({0})".FormatWith(timeSheetCodes);
            }

            if (startDateTime.HasValue)
            {
                condition += " AND [TimeDate] IS NOT NULL AND [TimeDate] >= '{0}' "
                    .FormatWith(startDateTime.Value.ToString("yyyy-MM-dd"));
            }

            if (endDateTime.HasValue)
            {
                condition += " AND [TimeDate] IS NOT NULL AND [TimeDate] <= '{0}' "
                    .FormatWith(endDateTime.Value.AddDays(1).ToString("yyyy-MM-dd"));
            }

            return hr_TimeSheetLogServices.GetAll(condition, order, limit).Select(r => new TimeSheetLogModel(r)).ToList();
        }
        
    }
}
