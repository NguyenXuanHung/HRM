using System.Collections.Generic;
using Web.Core.Object.HumanRecord;

namespace Web.Core.Service.HumanRecord
{
    public class hr_ContractServices : BaseServices<hr_Contract>
    {
        public static List<hr_Contract> GetAll(int? recordId)
        {
            var condition = "1=1";
            if (recordId != null)
                condition += @" AND [RecordId]='{0}'".FormatWith(recordId);
            return GetAll(condition, null, null);
        }
    }
}
