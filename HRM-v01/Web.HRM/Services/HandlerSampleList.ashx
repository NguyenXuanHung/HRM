<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerSampleList" %>

using System;
using System.Web;
using Web.Core;
using Web.Core.Service.Sample;

namespace Web.HRM.Services
{
    public class HandlerSampleList : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {        
        private string _departmentIds = string.Empty;

        public void ProcessRequest(HttpContext context)
        {
            if(!string.IsNullOrEmpty(context.Request["start"]))
            {
                Start = int.Parse(context.Request["start"]);
            }
            if(!string.IsNullOrEmpty(context.Request["limit"]))
            {
                Limit = int.Parse(context.Request["limit"]);
            }
            if(!string.IsNullOrEmpty(context.Request["Departments"]))
            {
                _departmentIds = context.Request["Departments"];
            }

            // init page result
            // select from table
            var arrDepartment = _departmentIds.Split(new[] { ',' }, StringSplitOptions.None);
            for(var i = 0; i < arrDepartment.Length; i++)
            {
                arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
            }
            var pageResult = SampleListServices.GetPaging(string.Join(",", arrDepartment), null, null, Start, Limit);
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
