namespace Web.Core.Object.Security
{
    public class UserDepartment : BaseEntity
    {
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public bool IsPrimary { get; set; }
    }
}
