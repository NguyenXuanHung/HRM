<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerSalaryDecision" %>

using System;
using System.Web;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Services
{
    public class HandlerSalaryDecision : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        // init default value        
        private string _keyword;
        private int? _recordId;
        private int? _contractId;
        private int? _groupQuantumId;
        private int? _quantumId;
        private SalaryDecisionType? _type;
        private SalaryDecisionStatus? _status;
        private string _order;
        private int _start;
        private int _limit;
        private string _departmentSelectedId;

        public void ProcessRequest(HttpContext context)
        {
            // set default paging
            _start = Start;
            _limit = Limit;            
            // get params value
            if(!string.IsNullOrEmpty(context.Request["start"]))
            {
                _start = Convert.ToInt32(context.Request["start"]);
            }
            if(!string.IsNullOrEmpty(context.Request["limit"]))
            {
                _limit = Convert.ToInt32(context.Request["limit"]);
            }
            if(!string.IsNullOrEmpty(context.Request["query"]))
            {
                _keyword = context.Request["query"];
            }
            if(!string.IsNullOrEmpty(context.Request["recordId"]))
            {
                _recordId = Convert.ToInt32(context.Request["recordId"]);
            }
            if(!string.IsNullOrEmpty(context.Request["contractId"]))
            {
                _contractId = Convert.ToInt32(context.Request["contractId"]);
            }
            if(!string.IsNullOrEmpty(context.Request["groupQuantumId"]))
            {
                _groupQuantumId = Convert.ToInt32(context.Request["groupQuantumId"]);
            }
            if(!string.IsNullOrEmpty(context.Request["quantumId"]))
            {
                _quantumId = Convert.ToInt32(context.Request["quantumId"]);
            }
            if(!string.IsNullOrEmpty(context.Request["type"]))
            {
                _type = (SalaryDecisionType) Convert.ToInt32(context.Request["type"]);
            }
            if(!string.IsNullOrEmpty(context.Request["status"]))
            {
                _status = (SalaryDecisionStatus) Convert.ToInt32(context.Request["status"]);
            }
            if(!string.IsNullOrEmpty(context.Request["order"]))
            {
                _order = context.Request["order"];
            }
            if(!string.IsNullOrEmpty(context.Request["departmentSelected"]))
            {
                _departmentSelectedId = context.Request["departmentSelected"];
            }
            // init page result
            var pageResult = SalaryDecisionController.GetPaging(_keyword, _departmentSelectedId, _recordId, _contractId, _groupQuantumId,
                _quantumId, _type, _status, null, null, false, _order, _start, _limit);

            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        public bool IsReusable => false;
    }
}