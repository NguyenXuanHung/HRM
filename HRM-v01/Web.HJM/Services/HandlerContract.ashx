<%@ WebHandler Language="C#" Class="Web.HJM.Services.HandlerContract" %>

using System;
using System.Web;
using Web.Core.Framework;
using Web.Core;
using Web.Core.Framework.Adapter;
using Web.Core.Framework.SQLAdapter;

namespace Web.HJM.Services
{
    public class HandlerContract : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            var searchKey = string.Empty;
            var departments = context.Request["departments"];
            var departmentSelected = context.Request["departmentSelected"];
            var query = string.Empty;
            int start = 0;
            int limit = 25;

            if (!string.IsNullOrEmpty(context.Request["start"]))
            {
                start = int.Parse(context.Request["start"]);
            }
            if (!string.IsNullOrEmpty(context.Request["limit"]))
            {
                string s = context.Request["limit"];
                limit = int.Parse(context.Request["limit"]);
            }
            
            if (!string.IsNullOrEmpty(context.Request["SearchKey"]))
            {
                searchKey = context.Request["SearchKey"].ToString();
            }
            if (!string.IsNullOrEmpty(context.Request["Query"]))
            {
                query = context.Request["Query"].ToString();
            }
            
            var arrOrgCode = string.IsNullOrEmpty(departments) ? new string[] { }
             : departments.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < arrOrgCode.Length; i++)
            {
                arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
            }

            var data = SQLHelper.ExecuteTable(SQLContractAdapter.GetStore_Contract(string.Join(",", arrOrgCode), start, limit, searchKey, query, departmentSelected));
            var countData = SQLHelper.ExecuteTable(SQLContractAdapter.GetStore_Contract(string.Join(",", arrOrgCode), null, null, searchKey, query, departmentSelected));
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), countData.Rows.Count));

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