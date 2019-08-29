<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerTimeSheetAutomatic" %>

using System;
using System.Web;
using System.Data;
using Web.Core.Framework;
using Web.Core;
using Web.Core.Framework.Controller.TimeSheet;

namespace Web.HRM.Services
{
    public class HandlerTimeSheetAutomatic : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        private const string TypeTimeSheet = "Automatic";

        private string _departments = string.Empty;
        private string _searchKey = string.Empty;
        private int _departmentSelected;
        private int _count;
        private int _timeSheetReportId;
        private DateTime? _startDate = null;
        private DateTime? _endDate = null;
        private string _recordIds = string.Empty;

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
            }
            if(!string.IsNullOrEmpty(context.Request["departments"]))
            {
                _departments = context.Request["departments"];
            }
            if (!string.IsNullOrEmpty(context.Request["startDate"]))
            {
                _startDate = context.Request["startDate"].ToVnTime("dd/MM/yyyy hh:mm:ss tt");
            }
            if (!string.IsNullOrEmpty(context.Request["endDate"]))
            {
                _endDate = context.Request["endDate"].ToVnTime("dd/MM/yyyy hh:mm:ss tt");
            }
            if(!string.IsNullOrEmpty(context.Request["departmentSelected"]))
            {
                _departmentSelected = int.Parse(context.Request["departmentSelected"]);
            }
            if(!string.IsNullOrEmpty(context.Request["timeSheetReportId"]))
            {
                _timeSheetReportId = int.Parse(context.Request["timeSheetReportId"]);
            }
            if (!string.IsNullOrEmpty(context.Request["recordIds"]))
            {
                _recordIds = context.Request["recordIds"];
            }

            var pageResult = TimeSheetEventController.GetReportDetail(_searchKey, _recordIds, _startDate, _endDate, Start,
                Limit);

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
