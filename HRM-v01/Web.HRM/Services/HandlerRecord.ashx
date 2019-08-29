<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerRecord" %>

using System;
using System.Web;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Services
{
    public class HandlerRecord : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {        
        private string _departments = string.Empty;
        private string _searchKey = string.Empty;
        private string _searchCondition = string.Empty;
        private string _query = string.Empty;

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
            if(!string.IsNullOrEmpty(context.Request["SearchKey"]))
            {
                _searchKey = context.Request["SearchKey"];
            }
            if(!string.IsNullOrEmpty(context.Request["Query"]))
            {
                _query = context.Request["Query"];
            }

            if(!string.IsNullOrEmpty(context.Request["departments"]))
            {
                _departments = context.Request["departments"];
            }
            
            var arrDepartment = string.IsNullOrEmpty(_departments) ? new string[] { }
                : _departments.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for(var i = 0; i < arrDepartment.Length; i++)
            {
                arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
            }

            var data = RecordController.GetAllListRecords(string.Join(",", arrDepartment), _searchKey ,_query, null, Start, Limit);
            var count = RecordController.GetAllListRecords(string.Join(",", arrDepartment), _searchKey ,_query, null, null, null).Rows.Count;
            
            //Response
            context.Response.ContentType = "text/json";
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

