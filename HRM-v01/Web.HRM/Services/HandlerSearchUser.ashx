<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerSearchUser" %>
using System;
using System.Web;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Services
{
    public class HandlerSearchUser : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {        
        private string _query = string.Empty;
        private string _departments = string.Empty;

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
            }
            if(!string.IsNullOrEmpty(context.Request["Query"]))
            {
                _query = context.Request["Query"].ToString();
            }
            if(!string.IsNullOrEmpty(context.Request["departments"]))
            {
                _departments = context.Request["departments"];
            }
            var arrDepartment = _departments.Split(new[] { ',' }, StringSplitOptions.None);
            for(var i = 0; i < arrDepartment.Length; i++)
            {
                arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
            }

            var searchCondition = @" AND [DepartmentId] IN ({0}) ".FormatWith(string.Join(",", arrDepartment));

            var pageResult = RecordController.GetPagingSearchUserReport(searchCondition, _query, null, Start, Limit);
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

