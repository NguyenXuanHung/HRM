using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Object.Catalog;

namespace Web.Core.Service.Catalog
{
    public class cat_DepartmentServices : BaseServices<cat_Department>
    {
        /// <summary>
        /// Create object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="shortName"></param>
        /// <param name="address"></param>
        /// <param name="telephone"></param>
        /// <param name="fax"></param>
        /// <param name="taxCode"></param>
        /// <param name="bankAccount"></param>
        /// <param name="director"></param>
        /// <param name="chiefAccountant"></param>
        /// <param name="parentId"></param>
        /// <param name="order"></param>
        /// <param name="type"></param>
        /// <param name="isPrimary"></param>
        /// <param name="isLocked"></param>
        /// <param name="isDeleted"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public static cat_Department CreateObject(string name, string shortName, string address, string telephone, string fax, string taxCode, string bankAccount, 
            string director, string chiefAccountant, int parentId, int order, DepartmentType type, bool isPrimary, bool isLocked, bool isDeleted, string createdBy)
        {
            var obj = new cat_Department
            {
                Name = name,
                ShortName = shortName,
                Address = address,
                Telephone = telephone,
                Fax = fax,
                TaxCode = taxCode,
                BankAccount = bankAccount,
                Director = director,
                ChiefAccountant = chiefAccountant,
                ParentId = parentId,
                Order = order,
                Type = type,
                IsPrimary = isPrimary,
                IsLocked = isLocked,
                IsDeleted = isDeleted,
                CreatedBy = createdBy,
                CreatedDate = DateTime.Now
            };
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static cat_Department GetPrimary(int? userId)
        {
            var condition = @"[IsPrimary]='1'";
            if (userId != null)
                condition = @"[Id] IN (SELECT TOP 1 [DepartmentId] FROM [UserDepartment] WHERE [UserId]='{0}' AND [IsPrimary]='1')".FormatWith(userId.Value);
            return GetByCondition(condition);
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="type"></param>
        /// <param name="isPrimary"></param>
        /// <param name="isLocked"></param>
        /// <param name="isDeleted"></param>
        /// <param name="userId"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<cat_Department> GetAll(int? parentId, DepartmentType? type, bool? isPrimary, bool? isLocked, bool? isDeleted, int? userId, string order, int? limit)
        {
            // init condition
            var condition = "1=1";
            // check ParentId
            if (parentId != null)
                condition += @" AND [ParentId]='{0}'".FormatWith(parentId);
            // check department type
            if (type != null)
                condition += @" AND [Type]='{0}'".FormatWith((int)type);
            // check IsPrimary
            if (isPrimary != null)
                condition += @" AND [IsPrimary]='{0}'".FormatWith(isPrimary.Value);
            // check IsLocked
            if (isLocked != null)
                condition += @" AND [IsLocked]='{0}'".FormatWith(isLocked.Value);
            // check IsDeleted
            if (isDeleted != null)
                condition += @" AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value);
            // check UserId
            if (userId != null)
                condition += @" AND [Id] IN (SELECT [DepartmentId] FROM [UserDepartment] WHERE [UserId]='{0}')".FormatWith(userId.Value);
            // order
            if (string.IsNullOrEmpty(order))
                order = @"[Order]";
            return GetAll(condition, order, limit);
        }

        /// <summary>
        /// Get all department in tree view
        /// </summary>
        /// <returns></returns>
        public static List<cat_Department> GetTree(int? rootId)
        {
            var lstTree = new List<cat_Department>();
            var lstDepartment = GetAll();
            if (rootId == null)
                rootId = 0;
            // init root department
            var lstRoot = lstDepartment.Where(d => d.ParentId == rootId).OrderBy(d => d.Order);
            // init level
            var level = 1;
            foreach (var department in lstRoot)
            {
                lstTree.Add(department);
                PopulateDepartmentChild(department, level, lstDepartment, ref lstTree);
            }
            return lstTree;
        }

        #region Private Methods

        /// <summary>
        /// Populate child
        /// </summary>
        /// <param name="parentDepartment"></param>
        /// <param name="level"></param>
        /// <param name="lstDepartment"></param>
        /// <param name="lstTree"></param>
        private static void PopulateDepartmentChild(cat_Department parentDepartment, int level,
            List<cat_Department> lstDepartment, ref List<cat_Department> lstTree)
        {
            var lstDepartmentChild = lstDepartment.Where(d => d.ParentId == parentDepartment.Id).OrderBy(d => d.Order);
            foreach (var department in lstDepartmentChild)
            {
                // init prefix name
                var prefixName = string.Empty;
                // generate prefix name
                for (var i = 0; i < level; i++)
                {
                    prefixName += "---";
                }
                // add prefix name into name
                department.Name = prefixName + department.Name;
                // add node into tree
                lstTree.Add(department);
                // populate child
                PopulateDepartmentChild(department, level + 1, lstDepartment, ref lstTree);
            }
        }

        #endregion

        public static cat_Department GetByDepartments(string departments)
        {
            var condition = "1=1 ";
            if (!string.IsNullOrEmpty(departments))
                condition += @"AND [Id] IN ({0}) ".FormatWith(departments);
            return GetByCondition(condition);
        }

        public static List<cat_Department> GetAllByDepartmentCode(string departmentCodes)
        {
            var condition = "1=1 ";
            if (!string.IsNullOrEmpty(departmentCodes))
                condition += @"AND [MA_DONVI] = '{0}' ".FormatWith(departmentCodes);
            return GetAll(condition);
        }

        /// <summary>
        /// Lấy danh sách các mã bộ phận con của một bộ phận
        /// </summary>
        /// <param name="parentId">Mã của bộ phận cần lấy</param>
        /// <returns>Danh sách mã các bộ phận cách nhau bởi dấu phẩy</returns>
        public static string GetAllDepartment(int parentId)
        {
            var condition = "1=1 ";
            var order = @"[Order]";
            condition += @" AND [ParentId]='{0}'".FormatWith(parentId);
            var lists = GetAll(condition, order);
            var strDepartmentId = string.Empty;
            foreach (var info in lists)
            {
                strDepartmentId += info.Id + ",";
                strDepartmentId = GetChildDepartment(strDepartmentId, info.Id);
            }
            strDepartmentId += parentId + ",";
            if (strDepartmentId.LastIndexOf(',') != -1)
                strDepartmentId = strDepartmentId.Remove(strDepartmentId.LastIndexOf(',')).Trim();
            return strDepartmentId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldStr"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private static string GetChildDepartment(string oldStr, int parentId)
        {
            var condition = "1=1 ";
            var order = @"[Order]";
            condition += @" AND [ParentId]='{0}'".FormatWith(parentId);
            var lists = GetAll(condition, order);
            foreach (var item in lists)
            {
                oldStr += item.Id + ",";
                oldStr = GetChildDepartment(oldStr, item.Id);
            }
            return oldStr;
        }

    }
}
