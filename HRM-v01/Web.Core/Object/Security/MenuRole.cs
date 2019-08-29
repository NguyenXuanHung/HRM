namespace Web.Core.Object.Security
{
    public class MenuRole : BaseEntity
    {
        // Menu id
        public int MenuId { get; set; }
        // Role id
        public int RoleId { get; set; }
        // Permission
        public string Permission { get; set; }
    }
}
