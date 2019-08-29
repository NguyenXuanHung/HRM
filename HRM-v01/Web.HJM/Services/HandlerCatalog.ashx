<%@ WebHandler Language="C#" Class="Web.HJM.Services.HandlerCatalog" %>

using System.Collections.Generic;
using System.Web;
using Web.Core;
using Web.Core.Object.Catalog;
using Web.Core.Service.Catalog;

namespace Web.HJM.Services
{
    public class HandlerCatalog : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {

            // init default value
            var start = 0;
            var limit = 200;
            var objName = string.Empty;
            var keyword = string.Empty;
            var itemType = string.Empty;

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
                keyword = SoftCore.Util.GetInstance().GetKeyword(context.Request["query"]);
            }
            if (!string.IsNullOrEmpty(context.Request["itemType"]))
            {
                itemType = context.Request["itemType"];
            }
            // init page result
            var pageResult = new PageResult<cat_Base>(0, new List<cat_Base>());
            // check table name
            if (!string.IsNullOrEmpty(objName))
                // select from table
                pageResult = cat_BaseServices.GetPaging(objName.EscapeQuote(), keyword, itemType, false, null, start, limit);
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