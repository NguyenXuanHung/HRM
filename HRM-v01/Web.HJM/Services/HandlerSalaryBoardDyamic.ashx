<%@ WebHandler Language="C#" Class="Web.HJM.Services.HandlerSalaryBoardDyamic" %>

using System;
using System.Web;
using Web.Core;
using Web.Core.Framework.Controller;

namespace Web.HJM.Services
{
    public class HandlerSalaryBoardDyamic : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var start = 0;
            var limit = 10;
            
            var year = 0;
            var month = 0;
            var departments = string.Empty;
            var searchKey = string.Empty;
            var salaryBoardId = 0;
            var count = 0;
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
                searchKey = "%" + SoftCore.Util.GetInstance().GetKeyword(context.Request["searchKey"]) + "%";
            }
            if (!string.IsNullOrEmpty(context.Request["departments"]))
            {
                departments = context.Request["departments"];
            }
          
            if (!string.IsNullOrEmpty(context.Request["year"]))
            {
                year = int.Parse(context.Request["year"]);
            }
            if (!string.IsNullOrEmpty(context.Request["month"]))
            {
                month = int.Parse(context.Request["month"]);
            }

            if (!string.IsNullOrEmpty(context.Request["salaryBoardId"]))
                salaryBoardId = int.Parse(context.Request["salaryBoardId"]);

            var arrOrgCode = string.IsNullOrEmpty(departments) ? new string[] { }
                : departments.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < arrOrgCode.Length; i++)
            {
                arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
            }
            var table = SalaryBoardDynamicController.GetBoardDynamicList(searchKey, salaryBoardId, start, limit);
            count = SalaryBoardDynamicController.GetBoardDynamicList(searchKey, salaryBoardId, null, null).Count;
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(count, Ext.Net.JSON.Serialize(table)));
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
