using System.Web;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Services.TimeSheet
{
    /// <summary>
    /// Summary description for HandlerTimeSheetWorkShiftTemplate
    /// </summary>
    public class HandlerTimeSheetWorkShiftTemplate : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        // init variables      
        private string _keyword;
        private int _start;
        private int _limit;
        private string _order;
        public void ProcessRequest(HttpContext context)
        {
            // init paging argument
            _start = Start;
            _limit = Limit;

            // get params value
            if (!string.IsNullOrEmpty(context.Request["start"]))
            {
                _start = int.Parse(context.Request["start"]);
            }
            if (!string.IsNullOrEmpty(context.Request["limit"]))
            {
                _limit = int.Parse(context.Request["limit"]);
            }
            if (!string.IsNullOrEmpty(context.Request["query"]))
            {
                _keyword = context.Request["query"];
            }
           
            if (!string.IsNullOrEmpty(context.Request["order"]))
            {
                _order = context.Request["order"];
            }
            else
            {
                _order = "[EditedDate] DESC,[CreatedDate] DESC ";
            }
            // select from db
            var pageResult = TimeSheetWorkShiftTemplateController.GetPaging(_keyword, false, null, null, null, null, _order, _start, _limit);

            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        public bool IsReusable => false;
    }
}