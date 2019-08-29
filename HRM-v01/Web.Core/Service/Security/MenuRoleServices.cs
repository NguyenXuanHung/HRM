using System.Collections.Generic;

namespace Web.Core.Object.Security
{
    public class MenuRoleServices:BaseServices<MenuRole>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static MenuRole GetUnique(int menuId, int roleId)
        {
            var condition = @"[MenuId]='{0}' AND [RoleId]='{1}'".FormatWith(menuId, roleId);
            return GetByCondition(condition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<MenuRole> GetAll(int? roleId, int? menuId, string order, int? limit)
        {
            // init condition
            var condition = "1=1";
            // check roleId
            if (roleId != null && roleId > 0)
                condition += @" AND [RoleId]='{0}'".FormatWith(roleId);
            // check menuId
            if (menuId != null && menuId > 0)
                condition += @" AND [MenuId]='{0}'".FormatWith(menuId);
            return GetAll(condition, order, limit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static bool Delete(int? roleId, int? menuId)
        {
            // init condition
            var condition = "1=1";
            // check roleId
            if (roleId != null && roleId > 0)
                condition += @" AND [RoleId]='{0}'".FormatWith(roleId);
            // check menu id
            if (menuId != null && menuId > 0)
                condition += @" AND [MenuId]='{0}'".FormatWith(menuId);
            return Delete(condition);
        }
    }
}
