using System;
using System.Collections.Generic;
using System.Linq;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Object.Catalog;
using Web.Core.Object.Security;
using Web.Core.Service.Catalog;
using Web.Core.Service.Security;

namespace Web.HRM.Modules.Setting
{
    public partial class UserManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!ExtNet.IsAjaxRequest)
            {
                // init setting permission
                toolbarSeparatorSetting.Visible = CurrentPermission.CanWrite;
                btnSetting.Visible = CurrentPermission.CanWrite;

                // init current departments
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id).ToArray());
                
                // load role filter
                LoadRolesForFilterTree();



                // get all avaiable roles
                var roles = RoleServices.GetAll(false, null, null, null);
                // load roles for tree panel left
                //LoadRolesForTreePanel(roles);
                // load roles for window assign role
                //LoadRolesForAssignWindow(roles);
                // load departments for window assign department
                LoadDepartmentsForAssignWindow(CurrentUser.Departments);
            }

            chooseEmployee.AfterClickAcceptButton += ucChooseEmployee1_AfterClickAcceptButton;
        }

        #region Event Methods

        /// <summary>
        /// Init setting window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindowUserSetting(object sender, DirectEventArgs e)
        {
            // init id
            var param = e.ExtraParams["Id"];
            // parse id
            if(int.TryParse(param, out var id))
            {
                // init window props
                if(id > 0)
                {
                    // edit
                    wdSetting.Title = @"Sửa";
                    wdSetting.Icon = Icon.UserEdit;
                }
                else
                {
                    // insert
                    wdSetting.Title = @"Thêm mới";
                    wdSetting.Icon = Icon.UserAdd;
                }
                // init id
                hdfUserId.Text = id.ToString();
                // init object
                var entity = new User();
                // check id
                if(id > 0)
                {
                    var result = UserServices.GetById(id);
                    if(result != null)
                        entity = result;
                }
                // check entity id
                if (entity.Id > 0)
                {
                    // account info
                    txtFirstName.Text = entity.FirstName;
                    txtLastName.Text = entity.LastName;
                    txtDisplayName.Text = entity.DisplayName;
                    txtEmail.Text = entity.Email;
                    txtAddress.Text = entity.Address;
                    txtPhoneNumber.Text = entity.PhoneNumber;
                    if (entity.BirthDate.HasValue)
                        dfBirthday.SelectedDate = entity.BirthDate.Value;
                    if (entity.Sex)
                        rdMale.Checked = true;
                    else
                        rdFemale.Checked = true;
                    // disable some field
                    txtUserName.Disabled = true;
                    txtPassword.Disabled = true;
                    txtConfirmPassword.Disabled = true;
                    cboDepartment.Disabled = true;
                }
                // show window
                wdSetting.Show();
            }
        }

        /// <summary>
        /// Insert or Update user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdateUser(object sender, DirectEventArgs e)
        {
            // check user exist
            var findUser = UserServices.GetByName(txtUserName.Text);
            if(findUser != null)
            {
                // user existed
                ExtNet.MessageBox.Alert("Lỗi tên đăng nhập", "Tài khoản này đã tồn tại trong hệ thống").Show();
                return;
            }
            // init user
            var user = new User();
            // check id
            if(!string.IsNullOrEmpty(hdfUserId.Text) && Convert.ToInt32(hdfUserId.Text) > 0)
            {
                var result = UserServices.GetById(Convert.ToInt32(hdfUserId.Text));
                if(result != null)
                    user = result;
            }
            // set user props with form value
            user.UserName = txtUserName.Text;
            user.Password = txtPassword.Text.ToSHA256();
            user.Email = txtEmail.Text;
            user.FirstName = txtFirstName.Text;
            user.LastName = txtLastName.Text;
            user.DisplayName = txtDisplayName.Text.Trim();
            user.Sex = rdMale.Checked;
            user.PhoneNumber = txtPhoneNumber.Text;
            user.Address = txtAddress.Text;
            // set date of birth
            if(dfBirthday.SelectedDate.Year != 1)
                user.BirthDate = dfBirthday.SelectedDate;
            // set user props with default value
            user.IsSuperUser = false;
            user.IsLocked = false;
            user.IsDeleted = false;
            user.CreatedBy = CurrentUser.User.UserName;
            user.CreatedDate = DateTime.Now;
            user.EditedBy = CurrentUser.User.UserName;
            user.EditedDate = DateTime.Now;
            // insert or update user
            user = user.Id > 0 ? UserServices.Update(user) : UserServices.Create(user);
            // add user department
            if (!string.IsNullOrEmpty(hdfDepartmentForEditingUser.Text) && Convert.ToInt32(hdfDepartmentForEditingUser.Text) > 0)
            UserDepartmentServices.Create(new UserDepartment
            {
                DepartmentId = Convert.ToInt32(hdfDepartmentForEditingUser.Text),
                UserId = user.Id,
                IsPrimary = true
            });
            // hide window
            wdSetting.Hide();
            // reload data
            gpUser.Reload();
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChangePassword(object sender, DirectEventArgs e)
        {
            var message = string.Empty;
            var user = UserServices.AdminChangePassword(int.Parse(hdfUserId.Text), txtNewPassword.Text, ref message);
            if(user != null)
            {
                wdChangePassword.Hide();
                Dialog.ShowNotification("Đổi mật khẩu thành công !");
            }
        }

        /// <summary>
        /// Change status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChangeStatus(object sender, DirectEventArgs e)
        {
            // init param
            var paramId = e.ExtraParams["Id"];
            var paramStatus = e.ExtraParams["Status"];
            // parse id
            if(!int.TryParse(paramId, out var id) || id <= 0)
            {
                // parse error, show error
                Dialog.ShowError("Có lỗi xảy ra trong quá trình xử lý");
                return;
            }
            // udate locked
            var user = UserServices.GetById(id);
            user.IsLocked = bool.Parse(paramStatus);
            UserServices.Update(user);
            // reload data
            gpUser.Reload();
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete(object sender, DirectEventArgs e)
        {
            // init id
            var param = e.ExtraParams["Id"];
            // parse id
            if(!int.TryParse(param, out var id) || id <= 0)
            {
                // parse error, show error
                Dialog.ShowError("Có lỗi xảy ra trong quá trình xử lý");
                return;
            }
            // udate isDeleted true
            var user = UserServices.GetById(id);
            user.IsDeleted = true;
            UserServices.Update(user);
            // reload data
            gpUser.Reload();
        }

        /// <summary>
        /// Init window user role
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindowUserRole(object sender, DirectEventArgs e)
        {
            // init id
            var param = e.ExtraParams["Id"];
            // parse id
            if(int.TryParse(param, out var id))
            {
                // init id
                hdfUserId.Text = id.ToString();

                // load data for window user role
                LoadRolesForUserRoleWindow(id);
                
                // show window
                wdUserRole.Show();
            }
        }

        protected void AssignRolesForUsers(object sender, DirectEventArgs e)
        {
            try
            {
                // delete role for current user
                UserRoleServices.Delete(null, Convert.ToInt32(hdfUserId.Text));

                // get new role list
                var roleIds = hdfSelectedRoleIds.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                // add each user to selected role
                foreach(var row in rowSelectionModel.SelectedRows)
                {
                    // check user id
                    if(!string.IsNullOrEmpty(row.RecordID))
                    {
                        foreach(var roleId in roleIds)
                        {
                            UserRoleServices.Create(new UserRole
                            {
                                UserId = Convert.ToInt32(row.RecordID),
                                RoleId = Convert.ToInt32(roleId)
                            });
                        }
                    }
                }

                // hide window
                wdUserRole.Hide();
                // show dialog
                Dialog.ShowNotification("Thiết lập quyền thành công !");
            }
            catch(Exception ex)
            {
                // exception
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeDepartment_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeDepartment.DataSource = CurrentUser.DepartmentsTree;
            storeDepartment.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucChooseEmployee1_AfterClickAcceptButton(object sender, EventArgs e)
        {
            //hdfTotalRecord.Text = ucChooseEmployee.SelectedRow.Count.ToString();
            //foreach(var item in ucChooseEmployee.SelectedRow)
            //{
            //    // get employee information
            //    // get employee information
            //    var recordModel = RecordController.GetById(Convert.ToInt32(item.RecordID));
            //    // insert record to grid
            //    RM.RegisterClientScriptBlock("insert" + recordModel.Id, AddRecordString(recordModel));
            //}

        }

        protected void ShowWindowRole(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfSeletedUserId.Text) || Convert.ToInt32(hdfSeletedUserId.Text) <= 0) return;
            var lstRoleIds = RoleServices.GetAll(false, Convert.ToInt32(hdfSeletedUserId.Text), null, null)
                .Select(r => r.Id).ToList();
            hdfCurrentRoleIdsForUser.Text = lstRoleIds.Count > 0 ? string.Join(",", lstRoleIds) : string.Empty;
            wdUserRole.Show();
        }

        protected void ShowWindowDepartment(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfSeletedUserId.Text) || Convert.ToInt32(hdfSeletedUserId.Text) <= 0) return;
            var lstDepartmentIds = cat_DepartmentServices
                .GetAll(null, null, null, null, false, Convert.ToInt32(hdfSeletedUserId.Text), null, null)
                .Select(d => d.Id).ToList();
            hdfCurrentDepartmentIdsForUser.Text =
                lstDepartmentIds.Count > 0 ? string.Join(",", lstDepartmentIds) : string.Empty;
            wdAssignDepartment.Show();
        }

        

        protected void AssignDepartmentsForUsers(object sender, DirectEventArgs e)
        {
            try
            {
                // delete role for each selected user
                foreach (var row in rowSelectionModel.SelectedRows)
                {
                    // check user id
                    if (!string.IsNullOrEmpty(row.RecordID))
                    {
                        // delete roles for user
                        UserDepartmentServices.Delete(Convert.ToInt32(row.RecordID), null);
                    }
                }

                // get new role list
                var departmentIds =
                    hdfSelectedDepartmentIds.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                // add each user to selected role
                foreach (var row in rowSelectionModel.SelectedRows)
                {
                    // check user id
                    if (!string.IsNullOrEmpty(row.RecordID))
                    {
                        foreach (var departmentId in departmentIds)
                        {
                            UserDepartmentServices.Create(new UserDepartment
                            {
                                UserId = Convert.ToInt32(row.RecordID),
                                DepartmentId = Convert.ToInt32(departmentId),
                                IsPrimary = false
                            });
                        }
                    }
                }

                // hide window
                wdAssignDepartment.Hide();
                // show dialog
                Dialog.ShowNotification("Thiết lập đơn vị thành công !");
            }
            catch (Exception ex)
            {
                // exception
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }


        }


        #endregion

        #region Direct Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        [DirectMethod]
        public void DeleteUser(int id)
        {
            try
            {
                var user = UserServices.GetById(id);
                if(user != null)
                {
                    user.IsDeleted = true;
                    if(UserServices.Update(user) != null)
                    {
                        // show notification
                        Dialog.ShowNotification("Xóa tài khoản thành công");
                    }
                }
            }
            catch(Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        [DirectMethod]
        public void ResetWindowTitle()
        {
            wdSetting.Title = @"Thêm người dùng";
            wdSetting.Icon = Icon.Add;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Load all roles for filter
        /// </summary>
        private void LoadRolesForFilterTree()
        {
            // init root, add into tree
            var root = new TreeNode();
            treeRoleFilter.Root.Clear();
            treeRoleFilter.Root.Add(root);
            // init node all role, add into tree
            var nodeAllRole = new TreeNode
            {
                NodeID = @"0",
                Text = @"Tất cả",
                Icon = Icon.Group,
                Expanded = true
            };
            nodeAllRole.Listeners.Click.Handler = @"handlerRoleChanged(0)";
            root.Nodes.Add(nodeAllRole);
            // get all roles
            var roles = RoleServices.GetAll(false, null, null, null);
            // add roles into tree
            foreach(var role in roles)
            {
                // init node
                var node = new TreeNode
                {
                    NodeID = role.Id.ToString(),
                    Text = role.RoleName,
                    Icon = Icon.Group,
                    Expanded = true
                };
                // bind handler click
                node.Listeners.Click.Handler = @"handlerRoleChanged({0})".FormatWith(role.Id);
                // add node
                nodeAllRole.Nodes.Add(node);
            }
        }

        /// <summary>
        /// Load all roles for user role window
        /// </summary>
        private void LoadRolesForUserRoleWindow(int userId)
        {
            // init root, add into tree
            var root = new TreeNode();
            treeAvailableRoles.Root.Clear();
            treeAvailableRoles.Root.Add(root);
            // init node all role, add into tree
            var nodeAllRole = new TreeNode
            {
                Text = @"Danh mục quyền",
                NodeID = "0",
                Icon = Icon.Group,
                Expanded = true
            };
            root.Nodes.Add(nodeAllRole);
            // get all available roles
            var roles = RoleServices.GetAll(false, null, null, null);
            // get current user roles
            var userRoles = RoleServices.GetAll(false, userId, null, null);
            // add roles into tree
            foreach (var role in roles)
            {
                var node = new TreeNode
                {
                    Text = role.RoleName,
                    NodeID = role.Id.ToString(),
                    Expanded = true,
                    Checked = userRoles.Contains(role) ? ThreeStateBool.True : ThreeStateBool.False
                };
                nodeAllRole.Nodes.Add(node);
            }
        }

        private void LoadDepartmentsForAssignWindow(List<cat_Department> departments)
        {
            // init hidden all roles id
            hdfAvailableDepartmentIds.Text =
                departments.Count > 0 ? string.Join(",", departments.Select(d => d.Id)) : string.Empty;
            // init root, add into tree
            var nodeRoot = new TreeNode();
            treeAvailableDepartments.Root.Clear();
            treeAvailableDepartments.Root.Add(nodeRoot);
            // init node all department, add into tree
            var nodeAllDepartment = new TreeNode
            {
                Text = @"Danh mục đơn vị",
                NodeID = "0",
                Icon = Icon.Group,
                Expanded = true
            };
            nodeRoot.Nodes.Add(nodeAllDepartment);
            // add root department into tree
            var lstRootDepartments = departments.Where(d => d.ParentId == 0).OrderBy(d => d.Order);
            foreach (var d in lstRootDepartments)
            {
                var node = new TreeNode
                {
                    Text = d.Name,
                    NodeID = d.Id.ToString(),
                    Expanded = true,
                    Checked = ThreeStateBool.False
                };
                nodeAllDepartment.Nodes.Add(node);
                PopulateChildDepartments(departments, ref node);
            }
        }

        private void PopulateChildDepartments(List<cat_Department> departments, ref TreeNode currNode)
        {
            var parentId = Convert.ToInt32(currNode.NodeID);
            var lstChildDepartments =
                departments.Where(d => d.ParentId == Convert.ToInt32(parentId)).OrderBy(d => d.Order);
            foreach (var d in lstChildDepartments)
            {
                var node = new TreeNode
                {
                    Text = d.Name,
                    NodeID = d.Id.ToString(),
                    Expanded = true,
                    Checked = ThreeStateBool.False
                };
                currNode.Nodes.Add(node);
                PopulateChildDepartments(departments, ref node);
            }
        }

        private void AddUserDepartment(int userId, int departmentId)
        {
            var department = CurrentUser.Departments.FirstOrDefault(d => d.Id == departmentId);
            if (department != null && department.Id > 0)
            {
                // add primary department
                var userDepartment = new UserDepartment
                {
                    UserId = userId,
                    DepartmentId = department.Id,
                    IsPrimary = true
                };
                UserDepartmentServices.Create(userDepartment);
                // add user into parent department
                var parentDepartment = CurrentUser.Departments.FirstOrDefault(d => d.Id == department.ParentId);
                // check parent department is valid
                while (parentDepartment != null && parentDepartment.ParentId >= 0)
                {
                    // init new object
                    userDepartment = new UserDepartment
                    {
                        UserId = userId,
                        DepartmentId = parentDepartment.Id,
                        IsPrimary = false
                    };
                    // add user into parent department
                    UserDepartmentServices.Create(userDepartment);
                    // move to hight level
                    parentDepartment = CurrentUser.Departments.FirstOrDefault(d => d.Id == parentDepartment.ParentId);
                }

                // check department was locked
                if (department.IsLocked)
                {
                    // add user into child departments
                    AddUserIntoChildDepartment(userId, department.Id);
                }
            }
        }

        private void AddUserIntoChildDepartment(int userId, int parentId)
        {
            // get child departments
            var childDepartments = CurrentUser.Departments.Where(d => d.ParentId == parentId);
            // add user into each child department
            foreach (var d in childDepartments)
            {
                // init new object
                var userDepartment = new UserDepartment
                {
                    UserId = userId,
                    DepartmentId = d.Id,
                    IsPrimary = false
                };
                // add user into department
                UserDepartmentServices.Create(userDepartment);
                // add user into child departments
                AddUserIntoChildDepartment(userId, d.Id);
            }
        }

        #endregion


    }
}