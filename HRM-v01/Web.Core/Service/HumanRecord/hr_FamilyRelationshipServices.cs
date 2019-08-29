using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Object.HumanRecord;

namespace Web.Core.Service.HumanRecord
{
    public class hr_FamilyRelationshipServices : BaseServices<hr_FamilyRelationship>
    {
        public static List<hr_FamilyRelationship> GetAll(int? recordId)
        {
            var condition = "1=1";
            if (recordId != null)
                condition += @" AND [RecordId]='{0}'".FormatWith(recordId);
            return GetAll(condition, null, null);
        }

        public static int GetDependenceNumber(int recordId)
        {
            var condition = "1=1";
                condition += @" AND [RecordId]='{0}'".FormatWith(recordId)+
                            " AND [IsDependent] = '1' ";
            return GetAll(condition).Count;
        }
    }
}
