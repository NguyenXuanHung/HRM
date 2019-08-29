using System;
using System.Web.UI;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Service.Security;

namespace Web.HRM
{
    public partial class LoginHJM : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region Event Methods

        /// <summary>
        /// Reset password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ResetPassword(object sender, DirectEventArgs e)
        {

        }

        [DirectMethod]
        public void ProcessLogin()
        {
            try
            {
                // init return message
                var message = string.Empty;

                // init user model
                var userModel = new UserModel
                {
                    // authenticate
                    User = UserServices.Authenticate(txtUserName.Text.Trim(), txtPassword.Text, ref message)
                };

                // check user login
                if(userModel.User == null)
                {
                    // login fail
                    Dialog.ShowError("Login Fail", message);
                    return;
                }

                // check departments
                if(userModel.Departments.Count > 0)
                {
                    // log action
                    SystemLogController.Create(new SystemLogModel(userModel.User.UserName, "Default", SystemAction.Login,
                        SystemLogType.UserAction, "Đăng nhập"));

                    // set current user
                    Session["CurrentUser"] = userModel;

                    // redirect home page
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    // alert error message
                    Dialog.ShowError("No Department", "Người dùng chưa được cấp quyền với bất kỳ đơn vị nào.");

                    // redirect login page
                    Response.Redirect(Resource.Get("LoginUrl"));
                }
            }
            catch(Exception ex)
            {
                Dialog.ShowError(ex);
            }
        }

        #endregion
    }

}

