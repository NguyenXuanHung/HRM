<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerContract" %>

using System;
using System.Web;
using Web.Core;
using Web.Core.Framework.Adapter;
using Web.Core.Framework.SQLAdapter;

namespace Web.HRM.Services
{
    public class HandlerContract : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        private string _searchKey = string.Empty;
        private string _departments = string.Empty;
        private string _departmentSelected = string.Empty;
        private string _query = string.Empty;        

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";

            _departments = context.Request["departments"];
            _departmentSelected = context.Request["departmentSelected"];

            if(!string.IsNullOrEmpty(context.Request["start"]))
            {
                Start = int.Parse(context.Request["start"]);
            }
            if(!string.IsNullOrEmpty(context.Request["limit"]))
            {
                var s = context.Request["limit"];
                Limit = int.Parse(context.Request["limit"]);
            }

            if(!string.IsNullOrEmpty(context.Request["SearchKey"]))
            {
                _searchKey = context.Request["SearchKey"].ToString();
            }
            if(!string.IsNullOrEmpty(context.Request["Query"]))
            {
                _query = context.Request["Query"].ToString();
            }

            var arrOrgCode = string.IsNullOrEmpty(_departments) ? new string[] { }
             : _departments.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for(var i = 0; i < arrOrgCode.Length; i++)
            {
                arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
            }

            var data = SQLHelper.ExecuteTable(SQLContractAdapter.GetStore_Contract(string.Join(",", arrOrgCode), Start, Limit, _searchKey, _query, _departmentSelected));
            var countData = SQLHelper.ExecuteTable(SQLContractAdapter.GetStore_Contract(string.Join(",", arrOrgCode), null, null, _searchKey, _query, _departmentSelected));
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