<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerDecisionSalary" %>

using System;
using System.Web;
using Web.Core;
using Web.Core.Framework.Adapter;

namespace Web.HRM.Services
{
    public class HandlerDecisionSalary : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {        
        private int _count = 0;
        private string _searchKey = string.Empty;
        private string _status = string.Empty;
        private string _departments = string.Empty;
        private string _departmentSelected = string.Empty;

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
            if(!string.IsNullOrEmpty(context.Request["searchKey"]))
            {
                _searchKey = context.Request["searchKey"];
                _searchKey = SoftCore.Util.GetInstance().GetKeyword(_searchKey);
            }

            if(!string.IsNullOrEmpty(context.Request["trangThai"]))
            {
                _status = context.Request["trangThai"];
            }

            if(!string.IsNullOrEmpty(context.Request["departments"]))
            {
                _departments = context.Request["departments"];
            }

            if(!string.IsNullOrEmpty(context.Request["departmentSelected"]))
            {
                _departmentSelected = context.Request["departmentSelected"];
            }

            var arrOrgCode = string.IsNullOrEmpty(_departments)
                ? new string[] { }
                : _departments.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for(var i = 0; i < arrOrgCode.Length; i++)
            {
                arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
            }

            var data = SQLHelper.ExecuteTable(SQLManagementSalaryAdapter.GetStore_DecisionSalary(string.Join(",", arrOrgCode), Start, Limit, _searchKey, _status, _departmentSelected));
            _count = SQLHelper.ExecuteTable(SQLManagementSalaryAdapter.GetStore_DecisionSalary(string.Join(",", arrOrgCode), null, null, _searchKey, _status, _departmentSelected)).Rows.Count;
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), _count));
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