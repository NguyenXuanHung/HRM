using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Object.Catalog;
using Web.Core.Object.Security;
using Web.Core.Service.Catalog;
using Web.Core.Service.Security;

namespace Web.Core.Framework
{
    public class UserModel
    {
        #region Ctors

        public UserModel()
        { }

        public UserModel(User user)
        {
            User = user;
        }

        #endregion

        /// <summary>
        /// Current User
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Primary department
        /// </summary>
        public cat_Department PrimaryDepartment
        {
            get
            {
                try
                {
                    // check is super user
                    if (User.IsSuperUser)
                    {
                        // return primary department
                        return cat_DepartmentServices.GetPrimary(null);
                    }
                    var department = cat_DepartmentServices.GetPrimary(User.Id);
                    // get primary department of user
                    return department ?? new cat_Department();
                }
                catch
                {
                    return new cat_Department();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public cat_Department RootDepartment
        {
            get
            {
                try
                {
                    var department = Departments.FirstOrDefault(d => d.ParentId == 0);
                    return department ?? Departments[0];
                }
                catch
                {
                    return new cat_Department();
                }
            }
        }

        /// <summary>
        /// Current departments
        /// </summary>
        public List<cat_Department> Departments
        {
            get
            {
                try
                {
                    // check is super user
                    if (User.IsSuperUser)
                    {
                        // return all departments
                        return cat_DepartmentServices.GetAll();
                    }
                    // get departments for user  
                    return cat_DepartmentServices.GetAll(null, null, null, null, false, User.Id, null, null);
                }
                catch(Exception exception)
                {
                    
                    return new List<cat_Department>();
                }
            }
        }

        /// <summary>
        /// Current departments in tree view
        /// </summary>
        public List<cat_Department> DepartmentsTree => PopulateDepartmentTree(Departments);

        /// <summary>
        /// Current roles
        /// </summary>
        public List<Role> Roles
        {
            get
            {
                try
                {
                    return User.IsSuperUser ? RoleServices.GetAll(false, null, null, null).ToList() : RoleServices.GetAll(false, User.Id, null, null);
                }
                catch
                {
                    return new List<Role>();
                }
            }
        }

        /// <summary>
        /// Current menus
        /// </summary>
        public List<MenuModel> Menus
        {
            get
            {
                try
                {
                    return User.IsSuperUser ? MenuController.GetAll(null, null, null, null, MenuStatus.Active, null, false, null, null) : MenuController.GetAll(null, null, null, null, MenuStatus.Active, User.Id, false, null, null);
                }
                catch
                {
                    return new List<MenuModel>();
                }
            }
        }

        #region Private Methods

        /// <summary>
        /// Populate list of department in treeview
        /// </summary>
        /// <param name="lstDepartments"></param>
        /// <returns></returns>
        private List<cat_Department> PopulateDepartmentTree(List<cat_Department> lstDepartments)
        {
            try
            {
                var lstDepartmentsTree = new List<cat_Department>();
                var lstRootDepartments = lstDepartments.Where(d => d.ParentId == 0).OrderBy(d => d.Order);
                var level = 0;
                foreach (var d in lstRootDepartments)
                {
                    lstDepartmentsTree.Add(d);
                    PopulateDepartmentsTreeNode(lstDepartments, d, level, ref lstDepartmentsTree);
                }
                return lstDepartmentsTree;
            }
            catch
            {
                return Departments;
            }
        }

        /// <summary>
        /// Recursive function for populating child node
        /// </summary>
        /// <param name="lstDepartments"></param>
        /// <param name="currentDepartment"></param>
        /// <param name="level"></param>
        /// <param name="departmentTree"></param>
        private void PopulateDepartmentsTreeNode(List<cat_Department> lstDepartments, cat_Department currentDepartment,
            int level, ref List<cat_Department> departmentTree)
        {
            var lstChildDepartments = lstDepartments.Where(d => d.ParentId == currentDepartment.Id).OrderBy(d => d.Order);
            level += 1;
            var prefix = string.Empty;
            for (var i = 0; i < level; i++)
                prefix += @"+---";
            foreach (var d in lstChildDepartments)
            {
                d.Name = "{0}{1}".FormatWith(prefix, d.Name);
                departmentTree.Add(d);
                PopulateDepartmentsTreeNode(lstDepartments, d, level, ref departmentTree);
            }
        }

        #endregion
    }
}
