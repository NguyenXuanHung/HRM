using System;
using System.Collections.Generic;
using System.Linq;
using Ext.Net;
using SoftCore;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Object.Security;
using Web.Core.Service.Catalog;
using Web.Core.Service.Security;

namespace Web.HJM.Modules.UserControl
{
    public partial class ucSchedulerLogManagement : BaseUserControl
    {
        public SelectedRowCollection SelectedRow;
        public event EventHandler AfterClickAcceptButton;
        int _countRole = -1;
        string[] departmentList;
        //private string keyRecord = hdfKeyRecord.Text;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            }
        }

        protected void AddOrUpdateSchedulerLog(object sender, DirectEventArgs e)
        {
            btnAddAndCloseLog.Show();
            btnUpdateLog.Hide();
            ResetForm();
            txtSchedulerHistory.Text = SchedulerHistoryServices.GetFieldValueById(Convert.ToInt32(hdfKeyRecord.Text), "Description");
            wdSchedulerLogManagement.Show();
        }

        private void ResetForm()
        {
            txtDesciptionLog.Reset();
        }

        protected void storeSchedulerHistoryLog_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeSchedulerHistoryLog.DataSource = SchedulerHistoryServices.GetAll();
            storeSchedulerHistoryLog.DataBind();
        }

        protected void CloseWindow(object sender, DirectEventArgs e)
        {
            wdSchedulerLogManagement.Hide();
        }

        protected void InsertOrUpdateSchedulerLog(object sender, DirectEventArgs e)
        {
            if (e.ExtraParams["Command"] == "Update")
                UpdateLog();
            else
                InsertLog();
            gridSchedulerLog.Reload();
        }

        private void InsertLog()
        {
            try
            {
                var schedulerLog = new SchedulerLog
                {
                    SchedulerHistoryId = Convert.ToInt32(hdfKeyRecord.Text),
                    Description = txtDesciptionLog.Text.Trim(),
                    CreatedDate = DateTime.Now
                };
                BaseServices<SchedulerLog>.Create(schedulerLog);

            }
            catch (Exception e)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình thêm mới: {0}".FormatWith(e.Message));
            }

        }

        private void UpdateLog()
        {
            if (string.IsNullOrEmpty(hdfKeyRecordLog.Text)) return;
            var schedulerLog = SchedulerLogServices.GetById(Convert.ToInt32(hdfKeyRecordLog.Text));
            try
            {
                schedulerLog.Description = txtDesciptionLog.Text.Trim();
                BaseServices<SchedulerLog>.Update(schedulerLog);
            }
            catch (Exception e)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình sửa: {0}".FormatWith(e.Message));
            }

        }
        [DirectMethod]
        protected void DeleteLog(object sender, DirectEventArgs e)
        {
            try
            {
                int id;
                if (!int.TryParse(hdfKeyRecordLog.Text, out id) || id <= 0) return;
                SchedulerLogServices.Delete(id);
                PagingToolbar1.PageIndex = 0;
                RowSelectionModel1.ClearSelections();
                btnDelete.Disabled = true;
                btnEdit.Disabled = true;
                gridSchedulerLog.Reload();
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }

        }

        protected void EditSchedulerLog_Click(object sender, DirectEventArgs e)
        {
            btnAddAndCloseLog.Hide();
            btnUpdateLog.Show();
            int id;
            if (!int.TryParse(hdfKeyRecordLog.Text, out id) || id <= 0) return;
            var schedulerLog = SchedulerLogServices.GetById(Convert.ToInt32(hdfKeyRecordLog.Text));
            if (!string.IsNullOrEmpty(schedulerLog.SchedulerHistoryDescription))
                txtSchedulerHistory.Text = schedulerLog.SchedulerHistoryDescription;
            txtDesciptionLog.Text = schedulerLog.Description;
            // show window
            wdSchedulerLogManagement.Title = @"Cập nhật quản lý log tiến trình";
            wdSchedulerLogManagement.Show();
        }
    }
}

