<%@ WebHandler Language="C#" Class="Web.HJM.Services.HandlerDecisionSalary" %>

using System;
using System.Web;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Adapter;

namespace Web.HJM.Services
{
    public class HandlerDecisionSalary : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var start = 0;
            var limit = 10;
            var count = 0;
            string searchKey = string.Empty;
            string trangThai = "";//context.Request["trangThai"];
            string dsDv = context.Request["departments"];
            var departmentSelected = context.Request["departmentSelected"];
            if (!string.IsNullOrEmpty(context.Request["start"]))
            {
                start = int.Parse(context.Request["start"]);
            }
            if (!string.IsNullOrEmpty(context.Request["limit"]))
            {
                limit = int.Parse(context.Request["limit"]);
            }
            if (!string.IsNullOrEmpty(context.Request["searchKey"]))
            {
                searchKey = context.Request["searchKey"];
                searchKey = SoftCore.Util.GetInstance().GetKeyword(searchKey);
            }

            var arrOrgCode = string.IsNullOrEmpty(dsDv)
                ? new string[] { }
                : dsDv.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < arrOrgCode.Length; i++)
            {
                arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
            }

            var data = SQLHelper.ExecuteTable(SQLManagementSalaryAdapter.GetStore_DecisionSalary(string.Join(",", arrOrgCode), start, limit, searchKey, trangThai, departmentSelected));
            count = SQLHelper.ExecuteTable(SQLManagementSalaryAdapter.GetStore_DecisionSalary(string.Join(",", arrOrgCode), null, null, searchKey, trangThai, departmentSelected)).Rows.Count;
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