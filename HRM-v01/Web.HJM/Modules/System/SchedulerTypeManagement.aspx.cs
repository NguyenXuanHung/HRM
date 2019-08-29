using System;
using Ext.Net;
using Web.Core.Framework;
using Web.Core;
using System.Linq;
using Web.Core.Object.Security;
using Web.Core.Service.Security;

namespace Web.HJM.Modules.System
{
    public partial class SchedulerTypeManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));

            }
            if (btnEdit.Visible)
            {
                gridScheduleType.Listeners.RowDblClick.Handler += " if(CheckSelectedRows(gridScheduleType)){btnUpdate.show();btnUpdateClose.hide()}";
            }

        }

        #region Event Method

        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                if (e.ExtraParams["Command"] == "Update")
                    Update();
                else
                    Insert();
                //reload data
                gridScheduleType.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        private void Insert()
        {
            try
            {
                var schedulerType = new SchedulerType
                {
                    Name = txtName.Text.Trim(),
                    DisplayName = txtDisplayName.Text.Trim()
                };
                if (!string.IsNullOrEmpty(txtDescription.Text))
                {
                    schedulerType.Description = txtDescription.Text.Trim();
                }
                int status;
                if (int.TryParse(cbxSchedulerTypeStatus.SelectedItem.Value, out status))
                    schedulerType.Status = (SchedulerTypeStatus)status;
                schedulerType.Status = (SchedulerTypeStatus)status;
                SchedulerTypeServices.Create(schedulerType);
                wdTimeSheetRule.Hide();
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
                if (string.IsNullOrEmpty(hdfKeyRecord.Text)) return;
                var schedulerType = SchedulerTypeServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                if (schedulerType == null) return;
                schedulerType.Name = txtName.Text.Trim();
                schedulerType.DisplayName = txtDisplayName.Text.Trim();
                schedulerType.Description = txtDescription.Text.Trim();
                int status;
                if (int.TryParse(cbxSchedulerTypeStatus.SelectedItem.Value, out status))
                    schedulerType.Status = (SchedulerTypeStatus)status;
                SchedulerTypeServices.Update(schedulerType);
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(ex.Message));
            }
        }

        [DirectMethod]
        public void ResetForm()
        {
            txtName.Reset();
            txtDisplayName.Reset();
            txtDescription.Reset();
            cbxSchedulerTypeStatus.Reset();
        }

        protected void Delete(object sender, DirectEventArgs directEventArgs)
        {
            try
            {
                int id;
                if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
                SchedulerTypeServices.Delete(id);
                gridScheduleType.Reload();
                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }
        }

        protected void EditSchedulerType_Click(object sender, DirectEventArgs e)
        {
            int id;
            if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
            var scheduleType = SchedulerTypeServices.GetById(id);
            if (scheduleType != null)
            {
                txtName.Text = scheduleType.Name;
                txtDisplayName.Text = scheduleType.DisplayName;
                txtDescription.Text = scheduleType.Description;
                cbxSchedulerTypeStatus.SelectedItem.Text = scheduleType.Status.ToString();
            }
            // show window
            btnUpdate.Show();
            btnUpdateClose.Hide();

            wdTimeSheetRule.Title = @"Cập nhật quản lý loại tiến trình";
            wdTimeSheetRule.Show();
        }

        #endregion

        protected void storeSchedulerStatus_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeSchedulerStatus.DataSource = typeof(SchedulerTypeStatus).GetIntAndDescription();
            storeSchedulerStatus.DataBind();
        }
    }

}

