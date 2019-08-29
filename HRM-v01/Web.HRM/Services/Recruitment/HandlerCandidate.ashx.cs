using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.BaseControl;

namespace Web.HRM.Services.Recruitment
{
    /// <summary>
    /// Summary description for HandlerCandidate
    /// </summary>
    public class HandlerCandidate : BaseHandler, IHttpHandler
    {
        private int _start;
        private int _limit;
        private string _keyword;
        private DateTime? _fromDate;
        private DateTime? _toDate;

        public void ProcessRequest(HttpContext context)
        {
            _start = Start;
            _limit = Limit;

            context.Response.ContentType = "text/plain";

            // start
            if(!string.IsNullOrEmpty(context.Request["start"]))
            {
                _start = int.Parse(context.Request["start"]);
            }

            // limit
            if(!string.IsNullOrEmpty(context.Request["limit"]))
            {
                _limit = int.Parse(context.Request["limit"]);
            }

            if(!string.IsNullOrEmpty(context.Request["keyword"]))
            {
                _keyword = context.Request["keyword"];
            }

            if (DateTime.TryParseExact(context.Request["fromDate"], "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out var parseFromDate))
            {
                _fromDate = parseFromDate;
            }
            if(DateTime.TryParseExact(context.Request["toDate"], "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out var parseToDate))
            {
                _toDate = parseToDate;
            }

            CandidateType? type = null;
            if (!string.IsNullOrEmpty(context.Request["candidateType"]))
            {
                type = (CandidateType) Enum.Parse( typeof(CandidateType),context.Request["candidateType"]);
            }

            var pageResult =
                CandidateController.GetPaging(_keyword, null, null, type, _fromDate, _toDate, false, null, _start, _limit);

            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        public bool IsReusable => false;
    }
}