<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerTimeSheetRuleWrongTime" %>

using System.Collections.Generic;
using System.Web;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Services
{
    public class HandlerTimeSheetRuleWrongTime : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        // init variables      
        private string _keyword;
        private int _start;
        private int _limit;

        private int? _symbolId;
        private bool? _isMinus;
        private TimeSheetRuleWrongTimeType? _type;
        private string _order;
       
        public void ProcessRequest(HttpContext context)
        {
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
            if(!string.IsNullOrEmpty(context.Request["symbolId"]))
            {
                _symbolId = int.Parse(context.Request["symbolId"]);
            }
            if(!string.IsNullOrEmpty(context.Request["isMinus"]))
            {
                _isMinus = bool.Parse(context.Request["isMinus"]);
            }
            if(!string.IsNullOrEmpty(context.Request["type"]))
            {
                _type = (TimeSheetRuleWrongTimeType)int.Parse(context.Request["type"]);
            }
            if(!string.IsNullOrEmpty(context.Request["order"]))
            {
                _order = context.Request["order"];
            }

            // select from db
            var pageResult = TimeSheetRuleWrongTimeController.GetPaging(_symbolId, _isMinus, _type, false, _order, _start, _limit);
                
            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        public bool IsReusable => false;
    }
}