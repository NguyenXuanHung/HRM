using System;
using System.Collections.Generic;
using System.Linq;
using Ext.Net;
using SoftCore;
using SoftCore.User;
using SoftCore.Utilities;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Object.Catalog;
using Web.Core.Object.Security;
using Web.Core.Service.Catalog;
using Web.Core.Service.Security;

namespace Web.HRM.Modules.Setting
{
    public partial class ModulesSystemUserDefault : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!ExtNet.IsAjaxRequest)
            {
                // init action
                hdfAction.Text = @"getUsersForRole";
                // init current departments
                hdfDepartments.Text = CurrentUser.User.IsSuperUser
                    ? string.Empty
                    : string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                // init department
                cboDepartmentStore.DataSource = CurrentUser.DepartmentsTree;
                cboDepartmentStore.DataBind();

                // LoadRoleOnLeftPanel(roles);
                // 

                // get all avaiable roles
                var roles = RoleServices.GetAll(false, null, null, null);
                // load roles for tree panel left
                LoadRolesForTreePanel(roles);
                // load roles for window assign role
                LoadRolesForAssignWindow(roles);
                // load departments for window assign department
                LoadDepartmentsForAssignWindow(CurrentUser.Departments);
            }

            ucChooseEmployee1.AfterClickAcceptButton += ucChooseEmployee1_AfterClickAcceptButton;
        }

        #region Event Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShowWindowRole(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfSeletedUserId.Text) || Convert.ToInt32(hdfSeletedUserId.Text) <= 0) return;
            var lstRoleIds = RoleServices.GetAll(false, Convert.ToInt32(hdfSeletedUserId.Text), null, null)
                .Select(r => r.Id).ToList();
            hdfCurrentRoleIdsForUser.Text = lstRoleIds.Count > 0 ? string.Join(",", lstRoleIds) : string.Empty;
            wdAssignRole.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitEditedUser(object sender, DirectEventArgs e)
        {
            try
            {
                var userEdit = UserServices.GetById(Convert.ToInt32(hdfSeletedUserId.Text));
                if (userEdit != null)
                {
                    // init current user info
                    txtUserName.Text = userEdit.UserName;
                    txtEmail.Text = userEdit.Email;
                    txtFirstName.Text = userEdit.FirstName;
                    txtLastName.Text = userEdit.LastName;
                    txtDisplayName.Text = userEdit.DisplayName;
                    if (userEdit.Sex)
                        rdNam.Checked = true;
                    else
                        rdNu.Checked = true;
                    txtPhone.Text = userEdit.PhoneNumber;
                    txtAddress.Text = userEdit.Address;
                    if (userEdit.BirthDate.HasValue)
                        dfBirthday.SelectedDate = userEdit.BirthDate.Value;
                    // init current department
                    var primaryDepartment = cat_DepartmentServices.GetPrimary(userEdit.Id);
                    if (primaryDepartment != null)
                    {
                        var foundDepartment =
                            CurrentUser.DepartmentsTree.FirstOrDefault(d => d.Id == primaryDepartment.Id);
                        if (foundDepartment != null)
                        {
                            // set selected department
                            cboDepartment.Text = foundDepartment.Name;
                            // set current primary department id on hiddenfield
                            hdfSelectedDepartmentID.Text = foundDepartment.Id.ToString();
                        }
                    }

                    // set windows properties
                    hdfUserCommand.Text = @"Update";
                    wdAddUser.Icon = Icon.Pencil;
                    wdAddUser.Title = @"Sửa tài khoản người dùng";
                    wdAddUser.Show();
                }
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdateUser(object sender, DirectEventArgs e)
        {
            try
            {
                if (e.ExtraParams["Command"] == "Insert")
                {
                    // check user exist
                    var findUser = UserServices.GetByName(txtUserName.Text);
                    if (findUser != null)
                    {
                        // user existed
                        ExtNet.MessageBox.Alert("Có lỗi xảy ra", "Tài khoản này đã tồn tại trong hệ thống.").Show();
                        return;
                    }

                    // init object
                    var userInsert = new Web.Core.Object.Security.User
                    {
                        UserName = txtUserName.Text,
                        Password = txtPassword.Text.ToSHA256(),
                        Email = txtEmail.Text,
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        DisplayName = txtDisplayName.Text.Trim(),
                        Sex = rdNam.Checked,
                        PhoneNumber = txtPhone.Text,
                        Address = txtAddress.Text,
                        IsSuperUser = false,
                        IsLocked = false,
                        IsDeleted = false,
                        CreatedBy = CurrentUser.User.Id.ToString(),
                        CreatedDate = DateTime.Now,
                        EditedBy = CurrentUser.User.Id.ToString(),
                        EditedDate = DateTime.Now
                    };
                    // set date of birth
                    if (dfBirthday.SelectedDate.Year != 1)
                        userInsert.BirthDate = dfBirthday.SelectedDate;
                    // create user
                    var userId = UserServices.Create(userInsert);
                    // check isert result
                    if (userId != null && !string.IsNullOrEmpty(hdfSelectedDepartmentID.Text) &&
                        Convert.ToInt32(hdfSelectedDepartmentID.Text) > 0)
                    {
                        // add user in selected department
                        AddUserDepartment(userId.Id, Convert.ToInt32(hdfSelectedDepartmentID.Text));
                        // show notification
                        Dialog.ShowNotification("Thêm mới tài khoản thành công");
                    }
                    else
                    {
                        // show alert
                        ExtNet.MessageBox.Alert("Có lỗi xảy ra",
                            "Có lỗi xảy ra trong quá trình thêm tài khoản, hãy thử lại.").Show();
                        return;
                    }
                }

                if (e.ExtraParams["Command"] == "Update")
                {
                    var userEdit = UserServices.GetById(Convert.ToInt32(hdfSeletedUserId.Text));
                    if (userEdit != null)
                    {
                        // init date of birth
                        DateTime? birthDate = null;
                        if (dfBirthday.SelectedDate.Year != 1)
                            birthDate = dfBirthday.SelectedDate;
                        // update user
                        var updateResult = UserServices.Update(userEdit.Id, txtPassword.Text, txtEmail.Text,
                            txtFirstName.Text,
                            txtLastName.Text, txtDisplayName.Text, null, birthDate, rdNam.Checked,
                            null, null, null, null, null, CurrentUser.User.Id.ToString());
                        // if success, update department
                        if (updateResult != null && !string.IsNullOrEmpty(hdfSelectedDepartmentID.Text) &&
                            Convert.ToInt32(hdfSelectedDepartmentID.Text) > 0)
                        {
                            // remove user from old departments
                            UserDepartmentServices.Delete(userEdit.Id, null);
                            // add user into new departments
                            AddUserDepartment(userEdit.Id, Convert.ToInt32(hdfSelectedDepartmentID.Text));
                        }
                    }
                }

                // reload gỉd
                gridUsersForRole.Reload();

                // send notification email
                //if (!string.IsNullOrEmpty(txtEmail.Text) && chkSendMail.Checked)
                //{
                //    sendEmailCreateAccount(uInfo);
                //}

                // check close param
                if (e.ExtraParams["Close"] == "True")
                {
                    // close window
                    wdAddUser.Hide();
                }
                else
                {
                    // reset form
                    RM.RegisterClientScriptBlock("resetForm", "resetForm();");
                }

            }
            catch (Exception ex)
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
        protected void AssignRolesForUsers(object sender, DirectEventArgs e)
        {
            try
            {
                // delete role for each selected user
                foreach (var row in RowSelectionModel1.SelectedRows)
                {
                    // check user id
                    if (!string.IsNullOrEmpty(row.RecordID))
                    {
                        // delete roles for user
                        UserRoleServices.Delete(null, Convert.ToInt32(row.RecordID));
                    }
                }

                // get new role list
                var roleIds = hdfSelectedRoleIds.Text.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                // add each user to selected role
                foreach (var row in RowSelectionModel1.SelectedRows)
                {
                    // check user id
                    if (!string.IsNullOrEmpty(row.RecordID))
                    {
                        foreach (var roleId in roleIds)
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
                wdAssignRole.Hide();
                // show dialog
                Dialog.ShowNotification("Thiết lập quyền thành công !");
            }
            catch (Exception ex)
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
        protected void AssignDepartmentsForUsers(object sender, DirectEventArgs e)
        {
            try
            {
                // delete role for each selected user
                foreach (var row in RowSelectionModel1.SelectedRows)
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
                    hdfSelectedDepartmentIds.Text.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                // add each user to selected role
                foreach (var row in RowSelectionModel1.SelectedRows)
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
                if (user != null)
                {
                    user.IsDeleted = true;
                    if (UserServices.Update(user) != null)
                    {
                        // show notification
                        Dialog.ShowNotification("Xóa tài khoản thành công");
                    }
                }
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        [DirectMethod]
        public void ResetWindowTitle()
        {
            wdAddUser.Title = @"Thêm người dùng";
            wdAddUser.Icon = Icon.Add;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roles"></param>
        private void LoadRolesForTreePanel(List<Role> roles)
        {
            // init root, add into tree
            var root = new TreeNode();
            treeRoleFilter.Root.Clear();
            treeRoleFilter.Root.Add(root);
            // init node all role, add into tree
            var nodeAllRole = new TreeNode
            {
                Text = @"Danh mục quyền",
                NodeID = @"0",
                Icon = Icon.Group,
                Expanded = true
            };
            nodeAllRole.Listeners.Click.Handler = @"reloadUsersForRole(0)";
            root.Nodes.Add(nodeAllRole);
            // add roles into tree
            foreach (var role in roles)
            {
                // init node
                var node = new TreeNode
                {
                    NodeID = role.Id.ToString(),
                    Text = role.RoleName,
                    Expanded = true
                };
                // bind handler click
                node.Listeners.Click.Handler = @"reloadUsersForRole({0})".FormatWith(node.NodeID);
                // add node
                nodeAllRole.Nodes.Add(node);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roles"></param>
        private void LoadRolesForAssignWindow(List<Role> roles)
        {
            // init hidden all roles id
            hdfAvailableRoleIds.Text = "";
            // init root, add into tree
            var nodeRoot = new TreeNode();
            treeAvailableRoles.Root.Clear();
            treeAvailableRoles.Root.Add(nodeRoot);
            // init node all role, add into tree
            var nodeAllRole = new TreeNode
            {
                Text = @"Danh mục quyền",
                NodeID = "0",
                Icon = Icon.Group,
                Expanded = true
            };
            nodeRoot.Nodes.Add(nodeAllRole);
            // add roles into tree
            foreach (var role in roles)
            {
                var node = new TreeNode
                {
                    Text = role.RoleName,
                    NodeID = role.Id.ToString(),
                    Expanded = true,
                    Checked = ThreeStateBool.False
                };
                nodeAllRole.Nodes.Add(node);
                hdfAvailableRoleIds.Text += role.Id + @",";
            }

            hdfAvailableRoleIds.Text = hdfAvailableRoleIds.Text.TrimEnd(',');
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="currNode"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="departmentId"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucChooseEmployee1_AfterClickAcceptButton(object sender, EventArgs e)
        {
            SelectedRowCollection s = (SelectedRowCollection) sender;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdatePassword_Click(object sender, DirectEventArgs e)
        {
            if (hdfSeletedUserId.Text != "")
            {
                string message = string.Empty;
                var user = UserServices.AdminChangePassword(int.Parse(hdfSeletedUserId.Text), txtNewPassword.Text,
                    ref message);
                if (user != null)
                {
                    wdChangePassword.Hide();
                    Dialog.ShowNotification("Đổi mật khẩu thành công !");
                }
            }
            else
            {
                X.MessageBox.Alert("Thông báo", "Bạn chưa chọn nhân viên nào").Show();
            }
        }

        /// <summary>
        /// Reset mật khẩu người dùng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void mnuResetPassword_Click(object sender, DirectEventArgs e)
        {
            string userid = "";
            string failUserId = "";
            try
            {
                string[] ds = hdfListUserID.Text.Split(',');
                List<string> userIdList = new List<string>();
                for (int i = 0; i < ds.Count(); i++)
                {
                    if (ds[i] != "")
                        userIdList.Add(ds[i]);
                }

                string mailContent = Util.GetInstance().ReadFile(Server.MapPath("EmailHTML/ResetPassword.htm"));
                string systemName = SystemConfigController
                    .GetValueByNameFollowDepartment(SystemConfigParameter.COMPANY_NAME, string.Join(",", CurrentUser.Departments.Select(d => d.Id)))
                    .ToString();
                string systemEmail =
                    SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.EMAIL,
                        string.Join(",", CurrentUser.Departments.Select(d => d.Id)));
                string systemPassword =
                    SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.PASSWORD_EMAIL,
                        string.Join(",", CurrentUser.Departments.Select(d => d.Id)));
                foreach (var item in userIdList)
                {
                    string password = Util.GetInstance().GetRandomString(7);
                    UserInfo u = UsersController.GetInstance().GetUser(int.Parse(item));
                    if (Email.SendEmail(systemEmail, systemPassword, systemName, u.Email, "Thông báo mật khẩu mới",
                        string.Format(mailContent, password, u.DisplayName)))
                    {
                        u.Password = password;
                        UsersController.GetInstance().UpdateUser(u);
                        userid += item + ", ";
                    }
                    else
                    {
                        failUserId += item + ", ";
                    }
                }

                if (!string.IsNullOrEmpty(failUserId))
                {
                    X.MessageBox.Alert("Thông báo", "Các mã tài khoản sau không đổi đổi được mật khẩu: " + failUserId)
                        .Show();
                }
                else
                {
                    X.MessageBox.Alert("Thông báo", "Đặt lại mật khẩu thành công").Show();
                }

                //var accessDiary = new AccessHistory
                //{
                //    Function = "Đặt lại mật khẩu",
                //    Description = "Đặt lại mật khẩu",
                //    IsError = false,
                //    UserName = CurrentUser.User.UserName,
                //    Time = DateTime.Now,
                //    BusinessCode = "Users",
                //    ComputerName = Util.GetInstance().GetComputerName(Request.UserHostAddress),
                //    ComputerIP = Request.UserHostAddress,
                //    Referent = "Các UserId bị reset mật khẩu : " + userid
                //};
                //AccessHistoryServices.Create(accessDiary);
            }
            catch (Exception ex)
            {
                //var accessDiary = new AccessHistory
                //{
                //    Function = "Đặt lại mật khẩu",
                //    Description = ex.Message.Replace("'", " "),
                //    IsError = true,
                //    UserName = CurrentUser.User.UserName,
                //    Time = DateTime.Now,
                //    BusinessCode = "Users",
                //    ComputerName = Util.GetInstance().GetComputerName(Request.UserHostAddress),
                //    ComputerIP = Request.UserHostAddress,
                //    Referent = "Các UserId bị reset mật khẩu : " + userid
                //};
                //AccessHistoryServices.Create(accessDiary);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void mnuLockUser_Click(object sender, DirectEventArgs e)
        {
            if (RowSelectionModel1.SelectedRows.Count() == 0)
            {
                Dialog.ShowNotification("Bạn chưa chọn tài khoản nào !");
                return;
            }

            foreach (var item in RowSelectionModel1.SelectedRows)
            {
                UsersController.GetInstance().LockUser(int.Parse(item.RecordID));
            }

            Dialog.ShowNotification("Khóa tài khoản thành công");
            ReloadStore();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void mnuUnlockUser_Click(object sender, DirectEventArgs e)
        {
            if (RowSelectionModel1.SelectedRows.Count() == 0)
            {
                Dialog.ShowNotification("Bạn chưa chọn tài khoản nào !");
                return;
            }

            foreach (var item in RowSelectionModel1.SelectedRows)
            {
                UsersController.GetInstance().UnLockUser(int.Parse(item.RecordID));
            }

            Dialog.ShowNotification("Mở tài khoản thành công");
            ReloadStore();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uInfo"></param>
        private void sendEmailCreateAccount(UserInfo uInfo)
        {
            // send email to user
            //SystemController htController = new SystemController();
            object mailFrom =
                SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.EMAIL,
                    string.Join(",", CurrentUser.Departments.Select(d => d.Id)));
            var from = mailFrom != null ? mailFrom.ToString() : "";

            var mailPassword =
                SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.PASSWORD_EMAIL,
                    string.Join(",", CurrentUser.Departments.Select(d => d.Id)));
            var fromEmailPassword = mailPassword != null ? mailPassword : "";

            if (from == "" || fromEmailPassword != "")
            {
                from = "dsofthrm@gmail.com"; // TODO : move to  constant
                fromEmailPassword = "dsofthrm123";
            }
            
            var mailName = "Đăng ký tài khoản"; // TODO : move to constant
            var subject = "Đăng ký tài khoản";
            var content = Util.GetInstance().ReadFile(Server.MapPath(
                "../../Modules/MailTemplate/CreateAccount.vi-VN.html")); 

            content = string.Format(content, uInfo.DisplayName ?? uInfo.UserName, uInfo.UserName, uInfo.Password);

            if (mailFrom != "" && mailPassword != "")
            {
                SoftCore.Utilities.Email.SendEmail(from, fromEmailPassword, mailName, uInfo.Email, subject, content);

                X.Msg.Alert("Cảnh báo", "Đã gửi mail đến người dùng!").Show();
            }

            // end send email to user
        }

        /// <summary>
        /// 
        /// </summary>
        private void ReloadStore()
        {
            RM.RegisterClientScriptBlock("d", "#{storeUsersForRole}.reload();");
        }

        #endregion
    }
}