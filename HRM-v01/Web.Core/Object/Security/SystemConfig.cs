using System;

namespace Web.Core.Object.Security
{
    public class SystemConfig : BaseEntity
    {
        //Name
        public string Name { get; set; }
        //Value
        public string Value { get; set; }
        //DepartmentId
        public int DepartmentId { get; set; }
        //Created Date
        public DateTime? CreatedDate { get; set; }
        //Created By
        public string CreatedBy { get; set; }
        //Edited Date
        public DateTime? EditedDate { get; set; }
        //Edited By
        public string EditedBy { get; set; }
    }

}
