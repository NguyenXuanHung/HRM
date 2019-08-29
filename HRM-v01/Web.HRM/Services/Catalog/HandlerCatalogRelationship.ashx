<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerCatalogRelationship" %>

using System.Web;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Services
{
    public class HandlerCatalogRelationship : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        private CatalogStatus? _status = null;
        public void ProcessRequest(HttpContext context)
        {            
            // get params value
            if(!string.IsNullOrEmpty(context.Request["start"]))
            {
                Start = int.Parse(context.Request["start"]);
            }
            if(!string.IsNullOrEmpty(context.Request["limit"]))
            {
                Limit = int.Parse(context.Request["limit"]);
            }
            if(!string.IsNullOrEmpty(context.Request["query"]))
            {
                Keyword = context.Request["query"];
            }
            if(!string.IsNullOrEmpty(context.Request["status"]))
            {
                _status = (CatalogStatus)int.Parse(context.Request["status"]);
            }
            // check table name
            var pageResult = CatalogRelationshipController.GetPaging(Keyword, null, _status, false, null, Start, Limit);
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