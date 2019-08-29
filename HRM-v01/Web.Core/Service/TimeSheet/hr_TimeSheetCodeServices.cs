using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.Catalog;

namespace Web.Core.Service.TimeSheet
{
    public class hr_TimeSheetCodeServices : BaseServices<hr_TimeSheetCode>
    {
        
        public static hr_TimeSheetCode GetTimeSheetCodeByRecordId(int recordId)
        {
            var condition = "1=1";
            condition += @" AND [RecordId] = '{0}' ".FormatWith(recordId) +
                " AND [IsActive] = 1 " ;
            return GetByCondition(condition);
        }
    }
}
