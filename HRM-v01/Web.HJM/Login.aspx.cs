using System;
using System.Linq;
using System.Web.UI;
using Ext.Net;
using DataController;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Adapter;
using Web.Core.Object.Catalog;
using Web.Core.Service.Security;
using Web.Core.Service.Catalog;

namespace Web.HJM
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [DirectMethod]
        public bool checkkey()
        {
            if (!string.IsNullOrEmpty(hdfKey.Text))
            {
                var key = cat_DepartmentServices.GetById(Convert.ToInt32(hdfKey.Text));
                if (key != null)
                {
                    return true;
                }
            }
            wdCapKey.SetTitle("Đơn vị chưa có key =>Vui lòng đăng ký!!");
            wdCapKey.Show();
            return false;

        }

        protected void btnUpdateKey_Click(object sender, DirectEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {

                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        #region Event Methods

        protected void stDepartment_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            stDepartment.DataSource = DataHandler.GetInstance().ExecuteDataTable("SELECT Id, Name FROM cat_Department dd WHERE dd.ParentId = 0");
            stDepartment.DataBind();
        }


        protected void btnResetPassword_Click(object sender, DirectEventArgs e)
        {

        }

        [DirectMethod]
        public void DirectLogin()
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
                if (userModel.User == null)
                {
                    // login fail
                    ExtNet.MessageBox.Alert("Login Fail", message).Show();
                    return;
                }
                // check departments
                if (userModel.Departments.Count > 0)
                {
                    // set current user
                    Session["CurrentUser"] = userModel;
                    //
                    
                    if (userModel.User != null)
                    {
                        // check u
                        //lấy danh sách các bộ phận được truy cập tìm cán bộ sinh nhật trong tháng                        
                        var listDepartmentId = string.Join(",", userModel.Departments.Select(d => d.Id));
                        var objCountContract =
                            SQLHelper.ExecuteTable(
                                SQLManagementAdapter.GetStore_DanhSachNhanVienSapHetHopDong(null, null, 30, listDepartmentId)).Rows.Count;

                        // Danh sách sinh nhật trong tháng
                        var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);  // get start of month
                        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1); // last day of month                       
                        var tableBirthInMonth = SQLHelper
                            .ExecuteTable(SQLManagementAdapter.GetStore_BirthdayOfEmployee(listDepartmentId, firstDayOfMonth, lastDayOfMonth, null, null)).Rows.Count;

                        // Danh sách nhân viên đến kì nghỉ hưu
                        var tableRetirement = SQLHelper
                            .ExecuteTable(SQLManagementAdapter.GetStore_RetirementOfEmployee(null, null, listDepartmentId));
                        var countRetirement = 0;
                        var newEmployeeCode = string.Empty;
                        for (var i = 0; i < tableRetirement.Rows.Count; i++)
                        {
                            var currentEmployeeCode = (string)tableRetirement.Rows[i]["EmployeeCode"];
                            if (currentEmployeeCode != newEmployeeCode)
                            {
                                countRetirement++;
                                newEmployeeCode = currentEmployeeCode;
                            }
                        }

                        // Danh sách nhân viên được nâng lương
                        var tableListSalaryRaise =
                            SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_ListSalaryRaise(listDepartmentId,
                                "1900-01-01", DateTime.Now.ToString("MM/dd/yyyy"), null, null, (int)SalaryDecisionType.Regular, null, null)).Rows.Count;


                        var tableOutFrameSalary =
                            SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_ListSalaryRaise(listDepartmentId, null, null, null, null, (int)SalaryDecisionType.OverGrade, null, null)).Rows.Count;
                        Session["DataHomePage"] = tableBirthInMonth + ";" + objCountContract + ";" + countRetirement + ";" + tableListSalaryRaise + ";" + tableOutFrameSalary;
                    }
                    else
                    {
                        RM.RegisterClientScriptBlock("al", "Ext.Msg.alert('Lỗi đăng nhập', 'Có lỗi xảy ra trong quá trình đăng nhập');");
                    }

                    // redirect home page
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    // alert error message
                    ExtNet.MessageBox.Alert("No Department", "Người dùng chưa được cấp quyền với bất kỳ đơn vị nào.").Show();
                    // redirect login page
                    Response.Redirect("~/Login.aspx");
                }
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Exception", ex.Message).Show();
            }
        }

        #endregion
    }

}

