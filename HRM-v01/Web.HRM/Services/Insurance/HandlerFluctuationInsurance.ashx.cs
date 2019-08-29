using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Core;
using Web.Core.Framework.BaseControl;
using Web.Core.Framework.Controller;

namespace Web.HRM.Services.Insurance
{
    /// <summary>
    /// Summary description for HandlerFluctuationInsurance
    /// </summary>
    public class HandlerFluctuationInsurance : BaseHandler, IHttpHandler
    {
        /// <summary>
        /// Declare private variables
        /// </summary>
        private int _start;
        private int _limit;
        private string _keyWord;
        public void ProcessRequest(HttpContext context)
        {
            // init params
            _start = Start;
            _limit = Limit;

            // init params
            context.Response.ContentType = "text/plain";
           
            // start
            if (!string.IsNullOrEmpty(context.Request["start"]))
            {
                _start = int.Parse(context.Request["start"]);
            }

            // limit
            if (!string.IsNullOrEmpty(context.Request["limit"]))
            {
                _limit = int.Parse(context.Request["limit"]);
            }

            if (!string.IsNullOrEmpty(context.Request["query"]))
            {
                _keyWord = context.Request["query"];
            }
            var groupId = !string.IsNullOrEmpty(context.Request["group"])
                ? Convert.ToInt32(context.Request["group"])
                : 0;
            // select from db
            var pageResult = FluctuationInsuranceController.GetPaging(_keyWord, null, null, null, _start, _limit);

            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        public bool IsReusable => false;
    }
}