using Web.Core.Object.Sample;

namespace Web.Core.Service.Sample
{
    public class SampleListServices : BaseServices<SampleList>
    {
        public static PageResult<SampleList> GetPaging(string keyword, bool? isDeleted, string order, int start, int pagesize)
        {
            var condition = "1=1";
            if (!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND [DepartmentId] IN ({0}) ".FormatWith(keyword);
            }
            return GetPaging(condition, null, start, pagesize);
        }
    }
}
