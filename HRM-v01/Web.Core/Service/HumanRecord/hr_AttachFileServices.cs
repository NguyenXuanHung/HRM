using System.Collections.Generic;
using Web.Core.Object.HumanRecord;

namespace Web.Core.Service.HumanRecord
{
    public class hr_AttachFileServices : BaseServices<hr_AttachFile>
    {
        public static List<hr_AttachFile> GetAll(int? recordId)
        {
            var condition = "1=1";
            if (recordId != null)
                condition += @" AND [RecordId]='{0}'".FormatWith(recordId);
            return GetAll(condition, null, null);
        }
    }
}
