using Web.Core.Framework.Report;

namespace Web.Core.Framework.Interface
{
    public interface IBaseReport
    {
        Filter GetFilter();
        void SetFilter(Filter filter);
        void BindData();
    }
}
