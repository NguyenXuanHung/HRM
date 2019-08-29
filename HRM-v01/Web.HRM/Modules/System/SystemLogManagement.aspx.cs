using System;
using Ext.Net;
using Web.Core.Framework;

namespace Web.HRM.Modules.Setting
{
    public partial class SystemLogManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!ExtNet.IsAjaxRequest)
            {
                // init data
                hdfIsEx.Text = Request.QueryString["isEx"];
            }
        }
    }
}