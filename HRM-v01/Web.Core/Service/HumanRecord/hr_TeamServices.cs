using System.Collections.Generic;
using Web.Core.Object.HumanRecord;

namespace Web.Core.Service.HumanRecord
{
    public class hr_TeamServices : BaseServices<hr_Team>
    {
        public static List<hr_Team> GetAll(int? recordId)
        {
            var condition = "1=1";
            if (recordId != null)
                condition += @" AND [RecordId]='{0}'".FormatWith(recordId);
            return GetAll(condition, null, null);
        }

        public static hr_Team GetByRecordId(int? recordId)
        {
            var condition = "1=1";
            if (recordId != null)
                condition += @" AND [RecordId]='{0}'".FormatWith(recordId);
            return GetByCondition(condition);
        }
    }
}
