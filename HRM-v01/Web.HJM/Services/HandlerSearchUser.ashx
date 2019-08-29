<%@ WebHandler Language="C#" Class="Web.HJM.Services.HandlerSearchUser" %>
using System;
using System.Web;
using Web.Core;
using Web.Core.Framework;

namespace Web.HJM.Services
{
    public class HandlerSearchUser : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var start = 0;
            var limit = 200;
            var SearchKey = string.Empty;
            var searchCondition = string.Empty;
            var Query = string.Empty;
            var departments = string.Empty;

            if (!string.IsNullOrEmpty(context.Request["start"]))
            {
                start = int.Parse(context.Request["start"]);
            }
            if (!string.IsNullOrEmpty(context.Request["limit"]))
            {
                limit = int.Parse(context.Request["limit"]);
            }
            if (!string.IsNullOrEmpty(context.Request["SearchKey"]))
            {
                SearchKey = context.Request["SearchKey"].ToString();
            }
            if (!string.IsNullOrEmpty(context.Request["Query"]))
            {
                Query = context.Request["Query"].ToString();
            }
            if (!string.IsNullOrEmpty(context.Request["departments"]))
            {
                departments = context.Request["departments"];
            }
             var arrDepartment = departments.Split(new[] { ',' }, StringSplitOptions.None);
            for (var i = 0; i < arrDepartment.Length; i++)
            {
                arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
            }

            searchCondition = @" AND [DepartmentId] IN ({0}) ".FormatWith(string.Join(",", arrDepartment));

            var pageResult = RecordController.GetPagingSearchUserReport(searchCondition ,Query, null, start, limit);
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

