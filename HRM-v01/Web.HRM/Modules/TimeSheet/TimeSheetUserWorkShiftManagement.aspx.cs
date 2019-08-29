using System;
using Ext.Net;
using Web.Core.Framework;
using System.Linq;
using Web.Core.Framework.Controller.TimeSheet;
using Web.Core.Framework.Model.TimeSheet;

namespace Web.HRM.Modules.TimeSheet
{
    public partial class TimeSheetUserWorkShiftManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfDepartments.Text = DepartmentIds;
                // load west
                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelected}.setValue('" + Core.Framework.Common.BorderLayout.nodeID + "');#{txtSearch}.reset();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();",
                }.AddDepartmentList(brlayout, CurrentUser, false);

            }
        }

        #region Event Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            if (e.ExtraParams["Command"] == "Update")
                Update();
            else
                Insert(e);
            //reload data
            gridUserRule.Reload();
            grp_ListEmployee.Reload();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void Insert(DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfGroupWorkShift.Text))
            {
                foreach (var itemRow in chkEmployeeRowSelection.SelectedRows)
                {
                    var model = new TimeSheetEmployeeGroupWorkShiftModel
                    {
                        RecordId = Convert.ToInt32(itemRow.RecordID),
                        CreatedDate = DateTime.Now,
                        CreatedBy = CurrentUser.User.UserName,
                        EditedDate = DateTime.Now,
                        GroupWorkShiftId = Convert.ToInt32(hdfGroupWorkShift.Text),
                    };

                    var listUserRule =
                        TimeSheetEmployeeGroupWorkShiftController.GetAll(null, null, null, Convert.ToInt32(hdfGroupWorkShift.Text), null, null);
                    var listRecordId = listUserRule.Select(d => d.RecordId).ToList();
                    if (listRecordId.Count <= 0 ||
                        (listRecordId.Count > 0 && !listRecordId.Contains(model.RecordId)))
                    {
                        //Create 
                        TimeSheetEmployeeGroupWorkShiftController.Create(model);
                    }
                }
            }
           
            //reload grid
            gridUserRule.Reload();
            wdUserRule.Hide();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
            {
                var model = TimeSheetEmployeeGroupWorkShiftController.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                if (model != null)
                {
                    if (!string.IsNullOrEmpty(hdfUpdateGroupWorkShift.Text))
                    {
                        model.GroupWorkShiftId = Convert.ToInt32(hdfUpdateGroupWorkShift.Text);
                        model.EditedDate = DateTime.Now;
                        model.EditedBy = CurrentUser.User.UserName;
                    }
                    //update
                    TimeSheetEmployeeGroupWorkShiftController.Update(model);
                }
                gridUserRule.Reload();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="directEventArgs"></param>
        protected void Delete(object sender, DirectEventArgs directEventArgs)
        {
            if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
            {
                TimeSheetEmployeeGroupWorkShiftController.Delete(Convert.ToInt32(hdfKeyRecord.Text));
            }
            gridUserRule.Reload();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAccept_ClickAllEmployee(object sender, DirectEventArgs e)
        {
            try
            {
                //Áp dụng phân ca cho tất cả các nhân viên
                //Lấy tất cả nhân viên trong hồ sơ
                var records = RecordController.GetAllEmployeeInOrganization(hdfDepartments.Text);
                foreach (var item in records)
                {
                    var model = new TimeSheetEmployeeGroupWorkShiftModel()
                    {
                        RecordId = item.Id,
                        CreatedDate = DateTime.Now,
                        CreatedBy = CurrentUser.User.UserName,
                        EditedDate = DateTime.Now,
                    };
                    if (!string.IsNullOrEmpty(hdfGroupWorkShiftAll.Text))
                    {
                        model.GroupWorkShiftId = Convert.ToInt32(hdfGroupWorkShiftAll.Text);
                    }

                    //Create
                    TimeSheetEmployeeGroupWorkShiftController.Create(model);
                }

                gridUserRule.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteUserRule(object sender, DirectEventArgs e)
        {
            //Lấy tất cả bản ghi
            var userRules = TimeSheetEmployeeGroupWorkShiftController.GetAll(null, null, null, null, null, null);
            foreach (var item in userRules)
            {
                TimeSheetEmployeeGroupWorkShiftController.Delete(Convert.ToInt32(item.RecordId), null);
            }
            gridUserRule.Reload();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindow(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
            {
                var model = TimeSheetEmployeeGroupWorkShiftController.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                if (model != null)
                {
                    txtFullName.Text = model.FullName;
                    cbxUpdateGroupWorkShift.Text = model.GroupWorkShiftName;
                    hdfGroupWorkShift.Text = model.GroupWorkShiftId.ToString();
                }
                wdUpdateUserRule.Show();
            }
        }
        #endregion
    }
}
