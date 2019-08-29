<%@ WebHandler Language="C#" Class="Web.HRM.Services.FilterConditionHandler" %>

using System.Web;
using Web.Core;

namespace Web.HRM.Services
{
    public class FilterConditionHandler : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {        
        private string _sort = string.Empty;
        private string _dir = string.Empty;
        private string _query = string.Empty;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            
            if (!string.IsNullOrEmpty(context.Request["start"]))
            {
                Start = int.Parse(context.Request["start"]);
            }

            if (!string.IsNullOrEmpty(context.Request["limit"]))
            {
                Limit = int.Parse(context.Request["limit"]);
            }

            if (!string.IsNullOrEmpty(context.Request["sort"]))
            {
                _sort = context.Request["sort"];
            }

            if (!string.IsNullOrEmpty(context.Request["dir"]))
            {
                _dir = context.Request["dir"];
            }

            if (!string.IsNullOrEmpty(context.Request["query"]))
            {
                _query = "%" + SoftCore.Util.GetInstance().GetKeyword(context.Request["query"]) + "%";
            }
            const int count = 0;
            var data = SQLHelper.ExecuteTable("SELECT * FROM ReportTableFilter");
            context.Response.Write(string.Format("{{total:{1},'plants':{0}}}", Ext.Net.JSON.Serialize(data), count));
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
