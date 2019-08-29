using System;
using System.Collections.Generic;
using Web.Core.Object.Security;

namespace Web.Core.Service.Security
{
    public class UserServices : BaseServices<User>
    {
        /// <summary>
        /// Get by user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static User GetByName(string userName)
        {
            var condition = "[UserName]='{0}'".FormatWith(userName.EscapeQuote());
            return GetByCondition(condition);
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="sex"></param>
        /// <param name="isSuperUser"></param>
        /// <param name="isLocked"></param>
        /// <param name="isDeleted"></param>
        /// <param name="roleId"></param>
        /// <param name="departments"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<User> GetAll(string keyword, bool? sex,  bool? isSuperUser, bool? isLocked, bool? isDeleted, int? roleId, 
            string departments, string order, int? limit)
        {
            return GetAll(keyword, keyword, keyword, keyword, keyword, sex, isSuperUser, isLocked, isDeleted, roleId, departments, null, null);
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="displayName"></param>
        /// <param name="sex"></param>
        /// <param name="isSuperUser"></param>
        /// <param name="isLocked"></param>
        /// <param name="isDeleted"></param>
        /// <param name="roleId"></param>
        /// <param name="departments"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<User> GetAll(string userName, string email, string firstName, string lastName, string displayName, bool? sex,  
            bool? isSuperUser, bool? isLocked, bool? isDeleted, int? roleId, string departments, string order, int? limit)
        {
            // init condition
            var condition = "1=1";
            // check gender
            if (sex != null)
                condition += @" AND [Sex]='{0}'".FormatWith(sex.Value);
            // check is superuser
            if (isSuperUser != null)
                condition += @" AND [IsSuperUser]='{0}'".FormatWith(isSuperUser.Value);
            // check is locked
            if (isLocked != null)
                condition += @" AND [IsLocked]='{0}'".FormatWith(isLocked.Value);
            // check is deleted
            if (isDeleted != null)
                condition += @" AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value);
            // check role 
            if (roleId != null)
                condition += @" AND [Id] IN (SELECT [UserId] FROM [UserRole] WHERE [RoleId]='{0}')".FormatWith(roleId.Value);
            // check departments
            if (!string.IsNullOrEmpty(departments))
                condition += @" AND [Id] IN (SELECT [UserId] FROM [UserDepartment] WHERE [DepartmentId] IN ({0}))".FormatWith(departments);
            // init OR condtion
            var orCondition = string.Empty;
            // check username
            if (!string.IsNullOrEmpty(userName))
                orCondition += @"[UserName] LIKE '%{0}%' OR ".FormatWith(userName.EscapeQuote());
            // check email
            if (!string.IsNullOrEmpty(email))
                orCondition += @"[Email] LIKE '%{0}%' OR ".FormatWith(email.EscapeQuote());
            // check first name
            if (!string.IsNullOrEmpty(firstName))
                orCondition += @"[FirstName] LIKE '%{0}%' OR ".FormatWith(firstName.EscapeQuote());
            // check last name
            if (!string.IsNullOrEmpty(lastName))
                orCondition += @"[LastName] LIKE '%{0}%' OR ".FormatWith(lastName.EscapeQuote());
            // check display name
            if (!string.IsNullOrEmpty(displayName))
                orCondition += @"[DisplayName] LIKE '%{0}%' OR ".FormatWith(displayName.EscapeQuote());
            // trim end
            if (!string.IsNullOrEmpty(orCondition) && orCondition.EndsWith(" OR "))
                orCondition = " AND ({0})".FormatWith(orCondition.Substring(0, orCondition.Length - 4));
            // join condition
            if (!string.IsNullOrEmpty(orCondition))
                condition += orCondition;
            // get all
            return GetAll(condition, order, limit);
        }

        /// <summary>
        /// Get Paging
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="sex"></param>
        /// <param name="isSuperUser"></param>
        /// <param name="isLocked"></param>
        /// <param name="isDeleted"></param>
        /// <param name="roleId"></param>
        /// <param name="departments"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PageResult<User> GetPaging(string keyword, bool? sex, bool? isSuperUser, bool? isLocked, bool? isDeleted, 
            int? roleId, string departments, string order, int start, int pageSize)
        {
            return GetPaging(keyword, keyword, keyword, keyword, keyword, sex, isSuperUser, isLocked, isDeleted, roleId, departments,
                order, start, pageSize);
        }

        /// <summary>
        /// Get Paging
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="displayName"></param>
        /// <param name="sex"></param>
        /// <param name="isSuperUser"></param>
        /// <param name="isLocked"></param>
        /// <param name="isDeleted"></param>
        /// <param name="roleId"></param>
        /// <param name="departments"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PageResult<User> GetPaging(string userName, string email, string firstName, string lastName, string displayName, bool? sex,
            bool? isSuperUser, bool? isLocked, bool? isDeleted, int? roleId, string departments, string order, int start, int pageSize)
        {
            // init condition
            var condition = "1=1";
            // check gender
            if (sex != null)
                condition += @" AND [Sex]='{0}'".FormatWith(sex.Value);
            // check is superuser
            if (isSuperUser != null)
                condition += @" AND [IsSuperUser]='{0}'".FormatWith(isSuperUser.Value);
            // check is locked
            if (isLocked != null)
                condition += @" AND [IsLocked]='{0}'".FormatWith(isLocked.Value);
            // check is deleted
            if (isDeleted != null)
                condition += @" AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value);
            // check role 
            if (roleId != null)
                condition += @" AND [Id] IN (SELECT [UserId] FROM [UserRole] WHERE [RoleId]='{0}')".FormatWith(roleId.Value);
            // check departments
            if (!string.IsNullOrEmpty(departments))
                condition += @" AND [Id] IN (SELECT [UserId] FROM [UserDepartment] WHERE [DepartmentId] IN ({0}))".FormatWith(departments);
            // init OR condtion
            var orCondition = string.Empty;
            // check username
            if (!string.IsNullOrEmpty(userName))
                orCondition += @"[UserName] LIKE '%{0}%' OR ".FormatWith(userName.EscapeQuote());
            // check email
            if (!string.IsNullOrEmpty(email))
                orCondition += @"[Email] LIKE '%{0}%' OR ".FormatWith(email.EscapeQuote());
            // check first name
            if (!string.IsNullOrEmpty(firstName))
                orCondition += @"[FirstName] LIKE '%{0}%' OR ".FormatWith(firstName.EscapeQuote());
            // check last name
            if (!string.IsNullOrEmpty(lastName))
                orCondition += @"[LastName] LIKE '%{0}%' OR ".FormatWith(lastName.EscapeQuote());
            // check display name
            if (!string.IsNullOrEmpty(displayName))
                orCondition += @"[DisplayName] LIKE '%{0}%' OR ".FormatWith(displayName.EscapeQuote());
            // trim end
            if (!string.IsNullOrEmpty(orCondition) && orCondition.EndsWith(" OR "))
                orCondition = " AND ({0})".FormatWith(orCondition.Substring(0, orCondition.Length - 4));
            // join condition
            if (!string.IsNullOrEmpty(orCondition))
                condition += orCondition;
            // get all
            return GetPaging(condition, order, start, pageSize);
        }

        /// <summary>
        /// Update User, Ignore update fields that is null or empty
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="displayName"></param>
        /// <param name="image"></param>
        /// <param name="birthDate"></param>
        /// <param name="sex"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="address"></param>
        /// <param name="isSuperUser"></param>
        /// <param name="isLocked"></param>
        /// <param name="isDeleted"></param>
        /// <param name="editedBy"></param>
        /// <returns></returns>
        public static User Update(int id, string password, string email, string firstName, string lastName, string displayName, string image,
            DateTime? birthDate, bool? sex, string phoneNumber, string address, bool? isSuperUser, bool? isLocked, bool? isDeleted, string editedBy)
        {
            var user = GetById(id);
            if (user != null)
            {
                // set new properties
                if (!string.IsNullOrEmpty(password))
                    user.Password = password.ToSHA256();
                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                if (!string.IsNullOrEmpty(firstName))
                    user.FirstName = firstName;
                if (!string.IsNullOrEmpty(lastName))
                    user.LastName = lastName;
                if (!string.IsNullOrEmpty(displayName))
                    user.DisplayName = displayName;
                if (!string.IsNullOrEmpty(image))
                    user.Image = image;
                if (birthDate != null)
                    user.BirthDate = birthDate.Value;
                if (sex != null)
                    user.Sex = sex.Value;
                if (isSuperUser != null)
                    user.IsSuperUser = isSuperUser.Value;
                if (isLocked != null)
                    user.IsLocked = isLocked.Value;
                if (isDeleted != null)
                    user.IsDeleted = isDeleted.Value;
                // set edited user
                user.EditedBy = editedBy;
                user.EditedDate = DateTime.Now;
                // update user
                return Update(user);
            }
            return user;
        }

        #region Membership Methods

        /// <summary>
        /// Check user authenticate
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static User Authenticate(string userName, string password, ref string message)
        {
            // get user by name
            var user = GetByCondition("[UserName]='{0}'".FormatWith(userName.EscapeQuote()));
            // check user
            if (user == null)
            {
                // user not found
                message = @"Tên đăng nhập không tồn tại trong hệ thống.";
                return null;
            }
            // check password
            if (!user.Password.Equals(password.ToSHA256()))
            {
                // wrong password
                message = @"Sai mật khẩu.";
                return null;
            }
            // return logged in user
            return user;
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="currentPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static User ChangePassword(string userName, string currentPassword, string newPassword, ref string message)
        {
            // get user by name
            var user = GetByCondition("[UserName]='{0}'".FormatWith(userName.EscapeQuote()));
            // check user
            if (user == null)
            {
                // user not found
                message = @"Tên đăng nhập không tồn tại trong hệ thống.";
                return null;
            }
            // check current password
            if (!user.Password.Equals(currentPassword.ToSHA256()))
            {
                // wrong current password
                message = @"Sai mật khẩu hiện tại.";
                return null;
            }
            // update password
            user.Password = newPassword.ToSHA256();
            // commit change, return updated user if success
            if (Update(user) != null) return user;
            // has error
            message = @"Có lỗi xảy ra trong quá trình cập nhật.";
            // return null
            return null;
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="currentPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static User AdminChangePassword(int? userId, string newPassword, ref string message)
        {
            var condition = "1=1 ";
            if (userId != null)
            {
                condition += @" AND [Id] = {0} ".FormatWith(userId);
            }
            // get user by name
            var user = GetByCondition(condition);
            // check user
            if (user == null)
            {
                // user not found
                message = @"Tên đăng nhập không tồn tại trong hệ thống.";
                return null;
            }
            
            // update password
            user.Password = newPassword.ToSHA256();
            // commit change, return updated user if success
            if (Update(user) != null) return user;
            // has error
            message = @"Có lỗi xảy ra trong quá trình cập nhật.";
            // return null
            return null;
        }

        #endregion
    }
}
