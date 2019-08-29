<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerQuantum" %>

using System.Collections.Generic;
using System.Web;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Object.Catalog;

namespace Web.HRM.Services
{
    public class HandlerQuantum : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        // init default value     
        private string _keyword = string.Empty;

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
          
            if (!string.IsNullOrEmpty(context.Request["query"]))
            {
                _keyword = context.Request["query"];
            }
            // init page result
            var pageResult  = CatalogQuantumController.GetPaging(_keyword, null, null, CatalogStatus.Active, false, null, Start, Limit);
            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));

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