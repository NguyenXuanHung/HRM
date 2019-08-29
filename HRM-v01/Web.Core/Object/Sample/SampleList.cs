using System;

namespace Web.Core.Object.Sample
{
    public class SampleList: BaseEntity
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public int DepartmentId { get; set; }
        public string MA_DONVI { get; set; }

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
