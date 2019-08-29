using System;
using System.Linq;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Object.Security;
using Web.Core.Service.Security;

namespace Web.HRM.Modules.UC
{
    public partial class SchedulerLogManagement : BaseUserControl
    {
        public SelectedRowCollection SelectedRow;
        public event EventHandler AfterClickAcceptButton;
        private int _countRole = -1;
        string[] _departmentList;
        //private string keyRecord = hdfKeyRecord.Text;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddOrUpdateSchedulerLog(object sender, DirectEventArgs e)
        {
            btnAddAndCloseLog.Show();
            btnUpdateLog.Hide();
            ResetForm();
            txtSchedulerHistory.Text = SchedulerHistoryServices.GetFieldValueById(Convert.ToInt32(hdfKeyRecord.Text), "Description");
            wdSchedulerLogManagement.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetForm()
        {
            txtDesciptionLog.Reset();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeSchedulerHistoryLog_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeSchedulerHistoryLog.DataSource = SchedulerHistoryServices.GetAll();
            storeSchedulerHistoryLog.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CloseWindow(object sender, DirectEventArgs e)
        {
            wdSchedulerLogManagement.Hide();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdateSchedulerLog(object sender, DirectEventArgs e)
        {
            if (e.ExtraParams["Command"] == "Update")
                UpdateLog();
            else
                InsertLog();
            gridSchedulerLog.Reload();
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [DirectMethod]
        protected void DeleteLog(object sender, DirectEventArgs e)
        {
            try
            {
                if (!int.TryParse(hdfKeyRecordLog.Text, out var id) || id <= 0) return;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditSchedulerLog_Click(object sender, DirectEventArgs e)
        {
            btnAddAndCloseLog.Hide();
            btnUpdateLog.Show();
            if (!int.TryParse(hdfKeyRecordLog.Text, out var id) || id <= 0) return;
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

