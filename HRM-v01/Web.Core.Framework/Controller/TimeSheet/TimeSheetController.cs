using System;
using Web.Core.Framework.Common;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework
{

    /// <summary>
    /// Summary description for TimeSheetController
    /// </summary>
    public class TimeSheetController
    {
        public static TimeSheetModel GetTimeSheet(int? recordId, DateTime? startDate, DateTime? endDate)
        {
            var condition = Constant.ConditionDefault;
            if (recordId != null)
            {
                condition += " AND [RecordId] = {0}".FormatWith(recordId);
            }
            if (startDate.HasValue)
            {
                condition += " AND [StartDate] >= '{0}'".FormatWith(startDate.Value.ToString("yyyy-MM-dd"));
            }

            if (endDate.HasValue)
            {
                condition += " AND [StartDate] <= '{0}'".FormatWith(endDate.Value.AddDays(1).ToString("yyyy-MM-dd"));
            }
            var entity = hr_TimeSheetServices.GetByCondition(condition);
            return new TimeSheetModel(entity);
        }

    }
}
