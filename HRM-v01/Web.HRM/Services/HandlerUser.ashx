<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerUser" %>

using System;
using System.Web;
using Web.Core;
using Web.Core.Service.Security;

namespace Web.HRM.Services
{
    public class HandlerUser : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {        
        private string _keyword = string.Empty;
        private int? _roleId;
        private string _departments = string.Empty;

        public void ProcessRequest(HttpContext context)
        {           
            // get params value
            if (!string.IsNullOrEmpty(context.Request["start"]))
            {
                Start = int.Parse(context.Request["start"]);
            }
            if (!string.IsNullOrEmpty(context.Request["limit"]))
            {
                Limit = int.Parse(context.Request["limit"]);
            }
            if (!string.IsNullOrEmpty(context.Request["keyword"]))
            {
                _keyword = context.Request["keyword"];
            }
            if (!string.IsNullOrEmpty(context.Request["role"]) && Convert.ToInt32(context.Request["role"]) > 0)
            {
                _roleId = Convert.ToInt32(context.Request["role"]);
            }
            if (!string.IsNullOrEmpty(context.Request["departments"]))
            {
                _departments = context.Request["departments"];
            }
            // select from table
            var pageResult = UserServices.GetPaging(_keyword, null, false, null, false, _roleId, _departments, null, Start, Limit);

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