using System;
using System.Globalization;
using System.Linq;
using Ext.Net;
using Web.Core.Framework;
using Web.Core;
using Web.Core.Framework.Common;
using Web.Core.Framework.Controller;
using Web.Core.Framework.Controller.TimeSheet;
using Web.Core.Object.Security;

namespace Web.HRM.Modules.Setting
{
    public partial class SchedulerTimeSheetManagement : BasePage
    {
        private const string SchedulerName = "CalculateDynamicColumn";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                // init data
                var objType = CatalogController.GetByName("cat_SchedulerType", "TaskTimeSheet");
                if (objType != null)
                {
                    hdfSchedulerType.Text = objType.Id.ToString();
                }
                hdfMonth.Text = DateTime.Now.Month.ToString();
                hdfYear.Text = DateTime.Now.Year.ToString();
                cbxMonth.SetValue(DateTime.Now.Month);
                spnYear.SetValue(DateTime.Now.Year);
                hdfChoseMonth.Text = DateTime.Now.Month.ToString();
                hdfChoseYear.Text = DateTime.Now.Year.ToString();
                cboChoseMonth.SetValue(DateTime.Now.Month);
                sfChoseYear.SetValue(DateTime.Now.Year);
                hdfDataType.Text = ((int) SalaryConfigDataType.DynamicValue).ToString();
            }
        }

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
                    wdSetting.Title = @"Cập nhật thông tin chấm công";
                    wdSetting.Icon = Icon.Pencil;
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
            
            // hide window
            wdSetting.Hide();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete_Click(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfChoseMonth.Text) && !string.IsNullOrEmpty(hdfChoseYear.Text))
            {
                if (!string.IsNullOrEmpty(hdfGroupWorkShiftId.Text))
                {
                    var groupWorkShiftId = Convert.ToInt32(hdfGroupWorkShiftId.Text);

                    //get all detail workShift
                    var order = " [StartDate] ASC ";
                    var lstWorkShift =
                        TimeSheetWorkShiftController.GetAll(null, false, groupWorkShiftId, null, null, null, null, Convert.ToInt32(hdfChoseMonth.Text), Convert.ToInt32(hdfChoseYear.Text), order, null);

                    var listWorkShiftIds = lstWorkShift.Select(ws => ws.Id).ToList();
                    if (listWorkShiftIds.Count > 0)
                    {
                        TimeSheetEventController.DeleteByCondition(listWorkShiftIds, null, null, null, TimeSheetAdjustmentType.Default);
                    }

                    //Update status schedule create new timeSheet
                    var scheduler = SchedulerController.GetByName(Constant.SchedulerTimeSheet);
                    if (scheduler != null)
                    {
                        scheduler.Status = SchedulerStatus.Ready;
                        scheduler.Arguments = "-m {0} -y {1} -groupWorkShiftId {2}".FormatWith(hdfChoseMonth.Text, hdfChoseYear.Text, hdfGroupWorkShiftId.Text);
                        //update
                        SchedulerController.Update(scheduler);
                    }
                }

                hdfGroupWorkShiftId.Reset();
                wdWindow.Hide();
                //reload
                gpScheduler.Reload();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveDynamicColumnClick(object sender, DirectEventArgs e)
        {
            try
            {
                //Update status schedule create new timeSheet
                var scheduler = SchedulerController.GetByName(SchedulerName);
                if (scheduler != null)
                {
                    scheduler.Status = SchedulerStatus.Ready;
                    var symbol = string.Empty;
                    foreach (var itemRow in chkSelectionModelSymbol.SelectedRows)
                    {
                        symbol += "," + itemRow.RecordID;
                    }
                    
                    if (!string.IsNullOrEmpty(symbol))
                    {
                        scheduler.Arguments = "-column {0} -symbol {1} -payrollId {2}".FormatWith(hdfColumnCode.Text, symbol.TrimStart(','), Convert.ToInt32(hdfPayrollId.Text));

                        SchedulerController.Update(scheduler);
                    }
                }

                //close window
                wdDynamicColumn.Hide();
                hdfColumnCode.Reset();
                cbxColumnCode.Reset();
                hdfPayrollId.Reset();
                cboPayroll.Reset();
                chkSelectionModelSymbol.ClearSelections();
            }
            catch (Exception exception)
            {
                Dialog.Alert(exception.Message);
            }
        }

        [DirectMethod]
        public void GetConfig()
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfPayrollId.Text))
                {
                    var payroll = PayrollController.GetById(Convert.ToInt32(hdfPayrollId.Text));
                    if (payroll != null)
                    {
                        hdfConfigId.Text = payroll.ConfigId.ToString();
                    }
                }
            }
            catch (Exception exception)
            {
                Dialog.Alert(exception.Message);
            }
        }
    }

}

