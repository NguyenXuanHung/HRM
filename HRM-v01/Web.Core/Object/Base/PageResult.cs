using System.Collections.Generic;

namespace Web.Core
{
    public class PageResult<T>
    {
        public int Total { get; set; }
        public List<T> Data { get; set; }

        public PageResult(int total, List<T> data)
        {
            Total = total;
            Data = data;
        }
    }
}
