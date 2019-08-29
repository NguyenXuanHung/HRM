using System;
using System.Globalization;
using System.Reflection;
using System.Web;
using SmartXLS;
using Web.Core;
using Web.Core.Framework.Controller;

namespace Web.HRM.Services.Salary
{
    /// <summary>
    /// Summary description for HandlerPayrollDetail
    /// </summary>
    public class HandlerPayrollDetail : IHttpHandler
    {
        private int _payrollId;
        private string _keyword;

        public void ProcessRequest(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request["payrollId"]))
                _payrollId = int.Parse(context.Request["payrollId"]);
            if (!string.IsNullOrEmpty(context.Request["keyword"]))
                _keyword = context.Request["keyword"];

            var result = PayrollController.GetPayrollDetail(_keyword, _payrollId, null, null);
            
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(result.Rows.Count, Ext.Net.JSON.Serialize(result)));
        }

        public bool IsReusable => false;
    }
}