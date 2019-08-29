<%@ WebHandler Language="C#" Class="Web.HJM.Services.HandlerRecord" %>

using System;
using System.Web;
using Web.Core;
using Web.Core.Framework;

namespace Web.HJM.Services
{
    public class HandlerRecord : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var start = 0;
            var limit = 12;
            var departments = context.Request["departments"];
            var searchKey = string.Empty;
            var query = string.Empty;

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
                searchKey = context.Request["SearchKey"];
            }
            if (!string.IsNullOrEmpty(context.Request["Query"]))
            {
                query = context.Request["Query"];
            }
            
            var arrDepartment = string.IsNullOrEmpty(departments) ? new string[] { }
                : departments.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for(var i = 0; i < arrDepartment.Length; i++)
            {
                arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
            }

            var data = RecordController.GetAllListRecords(string.Join(",", arrDepartment), searchKey ,query, null, start, limit);
            var count = RecordController.GetAllListRecords(string.Join(",", arrDepartment), searchKey ,query, null, null, null).Rows.Count;
            
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), count));
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

