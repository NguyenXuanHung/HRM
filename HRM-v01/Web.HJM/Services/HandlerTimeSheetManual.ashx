<%@ WebHandler Language="C#" Class="Web.HJM.Services.HandlerTimeSheetManual" %>

using System;
using System.Web;
using System.Data;
using Web.Core.Framework;
using Web.Core;

namespace Web.HJM.Services
{
    public class HandlerTimeSheetManual : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var start = 0;
            var limit = 10;
            var numDay = 0;
            var year = 0;
            var month = 0;
            var departments = string.Empty;
            var searchKey = string.Empty;
            var departmentSelected = string.Empty;
            var count = 0;
            var timeSheetReportId = 0;

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
            if (!string.IsNullOrEmpty(context.Request["numDayOfMonth"]))
            {
                numDay = int.Parse(context.Request["numDayOfMonth"]);
            }
            if (!string.IsNullOrEmpty(context.Request["year"]))
            {
                year = int.Parse(context.Request["year"]);
            }
            if (!string.IsNullOrEmpty(context.Request["month"]))
            {
                month = int.Parse(context.Request["month"]);
            }
            if (!string.IsNullOrEmpty(context.Request["departmentSelected"]))
            {
                departmentSelected = context.Request["departmentSelected"];
            }
            if (!string.IsNullOrEmpty(context.Request["timeSheetReportId"]))
            {
                timeSheetReportId = int.Parse(context.Request["timeSheetReportId"]);
            }

            var arrOrgCode = string.IsNullOrEmpty(departments) ? new string[] { }
                : departments.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < arrOrgCode.Length; i++)
            {
                arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
            }
            var table = TimeSheetController.Get(string.Join(",", arrOrgCode), departmentSelected, searchKey, month, year, start, limit, null, timeSheetReportId);
            count = TimeSheetController.Get(string.Join(",", arrOrgCode), departmentSelected, searchKey, month, year, null, null, null, timeSheetReportId).Count;
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
