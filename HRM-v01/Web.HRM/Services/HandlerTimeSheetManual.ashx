<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerTimeSheetManual" %>

using System;
using System.Web;
using Web.Core.Framework;
using Web.Core;

namespace Web.HRM.Services
{
    public class HandlerTimeSheetManual : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {        
        private int _numDay = 0;
        private int _year = 0;
        private int _month = 0;
        private string _departments = string.Empty;
        private string _searchKey = string.Empty;
        private string _departmentSelected = string.Empty;
        private int _count = 0;
        private int _timeSheetReportId = 0;

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
                _searchKey = "%" + SoftCore.Util.GetInstance().GetKeyword(context.Request["searchKey"]) + "%";
            }
            if(!string.IsNullOrEmpty(context.Request["departments"]))
            {
                _departments = context.Request["departments"];
            }
            if(!string.IsNullOrEmpty(context.Request["numDayOfMonth"]))
            {
                _numDay = int.Parse(context.Request["numDayOfMonth"]);
            }
            if(!string.IsNullOrEmpty(context.Request["year"]))
            {
                _year = int.Parse(context.Request["year"]);
            }
            if(!string.IsNullOrEmpty(context.Request["month"]))
            {
                _month = int.Parse(context.Request["month"]);
            }
            if(!string.IsNullOrEmpty(context.Request["departmentSelected"]))
            {
                _departmentSelected = context.Request["departmentSelected"];
            }
            if(!string.IsNullOrEmpty(context.Request["timeSheetReportId"]))
            {
                _timeSheetReportId = int.Parse(context.Request["timeSheetReportId"]);
            }

            var arrOrgCode = string.IsNullOrEmpty(_departments) ? new string[] { }
                : _departments.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for(var i = 0; i < arrOrgCode.Length; i++)
            {
                arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
            }

            // get time sheets
            var table = TimeSheetController.Get(string.Join(",", arrOrgCode), _departmentSelected, _searchKey, _month, _year, Start, Limit, null, _timeSheetReportId);
            _count = TimeSheetController.Get(string.Join(",", arrOrgCode), _departmentSelected, _searchKey, _month, _year, null, null, null, _timeSheetReportId).Count;

            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(_count, Ext.Net.JSON.Serialize(table)));
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
