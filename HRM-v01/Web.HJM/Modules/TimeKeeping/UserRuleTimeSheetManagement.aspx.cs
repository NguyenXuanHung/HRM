using System;
using Ext.Net;
using Web.Core.Framework;
using Web.Core;
using System.Linq;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.HJM.Modules.TimeKeeping
{
    public partial class UserRuleTimeSheetManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));

            }
            ucChooseEmployee.AfterClickAcceptButton += new EventHandler(ucChooseEmployee_AfterClickAcceptButton);
        }

        #region Event Method
        private void ucChooseEmployee_AfterClickAcceptButton(object sender, EventArgs e)
        {
            try
            {
                hdfTotalRecord.Text = ucChooseEmployee.SelectedRow.Count.ToString();
                foreach (var item in ucChooseEmployee.SelectedRow)
                {
                    // get employee information
                    var hs = RecordController.GetByEmployeeCode(item.RecordID);
                    var recordId = hs.Id.ToString();
                    var employeeCode = hs.EmployeeCode;
                    var fullName = hs.FullName;
                    var departmentName = hs.DepartmentName;
                    // insert record to grid
                    RM.RegisterClientScriptBlock("insert" + recordId,
                        string.Format("addRecord('{0}', '{1}', '{2}', '{3}');", recordId, employeeCode, fullName,
                            departmentName));
                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                if (e.ExtraParams["Command"] == "Update")
                    Update();
                else
                    Insert(e);
                //reload data
                gridUserRule.Reload();

            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        private void Insert(DirectEventArgs e)
        {
            try
            {
                var listId = e.ExtraParams["ListRecordId"].Split(',');
                if (listId.Count() < 1)
                {
                    ExtNet.Msg.Alert("Thông báo", "Bạn hãy chọn ít nhất 1 cán bộ").Show();
                    return;
                }

                if (!string.IsNullOrEmpty(hdfGroupWorkShift.Text))
                {
                    for (var i = 0; i < listId.Length - 1; i++)
                    {
                        var recordId = listId[i];
                        var user = new UserRuleTimeSheet()
                        {
                            RecordId = Convert.ToInt32(recordId),
                            CreatedDate = DateTime.Now,
                            CreatedBy = CurrentUser.User.UserName,
                        };

                        user.GroupWorkShiftId = Convert.ToInt32(hdfGroupWorkShift.Text);

                        var listUserRule =
                            UserRuleTimeSheetServices.GetAllUserRuleByGroupId(Convert.ToInt32(hdfGroupWorkShift.Text));
                        var listRecordId = listUserRule.Select(d => d.RecordId);
                        if (listUserRule.Count() <= 0 ||
                            (listRecordId.Count() > 0 && !listRecordId.Contains(user.RecordId)))
                        {
                            //Create 
                            UserRuleTimeSheetServices.Create(user);
                        }
                    }
                }
                if (e.ExtraParams["Close"] == "True")
                {
                    wdUserRule.Hide();
                }
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình thêm mới: {0}".FormatWith(ex.Message));
            }
        }

        private void Update()
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
                {
                    var userRule = UserRuleTimeSheetServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                    if (userRule != null)
                    {
                        if (!string.IsNullOrEmpty(hdfUpdateGroupWorkShift.Text))
                        {
                            userRule.GroupWorkShiftId = Convert.ToInt32(hdfUpdateGroupWorkShift.Text);
                            userRule.EditedDate = DateTime.Now;
                        }

                        UserRuleTimeSheetServices.Update(userRule);
                    }
                    gridUserRule.Reload();
                }
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(ex.Message));
            }
        }

        protected void Delete(object sender, DirectEventArgs directEventArgs)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
                {
                    UserRuleTimeSheetServices.Delete(Convert.ToInt32(hdfKeyRecord.Text));
                }
                gridUserRule.Reload();
                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }
        }

        protected void btnAccept_ClickAllEmployee(object sender, DirectEventArgs e)
        {
            try
            {
                //Áp dụng rule phân ca cho tất cả các nhân viên
                //Lấy tất cả nhân viên trong hồ sơ
                var records = RecordController.GetAllEmployeeInOrganization(hdfDepartments.Text);
                foreach (var item in records)
                {
                    var userRule = new UserRuleTimeSheet()
                    {
                        RecordId = item.Id,
                        CreatedDate = DateTime.Now,
                        CreatedBy = CurrentUser.User.UserName,
                    };
                    if (!string.IsNullOrEmpty(hdfGroupWorkShiftAll.Text))
                    {
                        userRule.GroupWorkShiftId = Convert.ToInt32(hdfGroupWorkShiftAll.Text);
                    }

                    //Create
                    UserRuleTimeSheetServices.Create(userRule);
                }

                gridUserRule.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        protected void DeleteUserRule(object sender, DirectEventArgs e)
        {
            //Lấy tất cả bản ghi
            var userRules = UserRuleTimeSheetServices.GetAll();
            foreach (var item in userRules)
            {
                UserRuleTimeSheetServices.DeleteByRecordId(item.RecordId);
            }
            gridUserRule.Reload();
        }

        protected void EditUserRule_Click(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
            {
                var userRule = UserRuleTimeSheetServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                if (userRule != null)
                {
                    txtFullName.Text = hr_RecordServices.GetFieldValueById(userRule.RecordId, "FullName");
                    cbxUpdateGroupWorkShift.Text =
                        hr_TimeSheetGroupWorkShiftServices.GetFieldValueById(userRule.GroupWorkShiftId, "Name");
                }
                wdUpdateUserRule.Show();
            }
        }
        #endregion
    }
}
