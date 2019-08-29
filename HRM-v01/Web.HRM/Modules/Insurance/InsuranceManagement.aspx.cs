using System;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Modules.Insurance
{
    public partial class InsuranceManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfOrder.Text = Request.QueryString["order"];

                // load west
                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelected}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');#{txtKeyword}.reset();#{pagingToolbar}.pageIndex = 0; #{pagingToolbar}.doLoad();"
                }.AddDepartmentList(brlayout, CurrentUser, false);
            }
        }
        
    }
}