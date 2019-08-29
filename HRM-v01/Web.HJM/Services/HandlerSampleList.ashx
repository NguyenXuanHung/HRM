<%@ WebHandler Language="C#" Class="Web.HJM.Services.HandlerSampleList" %>

using System;
using System.Collections.Generic;
using System.Web;
using Web.Core;
using Web.Core.Object.Sample;
using Web.Core.Service.Sample;

namespace Web.HJM.Services
{
    public class HandlerSampleList : IHttpHandler {

        public void ProcessRequest (HttpContext context) {
            var start = 0;
            var limit = 15;
            string departmentIds = string.Empty;
            if (!string.IsNullOrEmpty(context.Request["start"]))
            {
                start = int.Parse(context.Request["start"]);
            }
            if (!string.IsNullOrEmpty(context.Request["limit"]))
            {
                limit = int.Parse(context.Request["limit"]);
            }
            if (!string.IsNullOrEmpty(context.Request["Departments"]))
            {
                departmentIds = context.Request["Departments"];
            }

            // init page result
            var pageResult = new PageResult<SampleList>(0, new List<SampleList>());
            // select from table
            var arrDepartment = departmentIds.Split(new[] { ',' }, StringSplitOptions.None);
            for (var i = 0; i < arrDepartment.Length; i++)
            {
                arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
            }
            pageResult = SampleListServices.GetPaging(string.Join(",", arrDepartment), null, null, start, limit);
            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));

        }

        public bool IsReusable {
            get {
                return false;
            }
        }

    }
}
