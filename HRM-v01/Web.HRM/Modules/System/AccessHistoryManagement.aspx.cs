using System;
using Ext.Net;
using SoftCore;
using DataController;
using Web.Core.Framework;
using Web.Core.Object.Security;
using Web.Core.Service.Security;

namespace Web.HRM.Modules.System
{
    public partial class AccessHistoryManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grp_nhatkytrucap.AddComponent(btnTruncateHistory, 2);
            grp_nhatkytrucap.HiddenDuplicateButton(true);
            if (!ExtNet.IsAjaxRequest)
            {
                var accessDiary = new AccessHistory
                {
                    Function = "Nhật ký truy cập",
                    Description = "Nhật ký truy cập",
                    IsError = false,
                    UserName = CurrentUser.User.UserName,
                    Time = DateTime.Now,
                    BusinessCode = "NHATKY_TRUYCAP",
                    ComputerName = Util.GetInstance().GetComputerName(Request.UserHostAddress),
                    ComputerIP = Request.UserHostAddress,
                    Referent = ""
                };
                AccessHistoryServices.Create(accessDiary);
            }
            Button btnChooseTime = new Button()
            {
                Text = @"Chọn thời gian",
                Icon = Icon.Clock,
            };
            btnChooseTime.Listeners.Click.Handler = "#{wdChooseTime}.show();";
            if (!ExtNet.IsAjaxRequest)
            {
                grp_nhatkytrucap.HiddenAddButton(true);
                grp_nhatkytrucap.HiddenEditButton(true);
                grp_nhatkytrucap.HiddenTienIch(true);
            }
            grp_nhatkytrucap.AddComponent(btnChooseTime, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTruncateHistory_Click(object sender, DirectEventArgs e)
        {
            DataHandler.GetInstance().ExecuteNonQuery("truncate table AccessHistory");
            grp_nhatkytrucap.ReloadStore();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOK_Click(object sender, DirectEventArgs e)
        {
            try
            {
                string startTime = "'" + DateTime1.GetValue() + "'";
                string endTime = "'" + DateTime2.GetValue() + "'";
                grp_nhatkytrucap.OutSideQuery = " Time > " + startTime + " and Time < " + endTime;
                grp_nhatkytrucap.ReloadStore();
                wdChooseTime.Hide();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }
    }

}

