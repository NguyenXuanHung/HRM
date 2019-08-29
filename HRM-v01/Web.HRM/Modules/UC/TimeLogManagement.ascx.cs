using System;
using System.Linq;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Common;
using Web.Core.Framework.Controller.TimeSheet;
using Web.Core.Object.Security;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Security;
using Web.Core.Service.TimeSheet;

namespace Web.HRM.Modules.UC
{
    public partial class TimeLogManagement : BaseUserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfMonth.Text = DateTime.Now.Month.ToString();
                hdfYear.Text = DateTime.Now.Year.ToString();
                cbxMonth.SetValue(DateTime.Now.Month);
                spnYear.SetValue(DateTime.Now.Year);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete_Click(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfMonth.Text) && !string.IsNullOrEmpty(hdfYear.Text))
            {
                if (!string.IsNullOrEmpty(hdfTimeSheetGroupListId.Text))
                {
                    var groupWorkShiftId = Convert.ToInt32(hdfTimeSheetGroupListId.Text);
                    
                    //get all detail workShift
                    var order = " [StartDate] ASC ";
                    var lstWorkShift =
                        TimeSheetWorkShiftController.GetAll(null, false, groupWorkShiftId, null, null, null, null, Convert.ToInt32(hdfMonth.Text), Convert.ToInt32(hdfYear.Text), order, null);

                    var listWorkShiftIds = lstWorkShift.Select(ws => ws.Id).ToList();
                    if (listWorkShiftIds.Count > 0)
                    {
                        TimeSheetEventController.DeleteByCondition(listWorkShiftIds, null, null, null, TimeSheetAdjustmentType.Default);
                    }
                    
                    //Update status schedule create new timeSheet
                    var scheduler = SchedulerServices.GetSchedulerByName();
                    if (scheduler != null)
                    {
                        scheduler.Status = SchedulerStatus.Ready;
                        scheduler.Arguments = "-m {0} -y {1} -groupWorkShiftId {2}".FormatWith(hdfMonth.Text, hdfYear.Text, hdfTimeSheetGroupListId.Text);

                        SchedulerServices.Update(scheduler);
                    }
                }
               
                wdWindow.Hide();
            }
        }
    }
}

