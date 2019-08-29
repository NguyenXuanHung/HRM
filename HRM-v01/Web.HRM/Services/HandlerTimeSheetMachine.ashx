<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerTimeSheetMachine" %>

using System.Collections.Generic;
using System.Web;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Services
{
    public class HandlerTimeSheetMachine : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        // init default value      
        private string _keyword;
        private string _order;
        private int _start; 
        private int _limit;

        public void ProcessRequest(HttpContext context)
        {
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
            if(!string.IsNullOrEmpty(context.Request["order"]))
            {
                _order = context.Request["order"];
            }
            // select from db
            var pageResult = TimeSheetMachineController.GetPaging(_keyword, null, null, null, null, _order, _start, _limit);
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