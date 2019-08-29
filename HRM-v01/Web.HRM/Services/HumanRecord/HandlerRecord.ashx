<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerRecord" %>

using System;
using System.Web;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Services
{
    public class HandlerRecord : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            // get params value
            if(!string.IsNullOrEmpty(context.Request["query"]))
            {
                Keyword = $" AND [FullName] LIKE N'%{context.Request["query"]}%'";
            }
            if (!string.IsNullOrEmpty(context.Request["filter"]))
            {
                Filter = context.Request["filter"];
            }
            if(!string.IsNullOrEmpty(context.Request["departmentIds"]))
            {
                DepartmentIds = context.Request["departmentIds"];
            }
            if(!string.IsNullOrEmpty(context.Request["start"]))
            {
                Start = Convert.ToInt32(context.Request["start"]);
            }
            if(!string.IsNullOrEmpty(context.Request["limit"]))
            {
                Limit = Convert.ToInt32(context.Request["limit"]);
            }
            if(!string.IsNullOrEmpty(context.Request["order"]))
            {
                Order = context.Request["order"];
            }

            // init page result
            var pageResult = RecordController.GetPaging(DepartmentIds, Keyword, Filter, Order, Start, Limit);

            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        public bool IsReusable => false;
    }
}