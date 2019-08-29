using System;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Modules.Kpi
{
    public partial class CriterionGroupKpiManagement : BasePage
    {
        private const string GroupKpiManagementUrl = @"~/Modules/Kpi/GroupKpiManagement.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfStatus.Text = ((int) KpiStatus.Active).ToString();
                hdfGroup.Text = Request.QueryString["Id"];
                if (!string.IsNullOrEmpty(hdfGroup.Text))
                {
                    var group = GroupKpiController.GetById(Convert.ToInt32(hdfGroup.Text));
                    if (group != null)
                    {
                        gpGroupKpi.Title = @"Thông tin chi tiết: " + group.Name;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, DirectEventArgs e)
        {
            Response.Redirect(GroupKpiManagementUrl + "?mId=" + MenuId, true);
        }
    }
}