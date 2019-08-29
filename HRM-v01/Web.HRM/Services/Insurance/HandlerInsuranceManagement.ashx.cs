using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.BaseControl;

namespace Web.HRM.Services.Insurance
{
    /// <summary>
    /// Summary description for HandlerInsuranceManagement
    /// </summary>
    public class HandlerInsuranceManagement : BaseHandler, IHttpHandler
    {

        /// <summary>
        /// Declare private variables
        /// </summary>
        private int _start;
        private int _limit;
        private string _keyword;

        public void ProcessRequest(HttpContext context)
        {
            // init params
            _start = Start;
            _limit = Limit;
            var recordIds = string.Empty;
            var order = string.Empty;
            int? positionId = null;
            var departmentId = string.Empty;
            DateTime? fromDate = null;
            DateTime? toDate = null;

            // init params
            context.Response.ContentType = "text/plain";

            // start
            if(!string.IsNullOrEmpty(context.Request["start"]))
                _start = int.Parse(context.Request["start"]);

            // limit
            if(!string.IsNullOrEmpty(context.Request["limit"]))
                _limit = int.Parse(context.Request["limit"]);

            if (!string.IsNullOrEmpty(context.Request["order"]))
                order = context.Request["order"];

            if(!string.IsNullOrEmpty(context.Request["keyword"]))
                _keyword = context.Request["keyword"];

            if (int.TryParse(context.Request["positionId"], out var positionIdResult))
                positionId = positionIdResult;

            if (!string.IsNullOrEmpty(context.Request["departmentIds"]))
                departmentId = context.Request["departmentIds"];

            var pageResult = RecordController.GetInsurance(_keyword, departmentId, fromDate, toDate, order, _start, _limit);

            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
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