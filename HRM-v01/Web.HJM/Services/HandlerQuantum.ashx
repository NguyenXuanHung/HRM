<%@ WebHandler Language="C#" Class="Web.HJM.Services.HandlerQuantum" %>

using System;
using System.Collections.Generic;
using System.Web;
using Web.Core;
using Web.Core.Object.Catalog;
using Web.Core.Service.Catalog;

namespace Web.HJM.Services
{
    public class HandlerQuantum : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {

            // init default value
            var start = 0;
            var limit = 50;
            var objName = string.Empty;
            var keyword = string.Empty;
            // get params value
            if (!string.IsNullOrEmpty(context.Request["start"]))
            {
                start = int.Parse(context.Request["start"]);
            }
            if (!string.IsNullOrEmpty(context.Request["limit"]))
            {
                limit = int.Parse(context.Request["limit"]);
            }
            if (!string.IsNullOrEmpty(context.Request["objname"]))
            {
                objName = context.Request["objname"];
            }
            if (!string.IsNullOrEmpty(context.Request["query"]))
            {
                keyword = context.Request["query"];
            }
            // init page result
            var pageResult = new PageResult<cat_Quantum>(0, new List<cat_Quantum>());
            // check table name
            if (!string.IsNullOrEmpty(objName))
                // select from table
                pageResult = cat_QuantumServices.GetPaging(keyword, false, null, start, limit);
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