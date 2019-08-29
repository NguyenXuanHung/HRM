<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerRole" %>

using System.Web;
using Web.Core;
using Web.Core.Framework.Common;
using Web.Core.Service.Security;

namespace Web.HRM.Services
{
    public class HandlerRole : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        // init default value
        private int _start = Constant.DefaultStart;
        private int _limit = Constant.DefaultPagesize;

        public void ProcessRequest(HttpContext context)
        {
            // get params value
            if(!string.IsNullOrEmpty(context.Request["start"]))
            {
                _start = int.Parse(context.Request["start"]);
            }
            if(!string.IsNullOrEmpty(context.Request["limit"]))
            {
                _limit = int.Parse(context.Request["limit"]);
            }
            // select from table
            var pageResult = RoleServices.GetPaging(false, null, null, _start, _limit);
            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}