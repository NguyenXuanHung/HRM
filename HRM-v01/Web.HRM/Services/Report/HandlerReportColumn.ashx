<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerReportColumn" %>

using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Services
{
    public class HandlerReportColumn : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        // init variables  
        private string _keyword;
        private int _reportId;
        private string _order;
        private int _start; 
        private int _limit;

        public void ProcessRequest(HttpContext context)
        {
            // order id of ReportColumnType
            var ids = new List<int> { 1, 3, 5, 4, 2 };

            // init paging argument
            _start = Start;
            _limit = Limit;

            // get params value
            if(!string.IsNullOrEmpty(context.Request["start"]))
            {
                _start = int.Parse(context.Request["start"]);
            }
            if(!string.IsNullOrEmpty(context.Request["limit"]))
            {
                _limit = int.Parse(context.Request["limit"]);
            }
            if(!string.IsNullOrEmpty(context.Request["query"]))
            {
                _keyword = context.Request["query"];
            }
            if(!string.IsNullOrEmpty(context.Request["reportId"]))
            {
                _reportId = int.Parse(context.Request["reportId"]);
            }
            if(!string.IsNullOrEmpty(context.Request["order"]))
            {
                _order = context.Request["order"];
            }

            // select from table
            var data = ReportColumnController.GetTree(_keyword, _reportId, null, null, null, null, false, _order, null).OrderBy(r => ids.IndexOf((int)r.Type)).ToList();

            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(data.Count, Ext.Net.JSON.Serialize(data)));
        }

        public bool IsReusable => false;
    }
}