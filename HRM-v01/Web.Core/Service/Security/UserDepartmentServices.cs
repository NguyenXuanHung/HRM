using System.Collections.Generic;
using Web.Core.Object.Security;

namespace Web.Core.Service.Security
{
    public class UserDepartmentServices : BaseServices<UserDepartment>
    {
        /// <summary>
        /// init object
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="departmentId"></param>
        /// <param name="isPrimary"></param>
        /// <returns></returns>
        public static UserDepartment CreateObject(int userId, int departmentId, bool isPrimary)
        {
            var obj = new UserDepartment
            {
                UserId = userId,
                DepartmentId = departmentId,
                IsPrimary = isPrimary
            };
            return obj;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public static UserDepartment GetUnique(int userId, int departmentId)
        {
            var condition = @"[UserId]='{0}' AND [DepartmentId]='{1}'".FormatWith(userId, departmentId);
            return GetByCondition(condition);
        }

        /// <summary>
        /// Get by user or department
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public static List<UserDepartment> GetAll(int? userId, int? departmentId)
        {
            // init condition
            var condition = "1=1";
            // check userIdId
            if (userId != null && userId > 0)
                condition += @" AND [UserId]='{0}'".FormatWith(userId);
            // check departmentId
            if (departmentId != null && departmentId > 0)
                condition += @" AND [DepartmentId]='{0}'".FormatWith(departmentId);
            // get all
            return GetAll(condition, null, null);
        }

        /// <summary>
        /// Delete by user or department
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public static bool Delete(int? userId, int? departmentId)
        {
            // init condition
            var condition = "1=1";
            // check user id
            if (userId != null && userId > 0)
                condition += @" AND [UserId]='{0}'".FormatWith(userId);
            // check departmentId
            if (departmentId != null && departmentId > 0)
                condition += @" AND [DepartmentId]='{0}'".FormatWith(departmentId);
            // delete
            return Delete(condition);
        }
    }
}
