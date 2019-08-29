<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerScheduler" %>

using System;
using System.Web;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Object.Security;

namespace Web.HRM.Services
{
    public class HandlerScheduler : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        // init default value        
        private int? _schedulerType;
        private SchedulerRepeatType? _repeatType;
        private SchedulerStatus? _status;
        private SchedulerScope? _scope;

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
            if(!string.IsNullOrEmpty(context.Request["schedulerType"]))
            {
                _schedulerType = int.Parse(context.Request["schedulerType"]);
            }
            if(!string.IsNullOrEmpty(context.Request["repeatType"]))
            {
                _repeatType = (SchedulerRepeatType)Enum.Parse(typeof(SchedulerRepeatType), context.Request["repeatType"]);
            }
            if(!string.IsNullOrEmpty(context.Request["status"]))
            {
                _status = (SchedulerStatus)Enum.Parse(typeof(SchedulerStatus), context.Request["status"]);
            }
            if(!string.IsNullOrEmpty(context.Request["scope"]))
            {
                _scope = (SchedulerScope)Enum.Parse(typeof(SchedulerScope), context.Request["scope"]);
            }

            // init page result
            var pageResult = SchedulerController.GetPaging(null, _schedulerType, _repeatType, _status, null, _scope, null, null, null, Start, Limit);

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