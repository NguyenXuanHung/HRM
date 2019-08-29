<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerPayroll" %>

using System.Web;
using Web.Core;
using Web.Core.Framework.Controller;

namespace Web.HRM.Services
{
    public class HandlerPayroll : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        /// <summary>
        /// Declare private variables
        /// </summary>
        private int? _configId;
        private string _departments;
        private string _order;
        private int _start;
        private int _limit;

        /// <inheritdoc />
        /// <summary>
        /// Process request
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            // init params
            _start = Start;
            _limit = Limit;

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

            // config id
            if (int.TryParse(context.Request["configId"], out var parseConfigId) && parseConfigId > 0)
            {
                _configId = parseConfigId;
            }

            // department
            if(!string.IsNullOrEmpty(context.Request["departments"]))
            {
                _departments = context.Request["departments"];
            }

            // order, set default order by year, month
            _order = !string.IsNullOrEmpty(context.Request["order"]) ? context.Request["order"] : "[Year],[Month] DESC";

            // select from db
            var pageResult = PayrollController.GetPaging(null, _configId, _departments, null, null, null, false, _order, _start, _limit);

            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        /// <inheritdoc />
        /// <summary>
        /// Reusable
        /// </summary>
        public bool IsReusable => false;
    }
}