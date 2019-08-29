using System;
using Ext.Net;
using Web.Core.Framework;
using System.Linq;
using Web.Core.Framework.Utils;

namespace Web.HRM.Modules.TimeSheet
{
    public partial class TimeSheetWorkShiftManagement : BasePage
    {
        private const string TimeSheetGroupListUrl = "~/Modules/TimeSheet/TimeSheetGroupWorkShiftManagement.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                hdfTimeSheetGroupListId.Text = Request.QueryString["id"];
                
                // get start of month
                dfStartDate.SetValue(ConvertUtils.GetStartDayOfMonth());
                // last day of month
                dfEndDate.SetValue(ConvertUtils.GetLastDayOfMonth());
                if (!string.IsNullOrEmpty(hdfTimeSheetGroupListId.Text))
                {
                    var groupWork =
                        TimeSheetGroupWorkShiftController.GetById(Convert.ToInt32(hdfTimeSheetGroupListId.Text));
                    if (groupWork != null)
                    {
                        gridTime.Title = @"Chi tiết bảng phân ca " + groupWork.Name;
                        cboGroupWorkShift.Text = groupWork.Name;
                        hdfGroupWorkShiftName.Text = groupWork.Name;
                    }
                }
            }

        }

        #region Event Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, DirectEventArgs e)
        {
            Response.Redirect(TimeSheetGroupListUrl + "?mId=" + MenuId, true);
        }
        #endregion

    }
}