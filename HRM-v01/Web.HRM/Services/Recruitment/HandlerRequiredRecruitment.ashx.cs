using System;
using System.Web;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.BaseControl;

namespace Web.HRM.Services.Recruitment
{
    /// <summary>
    /// Summary description for HandlerRequiredRecruitment
    /// </summary>
    public class HandlerRequiredRecruitment : BaseHandler, IHttpHandler
    {
        /// <summary>
        /// Declare private variables
        /// </summary>
        private int _start;
        private int _limit;
        private string _keyWord;
        private string _order = "";

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

            if (!string.IsNullOrEmpty(context.Request["order"]))
            {
                _order = context.Request["order"];
            }
            else
            {
                //default
                _order = " [EditedDate] DESC, [CreatedDate] DESC ";
            }

            RecruitmentStatus? status = null;
            if (!string.IsNullOrEmpty(context.Request["status"]))
            {
                status = (RecruitmentStatus) Enum.Parse(typeof(RecruitmentStatus), context.Request["status"]) ;
            }
            //TH all
            if (status == RecruitmentStatus.All)
            {
                status = null;
            }
            // select from db
            var pageResult = RequiredRecruitmentController.GetPaging(_keyWord, null, null, null, null, false, status,
                null, null, _order, _start, _limit);
            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        public bool IsReusable => false;
    }
}