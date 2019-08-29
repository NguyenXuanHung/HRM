using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Object.HumanRecord;

namespace Web.Core.Service.HumanRecord
{
    public class hr_InsuranceServices : BaseServices<hr_Insurance>
    {
        public static List<hr_Insurance> GetAll(int? recordId)
        {
            var condition = "1=1";
            if (recordId != null)
                condition += @" AND [RecordId]='{0}'".FormatWith(recordId);
            return GetAll(condition, null, null);
        }
    }
}
