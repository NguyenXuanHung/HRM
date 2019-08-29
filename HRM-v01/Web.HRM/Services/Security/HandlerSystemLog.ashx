<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerSystemLog" %>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Services
{
    public class HandlerSystemLog : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        // init default value     
        private string _keyword;
        private SystemAction? _action;
        private SystemLogType? _type;
        private bool? _isException;
        private DateTime? _fromDate;
        private DateTime? _toDate;
        private string _order;
        private int _start;
        private int _limit;

        public void ProcessRequest(HttpContext context)
        {
            // set default paging
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
            if(!string.IsNullOrEmpty(context.Request["action"]))
            {
                _action = (SystemAction) Convert.ToInt32(context.Request["action"]);
            }
            if(!string.IsNullOrEmpty(context.Request["type"]))
            {
                _type = (SystemLogType) Convert.ToInt32(context.Request["type"]);
            }
            if(!string.IsNullOrEmpty(context.Request["isEx"]))
            {
                _isException = Convert.ToBoolean(context.Request["isEx"]);
            }
            if(DateTime.TryParseExact(context.Request["fromDate"], "yyyy-MM-ddTHH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.None, out var parseFromDate))
            {
                _fromDate = parseFromDate;
            }
            if(DateTime.TryParseExact(context.Request["toDate"], "yyyy-MM-ddTHH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.None, out var parseToDate))
            {
                _toDate = parseToDate;
            }
            if(!string.IsNullOrEmpty(context.Request["order"]))
            {
                _order = context.Request["order"];
            }
            else
            {
                //default
                _order = " [CreatedDate] DESC ";
            }
            // init page result
            var pageResult = SystemLogController.GetPaging(_keyword, _action, _type, _isException, _fromDate, _toDate, _order, _start, _limit);
            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        public bool IsReusable => false;
    }
}