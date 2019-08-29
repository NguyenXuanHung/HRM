using System;
using System.Globalization;
using Ext.Net;
using Web.Core.Framework;
using Web.Core;
using Web.Core.Object.Security;

namespace Web.HRM.Modules.Setting
{
    public partial class SchedulerManagement : BasePage
    {
        #region Event Methods

        /// <summary>
        /// Init window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindow(object sender, DirectEventArgs e)
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
                    wdSetting.Icon = Icon.Pencil;
                }
                else
                {
                    // insert
                    wdSetting.Title = @"Thêm mới";
                    wdSetting.Icon = Icon.Add;
                }
                // init id
                hdfId.Text = id.ToString();
                // init model
                var model = new SchedulerModel(null);
                // check id
                if(id > 0)
                {
                    var result = SchedulerController.GetById(id);
                    if(result != null)
                        model = result;
                }
                // set scheduler prop
                txtName.Text = model.Name;
                txtDescription.Text = model.Description;
                txtIntervalTime.Text = model.IntervalTime.ToString();
                txtExpiredAfter.Text = model.ExpiredAfter.ToString();
                txtArguments.Text = model.Arguments;
                chkEnable.Checked = model.Enabled;
                if (model.NextRunTime != null)
                    txtNextRuntime.Text = model.NextRunTime.Value.ToString("yyyy/MM/dd HH:mm");
                if (model.Id > 0)
                {
                    // scheduler type
                    hdfSchedulerType.Text = model.SchedulerTypeId.ToString();
                    cbxSchedulerType.Text = model.SchedulerTypeName;
                    // repeat type
                    hdfSchedulerRepeatType.Text = ((int) model.RepeatType).ToString();
                    cbxSchedulerRepeatType.Text = model.RepeatTypeName;
                    // scope
                    hdfSchedulerScope.Text = ((int) model.Scope).ToString();
                    cbxSchedulerScope.Text = model.ScopeName;
                    // status
                    hdfSchedulerStatus.Text = ((int) model.Status).ToString();
                    cbxSchedulerStatus.Text = model.StatusName;
                }
                // show window
                wdSetting.Show();
            }
        }

        /// <summary>
        /// Insert or Update scheduler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            // init model
            var model = new SchedulerModel(null);
            // check id
            if (!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
            {
                var result = SchedulerController.GetById(Convert.ToInt32(hdfId.Text));
                if(result != null)
                    model = result;
            }
            // set new props for model
            model.Name = txtName.Text;
            model.Description = txtDescription.Text;
            model.Arguments = txtArguments.Text;
            model.IntervalTime = !string.IsNullOrEmpty(txtIntervalTime.Text) 
                ? Convert.ToInt32(txtIntervalTime.Text) : 0;
            model.ExpiredAfter = !string.IsNullOrEmpty(txtExpiredAfter.Text) 
                ? Convert.ToInt32(txtExpiredAfter.Text) : 0;
            model.Enabled = chkEnable.Checked;
            model.SchedulerTypeId = Convert.ToInt32(hdfSchedulerType.Text);
            model.RepeatType = (SchedulerRepeatType) Enum.Parse(typeof(SchedulerRepeatType), hdfSchedulerRepeatType.Text);
            model.Scope = (SchedulerScope) Enum.Parse(typeof(SchedulerScope), hdfSchedulerScope.Text);
            model.Status = (SchedulerStatus) Enum.Parse(typeof(SchedulerStatus), hdfSchedulerStatus.Text);
            model.NextRunTime = DateTime.TryParseExact(txtNextRuntime.Text, "yyyy/MM/dd HH:mm",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var nextRunTime)
                ? nextRunTime
                : DateTime.Now;
            // check model id
            if (model.Id > 0)
            {
                // update
                SchedulerController.Update(model);
            }
            else
            {
                // insert
                SchedulerController.Create(model);
            }
            // hide window
            wdSetting.Hide();
            // reload data
            gpScheduler.Reload();
        }

        /// <summary>
        /// Delete scheduler
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
            // delete scheduler
            SchedulerController.Delete(id);
            // reload data
            gpScheduler.Reload();
        }

        #endregion

        #region Store

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeSchedulerType_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeSchedulerType.DataSource = CatalogController.GetAll("cat_SchedulerType", null, null, CatalogStatus.Active, false, "[Name]", null);
            storeSchedulerType.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeSchedulerRepeatType_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeSchedulerRepeatType.DataSource = typeof(SchedulerRepeatType).GetIntAndDescription();
            storeSchedulerRepeatType.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeSchedulerScope_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeSchedulerScope.DataSource = typeof(SchedulerScope).GetIntAndDescription();
            storeSchedulerScope.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeSchedulerStatus_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeSchedulerStatus.DataSource = typeof(SchedulerStatus).GetIntAndDescription();
            storeSchedulerStatus.DataBind();
        }

        #endregion
    }

}

