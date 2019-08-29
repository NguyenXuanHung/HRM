using System;

namespace Web.Core.Object.Security
{
    public class User : BaseEntity
    {
        #region Properties

        // Username
        public string UserName { get; set; }
        // Password
        public string Password { get; set; }
        // Email
        public string Email { get; set; }
        // First name
        public string FirstName { get; set; }
        // Last name
        public string LastName { get; set; }
        // Display name
        public string DisplayName { get; set; }
        // Image
        public string Image { get; set; }
        // Birthday
        public DateTime? BirthDate { get; set; }
        // Gender
        public bool Sex { get; set; }
        // Phone number
        public string PhoneNumber { get; set; }
        // Address
        public string Address { get; set; }
        // Is superuser
        public bool IsSuperUser { get; set; }
        // Is locked
        public bool IsLocked { get; set; }
        // Is deleted
        public bool IsDeleted { get; set; }
        // Created by
        public string CreatedBy { get; set; }
        // Created date
        public DateTime CreatedDate { get; set; }
        // Edited by
        public string EditedBy { get; set; }
        // Edited date
        public DateTime EditedDate { get; set; }

        #endregion
    }
}
