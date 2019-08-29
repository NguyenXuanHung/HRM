using System.Collections.Generic;
using Web.Core.Object.HumanRecord;

namespace Web.Core.Service.HumanRecord
{
    public class hr_BankServices : BaseServices<hr_Bank>
    {
        public static List<hr_Bank> GetAll(int? recordId)
        {
            var condition = "1=1";
            if (recordId != null)
                condition += @" AND [RecordId]='{0}'".FormatWith(recordId);
            return GetAll(condition, null, null);
        }

        public static hr_Bank GetBankByRecordId(int? recordId)
        {
            var condition = "1=1";
            if (recordId != null)
                condition += @" AND [RecordId]='{0}'".FormatWith(recordId);
            condition += @" AND [IsInUsed] = 1 ";
            return GetByCondition(condition);
        }

    }
}
