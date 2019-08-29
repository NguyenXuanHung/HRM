using System.Collections.Generic;
using Web.Core.Object.Security;

namespace Web.Core.Service.Security
{
    public class RoleServices : BaseServices<Role>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isDeleted"></param>
        /// <param name="userId"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<Role> GetAll(bool? isDeleted, int? userId, string order, int? limit)
        {
            // init condition
            var condition = "1=1";
            // check IsDeleted
            if (isDeleted != null)
                condition += @" AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value);
            // check userId
            if (userId != null && userId.Value > 0)
                condition += @" AND [Role].[Id] IN (SELECT [RoleId] FROM [UserRole] WHERE [UserId]='{0}')".FormatWith(userId.Value);
            // init order
            if (string.IsNullOrEmpty(order))
                order = @"[Order]";
            // get all
            return GetAll(condition, order, limit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isDeleted"></param>
        /// <param name="userId"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public static PageResult<Role> GetPaging(bool? isDeleted, int? userId, string order, int start, int pagesize)
        {
            // init condition
            var condition = "1=1";
            // check IsDeleted
            if (isDeleted != null)
                condition += @" AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value);
            // check userId
            if (userId != null && userId.Value > 0)
                condition += @" AND [Role].[Id] IN (SELECT [RoleId] FROM [UserRole] WHERE [UserId]='{0}')".FormatWith(userId.Value);
            // init order
            if (string.IsNullOrEmpty(order))
                order = @"[Order]";
            // return
            return GetPaging(condition, order, start, pagesize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Role DeleteTemporary(int id)
        {
            var role = GetById(id);
            if (role != null)
            {
                role.IsDeleted = true;
                return Update(role);
            }
            return role;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeletePermanent(int id)
        {
            // delete menus in role
            MenuRoleServices.Delete(id, null);
            // delete users in role
            UserRoleServices.Delete(id, null);
            // delete role
            return Delete(id);
        }
    }
}
