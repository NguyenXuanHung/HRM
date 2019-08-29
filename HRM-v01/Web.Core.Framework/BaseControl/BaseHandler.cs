using Web.Core.Framework.Common;

namespace Web.Core.Framework.BaseControl
{
    public class BaseHandler
    {
        public string Keyword = "";
        public string DepartmentIds = "";
        public string Filter = "";
        public string Order = "";
        public int Start = Constant.DefaultStart;
        public int Limit = Constant.DefaultPagesize;
    }
}
