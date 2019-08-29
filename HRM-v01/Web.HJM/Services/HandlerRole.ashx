<%@ WebHandler Language="C#" Class="Web.HJM.Services.HandlerRole" %>

using System;
using System.Web;
using Web.Core;
using Web.Core.Service.Security;

namespace Web.HJM.Services
{
    public class HandlerRole : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            // init default value
            var start = 0;
            var limit = 30;

            // get params value
            if (!string.IsNullOrEmpty(context.Request["start"]))
            {
                start = int.Parse(context.Request["start"]);
            }
            if (!string.IsNullOrEmpty(context.Request["limit"]))
            {
                limit = int.Parse(context.Request["limit"]);
            }
            // select from table
            var pageResult = RoleServices.GetPaging(false, null, null, start, limit);
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