using System.Collections.Generic;
using Web.Core.Object.Security;

namespace Web.Core.Service.Security
{
    public class UserRoleServices:BaseServices<UserRole>
    {
        /// <summary>
        /// Get by unique key
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static UserRole GetUnique(int userId, int roleId)
        {
            var condition = @"[UserId]='{0}' AND [RoleId]='{1}'".FormatWith(userId, roleId);
            return GetByCondition(condition);
        }

        /// <summary>
        /// Get by user or role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<UserRole> GetAll(int? roleId, int? userId)
        {
            // init condition
            var condition = "1=1";
            // check roleId
            if (roleId != null && roleId > 0)
                condition += @" AND [RoleId]='{0}'".FormatWith(roleId);
            // check userIdId
            if (userId != null && userId > 0)
                condition += @" AND [UserId]='{0}'".FormatWith(userId);
            
            return GetAll(condition, null, null);
        }

        /// <summary>
        /// Delete by user or role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool Delete(int? roleId, int? userId)
        {
            // init condition
            var condition = "1=1";
            // check roleId
            if (roleId != null && roleId > 0)
                condition += @" AND [RoleId]='{0}'".FormatWith(roleId);
            // check user id
            if (userId != null && userId > 0)
                condition += @" AND [UserId]='{0}'".FormatWith(userId);
            return Delete(condition);
        }
    }
}
